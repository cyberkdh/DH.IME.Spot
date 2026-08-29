//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: StateIconRenderer
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using DH.IME.Spot.Core;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.UI {
	internal static class StateIconRenderer {
		public static Icon Create(ImeState state) {
			int nsize = GetIconSize();

			Color clrbackground;
			string strtext;
			switch (state.Kind) {
				case enumImeKind.Hangul:
					clrbackground = Color.FromArgb(0xD6, 0x45, 0x41);
					strtext = "K";
					break;
				case enumImeKind.Latin:
					clrbackground = Color.FromArgb(0x2E, 0x6D, 0xA4);
					strtext = "E";
					break;
				default:
					clrbackground = Color.FromArgb(0x75, 0x75, 0x75);
					strtext = "?";
					break;
			}

			using (Bitmap bm = new Bitmap(nsize, nsize)) {
				using (Graphics gfx = Graphics.FromImage(bm)) {
					gfx.SmoothingMode = SmoothingMode.AntiAlias;
					gfx.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
					gfx.Clear(Color.Transparent);

					RectangleF rcbounds = new RectangleF(0.5f, 0.5f, nsize - 1f, nsize - 1f);
					using (SolidBrush brushback = new SolidBrush(clrbackground))
					using (GraphicsPath gpath = BuildRoundedRect(rcbounds, nsize * 0.22f)) {
						gfx.FillPath(brushback, gpath);
					}

					float ffontsize = strtext.Length > 1 ? nsize * 0.5f : nsize * 0.62f;
					using (Font fnt = new Font("Segoe UI", ffontsize, FontStyle.Bold, GraphicsUnit.Pixel))
					using (StringFormat strfmt = new StringFormat())
					using (SolidBrush brushfore = new SolidBrush(Color.White)) {
						strfmt.Alignment = StringAlignment.Center;
						strfmt.LineAlignment = StringAlignment.Center;
						gfx.DrawString(strtext, fnt, brushfore, new RectangleF(0, 0, nsize, nsize), strfmt);
					}
				}

				return ToIcon(bm);
			}
		}

		private static Icon ToIcon(Bitmap bm) {
			IntPtr hicon = bm.GetHicon();
			try {
				using (Icon icoshared = Icon.FromHandle(hicon)) {
					return (Icon)icoshared.Clone();
				}
			}
			finally {
				NativeMethods.DestroyIcon(hicon);
			}
		}

		private static int GetIconSize() {
			int nsize = SystemInformation.SmallIconSize.Width;
			return nsize > 0 ? nsize : 16;
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
