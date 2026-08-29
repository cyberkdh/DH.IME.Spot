//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: BadgeController
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DH.IME.Spot.Core;
using DH.IME.Spot.Interop;
using DH.IME.Spot.Overlay.Placement;

namespace DH.IME.Spot.Overlay {
	internal sealed class BadgeController : IDisposable {
		private const int TRACK_INTERVAL_MS = 120;
		private const int CURSOR_INTERVAL_MS = 16;
		private const int CURSOR_IDLE_INTERVAL_MS = 125;
		private const int CURSOR_IDLE_TICKS = 40;
		private const int OFFSCREEN_THRESHOLD = -30000;

		private readonly Timer m_tmrTrack;
		private readonly Timer m_tmrCursor;
		private readonly int m_nOwnProcessId;
		private readonly Slot[] m_arrSlots;
		private readonly Slot m_cursorSlot;
		private readonly List<Slot> m_lstMonitorSlots;

		private int m_nBackgroundAlpha;
		private int m_nCursorIdleTicks;
		private IntPtr m_hForegroundHint;
		private bool m_bForegroundHintValid;
		private ImeState m_state;
		private Point m_ptLastCursor;
		private bool m_bStarted;
		private bool m_bPaused;
		private bool m_bDisposed;
		private bool m_bMonitorActive;
		private enumBadgeCorner m_eMonitorCorner;
		private enumMonitorWidgetScope m_eMonitorScope;

		public BadgeController(AppSettings settings) {
			m_nOwnProcessId = Process.GetCurrentProcess().Id;
			m_state = ImeState.Unknown;

			m_arrSlots = new[] {
				new Slot(enumDisplayMode.ActiveWindowCorner),
				new Slot(enumDisplayMode.CursorCompanion)
			};

			m_cursorSlot = m_arrSlots[1];
			m_lstMonitorSlots = new List<Slot>();
			m_ptLastCursor = new Point(int.MinValue, int.MinValue);

			m_tmrTrack = new Timer();
			m_tmrTrack.Interval = TRACK_INTERVAL_MS;
			m_tmrTrack.Tick += OnTrackTick;

			m_tmrCursor = new Timer();
			m_tmrCursor.Interval = CURSOR_INTERVAL_MS;
			m_tmrCursor.Tick += OnCursorTick;

			ApplySettings(settings ?? AppSettings.Defaults());
		}

		public void Start() {
			if (m_bDisposed == true) {
				return;
			}

			m_bStarted = true;
			m_tmrTrack.Start();
			UpdateCursorTimer();
		}

		public void SetState(ImeState state) {
			SetState(state, IntPtr.Zero);
		}

		public void SetState(ImeState state, IntPtr hforeground) {
			m_state = state;
			m_hForegroundHint = hforeground;
			m_bForegroundHintValid = hforeground != IntPtr.Zero;
			Refresh();
			m_bForegroundHintValid = false;
		}

		public void ForceRefresh() {
			if (m_bDisposed == true) {
				return;
			}

			foreach (Slot slot in m_arrSlots) {
				slot.HasVisual = false;
			}

			foreach (Slot slot in m_lstMonitorSlots) {
				slot.HasVisual = false;
			}

			Refresh();
		}

		public bool IsPaused {
			get { return m_bPaused; }
		}

		public void SetPaused(bool bpaused) {
			if (m_bDisposed == true || m_bPaused == bpaused) {
				return;
			}

			m_bPaused = bpaused;
			UpdateCursorTimer();
			Refresh();
		}

		public void ApplySettings(AppSettings settings) {
			if (m_bDisposed == true || settings == null) {
				return;
			}

			AppSettings copy = settings.Clone();
			copy.Normalize();

			m_nBackgroundAlpha = copy.BackgroundAlpha;

			foreach (Slot slot in m_arrSlots) {
				switch (slot.Mode) {
					case enumDisplayMode.CursorCompanion:
						slot.Active = copy.CursorEnabled;
						slot.Placement = new CursorCompanionPlacement();
						slot.SizeScale = AppSettings.ScaleOf(copy.CursorBadgeSize);
						break;
					default:
						slot.Active = copy.ActiveWindowEnabled;
						slot.Placement = new ActiveWindowCornerPlacement(copy.ActiveWindowCorner);
						slot.SizeScale = 1.0;
						break;
				}

				slot.HasVisual = false;

				if (slot.Active == false) {
					HideSlot(slot);
				}
			}

			m_bMonitorActive = copy.MonitorWidgetEnabled;
			m_eMonitorCorner = copy.MonitorWidgetCorner;
			m_eMonitorScope = copy.MonitorWidgetScope;

			foreach (Slot slot in m_lstMonitorSlots) {
				slot.Placement = new PerMonitorWidgetPlacement(m_eMonitorCorner);
				slot.HasVisual = false;
			}

			if (m_bMonitorActive == false) {
				HideMonitorSlots();
			}

			UpdateCursorTimer();
			Refresh();
		}

