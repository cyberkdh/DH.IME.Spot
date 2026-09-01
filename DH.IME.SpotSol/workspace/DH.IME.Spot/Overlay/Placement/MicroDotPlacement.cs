//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: MicroDotPlacement
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System.Drawing;
using DH.IME.Spot.Core;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Overlay.Placement {
	internal sealed class MicroDotPlacement : IBadgePlacement {
		private const int CURSOR_OFFSET_PX = 14;

		private readonly enumMicroDotAnchor m_eAnchor;
		private readonly enumBadgeCorner m_eCorner;

		public MicroDotPlacement(enumMicroDotAnchor eanchor, enumBadgeCorner ecorner) {
			m_eAnchor = eanchor;
			m_eCorner = ecorner;
		}

		public Point GetLocation(PlacementContext context) {
			int nmargin = context.Monitor.Scale(CornerPlacement.BaseMarginPx);

			if (m_eAnchor == enumMicroDotAnchor.WindowCorner) {
				return CornerPlacement.Locate(context.WindowRect, context.BadgeSize, m_eCorner, nmargin, context.Monitor.WorkArea);
			}

			if (m_eAnchor == enumMicroDotAnchor.MonitorCorner) {
				return CornerPlacement.Locate(context.Monitor.WorkArea, context.BadgeSize, m_eCorner, nmargin, context.Monitor.WorkArea);
			}

			int noffset = context.Monitor.Scale(CURSOR_OFFSET_PX);
			RECT rcbounds = context.Monitor.WorkArea;

			int nx = context.Cursor.X + noffset;
			int ny = context.Cursor.Y + noffset;

			bool bhasbounds = rcbounds.Right > rcbounds.Left && rcbounds.Bottom > rcbounds.Top;
			if (bhasbounds == true) {
				if (nx + context.BadgeSize.Width > rcbounds.Right) {
					nx = context.Cursor.X - noffset - context.BadgeSize.Width;
				}

				if (ny + context.BadgeSize.Height > rcbounds.Bottom) {
					ny = context.Cursor.Y - noffset - context.BadgeSize.Height;
				}
			}

			return CornerPlacement.Clamp(new Point(nx, ny), context.BadgeSize, rcbounds);
		}
	}
}
