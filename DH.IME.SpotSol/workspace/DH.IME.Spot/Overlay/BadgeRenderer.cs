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
	internal static partial class BadgeRenderer {
		private const int BASE_SIZE_PX = 40;
		private const double PAD_RATIO = 0.13;
		private const double SHADOW_DROP_RATIO = 0.05;
		private const int SHADOW_ALPHA = 120;
		private const int GLYPH_ALPHA = 245;
		private const int MIN_GLYPH_ALPHA = 15;
		private const int MIN_SHADOW_ALPHA = 1;
		private const int DEFAULT_LOCK_DOT_PX = 10;

		private static readonly Color CLR_LOCK_CAPS = Color.FromArgb(0xF5, 0xA6, 0x23);
		private static readonly Color CLR_LOCK_NUM = Color.FromArgb(0x2E, 0xA0, 0x4A);
		private static readonly Color CLR_LOCK_SCROLL = Color.FromArgb(0x8E, 0x5B, 0xD0);

		private static Color LockDotColor(enumLockDotColor ecolor) {
			switch (ecolor) {
				case enumLockDotColor.Amber:
					return Color.FromArgb(0xF5, 0xA6, 0x23);
				case enumLockDotColor.Orange:
					return Color.FromArgb(0xF2, 0x71, 0x1C);
				case enumLockDotColor.Red:
					return Color.FromArgb(0xD6, 0x45, 0x41);
				case enumLockDotColor.Pink:
					return Color.FromArgb(0xE0, 0x55, 0x9E);
				case enumLockDotColor.Purple:
					return Color.FromArgb(0x8E, 0x5B, 0xD0);
				case enumLockDotColor.Indigo:
					return Color.FromArgb(0x5B, 0x6E, 0xE1);
				case enumLockDotColor.Blue:
					return Color.FromArgb(0x2E, 0x6D, 0xA4);
				case enumLockDotColor.Teal:
					return Color.FromArgb(0x17, 0xA2, 0xB8);
				case enumLockDotColor.Green:
					return Color.FromArgb(0x2E, 0xA0, 0x4A);
				case enumLockDotColor.Lime:
					return Color.FromArgb(0x7C, 0xB3, 0x42);
				case enumLockDotColor.Gray:
					return Color.FromArgb(0x75, 0x75, 0x75);
				case enumLockDotColor.White:
					return Color.FromArgb(0xFF, 0xFF, 0xFF);
				default:
					return Color.FromArgb(0xF5, 0xA6, 0x23);
			}
		}

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
			return Render(state, monitor, nbackgroundalpha, MeasureSize(monitor), true, false, false, false, false,
				enumBadgeCorner.TopRight, enumBadgeCorner.BottomRight, enumBadgeCorner.BottomLeft,
				DEFAULT_LOCK_DOT_PX, DEFAULT_LOCK_DOT_PX, DEFAULT_LOCK_DOT_PX,
				enumLockDotColor.Amber, enumLockDotColor.Green, enumLockDotColor.Purple, "K", "E");
		}

		public static Bitmap Render(ImeState state, MonitorMetrics monitor, int nbackgroundalpha, int nsize) {
			return Render(state, monitor, nbackgroundalpha, nsize, true, false, false, false, false,
				enumBadgeCorner.TopRight, enumBadgeCorner.BottomRight, enumBadgeCorner.BottomLeft,
				DEFAULT_LOCK_DOT_PX, DEFAULT_LOCK_DOT_PX, DEFAULT_LOCK_DOT_PX,
				enumLockDotColor.Amber, enumLockDotColor.Green, enumLockDotColor.Purple, "K", "E");
		}

		public static Bitmap Render(ImeState state, MonitorMetrics monitor, int nbackgroundalpha, int nsize,
			bool bshadow, bool blockpill, bool bcaps, bool bnum, bool bscroll,
			enumBadgeCorner ecornercaps, enumBadgeCorner ecornernum, enumBadgeCorner ecornerscroll,
			int ncapsdotpx, int nnumdotpx, int nscrolldotpx,
			enumLockDotColor ecolorcaps, enumLockDotColor ecolornum, enumLockDotColor ecolorscroll,
			string strhangulglyph, string strlatinglyph) {
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
					strtext = string.IsNullOrEmpty(strhangulglyph) == true ? "K" : strhangulglyph;
					break;
				case enumImeKind.Latin:
					clrbackground = Color.FromArgb(nalpha, 0x2E, 0x6D, 0xA4);
					strtext = string.IsNullOrEmpty(strlatinglyph) == true ? "E" : strlatinglyph;
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

				if (bshadow == true) {
					using (SolidBrush brushshadow = new SolidBrush(Color.FromArgb(nshadowalpha, 0, 0, 0)))
					using (GraphicsPath gpathshadow = BuildRoundedRect(rcshadow, fradius)) {
						gfx.FillPath(brushshadow, gpathshadow);
					}
				}
			}

			if (bshadow == true) {
				BoxBlur(bm, Math.Max(1, npad / 2));
			}

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

				if (blockpill == true && (bcaps == true || bnum == true || bscroll == true)) {
					DrawLockPill(gfx, rcbody, bcaps, bnum, bscroll, nglyphalpha);
				}

				if (bcaps == true || bnum == true || bscroll == true) {
					DrawLockCornerBadges(gfx, rcbody, nsize, bcaps, bnum, bscroll,
						ecornercaps, ecornernum, ecornerscroll,
						monitor.Scale(ncapsdotpx), monitor.Scale(nnumdotpx), monitor.Scale(nscrolldotpx),
						LockDotColor(ecolorcaps), LockDotColor(ecolornum), LockDotColor(ecolorscroll), nglyphalpha);
				}
			}

			Premultiply(bm);
			return bm;
		}

		public static Bitmap RenderFlash(int nsizepx, Color clrbase, string strglyph, bool bon) {
			int nsize = nsizepx < 16 ? 16 : nsizepx;
			int npad = Math.Max(2, (int)Math.Round(nsize * 0.10));
			int nbody = nsize - npad * 2;
			if (nbody < 8) {
				nbody = 8;
			}

			float fradius = nbody * 0.28f;
			RectangleF rcbody = new RectangleF(npad, npad, nbody, nbody);

			Bitmap bm = new Bitmap(nsize, nsize, PixelFormat.Format32bppArgb);

			using (Graphics gfx = Graphics.FromImage(bm)) {
				gfx.SmoothingMode = SmoothingMode.AntiAlias;
				gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
				gfx.Clear(Color.Transparent);

				int nfillalpha = bon == true ? 235 : 90;
				using (SolidBrush brushback = new SolidBrush(Color.FromArgb(nfillalpha, clrbase)))
				using (GraphicsPath gpath = BuildRoundedRect(rcbody, fradius)) {
					gfx.FillPath(brushback, gpath);
				}

				float fpen = Math.Max(1.5f, nbody * 0.06f);
				int nringalpha = bon == true ? 255 : 170;
				using (Pen penring = new Pen(Color.FromArgb(nringalpha, Color.White), fpen))
				using (GraphicsPath gpathring = BuildRoundedRect(rcbody, fradius)) {
					gfx.DrawPath(penring, gpathring);
				}

				Color clrglyph = bon == true ? Color.White : clrbase;
				using (Font fnt = new Font("Segoe UI", nbody * 0.5f, FontStyle.Bold, GraphicsUnit.Pixel))
				using (StringFormat strfmt = new StringFormat())
				using (SolidBrush brushfore = new SolidBrush(Color.FromArgb(255, clrglyph))) {
					strfmt.Alignment = StringAlignment.Center;
					strfmt.LineAlignment = StringAlignment.Center;
					gfx.DrawString(strglyph, fnt, brushfore, rcbody, strfmt);
				}
			}

			Premultiply(bm);
			return bm;
		}

		private static void DrawLockPill(Graphics gfx, RectangleF rcbody, bool bcaps, bool bnum, bool bscroll, int nalpha) {
			int ncount = (bcaps == true ? 1 : 0) + (bnum == true ? 1 : 0) + (bscroll == true ? 1 : 0);
			if (ncount == 0) {
				return;
			}

			float fdot = rcbody.Width * 0.16f;
			if (fdot < 2f) {
				fdot = 2f;
			}

			float fgap = fdot * 0.6f;
			float ftotal = ncount * fdot + (ncount - 1) * fgap;
			float fx = rcbody.Left + (rcbody.Width - ftotal) / 2f;
			float fy = rcbody.Bottom - fdot * 1.4f;

			Color[] arrclr = new Color[3];
			int nidx = 0;
			if (bcaps == true) {
				arrclr[nidx++] = CLR_LOCK_CAPS;
			}

			if (bnum == true) {
				arrclr[nidx++] = CLR_LOCK_NUM;
			}

			if (bscroll == true) {
				arrclr[nidx++] = CLR_LOCK_SCROLL;
			}

			for (int i = 0; i < ncount; i++) {
				using (SolidBrush brushdot = new SolidBrush(Color.FromArgb(nalpha, arrclr[i]))) {
					gfx.FillEllipse(brushdot, fx, fy, fdot, fdot);
				}

				fx += fdot + fgap;
			}
		}

		private static void DrawLockCornerBadges(Graphics gfx, RectangleF rcbody, int ncanvas, bool bcaps, bool bnum, bool bscroll,
			enumBadgeCorner ecornercaps, enumBadgeCorner ecornernum, enumBadgeCorner ecornerscroll,
			int ncapsdotpx, int nnumdotpx, int nscrolldotpx,
			Color clrcaps, Color clrnum, Color clrscroll, int nalpha) {
			if (bcaps == false && bnum == false && bscroll == false) {
				return;
			}

			int[] arrcornercount = new int[4];

			if (bcaps == true) {
				float fdot = DotDiameter(ncapsdotpx);
				DrawOneCornerBadge(gfx, rcbody, ncanvas, fdot, fdot * 1.15f, ecornercaps, clrcaps, nalpha, arrcornercount);
			}

			if (bnum == true) {
				float fdot = DotDiameter(nnumdotpx);
				DrawOneCornerBadge(gfx, rcbody, ncanvas, fdot, fdot * 1.15f, ecornernum, clrnum, nalpha, arrcornercount);
			}

			if (bscroll == true) {
				float fdot = DotDiameter(nscrolldotpx);
				DrawOneCornerBadge(gfx, rcbody, ncanvas, fdot, fdot * 1.15f, ecornerscroll, clrscroll, nalpha, arrcornercount);
			}
		}

		private static float DotDiameter(int ndotpx) {
			return ndotpx < 4 ? 4f : ndotpx;
		}

		private static void DrawOneCornerBadge(Graphics gfx, RectangleF rcbody, int ncanvas, float fdot, float fstep,
			enumBadgeCorner ecorner, Color clr, int nalpha, int[] arrcornercount) {
			int nslot = arrcornercount[(int)ecorner];
			arrcornercount[(int)ecorner] = nslot + 1;

			float foffset = nslot * fstep;
			float fcx;
			float fcy;

			switch (ecorner) {
				case enumBadgeCorner.TopLeft:
					fcx = rcbody.Left + foffset;
					fcy = rcbody.Top;
					break;
				case enumBadgeCorner.BottomLeft:
					fcx = rcbody.Left + foffset;
					fcy = rcbody.Bottom;
					break;
				case enumBadgeCorner.BottomRight:
					fcx = rcbody.Right - foffset;
					fcy = rcbody.Bottom;
					break;
				default:
					fcx = rcbody.Right - foffset;
					fcy = rcbody.Top;
					break;
			}

			float fx = Clamp((int)Math.Round(fcx - fdot / 2f), 0, ncanvas - (int)Math.Ceiling(fdot));
			float fy = Clamp((int)Math.Round(fcy - fdot / 2f), 0, ncanvas - (int)Math.Ceiling(fdot));

			float fpen = Math.Max(1f, fdot * 0.14f);
			using (SolidBrush brushdot = new SolidBrush(Color.FromArgb(nalpha, clr))) {
				gfx.FillEllipse(brushdot, fx, fy, fdot, fdot);
			}

			using (Pen penring = new Pen(Color.FromArgb(Math.Min(255, nalpha + 40), Color.White), fpen)) {
				float finset = fpen / 2f;
				gfx.DrawEllipse(penring, fx + finset, fy + finset, fdot - fpen, fdot - fpen);
			}
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