		private void UpdateCursorTimer() {
			bool bshouldrun = m_bStarted == true && m_bDisposed == false && m_bPaused == false && m_cursorSlot.Active == true;
			if (bshouldrun == true) {
				if (m_tmrCursor.Enabled == false) {
					m_nCursorIdleTicks = 0;
					m_tmrCursor.Interval = CURSOR_INTERVAL_MS;
				}

				m_tmrCursor.Start();
			}
			else {
				m_tmrCursor.Stop();
			}
		}

		private void OnTrackTick(object sender, EventArgs e) {
			Refresh();
		}

		private void OnCursorTick(object sender, EventArgs e) {
			if (m_bDisposed == true || m_bPaused == true) {
				return;
			}

			Slot slot = m_cursorSlot;
			if (slot.Active == false || m_state.Kind == enumImeKind.Unknown) {
				return;
			}

			Point ptcursor;
			if (TryGetCursor(out ptcursor) == false) {
				return;
			}

			if (ptcursor == m_ptLastCursor) {
				if (m_nCursorIdleTicks < CURSOR_IDLE_TICKS) {
					m_nCursorIdleTicks = m_nCursorIdleTicks + 1;
					if (m_nCursorIdleTicks >= CURSOR_IDLE_TICKS && m_tmrCursor.Interval != CURSOR_IDLE_INTERVAL_MS) {
						m_tmrCursor.Interval = CURSOR_IDLE_INTERVAL_MS;
					}
				}

				return;
			}

			m_nCursorIdleTicks = 0;
			if (m_tmrCursor.Interval != CURSOR_INTERVAL_MS) {
				m_tmrCursor.Interval = CURSOR_INTERVAL_MS;
			}

			m_ptLastCursor = ptcursor;

			if (slot.Visible == false || slot.Current == null || slot.HasVisual == false) {
				return;
			}

			MonitorMetrics monitor = MonitorInfo.ForPoint(ptcursor);
			int nsize = BadgeRenderer.MeasureSize(monitor, slot.SizeScale);
			VisualKey visualkey = new VisualKey(m_state, monitor.Dpi, nsize, m_nBackgroundAlpha);

			if (visualkey.Equals(slot.Visual) == false) {
				RenderSlot(slot, new PlacementContext { Cursor = ptcursor, Monitor = monitor });
				return;
			}

			PlacementContext context = new PlacementContext {
				Cursor = ptcursor,
				Monitor = monitor,
				BadgeSize = new Size(nsize, nsize)
			};

			Point ptlocation = slot.Placement.GetLocation(context);
			if (ptlocation == slot.Location) {
				return;
			}

			slot.Window.MoveTo(ptlocation);
			slot.Location = ptlocation;
		}

		private void Refresh() {
			if (m_bDisposed == true) {
				return;
			}

			if (m_bPaused == true || m_state.Kind == enumImeKind.Unknown || IsForegroundFullscreen() == true) {
				foreach (Slot slot in m_arrSlots) {
					HideSlot(slot);
				}

				HideMonitorSlots();
				return;
			}

			Point ptcursor;
			if (TryGetCursor(out ptcursor) == false) {
				ptcursor = Point.Empty;
			}

			bool bforegroundresolved = false;
			bool bforegroundvalid = false;
			RECT rcwindow = default(RECT);
			MonitorMetrics monitorwindow = default(MonitorMetrics);

			foreach (Slot slot in m_arrSlots) {
				if (slot.Active == false) {
					continue;
				}

				PlacementContext context = new PlacementContext { Cursor = ptcursor };

				if (slot.Mode == enumDisplayMode.CursorCompanion) {
					context.Monitor = MonitorInfo.ForPoint(ptcursor);
				}
				else {
					if (bforegroundresolved == false) {
						bforegroundvalid = TryResolveForeground(out rcwindow, out monitorwindow);
						bforegroundresolved = true;
					}

					if (bforegroundvalid == false) {
						HideSlot(slot);
						continue;
					}

					context.WindowRect = rcwindow;
					context.Monitor = monitorwindow;
				}

				RenderSlot(slot, context);
			}

			RefreshMonitorWidgets();
		}

