//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: NativeConstants
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
namespace DH.IME.Spot.Interop {
	internal static class NativeConstants {
		public const uint WM_IME_CONTROL = 0x0283;

		public const int IMC_GETCONVERSIONMODE = 0x0001;
		public const int IMC_GETOPENSTATUS = 0x0005;

		public const int IME_CMODE_NATIVE = 0x0001;
		public const int IME_CMODE_FULLSHAPE = 0x0008;

		public const uint EVENT_SYSTEM_FOREGROUND = 0x0003;
		public const uint EVENT_OBJECT_FOCUS = 0x8005;
		public const uint EVENT_OBJECT_LOCATIONCHANGE = 0x800B;

		public const uint WINEVENT_OUTOFCONTEXT = 0x0000;
		public const uint WINEVENT_SKIPOWNPROCESS = 0x0002;

		public const uint SMTO_ABORTIFHUNG = 0x0002;

		public const int LANG_KOREAN = 0x0412;

		public const int GWL_EXSTYLE = -20;

		public const int WS_EX_LAYERED = 0x00080000;
		public const int WS_EX_TRANSPARENT = 0x00000020;
		public const int WS_EX_TOOLWINDOW = 0x00000080;
		public const int WS_EX_NOACTIVATE = 0x08000000;
		public const int WS_EX_TOPMOST = 0x00000008;

		public const int ULW_ALPHA = 0x00000002;
		public const byte AC_SRC_OVER = 0x00;
		public const byte AC_SRC_ALPHA = 0x01;

		public const int SW_HIDE = 0;
		public const int SW_SHOWNOACTIVATE = 4;

		public const uint SWP_NOSIZE = 0x0001;
		public const uint SWP_NOMOVE = 0x0002;
		public const uint SWP_NOACTIVATE = 0x0010;
		public const uint SWP_NOZORDER = 0x0004;
		public const uint SWP_NOOWNERZORDER = 0x0200;

		public const uint MONITOR_DEFAULTTONULL = 0x00000000;
		public const uint MONITOR_DEFAULTTONEAREST = 0x00000002;

		public const int MDT_EFFECTIVE_DPI = 0;

		public const int USER_DEFAULT_SCREEN_DPI = 96;
	}
}
