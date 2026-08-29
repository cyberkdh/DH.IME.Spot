//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: BadgeRenderer
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using DH.IME.Spot.Core;

namespace DH.IME.Spot.Overlay {
	internal static class BadgeRenderer {
		private const int BASE_SIZE_PX = 40;
		private const double PAD_RATIO = 0.13;
		private const double SHADOW_DROP_RATIO = 0.05;
		private const int SHADOW_ALPHA = 120;
		private const int GLYPH_ALPHA = 245;
		private const int MIN_GLYPH_ALPHA = 15;
		private const int MIN_SHADOW_ALPHA = 1;

		public static int MeasureSize(MonitorMetrics monitor) {
			return MeasureSize(monitor, 1.0);
		}

		public static int MeasureSize(MonitorMetrics monitor, double fscale) {
			int nbody = monitor.Scale((int)Math.Round(BASE_SIZE_PX * fscale));
			if (nbody < 12) {
				nbody = 12;
			}

			return (int)Math.Round(nbody / (1.0 - 2.0 * PAD_RATIO));
		}

		public static Bitmap Render(ImeState state, MonitorMetrics monitor, int nbackgroundalpha) {
			return Render(state, monitor, nbackgroundalpha, MeasureSize(monitor));
		}

		public static Bitmap Render(ImeState state, MonitorMetrics monitor, int nbackgroundalpha, int nsize) {
			int nalpha = Clamp(nbackgroundalpha, AppSettings.MinBackgroundAlpha, AppSettings.MaxBackgroundAlpha);

			double ffade = nalpha / (double)AppSettings.MaxBackgroundAlpha;
			int nglyphalpha = (int)Math.Round(GLYPH_ALPHA * ffade);
			if (nglyphalpha < MIN_GLYPH_ALPHA) {
				nglyphalpha = MIN_GLYPH_ALPHA;
			}

			int nshadowalpha = (int)Math.Round(SHADOW_ALPHA * ffade);
			if (nshadowalpha < MIN_SHADOW_ALPHA) {
				nshadowalpha = MIN_SHADOW_ALPHA;
			}

			Color clrbackground;
			string strtext;
			switch (state.Kind) {
				case enumImeKind.Hangul:
					clrbackground = Color.FromArgb(nalpha, 0xD6, 0x45, 0x41);
					strtext = "K";
					break;
				case enumImeKind.Latin:
					clrbackground = Color.FromArgb(nalpha, 0x2E, 0x6D, 0xA4);
					strtext = "E";
					break;
				default:
					clrbackground = Color.FromArgb(nalpha, 0x75, 0x75, 0x75);
					strtext = "?";
					break;
			}

			int npad = (int)Math.Round(nsize * PAD_RATIO);
			if (npad < 2) {
				npad = 2;
			}

			int nbody = nsize - npad * 2;
			if (nbody < 8) {
				nbody = 8;
			}

			float fdrop = (float)(nbody * SHADOW_DROP_RATIO);
			float fradius = nbody * 0.26f;
			RectangleF rcbody = new RectangleF(npad, npad, nbody, nbody);
			RectangleF rcshadow = new RectangleF(npad, npad + fdrop, nbody, nbody);

			Bitmap bm = new Bitmap(nsize, nsize, PixelFormat.Format32bppArgb);

			using (Graphics gfx = Graphics.FromImage(bm)) {
				gfx.SmoothingMode = SmoothingMode.AntiAlias;
				gfx.Clear(Color.Transparent);

				using (SolidBrush brushshadow = new SolidBrush(Color.FromArgb(nshadowalpha, 0, 0, 0)))
				using (GraphicsPath gpathshadow = BuildRoundedRect(rcshadow, fradius)) {
					gfx.FillPath(brushshadow, gpathshadow);
				}
			}

			BoxBlur(bm, Math.Max(1, npad / 2));

			using (Graphics gfx = Graphics.FromImage(bm)) {
				gfx.SmoothingMode = SmoothingMode.AntiAlias;
				gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

				using (SolidBrush brushback = new SolidBrush(clrbackground))
				using (GraphicsPath gpath = BuildRoundedRect(rcbody, fradius)) {
					gfx.FillPath(brushback, gpath);
				}

				using (Font fnt = new Font("Segoe UI", nbody * 0.56f, FontStyle.Bold, GraphicsUnit.Pixel))
				using (StringFormat strfmt = new StringFormat())
				using (SolidBrush brushfore = new SolidBrush(Color.FromArgb(nglyphalpha, Color.White))) {
					strfmt.Alignment = StringAlignment.Center;
					strfmt.LineAlignment = StringAlignment.Center;
					gfx.DrawString(strtext, fnt, brushfore, rcbody, strfmt);
				}
			}

			Premultiply(bm);
			return bm;
		}

