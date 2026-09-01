namespace DH.IME.Spot.UI
{
    partial class OptionsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl m_tabs;
        private System.Windows.Forms.TabPage m_tabLocks;
        private System.Windows.Forms.TabPage m_tabBadges;
        private System.Windows.Forms.TabPage m_tabFlash;
        private System.Windows.Forms.TabPage m_tabGeneral;

        private System.Windows.Forms.CheckBox m_chkBadgeLockPill;
        private System.Windows.Forms.Label m_lblLockCorner;
        private System.Windows.Forms.Label m_lblLockDotSize;
        private System.Windows.Forms.Label m_lblLockDotColor;
        private System.Windows.Forms.CheckBox m_chkShowCaps;
        private System.Windows.Forms.ComboBox m_cboCapsCorner;
        private System.Windows.Forms.ComboBox m_cboCapsDotSize;
        private System.Windows.Forms.ComboBox m_cboCapsDotColor;
        private System.Windows.Forms.CheckBox m_chkShowNum;
        private System.Windows.Forms.ComboBox m_cboNumCorner;
        private System.Windows.Forms.ComboBox m_cboNumDotSize;
        private System.Windows.Forms.ComboBox m_cboNumDotColor;
        private System.Windows.Forms.CheckBox m_chkShowScroll;
        private System.Windows.Forms.ComboBox m_cboScrollCorner;
        private System.Windows.Forms.ComboBox m_cboScrollDotSize;
        private System.Windows.Forms.ComboBox m_cboScrollDotColor;

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
        private System.Windows.Forms.CheckBox m_chkBadgeShadow;

        private System.Windows.Forms.CheckBox m_chkFlashEnabled;
        private System.Windows.Forms.CheckBox m_chkFlashImeSwitch;
        private System.Windows.Forms.CheckBox m_chkFlashCaps;
        private System.Windows.Forms.CheckBox m_chkFlashNum;
        private System.Windows.Forms.CheckBox m_chkFlashScroll;
        private System.Windows.Forms.Label m_lblFlashDuration;
        private System.Windows.Forms.ComboBox m_cboFlashDuration;
        private System.Windows.Forms.Label m_lblFlashAnchor;
        private System.Windows.Forms.ComboBox m_cboFlashAnchor;
        private System.Windows.Forms.Label m_lblFlashSize;
        private System.Windows.Forms.ComboBox m_cboFlashSize;

        private System.Windows.Forms.CheckBox m_chkFadeIdle;
        private System.Windows.Forms.Label m_lblFadeDelay;
        private System.Windows.Forms.ComboBox m_cboFadeDelay;
        private System.Windows.Forms.Label m_lblFadeAction;
        private System.Windows.Forms.ComboBox m_cboFadeAction;
        private System.Windows.Forms.Label m_lblFadeDim;
        private System.Windows.Forms.ComboBox m_cboFadeDim;
        private System.Windows.Forms.Label m_lblPollInterval;
        private System.Windows.Forms.ComboBox m_cboPollInterval;
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
            this.m_tabs = new System.Windows.Forms.TabControl();
            this.m_tabLocks = new System.Windows.Forms.TabPage();
            this.m_tabBadges = new System.Windows.Forms.TabPage();
            this.m_tabFlash = new System.Windows.Forms.TabPage();
            this.m_tabGeneral = new System.Windows.Forms.TabPage();
            this.m_chkBadgeLockPill = new System.Windows.Forms.CheckBox();
            this.m_lblLockCorner = new System.Windows.Forms.Label();
            this.m_lblLockDotSize = new System.Windows.Forms.Label();
            this.m_lblLockDotColor = new System.Windows.Forms.Label();
            this.m_chkShowCaps = new System.Windows.Forms.CheckBox();
            this.m_cboCapsCorner = new System.Windows.Forms.ComboBox();
            this.m_cboCapsDotSize = new System.Windows.Forms.ComboBox();
            this.m_cboCapsDotColor = new System.Windows.Forms.ComboBox();
            this.m_chkShowNum = new System.Windows.Forms.CheckBox();
            this.m_cboNumCorner = new System.Windows.Forms.ComboBox();
            this.m_cboNumDotSize = new System.Windows.Forms.ComboBox();
            this.m_cboNumDotColor = new System.Windows.Forms.ComboBox();
            this.m_chkShowScroll = new System.Windows.Forms.CheckBox();
            this.m_cboScrollCorner = new System.Windows.Forms.ComboBox();
            this.m_cboScrollDotSize = new System.Windows.Forms.ComboBox();
            this.m_cboScrollDotColor = new System.Windows.Forms.ComboBox();
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
            this.m_chkBadgeShadow = new System.Windows.Forms.CheckBox();
            this.m_chkFlashEnabled = new System.Windows.Forms.CheckBox();
            this.m_chkFlashImeSwitch = new System.Windows.Forms.CheckBox();
            this.m_chkFlashCaps = new System.Windows.Forms.CheckBox();
            this.m_chkFlashNum = new System.Windows.Forms.CheckBox();
            this.m_chkFlashScroll = new System.Windows.Forms.CheckBox();
            this.m_lblFlashDuration = new System.Windows.Forms.Label();
            this.m_cboFlashDuration = new System.Windows.Forms.ComboBox();
            this.m_lblFlashAnchor = new System.Windows.Forms.Label();
            this.m_cboFlashAnchor = new System.Windows.Forms.ComboBox();
            this.m_lblFlashSize = new System.Windows.Forms.Label();
            this.m_cboFlashSize = new System.Windows.Forms.ComboBox();
            this.m_chkFadeIdle = new System.Windows.Forms.CheckBox();
            this.m_lblFadeDelay = new System.Windows.Forms.Label();
            this.m_cboFadeDelay = new System.Windows.Forms.ComboBox();
            this.m_lblFadeAction = new System.Windows.Forms.Label();
            this.m_cboFadeAction = new System.Windows.Forms.ComboBox();
            this.m_lblFadeDim = new System.Windows.Forms.Label();
            this.m_cboFadeDim = new System.Windows.Forms.ComboBox();
            this.m_lblPollInterval = new System.Windows.Forms.Label();
            this.m_cboPollInterval = new System.Windows.Forms.ComboBox();
            this.m_chkRunAtStartup = new System.Windows.Forms.CheckBox();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnApply = new System.Windows.Forms.Button();
            this.m_tabs.SuspendLayout();
            this.m_tabLocks.SuspendLayout();
            this.m_tabBadges.SuspendLayout();
            this.m_tabFlash.SuspendLayout();
            this.m_tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trkOpacity)).BeginInit();
            this.SuspendLayout();
            //
            // m_tabs
            //
            this.m_tabs.Controls.Add(this.m_tabLocks);
            this.m_tabs.Controls.Add(this.m_tabBadges);
            this.m_tabs.Controls.Add(this.m_tabFlash);
            this.m_tabs.Controls.Add(this.m_tabGeneral);
            this.m_tabs.Location = new System.Drawing.Point(12, 12);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedIndex = 0;
            this.m_tabs.Size = new System.Drawing.Size(432, 396);
            this.m_tabs.TabIndex = 0;
            //
            // m_tabLocks
            //
            this.m_tabLocks.Controls.Add(this.m_chkBadgeLockPill);
            this.m_tabLocks.Controls.Add(this.m_lblLockCorner);
            this.m_tabLocks.Controls.Add(this.m_lblLockDotSize);
            this.m_tabLocks.Controls.Add(this.m_lblLockDotColor);
            this.m_tabLocks.Controls.Add(this.m_chkShowCaps);
            this.m_tabLocks.Controls.Add(this.m_cboCapsCorner);
            this.m_tabLocks.Controls.Add(this.m_cboCapsDotSize);
            this.m_tabLocks.Controls.Add(this.m_cboCapsDotColor);
            this.m_tabLocks.Controls.Add(this.m_chkShowNum);
            this.m_tabLocks.Controls.Add(this.m_cboNumCorner);
            this.m_tabLocks.Controls.Add(this.m_cboNumDotSize);
            this.m_tabLocks.Controls.Add(this.m_cboNumDotColor);
            this.m_tabLocks.Controls.Add(this.m_chkShowScroll);
            this.m_tabLocks.Controls.Add(this.m_cboScrollCorner);
            this.m_tabLocks.Controls.Add(this.m_cboScrollDotSize);
            this.m_tabLocks.Controls.Add(this.m_cboScrollDotColor);
            this.m_tabLocks.Location = new System.Drawing.Point(4, 22);
            this.m_tabLocks.Name = "m_tabLocks";
            this.m_tabLocks.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabLocks.Size = new System.Drawing.Size(424, 370);
            this.m_tabLocks.TabIndex = 0;
            this.m_tabLocks.Text = "Lock keys";
            this.m_tabLocks.UseVisualStyleBackColor = true;
            //
            // m_chkBadgeLockPill
            //
            this.m_chkBadgeLockPill.AutoSize = true;
            this.m_chkBadgeLockPill.Location = new System.Drawing.Point(18, 16);
            this.m_chkBadgeLockPill.Name = "m_chkBadgeLockPill";
            this.m_chkBadgeLockPill.Size = new System.Drawing.Size(240, 17);
            this.m_chkBadgeLockPill.TabIndex = 0;
            this.m_chkBadgeLockPill.Text = "Show active lock keys as a small pill";
            this.m_chkBadgeLockPill.UseVisualStyleBackColor = true;
            this.m_chkBadgeLockPill.CheckedChanged += new System.EventHandler(this.OnGroupChanged);
            //
            // m_lblLockCorner
            //
            this.m_lblLockCorner.AutoSize = true;
            this.m_lblLockCorner.Location = new System.Drawing.Point(144, 52);
            this.m_lblLockCorner.Name = "m_lblLockCorner";
            this.m_lblLockCorner.Size = new System.Drawing.Size(39, 13);
            this.m_lblLockCorner.TabIndex = 1;
            this.m_lblLockCorner.Text = "Corner";
            //
            // m_lblLockDotSize
            //
            this.m_lblLockDotSize.AutoSize = true;
            this.m_lblLockDotSize.Location = new System.Drawing.Point(250, 52);
            this.m_lblLockDotSize.Name = "m_lblLockDotSize";
            this.m_lblLockDotSize.Size = new System.Drawing.Size(27, 13);
            this.m_lblLockDotSize.TabIndex = 2;
            this.m_lblLockDotSize.Text = "Size";
            //
            // m_lblLockDotColor
            //
            this.m_lblLockDotColor.AutoSize = true;
            this.m_lblLockDotColor.Location = new System.Drawing.Point(314, 52);
            this.m_lblLockDotColor.Name = "m_lblLockDotColor";
            this.m_lblLockDotColor.Size = new System.Drawing.Size(31, 13);
            this.m_lblLockDotColor.TabIndex = 3;
            this.m_lblLockDotColor.Text = "Color";
            //
            // m_chkShowCaps
            //
            this.m_chkShowCaps.AutoSize = true;
            this.m_chkShowCaps.Location = new System.Drawing.Point(18, 74);
            this.m_chkShowCaps.Name = "m_chkShowCaps";
            this.m_chkShowCaps.Size = new System.Drawing.Size(160, 17);
            this.m_chkShowCaps.TabIndex = 4;
            this.m_chkShowCaps.Text = "Track Caps Lock";
            this.m_chkShowCaps.UseVisualStyleBackColor = true;
            this.m_chkShowCaps.CheckedChanged += new System.EventHandler(this.OnGroupChanged);
            //
            // m_cboCapsCorner
            //
            this.m_cboCapsCorner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCapsCorner.FormattingEnabled = true;
            this.m_cboCapsCorner.Items.AddRange(new object[] {
            "Top-left",
            "Top-right",
            "Bottom-left",
            "Bottom-right"});
            this.m_cboCapsCorner.Location = new System.Drawing.Point(144, 71);
            this.m_cboCapsCorner.Name = "m_cboCapsCorner";
            this.m_cboCapsCorner.Size = new System.Drawing.Size(100, 21);
            this.m_cboCapsCorner.TabIndex = 5;
            this.m_cboCapsCorner.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_cboCapsDotSize
            //
            this.m_cboCapsDotSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCapsDotSize.FormattingEnabled = true;
            this.m_cboCapsDotSize.Items.AddRange(new object[] {
            "6 px",
            "8 px",
            "10 px",
            "12 px",
            "16 px",
            "20 px"});
            this.m_cboCapsDotSize.Location = new System.Drawing.Point(250, 71);
            this.m_cboCapsDotSize.Name = "m_cboCapsDotSize";
            this.m_cboCapsDotSize.Size = new System.Drawing.Size(58, 21);
            this.m_cboCapsDotSize.TabIndex = 6;
            this.m_cboCapsDotSize.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_cboCapsDotColor
            //
            this.m_cboCapsDotColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCapsDotColor.FormattingEnabled = true;
            this.m_cboCapsDotColor.Items.AddRange(new object[] {
            "Amber",
            "Orange",
            "Red",
            "Pink",
            "Purple",
            "Indigo",
            "Blue",
            "Teal",
            "Green",
            "Lime",
            "Gray",
            "White"});
            this.m_cboCapsDotColor.Location = new System.Drawing.Point(314, 71);
            this.m_cboCapsDotColor.Name = "m_cboCapsDotColor";
            this.m_cboCapsDotColor.Size = new System.Drawing.Size(94, 21);
            this.m_cboCapsDotColor.TabIndex = 7;
            this.m_cboCapsDotColor.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_chkShowNum
            //
            this.m_chkShowNum.AutoSize = true;
            this.m_chkShowNum.Location = new System.Drawing.Point(18, 104);
            this.m_chkShowNum.Name = "m_chkShowNum";
            this.m_chkShowNum.Size = new System.Drawing.Size(160, 17);
            this.m_chkShowNum.TabIndex = 8;
            this.m_chkShowNum.Text = "Track Num Lock";
            this.m_chkShowNum.UseVisualStyleBackColor = true;
            this.m_chkShowNum.CheckedChanged += new System.EventHandler(this.OnGroupChanged);
            //
            // m_cboNumCorner
            //
            this.m_cboNumCorner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboNumCorner.FormattingEnabled = true;
            this.m_cboNumCorner.Items.AddRange(new object[] {
            "Top-left",
            "Top-right",
            "Bottom-left",
            "Bottom-right"});
            this.m_cboNumCorner.Location = new System.Drawing.Point(144, 101);
            this.m_cboNumCorner.Name = "m_cboNumCorner";
            this.m_cboNumCorner.Size = new System.Drawing.Size(100, 21);
            this.m_cboNumCorner.TabIndex = 9;
            this.m_cboNumCorner.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_cboNumDotSize
            //
            this.m_cboNumDotSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboNumDotSize.FormattingEnabled = true;
            this.m_cboNumDotSize.Items.AddRange(new object[] {
            "6 px",
            "8 px",
            "10 px",
            "12 px",
            "16 px",
            "20 px"});
            this.m_cboNumDotSize.Location = new System.Drawing.Point(250, 101);
            this.m_cboNumDotSize.Name = "m_cboNumDotSize";
            this.m_cboNumDotSize.Size = new System.Drawing.Size(58, 21);
            this.m_cboNumDotSize.TabIndex = 10;
            this.m_cboNumDotSize.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_cboNumDotColor
            //
            this.m_cboNumDotColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboNumDotColor.FormattingEnabled = true;
            this.m_cboNumDotColor.Items.AddRange(new object[] {
            "Amber",
            "Orange",
            "Red",
            "Pink",
            "Purple",
            "Indigo",
            "Blue",
            "Teal",
            "Green",
            "Lime",
            "Gray",
            "White"});
            this.m_cboNumDotColor.Location = new System.Drawing.Point(314, 101);
            this.m_cboNumDotColor.Name = "m_cboNumDotColor";
            this.m_cboNumDotColor.Size = new System.Drawing.Size(94, 21);
            this.m_cboNumDotColor.TabIndex = 11;
            this.m_cboNumDotColor.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_chkShowScroll
            //
            this.m_chkShowScroll.AutoSize = true;
            this.m_chkShowScroll.Location = new System.Drawing.Point(18, 134);
            this.m_chkShowScroll.Name = "m_chkShowScroll";
            this.m_chkShowScroll.Size = new System.Drawing.Size(160, 17);
            this.m_chkShowScroll.TabIndex = 12;
            this.m_chkShowScroll.Text = "Track Scroll Lock";
            this.m_chkShowScroll.UseVisualStyleBackColor = true;
            this.m_chkShowScroll.CheckedChanged += new System.EventHandler(this.OnGroupChanged);
            //
            // m_cboScrollCorner
            //
            this.m_cboScrollCorner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboScrollCorner.FormattingEnabled = true;
            this.m_cboScrollCorner.Items.AddRange(new object[] {
            "Top-left",
            "Top-right",
            "Bottom-left",
            "Bottom-right"});
            this.m_cboScrollCorner.Location = new System.Drawing.Point(144, 131);
            this.m_cboScrollCorner.Name = "m_cboScrollCorner";
            this.m_cboScrollCorner.Size = new System.Drawing.Size(100, 21);
            this.m_cboScrollCorner.TabIndex = 13;
            this.m_cboScrollCorner.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_cboScrollDotSize
            //
            this.m_cboScrollDotSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboScrollDotSize.FormattingEnabled = true;
            this.m_cboScrollDotSize.Items.AddRange(new object[] {
            "6 px",
            "8 px",
            "10 px",
            "12 px",
            "16 px",
            "20 px"});
            this.m_cboScrollDotSize.Location = new System.Drawing.Point(250, 131);
            this.m_cboScrollDotSize.Name = "m_cboScrollDotSize";
            this.m_cboScrollDotSize.Size = new System.Drawing.Size(58, 21);
            this.m_cboScrollDotSize.TabIndex = 14;
            this.m_cboScrollDotSize.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_cboScrollDotColor
            //
            this.m_cboScrollDotColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboScrollDotColor.FormattingEnabled = true;
            this.m_cboScrollDotColor.Items.AddRange(new object[] {
            "Amber",
            "Orange",
            "Red",
            "Pink",
            "Purple",
            "Indigo",
            "Blue",
            "Teal",
            "Green",
            "Lime",
            "Gray",
            "White"});
            this.m_cboScrollDotColor.Location = new System.Drawing.Point(314, 131);
            this.m_cboScrollDotColor.Name = "m_cboScrollDotColor";
            this.m_cboScrollDotColor.Size = new System.Drawing.Size(94, 21);
            this.m_cboScrollDotColor.TabIndex = 15;
            this.m_cboScrollDotColor.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_tabBadges
            //
            this.m_tabBadges.Controls.Add(this.m_chkActiveWindow);
            this.m_tabBadges.Controls.Add(this.m_lblActiveCorner);
            this.m_tabBadges.Controls.Add(this.m_cboActiveWindowCorner);
            this.m_tabBadges.Controls.Add(this.m_chkCursor);
            this.m_tabBadges.Controls.Add(this.m_lblCursorSize);
            this.m_tabBadges.Controls.Add(this.m_cboCursorSize);
            this.m_tabBadges.Controls.Add(this.m_chkMonitorWidget);
            this.m_tabBadges.Controls.Add(this.m_lblMonitorScope);
            this.m_tabBadges.Controls.Add(this.m_cboMonitorScope);
            this.m_tabBadges.Controls.Add(this.m_lblMonitorCorner);
            this.m_tabBadges.Controls.Add(this.m_cboMonitorWidgetCorner);
            this.m_tabBadges.Controls.Add(this.m_lblOpacity);
            this.m_tabBadges.Controls.Add(this.m_trkOpacity);
            this.m_tabBadges.Controls.Add(this.m_lblOpacityValue);
            this.m_tabBadges.Controls.Add(this.m_chkBadgeShadow);
            this.m_tabBadges.Location = new System.Drawing.Point(4, 22);
            this.m_tabBadges.Name = "m_tabBadges";
            this.m_tabBadges.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabBadges.Size = new System.Drawing.Size(424, 370);
            this.m_tabBadges.TabIndex = 1;
            this.m_tabBadges.Text = "Badge modes";
            this.m_tabBadges.UseVisualStyleBackColor = true;
            //
            // m_chkActiveWindow
            //
            this.m_chkActiveWindow.AutoSize = true;
            this.m_chkActiveWindow.Location = new System.Drawing.Point(16, 16);
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
            this.m_lblActiveCorner.Location = new System.Drawing.Point(36, 42);
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
            this.m_cboActiveWindowCorner.Location = new System.Drawing.Point(150, 38);
            this.m_cboActiveWindowCorner.Name = "m_cboActiveWindowCorner";
            this.m_cboActiveWindowCorner.Size = new System.Drawing.Size(150, 21);
            this.m_cboActiveWindowCorner.TabIndex = 2;
            this.m_cboActiveWindowCorner.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_chkCursor
            //
            this.m_chkCursor.AutoSize = true;
            this.m_chkCursor.Location = new System.Drawing.Point(16, 72);
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
            this.m_lblCursorSize.Location = new System.Drawing.Point(36, 98);
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
            "10%",
            "15%",
            "25%",
            "50%",
            "75%",
            "100%",
            "125%",
            "150%"});
            this.m_cboCursorSize.Location = new System.Drawing.Point(150, 94);
            this.m_cboCursorSize.Name = "m_cboCursorSize";
            this.m_cboCursorSize.Size = new System.Drawing.Size(150, 21);
            this.m_cboCursorSize.TabIndex = 5;
            this.m_cboCursorSize.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_chkMonitorWidget
            //
            this.m_chkMonitorWidget.AutoSize = true;
            this.m_chkMonitorWidget.Location = new System.Drawing.Point(16, 128);
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
            this.m_lblMonitorScope.Location = new System.Drawing.Point(36, 154);
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
            this.m_cboMonitorScope.Location = new System.Drawing.Point(150, 150);
            this.m_cboMonitorScope.Name = "m_cboMonitorScope";
            this.m_cboMonitorScope.Size = new System.Drawing.Size(150, 21);
            this.m_cboMonitorScope.TabIndex = 8;
            this.m_cboMonitorScope.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_lblMonitorCorner
            //
            this.m_lblMonitorCorner.AutoSize = true;
            this.m_lblMonitorCorner.Location = new System.Drawing.Point(36, 184);
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
            this.m_cboMonitorWidgetCorner.Location = new System.Drawing.Point(150, 180);
            this.m_cboMonitorWidgetCorner.Name = "m_cboMonitorWidgetCorner";
            this.m_cboMonitorWidgetCorner.Size = new System.Drawing.Size(150, 21);
            this.m_cboMonitorWidgetCorner.TabIndex = 10;
            this.m_cboMonitorWidgetCorner.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_lblOpacity
            //
            this.m_lblOpacity.AutoSize = true;
            this.m_lblOpacity.Location = new System.Drawing.Point(16, 224);
            this.m_lblOpacity.Name = "m_lblOpacity";
            this.m_lblOpacity.Size = new System.Drawing.Size(46, 13);
            this.m_lblOpacity.TabIndex = 11;
            this.m_lblOpacity.Text = "Opacity:";
            //
            // m_trkOpacity
            //
            this.m_trkOpacity.LargeChange = 20;
            this.m_trkOpacity.Location = new System.Drawing.Point(96, 218);
            this.m_trkOpacity.Maximum = 255;
            this.m_trkOpacity.Minimum = 26;
            this.m_trkOpacity.Name = "m_trkOpacity";
            this.m_trkOpacity.Size = new System.Drawing.Size(240, 45);
            this.m_trkOpacity.SmallChange = 5;
            this.m_trkOpacity.TabIndex = 12;
            this.m_trkOpacity.TickFrequency = 15;
            this.m_trkOpacity.Value = 128;
            this.m_trkOpacity.ValueChanged += new System.EventHandler(this.OnOpacityChanged);
            //
            // m_lblOpacityValue
            //
            this.m_lblOpacityValue.AutoSize = true;
            this.m_lblOpacityValue.Location = new System.Drawing.Point(342, 224);
            this.m_lblOpacityValue.Name = "m_lblOpacityValue";
            this.m_lblOpacityValue.Size = new System.Drawing.Size(21, 13);
            this.m_lblOpacityValue.TabIndex = 13;
            this.m_lblOpacityValue.Text = "0%";
            //
            // m_chkBadgeShadow
            //
            this.m_chkBadgeShadow.AutoSize = true;
            this.m_chkBadgeShadow.Location = new System.Drawing.Point(16, 280);
            this.m_chkBadgeShadow.Name = "m_chkBadgeShadow";
            this.m_chkBadgeShadow.Size = new System.Drawing.Size(140, 17);
            this.m_chkBadgeShadow.TabIndex = 14;
            this.m_chkBadgeShadow.Text = "Drop shadow under badge";
            this.m_chkBadgeShadow.UseVisualStyleBackColor = true;
            this.m_chkBadgeShadow.CheckedChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_tabFlash
            //
            this.m_tabFlash.Controls.Add(this.m_chkFlashEnabled);
            this.m_tabFlash.Controls.Add(this.m_chkFlashImeSwitch);
            this.m_tabFlash.Controls.Add(this.m_chkFlashCaps);
            this.m_tabFlash.Controls.Add(this.m_chkFlashNum);
            this.m_tabFlash.Controls.Add(this.m_chkFlashScroll);
            this.m_tabFlash.Controls.Add(this.m_lblFlashDuration);
            this.m_tabFlash.Controls.Add(this.m_cboFlashDuration);
            this.m_tabFlash.Controls.Add(this.m_lblFlashAnchor);
            this.m_tabFlash.Controls.Add(this.m_cboFlashAnchor);
            this.m_tabFlash.Controls.Add(this.m_lblFlashSize);
            this.m_tabFlash.Controls.Add(this.m_cboFlashSize);
            this.m_tabFlash.Location = new System.Drawing.Point(4, 22);
            this.m_tabFlash.Name = "m_tabFlash";
            this.m_tabFlash.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabFlash.Size = new System.Drawing.Size(424, 370);
            this.m_tabFlash.TabIndex = 2;
            this.m_tabFlash.Text = "Flash";
            this.m_tabFlash.UseVisualStyleBackColor = true;
            //
            // m_chkFlashEnabled
            //
            this.m_chkFlashEnabled.AutoSize = true;
            this.m_chkFlashEnabled.Location = new System.Drawing.Point(16, 16);
            this.m_chkFlashEnabled.Name = "m_chkFlashEnabled";
            this.m_chkFlashEnabled.Size = new System.Drawing.Size(220, 17);
            this.m_chkFlashEnabled.TabIndex = 0;
            this.m_chkFlashEnabled.Text = "Flash a hint near the cursor on change";
            this.m_chkFlashEnabled.UseVisualStyleBackColor = true;
            this.m_chkFlashEnabled.CheckedChanged += new System.EventHandler(this.OnGroupChanged);
            //
            // m_chkFlashImeSwitch
            //
            this.m_chkFlashImeSwitch.AutoSize = true;
            this.m_chkFlashImeSwitch.Location = new System.Drawing.Point(36, 42);
            this.m_chkFlashImeSwitch.Name = "m_chkFlashImeSwitch";
            this.m_chkFlashImeSwitch.Size = new System.Drawing.Size(160, 17);
            this.m_chkFlashImeSwitch.TabIndex = 1;
            this.m_chkFlashImeSwitch.Text = "On Korean/English switch";
            this.m_chkFlashImeSwitch.UseVisualStyleBackColor = true;
            this.m_chkFlashImeSwitch.CheckedChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_chkFlashCaps
            //
            this.m_chkFlashCaps.AutoSize = true;
            this.m_chkFlashCaps.Location = new System.Drawing.Point(36, 66);
            this.m_chkFlashCaps.Name = "m_chkFlashCaps";
            this.m_chkFlashCaps.Size = new System.Drawing.Size(160, 17);
            this.m_chkFlashCaps.TabIndex = 2;
            this.m_chkFlashCaps.Text = "On Caps Lock toggle";
            this.m_chkFlashCaps.UseVisualStyleBackColor = true;
            this.m_chkFlashCaps.CheckedChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_chkFlashNum
            //
            this.m_chkFlashNum.AutoSize = true;
            this.m_chkFlashNum.Location = new System.Drawing.Point(36, 90);
            this.m_chkFlashNum.Name = "m_chkFlashNum";
            this.m_chkFlashNum.Size = new System.Drawing.Size(160, 17);
            this.m_chkFlashNum.TabIndex = 3;
            this.m_chkFlashNum.Text = "On Num Lock toggle";
            this.m_chkFlashNum.UseVisualStyleBackColor = true;
            this.m_chkFlashNum.CheckedChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_chkFlashScroll
            //
            this.m_chkFlashScroll.AutoSize = true;
            this.m_chkFlashScroll.Location = new System.Drawing.Point(36, 114);
            this.m_chkFlashScroll.Name = "m_chkFlashScroll";
            this.m_chkFlashScroll.Size = new System.Drawing.Size(160, 17);
            this.m_chkFlashScroll.TabIndex = 4;
            this.m_chkFlashScroll.Text = "On Scroll Lock toggle";
            this.m_chkFlashScroll.UseVisualStyleBackColor = true;
            this.m_chkFlashScroll.CheckedChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_lblFlashDuration
            //
            this.m_lblFlashDuration.AutoSize = true;
            this.m_lblFlashDuration.Location = new System.Drawing.Point(36, 148);
            this.m_lblFlashDuration.Name = "m_lblFlashDuration";
            this.m_lblFlashDuration.Size = new System.Drawing.Size(55, 13);
            this.m_lblFlashDuration.TabIndex = 5;
            this.m_lblFlashDuration.Text = "Duration:";
            //
            // m_cboFlashDuration
            //
            this.m_cboFlashDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFlashDuration.FormattingEnabled = true;
            this.m_cboFlashDuration.Items.AddRange(new object[] {
            "0.3 s",
            "0.5 s",
            "0.8 s",
            "1.2 s",
            "2.0 s"});
            this.m_cboFlashDuration.Location = new System.Drawing.Point(150, 144);
            this.m_cboFlashDuration.Name = "m_cboFlashDuration";
            this.m_cboFlashDuration.Size = new System.Drawing.Size(150, 21);
            this.m_cboFlashDuration.TabIndex = 6;
            this.m_cboFlashDuration.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_lblFlashAnchor
            //
            this.m_lblFlashAnchor.AutoSize = true;
            this.m_lblFlashAnchor.Location = new System.Drawing.Point(36, 176);
            this.m_lblFlashAnchor.Name = "m_lblFlashAnchor";
            this.m_lblFlashAnchor.Size = new System.Drawing.Size(43, 13);
            this.m_lblFlashAnchor.TabIndex = 7;
            this.m_lblFlashAnchor.Text = "Anchor:";
            //
            // m_cboFlashAnchor
            //
            this.m_cboFlashAnchor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFlashAnchor.FormattingEnabled = true;
            this.m_cboFlashAnchor.Items.AddRange(new object[] {
            "Mouse cursor",
            "Screen center",
            "Active window center"});
            this.m_cboFlashAnchor.Location = new System.Drawing.Point(150, 172);
            this.m_cboFlashAnchor.Name = "m_cboFlashAnchor";
            this.m_cboFlashAnchor.Size = new System.Drawing.Size(150, 21);
            this.m_cboFlashAnchor.TabIndex = 8;
            this.m_cboFlashAnchor.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_lblFlashSize
            //
            this.m_lblFlashSize.AutoSize = true;
            this.m_lblFlashSize.Location = new System.Drawing.Point(36, 204);
            this.m_lblFlashSize.Name = "m_lblFlashSize";
            this.m_lblFlashSize.Size = new System.Drawing.Size(30, 13);
            this.m_lblFlashSize.TabIndex = 9;
            this.m_lblFlashSize.Text = "Size:";
            //
            // m_cboFlashSize
            //
            this.m_cboFlashSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFlashSize.FormattingEnabled = true;
            this.m_cboFlashSize.Items.AddRange(new object[] {
            "Small",
            "Medium",
            "Large"});
            this.m_cboFlashSize.Location = new System.Drawing.Point(150, 200);
            this.m_cboFlashSize.Name = "m_cboFlashSize";
            this.m_cboFlashSize.Size = new System.Drawing.Size(150, 21);
            this.m_cboFlashSize.TabIndex = 10;
            this.m_cboFlashSize.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_tabGeneral
            //
            this.m_tabGeneral.Controls.Add(this.m_chkFadeIdle);
            this.m_tabGeneral.Controls.Add(this.m_lblFadeDelay);
            this.m_tabGeneral.Controls.Add(this.m_cboFadeDelay);
            this.m_tabGeneral.Controls.Add(this.m_lblFadeAction);
            this.m_tabGeneral.Controls.Add(this.m_cboFadeAction);
            this.m_tabGeneral.Controls.Add(this.m_lblFadeDim);
            this.m_tabGeneral.Controls.Add(this.m_cboFadeDim);
            this.m_tabGeneral.Controls.Add(this.m_lblPollInterval);
            this.m_tabGeneral.Controls.Add(this.m_cboPollInterval);
            this.m_tabGeneral.Controls.Add(this.m_chkRunAtStartup);
            this.m_tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.m_tabGeneral.Name = "m_tabGeneral";
            this.m_tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabGeneral.Size = new System.Drawing.Size(424, 370);
            this.m_tabGeneral.TabIndex = 3;
            this.m_tabGeneral.Text = "Fade & general";
            this.m_tabGeneral.UseVisualStyleBackColor = true;
            //
            // m_chkFadeIdle
            //
            this.m_chkFadeIdle.AutoSize = true;
            this.m_chkFadeIdle.Location = new System.Drawing.Point(16, 16);
            this.m_chkFadeIdle.Name = "m_chkFadeIdle";
            this.m_chkFadeIdle.Size = new System.Drawing.Size(240, 17);
            this.m_chkFadeIdle.TabIndex = 0;
            this.m_chkFadeIdle.Text = "Fade the overlay when the state is steady";
            this.m_chkFadeIdle.UseVisualStyleBackColor = true;
            this.m_chkFadeIdle.CheckedChanged += new System.EventHandler(this.OnGroupChanged);
            //
            // m_lblFadeDelay
            //
            this.m_lblFadeDelay.AutoSize = true;
            this.m_lblFadeDelay.Location = new System.Drawing.Point(36, 42);
            this.m_lblFadeDelay.Name = "m_lblFadeDelay";
            this.m_lblFadeDelay.Size = new System.Drawing.Size(37, 13);
            this.m_lblFadeDelay.TabIndex = 1;
            this.m_lblFadeDelay.Text = "Delay:";
            //
            // m_cboFadeDelay
            //
            this.m_cboFadeDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFadeDelay.FormattingEnabled = true;
            this.m_cboFadeDelay.Items.AddRange(new object[] {
            "1 s",
            "2 s",
            "3 s",
            "5 s",
            "8 s"});
            this.m_cboFadeDelay.Location = new System.Drawing.Point(150, 38);
            this.m_cboFadeDelay.Name = "m_cboFadeDelay";
            this.m_cboFadeDelay.Size = new System.Drawing.Size(150, 21);
            this.m_cboFadeDelay.TabIndex = 2;
            this.m_cboFadeDelay.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_lblFadeAction
            //
            this.m_lblFadeAction.AutoSize = true;
            this.m_lblFadeAction.Location = new System.Drawing.Point(36, 70);
            this.m_lblFadeAction.Name = "m_lblFadeAction";
            this.m_lblFadeAction.Size = new System.Drawing.Size(40, 13);
            this.m_lblFadeAction.TabIndex = 3;
            this.m_lblFadeAction.Text = "Action:";
            //
            // m_cboFadeAction
            //
            this.m_cboFadeAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFadeAction.FormattingEnabled = true;
            this.m_cboFadeAction.Items.AddRange(new object[] {
            "Shrink to a dot",
            "Dim",
            "Hide completely"});
            this.m_cboFadeAction.Location = new System.Drawing.Point(150, 66);
            this.m_cboFadeAction.Name = "m_cboFadeAction";
            this.m_cboFadeAction.Size = new System.Drawing.Size(150, 21);
            this.m_cboFadeAction.TabIndex = 4;
            this.m_cboFadeAction.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_lblFadeDim
            //
            this.m_lblFadeDim.AutoSize = true;
            this.m_lblFadeDim.Location = new System.Drawing.Point(36, 98);
            this.m_lblFadeDim.Name = "m_lblFadeDim";
            this.m_lblFadeDim.Size = new System.Drawing.Size(80, 13);
            this.m_lblFadeDim.TabIndex = 5;
            this.m_lblFadeDim.Text = "Dim to opacity:";
            //
            // m_cboFadeDim
            //
            this.m_cboFadeDim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFadeDim.FormattingEnabled = true;
            this.m_cboFadeDim.Items.AddRange(new object[] {
            "10%",
            "20%",
            "30%",
            "50%",
            "75%"});
            this.m_cboFadeDim.Location = new System.Drawing.Point(150, 94);
            this.m_cboFadeDim.Name = "m_cboFadeDim";
            this.m_cboFadeDim.Size = new System.Drawing.Size(150, 21);
            this.m_cboFadeDim.TabIndex = 6;
            this.m_cboFadeDim.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_lblPollInterval
            //
            this.m_lblPollInterval.AutoSize = true;
            this.m_lblPollInterval.Location = new System.Drawing.Point(16, 140);
            this.m_lblPollInterval.Name = "m_lblPollInterval";
            this.m_lblPollInterval.Size = new System.Drawing.Size(95, 13);
            this.m_lblPollInterval.TabIndex = 7;
            this.m_lblPollInterval.Text = "Polling interval:";
            //
            // m_cboPollInterval
            //
            this.m_cboPollInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboPollInterval.FormattingEnabled = true;
            this.m_cboPollInterval.Items.AddRange(new object[] {
            "100 ms",
            "150 ms",
            "250 ms",
            "400 ms",
            "600 ms"});
            this.m_cboPollInterval.Location = new System.Drawing.Point(150, 136);
            this.m_cboPollInterval.Name = "m_cboPollInterval";
            this.m_cboPollInterval.Size = new System.Drawing.Size(150, 21);
            this.m_cboPollInterval.TabIndex = 8;
            this.m_cboPollInterval.SelectedIndexChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_chkRunAtStartup
            //
            this.m_chkRunAtStartup.AutoSize = true;
            this.m_chkRunAtStartup.Location = new System.Drawing.Point(16, 176);
            this.m_chkRunAtStartup.Name = "m_chkRunAtStartup";
            this.m_chkRunAtStartup.Size = new System.Drawing.Size(190, 17);
            this.m_chkRunAtStartup.TabIndex = 9;
            this.m_chkRunAtStartup.Text = "Start automatically when I sign in";
            this.m_chkRunAtStartup.UseVisualStyleBackColor = true;
            this.m_chkRunAtStartup.CheckedChanged += new System.EventHandler(this.OnFieldChanged);
            //
            // m_btnOk
            //
            this.m_btnOk.Location = new System.Drawing.Point(180, 418);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 28);
            this.m_btnOk.TabIndex = 1;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.OnOkClick);
            //
            // m_btnCancel
            //
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(268, 418);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(80, 28);
            this.m_btnCancel.TabIndex = 2;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.OnCancelClick);
            //
            // m_btnApply
            //
            this.m_btnApply.Enabled = false;
            this.m_btnApply.Location = new System.Drawing.Point(356, 418);
            this.m_btnApply.Name = "m_btnApply";
            this.m_btnApply.Size = new System.Drawing.Size(80, 28);
            this.m_btnApply.TabIndex = 3;
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
            this.ClientSize = new System.Drawing.Size(456, 458);
            this.Controls.Add(this.m_tabs);
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
            this.m_tabs.ResumeLayout(false);
            this.m_tabLocks.ResumeLayout(false);
            this.m_tabLocks.PerformLayout();
            this.m_tabBadges.ResumeLayout(false);
            this.m_tabBadges.PerformLayout();
            this.m_tabFlash.ResumeLayout(false);
            this.m_tabFlash.PerformLayout();
            this.m_tabGeneral.ResumeLayout(false);
            this.m_tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trkOpacity)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
