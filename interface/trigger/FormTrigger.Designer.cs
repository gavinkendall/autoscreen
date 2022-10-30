namespace AutoScreenCapture
{
    partial class FormTrigger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTrigger));
            this.labelEditorName = new System.Windows.Forms.Label();
            this.textBoxTriggerName = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxEnable = new System.Windows.Forms.CheckBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.listBoxCondition = new System.Windows.Forms.ListBox();
            this.listBoxModuleItemList = new System.Windows.Forms.ListBox();
            this.listBoxAction = new System.Windows.Forms.ListBox();
            this.labelActiveWindowTitle = new System.Windows.Forms.Label();
            this.textBoxActiveWindowTitle = new System.Windows.Forms.TextBox();
            this.labelDay = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelDays = new System.Windows.Forms.Label();
            this.numericUpDownDays = new System.Windows.Forms.NumericUpDown();
            this.comboBoxDay = new System.Windows.Forms.ComboBox();
            this.dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.textBoxConditionHelp = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelPage = new System.Windows.Forms.Label();
            this.groupBoxCondition = new System.Windows.Forms.GroupBox();
            this.comboBoxDuration = new System.Windows.Forms.ComboBox();
            this.numericUpDownDuration = new System.Windows.Forms.NumericUpDown();
            this.labelDuration = new System.Windows.Forms.Label();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.textBoxApplicationFocus = new System.Windows.Forms.TextBox();
            this.labelApplicationFocus = new System.Windows.Forms.Label();
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.labelLabel = new System.Windows.Forms.Label();
            this.groupBoxModules = new System.Windows.Forms.GroupBox();
            this.groupBoxInterval = new System.Windows.Forms.GroupBox();
            this.labelHours = new System.Windows.Forms.Label();
            this.labelMinutes = new System.Windows.Forms.Label();
            this.labelSeconds = new System.Windows.Forms.Label();
            this.groupBoxDeleteScreenshots = new System.Windows.Forms.GroupBox();
            this.labelCycleCount = new System.Windows.Forms.Label();
            this.numericUpDownCycleCount = new System.Windows.Forms.NumericUpDown();
            this.labelDeleteFolder = new System.Windows.Forms.Label();
            this.textBoxDeleteFolder = new System.Windows.Forms.TextBox();
            this.textBoxActionHelp = new System.Windows.Forms.TextBox();
            this.numericUpDownMillisecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.labelMilliseconds = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            this.groupBoxCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuration)).BeginInit();
            this.groupBoxAction.SuspendLayout();
            this.groupBoxModules.SuspendLayout();
            this.groupBoxInterval.SuspendLayout();
            this.groupBoxDeleteScreenshots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCycleCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // labelEditorName
            // 
            this.labelEditorName.AutoSize = true;
            this.labelEditorName.Location = new System.Drawing.Point(9, 35);
            this.labelEditorName.Name = "labelEditorName";
            this.labelEditorName.Size = new System.Drawing.Size(38, 13);
            this.labelEditorName.TabIndex = 1;
            this.labelEditorName.Text = "Name:";
            // 
            // textBoxTriggerName
            // 
            this.textBoxTriggerName.Location = new System.Drawing.Point(56, 32);
            this.textBoxTriggerName.MaxLength = 50;
            this.textBoxTriggerName.Name = "textBoxTriggerName";
            this.textBoxTriggerName.Size = new System.Drawing.Size(1111, 20);
            this.textBoxTriggerName.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(1148, 549);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxEnable.AutoSize = true;
            this.checkBoxEnable.Location = new System.Drawing.Point(1188, 34);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(59, 17);
            this.checkBoxEnable.TabIndex = 3;
            this.checkBoxEnable.TabStop = false;
            this.checkBoxEnable.Text = "Enable";
            this.checkBoxEnable.UseVisualStyleBackColor = true;
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
            this.labelHelp.Size = new System.Drawing.Size(1275, 17);
            this.labelHelp.TabIndex = 0;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxCondition
            // 
            this.listBoxCondition.FormattingEnabled = true;
            this.listBoxCondition.Location = new System.Drawing.Point(6, 107);
            this.listBoxCondition.Name = "listBoxCondition";
            this.listBoxCondition.Size = new System.Drawing.Size(419, 316);
            this.listBoxCondition.TabIndex = 5;
            this.listBoxCondition.TabStop = false;
            this.listBoxCondition.SelectedIndexChanged += new System.EventHandler(this.listBoxCondition_SelectedIndexChanged);
            // 
            // listBoxModuleItemList
            // 
            this.listBoxModuleItemList.FormattingEnabled = true;
            this.listBoxModuleItemList.Location = new System.Drawing.Point(6, 19);
            this.listBoxModuleItemList.Name = "listBoxModuleItemList";
            this.listBoxModuleItemList.Size = new System.Drawing.Size(320, 433);
            this.listBoxModuleItemList.TabIndex = 14;
            this.listBoxModuleItemList.TabStop = false;
            this.listBoxModuleItemList.UseTabStops = false;
            // 
            // listBoxAction
            // 
            this.listBoxAction.FormattingEnabled = true;
            this.listBoxAction.Location = new System.Drawing.Point(6, 107);
            this.listBoxAction.Name = "listBoxAction";
            this.listBoxAction.Size = new System.Drawing.Size(447, 147);
            this.listBoxAction.TabIndex = 22;
            this.listBoxAction.TabStop = false;
            this.listBoxAction.SelectedIndexChanged += new System.EventHandler(this.listBoxAction_SelectedIndexChanged);
            // 
            // labelActiveWindowTitle
            // 
            this.labelActiveWindowTitle.AutoSize = true;
            this.labelActiveWindowTitle.Enabled = false;
            this.labelActiveWindowTitle.Location = new System.Drawing.Point(6, 320);
            this.labelActiveWindowTitle.Name = "labelActiveWindowTitle";
            this.labelActiveWindowTitle.Size = new System.Drawing.Size(105, 13);
            this.labelActiveWindowTitle.TabIndex = 28;
            this.labelActiveWindowTitle.Text = "Active Window Title:";
            // 
            // textBoxActiveWindowTitle
            // 
            this.textBoxActiveWindowTitle.Enabled = false;
            this.textBoxActiveWindowTitle.Location = new System.Drawing.Point(117, 317);
            this.textBoxActiveWindowTitle.Name = "textBoxActiveWindowTitle";
            this.textBoxActiveWindowTitle.Size = new System.Drawing.Size(336, 20);
            this.textBoxActiveWindowTitle.TabIndex = 27;
            this.textBoxActiveWindowTitle.TabStop = false;
            // 
            // labelDay
            // 
            this.labelDay.AutoSize = true;
            this.labelDay.Enabled = false;
            this.labelDay.Location = new System.Drawing.Point(6, 459);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(29, 13);
            this.labelDay.TabIndex = 26;
            this.labelDay.Text = "Day:";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Enabled = false;
            this.labelTime.Location = new System.Drawing.Point(241, 459);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(33, 13);
            this.labelTime.TabIndex = 25;
            this.labelTime.Text = "Time:";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Enabled = false;
            this.labelDate.Location = new System.Drawing.Point(6, 431);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(33, 13);
            this.labelDate.TabIndex = 24;
            this.labelDate.Text = "Date:";
            // 
            // labelDays
            // 
            this.labelDays.AutoSize = true;
            this.labelDays.Location = new System.Drawing.Point(167, 27);
            this.labelDays.Name = "labelDays";
            this.labelDays.Size = new System.Drawing.Size(34, 13);
            this.labelDays.TabIndex = 23;
            this.labelDays.Text = "Days:";
            // 
            // numericUpDownDays
            // 
            this.numericUpDownDays.Location = new System.Drawing.Point(207, 25);
            this.numericUpDownDays.Maximum = new decimal(new int[] {
            -469762049,
            -590869294,
            5421010,
            0});
            this.numericUpDownDays.Name = "numericUpDownDays";
            this.numericUpDownDays.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownDays.TabIndex = 14;
            this.numericUpDownDays.TabStop = false;
            // 
            // comboBoxDay
            // 
            this.comboBoxDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDay.Enabled = false;
            this.comboBoxDay.FormattingEnabled = true;
            this.comboBoxDay.Items.AddRange(new object[] {
            "Weekday",
            "Weekend",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"});
            this.comboBoxDay.Location = new System.Drawing.Point(45, 455);
            this.comboBoxDay.Name = "comboBoxDay";
            this.comboBoxDay.Size = new System.Drawing.Size(183, 21);
            this.comboBoxDay.TabIndex = 14;
            this.comboBoxDay.TabStop = false;
            // 
            // dateTimePickerTime
            // 
            this.dateTimePickerTime.CustomFormat = "HH:mm";
            this.dateTimePickerTime.Enabled = false;
            this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTime.Location = new System.Drawing.Point(297, 455);
            this.dateTimePickerTime.Name = "dateTimePickerTime";
            this.dateTimePickerTime.ShowUpDown = true;
            this.dateTimePickerTime.Size = new System.Drawing.Size(55, 20);
            this.dateTimePickerTime.TabIndex = 14;
            this.dateTimePickerTime.TabStop = false;
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Enabled = false;
            this.dateTimePickerDate.Location = new System.Drawing.Point(45, 429);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(183, 20);
            this.dateTimePickerDate.TabIndex = 14;
            this.dateTimePickerDate.TabStop = false;
            // 
            // numericUpDownHoursInterval
            // 
            this.numericUpDownHoursInterval.Enabled = false;
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(50, 19);
            this.numericUpDownHoursInterval.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownHoursInterval.Name = "numericUpDownHoursInterval";
            this.numericUpDownHoursInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownHoursInterval.TabIndex = 21;
            this.numericUpDownHoursInterval.TabStop = false;
            // 
            // numericUpDownMinutesInterval
            // 
            this.numericUpDownMinutesInterval.Enabled = false;
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(156, 19);
            this.numericUpDownMinutesInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownMinutesInterval.Name = "numericUpDownMinutesInterval";
            this.numericUpDownMinutesInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMinutesInterval.TabIndex = 20;
            this.numericUpDownMinutesInterval.TabStop = false;
            // 
            // numericUpDownSecondsInterval
            // 
            this.numericUpDownSecondsInterval.Enabled = false;
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(267, 19);
            this.numericUpDownSecondsInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSecondsInterval.Name = "numericUpDownSecondsInterval";
            this.numericUpDownSecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSecondsInterval.TabIndex = 19;
            this.numericUpDownSecondsInterval.TabStop = false;
            // 
            // textBoxConditionHelp
            // 
            this.textBoxConditionHelp.BackColor = System.Drawing.Color.LightYellow;
            this.textBoxConditionHelp.Location = new System.Drawing.Point(6, 19);
            this.textBoxConditionHelp.Multiline = true;
            this.textBoxConditionHelp.Name = "textBoxConditionHelp";
            this.textBoxConditionHelp.ReadOnly = true;
            this.textBoxConditionHelp.Size = new System.Drawing.Size(419, 82);
            this.textBoxConditionHelp.TabIndex = 11;
            this.textBoxConditionHelp.TabStop = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(1043, 549);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(99, 23);
            this.buttonOK.TabIndex = 14;
            this.buttonOK.TabStop = false;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Location = new System.Drawing.Point(9, 433);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(0, 13);
            this.labelPage.TabIndex = 15;
            // 
            // groupBoxCondition
            // 
            this.groupBoxCondition.Controls.Add(this.comboBoxDuration);
            this.groupBoxCondition.Controls.Add(this.numericUpDownDuration);
            this.groupBoxCondition.Controls.Add(this.labelDuration);
            this.groupBoxCondition.Controls.Add(this.labelDay);
            this.groupBoxCondition.Controls.Add(this.listBoxCondition);
            this.groupBoxCondition.Controls.Add(this.textBoxConditionHelp);
            this.groupBoxCondition.Controls.Add(this.labelDate);
            this.groupBoxCondition.Controls.Add(this.dateTimePickerDate);
            this.groupBoxCondition.Controls.Add(this.dateTimePickerTime);
            this.groupBoxCondition.Controls.Add(this.comboBoxDay);
            this.groupBoxCondition.Controls.Add(this.labelTime);
            this.groupBoxCondition.Location = new System.Drawing.Point(12, 58);
            this.groupBoxCondition.Name = "groupBoxCondition";
            this.groupBoxCondition.Size = new System.Drawing.Size(431, 485);
            this.groupBoxCondition.TabIndex = 31;
            this.groupBoxCondition.TabStop = false;
            this.groupBoxCondition.Text = "Condition";
            // 
            // comboBoxDuration
            // 
            this.comboBoxDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDuration.FormattingEnabled = true;
            this.comboBoxDuration.Items.AddRange(new object[] {
            "Seconds",
            "Minutes",
            "Hours"});
            this.comboBoxDuration.Location = new System.Drawing.Point(358, 428);
            this.comboBoxDuration.Name = "comboBoxDuration";
            this.comboBoxDuration.Size = new System.Drawing.Size(67, 21);
            this.comboBoxDuration.TabIndex = 29;
            this.comboBoxDuration.TabStop = false;
            // 
            // numericUpDownDuration
            // 
            this.numericUpDownDuration.Location = new System.Drawing.Point(297, 429);
            this.numericUpDownDuration.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownDuration.Name = "numericUpDownDuration";
            this.numericUpDownDuration.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownDuration.TabIndex = 28;
            this.numericUpDownDuration.TabStop = false;
            // 
            // labelDuration
            // 
            this.labelDuration.AutoSize = true;
            this.labelDuration.Location = new System.Drawing.Point(241, 431);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(50, 13);
            this.labelDuration.TabIndex = 27;
            this.labelDuration.Text = "Duration:";
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.textBoxApplicationFocus);
            this.groupBoxAction.Controls.Add(this.labelApplicationFocus);
            this.groupBoxAction.Controls.Add(this.textBoxLabel);
            this.groupBoxAction.Controls.Add(this.labelLabel);
            this.groupBoxAction.Controls.Add(this.groupBoxModules);
            this.groupBoxAction.Controls.Add(this.groupBoxInterval);
            this.groupBoxAction.Controls.Add(this.groupBoxDeleteScreenshots);
            this.groupBoxAction.Controls.Add(this.textBoxActionHelp);
            this.groupBoxAction.Controls.Add(this.listBoxAction);
            this.groupBoxAction.Controls.Add(this.labelActiveWindowTitle);
            this.groupBoxAction.Controls.Add(this.textBoxActiveWindowTitle);
            this.groupBoxAction.Location = new System.Drawing.Point(449, 58);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(798, 485);
            this.groupBoxAction.TabIndex = 32;
            this.groupBoxAction.TabStop = false;
            this.groupBoxAction.Text = "Action";
            // 
            // textBoxApplicationFocus
            // 
            this.textBoxApplicationFocus.Enabled = false;
            this.textBoxApplicationFocus.Location = new System.Drawing.Point(117, 291);
            this.textBoxApplicationFocus.Name = "textBoxApplicationFocus";
            this.textBoxApplicationFocus.Size = new System.Drawing.Size(336, 20);
            this.textBoxApplicationFocus.TabIndex = 40;
            this.textBoxApplicationFocus.TabStop = false;
            // 
            // labelApplicationFocus
            // 
            this.labelApplicationFocus.AutoSize = true;
            this.labelApplicationFocus.Enabled = false;
            this.labelApplicationFocus.Location = new System.Drawing.Point(6, 294);
            this.labelApplicationFocus.Name = "labelApplicationFocus";
            this.labelApplicationFocus.Size = new System.Drawing.Size(94, 13);
            this.labelApplicationFocus.TabIndex = 39;
            this.labelApplicationFocus.Text = "Application Focus:";
            // 
            // textBoxLabel
            // 
            this.textBoxLabel.Enabled = false;
            this.textBoxLabel.Location = new System.Drawing.Point(117, 265);
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.Size = new System.Drawing.Size(336, 20);
            this.textBoxLabel.TabIndex = 38;
            this.textBoxLabel.TabStop = false;
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Enabled = false;
            this.labelLabel.Location = new System.Drawing.Point(6, 268);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(36, 13);
            this.labelLabel.TabIndex = 37;
            this.labelLabel.Text = "Label:";
            // 
            // groupBoxModules
            // 
            this.groupBoxModules.Controls.Add(this.listBoxModuleItemList);
            this.groupBoxModules.Location = new System.Drawing.Point(459, 19);
            this.groupBoxModules.Name = "groupBoxModules";
            this.groupBoxModules.Size = new System.Drawing.Size(333, 456);
            this.groupBoxModules.TabIndex = 33;
            this.groupBoxModules.TabStop = false;
            this.groupBoxModules.Text = "Screens / Regions / Editors / Schedules / Macro Tags / Triggers";
            // 
            // groupBoxInterval
            // 
            this.groupBoxInterval.Controls.Add(this.labelMilliseconds);
            this.groupBoxInterval.Controls.Add(this.numericUpDownMillisecondsInterval);
            this.groupBoxInterval.Controls.Add(this.labelHours);
            this.groupBoxInterval.Controls.Add(this.labelMinutes);
            this.groupBoxInterval.Controls.Add(this.labelSeconds);
            this.groupBoxInterval.Controls.Add(this.numericUpDownSecondsInterval);
            this.groupBoxInterval.Controls.Add(this.numericUpDownMinutesInterval);
            this.groupBoxInterval.Controls.Add(this.numericUpDownHoursInterval);
            this.groupBoxInterval.Location = new System.Drawing.Point(6, 347);
            this.groupBoxInterval.Name = "groupBoxInterval";
            this.groupBoxInterval.Size = new System.Drawing.Size(447, 45);
            this.groupBoxInterval.TabIndex = 36;
            this.groupBoxInterval.TabStop = false;
            this.groupBoxInterval.Text = "Set Screen Capture Interval";
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(6, 21);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(38, 13);
            this.labelHours.TabIndex = 25;
            this.labelHours.Text = "Hours:";
            // 
            // labelMinutes
            // 
            this.labelMinutes.AutoSize = true;
            this.labelMinutes.Location = new System.Drawing.Point(103, 21);
            this.labelMinutes.Name = "labelMinutes";
            this.labelMinutes.Size = new System.Drawing.Size(47, 13);
            this.labelMinutes.TabIndex = 24;
            this.labelMinutes.Text = "Minutes:";
            // 
            // labelSeconds
            // 
            this.labelSeconds.AutoSize = true;
            this.labelSeconds.Location = new System.Drawing.Point(209, 21);
            this.labelSeconds.Name = "labelSeconds";
            this.labelSeconds.Size = new System.Drawing.Size(52, 13);
            this.labelSeconds.TabIndex = 23;
            this.labelSeconds.Text = "Seconds:";
            // 
            // groupBoxDeleteScreenshots
            // 
            this.groupBoxDeleteScreenshots.Controls.Add(this.labelCycleCount);
            this.groupBoxDeleteScreenshots.Controls.Add(this.numericUpDownCycleCount);
            this.groupBoxDeleteScreenshots.Controls.Add(this.labelDeleteFolder);
            this.groupBoxDeleteScreenshots.Controls.Add(this.textBoxDeleteFolder);
            this.groupBoxDeleteScreenshots.Controls.Add(this.numericUpDownDays);
            this.groupBoxDeleteScreenshots.Controls.Add(this.labelDays);
            this.groupBoxDeleteScreenshots.Enabled = false;
            this.groupBoxDeleteScreenshots.Location = new System.Drawing.Point(6, 398);
            this.groupBoxDeleteScreenshots.Name = "groupBoxDeleteScreenshots";
            this.groupBoxDeleteScreenshots.Size = new System.Drawing.Size(447, 77);
            this.groupBoxDeleteScreenshots.TabIndex = 35;
            this.groupBoxDeleteScreenshots.TabStop = false;
            this.groupBoxDeleteScreenshots.Text = "Delete Screenshots";
            // 
            // labelCycleCount
            // 
            this.labelCycleCount.AutoSize = true;
            this.labelCycleCount.Location = new System.Drawing.Point(6, 27);
            this.labelCycleCount.Name = "labelCycleCount";
            this.labelCycleCount.Size = new System.Drawing.Size(67, 13);
            this.labelCycleCount.TabIndex = 39;
            this.labelCycleCount.Text = "Cycle Count:";
            // 
            // numericUpDownCycleCount
            // 
            this.numericUpDownCycleCount.Location = new System.Drawing.Point(85, 25);
            this.numericUpDownCycleCount.Maximum = new decimal(new int[] {
            -469762049,
            -590869294,
            5421010,
            0});
            this.numericUpDownCycleCount.Name = "numericUpDownCycleCount";
            this.numericUpDownCycleCount.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownCycleCount.TabIndex = 38;
            this.numericUpDownCycleCount.TabStop = false;
            // 
            // labelDeleteFolder
            // 
            this.labelDeleteFolder.AutoSize = true;
            this.labelDeleteFolder.Location = new System.Drawing.Point(6, 54);
            this.labelDeleteFolder.Name = "labelDeleteFolder";
            this.labelDeleteFolder.Size = new System.Drawing.Size(73, 13);
            this.labelDeleteFolder.TabIndex = 37;
            this.labelDeleteFolder.Text = "Delete Folder:";
            // 
            // textBoxDeleteFolder
            // 
            this.textBoxDeleteFolder.Location = new System.Drawing.Point(85, 51);
            this.textBoxDeleteFolder.Name = "textBoxDeleteFolder";
            this.textBoxDeleteFolder.Size = new System.Drawing.Size(356, 20);
            this.textBoxDeleteFolder.TabIndex = 37;
            this.textBoxDeleteFolder.TabStop = false;
            // 
            // textBoxActionHelp
            // 
            this.textBoxActionHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxActionHelp.BackColor = System.Drawing.Color.LightYellow;
            this.textBoxActionHelp.Location = new System.Drawing.Point(6, 19);
            this.textBoxActionHelp.Multiline = true;
            this.textBoxActionHelp.Name = "textBoxActionHelp";
            this.textBoxActionHelp.ReadOnly = true;
            this.textBoxActionHelp.Size = new System.Drawing.Size(447, 82);
            this.textBoxActionHelp.TabIndex = 33;
            this.textBoxActionHelp.TabStop = false;
            // 
            // numericUpDownMillisecondsInterval
            // 
            this.numericUpDownMillisecondsInterval.Enabled = false;
            this.numericUpDownMillisecondsInterval.Location = new System.Drawing.Point(399, 19);
            this.numericUpDownMillisecondsInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownMillisecondsInterval.Name = "numericUpDownMillisecondsInterval";
            this.numericUpDownMillisecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMillisecondsInterval.TabIndex = 26;
            this.numericUpDownMillisecondsInterval.TabStop = false;
            // 
            // labelMilliseconds
            // 
            this.labelMilliseconds.AutoSize = true;
            this.labelMilliseconds.Location = new System.Drawing.Point(326, 21);
            this.labelMilliseconds.Name = "labelMilliseconds";
            this.labelMilliseconds.Size = new System.Drawing.Size(67, 13);
            this.labelMilliseconds.TabIndex = 27;
            this.labelMilliseconds.Text = "Milliseconds:";
            // 
            // FormTrigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1259, 580);
            this.Controls.Add(this.groupBoxAction);
            this.Controls.Add(this.labelPage);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.checkBoxEnable);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelEditorName);
            this.Controls.Add(this.textBoxTriggerName);
            this.Controls.Add(this.groupBoxCondition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(772, 493);
            this.Name = "FormTrigger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormTrigger_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            this.groupBoxCondition.ResumeLayout(false);
            this.groupBoxCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuration)).EndInit();
            this.groupBoxAction.ResumeLayout(false);
            this.groupBoxAction.PerformLayout();
            this.groupBoxModules.ResumeLayout(false);
            this.groupBoxInterval.ResumeLayout(false);
            this.groupBoxInterval.PerformLayout();
            this.groupBoxDeleteScreenshots.ResumeLayout(false);
            this.groupBoxDeleteScreenshots.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCycleCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEditorName;
        private System.Windows.Forms.TextBox textBoxTriggerName;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxEnable;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.ListBox listBoxCondition;
        private System.Windows.Forms.TextBox textBoxConditionHelp;
        private System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMillisecondsInterval;
        private System.Windows.Forms.ComboBox comboBoxDay;
        private System.Windows.Forms.DateTimePicker dateTimePickerTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.NumericUpDown numericUpDownDays;
        private System.Windows.Forms.ListBox listBoxModuleItemList;
        private System.Windows.Forms.ListBox listBoxAction;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelDays;
        private System.Windows.Forms.Label labelDay;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.TextBox textBoxActiveWindowTitle;
        private System.Windows.Forms.Label labelActiveWindowTitle;
        private System.Windows.Forms.GroupBox groupBoxCondition;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.TextBox textBoxActionHelp;
        private System.Windows.Forms.GroupBox groupBoxDeleteScreenshots;
        private System.Windows.Forms.GroupBox groupBoxInterval;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.Label labelMinutes;
        private System.Windows.Forms.Label labelSeconds;
        private System.Windows.Forms.Label labelMilliseconds;
        private System.Windows.Forms.Label labelCycleCount;
        private System.Windows.Forms.NumericUpDown numericUpDownCycleCount;
        private System.Windows.Forms.Label labelDeleteFolder;
        private System.Windows.Forms.TextBox textBoxDeleteFolder;
        private System.Windows.Forms.GroupBox groupBoxModules;
        private System.Windows.Forms.TextBox textBoxApplicationFocus;
        private System.Windows.Forms.Label labelApplicationFocus;
        private System.Windows.Forms.TextBox textBoxLabel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.ComboBox comboBoxDuration;
        private System.Windows.Forms.NumericUpDown numericUpDownDuration;
        private System.Windows.Forms.Label labelDuration;
    }
}