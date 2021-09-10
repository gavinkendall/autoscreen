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
            this.numericUpDownMillisecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.labelInterval = new System.Windows.Forms.Label();
            this.groupBoxDays.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(117, 420);
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
            this.buttonOK.Location = new System.Drawing.Point(12, 420);
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
            this.textBoxName.Size = new System.Drawing.Size(546, 20);
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
            this.checkBoxEnable.Location = new System.Drawing.Point(688, 34);
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
            this.dateTimePickerStartAt.Location = new System.Drawing.Point(243, 110);
            this.dateTimePickerStartAt.Name = "dateTimePickerStartAt";
            this.dateTimePickerStartAt.ShowUpDown = true;
            this.dateTimePickerStartAt.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerStartAt.TabIndex = 9;
            // 
            // dateTimePickerStopAt
            // 
            this.dateTimePickerStopAt.CustomFormat = "HH:mm:ss";
            this.dateTimePickerStopAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStopAt.Location = new System.Drawing.Point(403, 110);
            this.dateTimePickerStopAt.Name = "dateTimePickerStopAt";
            this.dateTimePickerStopAt.ShowUpDown = true;
            this.dateTimePickerStopAt.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerStopAt.TabIndex = 11;
            // 
            // radioButtonOneTime
            // 
            this.radioButtonOneTime.AutoSize = true;
            this.radioButtonOneTime.Checked = true;
            this.radioButtonOneTime.Location = new System.Drawing.Point(12, 75);
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
            this.radioButtonPeriod.Location = new System.Drawing.Point(12, 111);
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
            this.dateTimePickerCaptureAt.Location = new System.Drawing.Point(243, 74);
            this.dateTimePickerCaptureAt.Name = "dateTimePickerCaptureAt";
            this.dateTimePickerCaptureAt.ShowUpDown = true;
            this.dateTimePickerCaptureAt.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerCaptureAt.TabIndex = 6;
            // 
            // checkBoxMonday
            // 
            this.checkBoxMonday.AutoSize = true;
            this.checkBoxMonday.Location = new System.Drawing.Point(119, 19);
            this.checkBoxMonday.Name = "checkBoxMonday";
            this.checkBoxMonday.Size = new System.Drawing.Size(64, 17);
            this.checkBoxMonday.TabIndex = 19;
            this.checkBoxMonday.Text = "Monday";
            this.checkBoxMonday.UseVisualStyleBackColor = true;
            // 
            // groupBoxDays
            // 
            this.groupBoxDays.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDays.Controls.Add(this.checkBoxWeekend);
            this.groupBoxDays.Controls.Add(this.checkBoxWorkWeek);
            this.groupBoxDays.Controls.Add(this.checkBoxSunday);
            this.groupBoxDays.Controls.Add(this.checkBoxSaturday);
            this.groupBoxDays.Controls.Add(this.checkBoxFriday);
            this.groupBoxDays.Controls.Add(this.checkBoxThursday);
            this.groupBoxDays.Controls.Add(this.checkBoxWednesday);
            this.groupBoxDays.Controls.Add(this.checkBoxTuesday);
            this.groupBoxDays.Controls.Add(this.checkBoxMonday);
            this.groupBoxDays.Location = new System.Drawing.Point(12, 149);
            this.groupBoxDays.Name = "groupBoxDays";
            this.groupBoxDays.Size = new System.Drawing.Size(732, 67);
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
            this.checkBoxSunday.Location = new System.Drawing.Point(189, 42);
            this.checkBoxSunday.Name = "checkBoxSunday";
            this.checkBoxSunday.Size = new System.Drawing.Size(62, 17);
            this.checkBoxSunday.TabIndex = 26;
            this.checkBoxSunday.Text = "Sunday";
            this.checkBoxSunday.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaturday
            // 
            this.checkBoxSaturday.AutoSize = true;
            this.checkBoxSaturday.Location = new System.Drawing.Point(119, 42);
            this.checkBoxSaturday.Name = "checkBoxSaturday";
            this.checkBoxSaturday.Size = new System.Drawing.Size(68, 17);
            this.checkBoxSaturday.TabIndex = 25;
            this.checkBoxSaturday.Text = "Saturday";
            this.checkBoxSaturday.UseVisualStyleBackColor = true;
            // 
            // checkBoxFriday
            // 
            this.checkBoxFriday.AutoSize = true;
            this.checkBoxFriday.Location = new System.Drawing.Point(427, 19);
            this.checkBoxFriday.Name = "checkBoxFriday";
            this.checkBoxFriday.Size = new System.Drawing.Size(54, 17);
            this.checkBoxFriday.TabIndex = 23;
            this.checkBoxFriday.Text = "Friday";
            this.checkBoxFriday.UseVisualStyleBackColor = true;
            // 
            // checkBoxThursday
            // 
            this.checkBoxThursday.AutoSize = true;
            this.checkBoxThursday.Location = new System.Drawing.Point(351, 19);
            this.checkBoxThursday.Name = "checkBoxThursday";
            this.checkBoxThursday.Size = new System.Drawing.Size(70, 17);
            this.checkBoxThursday.TabIndex = 22;
            this.checkBoxThursday.Text = "Thursday";
            this.checkBoxThursday.UseVisualStyleBackColor = true;
            // 
            // checkBoxWednesday
            // 
            this.checkBoxWednesday.AutoSize = true;
            this.checkBoxWednesday.Location = new System.Drawing.Point(262, 19);
            this.checkBoxWednesday.Name = "checkBoxWednesday";
            this.checkBoxWednesday.Size = new System.Drawing.Size(83, 17);
            this.checkBoxWednesday.TabIndex = 21;
            this.checkBoxWednesday.Text = "Wednesday";
            this.checkBoxWednesday.UseVisualStyleBackColor = true;
            // 
            // checkBoxTuesday
            // 
            this.checkBoxTuesday.AutoSize = true;
            this.checkBoxTuesday.Location = new System.Drawing.Point(189, 19);
            this.checkBoxTuesday.Name = "checkBoxTuesday";
            this.checkBoxTuesday.Size = new System.Drawing.Size(67, 17);
            this.checkBoxTuesday.TabIndex = 20;
            this.checkBoxTuesday.Text = "Tuesday";
            this.checkBoxTuesday.UseVisualStyleBackColor = true;
            // 
            // labelTakeScreenshotsOnce
            // 
            this.labelTakeScreenshotsOnce.AutoSize = true;
            this.labelTakeScreenshotsOnce.Location = new System.Drawing.Point(101, 77);
            this.labelTakeScreenshotsOnce.Name = "labelTakeScreenshotsOnce";
            this.labelTakeScreenshotsOnce.Size = new System.Drawing.Size(140, 13);
            this.labelTakeScreenshotsOnce.TabIndex = 5;
            this.labelTakeScreenshotsOnce.Text = "Take screenshots exactly at";
            // 
            // labelTakeScreenshotsPeriod
            // 
            this.labelTakeScreenshotsPeriod.AutoSize = true;
            this.labelTakeScreenshotsPeriod.Location = new System.Drawing.Point(101, 113);
            this.labelTakeScreenshotsPeriod.Name = "labelTakeScreenshotsPeriod";
            this.labelTakeScreenshotsPeriod.Size = new System.Drawing.Size(141, 13);
            this.labelTakeScreenshotsPeriod.TabIndex = 8;
            this.labelTakeScreenshotsPeriod.Text = "Take screenshots starting at";
            // 
            // labelAnd
            // 
            this.labelAnd.AutoSize = true;
            this.labelAnd.Location = new System.Drawing.Point(317, 113);
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
            this.labelHelp.Size = new System.Drawing.Size(752, 17);
            this.labelHelp.TabIndex = 0;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(9, 229);
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
            this.textBoxNotes.Location = new System.Drawing.Point(9, 245);
            this.textBoxNotes.MaxLength = 500;
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNotes.Size = new System.Drawing.Size(735, 160);
            this.textBoxNotes.TabIndex = 28;
            // 
            // numericUpDownMillisecondsInterval
            // 
            this.numericUpDownMillisecondsInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownMillisecondsInterval.Location = new System.Drawing.Point(700, 111);
            this.numericUpDownMillisecondsInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownMillisecondsInterval.Name = "numericUpDownMillisecondsInterval";
            this.numericUpDownMillisecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMillisecondsInterval.TabIndex = 16;
            // 
            // numericUpDownSecondsInterval
            // 
            this.numericUpDownSecondsInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(652, 111);
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
            this.numericUpDownMinutesInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(604, 111);
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
            this.numericUpDownHoursInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(556, 111);
            this.numericUpDownHoursInterval.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownHoursInterval.Name = "numericUpDownHoursInterval";
            this.numericUpDownHoursInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownHoursInterval.TabIndex = 13;
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(482, 113);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(63, 13);
            this.labelInterval.TabIndex = 12;
            this.labelInterval.Text = "with interval";
            // 
            // FormSchedule
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(756, 454);
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.numericUpDownMillisecondsInterval);
            this.Controls.Add(this.numericUpDownSecondsInterval);
            this.Controls.Add(this.numericUpDownMinutesInterval);
            this.Controls.Add(this.numericUpDownHoursInterval);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.labelAnd);
            this.Controls.Add(this.labelTakeScreenshotsPeriod);
            this.Controls.Add(this.labelTakeScreenshotsOnce);
            this.Controls.Add(this.groupBoxDays);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(772, 493);
            this.Name = "FormSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormSchedule_Load);
            this.groupBoxDays.ResumeLayout(false);
            this.groupBoxDays.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numericUpDownMillisecondsInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;
        private System.Windows.Forms.Label labelInterval;
    }
}