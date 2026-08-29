//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: OptionsForm
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;
using DH.IME.Spot.Core;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.UI {
	internal sealed partial class OptionsForm : Form {
		private static readonly enumBadgeCorner[] g_arrCorners = {
			enumBadgeCorner.TopLeft,
			enumBadgeCorner.TopRight,
			enumBadgeCorner.BottomLeft,
			enumBadgeCorner.BottomRight
		};

		private static readonly enumMonitorWidgetScope[] g_arrScopes = {
			enumMonitorWidgetScope.CurrentMonitor,
			enumMonitorWidgetScope.AllMonitors
		};

		private static readonly enumCursorBadgeSize[] g_arrCursorSizes = {
			enumCursorBadgeSize.Scale25,
			enumCursorBadgeSize.Scale50,
			enumCursorBadgeSize.Scale75,
			enumCursorBadgeSize.Scale100,
			enumCursorBadgeSize.Scale125,
			enumCursorBadgeSize.Scale150
		};

		private OptionsForm() {
			InitializeComponent();
		}

		public OptionsForm(AppSettings settings) : this() {
			LoadFrom(settings ?? AppSettings.Defaults());
			this.m_btnApply.Enabled = false;
		}

		public event EventHandler<OptionsAppliedEventArgs> OptionsApplied;

		protected override void OnShown(EventArgs e) {
			base.OnShown(e);
			this.Activate();
			this.BringToFront();
			NativeMethods.SetForegroundWindow(this.Handle);
		}

		private void LoadFrom(AppSettings settings) {
			AppSettings copy = settings.Clone();
			copy.Normalize();

			this.m_chkActiveWindow.Checked = copy.ActiveWindowEnabled;
			this.m_cboActiveWindowCorner.SelectedIndex = IndexOfCorner(copy.ActiveWindowCorner);
			this.m_chkCursor.Checked = copy.CursorEnabled;
			this.m_cboCursorSize.SelectedIndex = IndexOfCursorSize(copy.CursorBadgeSize);
			this.m_chkMonitorWidget.Checked = copy.MonitorWidgetEnabled;
			this.m_cboMonitorScope.SelectedIndex = IndexOfScope(copy.MonitorWidgetScope);
			this.m_cboMonitorWidgetCorner.SelectedIndex = IndexOfCorner(copy.MonitorWidgetCorner);
			this.m_trkOpacity.Value = copy.BackgroundAlpha;
			this.m_chkRunAtStartup.Checked = copy.RunAtStartup;

			UpdateOpacityValue();
			UpdateGroupEnabled();
		}

		private AppSettings BuildSettings() {
			AppSettings settingsresult = new AppSettings {
				ActiveWindowEnabled = this.m_chkActiveWindow.Checked,
				ActiveWindowCorner = PickCorner(this.m_cboActiveWindowCorner.SelectedIndex),
				CursorEnabled = this.m_chkCursor.Checked,
				CursorBadgeSize = PickCursorSize(this.m_cboCursorSize.SelectedIndex),
				MonitorWidgetEnabled = this.m_chkMonitorWidget.Checked,
				MonitorWidgetCorner = PickCorner(this.m_cboMonitorWidgetCorner.SelectedIndex),
				MonitorWidgetScope = PickScope(this.m_cboMonitorScope.SelectedIndex),
				BackgroundAlpha = this.m_trkOpacity.Value,
				RunAtStartup = this.m_chkRunAtStartup.Checked
			};
			settingsresult.Normalize();
			return settingsresult;
		}

		private void CommitAndRaise() {
			AppSettings settings = BuildSettings();
			SettingsStore.Save(settings);

			EventHandler<OptionsAppliedEventArgs> onhandler = OptionsApplied;
			if (onhandler != null) {
				onhandler(this, new OptionsAppliedEventArgs(settings));
			}

			this.m_btnApply.Enabled = false;
		}

		private void OnOkClick(object sender, EventArgs e) {
			this.CommitAndRaise();
			this.Close();
		}

		private void OnCancelClick(object sender, EventArgs e) {
			this.Close();
		}

		private void OnApplyClick(object sender, EventArgs e) {
			this.CommitAndRaise();
		}

		private void OnFieldChanged(object sender, EventArgs e) {
			this.m_btnApply.Enabled = true;
		}

		private void OnGroupChanged(object sender, EventArgs e) {
			this.UpdateGroupEnabled();
			this.m_btnApply.Enabled = true;
		}

		private void OnOpacityChanged(object sender, EventArgs e) {
			this.UpdateOpacityValue();
			this.m_btnApply.Enabled = true;
		}

		private void UpdateGroupEnabled() {
			this.m_cboActiveWindowCorner.Enabled = this.m_chkActiveWindow.Checked;
			this.m_cboCursorSize.Enabled = this.m_chkCursor.Checked;
			this.m_cboMonitorScope.Enabled = this.m_chkMonitorWidget.Checked;
			this.m_cboMonitorWidgetCorner.Enabled = this.m_chkMonitorWidget.Checked;
		}

		private void UpdateOpacityValue() {
			int npercent = (int)Math.Round(this.m_trkOpacity.Value * 100.0 / AppSettings.MaxBackgroundAlpha);
			this.m_lblOpacityValue.Text = npercent + "%";
		}

		private static int IndexOfCorner(enumBadgeCorner ecorner) {
			for (int i = 0; i < g_arrCorners.Length; i++) {
				if (g_arrCorners[i] == ecorner) {
					return i;
				}
			}

			return 1;
		}

		private static enumBadgeCorner PickCorner(int nindex) {
			if (nindex < 0 || nindex >= g_arrCorners.Length) {
				return enumBadgeCorner.TopRight;
			}

			return g_arrCorners[nindex];
		}

		private static int IndexOfScope(enumMonitorWidgetScope escope) {
			for (int i = 0; i < g_arrScopes.Length; i++) {
				if (g_arrScopes[i] == escope) {
					return i;
				}
			}

			return 0;
		}

		private static enumMonitorWidgetScope PickScope(int nindex) {
			if (nindex < 0 || nindex >= g_arrScopes.Length) {
				return enumMonitorWidgetScope.CurrentMonitor;
			}

			return g_arrScopes[nindex];
		}

		private static int IndexOfCursorSize(enumCursorBadgeSize esize) {
			for (int i = 0; i < g_arrCursorSizes.Length; i++) {
				if (g_arrCursorSizes[i] == esize) {
					return i;
				}
			}

			return 2;
		}

		private static enumCursorBadgeSize PickCursorSize(int nindex) {
			if (nindex < 0 || nindex >= g_arrCursorSizes.Length) {
				return enumCursorBadgeSize.Scale75;
			}

			return g_arrCursorSizes[nindex];
		}
	}
}
