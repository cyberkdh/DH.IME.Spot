namespace DH.IME.Spot.UI
{
    partial class OptionsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox m_grpPlacements;
        private System.Windows.Forms.CheckBox m_chkActiveWindow;
        private System.Windows.Forms.Label m_lblActiveCorner;
        private System.Windows.Forms.ComboBox m_cboActiveWindowCorner;
        private System.Windows.Forms.CheckBox m_chkCursor;
        private System.Windows.Forms.Label m_lblCursorSize;
        private System.Windows.Forms.ComboBox m_cboCursorSize;
        private System.Windows.Forms.CheckBox m_chkMonitorWidget;
        private System.Windows.Forms.Label m_lblMonitorScope;
        private System.Windows.Forms.ComboBox m_cboMonitorScope;
        private System.Windows.Forms.Label m_lblMonitorCorner;
        private System.Windows.Forms.ComboBox m_cboMonitorWidgetCorner;
        private System.Windows.Forms.Label m_lblOpacity;
        private System.Windows.Forms.TrackBar m_trkOpacity;
        private System.Windows.Forms.Label m_lblOpacityValue;
        private System.Windows.Forms.CheckBox m_chkRunAtStartup;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Button m_btnApply;

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
            this.m_grpPlacements = new System.Windows.Forms.GroupBox();
            this.m_chkActiveWindow = new System.Windows.Forms.CheckBox();
            this.m_lblActiveCorner = new System.Windows.Forms.Label();
            this.m_cboActiveWindowCorner = new System.Windows.Forms.ComboBox();
            this.m_chkCursor = new System.Windows.Forms.CheckBox();
            this.m_lblCursorSize = new System.Windows.Forms.Label();
            this.m_cboCursorSize = new System.Windows.Forms.ComboBox();
            this.m_chkMonitorWidget = new System.Windows.Forms.CheckBox();
            this.m_lblMonitorScope = new System.Windows.Forms.Label();
            this.m_cboMonitorScope = new System.Windows.Forms.ComboBox();
            this.m_lblMonitorCorner = new System.Windows.Forms.Label();
            this.m_cboMonitorWidgetCorner = new System.Windows.Forms.ComboBox();
            this.m_lblOpacity = new System.Windows.Forms.Label();
            this.m_trkOpacity = new System.Windows.Forms.TrackBar();
            this.m_lblOpacityValue = new System.Windows.Forms.Label();
            this.m_chkRunAtStartup = new System.Windows.Forms.CheckBox();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnApply = new System.Windows.Forms.Button();
            this.m_grpPlacements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trkOpacity)).BeginInit();
            this.SuspendLayout();
            //
            // m_grpPlacements
            //
            this.m_grpPlacements.Controls.Add(this.m_chkActiveWindow);
            this.m_grpPlacements.Controls.Add(this.m_lblActiveCorner);
            this.m_grpPlacements.Controls.Add(this.m_cboActiveWindowCorner);
            this.m_grpPlacements.Controls.Add(this.m_chkCursor);
            this.m_grpPlacements.Controls.Add(this.m_lblCursorSize);
            this.m_grpPlacements.Controls.Add(this.m_cboCursorSize);
            this.m_grpPlacements.Controls.Add(this.m_chkMonitorWidget);
            this.m_grpPlacements.Controls.Add(this.m_lblMonitorScope);
            this.m_grpPlacements.Controls.Add(this.m_cboMonitorScope);
            this.m_grpPlacements.Controls.Add(this.m_lblMonitorCorner);
            this.m_grpPlacements.Controls.Add(this.m_cboMonitorWidgetCorner);
            this.m_grpPlacements.Location = new System.Drawing.Point(14, 12);
            this.m_grpPlacements.Name = "m_grpPlacements";
            this.m_grpPlacements.Size = new System.Drawing.Size(372, 242);
            this.m_grpPlacements.TabIndex = 0;
            this.m_grpPlacements.TabStop = false;
            this.m_grpPlacements.Text = "Placements (any combination)";
            //
            // m_chkActiveWindow
            //
            this.m_chkActiveWindow.AutoSize = true;
            this.m_chkActiveWindow.Location = new System.Drawing.Point(16, 26);
            this.m_chkActiveWindow.Name = "m_chkActiveWindow";
            this.m_chkActiveWindow.Size = new System.Drawing.Size(174, 17);
            this.m_chkActiveWindow.TabIndex = 0;
            this.m_chkActiveWindow.Text = "Near the active window corner";
            this.m_chkActiveWindow.UseVisualStyleBackColor = true;
            this.m_chkActiveWindow.CheckedChanged += new System.EventHandler(this.OnGroupChanged);
            //
            // m_lblActiveCorner
            //
            this.m_lblActiveCorner.AutoSize = true;
            this.m_lblActiveCorner.Location = new System.Drawing.Point(36, 54);
            this.m_lblActiveCorner.Name = "m_lblActiveCorner";
            this.m_lblActiveCorner.Size = new System.Drawing.Size(42, 13);
            this.m_lblActiveCorner.TabIndex = 1;
            this.m_lblActiveCorner.Text = "Corner:";
            //
            // m_cboActiveWindowCorner
            //
            this.m_cboActiveWindowCorner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboActiveWindowCorner.FormattingEnabled = true;
            this.m_cboActiveWindowCorner.Items.AddRange(new object[] {
            "Top-left",
            "Top-right",
            "Bottom-left",
            "Bottom-right"});
            this.m_cboActiveWindowCorner.Location = new System.Drawing.Point(96, 50);
            this.m_cboActiveWindowCorner.Name = "m_cboActiveWindowCorner";
            this.m_cboActiveWindowCorner.Size = new System.Drawing.Size(150, 21);
            this.m_cboActiveWindowCorner.TabIndex = 2;
            this.m_cboActiveWindowCorner.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_chkCursor
            //
            this.m_chkCursor.AutoSize = true;
            this.m_chkCursor.Location = new System.Drawing.Point(16, 88);
            this.m_chkCursor.Name = "m_chkCursor";
            this.m_chkCursor.Size = new System.Drawing.Size(144, 17);
            this.m_chkCursor.TabIndex = 3;
            this.m_chkCursor.Text = "Follow the mouse cursor";
            this.m_chkCursor.UseVisualStyleBackColor = true;
            this.m_chkCursor.CheckedChanged += new System.EventHandler(this.OnGroupChanged);
            //
            // m_lblCursorSize
            //
            this.m_lblCursorSize.AutoSize = true;
            this.m_lblCursorSize.Location = new System.Drawing.Point(36, 116);
            this.m_lblCursorSize.Name = "m_lblCursorSize";
            this.m_lblCursorSize.Size = new System.Drawing.Size(30, 13);
            this.m_lblCursorSize.TabIndex = 4;
            this.m_lblCursorSize.Text = "Size:";
            //
            // m_cboCursorSize
            //
            this.m_cboCursorSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCursorSize.FormattingEnabled = true;
            this.m_cboCursorSize.Items.AddRange(new object[] {
            "25%",
            "50%",
            "75%",
            "100%",
            "125%",
            "150%"});
            this.m_cboCursorSize.Location = new System.Drawing.Point(96, 112);
            this.m_cboCursorSize.Name = "m_cboCursorSize";
            this.m_cboCursorSize.Size = new System.Drawing.Size(150, 21);
            this.m_cboCursorSize.TabIndex = 5;
            this.m_cboCursorSize.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_chkMonitorWidget
            //
            this.m_chkMonitorWidget.AutoSize = true;
            this.m_chkMonitorWidget.Location = new System.Drawing.Point(16, 148);
            this.m_chkMonitorWidget.Name = "m_chkMonitorWidget";
            this.m_chkMonitorWidget.Size = new System.Drawing.Size(164, 17);
            this.m_chkMonitorWidget.TabIndex = 6;
            this.m_chkMonitorWidget.Text = "Fixed corner of the monitor";
            this.m_chkMonitorWidget.UseVisualStyleBackColor = true;
            this.m_chkMonitorWidget.CheckedChanged += new System.EventHandler(this.OnGroupChanged);
            //
            // m_lblMonitorScope
            //
            this.m_lblMonitorScope.AutoSize = true;
            this.m_lblMonitorScope.Location = new System.Drawing.Point(36, 176);
            this.m_lblMonitorScope.Name = "m_lblMonitorScope";
            this.m_lblMonitorScope.Size = new System.Drawing.Size(49, 13);
            this.m_lblMonitorScope.TabIndex = 7;
            this.m_lblMonitorScope.Text = "Show on:";
            //
            // m_cboMonitorScope
            //
            this.m_cboMonitorScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboMonitorScope.FormattingEnabled = true;
            this.m_cboMonitorScope.Items.AddRange(new object[] {
            "Current monitor only",
            "All monitors"});
            this.m_cboMonitorScope.Location = new System.Drawing.Point(96, 172);
            this.m_cboMonitorScope.Name = "m_cboMonitorScope";
            this.m_cboMonitorScope.Size = new System.Drawing.Size(150, 21);
            this.m_cboMonitorScope.TabIndex = 8;
            this.m_cboMonitorScope.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_lblMonitorCorner
            //
            this.m_lblMonitorCorner.AutoSize = true;
            this.m_lblMonitorCorner.Location = new System.Drawing.Point(36, 206);
            this.m_lblMonitorCorner.Name = "m_lblMonitorCorner";
            this.m_lblMonitorCorner.Size = new System.Drawing.Size(42, 13);
            this.m_lblMonitorCorner.TabIndex = 9;
            this.m_lblMonitorCorner.Text = "Corner:";
            //
            // m_cboMonitorWidgetCorner
            //
            this.m_cboMonitorWidgetCorner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboMonitorWidgetCorner.FormattingEnabled = true;
            this.m_cboMonitorWidgetCorner.Items.AddRange(new object[] {
            "Top-left",
            "Top-right",
            "Bottom-left",
            "Bottom-right"});
            this.m_cboMonitorWidgetCorner.Location = new System.Drawing.Point(96, 202);
            this.m_cboMonitorWidgetCorner.Name = "m_cboMonitorWidgetCorner";
            this.m_cboMonitorWidgetCorner.Size = new System.Drawing.Size(150, 21);
            this.m_cboMonitorWidgetCorner.TabIndex = 10;
            this.m_cboMonitorWidgetCorner.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_lblOpacity
            //
            this.m_lblOpacity.AutoSize = true;
            this.m_lblOpacity.Location = new System.Drawing.Point(18, 270);
            this.m_lblOpacity.Name = "m_lblOpacity";
            this.m_lblOpacity.Size = new System.Drawing.Size(46, 13);
            this.m_lblOpacity.TabIndex = 1;
            this.m_lblOpacity.Text = "Opacity:";
            //
            // m_trkOpacity
            //
            this.m_trkOpacity.LargeChange = 20;
            this.m_trkOpacity.Location = new System.Drawing.Point(104, 264);
            this.m_trkOpacity.Maximum = 255;
            this.m_trkOpacity.Minimum = 26;
            this.m_trkOpacity.Name = "m_trkOpacity";
            this.m_trkOpacity.Size = new System.Drawing.Size(220, 45);
            this.m_trkOpacity.SmallChange = 5;
            this.m_trkOpacity.TabIndex = 2;
            this.m_trkOpacity.TickFrequency = 15;
            this.m_trkOpacity.Value = 128;
            this.m_trkOpacity.ValueChanged += new System.EventHandler(this.OnOpacityChanged);
            //
            // m_lblOpacityValue
            //
            this.m_lblOpacityValue.AutoSize = true;
            this.m_lblOpacityValue.Location = new System.Drawing.Point(330, 270);
            this.m_lblOpacityValue.Name = "m_lblOpacityValue";
            this.m_lblOpacityValue.Size = new System.Drawing.Size(21, 13);
            this.m_lblOpacityValue.TabIndex = 3;
            this.m_lblOpacityValue.Text = "0%";
            //
            // m_chkRunAtStartup
            //
            this.m_chkRunAtStartup.AutoSize = true;
            this.m_chkRunAtStartup.Location = new System.Drawing.Point(18, 318);
            this.m_chkRunAtStartup.Name = "m_chkRunAtStartup";
            this.m_chkRunAtStartup.Size = new System.Drawing.Size(190, 17);
            this.m_chkRunAtStartup.TabIndex = 4;
            this.m_chkRunAtStartup.Text = "Start automatically when I sign in";
            this.m_chkRunAtStartup.UseVisualStyleBackColor = true;
            this.m_chkRunAtStartup.CheckedChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_btnOk
            //
            this.m_btnOk.Location = new System.Drawing.Point(122, 346);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 28);
            this.m_btnOk.TabIndex = 5;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.OnOkClick);
            //
            // m_btnCancel
            //
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(210, 346);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(80, 28);
            this.m_btnCancel.TabIndex = 6;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.OnCancelClick);
            //
            // m_btnApply
            //
            this.m_btnApply.Enabled = false;
            this.m_btnApply.Location = new System.Drawing.Point(298, 346);
            this.m_btnApply.Name = "m_btnApply";
            this.m_btnApply.Size = new System.Drawing.Size(80, 28);
            this.m_btnApply.TabIndex = 7;
            this.m_btnApply.Text = "Apply";
            this.m_btnApply.UseVisualStyleBackColor = true;
            this.m_btnApply.Click += new System.EventHandler(this.OnApplyClick);
            //
            // OptionsForm
            //
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 386);
            this.Controls.Add(this.m_grpPlacements);
            this.Controls.Add(this.m_lblOpacity);
            this.Controls.Add(this.m_trkOpacity);
            this.Controls.Add(this.m_lblOpacityValue);
            this.Controls.Add(this.m_chkRunAtStartup);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DH.IME.Spot Options";
            this.m_grpPlacements.ResumeLayout(false);
            this.m_grpPlacements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trkOpacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
