//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: TrayApplicationContext
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using DH.IME.Spot.Core;
using DH.IME.Spot.Overlay;
using DH.IME.Spot.UI;

namespace DH.IME.Spot.App {
	internal sealed class TrayApplicationContext : ApplicationContext {
		private const int DEFAULT_POLL_INTERVAL_MS = 150;

		private readonly NotifyIcon m_notifyIcon;
		private readonly ImeWatcher m_watcher;
		private readonly BadgeController m_badge;
		private readonly ToolStripMenuItem m_mnuPause;
		private readonly Control m_ctlSync;

		private AppSettings m_settings;
		private Icon m_icoState;
		private AboutForm m_frmAbout;
		private OptionsForm m_frmOptions;
		private bool m_bDisposed;

		public TrayApplicationContext() {
			m_ctlSync = new Control();
			_ = m_ctlSync.Handle;

			m_settings = SettingsStore.Load();

			m_mnuPause = new ToolStripMenuItem("Pause overlay(&P)");
			m_mnuPause.CheckOnClick = true;
			m_mnuPause.Click += OnPauseOverlay;

			m_notifyIcon = new NotifyIcon {
				Text = "DH.IME.Spot",
				Visible = true,
				Icon = SystemIcons.Application,
				ContextMenuStrip = TrayMenuBuilder.Build(
					mnupause: m_mnuPause,
					onoptions: OnOptions,
					onabout: OnAbout,
					onexit: OnExit)
			};

			m_notifyIcon.DoubleClick += OnAbout;

			m_badge = new BadgeController(m_settings);
			m_badge.Start();

			int npollinterval = m_settings != null ? m_settings.PollIntervalMs : DEFAULT_POLL_INTERVAL_MS;
			m_watcher = new ImeWatcher(npollinterval);
			m_watcher.ImeStateChanged += OnImeStateChanged;
			m_watcher.Start();

			SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
		}

		private void OnDisplaySettingsChanged(object sender, EventArgs e) {
			MonitorInfo.InvalidateCache();
			m_badge.ForceRefresh();
		}

		private void OnImeStateChanged(object sender, ImeStateChangedEventArgs e) {
			Icon ico = StateIconRenderer.Create(e.State);
			m_notifyIcon.Icon = ico;

			if (m_icoState != null) {
				m_icoState.Dispose();
			}

			m_icoState = ico;
			m_notifyIcon.Text = BuildTooltip(e.State, m_settings);

			m_badge.SetState(e.State, e.Foreground.Hwnd);
		}

		private static string BuildTooltip(ImeState state, AppSettings settings) {
			string strlabel;
			switch (state.Kind) {
				case enumImeKind.Hangul:
					strlabel = state.FullShape ? "Korean (Full-width)" : "Korean";
					break;
				case enumImeKind.Latin:
					strlabel = state.FullShape ? "English (Full-width)" : "English";
					break;
				default:
					strlabel = "Unknown";
					break;
			}

			string strlocks = BuildLockSuffix(state, settings);
			return "DH.IME.Spot - " + strlabel + strlocks;
		}

		private static string BuildLockSuffix(ImeState state, AppSettings settings) {
			bool bcaps = state.CapsLock == true && (settings == null || settings.ShowCapsLock == true);
			bool bnum = state.NumLock == true && (settings == null || settings.ShowNumLock == true);
			bool bscroll = state.ScrollLock == true && (settings == null || settings.ShowScrollLock == true);

			if (bcaps == false && bnum == false && bscroll == false) {
				return string.Empty;
			}

			string strjoined = string.Empty;
			if (bcaps == true) {
				strjoined = "Caps";
			}

			if (bnum == true) {
				strjoined = strjoined.Length > 0 ? strjoined + " / Num" : "Num";
			}

			if (bscroll == true) {
				strjoined = strjoined.Length > 0 ? strjoined + " / Scroll" : "Scroll";
			}

			return " [" + strjoined + "]";
		}

		private void OnPauseOverlay(object sender, EventArgs e) {
			m_badge.SetPaused(m_mnuPause.Checked);
		}

		private void OnOptions(object sender, EventArgs e) {
			m_ctlSync.BeginInvoke(new Action(ShowOptions));
		}

		private void OnAbout(object sender, EventArgs e) {
			m_ctlSync.BeginInvoke(new Action(ShowAbout));
		}

		private void ShowOptions() {
			if (m_frmOptions == null || m_frmOptions.IsDisposed == true) {
				m_frmOptions = new OptionsForm(m_settings);
				m_frmOptions.OptionsApplied += OnOptionsApplied;
			}

			if (m_frmOptions.Visible == true) {
				m_frmOptions.Activate();
				return;
			}

			m_frmOptions.Show();
			m_frmOptions.Activate();
		}

		private void OnOptionsApplied(object sender, OptionsAppliedEventArgs e) {
			m_settings = e.Settings;
			m_badge.ApplySettings(m_settings);

			if (m_watcher != null) {
				m_watcher.PollIntervalMs = m_settings.PollIntervalMs;
			}
		}

		private void ShowAbout() {
			if (m_frmAbout == null || m_frmAbout.IsDisposed == true) {
				m_frmAbout = new AboutForm();
			}

			if (m_frmAbout.Visible == true) {
				m_frmAbout.Activate();
				return;
			}

			m_frmAbout.Show();
			m_frmAbout.Activate();
		}

		private void OnExit(object sender, EventArgs e) {
			ExitThread();
		}

		protected override void Dispose(bool disposing) {
			if (m_bDisposed == false && disposing == true) {
				m_bDisposed = true;

				SystemEvents.DisplaySettingsChanged -= OnDisplaySettingsChanged;

				if (m_watcher != null) {
					m_watcher.ImeStateChanged -= OnImeStateChanged;
					m_watcher.Dispose();
				}

				if (m_badge != null) {
					m_badge.Dispose();
				}

				if (m_notifyIcon != null) {
					m_notifyIcon.Visible = false;
					m_notifyIcon.Dispose();
				}

				if (m_icoState != null) {
					m_icoState.Dispose();
				}

				if (m_frmAbout != null) {
					m_frmAbout.Dispose();
				}

				if (m_frmOptions != null) {
					m_frmOptions.OptionsApplied -= OnOptionsApplied;
					m_frmOptions.Dispose();
				}

				if (m_ctlSync != null) {
					m_ctlSync.Dispose();
				}
			}

			base.Dispose(disposing);
		}
	}
}
