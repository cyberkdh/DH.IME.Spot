//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: Program
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Threading;
using System.Windows.Forms;

namespace DH.IME.Spot.App {
	internal static class Program {
		private const string MUTEX_NAME = "DH.IME.Spot.SingleInstance.{AD26CBB0-A084-423C-BE68-F1461BF199AE}";

		[STAThread]
		private static void Main() {
			bool bcreatednew;
			using (Mutex mtx = new Mutex(true, MUTEX_NAME, out bcreatednew)) {
				if (bcreatednew == false) {
					return;
				}

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				Application.ThreadException += (s, e) => ShowFatal(e.Exception);
				AppDomain.CurrentDomain.UnhandledException += (s, e) => ShowFatal(e.ExceptionObject as Exception);

				using (TrayApplicationContext context = new TrayApplicationContext()) {
					Application.Run(context);
				}

				GC.KeepAlive(mtx);
			}
		}

		private static void ShowFatal(Exception ex) {
			MessageBox.Show(
				ex != null ? ex.ToString() : "An unknown error occurred.",
				"DH.IME.Spot - Error",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
		}
	}
}
