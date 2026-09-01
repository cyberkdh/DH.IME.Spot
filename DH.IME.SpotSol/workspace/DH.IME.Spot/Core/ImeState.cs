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
		private readonly bool m_bCapsLock;
		private readonly bool m_bNumLock;
		private readonly bool m_bScrollLock;

		public ImeState(enumImeKind ekind, bool bfullshape, bool bkoreanlayoutactive)
			: this(ekind, bfullshape, bkoreanlayoutactive, false, false, false) {
		}

		public ImeState(
			enumImeKind ekind,
			bool bfullshape,
			bool bkoreanlayoutactive,
			bool bcapslock,
			bool bnumlock,
			bool bscrolllock) {
			m_eKind = ekind;
			m_bFullShape = bfullshape;
			m_bKoreanLayoutActive = bkoreanlayoutactive;
			m_bCapsLock = bcapslock;
			m_bNumLock = bnumlock;
			m_bScrollLock = bscrolllock;
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

		public bool CapsLock {
			get { return m_bCapsLock; }
		}

		public bool NumLock {
			get { return m_bNumLock; }
		}

		public bool ScrollLock {
			get { return m_bScrollLock; }
		}

		public static ImeState Unknown {
			get { return new ImeState(enumImeKind.Unknown, false, false, false, false, false); }
		}

		public ImeState WithLocks(bool bcapslock, bool bnumlock, bool bscrolllock) {
			return new ImeState(m_eKind, m_bFullShape, m_bKoreanLayoutActive, bcapslock, bnumlock, bscrolllock);
		}

		public bool Equals(ImeState other) {
			return m_eKind == other.m_eKind
				&& m_bFullShape == other.m_bFullShape
				&& m_bKoreanLayoutActive == other.m_bKoreanLayoutActive
				&& m_bCapsLock == other.m_bCapsLock
				&& m_bNumLock == other.m_bNumLock
				&& m_bScrollLock == other.m_bScrollLock;
		}

		public override bool Equals(object obj) {
			return obj is ImeState && Equals((ImeState)obj);
		}

		public override int GetHashCode() {
			int nhash = (int)m_eKind;
			nhash = (nhash * 397) ^ (m_bFullShape ? 1 : 0);
			nhash = (nhash * 397) ^ (m_bKoreanLayoutActive ? 1 : 0);
			nhash = (nhash * 397) ^ (m_bCapsLock ? 1 : 0);
			nhash = (nhash * 397) ^ (m_bNumLock ? 1 : 0);
			nhash = (nhash * 397) ^ (m_bScrollLock ? 1 : 0);
			return nhash;
		}
	}
}
