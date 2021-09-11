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
            this.toolStripDropDownButtonSetup = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItemSetupWizard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorSetupWizard = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemActiveWindowTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemApplicationFocus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInterval = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemKeyboardShortcuts = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSecurity = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonPreview = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButtonStartScreenCapture = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButtonStopScreenCapture = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButtonCommandLine = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButtonHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControlViews = new System.Windows.Forms.TabControl();
            this.contextMenuStripScreenshot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listBoxScreenshots = new System.Windows.Forms.ListBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripSystemTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorAbout = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemShowHideInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorInterface = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemStartScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStopScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCommandLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemScreenCaptureStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorScreenCapture = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddMacroTag = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChange = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeMacroTag = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEmailSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileTransferSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDynamicRegexValidator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorTools = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemCaptureNowArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCaptureNowEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorCaptureNow = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRegionSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorRegionSelectOptions = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRegionSelectClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectClipboardAutoSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectClipboardAutoSaveEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFloatingScreenshot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorRegionSelect = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemApplyLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorApplyLabel = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlModules = new System.Windows.Forms.TabControl();
            this.tabPageScreenshots = new System.Windows.Forms.TabPage();
            this.tabPageScreens = new System.Windows.Forms.TabPage();
            this.tabPageRegions = new System.Windows.Forms.TabPage();
            this.tabPageEditors = new System.Windows.Forms.TabPage();
            this.tabPageSchedules = new System.Windows.Forms.TabPage();
            this.tabPageMacroTags = new System.Windows.Forms.TabPage();
            this.tabPageTriggers = new System.Windows.Forms.TabPage();
            this.timerScheduledCapture = new System.Windows.Forms.Timer(this.components);
            this.timerScreenCapture = new System.Windows.Forms.Timer(this.components);
            this.comboBoxFilterValue = new System.Windows.Forms.ComboBox();
            this.labelFilter = new System.Windows.Forms.Label();
            this.buttonRefreshFilterValues = new System.Windows.Forms.Button();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.timerPerformMaintenance = new System.Windows.Forms.Timer(this.components);
            this.labelHelp = new System.Windows.Forms.Label();
            this.timerShowNextHelpTip = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.contextMenuStripSystemTrayIcon.SuspendLayout();
            this.tabControlModules.SuspendLayout();
            this.tabPageScreenshots.SuspendLayout();
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
            this.toolStripDropDownButtonSetup,
            this.toolStripDropDownButtonPreview,
            this.toolStripDropDownButtonStartScreenCapture,
            this.toolStripDropDownButtonStopScreenCapture,
            this.toolStripDropDownButtonCommandLine,
            this.toolStripDropDownButtonHelp,
            this.toolStripInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 633);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(1040, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // toolStripDropDownButtonSetup
            // 
            this.toolStripDropDownButtonSetup.AutoToolTip = false;
            this.toolStripDropDownButtonSetup.BackColor = System.Drawing.Color.White;
            this.toolStripDropDownButtonSetup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSetupWizard,
            this.toolStripSeparatorSetupWizard,
            this.toolStripMenuItemInterval,
            this.toolStripMenuItemLabels,
            this.toolStripMenuItemActiveWindowTitle,
            this.toolStripMenuItemApplicationFocus,
            this.toolStripMenuItemSecurity,
            this.toolStripMenuItemKeyboardShortcuts});
            this.toolStripDropDownButtonSetup.ForeColor = System.Drawing.Color.Black;
            this.toolStripDropDownButtonSetup.Image = global::AutoScreenCapture.Properties.Resources.setup;
            this.toolStripDropDownButtonSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonSetup.Name = "toolStripDropDownButtonSetup";
            this.toolStripDropDownButtonSetup.Size = new System.Drawing.Size(66, 20);
            this.toolStripDropDownButtonSetup.Text = "Setup";
            // 
            // toolStripMenuItemSetupWizard
            // 
            this.toolStripMenuItemSetupWizard.Name = "toolStripMenuItemSetupWizard";
            this.toolStripMenuItemSetupWizard.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemSetupWizard.Text = "Setup Wizard";
            this.toolStripMenuItemSetupWizard.Visible = false;
            this.toolStripMenuItemSetupWizard.Click += new System.EventHandler(this.toolStripMenuItemSetupWizard_Click);
            // 
            // toolStripSeparatorSetupWizard
            // 
            this.toolStripSeparatorSetupWizard.Name = "toolStripSeparatorSetupWizard";
            this.toolStripSeparatorSetupWizard.Size = new System.Drawing.Size(177, 6);
            this.toolStripSeparatorSetupWizard.Visible = false;
            // 
            // toolStripMenuItemActiveWindowTitle
            // 
            this.toolStripMenuItemActiveWindowTitle.Name = "toolStripMenuItemActiveWindowTitle";
            this.toolStripMenuItemActiveWindowTitle.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemActiveWindowTitle.Text = "Active Window Title";
            this.toolStripMenuItemActiveWindowTitle.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemApplicationFocus
            // 
            this.toolStripMenuItemApplicationFocus.Name = "toolStripMenuItemApplicationFocus";
            this.toolStripMenuItemApplicationFocus.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemApplicationFocus.Text = "Application Focus";
            this.toolStripMenuItemApplicationFocus.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemInterval
            // 
            this.toolStripMenuItemInterval.Name = "toolStripMenuItemInterval";
            this.toolStripMenuItemInterval.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemInterval.Text = "Interval";
            this.toolStripMenuItemInterval.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemKeyboardShortcuts
            // 
            this.toolStripMenuItemKeyboardShortcuts.Image = global::AutoScreenCapture.Properties.Resources.keyboard;
            this.toolStripMenuItemKeyboardShortcuts.Name = "toolStripMenuItemKeyboardShortcuts";
            this.toolStripMenuItemKeyboardShortcuts.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemKeyboardShortcuts.Text = "Keyboard Shortcuts";
            this.toolStripMenuItemKeyboardShortcuts.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemLabels
            // 
            this.toolStripMenuItemLabels.Name = "toolStripMenuItemLabels";
            this.toolStripMenuItemLabels.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemLabels.Text = "Labels";
            this.toolStripMenuItemLabels.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemSecurity
            // 
            this.toolStripMenuItemSecurity.Name = "toolStripMenuItemSecurity";
            this.toolStripMenuItemSecurity.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemSecurity.Text = "Security";
            this.toolStripMenuItemSecurity.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripDropDownButtonPreview
            // 
            this.toolStripDropDownButtonPreview.AutoToolTip = false;
            this.toolStripDropDownButtonPreview.BackColor = System.Drawing.Color.White;
            this.toolStripDropDownButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonPreview.ForeColor = System.Drawing.Color.Black;
            this.toolStripDropDownButtonPreview.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonPreview.Image")));
            this.toolStripDropDownButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonPreview.Name = "toolStripDropDownButtonPreview";
            this.toolStripDropDownButtonPreview.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripDropDownButtonPreview.ShowDropDownArrow = false;
            this.toolStripDropDownButtonPreview.Size = new System.Drawing.Size(52, 20);
            this.toolStripDropDownButtonPreview.Text = "Preview";
            this.toolStripDropDownButtonPreview.Click += new System.EventHandler(this.toolStripDropDownButtonPreview_Click);
            // 
            // toolStripDropDownButtonStartScreenCapture
            // 
            this.toolStripDropDownButtonStartScreenCapture.AutoToolTip = false;
            this.toolStripDropDownButtonStartScreenCapture.BackColor = System.Drawing.Color.White;
            this.toolStripDropDownButtonStartScreenCapture.Enabled = false;
            this.toolStripDropDownButtonStartScreenCapture.ForeColor = System.Drawing.Color.Black;
            this.toolStripDropDownButtonStartScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.start_screen_capture;
            this.toolStripDropDownButtonStartScreenCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonStartScreenCapture.Name = "toolStripDropDownButtonStartScreenCapture";
            this.toolStripDropDownButtonStartScreenCapture.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripDropDownButtonStartScreenCapture.ShowDropDownArrow = false;
            this.toolStripDropDownButtonStartScreenCapture.Size = new System.Drawing.Size(134, 20);
            this.toolStripDropDownButtonStartScreenCapture.Text = "Start Screen Capture";
            this.toolStripDropDownButtonStartScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemStartScreenCapture_Click);
            // 
            // toolStripDropDownButtonStopScreenCapture
            // 
            this.toolStripDropDownButtonStopScreenCapture.AutoToolTip = false;
            this.toolStripDropDownButtonStopScreenCapture.BackColor = System.Drawing.Color.White;
            this.toolStripDropDownButtonStopScreenCapture.Enabled = false;
            this.toolStripDropDownButtonStopScreenCapture.ForeColor = System.Drawing.Color.Black;
            this.toolStripDropDownButtonStopScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.stop_screen_capture;
            this.toolStripDropDownButtonStopScreenCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonStopScreenCapture.Name = "toolStripDropDownButtonStopScreenCapture";
            this.toolStripDropDownButtonStopScreenCapture.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripDropDownButtonStopScreenCapture.ShowDropDownArrow = false;
            this.toolStripDropDownButtonStopScreenCapture.Size = new System.Drawing.Size(134, 20);
            this.toolStripDropDownButtonStopScreenCapture.Text = "Stop Screen Capture";
            this.toolStripDropDownButtonStopScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemStopScreenCapture_Click);
            // 
            // toolStripDropDownButtonCommandLine
            // 
            this.toolStripDropDownButtonCommandLine.AutoToolTip = false;
            this.toolStripDropDownButtonCommandLine.BackColor = System.Drawing.Color.White;
            this.toolStripDropDownButtonCommandLine.ForeColor = System.Drawing.Color.Black;
            this.toolStripDropDownButtonCommandLine.Image = global::AutoScreenCapture.Properties.Resources.command_line;
            this.toolStripDropDownButtonCommandLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonCommandLine.Name = "toolStripDropDownButtonCommandLine";
            this.toolStripDropDownButtonCommandLine.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripDropDownButtonCommandLine.ShowDropDownArrow = false;
            this.toolStripDropDownButtonCommandLine.Size = new System.Drawing.Size(109, 20);
            this.toolStripDropDownButtonCommandLine.Text = "Command Line";
            this.toolStripDropDownButtonCommandLine.Click += new System.EventHandler(this.toolStripDropDownButtonCommandLine_Click);
            // 
            // toolStripDropDownButtonHelp
            // 
            this.toolStripDropDownButtonHelp.AutoToolTip = false;
            this.toolStripDropDownButtonHelp.BackColor = System.Drawing.Color.White;
            this.toolStripDropDownButtonHelp.ForeColor = System.Drawing.Color.Black;
            this.toolStripDropDownButtonHelp.Image = global::AutoScreenCapture.Properties.Resources.help;
            this.toolStripDropDownButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonHelp.Name = "toolStripDropDownButtonHelp";
            this.toolStripDropDownButtonHelp.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripDropDownButtonHelp.ShowDropDownArrow = false;
            this.toolStripDropDownButtonHelp.Size = new System.Drawing.Size(52, 20);
            this.toolStripDropDownButtonHelp.Text = "Help";
            this.toolStripDropDownButtonHelp.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
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
            this.toolStripInfo.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripInfo.Size = new System.Drawing.Size(478, 17);
            this.toolStripInfo.Spring = true;
            this.toolStripInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.tabControlViews.Size = new System.Drawing.Size(789, 600);
            this.tabControlViews.TabIndex = 0;
            this.tabControlViews.TabStop = false;
            this.tabControlViews.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlViews_Selected);
            // 
            // contextMenuStripScreenshot
            // 
            this.contextMenuStripScreenshot.Name = "contextMenuStripScreenshotPreview";
            this.contextMenuStripScreenshot.Size = new System.Drawing.Size(61, 4);
            // 
            // listBoxScreenshots
            // 
            this.listBoxScreenshots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxScreenshots.FormattingEnabled = true;
            this.listBoxScreenshots.HorizontalScrollbar = true;
            this.listBoxScreenshots.IntegralHeight = false;
            this.listBoxScreenshots.Location = new System.Drawing.Point(3, 3);
            this.listBoxScreenshots.Name = "listBoxScreenshots";
            this.listBoxScreenshots.ScrollAlwaysVisible = true;
            this.listBoxScreenshots.Size = new System.Drawing.Size(235, 357);
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
            this.toolStripMenuItemShowHideInterface,
            this.toolStripSeparatorInterface,
            this.toolStripMenuItemStartScreenCapture,
            this.toolStripMenuItemStopScreenCapture,
            this.toolStripMenuItemCommandLine,
            this.toolStripMenuItemScreenCaptureStatus,
            this.toolStripSeparatorScreenCapture,
            this.toolStripMenuItemAdd,
            this.toolStripMenuItemChange,
            this.toolStripMenuItemSettings,
            this.toolStripMenuItemTools,
            this.toolStripSeparatorTools,
            this.toolStripMenuItemCaptureNowArchive,
            this.toolStripMenuItemCaptureNowEdit,
            this.toolStripSeparatorCaptureNow,
            this.toolStripMenuItemRegionSelect,
            this.toolStripSeparatorRegionSelect,
            this.toolStripMenuItemApplyLabel,
            this.toolStripSeparatorApplyLabel,
            this.toolStripMenuItemExit});
            this.contextMenuStripSystemTrayIcon.Name = "contextMenuStrip";
            this.contextMenuStripSystemTrayIcon.Size = new System.Drawing.Size(220, 376);
            this.contextMenuStripSystemTrayIcon.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripSystemTrayIcon_Opening);
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Image = global::AutoScreenCapture.Properties.Resources.about;
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemAbout.Text = "About Auto Screen Capture";
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // toolStripSeparatorAbout
            // 
            this.toolStripSeparatorAbout.Name = "toolStripSeparatorAbout";
            this.toolStripSeparatorAbout.Size = new System.Drawing.Size(216, 6);
            // 
            // toolStripMenuItemShowHideInterface
            // 
            this.toolStripMenuItemShowHideInterface.Name = "toolStripMenuItemShowHideInterface";
            this.toolStripMenuItemShowHideInterface.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemShowHideInterface.Text = "Show/Hide Interface";
            this.toolStripMenuItemShowHideInterface.Click += new System.EventHandler(this.toolStripMenuItemShowHideInterface_Click);
            // 
            // toolStripSeparatorInterface
            // 
            this.toolStripSeparatorInterface.Name = "toolStripSeparatorInterface";
            this.toolStripSeparatorInterface.Size = new System.Drawing.Size(216, 6);
            // 
            // toolStripMenuItemStartScreenCapture
            // 
            this.toolStripMenuItemStartScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.start_screen_capture;
            this.toolStripMenuItemStartScreenCapture.Name = "toolStripMenuItemStartScreenCapture";
            this.toolStripMenuItemStartScreenCapture.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemStartScreenCapture.Text = "Start Screen Capture";
            this.toolStripMenuItemStartScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemStartScreenCapture_Click);
            // 
            // toolStripMenuItemStopScreenCapture
            // 
            this.toolStripMenuItemStopScreenCapture.Enabled = false;
            this.toolStripMenuItemStopScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.stop_screen_capture;
            this.toolStripMenuItemStopScreenCapture.Name = "toolStripMenuItemStopScreenCapture";
            this.toolStripMenuItemStopScreenCapture.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemStopScreenCapture.Text = "Stop Screen Capture";
            this.toolStripMenuItemStopScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemStopScreenCapture_Click);
            // 
            // toolStripMenuItemCommandLine
            // 
            this.toolStripMenuItemCommandLine.Image = global::AutoScreenCapture.Properties.Resources.command_line;
            this.toolStripMenuItemCommandLine.Name = "toolStripMenuItemCommandLine";
            this.toolStripMenuItemCommandLine.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemCommandLine.Text = "Command Line";
            this.toolStripMenuItemCommandLine.Click += new System.EventHandler(this.toolStripDropDownButtonCommandLine_Click);
            // 
            // toolStripMenuItemScreenCaptureStatus
            // 
            this.toolStripMenuItemScreenCaptureStatus.Name = "toolStripMenuItemScreenCaptureStatus";
            this.toolStripMenuItemScreenCaptureStatus.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemScreenCaptureStatus.Text = "Screen Capture Status";
            this.toolStripMenuItemScreenCaptureStatus.Click += new System.EventHandler(this.toolStripMenuItemScreenCaptureStatus_Click);
            // 
            // toolStripSeparatorScreenCapture
            // 
            this.toolStripSeparatorScreenCapture.Name = "toolStripSeparatorScreenCapture";
            this.toolStripSeparatorScreenCapture.Size = new System.Drawing.Size(216, 6);
            // 
            // toolStripMenuItemAdd
            // 
            this.toolStripMenuItemAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAddScreen,
            this.toolStripMenuItemAddRegion,
            this.toolStripMenuItemAddEditor,
            this.toolStripMenuItemAddSchedule,
            this.toolStripMenuItemAddMacroTag,
            this.toolStripMenuItemAddTrigger});
            this.toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
            this.toolStripMenuItemAdd.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemAdd.Text = "Add";
            // 
            // toolStripMenuItemAddScreen
            // 
            this.toolStripMenuItemAddScreen.Image = global::AutoScreenCapture.Properties.Resources.screen;
            this.toolStripMenuItemAddScreen.Name = "toolStripMenuItemAddScreen";
            this.toolStripMenuItemAddScreen.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemAddScreen.Text = "Screen";
            this.toolStripMenuItemAddScreen.Click += new System.EventHandler(this.addScreen_Click);
            // 
            // toolStripMenuItemAddRegion
            // 
            this.toolStripMenuItemAddRegion.Image = global::AutoScreenCapture.Properties.Resources.region;
            this.toolStripMenuItemAddRegion.Name = "toolStripMenuItemAddRegion";
            this.toolStripMenuItemAddRegion.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemAddRegion.Text = "Region";
            this.toolStripMenuItemAddRegion.Click += new System.EventHandler(this.addRegion_Click);
            // 
            // toolStripMenuItemAddEditor
            // 
            this.toolStripMenuItemAddEditor.Image = global::AutoScreenCapture.Properties.Resources.edit;
            this.toolStripMenuItemAddEditor.Name = "toolStripMenuItemAddEditor";
            this.toolStripMenuItemAddEditor.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemAddEditor.Text = "Editor";
            this.toolStripMenuItemAddEditor.Click += new System.EventHandler(this.addEditor_Click);
            // 
            // toolStripMenuItemAddSchedule
            // 
            this.toolStripMenuItemAddSchedule.Image = global::AutoScreenCapture.Properties.Resources.schedule;
            this.toolStripMenuItemAddSchedule.Name = "toolStripMenuItemAddSchedule";
            this.toolStripMenuItemAddSchedule.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemAddSchedule.Text = "Schedule";
            this.toolStripMenuItemAddSchedule.Click += new System.EventHandler(this.addSchedule_Click);
            // 
            // toolStripMenuItemAddMacroTag
            // 
            this.toolStripMenuItemAddMacroTag.Image = global::AutoScreenCapture.Properties.Resources.brick;
            this.toolStripMenuItemAddMacroTag.Name = "toolStripMenuItemAddMacroTag";
            this.toolStripMenuItemAddMacroTag.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemAddMacroTag.Text = "Macro Tag";
            this.toolStripMenuItemAddMacroTag.Click += new System.EventHandler(this.addMacroTag_Click);
            // 
            // toolStripMenuItemAddTrigger
            // 
            this.toolStripMenuItemAddTrigger.Image = global::AutoScreenCapture.Properties.Resources.trigger;
            this.toolStripMenuItemAddTrigger.Name = "toolStripMenuItemAddTrigger";
            this.toolStripMenuItemAddTrigger.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemAddTrigger.Text = "Trigger";
            this.toolStripMenuItemAddTrigger.Click += new System.EventHandler(this.addTrigger_Click);
            // 
            // toolStripMenuItemChange
            // 
            this.toolStripMenuItemChange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemChangeScreen,
            this.toolStripMenuItemChangeRegion,
            this.toolStripMenuItemChangeEditor,
            this.toolStripMenuItemChangeSchedule,
            this.toolStripMenuItemChangeMacroTag,
            this.toolStripMenuItemChangeTrigger});
            this.toolStripMenuItemChange.Name = "toolStripMenuItemChange";
            this.toolStripMenuItemChange.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemChange.Text = "Change";
            // 
            // toolStripMenuItemChangeScreen
            // 
            this.toolStripMenuItemChangeScreen.Image = global::AutoScreenCapture.Properties.Resources.screen;
            this.toolStripMenuItemChangeScreen.Name = "toolStripMenuItemChangeScreen";
            this.toolStripMenuItemChangeScreen.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemChangeScreen.Text = "Screen";
            // 
            // toolStripMenuItemChangeRegion
            // 
            this.toolStripMenuItemChangeRegion.Image = global::AutoScreenCapture.Properties.Resources.region;
            this.toolStripMenuItemChangeRegion.Name = "toolStripMenuItemChangeRegion";
            this.toolStripMenuItemChangeRegion.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemChangeRegion.Text = "Region";
            // 
            // toolStripMenuItemChangeEditor
            // 
            this.toolStripMenuItemChangeEditor.Image = global::AutoScreenCapture.Properties.Resources.edit;
            this.toolStripMenuItemChangeEditor.Name = "toolStripMenuItemChangeEditor";
            this.toolStripMenuItemChangeEditor.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemChangeEditor.Text = "Editor";
            // 
            // toolStripMenuItemChangeSchedule
            // 
            this.toolStripMenuItemChangeSchedule.Image = global::AutoScreenCapture.Properties.Resources.schedule;
            this.toolStripMenuItemChangeSchedule.Name = "toolStripMenuItemChangeSchedule";
            this.toolStripMenuItemChangeSchedule.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemChangeSchedule.Text = "Schedule";
            // 
            // toolStripMenuItemChangeMacroTag
            // 
            this.toolStripMenuItemChangeMacroTag.Image = global::AutoScreenCapture.Properties.Resources.brick;
            this.toolStripMenuItemChangeMacroTag.Name = "toolStripMenuItemChangeMacroTag";
            this.toolStripMenuItemChangeMacroTag.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemChangeMacroTag.Text = "Macro Tag";
            // 
            // toolStripMenuItemChangeTrigger
            // 
            this.toolStripMenuItemChangeTrigger.Image = global::AutoScreenCapture.Properties.Resources.trigger;
            this.toolStripMenuItemChangeTrigger.Name = "toolStripMenuItemChangeTrigger";
            this.toolStripMenuItemChangeTrigger.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItemChangeTrigger.Text = "Trigger";
            // 
            // toolStripMenuItemSettings
            // 
            this.toolStripMenuItemSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEmailSettings,
            this.toolStripMenuItemFileTransferSettings});
            this.toolStripMenuItemSettings.Name = "toolStripMenuItemSettings";
            this.toolStripMenuItemSettings.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemSettings.Text = "Settings";
            // 
            // toolStripMenuItemEmailSettings
            // 
            this.toolStripMenuItemEmailSettings.Image = global::AutoScreenCapture.Properties.Resources.email;
            this.toolStripMenuItemEmailSettings.Name = "toolStripMenuItemEmailSettings";
            this.toolStripMenuItemEmailSettings.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuItemEmailSettings.Text = "Email Settings";
            this.toolStripMenuItemEmailSettings.Visible = false;
            this.toolStripMenuItemEmailSettings.Click += new System.EventHandler(this.toolStripMenuItemEmailSettings_Click);
            // 
            // toolStripMenuItemFileTransferSettings
            // 
            this.toolStripMenuItemFileTransferSettings.Image = global::AutoScreenCapture.Properties.Resources.file_transfer;
            this.toolStripMenuItemFileTransferSettings.Name = "toolStripMenuItemFileTransferSettings";
            this.toolStripMenuItemFileTransferSettings.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuItemFileTransferSettings.Text = "File Transfer Settings";
            this.toolStripMenuItemFileTransferSettings.Visible = false;
            this.toolStripMenuItemFileTransferSettings.Click += new System.EventHandler(this.toolStripMenuItemFileTransferSettings_Click);
            // 
            // toolStripMenuItemTools
            // 
            this.toolStripMenuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDynamicRegexValidator});
            this.toolStripMenuItemTools.Name = "toolStripMenuItemTools";
            this.toolStripMenuItemTools.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemTools.Text = "Tools";
            // 
            // toolStripMenuItemDynamicRegexValidator
            // 
            this.toolStripMenuItemDynamicRegexValidator.Name = "toolStripMenuItemDynamicRegexValidator";
            this.toolStripMenuItemDynamicRegexValidator.Size = new System.Drawing.Size(204, 22);
            this.toolStripMenuItemDynamicRegexValidator.Text = "Dynamic Regex Validator";
            this.toolStripMenuItemDynamicRegexValidator.Click += new System.EventHandler(this.toolStripMenuItemDynamicRegexValidator_Click);
            // 
            // toolStripSeparatorTools
            // 
            this.toolStripSeparatorTools.Name = "toolStripSeparatorTools";
            this.toolStripSeparatorTools.Size = new System.Drawing.Size(216, 6);
            // 
            // toolStripMenuItemCaptureNowArchive
            // 
            this.toolStripMenuItemCaptureNowArchive.Image = global::AutoScreenCapture.Properties.Resources.capture_archive;
            this.toolStripMenuItemCaptureNowArchive.Name = "toolStripMenuItemCaptureNowArchive";
            this.toolStripMenuItemCaptureNowArchive.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemCaptureNowArchive.Text = "Capture Now / Archive";
            this.toolStripMenuItemCaptureNowArchive.Click += new System.EventHandler(this.toolStripMenuItemCaptureNowArchive_Click);
            // 
            // toolStripMenuItemCaptureNowEdit
            // 
            this.toolStripMenuItemCaptureNowEdit.Image = global::AutoScreenCapture.Properties.Resources.capture_edit;
            this.toolStripMenuItemCaptureNowEdit.Name = "toolStripMenuItemCaptureNowEdit";
            this.toolStripMenuItemCaptureNowEdit.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemCaptureNowEdit.Text = "Capture Now / Edit";
            this.toolStripMenuItemCaptureNowEdit.Click += new System.EventHandler(this.toolStripMenuItemCaptureNowEdit_Click);
            // 
            // toolStripSeparatorCaptureNow
            // 
            this.toolStripSeparatorCaptureNow.Name = "toolStripSeparatorCaptureNow";
            this.toolStripSeparatorCaptureNow.Size = new System.Drawing.Size(216, 6);
            // 
            // toolStripMenuItemRegionSelect
            // 
            this.toolStripMenuItemRegionSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRegionSelectOptions,
            this.toolStripSeparatorRegionSelectOptions,
            this.toolStripMenuItemRegionSelectClipboard,
            this.toolStripMenuItemRegionSelectClipboardAutoSave,
            this.toolStripMenuItemRegionSelectClipboardAutoSaveEdit,
            this.toolStripMenuItemFloatingScreenshot});
            this.toolStripMenuItemRegionSelect.Image = global::AutoScreenCapture.Properties.Resources.region;
            this.toolStripMenuItemRegionSelect.Name = "toolStripMenuItemRegionSelect";
            this.toolStripMenuItemRegionSelect.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemRegionSelect.Text = "Region Select";
            // 
            // toolStripMenuItemRegionSelectOptions
            // 
            this.toolStripMenuItemRegionSelectOptions.Name = "toolStripMenuItemRegionSelectOptions";
            this.toolStripMenuItemRegionSelectOptions.Size = new System.Drawing.Size(241, 22);
            this.toolStripMenuItemRegionSelectOptions.Text = "Region Select Options ...";
            this.toolStripMenuItemRegionSelectOptions.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectOptions_Click);
            // 
            // toolStripSeparatorRegionSelectOptions
            // 
            this.toolStripSeparatorRegionSelectOptions.Name = "toolStripSeparatorRegionSelectOptions";
            this.toolStripSeparatorRegionSelectOptions.Size = new System.Drawing.Size(238, 6);
            // 
            // toolStripMenuItemRegionSelectClipboard
            // 
            this.toolStripMenuItemRegionSelectClipboard.Name = "toolStripMenuItemRegionSelectClipboard";
            this.toolStripMenuItemRegionSelectClipboard.Size = new System.Drawing.Size(241, 22);
            this.toolStripMenuItemRegionSelectClipboard.Text = "Clipboard";
            this.toolStripMenuItemRegionSelectClipboard.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectClipboard_Click);
            // 
            // toolStripMenuItemRegionSelectClipboardAutoSave
            // 
            this.toolStripMenuItemRegionSelectClipboardAutoSave.Name = "toolStripMenuItemRegionSelectClipboardAutoSave";
            this.toolStripMenuItemRegionSelectClipboardAutoSave.Size = new System.Drawing.Size(241, 22);
            this.toolStripMenuItemRegionSelectClipboardAutoSave.Text = "Clipboard / Auto Save";
            this.toolStripMenuItemRegionSelectClipboardAutoSave.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectClipboardAutoSave_Click);
            // 
            // toolStripMenuItemRegionSelectClipboardAutoSaveEdit
            // 
            this.toolStripMenuItemRegionSelectClipboardAutoSaveEdit.Name = "toolStripMenuItemRegionSelectClipboardAutoSaveEdit";
            this.toolStripMenuItemRegionSelectClipboardAutoSaveEdit.Size = new System.Drawing.Size(241, 22);
            this.toolStripMenuItemRegionSelectClipboardAutoSaveEdit.Text = "Clipboard / Auto Save / Edit";
            this.toolStripMenuItemRegionSelectClipboardAutoSaveEdit.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectClipboardAutoSaveEdit_Click);
            // 
            // toolStripMenuItemFloatingScreenshot
            // 
            this.toolStripMenuItemFloatingScreenshot.Name = "toolStripMenuItemFloatingScreenshot";
            this.toolStripMenuItemFloatingScreenshot.Size = new System.Drawing.Size(241, 22);
            this.toolStripMenuItemFloatingScreenshot.Text = "Clipboard / Floating Screenshot";
            this.toolStripMenuItemFloatingScreenshot.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectClipboardFloatingScreenshot_Click);
            // 
            // toolStripSeparatorRegionSelect
            // 
            this.toolStripSeparatorRegionSelect.Name = "toolStripSeparatorRegionSelect";
            this.toolStripSeparatorRegionSelect.Size = new System.Drawing.Size(216, 6);
            // 
            // toolStripMenuItemApplyLabel
            // 
            this.toolStripMenuItemApplyLabel.Name = "toolStripMenuItemApplyLabel";
            this.toolStripMenuItemApplyLabel.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemApplyLabel.Text = "Apply Label";
            // 
            // toolStripSeparatorApplyLabel
            // 
            this.toolStripSeparatorApplyLabel.Name = "toolStripSeparatorApplyLabel";
            this.toolStripSeparatorApplyLabel.Size = new System.Drawing.Size(216, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Image = global::AutoScreenCapture.Properties.Resources.exit;
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.ShowShortcutKeys = false;
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemExit.Text = "Exit";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // tabControlModules
            // 
            this.tabControlModules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlModules.Controls.Add(this.tabPageScreenshots);
            this.tabControlModules.Controls.Add(this.tabPageScreens);
            this.tabControlModules.Controls.Add(this.tabPageRegions);
            this.tabControlModules.Controls.Add(this.tabPageEditors);
            this.tabControlModules.Controls.Add(this.tabPageSchedules);
            this.tabControlModules.Controls.Add(this.tabPageMacroTags);
            this.tabControlModules.Controls.Add(this.tabPageTriggers);
            this.tabControlModules.Location = new System.Drawing.Point(0, 220);
            this.tabControlModules.Multiline = true;
            this.tabControlModules.Name = "tabControlModules";
            this.tabControlModules.SelectedIndex = 0;
            this.tabControlModules.Size = new System.Drawing.Size(249, 407);
            this.tabControlModules.TabIndex = 0;
            this.tabControlModules.TabStop = false;
            // 
            // tabPageScreenshots
            // 
            this.tabPageScreenshots.AutoScroll = true;
            this.tabPageScreenshots.Controls.Add(this.listBoxScreenshots);
            this.tabPageScreenshots.Location = new System.Drawing.Point(4, 40);
            this.tabPageScreenshots.Name = "tabPageScreenshots";
            this.tabPageScreenshots.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScreenshots.Size = new System.Drawing.Size(241, 363);
            this.tabPageScreenshots.TabIndex = 1;
            this.tabPageScreenshots.Text = "Screenshots";
            this.tabPageScreenshots.UseVisualStyleBackColor = true;
            // 
            // tabPageScreens
            // 
            this.tabPageScreens.AutoScroll = true;
            this.tabPageScreens.Location = new System.Drawing.Point(4, 40);
            this.tabPageScreens.Name = "tabPageScreens";
            this.tabPageScreens.Size = new System.Drawing.Size(241, 363);
            this.tabPageScreens.TabIndex = 5;
            this.tabPageScreens.Text = "Screens";
            this.tabPageScreens.UseVisualStyleBackColor = true;
            // 
            // tabPageRegions
            // 
            this.tabPageRegions.AutoScroll = true;
            this.tabPageRegions.Location = new System.Drawing.Point(4, 40);
            this.tabPageRegions.Name = "tabPageRegions";
            this.tabPageRegions.Size = new System.Drawing.Size(241, 363);
            this.tabPageRegions.TabIndex = 4;
            this.tabPageRegions.Text = "Regions";
            this.tabPageRegions.UseVisualStyleBackColor = true;
            // 
            // tabPageEditors
            // 
            this.tabPageEditors.AutoScroll = true;
            this.tabPageEditors.Location = new System.Drawing.Point(4, 40);
            this.tabPageEditors.Name = "tabPageEditors";
            this.tabPageEditors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEditors.Size = new System.Drawing.Size(241, 363);
            this.tabPageEditors.TabIndex = 2;
            this.tabPageEditors.Text = "Editors";
            this.tabPageEditors.UseVisualStyleBackColor = true;
            // 
            // tabPageSchedules
            // 
            this.tabPageSchedules.Location = new System.Drawing.Point(4, 40);
            this.tabPageSchedules.Name = "tabPageSchedules";
            this.tabPageSchedules.Size = new System.Drawing.Size(241, 363);
            this.tabPageSchedules.TabIndex = 8;
            this.tabPageSchedules.Text = "Schedules";
            this.tabPageSchedules.UseVisualStyleBackColor = true;
            // 
            // tabPageMacroTags
            // 
            this.tabPageMacroTags.AutoScroll = true;
            this.tabPageMacroTags.Location = new System.Drawing.Point(4, 40);
            this.tabPageMacroTags.Name = "tabPageMacroTags";
            this.tabPageMacroTags.Size = new System.Drawing.Size(241, 363);
            this.tabPageMacroTags.TabIndex = 7;
            this.tabPageMacroTags.Text = "Macro Tags";
            this.tabPageMacroTags.UseVisualStyleBackColor = true;
            // 
            // tabPageTriggers
            // 
            this.tabPageTriggers.AutoScroll = true;
            this.tabPageTriggers.Location = new System.Drawing.Point(4, 40);
            this.tabPageTriggers.Name = "tabPageTriggers";
            this.tabPageTriggers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTriggers.Size = new System.Drawing.Size(241, 363);
            this.tabPageTriggers.TabIndex = 3;
            this.tabPageTriggers.Text = "Triggers";
            this.tabPageTriggers.UseVisualStyleBackColor = true;
            // 
            // timerScheduledCapture
            // 
            this.timerScheduledCapture.Interval = 1000;
            this.timerScheduledCapture.Tick += new System.EventHandler(this.timerScheduledCapture_Tick);
            // 
            // timerScreenCapture
            // 
            this.timerScreenCapture.Tick += new System.EventHandler(this.timerScreenCapture_Tick);
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
            // 
            // timerPerformMaintenance
            // 
            this.timerPerformMaintenance.Enabled = true;
            this.timerPerformMaintenance.Interval = 300000;
            this.timerPerformMaintenance.Tick += new System.EventHandler(this.timerPerformMaintenance_Tick);
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
            this.labelHelp.Size = new System.Drawing.Size(1036, 17);
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
            this.ClientSize = new System.Drawing.Size(1040, 655);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.comboBoxFilterType);
            this.Controls.Add(this.buttonRefreshFilterValues);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.comboBoxFilterValue);
            this.Controls.Add(this.tabControlModules);
            this.Controls.Add(this.tabControlViews);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.monthCalendar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1056, 694);
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
            this.tabPageScreenshots.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStripScreenshot;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowHideInterface;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorInterface;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorCaptureNow;
        private System.Windows.Forms.TabControl tabControlModules;
        private System.Windows.Forms.TabPage tabPageScreenshots;
        private System.Windows.Forms.Timer timerScheduledCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorAbout;
        private System.Windows.Forms.Timer timerScreenCapture;
        private System.Windows.Forms.TabPage tabPageEditors;
        private System.Windows.Forms.TabPage tabPageTriggers;
        private System.Windows.Forms.TabPage tabPageRegions;
        private System.Windows.Forms.TabPage tabPageScreens;
        private System.Windows.Forms.TabPage tabPageMacroTags;
        private ComboBox comboBoxFilterValue;
        private Label labelFilter;
        private Button buttonRefreshFilterValues;
        private ComboBox comboBoxFilterType;
        private Timer timerPerformMaintenance;
        private ToolStripMenuItem toolStripMenuItemApplyLabel;
        private ToolStripSeparator toolStripSeparatorApplyLabel;
        private ToolStripStatusLabel toolStripInfo;
        private TabPage tabPageSchedules;
        private ToolStripMenuItem toolStripMenuItemCaptureNowEdit;
        private ToolStripMenuItem toolStripMenuItemCaptureNowArchive;
        private Label labelHelp;
        private Timer timerShowNextHelpTip;
        private ToolStripMenuItem toolStripMenuItemRegionSelectClipboard;
        private ToolStripMenuItem toolStripMenuItemRegionSelectClipboardAutoSave;
        private ToolStripMenuItem toolStripMenuItemRegionSelectClipboardAutoSaveEdit;
        private ToolStripSeparator toolStripSeparatorRegionSelect;
        private ToolStripSeparator toolStripSeparatorTools;
        private ToolStripSeparator toolStripSeparatorScreenCapture;
        private ToolStripMenuItem toolStripMenuItemEmailSettings;
        private ToolStripMenuItem toolStripMenuItemFileTransferSettings;
        private ToolStripMenuItem toolStripMenuItemAdd;
        private ToolStripMenuItem toolStripMenuItemAddScreen;
        private ToolStripMenuItem toolStripMenuItemAddRegion;
        private ToolStripMenuItem toolStripMenuItemAddEditor;
        private ToolStripMenuItem toolStripMenuItemAddSchedule;
        private ToolStripMenuItem toolStripMenuItemAddMacroTag;
        private ToolStripMenuItem toolStripMenuItemAddTrigger;
        private ToolStripMenuItem toolStripMenuItemRegionSelect;
        private ToolStripMenuItem toolStripMenuItemSettings;
        private ToolStripMenuItem toolStripMenuItemTools;
        private ToolStripMenuItem toolStripMenuItemDynamicRegexValidator;
        private ToolStripMenuItem toolStripMenuItemScreenCaptureStatus;
        private ToolStripMenuItem toolStripMenuItemChange;
        private ToolStripMenuItem toolStripMenuItemChangeScreen;
        private ToolStripMenuItem toolStripMenuItemChangeRegion;
        private ToolStripMenuItem toolStripMenuItemChangeEditor;
        private ToolStripMenuItem toolStripMenuItemChangeSchedule;
        private ToolStripMenuItem toolStripMenuItemChangeMacroTag;
        private ToolStripMenuItem toolStripMenuItemChangeTrigger;
        private ToolStripMenuItem toolStripMenuItemRegionSelectOptions;
        private ToolStripSeparator toolStripSeparatorRegionSelectOptions;
        private ToolStripMenuItem toolStripMenuItemFloatingScreenshot;
        private ToolStripDropDownButton toolStripDropDownButtonSetup;
        private ToolStripDropDownButton toolStripDropDownButtonStartScreenCapture;
        private ToolStripDropDownButton toolStripDropDownButtonStopScreenCapture;
        private ToolStripDropDownButton toolStripDropDownButtonCommandLine;
        private ToolStripDropDownButton toolStripDropDownButtonHelp;
        private ToolStripMenuItem toolStripMenuItemSecurity;
        private ToolStripMenuItem toolStripMenuItemApplicationFocus;
        private ToolStripMenuItem toolStripMenuItemActiveWindowTitle;
        private ToolStripMenuItem toolStripMenuItemLabels;
        private ToolStripMenuItem toolStripMenuItemInterval;
        private ToolStripMenuItem toolStripMenuItemCommandLine;
        private ToolStripMenuItem toolStripMenuItemKeyboardShortcuts;
        private ToolStripDropDownButton toolStripDropDownButtonPreview;
        private ToolStripMenuItem toolStripMenuItemSetupWizard;
        private ToolStripSeparator toolStripSeparatorSetupWizard;
    }
}