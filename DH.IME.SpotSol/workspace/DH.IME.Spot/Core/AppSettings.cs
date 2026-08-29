//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: AppSettings
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;

namespace DH.IME.Spot.Core {
	internal enum enumBadgeCorner {
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	internal enum enumDisplayMode {
		ActiveWindowCorner,
		CursorCompanion,
		PerMonitorWidget
	}

	internal enum enumMonitorWidgetScope {
		CurrentMonitor,
		AllMonitors
	}

	internal enum enumCursorBadgeSize {
		Scale25,
		Scale50,
		Scale75,
		Scale100,
		Scale125,
		Scale150
	}

	internal sealed class AppSettings {
		public const int MinBackgroundAlpha = 26;
		public const int MaxBackgroundAlpha = 255;

		public int BackgroundAlpha { get; set; }

		public bool RunAtStartup { get; set; }

		public bool ActiveWindowEnabled { get; set; }

		public enumBadgeCorner ActiveWindowCorner { get; set; }

		public bool CursorEnabled { get; set; }

		public enumCursorBadgeSize CursorBadgeSize { get; set; }

		public bool MonitorWidgetEnabled { get; set; }

		public enumBadgeCorner MonitorWidgetCorner { get; set; }

		public enumMonitorWidgetScope MonitorWidgetScope { get; set; }

		public static AppSettings Defaults() {
			return new AppSettings {
				BackgroundAlpha = 128,
				RunAtStartup = true,
				ActiveWindowEnabled = true,
				ActiveWindowCorner = enumBadgeCorner.TopRight,
				CursorEnabled = true,
				CursorBadgeSize = enumCursorBadgeSize.Scale75,
				MonitorWidgetEnabled = true,
				MonitorWidgetCorner = enumBadgeCorner.BottomLeft,
				MonitorWidgetScope = enumMonitorWidgetScope.AllMonitors
			};
		}

		public AppSettings Clone() {
			return new AppSettings {
				BackgroundAlpha = BackgroundAlpha,
				RunAtStartup = RunAtStartup,
				ActiveWindowEnabled = ActiveWindowEnabled,
				ActiveWindowCorner = ActiveWindowCorner,
				CursorEnabled = CursorEnabled,
				CursorBadgeSize = CursorBadgeSize,
				MonitorWidgetEnabled = MonitorWidgetEnabled,
				MonitorWidgetCorner = MonitorWidgetCorner,
				MonitorWidgetScope = MonitorWidgetScope
			};
		}

		public void Normalize() {
			if (BackgroundAlpha < MinBackgroundAlpha) {
				BackgroundAlpha = MinBackgroundAlpha;
			}
			else if (BackgroundAlpha > MaxBackgroundAlpha) {
				BackgroundAlpha = MaxBackgroundAlpha;
			}

			if (Enum.IsDefined(typeof(enumBadgeCorner), ActiveWindowCorner) == false) {
				ActiveWindowCorner = enumBadgeCorner.TopRight;
			}

			if (Enum.IsDefined(typeof(enumBadgeCorner), MonitorWidgetCorner) == false) {
				MonitorWidgetCorner = enumBadgeCorner.TopRight;
			}

			if (Enum.IsDefined(typeof(enumMonitorWidgetScope), MonitorWidgetScope) == false) {
				MonitorWidgetScope = enumMonitorWidgetScope.CurrentMonitor;
			}

			if (Enum.IsDefined(typeof(enumCursorBadgeSize), CursorBadgeSize) == false) {
				CursorBadgeSize = enumCursorBadgeSize.Scale75;
			}
		}

		public static double ScaleOf(enumCursorBadgeSize esize) {
			switch (esize) {
				case enumCursorBadgeSize.Scale25:
					return 0.25;
				case enumCursorBadgeSize.Scale50:
					return 0.50;
				case enumCursorBadgeSize.Scale75:
					return 0.75;
				case enumCursorBadgeSize.Scale125:
					return 1.25;
				case enumCursorBadgeSize.Scale150:
					return 1.50;
				default:
					return 1.00;
			}
		}
	}
}
