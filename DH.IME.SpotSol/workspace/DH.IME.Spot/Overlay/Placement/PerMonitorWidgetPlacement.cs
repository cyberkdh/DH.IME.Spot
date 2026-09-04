//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: PerMonitorWidgetPlacement
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System.Drawing;
using DH.IME.Spot.Core;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Overlay.Placement {
	internal sealed class PerMonitorWidgetPlacement : IBadgePlacement {
		private readonly enumBadgeCorner m_eCorner;

		public PerMonitorWidgetPlacement(enumBadgeCorner ecorner) {
			m_eCorner = ecorner;
		}

		public Point GetLocation(PlacementContext context) {
			int nmargin = context.Monitor.Scale(CornerPlacement.BaseMarginPx);
			RECT rcbounds = context.BoundsMode == enumPlacementBoundsMode.MonitorArea
				? context.Monitor.MonitorArea
				: context.Monitor.WorkArea;
			return CornerPlacement.Locate(
				rcbounds,
				context.BadgeSize,
				m_eCorner,
				nmargin,
				rcbounds);
		}
	}
}
