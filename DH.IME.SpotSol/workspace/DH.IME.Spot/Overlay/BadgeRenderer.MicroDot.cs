//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: BadgeRenderer.MicroDot
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using DH.IME.Spot.Core;

namespace DH.IME.Spot.Overlay {
	internal static partial class BadgeRenderer {
		public static int MicroDotCanvasSize(int ndotpx, enumMicroDotLockStyle elockstyle) {
			int ndot = ndotpx < 4 ? 4 : ndotpx;
			int npad;
			switch (elockstyle) {
				case enumMicroDotLockStyle.Satellites:
					npad = ndot;
					break;
				case enumMicroDotLockStyle.Ring:
					npad = Math.Max(4, ndot / 3);
					break;
				default:
					npad = 2;
					break;
			}

			return ndot + npad * 2;
		}

		public static Bitmap RenderMicroDot(ImeState state, int nbackgroundalpha, int ndotpx,
			enumMicroDotLockStyle elockstyle, bool bcaps, bool bnum, bool bscroll) {
			int nalpha = Clamp(nbackgroundalpha, AppSettings.MinBackgroundAlpha, AppSettings.MaxBackgroundAlpha);
			int ndot = ndotpx < 4 ? 4 : ndotpx;
			int ncanvas = MicroDotCanvasSize(ndot, elockstyle);

			Color clrdot;
			switch (state.Kind) {
				case enumImeKind.Hangul:
					clrdot = Color.FromArgb(nalpha, 0xD6, 0x45, 0x41);
					break;
				case enumImeKind.Latin:
					clrdot = Color.FromArgb(nalpha, 0x2E, 0x6D, 0xA4);
					break;
				default:
					clrdot = Color.FromArgb(nalpha, 0x75, 0x75, 0x75);
					break;
			}

			Bitmap bm = new Bitmap(ncanvas, ncanvas, PixelFormat.Format32bppArgb);

			using (Graphics gfx = Graphics.FromImage(bm)) {
				gfx.SmoothingMode = SmoothingMode.AntiAlias;
				gfx.Clear(Color.Transparent);

				float fcenter = ncanvas / 2f;
				float fradius = ndot / 2f;
				RectangleF rcdot = new RectangleF(fcenter - fradius, fcenter - fradius, ndot, ndot);

				using (SolidBrush brushdot = new SolidBrush(clrdot)) {
					gfx.FillEllipse(brushdot, rcdot);
				}

				bool banylock = bcaps == true || bnum == true || bscroll == true;

				if (elockstyle == enumMicroDotLockStyle.Ring && banylock == true) {
					float fpen = Math.Max(1f, ndot / 6f);
					using (Pen penring = new Pen(Color.FromArgb(nalpha, CLR_LOCK_CAPS), fpen)) {
						float finset = fpen / 2f;
						gfx.DrawEllipse(penring, rcdot.Left - finset, rcdot.Top - finset,
							rcdot.Width + finset * 2f, rcdot.Height + finset * 2f);
					}
				}

				if (elockstyle == enumMicroDotLockStyle.Satellites && banylock == true) {
					float fsat = Math.Max(2f, ndot * 0.24f);
					float forbit = fradius + fsat;
					DrawSatellite(gfx, fcenter, fcenter, forbit, fsat, -90, bcaps, CLR_LOCK_CAPS, nalpha);
					DrawSatellite(gfx, fcenter, fcenter, forbit, fsat, 0, bnum, CLR_LOCK_NUM, nalpha);
					DrawSatellite(gfx, fcenter, fcenter, forbit, fsat, 90, bscroll, CLR_LOCK_SCROLL, nalpha);
				}
			}

			Premultiply(bm);
			return bm;
		}

		private static void DrawSatellite(Graphics gfx, float fcx, float fcy, float forbit, float fsat,
			double fangledeg, bool bactive, Color clr, int nalpha) {
			if (bactive == false) {
				return;
			}

			double frad = fangledeg * Math.PI / 180.0;
			float fx = fcx + (float)(Math.Cos(frad) * forbit) - fsat / 2f;
			float fy = fcy + (float)(Math.Sin(frad) * forbit) - fsat / 2f;

			using (SolidBrush brushsat = new SolidBrush(Color.FromArgb(nalpha, clr))) {
				gfx.FillEllipse(brushsat, fx, fy, fsat, fsat);
			}
		}
	}
}
