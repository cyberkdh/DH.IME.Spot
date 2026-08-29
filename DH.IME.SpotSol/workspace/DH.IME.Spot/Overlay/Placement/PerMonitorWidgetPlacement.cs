//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: PerMonitorWidgetPlacement
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System.Drawing;
using DH.IME.Spot.Core;

namespace DH.IME.Spot.Overlay.Placement {
	internal sealed class PerMonitorWidgetPlacement : IBadgePlacement {
		private readonly enumBadgeCorner m_eCorner;

		public PerMonitorWidgetPlacement(enumBadgeCorner ecorner) {
			m_eCorner = ecorner;
		}

		public Point GetLocation(PlacementContext context) {
			int nmargin = context.Monitor.Scale(CornerPlacement.BaseMarginPx);
			return CornerPlacement.Locate(
				context.Monitor.WorkArea,
				context.BadgeSize,
				m_eCorner,
				nmargin,
				context.Monitor.WorkArea);
		}
	}
}
