//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: NativeStructs
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Runtime.InteropServices;

namespace DH.IME.Spot.Interop {
	[StructLayout(LayoutKind.Sequential)]
	internal struct RECT {
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;

		public int Width {
			get { return Right - Left; }
		}

		public int Height {
			get { return Bottom - Top; }
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct POINT {
		public int X;
		public int Y;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct SIZE {
		public int cx;
		public int cy;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct BLENDFUNCTION {
		public byte BlendOp;
		public byte BlendFlags;
		public byte SourceConstantAlpha;
		public byte AlphaFormat;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct MONITORINFO {
		public int cbSize;
		public RECT rcMonitor;
		public RECT rcWork;
		public uint dwFlags;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct GUITHREADINFO {
		public int cbSize;
		public uint flags;
		public IntPtr hwndActive;
		public IntPtr hwndFocus;
		public IntPtr hwndCapture;
		public IntPtr hwndMenuOwner;
		public IntPtr hwndMoveSize;
		public IntPtr hwndCaret;
		public RECT rcCaret;
	}
}
