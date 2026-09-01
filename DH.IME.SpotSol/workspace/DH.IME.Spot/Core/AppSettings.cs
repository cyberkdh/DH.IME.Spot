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
		PerMonitorWidget,
		EdgeBar,
		MicroDot
	}

	internal enum enumMonitorWidgetScope {
		CurrentMonitor,
		AllMonitors
	}

	internal enum enumCursorBadgeSize {
		Scale10,
		Scale15,
		Scale25,
		Scale50,
		Scale75,
		Scale100,
		Scale125,
		Scale150
	}

	internal enum enumEdge {
		Top,
		Bottom,
		Left,
		Right
	}

	internal enum enumOverlayScope {
		ActiveWindow,
		PerMonitor
	}

	internal enum enumMicroDotAnchor {
		Cursor,
		WindowCorner,
		MonitorCorner
	}

	internal enum enumFadeIdleAction {
		Shrink,
		Dim,
		Hide
	}

	internal enum enumFlashAnchor {
		Cursor,
		ScreenCenter,
		ActiveWindowCenter
	}

	internal enum enumFlashSize {
		Small,
		Medium,
		Large
	}

	internal enum enumMicroDotLockStyle {
		None,
		Ring,
		Satellites
	}

	internal enum enumLockDotColor {
		Amber,
		Orange,
		Red,
		Pink,
		Purple,
		Indigo,
		Blue,
		Teal,
		Green,
		Lime,
		Gray,
		White
	}

	internal sealed class AppSettings {
		public const int MinBackgroundAlpha = 26;
		public const int MaxBackgroundAlpha = 255;

		public const int MinMicroDotSize = 6;
		public const int MaxMicroDotSize = 32;

		public const int MinLockDotSize = 6;
		public const int MaxLockDotSize = 20;

		public const int MinFadeIdleDelayMs = 500;
		public const int MaxFadeIdleDelayMs = 15000;

		public const int MinFadeIdleDimPercent = 5;
		public const int MaxFadeIdleDimPercent = 90;

		public const int MinFlashDurationMs = 200;
		public const int MaxFlashDurationMs = 5000;

		public const int MinPollIntervalMs = 50;
		public const int MaxPollIntervalMs = 2000;

		public int BackgroundAlpha { get; set; }

		public bool RunAtStartup { get; set; }

		public int PollIntervalMs { get; set; }

		public bool ShowCapsLock { get; set; }

		public bool ShowNumLock { get; set; }

		public bool ShowScrollLock { get; set; }

		public enumBadgeCorner CapsLockCorner { get; set; }

		public enumBadgeCorner NumLockCorner { get; set; }

		public enumBadgeCorner ScrollLockCorner { get; set; }

		public int CapsLockDotSize { get; set; }

		public int NumLockDotSize { get; set; }

		public int ScrollLockDotSize { get; set; }

		public enumLockDotColor CapsLockDotColor { get; set; }

		public enumLockDotColor NumLockDotColor { get; set; }

		public enumLockDotColor ScrollLockDotColor { get; set; }

		public bool BadgeShadow { get; set; }

		public bool BadgeLockPill { get; set; }

		public bool ActiveWindowEnabled { get; set; }

		public enumBadgeCorner ActiveWindowCorner { get; set; }

		public bool CursorEnabled { get; set; }

		public enumCursorBadgeSize CursorBadgeSize { get; set; }

		public bool MonitorWidgetEnabled { get; set; }

		public enumBadgeCorner MonitorWidgetCorner { get; set; }

		public enumMonitorWidgetScope MonitorWidgetScope { get; set; }

		public bool FadeIdleEnabled { get; set; }

		public int FadeIdleDelayMs { get; set; }

		public enumFadeIdleAction FadeIdleAction { get; set; }

		public int FadeIdleDimPercent { get; set; }

		public bool FlashEnabled { get; set; }

		public bool FlashOnImeSwitch { get; set; }

		public bool FlashOnCapsLock { get; set; }

		public bool FlashOnNumLock { get; set; }

		public bool FlashOnScrollLock { get; set; }

		public int FlashDurationMs { get; set; }

		public enumFlashAnchor FlashAnchor { get; set; }

		public enumFlashSize FlashSize { get; set; }

		public static AppSettings Defaults() {
			return new AppSettings {
				BackgroundAlpha = 128,
				RunAtStartup = true,
				PollIntervalMs = 150,
				ShowCapsLock = true,
				ShowNumLock = false,
				ShowScrollLock = false,
				CapsLockCorner = enumBadgeCorner.TopRight,
				NumLockCorner = enumBadgeCorner.BottomRight,
				ScrollLockCorner = enumBadgeCorner.BottomLeft,
				CapsLockDotSize = 10,
				NumLockDotSize = 10,
				ScrollLockDotSize = 10,
				CapsLockDotColor = enumLockDotColor.Amber,
				NumLockDotColor = enumLockDotColor.Green,
				ScrollLockDotColor = enumLockDotColor.Purple,
				BadgeShadow = true,
				BadgeLockPill = true,
				ActiveWindowEnabled = true,
				ActiveWindowCorner = enumBadgeCorner.TopRight,
				CursorEnabled = true,
				CursorBadgeSize = enumCursorBadgeSize.Scale75,
				MonitorWidgetEnabled = true,
				MonitorWidgetCorner = enumBadgeCorner.BottomLeft,
				MonitorWidgetScope = enumMonitorWidgetScope.AllMonitors,
				FadeIdleEnabled = false,
				FadeIdleDelayMs = 2000,
				FadeIdleAction = enumFadeIdleAction.Dim,
				FadeIdleDimPercent = 25,
				FlashEnabled = true,
				FlashOnImeSwitch = true,
				FlashOnCapsLock = true,
				FlashOnNumLock = true,
				FlashOnScrollLock = true,
				FlashDurationMs = 800,
				FlashAnchor = enumFlashAnchor.Cursor,
				FlashSize = enumFlashSize.Medium
			};
		}

		public AppSettings Clone() {
			return new AppSettings {
				BackgroundAlpha = BackgroundAlpha,
				RunAtStartup = RunAtStartup,
				PollIntervalMs = PollIntervalMs,
				ShowCapsLock = ShowCapsLock,
				ShowNumLock = ShowNumLock,
				ShowScrollLock = ShowScrollLock,
				CapsLockCorner = CapsLockCorner,
				NumLockCorner = NumLockCorner,
				ScrollLockCorner = ScrollLockCorner,
				CapsLockDotSize = CapsLockDotSize,
				NumLockDotSize = NumLockDotSize,
				ScrollLockDotSize = ScrollLockDotSize,
				CapsLockDotColor = CapsLockDotColor,
				NumLockDotColor = NumLockDotColor,
				ScrollLockDotColor = ScrollLockDotColor,
				BadgeShadow = BadgeShadow,
				BadgeLockPill = BadgeLockPill,
				ActiveWindowEnabled = ActiveWindowEnabled,
				ActiveWindowCorner = ActiveWindowCorner,
				CursorEnabled = CursorEnabled,
				CursorBadgeSize = CursorBadgeSize,
				MonitorWidgetEnabled = MonitorWidgetEnabled,
				MonitorWidgetCorner = MonitorWidgetCorner,
				MonitorWidgetScope = MonitorWidgetScope,
				FadeIdleEnabled = FadeIdleEnabled,
				FadeIdleDelayMs = FadeIdleDelayMs,
				FadeIdleAction = FadeIdleAction,
				FadeIdleDimPercent = FadeIdleDimPercent,
				FlashEnabled = FlashEnabled,
				FlashOnImeSwitch = FlashOnImeSwitch,
				FlashOnCapsLock = FlashOnCapsLock,
				FlashOnNumLock = FlashOnNumLock,
				FlashOnScrollLock = FlashOnScrollLock,
				FlashDurationMs = FlashDurationMs,
				FlashAnchor = FlashAnchor,
				FlashSize = FlashSize
			};
		}

		public void Normalize() {
			BackgroundAlpha = Clamp(BackgroundAlpha, MinBackgroundAlpha, MaxBackgroundAlpha);
			PollIntervalMs = Clamp(PollIntervalMs, MinPollIntervalMs, MaxPollIntervalMs);
			FadeIdleDelayMs = Clamp(FadeIdleDelayMs, MinFadeIdleDelayMs, MaxFadeIdleDelayMs);
			FadeIdleDimPercent = Clamp(FadeIdleDimPercent, MinFadeIdleDimPercent, MaxFadeIdleDimPercent);
			FlashDurationMs = Clamp(FlashDurationMs, MinFlashDurationMs, MaxFlashDurationMs);

			if (Enum.IsDefined(typeof(enumBadgeCorner), ActiveWindowCorner) == false) {
				ActiveWindowCorner = enumBadgeCorner.TopRight;
			}

			if (Enum.IsDefined(typeof(enumBadgeCorner), MonitorWidgetCorner) == false) {
				MonitorWidgetCorner = enumBadgeCorner.TopRight;
			}

			if (Enum.IsDefined(typeof(enumBadgeCorner), CapsLockCorner) == false) {
				CapsLockCorner = enumBadgeCorner.TopRight;
			}

			if (Enum.IsDefined(typeof(enumBadgeCorner), NumLockCorner) == false) {
				NumLockCorner = enumBadgeCorner.BottomRight;
			}

			if (Enum.IsDefined(typeof(enumBadgeCorner), ScrollLockCorner) == false) {
				ScrollLockCorner = enumBadgeCorner.BottomLeft;
			}

			CapsLockDotSize = Clamp(CapsLockDotSize, MinLockDotSize, MaxLockDotSize);
			NumLockDotSize = Clamp(NumLockDotSize, MinLockDotSize, MaxLockDotSize);
			ScrollLockDotSize = Clamp(ScrollLockDotSize, MinLockDotSize, MaxLockDotSize);

			if (Enum.IsDefined(typeof(enumLockDotColor), CapsLockDotColor) == false) {
				CapsLockDotColor = enumLockDotColor.Amber;
			}

			if (Enum.IsDefined(typeof(enumLockDotColor), NumLockDotColor) == false) {
				NumLockDotColor = enumLockDotColor.Green;
			}

			if (Enum.IsDefined(typeof(enumLockDotColor), ScrollLockDotColor) == false) {
				ScrollLockDotColor = enumLockDotColor.Purple;
			}

			if (Enum.IsDefined(typeof(enumMonitorWidgetScope), MonitorWidgetScope) == false) {
				MonitorWidgetScope = enumMonitorWidgetScope.CurrentMonitor;
			}

			if (Enum.IsDefined(typeof(enumCursorBadgeSize), CursorBadgeSize) == false) {
				CursorBadgeSize = enumCursorBadgeSize.Scale75;
			}

			if (Enum.IsDefined(typeof(enumFadeIdleAction), FadeIdleAction) == false) {
				FadeIdleAction = enumFadeIdleAction.Dim;
			}

			if (Enum.IsDefined(typeof(enumFlashAnchor), FlashAnchor) == false) {
				FlashAnchor = enumFlashAnchor.Cursor;
			}

			if (Enum.IsDefined(typeof(enumFlashSize), FlashSize) == false) {
				FlashSize = enumFlashSize.Medium;
			}
		}

		public static double ScaleOf(enumCursorBadgeSize esize) {
			switch (esize) {
				case enumCursorBadgeSize.Scale10:
					return 0.10;
				case enumCursorBadgeSize.Scale15:
					return 0.15;
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

		private static int Clamp(int nvalue, int nmin, int nmax) {
			if (nvalue < nmin) {
				return nmin;
			}

			return nvalue > nmax ? nmax : nvalue;
		}
	}
}
