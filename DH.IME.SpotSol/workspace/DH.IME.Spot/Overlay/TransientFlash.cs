//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DH.IME.Spot
//	Author			: CYBERKDH
//	Module			: TransientFlash
//	History			:
//	Copyrights		: Copyright (C)CYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DH.IME.Spot.Overlay {
	internal sealed class TransientFlash : IDisposable {
		private readonly OverlayWindow m_window;
		private readonly Timer m_tmrHide;
		private bool m_bDisposed;

		public TransientFlash() {
			m_window = new OverlayWindow();
			m_tmrHide = new Timer();
			m_tmrHide.Interval = 800;
			m_tmrHide.Tick += OnHideTick;
		}

		public void Show(Bitmap bmcontent, Point ptcenter, int ndurationms) {
			if (m_bDisposed == true || bmcontent == null) {
				return;
			}

			Point ptlocation = new Point(ptcenter.X - bmcontent.Width / 2, ptcenter.Y - bmcontent.Height / 2);
			m_window.SetContent(bmcontent, ptlocation);
			m_window.ShowNoActivate();

			m_tmrHide.Stop();
			m_tmrHide.Interval = ndurationms < 1 ? 1 : ndurationms;
			m_tmrHide.Start();
		}

		public void HideNow() {
			if (m_bDisposed == true) {
				return;
			}

			m_tmrHide.Stop();
			m_window.HideOverlay();
		}

		private void OnHideTick(object sender, EventArgs e) {
			m_tmrHide.Stop();
			m_window.HideOverlay();
		}

		public void Dispose() {
			if (m_bDisposed == true) {
				return;
			}

			m_bDisposed = true;
			m_tmrHide.Tick -= OnHideTick;
			m_tmrHide.Stop();
			m_tmrHide.Dispose();
			m_window.Dispose();
		}
	}
}
