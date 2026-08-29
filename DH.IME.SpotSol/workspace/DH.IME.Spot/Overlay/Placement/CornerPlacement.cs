//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: CornerPlacement
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System.Drawing;
using DH.IME.Spot.Core;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Overlay.Placement {
	internal static class CornerPlacement {
		public const int BaseMarginPx = 8;

		public static Point Locate(RECT rcarea, Size szbadge, enumBadgeCorner ecorner, int nmargin, RECT rcclampbounds) {
			int nleft = rcarea.Left + nmargin;
			int nright = rcarea.Right - szbadge.Width - nmargin;
			int ntop = rcarea.Top + nmargin;
			int nbottom = rcarea.Bottom - szbadge.Height - nmargin;

			int nx;
			int ny;
			switch (ecorner) {
				case enumBadgeCorner.TopLeft:
					nx = nleft;
					ny = ntop;
					break;
				case enumBadgeCorner.BottomLeft:
					nx = nleft;
					ny = nbottom;
					break;
				case enumBadgeCorner.BottomRight:
					nx = nright;
					ny = nbottom;
					break;
				default:
					nx = nright;
					ny = ntop;
					break;
			}

			return Clamp(new Point(nx, ny), szbadge, rcclampbounds);
		}

		public static Point Clamp(Point ptlocation, Size szbadge, RECT rcbounds) {
			bool bhasbounds = rcbounds.Right > rcbounds.Left && rcbounds.Bottom > rcbounds.Top;
			if (bhasbounds == false) {
				return ptlocation;
			}

			int nx = ClampValue(ptlocation.X, rcbounds.Left, rcbounds.Right - szbadge.Width);
			int ny = ClampValue(ptlocation.Y, rcbounds.Top, rcbounds.Bottom - szbadge.Height);
			return new Point(nx, ny);
		}

		private static int ClampValue(int nvalue, int nmin, int nmax) {
			if (nmax < nmin) {
				return nmin;
			}

			if (nvalue < nmin) {
				return nmin;
			}

			return nvalue > nmax ? nmax : nvalue;
		}
	}
}
