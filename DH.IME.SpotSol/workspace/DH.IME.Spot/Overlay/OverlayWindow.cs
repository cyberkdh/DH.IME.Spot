//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: OverlayWindow
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Windows.Forms;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Overlay {
	internal sealed class OverlayWindow : Form {
		public OverlayWindow() {
			FormBorderStyle = FormBorderStyle.None;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.Manual;
			AutoScaleMode = AutoScaleMode.None;
			Text = "DH.IME.Spot Overlay";
			Visible = false;
		}

		protected override bool ShowWithoutActivation {
			get { return true; }
		}

		protected override CreateParams CreateParams {
			get {
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= NativeConstants.WS_EX_LAYERED
					| NativeConstants.WS_EX_TRANSPARENT
					| NativeConstants.WS_EX_TOOLWINDOW
					| NativeConstants.WS_EX_NOACTIVATE
					| NativeConstants.WS_EX_TOPMOST;
				return cp;
			}
		}

		public void SetContent(Bitmap bmpremultipliedargb, Point ptscreenlocation) {
			if (bmpremultipliedargb == null) {
				return;
			}

			if (IsHandleCreated == false) {
				CreateHandle();
			}

			IntPtr hscreendc = NativeMethods.GetDC(IntPtr.Zero);
			IntPtr hmemdc = NativeMethods.CreateCompatibleDC(hscreendc);
			IntPtr hbitmap = IntPtr.Zero;
			IntPtr holdbitmap = IntPtr.Zero;

			try {
				hbitmap = bmpremultipliedargb.GetHbitmap(Color.FromArgb(0));
				holdbitmap = NativeMethods.SelectObject(hmemdc, hbitmap);

				SIZE szsize = new SIZE { cx = bmpremultipliedargb.Width, cy = bmpremultipliedargb.Height };
				POINT ptsource = new POINT { X = 0, Y = 0 };
				POINT ptdestination = new POINT { X = ptscreenlocation.X, Y = ptscreenlocation.Y };
				BLENDFUNCTION blendfunc = new BLENDFUNCTION {
					BlendOp = NativeConstants.AC_SRC_OVER,
					BlendFlags = 0,
					SourceConstantAlpha = 255,
					AlphaFormat = NativeConstants.AC_SRC_ALPHA
				};

				NativeMethods.UpdateLayeredWindow(
					Handle,
					hscreendc,
					ref ptdestination,
					ref szsize,
					hmemdc,
					ref ptsource,
					0,
					ref blendfunc,
					NativeConstants.ULW_ALPHA);
			}
			finally {
				if (holdbitmap != IntPtr.Zero) {
					NativeMethods.SelectObject(hmemdc, holdbitmap);
				}

				if (hbitmap != IntPtr.Zero) {
					NativeMethods.DeleteObject(hbitmap);
				}

				NativeMethods.DeleteDC(hmemdc);
				NativeMethods.ReleaseDC(IntPtr.Zero, hscreendc);
			}
		}

		public void ShowNoActivate() {
			if (IsHandleCreated == false) {
				CreateHandle();
			}

			NativeMethods.ShowWindow(Handle, NativeConstants.SW_SHOWNOACTIVATE);
			NativeMethods.SetWindowPos(
				Handle,
				NativeMethods.HWND_TOPMOST,
				0, 0, 0, 0,
				NativeConstants.SWP_NOMOVE | NativeConstants.SWP_NOSIZE
					| NativeConstants.SWP_NOACTIVATE | NativeConstants.SWP_NOOWNERZORDER);
		}

		public void MoveTo(Point ptscreenlocation) {
			if (IsHandleCreated == false) {
				return;
			}

			NativeMethods.SetWindowPos(
				Handle,
				NativeMethods.HWND_TOPMOST,
				ptscreenlocation.X, ptscreenlocation.Y,
				0, 0,
				NativeConstants.SWP_NOSIZE | NativeConstants.SWP_NOACTIVATE
					| NativeConstants.SWP_NOOWNERZORDER);
		}

		public void HideOverlay() {
			if (IsHandleCreated == true) {
				NativeMethods.ShowWindow(Handle, NativeConstants.SW_HIDE);
			}
		}
	}
}
