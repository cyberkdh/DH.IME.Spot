//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: BadgeRenderer.EdgeBar
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using DH.IME.Spot.Core;

namespace DH.IME.Spot.Overlay {
	internal static partial class BadgeRenderer {
		public static Bitmap RenderEdgeBar(ImeState state, int nbackgroundalpha, bool bhorizontal,
			int nlengthpx, int nthicknesspx, bool bcaps, bool bnum, bool bscroll) {
			int nalpha = Clamp(nbackgroundalpha, AppSettings.MinBackgroundAlpha, AppSettings.MaxBackgroundAlpha);
			int nlength = nlengthpx < 8 ? 8 : nlengthpx;
			int nthickness = nthicknesspx < 2 ? 2 : nthicknesspx;

			Color clrbar;
			switch (state.Kind) {
				case enumImeKind.Hangul:
					clrbar = Color.FromArgb(nalpha, 0xD6, 0x45, 0x41);
					break;
				case enumImeKind.Latin:
					clrbar = Color.FromArgb(nalpha, 0x2E, 0x6D, 0xA4);
					break;
				default:
					clrbar = Color.FromArgb(nalpha, 0x75, 0x75, 0x75);
					break;
			}

			int nwidth = bhorizontal == true ? nlength : nthickness;
			int nheight = bhorizontal == true ? nthickness : nlength;

			Bitmap bm = new Bitmap(nwidth, nheight, PixelFormat.Format32bppArgb);

			using (Graphics gfx = Graphics.FromImage(bm)) {
				gfx.SmoothingMode = SmoothingMode.AntiAlias;
				gfx.Clear(Color.Transparent);

				float fradius = nthickness / 2f;
				RectangleF rcbar = new RectangleF(0, 0, nwidth, nheight);

				using (SolidBrush brushbar = new SolidBrush(clrbar))
				using (GraphicsPath gpath = BuildRoundedRect(rcbar, fradius)) {
					gfx.FillPath(brushbar, gpath);
				}

				bool banylock = bcaps == true || bnum == true || bscroll == true;
				if (banylock == true) {
					DrawEdgeBarLocks(gfx, nwidth, nheight, nthickness, bhorizontal, bcaps, bnum, bscroll, nalpha);
				}
			}

			Premultiply(bm);
			return bm;
		}

		private static void DrawEdgeBarLocks(Graphics gfx, int nwidth, int nheight, int nthickness,
			bool bhorizontal, bool bcaps, bool bnum, bool bscroll, int nalpha) {
			float fnotch = Math.Max(2f, nthickness * 0.7f);
			float fgap = fnotch * 1.8f;

			Color[] arrclr = new Color[3];
			int ncount = 0;
			if (bcaps == true) {
				arrclr[ncount++] = CLR_LOCK_CAPS;
			}

			if (bnum == true) {
				arrclr[ncount++] = CLR_LOCK_NUM;
			}

			if (bscroll == true) {
				arrclr[ncount++] = CLR_LOCK_SCROLL;
			}

			for (int i = 0; i < ncount; i++) {
				float foffset = fnotch + i * fgap;
				RectangleF rcnotch = bhorizontal == true
					? new RectangleF(nwidth - foffset - fnotch, (nheight - fnotch) / 2f, fnotch, fnotch)
					: new RectangleF((nwidth - fnotch) / 2f, nheight - foffset - fnotch, fnotch, fnotch);

				using (SolidBrush brushnotch = new SolidBrush(Color.FromArgb(nalpha, arrclr[i]))) {
					gfx.FillEllipse(brushnotch, rcnotch);
				}
			}
		}
	}
}
