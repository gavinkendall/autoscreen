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
            this.textBoxScheduleName = new System.Windows.Forms.TextBox();
            this.labelScheduleName = new System.Windows.Forms.Label();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.dateTimePickerScheduleStartAt = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerScheduleStopAt = new System.Windows.Forms.DateTimePicker();
            this.radioButtonOneTime = new System.Windows.Forms.RadioButton();
            this.radioButtonPeriod = new System.Windows.Forms.RadioButton();
            this.dateTimePickerSingleShot = new System.Windows.Forms.DateTimePicker();
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
            this.groupBoxDays.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(425, 193);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(344, 193);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxScheduleName
            // 
            this.textBoxScheduleName.Location = new System.Drawing.Point(53, 6);
            this.textBoxScheduleName.MaxLength = 50;
            this.textBoxScheduleName.Name = "textBoxScheduleName";
            this.textBoxScheduleName.Size = new System.Drawing.Size(318, 20);
            this.textBoxScheduleName.TabIndex = 1;
            // 
            // labelScheduleName
            // 
            this.labelScheduleName.AutoSize = true;
            this.labelScheduleName.Location = new System.Drawing.Point(9, 9);
            this.labelScheduleName.Name = "labelScheduleName";
            this.labelScheduleName.Size = new System.Drawing.Size(38, 13);
            this.labelScheduleName.TabIndex = 0;
            this.labelScheduleName.Text = "Name:";
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Location = new System.Drawing.Point(444, 8);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(56, 17);
            this.checkBoxActive.TabIndex = 7;
            this.checkBoxActive.TabStop = false;
            this.checkBoxActive.Text = "Active";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerScheduleStartAt
            // 
            this.dateTimePickerScheduleStartAt.CustomFormat = "HH:mm:ss";
            this.dateTimePickerScheduleStartAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerScheduleStartAt.Location = new System.Drawing.Point(243, 78);
            this.dateTimePickerScheduleStartAt.Name = "dateTimePickerScheduleStartAt";
            this.dateTimePickerScheduleStartAt.ShowUpDown = true;
            this.dateTimePickerScheduleStartAt.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerScheduleStartAt.TabIndex = 8;
            this.dateTimePickerScheduleStartAt.TabStop = false;
            // 
            // dateTimePickerScheduleStopAt
            // 
            this.dateTimePickerScheduleStopAt.CustomFormat = "HH:mm:ss";
            this.dateTimePickerScheduleStopAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerScheduleStopAt.Location = new System.Drawing.Point(348, 78);
            this.dateTimePickerScheduleStopAt.Name = "dateTimePickerScheduleStopAt";
            this.dateTimePickerScheduleStopAt.ShowUpDown = true;
            this.dateTimePickerScheduleStopAt.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerScheduleStopAt.TabIndex = 9;
            this.dateTimePickerScheduleStopAt.TabStop = false;
            // 
            // radioButtonOneTime
            // 
            this.radioButtonOneTime.AutoSize = true;
            this.radioButtonOneTime.Checked = true;
            this.radioButtonOneTime.Location = new System.Drawing.Point(12, 43);
            this.radioButtonOneTime.Name = "radioButtonOneTime";
            this.radioButtonOneTime.Size = new System.Drawing.Size(71, 17);
            this.radioButtonOneTime.TabIndex = 11;
            this.radioButtonOneTime.TabStop = true;
            this.radioButtonOneTime.Text = "One Time";
            this.radioButtonOneTime.UseVisualStyleBackColor = true;
            this.radioButtonOneTime.CheckedChanged += new System.EventHandler(this.radioButtonOneTime_CheckedChanged);
            // 
            // radioButtonPeriod
            // 
            this.radioButtonPeriod.AutoSize = true;
            this.radioButtonPeriod.Location = new System.Drawing.Point(12, 79);
            this.radioButtonPeriod.Name = "radioButtonPeriod";
            this.radioButtonPeriod.Size = new System.Drawing.Size(55, 17);
            this.radioButtonPeriod.TabIndex = 12;
            this.radioButtonPeriod.Text = "Period";
            this.radioButtonPeriod.UseVisualStyleBackColor = true;
            this.radioButtonPeriod.CheckedChanged += new System.EventHandler(this.radioButtonPeriod_CheckedChanged);
            // 
            // dateTimePickerSingleShot
            // 
            this.dateTimePickerSingleShot.CustomFormat = "HH:mm:ss";
            this.dateTimePickerSingleShot.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerSingleShot.Location = new System.Drawing.Point(243, 42);
            this.dateTimePickerSingleShot.Name = "dateTimePickerSingleShot";
            this.dateTimePickerSingleShot.ShowUpDown = true;
            this.dateTimePickerSingleShot.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerSingleShot.TabIndex = 13;
            this.dateTimePickerSingleShot.TabStop = false;
            // 
            // checkBoxMonday
            // 
            this.checkBoxMonday.AutoSize = true;
            this.checkBoxMonday.Location = new System.Drawing.Point(119, 19);
            this.checkBoxMonday.Name = "checkBoxMonday";
            this.checkBoxMonday.Size = new System.Drawing.Size(64, 17);
            this.checkBoxMonday.TabIndex = 14;
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
            this.groupBoxDays.Location = new System.Drawing.Point(12, 114);
            this.groupBoxDays.Name = "groupBoxDays";
            this.groupBoxDays.Size = new System.Drawing.Size(488, 67);
            this.groupBoxDays.TabIndex = 15;
            this.groupBoxDays.TabStop = false;
            this.groupBoxDays.Text = "Days";
            // 
            // checkBoxWeekend
            // 
            this.checkBoxWeekend.AutoSize = true;
            this.checkBoxWeekend.Location = new System.Drawing.Point(10, 42);
            this.checkBoxWeekend.Name = "checkBoxWeekend";
            this.checkBoxWeekend.Size = new System.Drawing.Size(73, 17);
            this.checkBoxWeekend.TabIndex = 22;
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
            this.checkBoxWorkWeek.TabIndex = 21;
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
            this.checkBoxSunday.TabIndex = 20;
            this.checkBoxSunday.Text = "Sunday";
            this.checkBoxSunday.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaturday
            // 
            this.checkBoxSaturday.AutoSize = true;
            this.checkBoxSaturday.Location = new System.Drawing.Point(119, 42);
            this.checkBoxSaturday.Name = "checkBoxSaturday";
            this.checkBoxSaturday.Size = new System.Drawing.Size(68, 17);
            this.checkBoxSaturday.TabIndex = 19;
            this.checkBoxSaturday.Text = "Saturday";
            this.checkBoxSaturday.UseVisualStyleBackColor = true;
            // 
            // checkBoxFriday
            // 
            this.checkBoxFriday.AutoSize = true;
            this.checkBoxFriday.Location = new System.Drawing.Point(427, 19);
            this.checkBoxFriday.Name = "checkBoxFriday";
            this.checkBoxFriday.Size = new System.Drawing.Size(54, 17);
            this.checkBoxFriday.TabIndex = 18;
            this.checkBoxFriday.Text = "Friday";
            this.checkBoxFriday.UseVisualStyleBackColor = true;
            // 
            // checkBoxThursday
            // 
            this.checkBoxThursday.AutoSize = true;
            this.checkBoxThursday.Location = new System.Drawing.Point(351, 19);
            this.checkBoxThursday.Name = "checkBoxThursday";
            this.checkBoxThursday.Size = new System.Drawing.Size(70, 17);
            this.checkBoxThursday.TabIndex = 17;
            this.checkBoxThursday.Text = "Thursday";
            this.checkBoxThursday.UseVisualStyleBackColor = true;
            // 
            // checkBoxWednesday
            // 
            this.checkBoxWednesday.AutoSize = true;
            this.checkBoxWednesday.Location = new System.Drawing.Point(262, 19);
            this.checkBoxWednesday.Name = "checkBoxWednesday";
            this.checkBoxWednesday.Size = new System.Drawing.Size(83, 17);
            this.checkBoxWednesday.TabIndex = 16;
            this.checkBoxWednesday.Text = "Wednesday";
            this.checkBoxWednesday.UseVisualStyleBackColor = true;
            // 
            // checkBoxTuesday
            // 
            this.checkBoxTuesday.AutoSize = true;
            this.checkBoxTuesday.Location = new System.Drawing.Point(189, 19);
            this.checkBoxTuesday.Name = "checkBoxTuesday";
            this.checkBoxTuesday.Size = new System.Drawing.Size(67, 17);
            this.checkBoxTuesday.TabIndex = 15;
            this.checkBoxTuesday.Text = "Tuesday";
            this.checkBoxTuesday.UseVisualStyleBackColor = true;
            // 
            // labelTakeScreenshotsOnce
            // 
            this.labelTakeScreenshotsOnce.AutoSize = true;
            this.labelTakeScreenshotsOnce.Location = new System.Drawing.Point(101, 45);
            this.labelTakeScreenshotsOnce.Name = "labelTakeScreenshotsOnce";
            this.labelTakeScreenshotsOnce.Size = new System.Drawing.Size(140, 13);
            this.labelTakeScreenshotsOnce.TabIndex = 16;
            this.labelTakeScreenshotsOnce.Text = "Take screenshots exactly at";
            // 
            // labelTakeScreenshotsPeriod
            // 
            this.labelTakeScreenshotsPeriod.AutoSize = true;
            this.labelTakeScreenshotsPeriod.Location = new System.Drawing.Point(101, 81);
            this.labelTakeScreenshotsPeriod.Name = "labelTakeScreenshotsPeriod";
            this.labelTakeScreenshotsPeriod.Size = new System.Drawing.Size(136, 13);
            this.labelTakeScreenshotsPeriod.TabIndex = 17;
            this.labelTakeScreenshotsPeriod.Text = "Take screenshots between";
            // 
            // labelAnd
            // 
            this.labelAnd.AutoSize = true;
            this.labelAnd.Location = new System.Drawing.Point(317, 81);
            this.labelAnd.Name = "labelAnd";
            this.labelAnd.Size = new System.Drawing.Size(25, 13);
            this.labelAnd.TabIndex = 18;
            this.labelAnd.Text = "and";
            // 
            // FormSchedule
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(512, 228);
            this.Controls.Add(this.labelAnd);
            this.Controls.Add(this.labelTakeScreenshotsPeriod);
            this.Controls.Add(this.labelTakeScreenshotsOnce);
            this.Controls.Add(this.groupBoxDays);
            this.Controls.Add(this.dateTimePickerSingleShot);
            this.Controls.Add(this.radioButtonPeriod);
            this.Controls.Add(this.radioButtonOneTime);
            this.Controls.Add(this.dateTimePickerScheduleStopAt);
            this.Controls.Add(this.dateTimePickerScheduleStartAt);
            this.Controls.Add(this.checkBoxActive);
            this.Controls.Add(this.labelScheduleName);
            this.Controls.Add(this.textBoxScheduleName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSchedule";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormSchedule_Load);
            this.groupBoxDays.ResumeLayout(false);
            this.groupBoxDays.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxScheduleName;
        private System.Windows.Forms.Label labelScheduleName;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.DateTimePicker dateTimePickerScheduleStartAt;
        private System.Windows.Forms.DateTimePicker dateTimePickerScheduleStopAt;
        private System.Windows.Forms.RadioButton radioButtonOneTime;
        private System.Windows.Forms.RadioButton radioButtonPeriod;
        private System.Windows.Forms.DateTimePicker dateTimePickerSingleShot;
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
    }
}