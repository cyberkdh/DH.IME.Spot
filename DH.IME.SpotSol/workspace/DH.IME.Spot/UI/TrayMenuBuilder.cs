//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: TrayMenuBuilder
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Windows.Forms;

namespace DH.IME.Spot.UI {
	internal static class TrayMenuBuilder {
		public static ContextMenuStrip Build(
			ToolStripMenuItem mnupause,
			EventHandler onoptions,
			EventHandler onabout,
			EventHandler onexit) {
			ContextMenuStrip mnu = new ContextMenuStrip();

			ToolStripMenuItem mnuoptions = new ToolStripMenuItem("Options(&O)...");
			mnuoptions.Click += onoptions;

			ToolStripMenuItem mnuabout = new ToolStripMenuItem("About(&A)...");
			mnuabout.Click += onabout;

			ToolStripMenuItem mnuexit = new ToolStripMenuItem("Exit(&X)");
			mnuexit.Click += onexit;

			mnu.Items.Add(mnupause);
			mnu.Items.Add(new ToolStripSeparator());
			mnu.Items.Add(mnuoptions);
			mnu.Items.Add(new ToolStripSeparator());
			mnu.Items.Add(mnuabout);
			mnu.Items.Add(mnuexit);

			return mnu;
		}
	}
}
