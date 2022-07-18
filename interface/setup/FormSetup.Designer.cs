namespace AutoScreenCapture
{
    partial class FormSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetup));
            this.tabControlSetup = new System.Windows.Forms.TabControl();
            this.tabPageScreenshotsFolder = new System.Windows.Forms.TabPage();
            this.labelScreenshotsFolderApplyToAllRegions = new System.Windows.Forms.Label();
            this.labelScreenshotsFolderApplyToAllScreens = new System.Windows.Forms.Label();
            this.buttonScreenshotsFolderApplyToAllRegions = new System.Windows.Forms.Button();
            this.buttonScreenshotsFolderApplyToAllScreens = new System.Windows.Forms.Button();
            this.labelScreenshotsFolderHelp = new System.Windows.Forms.Label();
            this.buttonScreenshotsFolderBrowseFolder = new System.Windows.Forms.Button();
            this.textBoxScreenshotsFolder = new System.Windows.Forms.TextBox();
            this.tabPageFilenamePattern = new System.Windows.Forms.TabPage();
            this.labelFilenamePatternFilename = new System.Windows.Forms.Label();
            this.labelFilenamePatternPreview = new System.Windows.Forms.Label();
            this.labelFilenamePatternApplyHelp = new System.Windows.Forms.Label();
            this.listBoxMacroTags = new System.Windows.Forms.ListBox();
            this.buttonFilenamePatternApplyToAllRegions = new System.Windows.Forms.Button();
            this.buttonFilenamePatternApplyToAllScreens = new System.Windows.Forms.Button();
            this.textBoxMacroPreview = new System.Windows.Forms.TextBox();
            this.textBoxFilenamePattern = new System.Windows.Forms.TextBox();
            this.labelFilenamePatternHelp = new System.Windows.Forms.Label();
            this.tabPageImageFormat = new System.Windows.Forms.TabPage();
            this.buttonImageFormatApplyToAllRegions = new System.Windows.Forms.Button();
            this.buttonImageFormatApplyToAllScreens = new System.Windows.Forms.Button();
            this.labelImageFormatHelp = new System.Windows.Forms.Label();
            this.radioButtonImageFormatWmf = new System.Windows.Forms.RadioButton();
            this.radioButtonImageFormatTiff = new System.Windows.Forms.RadioButton();
            this.radioButtonImageFormatPng = new System.Windows.Forms.RadioButton();
            this.radioButtonImageFormatJpeg = new System.Windows.Forms.RadioButton();
            this.radioButtonImageFormatGif = new System.Windows.Forms.RadioButton();
            this.radioButtonImageFormatEmf = new System.Windows.Forms.RadioButton();
            this.radioButtonImageFormatBmp = new System.Windows.Forms.RadioButton();
            this.tabPageInterval = new System.Windows.Forms.TabPage();
            this.labelLimitHelp = new System.Windows.Forms.Label();
            this.labelInitialCaptureHelp = new System.Windows.Forms.Label();
            this.labelIntervalHelp = new System.Windows.Forms.Label();
            this.checkBoxInitialScreenshot = new System.Windows.Forms.CheckBox();
            this.labelHoursInterval = new System.Windows.Forms.Label();
            this.numericUpDownCaptureLimit = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.checkBoxCaptureLimit = new System.Windows.Forms.CheckBox();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.labelMinutesInterval = new System.Windows.Forms.Label();
            this.labelSecondsInterval = new System.Windows.Forms.Label();
            this.tabPageOptimizeScreenCapture = new System.Windows.Forms.TabPage();
            this.labelOptimizeScreenCaptureHelp = new System.Windows.Forms.Label();
            this.checkBoxOptimizeScreenCapture = new System.Windows.Forms.CheckBox();
            this.tabPageLabels = new System.Windows.Forms.TabPage();
            this.buttonAddScreenshotLabelToList = new System.Windows.Forms.Button();
            this.labelScreenshotLabel = new System.Windows.Forms.Label();
            this.textBoxScreenshotLabel = new System.Windows.Forms.TextBox();
            this.labelScreenshotLabelHelp = new System.Windows.Forms.Label();
            this.listBoxScreenshotLabel = new System.Windows.Forms.ListBox();
            this.checkBoxScreenshotLabel = new System.Windows.Forms.CheckBox();
            this.tabPageActiveWindowTitle = new System.Windows.Forms.TabPage();
            this.textBoxRegularExpressionHelp = new System.Windows.Forms.TextBox();
            this.labelMatchHelp = new System.Windows.Forms.Label();
            this.labelMatchTestResult = new System.Windows.Forms.Label();
            this.labelActiveWindowTitleTest = new System.Windows.Forms.Label();
            this.textBoxActiveWindowTitleTest = new System.Windows.Forms.TextBox();
            this.labelActiveWindowTitle = new System.Windows.Forms.Label();
            this.labelActiveWindowTitleHelp = new System.Windows.Forms.Label();
            this.checkBoxActiveWindowTitleComparisonCheckReverse = new System.Windows.Forms.CheckBox();
            this.radioButtonRegularExpressionMatch = new System.Windows.Forms.RadioButton();
            this.checkBoxActiveWindowTitleComparisonCheck = new System.Windows.Forms.CheckBox();
            this.radioButtonCaseSensitiveMatch = new System.Windows.Forms.RadioButton();
            this.textBoxActiveWindowTitle = new System.Windows.Forms.TextBox();
            this.radioButtonCaseInsensitiveMatch = new System.Windows.Forms.RadioButton();
            this.tabPageApplicationFocus = new System.Windows.Forms.TabPage();
            this.checkBoxEnableApplicationFocus = new System.Windows.Forms.CheckBox();
            this.labelApplicationFocusHelp = new System.Windows.Forms.Label();
            this.listBoxProcessList = new System.Windows.Forms.ListBox();
            this.numericUpDownApplicationFocusDelayAfter = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownApplicationFocusDelayBefore = new System.Windows.Forms.NumericUpDown();
            this.labelApplicationFocusDelayAfter = new System.Windows.Forms.Label();
            this.labelApplicationFocusDelayBefore = new System.Windows.Forms.Label();
            this.buttonApplicationFocusTest = new System.Windows.Forms.Button();
            this.buttonApplicationFocusRefresh = new System.Windows.Forms.Button();
            this.tabPageSecurity = new System.Windows.Forms.TabPage();
            this.labelPassphrase = new System.Windows.Forms.Label();
            this.labelHash = new System.Windows.Forms.Label();
            this.textBoxPassphraseHash = new System.Windows.Forms.TextBox();
            this.labelLastUpdated = new System.Windows.Forms.Label();
            this.buttonClearPassphrase = new System.Windows.Forms.Button();
            this.buttonSetPassphrase = new System.Windows.Forms.Button();
            this.labelSecurityHelp = new System.Windows.Forms.Label();
            this.textBoxPassphrase = new System.Windows.Forms.TextBox();
            this.tabPageKeyboardShortcuts = new System.Windows.Forms.TabPage();
            this.labelRegionSelectEdit = new System.Windows.Forms.Label();
            this.labelStopScreenCapture = new System.Windows.Forms.Label();
            this.textBoxKeyboardShortcutRegionSelectEditKey = new System.Windows.Forms.TextBox();
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1 = new System.Windows.Forms.ComboBox();
            this.labelStartScreenCapture = new System.Windows.Forms.Label();
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1 = new System.Windows.Forms.ComboBox();
            this.labelCaptureNowEdit = new System.Windows.Forms.Label();
            this.textBoxKeyboardShortcutRegionSelectClipboardKey = new System.Windows.Forms.TextBox();
            this.textBoxKeyboardShortcutStopScreenCaptureKey = new System.Windows.Forms.TextBox();
            this.labelRegionSelectAutoSave = new System.Windows.Forms.Label();
            this.labelCaptureNowArchive = new System.Windows.Forms.Label();
            this.labelRegionSelectClipboard = new System.Windows.Forms.Label();
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey = new System.Windows.Forms.TextBox();
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2 = new System.Windows.Forms.ComboBox();
            this.textBoxKeyboardShortcutCaptureNowEditKey = new System.Windows.Forms.TextBox();
            this.textBoxKeyboardShortcutStartScreenCaptureKey = new System.Windows.Forms.TextBox();
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1 = new System.Windows.Forms.ComboBox();
            this.checkBoxUseKeyboardShortcuts = new System.Windows.Forms.CheckBox();
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1 = new System.Windows.Forms.ComboBox();
            this.textBoxKeyboardShortcutCaptureNowArchiveKey = new System.Windows.Forms.TextBox();
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2 = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelHelp = new System.Windows.Forms.Label();
            this.tabControlSetup.SuspendLayout();
            this.tabPageScreenshotsFolder.SuspendLayout();
            this.tabPageFilenamePattern.SuspendLayout();
            this.tabPageImageFormat.SuspendLayout();
            this.tabPageInterval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            this.tabPageOptimizeScreenCapture.SuspendLayout();
            this.tabPageLabels.SuspendLayout();
            this.tabPageActiveWindowTitle.SuspendLayout();
            this.tabPageApplicationFocus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayBefore)).BeginInit();
            this.tabPageSecurity.SuspendLayout();
            this.tabPageKeyboardShortcuts.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSetup
            // 
            this.tabControlSetup.Controls.Add(this.tabPageScreenshotsFolder);
            this.tabControlSetup.Controls.Add(this.tabPageFilenamePattern);
            this.tabControlSetup.Controls.Add(this.tabPageImageFormat);
            this.tabControlSetup.Controls.Add(this.tabPageInterval);
            this.tabControlSetup.Controls.Add(this.tabPageOptimizeScreenCapture);
            this.tabControlSetup.Controls.Add(this.tabPageLabels);
            this.tabControlSetup.Controls.Add(this.tabPageActiveWindowTitle);
            this.tabControlSetup.Controls.Add(this.tabPageApplicationFocus);
            this.tabControlSetup.Controls.Add(this.tabPageSecurity);
            this.tabControlSetup.Controls.Add(this.tabPageKeyboardShortcuts);
            this.tabControlSetup.Location = new System.Drawing.Point(2, 24);
            this.tabControlSetup.Name = "tabControlSetup";
            this.tabControlSetup.SelectedIndex = 0;
            this.tabControlSetup.Size = new System.Drawing.Size(685, 279);
            this.tabControlSetup.TabIndex = 0;
            this.tabControlSetup.TabStop = false;
            // 
            // tabPageScreenshotsFolder
            // 
            this.tabPageScreenshotsFolder.Controls.Add(this.labelScreenshotsFolderApplyToAllRegions);
            this.tabPageScreenshotsFolder.Controls.Add(this.labelScreenshotsFolderApplyToAllScreens);
            this.tabPageScreenshotsFolder.Controls.Add(this.buttonScreenshotsFolderApplyToAllRegions);
            this.tabPageScreenshotsFolder.Controls.Add(this.buttonScreenshotsFolderApplyToAllScreens);
            this.tabPageScreenshotsFolder.Controls.Add(this.labelScreenshotsFolderHelp);
            this.tabPageScreenshotsFolder.Controls.Add(this.buttonScreenshotsFolderBrowseFolder);
            this.tabPageScreenshotsFolder.Controls.Add(this.textBoxScreenshotsFolder);
            this.tabPageScreenshotsFolder.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreenshotsFolder.Name = "tabPageScreenshotsFolder";
            this.tabPageScreenshotsFolder.Size = new System.Drawing.Size(677, 253);
            this.tabPageScreenshotsFolder.TabIndex = 7;
            this.tabPageScreenshotsFolder.Text = "Screenshots Folder";
            this.tabPageScreenshotsFolder.UseVisualStyleBackColor = true;
            // 
            // labelScreenshotsFolderApplyToAllRegions
            // 
            this.labelScreenshotsFolderApplyToAllRegions.BackColor = System.Drawing.Color.LightYellow;
            this.labelScreenshotsFolderApplyToAllRegions.Location = new System.Drawing.Point(6, 166);
            this.labelScreenshotsFolderApplyToAllRegions.Name = "labelScreenshotsFolderApplyToAllRegions";
            this.labelScreenshotsFolderApplyToAllRegions.Size = new System.Drawing.Size(665, 17);
            this.labelScreenshotsFolderApplyToAllRegions.TabIndex = 37;
            this.labelScreenshotsFolderApplyToAllRegions.Text = "Clicking this button will apply the folder path to all existing Regions.";
            // 
            // labelScreenshotsFolderApplyToAllScreens
            // 
            this.labelScreenshotsFolderApplyToAllScreens.BackColor = System.Drawing.Color.LightYellow;
            this.labelScreenshotsFolderApplyToAllScreens.Location = new System.Drawing.Point(6, 97);
            this.labelScreenshotsFolderApplyToAllScreens.Name = "labelScreenshotsFolderApplyToAllScreens";
            this.labelScreenshotsFolderApplyToAllScreens.Size = new System.Drawing.Size(665, 17);
            this.labelScreenshotsFolderApplyToAllScreens.TabIndex = 36;
            this.labelScreenshotsFolderApplyToAllScreens.Text = "Clicking this button will apply the folder path to all existing Screens.";
            // 
            // buttonScreenshotsFolderApplyToAllRegions
            // 
            this.buttonScreenshotsFolderApplyToAllRegions.Location = new System.Drawing.Point(6, 186);
            this.buttonScreenshotsFolderApplyToAllRegions.Name = "buttonScreenshotsFolderApplyToAllRegions";
            this.buttonScreenshotsFolderApplyToAllRegions.Size = new System.Drawing.Size(159, 23);
            this.buttonScreenshotsFolderApplyToAllRegions.TabIndex = 35;
            this.buttonScreenshotsFolderApplyToAllRegions.TabStop = false;
            this.buttonScreenshotsFolderApplyToAllRegions.Text = "Apply To All Regions";
            this.buttonScreenshotsFolderApplyToAllRegions.UseVisualStyleBackColor = true;
            this.buttonScreenshotsFolderApplyToAllRegions.Click += new System.EventHandler(this.buttonScreenshotsFolderApplyToAllRegions_Click);
            // 
            // buttonScreenshotsFolderApplyToAllScreens
            // 
            this.buttonScreenshotsFolderApplyToAllScreens.Location = new System.Drawing.Point(6, 117);
            this.buttonScreenshotsFolderApplyToAllScreens.Name = "buttonScreenshotsFolderApplyToAllScreens";
            this.buttonScreenshotsFolderApplyToAllScreens.Size = new System.Drawing.Size(159, 23);
            this.buttonScreenshotsFolderApplyToAllScreens.TabIndex = 34;
            this.buttonScreenshotsFolderApplyToAllScreens.TabStop = false;
            this.buttonScreenshotsFolderApplyToAllScreens.Text = "Apply To All Screens";
            this.buttonScreenshotsFolderApplyToAllScreens.UseVisualStyleBackColor = true;
            this.buttonScreenshotsFolderApplyToAllScreens.Click += new System.EventHandler(this.buttonScreenshotsFolderApplyToAllScreens_Click);
            // 
            // labelScreenshotsFolderHelp
            // 
            this.labelScreenshotsFolderHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelScreenshotsFolderHelp.Location = new System.Drawing.Point(6, 13);
            this.labelScreenshotsFolderHelp.Name = "labelScreenshotsFolderHelp";
            this.labelScreenshotsFolderHelp.Size = new System.Drawing.Size(665, 32);
            this.labelScreenshotsFolderHelp.TabIndex = 33;
            this.labelScreenshotsFolderHelp.Text = resources.GetString("labelScreenshotsFolderHelp.Text");
            // 
            // buttonScreenshotsFolderBrowseFolder
            // 
            this.buttonScreenshotsFolderBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonScreenshotsFolderBrowseFolder.Image = global::AutoScreenCapture.Properties.Resources.openfolder;
            this.buttonScreenshotsFolderBrowseFolder.Location = new System.Drawing.Point(644, 49);
            this.buttonScreenshotsFolderBrowseFolder.Name = "buttonScreenshotsFolderBrowseFolder";
            this.buttonScreenshotsFolderBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonScreenshotsFolderBrowseFolder.TabIndex = 32;
            this.buttonScreenshotsFolderBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonScreenshotsFolderBrowseFolder.Click += new System.EventHandler(this.buttonScreenshotsFolderBrowseFolder_Click);
            // 
            // textBoxScreenshotsFolder
            // 
            this.textBoxScreenshotsFolder.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxScreenshotsFolder.Location = new System.Drawing.Point(6, 48);
            this.textBoxScreenshotsFolder.Name = "textBoxScreenshotsFolder";
            this.textBoxScreenshotsFolder.Size = new System.Drawing.Size(632, 25);
            this.textBoxScreenshotsFolder.TabIndex = 7;
            this.textBoxScreenshotsFolder.TabStop = false;
            // 
            // tabPageFilenamePattern
            // 
            this.tabPageFilenamePattern.Controls.Add(this.labelFilenamePatternFilename);
            this.tabPageFilenamePattern.Controls.Add(this.labelFilenamePatternPreview);
            this.tabPageFilenamePattern.Controls.Add(this.labelFilenamePatternApplyHelp);
            this.tabPageFilenamePattern.Controls.Add(this.listBoxMacroTags);
            this.tabPageFilenamePattern.Controls.Add(this.buttonFilenamePatternApplyToAllRegions);
            this.tabPageFilenamePattern.Controls.Add(this.buttonFilenamePatternApplyToAllScreens);
            this.tabPageFilenamePattern.Controls.Add(this.textBoxMacroPreview);
            this.tabPageFilenamePattern.Controls.Add(this.textBoxFilenamePattern);
            this.tabPageFilenamePattern.Controls.Add(this.labelFilenamePatternHelp);
            this.tabPageFilenamePattern.Location = new System.Drawing.Point(4, 22);
            this.tabPageFilenamePattern.Name = "tabPageFilenamePattern";
            this.tabPageFilenamePattern.Size = new System.Drawing.Size(677, 253);
            this.tabPageFilenamePattern.TabIndex = 8;
            this.tabPageFilenamePattern.Text = "Filename Pattern";
            this.tabPageFilenamePattern.UseVisualStyleBackColor = true;
            // 
            // labelFilenamePatternFilename
            // 
            this.labelFilenamePatternFilename.AutoSize = true;
            this.labelFilenamePatternFilename.Location = new System.Drawing.Point(6, 54);
            this.labelFilenamePatternFilename.Name = "labelFilenamePatternFilename";
            this.labelFilenamePatternFilename.Size = new System.Drawing.Size(52, 13);
            this.labelFilenamePatternFilename.TabIndex = 42;
            this.labelFilenamePatternFilename.Text = "Filename:";
            // 
            // labelFilenamePatternPreview
            // 
            this.labelFilenamePatternPreview.AutoSize = true;
            this.labelFilenamePatternPreview.Location = new System.Drawing.Point(6, 82);
            this.labelFilenamePatternPreview.Name = "labelFilenamePatternPreview";
            this.labelFilenamePatternPreview.Size = new System.Drawing.Size(48, 13);
            this.labelFilenamePatternPreview.TabIndex = 41;
            this.labelFilenamePatternPreview.Text = "Preview:";
            // 
            // labelFilenamePatternApplyHelp
            // 
            this.labelFilenamePatternApplyHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelFilenamePatternApplyHelp.Location = new System.Drawing.Point(6, 105);
            this.labelFilenamePatternApplyHelp.Name = "labelFilenamePatternApplyHelp";
            this.labelFilenamePatternApplyHelp.Size = new System.Drawing.Size(335, 23);
            this.labelFilenamePatternApplyHelp.TabIndex = 40;
            this.labelFilenamePatternApplyHelp.Text = "The macro tags that can be used in the filename are listed below:";
            this.labelFilenamePatternApplyHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxMacroTags
            // 
            this.listBoxMacroTags.FormattingEnabled = true;
            this.listBoxMacroTags.Location = new System.Drawing.Point(6, 134);
            this.listBoxMacroTags.Name = "listBoxMacroTags";
            this.listBoxMacroTags.Size = new System.Drawing.Size(665, 108);
            this.listBoxMacroTags.TabIndex = 39;
            this.listBoxMacroTags.TabStop = false;
            // 
            // buttonFilenamePatternApplyToAllRegions
            // 
            this.buttonFilenamePatternApplyToAllRegions.Location = new System.Drawing.Point(512, 105);
            this.buttonFilenamePatternApplyToAllRegions.Name = "buttonFilenamePatternApplyToAllRegions";
            this.buttonFilenamePatternApplyToAllRegions.Size = new System.Drawing.Size(159, 23);
            this.buttonFilenamePatternApplyToAllRegions.TabIndex = 38;
            this.buttonFilenamePatternApplyToAllRegions.TabStop = false;
            this.buttonFilenamePatternApplyToAllRegions.Text = "Apply To All Regions";
            this.buttonFilenamePatternApplyToAllRegions.UseVisualStyleBackColor = true;
            this.buttonFilenamePatternApplyToAllRegions.Click += new System.EventHandler(this.buttonFilenamePatternApplyToAllRegions_Click);
            // 
            // buttonFilenamePatternApplyToAllScreens
            // 
            this.buttonFilenamePatternApplyToAllScreens.Location = new System.Drawing.Point(347, 105);
            this.buttonFilenamePatternApplyToAllScreens.Name = "buttonFilenamePatternApplyToAllScreens";
            this.buttonFilenamePatternApplyToAllScreens.Size = new System.Drawing.Size(159, 23);
            this.buttonFilenamePatternApplyToAllScreens.TabIndex = 37;
            this.buttonFilenamePatternApplyToAllScreens.TabStop = false;
            this.buttonFilenamePatternApplyToAllScreens.Text = "Apply To All Screens";
            this.buttonFilenamePatternApplyToAllScreens.UseVisualStyleBackColor = true;
            this.buttonFilenamePatternApplyToAllScreens.Click += new System.EventHandler(this.buttonFilenamePatternApplyToAllScreens_Click);
            // 
            // textBoxMacroPreview
            // 
            this.textBoxMacroPreview.Location = new System.Drawing.Point(60, 79);
            this.textBoxMacroPreview.Name = "textBoxMacroPreview";
            this.textBoxMacroPreview.ReadOnly = true;
            this.textBoxMacroPreview.Size = new System.Drawing.Size(611, 20);
            this.textBoxMacroPreview.TabIndex = 36;
            this.textBoxMacroPreview.TabStop = false;
            // 
            // textBoxFilenamePattern
            // 
            this.textBoxFilenamePattern.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxFilenamePattern.Location = new System.Drawing.Point(60, 48);
            this.textBoxFilenamePattern.Name = "textBoxFilenamePattern";
            this.textBoxFilenamePattern.Size = new System.Drawing.Size(611, 25);
            this.textBoxFilenamePattern.TabIndex = 35;
            this.textBoxFilenamePattern.TabStop = false;
            this.textBoxFilenamePattern.TextChanged += new System.EventHandler(this.textBoxFilenamePattern_TextChanged);
            // 
            // labelFilenamePatternHelp
            // 
            this.labelFilenamePatternHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelFilenamePatternHelp.Location = new System.Drawing.Point(6, 13);
            this.labelFilenamePatternHelp.Name = "labelFilenamePatternHelp";
            this.labelFilenamePatternHelp.Size = new System.Drawing.Size(665, 32);
            this.labelFilenamePatternHelp.TabIndex = 34;
            this.labelFilenamePatternHelp.Text = resources.GetString("labelFilenamePatternHelp.Text");
            // 
            // tabPageImageFormat
            // 
            this.tabPageImageFormat.Controls.Add(this.buttonImageFormatApplyToAllRegions);
            this.tabPageImageFormat.Controls.Add(this.buttonImageFormatApplyToAllScreens);
            this.tabPageImageFormat.Controls.Add(this.labelImageFormatHelp);
            this.tabPageImageFormat.Controls.Add(this.radioButtonImageFormatWmf);
            this.tabPageImageFormat.Controls.Add(this.radioButtonImageFormatTiff);
            this.tabPageImageFormat.Controls.Add(this.radioButtonImageFormatPng);
            this.tabPageImageFormat.Controls.Add(this.radioButtonImageFormatJpeg);
            this.tabPageImageFormat.Controls.Add(this.radioButtonImageFormatGif);
            this.tabPageImageFormat.Controls.Add(this.radioButtonImageFormatEmf);
            this.tabPageImageFormat.Controls.Add(this.radioButtonImageFormatBmp);
            this.tabPageImageFormat.Location = new System.Drawing.Point(4, 22);
            this.tabPageImageFormat.Name = "tabPageImageFormat";
            this.tabPageImageFormat.Size = new System.Drawing.Size(677, 253);
            this.tabPageImageFormat.TabIndex = 9;
            this.tabPageImageFormat.Text = "Image Format";
            this.tabPageImageFormat.UseVisualStyleBackColor = true;
            // 
            // buttonImageFormatApplyToAllRegions
            // 
            this.buttonImageFormatApplyToAllRegions.Location = new System.Drawing.Point(175, 209);
            this.buttonImageFormatApplyToAllRegions.Name = "buttonImageFormatApplyToAllRegions";
            this.buttonImageFormatApplyToAllRegions.Size = new System.Drawing.Size(159, 23);
            this.buttonImageFormatApplyToAllRegions.TabIndex = 39;
            this.buttonImageFormatApplyToAllRegions.TabStop = false;
            this.buttonImageFormatApplyToAllRegions.Text = "Apply To All Regions";
            this.buttonImageFormatApplyToAllRegions.UseVisualStyleBackColor = true;
            this.buttonImageFormatApplyToAllRegions.Click += new System.EventHandler(this.buttonImageFormatApplyToAllRegions_Click);
            // 
            // buttonImageFormatApplyToAllScreens
            // 
            this.buttonImageFormatApplyToAllScreens.Location = new System.Drawing.Point(10, 209);
            this.buttonImageFormatApplyToAllScreens.Name = "buttonImageFormatApplyToAllScreens";
            this.buttonImageFormatApplyToAllScreens.Size = new System.Drawing.Size(159, 23);
            this.buttonImageFormatApplyToAllScreens.TabIndex = 38;
            this.buttonImageFormatApplyToAllScreens.TabStop = false;
            this.buttonImageFormatApplyToAllScreens.Text = "Apply To All Screens";
            this.buttonImageFormatApplyToAllScreens.UseVisualStyleBackColor = true;
            this.buttonImageFormatApplyToAllScreens.Click += new System.EventHandler(this.buttonImageFormatApplyToAllScreens_Click);
            // 
            // labelImageFormatHelp
            // 
            this.labelImageFormatHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelImageFormatHelp.Location = new System.Drawing.Point(6, 13);
            this.labelImageFormatHelp.Name = "labelImageFormatHelp";
            this.labelImageFormatHelp.Size = new System.Drawing.Size(665, 32);
            this.labelImageFormatHelp.TabIndex = 35;
            this.labelImageFormatHelp.Text = resources.GetString("labelImageFormatHelp.Text");
            // 
            // radioButtonImageFormatWmf
            // 
            this.radioButtonImageFormatWmf.AutoSize = true;
            this.radioButtonImageFormatWmf.Location = new System.Drawing.Point(10, 186);
            this.radioButtonImageFormatWmf.Name = "radioButtonImageFormatWmf";
            this.radioButtonImageFormatWmf.Size = new System.Drawing.Size(51, 17);
            this.radioButtonImageFormatWmf.TabIndex = 6;
            this.radioButtonImageFormatWmf.TabStop = true;
            this.radioButtonImageFormatWmf.Text = "WMF";
            this.radioButtonImageFormatWmf.UseVisualStyleBackColor = true;
            // 
            // radioButtonImageFormatTiff
            // 
            this.radioButtonImageFormatTiff.AutoSize = true;
            this.radioButtonImageFormatTiff.Location = new System.Drawing.Point(10, 163);
            this.radioButtonImageFormatTiff.Name = "radioButtonImageFormatTiff";
            this.radioButtonImageFormatTiff.Size = new System.Drawing.Size(47, 17);
            this.radioButtonImageFormatTiff.TabIndex = 5;
            this.radioButtonImageFormatTiff.TabStop = true;
            this.radioButtonImageFormatTiff.Text = "TIFF";
            this.radioButtonImageFormatTiff.UseVisualStyleBackColor = true;
            // 
            // radioButtonImageFormatPng
            // 
            this.radioButtonImageFormatPng.AutoSize = true;
            this.radioButtonImageFormatPng.Location = new System.Drawing.Point(10, 140);
            this.radioButtonImageFormatPng.Name = "radioButtonImageFormatPng";
            this.radioButtonImageFormatPng.Size = new System.Drawing.Size(48, 17);
            this.radioButtonImageFormatPng.TabIndex = 4;
            this.radioButtonImageFormatPng.Text = "PNG";
            this.radioButtonImageFormatPng.UseVisualStyleBackColor = true;
            // 
            // radioButtonImageFormatJpeg
            // 
            this.radioButtonImageFormatJpeg.AutoSize = true;
            this.radioButtonImageFormatJpeg.Location = new System.Drawing.Point(10, 117);
            this.radioButtonImageFormatJpeg.Name = "radioButtonImageFormatJpeg";
            this.radioButtonImageFormatJpeg.Size = new System.Drawing.Size(52, 17);
            this.radioButtonImageFormatJpeg.TabIndex = 3;
            this.radioButtonImageFormatJpeg.Text = "JPEG";
            this.radioButtonImageFormatJpeg.UseVisualStyleBackColor = true;
            // 
            // radioButtonImageFormatGif
            // 
            this.radioButtonImageFormatGif.AutoSize = true;
            this.radioButtonImageFormatGif.Location = new System.Drawing.Point(10, 94);
            this.radioButtonImageFormatGif.Name = "radioButtonImageFormatGif";
            this.radioButtonImageFormatGif.Size = new System.Drawing.Size(42, 17);
            this.radioButtonImageFormatGif.TabIndex = 2;
            this.radioButtonImageFormatGif.Text = "GIF";
            this.radioButtonImageFormatGif.UseVisualStyleBackColor = true;
            // 
            // radioButtonImageFormatEmf
            // 
            this.radioButtonImageFormatEmf.AutoSize = true;
            this.radioButtonImageFormatEmf.Location = new System.Drawing.Point(10, 71);
            this.radioButtonImageFormatEmf.Name = "radioButtonImageFormatEmf";
            this.radioButtonImageFormatEmf.Size = new System.Drawing.Size(47, 17);
            this.radioButtonImageFormatEmf.TabIndex = 1;
            this.radioButtonImageFormatEmf.Text = "EMF";
            this.radioButtonImageFormatEmf.UseVisualStyleBackColor = true;
            // 
            // radioButtonImageFormatBmp
            // 
            this.radioButtonImageFormatBmp.AutoSize = true;
            this.radioButtonImageFormatBmp.Location = new System.Drawing.Point(10, 48);
            this.radioButtonImageFormatBmp.Name = "radioButtonImageFormatBmp";
            this.radioButtonImageFormatBmp.Size = new System.Drawing.Size(48, 17);
            this.radioButtonImageFormatBmp.TabIndex = 0;
            this.radioButtonImageFormatBmp.Text = "BMP";
            this.radioButtonImageFormatBmp.UseVisualStyleBackColor = true;
            // 
            // tabPageInterval
            // 
            this.tabPageInterval.Controls.Add(this.labelLimitHelp);
            this.tabPageInterval.Controls.Add(this.labelInitialCaptureHelp);
            this.tabPageInterval.Controls.Add(this.labelIntervalHelp);
            this.tabPageInterval.Controls.Add(this.checkBoxInitialScreenshot);
            this.tabPageInterval.Controls.Add(this.labelHoursInterval);
            this.tabPageInterval.Controls.Add(this.numericUpDownCaptureLimit);
            this.tabPageInterval.Controls.Add(this.numericUpDownHoursInterval);
            this.tabPageInterval.Controls.Add(this.checkBoxCaptureLimit);
            this.tabPageInterval.Controls.Add(this.numericUpDownMinutesInterval);
            this.tabPageInterval.Controls.Add(this.numericUpDownSecondsInterval);
            this.tabPageInterval.Controls.Add(this.labelMinutesInterval);
            this.tabPageInterval.Controls.Add(this.labelSecondsInterval);
            this.tabPageInterval.Location = new System.Drawing.Point(4, 22);
            this.tabPageInterval.Name = "tabPageInterval";
            this.tabPageInterval.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInterval.Size = new System.Drawing.Size(677, 253);
            this.tabPageInterval.TabIndex = 0;
            this.tabPageInterval.Text = "Interval";
            this.tabPageInterval.UseVisualStyleBackColor = true;
            // 
            // labelLimitHelp
            // 
            this.labelLimitHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelLimitHelp.Location = new System.Drawing.Point(6, 169);
            this.labelLimitHelp.Name = "labelLimitHelp";
            this.labelLimitHelp.Size = new System.Drawing.Size(665, 32);
            this.labelLimitHelp.TabIndex = 3;
            this.labelLimitHelp.Text = resources.GetString("labelLimitHelp.Text");
            // 
            // labelInitialCaptureHelp
            // 
            this.labelInitialCaptureHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelInitialCaptureHelp.Location = new System.Drawing.Point(6, 95);
            this.labelInitialCaptureHelp.Name = "labelInitialCaptureHelp";
            this.labelInitialCaptureHelp.Size = new System.Drawing.Size(665, 32);
            this.labelInitialCaptureHelp.TabIndex = 2;
            this.labelInitialCaptureHelp.Text = resources.GetString("labelInitialCaptureHelp.Text");
            // 
            // labelIntervalHelp
            // 
            this.labelIntervalHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelIntervalHelp.Location = new System.Drawing.Point(6, 13);
            this.labelIntervalHelp.Name = "labelIntervalHelp";
            this.labelIntervalHelp.Size = new System.Drawing.Size(665, 32);
            this.labelIntervalHelp.TabIndex = 1;
            this.labelIntervalHelp.Text = resources.GetString("labelIntervalHelp.Text");
            // 
            // checkBoxInitialScreenshot
            // 
            this.checkBoxInitialScreenshot.AutoSize = true;
            this.checkBoxInitialScreenshot.Location = new System.Drawing.Point(9, 130);
            this.checkBoxInitialScreenshot.Name = "checkBoxInitialScreenshot";
            this.checkBoxInitialScreenshot.Size = new System.Drawing.Size(90, 17);
            this.checkBoxInitialScreenshot.TabIndex = 0;
            this.checkBoxInitialScreenshot.TabStop = false;
            this.checkBoxInitialScreenshot.Text = "Initial Capture";
            this.checkBoxInitialScreenshot.UseVisualStyleBackColor = true;
            // 
            // labelHoursInterval
            // 
            this.labelHoursInterval.AutoSize = true;
            this.labelHoursInterval.Location = new System.Drawing.Point(57, 50);
            this.labelHoursInterval.Name = "labelHoursInterval";
            this.labelHoursInterval.Size = new System.Drawing.Size(33, 13);
            this.labelHoursInterval.TabIndex = 0;
            this.labelHoursInterval.Text = "hours";
            // 
            // numericUpDownCaptureLimit
            // 
            this.numericUpDownCaptureLimit.Location = new System.Drawing.Point(57, 205);
            this.numericUpDownCaptureLimit.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownCaptureLimit.Name = "numericUpDownCaptureLimit";
            this.numericUpDownCaptureLimit.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownCaptureLimit.TabIndex = 0;
            this.numericUpDownCaptureLimit.TabStop = false;
            // 
            // numericUpDownHoursInterval
            // 
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(9, 48);
            this.numericUpDownHoursInterval.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownHoursInterval.Name = "numericUpDownHoursInterval";
            this.numericUpDownHoursInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownHoursInterval.TabIndex = 0;
            this.numericUpDownHoursInterval.TabStop = false;
            // 
            // checkBoxCaptureLimit
            // 
            this.checkBoxCaptureLimit.AutoSize = true;
            this.checkBoxCaptureLimit.Location = new System.Drawing.Point(9, 206);
            this.checkBoxCaptureLimit.Name = "checkBoxCaptureLimit";
            this.checkBoxCaptureLimit.Size = new System.Drawing.Size(50, 17);
            this.checkBoxCaptureLimit.TabIndex = 0;
            this.checkBoxCaptureLimit.TabStop = false;
            this.checkBoxCaptureLimit.Text = "Limit:";
            this.checkBoxCaptureLimit.UseVisualStyleBackColor = true;
            // 
            // numericUpDownMinutesInterval
            // 
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(127, 48);
            this.numericUpDownMinutesInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownMinutesInterval.Name = "numericUpDownMinutesInterval";
            this.numericUpDownMinutesInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMinutesInterval.TabIndex = 0;
            this.numericUpDownMinutesInterval.TabStop = false;
            // 
            // numericUpDownSecondsInterval
            // 
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(258, 48);
            this.numericUpDownSecondsInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSecondsInterval.Name = "numericUpDownSecondsInterval";
            this.numericUpDownSecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSecondsInterval.TabIndex = 0;
            this.numericUpDownSecondsInterval.TabStop = false;
            // 
            // labelMinutesInterval
            // 
            this.labelMinutesInterval.AutoSize = true;
            this.labelMinutesInterval.Location = new System.Drawing.Point(175, 50);
            this.labelMinutesInterval.Name = "labelMinutesInterval";
            this.labelMinutesInterval.Size = new System.Drawing.Size(43, 13);
            this.labelMinutesInterval.TabIndex = 0;
            this.labelMinutesInterval.Text = "minutes";
            // 
            // labelSecondsInterval
            // 
            this.labelSecondsInterval.AutoSize = true;
            this.labelSecondsInterval.Location = new System.Drawing.Point(306, 50);
            this.labelSecondsInterval.Name = "labelSecondsInterval";
            this.labelSecondsInterval.Size = new System.Drawing.Size(47, 13);
            this.labelSecondsInterval.TabIndex = 0;
            this.labelSecondsInterval.Text = "seconds";
            // 
            // tabPageOptimizeScreenCapture
            // 
            this.tabPageOptimizeScreenCapture.Controls.Add(this.labelOptimizeScreenCaptureHelp);
            this.tabPageOptimizeScreenCapture.Controls.Add(this.checkBoxOptimizeScreenCapture);
            this.tabPageOptimizeScreenCapture.Location = new System.Drawing.Point(4, 22);
            this.tabPageOptimizeScreenCapture.Name = "tabPageOptimizeScreenCapture";
            this.tabPageOptimizeScreenCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOptimizeScreenCapture.Size = new System.Drawing.Size(677, 253);
            this.tabPageOptimizeScreenCapture.TabIndex = 6;
            this.tabPageOptimizeScreenCapture.Text = "Optimize Screen Capture";
            this.tabPageOptimizeScreenCapture.UseVisualStyleBackColor = true;
            // 
            // labelOptimizeScreenCaptureHelp
            // 
            this.labelOptimizeScreenCaptureHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelOptimizeScreenCaptureHelp.Location = new System.Drawing.Point(6, 13);
            this.labelOptimizeScreenCaptureHelp.Name = "labelOptimizeScreenCaptureHelp";
            this.labelOptimizeScreenCaptureHelp.Size = new System.Drawing.Size(665, 32);
            this.labelOptimizeScreenCaptureHelp.TabIndex = 0;
            this.labelOptimizeScreenCaptureHelp.Text = resources.GetString("labelOptimizeScreenCaptureHelp.Text");
            // 
            // checkBoxOptimizeScreenCapture
            // 
            this.checkBoxOptimizeScreenCapture.AutoSize = true;
            this.checkBoxOptimizeScreenCapture.Location = new System.Drawing.Point(6, 52);
            this.checkBoxOptimizeScreenCapture.Name = "checkBoxOptimizeScreenCapture";
            this.checkBoxOptimizeScreenCapture.Size = new System.Drawing.Size(140, 17);
            this.checkBoxOptimizeScreenCapture.TabIndex = 0;
            this.checkBoxOptimizeScreenCapture.TabStop = false;
            this.checkBoxOptimizeScreenCapture.Text = "Optimize screen capture";
            this.checkBoxOptimizeScreenCapture.UseVisualStyleBackColor = true;
            this.checkBoxOptimizeScreenCapture.CheckedChanged += new System.EventHandler(this.checkBoxOptimizeScreenCapture_CheckedChanged);
            // 
            // tabPageLabels
            // 
            this.tabPageLabels.Controls.Add(this.buttonAddScreenshotLabelToList);
            this.tabPageLabels.Controls.Add(this.labelScreenshotLabel);
            this.tabPageLabels.Controls.Add(this.textBoxScreenshotLabel);
            this.tabPageLabels.Controls.Add(this.labelScreenshotLabelHelp);
            this.tabPageLabels.Controls.Add(this.listBoxScreenshotLabel);
            this.tabPageLabels.Controls.Add(this.checkBoxScreenshotLabel);
            this.tabPageLabels.Location = new System.Drawing.Point(4, 22);
            this.tabPageLabels.Name = "tabPageLabels";
            this.tabPageLabels.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLabels.Size = new System.Drawing.Size(677, 253);
            this.tabPageLabels.TabIndex = 1;
            this.tabPageLabels.Text = "Labels";
            this.tabPageLabels.UseVisualStyleBackColor = true;
            // 
            // buttonAddScreenshotLabelToList
            // 
            this.buttonAddScreenshotLabelToList.Location = new System.Drawing.Point(596, 49);
            this.buttonAddScreenshotLabelToList.Name = "buttonAddScreenshotLabelToList";
            this.buttonAddScreenshotLabelToList.Size = new System.Drawing.Size(75, 23);
            this.buttonAddScreenshotLabelToList.TabIndex = 8;
            this.buttonAddScreenshotLabelToList.TabStop = false;
            this.buttonAddScreenshotLabelToList.Text = "Add To List";
            this.buttonAddScreenshotLabelToList.UseVisualStyleBackColor = true;
            this.buttonAddScreenshotLabelToList.Click += new System.EventHandler(this.buttonAddScreenshotLabelToList_Click);
            // 
            // labelScreenshotLabel
            // 
            this.labelScreenshotLabel.AutoSize = true;
            this.labelScreenshotLabel.Location = new System.Drawing.Point(6, 54);
            this.labelScreenshotLabel.Name = "labelScreenshotLabel";
            this.labelScreenshotLabel.Size = new System.Drawing.Size(93, 13);
            this.labelScreenshotLabel.TabIndex = 7;
            this.labelScreenshotLabel.Text = "Screenshot Label:";
            // 
            // textBoxScreenshotLabel
            // 
            this.textBoxScreenshotLabel.Location = new System.Drawing.Point(105, 51);
            this.textBoxScreenshotLabel.Name = "textBoxScreenshotLabel";
            this.textBoxScreenshotLabel.Size = new System.Drawing.Size(485, 20);
            this.textBoxScreenshotLabel.TabIndex = 6;
            this.textBoxScreenshotLabel.TabStop = false;
            // 
            // labelScreenshotLabelHelp
            // 
            this.labelScreenshotLabelHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelScreenshotLabelHelp.Location = new System.Drawing.Point(6, 13);
            this.labelScreenshotLabelHelp.Name = "labelScreenshotLabelHelp";
            this.labelScreenshotLabelHelp.Size = new System.Drawing.Size(665, 32);
            this.labelScreenshotLabelHelp.TabIndex = 5;
            this.labelScreenshotLabelHelp.Text = resources.GetString("labelScreenshotLabelHelp.Text");
            // 
            // listBoxScreenshotLabel
            // 
            this.listBoxScreenshotLabel.FormattingEnabled = true;
            this.listBoxScreenshotLabel.Location = new System.Drawing.Point(6, 102);
            this.listBoxScreenshotLabel.Name = "listBoxScreenshotLabel";
            this.listBoxScreenshotLabel.Size = new System.Drawing.Size(665, 147);
            this.listBoxScreenshotLabel.Sorted = true;
            this.listBoxScreenshotLabel.TabIndex = 4;
            this.listBoxScreenshotLabel.TabStop = false;
            this.listBoxScreenshotLabel.SelectedIndexChanged += new System.EventHandler(this.listBoxScreenshotLabel_SelectedIndexChanged);
            // 
            // checkBoxScreenshotLabel
            // 
            this.checkBoxScreenshotLabel.AutoSize = true;
            this.checkBoxScreenshotLabel.Location = new System.Drawing.Point(6, 79);
            this.checkBoxScreenshotLabel.Name = "checkBoxScreenshotLabel";
            this.checkBoxScreenshotLabel.Size = new System.Drawing.Size(193, 17);
            this.checkBoxScreenshotLabel.TabIndex = 3;
            this.checkBoxScreenshotLabel.TabStop = false;
            this.checkBoxScreenshotLabel.Text = "Apply this label to each screenshot:";
            this.checkBoxScreenshotLabel.UseVisualStyleBackColor = true;
            // 
            // tabPageActiveWindowTitle
            // 
            this.tabPageActiveWindowTitle.Controls.Add(this.textBoxRegularExpressionHelp);
            this.tabPageActiveWindowTitle.Controls.Add(this.labelMatchHelp);
            this.tabPageActiveWindowTitle.Controls.Add(this.labelMatchTestResult);
            this.tabPageActiveWindowTitle.Controls.Add(this.labelActiveWindowTitleTest);
            this.tabPageActiveWindowTitle.Controls.Add(this.textBoxActiveWindowTitleTest);
            this.tabPageActiveWindowTitle.Controls.Add(this.labelActiveWindowTitle);
            this.tabPageActiveWindowTitle.Controls.Add(this.labelActiveWindowTitleHelp);
            this.tabPageActiveWindowTitle.Controls.Add(this.checkBoxActiveWindowTitleComparisonCheckReverse);
            this.tabPageActiveWindowTitle.Controls.Add(this.radioButtonRegularExpressionMatch);
            this.tabPageActiveWindowTitle.Controls.Add(this.checkBoxActiveWindowTitleComparisonCheck);
            this.tabPageActiveWindowTitle.Controls.Add(this.radioButtonCaseSensitiveMatch);
            this.tabPageActiveWindowTitle.Controls.Add(this.textBoxActiveWindowTitle);
            this.tabPageActiveWindowTitle.Controls.Add(this.radioButtonCaseInsensitiveMatch);
            this.tabPageActiveWindowTitle.Location = new System.Drawing.Point(4, 22);
            this.tabPageActiveWindowTitle.Name = "tabPageActiveWindowTitle";
            this.tabPageActiveWindowTitle.Size = new System.Drawing.Size(677, 253);
            this.tabPageActiveWindowTitle.TabIndex = 2;
            this.tabPageActiveWindowTitle.Text = "Active Window Title";
            this.tabPageActiveWindowTitle.UseVisualStyleBackColor = true;
            // 
            // textBoxRegularExpressionHelp
            // 
            this.textBoxRegularExpressionHelp.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRegularExpressionHelp.Location = new System.Drawing.Point(436, 121);
            this.textBoxRegularExpressionHelp.Multiline = true;
            this.textBoxRegularExpressionHelp.Name = "textBoxRegularExpressionHelp";
            this.textBoxRegularExpressionHelp.ReadOnly = true;
            this.textBoxRegularExpressionHelp.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxRegularExpressionHelp.Size = new System.Drawing.Size(235, 129);
            this.textBoxRegularExpressionHelp.TabIndex = 0;
            this.textBoxRegularExpressionHelp.TabStop = false;
            this.textBoxRegularExpressionHelp.Text = resources.GetString("textBoxRegularExpressionHelp.Text");
            this.textBoxRegularExpressionHelp.WordWrap = false;
            // 
            // labelMatchHelp
            // 
            this.labelMatchHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelMatchHelp.Location = new System.Drawing.Point(3, 121);
            this.labelMatchHelp.Name = "labelMatchHelp";
            this.labelMatchHelp.Size = new System.Drawing.Size(157, 63);
            this.labelMatchHelp.TabIndex = 12;
            this.labelMatchHelp.Text = "The \"Match\" option tests if your text matches with the active window title. The \"" +
    "No Match\" option is the opposite test.";
            // 
            // labelMatchTestResult
            // 
            this.labelMatchTestResult.BackColor = System.Drawing.Color.LightYellow;
            this.labelMatchTestResult.Location = new System.Drawing.Point(6, 234);
            this.labelMatchTestResult.Name = "labelMatchTestResult";
            this.labelMatchTestResult.Size = new System.Drawing.Size(408, 14);
            this.labelMatchTestResult.TabIndex = 11;
            this.labelMatchTestResult.Text = "Test Result: (empty)";
            // 
            // labelActiveWindowTitleTest
            // 
            this.labelActiveWindowTitleTest.AutoSize = true;
            this.labelActiveWindowTitleTest.Location = new System.Drawing.Point(6, 188);
            this.labelActiveWindowTitleTest.Name = "labelActiveWindowTitleTest";
            this.labelActiveWindowTitleTest.Size = new System.Drawing.Size(408, 13);
            this.labelActiveWindowTitleTest.TabIndex = 10;
            this.labelActiveWindowTitleTest.Text = "Test your comparison text and matching options with this example active window ti" +
    "tle:";
            // 
            // textBoxActiveWindowTitleTest
            // 
            this.textBoxActiveWindowTitleTest.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxActiveWindowTitleTest.Location = new System.Drawing.Point(6, 204);
            this.textBoxActiveWindowTitleTest.Name = "textBoxActiveWindowTitleTest";
            this.textBoxActiveWindowTitleTest.Size = new System.Drawing.Size(408, 25);
            this.textBoxActiveWindowTitleTest.TabIndex = 0;
            this.textBoxActiveWindowTitleTest.TabStop = false;
            this.textBoxActiveWindowTitleTest.TextChanged += new System.EventHandler(this.textBoxActiveWindowTitleTest_TextChanged);
            // 
            // labelActiveWindowTitle
            // 
            this.labelActiveWindowTitle.AutoSize = true;
            this.labelActiveWindowTitle.Location = new System.Drawing.Point(6, 74);
            this.labelActiveWindowTitle.Name = "labelActiveWindowTitle";
            this.labelActiveWindowTitle.Size = new System.Drawing.Size(580, 13);
            this.labelActiveWindowTitle.TabIndex = 7;
            this.labelActiveWindowTitle.Text = "Text or regular expression pattern to compare against the active window title to " +
    "determine if a screenshot should be taken:";
            // 
            // labelActiveWindowTitleHelp
            // 
            this.labelActiveWindowTitleHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelActiveWindowTitleHelp.Location = new System.Drawing.Point(6, 13);
            this.labelActiveWindowTitleHelp.Name = "labelActiveWindowTitleHelp";
            this.labelActiveWindowTitleHelp.Size = new System.Drawing.Size(665, 60);
            this.labelActiveWindowTitleHelp.TabIndex = 6;
            this.labelActiveWindowTitleHelp.Text = resources.GetString("labelActiveWindowTitleHelp.Text");
            // 
            // checkBoxActiveWindowTitleComparisonCheckReverse
            // 
            this.checkBoxActiveWindowTitleComparisonCheckReverse.AutoSize = true;
            this.checkBoxActiveWindowTitleComparisonCheckReverse.Location = new System.Drawing.Point(174, 144);
            this.checkBoxActiveWindowTitleComparisonCheckReverse.Name = "checkBoxActiveWindowTitleComparisonCheckReverse";
            this.checkBoxActiveWindowTitleComparisonCheckReverse.Size = new System.Drawing.Size(73, 17);
            this.checkBoxActiveWindowTitleComparisonCheckReverse.TabIndex = 0;
            this.checkBoxActiveWindowTitleComparisonCheckReverse.TabStop = false;
            this.checkBoxActiveWindowTitleComparisonCheckReverse.Text = "No Match";
            this.checkBoxActiveWindowTitleComparisonCheckReverse.UseVisualStyleBackColor = true;
            this.checkBoxActiveWindowTitleComparisonCheckReverse.CheckedChanged += new System.EventHandler(this.checkBoxActiveWindowTitleComparisonCheckReverse_CheckedChanged);
            // 
            // radioButtonRegularExpressionMatch
            // 
            this.radioButtonRegularExpressionMatch.AutoSize = true;
            this.radioButtonRegularExpressionMatch.Location = new System.Drawing.Point(265, 167);
            this.radioButtonRegularExpressionMatch.Name = "radioButtonRegularExpressionMatch";
            this.radioButtonRegularExpressionMatch.Size = new System.Drawing.Size(149, 17);
            this.radioButtonRegularExpressionMatch.TabIndex = 0;
            this.radioButtonRegularExpressionMatch.Text = "Regular Expression Match";
            this.radioButtonRegularExpressionMatch.UseVisualStyleBackColor = true;
            this.radioButtonRegularExpressionMatch.CheckedChanged += new System.EventHandler(this.radioButtonRegularExpressionMatch_CheckedChanged);
            // 
            // checkBoxActiveWindowTitleComparisonCheck
            // 
            this.checkBoxActiveWindowTitleComparisonCheck.AutoSize = true;
            this.checkBoxActiveWindowTitleComparisonCheck.Location = new System.Drawing.Point(174, 121);
            this.checkBoxActiveWindowTitleComparisonCheck.Name = "checkBoxActiveWindowTitleComparisonCheck";
            this.checkBoxActiveWindowTitleComparisonCheck.Size = new System.Drawing.Size(56, 17);
            this.checkBoxActiveWindowTitleComparisonCheck.TabIndex = 0;
            this.checkBoxActiveWindowTitleComparisonCheck.TabStop = false;
            this.checkBoxActiveWindowTitleComparisonCheck.Text = "Match";
            this.checkBoxActiveWindowTitleComparisonCheck.UseVisualStyleBackColor = true;
            this.checkBoxActiveWindowTitleComparisonCheck.CheckedChanged += new System.EventHandler(this.checkBoxActiveWindowTitleComparisonCheck_CheckedChanged);
            // 
            // radioButtonCaseSensitiveMatch
            // 
            this.radioButtonCaseSensitiveMatch.AutoSize = true;
            this.radioButtonCaseSensitiveMatch.Location = new System.Drawing.Point(265, 121);
            this.radioButtonCaseSensitiveMatch.Name = "radioButtonCaseSensitiveMatch";
            this.radioButtonCaseSensitiveMatch.Size = new System.Drawing.Size(128, 17);
            this.radioButtonCaseSensitiveMatch.TabIndex = 0;
            this.radioButtonCaseSensitiveMatch.Text = "Case Sensitive Match";
            this.radioButtonCaseSensitiveMatch.UseVisualStyleBackColor = true;
            this.radioButtonCaseSensitiveMatch.CheckedChanged += new System.EventHandler(this.radioButtonCaseSensitiveMatch_CheckedChanged);
            // 
            // textBoxActiveWindowTitle
            // 
            this.textBoxActiveWindowTitle.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxActiveWindowTitle.Location = new System.Drawing.Point(6, 90);
            this.textBoxActiveWindowTitle.MaxLength = 500;
            this.textBoxActiveWindowTitle.Name = "textBoxActiveWindowTitle";
            this.textBoxActiveWindowTitle.Size = new System.Drawing.Size(665, 25);
            this.textBoxActiveWindowTitle.TabIndex = 0;
            this.textBoxActiveWindowTitle.TabStop = false;
            this.textBoxActiveWindowTitle.TextChanged += new System.EventHandler(this.textBoxActiveWindowTitle_TextChanged);
            // 
            // radioButtonCaseInsensitiveMatch
            // 
            this.radioButtonCaseInsensitiveMatch.AutoSize = true;
            this.radioButtonCaseInsensitiveMatch.Location = new System.Drawing.Point(265, 144);
            this.radioButtonCaseInsensitiveMatch.Name = "radioButtonCaseInsensitiveMatch";
            this.radioButtonCaseInsensitiveMatch.Size = new System.Drawing.Size(135, 17);
            this.radioButtonCaseInsensitiveMatch.TabIndex = 0;
            this.radioButtonCaseInsensitiveMatch.Text = "Case Insensitive Match";
            this.radioButtonCaseInsensitiveMatch.UseVisualStyleBackColor = true;
            this.radioButtonCaseInsensitiveMatch.CheckedChanged += new System.EventHandler(this.radioButtonCaseInsensitiveMatch_CheckedChanged);
            // 
            // tabPageApplicationFocus
            // 
            this.tabPageApplicationFocus.Controls.Add(this.checkBoxEnableApplicationFocus);
            this.tabPageApplicationFocus.Controls.Add(this.labelApplicationFocusHelp);
            this.tabPageApplicationFocus.Controls.Add(this.listBoxProcessList);
            this.tabPageApplicationFocus.Controls.Add(this.numericUpDownApplicationFocusDelayAfter);
            this.tabPageApplicationFocus.Controls.Add(this.numericUpDownApplicationFocusDelayBefore);
            this.tabPageApplicationFocus.Controls.Add(this.labelApplicationFocusDelayAfter);
            this.tabPageApplicationFocus.Controls.Add(this.labelApplicationFocusDelayBefore);
            this.tabPageApplicationFocus.Controls.Add(this.buttonApplicationFocusTest);
            this.tabPageApplicationFocus.Controls.Add(this.buttonApplicationFocusRefresh);
            this.tabPageApplicationFocus.Location = new System.Drawing.Point(4, 22);
            this.tabPageApplicationFocus.Name = "tabPageApplicationFocus";
            this.tabPageApplicationFocus.Size = new System.Drawing.Size(677, 253);
            this.tabPageApplicationFocus.TabIndex = 3;
            this.tabPageApplicationFocus.Text = "Application Focus";
            this.tabPageApplicationFocus.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnableApplicationFocus
            // 
            this.checkBoxEnableApplicationFocus.AutoSize = true;
            this.checkBoxEnableApplicationFocus.Location = new System.Drawing.Point(6, 60);
            this.checkBoxEnableApplicationFocus.Name = "checkBoxEnableApplicationFocus";
            this.checkBoxEnableApplicationFocus.Size = new System.Drawing.Size(347, 17);
            this.checkBoxEnableApplicationFocus.TabIndex = 0;
            this.checkBoxEnableApplicationFocus.TabStop = false;
            this.checkBoxEnableApplicationFocus.Text = "Focus on selected application in process list when screenshot taken";
            this.checkBoxEnableApplicationFocus.UseVisualStyleBackColor = true;
            this.checkBoxEnableApplicationFocus.CheckedChanged += new System.EventHandler(this.checkBoxEnableApplicationFocus_CheckedChanged);
            // 
            // labelApplicationFocusHelp
            // 
            this.labelApplicationFocusHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelApplicationFocusHelp.Location = new System.Drawing.Point(6, 13);
            this.labelApplicationFocusHelp.Name = "labelApplicationFocusHelp";
            this.labelApplicationFocusHelp.Size = new System.Drawing.Size(665, 44);
            this.labelApplicationFocusHelp.TabIndex = 7;
            this.labelApplicationFocusHelp.Text = resources.GetString("labelApplicationFocusHelp.Text");
            // 
            // listBoxProcessList
            // 
            this.listBoxProcessList.Enabled = false;
            this.listBoxProcessList.FormattingEnabled = true;
            this.listBoxProcessList.Location = new System.Drawing.Point(6, 83);
            this.listBoxProcessList.Name = "listBoxProcessList";
            this.listBoxProcessList.Size = new System.Drawing.Size(451, 160);
            this.listBoxProcessList.TabIndex = 0;
            this.listBoxProcessList.TabStop = false;
            this.listBoxProcessList.SelectedIndexChanged += new System.EventHandler(this.listBoxProcessList_SelectedIndexChanged);
            // 
            // numericUpDownApplicationFocusDelayAfter
            // 
            this.numericUpDownApplicationFocusDelayAfter.Enabled = false;
            this.numericUpDownApplicationFocusDelayAfter.Location = new System.Drawing.Point(620, 109);
            this.numericUpDownApplicationFocusDelayAfter.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDownApplicationFocusDelayAfter.Name = "numericUpDownApplicationFocusDelayAfter";
            this.numericUpDownApplicationFocusDelayAfter.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownApplicationFocusDelayAfter.TabIndex = 0;
            this.numericUpDownApplicationFocusDelayAfter.TabStop = false;
            // 
            // numericUpDownApplicationFocusDelayBefore
            // 
            this.numericUpDownApplicationFocusDelayBefore.Enabled = false;
            this.numericUpDownApplicationFocusDelayBefore.Location = new System.Drawing.Point(620, 83);
            this.numericUpDownApplicationFocusDelayBefore.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDownApplicationFocusDelayBefore.Name = "numericUpDownApplicationFocusDelayBefore";
            this.numericUpDownApplicationFocusDelayBefore.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownApplicationFocusDelayBefore.TabIndex = 0;
            this.numericUpDownApplicationFocusDelayBefore.TabStop = false;
            // 
            // labelApplicationFocusDelayAfter
            // 
            this.labelApplicationFocusDelayAfter.AutoSize = true;
            this.labelApplicationFocusDelayAfter.Enabled = false;
            this.labelApplicationFocusDelayAfter.Location = new System.Drawing.Point(463, 111);
            this.labelApplicationFocusDelayAfter.Name = "labelApplicationFocusDelayAfter";
            this.labelApplicationFocusDelayAfter.Size = new System.Drawing.Size(127, 13);
            this.labelApplicationFocusDelayAfter.TabIndex = 0;
            this.labelApplicationFocusDelayAfter.Text = "Delay After (milliseconds):";
            // 
            // labelApplicationFocusDelayBefore
            // 
            this.labelApplicationFocusDelayBefore.AutoSize = true;
            this.labelApplicationFocusDelayBefore.Enabled = false;
            this.labelApplicationFocusDelayBefore.Location = new System.Drawing.Point(463, 85);
            this.labelApplicationFocusDelayBefore.Name = "labelApplicationFocusDelayBefore";
            this.labelApplicationFocusDelayBefore.Size = new System.Drawing.Size(136, 13);
            this.labelApplicationFocusDelayBefore.TabIndex = 0;
            this.labelApplicationFocusDelayBefore.Text = "Delay Before (milliseconds):";
            // 
            // buttonApplicationFocusTest
            // 
            this.buttonApplicationFocusTest.Enabled = false;
            this.buttonApplicationFocusTest.Location = new System.Drawing.Point(466, 220);
            this.buttonApplicationFocusTest.Name = "buttonApplicationFocusTest";
            this.buttonApplicationFocusTest.Size = new System.Drawing.Size(96, 23);
            this.buttonApplicationFocusTest.TabIndex = 0;
            this.buttonApplicationFocusTest.TabStop = false;
            this.buttonApplicationFocusTest.Text = "Test";
            this.buttonApplicationFocusTest.UseVisualStyleBackColor = true;
            this.buttonApplicationFocusTest.Click += new System.EventHandler(this.buttonApplicationFocusTest_Click);
            // 
            // buttonApplicationFocusRefresh
            // 
            this.buttonApplicationFocusRefresh.Enabled = false;
            this.buttonApplicationFocusRefresh.Location = new System.Drawing.Point(575, 220);
            this.buttonApplicationFocusRefresh.Name = "buttonApplicationFocusRefresh";
            this.buttonApplicationFocusRefresh.Size = new System.Drawing.Size(96, 23);
            this.buttonApplicationFocusRefresh.TabIndex = 0;
            this.buttonApplicationFocusRefresh.TabStop = false;
            this.buttonApplicationFocusRefresh.Text = "Refresh";
            this.buttonApplicationFocusRefresh.UseVisualStyleBackColor = true;
            this.buttonApplicationFocusRefresh.Click += new System.EventHandler(this.buttonApplicationFocusRefresh_Click);
            // 
            // tabPageSecurity
            // 
            this.tabPageSecurity.Controls.Add(this.labelPassphrase);
            this.tabPageSecurity.Controls.Add(this.labelHash);
            this.tabPageSecurity.Controls.Add(this.textBoxPassphraseHash);
            this.tabPageSecurity.Controls.Add(this.labelLastUpdated);
            this.tabPageSecurity.Controls.Add(this.buttonClearPassphrase);
            this.tabPageSecurity.Controls.Add(this.buttonSetPassphrase);
            this.tabPageSecurity.Controls.Add(this.labelSecurityHelp);
            this.tabPageSecurity.Controls.Add(this.textBoxPassphrase);
            this.tabPageSecurity.Location = new System.Drawing.Point(4, 22);
            this.tabPageSecurity.Name = "tabPageSecurity";
            this.tabPageSecurity.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSecurity.Size = new System.Drawing.Size(677, 253);
            this.tabPageSecurity.TabIndex = 4;
            this.tabPageSecurity.Text = "Security";
            this.tabPageSecurity.UseVisualStyleBackColor = true;
            // 
            // labelPassphrase
            // 
            this.labelPassphrase.AutoSize = true;
            this.labelPassphrase.Location = new System.Drawing.Point(6, 85);
            this.labelPassphrase.Name = "labelPassphrase";
            this.labelPassphrase.Size = new System.Drawing.Size(65, 13);
            this.labelPassphrase.TabIndex = 5;
            this.labelPassphrase.Text = "Passphrase:";
            // 
            // labelHash
            // 
            this.labelHash.AutoSize = true;
            this.labelHash.Location = new System.Drawing.Point(6, 59);
            this.labelHash.Name = "labelHash";
            this.labelHash.Size = new System.Drawing.Size(87, 13);
            this.labelHash.TabIndex = 4;
            this.labelHash.Text = "Hash (SHA-512):";
            // 
            // textBoxPassphraseHash
            // 
            this.textBoxPassphraseHash.Location = new System.Drawing.Point(121, 56);
            this.textBoxPassphraseHash.MaxLength = 30;
            this.textBoxPassphraseHash.Name = "textBoxPassphraseHash";
            this.textBoxPassphraseHash.ReadOnly = true;
            this.textBoxPassphraseHash.Size = new System.Drawing.Size(550, 20);
            this.textBoxPassphraseHash.TabIndex = 3;
            this.textBoxPassphraseHash.TabStop = false;
            // 
            // labelLastUpdated
            // 
            this.labelLastUpdated.AutoSize = true;
            this.labelLastUpdated.Location = new System.Drawing.Point(6, 113);
            this.labelLastUpdated.Name = "labelLastUpdated";
            this.labelLastUpdated.Size = new System.Drawing.Size(72, 13);
            this.labelLastUpdated.TabIndex = 2;
            this.labelLastUpdated.Text = "Last updated:";
            // 
            // buttonClearPassphrase
            // 
            this.buttonClearPassphrase.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonClearPassphrase.Location = new System.Drawing.Point(492, 108);
            this.buttonClearPassphrase.Name = "buttonClearPassphrase";
            this.buttonClearPassphrase.Size = new System.Drawing.Size(179, 23);
            this.buttonClearPassphrase.TabIndex = 1;
            this.buttonClearPassphrase.TabStop = false;
            this.buttonClearPassphrase.Text = "Clear Passphrase";
            this.buttonClearPassphrase.UseVisualStyleBackColor = true;
            this.buttonClearPassphrase.Click += new System.EventHandler(this.buttonClearPassphrase_Click);
            // 
            // buttonSetPassphrase
            // 
            this.buttonSetPassphrase.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSetPassphrase.Location = new System.Drawing.Point(307, 108);
            this.buttonSetPassphrase.Name = "buttonSetPassphrase";
            this.buttonSetPassphrase.Size = new System.Drawing.Size(179, 23);
            this.buttonSetPassphrase.TabIndex = 0;
            this.buttonSetPassphrase.TabStop = false;
            this.buttonSetPassphrase.Text = "Set Passphrase";
            this.buttonSetPassphrase.UseVisualStyleBackColor = true;
            this.buttonSetPassphrase.Click += new System.EventHandler(this.buttonSetPassphrase_Click);
            // 
            // labelSecurityHelp
            // 
            this.labelSecurityHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelSecurityHelp.Location = new System.Drawing.Point(6, 13);
            this.labelSecurityHelp.Name = "labelSecurityHelp";
            this.labelSecurityHelp.Size = new System.Drawing.Size(665, 32);
            this.labelSecurityHelp.TabIndex = 0;
            this.labelSecurityHelp.Text = resources.GetString("labelSecurityHelp.Text");
            // 
            // textBoxPassphrase
            // 
            this.textBoxPassphrase.Location = new System.Drawing.Point(121, 82);
            this.textBoxPassphrase.MaxLength = 30;
            this.textBoxPassphrase.Name = "textBoxPassphrase";
            this.textBoxPassphrase.Size = new System.Drawing.Size(550, 20);
            this.textBoxPassphrase.TabIndex = 0;
            this.textBoxPassphrase.TabStop = false;
            // 
            // tabPageKeyboardShortcuts
            // 
            this.tabPageKeyboardShortcuts.Controls.Add(this.labelRegionSelectEdit);
            this.tabPageKeyboardShortcuts.Controls.Add(this.labelStopScreenCapture);
            this.tabPageKeyboardShortcuts.Controls.Add(this.textBoxKeyboardShortcutRegionSelectEditKey);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1);
            this.tabPageKeyboardShortcuts.Controls.Add(this.labelStartScreenCapture);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectEditModifier2);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutStartScreenCaptureModifier1);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectEditModifier1);
            this.tabPageKeyboardShortcuts.Controls.Add(this.labelCaptureNowEdit);
            this.tabPageKeyboardShortcuts.Controls.Add(this.textBoxKeyboardShortcutRegionSelectClipboardKey);
            this.tabPageKeyboardShortcuts.Controls.Add(this.textBoxKeyboardShortcutStopScreenCaptureKey);
            this.tabPageKeyboardShortcuts.Controls.Add(this.labelRegionSelectAutoSave);
            this.tabPageKeyboardShortcuts.Controls.Add(this.labelCaptureNowArchive);
            this.tabPageKeyboardShortcuts.Controls.Add(this.labelRegionSelectClipboard);
            this.tabPageKeyboardShortcuts.Controls.Add(this.textBoxKeyboardShortcutRegionSelectAutoSaveKey);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutStartScreenCaptureModifier2);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutStopScreenCaptureModifier2);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2);
            this.tabPageKeyboardShortcuts.Controls.Add(this.textBoxKeyboardShortcutCaptureNowEditKey);
            this.tabPageKeyboardShortcuts.Controls.Add(this.textBoxKeyboardShortcutStartScreenCaptureKey);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutStopScreenCaptureModifier1);
            this.tabPageKeyboardShortcuts.Controls.Add(this.checkBoxUseKeyboardShortcuts);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutCaptureNowEditModifier2);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutCaptureNowEditModifier1);
            this.tabPageKeyboardShortcuts.Controls.Add(this.textBoxKeyboardShortcutCaptureNowArchiveKey);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1);
            this.tabPageKeyboardShortcuts.Controls.Add(this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2);
            this.tabPageKeyboardShortcuts.Location = new System.Drawing.Point(4, 22);
            this.tabPageKeyboardShortcuts.Name = "tabPageKeyboardShortcuts";
            this.tabPageKeyboardShortcuts.Size = new System.Drawing.Size(677, 253);
            this.tabPageKeyboardShortcuts.TabIndex = 5;
            this.tabPageKeyboardShortcuts.Text = "Keyboard Shortcuts";
            this.tabPageKeyboardShortcuts.UseVisualStyleBackColor = true;
            // 
            // labelRegionSelectEdit
            // 
            this.labelRegionSelectEdit.AutoSize = true;
            this.labelRegionSelectEdit.Location = new System.Drawing.Point(43, 209);
            this.labelRegionSelectEdit.Name = "labelRegionSelectEdit";
            this.labelRegionSelectEdit.Size = new System.Drawing.Size(223, 13);
            this.labelRegionSelectEdit.TabIndex = 37;
            this.labelRegionSelectEdit.Text = "Region Select -> Clipboard / Auto Save / Edit";
            // 
            // labelStopScreenCapture
            // 
            this.labelStopScreenCapture.AutoSize = true;
            this.labelStopScreenCapture.Location = new System.Drawing.Point(43, 74);
            this.labelStopScreenCapture.Name = "labelStopScreenCapture";
            this.labelStopScreenCapture.Size = new System.Drawing.Size(106, 13);
            this.labelStopScreenCapture.TabIndex = 13;
            this.labelStopScreenCapture.Text = "Stop Screen Capture";
            // 
            // textBoxKeyboardShortcutRegionSelectEditKey
            // 
            this.textBoxKeyboardShortcutRegionSelectEditKey.Location = new System.Drawing.Point(467, 206);
            this.textBoxKeyboardShortcutRegionSelectEditKey.MaxLength = 1;
            this.textBoxKeyboardShortcutRegionSelectEditKey.Name = "textBoxKeyboardShortcutRegionSelectEditKey";
            this.textBoxKeyboardShortcutRegionSelectEditKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutRegionSelectEditKey.TabIndex = 40;
            // 
            // comboBoxKeyboardShortcutRegionSelectClipboardModifier1
            // 
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Location = new System.Drawing.Point(281, 152);
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Name = "comboBoxKeyboardShortcutRegionSelectClipboardModifier1";
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.TabIndex = 30;
            // 
            // labelStartScreenCapture
            // 
            this.labelStartScreenCapture.AutoSize = true;
            this.labelStartScreenCapture.Location = new System.Drawing.Point(43, 47);
            this.labelStartScreenCapture.Name = "labelStartScreenCapture";
            this.labelStartScreenCapture.Size = new System.Drawing.Size(106, 13);
            this.labelStartScreenCapture.TabIndex = 9;
            this.labelStartScreenCapture.Text = "Start Screen Capture";
            // 
            // comboBoxKeyboardShortcutRegionSelectEditModifier2
            // 
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.Location = new System.Drawing.Point(374, 206);
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.Name = "comboBoxKeyboardShortcutRegionSelectEditModifier2";
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.TabIndex = 39;
            // 
            // comboBoxKeyboardShortcutRegionSelectClipboardModifier2
            // 
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Location = new System.Drawing.Point(374, 152);
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Name = "comboBoxKeyboardShortcutRegionSelectClipboardModifier2";
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.TabIndex = 31;
            // 
            // comboBoxKeyboardShortcutStartScreenCaptureModifier1
            // 
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.Location = new System.Drawing.Point(281, 44);
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.Name = "comboBoxKeyboardShortcutStartScreenCaptureModifier1";
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.TabIndex = 10;
            // 
            // comboBoxKeyboardShortcutRegionSelectEditModifier1
            // 
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.Location = new System.Drawing.Point(281, 206);
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.Name = "comboBoxKeyboardShortcutRegionSelectEditModifier1";
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.TabIndex = 38;
            // 
            // labelCaptureNowEdit
            // 
            this.labelCaptureNowEdit.AutoSize = true;
            this.labelCaptureNowEdit.Location = new System.Drawing.Point(43, 128);
            this.labelCaptureNowEdit.Name = "labelCaptureNowEdit";
            this.labelCaptureNowEdit.Size = new System.Drawing.Size(98, 13);
            this.labelCaptureNowEdit.TabIndex = 21;
            this.labelCaptureNowEdit.Text = "Capture Now / Edit";
            // 
            // textBoxKeyboardShortcutRegionSelectClipboardKey
            // 
            this.textBoxKeyboardShortcutRegionSelectClipboardKey.Location = new System.Drawing.Point(467, 152);
            this.textBoxKeyboardShortcutRegionSelectClipboardKey.MaxLength = 1;
            this.textBoxKeyboardShortcutRegionSelectClipboardKey.Name = "textBoxKeyboardShortcutRegionSelectClipboardKey";
            this.textBoxKeyboardShortcutRegionSelectClipboardKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutRegionSelectClipboardKey.TabIndex = 32;
            // 
            // textBoxKeyboardShortcutStopScreenCaptureKey
            // 
            this.textBoxKeyboardShortcutStopScreenCaptureKey.Location = new System.Drawing.Point(467, 71);
            this.textBoxKeyboardShortcutStopScreenCaptureKey.MaxLength = 1;
            this.textBoxKeyboardShortcutStopScreenCaptureKey.Name = "textBoxKeyboardShortcutStopScreenCaptureKey";
            this.textBoxKeyboardShortcutStopScreenCaptureKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutStopScreenCaptureKey.TabIndex = 16;
            // 
            // labelRegionSelectAutoSave
            // 
            this.labelRegionSelectAutoSave.AutoSize = true;
            this.labelRegionSelectAutoSave.Location = new System.Drawing.Point(43, 182);
            this.labelRegionSelectAutoSave.Name = "labelRegionSelectAutoSave";
            this.labelRegionSelectAutoSave.Size = new System.Drawing.Size(194, 13);
            this.labelRegionSelectAutoSave.TabIndex = 33;
            this.labelRegionSelectAutoSave.Text = "Region Select -> Clipboard / Auto Save";
            // 
            // labelCaptureNowArchive
            // 
            this.labelCaptureNowArchive.AutoSize = true;
            this.labelCaptureNowArchive.Location = new System.Drawing.Point(43, 101);
            this.labelCaptureNowArchive.Name = "labelCaptureNowArchive";
            this.labelCaptureNowArchive.Size = new System.Drawing.Size(116, 13);
            this.labelCaptureNowArchive.TabIndex = 17;
            this.labelCaptureNowArchive.Text = "Capture Now / Archive";
            // 
            // labelRegionSelectClipboard
            // 
            this.labelRegionSelectClipboard.AutoSize = true;
            this.labelRegionSelectClipboard.Location = new System.Drawing.Point(43, 155);
            this.labelRegionSelectClipboard.Name = "labelRegionSelectClipboard";
            this.labelRegionSelectClipboard.Size = new System.Drawing.Size(133, 13);
            this.labelRegionSelectClipboard.TabIndex = 29;
            this.labelRegionSelectClipboard.Text = "Region Select -> Clipboard";
            // 
            // textBoxKeyboardShortcutRegionSelectAutoSaveKey
            // 
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey.Location = new System.Drawing.Point(467, 179);
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey.MaxLength = 1;
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey.Name = "textBoxKeyboardShortcutRegionSelectAutoSaveKey";
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey.TabIndex = 36;
            // 
            // comboBoxKeyboardShortcutStartScreenCaptureModifier2
            // 
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.Location = new System.Drawing.Point(374, 44);
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.Name = "comboBoxKeyboardShortcutStartScreenCaptureModifier2";
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.TabIndex = 11;
            // 
            // comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1
            // 
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Location = new System.Drawing.Point(281, 179);
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Name = "comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1";
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.TabIndex = 34;
            // 
            // comboBoxKeyboardShortcutStopScreenCaptureModifier2
            // 
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.Location = new System.Drawing.Point(374, 71);
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.Name = "comboBoxKeyboardShortcutStopScreenCaptureModifier2";
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.TabIndex = 15;
            // 
            // comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2
            // 
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Location = new System.Drawing.Point(374, 179);
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Name = "comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2";
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.TabIndex = 35;
            // 
            // textBoxKeyboardShortcutCaptureNowEditKey
            // 
            this.textBoxKeyboardShortcutCaptureNowEditKey.Location = new System.Drawing.Point(467, 125);
            this.textBoxKeyboardShortcutCaptureNowEditKey.MaxLength = 1;
            this.textBoxKeyboardShortcutCaptureNowEditKey.Name = "textBoxKeyboardShortcutCaptureNowEditKey";
            this.textBoxKeyboardShortcutCaptureNowEditKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutCaptureNowEditKey.TabIndex = 24;
            // 
            // textBoxKeyboardShortcutStartScreenCaptureKey
            // 
            this.textBoxKeyboardShortcutStartScreenCaptureKey.Location = new System.Drawing.Point(467, 44);
            this.textBoxKeyboardShortcutStartScreenCaptureKey.MaxLength = 1;
            this.textBoxKeyboardShortcutStartScreenCaptureKey.Name = "textBoxKeyboardShortcutStartScreenCaptureKey";
            this.textBoxKeyboardShortcutStartScreenCaptureKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutStartScreenCaptureKey.TabIndex = 12;
            // 
            // comboBoxKeyboardShortcutStopScreenCaptureModifier1
            // 
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.Location = new System.Drawing.Point(281, 71);
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.Name = "comboBoxKeyboardShortcutStopScreenCaptureModifier1";
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.TabIndex = 14;
            // 
            // checkBoxUseKeyboardShortcuts
            // 
            this.checkBoxUseKeyboardShortcuts.AutoSize = true;
            this.checkBoxUseKeyboardShortcuts.Location = new System.Drawing.Point(11, 13);
            this.checkBoxUseKeyboardShortcuts.Name = "checkBoxUseKeyboardShortcuts";
            this.checkBoxUseKeyboardShortcuts.Size = new System.Drawing.Size(138, 17);
            this.checkBoxUseKeyboardShortcuts.TabIndex = 1;
            this.checkBoxUseKeyboardShortcuts.Text = "Use keyboard shortcuts";
            this.checkBoxUseKeyboardShortcuts.UseVisualStyleBackColor = true;
            // 
            // comboBoxKeyboardShortcutCaptureNowEditModifier2
            // 
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.Location = new System.Drawing.Point(374, 125);
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.Name = "comboBoxKeyboardShortcutCaptureNowEditModifier2";
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.TabIndex = 23;
            // 
            // comboBoxKeyboardShortcutCaptureNowEditModifier1
            // 
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.Location = new System.Drawing.Point(281, 125);
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.Name = "comboBoxKeyboardShortcutCaptureNowEditModifier1";
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.TabIndex = 22;
            // 
            // textBoxKeyboardShortcutCaptureNowArchiveKey
            // 
            this.textBoxKeyboardShortcutCaptureNowArchiveKey.Location = new System.Drawing.Point(467, 98);
            this.textBoxKeyboardShortcutCaptureNowArchiveKey.MaxLength = 1;
            this.textBoxKeyboardShortcutCaptureNowArchiveKey.Name = "textBoxKeyboardShortcutCaptureNowArchiveKey";
            this.textBoxKeyboardShortcutCaptureNowArchiveKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutCaptureNowArchiveKey.TabIndex = 20;
            // 
            // comboBoxKeyboardShortcutCaptureNowArchiveModifier1
            // 
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Location = new System.Drawing.Point(281, 98);
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Name = "comboBoxKeyboardShortcutCaptureNowArchiveModifier1";
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.TabIndex = 18;
            // 
            // comboBoxKeyboardShortcutCaptureNowArchiveModifier2
            // 
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Location = new System.Drawing.Point(374, 98);
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Name = "comboBoxKeyboardShortcutCaptureNowArchiveModifier2";
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.TabIndex = 19;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Location = new System.Drawing.Point(578, 311);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(99, 23);
            this.buttonOK.TabIndex = 31;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
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
            this.labelHelp.Size = new System.Drawing.Size(685, 17);
            this.labelHelp.TabIndex = 33;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormSetup
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 346);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControlSetup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Auto Screen Capture - Setup";
            this.Load += new System.EventHandler(this.FormSetup_Load);
            this.Shown += new System.EventHandler(this.FormSetup_Shown);
            this.tabControlSetup.ResumeLayout(false);
            this.tabPageScreenshotsFolder.ResumeLayout(false);
            this.tabPageScreenshotsFolder.PerformLayout();
            this.tabPageFilenamePattern.ResumeLayout(false);
            this.tabPageFilenamePattern.PerformLayout();
            this.tabPageImageFormat.ResumeLayout(false);
            this.tabPageImageFormat.PerformLayout();
            this.tabPageInterval.ResumeLayout(false);
            this.tabPageInterval.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            this.tabPageOptimizeScreenCapture.ResumeLayout(false);
            this.tabPageOptimizeScreenCapture.PerformLayout();
            this.tabPageLabels.ResumeLayout(false);
            this.tabPageLabels.PerformLayout();
            this.tabPageActiveWindowTitle.ResumeLayout(false);
            this.tabPageActiveWindowTitle.PerformLayout();
            this.tabPageApplicationFocus.ResumeLayout(false);
            this.tabPageApplicationFocus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayBefore)).EndInit();
            this.tabPageSecurity.ResumeLayout(false);
            this.tabPageSecurity.PerformLayout();
            this.tabPageKeyboardShortcuts.ResumeLayout(false);
            this.tabPageKeyboardShortcuts.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSetup;
        private System.Windows.Forms.TabPage tabPageInterval;
        private System.Windows.Forms.TabPage tabPageLabels;
        private System.Windows.Forms.TabPage tabPageActiveWindowTitle;
        private System.Windows.Forms.TabPage tabPageApplicationFocus;
        private System.Windows.Forms.TabPage tabPageSecurity;
        private System.Windows.Forms.TabPage tabPageKeyboardShortcuts;
        private System.Windows.Forms.Label labelSecondsInterval;
        private System.Windows.Forms.Label labelMinutesInterval;
        private System.Windows.Forms.Label labelHoursInterval;
        private System.Windows.Forms.Label labelSecurityHelp;
        private System.Windows.Forms.Button buttonSetPassphrase;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Label labelStopScreenCapture;
        private System.Windows.Forms.Label labelStartScreenCapture;
        private System.Windows.Forms.Label labelRegionSelectEdit;
        private System.Windows.Forms.Label labelRegionSelectAutoSave;
        private System.Windows.Forms.Label labelRegionSelectClipboard;
        private System.Windows.Forms.Label labelCaptureNowEdit;
        private System.Windows.Forms.Label labelCaptureNowArchive;
        private System.Windows.Forms.Button buttonClearPassphrase;
        private System.Windows.Forms.Label labelLastUpdated;
        private System.Windows.Forms.Label labelHash;
        private System.Windows.Forms.CheckBox checkBoxUseKeyboardShortcuts;
        private System.Windows.Forms.Label labelPassphrase;
        private System.Windows.Forms.Label labelIntervalHelp;
        private System.Windows.Forms.Label labelInitialCaptureHelp;
        private System.Windows.Forms.Label labelLimitHelp;
        private System.Windows.Forms.TabPage tabPageOptimizeScreenCapture;
        private System.Windows.Forms.Button buttonAddScreenshotLabelToList;
        private System.Windows.Forms.Label labelScreenshotLabel;
        private System.Windows.Forms.TextBox textBoxScreenshotLabel;
        private System.Windows.Forms.Label labelScreenshotLabelHelp;
        private System.Windows.Forms.Label labelActiveWindowTitle;
        private System.Windows.Forms.Label labelActiveWindowTitleHelp;
        private System.Windows.Forms.Label labelActiveWindowTitleTest;
        private System.Windows.Forms.Label labelMatchTestResult;
        private System.Windows.Forms.Label labelMatchHelp;
        private System.Windows.Forms.TextBox textBoxRegularExpressionHelp;
        private System.Windows.Forms.CheckBox checkBoxEnableApplicationFocus;
        private System.Windows.Forms.Label labelApplicationFocusHelp;
        private System.Windows.Forms.Label labelOptimizeScreenCaptureHelp;
        private System.Windows.Forms.TabPage tabPageScreenshotsFolder;
        private System.Windows.Forms.TextBox textBoxScreenshotsFolder;
        private System.Windows.Forms.Button buttonScreenshotsFolderBrowseFolder;
        private System.Windows.Forms.Label labelScreenshotsFolderHelp;
        private System.Windows.Forms.Button buttonScreenshotsFolderApplyToAllRegions;
        private System.Windows.Forms.Button buttonScreenshotsFolderApplyToAllScreens;
        private System.Windows.Forms.Label labelScreenshotsFolderApplyToAllRegions;
        private System.Windows.Forms.Label labelScreenshotsFolderApplyToAllScreens;
        private System.Windows.Forms.TabPage tabPageFilenamePattern;
        private System.Windows.Forms.TabPage tabPageImageFormat;
        private System.Windows.Forms.TextBox textBoxFilenamePattern;
        private System.Windows.Forms.Label labelFilenamePatternHelp;
        private System.Windows.Forms.TextBox textBoxMacroPreview;
        private System.Windows.Forms.Button buttonFilenamePatternApplyToAllRegions;
        private System.Windows.Forms.Button buttonFilenamePatternApplyToAllScreens;
        private System.Windows.Forms.ListBox listBoxMacroTags;
        private System.Windows.Forms.Label labelFilenamePatternApplyHelp;
        private System.Windows.Forms.RadioButton radioButtonImageFormatWmf;
        private System.Windows.Forms.RadioButton radioButtonImageFormatTiff;
        private System.Windows.Forms.RadioButton radioButtonImageFormatPng;
        private System.Windows.Forms.RadioButton radioButtonImageFormatJpeg;
        private System.Windows.Forms.RadioButton radioButtonImageFormatGif;
        private System.Windows.Forms.RadioButton radioButtonImageFormatEmf;
        private System.Windows.Forms.RadioButton radioButtonImageFormatBmp;
        private System.Windows.Forms.Label labelImageFormatHelp;
        private System.Windows.Forms.Button buttonImageFormatApplyToAllRegions;
        private System.Windows.Forms.Button buttonImageFormatApplyToAllScreens;
        private System.Windows.Forms.Label labelFilenamePatternPreview;
        private System.Windows.Forms.Label labelFilenamePatternFilename;

        /// <summary>
        /// The checkbox control for "Initial Capture".
        /// </summary>
        public System.Windows.Forms.CheckBox checkBoxInitialScreenshot;

        /// <summary>
        /// The numeric up/down control for "Limit".
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownCaptureLimit;

        /// <summary>
        /// The checkbox control for "Limit".
        /// </summary>
        public System.Windows.Forms.CheckBox checkBoxCaptureLimit;

        /// <summary>
        /// The numeric up/down control for "Seconds".
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;

        /// <summary>
        /// The numeric up/down control for "Minutes".
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;

        /// <summary>
        /// The numeric up/down control for "Hours".
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;

        /// <summary>
        /// The checkbox control for applying a label.
        /// </summary>
        public System.Windows.Forms.CheckBox checkBoxScreenshotLabel;

        /// <summary>
        /// The radio button control for "Regular Expression Match".
        /// </summary>
        public System.Windows.Forms.RadioButton radioButtonRegularExpressionMatch;

        /// <summary>
        /// The radio button control for "Case Sensitive Match".
        /// </summary>
        public System.Windows.Forms.RadioButton radioButtonCaseSensitiveMatch;

        /// <summary>
        /// The radio button control for "Case Insensitive Match".
        /// </summary>
        public System.Windows.Forms.RadioButton radioButtonCaseInsensitiveMatch;

        /// <summary>
        /// The textbox control for the active window title text to compare with the active window title.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxActiveWindowTitle;

        /// <summary>
        /// The checkbox control for enabling an active window title comparison check.
        /// </summary>
        public System.Windows.Forms.CheckBox checkBoxActiveWindowTitleComparisonCheck;

        /// <summary>
        /// The radio button control for "Case Sensitive Match".
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownApplicationFocusDelayAfter;

        /// <summary>
        /// The numeric up/down control for Application Focus Delay Before (in milliseconds).
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownApplicationFocusDelayBefore;

        /// <summary>
        /// A label for Application Focus Delay After.
        /// </summary>
        public System.Windows.Forms.Label labelApplicationFocusDelayAfter;

        /// <summary>
        /// A label for Application Focus Delay Before.
        /// </summary>
        public System.Windows.Forms.Label labelApplicationFocusDelayBefore;

        /// <summary>
        /// A button for testing the Application Focus feature.
        /// </summary>
        public System.Windows.Forms.Button buttonApplicationFocusTest;

        /// <summary>
        /// A button for refreshing the process list for Application Focus.
        /// </summary>
        public System.Windows.Forms.Button buttonApplicationFocusRefresh;

        /// <summary>
        /// The textbox field for the passphrase to use when locking down the application from unauthorized usage.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxPassphrase;

        /// <summary>
        /// Keyboard shortcut for doing Stop Screen Capture.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutStopScreenCaptureKey;

        /// <summary>
        /// Keyboard shortcut for doing Stop Screen Capture.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutStopScreenCaptureModifier2;

        /// <summary>
        /// Keyboard shortcut for doing Stop Screen Capture.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutStopScreenCaptureModifier1;

        /// <summary>
        /// Keyboard shortcut for doing Start Screen Capture.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutStartScreenCaptureKey;

        /// <summary>
        /// Keyboard shortcut for doing Start Screen Capture.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutStartScreenCaptureModifier2;

        /// <summary>
        /// Keyboard shortcut for doing Start Screen Capture.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutStartScreenCaptureModifier1;

        /// <summary>
        /// Keyboard shortcut for doing Region Select Edit.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutRegionSelectEditKey;

        /// <summary>
        /// Keyboard shortcut for doing Region Select Edit.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectEditModifier2;

        /// <summary>
        /// Keyboard shortcut for doing Region Select Edit.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectEditModifier1;

        /// <summary>
        /// Keyboard shortcut for doing Region Select Auto Save.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutRegionSelectAutoSaveKey;

        /// <summary>
        /// Keyboard shortcut for doing Region Select Auto Save.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2;

        /// <summary>
        /// Keyboard shortcut for doing Region Select Auto Save.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1;

        /// <summary>
        /// Keyboard shortcut for doing Region Select Clipboard.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutRegionSelectClipboardKey;

        /// <summary>
        /// Keyboard shortcut for doing Region Select Clipboard.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectClipboardModifier2;

        /// <summary>
        /// Keyboard shortcut for doing Region Select Clipboard.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectClipboardModifier1;

        /// <summary>
        /// Keyboard shortcut for doing Capture Now Edit.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutCaptureNowEditKey;

        /// <summary>
        /// Keyboard shortcut for doing Capture Now Edit.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutCaptureNowEditModifier2;

        /// <summary>
        /// Keyboard shortcut for doing Capture Now Edit.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutCaptureNowEditModifier1;

        /// <summary>
        /// Keyboard shortcut for doing Capture Now Archive.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutCaptureNowArchiveKey;

        /// <summary>
        /// Keyboard shortcut for doing Capture Now Archive.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutCaptureNowArchiveModifier2;

        /// <summary>
        /// Keyboard shortcut for doing Capture Now Archive.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutCaptureNowArchiveModifier1;

        /// <summary>
        /// A checkbox control for the active window title comparison check but in reverse logic.
        /// </summary>
        public System.Windows.Forms.CheckBox checkBoxActiveWindowTitleComparisonCheckReverse;

        /// <summary>
        /// The passphrase hash used for a password-protected screen capture session.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxPassphraseHash;

        /// <summary>
        /// A list of screenshot labels.
        /// </summary>
        public System.Windows.Forms.ListBox listBoxScreenshotLabel;

        /// <summary>
        /// A textbox field for testing the active window title text comparison check.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxActiveWindowTitleTest;

        /// <summary>
        /// A checkbox to enable or disable Optimize Screen Capture.
        /// </summary>
        public System.Windows.Forms.CheckBox checkBoxOptimizeScreenCapture;

        /// <summary>
        /// A listbox control for the process list.
        /// </summary>
        public System.Windows.Forms.ListBox listBoxProcessList;
    }
}