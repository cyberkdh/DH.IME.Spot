//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: ForegroundTracker
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Diagnostics;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Core {
	internal struct ForegroundInfo {
		private readonly IntPtr m_hHwnd;
		private readonly uint m_nThreadId;
		private readonly uint m_nProcessId;
		private readonly string m_strProcessName;

		public ForegroundInfo(IntPtr hhwnd, uint nthreadid, uint nprocessid, string strprocessname) {
			m_hHwnd = hhwnd;
			m_nThreadId = nthreadid;
			m_nProcessId = nprocessid;
			m_strProcessName = strprocessname ?? string.Empty;
		}

		public IntPtr Hwnd {
			get { return m_hHwnd; }
		}

		public uint ThreadId {
			get { return m_nThreadId; }
		}

		public uint ProcessId {
			get { return m_nProcessId; }
		}

		public string ProcessName {
			get { return m_strProcessName; }
		}

		public bool IsValid {
			get { return m_hHwnd != IntPtr.Zero && m_nThreadId != 0; }
		}

		public static ForegroundInfo Empty {
			get { return new ForegroundInfo(IntPtr.Zero, 0, 0, string.Empty); }
		}
	}

	internal static class ForegroundTracker {
		public static ForegroundInfo Current() {
			IntPtr hhwnd = NativeMethods.GetForegroundWindow();
			if (hhwnd == IntPtr.Zero) {
				return ForegroundInfo.Empty;
			}

			uint nprocessid;
			uint nthreadid = NativeMethods.GetWindowThreadProcessId(hhwnd, out nprocessid);
			string strname = SafeProcessName(nprocessid);
			return new ForegroundInfo(hhwnd, nthreadid, nprocessid, strname);
		}

		private static string SafeProcessName(uint nprocessid) {
			if (nprocessid == 0) {
				return string.Empty;
			}

			try {
				using (Process proc = Process.GetProcessById((int)nprocessid)) {
					return proc.ProcessName;
				}
			}
			catch {
			}

			return string.Empty;
		}
	}
}