		private static int Clamp(int nvalue, int nmin, int nmax) {
			if (nvalue < nmin) {
				return nmin;
			}

			return nvalue > nmax ? nmax : nvalue;
		}

		private static void BoxBlur(Bitmap bm, int nradius) {
			if (nradius < 1) {
				return;
			}

			int nwidth = bm.Width;
			int nheight = bm.Height;
			Rectangle rc = new Rectangle(0, 0, nwidth, nheight);
			BitmapData bmpdata = bm.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			try {
				int nstride = bmpdata.Stride;
				int ncount = nstride * nheight;
				byte[] arrsrc = new byte[ncount];
				byte[] arrdst = new byte[ncount];
				Marshal.Copy(bmpdata.Scan0, arrsrc, 0, ncount);

				for (int npass = 0; npass < 2; npass++) {
					BlurPass(arrsrc, arrdst, nwidth, nheight, nstride, nradius, true);
					BlurPass(arrdst, arrsrc, nwidth, nheight, nstride, nradius, false);
				}

				Marshal.Copy(arrsrc, 0, bmpdata.Scan0, ncount);
			}
			finally {
				bm.UnlockBits(bmpdata);
			}
		}

		private static void BlurPass(byte[] arrsrc, byte[] arrdst, int nwidth, int nheight, int nstride, int nradius, bool bhorizontal) {
			for (int y = 0; y < nheight; y++) {
				for (int x = 0; x < nwidth; x++) {
					int nsumb = 0;
					int nsumg = 0;
					int nsumr = 0;
					int nsuma = 0;
					int ntaps = 0;

					for (int k = -nradius; k <= nradius; k++) {
						int nx = bhorizontal ? x + k : x;
						int ny = bhorizontal ? y : y + k;
						if (nx < 0 || nx >= nwidth || ny < 0 || ny >= nheight) {
							continue;
						}

						int nidx = ny * nstride + nx * 4;
						nsumb += arrsrc[nidx];
						nsumg += arrsrc[nidx + 1];
						nsumr += arrsrc[nidx + 2];
						nsuma += arrsrc[nidx + 3];
						ntaps++;
					}

					int ndi = y * nstride + x * 4;
					arrdst[ndi] = (byte)(nsumb / ntaps);
					arrdst[ndi + 1] = (byte)(nsumg / ntaps);
					arrdst[ndi + 2] = (byte)(nsumr / ntaps);
					arrdst[ndi + 3] = (byte)(nsuma / ntaps);
				}
			}
		}

		private static void Premultiply(Bitmap bm) {
			Rectangle rc = new Rectangle(0, 0, bm.Width, bm.Height);
			BitmapData bmpdata = bm.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			try {
				int ncount = bmpdata.Stride * bmpdata.Height;
				byte[] arrbuffer = new byte[ncount];
				Marshal.Copy(bmpdata.Scan0, arrbuffer, 0, ncount);

				for (int i = 0; i < ncount; i += 4) {
					byte byalpha = arrbuffer[i + 3];
					if (byalpha == 255) {
						continue;
					}

					arrbuffer[i] = (byte)(arrbuffer[i] * byalpha / 255);
					arrbuffer[i + 1] = (byte)(arrbuffer[i + 1] * byalpha / 255);
					arrbuffer[i + 2] = (byte)(arrbuffer[i + 2] * byalpha / 255);
				}

				Marshal.Copy(arrbuffer, 0, bmpdata.Scan0, ncount);
			}
			finally {
				bm.UnlockBits(bmpdata);
			}
		}

		private static GraphicsPath BuildRoundedRect(RectangleF rc, float fradius) {
			float fdiameter = fradius * 2f;
			GraphicsPath gpath = new GraphicsPath();
			gpath.AddArc(rc.Left, rc.Top, fdiameter, fdiameter, 180, 90);
			gpath.AddArc(rc.Right - fdiameter, rc.Top, fdiameter, fdiameter, 270, 90);
			gpath.AddArc(rc.Right - fdiameter, rc.Bottom - fdiameter, fdiameter, fdiameter, 0, 90);
			gpath.AddArc(rc.Left, rc.Bottom - fdiameter, fdiameter, fdiameter, 90, 90);
			gpath.CloseFigure();
			return gpath;
		}
	}
}
