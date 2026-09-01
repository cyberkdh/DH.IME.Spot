//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: EdgeBarPlacement
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System.Drawing;
using DH.IME.Spot.Core;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Overlay.Placement {
	internal sealed class EdgeBarPlacement : IBadgePlacement {
		private const int BASE_MARGIN_PX = 2;

		private readonly enumEdge m_eEdge;

		public EdgeBarPlacement(enumEdge eedge) {
			m_eEdge = eedge;
		}

		public Point GetLocation(PlacementContext context) {
			RECT rcarea = context.Monitor.WorkArea;
			if (rcarea.Right <= rcarea.Left || rcarea.Bottom <= rcarea.Top) {
				rcarea = context.Monitor.MonitorArea;
			}

			int nareaw = rcarea.Right - rcarea.Left;
			int nareah = rcarea.Bottom - rcarea.Top;
			int nbadgew = context.BadgeSize.Width;
			int nbadgeh = context.BadgeSize.Height;
			int nmargin = context.Monitor.Scale(BASE_MARGIN_PX);

			int nx;
			int ny;
			switch (m_eEdge) {
				case enumEdge.Bottom:
					nx = rcarea.Left + (nareaw - nbadgew) / 2;
					ny = rcarea.Bottom - nbadgeh - nmargin;
					break;
				case enumEdge.Left:
					nx = rcarea.Left + nmargin;
					ny = rcarea.Top + (nareah - nbadgeh) / 2;
					break;
				case enumEdge.Right:
					nx = rcarea.Right - nbadgew - nmargin;
					ny = rcarea.Top + (nareah - nbadgeh) / 2;
					break;
				default:
					nx = rcarea.Left + (nareaw - nbadgew) / 2;
					ny = rcarea.Top + nmargin;
					break;
			}

			return CornerPlacement.Clamp(new Point(nx, ny), context.BadgeSize, rcarea);
		}
	}
}
