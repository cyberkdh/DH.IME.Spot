//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: OverlayWindow
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using DH.IME.Spot.Core;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Overlay {
	internal sealed class OverlayWindow : Form {
		private const double IDLE_SHRINK_RATIO = 0.34;

		private Bitmap m_bmpLast;
		private Point m_ptLast;
		private byte m_byLayerAlpha;
		private bool m_bIdleModified;
		private enumFadeIdleAction m_eIdleAction;

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

			if (m_bmpLast != null) {
				m_bmpLast.Dispose();
			}

			m_bmpLast = (Bitmap)bmpremultipliedargb.Clone();
			m_ptLast = ptscreenlocation;
			m_byLayerAlpha = 255;
			m_bIdleModified = false;

			Push(bmpremultipliedargb, ptscreenlocation, 255);
		}

		public void ApplyIdleModifier(enumFadeIdleAction eaction, int ndimpercent) {
			if (m_bmpLast == null) {
				return;
			}

			m_bIdleModified = true;
			m_eIdleAction = eaction;

			if (eaction == enumFadeIdleAction.Hide) {
				HideOverlay();
				return;
			}

			if (eaction == enumFadeIdleAction.Shrink) {
				PushShrunk();
				return;
			}

			int nkeep = 100 - Clamp(ndimpercent, 0, 95);
			byte byalpha = (byte)Math.Max(1, 255 * nkeep / 100);
			m_byLayerAlpha = byalpha;
			Push(m_bmpLast, m_ptLast, byalpha);
		}

		public void ClearIdleModifier() {
			if (m_bIdleModified == false || m_bmpLast == null) {
				m_bIdleModified = false;
				return;
			}

			m_bIdleModified = false;
			m_byLayerAlpha = 255;

			if (m_eIdleAction == enumFadeIdleAction.Hide) {
				Push(m_bmpLast, m_ptLast, 255);
				ShowNoActivate();
				return;
			}

			Push(m_bmpLast, m_ptLast, 255);
		}

		public bool IsIdleModified {
			get { return m_bIdleModified; }
		}

		private void PushShrunk() {
			int nw = Math.Max(2, (int)Math.Round(m_bmpLast.Width * IDLE_SHRINK_RATIO));
			int nh = Math.Max(2, (int)Math.Round(m_bmpLast.Height * IDLE_SHRINK_RATIO));

			using (Bitmap bmsmall = new Bitmap(nw, nh, PixelFormat.Format32bppArgb)) {
				using (Graphics gfx = Graphics.FromImage(bmsmall)) {
					gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
					gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
					gfx.Clear(Color.Transparent);
					gfx.DrawImage(m_bmpLast, new Rectangle(0, 0, nw, nh));
				}

				int nx = m_ptLast.X + (m_bmpLast.Width - nw) / 2;
				int ny = m_ptLast.Y + (m_bmpLast.Height - nh) / 2;
				Push(bmsmall, new Point(nx, ny), 255);
			}
		}

		private void Push(Bitmap bmpremultipliedargb, Point ptscreenlocation, byte bylayeralpha) {
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
					SourceConstantAlpha = bylayeralpha,
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

		private static int Clamp(int nvalue, int nmin, int nmax) {
			if (nvalue < nmin) {
				return nmin;
			}

			return nvalue > nmax ? nmax : nvalue;
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

		protected override void Dispose(bool bdisposing) {
			if (bdisposing == true && m_bmpLast != null) {
				m_bmpLast.Dispose();
				m_bmpLast = null;
			}

			base.Dispose(bdisposing);
		}
	}
}
