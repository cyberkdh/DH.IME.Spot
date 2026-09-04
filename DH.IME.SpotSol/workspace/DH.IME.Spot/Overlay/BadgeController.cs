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

		private static readonly Color CLR_FLASH_CAPS = Color.FromArgb(0xF5, 0xA6, 0x23);
		private static readonly Color CLR_FLASH_NUM = Color.FromArgb(0x2E, 0xA0, 0x4A);
		private static readonly Color CLR_FLASH_SCROLL = Color.FromArgb(0x8E, 0x5B, 0xD0);

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
		private enumPlacementBoundsMode m_eCursorBoundsMode;
		private enumPlacementBoundsMode m_eMonitorBoundsMode;

		private bool m_bShowCaps;
		private bool m_bShowNum;
		private bool m_bShowScroll;
		private enumBadgeCorner m_eCapsCorner;
		private enumBadgeCorner m_eNumCorner;
		private enumBadgeCorner m_eScrollCorner;
		private int m_nCapsDotSize;
		private int m_nNumDotSize;
		private int m_nScrollDotSize;
		private enumLockDotColor m_eCapsDotColor;
		private enumLockDotColor m_eNumDotColor;
		private enumLockDotColor m_eScrollDotColor;
		private bool m_bBadgeShadow;
		private bool m_bBadgeLockPill;

		private bool m_bFadeEnabled;
		private int m_nFadeDelayMs;
		private enumFadeIdleAction m_eFadeAction;
		private int m_nFadeDimPercent;
		private bool m_bIdleActive;
		private readonly Timer m_tmrIdle;

		private bool m_bFlashEnabled;
		private bool m_bFlashIme;
		private bool m_bFlashCaps;
		private bool m_bFlashNum;
		private bool m_bFlashScroll;
		private int m_nFlashDurationMs;
		private enumFlashAnchor m_eFlashAnchor;
		private enumFlashSize m_eFlashSize;
		private string m_strHangulGlyph;
		private string m_strLatinGlyph;
		private bool m_bFlashPrimed;
		private readonly TransientFlash m_flash;

		public BadgeController(AppSettings settings) {
			m_nOwnProcessId = Process.GetCurrentProcess().Id;
			m_state = ImeState.Unknown;

			m_arrSlots = new[] {
				new Slot(enumDisplayMode.ActiveWindowCorner),
				new Slot(enumDisplayMode.CursorCompanion)
			};

			m_cursorSlot = m_arrSlots[1];
			m_lstMonitorSlots = new List<Slot>();
			m_flash = new TransientFlash();
			m_ptLastCursor = new Point(int.MinValue, int.MinValue);

			m_tmrTrack = new Timer();
			m_tmrTrack.Interval = TRACK_INTERVAL_MS;
			m_tmrTrack.Tick += OnTrackTick;

			m_tmrCursor = new Timer();
			m_tmrCursor.Interval = CURSOR_INTERVAL_MS;
			m_tmrCursor.Tick += OnCursorTick;

			m_tmrIdle = new Timer();
			m_tmrIdle.Interval = 2000;
			m_tmrIdle.Tick += OnIdleTick;

			ApplySettings(settings ?? AppSettings.Defaults());
		}

		public void Start() {
			if (m_bDisposed == true) {
				return;
			}

			m_bStarted = true;
			m_tmrTrack.Start();
			UpdateCursorTimer();
			RestartIdleTimer();
		}

		public void SetState(ImeState state) {
			SetState(state, IntPtr.Zero);
		}

		public void SetState(ImeState state, IntPtr hforeground) {
			ImeState stateold = m_state;
			m_state = state;
			m_hForegroundHint = hforeground;
			m_bForegroundHintValid = hforeground != IntPtr.Zero;
			ClearIdle();
			Refresh();
			EvaluateFlash(stateold, state);
			m_bForegroundHintValid = false;
			RestartIdleTimer();
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
			ClearIdle();
			UpdateCursorTimer();
			Refresh();
			RestartIdleTimer();
		}

		public void ApplySettings(AppSettings settings) {
			if (m_bDisposed == true || settings == null) {
				return;
			}

			AppSettings copy = settings.Clone();
			copy.Normalize();

			m_nBackgroundAlpha = copy.BackgroundAlpha;
			m_bShowCaps = copy.ShowCapsLock;
			m_bShowNum = copy.ShowNumLock;
			m_bShowScroll = copy.ShowScrollLock;
			m_eCapsCorner = copy.CapsLockCorner;
			m_eNumCorner = copy.NumLockCorner;
			m_eScrollCorner = copy.ScrollLockCorner;
			m_nCapsDotSize = copy.CapsLockDotSize;
			m_nNumDotSize = copy.NumLockDotSize;
			m_nScrollDotSize = copy.ScrollLockDotSize;
			m_eCapsDotColor = copy.CapsLockDotColor;
			m_eNumDotColor = copy.NumLockDotColor;
			m_eScrollDotColor = copy.ScrollLockDotColor;
			m_bBadgeShadow = copy.BadgeShadow;
			m_bBadgeLockPill = copy.BadgeLockPill;
			m_eCursorBoundsMode = copy.CursorBoundsMode;

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
			m_eMonitorBoundsMode = copy.MonitorWidgetBoundsMode;

			foreach (Slot slot in m_lstMonitorSlots) {
				slot.Placement = new PerMonitorWidgetPlacement(m_eMonitorCorner);
				slot.HasVisual = false;
			}

			if (m_bMonitorActive == false) {
				HideMonitorSlots();
			}

			m_bFadeEnabled = copy.FadeIdleEnabled;
			m_nFadeDelayMs = copy.FadeIdleDelayMs;
			m_eFadeAction = copy.FadeIdleAction;
			m_nFadeDimPercent = copy.FadeIdleDimPercent;

			m_bFlashEnabled = copy.FlashEnabled;
			m_bFlashIme = copy.FlashOnImeSwitch;
			m_bFlashCaps = copy.FlashOnCapsLock;
			m_bFlashNum = copy.FlashOnNumLock;
			m_bFlashScroll = copy.FlashOnScrollLock;
			m_nFlashDurationMs = copy.FlashDurationMs;
			m_eFlashAnchor = copy.FlashAnchor;
			m_eFlashSize = copy.FlashSize;
			m_strHangulGlyph = copy.HangulGlyph;
			m_strLatinGlyph = copy.LatinGlyph;

			ClearIdle();
			UpdateCursorTimer();
			Refresh();
			RestartIdleTimer();
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

		private void RestartIdleTimer() {
			ClearIdle();
			m_tmrIdle.Stop();

			bool bshouldrun = m_bFadeEnabled == true && m_bStarted == true
				&& m_bDisposed == false && m_bPaused == false;
			if (bshouldrun == false) {
				return;
			}

			int ndelay = m_nFadeDelayMs < 1 ? 1 : m_nFadeDelayMs;
			m_tmrIdle.Interval = ndelay;
			m_tmrIdle.Start();
		}

		private void OnIdleTick(object sender, EventArgs e) {
			m_tmrIdle.Stop();

			if (m_bFadeEnabled == false || m_bDisposed == true || m_bPaused == true) {
				return;
			}

			m_bIdleActive = true;
			ApplyIdleToSlot(m_arrSlots[0]);
			ApplyIdleToSlot(m_arrSlots[1]);

			foreach (Slot slot in m_lstMonitorSlots) {
				ApplyIdleToSlot(slot);
			}
		}

		private void ApplyIdleToSlot(Slot slot) {
			if (slot.Visible == false) {
				return;
			}

			slot.Window.ApplyIdleModifier(m_eFadeAction, m_nFadeDimPercent);
		}

		private void ClearIdle() {
			if (m_bIdleActive == false) {
				return;
			}

			m_bIdleActive = false;
			ClearIdleOnSlot(m_arrSlots[0]);
			ClearIdleOnSlot(m_arrSlots[1]);

			foreach (Slot slot in m_lstMonitorSlots) {
				ClearIdleOnSlot(slot);
			}
		}

		private static void ClearIdleOnSlot(Slot slot) {
			if (slot.Window.IsIdleModified == true) {
				slot.Window.ClearIdleModifier();
			}
		}

		private void EvaluateFlash(ImeState stateold, ImeState statenew) {
			if (m_bFlashPrimed == false) {
				m_bFlashPrimed = true;
				return;
			}

			if (m_bFlashEnabled == false || m_bPaused == true || m_bDisposed == true) {
				return;
			}

			if (m_bFlashIme == true && statenew.Kind != enumImeKind.Unknown
				&& stateold.Kind != enumImeKind.Unknown && stateold.Kind != statenew.Kind) {
				FireFlash(FlashGlyph(statenew.Kind), FlashColor(statenew.Kind), true);
			}

			if (m_bFlashCaps == true && stateold.CapsLock != statenew.CapsLock) {
				FireFlash("A", CLR_FLASH_CAPS, statenew.CapsLock);
			}

			if (m_bFlashNum == true && stateold.NumLock != statenew.NumLock) {
				FireFlash("1", CLR_FLASH_NUM, statenew.NumLock);
			}

			if (m_bFlashScroll == true && stateold.ScrollLock != statenew.ScrollLock) {
				FireFlash("S", CLR_FLASH_SCROLL, statenew.ScrollLock);
			}
		}

		private void FireFlash(string strglyph, Color clrbase, bool bon) {
			Point ptcenter;
			if (TryGetFlashAnchor(out ptcenter) == false) {
				return;
			}

			MonitorMetrics monitor = MonitorInfo.ForPoint(ptcenter);
			int nsize = monitor.Scale(FlashBaseSize(m_eFlashSize));

			using (Bitmap bm = BadgeRenderer.RenderFlash(nsize, clrbase, strglyph, bon)) {
				m_flash.Show(bm, ptcenter, m_nFlashDurationMs);
			}
		}

		private bool TryGetFlashAnchor(out Point ptcenter) {
			ptcenter = Point.Empty;

			if (m_eFlashAnchor == enumFlashAnchor.ActiveWindowCenter) {
				RECT rcwindow;
				MonitorMetrics monitorwindow;
				if (TryResolveForeground(out rcwindow, out monitorwindow) == true) {
					ptcenter = new Point((rcwindow.Left + rcwindow.Right) / 2, (rcwindow.Top + rcwindow.Bottom) / 2);
					return true;
				}
			}

			Point ptcursor;
			if (TryGetCursor(out ptcursor) == false) {
				return false;
			}

			if (m_eFlashAnchor == enumFlashAnchor.ScreenCenter) {
				RECT rcarea = MonitorInfo.ForPoint(ptcursor).MonitorArea;
				if (rcarea.Right > rcarea.Left && rcarea.Bottom > rcarea.Top) {
					ptcenter = new Point((rcarea.Left + rcarea.Right) / 2, (rcarea.Top + rcarea.Bottom) / 2);
					return true;
				}
			}

			ptcenter = ptcursor;
			return true;
		}

		private string FlashGlyph(enumImeKind ekind) {
			switch (ekind) {
				case enumImeKind.Hangul:
					return m_strHangulGlyph;
				case enumImeKind.Latin:
					return m_strLatinGlyph;
				default:
					return "?";
			}
		}

		private static Color FlashColor(enumImeKind ekind) {
			switch (ekind) {
				case enumImeKind.Hangul:
					return Color.FromArgb(0xD6, 0x45, 0x41);
				case enumImeKind.Latin:
					return Color.FromArgb(0x2E, 0x6D, 0xA4);
				default:
					return Color.FromArgb(0x75, 0x75, 0x75);
			}
		}

		private static int FlashBaseSize(enumFlashSize esize) {
			switch (esize) {
				case enumFlashSize.Small:
					return 28;
				case enumFlashSize.Large:
					return 56;
				default:
					return 40;
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

			if (m_bIdleActive == true) {
				ClearIdle();
				RestartIdleTimer();
				Refresh();
			}

			if (slot.Visible == false || slot.Current == null || slot.HasVisual == false) {
				return;
			}

			MonitorMetrics monitor = MonitorInfo.ForPoint(ptcursor);
			int nsize = BadgeRenderer.MeasureSize(monitor, slot.SizeScale);
			VisualKey visualkey = new VisualKey(m_state, EffCaps(), EffNum(), EffScroll(),
				m_bBadgeShadow, m_bBadgeLockPill, m_eCapsCorner, m_eNumCorner, m_eScrollCorner,
				m_nCapsDotSize, m_nNumDotSize, m_nScrollDotSize, m_eCapsDotColor, m_eNumDotColor, m_eScrollDotColor,
				monitor.Dpi, nsize, m_nBackgroundAlpha, m_strHangulGlyph, m_strLatinGlyph);

			if (visualkey.Equals(slot.Visual) == false) {
				RenderSlot(slot, new PlacementContext {
					Cursor = ptcursor,
					Monitor = monitor,
					BoundsMode = m_eCursorBoundsMode
				});
				return;
			}

			PlacementContext context = new PlacementContext {
				Cursor = ptcursor,
				Monitor = monitor,
				BadgeSize = new Size(nsize, nsize),
				BoundsMode = m_eCursorBoundsMode
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

			if (m_bIdleActive == true) {
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
					context.BoundsMode = m_eCursorBoundsMode;
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
					context.BoundsMode = enumPlacementBoundsMode.WorkArea;
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
					RenderSlot(m_lstMonitorSlots[i], new PlacementContext {
						Monitor = monitor,
						BoundsMode = m_eMonitorBoundsMode
					});
				}

				return;
			}

			EnsureMonitorSlotCount(1);

			Point ptcursor;
			if (TryGetCursor(out ptcursor) == false) {
				ptcursor = Point.Empty;
			}

			MonitorMetrics monitorcursor = MonitorInfo.ForPoint(ptcursor);
			RenderSlot(m_lstMonitorSlots[0], new PlacementContext {
				Monitor = monitorcursor,
				BoundsMode = m_eMonitorBoundsMode
			});
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

		private bool EffCaps() {
			return m_state.CapsLock == true && m_bShowCaps == true;
		}

		private bool EffNum() {
			return m_state.NumLock == true && m_bShowNum == true;
		}

		private bool EffScroll() {
			return m_state.ScrollLock == true && m_bShowScroll == true;
		}

		private void RenderSlot(Slot slot, PlacementContext context) {
			int nsize = BadgeRenderer.MeasureSize(context.Monitor, slot.SizeScale);
			context.BadgeSize = new Size(nsize, nsize);

			Point ptlocation = slot.Placement.GetLocation(context);

			VisualKey visualkey = new VisualKey(m_state, EffCaps(), EffNum(), EffScroll(),
				m_bBadgeShadow, m_bBadgeLockPill, m_eCapsCorner, m_eNumCorner, m_eScrollCorner,
				m_nCapsDotSize, m_nNumDotSize, m_nScrollDotSize, m_eCapsDotColor, m_eNumDotColor, m_eScrollDotColor,
				context.Monitor.Dpi, nsize, m_nBackgroundAlpha, m_strHangulGlyph, m_strLatinGlyph);
			bool bvisualchanged = slot.HasVisual == false || visualkey.Equals(slot.Visual) == false;
			bool bmoved = slot.Visible == false || ptlocation != slot.Location;
			if (bvisualchanged == false && bmoved == false) {
				return;
			}

			bool bcontentchanged = bvisualchanged == true || slot.Current == null;
			if (bcontentchanged == true) {
				Bitmap bm = BadgeRenderer.Render(m_state, context.Monitor, m_nBackgroundAlpha, nsize,
					m_bBadgeShadow, m_bBadgeLockPill, EffCaps(), EffNum(), EffScroll(),
					m_eCapsCorner, m_eNumCorner, m_eScrollCorner,
					m_nCapsDotSize, m_nNumDotSize, m_nScrollDotSize,
					m_eCapsDotColor, m_eNumDotColor, m_eScrollDotColor,
					m_strHangulGlyph, m_strLatinGlyph);
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

			m_tmrIdle.Tick -= OnIdleTick;
			m_tmrIdle.Stop();
			m_tmrIdle.Dispose();

			m_flash.Dispose();

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
			private readonly bool m_bCapsLock;
			private readonly bool m_bNumLock;
			private readonly bool m_bScrollLock;
			private readonly bool m_bShadow;
			private readonly bool m_bLockPill;
			private readonly enumBadgeCorner m_eCornerCaps;
			private readonly enumBadgeCorner m_eCornerNum;
			private readonly enumBadgeCorner m_eCornerScroll;
			private readonly int m_nDotCaps;
			private readonly int m_nDotNum;
			private readonly int m_nDotScroll;
			private readonly enumLockDotColor m_eDotColorCaps;
			private readonly enumLockDotColor m_eDotColorNum;
			private readonly enumLockDotColor m_eDotColorScroll;
			private readonly int m_nDpi;
			private readonly int m_nSize;
			private readonly int m_nAlpha;
			private readonly string m_strHangulGlyph;
			private readonly string m_strLatinGlyph;

			public VisualKey(ImeState state, bool bcaps, bool bnum, bool bscroll, bool bshadow, bool blockpill,
				enumBadgeCorner ecornercaps, enumBadgeCorner ecornernum, enumBadgeCorner ecornerscroll,
				int ndotcaps, int ndotnum, int ndotscroll,
				enumLockDotColor edotcolorcaps, enumLockDotColor edotcolornum, enumLockDotColor edotcolorscroll,
				int ndpi, int nsize, int nalpha, string strhangulglyph, string strlatinglyph) {
				m_eKind = state.Kind;
				m_bFullShape = state.FullShape;
				m_bCapsLock = bcaps;
				m_bNumLock = bnum;
				m_bScrollLock = bscroll;
				m_bShadow = bshadow;
				m_bLockPill = blockpill;
				m_eCornerCaps = ecornercaps;
				m_eCornerNum = ecornernum;
				m_eCornerScroll = ecornerscroll;
				m_nDotCaps = ndotcaps;
				m_nDotNum = ndotnum;
				m_nDotScroll = ndotscroll;
				m_eDotColorCaps = edotcolorcaps;
				m_eDotColorNum = edotcolornum;
				m_eDotColorScroll = edotcolorscroll;
				m_nDpi = ndpi;
				m_nSize = nsize;
				m_nAlpha = nalpha;
				m_strHangulGlyph = strhangulglyph ?? string.Empty;
				m_strLatinGlyph = strlatinglyph ?? string.Empty;
			}

			public bool Equals(VisualKey other) {
				return m_eKind == other.m_eKind
					&& m_bFullShape == other.m_bFullShape
					&& m_bCapsLock == other.m_bCapsLock
					&& m_bNumLock == other.m_bNumLock
					&& m_bScrollLock == other.m_bScrollLock
					&& m_bShadow == other.m_bShadow
					&& m_bLockPill == other.m_bLockPill
					&& m_eCornerCaps == other.m_eCornerCaps
					&& m_eCornerNum == other.m_eCornerNum
					&& m_eCornerScroll == other.m_eCornerScroll
					&& m_nDotCaps == other.m_nDotCaps
					&& m_nDotNum == other.m_nDotNum
					&& m_nDotScroll == other.m_nDotScroll
					&& m_eDotColorCaps == other.m_eDotColorCaps
					&& m_eDotColorNum == other.m_eDotColorNum
					&& m_eDotColorScroll == other.m_eDotColorScroll
					&& m_nDpi == other.m_nDpi
					&& m_nSize == other.m_nSize
					&& m_nAlpha == other.m_nAlpha
					&& string.Equals(m_strHangulGlyph, other.m_strHangulGlyph, StringComparison.Ordinal)
					&& string.Equals(m_strLatinGlyph, other.m_strLatinGlyph, StringComparison.Ordinal);
			}

			public override bool Equals(object obj) {
				return obj is VisualKey && Equals((VisualKey)obj);
			}

			public override int GetHashCode() {
				int nhash = (int)m_eKind;
				nhash = (nhash * 397) ^ (m_bFullShape ? 1 : 0);
				nhash = (nhash * 397) ^ (m_bCapsLock ? 1 : 0);
				nhash = (nhash * 397) ^ (m_bNumLock ? 1 : 0);
				nhash = (nhash * 397) ^ (m_bScrollLock ? 1 : 0);
				nhash = (nhash * 397) ^ (m_bShadow ? 1 : 0);
				nhash = (nhash * 397) ^ (m_bLockPill ? 1 : 0);
				nhash = (nhash * 397) ^ (int)m_eCornerCaps;
				nhash = (nhash * 397) ^ (int)m_eCornerNum;
				nhash = (nhash * 397) ^ (int)m_eCornerScroll;
				nhash = (nhash * 397) ^ m_nDotCaps;
				nhash = (nhash * 397) ^ m_nDotNum;
				nhash = (nhash * 397) ^ m_nDotScroll;
				nhash = (nhash * 397) ^ (int)m_eDotColorCaps;
				nhash = (nhash * 397) ^ (int)m_eDotColorNum;
				nhash = (nhash * 397) ^ (int)m_eDotColorScroll;
				nhash = (nhash * 397) ^ m_nDpi;
				nhash = (nhash * 397) ^ m_nSize;
				nhash = (nhash * 397) ^ m_nAlpha;
				nhash = (nhash * 397) ^ m_strHangulGlyph.GetHashCode();
				nhash = (nhash * 397) ^ m_strLatinGlyph.GetHashCode();
				return nhash;
			}
		}
	}
}
