namespace autoscreen
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
            this.toolStripDropDownButtonOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItemDemoModeAtApplicationStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemScheduleAtApplicationStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemOpenAtApplicationStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenOnStopScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemShowSlideshowOnStopScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSearchOnStopScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExitOnCloseWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripLabelDemo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelSchedule = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripLabelLastCapture = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControlScreens = new System.Windows.Forms.TabControl();
            this.tabPageAllScreens = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelScreenshotPreview = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxScreenshotPreviewMonitor1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxScreenshotPreviewMonitor4 = new System.Windows.Forms.PictureBox();
            this.pictureBoxScreenshotPreviewMonitor2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxScreenshotPreviewMonitor3 = new System.Windows.Forms.PictureBox();
            this.tabPageScreen1 = new System.Windows.Forms.TabPage();
            this.pictureBoxScreen1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStripScreenshotPreview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemOpenFileLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.editWithToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageScreen2 = new System.Windows.Forms.TabPage();
            this.pictureBoxScreen2 = new System.Windows.Forms.PictureBox();
            this.tabPageScreen3 = new System.Windows.Forms.TabPage();
            this.pictureBoxScreen3 = new System.Windows.Forms.PictureBox();
            this.tabPageScreen4 = new System.Windows.Forms.TabPage();
            this.pictureBoxScreen4 = new System.Windows.Forms.PictureBox();
            this.tabPageActiveWindow = new System.Windows.Forms.TabPage();
            this.pictureBoxActiveWindow = new System.Windows.Forms.PictureBox();
            this.listBoxScreenshots = new System.Windows.Forms.ListBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripSystemTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemStartScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStopScreenCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxScreenshotsFolderSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonBrowseFolder = new System.Windows.Forms.Button();
            this.toolStripSlideshow = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxImageFormatFilter = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonFirstSlide = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPreviousSlide = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPlaySlideshow = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextSlide = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLastSlide = new System.Windows.Forms.ToolStripButton();
            this.tabControlModules = new System.Windows.Forms.TabControl();
            this.tabPageScreenCapture = new System.Windows.Forms.TabPage();
            this.groupBoxCaptureDelay = new System.Windows.Forms.GroupBox();
            this.checkBoxDemoMode = new System.Windows.Forms.CheckBox();
            this.labelLimit = new System.Windows.Forms.Label();
            this.checkBoxInitialScreenshot = new System.Windows.Forms.CheckBox();
            this.numericUpDownImageResolutionRatio = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCaptureLimit = new System.Windows.Forms.NumericUpDown();
            this.labelPercentResolution = new System.Windows.Forms.Label();
            this.labelAt = new System.Windows.Forms.Label();
            this.checkBoxCaptureLimit = new System.Windows.Forms.CheckBox();
            this.labelMillisecondsInterval = new System.Windows.Forms.Label();
            this.numericUpDownMillisecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.labelSecondsInterval = new System.Windows.Forms.Label();
            this.labelMinutesInterval = new System.Windows.Forms.Label();
            this.labelHoursInterval = new System.Windows.Forms.Label();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBoxSchedule = new System.Windows.Forms.GroupBox();
            this.checkBoxScheduleStartOnSchedule = new System.Windows.Forms.CheckBox();
            this.comboBoxScheduleImageFormat = new System.Windows.Forms.ComboBox();
            this.buttonScheduleClear = new System.Windows.Forms.Button();
            this.buttonScheduleSet = new System.Windows.Forms.Button();
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
            this.tabPageSlideshow = new System.Windows.Forms.TabPage();
            this.groupBoxSlideshowDelay = new System.Windows.Forms.GroupBox();
            this.labelSlideshowDelayMilliseconds = new System.Windows.Forms.Label();
            this.numericUpDownSlideshowDelayMilliseconds = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSlideSkip = new System.Windows.Forms.NumericUpDown();
            this.labelSlideshowDelaySeconds = new System.Windows.Forms.Label();
            this.labelSlideshowDelayMinutes = new System.Windows.Forms.Label();
            this.checkBoxSlideSkip = new System.Windows.Forms.CheckBox();
            this.labelSlideshowDelayHours = new System.Windows.Forms.Label();
            this.numericUpDownSlideshowDelaySeconds = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSlideshowDelayMinutes = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSlideshowDelayHours = new System.Windows.Forms.NumericUpDown();
            this.tabPageKeylogger = new System.Windows.Forms.TabPage();
            this.labelKeyloggingFile = new System.Windows.Forms.Label();
            this.textBoxKeyloggingFile = new System.Windows.Forms.TextBox();
            this.toolStripScreenCapture = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButtonStartScreenCapture = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripButtonStopScreenCapture = new System.Windows.Forms.ToolStripButton();
            this.buttonOpenFolder = new System.Windows.Forms.Button();
            this.timerDemoCapture = new System.Windows.Forms.Timer(this.components);
            this.timerScheduledCaptureStart = new System.Windows.Forms.Timer(this.components);
            this.timerScheduledCaptureStop = new System.Windows.Forms.Timer(this.components);
            this.checkBoxEnableKeylogging = new System.Windows.Forms.CheckBox();
            this.toolStripMenuItemCloseWindowOnStartCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.tabControlScreens.SuspendLayout();
            this.tabPageAllScreens.SuspendLayout();
            this.tableLayoutPanelScreenshotPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshotPreviewMonitor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshotPreviewMonitor4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshotPreviewMonitor2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshotPreviewMonitor3)).BeginInit();
            this.tabPageScreen1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen1)).BeginInit();
            this.contextMenuStripScreenshotPreview.SuspendLayout();
            this.tabPageScreen2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen2)).BeginInit();
            this.tabPageScreen3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen3)).BeginInit();
            this.tabPageScreen4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen4)).BeginInit();
            this.tabPageActiveWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActiveWindow)).BeginInit();
            this.contextMenuStripSystemTrayIcon.SuspendLayout();
            this.toolStripSlideshow.SuspendLayout();
            this.tabControlModules.SuspendLayout();
            this.tabPageScreenCapture.SuspendLayout();
            this.groupBoxCaptureDelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageResolutionRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            this.groupBoxSchedule.SuspendLayout();
            this.tabPageSlideshow.SuspendLayout();
            this.groupBoxSlideshowDelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayMilliseconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelaySeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayHours)).BeginInit();
            this.tabPageKeylogger.SuspendLayout();
            this.toolStripScreenCapture.SuspendLayout();
            this.SuspendLayout();
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(0, 1);
            this.monthCalendar.MaxSelectionCount = 1;
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.ShowWeekNumbers = true;
            this.monthCalendar.TabIndex = 0;
            this.monthCalendar.TabStop = false;
            this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonOptions,
            this.statusStripLabelDemo,
            this.statusStripLabelSchedule,
            this.statusStripLabelLastCapture});
            this.statusStrip.Location = new System.Drawing.Point(0, 374);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(752, 24);
            this.statusStrip.TabIndex = 3;
            // 
            // toolStripDropDownButtonOptions
            // 
            this.toolStripDropDownButtonOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDemoModeAtApplicationStartup,
            this.toolStripMenuItemScheduleAtApplicationStartup,
            this.toolStripSeparator7,
            this.toolStripMenuItemOpenAtApplicationStartup,
            this.toolStripMenuItemOpenOnStopScreenCapture,
            this.toolStripMenuItemCloseWindowOnStartCapture,
            this.toolStripSeparator6,
            this.toolStripMenuItemShowSlideshowOnStopScreenCapture,
            this.toolStripMenuItemSearchOnStopScreenCapture,
            this.toolStripSeparator5,
            this.toolStripMenuItemExitOnCloseWindow});
            this.toolStripDropDownButtonOptions.Image = global::autoscreen.Properties.Resources.options;
            this.toolStripDropDownButtonOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonOptions.Name = "toolStripDropDownButtonOptions";
            this.toolStripDropDownButtonOptions.Size = new System.Drawing.Size(78, 22);
            this.toolStripDropDownButtonOptions.Text = "Options";
            // 
            // toolStripMenuItemDemoModeAtApplicationStartup
            // 
            this.toolStripMenuItemDemoModeAtApplicationStartup.CheckOnClick = true;
            this.toolStripMenuItemDemoModeAtApplicationStartup.Name = "toolStripMenuItemDemoModeAtApplicationStartup";
            this.toolStripMenuItemDemoModeAtApplicationStartup.Size = new System.Drawing.Size(455, 22);
            this.toolStripMenuItemDemoModeAtApplicationStartup.Text = "Turn on demo mode at application startup";
            // 
            // toolStripMenuItemScheduleAtApplicationStartup
            // 
            this.toolStripMenuItemScheduleAtApplicationStartup.CheckOnClick = true;
            this.toolStripMenuItemScheduleAtApplicationStartup.Name = "toolStripMenuItemScheduleAtApplicationStartup";
            this.toolStripMenuItemScheduleAtApplicationStartup.Size = new System.Drawing.Size(455, 22);
            this.toolStripMenuItemScheduleAtApplicationStartup.Text = "Turn on scheduled screen capturing at application startup";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(452, 6);
            // 
            // toolStripMenuItemOpenAtApplicationStartup
            // 
            this.toolStripMenuItemOpenAtApplicationStartup.CheckOnClick = true;
            this.toolStripMenuItemOpenAtApplicationStartup.Name = "toolStripMenuItemOpenAtApplicationStartup";
            this.toolStripMenuItemOpenAtApplicationStartup.Size = new System.Drawing.Size(455, 22);
            this.toolStripMenuItemOpenAtApplicationStartup.Text = "Open this window at application startup";
            // 
            // toolStripMenuItemOpenOnStopScreenCapture
            // 
            this.toolStripMenuItemOpenOnStopScreenCapture.CheckOnClick = true;
            this.toolStripMenuItemOpenOnStopScreenCapture.Name = "toolStripMenuItemOpenOnStopScreenCapture";
            this.toolStripMenuItemOpenOnStopScreenCapture.Size = new System.Drawing.Size(455, 22);
            this.toolStripMenuItemOpenOnStopScreenCapture.Text = "Open this window after stopping the running screen capture session";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(452, 6);
            // 
            // toolStripMenuItemShowSlideshowOnStopScreenCapture
            // 
            this.toolStripMenuItemShowSlideshowOnStopScreenCapture.CheckOnClick = true;
            this.toolStripMenuItemShowSlideshowOnStopScreenCapture.Name = "toolStripMenuItemShowSlideshowOnStopScreenCapture";
            this.toolStripMenuItemShowSlideshowOnStopScreenCapture.Size = new System.Drawing.Size(455, 22);
            this.toolStripMenuItemShowSlideshowOnStopScreenCapture.Text = "Show the slideshow after stopping the running screen capture session";
            // 
            // toolStripMenuItemSearchOnStopScreenCapture
            // 
            this.toolStripMenuItemSearchOnStopScreenCapture.CheckOnClick = true;
            this.toolStripMenuItemSearchOnStopScreenCapture.Name = "toolStripMenuItemSearchOnStopScreenCapture";
            this.toolStripMenuItemSearchOnStopScreenCapture.Size = new System.Drawing.Size(455, 22);
            this.toolStripMenuItemSearchOnStopScreenCapture.Text = "Search for screenshots after stopping the running screen capture session";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(452, 6);
            // 
            // toolStripMenuItemExitOnCloseWindow
            // 
            this.toolStripMenuItemExitOnCloseWindow.CheckOnClick = true;
            this.toolStripMenuItemExitOnCloseWindow.Name = "toolStripMenuItemExitOnCloseWindow";
            this.toolStripMenuItemExitOnCloseWindow.Size = new System.Drawing.Size(455, 22);
            this.toolStripMenuItemExitOnCloseWindow.Text = "Exit application when closing this window";
            // 
            // statusStripLabelDemo
            // 
            this.statusStripLabelDemo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusStripLabelDemo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.statusStripLabelDemo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelDemo.Name = "statusStripLabelDemo";
            this.statusStripLabelDemo.Size = new System.Drawing.Size(66, 19);
            this.statusStripLabelDemo.Text = "Demo: Off";
            // 
            // statusStripLabelSchedule
            // 
            this.statusStripLabelSchedule.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusStripLabelSchedule.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.statusStripLabelSchedule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelSchedule.Name = "statusStripLabelSchedule";
            this.statusStripLabelSchedule.Size = new System.Drawing.Size(82, 19);
            this.statusStripLabelSchedule.Text = "Schedule: Off";
            // 
            // statusStripLabelLastCapture
            // 
            this.statusStripLabelLastCapture.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusStripLabelLastCapture.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.statusStripLabelLastCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusStripLabelLastCapture.Name = "statusStripLabelLastCapture";
            this.statusStripLabelLastCapture.Size = new System.Drawing.Size(116, 19);
            this.statusStripLabelLastCapture.Text = "Last capture: (none)";
            // 
            // tabControlScreens
            // 
            this.tabControlScreens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlScreens.Controls.Add(this.tabPageAllScreens);
            this.tabControlScreens.Controls.Add(this.tabPageScreen1);
            this.tabControlScreens.Controls.Add(this.tabPageScreen2);
            this.tabControlScreens.Controls.Add(this.tabPageScreen3);
            this.tabControlScreens.Controls.Add(this.tabPageScreen4);
            this.tabControlScreens.Controls.Add(this.tabPageActiveWindow);
            this.tabControlScreens.Location = new System.Drawing.Point(251, 1);
            this.tabControlScreens.Name = "tabControlScreens";
            this.tabControlScreens.SelectedIndex = 0;
            this.tabControlScreens.Size = new System.Drawing.Size(501, 345);
            this.tabControlScreens.TabIndex = 4;
            this.tabControlScreens.SelectedIndexChanged += new System.EventHandler(this.tabControlScreens_SelectedIndexChanged);
            // 
            // tabPageAllScreens
            // 
            this.tabPageAllScreens.Controls.Add(this.tableLayoutPanelScreenshotPreview);
            this.tabPageAllScreens.Location = new System.Drawing.Point(4, 22);
            this.tabPageAllScreens.Name = "tabPageAllScreens";
            this.tabPageAllScreens.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAllScreens.Size = new System.Drawing.Size(493, 319);
            this.tabPageAllScreens.TabIndex = 0;
            this.tabPageAllScreens.Text = "All Screens";
            this.tabPageAllScreens.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelScreenshotPreview
            // 
            this.tableLayoutPanelScreenshotPreview.BackColor = System.Drawing.Color.Gray;
            this.tableLayoutPanelScreenshotPreview.ColumnCount = 2;
            this.tableLayoutPanelScreenshotPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelScreenshotPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelScreenshotPreview.Controls.Add(this.pictureBoxScreenshotPreviewMonitor1, 0, 0);
            this.tableLayoutPanelScreenshotPreview.Controls.Add(this.pictureBoxScreenshotPreviewMonitor4, 1, 1);
            this.tableLayoutPanelScreenshotPreview.Controls.Add(this.pictureBoxScreenshotPreviewMonitor2, 1, 0);
            this.tableLayoutPanelScreenshotPreview.Controls.Add(this.pictureBoxScreenshotPreviewMonitor3, 0, 1);
            this.tableLayoutPanelScreenshotPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelScreenshotPreview.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelScreenshotPreview.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanelScreenshotPreview.Name = "tableLayoutPanelScreenshotPreview";
            this.tableLayoutPanelScreenshotPreview.RowCount = 2;
            this.tableLayoutPanelScreenshotPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelScreenshotPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelScreenshotPreview.Size = new System.Drawing.Size(487, 313);
            this.tableLayoutPanelScreenshotPreview.TabIndex = 4;
            // 
            // pictureBoxScreenshotPreviewMonitor1
            // 
            this.pictureBoxScreenshotPreviewMonitor1.BackColor = System.Drawing.Color.Black;
            this.pictureBoxScreenshotPreviewMonitor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScreenshotPreviewMonitor1.Location = new System.Drawing.Point(1, 1);
            this.pictureBoxScreenshotPreviewMonitor1.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBoxScreenshotPreviewMonitor1.Name = "pictureBoxScreenshotPreviewMonitor1";
            this.pictureBoxScreenshotPreviewMonitor1.Size = new System.Drawing.Size(241, 154);
            this.pictureBoxScreenshotPreviewMonitor1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreenshotPreviewMonitor1.TabIndex = 1;
            this.pictureBoxScreenshotPreviewMonitor1.TabStop = false;
            // 
            // pictureBoxScreenshotPreviewMonitor4
            // 
            this.pictureBoxScreenshotPreviewMonitor4.BackColor = System.Drawing.Color.Black;
            this.pictureBoxScreenshotPreviewMonitor4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScreenshotPreviewMonitor4.Location = new System.Drawing.Point(244, 157);
            this.pictureBoxScreenshotPreviewMonitor4.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBoxScreenshotPreviewMonitor4.Name = "pictureBoxScreenshotPreviewMonitor4";
            this.pictureBoxScreenshotPreviewMonitor4.Size = new System.Drawing.Size(242, 155);
            this.pictureBoxScreenshotPreviewMonitor4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreenshotPreviewMonitor4.TabIndex = 3;
            this.pictureBoxScreenshotPreviewMonitor4.TabStop = false;
            // 
            // pictureBoxScreenshotPreviewMonitor2
            // 
            this.pictureBoxScreenshotPreviewMonitor2.BackColor = System.Drawing.Color.Black;
            this.pictureBoxScreenshotPreviewMonitor2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScreenshotPreviewMonitor2.Location = new System.Drawing.Point(244, 1);
            this.pictureBoxScreenshotPreviewMonitor2.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBoxScreenshotPreviewMonitor2.Name = "pictureBoxScreenshotPreviewMonitor2";
            this.pictureBoxScreenshotPreviewMonitor2.Size = new System.Drawing.Size(242, 154);
            this.pictureBoxScreenshotPreviewMonitor2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreenshotPreviewMonitor2.TabIndex = 2;
            this.pictureBoxScreenshotPreviewMonitor2.TabStop = false;
            // 
            // pictureBoxScreenshotPreviewMonitor3
            // 
            this.pictureBoxScreenshotPreviewMonitor3.BackColor = System.Drawing.Color.Black;
            this.pictureBoxScreenshotPreviewMonitor3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScreenshotPreviewMonitor3.Location = new System.Drawing.Point(1, 157);
            this.pictureBoxScreenshotPreviewMonitor3.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBoxScreenshotPreviewMonitor3.Name = "pictureBoxScreenshotPreviewMonitor3";
            this.pictureBoxScreenshotPreviewMonitor3.Size = new System.Drawing.Size(241, 155);
            this.pictureBoxScreenshotPreviewMonitor3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreenshotPreviewMonitor3.TabIndex = 2;
            this.pictureBoxScreenshotPreviewMonitor3.TabStop = false;
            // 
            // tabPageScreen1
            // 
            this.tabPageScreen1.Controls.Add(this.pictureBoxScreen1);
            this.tabPageScreen1.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreen1.Name = "tabPageScreen1";
            this.tabPageScreen1.Size = new System.Drawing.Size(493, 319);
            this.tabPageScreen1.TabIndex = 3;
            this.tabPageScreen1.Text = "Screen 1";
            this.tabPageScreen1.UseVisualStyleBackColor = true;
            // 
            // pictureBoxScreen1
            // 
            this.pictureBoxScreen1.BackColor = System.Drawing.Color.Black;
            this.pictureBoxScreen1.ContextMenuStrip = this.contextMenuStripScreenshotPreview;
            this.pictureBoxScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScreen1.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxScreen1.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBoxScreen1.Name = "pictureBoxScreen1";
            this.pictureBoxScreen1.Size = new System.Drawing.Size(493, 319);
            this.pictureBoxScreen1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreen1.TabIndex = 2;
            this.pictureBoxScreen1.TabStop = false;
            // 
            // contextMenuStripScreenshotPreview
            // 
            this.contextMenuStripScreenshotPreview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenFileLocation,
            this.toolStripSeparator4,
            this.editWithToolStripMenuItem});
            this.contextMenuStripScreenshotPreview.Name = "contextMenuStripScreenshotPreview";
            this.contextMenuStripScreenshotPreview.Size = new System.Drawing.Size(174, 54);
            // 
            // toolStripMenuItemOpenFileLocation
            // 
            this.toolStripMenuItemOpenFileLocation.Name = "toolStripMenuItemOpenFileLocation";
            this.toolStripMenuItemOpenFileLocation.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItemOpenFileLocation.Text = "Open File Location";
            this.toolStripMenuItemOpenFileLocation.Click += new System.EventHandler(this.toolStripMenuItemOpenFileLocation_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(170, 6);
            // 
            // editWithToolStripMenuItem
            // 
            this.editWithToolStripMenuItem.Name = "editWithToolStripMenuItem";
            this.editWithToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.editWithToolStripMenuItem.Text = "Edit With";
            // 
            // tabPageScreen2
            // 
            this.tabPageScreen2.Controls.Add(this.pictureBoxScreen2);
            this.tabPageScreen2.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreen2.Name = "tabPageScreen2";
            this.tabPageScreen2.Size = new System.Drawing.Size(493, 319);
            this.tabPageScreen2.TabIndex = 4;
            this.tabPageScreen2.Text = "Screen 2";
            this.tabPageScreen2.UseVisualStyleBackColor = true;
            // 
            // pictureBoxScreen2
            // 
            this.pictureBoxScreen2.BackColor = System.Drawing.Color.Black;
            this.pictureBoxScreen2.ContextMenuStrip = this.contextMenuStripScreenshotPreview;
            this.pictureBoxScreen2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScreen2.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxScreen2.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBoxScreen2.Name = "pictureBoxScreen2";
            this.pictureBoxScreen2.Size = new System.Drawing.Size(493, 319);
            this.pictureBoxScreen2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreen2.TabIndex = 3;
            this.pictureBoxScreen2.TabStop = false;
            // 
            // tabPageScreen3
            // 
            this.tabPageScreen3.Controls.Add(this.pictureBoxScreen3);
            this.tabPageScreen3.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreen3.Name = "tabPageScreen3";
            this.tabPageScreen3.Size = new System.Drawing.Size(493, 319);
            this.tabPageScreen3.TabIndex = 5;
            this.tabPageScreen3.Text = "Screen 3";
            this.tabPageScreen3.UseVisualStyleBackColor = true;
            // 
            // pictureBoxScreen3
            // 
            this.pictureBoxScreen3.BackColor = System.Drawing.Color.Black;
            this.pictureBoxScreen3.ContextMenuStrip = this.contextMenuStripScreenshotPreview;
            this.pictureBoxScreen3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScreen3.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxScreen3.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBoxScreen3.Name = "pictureBoxScreen3";
            this.pictureBoxScreen3.Size = new System.Drawing.Size(493, 319);
            this.pictureBoxScreen3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreen3.TabIndex = 3;
            this.pictureBoxScreen3.TabStop = false;
            // 
            // tabPageScreen4
            // 
            this.tabPageScreen4.Controls.Add(this.pictureBoxScreen4);
            this.tabPageScreen4.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreen4.Name = "tabPageScreen4";
            this.tabPageScreen4.Size = new System.Drawing.Size(493, 319);
            this.tabPageScreen4.TabIndex = 6;
            this.tabPageScreen4.Text = "Screen 4";
            this.tabPageScreen4.UseVisualStyleBackColor = true;
            // 
            // pictureBoxScreen4
            // 
            this.pictureBoxScreen4.BackColor = System.Drawing.Color.Black;
            this.pictureBoxScreen4.ContextMenuStrip = this.contextMenuStripScreenshotPreview;
            this.pictureBoxScreen4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScreen4.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxScreen4.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBoxScreen4.Name = "pictureBoxScreen4";
            this.pictureBoxScreen4.Size = new System.Drawing.Size(493, 319);
            this.pictureBoxScreen4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreen4.TabIndex = 3;
            this.pictureBoxScreen4.TabStop = false;
            // 
            // tabPageActiveWindow
            // 
            this.tabPageActiveWindow.Controls.Add(this.pictureBoxActiveWindow);
            this.tabPageActiveWindow.Location = new System.Drawing.Point(4, 22);
            this.tabPageActiveWindow.Name = "tabPageActiveWindow";
            this.tabPageActiveWindow.Size = new System.Drawing.Size(493, 319);
            this.tabPageActiveWindow.TabIndex = 7;
            this.tabPageActiveWindow.Text = "Active Window";
            this.tabPageActiveWindow.UseVisualStyleBackColor = true;
            // 
            // pictureBoxActiveWindow
            // 
            this.pictureBoxActiveWindow.BackColor = System.Drawing.Color.Black;
            this.pictureBoxActiveWindow.ContextMenuStrip = this.contextMenuStripScreenshotPreview;
            this.pictureBoxActiveWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxActiveWindow.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxActiveWindow.Name = "pictureBoxActiveWindow";
            this.pictureBoxActiveWindow.Size = new System.Drawing.Size(493, 319);
            this.pictureBoxActiveWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxActiveWindow.TabIndex = 0;
            this.pictureBoxActiveWindow.TabStop = false;
            // 
            // listBoxScreenshots
            // 
            this.listBoxScreenshots.FormattingEnabled = true;
            this.listBoxScreenshots.IntegralHeight = false;
            this.listBoxScreenshots.Location = new System.Drawing.Point(8, 3);
            this.listBoxScreenshots.MaximumSize = new System.Drawing.Size(204, 123);
            this.listBoxScreenshots.MinimumSize = new System.Drawing.Size(204, 123);
            this.listBoxScreenshots.Name = "listBoxScreenshots";
            this.listBoxScreenshots.ScrollAlwaysVisible = true;
            this.listBoxScreenshots.Size = new System.Drawing.Size(204, 123);
            this.listBoxScreenshots.Sorted = true;
            this.listBoxScreenshots.TabIndex = 6;
            this.listBoxScreenshots.SelectedIndexChanged += new System.EventHandler(this.listBoxScreenshots_SelectedIndexChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripSystemTrayIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            // 
            // contextMenuStripSystemTrayIcon
            // 
            this.contextMenuStripSystemTrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAbout,
            this.toolStripSeparator2,
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemClose,
            this.toolStripSeparator1,
            this.toolStripMenuItemStartScreenCapture,
            this.toolStripMenuItemStopScreenCapture,
            this.toolStripSeparator3,
            this.toolStripMenuItemExit});
            this.contextMenuStripSystemTrayIcon.Name = "contextMenuStrip";
            this.contextMenuStripSystemTrayIcon.Size = new System.Drawing.Size(151, 154);
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItemAbout.Text = "About ...";
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(147, 6);
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItemOpen.Text = "Open Window";
            this.toolStripMenuItemOpen.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
            // 
            // toolStripMenuItemClose
            // 
            this.toolStripMenuItemClose.Enabled = false;
            this.toolStripMenuItemClose.Name = "toolStripMenuItemClose";
            this.toolStripMenuItemClose.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItemClose.Text = "Close Window";
            this.toolStripMenuItemClose.Click += new System.EventHandler(this.toolStripMenuItemClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // toolStripMenuItemStartScreenCapture
            // 
            this.toolStripMenuItemStartScreenCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItemStartScreenCapture.Name = "toolStripMenuItemStartScreenCapture";
            this.toolStripMenuItemStartScreenCapture.ShowShortcutKeys = false;
            this.toolStripMenuItemStartScreenCapture.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItemStartScreenCapture.Text = "Start Capture";
            // 
            // toolStripMenuItemStopScreenCapture
            // 
            this.toolStripMenuItemStopScreenCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItemStopScreenCapture.Enabled = false;
            this.toolStripMenuItemStopScreenCapture.Name = "toolStripMenuItemStopScreenCapture";
            this.toolStripMenuItemStopScreenCapture.ShowShortcutKeys = false;
            this.toolStripMenuItemStopScreenCapture.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItemStopScreenCapture.Text = "Stop Capture";
            this.toolStripMenuItemStopScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemStopScreenCapture_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(147, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.ShowShortcutKeys = false;
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItemExit.Text = "Exit";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // textBoxScreenshotsFolderSearch
            // 
            this.textBoxScreenshotsFolderSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScreenshotsFolderSearch.Location = new System.Drawing.Point(4, 352);
            this.textBoxScreenshotsFolderSearch.Name = "textBoxScreenshotsFolderSearch";
            this.textBoxScreenshotsFolderSearch.Size = new System.Drawing.Size(504, 20);
            this.textBoxScreenshotsFolderSearch.TabIndex = 7;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(676, 350);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 8;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearchScreenshotsDirectory_Click);
            // 
            // buttonBrowseFolder
            // 
            this.buttonBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseFolder.Location = new System.Drawing.Point(514, 350);
            this.buttonBrowseFolder.Name = "buttonBrowseFolder";
            this.buttonBrowseFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseFolder.TabIndex = 10;
            this.buttonBrowseFolder.Text = "Browse ...";
            this.buttonBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // toolStripSlideshow
            // 
            this.toolStripSlideshow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStripSlideshow.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripSlideshow.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripSlideshow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxImageFormatFilter,
            this.toolStripButtonFirstSlide,
            this.toolStripButtonPreviousSlide,
            this.toolStripButtonPlaySlideshow,
            this.toolStripButtonNextSlide,
            this.toolStripButtonLastSlide});
            this.toolStripSlideshow.Location = new System.Drawing.Point(16, 321);
            this.toolStripSlideshow.Name = "toolStripSlideshow";
            this.toolStripSlideshow.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripSlideshow.Size = new System.Drawing.Size(220, 25);
            this.toolStripSlideshow.TabIndex = 11;
            this.toolStripSlideshow.Visible = false;
            // 
            // toolStripComboBoxImageFormatFilter
            // 
            this.toolStripComboBoxImageFormatFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxImageFormatFilter.Name = "toolStripComboBoxImageFormatFilter";
            this.toolStripComboBoxImageFormatFilter.Size = new System.Drawing.Size(100, 25);
            this.toolStripComboBoxImageFormatFilter.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxImageFormatFilter_SelectedIndexChanged);
            // 
            // toolStripButtonFirstSlide
            // 
            this.toolStripButtonFirstSlide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFirstSlide.Enabled = false;
            this.toolStripButtonFirstSlide.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFirstSlide.Image")));
            this.toolStripButtonFirstSlide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFirstSlide.Name = "toolStripButtonFirstSlide";
            this.toolStripButtonFirstSlide.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonFirstSlide.Click += new System.EventHandler(this.toolStripButtonFirstSlide_Click);
            // 
            // toolStripButtonPreviousSlide
            // 
            this.toolStripButtonPreviousSlide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPreviousSlide.Enabled = false;
            this.toolStripButtonPreviousSlide.Image = global::autoscreen.Properties.Resources.player_rew;
            this.toolStripButtonPreviousSlide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreviousSlide.Name = "toolStripButtonPreviousSlide";
            this.toolStripButtonPreviousSlide.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPreviousSlide.Click += new System.EventHandler(this.toolStripButtonPreviousSlide_Click);
            // 
            // toolStripButtonPlaySlideshow
            // 
            this.toolStripButtonPlaySlideshow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPlaySlideshow.Enabled = false;
            this.toolStripButtonPlaySlideshow.Image = global::autoscreen.Properties.Resources.player_play;
            this.toolStripButtonPlaySlideshow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlaySlideshow.Name = "toolStripButtonPlaySlideshow";
            this.toolStripButtonPlaySlideshow.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPlaySlideshow.Click += new System.EventHandler(this.toolStripButtonPlaySlideshow_Click);
            // 
            // toolStripButtonNextSlide
            // 
            this.toolStripButtonNextSlide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNextSlide.Enabled = false;
            this.toolStripButtonNextSlide.Image = global::autoscreen.Properties.Resources.player_fwd;
            this.toolStripButtonNextSlide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNextSlide.Name = "toolStripButtonNextSlide";
            this.toolStripButtonNextSlide.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNextSlide.Click += new System.EventHandler(this.toolStripButtonNextSlide_Click);
            // 
            // toolStripButtonLastSlide
            // 
            this.toolStripButtonLastSlide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLastSlide.Enabled = false;
            this.toolStripButtonLastSlide.Image = global::autoscreen.Properties.Resources.player_end;
            this.toolStripButtonLastSlide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLastSlide.Name = "toolStripButtonLastSlide";
            this.toolStripButtonLastSlide.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLastSlide.Click += new System.EventHandler(this.toolStripButtonLastSlide_Click);
            // 
            // tabControlModules
            // 
            this.tabControlModules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControlModules.Controls.Add(this.tabPageScreenCapture);
            this.tabControlModules.Controls.Add(this.tabPageSlideshow);
            this.tabControlModules.Controls.Add(this.tabPageKeylogger);
            this.tabControlModules.Location = new System.Drawing.Point(0, 164);
            this.tabControlModules.Multiline = true;
            this.tabControlModules.Name = "tabControlModules";
            this.tabControlModules.SelectedIndex = 0;
            this.tabControlModules.Size = new System.Drawing.Size(249, 154);
            this.tabControlModules.TabIndex = 12;
            this.tabControlModules.SelectedIndexChanged += new System.EventHandler(this.tabControlModules_SelectedIndexChanged);
            // 
            // tabPageScreenCapture
            // 
            this.tabPageScreenCapture.AutoScroll = true;
            this.tabPageScreenCapture.Controls.Add(this.groupBoxCaptureDelay);
            this.tabPageScreenCapture.Controls.Add(this.groupBoxSchedule);
            this.tabPageScreenCapture.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreenCapture.Name = "tabPageScreenCapture";
            this.tabPageScreenCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScreenCapture.Size = new System.Drawing.Size(241, 128);
            this.tabPageScreenCapture.TabIndex = 0;
            this.tabPageScreenCapture.Text = "Screen Capture";
            this.tabPageScreenCapture.UseVisualStyleBackColor = true;
            // 
            // groupBoxCaptureDelay
            // 
            this.groupBoxCaptureDelay.Controls.Add(this.checkBoxDemoMode);
            this.groupBoxCaptureDelay.Controls.Add(this.labelLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.checkBoxInitialScreenshot);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownImageResolutionRatio);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownCaptureLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.labelPercentResolution);
            this.groupBoxCaptureDelay.Controls.Add(this.labelAt);
            this.groupBoxCaptureDelay.Controls.Add(this.checkBoxCaptureLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.labelMillisecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownMillisecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelSecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelMinutesInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelHoursInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownSecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownMinutesInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownHoursInterval);
            this.groupBoxCaptureDelay.Location = new System.Drawing.Point(6, 3);
            this.groupBoxCaptureDelay.Name = "groupBoxCaptureDelay";
            this.groupBoxCaptureDelay.Size = new System.Drawing.Size(205, 122);
            this.groupBoxCaptureDelay.TabIndex = 14;
            this.groupBoxCaptureDelay.TabStop = false;
            this.groupBoxCaptureDelay.Text = "Take screenshots every ...";
            // 
            // checkBoxDemoMode
            // 
            this.checkBoxDemoMode.AutoSize = true;
            this.checkBoxDemoMode.Location = new System.Drawing.Point(127, 98);
            this.checkBoxDemoMode.Name = "checkBoxDemoMode";
            this.checkBoxDemoMode.Size = new System.Drawing.Size(81, 17);
            this.checkBoxDemoMode.TabIndex = 27;
            this.checkBoxDemoMode.Text = "demo mode";
            this.checkBoxDemoMode.UseVisualStyleBackColor = true;
            this.checkBoxDemoMode.CheckedChanged += new System.EventHandler(this.checkBoxDemoMode_CheckedChanged);
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
            this.checkBoxInitialScreenshot.Size = new System.Drawing.Size(88, 17);
            this.checkBoxInitialScreenshot.TabIndex = 18;
            this.checkBoxInitialScreenshot.Text = "initial capture";
            this.checkBoxInitialScreenshot.UseVisualStyleBackColor = true;
            // 
            // numericUpDownImageResolutionRatio
            // 
            this.numericUpDownImageResolutionRatio.Location = new System.Drawing.Point(127, 71);
            this.numericUpDownImageResolutionRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImageResolutionRatio.Name = "numericUpDownImageResolutionRatio";
            this.numericUpDownImageResolutionRatio.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownImageResolutionRatio.TabIndex = 22;
            this.numericUpDownImageResolutionRatio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            // 
            // labelPercentResolution
            // 
            this.labelPercentResolution.AutoSize = true;
            this.labelPercentResolution.Location = new System.Drawing.Point(167, 73);
            this.labelPercentResolution.Name = "labelPercentResolution";
            this.labelPercentResolution.Size = new System.Drawing.Size(35, 13);
            this.labelPercentResolution.TabIndex = 25;
            this.labelPercentResolution.Text = "% res.";
            // 
            // labelAt
            // 
            this.labelAt.AutoSize = true;
            this.labelAt.Location = new System.Drawing.Point(113, 73);
            this.labelAt.Name = "labelAt";
            this.labelAt.Size = new System.Drawing.Size(16, 13);
            this.labelAt.TabIndex = 24;
            this.labelAt.Text = "at";
            // 
            // checkBoxCaptureLimit
            // 
            this.checkBoxCaptureLimit.AutoSize = true;
            this.checkBoxCaptureLimit.Location = new System.Drawing.Point(110, 47);
            this.checkBoxCaptureLimit.Name = "checkBoxCaptureLimit";
            this.checkBoxCaptureLimit.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCaptureLimit.TabIndex = 16;
            this.checkBoxCaptureLimit.UseVisualStyleBackColor = true;
            this.checkBoxCaptureLimit.CheckedChanged += new System.EventHandler(this.checkBoxCaptureLimit_CheckedChanged);
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
            this.numericUpDownMillisecondsInterval.ValueChanged += new System.EventHandler(this.demoInterval_ValueChanged);
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
            this.numericUpDownSecondsInterval.ValueChanged += new System.EventHandler(this.demoInterval_ValueChanged);
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
            this.numericUpDownMinutesInterval.ValueChanged += new System.EventHandler(this.demoInterval_ValueChanged);
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
            this.numericUpDownHoursInterval.ValueChanged += new System.EventHandler(this.demoInterval_ValueChanged);
            // 
            // groupBoxSchedule
            // 
            this.groupBoxSchedule.Controls.Add(this.checkBoxScheduleStartOnSchedule);
            this.groupBoxSchedule.Controls.Add(this.comboBoxScheduleImageFormat);
            this.groupBoxSchedule.Controls.Add(this.buttonScheduleClear);
            this.groupBoxSchedule.Controls.Add(this.buttonScheduleSet);
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
            this.groupBoxSchedule.Location = new System.Drawing.Point(6, 131);
            this.groupBoxSchedule.Name = "groupBoxSchedule";
            this.groupBoxSchedule.Size = new System.Drawing.Size(205, 158);
            this.groupBoxSchedule.TabIndex = 21;
            this.groupBoxSchedule.TabStop = false;
            this.groupBoxSchedule.Text = "Schedule";
            // 
            // checkBoxScheduleStartOnSchedule
            // 
            this.checkBoxScheduleStartOnSchedule.AutoSize = true;
            this.checkBoxScheduleStartOnSchedule.Location = new System.Drawing.Point(6, 19);
            this.checkBoxScheduleStartOnSchedule.Name = "checkBoxScheduleStartOnSchedule";
            this.checkBoxScheduleStartOnSchedule.Size = new System.Drawing.Size(187, 17);
            this.checkBoxScheduleStartOnSchedule.TabIndex = 15;
            this.checkBoxScheduleStartOnSchedule.Text = "Start capture when schedule is on";
            this.checkBoxScheduleStartOnSchedule.UseVisualStyleBackColor = true;
            this.checkBoxScheduleStartOnSchedule.CheckedChanged += new System.EventHandler(this.checkBoxScheduleStartOnSchedule_CheckedChanged);
            // 
            // comboBoxScheduleImageFormat
            // 
            this.comboBoxScheduleImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleImageFormat.FormattingEnabled = true;
            this.comboBoxScheduleImageFormat.Location = new System.Drawing.Point(118, 132);
            this.comboBoxScheduleImageFormat.Name = "comboBoxScheduleImageFormat";
            this.comboBoxScheduleImageFormat.Size = new System.Drawing.Size(80, 21);
            this.comboBoxScheduleImageFormat.TabIndex = 14;
            this.comboBoxScheduleImageFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxScheduleImageFormat_SelectedIndexChanged);
            // 
            // buttonScheduleClear
            // 
            this.buttonScheduleClear.Enabled = false;
            this.buttonScheduleClear.Location = new System.Drawing.Point(62, 131);
            this.buttonScheduleClear.Name = "buttonScheduleClear";
            this.buttonScheduleClear.Size = new System.Drawing.Size(50, 23);
            this.buttonScheduleClear.TabIndex = 13;
            this.buttonScheduleClear.Text = "Off";
            this.buttonScheduleClear.UseVisualStyleBackColor = true;
            this.buttonScheduleClear.Click += new System.EventHandler(this.buttonScheduleClear_Click);
            // 
            // buttonScheduleSet
            // 
            this.buttonScheduleSet.Location = new System.Drawing.Point(6, 131);
            this.buttonScheduleSet.Name = "buttonScheduleSet";
            this.buttonScheduleSet.Size = new System.Drawing.Size(50, 23);
            this.buttonScheduleSet.TabIndex = 12;
            this.buttonScheduleSet.Text = "On";
            this.buttonScheduleSet.UseVisualStyleBackColor = true;
            this.buttonScheduleSet.Click += new System.EventHandler(this.buttonScheduleSet_Click);
            // 
            // checkBoxScheduleOnTheseDays
            // 
            this.checkBoxScheduleOnTheseDays.AutoSize = true;
            this.checkBoxScheduleOnTheseDays.Location = new System.Drawing.Point(6, 87);
            this.checkBoxScheduleOnTheseDays.Name = "checkBoxScheduleOnTheseDays";
            this.checkBoxScheduleOnTheseDays.Size = new System.Drawing.Size(119, 17);
            this.checkBoxScheduleOnTheseDays.TabIndex = 11;
            this.checkBoxScheduleOnTheseDays.Text = "Only on these days:";
            this.checkBoxScheduleOnTheseDays.UseVisualStyleBackColor = true;
            this.checkBoxScheduleOnTheseDays.CheckedChanged += new System.EventHandler(this.checkBoxScheduleOnTheseDays_CheckedChanged);
            // 
            // checkBoxFriday
            // 
            this.checkBoxFriday.AutoSize = true;
            this.checkBoxFriday.Enabled = false;
            this.checkBoxFriday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxFriday.Location = new System.Drawing.Point(165, 110);
            this.checkBoxFriday.Name = "checkBoxFriday";
            this.checkBoxFriday.Size = new System.Drawing.Size(33, 16);
            this.checkBoxFriday.TabIndex = 10;
            this.checkBoxFriday.Text = "Fr";
            this.checkBoxFriday.UseVisualStyleBackColor = true;
            // 
            // checkBoxThursday
            // 
            this.checkBoxThursday.AutoSize = true;
            this.checkBoxThursday.Enabled = false;
            this.checkBoxThursday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxThursday.Location = new System.Drawing.Point(130, 110);
            this.checkBoxThursday.Name = "checkBoxThursday";
            this.checkBoxThursday.Size = new System.Drawing.Size(34, 16);
            this.checkBoxThursday.TabIndex = 9;
            this.checkBoxThursday.Text = "Th";
            this.checkBoxThursday.UseVisualStyleBackColor = true;
            // 
            // checkBoxWednesday
            // 
            this.checkBoxWednesday.AutoSize = true;
            this.checkBoxWednesday.Enabled = false;
            this.checkBoxWednesday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxWednesday.Location = new System.Drawing.Point(92, 110);
            this.checkBoxWednesday.Name = "checkBoxWednesday";
            this.checkBoxWednesday.Size = new System.Drawing.Size(38, 16);
            this.checkBoxWednesday.TabIndex = 8;
            this.checkBoxWednesday.Text = "We";
            this.checkBoxWednesday.UseVisualStyleBackColor = true;
            // 
            // checkBoxTuesday
            // 
            this.checkBoxTuesday.AutoSize = true;
            this.checkBoxTuesday.Enabled = false;
            this.checkBoxTuesday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTuesday.Location = new System.Drawing.Point(58, 110);
            this.checkBoxTuesday.Name = "checkBoxTuesday";
            this.checkBoxTuesday.Size = new System.Drawing.Size(34, 16);
            this.checkBoxTuesday.TabIndex = 7;
            this.checkBoxTuesday.Text = "Tu";
            this.checkBoxTuesday.UseVisualStyleBackColor = true;
            // 
            // checkBoxMonday
            // 
            this.checkBoxMonday.AutoSize = true;
            this.checkBoxMonday.Enabled = false;
            this.checkBoxMonday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMonday.Location = new System.Drawing.Point(20, 110);
            this.checkBoxMonday.Name = "checkBoxMonday";
            this.checkBoxMonday.Size = new System.Drawing.Size(38, 16);
            this.checkBoxMonday.TabIndex = 6;
            this.checkBoxMonday.Text = "Mo";
            this.checkBoxMonday.UseVisualStyleBackColor = true;
            // 
            // checkBoxSunday
            // 
            this.checkBoxSunday.AutoSize = true;
            this.checkBoxSunday.Enabled = false;
            this.checkBoxSunday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSunday.Location = new System.Drawing.Point(165, 88);
            this.checkBoxSunday.Name = "checkBoxSunday";
            this.checkBoxSunday.Size = new System.Drawing.Size(35, 16);
            this.checkBoxSunday.TabIndex = 5;
            this.checkBoxSunday.Text = "Su";
            this.checkBoxSunday.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaturday
            // 
            this.checkBoxSaturday.AutoSize = true;
            this.checkBoxSaturday.Enabled = false;
            this.checkBoxSaturday.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSaturday.Location = new System.Drawing.Point(130, 88);
            this.checkBoxSaturday.Name = "checkBoxSaturday";
            this.checkBoxSaturday.Size = new System.Drawing.Size(35, 16);
            this.checkBoxSaturday.TabIndex = 4;
            this.checkBoxSaturday.Text = "Sa";
            this.checkBoxSaturday.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerScheduleStopAt
            // 
            this.dateTimePickerScheduleStopAt.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerScheduleStopAt.Location = new System.Drawing.Point(105, 63);
            this.dateTimePickerScheduleStopAt.Name = "dateTimePickerScheduleStopAt";
            this.dateTimePickerScheduleStopAt.ShowUpDown = true;
            this.dateTimePickerScheduleStopAt.Size = new System.Drawing.Size(93, 20);
            this.dateTimePickerScheduleStopAt.TabIndex = 3;
            // 
            // dateTimePickerScheduleStartAt
            // 
            this.dateTimePickerScheduleStartAt.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerScheduleStartAt.Location = new System.Drawing.Point(105, 40);
            this.dateTimePickerScheduleStartAt.Name = "dateTimePickerScheduleStartAt";
            this.dateTimePickerScheduleStartAt.ShowUpDown = true;
            this.dateTimePickerScheduleStartAt.Size = new System.Drawing.Size(93, 20);
            this.dateTimePickerScheduleStartAt.TabIndex = 2;
            // 
            // checkBoxScheduleStopAt
            // 
            this.checkBoxScheduleStopAt.AutoSize = true;
            this.checkBoxScheduleStopAt.Location = new System.Drawing.Point(6, 65);
            this.checkBoxScheduleStopAt.Name = "checkBoxScheduleStopAt";
            this.checkBoxScheduleStopAt.Size = new System.Drawing.Size(99, 17);
            this.checkBoxScheduleStopAt.TabIndex = 1;
            this.checkBoxScheduleStopAt.Text = "Stop capture at";
            this.checkBoxScheduleStopAt.UseVisualStyleBackColor = true;
            // 
            // checkBoxScheduleStartAt
            // 
            this.checkBoxScheduleStartAt.AutoSize = true;
            this.checkBoxScheduleStartAt.Location = new System.Drawing.Point(6, 42);
            this.checkBoxScheduleStartAt.Name = "checkBoxScheduleStartAt";
            this.checkBoxScheduleStartAt.Size = new System.Drawing.Size(99, 17);
            this.checkBoxScheduleStartAt.TabIndex = 0;
            this.checkBoxScheduleStartAt.Text = "Start capture at";
            this.checkBoxScheduleStartAt.UseVisualStyleBackColor = true;
            this.checkBoxScheduleStartAt.CheckedChanged += new System.EventHandler(this.checkBoxScheduleStartAt_CheckedChanged);
            // 
            // tabPageSlideshow
            // 
            this.tabPageSlideshow.AutoScroll = true;
            this.tabPageSlideshow.Controls.Add(this.listBoxScreenshots);
            this.tabPageSlideshow.Controls.Add(this.groupBoxSlideshowDelay);
            this.tabPageSlideshow.Location = new System.Drawing.Point(4, 22);
            this.tabPageSlideshow.Name = "tabPageSlideshow";
            this.tabPageSlideshow.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSlideshow.Size = new System.Drawing.Size(241, 128);
            this.tabPageSlideshow.TabIndex = 1;
            this.tabPageSlideshow.Text = "Slideshow";
            this.tabPageSlideshow.UseVisualStyleBackColor = true;
            // 
            // groupBoxSlideshowDelay
            // 
            this.groupBoxSlideshowDelay.Controls.Add(this.labelSlideshowDelayMilliseconds);
            this.groupBoxSlideshowDelay.Controls.Add(this.numericUpDownSlideshowDelayMilliseconds);
            this.groupBoxSlideshowDelay.Controls.Add(this.numericUpDownSlideSkip);
            this.groupBoxSlideshowDelay.Controls.Add(this.labelSlideshowDelaySeconds);
            this.groupBoxSlideshowDelay.Controls.Add(this.labelSlideshowDelayMinutes);
            this.groupBoxSlideshowDelay.Controls.Add(this.checkBoxSlideSkip);
            this.groupBoxSlideshowDelay.Controls.Add(this.labelSlideshowDelayHours);
            this.groupBoxSlideshowDelay.Controls.Add(this.numericUpDownSlideshowDelaySeconds);
            this.groupBoxSlideshowDelay.Controls.Add(this.numericUpDownSlideshowDelayMinutes);
            this.groupBoxSlideshowDelay.Controls.Add(this.numericUpDownSlideshowDelayHours);
            this.groupBoxSlideshowDelay.Location = new System.Drawing.Point(8, 132);
            this.groupBoxSlideshowDelay.Name = "groupBoxSlideshowDelay";
            this.groupBoxSlideshowDelay.Size = new System.Drawing.Size(204, 124);
            this.groupBoxSlideshowDelay.TabIndex = 12;
            this.groupBoxSlideshowDelay.TabStop = false;
            this.groupBoxSlideshowDelay.Text = "Show the next slide in ...";
            // 
            // labelSlideshowDelayMilliseconds
            // 
            this.labelSlideshowDelayMilliseconds.AutoSize = true;
            this.labelSlideshowDelayMilliseconds.Location = new System.Drawing.Point(54, 99);
            this.labelSlideshowDelayMilliseconds.Name = "labelSlideshowDelayMilliseconds";
            this.labelSlideshowDelayMilliseconds.Size = new System.Drawing.Size(63, 13);
            this.labelSlideshowDelayMilliseconds.TabIndex = 7;
            this.labelSlideshowDelayMilliseconds.Text = "milliseconds";
            // 
            // numericUpDownSlideshowDelayMilliseconds
            // 
            this.numericUpDownSlideshowDelayMilliseconds.Location = new System.Drawing.Point(6, 97);
            this.numericUpDownSlideshowDelayMilliseconds.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownSlideshowDelayMilliseconds.Name = "numericUpDownSlideshowDelayMilliseconds";
            this.numericUpDownSlideshowDelayMilliseconds.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSlideshowDelayMilliseconds.TabIndex = 6;
            this.numericUpDownSlideshowDelayMilliseconds.ValueChanged += new System.EventHandler(this.numericUpDownSlideshowDelay_ValueChanged);
            // 
            // numericUpDownSlideSkip
            // 
            this.numericUpDownSlideSkip.Location = new System.Drawing.Point(156, 19);
            this.numericUpDownSlideSkip.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownSlideSkip.Name = "numericUpDownSlideSkip";
            this.numericUpDownSlideSkip.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSlideSkip.TabIndex = 8;
            // 
            // labelSlideshowDelaySeconds
            // 
            this.labelSlideshowDelaySeconds.AutoSize = true;
            this.labelSlideshowDelaySeconds.Location = new System.Drawing.Point(54, 73);
            this.labelSlideshowDelaySeconds.Name = "labelSlideshowDelaySeconds";
            this.labelSlideshowDelaySeconds.Size = new System.Drawing.Size(47, 13);
            this.labelSlideshowDelaySeconds.TabIndex = 5;
            this.labelSlideshowDelaySeconds.Text = "seconds";
            // 
            // labelSlideshowDelayMinutes
            // 
            this.labelSlideshowDelayMinutes.AutoSize = true;
            this.labelSlideshowDelayMinutes.Location = new System.Drawing.Point(54, 47);
            this.labelSlideshowDelayMinutes.Name = "labelSlideshowDelayMinutes";
            this.labelSlideshowDelayMinutes.Size = new System.Drawing.Size(43, 13);
            this.labelSlideshowDelayMinutes.TabIndex = 4;
            this.labelSlideshowDelayMinutes.Text = "minutes";
            // 
            // checkBoxSlideSkip
            // 
            this.checkBoxSlideSkip.AutoSize = true;
            this.checkBoxSlideSkip.Location = new System.Drawing.Point(113, 20);
            this.checkBoxSlideSkip.Name = "checkBoxSlideSkip";
            this.checkBoxSlideSkip.Size = new System.Drawing.Size(47, 17);
            this.checkBoxSlideSkip.TabIndex = 13;
            this.checkBoxSlideSkip.Text = "Skip";
            this.checkBoxSlideSkip.UseVisualStyleBackColor = true;
            // 
            // labelSlideshowDelayHours
            // 
            this.labelSlideshowDelayHours.AutoSize = true;
            this.labelSlideshowDelayHours.Location = new System.Drawing.Point(54, 21);
            this.labelSlideshowDelayHours.Name = "labelSlideshowDelayHours";
            this.labelSlideshowDelayHours.Size = new System.Drawing.Size(33, 13);
            this.labelSlideshowDelayHours.TabIndex = 3;
            this.labelSlideshowDelayHours.Text = "hours";
            // 
            // numericUpDownSlideshowDelaySeconds
            // 
            this.numericUpDownSlideshowDelaySeconds.Location = new System.Drawing.Point(6, 71);
            this.numericUpDownSlideshowDelaySeconds.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSlideshowDelaySeconds.Name = "numericUpDownSlideshowDelaySeconds";
            this.numericUpDownSlideshowDelaySeconds.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSlideshowDelaySeconds.TabIndex = 2;
            this.numericUpDownSlideshowDelaySeconds.ValueChanged += new System.EventHandler(this.numericUpDownSlideshowDelay_ValueChanged);
            // 
            // numericUpDownSlideshowDelayMinutes
            // 
            this.numericUpDownSlideshowDelayMinutes.Location = new System.Drawing.Point(6, 45);
            this.numericUpDownSlideshowDelayMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSlideshowDelayMinutes.Name = "numericUpDownSlideshowDelayMinutes";
            this.numericUpDownSlideshowDelayMinutes.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSlideshowDelayMinutes.TabIndex = 1;
            this.numericUpDownSlideshowDelayMinutes.ValueChanged += new System.EventHandler(this.numericUpDownSlideshowDelay_ValueChanged);
            // 
            // numericUpDownSlideshowDelayHours
            // 
            this.numericUpDownSlideshowDelayHours.Location = new System.Drawing.Point(6, 19);
            this.numericUpDownSlideshowDelayHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownSlideshowDelayHours.Name = "numericUpDownSlideshowDelayHours";
            this.numericUpDownSlideshowDelayHours.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSlideshowDelayHours.TabIndex = 0;
            this.numericUpDownSlideshowDelayHours.ValueChanged += new System.EventHandler(this.numericUpDownSlideshowDelay_ValueChanged);
            // 
            // tabPageKeylogger
            // 
            this.tabPageKeylogger.Controls.Add(this.labelKeyloggingFile);
            this.tabPageKeylogger.Controls.Add(this.textBoxKeyloggingFile);
            this.tabPageKeylogger.Location = new System.Drawing.Point(4, 22);
            this.tabPageKeylogger.Name = "tabPageKeylogger";
            this.tabPageKeylogger.Size = new System.Drawing.Size(241, 128);
            this.tabPageKeylogger.TabIndex = 3;
            this.tabPageKeylogger.Text = "Keylogger";
            this.tabPageKeylogger.UseVisualStyleBackColor = true;
            // 
            // labelKeyloggingFile
            // 
            this.labelKeyloggingFile.AutoSize = true;
            this.labelKeyloggingFile.Location = new System.Drawing.Point(1, 7);
            this.labelKeyloggingFile.Name = "labelKeyloggingFile";
            this.labelKeyloggingFile.Size = new System.Drawing.Size(26, 13);
            this.labelKeyloggingFile.TabIndex = 1;
            this.labelKeyloggingFile.Text = "File:";
            // 
            // textBoxKeyloggingFile
            // 
            this.textBoxKeyloggingFile.Location = new System.Drawing.Point(33, 4);
            this.textBoxKeyloggingFile.Name = "textBoxKeyloggingFile";
            this.textBoxKeyloggingFile.Size = new System.Drawing.Size(205, 20);
            this.textBoxKeyloggingFile.TabIndex = 0;
            this.textBoxKeyloggingFile.TextChanged += new System.EventHandler(this.textBoxKeyloggingFile_TextChanged);
            // 
            // toolStripScreenCapture
            // 
            this.toolStripScreenCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStripScreenCapture.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripScreenCapture.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripScreenCapture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonStartScreenCapture,
            this.toolStripButtonStopScreenCapture});
            this.toolStripScreenCapture.Location = new System.Drawing.Point(20, 321);
            this.toolStripScreenCapture.Name = "toolStripScreenCapture";
            this.toolStripScreenCapture.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripScreenCapture.Size = new System.Drawing.Size(207, 25);
            this.toolStripScreenCapture.TabIndex = 20;
            // 
            // toolStripSplitButtonStartScreenCapture
            // 
            this.toolStripSplitButtonStartScreenCapture.Enabled = false;
            this.toolStripSplitButtonStartScreenCapture.Image = global::autoscreen.Properties.Resources.start_screen_capture;
            this.toolStripSplitButtonStartScreenCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonStartScreenCapture.Name = "toolStripSplitButtonStartScreenCapture";
            this.toolStripSplitButtonStartScreenCapture.Size = new System.Drawing.Size(108, 22);
            this.toolStripSplitButtonStartScreenCapture.Text = "Start Capture";
            this.toolStripSplitButtonStartScreenCapture.ButtonClick += new System.EventHandler(this.toolStripMenuItemStartScreenCapture_Click);
            // 
            // toolStripButtonStopScreenCapture
            // 
            this.toolStripButtonStopScreenCapture.Enabled = false;
            this.toolStripButtonStopScreenCapture.Image = global::autoscreen.Properties.Resources.stop_screen_capture;
            this.toolStripButtonStopScreenCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStopScreenCapture.Name = "toolStripButtonStopScreenCapture";
            this.toolStripButtonStopScreenCapture.Size = new System.Drawing.Size(96, 22);
            this.toolStripButtonStopScreenCapture.Text = "Stop Capture";
            this.toolStripButtonStopScreenCapture.Click += new System.EventHandler(this.toolStripMenuItemStopScreenCapture_Click);
            // 
            // buttonOpenFolder
            // 
            this.buttonOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenFolder.Location = new System.Drawing.Point(595, 350);
            this.buttonOpenFolder.Name = "buttonOpenFolder";
            this.buttonOpenFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenFolder.TabIndex = 14;
            this.buttonOpenFolder.Text = "Open Folder";
            this.buttonOpenFolder.UseVisualStyleBackColor = true;
            this.buttonOpenFolder.Click += new System.EventHandler(this.buttonOpenFolder_Click);
            // 
            // timerDemoCapture
            // 
            this.timerDemoCapture.Tick += new System.EventHandler(this.timerDemoCapture_Tick);
            // 
            // timerScheduledCaptureStart
            // 
            this.timerScheduledCaptureStart.Interval = 1000;
            this.timerScheduledCaptureStart.Tick += new System.EventHandler(this.timerScheduledCaptureStart_Tick);
            // 
            // timerScheduledCaptureStop
            // 
            this.timerScheduledCaptureStop.Interval = 1000;
            this.timerScheduledCaptureStop.Tick += new System.EventHandler(this.timerScheduledCaptureStop_Tick);
            // 
            // checkBoxEnableKeylogging
            // 
            this.checkBoxEnableKeylogging.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxEnableKeylogging.AutoSize = true;
            this.checkBoxEnableKeylogging.Location = new System.Drawing.Point(4, 325);
            this.checkBoxEnableKeylogging.Name = "checkBoxEnableKeylogging";
            this.checkBoxEnableKeylogging.Size = new System.Drawing.Size(222, 17);
            this.checkBoxEnableKeylogging.TabIndex = 21;
            this.checkBoxEnableKeylogging.Text = "Enable keylogging while screen capturing";
            this.checkBoxEnableKeylogging.UseVisualStyleBackColor = true;
            this.checkBoxEnableKeylogging.Visible = false;
            // 
            // toolStripMenuItemCloseWindowOnStartCapture
            // 
            this.toolStripMenuItemCloseWindowOnStartCapture.CheckOnClick = true;
            this.toolStripMenuItemCloseWindowOnStartCapture.Name = "toolStripMenuItemCloseWindowOnStartCapture";
            this.toolStripMenuItemCloseWindowOnStartCapture.Size = new System.Drawing.Size(455, 22);
            this.toolStripMenuItemCloseWindowOnStartCapture.Text = "Close this window when starting a screen capture session";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 398);
            this.Controls.Add(this.checkBoxEnableKeylogging);
            this.Controls.Add(this.toolStripScreenCapture);
            this.Controls.Add(this.buttonOpenFolder);
            this.Controls.Add(this.tabControlModules);
            this.Controls.Add(this.buttonBrowseFolder);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxScreenshotsFolderSearch);
            this.Controls.Add(this.tabControlScreens);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.toolStripSlideshow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(590, 310);
            this.Name = "FormMain";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormViewer_FormClosing);
            this.Load += new System.EventHandler(this.FormViewer_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControlScreens.ResumeLayout(false);
            this.tabPageAllScreens.ResumeLayout(false);
            this.tableLayoutPanelScreenshotPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshotPreviewMonitor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshotPreviewMonitor4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshotPreviewMonitor2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshotPreviewMonitor3)).EndInit();
            this.tabPageScreen1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen1)).EndInit();
            this.contextMenuStripScreenshotPreview.ResumeLayout(false);
            this.tabPageScreen2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen2)).EndInit();
            this.tabPageScreen3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen3)).EndInit();
            this.tabPageScreen4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen4)).EndInit();
            this.tabPageActiveWindow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActiveWindow)).EndInit();
            this.contextMenuStripSystemTrayIcon.ResumeLayout(false);
            this.toolStripSlideshow.ResumeLayout(false);
            this.toolStripSlideshow.PerformLayout();
            this.tabControlModules.ResumeLayout(false);
            this.tabPageScreenCapture.ResumeLayout(false);
            this.groupBoxCaptureDelay.ResumeLayout(false);
            this.groupBoxCaptureDelay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageResolutionRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            this.groupBoxSchedule.ResumeLayout(false);
            this.groupBoxSchedule.PerformLayout();
            this.tabPageSlideshow.ResumeLayout(false);
            this.groupBoxSlideshowDelay.ResumeLayout(false);
            this.groupBoxSlideshowDelay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayMilliseconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelaySeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlideshowDelayHours)).EndInit();
            this.tabPageKeylogger.ResumeLayout(false);
            this.tabPageKeylogger.PerformLayout();
            this.toolStripScreenCapture.ResumeLayout(false);
            this.toolStripScreenCapture.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TabControl tabControlScreens;
        private System.Windows.Forms.TabPage tabPageAllScreens;
        private System.Windows.Forms.ListBox listBoxScreenshots;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSystemTrayIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStartScreenCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStopScreenCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.TextBox textBoxScreenshotsFolderSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripScreenshotPreview;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenFileLocation;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelLastCapture;
        private System.Windows.Forms.Button buttonBrowseFolder;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox pictureBoxScreenshotPreviewMonitor4;
        private System.Windows.Forms.PictureBox pictureBoxScreenshotPreviewMonitor3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelScreenshotPreview;
        private System.Windows.Forms.PictureBox pictureBoxScreenshotPreviewMonitor1;
        private System.Windows.Forms.PictureBox pictureBoxScreenshotPreviewMonitor2;
        private System.Windows.Forms.ToolStrip toolStripSlideshow;
        private System.Windows.Forms.ToolStripButton toolStripButtonFirstSlide;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviousSlide;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlaySlideshow;
        private System.Windows.Forms.ToolStripButton toolStripButtonNextSlide;
        private System.Windows.Forms.ToolStripButton toolStripButtonLastSlide;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxImageFormatFilter;
        private System.Windows.Forms.TabPage tabPageScreen1;
        private System.Windows.Forms.PictureBox pictureBoxScreen1;
        private System.Windows.Forms.TabPage tabPageScreen2;
        private System.Windows.Forms.PictureBox pictureBoxScreen2;
        private System.Windows.Forms.TabPage tabPageScreen3;
        private System.Windows.Forms.PictureBox pictureBoxScreen3;
        private System.Windows.Forms.TabPage tabPageScreen4;
        private System.Windows.Forms.PictureBox pictureBoxScreen4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem editWithToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlModules;
        private System.Windows.Forms.TabPage tabPageScreenCapture;
        private System.Windows.Forms.TabPage tabPageSlideshow;
        private System.Windows.Forms.Button buttonOpenFolder;
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
        private System.Windows.Forms.GroupBox groupBoxSlideshowDelay;
        private System.Windows.Forms.Label labelSlideshowDelayMilliseconds;
        private System.Windows.Forms.NumericUpDown numericUpDownSlideshowDelayMilliseconds;
        private System.Windows.Forms.Label labelSlideshowDelaySeconds;
        private System.Windows.Forms.Label labelSlideshowDelayMinutes;
        private System.Windows.Forms.Label labelSlideshowDelayHours;
        private System.Windows.Forms.NumericUpDown numericUpDownSlideshowDelaySeconds;
        private System.Windows.Forms.NumericUpDown numericUpDownSlideshowDelayMinutes;
        private System.Windows.Forms.NumericUpDown numericUpDownSlideshowDelayHours;
        private System.Windows.Forms.CheckBox checkBoxSlideSkip;
        private System.Windows.Forms.ToolStrip toolStripScreenCapture;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonStartScreenCapture;
        private System.Windows.Forms.NumericUpDown numericUpDownCaptureLimit;
        private System.Windows.Forms.NumericUpDown numericUpDownImageResolutionRatio;
        private System.Windows.Forms.NumericUpDown numericUpDownSlideSkip;
        private System.Windows.Forms.Timer timerDemoCapture;
        private System.Windows.Forms.Label labelPercentResolution;
        private System.Windows.Forms.Label labelAt;
        private System.Windows.Forms.Label labelLimit;
        private System.Windows.Forms.ToolStripButton toolStripButtonStopScreenCapture;
        private System.Windows.Forms.TabPage tabPageKeylogger;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonOptions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenAtApplicationStartup;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenOnStopScreenCapture;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExitOnCloseWindow;
        private System.Windows.Forms.GroupBox groupBoxSchedule;
        private System.Windows.Forms.CheckBox checkBoxScheduleStopAt;
        private System.Windows.Forms.CheckBox checkBoxScheduleStartAt;
        private System.Windows.Forms.DateTimePicker dateTimePickerScheduleStartAt;
        private System.Windows.Forms.DateTimePicker dateTimePickerScheduleStopAt;
        private System.Windows.Forms.CheckBox checkBoxFriday;
        private System.Windows.Forms.CheckBox checkBoxThursday;
        private System.Windows.Forms.CheckBox checkBoxWednesday;
        private System.Windows.Forms.CheckBox checkBoxTuesday;
        private System.Windows.Forms.CheckBox checkBoxMonday;
        private System.Windows.Forms.CheckBox checkBoxSunday;
        private System.Windows.Forms.CheckBox checkBoxSaturday;
        private System.Windows.Forms.CheckBox checkBoxScheduleOnTheseDays;
        private System.Windows.Forms.Timer timerScheduledCaptureStart;
        private System.Windows.Forms.Timer timerScheduledCaptureStop;
        private System.Windows.Forms.TabPage tabPageActiveWindow;
        private System.Windows.Forms.PictureBox pictureBoxActiveWindow;
        private System.Windows.Forms.CheckBox checkBoxDemoMode;
        private System.Windows.Forms.Button buttonScheduleClear;
        private System.Windows.Forms.Button buttonScheduleSet;
        private System.Windows.Forms.ComboBox comboBoxScheduleImageFormat;
        private System.Windows.Forms.CheckBox checkBoxEnableKeylogging;
        private System.Windows.Forms.Label labelKeyloggingFile;
        private System.Windows.Forms.TextBox textBoxKeyloggingFile;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelSchedule;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScheduleAtApplicationStartup;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDemoModeAtApplicationStartup;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowSlideshowOnStopScreenCapture;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabelDemo;
        private System.Windows.Forms.CheckBox checkBoxScheduleStartOnSchedule;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSearchOnStopScreenCapture;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCloseWindowOnStartCapture;
    }
}