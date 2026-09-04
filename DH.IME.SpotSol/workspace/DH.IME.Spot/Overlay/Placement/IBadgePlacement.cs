//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: IBadgePlacement
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System.Drawing;
using DH.IME.Spot.Core;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Overlay.Placement {
	internal struct PlacementContext {
		public RECT WindowRect;
		public Point Cursor;
		public Size BadgeSize;
		public MonitorMetrics Monitor;
		public enumPlacementBoundsMode BoundsMode;
	}

	internal interface IBadgePlacement {
		Point GetLocation(PlacementContext context);
	}
}
