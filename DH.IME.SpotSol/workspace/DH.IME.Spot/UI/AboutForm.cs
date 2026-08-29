//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: AboutForm
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using DH.IME.Spot.Interop;

namespace DH.IME.Spot.UI {
	internal sealed partial class AboutForm : Form {
		private const string GITHUB_URL = "https://github.com/cyberkdh/DH.IME.Spot";

		public AboutForm() {
			InitializeComponent();
			this.m_lblVersion.Text = "Version " + GetVersion();
		}

		protected override void OnShown(EventArgs e) {
			base.OnShown(e);
			this.Activate();
			this.BringToFront();
			NativeMethods.SetForegroundWindow(this.Handle);
		}

		private void OnGitHubLinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			try { Process.Start(GITHUB_URL); }
			catch { }
		}

		private void OnOkClick(object sender, EventArgs e) {
			this.Close();
		}

		private static string GetVersion() {
			Version ver = Assembly.GetExecutingAssembly().GetName().Version;
			return ver != null ? ver.ToString() : "1.0.0.0";
		}
	}
}
