namespace AutoScreenCapture
{
    partial class FormSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSchedule));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelScheduleName = new System.Windows.Forms.Label();
            this.checkBoxEnable = new System.Windows.Forms.CheckBox();
            this.dateTimePickerStartAt = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStopAt = new System.Windows.Forms.DateTimePicker();
            this.radioButtonOneTime = new System.Windows.Forms.RadioButton();
            this.radioButtonPeriod = new System.Windows.Forms.RadioButton();
            this.dateTimePickerCaptureAt = new System.Windows.Forms.DateTimePicker();
            this.checkBoxMonday = new System.Windows.Forms.CheckBox();
            this.groupBoxDays = new System.Windows.Forms.GroupBox();
            this.checkBoxWeekend = new System.Windows.Forms.CheckBox();
            this.checkBoxWorkWeek = new System.Windows.Forms.CheckBox();
            this.checkBoxSunday = new System.Windows.Forms.CheckBox();
            this.checkBoxSaturday = new System.Windows.Forms.CheckBox();
            this.checkBoxFriday = new System.Windows.Forms.CheckBox();
            this.checkBoxThursday = new System.Windows.Forms.CheckBox();
            this.checkBoxWednesday = new System.Windows.Forms.CheckBox();
            this.checkBoxTuesday = new System.Windows.Forms.CheckBox();
            this.labelTakeScreenshotsOnce = new System.Windows.Forms.Label();
            this.labelTakeScreenshotsPeriod = new System.Windows.Forms.Label();
            this.labelAnd = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.labelNotes = new System.Windows.Forms.Label();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBoxInterval = new System.Windows.Forms.GroupBox();
            this.labelMinutes = new System.Windows.Forms.Label();
            this.labelHours = new System.Windows.Forms.Label();
            this.comboBoxScope = new System.Windows.Forms.ComboBox();
            this.labelScope = new System.Windows.Forms.Label();
            this.comboBoxLogic = new System.Windows.Forms.ComboBox();
            this.labelLogic = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxDays.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            this.groupBoxInterval.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(583, 399);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 30;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Location = new System.Drawing.Point(478, 399);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(99, 23);
            this.buttonOK.TabIndex = 29;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(56, 32);
            this.textBoxName.MaxLength = 50;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(431, 20);
            this.textBoxName.TabIndex = 2;
            // 
            // labelScheduleName
            // 
            this.labelScheduleName.AutoSize = true;
            this.labelScheduleName.Location = new System.Drawing.Point(9, 35);
            this.labelScheduleName.Name = "labelScheduleName";
            this.labelScheduleName.Size = new System.Drawing.Size(38, 13);
            this.labelScheduleName.TabIndex = 1;
            this.labelScheduleName.Text = "Name:";
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxEnable.AutoSize = true;
            this.checkBoxEnable.Location = new System.Drawing.Point(623, 34);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(59, 17);
            this.checkBoxEnable.TabIndex = 3;
            this.checkBoxEnable.Text = "Enable";
            this.checkBoxEnable.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerStartAt
            // 
            this.dateTimePickerStartAt.CustomFormat = "HH:mm:ss";
            this.dateTimePickerStartAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartAt.Location = new System.Drawing.Point(243, 166);
            this.dateTimePickerStartAt.Name = "dateTimePickerStartAt";
            this.dateTimePickerStartAt.ShowUpDown = true;
            this.dateTimePickerStartAt.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerStartAt.TabIndex = 9;
            // 
            // dateTimePickerStopAt
            // 
            this.dateTimePickerStopAt.CustomFormat = "HH:mm:ss";
            this.dateTimePickerStopAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStopAt.Location = new System.Drawing.Point(403, 166);
            this.dateTimePickerStopAt.Name = "dateTimePickerStopAt";
            this.dateTimePickerStopAt.ShowUpDown = true;
            this.dateTimePickerStopAt.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerStopAt.TabIndex = 11;
            // 
            // radioButtonOneTime
            // 
            this.radioButtonOneTime.AutoSize = true;
            this.radioButtonOneTime.Checked = true;
            this.radioButtonOneTime.Location = new System.Drawing.Point(12, 131);
            this.radioButtonOneTime.Name = "radioButtonOneTime";
            this.radioButtonOneTime.Size = new System.Drawing.Size(71, 17);
            this.radioButtonOneTime.TabIndex = 4;
            this.radioButtonOneTime.TabStop = true;
            this.radioButtonOneTime.Text = "One Time";
            this.radioButtonOneTime.UseVisualStyleBackColor = true;
            this.radioButtonOneTime.CheckedChanged += new System.EventHandler(this.radioButtonOneTime_CheckedChanged);
            // 
            // radioButtonPeriod
            // 
            this.radioButtonPeriod.AutoSize = true;
            this.radioButtonPeriod.Location = new System.Drawing.Point(12, 167);
            this.radioButtonPeriod.Name = "radioButtonPeriod";
            this.radioButtonPeriod.Size = new System.Drawing.Size(55, 17);
            this.radioButtonPeriod.TabIndex = 7;
            this.radioButtonPeriod.TabStop = true;
            this.radioButtonPeriod.Text = "Period";
            this.radioButtonPeriod.UseVisualStyleBackColor = true;
            this.radioButtonPeriod.CheckedChanged += new System.EventHandler(this.radioButtonPeriod_CheckedChanged);
            // 
            // dateTimePickerCaptureAt
            // 
            this.dateTimePickerCaptureAt.CustomFormat = "HH:mm:ss";
            this.dateTimePickerCaptureAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerCaptureAt.Location = new System.Drawing.Point(243, 130);
            this.dateTimePickerCaptureAt.Name = "dateTimePickerCaptureAt";
            this.dateTimePickerCaptureAt.ShowUpDown = true;
            this.dateTimePickerCaptureAt.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerCaptureAt.TabIndex = 6;
            // 
            // checkBoxMonday
            // 
            this.checkBoxMonday.AutoSize = true;
            this.checkBoxMonday.Location = new System.Drawing.Point(100, 19);
            this.checkBoxMonday.Name = "checkBoxMonday";
            this.checkBoxMonday.Size = new System.Drawing.Size(64, 17);
            this.checkBoxMonday.TabIndex = 19;
            this.checkBoxMonday.Text = "Monday";
            this.checkBoxMonday.UseVisualStyleBackColor = true;
            // 
            // groupBoxDays
            // 
            this.groupBoxDays.Controls.Add(this.checkBoxWeekend);
            this.groupBoxDays.Controls.Add(this.checkBoxWorkWeek);
            this.groupBoxDays.Controls.Add(this.checkBoxSunday);
            this.groupBoxDays.Controls.Add(this.checkBoxSaturday);
            this.groupBoxDays.Controls.Add(this.checkBoxFriday);
            this.groupBoxDays.Controls.Add(this.checkBoxThursday);
            this.groupBoxDays.Controls.Add(this.checkBoxWednesday);
            this.groupBoxDays.Controls.Add(this.checkBoxTuesday);
            this.groupBoxDays.Controls.Add(this.checkBoxMonday);
            this.groupBoxDays.Location = new System.Drawing.Point(12, 58);
            this.groupBoxDays.Name = "groupBoxDays";
            this.groupBoxDays.Size = new System.Drawing.Size(475, 67);
            this.groupBoxDays.TabIndex = 17;
            this.groupBoxDays.TabStop = false;
            this.groupBoxDays.Text = "Days";
            // 
            // checkBoxWeekend
            // 
            this.checkBoxWeekend.AutoSize = true;
            this.checkBoxWeekend.Location = new System.Drawing.Point(10, 42);
            this.checkBoxWeekend.Name = "checkBoxWeekend";
            this.checkBoxWeekend.Size = new System.Drawing.Size(73, 17);
            this.checkBoxWeekend.TabIndex = 24;
            this.checkBoxWeekend.Text = "Weekend";
            this.checkBoxWeekend.UseVisualStyleBackColor = true;
            this.checkBoxWeekend.CheckedChanged += new System.EventHandler(this.checkBoxWeekend_CheckedChanged);
            // 
            // checkBoxWorkWeek
            // 
            this.checkBoxWorkWeek.AutoSize = true;
            this.checkBoxWorkWeek.Location = new System.Drawing.Point(10, 19);
            this.checkBoxWorkWeek.Name = "checkBoxWorkWeek";
            this.checkBoxWorkWeek.Size = new System.Drawing.Size(84, 17);
            this.checkBoxWorkWeek.TabIndex = 18;
            this.checkBoxWorkWeek.Text = "Work Week";
            this.checkBoxWorkWeek.UseVisualStyleBackColor = true;
            this.checkBoxWorkWeek.CheckedChanged += new System.EventHandler(this.checkBoxWorkWeek_CheckedChanged);
            // 
            // checkBoxSunday
            // 
            this.checkBoxSunday.AutoSize = true;
            this.checkBoxSunday.Location = new System.Drawing.Point(170, 42);
            this.checkBoxSunday.Name = "checkBoxSunday";
            this.checkBoxSunday.Size = new System.Drawing.Size(62, 17);
            this.checkBoxSunday.TabIndex = 26;
            this.checkBoxSunday.Text = "Sunday";
            this.checkBoxSunday.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaturday
            // 
            this.checkBoxSaturday.AutoSize = true;
            this.checkBoxSaturday.Location = new System.Drawing.Point(100, 42);
            this.checkBoxSaturday.Name = "checkBoxSaturday";
            this.checkBoxSaturday.Size = new System.Drawing.Size(68, 17);
            this.checkBoxSaturday.TabIndex = 25;
            this.checkBoxSaturday.Text = "Saturday";
            this.checkBoxSaturday.UseVisualStyleBackColor = true;
            // 
            // checkBoxFriday
            // 
            this.checkBoxFriday.AutoSize = true;
            this.checkBoxFriday.Location = new System.Drawing.Point(408, 19);
            this.checkBoxFriday.Name = "checkBoxFriday";
            this.checkBoxFriday.Size = new System.Drawing.Size(54, 17);
            this.checkBoxFriday.TabIndex = 23;
            this.checkBoxFriday.Text = "Friday";
            this.checkBoxFriday.UseVisualStyleBackColor = true;
            // 
            // checkBoxThursday
            // 
            this.checkBoxThursday.AutoSize = true;
            this.checkBoxThursday.Location = new System.Drawing.Point(332, 19);
            this.checkBoxThursday.Name = "checkBoxThursday";
            this.checkBoxThursday.Size = new System.Drawing.Size(70, 17);
            this.checkBoxThursday.TabIndex = 22;
            this.checkBoxThursday.Text = "Thursday";
            this.checkBoxThursday.UseVisualStyleBackColor = true;
            // 
            // checkBoxWednesday
            // 
            this.checkBoxWednesday.AutoSize = true;
            this.checkBoxWednesday.Location = new System.Drawing.Point(243, 19);
            this.checkBoxWednesday.Name = "checkBoxWednesday";
            this.checkBoxWednesday.Size = new System.Drawing.Size(83, 17);
            this.checkBoxWednesday.TabIndex = 21;
            this.checkBoxWednesday.Text = "Wednesday";
            this.checkBoxWednesday.UseVisualStyleBackColor = true;
            // 
            // checkBoxTuesday
            // 
            this.checkBoxTuesday.AutoSize = true;
            this.checkBoxTuesday.Location = new System.Drawing.Point(170, 19);
            this.checkBoxTuesday.Name = "checkBoxTuesday";
            this.checkBoxTuesday.Size = new System.Drawing.Size(67, 17);
            this.checkBoxTuesday.TabIndex = 20;
            this.checkBoxTuesday.Text = "Tuesday";
            this.checkBoxTuesday.UseVisualStyleBackColor = true;
            // 
            // labelTakeScreenshotsOnce
            // 
            this.labelTakeScreenshotsOnce.AutoSize = true;
            this.labelTakeScreenshotsOnce.Location = new System.Drawing.Point(101, 133);
            this.labelTakeScreenshotsOnce.Name = "labelTakeScreenshotsOnce";
            this.labelTakeScreenshotsOnce.Size = new System.Drawing.Size(140, 13);
            this.labelTakeScreenshotsOnce.TabIndex = 5;
            this.labelTakeScreenshotsOnce.Text = "Take screenshots exactly at";
            // 
            // labelTakeScreenshotsPeriod
            // 
            this.labelTakeScreenshotsPeriod.AutoSize = true;
            this.labelTakeScreenshotsPeriod.Location = new System.Drawing.Point(101, 169);
            this.labelTakeScreenshotsPeriod.Name = "labelTakeScreenshotsPeriod";
            this.labelTakeScreenshotsPeriod.Size = new System.Drawing.Size(141, 13);
            this.labelTakeScreenshotsPeriod.TabIndex = 8;
            this.labelTakeScreenshotsPeriod.Text = "Take screenshots starting at";
            // 
            // labelAnd
            // 
            this.labelAnd.AutoSize = true;
            this.labelAnd.Location = new System.Drawing.Point(317, 169);
            this.labelAnd.Name = "labelAnd";
            this.labelAnd.Size = new System.Drawing.Size(80, 13);
            this.labelAnd.TabIndex = 10;
            this.labelAnd.Text = "and stopping at";
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
            this.labelHelp.Size = new System.Drawing.Size(688, 17);
            this.labelHelp.TabIndex = 0;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(9, 239);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(38, 13);
            this.labelNotes.TabIndex = 27;
            this.labelNotes.Text = "Notes:";
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNotes.Location = new System.Drawing.Point(9, 259);
            this.textBoxNotes.MaxLength = 500;
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNotes.Size = new System.Drawing.Size(673, 126);
            this.textBoxNotes.TabIndex = 28;
            // 
            // numericUpDownSecondsInterval
            // 
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(138, 24);
            this.numericUpDownSecondsInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSecondsInterval.Name = "numericUpDownSecondsInterval";
            this.numericUpDownSecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSecondsInterval.TabIndex = 15;
            // 
            // numericUpDownMinutesInterval
            // 
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(74, 24);
            this.numericUpDownMinutesInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownMinutesInterval.Name = "numericUpDownMinutesInterval";
            this.numericUpDownMinutesInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMinutesInterval.TabIndex = 14;
            // 
            // numericUpDownHoursInterval
            // 
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(10, 24);
            this.numericUpDownHoursInterval.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownHoursInterval.Name = "numericUpDownHoursInterval";
            this.numericUpDownHoursInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownHoursInterval.TabIndex = 13;
            // 
            // groupBoxInterval
            // 
            this.groupBoxInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInterval.Controls.Add(this.labelMinutes);
            this.groupBoxInterval.Controls.Add(this.labelHours);
            this.groupBoxInterval.Controls.Add(this.numericUpDownHoursInterval);
            this.groupBoxInterval.Controls.Add(this.numericUpDownMinutesInterval);
            this.groupBoxInterval.Controls.Add(this.numericUpDownSecondsInterval);
            this.groupBoxInterval.Location = new System.Drawing.Point(493, 166);
            this.groupBoxInterval.Name = "groupBoxInterval";
            this.groupBoxInterval.Size = new System.Drawing.Size(189, 56);
            this.groupBoxInterval.TabIndex = 31;
            this.groupBoxInterval.TabStop = false;
            this.groupBoxInterval.Text = "Interval";
            // 
            // labelMinutes
            // 
            this.labelMinutes.AutoSize = true;
            this.labelMinutes.Location = new System.Drawing.Point(122, 26);
            this.labelMinutes.Name = "labelMinutes";
            this.labelMinutes.Size = new System.Drawing.Size(10, 13);
            this.labelMinutes.TabIndex = 18;
            this.labelMinutes.Text = ":";
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(58, 26);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(10, 13);
            this.labelHours.TabIndex = 17;
            this.labelHours.Text = ":";
            // 
            // comboBoxScope
            // 
            this.comboBoxScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScope.FormattingEnabled = true;
            this.comboBoxScope.Location = new System.Drawing.Point(491, 75);
            this.comboBoxScope.Name = "comboBoxScope";
            this.comboBoxScope.Size = new System.Drawing.Size(189, 21);
            this.comboBoxScope.TabIndex = 32;
            this.comboBoxScope.TabStop = false;
            // 
            // labelScope
            // 
            this.labelScope.AutoSize = true;
            this.labelScope.Location = new System.Drawing.Point(493, 58);
            this.labelScope.Name = "labelScope";
            this.labelScope.Size = new System.Drawing.Size(41, 13);
            this.labelScope.TabIndex = 33;
            this.labelScope.Text = "Scope:";
            // 
            // comboBoxLogic
            // 
            this.comboBoxLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLogic.FormattingEnabled = true;
            this.comboBoxLogic.Items.AddRange(new object[] {
            "Schedule controls main timer and overrides main interval",
            "Schedule acts independently and can overlap other schedules"});
            this.comboBoxLogic.Location = new System.Drawing.Point(143, 201);
            this.comboBoxLogic.Name = "comboBoxLogic";
            this.comboBoxLogic.Size = new System.Drawing.Size(344, 21);
            this.comboBoxLogic.TabIndex = 34;
            this.comboBoxLogic.TabStop = false;
            // 
            // labelLogic
            // 
            this.labelLogic.AutoSize = true;
            this.labelLogic.Location = new System.Drawing.Point(101, 204);
            this.labelLogic.Name = "labelLogic";
            this.labelLogic.Size = new System.Drawing.Size(36, 13);
            this.labelLogic.TabIndex = 35;
            this.labelLogic.Text = "Logic:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 399);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "label1";
            // 
            // FormSchedule
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(692, 434);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelLogic);
            this.Controls.Add(this.comboBoxLogic);
            this.Controls.Add(this.labelScope);
            this.Controls.Add(this.comboBoxScope);
            this.Controls.Add(this.groupBoxInterval);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.labelAnd);
            this.Controls.Add(this.labelTakeScreenshotsPeriod);
            this.Controls.Add(this.labelTakeScreenshotsOnce);
            this.Controls.Add(this.dateTimePickerCaptureAt);
            this.Controls.Add(this.radioButtonPeriod);
            this.Controls.Add(this.radioButtonOneTime);
            this.Controls.Add(this.dateTimePickerStopAt);
            this.Controls.Add(this.dateTimePickerStartAt);
            this.Controls.Add(this.checkBoxEnable);
            this.Controls.Add(this.labelScheduleName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxDays);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(708, 473);
            this.Name = "FormSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormSchedule_Load);
            this.Shown += new System.EventHandler(this.FormSchedule_Shown);
            this.groupBoxDays.ResumeLayout(false);
            this.groupBoxDays.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            this.groupBoxInterval.ResumeLayout(false);
            this.groupBoxInterval.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelScheduleName;
        private System.Windows.Forms.CheckBox checkBoxEnable;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartAt;
        private System.Windows.Forms.DateTimePicker dateTimePickerStopAt;
        private System.Windows.Forms.RadioButton radioButtonOneTime;
        private System.Windows.Forms.RadioButton radioButtonPeriod;
        private System.Windows.Forms.DateTimePicker dateTimePickerCaptureAt;
        private System.Windows.Forms.CheckBox checkBoxMonday;
        private System.Windows.Forms.GroupBox groupBoxDays;
        private System.Windows.Forms.CheckBox checkBoxWednesday;
        private System.Windows.Forms.CheckBox checkBoxTuesday;
        private System.Windows.Forms.CheckBox checkBoxSunday;
        private System.Windows.Forms.CheckBox checkBoxSaturday;
        private System.Windows.Forms.CheckBox checkBoxFriday;
        private System.Windows.Forms.CheckBox checkBoxThursday;
        private System.Windows.Forms.CheckBox checkBoxWeekend;
        private System.Windows.Forms.CheckBox checkBoxWorkWeek;
        private System.Windows.Forms.Label labelTakeScreenshotsOnce;
        private System.Windows.Forms.Label labelTakeScreenshotsPeriod;
        private System.Windows.Forms.Label labelAnd;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;
        private System.Windows.Forms.GroupBox groupBoxInterval;
        private System.Windows.Forms.Label labelMinutes;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.ComboBox comboBoxScope;
        private System.Windows.Forms.Label labelScope;
        private System.Windows.Forms.ComboBox comboBoxLogic;
        private System.Windows.Forms.Label labelLogic;
        private System.Windows.Forms.Label label1;
    }
}