		private void RefreshMonitorWidgets() {
			if (m_bMonitorActive == false) {
				HideMonitorSlots();
				return;
			}

			if (m_eMonitorScope == enumMonitorWidgetScope.AllMonitors) {
				Screen[] arrscreens = Screen.AllScreens;
				EnsureMonitorSlotCount(arrscreens.Length);

				for (int i = 0; i < arrscreens.Length; i++) {
					Rectangle rcbounds = arrscreens[i].Bounds;
					Point ptcenter = new Point(rcbounds.Left + rcbounds.Width / 2, rcbounds.Top + rcbounds.Height / 2);
					MonitorMetrics monitor = MonitorInfo.ForPoint(ptcenter);
					RenderSlot(m_lstMonitorSlots[i], new PlacementContext { Monitor = monitor });
				}

				return;
			}

			EnsureMonitorSlotCount(1);

			Point ptcursor;
			if (TryGetCursor(out ptcursor) == false) {
				ptcursor = Point.Empty;
			}

			MonitorMetrics monitorcursor = MonitorInfo.ForPoint(ptcursor);
			RenderSlot(m_lstMonitorSlots[0], new PlacementContext { Monitor = monitorcursor });
		}

		private void EnsureMonitorSlotCount(int ncount) {
			while (m_lstMonitorSlots.Count < ncount) {
				Slot slot = new Slot(enumDisplayMode.PerMonitorWidget);
				slot.Placement = new PerMonitorWidgetPlacement(m_eMonitorCorner);
				slot.Active = true;
				m_lstMonitorSlots.Add(slot);
			}

			while (m_lstMonitorSlots.Count > ncount) {
				int nlast = m_lstMonitorSlots.Count - 1;
				Slot slot = m_lstMonitorSlots[nlast];

				if (slot.Current != null) {
					slot.Current.Dispose();
					slot.Current = null;
				}

				slot.Window.Dispose();
				m_lstMonitorSlots.RemoveAt(nlast);
			}
		}

		private void HideMonitorSlots() {
			foreach (Slot slot in m_lstMonitorSlots) {
				HideSlot(slot);
			}
		}

		private void RenderSlot(Slot slot, PlacementContext context) {
			int nsize = BadgeRenderer.MeasureSize(context.Monitor, slot.SizeScale);
			context.BadgeSize = new Size(nsize, nsize);

			Point ptlocation = slot.Placement.GetLocation(context);

			VisualKey visualkey = new VisualKey(m_state, context.Monitor.Dpi, nsize, m_nBackgroundAlpha);
			bool bvisualchanged = slot.HasVisual == false || visualkey.Equals(slot.Visual) == false;
			bool bmoved = slot.Visible == false || ptlocation != slot.Location;
			if (bvisualchanged == false && bmoved == false) {
				return;
			}

			bool bcontentchanged = bvisualchanged == true || slot.Current == null;
			if (bcontentchanged == true) {
				Bitmap bm = BadgeRenderer.Render(m_state, context.Monitor, m_nBackgroundAlpha, nsize);
				if (slot.Current != null) {
					slot.Current.Dispose();
				}

				slot.Current = bm;
				slot.Visual = visualkey;
				slot.HasVisual = true;
			}

			if (bcontentchanged == true || slot.Visible == false) {
				slot.Window.SetContent(slot.Current, ptlocation);
				slot.Window.ShowNoActivate();
			}
			else {
				slot.Window.MoveTo(ptlocation);
			}

			slot.Location = ptlocation;
			slot.Visible = true;
		}

		private bool TryResolveForeground(out RECT rcwindow, out MonitorMetrics monitor) {
			rcwindow = default(RECT);
			monitor = default(MonitorMetrics);

			IntPtr hhwnd = m_bForegroundHintValid == true && NativeMethods.IsWindow(m_hForegroundHint) == true
				? m_hForegroundHint
				: NativeMethods.GetForegroundWindow();
			if (hhwnd == IntPtr.Zero || IsOwnWindow(hhwnd) == true) {
				return false;
			}

			RECT rc;
			if (NativeMethods.GetWindowRect(hhwnd, out rc) == false || rc.Left <= OFFSCREEN_THRESHOLD) {
				return false;
			}

			rcwindow = rc;
			monitor = MonitorInfo.ForWindow(hhwnd);
			return true;
		}

