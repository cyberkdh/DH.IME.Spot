//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: OptionsAppliedEventArgs
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using DH.IME.Spot.Core;

namespace DH.IME.Spot.UI {
	internal sealed class OptionsAppliedEventArgs : EventArgs {
		private readonly AppSettings m_settings;

		public OptionsAppliedEventArgs(AppSettings settings) {
			m_settings = settings;
		}

		public AppSettings Settings {
			get { return m_settings; }
		}
	}
}
