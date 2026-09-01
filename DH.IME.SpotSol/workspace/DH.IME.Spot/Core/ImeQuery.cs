//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: ImeQuery
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Core {
	internal static class ImeQuery {
		private const uint QUERY_TIMEOUT_MS = 20;

		public static ImeState Query(ForegroundInfo foreground) {
			bool bkoreanlayout = IsKoreanLayout(foreground.ThreadId);
			bool bcapslock = IsToggled(NativeConstants.VK_CAPITAL);
			bool bnumlock = IsToggled(NativeConstants.VK_NUMLOCK);
			bool bscrolllock = IsToggled(NativeConstants.VK_SCROLL);

			IntPtr himewnd = NativeMethods.ImmGetDefaultIMEWnd(foreground.Hwnd);
			if (himewnd == IntPtr.Zero) {
				return new ImeState(
					bkoreanlayout ? enumImeKind.Latin : enumImeKind.Unknown,
					false,
					bkoreanlayout,
					bcapslock,
					bnumlock,
					bscrolllock);
			}

			IntPtr hopenresult;
			NativeMethods.SendMessageTimeout(
				himewnd,
				NativeConstants.WM_IME_CONTROL,
				(IntPtr)NativeConstants.IMC_GETOPENSTATUS,
				IntPtr.Zero,
				NativeConstants.SMTO_ABORTIFHUNG,
				QUERY_TIMEOUT_MS,
				out hopenresult);

			IntPtr hmoderesult;
			NativeMethods.SendMessageTimeout(
				himewnd,
				NativeConstants.WM_IME_CONTROL,
				(IntPtr)NativeConstants.IMC_GETCONVERSIONMODE,
				IntPtr.Zero,
				NativeConstants.SMTO_ABORTIFHUNG,
				QUERY_TIMEOUT_MS,
				out hmoderesult);

			bool bisopen = hopenresult != IntPtr.Zero;
			long lconversionmode = hmoderesult.ToInt64();
			bool bnative = (lconversionmode & NativeConstants.IME_CMODE_NATIVE) != 0;
			bool bfullshape = (lconversionmode & NativeConstants.IME_CMODE_FULLSHAPE) != 0;

			enumImeKind ekind;
			if (bkoreanlayout == false) {
				ekind = enumImeKind.Latin;
			}
			else if (bisopen && bnative) {
				ekind = enumImeKind.Hangul;
			}
			else {
				ekind = enumImeKind.Latin;
			}

			return new ImeState(ekind, bfullshape, bkoreanlayout, bcapslock, bnumlock, bscrolllock);
		}

		public static ImeState UnknownWithGlobalLocks() {
			return ImeState.Unknown.WithLocks(
				IsToggled(NativeConstants.VK_CAPITAL),
				IsToggled(NativeConstants.VK_NUMLOCK),
				IsToggled(NativeConstants.VK_SCROLL));
		}

		private static bool IsToggled(int nvirtkey) {
			return (NativeMethods.GetKeyState(nvirtkey) & 0x0001) != 0;
		}

		private static bool IsKoreanLayout(uint nthreadid) {
			IntPtr hkl = NativeMethods.GetKeyboardLayout(nthreadid);
			int nlanguageid = (int)(hkl.ToInt64() & 0xFFFF);
			return nlanguageid == NativeConstants.LANG_KOREAN;
		}
	}
}
