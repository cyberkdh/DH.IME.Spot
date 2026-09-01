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
			enumCursorBadgeSize.Scale10,
			enumCursorBadgeSize.Scale15,
			enumCursorBadgeSize.Scale25,
			enumCursorBadgeSize.Scale50,
			enumCursorBadgeSize.Scale75,
			enumCursorBadgeSize.Scale100,
			enumCursorBadgeSize.Scale125,
			enumCursorBadgeSize.Scale150
		};

		private static readonly enumFadeIdleAction[] g_arrFadeActions = {
			enumFadeIdleAction.Shrink,
			enumFadeIdleAction.Dim,
			enumFadeIdleAction.Hide
		};

		private static readonly enumFlashAnchor[] g_arrFlashAnchors = {
			enumFlashAnchor.Cursor,
			enumFlashAnchor.ScreenCenter,
			enumFlashAnchor.ActiveWindowCenter
		};

		private static readonly enumFlashSize[] g_arrFlashSizes = {
			enumFlashSize.Small,
			enumFlashSize.Medium,
			enumFlashSize.Large
		};

		private static readonly int[] g_arrLockDotSizes = { 6, 8, 10, 12, 16, 20 };

		private static readonly enumLockDotColor[] g_arrLockDotColors = {
			enumLockDotColor.Amber,
			enumLockDotColor.Orange,
			enumLockDotColor.Red,
			enumLockDotColor.Pink,
			enumLockDotColor.Purple,
			enumLockDotColor.Indigo,
			enumLockDotColor.Blue,
			enumLockDotColor.Teal,
			enumLockDotColor.Green,
			enumLockDotColor.Lime,
			enumLockDotColor.Gray,
			enumLockDotColor.White
		};

		private static readonly int[] g_arrFlashDurations = { 300, 500, 800, 1200, 2000 };
		private static readonly int[] g_arrFadeDelays = { 1000, 2000, 3000, 5000, 8000 };
		private static readonly int[] g_arrFadeDims = { 10, 20, 30, 50, 75 };
		private static readonly int[] g_arrPollIntervals = { 100, 150, 250, 400, 600 };

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

			this.m_chkShowCaps.Checked = copy.ShowCapsLock;
			this.m_chkShowNum.Checked = copy.ShowNumLock;
			this.m_chkShowScroll.Checked = copy.ShowScrollLock;
			this.m_cboCapsCorner.SelectedIndex = IndexOfEnum(g_arrCorners, copy.CapsLockCorner);
			this.m_cboNumCorner.SelectedIndex = IndexOfEnum(g_arrCorners, copy.NumLockCorner);
			this.m_cboScrollCorner.SelectedIndex = IndexOfEnum(g_arrCorners, copy.ScrollLockCorner);
			this.m_cboCapsDotSize.SelectedIndex = IndexOfNearest(g_arrLockDotSizes, copy.CapsLockDotSize);
			this.m_cboNumDotSize.SelectedIndex = IndexOfNearest(g_arrLockDotSizes, copy.NumLockDotSize);
			this.m_cboScrollDotSize.SelectedIndex = IndexOfNearest(g_arrLockDotSizes, copy.ScrollLockDotSize);
			this.m_cboCapsDotColor.SelectedIndex = IndexOfEnum(g_arrLockDotColors, copy.CapsLockDotColor);
			this.m_cboNumDotColor.SelectedIndex = IndexOfEnum(g_arrLockDotColors, copy.NumLockDotColor);
			this.m_cboScrollDotColor.SelectedIndex = IndexOfEnum(g_arrLockDotColors, copy.ScrollLockDotColor);
			this.m_chkBadgeLockPill.Checked = copy.BadgeLockPill;

			this.m_chkActiveWindow.Checked = copy.ActiveWindowEnabled;
			this.m_cboActiveWindowCorner.SelectedIndex = IndexOfEnum(g_arrCorners, copy.ActiveWindowCorner);
			this.m_chkCursor.Checked = copy.CursorEnabled;
			this.m_cboCursorSize.SelectedIndex = IndexOfEnum(g_arrCursorSizes, copy.CursorBadgeSize);
			this.m_chkMonitorWidget.Checked = copy.MonitorWidgetEnabled;
			this.m_cboMonitorScope.SelectedIndex = IndexOfEnum(g_arrScopes, copy.MonitorWidgetScope);
			this.m_cboMonitorWidgetCorner.SelectedIndex = IndexOfEnum(g_arrCorners, copy.MonitorWidgetCorner);
			this.m_trkOpacity.Value = copy.BackgroundAlpha;
			this.m_chkBadgeShadow.Checked = copy.BadgeShadow;

			this.m_chkFlashEnabled.Checked = copy.FlashEnabled;
			this.m_chkFlashImeSwitch.Checked = copy.FlashOnImeSwitch;
			this.m_chkFlashCaps.Checked = copy.FlashOnCapsLock;
			this.m_chkFlashNum.Checked = copy.FlashOnNumLock;
			this.m_chkFlashScroll.Checked = copy.FlashOnScrollLock;
			this.m_cboFlashDuration.SelectedIndex = IndexOfNearest(g_arrFlashDurations, copy.FlashDurationMs);
			this.m_cboFlashAnchor.SelectedIndex = IndexOfEnum(g_arrFlashAnchors, copy.FlashAnchor);
			this.m_cboFlashSize.SelectedIndex = IndexOfEnum(g_arrFlashSizes, copy.FlashSize);

			this.m_chkFadeIdle.Checked = copy.FadeIdleEnabled;
			this.m_cboFadeDelay.SelectedIndex = IndexOfNearest(g_arrFadeDelays, copy.FadeIdleDelayMs);
			this.m_cboFadeAction.SelectedIndex = IndexOfEnum(g_arrFadeActions, copy.FadeIdleAction);
			this.m_cboFadeDim.SelectedIndex = IndexOfNearest(g_arrFadeDims, copy.FadeIdleDimPercent);

			this.m_cboPollInterval.SelectedIndex = IndexOfNearest(g_arrPollIntervals, copy.PollIntervalMs);
			this.m_chkRunAtStartup.Checked = copy.RunAtStartup;

			UpdateOpacityValue();
			UpdateGroupEnabled();
		}

		private AppSettings BuildSettings() {
			AppSettings settingsresult = new AppSettings {
				ShowCapsLock = this.m_chkShowCaps.Checked,
				ShowNumLock = this.m_chkShowNum.Checked,
				ShowScrollLock = this.m_chkShowScroll.Checked,
				CapsLockCorner = PickEnum(g_arrCorners, this.m_cboCapsCorner.SelectedIndex, enumBadgeCorner.TopRight),
				NumLockCorner = PickEnum(g_arrCorners, this.m_cboNumCorner.SelectedIndex, enumBadgeCorner.BottomRight),
				ScrollLockCorner = PickEnum(g_arrCorners, this.m_cboScrollCorner.SelectedIndex, enumBadgeCorner.BottomLeft),
				CapsLockDotSize = PickValue(g_arrLockDotSizes, this.m_cboCapsDotSize.SelectedIndex, 10),
				NumLockDotSize = PickValue(g_arrLockDotSizes, this.m_cboNumDotSize.SelectedIndex, 10),
				ScrollLockDotSize = PickValue(g_arrLockDotSizes, this.m_cboScrollDotSize.SelectedIndex, 10),
				CapsLockDotColor = PickEnum(g_arrLockDotColors, this.m_cboCapsDotColor.SelectedIndex, enumLockDotColor.Amber),
				NumLockDotColor = PickEnum(g_arrLockDotColors, this.m_cboNumDotColor.SelectedIndex, enumLockDotColor.Green),
				ScrollLockDotColor = PickEnum(g_arrLockDotColors, this.m_cboScrollDotColor.SelectedIndex, enumLockDotColor.Purple),
				ActiveWindowEnabled = this.m_chkActiveWindow.Checked,
				ActiveWindowCorner = PickEnum(g_arrCorners, this.m_cboActiveWindowCorner.SelectedIndex, enumBadgeCorner.TopRight),
				CursorEnabled = this.m_chkCursor.Checked,
				CursorBadgeSize = PickEnum(g_arrCursorSizes, this.m_cboCursorSize.SelectedIndex, enumCursorBadgeSize.Scale75),
				MonitorWidgetEnabled = this.m_chkMonitorWidget.Checked,
				MonitorWidgetCorner = PickEnum(g_arrCorners, this.m_cboMonitorWidgetCorner.SelectedIndex, enumBadgeCorner.BottomLeft),
				MonitorWidgetScope = PickEnum(g_arrScopes, this.m_cboMonitorScope.SelectedIndex, enumMonitorWidgetScope.CurrentMonitor),
				BackgroundAlpha = this.m_trkOpacity.Value,
				BadgeShadow = this.m_chkBadgeShadow.Checked,
				BadgeLockPill = this.m_chkBadgeLockPill.Checked,
				FadeIdleEnabled = this.m_chkFadeIdle.Checked,
				FadeIdleDelayMs = PickValue(g_arrFadeDelays, this.m_cboFadeDelay.SelectedIndex, 2000),
				FadeIdleAction = PickEnum(g_arrFadeActions, this.m_cboFadeAction.SelectedIndex, enumFadeIdleAction.Dim),
				FadeIdleDimPercent = PickValue(g_arrFadeDims, this.m_cboFadeDim.SelectedIndex, 25),
				FlashEnabled = this.m_chkFlashEnabled.Checked,
				FlashOnImeSwitch = this.m_chkFlashImeSwitch.Checked,
				FlashOnCapsLock = this.m_chkFlashCaps.Checked,
				FlashOnNumLock = this.m_chkFlashNum.Checked,
				FlashOnScrollLock = this.m_chkFlashScroll.Checked,
				FlashDurationMs = PickValue(g_arrFlashDurations, this.m_cboFlashDuration.SelectedIndex, 800),
				FlashAnchor = PickEnum(g_arrFlashAnchors, this.m_cboFlashAnchor.SelectedIndex, enumFlashAnchor.Cursor),
				FlashSize = PickEnum(g_arrFlashSizes, this.m_cboFlashSize.SelectedIndex, enumFlashSize.Medium),
				PollIntervalMs = PickValue(g_arrPollIntervals, this.m_cboPollInterval.SelectedIndex, 150),
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

			bool bcaps = this.m_chkShowCaps.Checked;
			bool bnum = this.m_chkShowNum.Checked;
			bool bscroll = this.m_chkShowScroll.Checked;
			this.m_cboCapsCorner.Enabled = bcaps;
			this.m_cboCapsDotSize.Enabled = bcaps;
			this.m_cboCapsDotColor.Enabled = bcaps;
			this.m_cboNumCorner.Enabled = bnum;
			this.m_cboNumDotSize.Enabled = bnum;
			this.m_cboNumDotColor.Enabled = bnum;
			this.m_cboScrollCorner.Enabled = bscroll;
			this.m_cboScrollDotSize.Enabled = bscroll;
			this.m_cboScrollDotColor.Enabled = bscroll;

			bool bflash = this.m_chkFlashEnabled.Checked;
			this.m_chkFlashImeSwitch.Enabled = bflash;
			this.m_chkFlashCaps.Enabled = bflash;
			this.m_chkFlashNum.Enabled = bflash;
			this.m_chkFlashScroll.Enabled = bflash;
			this.m_cboFlashDuration.Enabled = bflash;
			this.m_cboFlashAnchor.Enabled = bflash;
			this.m_cboFlashSize.Enabled = bflash;

			bool bfade = this.m_chkFadeIdle.Checked;
			this.m_cboFadeDelay.Enabled = bfade;
			this.m_cboFadeAction.Enabled = bfade;
			this.m_cboFadeDim.Enabled = bfade;
		}

		private void UpdateOpacityValue() {
			int npercent = (int)Math.Round(this.m_trkOpacity.Value * 100.0 / AppSettings.MaxBackgroundAlpha);
			this.m_lblOpacityValue.Text = npercent + "%";
		}

		private static int IndexOfEnum<T>(T[] arr, T evalue) where T : struct {
			for (int i = 0; i < arr.Length; i++) {
				if (arr[i].Equals(evalue) == true) {
					return i;
				}
			}

			return 0;
		}

		private static T PickEnum<T>(T[] arr, int nindex, T efallback) where T : struct {
			if (nindex < 0 || nindex >= arr.Length) {
				return efallback;
			}

			return arr[nindex];
		}

		private static int IndexOfNearest(int[] arr, int nvalue) {
			int nbest = 0;
			int nbestdiff = int.MaxValue;
			for (int i = 0; i < arr.Length; i++) {
				int ndiff = Math.Abs(arr[i] - nvalue);
				if (ndiff < nbestdiff) {
					nbestdiff = ndiff;
					nbest = i;
				}
			}

			return nbest;
		}

		private static int PickValue(int[] arr, int nindex, int nfallback) {
			if (nindex < 0 || nindex >= arr.Length) {
				return nfallback;
			}

			return arr[nindex];
		}
	}
}
