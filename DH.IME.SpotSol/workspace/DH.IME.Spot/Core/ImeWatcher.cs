//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: ImeWatcher
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Core {
	internal sealed class ImeStateChangedEventArgs : EventArgs {
		private readonly ImeState m_state;
		private readonly ForegroundInfo m_foreground;

		public ImeStateChangedEventArgs(ImeState state, ForegroundInfo foreground) {
			m_state = state;
			m_foreground = foreground;
		}

		public ImeState State {
			get { return m_state; }
		}

		public ForegroundInfo Foreground {
			get { return m_foreground; }
		}
	}

	internal sealed class ImeWatcher : IDisposable {
		private const int MIN_INTERVAL_MS = 30;
		private const int EVENT_DEBOUNCE_MS = 40;

		private readonly WinEventProc m_winEventProc;
		private readonly Timer m_tmrPoll;
		private readonly Timer m_tmrDebounce;

		private IntPtr m_hHookForeground;
		private IntPtr m_hHookFocus;
		private ImeState m_lastState;
		private bool m_bHasLast;
		private bool m_bPolling;
		private bool m_bDisposed;

		public event EventHandler<ImeStateChangedEventArgs> ImeStateChanged;

		public ImeWatcher(int npollintervalms) {
			m_winEventProc = OnWinEvent;
			m_tmrPoll = new Timer();
			m_tmrPoll.Interval = Clamp(npollintervalms);
			m_tmrPoll.Tick += OnPollTick;

			m_tmrDebounce = new Timer();
			m_tmrDebounce.Interval = EVENT_DEBOUNCE_MS;
			m_tmrDebounce.Tick += OnDebounceTick;
		}

		public int PollIntervalMs {
			get { return m_tmrPoll.Interval; }
			set { m_tmrPoll.Interval = Clamp(value); }
		}

		public void Start() {
			if (m_bDisposed == true) {
				return;
			}

			if (m_hHookForeground == IntPtr.Zero) {
				m_hHookForeground = NativeMethods.SetWinEventHook(
					NativeConstants.EVENT_SYSTEM_FOREGROUND,
					NativeConstants.EVENT_SYSTEM_FOREGROUND,
					IntPtr.Zero,
					m_winEventProc,
					0,
					0,
					NativeConstants.WINEVENT_OUTOFCONTEXT | NativeConstants.WINEVENT_SKIPOWNPROCESS);
			}

			if (m_hHookFocus == IntPtr.Zero) {
				m_hHookFocus = NativeMethods.SetWinEventHook(
					NativeConstants.EVENT_OBJECT_FOCUS,
					NativeConstants.EVENT_OBJECT_FOCUS,
					IntPtr.Zero,
					m_winEventProc,
					0,
					0,
					NativeConstants.WINEVENT_OUTOFCONTEXT | NativeConstants.WINEVENT_SKIPOWNPROCESS);
			}

			m_tmrPoll.Start();
			Poll();
		}

		public void Stop() {
			m_tmrPoll.Stop();
			m_tmrDebounce.Stop();

			if (m_hHookForeground != IntPtr.Zero) {
				NativeMethods.UnhookWinEvent(m_hHookForeground);
				m_hHookForeground = IntPtr.Zero;
			}

			if (m_hHookFocus != IntPtr.Zero) {
				NativeMethods.UnhookWinEvent(m_hHookFocus);
				m_hHookFocus = IntPtr.Zero;
			}
		}

		public void Refresh() {
			Poll();
		}

		private void OnPollTick(object sender, EventArgs e) {
			Poll();
		}

		private void OnDebounceTick(object sender, EventArgs e) {
			m_tmrDebounce.Stop();
			Poll();
		}

		private void OnWinEvent(
			IntPtr hWinEventHook,
			uint eventType,
			IntPtr hwnd,
			int idObject,
			int idChild,
			uint dwEventThread,
			uint dwmsEventTime) {
			if (m_bDisposed == true) {
				return;
			}

			m_tmrDebounce.Stop();
			m_tmrDebounce.Start();
		}

		private void Poll() {
			if (m_bDisposed == true || m_bPolling == true) {
				return;
			}

			m_bPolling = true;
			try {
				ForegroundInfo foreground = ForegroundTracker.Current();
				ImeState state = foreground.IsValid == true ? ImeQuery.Query(foreground) : ImeState.Unknown;

				if (m_bHasLast == true && state.Equals(m_lastState) == true) {
					return;
				}

				m_lastState = state;
				m_bHasLast = true;

				EventHandler<ImeStateChangedEventArgs> onhandler = ImeStateChanged;
				if (onhandler != null) {
					onhandler(this, new ImeStateChangedEventArgs(state, foreground));
				}
			}
			finally {
				m_bPolling = false;
			}
		}

		private static int Clamp(int nintervalms) {
			return nintervalms < MIN_INTERVAL_MS ? MIN_INTERVAL_MS : nintervalms;
		}

		public void Dispose() {
			if (m_bDisposed == true) {
				return;
			}

			m_bDisposed = true;
			Stop();
			m_tmrPoll.Tick -= OnPollTick;
			m_tmrPoll.Dispose();
			m_tmrDebounce.Tick -= OnDebounceTick;
			m_tmrDebounce.Dispose();
		}
	}
}
