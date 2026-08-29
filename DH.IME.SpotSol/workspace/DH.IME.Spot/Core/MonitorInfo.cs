//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: MonitorInfo
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Drawing;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.Core {
	internal struct MonitorMetrics {
		private readonly RECT m_rcMonitorArea;
		private readonly RECT m_rcWorkArea;
		private readonly int m_nDpi;

		public MonitorMetrics(RECT rcmonitorarea, RECT rcworkarea, int ndpi) {
			m_rcMonitorArea = rcmonitorarea;
			m_rcWorkArea = rcworkarea;
			m_nDpi = ndpi > 0 ? ndpi : NativeConstants.USER_DEFAULT_SCREEN_DPI;
		}

		public RECT MonitorArea {
			get { return m_rcMonitorArea; }
		}

		public RECT WorkArea {
			get { return m_rcWorkArea; }
		}

		public int Dpi {
			get { return m_nDpi; }
		}

		public int Scale(int nvalue) {
			return (int)Math.Round(nvalue * m_nDpi / (double)NativeConstants.USER_DEFAULT_SCREEN_DPI);
		}
	}

	internal static class MonitorInfo {
		private const int CACHE_TTL_MS = 2000;

		private static readonly Dictionary<IntPtr, CacheEntry> g_dicCache = new Dictionary<IntPtr, CacheEntry>();

		public static MonitorMetrics ForWindow(IntPtr hhwnd) {
			return FromHandle(NativeMethods.MonitorFromWindow(hhwnd, NativeConstants.MONITOR_DEFAULTTONEAREST));
		}

		public static MonitorMetrics ForPoint(Point pt) {
			POINT ptnative = new POINT { X = pt.X, Y = pt.Y };
			return FromHandle(NativeMethods.MonitorFromPoint(ptnative, NativeConstants.MONITOR_DEFAULTTONEAREST));
		}

		public static void InvalidateCache() {
			lock (g_dicCache) {
				g_dicCache.Clear();
			}
		}

		private static MonitorMetrics FromHandle(IntPtr hmonitor) {
			if (hmonitor != IntPtr.Zero) {
				lock (g_dicCache) {
					CacheEntry entry;
					if (g_dicCache.TryGetValue(hmonitor, out entry)
						&& unchecked(Environment.TickCount - entry.TickStamp) < CACHE_TTL_MS) {
						return entry.Metrics;
					}
				}
			}

			MonitorMetrics metrics = QueryHandle(hmonitor);

			if (hmonitor != IntPtr.Zero) {
				lock (g_dicCache) {
					g_dicCache[hmonitor] = new CacheEntry { Metrics = metrics, TickStamp = Environment.TickCount };
				}
			}

			return metrics;
		}

		private static MonitorMetrics QueryHandle(IntPtr hmonitor) {
			MONITORINFO monitorinfo = new MONITORINFO();
			monitorinfo.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(MONITORINFO));

			RECT rcmonitorarea = default(RECT);
			RECT rcworkarea = default(RECT);
			if (NativeMethods.GetMonitorInfo(hmonitor, ref monitorinfo)) {
				rcmonitorarea = monitorinfo.rcMonitor;
				rcworkarea = monitorinfo.rcWork;
			}

			return new MonitorMetrics(rcmonitorarea, rcworkarea, QueryDpi(hmonitor));
		}

		private static int QueryDpi(IntPtr hmonitor) {
			if (hmonitor == IntPtr.Zero) {
				return NativeConstants.USER_DEFAULT_SCREEN_DPI;
			}

			try {
				uint ndpix;
				uint ndpiy;
				if (NativeMethods.GetDpiForMonitor(hmonitor, NativeConstants.MDT_EFFECTIVE_DPI, out ndpix, out ndpiy) == 0 && ndpix > 0) {
					return (int)ndpix;
				}
			}
			catch (DllNotFoundException) {
			}
			catch (EntryPointNotFoundException) {
			}

			return NativeConstants.USER_DEFAULT_SCREEN_DPI;
		}

		private struct CacheEntry {
			public MonitorMetrics Metrics;
			public int TickStamp;
		}
	}
}