		private bool IsForegroundFullscreen() {
			IntPtr hhwnd = NativeMethods.GetForegroundWindow();
			if (hhwnd == IntPtr.Zero || IsOwnWindow(hhwnd) == true) {
				return false;
			}

			if (hhwnd == NativeMethods.GetShellWindow() || hhwnd == NativeMethods.GetDesktopWindow()) {
				return false;
			}

			RECT rcwindow;
			if (NativeMethods.GetWindowRect(hhwnd, out rcwindow) == false) {
				return false;
			}

			RECT rcmonitor = MonitorInfo.ForWindow(hhwnd).MonitorArea;
			if (rcmonitor.Right <= rcmonitor.Left || rcmonitor.Bottom <= rcmonitor.Top) {
				return false;
			}

			return rcwindow.Left <= rcmonitor.Left
				&& rcwindow.Top <= rcmonitor.Top
				&& rcwindow.Right >= rcmonitor.Right
				&& rcwindow.Bottom >= rcmonitor.Bottom;
		}

		private static void HideSlot(Slot slot) {
			if (slot.Visible == false) {
				return;
			}

			slot.Window.HideOverlay();
			slot.Visible = false;
		}

		private static bool TryGetCursor(out Point pt) {
			POINT ptnative;
			if (NativeMethods.GetCursorPos(out ptnative) == true) {
				pt = new Point(ptnative.X, ptnative.Y);
				return true;
			}

			pt = Point.Empty;
			return false;
		}

		private bool IsOwnWindow(IntPtr hhwnd) {
			uint nprocessid;
			NativeMethods.GetWindowThreadProcessId(hhwnd, out nprocessid);
			return nprocessid == (uint)m_nOwnProcessId;
		}

		public void Dispose() {
			if (m_bDisposed == true) {
				return;
			}

			m_bDisposed = true;

			m_tmrTrack.Tick -= OnTrackTick;
			m_tmrTrack.Stop();
			m_tmrTrack.Dispose();

			m_tmrCursor.Tick -= OnCursorTick;
			m_tmrCursor.Stop();
			m_tmrCursor.Dispose();

			foreach (Slot slot in m_arrSlots) {
				if (slot.Current != null) {
					slot.Current.Dispose();
					slot.Current = null;
				}

				slot.Window.Dispose();
			}

			foreach (Slot slot in m_lstMonitorSlots) {
				if (slot.Current != null) {
					slot.Current.Dispose();
					slot.Current = null;
				}

				slot.Window.Dispose();
			}

			m_lstMonitorSlots.Clear();
		}

		private sealed class Slot {
			public Slot(enumDisplayMode emode) {
				Mode = emode;
				Window = new OverlayWindow();
				SizeScale = 1.0;
			}

			public enumDisplayMode Mode { get; private set; }

			public double SizeScale { get; set; }

			public OverlayWindow Window { get; private set; }

			public IBadgePlacement Placement { get; set; }

			public bool Active { get; set; }

			public Bitmap Current { get; set; }

			public VisualKey Visual { get; set; }

			public bool HasVisual { get; set; }

			public Point Location { get; set; }

			public bool Visible { get; set; }
		}

		private struct VisualKey : IEquatable<VisualKey> {
			private readonly enumImeKind m_eKind;
			private readonly bool m_bFullShape;
			private readonly int m_nDpi;
			private readonly int m_nSize;
			private readonly int m_nAlpha;

			public VisualKey(ImeState state, int ndpi, int nsize, int nalpha) {
				m_eKind = state.Kind;
				m_bFullShape = state.FullShape;
				m_nDpi = ndpi;
				m_nSize = nsize;
				m_nAlpha = nalpha;
			}

			public bool Equals(VisualKey other) {
				return m_eKind == other.m_eKind
					&& m_bFullShape == other.m_bFullShape
					&& m_nDpi == other.m_nDpi
					&& m_nSize == other.m_nSize
					&& m_nAlpha == other.m_nAlpha;
			}

			public override bool Equals(object obj) {
				return obj is VisualKey && Equals((VisualKey)obj);
			}

			public override int GetHashCode() {
				int nhash = (int)m_eKind;
				nhash = (nhash * 397) ^ (m_bFullShape ? 1 : 0);
				nhash = (nhash * 397) ^ m_nDpi;
				nhash = (nhash * 397) ^ m_nSize;
				nhash = (nhash * 397) ^ m_nAlpha;
				return nhash;
			}
		}
	}
}
