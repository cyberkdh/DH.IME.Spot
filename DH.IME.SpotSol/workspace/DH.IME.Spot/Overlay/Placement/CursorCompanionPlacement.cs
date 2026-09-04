//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: CursorCompanionPlacement
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System.Drawing;
using DH.IME.Spot.Core;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Overlay.Placement {
	internal sealed class CursorCompanionPlacement : IBadgePlacement {
		private const int BASE_OFFSET_PX = 18;

		public Point GetLocation(PlacementContext context) {
			int noffset = context.Monitor.Scale(BASE_OFFSET_PX);
			RECT rcbounds = context.BoundsMode == enumPlacementBoundsMode.MonitorArea
				? context.Monitor.MonitorArea
				: context.Monitor.WorkArea;

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
