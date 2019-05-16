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
            this.toolStripSplitButtonSaveSettings = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripDropDownButtonOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItemShowSystemTrayIcon = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripMenuItemStartScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStopScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorCapture = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlModules = new System.Windows.Forms.TabControl();
            this.tabPageInterval = new System.Windows.Forms.TabPage();
            this.labelScreenshotLabel = new System.Windows.Forms.Label();
            this.textBoxScreenshotLabel = new System.Windows.Forms.TextBox();
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
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.groupBoxSchedule = new System.Windows.Forms.GroupBox();
            this.checkBoxScheduleOnTheseDays = new System.Windows.Forms.CheckBox();
            this.checkBoxFriday = new System.Windows.Forms.CheckBox();
            this.checkBoxThursday = new System.Windows.Forms.CheckBox();
            this.checkBoxWednesday = new System.Windows.Forms.CheckBox();
            this.checkBoxTuesday = new System.Windows.Forms.CheckBox();
            this.checkBoxMonday = new System.Windows.Forms.CheckBox();
            this.checkBoxSunday = new System.Windows.Forms.CheckBox();
            this.checkBoxSaturday = new System.Windows.Forms.CheckBox();
            this.dateTimePickerScheduleStopAt = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerScheduleStartAt = new System.Windows.Forms.DateTimePicker();
            this.checkBoxScheduleStopAt = new System.Windows.Forms.CheckBox();
            this.checkBoxScheduleStartAt = new System.Windows.Forms.CheckBox();
            this.tabPageScreenshots = new System.Windows.Forms.TabPage();
            this.labelDays = new System.Windows.Forms.Label();
            this.numericUpDownKeepScreenshotsForDays = new System.Windows.Forms.NumericUpDown();
            this.labelKeepScreenshots = new System.Windows.Forms.Label();
            this.tabPageSecurity = new System.Windows.Forms.TabPage();
            this.groupBoxSecurity = new System.Windows.Forms.GroupBox();
            this.checkBoxPassphraseLock = new System.Windows.Forms.CheckBox();
            this.buttonClearPassphrase = new System.Windows.Forms.Button();
            this.labelPasswordDescription = new System.Windows.Forms.Label();
            this.buttonSetPassphrase = new System.Windows.Forms.Button();
            this.textBoxPassphrase = new System.Windows.Forms.TextBox();
            this.tabPageScreens = new System.Windows.Forms.TabPage();
            this.tabPageRegions = new System.Windows.Forms.TabPage();
            this.tabPageEditors = new System.Windows.Forms.TabPage();
            this.tabPageTriggers = new System.Windows.Forms.TabPage();
            this.timerScheduledCaptureStart = new System.Windows.Forms.Timer(this.components);
            this.timerScheduledCaptureStop = new System.Windows.Forms.Timer(this.components);
            this.timerScreenCapture = new System.Windows.Forms.Timer(this.components);
            this.timerDeleteOldScreenshots = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
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
            this.statusStrip.SuspendLayout();
            this.contextMenuStripSystemTrayIcon.SuspendLayout();
            this.tabControlModules.SuspendLayout();
            this.tabPageInterval.SuspendLayout();
            this.groupBoxCaptureDelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            this.tabPageSchedule.SuspendLayout();
            this.groupBoxSchedule.SuspendLayout();
            this.tabPageScreenshots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeepScreenshotsForDays)).BeginInit();
            this.tabPageSecurity.SuspendLayout();
            this.groupBoxSecurity.SuspendLayout();
            this.SuspendLayout();
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(0, 32);
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
            this.toolStripSplitButtonSaveSettings,
            this.toolStripDropDownButtonOptions});
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
            this.toolStripSplitButtonStartScreenCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonStartScreenCapture.Name = "toolStripSplitButtonStartScreenCapture";
            this.toolStripSplitButtonStartScreenCapture.Size = new System.Drawing.Size(52, 20);
            this.toolStripSplitButtonStartScreenCapture.Text = "Start";
            this.toolStripSplitButtonStartScreenCapture.ButtonClick += new System.EventHandler(this.Click_toolStripMenuItemStartScreenCapture);
            // 
            // toolStripSplitButtonStopScreenCapture
            // 
            this.toolStripSplitButtonStopScreenCapture.AutoToolTip = false;
            this.toolStripSplitButtonStopScreenCapture.DropDownButtonWidth = 0;
            this.toolStripSplitButtonStopScreenCapture.Enabled = false;
            this.toolStripSplitButtonStopScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.stop_screen_capture;
            this.toolStripSplitButtonStopScreenCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonStopScreenCapture.Name = "toolStripSplitButtonStopScreenCapture";
            this.toolStripSplitButtonStopScreenCapture.Size = new System.Drawing.Size(52, 20);
            this.toolStripSplitButtonStopScreenCapture.Text = "Stop";
            this.toolStripSplitButtonStopScreenCapture.ButtonClick += new System.EventHandler(this.Click_toolStripMenuItemStopScreenCapture);
            // 
            // toolStripSplitButtonSaveSettings
            // 
            this.toolStripSplitButtonSaveSettings.DropDownButtonWidth = 0;
            this.toolStripSplitButtonSaveSettings.Image = global::AutoScreenCapture.Properties.Resources.save;
            this.toolStripSplitButtonSaveSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonSaveSettings.Name = "toolStripSplitButtonSaveSettings";
            this.toolStripSplitButtonSaveSettings.Size = new System.Drawing.Size(97, 20);
            this.toolStripSplitButtonSaveSettings.Text = "Save Settings";
            this.toolStripSplitButtonSaveSettings.ButtonClick += new System.EventHandler(this.toolStripSplitButtonSaveSettings_ButtonClick);
            // 
            // toolStripDropDownButtonOptions
            // 
            this.toolStripDropDownButtonOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemShowSystemTrayIcon});
            this.toolStripDropDownButtonOptions.Image = global::AutoScreenCapture.Properties.Resources.options;
            this.toolStripDropDownButtonOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonOptions.Name = "toolStripDropDownButtonOptions";
            this.toolStripDropDownButtonOptions.Size = new System.Drawing.Size(78, 20);
            this.toolStripDropDownButtonOptions.Text = "Options";
            // 
            // toolStripMenuItemShowSystemTrayIcon
            // 
            this.toolStripMenuItemShowSystemTrayIcon.CheckOnClick = true;
            this.toolStripMenuItemShowSystemTrayIcon.Name = "toolStripMenuItemShowSystemTrayIcon";
            this.toolStripMenuItemShowSystemTrayIcon.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItemShowSystemTrayIcon.Text = "Show system tray icon";
            this.toolStripMenuItemShowSystemTrayIcon.CheckedChanged += new System.EventHandler(this.CheckedChanged_toolStripMenuItemShowSystemTrayIcon);
            this.toolStripMenuItemShowSystemTrayIcon.Click += new System.EventHandler(this.SaveSettings);
            // 
            // tabControlViews
            // 
            this.tabControlViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlViews.Location = new System.Drawing.Point(251, 1);
            this.tabControlViews.Name = "tabControlViews";
            this.tabControlViews.SelectedIndex = 0;
            this.tabControlViews.Size = new System.Drawing.Size(582, 377);
            this.tabControlViews.TabIndex = 4;
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
            this.listBoxScreenshots.Size = new System.Drawing.Size(235, 160);
            this.listBoxScreenshots.TabIndex = 6;
            this.listBoxScreenshots.TabStop = false;
            this.listBoxScreenshots.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged_listBoxScreenshots);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripSystemTrayIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseMove);
            // 
            // contextMenuStripSystemTrayIcon
            // 
            this.contextMenuStripSystemTrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAbout,
            this.toolStripSeparatorAbout,
            this.toolStripMenuItemShowInterface,
            this.toolStripMenuItemHideInterface,
            this.toolStripSeparatorInterface,
            this.toolStripMenuItemStartScreenCapture,
            this.toolStripMenuItemStopScreenCapture,
            this.toolStripSeparatorCapture,
            this.toolStripMenuItemExit});
            this.contextMenuStripSystemTrayIcon.Name = "contextMenuStrip";
            this.contextMenuStripSystemTrayIcon.Size = new System.Drawing.Size(175, 154);
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Image = global::AutoScreenCapture.Properties.Resources.about;
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItemAbout.Text = "About ...";
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.Click_toolStripMenuItemAbout);
            // 
            // toolStripSeparatorAbout
            // 
            this.toolStripSeparatorAbout.Name = "toolStripSeparatorAbout";
            this.toolStripSeparatorAbout.Size = new System.Drawing.Size(171, 6);
            // 
            // toolStripMenuItemShowInterface
            // 
            this.toolStripMenuItemShowInterface.Enabled = false;
            this.toolStripMenuItemShowInterface.Name = "toolStripMenuItemShowInterface";
            this.toolStripMenuItemShowInterface.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItemShowInterface.Text = "Show Interface";
            this.toolStripMenuItemShowInterface.Click += new System.EventHandler(this.Click_toolStripMenuItemShowInterface);
            // 
            // toolStripMenuItemHideInterface
            // 
            this.toolStripMenuItemHideInterface.Enabled = false;
            this.toolStripMenuItemHideInterface.Name = "toolStripMenuItemHideInterface";
            this.toolStripMenuItemHideInterface.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItemHideInterface.Text = "Hide Interface";
            this.toolStripMenuItemHideInterface.Click += new System.EventHandler(this.Click_toolStripMenuItemHideInterface);
            // 
            // toolStripSeparatorInterface
            // 
            this.toolStripSeparatorInterface.Name = "toolStripSeparatorInterface";
            this.toolStripSeparatorInterface.Size = new System.Drawing.Size(171, 6);
            // 
            // toolStripMenuItemStartScreenCapture
            // 
            this.toolStripMenuItemStartScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.start_screen_capture;
            this.toolStripMenuItemStartScreenCapture.Name = "toolStripMenuItemStartScreenCapture";
            this.toolStripMenuItemStartScreenCapture.ShowShortcutKeys = false;
            this.toolStripMenuItemStartScreenCapture.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItemStartScreenCapture.Text = "Start Screen Capture";
            this.toolStripMenuItemStartScreenCapture.Click += new System.EventHandler(this.Click_toolStripMenuItemStartScreenCapture);
            // 
            // toolStripMenuItemStopScreenCapture
            // 
            this.toolStripMenuItemStopScreenCapture.Enabled = false;
            this.toolStripMenuItemStopScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.stop_screen_capture;
            this.toolStripMenuItemStopScreenCapture.Name = "toolStripMenuItemStopScreenCapture";
            this.toolStripMenuItemStopScreenCapture.ShowShortcutKeys = false;
            this.toolStripMenuItemStopScreenCapture.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItemStopScreenCapture.Text = "Stop Screen Capture";
            this.toolStripMenuItemStopScreenCapture.Click += new System.EventHandler(this.Click_toolStripMenuItemStopScreenCapture);
            // 
            // toolStripSeparatorCapture
            // 
            this.toolStripSeparatorCapture.Name = "toolStripSeparatorCapture";
            this.toolStripSeparatorCapture.Size = new System.Drawing.Size(171, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.ShowShortcutKeys = false;
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItemExit.Text = "Exit";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.Click_toolStripMenuItemExit);
            // 
            // tabControlModules
            // 
            this.tabControlModules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlModules.Controls.Add(this.tabPageInterval);
            this.tabControlModules.Controls.Add(this.tabPageSchedule);
            this.tabControlModules.Controls.Add(this.tabPageScreenshots);
            this.tabControlModules.Controls.Add(this.tabPageSecurity);
            this.tabControlModules.Controls.Add(this.tabPageScreens);
            this.tabControlModules.Controls.Add(this.tabPageRegions);
            this.tabControlModules.Controls.Add(this.tabPageEditors);
            this.tabControlModules.Controls.Add(this.tabPageTriggers);
            this.tabControlModules.Location = new System.Drawing.Point(0, 196);
            this.tabControlModules.Multiline = true;
            this.tabControlModules.Name = "tabControlModules";
            this.tabControlModules.SelectedIndex = 0;
            this.tabControlModules.Size = new System.Drawing.Size(249, 234);
            this.tabControlModules.TabIndex = 12;
            this.tabControlModules.TabStop = false;
            // 
            // tabPageInterval
            // 
            this.tabPageInterval.AutoScroll = true;
            this.tabPageInterval.Controls.Add(this.labelScreenshotLabel);
            this.tabPageInterval.Controls.Add(this.textBoxScreenshotLabel);
            this.tabPageInterval.Controls.Add(this.groupBoxCaptureDelay);
            this.tabPageInterval.Location = new System.Drawing.Point(4, 40);
            this.tabPageInterval.Name = "tabPageInterval";
            this.tabPageInterval.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInterval.Size = new System.Drawing.Size(241, 190);
            this.tabPageInterval.TabIndex = 0;
            this.tabPageInterval.Text = "Interval";
            this.tabPageInterval.UseVisualStyleBackColor = true;
            // 
            // labelScreenshotLabel
            // 
            this.labelScreenshotLabel.AutoSize = true;
            this.labelScreenshotLabel.Location = new System.Drawing.Point(6, 134);
            this.labelScreenshotLabel.Name = "labelScreenshotLabel";
            this.labelScreenshotLabel.Size = new System.Drawing.Size(174, 13);
            this.labelScreenshotLabel.TabIndex = 16;
            this.labelScreenshotLabel.Text = "Apply this label to each screenshot:";
            // 
            // textBoxScreenshotLabel
            // 
            this.textBoxScreenshotLabel.Location = new System.Drawing.Point(6, 150);
            this.textBoxScreenshotLabel.MaxLength = 500;
            this.textBoxScreenshotLabel.Name = "textBoxScreenshotLabel";
            this.textBoxScreenshotLabel.Size = new System.Drawing.Size(205, 20);
            this.textBoxScreenshotLabel.TabIndex = 15;
            this.textBoxScreenshotLabel.TabStop = false;
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
            this.groupBoxCaptureDelay.TabIndex = 14;
            this.groupBoxCaptureDelay.TabStop = false;
            this.groupBoxCaptureDelay.Text = "Take screenshots every ...";
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
            this.checkBoxInitialScreenshot.TabIndex = 18;
            this.checkBoxInitialScreenshot.TabStop = false;
            this.checkBoxInitialScreenshot.Text = "Initial Capture";
            this.checkBoxInitialScreenshot.UseVisualStyleBackColor = true;
            this.checkBoxInitialScreenshot.Click += new System.EventHandler(this.SaveSettings);
            this.checkBoxInitialScreenshot.Leave += new System.EventHandler(this.SaveSettings);
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
            this.numericUpDownCaptureLimit.TabIndex = 8;
            this.numericUpDownCaptureLimit.TabStop = false;
            this.numericUpDownCaptureLimit.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxCaptureLimit
            // 
            this.checkBoxCaptureLimit.AutoSize = true;
            this.checkBoxCaptureLimit.Location = new System.Drawing.Point(110, 47);
            this.checkBoxCaptureLimit.Name = "checkBoxCaptureLimit";
            this.checkBoxCaptureLimit.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCaptureLimit.TabIndex = 16;
            this.checkBoxCaptureLimit.TabStop = false;
            this.checkBoxCaptureLimit.UseVisualStyleBackColor = true;
            this.checkBoxCaptureLimit.CheckedChanged += new System.EventHandler(this.CheckedChanged_checkBoxCaptureLimit);
            this.checkBoxCaptureLimit.Click += new System.EventHandler(this.SaveSettings);
            this.checkBoxCaptureLimit.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // labelMillisecondsInterval
            // 
            this.labelMillisecondsInterval.AutoSize = true;
            this.labelMillisecondsInterval.Location = new System.Drawing.Point(54, 99);
            this.labelMillisecondsInterval.Name = "labelMillisecondsInterval";
            this.labelMillisecondsInterval.Size = new System.Drawing.Size(63, 13);
            this.labelMillisecondsInterval.TabIndex = 7;
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
            this.numericUpDownMillisecondsInterval.TabIndex = 6;
            this.numericUpDownMillisecondsInterval.TabStop = false;
            this.numericUpDownMillisecondsInterval.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // labelSecondsInterval
            // 
            this.labelSecondsInterval.AutoSize = true;
            this.labelSecondsInterval.Location = new System.Drawing.Point(54, 73);
            this.labelSecondsInterval.Name = "labelSecondsInterval";
            this.labelSecondsInterval.Size = new System.Drawing.Size(47, 13);
            this.labelSecondsInterval.TabIndex = 5;
            this.labelSecondsInterval.Text = "seconds";
            // 
            // labelMinutesInterval
            // 
            this.labelMinutesInterval.AutoSize = true;
            this.labelMinutesInterval.Location = new System.Drawing.Point(54, 47);
            this.labelMinutesInterval.Name = "labelMinutesInterval";
            this.labelMinutesInterval.Size = new System.Drawing.Size(43, 13);
            this.labelMinutesInterval.TabIndex = 4;
            this.labelMinutesInterval.Text = "minutes";
            // 
            // labelHoursInterval
            // 
            this.labelHoursInterval.AutoSize = true;
            this.labelHoursInterval.Location = new System.Drawing.Point(54, 21);
            this.labelHoursInterval.Name = "labelHoursInterval";
            this.labelHoursInterval.Size = new System.Drawing.Size(33, 13);
            this.labelHoursInterval.TabIndex = 3;
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
            this.numericUpDownSecondsInterval.TabIndex = 2;
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
            this.numericUpDownMinutesInterval.TabIndex = 1;
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
            // tabPageSchedule
            // 
            this.tabPageSchedule.Controls.Add(this.groupBoxSchedule);
            this.tabPageSchedule.Location = new System.Drawing.Point(4, 40);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.Size = new System.Drawing.Size(241, 190);
            this.tabPageSchedule.TabIndex = 6;
            this.tabPageSchedule.Text = "Schedule";
            this.tabPageSchedule.UseVisualStyleBackColor = true;
            // 
            // groupBoxSchedule
            // 
            this.groupBoxSchedule.Controls.Add(this.checkBoxScheduleOnTheseDays);
            this.groupBoxSchedule.Controls.Add(this.checkBoxFriday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxThursday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxWednesday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxTuesday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxMonday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxSunday);
            this.groupBoxSchedule.Controls.Add(this.checkBoxSaturday);
            this.groupBoxSchedule.Controls.Add(this.dateTimePickerScheduleStopAt);
            this.groupBoxSchedule.Controls.Add(this.dateTimePickerScheduleStartAt);
            this.groupBoxSchedule.Controls.Add(this.checkBoxScheduleStopAt);
            this.groupBoxSchedule.Controls.Add(this.checkBoxScheduleStartAt);
            this.groupBoxSchedule.Location = new System.Drawing.Point(7, 6);
            this.groupBoxSchedule.Name = "groupBoxSchedule";
            this.groupBoxSchedule.Size = new System.Drawing.Size(205, 108);
            this.groupBoxSchedule.TabIndex = 22;
            this.groupBoxSchedule.TabStop = false;
            this.groupBoxSchedule.Text = "Schedule";
            // 
            // checkBoxScheduleOnTheseDays
            // 
            this.checkBoxScheduleOnTheseDays.AutoSize = true;
            this.checkBoxScheduleOnTheseDays.Location = new System.Drawing.Point(6, 63);
            this.checkBoxScheduleOnTheseDays.Name = "checkBoxScheduleOnTheseDays";
            this.checkBoxScheduleOnTheseDays.Size = new System.Drawing.Size(119, 17);
            this.checkBoxScheduleOnTheseDays.TabIndex = 11;
            this.checkBoxScheduleOnTheseDays.TabStop = false;
            this.checkBoxScheduleOnTheseDays.Text = "Only on these days:";
            this.checkBoxScheduleOnTheseDays.UseVisualStyleBackColor = true;
            this.checkBoxScheduleOnTheseDays.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxFriday
            // 
            this.checkBoxFriday.AutoSize = true;
            this.checkBoxFriday.Enabled = false;
            this.checkBoxFriday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxFriday.Location = new System.Drawing.Point(165, 86);
            this.checkBoxFriday.Name = "checkBoxFriday";
            this.checkBoxFriday.Size = new System.Drawing.Size(33, 16);
            this.checkBoxFriday.TabIndex = 10;
            this.checkBoxFriday.TabStop = false;
            this.checkBoxFriday.Text = "Fr";
            this.checkBoxFriday.UseVisualStyleBackColor = true;
            this.checkBoxFriday.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxThursday
            // 
            this.checkBoxThursday.AutoSize = true;
            this.checkBoxThursday.Enabled = false;
            this.checkBoxThursday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxThursday.Location = new System.Drawing.Point(130, 86);
            this.checkBoxThursday.Name = "checkBoxThursday";
            this.checkBoxThursday.Size = new System.Drawing.Size(34, 16);
            this.checkBoxThursday.TabIndex = 9;
            this.checkBoxThursday.TabStop = false;
            this.checkBoxThursday.Text = "Th";
            this.checkBoxThursday.UseVisualStyleBackColor = true;
            this.checkBoxThursday.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxWednesday
            // 
            this.checkBoxWednesday.AutoSize = true;
            this.checkBoxWednesday.Enabled = false;
            this.checkBoxWednesday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxWednesday.Location = new System.Drawing.Point(92, 86);
            this.checkBoxWednesday.Name = "checkBoxWednesday";
            this.checkBoxWednesday.Size = new System.Drawing.Size(38, 16);
            this.checkBoxWednesday.TabIndex = 8;
            this.checkBoxWednesday.TabStop = false;
            this.checkBoxWednesday.Text = "We";
            this.checkBoxWednesday.UseVisualStyleBackColor = true;
            this.checkBoxWednesday.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxTuesday
            // 
            this.checkBoxTuesday.AutoSize = true;
            this.checkBoxTuesday.Enabled = false;
            this.checkBoxTuesday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTuesday.Location = new System.Drawing.Point(58, 86);
            this.checkBoxTuesday.Name = "checkBoxTuesday";
            this.checkBoxTuesday.Size = new System.Drawing.Size(34, 16);
            this.checkBoxTuesday.TabIndex = 7;
            this.checkBoxTuesday.TabStop = false;
            this.checkBoxTuesday.Text = "Tu";
            this.checkBoxTuesday.UseVisualStyleBackColor = true;
            this.checkBoxTuesday.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxMonday
            // 
            this.checkBoxMonday.AutoSize = true;
            this.checkBoxMonday.Enabled = false;
            this.checkBoxMonday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMonday.Location = new System.Drawing.Point(20, 86);
            this.checkBoxMonday.Name = "checkBoxMonday";
            this.checkBoxMonday.Size = new System.Drawing.Size(38, 16);
            this.checkBoxMonday.TabIndex = 6;
            this.checkBoxMonday.TabStop = false;
            this.checkBoxMonday.Text = "Mo";
            this.checkBoxMonday.UseVisualStyleBackColor = true;
            this.checkBoxMonday.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxSunday
            // 
            this.checkBoxSunday.AutoSize = true;
            this.checkBoxSunday.Enabled = false;
            this.checkBoxSunday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSunday.Location = new System.Drawing.Point(165, 64);
            this.checkBoxSunday.Name = "checkBoxSunday";
            this.checkBoxSunday.Size = new System.Drawing.Size(35, 16);
            this.checkBoxSunday.TabIndex = 5;
            this.checkBoxSunday.TabStop = false;
            this.checkBoxSunday.Text = "Su";
            this.checkBoxSunday.UseVisualStyleBackColor = true;
            this.checkBoxSunday.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxSaturday
            // 
            this.checkBoxSaturday.AutoSize = true;
            this.checkBoxSaturday.Enabled = false;
            this.checkBoxSaturday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSaturday.Location = new System.Drawing.Point(130, 64);
            this.checkBoxSaturday.Name = "checkBoxSaturday";
            this.checkBoxSaturday.Size = new System.Drawing.Size(35, 16);
            this.checkBoxSaturday.TabIndex = 4;
            this.checkBoxSaturday.TabStop = false;
            this.checkBoxSaturday.Text = "Sa";
            this.checkBoxSaturday.UseVisualStyleBackColor = true;
            this.checkBoxSaturday.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // dateTimePickerScheduleStopAt
            // 
            this.dateTimePickerScheduleStopAt.CustomFormat = "HH:mm:ss";
            this.dateTimePickerScheduleStopAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerScheduleStopAt.Location = new System.Drawing.Point(130, 39);
            this.dateTimePickerScheduleStopAt.Name = "dateTimePickerScheduleStopAt";
            this.dateTimePickerScheduleStopAt.ShowUpDown = true;
            this.dateTimePickerScheduleStopAt.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerScheduleStopAt.TabIndex = 3;
            this.dateTimePickerScheduleStopAt.TabStop = false;
            this.dateTimePickerScheduleStopAt.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // dateTimePickerScheduleStartAt
            // 
            this.dateTimePickerScheduleStartAt.CustomFormat = "HH:mm:ss";
            this.dateTimePickerScheduleStartAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerScheduleStartAt.Location = new System.Drawing.Point(130, 16);
            this.dateTimePickerScheduleStartAt.Name = "dateTimePickerScheduleStartAt";
            this.dateTimePickerScheduleStartAt.ShowUpDown = true;
            this.dateTimePickerScheduleStartAt.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerScheduleStartAt.TabIndex = 2;
            this.dateTimePickerScheduleStartAt.TabStop = false;
            this.dateTimePickerScheduleStartAt.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxScheduleStopAt
            // 
            this.checkBoxScheduleStopAt.AutoSize = true;
            this.checkBoxScheduleStopAt.Location = new System.Drawing.Point(6, 41);
            this.checkBoxScheduleStopAt.Name = "checkBoxScheduleStopAt";
            this.checkBoxScheduleStopAt.Size = new System.Drawing.Size(99, 17);
            this.checkBoxScheduleStopAt.TabIndex = 1;
            this.checkBoxScheduleStopAt.TabStop = false;
            this.checkBoxScheduleStopAt.Text = "Stop capture at";
            this.checkBoxScheduleStopAt.UseVisualStyleBackColor = true;
            this.checkBoxScheduleStopAt.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // checkBoxScheduleStartAt
            // 
            this.checkBoxScheduleStartAt.AutoSize = true;
            this.checkBoxScheduleStartAt.Location = new System.Drawing.Point(6, 18);
            this.checkBoxScheduleStartAt.Name = "checkBoxScheduleStartAt";
            this.checkBoxScheduleStartAt.Size = new System.Drawing.Size(99, 17);
            this.checkBoxScheduleStartAt.TabIndex = 0;
            this.checkBoxScheduleStartAt.TabStop = false;
            this.checkBoxScheduleStartAt.Text = "Start capture at";
            this.checkBoxScheduleStartAt.UseVisualStyleBackColor = true;
            this.checkBoxScheduleStartAt.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // tabPageScreenshots
            // 
            this.tabPageScreenshots.AutoScroll = true;
            this.tabPageScreenshots.Controls.Add(this.labelDays);
            this.tabPageScreenshots.Controls.Add(this.numericUpDownKeepScreenshotsForDays);
            this.tabPageScreenshots.Controls.Add(this.labelKeepScreenshots);
            this.tabPageScreenshots.Controls.Add(this.listBoxScreenshots);
            this.tabPageScreenshots.Location = new System.Drawing.Point(4, 40);
            this.tabPageScreenshots.Name = "tabPageScreenshots";
            this.tabPageScreenshots.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScreenshots.Size = new System.Drawing.Size(241, 190);
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
            this.labelDays.TabIndex = 20;
            this.labelDays.Text = "days";
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
            this.numericUpDownKeepScreenshotsForDays.TabIndex = 19;
            this.numericUpDownKeepScreenshotsForDays.TabStop = false;
            this.numericUpDownKeepScreenshotsForDays.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // labelKeepScreenshots
            // 
            this.labelKeepScreenshots.AutoSize = true;
            this.labelKeepScreenshots.Location = new System.Drawing.Point(3, 6);
            this.labelKeepScreenshots.Name = "labelKeepScreenshots";
            this.labelKeepScreenshots.Size = new System.Drawing.Size(107, 13);
            this.labelKeepScreenshots.TabIndex = 18;
            this.labelKeepScreenshots.Text = "Keep screenshots for";
            // 
            // tabPageSecurity
            // 
            this.tabPageSecurity.Controls.Add(this.groupBoxSecurity);
            this.tabPageSecurity.Location = new System.Drawing.Point(4, 40);
            this.tabPageSecurity.Name = "tabPageSecurity";
            this.tabPageSecurity.Size = new System.Drawing.Size(241, 190);
            this.tabPageSecurity.TabIndex = 7;
            this.tabPageSecurity.Text = "Security";
            this.tabPageSecurity.UseVisualStyleBackColor = true;
            // 
            // groupBoxSecurity
            // 
            this.groupBoxSecurity.Controls.Add(this.checkBoxPassphraseLock);
            this.groupBoxSecurity.Controls.Add(this.buttonClearPassphrase);
            this.groupBoxSecurity.Controls.Add(this.labelPasswordDescription);
            this.groupBoxSecurity.Controls.Add(this.buttonSetPassphrase);
            this.groupBoxSecurity.Controls.Add(this.textBoxPassphrase);
            this.groupBoxSecurity.Location = new System.Drawing.Point(6, 6);
            this.groupBoxSecurity.Name = "groupBoxSecurity";
            this.groupBoxSecurity.Size = new System.Drawing.Size(205, 135);
            this.groupBoxSecurity.TabIndex = 23;
            this.groupBoxSecurity.TabStop = false;
            this.groupBoxSecurity.Text = "Security";
            // 
            // checkBoxPassphraseLock
            // 
            this.checkBoxPassphraseLock.AutoSize = true;
            this.checkBoxPassphraseLock.Location = new System.Drawing.Point(6, 111);
            this.checkBoxPassphraseLock.Name = "checkBoxPassphraseLock";
            this.checkBoxPassphraseLock.Size = new System.Drawing.Size(50, 17);
            this.checkBoxPassphraseLock.TabIndex = 4;
            this.checkBoxPassphraseLock.TabStop = false;
            this.checkBoxPassphraseLock.Text = "Lock";
            this.checkBoxPassphraseLock.UseVisualStyleBackColor = true;
            this.checkBoxPassphraseLock.CheckedChanged += new System.EventHandler(this.CheckedChanged_checkBoxPassphraseLock);
            this.checkBoxPassphraseLock.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // buttonClearPassphrase
            // 
            this.buttonClearPassphrase.Location = new System.Drawing.Point(141, 107);
            this.buttonClearPassphrase.Name = "buttonClearPassphrase";
            this.buttonClearPassphrase.Size = new System.Drawing.Size(52, 23);
            this.buttonClearPassphrase.TabIndex = 3;
            this.buttonClearPassphrase.TabStop = false;
            this.buttonClearPassphrase.Text = "Clear";
            this.buttonClearPassphrase.UseVisualStyleBackColor = true;
            this.buttonClearPassphrase.Click += new System.EventHandler(this.Click_buttonClearPassphrase);
            this.buttonClearPassphrase.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // labelPasswordDescription
            // 
            this.labelPasswordDescription.Location = new System.Drawing.Point(7, 21);
            this.labelPasswordDescription.Name = "labelPasswordDescription";
            this.labelPasswordDescription.Size = new System.Drawing.Size(186, 55);
            this.labelPasswordDescription.TabIndex = 2;
            this.labelPasswordDescription.Text = "This passphrase will be required whenever screen capturing is stopped, this inter" +
    "face is shown, or the application is exiting.";
            // 
            // buttonSetPassphrase
            // 
            this.buttonSetPassphrase.Enabled = false;
            this.buttonSetPassphrase.Location = new System.Drawing.Point(83, 107);
            this.buttonSetPassphrase.Name = "buttonSetPassphrase";
            this.buttonSetPassphrase.Size = new System.Drawing.Size(52, 23);
            this.buttonSetPassphrase.TabIndex = 1;
            this.buttonSetPassphrase.TabStop = false;
            this.buttonSetPassphrase.Text = "Set";
            this.buttonSetPassphrase.UseVisualStyleBackColor = true;
            this.buttonSetPassphrase.Click += new System.EventHandler(this.Click_buttonSetPassphrase);
            this.buttonSetPassphrase.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // textBoxPassphrase
            // 
            this.textBoxPassphrase.Location = new System.Drawing.Point(6, 82);
            this.textBoxPassphrase.MaxLength = 30;
            this.textBoxPassphrase.Name = "textBoxPassphrase";
            this.textBoxPassphrase.Size = new System.Drawing.Size(187, 20);
            this.textBoxPassphrase.TabIndex = 0;
            this.textBoxPassphrase.TabStop = false;
            this.textBoxPassphrase.TextChanged += new System.EventHandler(this.TextChanged_textBoxPassphrase);
            this.textBoxPassphrase.Leave += new System.EventHandler(this.SaveSettings);
            // 
            // tabPageScreens
            // 
            this.tabPageScreens.Location = new System.Drawing.Point(4, 40);
            this.tabPageScreens.Name = "tabPageScreens";
            this.tabPageScreens.Size = new System.Drawing.Size(241, 190);
            this.tabPageScreens.TabIndex = 5;
            this.tabPageScreens.Text = "Screens";
            this.tabPageScreens.UseVisualStyleBackColor = true;
            // 
            // tabPageRegions
            // 
            this.tabPageRegions.Location = new System.Drawing.Point(4, 40);
            this.tabPageRegions.Name = "tabPageRegions";
            this.tabPageRegions.Size = new System.Drawing.Size(241, 190);
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
            this.tabPageEditors.Size = new System.Drawing.Size(241, 190);
            this.tabPageEditors.TabIndex = 2;
            this.tabPageEditors.Text = "Editors";
            this.tabPageEditors.UseVisualStyleBackColor = true;
            // 
            // tabPageTriggers
            // 
            this.tabPageTriggers.AutoScroll = true;
            this.tabPageTriggers.Location = new System.Drawing.Point(4, 40);
            this.tabPageTriggers.Name = "tabPageTriggers";
            this.tabPageTriggers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTriggers.Size = new System.Drawing.Size(241, 190);
            this.tabPageTriggers.TabIndex = 3;
            this.tabPageTriggers.Text = "Triggers";
            this.tabPageTriggers.UseVisualStyleBackColor = true;
            // 
            // timerScheduledCaptureStart
            // 
            this.timerScheduledCaptureStart.Enabled = true;
            this.timerScheduledCaptureStart.Interval = 1000;
            this.timerScheduledCaptureStart.Tick += new System.EventHandler(this.Tick_timerScheduledCaptureStart);
            // 
            // timerScheduledCaptureStop
            // 
            this.timerScheduledCaptureStop.Enabled = true;
            this.timerScheduledCaptureStop.Interval = 1000;
            this.timerScheduledCaptureStop.Tick += new System.EventHandler(this.Tick_timerScheduledCaptureStop);
            // 
            // timerScreenCapture
            // 
            this.timerScreenCapture.Enabled = true;
            this.timerScreenCapture.Tick += new System.EventHandler(this.Tick_timerScreenCapture);
            // 
            // timerDeleteOldScreenshots
            // 
            this.timerDeleteOldScreenshots.Enabled = true;
            this.timerDeleteOldScreenshots.Interval = 60000;
            this.timerDeleteOldScreenshots.Tick += new System.EventHandler(this.timerDeleteOldScreenshots_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelScreenshotTitle
            // 
            this.labelScreenshotTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScreenshotTitle.AutoSize = true;
            this.labelScreenshotTitle.Location = new System.Drawing.Point(258, 413);
            this.labelScreenshotTitle.Name = "labelScreenshotTitle";
            this.labelScreenshotTitle.Size = new System.Drawing.Size(72, 13);
            this.labelScreenshotTitle.TabIndex = 21;
            this.labelScreenshotTitle.Text = "Window Title:";
            // 
            // textBoxScreenshotTitle
            // 
            this.textBoxScreenshotTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScreenshotTitle.Location = new System.Drawing.Point(338, 410);
            this.textBoxScreenshotTitle.Name = "textBoxScreenshotTitle";
            this.textBoxScreenshotTitle.ReadOnly = true;
            this.textBoxScreenshotTitle.Size = new System.Drawing.Size(483, 20);
            this.textBoxScreenshotTitle.TabIndex = 22;
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
            this.labelScreenshotFormat.TabIndex = 23;
            this.labelScreenshotFormat.Text = "Image Format:";
            // 
            // textBoxScreenshotFormat
            // 
            this.textBoxScreenshotFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxScreenshotFormat.Location = new System.Drawing.Point(338, 384);
            this.textBoxScreenshotFormat.Name = "textBoxScreenshotFormat";
            this.textBoxScreenshotFormat.ReadOnly = true;
            this.textBoxScreenshotFormat.Size = new System.Drawing.Size(41, 20);
            this.textBoxScreenshotFormat.TabIndex = 24;
            this.textBoxScreenshotFormat.TabStop = false;
            // 
            // labelScreenshotWidth
            // 
            this.labelScreenshotWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelScreenshotWidth.AutoSize = true;
            this.labelScreenshotWidth.Location = new System.Drawing.Point(394, 387);
            this.labelScreenshotWidth.Name = "labelScreenshotWidth";
            this.labelScreenshotWidth.Size = new System.Drawing.Size(38, 13);
            this.labelScreenshotWidth.TabIndex = 29;
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
            this.textBoxScreenshotWidth.TabIndex = 30;
            this.textBoxScreenshotWidth.TabStop = false;
            // 
            // labelScreenshotHeight
            // 
            this.labelScreenshotHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelScreenshotHeight.AutoSize = true;
            this.labelScreenshotHeight.Location = new System.Drawing.Point(491, 387);
            this.labelScreenshotHeight.Name = "labelScreenshotHeight";
            this.labelScreenshotHeight.Size = new System.Drawing.Size(41, 13);
            this.labelScreenshotHeight.TabIndex = 31;
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
            this.textBoxScreenshotHeight.TabIndex = 32;
            this.textBoxScreenshotHeight.TabStop = false;
            // 
            // labelScreenshotDate
            // 
            this.labelScreenshotDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelScreenshotDate.AutoSize = true;
            this.labelScreenshotDate.Location = new System.Drawing.Point(591, 387);
            this.labelScreenshotDate.Name = "labelScreenshotDate";
            this.labelScreenshotDate.Size = new System.Drawing.Size(33, 13);
            this.labelScreenshotDate.TabIndex = 33;
            this.labelScreenshotDate.Text = "Date:";
            // 
            // textBoxScreenshotDate
            // 
            this.textBoxScreenshotDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxScreenshotDate.Location = new System.Drawing.Point(630, 384);
            this.textBoxScreenshotDate.Name = "textBoxScreenshotDate";
            this.textBoxScreenshotDate.ReadOnly = true;
            this.textBoxScreenshotDate.Size = new System.Drawing.Size(73, 20);
            this.textBoxScreenshotDate.TabIndex = 34;
            this.textBoxScreenshotDate.TabStop = false;
            // 
            // labelScreenshotTime
            // 
            this.labelScreenshotTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelScreenshotTime.AutoSize = true;
            this.labelScreenshotTime.Location = new System.Drawing.Point(709, 387);
            this.labelScreenshotTime.Name = "labelScreenshotTime";
            this.labelScreenshotTime.Size = new System.Drawing.Size(33, 13);
            this.labelScreenshotTime.TabIndex = 35;
            this.labelScreenshotTime.Text = "Time:";
            // 
            // textBoxScreenshotTime
            // 
            this.textBoxScreenshotTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxScreenshotTime.Location = new System.Drawing.Point(748, 384);
            this.textBoxScreenshotTime.Name = "textBoxScreenshotTime";
            this.textBoxScreenshotTime.ReadOnly = true;
            this.textBoxScreenshotTime.Size = new System.Drawing.Size(73, 20);
            this.textBoxScreenshotTime.TabIndex = 36;
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
            this.comboBoxFilterValue.Location = new System.Drawing.Point(133, 6);
            this.comboBoxFilterValue.Name = "comboBoxFilterValue";
            this.comboBoxFilterValue.Size = new System.Drawing.Size(88, 21);
            this.comboBoxFilterValue.TabIndex = 37;
            this.comboBoxFilterValue.TabStop = false;
            this.comboBoxFilterValue.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterValue_SelectedIndexChanged);
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(1, 9);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(32, 13);
            this.labelFilter.TabIndex = 38;
            this.labelFilter.Text = "Filter:";
            // 
            // buttonRefreshFilterValues
            // 
            this.buttonRefreshFilterValues.BackColor = System.Drawing.Color.White;
            this.buttonRefreshFilterValues.Enabled = false;
            this.buttonRefreshFilterValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefreshFilterValues.Image = global::AutoScreenCapture.Properties.Resources.refresh;
            this.buttonRefreshFilterValues.Location = new System.Drawing.Point(227, 6);
            this.buttonRefreshFilterValues.Name = "buttonRefreshFilterValues";
            this.buttonRefreshFilterValues.Size = new System.Drawing.Size(21, 21);
            this.buttonRefreshFilterValues.TabIndex = 39;
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
            "Region",
            "Screen",
            "Window Title"});
            this.comboBoxFilterType.Location = new System.Drawing.Point(39, 6);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(88, 21);
            this.comboBoxFilterType.TabIndex = 40;
            this.comboBoxFilterType.TabStop = false;
            this.comboBoxFilterType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterType_SelectedIndexChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 458);
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
            this.tabPageInterval.ResumeLayout(false);
            this.tabPageInterval.PerformLayout();
            this.groupBoxCaptureDelay.ResumeLayout(false);
            this.groupBoxCaptureDelay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            this.tabPageSchedule.ResumeLayout(false);
            this.groupBoxSchedule.ResumeLayout(false);
            this.groupBoxSchedule.PerformLayout();
            this.tabPageScreenshots.ResumeLayout(false);
            this.tabPageScreenshots.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeepScreenshotsForDays)).EndInit();
            this.tabPageSecurity.ResumeLayout(false);
            this.groupBoxSecurity.ResumeLayout(false);
            this.groupBoxSecurity.PerformLayout();
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorCapture;
        private System.Windows.Forms.TabControl tabControlModules;
        private System.Windows.Forms.TabPage tabPageInterval;
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
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonOptions;
        private System.Windows.Forms.Timer timerScheduledCaptureStart;
        private System.Windows.Forms.Timer timerScheduledCaptureStop;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorAbout;
        private System.Windows.Forms.Timer timerScreenCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowSystemTrayIcon;
        private System.Windows.Forms.TabPage tabPageEditors;
        private System.Windows.Forms.TabPage tabPageTriggers;
        private System.Windows.Forms.TabPage tabPageRegions;
        private System.Windows.Forms.TabPage tabPageScreens;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.TabPage tabPageSecurity;
        private System.Windows.Forms.GroupBox groupBoxSchedule;
        private System.Windows.Forms.CheckBox checkBoxScheduleOnTheseDays;
        private System.Windows.Forms.CheckBox checkBoxFriday;
        private System.Windows.Forms.CheckBox checkBoxThursday;
        private System.Windows.Forms.CheckBox checkBoxWednesday;
        private System.Windows.Forms.CheckBox checkBoxTuesday;
        private System.Windows.Forms.CheckBox checkBoxMonday;
        private System.Windows.Forms.CheckBox checkBoxSunday;
        private System.Windows.Forms.CheckBox checkBoxSaturday;
        private System.Windows.Forms.DateTimePicker dateTimePickerScheduleStopAt;
        private System.Windows.Forms.DateTimePicker dateTimePickerScheduleStartAt;
        private System.Windows.Forms.CheckBox checkBoxScheduleStopAt;
        private System.Windows.Forms.CheckBox checkBoxScheduleStartAt;
        private System.Windows.Forms.GroupBox groupBoxSecurity;
        private System.Windows.Forms.CheckBox checkBoxPassphraseLock;
        private System.Windows.Forms.Button buttonClearPassphrase;
        private System.Windows.Forms.Label labelPasswordDescription;
        private System.Windows.Forms.Button buttonSetPassphrase;
        private System.Windows.Forms.TextBox textBoxPassphrase;
        private System.Windows.Forms.Timer timerDeleteOldScreenshots;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
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
        private ToolStripSplitButton toolStripSplitButtonSaveSettings;
        private ComboBox comboBoxFilterType;
        private Label labelDays;
        private NumericUpDown numericUpDownKeepScreenshotsForDays;
        private Label labelKeepScreenshots;
        private TextBox textBoxScreenshotLabel;
        private Label labelScreenshotLabel;
    }
}