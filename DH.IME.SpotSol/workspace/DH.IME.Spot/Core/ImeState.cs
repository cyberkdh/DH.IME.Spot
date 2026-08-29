//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: ImeState
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;

namespace DH.IME.Spot.Core {
	internal enum enumImeKind {
		Unknown,
		Latin,
		Hangul
	}

	internal struct ImeState : IEquatable<ImeState> {
		private readonly enumImeKind m_eKind;
		private readonly bool m_bFullShape;
		private readonly bool m_bKoreanLayoutActive;

		public ImeState(enumImeKind ekind, bool bfullshape, bool bkoreanlayoutactive) {
			m_eKind = ekind;
			m_bFullShape = bfullshape;
			m_bKoreanLayoutActive = bkoreanlayoutactive;
		}

		public enumImeKind Kind {
			get { return m_eKind; }
		}

		public bool FullShape {
			get { return m_bFullShape; }
		}

		public bool KoreanLayoutActive {
			get { return m_bKoreanLayoutActive; }
		}

		public static ImeState Unknown {
			get { return new ImeState(enumImeKind.Unknown, false, false); }
		}

		public bool Equals(ImeState other) {
			return m_eKind == other.m_eKind
				&& m_bFullShape == other.m_bFullShape
				&& m_bKoreanLayoutActive == other.m_bKoreanLayoutActive;
		}

		public override bool Equals(object obj) {
			return obj is ImeState && Equals((ImeState)obj);
		}

		public override int GetHashCode() {
			int nhash = (int)m_eKind;
			nhash = (nhash * 397) ^ (m_bFullShape ? 1 : 0);
			nhash = (nhash * 397) ^ (m_bKoreanLayoutActive ? 1 : 0);
			return nhash;
		}
	}
}
