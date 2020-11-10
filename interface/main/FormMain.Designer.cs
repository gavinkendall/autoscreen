using System.Windows.Forms;

namespace AutoScreenCapture
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripSplitButtonStartScreenCapture = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSplitButtonStopScreenCapture = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSplitButtonKeyboardShortcuts = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSplitButtonHelp = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControlViews = new System.Windows.Forms.TabControl();
            this.contextMenuStripScreenshotPreview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listBoxScreenshots = new System.Windows.Forms.ListBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripSystemTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorAbout = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemShowInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHideInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorInterface = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemShowScreenCaptureStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorTools = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemStartScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStopScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorScreenCapture = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemCaptureNowArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCaptureNowEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorCaptureNow = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRegionSelectClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectAutoSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorRegionSelect = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemApplyLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorApplyLabel = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlModules = new System.Windows.Forms.TabControl();
            this.tabPageSetup = new System.Windows.Forms.TabPage();
            this.groupBoxApplicationFocus = new System.Windows.Forms.GroupBox();
            this.buttonApplicationFocusTest = new System.Windows.Forms.Button();
            this.buttonApplicationFocusRefresh = new System.Windows.Forms.Button();
            this.comboBoxProcessList = new System.Windows.Forms.ComboBox();
            this.groupBoxRegionSelectAutoSave = new System.Windows.Forms.GroupBox();
            this.buttonBrowseFolder = new System.Windows.Forms.Button();
            this.labelAutoSaveMacro = new System.Windows.Forms.Label();
            this.labelAutoSaveFolder = new System.Windows.Forms.Label();
            this.textBoxAutoSaveMacro = new System.Windows.Forms.TextBox();
            this.textBoxAutoSaveFolder = new System.Windows.Forms.TextBox();
            this.groupBoxActiveWindowTitle = new System.Windows.Forms.GroupBox();
            this.textBoxActiveWindowTitle = new System.Windows.Forms.TextBox();
            this.checkBoxActiveWindowTitle = new System.Windows.Forms.CheckBox();
            this.groupBoxSecurity = new System.Windows.Forms.GroupBox();
            this.labelPasswordDescription = new System.Windows.Forms.Label();
            this.buttonSetPassphrase = new System.Windows.Forms.Button();
            this.textBoxPassphrase = new System.Windows.Forms.TextBox();
            this.checkBoxScreenshotLabel = new System.Windows.Forms.CheckBox();
            this.comboBoxScreenshotLabel = new System.Windows.Forms.ComboBox();
            this.groupBoxCaptureDelay = new System.Windows.Forms.GroupBox();
            this.labelLimit = new System.Windows.Forms.Label();
            this.checkBoxInitialScreenshot = new System.Windows.Forms.CheckBox();
            this.numericUpDownCaptureLimit = new System.Windows.Forms.NumericUpDown();
            this.checkBoxCaptureLimit = new System.Windows.Forms.CheckBox();
            this.labelMillisecondsInterval = new System.Windows.Forms.Label();
            this.numericUpDownMillisecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.labelSecondsInterval = new System.Windows.Forms.Label();
            this.labelMinutesInterval = new System.Windows.Forms.Label();
            this.labelHoursInterval = new System.Windows.Forms.Label();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.tabPageScreenshots = new System.Windows.Forms.TabPage();
            this.labelDays = new System.Windows.Forms.Label();
            this.numericUpDownKeepScreenshotsForDays = new System.Windows.Forms.NumericUpDown();
            this.labelKeepScreenshots = new System.Windows.Forms.Label();
            this.tabPageScreens = new System.Windows.Forms.TabPage();
            this.tabPageRegions = new System.Windows.Forms.TabPage();
            this.tabPageEditors = new System.Windows.Forms.TabPage();
            this.tabPageSchedules = new System.Windows.Forms.TabPage();
            this.tabPageTags = new System.Windows.Forms.TabPage();
            this.tabPageTriggers = new System.Windows.Forms.TabPage();
            this.timerScheduledCapture = new System.Windows.Forms.Timer(this.components);
            this.timerScreenCapture = new System.Windows.Forms.Timer(this.components);
            this.labelScreenshotTitle = new System.Windows.Forms.Label();
            this.textBoxScreenshotTitle = new System.Windows.Forms.TextBox();
            this.labelScreenshotFormat = new System.Windows.Forms.Label();
            this.textBoxScreenshotFormat = new System.Windows.Forms.TextBox();
            this.labelScreenshotWidth = new System.Windows.Forms.Label();
            this.textBoxScreenshotWidth = new System.Windows.Forms.TextBox();
            this.labelScreenshotHeight = new System.Windows.Forms.Label();
            this.textBoxScreenshotHeight = new System.Windows.Forms.TextBox();
            this.labelScreenshotDate = new System.Windows.Forms.Label();
            this.textBoxScreenshotDate = new System.Windows.Forms.TextBox();
            this.labelScreenshotTime = new System.Windows.Forms.Label();
            this.textBoxScreenshotTime = new System.Windows.Forms.TextBox();
            this.comboBoxFilterValue = new System.Windows.Forms.ComboBox();
            this.labelFilter = new System.Windows.Forms.Label();
            this.buttonRefreshFilterValues = new System.Windows.Forms.Button();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.timerPerformMaintenance = new System.Windows.Forms.Timer(this.components);
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.labelLabel = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.timerShowNextHelpTip = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.contextMenuStripSystemTrayIcon.SuspendLayout();
            this.tabControlModules.SuspendLayout();
            this.tabPageSetup.SuspendLayout();
            this.groupBoxApplicationFocus.SuspendLayout();
            this.groupBoxRegionSelectAutoSave.SuspendLayout();
            this.groupBoxActiveWindowTitle.SuspendLayout();
            this.groupBoxSecurity.SuspendLayout();
            this.groupBoxCaptureDelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            this.tabPageScreenshots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeepScreenshotsForDays)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(0, 56);
            this.monthCalendar.MaxSelectionCount = 1;
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.ShowWeekNumbers = true;
            this.monthCalendar.TabIndex = 0;
            this.monthCalendar.TabStop = false;
            this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.DateSelected_monthCalendar);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonStartScreenCapture,
            this.toolStripSplitButtonStopScreenCapture,
            this.toolStripSplitButtonKeyboardShortcuts,
            this.toolStripSplitButtonHelp,
            this.toolStripInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 436);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(833, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // toolStripSplitButtonStartScreenCapture
            // 
            this.toolStripSplitButtonStartScreenCapture.AutoToolTip = false;
            this.toolStripSplitButtonStartScreenCapture.DropDownButtonWidth = 0;
            this.toolStripSplitButtonStartScreenCapture.Enabled = false;
            this.toolStripSplitButtonStartScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.start_screen_capture;
            this.toolStripSplitButtonStartScreenCapture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripSplitButtonStartScreenCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonStartScreenCapture.Name = "toolStripSplitButtonStartScreenCapture";
            this.toolStripSplitButtonStartScreenCapture.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSplitButtonStartScreenCapture.Size = new System.Drawing.Size(135, 20);
            this.toolStripSplitButtonStartScreenCapture.Text = "Start Screen Capture";
            this.toolStripSplitButtonStartScreenCapture.ButtonClick += new System.EventHandler(this.toolStripMenuItemStartScreenCapture_Click);
            // 
            // toolStripSplitButtonStopScreenCapture
            // 
            this.toolStripSplitButtonStopScreenCapture.AutoToolTip = false;
            this.toolStripSplitButtonStopScreenCapture.DropDownButtonWidth = 0;
            this.toolStripSplitButtonStopScreenCapture.Enabled = false;
            this.toolStripSplitButtonStopScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.stop_screen_capture;
            this.toolStripSplitButtonStopScreenCapture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripSplitButtonStopScreenCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonStopScreenCapture.Name = "toolStripSplitButtonStopScreenCapture";
            this.toolStripSplitButtonStopScreenCapture.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSplitButtonStopScreenCapture.Size = new System.Drawing.Size(135, 20);
            this.toolStripSplitButtonStopScreenCapture.Text = "Stop Screen Capture";
            this.toolStripSplitButtonStopScreenCapture.ButtonClick += new System.EventHandler(this.toolStripMenuItemStopScreenCapture_Click);
            // 
            // toolStripSplitButtonKeyboardShortcuts
            // 
            this.toolStripSplitButtonKeyboardShortcuts.AutoToolTip = false;
            this.toolStripSplitButtonKeyboardShortcuts.DropDownButtonWidth = 0;
            this.toolStripSplitButtonKeyboardShortcuts.Image = global::AutoScreenCapture.Properties.Resources.keyboard;
            this.toolStripSplitButtonKeyboardShortcuts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripSplitButtonKeyboardShortcuts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonKeyboardShortcuts.Name = "toolStripSplitButtonKeyboardShortcuts";
            this.toolStripSplitButtonKeyboardShortcuts.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSplitButtonKeyboardShortcuts.Size = new System.Drawing.Size(131, 20);
            this.toolStripSplitButtonKeyboardShortcuts.Text = "Keyboard Shortcuts";
            this.toolStripSplitButtonKeyboardShortcuts.ButtonClick += new System.EventHandler(this.toolStripSplitButtonKeyboardShortcuts_ButtonClick);
            // 
            // toolStripSplitButtonHelp
            // 
            this.toolStripSplitButtonHelp.Image = global::AutoScreenCapture.Properties.Resources.help;
            this.toolStripSplitButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonHelp.Name = "toolStripSplitButtonHelp";
            this.toolStripSplitButtonHelp.Size = new System.Drawing.Size(64, 20);
            this.toolStripSplitButtonHelp.Text = "Help";
            this.toolStripSplitButtonHelp.Visible = false;
            // 
            // toolStripInfo
            // 
            this.toolStripInfo.AutoSize = false;
            this.toolStripInfo.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)));
            this.toolStripInfo.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripInfo.Name = "toolStripInfo";
            this.toolStripInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripInfo.Size = new System.Drawing.Size(417, 17);
            this.toolStripInfo.Spring = true;
            this.toolStripInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // tabControlViews
            // 
            this.tabControlViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlViews.Location = new System.Drawing.Point(251, 27);
            this.tabControlViews.Name = "tabControlViews";
            this.tabControlViews.SelectedIndex = 0;
            this.tabControlViews.Size = new System.Drawing.Size(582, 351);
            this.tabControlViews.TabIndex = 0;
            this.tabControlViews.TabStop = false;
            this.tabControlViews.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlViews_Selected);
            // 
            // contextMenuStripScreenshotPreview
            // 
            this.contextMenuStripScreenshotPreview.Name = "contextMenuStripScreenshotPreview";
            this.contextMenuStripScreenshotPreview.Size = new System.Drawing.Size(61, 4);
            // 
            // listBoxScreenshots
            // 
            this.listBoxScreenshots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxScreenshots.FormattingEnabled = true;
            this.listBoxScreenshots.HorizontalScrollbar = true;
            this.listBoxScreenshots.IntegralHeight = false;
            this.listBoxScreenshots.Location = new System.Drawing.Point(3, 27);
            this.listBoxScreenshots.Name = "listBoxScreenshots";
            this.listBoxScreenshots.ScrollAlwaysVisible = true;
            this.listBoxScreenshots.Size = new System.Drawing.Size(235, 154);
            this.listBoxScreenshots.TabIndex = 0;
            this.listBoxScreenshots.TabStop = false;
            this.listBoxScreenshots.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged_listBoxScreenshots);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripSystemTrayIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // contextMenuStripSystemTrayIcon
            // 
            this.contextMenuStripSystemTrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAbout,
            this.toolStripSeparatorAbout,
            this.toolStripMenuItemShowInterface,
            this.toolStripMenuItemHideInterface,
            this.toolStripSeparatorInterface,
            this.toolStripMenuItemShowScreenCaptureStatus,
            this.toolStripSeparatorTools,
            this.toolStripMenuItemStartScreenCapture,
            this.toolStripMenuItemStopScreenCapture,
            this.toolStripSeparatorScreenCapture,
            this.toolStripMenuItemCaptureNowArchive,
            this.toolStripMenuItemCaptureNowEdit,
            this.toolStripSeparatorCaptureNow,
            this.toolStripMenuItemRegionSelectClipboard,
            this.toolStripMenuItemRegionSelectAutoSave,
            this.toolStripMenuItemRegionSelectEdit,
            this.toolStripSeparatorRegionSelect,
            this.toolStripMenuItemApplyLabel,
            this.toolStripSeparatorApplyLabel,
            this.toolStripMenuItemExit});
            this.contextMenuStripSystemTrayIcon.Name = "contextMenuStrip";
            this.contextMenuStripSystemTrayIcon.Size = new System.Drawing.Size(222, 332);
            this.contextMenuStripSystemTrayIcon.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripSystemTrayIcon_Opening);
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Image = global::AutoScreenCapture.Properties.Resources.about;
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemAbout.Text = "About Auto Screen Capture";
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // toolStripSeparatorAbout
            // 
            this.toolStripSeparatorAbout.Name = "toolStripSeparatorAbout";
            this.toolStripSeparatorAbout.Size = new System.Drawing.Size(218, 6);
            // 
            // toolStripMenuItemShowInterface
            // 
            this.toolStripMenuItemShowInterface.Name = "toolStripMenuItemShowInterface";
            this.toolStripMenuItemShowInterface.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemShowInterface.Text = "Show Interface";
            this.toolStripMenuItemShowInterface.Click += new System.EventHandler(this.toolStripMenuItemShowInterface_Click);
            // 
            // toolStripMenuItemHideInterface
            // 
            this.toolStripMenuItemHideInterface.Name = "toolStripMenuItemHideInterface";
            this.toolStripMenuItemHideInterface.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemHideInterface.Text = "Hide Interface";
            this.toolStripMenuItemHideInterface.Click += new System.EventHandler(this.toolStripMenuItemHideInterface_Click);
            // 
            // toolStripSeparatorInterface
            // 
            this.toolStripSeparatorInterface.Name = "toolStripSeparatorInterface";
            this.toolStripSeparatorInterface.Size = new System.Drawing.Size(218, 6);
            // 
            // toolStripMenuItemShowScreenCaptureStatus
            // 
            this.toolStripMenuItemShowScreenCaptureStatus.Name = "toolStripMenuItemShowScreenCaptureStatus";
            this.toolStripMenuItemShowScreenCaptureStatus.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemShowScreenCaptureStatus.Text = "Show Screen Capture Status";
            this.toolStripMenuItemShowScreenCaptureStatus.Click += new System.EventHandler(this.toolStripMenuItemScreenCaptureStatus_Click);
            // 
            // toolStripSeparatorTools
            // 
            this.toolStripSeparatorTools.Name = "toolStripSeparatorTools";
            this.toolStripSeparatorTools.Size = new System.Drawing.Size(218, 6);
            // 
            // toolStripMenuItemStartScreenCapture
            // 
            this.toolStripMenuItemStartScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.start_screen_capture;
            this.toolStripMenuItemStartScreenCapture.Name = "toolStripMenuItemStartScreenCapture";
            this.toolStripMenuItemStartScreenCapture.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemStartScreenCapture.Text = "Start Screen Capture";
            this.toolStripMenuItemStartScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemStartScreenCapture_Click);
            // 
            // toolStripMenuItemStopScreenCapture
            // 
            this.toolStripMenuItemStopScreenCapture.Enabled = false;
            this.toolStripMenuItemStopScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.stop_screen_capture;
            this.toolStripMenuItemStopScreenCapture.Name = "toolStripMenuItemStopScreenCapture";
            this.toolStripMenuItemStopScreenCapture.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemStopScreenCapture.Text = "Stop Screen Capture";
            this.toolStripMenuItemStopScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemStopScreenCapture_Click);
            // 
            // toolStripSeparatorScreenCapture
            // 
            this.toolStripSeparatorScreenCapture.Name = "toolStripSeparatorScreenCapture";
            this.toolStripSeparatorScreenCapture.Size = new System.Drawing.Size(218, 6);
            // 
            // toolStripMenuItemCaptureNowArchive
            // 
            this.toolStripMenuItemCaptureNowArchive.Image = global::AutoScreenCapture.Properties.Resources.capture_archive;
            this.toolStripMenuItemCaptureNowArchive.Name = "toolStripMenuItemCaptureNowArchive";
            this.toolStripMenuItemCaptureNowArchive.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemCaptureNowArchive.Text = "Capture Now / Archive";
            this.toolStripMenuItemCaptureNowArchive.Click += new System.EventHandler(this.toolStripMenuItemCaptureNowArchive_Click);
            // 
            // toolStripMenuItemCaptureNowEdit
            // 
            this.toolStripMenuItemCaptureNowEdit.Image = global::AutoScreenCapture.Properties.Resources.capture_edit;
            this.toolStripMenuItemCaptureNowEdit.Name = "toolStripMenuItemCaptureNowEdit";
            this.toolStripMenuItemCaptureNowEdit.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemCaptureNowEdit.Text = "Capture Now / Edit";
            this.toolStripMenuItemCaptureNowEdit.Click += new System.EventHandler(this.toolStripMenuItemCaptureNowEdit_Click);
            // 
            // toolStripSeparatorCaptureNow
            // 
            this.toolStripSeparatorCaptureNow.Name = "toolStripSeparatorCaptureNow";
            this.toolStripSeparatorCaptureNow.Size = new System.Drawing.Size(218, 6);
            // 
            // toolStripMenuItemRegionSelectClipboard
            // 
            this.toolStripMenuItemRegionSelectClipboard.Image = global::AutoScreenCapture.Properties.Resources.region_select;
            this.toolStripMenuItemRegionSelectClipboard.Name = "toolStripMenuItemRegionSelectClipboard";
            this.toolStripMenuItemRegionSelectClipboard.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemRegionSelectClipboard.Text = "Region Select / Clipboard";
            this.toolStripMenuItemRegionSelectClipboard.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectClipboard_Click);
            // 
            // toolStripMenuItemRegionSelectAutoSave
            // 
            this.toolStripMenuItemRegionSelectAutoSave.Image = global::AutoScreenCapture.Properties.Resources.region_select;
            this.toolStripMenuItemRegionSelectAutoSave.Name = "toolStripMenuItemRegionSelectAutoSave";
            this.toolStripMenuItemRegionSelectAutoSave.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemRegionSelectAutoSave.Text = "Region Select / Auto Save";
            this.toolStripMenuItemRegionSelectAutoSave.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectAutoSave_Click);
            // 
            // toolStripMenuItemRegionSelectEdit
            // 
            this.toolStripMenuItemRegionSelectEdit.Image = global::AutoScreenCapture.Properties.Resources.region_select;
            this.toolStripMenuItemRegionSelectEdit.Name = "toolStripMenuItemRegionSelectEdit";
            this.toolStripMenuItemRegionSelectEdit.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemRegionSelectEdit.Text = "Region Select / Edit";
            this.toolStripMenuItemRegionSelectEdit.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectEdit_Click);
            // 
            // toolStripSeparatorRegionSelect
            // 
            this.toolStripSeparatorRegionSelect.Name = "toolStripSeparatorRegionSelect";
            this.toolStripSeparatorRegionSelect.Size = new System.Drawing.Size(218, 6);
            // 
            // toolStripMenuItemApplyLabel
            // 
            this.toolStripMenuItemApplyLabel.Name = "toolStripMenuItemApplyLabel";
            this.toolStripMenuItemApplyLabel.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemApplyLabel.Text = "Apply Label";
            // 
            // toolStripSeparatorApplyLabel
            // 
            this.toolStripSeparatorApplyLabel.Name = "toolStripSeparatorApplyLabel";
            this.toolStripSeparatorApplyLabel.Size = new System.Drawing.Size(218, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.ShowShortcutKeys = false;
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItemExit.Text = "Exit";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // tabControlModules
            // 
            this.tabControlModules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlModules.Controls.Add(this.tabPageSetup);
            this.tabControlModules.Controls.Add(this.tabPageScreenshots);
            this.tabControlModules.Controls.Add(this.tabPageScreens);
            this.tabControlModules.Controls.Add(this.tabPageRegions);
            this.tabControlModules.Controls.Add(this.tabPageEditors);
            this.tabControlModules.Controls.Add(this.tabPageSchedules);
            this.tabControlModules.Controls.Add(this.tabPageTags);
            this.tabControlModules.Controls.Add(this.tabPageTriggers);
            this.tabControlModules.Location = new System.Drawing.Point(0, 220);
            this.tabControlModules.Name = "tabControlModules";
            this.tabControlModules.SelectedIndex = 0;
            this.tabControlModules.Size = new System.Drawing.Size(249, 210);
            this.tabControlModules.TabIndex = 0;
            this.tabControlModules.TabStop = false;
            // 
            // tabPageSetup
            // 
            this.tabPageSetup.AutoScroll = true;
            this.tabPageSetup.Controls.Add(this.groupBoxApplicationFocus);
            this.tabPageSetup.Controls.Add(this.groupBoxRegionSelectAutoSave);
            this.tabPageSetup.Controls.Add(this.groupBoxActiveWindowTitle);
            this.tabPageSetup.Controls.Add(this.groupBoxSecurity);
            this.tabPageSetup.Controls.Add(this.checkBoxScreenshotLabel);
            this.tabPageSetup.Controls.Add(this.comboBoxScreenshotLabel);
            this.tabPageSetup.Controls.Add(this.groupBoxCaptureDelay);
            this.tabPageSetup.Location = new System.Drawing.Point(4, 22);
            this.tabPageSetup.Name = "tabPageSetup";
            this.tabPageSetup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSetup.Size = new System.Drawing.Size(241, 184);
            this.tabPageSetup.TabIndex = 0;
            this.tabPageSetup.Text = "Setup";
            this.tabPageSetup.UseVisualStyleBackColor = true;
            // 
            // groupBoxApplicationFocus
            // 
            this.groupBoxApplicationFocus.Controls.Add(this.buttonApplicationFocusTest);
            this.groupBoxApplicationFocus.Controls.Add(this.buttonApplicationFocusRefresh);
            this.groupBoxApplicationFocus.Controls.Add(this.comboBoxProcessList);
            this.groupBoxApplicationFocus.Location = new System.Drawing.Point(6, 256);
            this.groupBoxApplicationFocus.Name = "groupBoxApplicationFocus";
            this.groupBoxApplicationFocus.Size = new System.Drawing.Size(205, 76);
            this.groupBoxApplicationFocus.TabIndex = 0;
            this.groupBoxApplicationFocus.TabStop = false;
            this.groupBoxApplicationFocus.Text = "Application Focus";
            // 
            // buttonApplicationFocusTest
            // 
            this.buttonApplicationFocusTest.Location = new System.Drawing.Point(5, 46);
            this.buttonApplicationFocusTest.Name = "buttonApplicationFocusTest";
            this.buttonApplicationFocusTest.Size = new System.Drawing.Size(92, 23);
            this.buttonApplicationFocusTest.TabIndex = 0;
            this.buttonApplicationFocusTest.TabStop = false;
            this.buttonApplicationFocusTest.Text = "Test";
            this.buttonApplicationFocusTest.UseVisualStyleBackColor = true;
            this.buttonApplicationFocusTest.Click += new System.EventHandler(this.buttonApplicationFocusTest_Click);
            // 
            // buttonApplicationFocusRefresh
            // 
            this.buttonApplicationFocusRefresh.Location = new System.Drawing.Point(106, 46);
            this.buttonApplicationFocusRefresh.Name = "buttonApplicationFocusRefresh";
            this.buttonApplicationFocusRefresh.Size = new System.Drawing.Size(92, 23);
            this.buttonApplicationFocusRefresh.TabIndex = 0;
            this.buttonApplicationFocusRefresh.TabStop = false;
            this.buttonApplicationFocusRefresh.Text = "Refresh";
            this.buttonApplicationFocusRefresh.UseVisualStyleBackColor = true;
            this.buttonApplicationFocusRefresh.Click += new System.EventHandler(this.buttonApplicationFocusRefresh_Click);
            // 
            // comboBoxProcessList
            // 
            this.comboBoxProcessList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcessList.FormattingEnabled = true;
            this.comboBoxProcessList.Location = new System.Drawing.Point(5, 19);
            this.comboBoxProcessList.Name = "comboBoxProcessList";
            this.comboBoxProcessList.Size = new System.Drawing.Size(193, 21);
            this.comboBoxProcessList.TabIndex = 0;
            this.comboBoxProcessList.TabStop = false;
            // 
            // groupBoxRegionSelectAutoSave
            // 
            this.groupBoxRegionSelectAutoSave.Controls.Add(this.buttonBrowseFolder);
            this.groupBoxRegionSelectAutoSave.Controls.Add(this.labelAutoSaveMacro);
            this.groupBoxRegionSelectAutoSave.Controls.Add(this.labelAutoSaveFolder);
            this.groupBoxRegionSelectAutoSave.Controls.Add(this.textBoxAutoSaveMacro);
            this.groupBoxRegionSelectAutoSave.Controls.Add(this.textBoxAutoSaveFolder);
            this.groupBoxRegionSelectAutoSave.Location = new System.Drawing.Point(6, 338);
            this.groupBoxRegionSelectAutoSave.Name = "groupBoxRegionSelectAutoSave";
            this.groupBoxRegionSelectAutoSave.Size = new System.Drawing.Size(205, 78);
            this.groupBoxRegionSelectAutoSave.TabIndex = 0;
            this.groupBoxRegionSelectAutoSave.TabStop = false;
            this.groupBoxRegionSelectAutoSave.Text = "Region Select / Auto Save";
            // 
            // buttonBrowseFolder
            // 
            this.buttonBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseFolder.Image = global::AutoScreenCapture.Properties.Resources.openfolder;
            this.buttonBrowseFolder.Location = new System.Drawing.Point(172, 21);
            this.buttonBrowseFolder.Name = "buttonBrowseFolder";
            this.buttonBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonBrowseFolder.TabIndex = 0;
            this.buttonBrowseFolder.TabStop = false;
            this.buttonBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // labelAutoSaveMacro
            // 
            this.labelAutoSaveMacro.AutoSize = true;
            this.labelAutoSaveMacro.Location = new System.Drawing.Point(3, 52);
            this.labelAutoSaveMacro.Name = "labelAutoSaveMacro";
            this.labelAutoSaveMacro.Size = new System.Drawing.Size(40, 13);
            this.labelAutoSaveMacro.TabIndex = 0;
            this.labelAutoSaveMacro.Text = "Macro:";
            // 
            // labelAutoSaveFolder
            // 
            this.labelAutoSaveFolder.AutoSize = true;
            this.labelAutoSaveFolder.Location = new System.Drawing.Point(3, 26);
            this.labelAutoSaveFolder.Name = "labelAutoSaveFolder";
            this.labelAutoSaveFolder.Size = new System.Drawing.Size(39, 13);
            this.labelAutoSaveFolder.TabIndex = 0;
            this.labelAutoSaveFolder.Text = "Folder:";
            // 
            // textBoxAutoSaveMacro
            // 
            this.textBoxAutoSaveMacro.Location = new System.Drawing.Point(49, 49);
            this.textBoxAutoSaveMacro.Name = "textBoxAutoSaveMacro";
            this.textBoxAutoSaveMacro.Size = new System.Drawing.Size(150, 20);
            this.textBoxAutoSaveMacro.TabIndex = 0;
            this.textBoxAutoSaveMacro.TabStop = false;
            // 
            // textBoxAutoSaveFolder
            // 
            this.textBoxAutoSaveFolder.Location = new System.Drawing.Point(49, 23);
            this.textBoxAutoSaveFolder.Name = "textBoxAutoSaveFolder";
            this.textBoxAutoSaveFolder.Size = new System.Drawing.Size(117, 20);
            this.textBoxAutoSaveFolder.TabIndex = 0;
            this.textBoxAutoSaveFolder.TabStop = false;
            // 
            // groupBoxActiveWindowTitle
            // 
            this.groupBoxActiveWindowTitle.Controls.Add(this.textBoxActiveWindowTitle);
            this.groupBoxActiveWindowTitle.Controls.Add(this.checkBoxActiveWindowTitle);
            this.groupBoxActiveWindowTitle.Location = new System.Drawing.Point(6, 178);
            this.groupBoxActiveWindowTitle.Name = "groupBoxActiveWindowTitle";
            this.groupBoxActiveWindowTitle.Size = new System.Drawing.Size(205, 72);
            this.groupBoxActiveWindowTitle.TabIndex = 0;
            this.groupBoxActiveWindowTitle.TabStop = false;
            this.groupBoxActiveWindowTitle.Text = "Active Window Title";
            // 
            // textBoxActiveWindowTitle
            // 
            this.textBoxActiveWindowTitle.Enabled = false;
            this.textBoxActiveWindowTitle.Location = new System.Drawing.Point(6, 42);
            this.textBoxActiveWindowTitle.MaxLength = 500;
            this.textBoxActiveWindowTitle.Name = "textBoxActiveWindowTitle";
            this.textBoxActiveWindowTitle.Size = new System.Drawing.Size(193, 20);
            this.textBoxActiveWindowTitle.TabIndex = 0;
            this.textBoxActiveWindowTitle.TabStop = false;
            // 
            // checkBoxActiveWindowTitle
            // 
            this.checkBoxActiveWindowTitle.AutoSize = true;
            this.checkBoxActiveWindowTitle.Location = new System.Drawing.Point(6, 19);
            this.checkBoxActiveWindowTitle.Name = "checkBoxActiveWindowTitle";
            this.checkBoxActiveWindowTitle.Size = new System.Drawing.Size(185, 17);
            this.checkBoxActiveWindowTitle.TabIndex = 0;
            this.checkBoxActiveWindowTitle.TabStop = false;
            this.checkBoxActiveWindowTitle.Text = "Capture only if the title contains ...";
            this.checkBoxActiveWindowTitle.UseVisualStyleBackColor = true;
            // 
            // groupBoxSecurity
            // 
            this.groupBoxSecurity.Controls.Add(this.labelPasswordDescription);
            this.groupBoxSecurity.Controls.Add(this.buttonSetPassphrase);
            this.groupBoxSecurity.Controls.Add(this.textBoxPassphrase);
            this.groupBoxSecurity.Location = new System.Drawing.Point(6, 422);
            this.groupBoxSecurity.Name = "groupBoxSecurity";
            this.groupBoxSecurity.Size = new System.Drawing.Size(205, 110);
            this.groupBoxSecurity.TabIndex = 0;
            this.groupBoxSecurity.TabStop = false;
            this.groupBoxSecurity.Text = "Security";
            // 
            // labelPasswordDescription
            // 
            this.labelPasswordDescription.Location = new System.Drawing.Point(7, 21);
            this.labelPasswordDescription.Name = "labelPasswordDescription";
            this.labelPasswordDescription.Size = new System.Drawing.Size(186, 55);
            this.labelPasswordDescription.TabIndex = 0;
            this.labelPasswordDescription.Text = "This passphrase will be required whenever screen capturing is stopped, this inter" +
    "face is shown, or the application is exiting:";
            // 
            // buttonSetPassphrase
            // 
            this.buttonSetPassphrase.Enabled = false;
            this.buttonSetPassphrase.Location = new System.Drawing.Point(147, 80);
            this.buttonSetPassphrase.Name = "buttonSetPassphrase";
            this.buttonSetPassphrase.Size = new System.Drawing.Size(52, 23);
            this.buttonSetPassphrase.TabIndex = 0;
            this.buttonSetPassphrase.TabStop = false;
            this.buttonSetPassphrase.Text = "Lock";
            this.buttonSetPassphrase.UseVisualStyleBackColor = true;
            this.buttonSetPassphrase.Click += new System.EventHandler(this.buttonSetPassphrase_Click);
            // 
            // textBoxPassphrase
            // 
            this.textBoxPassphrase.Location = new System.Drawing.Point(6, 82);
            this.textBoxPassphrase.MaxLength = 30;
            this.textBoxPassphrase.Name = "textBoxPassphrase";
            this.textBoxPassphrase.Size = new System.Drawing.Size(135, 20);
            this.textBoxPassphrase.TabIndex = 0;
            this.textBoxPassphrase.TabStop = false;
            this.textBoxPassphrase.TextChanged += new System.EventHandler(this.TextChanged_textBoxPassphrase);
            this.textBoxPassphrase.MouseHover += new System.EventHandler(this.textBoxPassphrase_MouseHover);
            // 
            // checkBoxScreenshotLabel
            // 
            this.checkBoxScreenshotLabel.AutoSize = true;
            this.checkBoxScreenshotLabel.Location = new System.Drawing.Point(6, 133);
            this.checkBoxScreenshotLabel.Name = "checkBoxScreenshotLabel";
            this.checkBoxScreenshotLabel.Size = new System.Drawing.Size(193, 17);
            this.checkBoxScreenshotLabel.TabIndex = 0;
            this.checkBoxScreenshotLabel.TabStop = false;
            this.checkBoxScreenshotLabel.Text = "Apply this label to each screenshot:";
            this.checkBoxScreenshotLabel.UseVisualStyleBackColor = true;
            this.checkBoxScreenshotLabel.MouseHover += new System.EventHandler(this.checkBoxScreenshotLabel_MouseHover);
            // 
            // comboBoxScreenshotLabel
            // 
            this.comboBoxScreenshotLabel.FormattingEnabled = true;
            this.comboBoxScreenshotLabel.Location = new System.Drawing.Point(6, 151);
            this.comboBoxScreenshotLabel.MaxDropDownItems = 10;
            this.comboBoxScreenshotLabel.MaxLength = 500;
            this.comboBoxScreenshotLabel.Name = "comboBoxScreenshotLabel";
            this.comboBoxScreenshotLabel.Size = new System.Drawing.Size(205, 21);
            this.comboBoxScreenshotLabel.TabIndex = 0;
            this.comboBoxScreenshotLabel.TabStop = false;
            this.comboBoxScreenshotLabel.SelectedIndexChanged += new System.EventHandler(this.ComboBoxScreenshotLabel_SelectedIndexChanged);
            // 
            // groupBoxCaptureDelay
            // 
            this.groupBoxCaptureDelay.Controls.Add(this.labelLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.checkBoxInitialScreenshot);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownCaptureLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.checkBoxCaptureLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.labelMillisecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownMillisecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelSecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelMinutesInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelHoursInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownSecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownMinutesInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownHoursInterval);
            this.groupBoxCaptureDelay.Location = new System.Drawing.Point(6, 5);
            this.groupBoxCaptureDelay.Name = "groupBoxCaptureDelay";
            this.groupBoxCaptureDelay.Size = new System.Drawing.Size(205, 122);
            this.groupBoxCaptureDelay.TabIndex = 0;
            this.groupBoxCaptureDelay.TabStop = false;
            this.groupBoxCaptureDelay.Text = "Interval";
            // 
            // labelLimit
            // 
            this.labelLimit.AutoSize = true;
            this.labelLimit.Location = new System.Drawing.Point(171, 47);
            this.labelLimit.Name = "labelLimit";
            this.labelLimit.Size = new System.Drawing.Size(24, 13);
            this.labelLimit.TabIndex = 26;
            this.labelLimit.Text = "limit";
            // 
            // checkBoxInitialScreenshot
            // 
            this.checkBoxInitialScreenshot.AutoSize = true;
            this.checkBoxInitialScreenshot.Location = new System.Drawing.Point(110, 20);
            this.checkBoxInitialScreenshot.Name = "checkBoxInitialScreenshot";
            this.checkBoxInitialScreenshot.Size = new System.Drawing.Size(90, 17);
            this.checkBoxInitialScreenshot.TabIndex = 0;
            this.checkBoxInitialScreenshot.TabStop = false;
            this.checkBoxInitialScreenshot.Text = "Initial Capture";
            this.checkBoxInitialScreenshot.UseVisualStyleBackColor = true;
            this.checkBoxInitialScreenshot.Click += new System.EventHandler(this.SaveSettings);
            this.checkBoxInitialScreenshot.Leave += new System.EventHandler(this.SaveSettings);
            this.checkBoxInitialScreenshot.MouseHover += new System.EventHandler(this.checkBoxInitialScreenshot_MouseHover);
            // 
            // numericUpDownCaptureLimit
            // 
            this.numericUpDownCaptureLimit.Location = new System.Drawing.Point(127, 45);
            this.numericUpDownCaptureLimit.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownCaptureLimit.Name = "numericUpDownCaptureLimit";
            this.numericUpDownCaptureLimit.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownCaptureLimit.TabIndex = 0;
            this.numericUpDownCaptureLimit.TabStop = false;
            this.numericUpDownCaptureLimit.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxCaptureLimit
            // 
            this.checkBoxCaptureLimit.AutoSize = true;
            this.checkBoxCaptureLimit.Location = new System.Drawing.Point(110, 47);
            this.checkBoxCaptureLimit.Name = "checkBoxCaptureLimit";
            this.checkBoxCaptureLimit.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCaptureLimit.TabIndex = 0;
            this.checkBoxCaptureLimit.TabStop = false;
            this.checkBoxCaptureLimit.UseVisualStyleBackColor = true;
            this.checkBoxCaptureLimit.CheckedChanged += new System.EventHandler(this.CheckedChanged_checkBoxCaptureLimit);
            this.checkBoxCaptureLimit.Click += new System.EventHandler(this.SaveSettings);
            this.checkBoxCaptureLimit.Leave += new System.EventHandler(this.SaveSettings);
            this.checkBoxCaptureLimit.MouseHover += new System.EventHandler(this.checkBoxCaptureLimit_MouseHover);
            // 
            // labelMillisecondsInterval
            // 
            this.labelMillisecondsInterval.AutoSize = true;
            this.labelMillisecondsInterval.Location = new System.Drawing.Point(54, 99);
            this.labelMillisecondsInterval.Name = "labelMillisecondsInterval";
            this.labelMillisecondsInterval.Size = new System.Drawing.Size(63, 13);
            this.labelMillisecondsInterval.TabIndex = 0;
            this.labelMillisecondsInterval.Text = "milliseconds";
            // 
            // numericUpDownMillisecondsInterval
            // 
            this.numericUpDownMillisecondsInterval.Location = new System.Drawing.Point(6, 97);
            this.numericUpDownMillisecondsInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownMillisecondsInterval.Name = "numericUpDownMillisecondsInterval";
            this.numericUpDownMillisecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMillisecondsInterval.TabIndex = 0;
            this.numericUpDownMillisecondsInterval.TabStop = false;
            this.numericUpDownMillisecondsInterval.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // labelSecondsInterval
            // 
            this.labelSecondsInterval.AutoSize = true;
            this.labelSecondsInterval.Location = new System.Drawing.Point(54, 73);
            this.labelSecondsInterval.Name = "labelSecondsInterval";
            this.labelSecondsInterval.Size = new System.Drawing.Size(47, 13);
            this.labelSecondsInterval.TabIndex = 0;
            this.labelSecondsInterval.Text = "seconds";
            // 
            // labelMinutesInterval
            // 
            this.labelMinutesInterval.AutoSize = true;
            this.labelMinutesInterval.Location = new System.Drawing.Point(54, 47);
            this.labelMinutesInterval.Name = "labelMinutesInterval";
            this.labelMinutesInterval.Size = new System.Drawing.Size(43, 13);
            this.labelMinutesInterval.TabIndex = 0;
            this.labelMinutesInterval.Text = "minutes";
            // 
            // labelHoursInterval
            // 
            this.labelHoursInterval.AutoSize = true;
            this.labelHoursInterval.Location = new System.Drawing.Point(54, 21);
            this.labelHoursInterval.Name = "labelHoursInterval";
            this.labelHoursInterval.Size = new System.Drawing.Size(33, 13);
            this.labelHoursInterval.TabIndex = 0;
            this.labelHoursInterval.Text = "hours";
            // 
            // numericUpDownSecondsInterval
            // 
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(6, 71);
            this.numericUpDownSecondsInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSecondsInterval.Name = "numericUpDownSecondsInterval";
            this.numericUpDownSecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSecondsInterval.TabIndex = 0;
            this.numericUpDownSecondsInterval.TabStop = false;
            this.numericUpDownSecondsInterval.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // numericUpDownMinutesInterval
            // 
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(6, 45);
            this.numericUpDownMinutesInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownMinutesInterval.Name = "numericUpDownMinutesInterval";
            this.numericUpDownMinutesInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMinutesInterval.TabIndex = 0;
            this.numericUpDownMinutesInterval.TabStop = false;
            this.numericUpDownMinutesInterval.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // numericUpDownHoursInterval
            // 
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(6, 19);
            this.numericUpDownHoursInterval.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownHoursInterval.Name = "numericUpDownHoursInterval";
            this.numericUpDownHoursInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownHoursInterval.TabIndex = 0;
            this.numericUpDownHoursInterval.TabStop = false;
            this.numericUpDownHoursInterval.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // tabPageScreenshots
            // 
            this.tabPageScreenshots.AutoScroll = true;
            this.tabPageScreenshots.Controls.Add(this.labelDays);
            this.tabPageScreenshots.Controls.Add(this.numericUpDownKeepScreenshotsForDays);
            this.tabPageScreenshots.Controls.Add(this.labelKeepScreenshots);
            this.tabPageScreenshots.Controls.Add(this.listBoxScreenshots);
            this.tabPageScreenshots.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreenshots.Name = "tabPageScreenshots";
            this.tabPageScreenshots.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScreenshots.Size = new System.Drawing.Size(241, 184);
            this.tabPageScreenshots.TabIndex = 1;
            this.tabPageScreenshots.Text = "Screenshots";
            this.tabPageScreenshots.UseVisualStyleBackColor = true;
            // 
            // labelDays
            // 
            this.labelDays.AutoSize = true;
            this.labelDays.Location = new System.Drawing.Point(154, 6);
            this.labelDays.Name = "labelDays";
            this.labelDays.Size = new System.Drawing.Size(29, 13);
            this.labelDays.TabIndex = 0;
            this.labelDays.Text = "days";
            this.labelDays.MouseHover += new System.EventHandler(this.labelKeepScreenshots_MouseHover);
            // 
            // numericUpDownKeepScreenshotsForDays
            // 
            this.numericUpDownKeepScreenshotsForDays.Location = new System.Drawing.Point(110, 4);
            this.numericUpDownKeepScreenshotsForDays.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numericUpDownKeepScreenshotsForDays.Name = "numericUpDownKeepScreenshotsForDays";
            this.numericUpDownKeepScreenshotsForDays.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownKeepScreenshotsForDays.TabIndex = 0;
            this.numericUpDownKeepScreenshotsForDays.TabStop = false;
            this.numericUpDownKeepScreenshotsForDays.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // labelKeepScreenshots
            // 
            this.labelKeepScreenshots.AutoSize = true;
            this.labelKeepScreenshots.Location = new System.Drawing.Point(3, 6);
            this.labelKeepScreenshots.Name = "labelKeepScreenshots";
            this.labelKeepScreenshots.Size = new System.Drawing.Size(107, 13);
            this.labelKeepScreenshots.TabIndex = 0;
            this.labelKeepScreenshots.Text = "Keep screenshots for";
            this.labelKeepScreenshots.MouseHover += new System.EventHandler(this.labelKeepScreenshots_MouseHover);
            // 
            // tabPageScreens
            // 
            this.tabPageScreens.AutoScroll = true;
            this.tabPageScreens.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreens.Name = "tabPageScreens";
            this.tabPageScreens.Size = new System.Drawing.Size(241, 184);
            this.tabPageScreens.TabIndex = 5;
            this.tabPageScreens.Text = "Screens";
            this.tabPageScreens.UseVisualStyleBackColor = true;
            // 
            // tabPageRegions
            // 
            this.tabPageRegions.AutoScroll = true;
            this.tabPageRegions.Location = new System.Drawing.Point(4, 22);
            this.tabPageRegions.Name = "tabPageRegions";
            this.tabPageRegions.Size = new System.Drawing.Size(241, 184);
            this.tabPageRegions.TabIndex = 4;
            this.tabPageRegions.Text = "Regions";
            this.tabPageRegions.UseVisualStyleBackColor = true;
            // 
            // tabPageEditors
            // 
            this.tabPageEditors.AutoScroll = true;
            this.tabPageEditors.Location = new System.Drawing.Point(4, 22);
            this.tabPageEditors.Name = "tabPageEditors";
            this.tabPageEditors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEditors.Size = new System.Drawing.Size(241, 184);
            this.tabPageEditors.TabIndex = 2;
            this.tabPageEditors.Text = "Editors";
            this.tabPageEditors.UseVisualStyleBackColor = true;
            // 
            // tabPageSchedules
            // 
            this.tabPageSchedules.Location = new System.Drawing.Point(4, 22);
            this.tabPageSchedules.Name = "tabPageSchedules";
            this.tabPageSchedules.Size = new System.Drawing.Size(241, 184);
            this.tabPageSchedules.TabIndex = 8;
            this.tabPageSchedules.Text = "Schedules";
            this.tabPageSchedules.UseVisualStyleBackColor = true;
            // 
            // tabPageTags
            // 
            this.tabPageTags.AutoScroll = true;
            this.tabPageTags.Location = new System.Drawing.Point(4, 22);
            this.tabPageTags.Name = "tabPageTags";
            this.tabPageTags.Size = new System.Drawing.Size(241, 184);
            this.tabPageTags.TabIndex = 7;
            this.tabPageTags.Text = "Tags";
            this.tabPageTags.UseVisualStyleBackColor = true;
            // 
            // tabPageTriggers
            // 
            this.tabPageTriggers.AutoScroll = true;
            this.tabPageTriggers.Location = new System.Drawing.Point(4, 22);
            this.tabPageTriggers.Name = "tabPageTriggers";
            this.tabPageTriggers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTriggers.Size = new System.Drawing.Size(241, 184);
            this.tabPageTriggers.TabIndex = 3;
            this.tabPageTriggers.Text = "Triggers";
            this.tabPageTriggers.UseVisualStyleBackColor = true;
            // 
            // timerScheduledCapture
            // 
            this.timerScheduledCapture.Enabled = true;
            this.timerScheduledCapture.Interval = 1000;
            this.timerScheduledCapture.Tick += new System.EventHandler(this.timerScheduledCapture_Tick);
            // 
            // timerScreenCapture
            // 
            this.timerScreenCapture.Tick += new System.EventHandler(this.timerScreenCapture_Tick);
            // 
            // labelScreenshotTitle
            // 
            this.labelScreenshotTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScreenshotTitle.AutoSize = true;
            this.labelScreenshotTitle.Location = new System.Drawing.Point(460, 413);
            this.labelScreenshotTitle.Name = "labelScreenshotTitle";
            this.labelScreenshotTitle.Size = new System.Drawing.Size(72, 13);
            this.labelScreenshotTitle.TabIndex = 0;
            this.labelScreenshotTitle.Text = "Window Title:";
            // 
            // textBoxScreenshotTitle
            // 
            this.textBoxScreenshotTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScreenshotTitle.Location = new System.Drawing.Point(538, 410);
            this.textBoxScreenshotTitle.Name = "textBoxScreenshotTitle";
            this.textBoxScreenshotTitle.ReadOnly = true;
            this.textBoxScreenshotTitle.Size = new System.Drawing.Size(283, 20);
            this.textBoxScreenshotTitle.TabIndex = 0;
            this.textBoxScreenshotTitle.TabStop = false;
            // 
            // labelScreenshotFormat
            // 
            this.labelScreenshotFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScreenshotFormat.AutoSize = true;
            this.labelScreenshotFormat.Location = new System.Drawing.Point(258, 387);
            this.labelScreenshotFormat.Name = "labelScreenshotFormat";
            this.labelScreenshotFormat.Size = new System.Drawing.Size(74, 13);
            this.labelScreenshotFormat.TabIndex = 0;
            this.labelScreenshotFormat.Text = "Image Format:";
            // 
            // textBoxScreenshotFormat
            // 
            this.textBoxScreenshotFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxScreenshotFormat.Location = new System.Drawing.Point(338, 384);
            this.textBoxScreenshotFormat.Name = "textBoxScreenshotFormat";
            this.textBoxScreenshotFormat.ReadOnly = true;
            this.textBoxScreenshotFormat.Size = new System.Drawing.Size(41, 20);
            this.textBoxScreenshotFormat.TabIndex = 0;
            this.textBoxScreenshotFormat.TabStop = false;
            // 
            // labelScreenshotWidth
            // 
            this.labelScreenshotWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelScreenshotWidth.AutoSize = true;
            this.labelScreenshotWidth.Location = new System.Drawing.Point(394, 387);
            this.labelScreenshotWidth.Name = "labelScreenshotWidth";
            this.labelScreenshotWidth.Size = new System.Drawing.Size(38, 13);
            this.labelScreenshotWidth.TabIndex = 0;
            this.labelScreenshotWidth.Text = "Width:";
            // 
            // textBoxScreenshotWidth
            // 
            this.textBoxScreenshotWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxScreenshotWidth.Location = new System.Drawing.Point(438, 384);
            this.textBoxScreenshotWidth.MaximumSize = new System.Drawing.Size(47, 20);
            this.textBoxScreenshotWidth.Name = "textBoxScreenshotWidth";
            this.textBoxScreenshotWidth.ReadOnly = true;
            this.textBoxScreenshotWidth.Size = new System.Drawing.Size(47, 20);
            this.textBoxScreenshotWidth.TabIndex = 0;
            this.textBoxScreenshotWidth.TabStop = false;
            // 
            // labelScreenshotHeight
            // 
            this.labelScreenshotHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelScreenshotHeight.AutoSize = true;
            this.labelScreenshotHeight.Location = new System.Drawing.Point(491, 387);
            this.labelScreenshotHeight.Name = "labelScreenshotHeight";
            this.labelScreenshotHeight.Size = new System.Drawing.Size(41, 13);
            this.labelScreenshotHeight.TabIndex = 0;
            this.labelScreenshotHeight.Text = "Height:";
            // 
            // textBoxScreenshotHeight
            // 
            this.textBoxScreenshotHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxScreenshotHeight.Location = new System.Drawing.Point(538, 384);
            this.textBoxScreenshotHeight.MaximumSize = new System.Drawing.Size(47, 20);
            this.textBoxScreenshotHeight.Name = "textBoxScreenshotHeight";
            this.textBoxScreenshotHeight.ReadOnly = true;
            this.textBoxScreenshotHeight.Size = new System.Drawing.Size(47, 20);
            this.textBoxScreenshotHeight.TabIndex = 0;
            this.textBoxScreenshotHeight.TabStop = false;
            // 
            // labelScreenshotDate
            // 
            this.labelScreenshotDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelScreenshotDate.AutoSize = true;
            this.labelScreenshotDate.Location = new System.Drawing.Point(591, 387);
            this.labelScreenshotDate.Name = "labelScreenshotDate";
            this.labelScreenshotDate.Size = new System.Drawing.Size(33, 13);
            this.labelScreenshotDate.TabIndex = 0;
            this.labelScreenshotDate.Text = "Date:";
            // 
            // textBoxScreenshotDate
            // 
            this.textBoxScreenshotDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxScreenshotDate.Location = new System.Drawing.Point(630, 384);
            this.textBoxScreenshotDate.Name = "textBoxScreenshotDate";
            this.textBoxScreenshotDate.ReadOnly = true;
            this.textBoxScreenshotDate.Size = new System.Drawing.Size(73, 20);
            this.textBoxScreenshotDate.TabIndex = 0;
            this.textBoxScreenshotDate.TabStop = false;
            // 
            // labelScreenshotTime
            // 
            this.labelScreenshotTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelScreenshotTime.AutoSize = true;
            this.labelScreenshotTime.Location = new System.Drawing.Point(709, 387);
            this.labelScreenshotTime.Name = "labelScreenshotTime";
            this.labelScreenshotTime.Size = new System.Drawing.Size(33, 13);
            this.labelScreenshotTime.TabIndex = 0;
            this.labelScreenshotTime.Text = "Time:";
            // 
            // textBoxScreenshotTime
            // 
            this.textBoxScreenshotTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxScreenshotTime.Location = new System.Drawing.Point(748, 384);
            this.textBoxScreenshotTime.Name = "textBoxScreenshotTime";
            this.textBoxScreenshotTime.ReadOnly = true;
            this.textBoxScreenshotTime.Size = new System.Drawing.Size(73, 20);
            this.textBoxScreenshotTime.TabIndex = 0;
            this.textBoxScreenshotTime.TabStop = false;
            // 
            // comboBoxFilterValue
            // 
            this.comboBoxFilterValue.DropDownHeight = 300;
            this.comboBoxFilterValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterValue.DropDownWidth = 700;
            this.comboBoxFilterValue.Enabled = false;
            this.comboBoxFilterValue.FormattingEnabled = true;
            this.comboBoxFilterValue.IntegralHeight = false;
            this.comboBoxFilterValue.Location = new System.Drawing.Point(133, 30);
            this.comboBoxFilterValue.Name = "comboBoxFilterValue";
            this.comboBoxFilterValue.Size = new System.Drawing.Size(88, 21);
            this.comboBoxFilterValue.TabIndex = 0;
            this.comboBoxFilterValue.TabStop = false;
            this.comboBoxFilterValue.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterValue_SelectedIndexChanged);
            this.comboBoxFilterValue.MouseHover += new System.EventHandler(this.comboBoxFilterValue_MouseHover);
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(1, 33);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(32, 13);
            this.labelFilter.TabIndex = 0;
            this.labelFilter.Text = "Filter:";
            // 
            // buttonRefreshFilterValues
            // 
            this.buttonRefreshFilterValues.BackColor = System.Drawing.Color.Transparent;
            this.buttonRefreshFilterValues.Enabled = false;
            this.buttonRefreshFilterValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefreshFilterValues.ForeColor = System.Drawing.Color.Transparent;
            this.buttonRefreshFilterValues.Image = global::AutoScreenCapture.Properties.Resources.refresh;
            this.buttonRefreshFilterValues.Location = new System.Drawing.Point(227, 30);
            this.buttonRefreshFilterValues.Name = "buttonRefreshFilterValues";
            this.buttonRefreshFilterValues.Size = new System.Drawing.Size(21, 21);
            this.buttonRefreshFilterValues.TabIndex = 0;
            this.buttonRefreshFilterValues.TabStop = false;
            this.buttonRefreshFilterValues.UseVisualStyleBackColor = false;
            this.buttonRefreshFilterValues.Click += new System.EventHandler(this.buttonRefreshFilterValues_Click);
            this.buttonRefreshFilterValues.MouseHover += new System.EventHandler(this.buttonRefreshFilterValues_MouseHover);
            // 
            // comboBoxFilterType
            // 
            this.comboBoxFilterType.DropDownHeight = 100;
            this.comboBoxFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterType.DropDownWidth = 100;
            this.comboBoxFilterType.FormattingEnabled = true;
            this.comboBoxFilterType.IntegralHeight = false;
            this.comboBoxFilterType.Items.AddRange(new object[] {
            "",
            "Image Format",
            "Label",
            "Process Name",
            "Window Title"});
            this.comboBoxFilterType.Location = new System.Drawing.Point(39, 30);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(88, 21);
            this.comboBoxFilterType.TabIndex = 0;
            this.comboBoxFilterType.TabStop = false;
            this.comboBoxFilterType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterType_SelectedIndexChanged);
            this.comboBoxFilterType.MouseHover += new System.EventHandler(this.comboBoxFilterType_MouseHover);
            // 
            // timerPerformMaintenance
            // 
            this.timerPerformMaintenance.Enabled = true;
            this.timerPerformMaintenance.Interval = 300000;
            this.timerPerformMaintenance.Tick += new System.EventHandler(this.timerPerformMaintenance_Tick);
            // 
            // textBoxLabel
            // 
            this.textBoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxLabel.Location = new System.Drawing.Point(300, 410);
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.ReadOnly = true;
            this.textBoxLabel.Size = new System.Drawing.Size(154, 20);
            this.textBoxLabel.TabIndex = 4;
            this.textBoxLabel.TabStop = false;
            // 
            // labelLabel
            // 
            this.labelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(258, 413);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(36, 13);
            this.labelLabel.TabIndex = 5;
            this.labelLabel.Text = "Label:";
            // 
            // labelHelp
            // 
            this.labelHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHelp.AutoEllipsis = true;
            this.labelHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelHelp.Image = global::AutoScreenCapture.Properties.Resources.about;
            this.labelHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelHelp.Location = new System.Drawing.Point(2, 4);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(829, 17);
            this.labelHelp.TabIndex = 0;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelHelp.Click += new System.EventHandler(this.labelHelp_Click);
            // 
            // timerShowNextHelpTip
            // 
            this.timerShowNextHelpTip.Enabled = true;
            this.timerShowNextHelpTip.Interval = 20000;
            this.timerShowNextHelpTip.Tick += new System.EventHandler(this.timerShowNextHelpTip_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 458);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.labelLabel);
            this.Controls.Add(this.textBoxLabel);
            this.Controls.Add(this.comboBoxFilterType);
            this.Controls.Add(this.buttonRefreshFilterValues);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.comboBoxFilterValue);
            this.Controls.Add(this.textBoxScreenshotTime);
            this.Controls.Add(this.labelScreenshotTime);
            this.Controls.Add(this.textBoxScreenshotDate);
            this.Controls.Add(this.labelScreenshotDate);
            this.Controls.Add(this.textBoxScreenshotHeight);
            this.Controls.Add(this.labelScreenshotHeight);
            this.Controls.Add(this.textBoxScreenshotWidth);
            this.Controls.Add(this.labelScreenshotWidth);
            this.Controls.Add(this.textBoxScreenshotFormat);
            this.Controls.Add(this.labelScreenshotFormat);
            this.Controls.Add(this.textBoxScreenshotTitle);
            this.Controls.Add(this.labelScreenshotTitle);
            this.Controls.Add(this.tabControlModules);
            this.Controls.Add(this.tabControlViews);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.monthCalendar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(849, 497);
            this.Name = "FormMain";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormViewer_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStripSystemTrayIcon.ResumeLayout(false);
            this.tabControlModules.ResumeLayout(false);
            this.tabPageSetup.ResumeLayout(false);
            this.tabPageSetup.PerformLayout();
            this.groupBoxApplicationFocus.ResumeLayout(false);
            this.groupBoxRegionSelectAutoSave.ResumeLayout(false);
            this.groupBoxRegionSelectAutoSave.PerformLayout();
            this.groupBoxActiveWindowTitle.ResumeLayout(false);
            this.groupBoxActiveWindowTitle.PerformLayout();
            this.groupBoxSecurity.ResumeLayout(false);
            this.groupBoxSecurity.PerformLayout();
            this.groupBoxCaptureDelay.ResumeLayout(false);
            this.groupBoxCaptureDelay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            this.tabPageScreenshots.ResumeLayout(false);
            this.tabPageScreenshots.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeepScreenshotsForDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TabControl tabControlViews;
        private System.Windows.Forms.ListBox listBoxScreenshots;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSystemTrayIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStartScreenCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStopScreenCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripScreenshotPreview;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowInterface;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorInterface;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHideInterface;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorCaptureNow;
        private System.Windows.Forms.TabControl tabControlModules;
        private System.Windows.Forms.TabPage tabPageSetup;
        private System.Windows.Forms.TabPage tabPageScreenshots;
        private System.Windows.Forms.GroupBox groupBoxCaptureDelay;
        private System.Windows.Forms.Label labelMillisecondsInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMillisecondsInterval;
        private System.Windows.Forms.Label labelSecondsInterval;
        private System.Windows.Forms.Label labelMinutesInterval;
        private System.Windows.Forms.Label labelHoursInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;
        private System.Windows.Forms.CheckBox checkBoxCaptureLimit;
        private System.Windows.Forms.CheckBox checkBoxInitialScreenshot;
        private System.Windows.Forms.NumericUpDown numericUpDownCaptureLimit;
        private System.Windows.Forms.Label labelLimit;
        private System.Windows.Forms.Timer timerScheduledCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorAbout;
        private System.Windows.Forms.Timer timerScreenCapture;
        private System.Windows.Forms.TabPage tabPageEditors;
        private System.Windows.Forms.TabPage tabPageTriggers;
        private System.Windows.Forms.TabPage tabPageRegions;
        private System.Windows.Forms.TabPage tabPageScreens;
        private System.Windows.Forms.TabPage tabPageTags;
        private System.Windows.Forms.Label labelScreenshotTitle;
        private System.Windows.Forms.TextBox textBoxScreenshotTitle;
        private System.Windows.Forms.Label labelScreenshotFormat;
        private System.Windows.Forms.TextBox textBoxScreenshotFormat;
        private System.Windows.Forms.Label labelScreenshotWidth;
        private System.Windows.Forms.TextBox textBoxScreenshotWidth;
        private System.Windows.Forms.Label labelScreenshotHeight;
        private System.Windows.Forms.TextBox textBoxScreenshotHeight;
        private System.Windows.Forms.Label labelScreenshotDate;
        private System.Windows.Forms.TextBox textBoxScreenshotDate;
        private System.Windows.Forms.Label labelScreenshotTime;
        private System.Windows.Forms.TextBox textBoxScreenshotTime;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonStartScreenCapture;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonStopScreenCapture;
        private ComboBox comboBoxFilterValue;
        private Label labelFilter;
        private Button buttonRefreshFilterValues;
        private ComboBox comboBoxFilterType;
        private Label labelDays;
        private NumericUpDown numericUpDownKeepScreenshotsForDays;
        private Label labelKeepScreenshots;
        private Timer timerPerformMaintenance;
        private ComboBox comboBoxScreenshotLabel;
        private CheckBox checkBoxScreenshotLabel;
        private ToolStripMenuItem toolStripMenuItemApplyLabel;
        private ToolStripSeparator toolStripSeparatorApplyLabel;
        private GroupBox groupBoxSecurity;
        private Label labelPasswordDescription;
        private Button buttonSetPassphrase;
        private TextBox textBoxPassphrase;
        private ToolStripStatusLabel toolStripInfo;
        private TextBox textBoxLabel;
        private Label labelLabel;
        private TabPage tabPageSchedules;
        private ToolStripMenuItem toolStripMenuItemCaptureNowEdit;
        private ToolStripMenuItem toolStripMenuItemCaptureNowArchive;
        private Label labelHelp;
        private ToolStripSplitButton toolStripSplitButtonKeyboardShortcuts;
        private Timer timerShowNextHelpTip;
        private ToolStripSplitButton toolStripSplitButtonHelp;
        private GroupBox groupBoxActiveWindowTitle;
        private TextBox textBoxActiveWindowTitle;
        private CheckBox checkBoxActiveWindowTitle;
        private ToolStripMenuItem toolStripMenuItemRegionSelectClipboard;
        private GroupBox groupBoxRegionSelectAutoSave;
        private Label labelAutoSaveMacro;
        private Label labelAutoSaveFolder;
        private TextBox textBoxAutoSaveMacro;
        private TextBox textBoxAutoSaveFolder;
        private Button buttonBrowseFolder;
        private ToolStripMenuItem toolStripMenuItemRegionSelectAutoSave;
        private ToolStripMenuItem toolStripMenuItemRegionSelectEdit;
        private ToolStripSeparator toolStripSeparatorRegionSelect;
        private ToolStripMenuItem toolStripMenuItemShowScreenCaptureStatus;
        private ToolStripSeparator toolStripSeparatorTools;
        private GroupBox groupBoxApplicationFocus;
        private ComboBox comboBoxProcessList;
        private Button buttonApplicationFocusTest;
        private Button buttonApplicationFocusRefresh;
        private ToolStripSeparator toolStripSeparatorScreenCapture;
    }
}