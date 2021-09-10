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
            this.tabPageInterval = new System.Windows.Forms.TabPage();
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
            this.tabPageLabels = new System.Windows.Forms.TabPage();
            this.checkBoxScreenshotLabel = new System.Windows.Forms.CheckBox();
            this.comboBoxScreenshotLabel = new System.Windows.Forms.ComboBox();
            this.tabPageActiveWindowTitle = new System.Windows.Forms.TabPage();
            this.groupBoxActiveWindowTitle = new System.Windows.Forms.GroupBox();
            this.buttonDynamicRegexValidator = new System.Windows.Forms.Button();
            this.radioButtonRegularExpressionMatch = new System.Windows.Forms.RadioButton();
            this.radioButtonCaseSensitiveMatch = new System.Windows.Forms.RadioButton();
            this.radioButtonCaseInsensitiveMatch = new System.Windows.Forms.RadioButton();
            this.textBoxActiveWindowTitle = new System.Windows.Forms.TextBox();
            this.checkBoxActiveWindowTitleComparisonCheck = new System.Windows.Forms.CheckBox();
            this.tabPageApplicationFocus = new System.Windows.Forms.TabPage();
            this.groupBoxApplicationFocus = new System.Windows.Forms.GroupBox();
            this.numericUpDownApplicationFocusDelayAfter = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownApplicationFocusDelayBefore = new System.Windows.Forms.NumericUpDown();
            this.labelApplicationFocusDelayAfter = new System.Windows.Forms.Label();
            this.labelApplicationFocusDelayBefore = new System.Windows.Forms.Label();
            this.buttonApplicationFocusTest = new System.Windows.Forms.Button();
            this.buttonApplicationFocusRefresh = new System.Windows.Forms.Button();
            this.comboBoxProcessList = new System.Windows.Forms.ComboBox();
            this.tabPageSecurity = new System.Windows.Forms.TabPage();
            this.groupBoxSecurity = new System.Windows.Forms.GroupBox();
            this.labelPasswordDescription = new System.Windows.Forms.Label();
            this.buttonSetPassphrase = new System.Windows.Forms.Button();
            this.textBoxPassphrase = new System.Windows.Forms.TextBox();
            this.tabPageKeyboardShortcuts = new System.Windows.Forms.TabPage();
            this.checkBoxUseKeyboardShortcuts = new System.Windows.Forms.CheckBox();
            this.tabControlKeyboardShortcuts = new System.Windows.Forms.TabControl();
            this.tabPageScreenCapture = new System.Windows.Forms.TabPage();
            this.labelStopScreenCapture = new System.Windows.Forms.Label();
            this.labelStartScreenCapture = new System.Windows.Forms.Label();
            this.textBoxKeyboardShortcutStopScreenCaptureKey = new System.Windows.Forms.TextBox();
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1 = new System.Windows.Forms.ComboBox();
            this.textBoxKeyboardShortcutStartScreenCaptureKey = new System.Windows.Forms.TextBox();
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1 = new System.Windows.Forms.ComboBox();
            this.tabPageRegionSelect = new System.Windows.Forms.TabPage();
            this.labelRegionSelectEdit = new System.Windows.Forms.Label();
            this.textBoxKeyboardShortcutRegionSelectEditKey = new System.Windows.Forms.TextBox();
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1 = new System.Windows.Forms.ComboBox();
            this.labelRegionSelectAutoSave = new System.Windows.Forms.Label();
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey = new System.Windows.Forms.TextBox();
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1 = new System.Windows.Forms.ComboBox();
            this.labelRegionSelectClipboard = new System.Windows.Forms.Label();
            this.textBoxKeyboardShortcutRegionSelectClipboardKey = new System.Windows.Forms.TextBox();
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1 = new System.Windows.Forms.ComboBox();
            this.tabPageCaptureNow = new System.Windows.Forms.TabPage();
            this.labelCaptureNowEdit = new System.Windows.Forms.Label();
            this.labelCaptureNowArchive = new System.Windows.Forms.Label();
            this.textBoxKeyboardShortcutCaptureNowEditKey = new System.Windows.Forms.TextBox();
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1 = new System.Windows.Forms.ComboBox();
            this.textBoxKeyboardShortcutCaptureNowArchiveKey = new System.Windows.Forms.TextBox();
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2 = new System.Windows.Forms.ComboBox();
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1 = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelHelp = new System.Windows.Forms.Label();
            this.checkBoxActiveWindowTitleComparisonCheckReverse = new System.Windows.Forms.CheckBox();
            this.tabControlSetup.SuspendLayout();
            this.tabPageInterval.SuspendLayout();
            this.groupBoxCaptureDelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            this.tabPageLabels.SuspendLayout();
            this.tabPageActiveWindowTitle.SuspendLayout();
            this.groupBoxActiveWindowTitle.SuspendLayout();
            this.tabPageApplicationFocus.SuspendLayout();
            this.groupBoxApplicationFocus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayBefore)).BeginInit();
            this.tabPageSecurity.SuspendLayout();
            this.groupBoxSecurity.SuspendLayout();
            this.tabPageKeyboardShortcuts.SuspendLayout();
            this.tabControlKeyboardShortcuts.SuspendLayout();
            this.tabPageScreenCapture.SuspendLayout();
            this.tabPageRegionSelect.SuspendLayout();
            this.tabPageCaptureNow.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSetup
            // 
            this.tabControlSetup.Controls.Add(this.tabPageInterval);
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
            // tabPageInterval
            // 
            this.tabPageInterval.Controls.Add(this.groupBoxCaptureDelay);
            this.tabPageInterval.Location = new System.Drawing.Point(4, 22);
            this.tabPageInterval.Name = "tabPageInterval";
            this.tabPageInterval.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInterval.Size = new System.Drawing.Size(677, 253);
            this.tabPageInterval.TabIndex = 0;
            this.tabPageInterval.Text = "Interval";
            this.tabPageInterval.UseVisualStyleBackColor = true;
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
            this.groupBoxCaptureDelay.Location = new System.Drawing.Point(84, 50);
            this.groupBoxCaptureDelay.Name = "groupBoxCaptureDelay";
            this.groupBoxCaptureDelay.Size = new System.Drawing.Size(205, 122);
            this.groupBoxCaptureDelay.TabIndex = 2;
            this.groupBoxCaptureDelay.TabStop = false;
            this.groupBoxCaptureDelay.Text = "Interval";
            // 
            // labelLimit
            // 
            this.labelLimit.AutoSize = true;
            this.labelLimit.Location = new System.Drawing.Point(171, 47);
            this.labelLimit.Name = "labelLimit";
            this.labelLimit.Size = new System.Drawing.Size(24, 13);
            this.labelLimit.TabIndex = 0;
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
            // 
            // tabPageLabels
            // 
            this.tabPageLabels.Controls.Add(this.checkBoxScreenshotLabel);
            this.tabPageLabels.Controls.Add(this.comboBoxScreenshotLabel);
            this.tabPageLabels.Location = new System.Drawing.Point(4, 22);
            this.tabPageLabels.Name = "tabPageLabels";
            this.tabPageLabels.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLabels.Size = new System.Drawing.Size(677, 253);
            this.tabPageLabels.TabIndex = 1;
            this.tabPageLabels.Text = "Labels";
            this.tabPageLabels.UseVisualStyleBackColor = true;
            // 
            // checkBoxScreenshotLabel
            // 
            this.checkBoxScreenshotLabel.AutoSize = true;
            this.checkBoxScreenshotLabel.Location = new System.Drawing.Point(236, 107);
            this.checkBoxScreenshotLabel.Name = "checkBoxScreenshotLabel";
            this.checkBoxScreenshotLabel.Size = new System.Drawing.Size(193, 17);
            this.checkBoxScreenshotLabel.TabIndex = 3;
            this.checkBoxScreenshotLabel.TabStop = false;
            this.checkBoxScreenshotLabel.Text = "Apply this label to each screenshot:";
            this.checkBoxScreenshotLabel.UseVisualStyleBackColor = true;
            // 
            // comboBoxScreenshotLabel
            // 
            this.comboBoxScreenshotLabel.FormattingEnabled = true;
            this.comboBoxScreenshotLabel.Location = new System.Drawing.Point(236, 125);
            this.comboBoxScreenshotLabel.MaxDropDownItems = 10;
            this.comboBoxScreenshotLabel.MaxLength = 500;
            this.comboBoxScreenshotLabel.Name = "comboBoxScreenshotLabel";
            this.comboBoxScreenshotLabel.Size = new System.Drawing.Size(205, 21);
            this.comboBoxScreenshotLabel.TabIndex = 4;
            this.comboBoxScreenshotLabel.TabStop = false;
            // 
            // tabPageActiveWindowTitle
            // 
            this.tabPageActiveWindowTitle.Controls.Add(this.groupBoxActiveWindowTitle);
            this.tabPageActiveWindowTitle.Location = new System.Drawing.Point(4, 22);
            this.tabPageActiveWindowTitle.Name = "tabPageActiveWindowTitle";
            this.tabPageActiveWindowTitle.Size = new System.Drawing.Size(677, 253);
            this.tabPageActiveWindowTitle.TabIndex = 2;
            this.tabPageActiveWindowTitle.Text = "Active Window Title";
            this.tabPageActiveWindowTitle.UseVisualStyleBackColor = true;
            // 
            // groupBoxActiveWindowTitle
            // 
            this.groupBoxActiveWindowTitle.Controls.Add(this.checkBoxActiveWindowTitleComparisonCheckReverse);
            this.groupBoxActiveWindowTitle.Controls.Add(this.buttonDynamicRegexValidator);
            this.groupBoxActiveWindowTitle.Controls.Add(this.radioButtonRegularExpressionMatch);
            this.groupBoxActiveWindowTitle.Controls.Add(this.radioButtonCaseSensitiveMatch);
            this.groupBoxActiveWindowTitle.Controls.Add(this.radioButtonCaseInsensitiveMatch);
            this.groupBoxActiveWindowTitle.Controls.Add(this.textBoxActiveWindowTitle);
            this.groupBoxActiveWindowTitle.Controls.Add(this.checkBoxActiveWindowTitleComparisonCheck);
            this.groupBoxActiveWindowTitle.Location = new System.Drawing.Point(236, 57);
            this.groupBoxActiveWindowTitle.Name = "groupBoxActiveWindowTitle";
            this.groupBoxActiveWindowTitle.Size = new System.Drawing.Size(205, 139);
            this.groupBoxActiveWindowTitle.TabIndex = 2;
            this.groupBoxActiveWindowTitle.TabStop = false;
            this.groupBoxActiveWindowTitle.Text = "Active Window Title";
            // 
            // buttonDynamicRegexValidator
            // 
            this.buttonDynamicRegexValidator.Image = global::AutoScreenCapture.Properties.Resources.lightbulb;
            this.buttonDynamicRegexValidator.Location = new System.Drawing.Point(176, 111);
            this.buttonDynamicRegexValidator.Name = "buttonDynamicRegexValidator";
            this.buttonDynamicRegexValidator.Size = new System.Drawing.Size(23, 23);
            this.buttonDynamicRegexValidator.TabIndex = 0;
            this.buttonDynamicRegexValidator.TabStop = false;
            this.buttonDynamicRegexValidator.UseVisualStyleBackColor = true;
            // 
            // radioButtonRegularExpressionMatch
            // 
            this.radioButtonRegularExpressionMatch.AutoSize = true;
            this.radioButtonRegularExpressionMatch.Location = new System.Drawing.Point(6, 114);
            this.radioButtonRegularExpressionMatch.Name = "radioButtonRegularExpressionMatch";
            this.radioButtonRegularExpressionMatch.Size = new System.Drawing.Size(149, 17);
            this.radioButtonRegularExpressionMatch.TabIndex = 0;
            this.radioButtonRegularExpressionMatch.Text = "Regular Expression Match";
            this.radioButtonRegularExpressionMatch.UseVisualStyleBackColor = true;
            // 
            // radioButtonCaseSensitiveMatch
            // 
            this.radioButtonCaseSensitiveMatch.AutoSize = true;
            this.radioButtonCaseSensitiveMatch.Location = new System.Drawing.Point(6, 68);
            this.radioButtonCaseSensitiveMatch.Name = "radioButtonCaseSensitiveMatch";
            this.radioButtonCaseSensitiveMatch.Size = new System.Drawing.Size(128, 17);
            this.radioButtonCaseSensitiveMatch.TabIndex = 0;
            this.radioButtonCaseSensitiveMatch.Text = "Case Sensitive Match";
            this.radioButtonCaseSensitiveMatch.UseVisualStyleBackColor = true;
            // 
            // radioButtonCaseInsensitiveMatch
            // 
            this.radioButtonCaseInsensitiveMatch.AutoSize = true;
            this.radioButtonCaseInsensitiveMatch.Location = new System.Drawing.Point(6, 91);
            this.radioButtonCaseInsensitiveMatch.Name = "radioButtonCaseInsensitiveMatch";
            this.radioButtonCaseInsensitiveMatch.Size = new System.Drawing.Size(135, 17);
            this.radioButtonCaseInsensitiveMatch.TabIndex = 0;
            this.radioButtonCaseInsensitiveMatch.Text = "Case Insensitive Match";
            this.radioButtonCaseInsensitiveMatch.UseVisualStyleBackColor = true;
            // 
            // textBoxActiveWindowTitle
            // 
            this.textBoxActiveWindowTitle.Location = new System.Drawing.Point(6, 42);
            this.textBoxActiveWindowTitle.MaxLength = 500;
            this.textBoxActiveWindowTitle.Name = "textBoxActiveWindowTitle";
            this.textBoxActiveWindowTitle.Size = new System.Drawing.Size(193, 20);
            this.textBoxActiveWindowTitle.TabIndex = 0;
            this.textBoxActiveWindowTitle.TabStop = false;
            // 
            // checkBoxActiveWindowTitleComparisonCheck
            // 
            this.checkBoxActiveWindowTitleComparisonCheck.AutoSize = true;
            this.checkBoxActiveWindowTitleComparisonCheck.Location = new System.Drawing.Point(6, 19);
            this.checkBoxActiveWindowTitleComparisonCheck.Name = "checkBoxActiveWindowTitleComparisonCheck";
            this.checkBoxActiveWindowTitleComparisonCheck.Size = new System.Drawing.Size(56, 17);
            this.checkBoxActiveWindowTitleComparisonCheck.TabIndex = 0;
            this.checkBoxActiveWindowTitleComparisonCheck.TabStop = false;
            this.checkBoxActiveWindowTitleComparisonCheck.Text = "Match";
            this.checkBoxActiveWindowTitleComparisonCheck.UseVisualStyleBackColor = true;
            // 
            // tabPageApplicationFocus
            // 
            this.tabPageApplicationFocus.Controls.Add(this.groupBoxApplicationFocus);
            this.tabPageApplicationFocus.Location = new System.Drawing.Point(4, 22);
            this.tabPageApplicationFocus.Name = "tabPageApplicationFocus";
            this.tabPageApplicationFocus.Size = new System.Drawing.Size(677, 253);
            this.tabPageApplicationFocus.TabIndex = 3;
            this.tabPageApplicationFocus.Text = "Application Focus";
            this.tabPageApplicationFocus.UseVisualStyleBackColor = true;
            // 
            // groupBoxApplicationFocus
            // 
            this.groupBoxApplicationFocus.Controls.Add(this.numericUpDownApplicationFocusDelayAfter);
            this.groupBoxApplicationFocus.Controls.Add(this.numericUpDownApplicationFocusDelayBefore);
            this.groupBoxApplicationFocus.Controls.Add(this.labelApplicationFocusDelayAfter);
            this.groupBoxApplicationFocus.Controls.Add(this.labelApplicationFocusDelayBefore);
            this.groupBoxApplicationFocus.Controls.Add(this.buttonApplicationFocusTest);
            this.groupBoxApplicationFocus.Controls.Add(this.buttonApplicationFocusRefresh);
            this.groupBoxApplicationFocus.Controls.Add(this.comboBoxProcessList);
            this.groupBoxApplicationFocus.Location = new System.Drawing.Point(236, 62);
            this.groupBoxApplicationFocus.Name = "groupBoxApplicationFocus";
            this.groupBoxApplicationFocus.Size = new System.Drawing.Size(205, 128);
            this.groupBoxApplicationFocus.TabIndex = 2;
            this.groupBoxApplicationFocus.TabStop = false;
            this.groupBoxApplicationFocus.Text = "Application Focus";
            // 
            // numericUpDownApplicationFocusDelayAfter
            // 
            this.numericUpDownApplicationFocusDelayAfter.Location = new System.Drawing.Point(147, 71);
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
            this.numericUpDownApplicationFocusDelayBefore.Location = new System.Drawing.Point(148, 47);
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
            this.labelApplicationFocusDelayAfter.Location = new System.Drawing.Point(6, 75);
            this.labelApplicationFocusDelayAfter.Name = "labelApplicationFocusDelayAfter";
            this.labelApplicationFocusDelayAfter.Size = new System.Drawing.Size(127, 13);
            this.labelApplicationFocusDelayAfter.TabIndex = 0;
            this.labelApplicationFocusDelayAfter.Text = "Delay After (milliseconds):";
            // 
            // labelApplicationFocusDelayBefore
            // 
            this.labelApplicationFocusDelayBefore.AutoSize = true;
            this.labelApplicationFocusDelayBefore.Location = new System.Drawing.Point(6, 49);
            this.labelApplicationFocusDelayBefore.Name = "labelApplicationFocusDelayBefore";
            this.labelApplicationFocusDelayBefore.Size = new System.Drawing.Size(136, 13);
            this.labelApplicationFocusDelayBefore.TabIndex = 0;
            this.labelApplicationFocusDelayBefore.Text = "Delay Before (milliseconds):";
            // 
            // buttonApplicationFocusTest
            // 
            this.buttonApplicationFocusTest.Location = new System.Drawing.Point(6, 98);
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
            this.buttonApplicationFocusRefresh.Location = new System.Drawing.Point(107, 98);
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
            // tabPageSecurity
            // 
            this.tabPageSecurity.Controls.Add(this.groupBoxSecurity);
            this.tabPageSecurity.Location = new System.Drawing.Point(4, 22);
            this.tabPageSecurity.Name = "tabPageSecurity";
            this.tabPageSecurity.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSecurity.Size = new System.Drawing.Size(677, 253);
            this.tabPageSecurity.TabIndex = 4;
            this.tabPageSecurity.Text = "Security";
            this.tabPageSecurity.UseVisualStyleBackColor = true;
            // 
            // groupBoxSecurity
            // 
            this.groupBoxSecurity.Controls.Add(this.labelPasswordDescription);
            this.groupBoxSecurity.Controls.Add(this.buttonSetPassphrase);
            this.groupBoxSecurity.Controls.Add(this.textBoxPassphrase);
            this.groupBoxSecurity.Location = new System.Drawing.Point(236, 71);
            this.groupBoxSecurity.Name = "groupBoxSecurity";
            this.groupBoxSecurity.Size = new System.Drawing.Size(205, 110);
            this.groupBoxSecurity.TabIndex = 2;
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
            this.buttonSetPassphrase.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetPassphrase.Image")));
            this.buttonSetPassphrase.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSetPassphrase.Location = new System.Drawing.Point(131, 80);
            this.buttonSetPassphrase.Name = "buttonSetPassphrase";
            this.buttonSetPassphrase.Size = new System.Drawing.Size(68, 23);
            this.buttonSetPassphrase.TabIndex = 0;
            this.buttonSetPassphrase.TabStop = false;
            this.buttonSetPassphrase.Text = "Lock";
            this.buttonSetPassphrase.UseVisualStyleBackColor = true;
            // 
            // textBoxPassphrase
            // 
            this.textBoxPassphrase.Location = new System.Drawing.Point(6, 82);
            this.textBoxPassphrase.MaxLength = 30;
            this.textBoxPassphrase.Name = "textBoxPassphrase";
            this.textBoxPassphrase.Size = new System.Drawing.Size(119, 20);
            this.textBoxPassphrase.TabIndex = 0;
            this.textBoxPassphrase.TabStop = false;
            // 
            // tabPageKeyboardShortcuts
            // 
            this.tabPageKeyboardShortcuts.Controls.Add(this.checkBoxUseKeyboardShortcuts);
            this.tabPageKeyboardShortcuts.Controls.Add(this.tabControlKeyboardShortcuts);
            this.tabPageKeyboardShortcuts.Location = new System.Drawing.Point(4, 22);
            this.tabPageKeyboardShortcuts.Name = "tabPageKeyboardShortcuts";
            this.tabPageKeyboardShortcuts.Size = new System.Drawing.Size(677, 253);
            this.tabPageKeyboardShortcuts.TabIndex = 5;
            this.tabPageKeyboardShortcuts.Text = "Keyboard Shortcuts";
            this.tabPageKeyboardShortcuts.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseKeyboardShortcuts
            // 
            this.checkBoxUseKeyboardShortcuts.AutoSize = true;
            this.checkBoxUseKeyboardShortcuts.Location = new System.Drawing.Point(532, 232);
            this.checkBoxUseKeyboardShortcuts.Name = "checkBoxUseKeyboardShortcuts";
            this.checkBoxUseKeyboardShortcuts.Size = new System.Drawing.Size(138, 17);
            this.checkBoxUseKeyboardShortcuts.TabIndex = 1;
            this.checkBoxUseKeyboardShortcuts.Text = "Use keyboard shortcuts";
            this.checkBoxUseKeyboardShortcuts.UseVisualStyleBackColor = true;
            // 
            // tabControlKeyboardShortcuts
            // 
            this.tabControlKeyboardShortcuts.Controls.Add(this.tabPageScreenCapture);
            this.tabControlKeyboardShortcuts.Controls.Add(this.tabPageRegionSelect);
            this.tabControlKeyboardShortcuts.Controls.Add(this.tabPageCaptureNow);
            this.tabControlKeyboardShortcuts.Location = new System.Drawing.Point(3, 3);
            this.tabControlKeyboardShortcuts.Name = "tabControlKeyboardShortcuts";
            this.tabControlKeyboardShortcuts.SelectedIndex = 0;
            this.tabControlKeyboardShortcuts.Size = new System.Drawing.Size(671, 223);
            this.tabControlKeyboardShortcuts.TabIndex = 0;
            this.tabControlKeyboardShortcuts.TabStop = false;
            // 
            // tabPageScreenCapture
            // 
            this.tabPageScreenCapture.Controls.Add(this.labelStopScreenCapture);
            this.tabPageScreenCapture.Controls.Add(this.labelStartScreenCapture);
            this.tabPageScreenCapture.Controls.Add(this.textBoxKeyboardShortcutStopScreenCaptureKey);
            this.tabPageScreenCapture.Controls.Add(this.comboBoxKeyboardShortcutStopScreenCaptureModifier2);
            this.tabPageScreenCapture.Controls.Add(this.comboBoxKeyboardShortcutStopScreenCaptureModifier1);
            this.tabPageScreenCapture.Controls.Add(this.textBoxKeyboardShortcutStartScreenCaptureKey);
            this.tabPageScreenCapture.Controls.Add(this.comboBoxKeyboardShortcutStartScreenCaptureModifier2);
            this.tabPageScreenCapture.Controls.Add(this.comboBoxKeyboardShortcutStartScreenCaptureModifier1);
            this.tabPageScreenCapture.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreenCapture.Name = "tabPageScreenCapture";
            this.tabPageScreenCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScreenCapture.Size = new System.Drawing.Size(663, 197);
            this.tabPageScreenCapture.TabIndex = 0;
            this.tabPageScreenCapture.Text = "Screen Capture";
            this.tabPageScreenCapture.UseVisualStyleBackColor = true;
            // 
            // labelStopScreenCapture
            // 
            this.labelStopScreenCapture.AutoSize = true;
            this.labelStopScreenCapture.Location = new System.Drawing.Point(42, 114);
            this.labelStopScreenCapture.Name = "labelStopScreenCapture";
            this.labelStopScreenCapture.Size = new System.Drawing.Size(106, 13);
            this.labelStopScreenCapture.TabIndex = 13;
            this.labelStopScreenCapture.Text = "Stop Screen Capture";
            // 
            // labelStartScreenCapture
            // 
            this.labelStartScreenCapture.AutoSize = true;
            this.labelStartScreenCapture.Location = new System.Drawing.Point(42, 87);
            this.labelStartScreenCapture.Name = "labelStartScreenCapture";
            this.labelStartScreenCapture.Size = new System.Drawing.Size(106, 13);
            this.labelStartScreenCapture.TabIndex = 9;
            this.labelStartScreenCapture.Text = "Start Screen Capture";
            // 
            // textBoxKeyboardShortcutStopScreenCaptureKey
            // 
            this.textBoxKeyboardShortcutStopScreenCaptureKey.Location = new System.Drawing.Point(400, 111);
            this.textBoxKeyboardShortcutStopScreenCaptureKey.MaxLength = 1;
            this.textBoxKeyboardShortcutStopScreenCaptureKey.Name = "textBoxKeyboardShortcutStopScreenCaptureKey";
            this.textBoxKeyboardShortcutStopScreenCaptureKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutStopScreenCaptureKey.TabIndex = 16;
            // 
            // comboBoxKeyboardShortcutStopScreenCaptureModifier2
            // 
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.Location = new System.Drawing.Point(307, 111);
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.Name = "comboBoxKeyboardShortcutStopScreenCaptureModifier2";
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier2.TabIndex = 15;
            // 
            // comboBoxKeyboardShortcutStopScreenCaptureModifier1
            // 
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.Location = new System.Drawing.Point(214, 111);
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.Name = "comboBoxKeyboardShortcutStopScreenCaptureModifier1";
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutStopScreenCaptureModifier1.TabIndex = 14;
            // 
            // textBoxKeyboardShortcutStartScreenCaptureKey
            // 
            this.textBoxKeyboardShortcutStartScreenCaptureKey.Location = new System.Drawing.Point(400, 84);
            this.textBoxKeyboardShortcutStartScreenCaptureKey.MaxLength = 1;
            this.textBoxKeyboardShortcutStartScreenCaptureKey.Name = "textBoxKeyboardShortcutStartScreenCaptureKey";
            this.textBoxKeyboardShortcutStartScreenCaptureKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutStartScreenCaptureKey.TabIndex = 12;
            // 
            // comboBoxKeyboardShortcutStartScreenCaptureModifier2
            // 
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.Location = new System.Drawing.Point(307, 84);
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.Name = "comboBoxKeyboardShortcutStartScreenCaptureModifier2";
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier2.TabIndex = 11;
            // 
            // comboBoxKeyboardShortcutStartScreenCaptureModifier1
            // 
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.Location = new System.Drawing.Point(214, 84);
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.Name = "comboBoxKeyboardShortcutStartScreenCaptureModifier1";
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutStartScreenCaptureModifier1.TabIndex = 10;
            // 
            // tabPageRegionSelect
            // 
            this.tabPageRegionSelect.Controls.Add(this.labelRegionSelectEdit);
            this.tabPageRegionSelect.Controls.Add(this.textBoxKeyboardShortcutRegionSelectEditKey);
            this.tabPageRegionSelect.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectEditModifier2);
            this.tabPageRegionSelect.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectEditModifier1);
            this.tabPageRegionSelect.Controls.Add(this.labelRegionSelectAutoSave);
            this.tabPageRegionSelect.Controls.Add(this.textBoxKeyboardShortcutRegionSelectAutoSaveKey);
            this.tabPageRegionSelect.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2);
            this.tabPageRegionSelect.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1);
            this.tabPageRegionSelect.Controls.Add(this.labelRegionSelectClipboard);
            this.tabPageRegionSelect.Controls.Add(this.textBoxKeyboardShortcutRegionSelectClipboardKey);
            this.tabPageRegionSelect.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2);
            this.tabPageRegionSelect.Controls.Add(this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1);
            this.tabPageRegionSelect.Location = new System.Drawing.Point(4, 22);
            this.tabPageRegionSelect.Name = "tabPageRegionSelect";
            this.tabPageRegionSelect.Size = new System.Drawing.Size(663, 197);
            this.tabPageRegionSelect.TabIndex = 2;
            this.tabPageRegionSelect.Text = "Region Select";
            this.tabPageRegionSelect.UseVisualStyleBackColor = true;
            // 
            // labelRegionSelectEdit
            // 
            this.labelRegionSelectEdit.AutoSize = true;
            this.labelRegionSelectEdit.Location = new System.Drawing.Point(132, 118);
            this.labelRegionSelectEdit.Name = "labelRegionSelectEdit";
            this.labelRegionSelectEdit.Size = new System.Drawing.Size(168, 13);
            this.labelRegionSelectEdit.TabIndex = 37;
            this.labelRegionSelectEdit.Text = "Region Select -> Auto Save / Edit";
            // 
            // textBoxKeyboardShortcutRegionSelectEditKey
            // 
            this.textBoxKeyboardShortcutRegionSelectEditKey.Location = new System.Drawing.Point(490, 115);
            this.textBoxKeyboardShortcutRegionSelectEditKey.MaxLength = 1;
            this.textBoxKeyboardShortcutRegionSelectEditKey.Name = "textBoxKeyboardShortcutRegionSelectEditKey";
            this.textBoxKeyboardShortcutRegionSelectEditKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutRegionSelectEditKey.TabIndex = 40;
            // 
            // comboBoxKeyboardShortcutRegionSelectEditModifier2
            // 
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.Location = new System.Drawing.Point(397, 115);
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.Name = "comboBoxKeyboardShortcutRegionSelectEditModifier2";
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectEditModifier2.TabIndex = 39;
            // 
            // comboBoxKeyboardShortcutRegionSelectEditModifier1
            // 
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.Location = new System.Drawing.Point(304, 115);
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.Name = "comboBoxKeyboardShortcutRegionSelectEditModifier1";
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectEditModifier1.TabIndex = 38;
            // 
            // labelRegionSelectAutoSave
            // 
            this.labelRegionSelectAutoSave.AutoSize = true;
            this.labelRegionSelectAutoSave.Location = new System.Drawing.Point(132, 91);
            this.labelRegionSelectAutoSave.Name = "labelRegionSelectAutoSave";
            this.labelRegionSelectAutoSave.Size = new System.Drawing.Size(139, 13);
            this.labelRegionSelectAutoSave.TabIndex = 33;
            this.labelRegionSelectAutoSave.Text = "Region Select -> Auto Save";
            // 
            // textBoxKeyboardShortcutRegionSelectAutoSaveKey
            // 
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey.Location = new System.Drawing.Point(490, 88);
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey.MaxLength = 1;
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey.Name = "textBoxKeyboardShortcutRegionSelectAutoSaveKey";
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutRegionSelectAutoSaveKey.TabIndex = 36;
            // 
            // comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2
            // 
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Location = new System.Drawing.Point(397, 88);
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Name = "comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2";
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.TabIndex = 35;
            // 
            // comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1
            // 
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Location = new System.Drawing.Point(304, 88);
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Name = "comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1";
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.TabIndex = 34;
            // 
            // labelRegionSelectClipboard
            // 
            this.labelRegionSelectClipboard.AutoSize = true;
            this.labelRegionSelectClipboard.Location = new System.Drawing.Point(132, 64);
            this.labelRegionSelectClipboard.Name = "labelRegionSelectClipboard";
            this.labelRegionSelectClipboard.Size = new System.Drawing.Size(133, 13);
            this.labelRegionSelectClipboard.TabIndex = 29;
            this.labelRegionSelectClipboard.Text = "Region Select -> Clipboard";
            // 
            // textBoxKeyboardShortcutRegionSelectClipboardKey
            // 
            this.textBoxKeyboardShortcutRegionSelectClipboardKey.Location = new System.Drawing.Point(490, 61);
            this.textBoxKeyboardShortcutRegionSelectClipboardKey.MaxLength = 1;
            this.textBoxKeyboardShortcutRegionSelectClipboardKey.Name = "textBoxKeyboardShortcutRegionSelectClipboardKey";
            this.textBoxKeyboardShortcutRegionSelectClipboardKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutRegionSelectClipboardKey.TabIndex = 32;
            // 
            // comboBoxKeyboardShortcutRegionSelectClipboardModifier2
            // 
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Location = new System.Drawing.Point(397, 61);
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Name = "comboBoxKeyboardShortcutRegionSelectClipboardModifier2";
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier2.TabIndex = 31;
            // 
            // comboBoxKeyboardShortcutRegionSelectClipboardModifier1
            // 
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Location = new System.Drawing.Point(304, 61);
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Name = "comboBoxKeyboardShortcutRegionSelectClipboardModifier1";
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutRegionSelectClipboardModifier1.TabIndex = 30;
            // 
            // tabPageCaptureNow
            // 
            this.tabPageCaptureNow.Controls.Add(this.labelCaptureNowEdit);
            this.tabPageCaptureNow.Controls.Add(this.labelCaptureNowArchive);
            this.tabPageCaptureNow.Controls.Add(this.textBoxKeyboardShortcutCaptureNowEditKey);
            this.tabPageCaptureNow.Controls.Add(this.comboBoxKeyboardShortcutCaptureNowEditModifier2);
            this.tabPageCaptureNow.Controls.Add(this.comboBoxKeyboardShortcutCaptureNowEditModifier1);
            this.tabPageCaptureNow.Controls.Add(this.textBoxKeyboardShortcutCaptureNowArchiveKey);
            this.tabPageCaptureNow.Controls.Add(this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2);
            this.tabPageCaptureNow.Controls.Add(this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1);
            this.tabPageCaptureNow.Location = new System.Drawing.Point(4, 22);
            this.tabPageCaptureNow.Name = "tabPageCaptureNow";
            this.tabPageCaptureNow.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCaptureNow.Size = new System.Drawing.Size(663, 197);
            this.tabPageCaptureNow.TabIndex = 1;
            this.tabPageCaptureNow.Text = "Capture Now";
            this.tabPageCaptureNow.UseVisualStyleBackColor = true;
            // 
            // labelCaptureNowEdit
            // 
            this.labelCaptureNowEdit.AutoSize = true;
            this.labelCaptureNowEdit.Location = new System.Drawing.Point(132, 104);
            this.labelCaptureNowEdit.Name = "labelCaptureNowEdit";
            this.labelCaptureNowEdit.Size = new System.Drawing.Size(98, 13);
            this.labelCaptureNowEdit.TabIndex = 21;
            this.labelCaptureNowEdit.Text = "Capture Now / Edit";
            // 
            // labelCaptureNowArchive
            // 
            this.labelCaptureNowArchive.AutoSize = true;
            this.labelCaptureNowArchive.Location = new System.Drawing.Point(132, 77);
            this.labelCaptureNowArchive.Name = "labelCaptureNowArchive";
            this.labelCaptureNowArchive.Size = new System.Drawing.Size(116, 13);
            this.labelCaptureNowArchive.TabIndex = 17;
            this.labelCaptureNowArchive.Text = "Capture Now / Archive";
            // 
            // textBoxKeyboardShortcutCaptureNowEditKey
            // 
            this.textBoxKeyboardShortcutCaptureNowEditKey.Location = new System.Drawing.Point(490, 101);
            this.textBoxKeyboardShortcutCaptureNowEditKey.MaxLength = 1;
            this.textBoxKeyboardShortcutCaptureNowEditKey.Name = "textBoxKeyboardShortcutCaptureNowEditKey";
            this.textBoxKeyboardShortcutCaptureNowEditKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutCaptureNowEditKey.TabIndex = 24;
            // 
            // comboBoxKeyboardShortcutCaptureNowEditModifier2
            // 
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.Location = new System.Drawing.Point(397, 101);
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.Name = "comboBoxKeyboardShortcutCaptureNowEditModifier2";
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutCaptureNowEditModifier2.TabIndex = 23;
            // 
            // comboBoxKeyboardShortcutCaptureNowEditModifier1
            // 
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.Location = new System.Drawing.Point(304, 101);
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.Name = "comboBoxKeyboardShortcutCaptureNowEditModifier1";
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutCaptureNowEditModifier1.TabIndex = 22;
            // 
            // textBoxKeyboardShortcutCaptureNowArchiveKey
            // 
            this.textBoxKeyboardShortcutCaptureNowArchiveKey.Location = new System.Drawing.Point(490, 74);
            this.textBoxKeyboardShortcutCaptureNowArchiveKey.MaxLength = 1;
            this.textBoxKeyboardShortcutCaptureNowArchiveKey.Name = "textBoxKeyboardShortcutCaptureNowArchiveKey";
            this.textBoxKeyboardShortcutCaptureNowArchiveKey.Size = new System.Drawing.Size(40, 20);
            this.textBoxKeyboardShortcutCaptureNowArchiveKey.TabIndex = 20;
            // 
            // comboBoxKeyboardShortcutCaptureNowArchiveModifier2
            // 
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Location = new System.Drawing.Point(397, 74);
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Name = "comboBoxKeyboardShortcutCaptureNowArchiveModifier2";
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier2.TabIndex = 19;
            // 
            // comboBoxKeyboardShortcutCaptureNowArchiveModifier1
            // 
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.FormattingEnabled = true;
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Location = new System.Drawing.Point(304, 74);
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Name = "comboBoxKeyboardShortcutCaptureNowArchiveModifier1";
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Size = new System.Drawing.Size(87, 21);
            this.comboBoxKeyboardShortcutCaptureNowArchiveModifier1.TabIndex = 18;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Location = new System.Drawing.Point(12, 311);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(99, 23);
            this.buttonOK.TabIndex = 31;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(117, 311);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 32;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
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
            // checkBoxActiveWindowTitleComparisonCheckReverse
            // 
            this.checkBoxActiveWindowTitleComparisonCheckReverse.AutoSize = true;
            this.checkBoxActiveWindowTitleComparisonCheckReverse.Location = new System.Drawing.Point(110, 19);
            this.checkBoxActiveWindowTitleComparisonCheckReverse.Name = "checkBoxActiveWindowTitleComparisonCheckReverse";
            this.checkBoxActiveWindowTitleComparisonCheckReverse.Size = new System.Drawing.Size(73, 17);
            this.checkBoxActiveWindowTitleComparisonCheckReverse.TabIndex = 1;
            this.checkBoxActiveWindowTitleComparisonCheckReverse.TabStop = false;
            this.checkBoxActiveWindowTitleComparisonCheckReverse.Text = "No Match";
            this.checkBoxActiveWindowTitleComparisonCheckReverse.UseVisualStyleBackColor = true;
            // 
            // FormSetup
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(689, 346);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tabControlSetup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup";
            this.Load += new System.EventHandler(this.FormSetup_Load);
            this.tabControlSetup.ResumeLayout(false);
            this.tabPageInterval.ResumeLayout(false);
            this.groupBoxCaptureDelay.ResumeLayout(false);
            this.groupBoxCaptureDelay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            this.tabPageLabels.ResumeLayout(false);
            this.tabPageLabels.PerformLayout();
            this.tabPageActiveWindowTitle.ResumeLayout(false);
            this.groupBoxActiveWindowTitle.ResumeLayout(false);
            this.groupBoxActiveWindowTitle.PerformLayout();
            this.tabPageApplicationFocus.ResumeLayout(false);
            this.groupBoxApplicationFocus.ResumeLayout(false);
            this.groupBoxApplicationFocus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayBefore)).EndInit();
            this.tabPageSecurity.ResumeLayout(false);
            this.groupBoxSecurity.ResumeLayout(false);
            this.groupBoxSecurity.PerformLayout();
            this.tabPageKeyboardShortcuts.ResumeLayout(false);
            this.tabPageKeyboardShortcuts.PerformLayout();
            this.tabControlKeyboardShortcuts.ResumeLayout(false);
            this.tabPageScreenCapture.ResumeLayout(false);
            this.tabPageScreenCapture.PerformLayout();
            this.tabPageRegionSelect.ResumeLayout(false);
            this.tabPageRegionSelect.PerformLayout();
            this.tabPageCaptureNow.ResumeLayout(false);
            this.tabPageCaptureNow.PerformLayout();
            this.ResumeLayout(false);

    }

        #endregion

        private System.Windows.Forms.TabControl tabControlSetup;
        private System.Windows.Forms.TabPage tabPageInterval;
        private System.Windows.Forms.TabPage tabPageLabels;
        private System.Windows.Forms.TabPage tabPageActiveWindowTitle;
        private System.Windows.Forms.TabPage tabPageApplicationFocus;
        private System.Windows.Forms.TabPage tabPageSecurity;
        private System.Windows.Forms.GroupBox groupBoxCaptureDelay;
        private System.Windows.Forms.Label labelLimit;
        private System.Windows.Forms.TabPage tabPageKeyboardShortcuts;
        private System.Windows.Forms.Label labelMillisecondsInterval;
        private System.Windows.Forms.Label labelSecondsInterval;
        private System.Windows.Forms.Label labelMinutesInterval;
        private System.Windows.Forms.Label labelHoursInterval;
        private System.Windows.Forms.GroupBox groupBoxActiveWindowTitle;
        private System.Windows.Forms.Button buttonDynamicRegexValidator;
        private System.Windows.Forms.GroupBox groupBoxApplicationFocus;
        private System.Windows.Forms.GroupBox groupBoxSecurity;
        private System.Windows.Forms.Label labelPasswordDescription;
        private System.Windows.Forms.Button buttonSetPassphrase;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.TabControl tabControlKeyboardShortcuts;
        private System.Windows.Forms.TabPage tabPageScreenCapture;
        private System.Windows.Forms.TabPage tabPageCaptureNow;
        private System.Windows.Forms.TabPage tabPageRegionSelect;
        private System.Windows.Forms.Label labelStopScreenCapture;
        private System.Windows.Forms.Label labelStartScreenCapture;
        private System.Windows.Forms.Label labelRegionSelectEdit;
        private System.Windows.Forms.Label labelRegionSelectAutoSave;
        private System.Windows.Forms.Label labelRegionSelectClipboard;
        private System.Windows.Forms.Label labelCaptureNowEdit;
        private System.Windows.Forms.Label labelCaptureNowArchive;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.CheckBox checkBoxInitialScreenshot;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownCaptureLimit;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.CheckBox checkBoxCaptureLimit;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownMillisecondsInterval;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.CheckBox checkBoxScreenshotLabel;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxScreenshotLabel;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.RadioButton radioButtonRegularExpressionMatch;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.RadioButton radioButtonCaseSensitiveMatch;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.RadioButton radioButtonCaseInsensitiveMatch;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.TextBox textBoxActiveWindowTitle;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.CheckBox checkBoxActiveWindowTitleComparisonCheck;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownApplicationFocusDelayAfter;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownApplicationFocusDelayBefore;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.Label labelApplicationFocusDelayAfter;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.Label labelApplicationFocusDelayBefore;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.Button buttonApplicationFocusTest;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.Button buttonApplicationFocusRefresh;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxProcessList;
 
        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.TextBox textBoxPassphrase;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutStopScreenCaptureKey;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutStopScreenCaptureModifier2;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutStopScreenCaptureModifier1;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutStartScreenCaptureKey;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutStartScreenCaptureModifier2;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutStartScreenCaptureModifier1;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutRegionSelectEditKey;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectEditModifier2;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectEditModifier1;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutRegionSelectAutoSaveKey;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutRegionSelectClipboardKey;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectClipboardModifier2;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutRegionSelectClipboardModifier1;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutCaptureNowEditKey;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutCaptureNowEditModifier2;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutCaptureNowEditModifier1;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.TextBox textBoxKeyboardShortcutCaptureNowArchiveKey;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutCaptureNowArchiveModifier2;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxKeyboardShortcutCaptureNowArchiveModifier1;
        private System.Windows.Forms.CheckBox checkBoxUseKeyboardShortcuts;
        public System.Windows.Forms.CheckBox checkBoxActiveWindowTitleComparisonCheckReverse;
    }
}