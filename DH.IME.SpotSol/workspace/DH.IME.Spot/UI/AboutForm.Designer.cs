namespace DH.IME.Spot.UI
{
    partial class AboutForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label m_lblTitle;
        private System.Windows.Forms.Label m_lblVersion;
        private System.Windows.Forms.Label m_lblDescription;
        private System.Windows.Forms.LinkLabel m_lnkGitHub;
        private System.Windows.Forms.Button m_btnOk;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.m_lblTitle = new System.Windows.Forms.Label();
            this.m_lblVersion = new System.Windows.Forms.Label();
            this.m_lblDescription = new System.Windows.Forms.Label();
            this.m_lnkGitHub = new System.Windows.Forms.LinkLabel();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // m_lblTitle
            //
            this.m_lblTitle.AutoSize = true;
            this.m_lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.m_lblTitle.Location = new System.Drawing.Point(16, 16);
            this.m_lblTitle.Name = "m_lblTitle";
            this.m_lblTitle.Size = new System.Drawing.Size(105, 21);
            this.m_lblTitle.TabIndex = 0;
            this.m_lblTitle.Text = "DH.IME.Spot";
            //
            // m_lblVersion
            //
            this.m_lblVersion.AutoSize = true;
            this.m_lblVersion.Location = new System.Drawing.Point(18, 48);
            this.m_lblVersion.Name = "m_lblVersion";
            this.m_lblVersion.Size = new System.Drawing.Size(45, 13);
            this.m_lblVersion.TabIndex = 1;
            this.m_lblVersion.Text = "Version";
            //
            // m_lblDescription
            //
            this.m_lblDescription.AutoSize = true;
            this.m_lblDescription.Location = new System.Drawing.Point(18, 74);
            this.m_lblDescription.Name = "m_lblDescription";
            this.m_lblDescription.Size = new System.Drawing.Size(298, 13);
            this.m_lblDescription.TabIndex = 2;
            this.m_lblDescription.Text = "Dynamic IME status indicator - shows the current input mode.";
            //
            // m_lnkGitHub
            //
            this.m_lnkGitHub.AutoSize = true;
            this.m_lnkGitHub.Location = new System.Drawing.Point(18, 100);
            this.m_lnkGitHub.Name = "m_lnkGitHub";
            this.m_lnkGitHub.Size = new System.Drawing.Size(214, 13);
            this.m_lnkGitHub.TabIndex = 3;
            this.m_lnkGitHub.TabStop = true;
            this.m_lnkGitHub.Text = "https://github.com/cyberkdh/DH.IME.Spot";
            this.m_lnkGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnGitHubLinkClicked);
            //
            // m_btnOk
            //
            this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOk.Location = new System.Drawing.Point(280, 128);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(84, 28);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.Click += new System.EventHandler(this.OnOkClick);
            //
            // AboutForm
            //
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnOk;
            this.ClientSize = new System.Drawing.Size(380, 168);
            this.Controls.Add(this.m_lblTitle);
            this.Controls.Add(this.m_lblVersion);
            this.Controls.Add(this.m_lblDescription);
            this.Controls.Add(this.m_lnkGitHub);
            this.Controls.Add(this.m_btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About DH.IME.Spot";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
