﻿using System.Windows.Forms;

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
            this.toolStripMenuItemScreenshotsFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFilenamePattern = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemImageFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInterval = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOptimizeScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemActiveWindowTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemApplicationFocus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSecurity = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemKeyboardShortcuts = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonPreview = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButtonStartScreenCapture = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButtonStopScreenCapture = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripCurrentDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripCurrentTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripCycleCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMenuItemEmailSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileTransferSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonEmailSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButtonFileTransferSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.tabControlViews = new System.Windows.Forms.TabControl();
            this.contextMenuStripScreenshot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listBoxScreenshots = new System.Windows.Forms.ListBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripSystemTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorAbout = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripRootMenuItemCommandDeck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemShowHideInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorInterface = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemStartScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStopScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemScreenCaptureStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenProgramFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCommandLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorScreenCapture = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddMacroTag = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemConfigure = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeMacroTag = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAutoScreenCaptureForBeginners = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCommandDeck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDynamicRegexValidator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEncryptorDecryptor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLabelSwitcher = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorTools = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemCaptureNowOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCaptureNowArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCaptureNowEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorCaptureNow = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRegionSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectCommandDeck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorRegionSelectOptions = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRegionSelectClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectClipboardAutoSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectClipboardAutoSaveEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectClipboardFloatingScreenshot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectFloatingScreenshot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectAddRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRegionSelectAddRegionExpress = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorRegionSelect = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemApplyLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorApplyLabel = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlModules = new System.Windows.Forms.TabControl();
            this.tabPageScreens = new System.Windows.Forms.TabPage();
            this.tabPageRegions = new System.Windows.Forms.TabPage();
            this.tabPageEditors = new System.Windows.Forms.TabPage();
            this.tabPageSchedules = new System.Windows.Forms.TabPage();
            this.tabPageMacroTags = new System.Windows.Forms.TabPage();
            this.tabPageTriggers = new System.Windows.Forms.TabPage();
            this.timerHelpTips = new System.Windows.Forms.Timer(this.components);
            this.timerScreenCapture = new System.Windows.Forms.Timer(this.components);
            this.comboBoxFilterValue = new System.Windows.Forms.ComboBox();
            this.labelFilter = new System.Windows.Forms.Label();
            this.buttonRefreshFilterValues = new System.Windows.Forms.Button();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.timerPerformMaintenance = new System.Windows.Forms.Timer(this.components);
            this.labelHelp = new System.Windows.Forms.Label();
            this.timerShowNextHelpTip = new System.Windows.Forms.Timer(this.components);
            this.labelModuleHelp = new System.Windows.Forms.Label();
            this.timerParseCommands = new System.Windows.Forms.Timer(this.components);
            this.timerScheduleCheck = new System.Windows.Forms.Timer(this.components);
            this.timerTriggerCheck = new System.Windows.Forms.Timer(this.components);
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMainMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemOpenProgramFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemEmailSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemFileTransferSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorExit = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMainMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemScreenshotsFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemFilenamePattern = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemImageFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemInterval = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemOptimizeScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemActiveWindowTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemApplicationFocus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemSecurity = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemKeyboardShortcuts = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemControl = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemStartScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemStopScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemControlCommandDeck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemScreenCaptureStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemScreenCaptureStatusWithLabelSwitcher = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemRegionSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemRegionSelectOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemCommandDeck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorMainRegionSelect = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMainMenuItemRegionSelectClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemRegionSelectClipboardAutoSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemRegionSelectClipboardAutoSaveEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemRegionSelectClipboardFloatingScreenshot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemRegionSelectFloatingScreenshot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemRegionSelectAddRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemRegionSelectAddRegionExpress = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemScreens = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemRegions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemEditors = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemSchedules = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemMacroTags = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemTriggers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemAutoScreenCaptureForBeginners = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemToolsCommandDeck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemDynamicRegexValidator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemEncryptorDecryptor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemToolsScreenCaptureStatusWithLabelSwitcher = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemAboutAutoScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainSeparatorAbout = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMainMenuItemAutoScreenCaptureForBeginnersInHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainSeparatorWelcome = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMainMenuItemHelpWelcome = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpChangelog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpQuickStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpGettingStarted = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpWhatsNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpScreens = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpRegions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpEditors = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpSchedules = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpMacroTags = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpTriggers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpCommandLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpCommonSetupScenarios = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpConfigurationFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemHelpDynamicRegexValidator = new System.Windows.Forms.ToolStripMenuItem();
            this.panelModules = new System.Windows.Forms.Panel();
            this.buttonResizeModulesPanel = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.contextMenuStripSystemTrayIcon.SuspendLayout();
            this.tabControlModules.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.panelModules.SuspendLayout();
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
            this.toolStripSpacer,
            this.toolStripCurrentDate,
            this.toolStripCurrentTime,
            this.toolStripInfo,
            this.toolStripCycleCount});
            this.statusStrip.Location = new System.Drawing.Point(0, 710);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(1339, 24);
            this.statusStrip.TabIndex = 3;
            // 
            // toolStripDropDownButtonSetup
            // 
            this.toolStripDropDownButtonSetup.AutoToolTip = false;
            this.toolStripDropDownButtonSetup.BackColor = System.Drawing.Color.White;
            this.toolStripDropDownButtonSetup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemScreenshotsFolder,
            this.toolStripMenuItemFilenamePattern,
            this.toolStripMenuItemImageFormat,
            this.toolStripMenuItemInterval,
            this.toolStripMenuItemOptimizeScreenCapture,
            this.toolStripMenuItemLabels,
            this.toolStripMenuItemActiveWindowTitle,
            this.toolStripMenuItemApplicationFocus,
            this.toolStripMenuItemSecurity,
            this.toolStripMenuItemKeyboardShortcuts});
            this.toolStripDropDownButtonSetup.ForeColor = System.Drawing.Color.Black;
            this.toolStripDropDownButtonSetup.Image = global::AutoScreenCapture.Properties.Resources.setup;
            this.toolStripDropDownButtonSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonSetup.Name = "toolStripDropDownButtonSetup";
            this.toolStripDropDownButtonSetup.Size = new System.Drawing.Size(66, 22);
            this.toolStripDropDownButtonSetup.Text = "Setup";
            // 
            // toolStripMenuItemScreenshotsFolder
            // 
            this.toolStripMenuItemScreenshotsFolder.Name = "toolStripMenuItemScreenshotsFolder";
            this.toolStripMenuItemScreenshotsFolder.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItemScreenshotsFolder.Text = "Screenshots Folder";
            this.toolStripMenuItemScreenshotsFolder.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemFilenamePattern
            // 
            this.toolStripMenuItemFilenamePattern.Name = "toolStripMenuItemFilenamePattern";
            this.toolStripMenuItemFilenamePattern.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItemFilenamePattern.Text = "Filename Pattern";
            this.toolStripMenuItemFilenamePattern.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemImageFormat
            // 
            this.toolStripMenuItemImageFormat.Name = "toolStripMenuItemImageFormat";
            this.toolStripMenuItemImageFormat.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItemImageFormat.Text = "Image Format";
            this.toolStripMenuItemImageFormat.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemInterval
            // 
            this.toolStripMenuItemInterval.Name = "toolStripMenuItemInterval";
            this.toolStripMenuItemInterval.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItemInterval.Text = "Interval";
            this.toolStripMenuItemInterval.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemOptimizeScreenCapture
            // 
            this.toolStripMenuItemOptimizeScreenCapture.Name = "toolStripMenuItemOptimizeScreenCapture";
            this.toolStripMenuItemOptimizeScreenCapture.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItemOptimizeScreenCapture.Text = "Optimize Screen Capture";
            this.toolStripMenuItemOptimizeScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemLabels
            // 
            this.toolStripMenuItemLabels.Name = "toolStripMenuItemLabels";
            this.toolStripMenuItemLabels.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItemLabels.Text = "Labels";
            this.toolStripMenuItemLabels.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemActiveWindowTitle
            // 
            this.toolStripMenuItemActiveWindowTitle.Name = "toolStripMenuItemActiveWindowTitle";
            this.toolStripMenuItemActiveWindowTitle.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItemActiveWindowTitle.Text = "Active Window Title";
            this.toolStripMenuItemActiveWindowTitle.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemApplicationFocus
            // 
            this.toolStripMenuItemApplicationFocus.Name = "toolStripMenuItemApplicationFocus";
            this.toolStripMenuItemApplicationFocus.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItemApplicationFocus.Text = "Application Focus";
            this.toolStripMenuItemApplicationFocus.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemSecurity
            // 
            this.toolStripMenuItemSecurity.Name = "toolStripMenuItemSecurity";
            this.toolStripMenuItemSecurity.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItemSecurity.Text = "Security";
            this.toolStripMenuItemSecurity.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMenuItemKeyboardShortcuts
            // 
            this.toolStripMenuItemKeyboardShortcuts.Name = "toolStripMenuItemKeyboardShortcuts";
            this.toolStripMenuItemKeyboardShortcuts.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItemKeyboardShortcuts.Text = "Keyboard Shortcuts";
            this.toolStripMenuItemKeyboardShortcuts.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
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
            this.toolStripDropDownButtonPreview.Size = new System.Drawing.Size(52, 22);
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
            this.toolStripDropDownButtonStartScreenCapture.Size = new System.Drawing.Size(134, 22);
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
            this.toolStripDropDownButtonStopScreenCapture.Size = new System.Drawing.Size(134, 22);
            this.toolStripDropDownButtonStopScreenCapture.Text = "Stop Screen Capture";
            this.toolStripDropDownButtonStopScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemStopScreenCapture_Click);
            // 
            // toolStripSpacer
            // 
            this.toolStripSpacer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSpacer.Name = "toolStripSpacer";
            this.toolStripSpacer.Size = new System.Drawing.Size(236, 19);
            this.toolStripSpacer.Spring = true;
            // 
            // toolStripCurrentDate
            // 
            this.toolStripCurrentDate.AutoSize = false;
            this.toolStripCurrentDate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)));
            this.toolStripCurrentDate.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripCurrentDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripCurrentDate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripCurrentDate.Name = "toolStripCurrentDate";
            this.toolStripCurrentDate.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripCurrentDate.Size = new System.Drawing.Size(200, 19);
            this.toolStripCurrentDate.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // toolStripCurrentTime
            // 
            this.toolStripCurrentTime.AutoSize = false;
            this.toolStripCurrentTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)));
            this.toolStripCurrentTime.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripCurrentTime.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripCurrentTime.Name = "toolStripCurrentTime";
            this.toolStripCurrentTime.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripCurrentTime.Size = new System.Drawing.Size(100, 19);
            this.toolStripCurrentTime.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
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
            this.toolStripInfo.Size = new System.Drawing.Size(350, 19);
            this.toolStripInfo.Text = "Ready";
            this.toolStripInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // toolStripCycleCount
            // 
            this.toolStripCycleCount.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)));
            this.toolStripCycleCount.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripCycleCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripCycleCount.Name = "toolStripCycleCount";
            this.toolStripCycleCount.Size = new System.Drawing.Size(52, 19);
            this.toolStripCycleCount.Text = "Cycle: 0";
            this.toolStripCycleCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripMenuItemEmailSettings
            // 
            this.toolStripMenuItemEmailSettings.Image = global::AutoScreenCapture.Properties.Resources.email;
            this.toolStripMenuItemEmailSettings.Name = "toolStripMenuItemEmailSettings";
            this.toolStripMenuItemEmailSettings.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuItemEmailSettings.Text = "Email Settings";
            this.toolStripMenuItemEmailSettings.Visible = false;
            this.toolStripMenuItemEmailSettings.Click += new System.EventHandler(this.emailSettings_Click);
            // 
            // toolStripMenuItemFileTransferSettings
            // 
            this.toolStripMenuItemFileTransferSettings.Image = global::AutoScreenCapture.Properties.Resources.file_transfer;
            this.toolStripMenuItemFileTransferSettings.Name = "toolStripMenuItemFileTransferSettings";
            this.toolStripMenuItemFileTransferSettings.Size = new System.Drawing.Size(182, 22);
            this.toolStripMenuItemFileTransferSettings.Text = "File Transfer Settings";
            this.toolStripMenuItemFileTransferSettings.Visible = false;
            this.toolStripMenuItemFileTransferSettings.Click += new System.EventHandler(this.fileTransferSettings_Click);
            // 
            // toolStripDropDownButtonEmailSettings
            // 
            this.toolStripDropDownButtonEmailSettings.Name = "toolStripDropDownButtonEmailSettings";
            this.toolStripDropDownButtonEmailSettings.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripDropDownButtonFileTransferSettings
            // 
            this.toolStripDropDownButtonFileTransferSettings.Name = "toolStripDropDownButtonFileTransferSettings";
            this.toolStripDropDownButtonFileTransferSettings.Size = new System.Drawing.Size(23, 23);
            // 
            // tabControlViews
            // 
            this.tabControlViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlViews.Location = new System.Drawing.Point(251, 56);
            this.tabControlViews.Name = "tabControlViews";
            this.tabControlViews.SelectedIndex = 0;
            this.tabControlViews.Size = new System.Drawing.Size(796, 653);
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
            this.listBoxScreenshots.Location = new System.Drawing.Point(0, 219);
            this.listBoxScreenshots.Name = "listBoxScreenshots";
            this.listBoxScreenshots.ScrollAlwaysVisible = true;
            this.listBoxScreenshots.Size = new System.Drawing.Size(249, 490);
            this.listBoxScreenshots.TabIndex = 0;
            this.listBoxScreenshots.TabStop = false;
            this.listBoxScreenshots.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged_listBoxScreenshots);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripSystemTrayIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStripSystemTrayIcon
            // 
            this.contextMenuStripSystemTrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAbout,
            this.toolStripSeparatorAbout,
            this.toolStripRootMenuItemCommandDeck,
            this.toolStripMenuItemShowHideInterface,
            this.toolStripSeparatorInterface,
            this.toolStripMenuItemStartScreenCapture,
            this.toolStripMenuItemStopScreenCapture,
            this.toolStripMenuItemScreenCaptureStatus,
            this.toolStripMenuItemOpenProgramFolder,
            this.toolStripMenuItemCommandLine,
            this.toolStripSeparatorScreenCapture,
            this.toolStripMenuItemAdd,
            this.toolStripMenuItemConfigure,
            this.toolStripMenuItemSettings,
            this.toolStripMenuItemTools,
            this.toolStripSeparatorTools,
            this.toolStripMenuItemCaptureNowOptions,
            this.toolStripMenuItemCaptureNowArchive,
            this.toolStripMenuItemCaptureNowEdit,
            this.toolStripSeparatorCaptureNow,
            this.toolStripMenuItemRegionSelect,
            this.toolStripSeparatorRegionSelect,
            this.toolStripMenuItemApplyLabel,
            this.toolStripSeparatorApplyLabel,
            this.toolStripMenuItemHelp,
            this.toolStripMenuItemExit});
            this.contextMenuStripSystemTrayIcon.Name = "contextMenuStrip";
            this.contextMenuStripSystemTrayIcon.Size = new System.Drawing.Size(220, 464);
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
            // toolStripRootMenuItemCommandDeck
            // 
            this.toolStripRootMenuItemCommandDeck.Name = "toolStripRootMenuItemCommandDeck";
            this.toolStripRootMenuItemCommandDeck.Size = new System.Drawing.Size(219, 22);
            this.toolStripRootMenuItemCommandDeck.Text = "Command Deck";
            this.toolStripRootMenuItemCommandDeck.Click += new System.EventHandler(this.toolStripMenuItemCommandDeck_Click);
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
            // toolStripMenuItemScreenCaptureStatus
            // 
            this.toolStripMenuItemScreenCaptureStatus.Name = "toolStripMenuItemScreenCaptureStatus";
            this.toolStripMenuItemScreenCaptureStatus.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemScreenCaptureStatus.Text = "Screen Capture Status";
            this.toolStripMenuItemScreenCaptureStatus.Click += new System.EventHandler(this.toolStripMenuItemScreenCaptureStatus_Click);
            // 
            // toolStripMenuItemOpenProgramFolder
            // 
            this.toolStripMenuItemOpenProgramFolder.Image = global::AutoScreenCapture.Properties.Resources.openfolder;
            this.toolStripMenuItemOpenProgramFolder.Name = "toolStripMenuItemOpenProgramFolder";
            this.toolStripMenuItemOpenProgramFolder.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemOpenProgramFolder.Text = "Open Program Folder";
            this.toolStripMenuItemOpenProgramFolder.Click += new System.EventHandler(this.toolStripMenuItemOpenProgramFolder_Click);
            // 
            // toolStripMenuItemCommandLine
            // 
            this.toolStripMenuItemCommandLine.Image = global::AutoScreenCapture.Properties.Resources.command_line;
            this.toolStripMenuItemCommandLine.Name = "toolStripMenuItemCommandLine";
            this.toolStripMenuItemCommandLine.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemCommandLine.Text = "Command Line";
            this.toolStripMenuItemCommandLine.Click += new System.EventHandler(this.toolStripDropDownButtonCommandLine_Click);
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
            // toolStripMenuItemConfigure
            // 
            this.toolStripMenuItemConfigure.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemChangeScreen,
            this.toolStripMenuItemChangeRegion,
            this.toolStripMenuItemChangeEditor,
            this.toolStripMenuItemChangeSchedule,
            this.toolStripMenuItemChangeMacroTag,
            this.toolStripMenuItemChangeTrigger});
            this.toolStripMenuItemConfigure.Name = "toolStripMenuItemConfigure";
            this.toolStripMenuItemConfigure.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemConfigure.Text = "Configure";
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
            // toolStripMenuItemTools
            // 
            this.toolStripMenuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAutoScreenCaptureForBeginners,
            this.toolStripMenuItemCommandDeck,
            this.toolStripMenuItemDynamicRegexValidator,
            this.toolStripMenuItemEncryptorDecryptor,
            this.toolStripMenuItemLabelSwitcher});
            this.toolStripMenuItemTools.Image = global::AutoScreenCapture.Properties.Resources.tools;
            this.toolStripMenuItemTools.Name = "toolStripMenuItemTools";
            this.toolStripMenuItemTools.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemTools.Text = "Tools";
            // 
            // toolStripMenuItemAutoScreenCaptureForBeginners
            // 
            this.toolStripMenuItemAutoScreenCaptureForBeginners.Name = "toolStripMenuItemAutoScreenCaptureForBeginners";
            this.toolStripMenuItemAutoScreenCaptureForBeginners.Size = new System.Drawing.Size(296, 22);
            this.toolStripMenuItemAutoScreenCaptureForBeginners.Text = "Auto Screen Capture For Beginners";
            this.toolStripMenuItemAutoScreenCaptureForBeginners.Click += new System.EventHandler(this.toolStripMenuItemAutoScreenCaptureForBeginners_Click);
            // 
            // toolStripMenuItemCommandDeck
            // 
            this.toolStripMenuItemCommandDeck.Name = "toolStripMenuItemCommandDeck";
            this.toolStripMenuItemCommandDeck.Size = new System.Drawing.Size(296, 22);
            this.toolStripMenuItemCommandDeck.Text = "Command Deck";
            this.toolStripMenuItemCommandDeck.Click += new System.EventHandler(this.toolStripMenuItemCommandDeck_Click);
            // 
            // toolStripMenuItemDynamicRegexValidator
            // 
            this.toolStripMenuItemDynamicRegexValidator.Name = "toolStripMenuItemDynamicRegexValidator";
            this.toolStripMenuItemDynamicRegexValidator.Size = new System.Drawing.Size(296, 22);
            this.toolStripMenuItemDynamicRegexValidator.Text = "Dynamic Regex Validator";
            this.toolStripMenuItemDynamicRegexValidator.Click += new System.EventHandler(this.toolStripMenuItemDynamicRegexValidator_Click);
            // 
            // toolStripMenuItemEncryptorDecryptor
            // 
            this.toolStripMenuItemEncryptorDecryptor.Name = "toolStripMenuItemEncryptorDecryptor";
            this.toolStripMenuItemEncryptorDecryptor.Size = new System.Drawing.Size(296, 22);
            this.toolStripMenuItemEncryptorDecryptor.Text = "Encryptor / Decryptor";
            this.toolStripMenuItemEncryptorDecryptor.Click += new System.EventHandler(this.toolStripMenuItemEncryptorDecryptor_Click);
            // 
            // toolStripMenuItemLabelSwitcher
            // 
            this.toolStripMenuItemLabelSwitcher.Name = "toolStripMenuItemLabelSwitcher";
            this.toolStripMenuItemLabelSwitcher.Size = new System.Drawing.Size(296, 22);
            this.toolStripMenuItemLabelSwitcher.Text = "Screen Capture Status With Label Switcher";
            this.toolStripMenuItemLabelSwitcher.Click += new System.EventHandler(this.toolStripMenuItemLabelSwitcher_Click);
            // 
            // toolStripSeparatorTools
            // 
            this.toolStripSeparatorTools.Name = "toolStripSeparatorTools";
            this.toolStripSeparatorTools.Size = new System.Drawing.Size(216, 6);
            // 
            // toolStripMenuItemCaptureNowOptions
            // 
            this.toolStripMenuItemCaptureNowOptions.Name = "toolStripMenuItemCaptureNowOptions";
            this.toolStripMenuItemCaptureNowOptions.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemCaptureNowOptions.Text = "Capture Now Options ...";
            this.toolStripMenuItemCaptureNowOptions.Click += new System.EventHandler(this.toolStripMenuItemCaptureNowOptions_Click);
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
            this.toolStripMenuItemRegionSelectCommandDeck,
            this.toolStripSeparatorRegionSelectOptions,
            this.toolStripMenuItemRegionSelectClipboard,
            this.toolStripMenuItemRegionSelectClipboardAutoSave,
            this.toolStripMenuItemRegionSelectClipboardAutoSaveEdit,
            this.toolStripMenuItemRegionSelectClipboardFloatingScreenshot,
            this.toolStripMenuItemRegionSelectFloatingScreenshot,
            this.toolStripMenuItemRegionSelectAddRegion,
            this.toolStripMenuItemRegionSelectAddRegionExpress});
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
            // toolStripMenuItemRegionSelectCommandDeck
            // 
            this.toolStripMenuItemRegionSelectCommandDeck.Name = "toolStripMenuItemRegionSelectCommandDeck";
            this.toolStripMenuItemRegionSelectCommandDeck.Size = new System.Drawing.Size(241, 22);
            this.toolStripMenuItemRegionSelectCommandDeck.Text = "Command Deck";
            this.toolStripMenuItemRegionSelectCommandDeck.Click += new System.EventHandler(this.toolStripMenuItemCommandDeck_Click);
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
            // toolStripMenuItemRegionSelectClipboardFloatingScreenshot
            // 
            this.toolStripMenuItemRegionSelectClipboardFloatingScreenshot.Name = "toolStripMenuItemRegionSelectClipboardFloatingScreenshot";
            this.toolStripMenuItemRegionSelectClipboardFloatingScreenshot.Size = new System.Drawing.Size(241, 22);
            this.toolStripMenuItemRegionSelectClipboardFloatingScreenshot.Text = "Clipboard / Floating Screenshot";
            this.toolStripMenuItemRegionSelectClipboardFloatingScreenshot.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectClipboardFloatingScreenshot_Click);
            // 
            // toolStripMenuItemRegionSelectFloatingScreenshot
            // 
            this.toolStripMenuItemRegionSelectFloatingScreenshot.Name = "toolStripMenuItemRegionSelectFloatingScreenshot";
            this.toolStripMenuItemRegionSelectFloatingScreenshot.Size = new System.Drawing.Size(241, 22);
            this.toolStripMenuItemRegionSelectFloatingScreenshot.Text = "Floating Screenshot";
            this.toolStripMenuItemRegionSelectFloatingScreenshot.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectFloatingScreenshot_Click);
            // 
            // toolStripMenuItemRegionSelectAddRegion
            // 
            this.toolStripMenuItemRegionSelectAddRegion.Name = "toolStripMenuItemRegionSelectAddRegion";
            this.toolStripMenuItemRegionSelectAddRegion.Size = new System.Drawing.Size(241, 22);
            this.toolStripMenuItemRegionSelectAddRegion.Text = "Add Region";
            this.toolStripMenuItemRegionSelectAddRegion.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectAddRegion_Click);
            // 
            // toolStripMenuItemRegionSelectAddRegionExpress
            // 
            this.toolStripMenuItemRegionSelectAddRegionExpress.Name = "toolStripMenuItemRegionSelectAddRegionExpress";
            this.toolStripMenuItemRegionSelectAddRegionExpress.Size = new System.Drawing.Size(241, 22);
            this.toolStripMenuItemRegionSelectAddRegionExpress.Text = "Add Region (Express)";
            this.toolStripMenuItemRegionSelectAddRegionExpress.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectAddRegionExpress_Click);
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
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.Image = global::AutoScreenCapture.Properties.Resources.help;
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemHelp.Text = "Help";
            this.toolStripMenuItemHelp.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
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
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlModules.Controls.Add(this.tabPageScreens);
            this.tabControlModules.Controls.Add(this.tabPageRegions);
            this.tabControlModules.Controls.Add(this.tabPageEditors);
            this.tabControlModules.Controls.Add(this.tabPageSchedules);
            this.tabControlModules.Controls.Add(this.tabPageMacroTags);
            this.tabControlModules.Controls.Add(this.tabPageTriggers);
            this.tabControlModules.Location = new System.Drawing.Point(3, 71);
            this.tabControlModules.Multiline = true;
            this.tabControlModules.Name = "tabControlModules";
            this.tabControlModules.SelectedIndex = 0;
            this.tabControlModules.Size = new System.Drawing.Size(264, 600);
            this.tabControlModules.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControlModules.TabIndex = 0;
            this.tabControlModules.TabStop = false;
            this.tabControlModules.SelectedIndexChanged += new System.EventHandler(this.tabControlModules_SelectedIndexChanged);
            // 
            // tabPageScreens
            // 
            this.tabPageScreens.AutoScroll = true;
            this.tabPageScreens.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.tabPageScreens.Location = new System.Drawing.Point(4, 40);
            this.tabPageScreens.Name = "tabPageScreens";
            this.tabPageScreens.Size = new System.Drawing.Size(256, 556);
            this.tabPageScreens.TabIndex = 5;
            this.tabPageScreens.Text = "Screens";
            this.tabPageScreens.UseVisualStyleBackColor = true;
            // 
            // tabPageRegions
            // 
            this.tabPageRegions.AutoScroll = true;
            this.tabPageRegions.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.tabPageRegions.Location = new System.Drawing.Point(4, 40);
            this.tabPageRegions.Name = "tabPageRegions";
            this.tabPageRegions.Size = new System.Drawing.Size(256, 556);
            this.tabPageRegions.TabIndex = 4;
            this.tabPageRegions.Text = "Regions";
            this.tabPageRegions.UseVisualStyleBackColor = true;
            // 
            // tabPageEditors
            // 
            this.tabPageEditors.AutoScroll = true;
            this.tabPageEditors.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.tabPageEditors.Location = new System.Drawing.Point(4, 40);
            this.tabPageEditors.Name = "tabPageEditors";
            this.tabPageEditors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEditors.Size = new System.Drawing.Size(256, 556);
            this.tabPageEditors.TabIndex = 2;
            this.tabPageEditors.Text = "Editors";
            this.tabPageEditors.UseVisualStyleBackColor = true;
            // 
            // tabPageSchedules
            // 
            this.tabPageSchedules.AutoScroll = true;
            this.tabPageSchedules.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.tabPageSchedules.Location = new System.Drawing.Point(4, 40);
            this.tabPageSchedules.Name = "tabPageSchedules";
            this.tabPageSchedules.Size = new System.Drawing.Size(256, 556);
            this.tabPageSchedules.TabIndex = 8;
            this.tabPageSchedules.Text = "Schedules";
            this.tabPageSchedules.UseVisualStyleBackColor = true;
            // 
            // tabPageMacroTags
            // 
            this.tabPageMacroTags.AutoScroll = true;
            this.tabPageMacroTags.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.tabPageMacroTags.Location = new System.Drawing.Point(4, 40);
            this.tabPageMacroTags.Name = "tabPageMacroTags";
            this.tabPageMacroTags.Size = new System.Drawing.Size(256, 556);
            this.tabPageMacroTags.TabIndex = 7;
            this.tabPageMacroTags.Text = "Macro Tags";
            this.tabPageMacroTags.UseVisualStyleBackColor = true;
            // 
            // tabPageTriggers
            // 
            this.tabPageTriggers.AutoScroll = true;
            this.tabPageTriggers.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.tabPageTriggers.Location = new System.Drawing.Point(4, 40);
            this.tabPageTriggers.Name = "tabPageTriggers";
            this.tabPageTriggers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTriggers.Size = new System.Drawing.Size(256, 556);
            this.tabPageTriggers.TabIndex = 3;
            this.tabPageTriggers.Text = "Triggers";
            this.tabPageTriggers.UseVisualStyleBackColor = true;
            // 
            // timerHelpTips
            // 
            this.timerHelpTips.Enabled = true;
            this.timerHelpTips.Interval = 1000;
            this.timerHelpTips.Tick += new System.EventHandler(this.timerHelpTip_Tick);
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
            this.labelFilter.ForeColor = System.Drawing.SystemColors.ControlText;
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
            this.comboBoxFilterType.Location = new System.Drawing.Point(32, 30);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(95, 21);
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
            this.labelHelp.Location = new System.Drawing.Point(254, 31);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(793, 17);
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
            // labelModuleHelp
            // 
            this.labelModuleHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelModuleHelp.AutoEllipsis = true;
            this.labelModuleHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelModuleHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelModuleHelp.Location = new System.Drawing.Point(3, 1);
            this.labelModuleHelp.Name = "labelModuleHelp";
            this.labelModuleHelp.Size = new System.Drawing.Size(264, 67);
            this.labelModuleHelp.TabIndex = 4;
            this.labelModuleHelp.Text = "Screens are listed here. Each screen represents a display. Have the Preview butto" +
    "n enabled while viewing the Dashboard to see what would be captured when startin" +
    "g a screen capture session.";
            // 
            // timerParseCommands
            // 
            this.timerParseCommands.Enabled = true;
            this.timerParseCommands.Interval = 1000;
            this.timerParseCommands.Tick += new System.EventHandler(this.timerParseCommands_Tick);
            // 
            // timerScheduleCheck
            // 
            this.timerScheduleCheck.Interval = 60000;
            this.timerScheduleCheck.Tick += new System.EventHandler(this.timerScheduleCheck_Tick);
            // 
            // timerTriggerCheck
            // 
            this.timerTriggerCheck.Enabled = true;
            this.timerTriggerCheck.Interval = 1000;
            this.timerTriggerCheck.Tick += new System.EventHandler(this.timerTriggerCheck_Tick);
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMainMenuItemFile,
            this.toolStripMainMenuItemSetup,
            this.toolStripMainMenuItemControl,
            this.toolStripMainMenuItemRegionSelect,
            this.toolStripMainMenuItemScreens,
            this.toolStripMainMenuItemRegions,
            this.toolStripMainMenuItemEditors,
            this.toolStripMainMenuItemSchedules,
            this.toolStripMainMenuItemMacroTags,
            this.toolStripMainMenuItemTriggers,
            this.toolStripMainMenuItemTools,
            this.toolStripMainMenuItemHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1339, 24);
            this.menuStripMain.TabIndex = 5;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // toolStripMainMenuItemFile
            // 
            this.toolStripMainMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMainMenuItemOpenProgramFolder,
            this.toolStripMainMenuItemEmailSettings,
            this.toolStripMainMenuItemFileTransferSettings,
            this.toolStripSeparatorExit,
            this.toolStripMainMenuItemExit});
            this.toolStripMainMenuItemFile.Name = "toolStripMainMenuItemFile";
            this.toolStripMainMenuItemFile.Size = new System.Drawing.Size(37, 20);
            this.toolStripMainMenuItemFile.Text = "File";
            // 
            // toolStripMainMenuItemOpenProgramFolder
            // 
            this.toolStripMainMenuItemOpenProgramFolder.Image = global::AutoScreenCapture.Properties.Resources.openfolder;
            this.toolStripMainMenuItemOpenProgramFolder.Name = "toolStripMainMenuItemOpenProgramFolder";
            this.toolStripMainMenuItemOpenProgramFolder.Size = new System.Drawing.Size(188, 22);
            this.toolStripMainMenuItemOpenProgramFolder.Text = "Open Program Folder";
            this.toolStripMainMenuItemOpenProgramFolder.Click += new System.EventHandler(this.toolStripMenuItemOpenProgramFolder_Click);
            // 
            // toolStripMainMenuItemEmailSettings
            // 
            this.toolStripMainMenuItemEmailSettings.Image = global::AutoScreenCapture.Properties.Resources.email;
            this.toolStripMainMenuItemEmailSettings.Name = "toolStripMainMenuItemEmailSettings";
            this.toolStripMainMenuItemEmailSettings.Size = new System.Drawing.Size(188, 22);
            this.toolStripMainMenuItemEmailSettings.Text = "Email Settings";
            this.toolStripMainMenuItemEmailSettings.Click += new System.EventHandler(this.emailSettings_Click);
            // 
            // toolStripMainMenuItemFileTransferSettings
            // 
            this.toolStripMainMenuItemFileTransferSettings.Image = global::AutoScreenCapture.Properties.Resources.file_transfer;
            this.toolStripMainMenuItemFileTransferSettings.Name = "toolStripMainMenuItemFileTransferSettings";
            this.toolStripMainMenuItemFileTransferSettings.Size = new System.Drawing.Size(188, 22);
            this.toolStripMainMenuItemFileTransferSettings.Text = "File Transfer Settings";
            this.toolStripMainMenuItemFileTransferSettings.Click += new System.EventHandler(this.fileTransferSettings_Click);
            // 
            // toolStripSeparatorExit
            // 
            this.toolStripSeparatorExit.Name = "toolStripSeparatorExit";
            this.toolStripSeparatorExit.Size = new System.Drawing.Size(185, 6);
            // 
            // toolStripMainMenuItemExit
            // 
            this.toolStripMainMenuItemExit.Image = global::AutoScreenCapture.Properties.Resources.exit;
            this.toolStripMainMenuItemExit.Name = "toolStripMainMenuItemExit";
            this.toolStripMainMenuItemExit.Size = new System.Drawing.Size(188, 22);
            this.toolStripMainMenuItemExit.Text = "Exit";
            this.toolStripMainMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // toolStripMainMenuItemSetup
            // 
            this.toolStripMainMenuItemSetup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMainMenuItemScreenshotsFolder,
            this.toolStripMainMenuItemFilenamePattern,
            this.toolStripMainMenuItemImageFormat,
            this.toolStripMainMenuItemInterval,
            this.toolStripMainMenuItemOptimizeScreenCapture,
            this.toolStripMainMenuItemLabels,
            this.toolStripMainMenuItemActiveWindowTitle,
            this.toolStripMainMenuItemApplicationFocus,
            this.toolStripMainMenuItemSecurity,
            this.toolStripMainMenuItemKeyboardShortcuts});
            this.toolStripMainMenuItemSetup.Name = "toolStripMainMenuItemSetup";
            this.toolStripMainMenuItemSetup.Size = new System.Drawing.Size(49, 20);
            this.toolStripMainMenuItemSetup.Text = "Setup";
            // 
            // toolStripMainMenuItemScreenshotsFolder
            // 
            this.toolStripMainMenuItemScreenshotsFolder.Name = "toolStripMainMenuItemScreenshotsFolder";
            this.toolStripMainMenuItemScreenshotsFolder.Size = new System.Drawing.Size(205, 22);
            this.toolStripMainMenuItemScreenshotsFolder.Text = "Screenshots Folder";
            this.toolStripMainMenuItemScreenshotsFolder.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMainMenuItemFilenamePattern
            // 
            this.toolStripMainMenuItemFilenamePattern.Name = "toolStripMainMenuItemFilenamePattern";
            this.toolStripMainMenuItemFilenamePattern.Size = new System.Drawing.Size(205, 22);
            this.toolStripMainMenuItemFilenamePattern.Text = "Filename Pattern";
            this.toolStripMainMenuItemFilenamePattern.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMainMenuItemImageFormat
            // 
            this.toolStripMainMenuItemImageFormat.Name = "toolStripMainMenuItemImageFormat";
            this.toolStripMainMenuItemImageFormat.Size = new System.Drawing.Size(205, 22);
            this.toolStripMainMenuItemImageFormat.Text = "Image Format";
            this.toolStripMainMenuItemImageFormat.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMainMenuItemInterval
            // 
            this.toolStripMainMenuItemInterval.Name = "toolStripMainMenuItemInterval";
            this.toolStripMainMenuItemInterval.Size = new System.Drawing.Size(205, 22);
            this.toolStripMainMenuItemInterval.Text = "Interval";
            this.toolStripMainMenuItemInterval.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMainMenuItemOptimizeScreenCapture
            // 
            this.toolStripMainMenuItemOptimizeScreenCapture.Name = "toolStripMainMenuItemOptimizeScreenCapture";
            this.toolStripMainMenuItemOptimizeScreenCapture.Size = new System.Drawing.Size(205, 22);
            this.toolStripMainMenuItemOptimizeScreenCapture.Text = "Optimize Screen Capture";
            this.toolStripMainMenuItemOptimizeScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMainMenuItemLabels
            // 
            this.toolStripMainMenuItemLabels.Name = "toolStripMainMenuItemLabels";
            this.toolStripMainMenuItemLabels.Size = new System.Drawing.Size(205, 22);
            this.toolStripMainMenuItemLabels.Text = "Labels";
            this.toolStripMainMenuItemLabels.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMainMenuItemActiveWindowTitle
            // 
            this.toolStripMainMenuItemActiveWindowTitle.Name = "toolStripMainMenuItemActiveWindowTitle";
            this.toolStripMainMenuItemActiveWindowTitle.Size = new System.Drawing.Size(205, 22);
            this.toolStripMainMenuItemActiveWindowTitle.Text = "Active Window Title";
            this.toolStripMainMenuItemActiveWindowTitle.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMainMenuItemApplicationFocus
            // 
            this.toolStripMainMenuItemApplicationFocus.Name = "toolStripMainMenuItemApplicationFocus";
            this.toolStripMainMenuItemApplicationFocus.Size = new System.Drawing.Size(205, 22);
            this.toolStripMainMenuItemApplicationFocus.Text = "Application Focus";
            this.toolStripMainMenuItemApplicationFocus.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMainMenuItemSecurity
            // 
            this.toolStripMainMenuItemSecurity.Name = "toolStripMainMenuItemSecurity";
            this.toolStripMainMenuItemSecurity.Size = new System.Drawing.Size(205, 22);
            this.toolStripMainMenuItemSecurity.Text = "Security";
            this.toolStripMainMenuItemSecurity.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMainMenuItemKeyboardShortcuts
            // 
            this.toolStripMainMenuItemKeyboardShortcuts.Name = "toolStripMainMenuItemKeyboardShortcuts";
            this.toolStripMainMenuItemKeyboardShortcuts.Size = new System.Drawing.Size(205, 22);
            this.toolStripMainMenuItemKeyboardShortcuts.Text = "Keyboard Shortcuts";
            this.toolStripMainMenuItemKeyboardShortcuts.Click += new System.EventHandler(this.toolStripMenuItemSetup_Click);
            // 
            // toolStripMainMenuItemControl
            // 
            this.toolStripMainMenuItemControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMainMenuItemStartScreenCapture,
            this.toolStripMainMenuItemStopScreenCapture,
            this.toolStripMainMenuItemControlCommandDeck,
            this.toolStripMainMenuItemScreenCaptureStatus,
            this.toolStripMainMenuItemScreenCaptureStatusWithLabelSwitcher});
            this.toolStripMainMenuItemControl.Name = "toolStripMainMenuItemControl";
            this.toolStripMainMenuItemControl.Size = new System.Drawing.Size(59, 20);
            this.toolStripMainMenuItemControl.Text = "Control";
            // 
            // toolStripMainMenuItemStartScreenCapture
            // 
            this.toolStripMainMenuItemStartScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.start_screen_capture;
            this.toolStripMainMenuItemStartScreenCapture.Name = "toolStripMainMenuItemStartScreenCapture";
            this.toolStripMainMenuItemStartScreenCapture.Size = new System.Drawing.Size(296, 22);
            this.toolStripMainMenuItemStartScreenCapture.Text = "Start Screen Capture";
            this.toolStripMainMenuItemStartScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemStartScreenCapture_Click);
            // 
            // toolStripMainMenuItemStopScreenCapture
            // 
            this.toolStripMainMenuItemStopScreenCapture.Enabled = false;
            this.toolStripMainMenuItemStopScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.stop_screen_capture;
            this.toolStripMainMenuItemStopScreenCapture.Name = "toolStripMainMenuItemStopScreenCapture";
            this.toolStripMainMenuItemStopScreenCapture.Size = new System.Drawing.Size(296, 22);
            this.toolStripMainMenuItemStopScreenCapture.Text = "Stop Screen Capture";
            this.toolStripMainMenuItemStopScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemStopScreenCapture_Click);
            // 
            // toolStripMainMenuItemControlCommandDeck
            // 
            this.toolStripMainMenuItemControlCommandDeck.Name = "toolStripMainMenuItemControlCommandDeck";
            this.toolStripMainMenuItemControlCommandDeck.Size = new System.Drawing.Size(296, 22);
            this.toolStripMainMenuItemControlCommandDeck.Text = "Command Deck";
            this.toolStripMainMenuItemControlCommandDeck.Click += new System.EventHandler(this.toolStripMenuItemCommandDeck_Click);
            // 
            // toolStripMainMenuItemScreenCaptureStatus
            // 
            this.toolStripMainMenuItemScreenCaptureStatus.Name = "toolStripMainMenuItemScreenCaptureStatus";
            this.toolStripMainMenuItemScreenCaptureStatus.Size = new System.Drawing.Size(296, 22);
            this.toolStripMainMenuItemScreenCaptureStatus.Text = "Screen Capture Status";
            this.toolStripMainMenuItemScreenCaptureStatus.Click += new System.EventHandler(this.toolStripMenuItemScreenCaptureStatus_Click);
            // 
            // toolStripMainMenuItemScreenCaptureStatusWithLabelSwitcher
            // 
            this.toolStripMainMenuItemScreenCaptureStatusWithLabelSwitcher.Name = "toolStripMainMenuItemScreenCaptureStatusWithLabelSwitcher";
            this.toolStripMainMenuItemScreenCaptureStatusWithLabelSwitcher.Size = new System.Drawing.Size(296, 22);
            this.toolStripMainMenuItemScreenCaptureStatusWithLabelSwitcher.Text = "Screen Capture Status With Label Switcher";
            this.toolStripMainMenuItemScreenCaptureStatusWithLabelSwitcher.Click += new System.EventHandler(this.toolStripMenuItemLabelSwitcher_Click);
            // 
            // toolStripMainMenuItemRegionSelect
            // 
            this.toolStripMainMenuItemRegionSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMainMenuItemRegionSelectOptions,
            this.toolStripMainMenuItemCommandDeck,
            this.toolStripSeparatorMainRegionSelect,
            this.toolStripMainMenuItemRegionSelectClipboard,
            this.toolStripMainMenuItemRegionSelectClipboardAutoSave,
            this.toolStripMainMenuItemRegionSelectClipboardAutoSaveEdit,
            this.toolStripMainMenuItemRegionSelectClipboardFloatingScreenshot,
            this.toolStripMainMenuItemRegionSelectFloatingScreenshot,
            this.toolStripMainMenuItemRegionSelectAddRegion,
            this.toolStripMainMenuItemRegionSelectAddRegionExpress});
            this.toolStripMainMenuItemRegionSelect.Name = "toolStripMainMenuItemRegionSelect";
            this.toolStripMainMenuItemRegionSelect.Size = new System.Drawing.Size(90, 20);
            this.toolStripMainMenuItemRegionSelect.Text = "Region Select";
            // 
            // toolStripMainMenuItemRegionSelectOptions
            // 
            this.toolStripMainMenuItemRegionSelectOptions.Name = "toolStripMainMenuItemRegionSelectOptions";
            this.toolStripMainMenuItemRegionSelectOptions.Size = new System.Drawing.Size(241, 22);
            this.toolStripMainMenuItemRegionSelectOptions.Text = "Region Select Options ...";
            this.toolStripMainMenuItemRegionSelectOptions.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectOptions_Click);
            // 
            // toolStripMainMenuItemCommandDeck
            // 
            this.toolStripMainMenuItemCommandDeck.Name = "toolStripMainMenuItemCommandDeck";
            this.toolStripMainMenuItemCommandDeck.Size = new System.Drawing.Size(241, 22);
            this.toolStripMainMenuItemCommandDeck.Text = "Command Deck";
            this.toolStripMainMenuItemCommandDeck.Click += new System.EventHandler(this.toolStripMenuItemCommandDeck_Click);
            // 
            // toolStripSeparatorMainRegionSelect
            // 
            this.toolStripSeparatorMainRegionSelect.Name = "toolStripSeparatorMainRegionSelect";
            this.toolStripSeparatorMainRegionSelect.Size = new System.Drawing.Size(238, 6);
            // 
            // toolStripMainMenuItemRegionSelectClipboard
            // 
            this.toolStripMainMenuItemRegionSelectClipboard.Name = "toolStripMainMenuItemRegionSelectClipboard";
            this.toolStripMainMenuItemRegionSelectClipboard.Size = new System.Drawing.Size(241, 22);
            this.toolStripMainMenuItemRegionSelectClipboard.Text = "Clipboard";
            this.toolStripMainMenuItemRegionSelectClipboard.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectClipboard_Click);
            // 
            // toolStripMainMenuItemRegionSelectClipboardAutoSave
            // 
            this.toolStripMainMenuItemRegionSelectClipboardAutoSave.Name = "toolStripMainMenuItemRegionSelectClipboardAutoSave";
            this.toolStripMainMenuItemRegionSelectClipboardAutoSave.Size = new System.Drawing.Size(241, 22);
            this.toolStripMainMenuItemRegionSelectClipboardAutoSave.Text = "Clipboard / Auto Save";
            this.toolStripMainMenuItemRegionSelectClipboardAutoSave.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectClipboardAutoSave_Click);
            // 
            // toolStripMainMenuItemRegionSelectClipboardAutoSaveEdit
            // 
            this.toolStripMainMenuItemRegionSelectClipboardAutoSaveEdit.Name = "toolStripMainMenuItemRegionSelectClipboardAutoSaveEdit";
            this.toolStripMainMenuItemRegionSelectClipboardAutoSaveEdit.Size = new System.Drawing.Size(241, 22);
            this.toolStripMainMenuItemRegionSelectClipboardAutoSaveEdit.Text = "Clipboard / Auto Save / Edit";
            this.toolStripMainMenuItemRegionSelectClipboardAutoSaveEdit.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectClipboardAutoSaveEdit_Click);
            // 
            // toolStripMainMenuItemRegionSelectClipboardFloatingScreenshot
            // 
            this.toolStripMainMenuItemRegionSelectClipboardFloatingScreenshot.Name = "toolStripMainMenuItemRegionSelectClipboardFloatingScreenshot";
            this.toolStripMainMenuItemRegionSelectClipboardFloatingScreenshot.Size = new System.Drawing.Size(241, 22);
            this.toolStripMainMenuItemRegionSelectClipboardFloatingScreenshot.Text = "Clipboard / Floating Screenshot";
            this.toolStripMainMenuItemRegionSelectClipboardFloatingScreenshot.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectClipboardFloatingScreenshot_Click);
            // 
            // toolStripMainMenuItemRegionSelectFloatingScreenshot
            // 
            this.toolStripMainMenuItemRegionSelectFloatingScreenshot.Name = "toolStripMainMenuItemRegionSelectFloatingScreenshot";
            this.toolStripMainMenuItemRegionSelectFloatingScreenshot.Size = new System.Drawing.Size(241, 22);
            this.toolStripMainMenuItemRegionSelectFloatingScreenshot.Text = "Floating Screenshot";
            this.toolStripMainMenuItemRegionSelectFloatingScreenshot.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectFloatingScreenshot_Click);
            // 
            // toolStripMainMenuItemRegionSelectAddRegion
            // 
            this.toolStripMainMenuItemRegionSelectAddRegion.Name = "toolStripMainMenuItemRegionSelectAddRegion";
            this.toolStripMainMenuItemRegionSelectAddRegion.Size = new System.Drawing.Size(241, 22);
            this.toolStripMainMenuItemRegionSelectAddRegion.Text = "Add Region";
            this.toolStripMainMenuItemRegionSelectAddRegion.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectAddRegion_Click);
            // 
            // toolStripMainMenuItemRegionSelectAddRegionExpress
            // 
            this.toolStripMainMenuItemRegionSelectAddRegionExpress.Name = "toolStripMainMenuItemRegionSelectAddRegionExpress";
            this.toolStripMainMenuItemRegionSelectAddRegionExpress.Size = new System.Drawing.Size(241, 22);
            this.toolStripMainMenuItemRegionSelectAddRegionExpress.Text = "Add Region (Express)";
            this.toolStripMainMenuItemRegionSelectAddRegionExpress.Click += new System.EventHandler(this.toolStripMenuItemRegionSelectAddRegionExpress_Click);
            // 
            // toolStripMainMenuItemScreens
            // 
            this.toolStripMainMenuItemScreens.Name = "toolStripMainMenuItemScreens";
            this.toolStripMainMenuItemScreens.Size = new System.Drawing.Size(59, 20);
            this.toolStripMainMenuItemScreens.Text = "Screens";
            // 
            // toolStripMainMenuItemRegions
            // 
            this.toolStripMainMenuItemRegions.Name = "toolStripMainMenuItemRegions";
            this.toolStripMainMenuItemRegions.Size = new System.Drawing.Size(61, 20);
            this.toolStripMainMenuItemRegions.Text = "Regions";
            // 
            // toolStripMainMenuItemEditors
            // 
            this.toolStripMainMenuItemEditors.Name = "toolStripMainMenuItemEditors";
            this.toolStripMainMenuItemEditors.Size = new System.Drawing.Size(55, 20);
            this.toolStripMainMenuItemEditors.Text = "Editors";
            // 
            // toolStripMainMenuItemSchedules
            // 
            this.toolStripMainMenuItemSchedules.Name = "toolStripMainMenuItemSchedules";
            this.toolStripMainMenuItemSchedules.Size = new System.Drawing.Size(72, 20);
            this.toolStripMainMenuItemSchedules.Text = "Schedules";
            // 
            // toolStripMainMenuItemMacroTags
            // 
            this.toolStripMainMenuItemMacroTags.Name = "toolStripMainMenuItemMacroTags";
            this.toolStripMainMenuItemMacroTags.Size = new System.Drawing.Size(80, 20);
            this.toolStripMainMenuItemMacroTags.Text = "Macro Tags";
            // 
            // toolStripMainMenuItemTriggers
            // 
            this.toolStripMainMenuItemTriggers.Name = "toolStripMainMenuItemTriggers";
            this.toolStripMainMenuItemTriggers.Size = new System.Drawing.Size(61, 20);
            this.toolStripMainMenuItemTriggers.Text = "Triggers";
            // 
            // toolStripMainMenuItemTools
            // 
            this.toolStripMainMenuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMainMenuItemAutoScreenCaptureForBeginners,
            this.toolStripMainMenuItemToolsCommandDeck,
            this.toolStripMainMenuItemDynamicRegexValidator,
            this.toolStripMainMenuItemEncryptorDecryptor,
            this.toolStripMainMenuItemToolsScreenCaptureStatusWithLabelSwitcher});
            this.toolStripMainMenuItemTools.Name = "toolStripMainMenuItemTools";
            this.toolStripMainMenuItemTools.Size = new System.Drawing.Size(47, 20);
            this.toolStripMainMenuItemTools.Text = "Tools";
            // 
            // toolStripMainMenuItemAutoScreenCaptureForBeginners
            // 
            this.toolStripMainMenuItemAutoScreenCaptureForBeginners.Name = "toolStripMainMenuItemAutoScreenCaptureForBeginners";
            this.toolStripMainMenuItemAutoScreenCaptureForBeginners.Size = new System.Drawing.Size(296, 22);
            this.toolStripMainMenuItemAutoScreenCaptureForBeginners.Text = "Auto Screen Capture For Beginners";
            this.toolStripMainMenuItemAutoScreenCaptureForBeginners.Click += new System.EventHandler(this.toolStripMenuItemAutoScreenCaptureForBeginners_Click);
            // 
            // toolStripMainMenuItemToolsCommandDeck
            // 
            this.toolStripMainMenuItemToolsCommandDeck.Name = "toolStripMainMenuItemToolsCommandDeck";
            this.toolStripMainMenuItemToolsCommandDeck.Size = new System.Drawing.Size(296, 22);
            this.toolStripMainMenuItemToolsCommandDeck.Text = "Command Deck";
            this.toolStripMainMenuItemToolsCommandDeck.Click += new System.EventHandler(this.toolStripMenuItemCommandDeck_Click);
            // 
            // toolStripMainMenuItemDynamicRegexValidator
            // 
            this.toolStripMainMenuItemDynamicRegexValidator.Name = "toolStripMainMenuItemDynamicRegexValidator";
            this.toolStripMainMenuItemDynamicRegexValidator.Size = new System.Drawing.Size(296, 22);
            this.toolStripMainMenuItemDynamicRegexValidator.Text = "Dynamic Regex Validator";
            this.toolStripMainMenuItemDynamicRegexValidator.Click += new System.EventHandler(this.toolStripMenuItemDynamicRegexValidator_Click);
            // 
            // toolStripMainMenuItemEncryptorDecryptor
            // 
            this.toolStripMainMenuItemEncryptorDecryptor.Name = "toolStripMainMenuItemEncryptorDecryptor";
            this.toolStripMainMenuItemEncryptorDecryptor.Size = new System.Drawing.Size(296, 22);
            this.toolStripMainMenuItemEncryptorDecryptor.Text = "Encryptor / Decryptor";
            this.toolStripMainMenuItemEncryptorDecryptor.Click += new System.EventHandler(this.toolStripMenuItemEncryptorDecryptor_Click);
            // 
            // toolStripMainMenuItemToolsScreenCaptureStatusWithLabelSwitcher
            // 
            this.toolStripMainMenuItemToolsScreenCaptureStatusWithLabelSwitcher.Name = "toolStripMainMenuItemToolsScreenCaptureStatusWithLabelSwitcher";
            this.toolStripMainMenuItemToolsScreenCaptureStatusWithLabelSwitcher.Size = new System.Drawing.Size(296, 22);
            this.toolStripMainMenuItemToolsScreenCaptureStatusWithLabelSwitcher.Text = "Screen Capture Status With Label Switcher";
            this.toolStripMainMenuItemToolsScreenCaptureStatusWithLabelSwitcher.Click += new System.EventHandler(this.toolStripMenuItemLabelSwitcher_Click);
            // 
            // toolStripMainMenuItemHelp
            // 
            this.toolStripMainMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMainMenuItemAboutAutoScreenCapture,
            this.toolStripMainSeparatorAbout,
            this.toolStripMainMenuItemAutoScreenCaptureForBeginnersInHelp,
            this.toolStripMainSeparatorWelcome,
            this.toolStripMainMenuItemHelpWelcome,
            this.toolStripMainMenuItemHelpLicense,
            this.toolStripMainMenuItemHelpChangelog,
            this.toolStripMainMenuItemHelpQuickStart,
            this.toolStripMainMenuItemHelpGettingStarted,
            this.toolStripMainMenuItemHelpWhatsNew,
            this.toolStripMainMenuItemHelpConfigurationFile,
            this.toolStripMainMenuItemHelpScreens,
            this.toolStripMainMenuItemHelpRegions,
            this.toolStripMainMenuItemHelpEditors,
            this.toolStripMainMenuItemHelpSchedules,
            this.toolStripMainMenuItemHelpMacroTags,
            this.toolStripMainMenuItemHelpTriggers,
            this.toolStripMainMenuItemHelpCommandLine,
            this.toolStripMainMenuItemHelpCommonSetupScenarios,
            this.toolStripMainMenuItemHelpDynamicRegexValidator});
            this.toolStripMainMenuItemHelp.Image = global::AutoScreenCapture.Properties.Resources.help;
            this.toolStripMainMenuItemHelp.Name = "toolStripMainMenuItemHelp";
            this.toolStripMainMenuItemHelp.Size = new System.Drawing.Size(60, 20);
            this.toolStripMainMenuItemHelp.Text = "Help";
            // 
            // toolStripMainMenuItemAboutAutoScreenCapture
            // 
            this.toolStripMainMenuItemAboutAutoScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.about;
            this.toolStripMainMenuItemAboutAutoScreenCapture.Name = "toolStripMainMenuItemAboutAutoScreenCapture";
            this.toolStripMainMenuItemAboutAutoScreenCapture.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemAboutAutoScreenCapture.Text = "About Auto Screen Capture";
            this.toolStripMainMenuItemAboutAutoScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // toolStripMainSeparatorAbout
            // 
            this.toolStripMainSeparatorAbout.Name = "toolStripMainSeparatorAbout";
            this.toolStripMainSeparatorAbout.Size = new System.Drawing.Size(255, 6);
            // 
            // toolStripMainMenuItemAutoScreenCaptureForBeginnersInHelp
            // 
            this.toolStripMainMenuItemAutoScreenCaptureForBeginnersInHelp.Name = "toolStripMainMenuItemAutoScreenCaptureForBeginnersInHelp";
            this.toolStripMainMenuItemAutoScreenCaptureForBeginnersInHelp.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemAutoScreenCaptureForBeginnersInHelp.Text = "Auto Screen Capture For Beginners";
            this.toolStripMainMenuItemAutoScreenCaptureForBeginnersInHelp.Click += new System.EventHandler(this.toolStripMenuItemAutoScreenCaptureForBeginners_Click);
            // 
            // toolStripMainSeparatorWelcome
            // 
            this.toolStripMainSeparatorWelcome.Name = "toolStripMainSeparatorWelcome";
            this.toolStripMainSeparatorWelcome.Size = new System.Drawing.Size(255, 6);
            // 
            // toolStripMainMenuItemHelpWelcome
            // 
            this.toolStripMainMenuItemHelpWelcome.Name = "toolStripMainMenuItemHelpWelcome";
            this.toolStripMainMenuItemHelpWelcome.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpWelcome.Text = "Welcome";
            this.toolStripMainMenuItemHelpWelcome.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpLicense
            // 
            this.toolStripMainMenuItemHelpLicense.Name = "toolStripMainMenuItemHelpLicense";
            this.toolStripMainMenuItemHelpLicense.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpLicense.Text = "License";
            this.toolStripMainMenuItemHelpLicense.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpChangelog
            // 
            this.toolStripMainMenuItemHelpChangelog.Name = "toolStripMainMenuItemHelpChangelog";
            this.toolStripMainMenuItemHelpChangelog.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpChangelog.Text = "Changelog";
            this.toolStripMainMenuItemHelpChangelog.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpQuickStart
            // 
            this.toolStripMainMenuItemHelpQuickStart.Name = "toolStripMainMenuItemHelpQuickStart";
            this.toolStripMainMenuItemHelpQuickStart.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpQuickStart.Text = "Quick Start";
            this.toolStripMainMenuItemHelpQuickStart.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpGettingStarted
            // 
            this.toolStripMainMenuItemHelpGettingStarted.Name = "toolStripMainMenuItemHelpGettingStarted";
            this.toolStripMainMenuItemHelpGettingStarted.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpGettingStarted.Text = "Getting Started";
            this.toolStripMainMenuItemHelpGettingStarted.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpWhatsNew
            // 
            this.toolStripMainMenuItemHelpWhatsNew.Name = "toolStripMainMenuItemHelpWhatsNew";
            this.toolStripMainMenuItemHelpWhatsNew.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpWhatsNew.Text = "What\'s New";
            this.toolStripMainMenuItemHelpWhatsNew.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpScreens
            // 
            this.toolStripMainMenuItemHelpScreens.Name = "toolStripMainMenuItemHelpScreens";
            this.toolStripMainMenuItemHelpScreens.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpScreens.Text = "Screens";
            this.toolStripMainMenuItemHelpScreens.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpRegions
            // 
            this.toolStripMainMenuItemHelpRegions.Name = "toolStripMainMenuItemHelpRegions";
            this.toolStripMainMenuItemHelpRegions.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpRegions.Text = "Regions";
            this.toolStripMainMenuItemHelpRegions.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpEditors
            // 
            this.toolStripMainMenuItemHelpEditors.Name = "toolStripMainMenuItemHelpEditors";
            this.toolStripMainMenuItemHelpEditors.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpEditors.Text = "Editors";
            this.toolStripMainMenuItemHelpEditors.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpSchedules
            // 
            this.toolStripMainMenuItemHelpSchedules.Name = "toolStripMainMenuItemHelpSchedules";
            this.toolStripMainMenuItemHelpSchedules.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpSchedules.Text = "Schedules";
            this.toolStripMainMenuItemHelpSchedules.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpMacroTags
            // 
            this.toolStripMainMenuItemHelpMacroTags.Name = "toolStripMainMenuItemHelpMacroTags";
            this.toolStripMainMenuItemHelpMacroTags.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpMacroTags.Text = "Macro Tags";
            this.toolStripMainMenuItemHelpMacroTags.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpTriggers
            // 
            this.toolStripMainMenuItemHelpTriggers.Name = "toolStripMainMenuItemHelpTriggers";
            this.toolStripMainMenuItemHelpTriggers.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpTriggers.Text = "Triggers";
            this.toolStripMainMenuItemHelpTriggers.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpCommandLine
            // 
            this.toolStripMainMenuItemHelpCommandLine.Name = "toolStripMainMenuItemHelpCommandLine";
            this.toolStripMainMenuItemHelpCommandLine.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpCommandLine.Text = "Command Line";
            this.toolStripMainMenuItemHelpCommandLine.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpCommonSetupScenarios
            // 
            this.toolStripMainMenuItemHelpCommonSetupScenarios.Name = "toolStripMainMenuItemHelpCommonSetupScenarios";
            this.toolStripMainMenuItemHelpCommonSetupScenarios.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpCommonSetupScenarios.Text = "Common Setup Scenarios";
            this.toolStripMainMenuItemHelpCommonSetupScenarios.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpConfigurationFile
            // 
            this.toolStripMainMenuItemHelpConfigurationFile.Name = "toolStripMainMenuItemHelpConfigurationFile";
            this.toolStripMainMenuItemHelpConfigurationFile.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpConfigurationFile.Text = "Configuration File";
            this.toolStripMainMenuItemHelpConfigurationFile.Click += new System.EventHandler(this.toolStripDropDownButtonHelp_Click);
            // 
            // toolStripMainMenuItemHelpDynamicRegexValidator
            // 
            this.toolStripMainMenuItemHelpDynamicRegexValidator.Name = "toolStripMainMenuItemHelpDynamicRegexValidator";
            this.toolStripMainMenuItemHelpDynamicRegexValidator.Size = new System.Drawing.Size(258, 22);
            this.toolStripMainMenuItemHelpDynamicRegexValidator.Text = "Dynamic Regex Validator";
            // 
            // panelModules
            // 
            this.panelModules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelModules.AutoScroll = true;
            this.panelModules.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelModules.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelModules.Controls.Add(this.labelModuleHelp);
            this.panelModules.Controls.Add(this.tabControlModules);
            this.panelModules.Location = new System.Drawing.Point(1053, 27);
            this.panelModules.Name = "panelModules";
            this.panelModules.Size = new System.Drawing.Size(274, 680);
            this.panelModules.TabIndex = 6;
            // 
            // buttonResizeModulesPanel
            // 
            this.buttonResizeModulesPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonResizeModulesPanel.Location = new System.Drawing.Point(1324, 24);
            this.buttonResizeModulesPanel.Name = "buttonResizeModulesPanel";
            this.buttonResizeModulesPanel.Size = new System.Drawing.Size(15, 686);
            this.buttonResizeModulesPanel.TabIndex = 7;
            this.buttonResizeModulesPanel.Text = ">";
            this.buttonResizeModulesPanel.UseVisualStyleBackColor = true;
            this.buttonResizeModulesPanel.Click += new System.EventHandler(this.buttonResizeModulesPanel_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1339, 734);
            this.Controls.Add(this.buttonResizeModulesPanel);
            this.Controls.Add(this.panelModules);
            this.Controls.Add(this.listBoxScreenshots);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.comboBoxFilterType);
            this.Controls.Add(this.buttonRefreshFilterValues);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.comboBoxFilterValue);
            this.Controls.Add(this.tabControlViews);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.monthCalendar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.MinimumSize = new System.Drawing.Size(1355, 773);
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
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.panelModules.ResumeLayout(false);
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
        private System.Windows.Forms.Timer timerHelpTips;
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
        private ToolStripMenuItem toolStripMenuItemConfigure;
        private ToolStripMenuItem toolStripMenuItemChangeScreen;
        private ToolStripMenuItem toolStripMenuItemChangeRegion;
        private ToolStripMenuItem toolStripMenuItemChangeEditor;
        private ToolStripMenuItem toolStripMenuItemChangeSchedule;
        private ToolStripMenuItem toolStripMenuItemChangeMacroTag;
        private ToolStripMenuItem toolStripMenuItemChangeTrigger;
        private ToolStripMenuItem toolStripMenuItemRegionSelectOptions;
        private ToolStripSeparator toolStripSeparatorRegionSelectOptions;
        private ToolStripMenuItem toolStripMenuItemRegionSelectClipboardFloatingScreenshot;
        private ToolStripDropDownButton toolStripDropDownButtonSetup;
        private ToolStripDropDownButton toolStripDropDownButtonStartScreenCapture;
        private ToolStripDropDownButton toolStripDropDownButtonStopScreenCapture;
        private ToolStripMenuItem toolStripMenuItemSecurity;
        private ToolStripMenuItem toolStripMenuItemApplicationFocus;
        private ToolStripMenuItem toolStripMenuItemActiveWindowTitle;
        private ToolStripMenuItem toolStripMenuItemLabels;
        private ToolStripMenuItem toolStripMenuItemInterval;
        private ToolStripMenuItem toolStripMenuItemCommandLine;
        private ToolStripMenuItem toolStripMenuItemKeyboardShortcuts;
        private ToolStripDropDownButton toolStripDropDownButtonPreview;
        private ToolStripMenuItem toolStripMenuItemOptimizeScreenCapture;
        private ToolStripDropDownButton toolStripDropDownButtonEmailSettings;
        private ToolStripDropDownButton toolStripDropDownButtonFileTransferSettings;
        private Label labelModuleHelp;
        private ToolStripMenuItem toolStripMenuItemOpenProgramFolder;
        private ToolStripMenuItem toolStripMenuItemHelp;
        private ToolStripMenuItem toolStripMenuItemRegionSelectFloatingScreenshot;
        private ToolStripMenuItem toolStripMenuItemScreenshotsFolder;
        private ToolStripMenuItem toolStripMenuItemLabelSwitcher;
        private ToolStripMenuItem toolStripMenuItemFilenamePattern;
        private ToolStripMenuItem toolStripMenuItemImageFormat;
        private ToolStripMenuItem toolStripMenuItemEncryptorDecryptor;
        private ToolStripStatusLabel toolStripCycleCount;
        private ToolStripStatusLabel toolStripSpacer;
        private ToolStripMenuItem toolStripMenuItemCommandDeck;
        private ToolStripMenuItem toolStripMenuItemRegionSelectCommandDeck;
        private ToolStripMenuItem toolStripMenuItemRegionSelectAddRegion;
        private Timer timerParseCommands;
        private Timer timerScheduleCheck;
        private Timer timerTriggerCheck;
        private ToolStripMenuItem toolStripMenuItemCaptureNowOptions;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem toolStripMainMenuItemFile;
        private ToolStripMenuItem toolStripMainMenuItemExit;
        private ToolStripMenuItem toolStripMainMenuItemSetup;
        private ToolStripMenuItem toolStripMainMenuItemControl;
        private ToolStripMenuItem toolStripMainMenuItemScreens;
        private ToolStripMenuItem toolStripMainMenuItemRegions;
        private ToolStripMenuItem toolStripMainMenuItemEditors;
        private ToolStripMenuItem toolStripMainMenuItemSchedules;
        private ToolStripMenuItem toolStripMainMenuItemMacroTags;
        private ToolStripMenuItem toolStripMainMenuItemTriggers;
        private ToolStripMenuItem toolStripMainMenuItemTools;
        private ToolStripMenuItem toolStripMainMenuItemHelp;
        private ToolStripMenuItem toolStripMainMenuItemScreenshotsFolder;
        private ToolStripMenuItem toolStripMainMenuItemFilenamePattern;
        private ToolStripMenuItem toolStripMainMenuItemImageFormat;
        private ToolStripMenuItem toolStripMainMenuItemInterval;
        private ToolStripMenuItem toolStripMainMenuItemOptimizeScreenCapture;
        private ToolStripMenuItem toolStripMainMenuItemLabels;
        private ToolStripMenuItem toolStripMainMenuItemActiveWindowTitle;
        private ToolStripMenuItem toolStripMainMenuItemApplicationFocus;
        private ToolStripMenuItem toolStripMainMenuItemSecurity;
        private ToolStripMenuItem toolStripMainMenuItemKeyboardShortcuts;
        private ToolStripMenuItem toolStripMainMenuItemAboutAutoScreenCapture;
        private ToolStripSeparator toolStripMainSeparatorAbout;
        private ToolStripMenuItem toolStripMainMenuItemHelpWelcome;
        private ToolStripMenuItem toolStripMainMenuItemHelpLicense;
        private ToolStripMenuItem toolStripMainMenuItemHelpChangelog;
        private ToolStripMenuItem toolStripMainMenuItemHelpQuickStart;
        private ToolStripMenuItem toolStripMainMenuItemHelpGettingStarted;
        private ToolStripMenuItem toolStripMainMenuItemHelpScreens;
        private ToolStripMenuItem toolStripMainMenuItemHelpRegions;
        private ToolStripMenuItem toolStripMainMenuItemHelpEditors;
        private ToolStripMenuItem toolStripMainMenuItemHelpSchedules;
        private ToolStripMenuItem toolStripMainMenuItemHelpMacroTags;
        private ToolStripMenuItem toolStripMainMenuItemHelpTriggers;
        private ToolStripMenuItem toolStripMainMenuItemHelpCommandLine;
        private ToolStripMenuItem toolStripMainMenuItemHelpCommonSetupScenarios;
        private ToolStripMenuItem toolStripMainMenuItemHelpConfigurationFile;
        private ToolStripMenuItem toolStripMainMenuItemRegionSelect;
        private ToolStripMenuItem toolStripMainMenuItemRegionSelectOptions;
        private ToolStripMenuItem toolStripMainMenuItemCommandDeck;
        private ToolStripSeparator toolStripSeparatorMainRegionSelect;
        private ToolStripMenuItem toolStripMainMenuItemRegionSelectClipboard;
        private ToolStripMenuItem toolStripMainMenuItemRegionSelectClipboardAutoSave;
        private ToolStripMenuItem toolStripMainMenuItemRegionSelectClipboardAutoSaveEdit;
        private ToolStripMenuItem toolStripMainMenuItemRegionSelectClipboardFloatingScreenshot;
        private ToolStripMenuItem toolStripMainMenuItemRegionSelectFloatingScreenshot;
        private ToolStripMenuItem toolStripMainMenuItemRegionSelectAddRegion;
        private ToolStripMenuItem toolStripMainMenuItemStartScreenCapture;
        private ToolStripMenuItem toolStripMainMenuItemStopScreenCapture;
        private ToolStripMenuItem toolStripMainMenuItemScreenCaptureStatus;
        private ToolStripMenuItem toolStripMainMenuItemScreenCaptureStatusWithLabelSwitcher;
        private ToolStripMenuItem toolStripMainMenuItemDynamicRegexValidator;
        private ToolStripMenuItem toolStripMainMenuItemEncryptorDecryptor;
        private ToolStripMenuItem toolStripMainMenuItemToolsScreenCaptureStatusWithLabelSwitcher;
        private ToolStripMenuItem toolStripMainMenuItemToolsCommandDeck;
        private ToolStripMenuItem toolStripMainMenuItemOpenProgramFolder;
        private ToolStripSeparator toolStripSeparatorExit;
        private ToolStripMenuItem toolStripMainMenuItemControlCommandDeck;
        private ToolStripMenuItem toolStripMainMenuItemFileTransferSettings;
        private ToolStripMenuItem toolStripMainMenuItemEmailSettings;
        private ToolStripStatusLabel toolStripCurrentDate;
        private ToolStripStatusLabel toolStripCurrentTime;
        private Panel panelModules;
        private Button buttonResizeModulesPanel;
        private ToolStripMenuItem toolStripMainMenuItemRegionSelectAddRegionExpress;
        private ToolStripMenuItem toolStripMenuItemRegionSelectAddRegionExpress;
        private ToolStripMenuItem toolStripMainMenuItemAutoScreenCaptureForBeginners;
        private ToolStripMenuItem toolStripMenuItemAutoScreenCaptureForBeginners;
        private ToolStripMenuItem toolStripRootMenuItemCommandDeck;
        private ToolStripMenuItem toolStripMainMenuItemAutoScreenCaptureForBeginnersInHelp;
        private ToolStripSeparator toolStripMainSeparatorWelcome;
        private ToolStripMenuItem toolStripMainMenuItemHelpWhatsNew;
        private ToolStripMenuItem toolStripMainMenuItemHelpDynamicRegexValidator;
    }
}