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
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.listBoxCondition = new System.Windows.Forms.ListBox();
            this.listBoxModuleItemList = new System.Windows.Forms.ListBox();
            this.listBoxAction = new System.Windows.Forms.ListBox();
            this.labelTriggerValue = new System.Windows.Forms.Label();
            this.textBoxTriggerValue = new System.Windows.Forms.TextBox();
            this.labelDay = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelDays = new System.Windows.Forms.Label();
            this.numericUpDownDays = new System.Windows.Forms.NumericUpDown();
            this.comboBoxDay = new System.Windows.Forms.ComboBox();
            this.dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.numericUpDownMillisecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.textBoxConditionHelp = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelPage = new System.Windows.Forms.Label();
            this.groupBoxCondition = new System.Windows.Forms.GroupBox();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.labelInterval = new System.Windows.Forms.Label();
            this.textBoxActionHelp = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            this.groupBoxCondition.SuspendLayout();
            this.groupBoxAction.SuspendLayout();
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
            this.textBoxTriggerName.Size = new System.Drawing.Size(546, 20);
            this.textBoxTriggerName.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(117, 428);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Location = new System.Drawing.Point(691, 34);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(56, 17);
            this.checkBoxActive.TabIndex = 3;
            this.checkBoxActive.TabStop = false;
            this.checkBoxActive.Text = "Active";
            this.checkBoxActive.UseVisualStyleBackColor = true;
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
            this.labelHelp.Size = new System.Drawing.Size(772, 17);
            this.labelHelp.TabIndex = 0;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxCondition
            // 
            this.listBoxCondition.FormattingEnabled = true;
            this.listBoxCondition.Location = new System.Drawing.Point(6, 68);
            this.listBoxCondition.Name = "listBoxCondition";
            this.listBoxCondition.Size = new System.Drawing.Size(348, 225);
            this.listBoxCondition.TabIndex = 5;
            this.listBoxCondition.TabStop = false;
            this.listBoxCondition.SelectedIndexChanged += new System.EventHandler(this.listBoxCondition_SelectedIndexChanged);
            // 
            // listBoxModuleItemList
            // 
            this.listBoxModuleItemList.FormattingEnabled = true;
            this.listBoxModuleItemList.Location = new System.Drawing.Point(6, 197);
            this.listBoxModuleItemList.Name = "listBoxModuleItemList";
            this.listBoxModuleItemList.Size = new System.Drawing.Size(354, 95);
            this.listBoxModuleItemList.TabIndex = 14;
            this.listBoxModuleItemList.TabStop = false;
            this.listBoxModuleItemList.UseTabStops = false;
            // 
            // listBoxAction
            // 
            this.listBoxAction.FormattingEnabled = true;
            this.listBoxAction.Location = new System.Drawing.Point(6, 68);
            this.listBoxAction.Name = "listBoxAction";
            this.listBoxAction.Size = new System.Drawing.Size(354, 121);
            this.listBoxAction.TabIndex = 22;
            this.listBoxAction.TabStop = false;
            this.listBoxAction.SelectedIndexChanged += new System.EventHandler(this.listBoxAction_SelectedIndexChanged);
            // 
            // labelTriggerValue
            // 
            this.labelTriggerValue.AutoSize = true;
            this.labelTriggerValue.Enabled = false;
            this.labelTriggerValue.Location = new System.Drawing.Point(6, 303);
            this.labelTriggerValue.Name = "labelTriggerValue";
            this.labelTriggerValue.Size = new System.Drawing.Size(105, 13);
            this.labelTriggerValue.TabIndex = 28;
            this.labelTriggerValue.Text = "Active Window Title:";
            // 
            // textBoxTriggerValue
            // 
            this.textBoxTriggerValue.Enabled = false;
            this.textBoxTriggerValue.Location = new System.Drawing.Point(126, 300);
            this.textBoxTriggerValue.Name = "textBoxTriggerValue";
            this.textBoxTriggerValue.Size = new System.Drawing.Size(234, 20);
            this.textBoxTriggerValue.TabIndex = 27;
            this.textBoxTriggerValue.TabStop = false;
            // 
            // labelDay
            // 
            this.labelDay.AutoSize = true;
            this.labelDay.Enabled = false;
            this.labelDay.Location = new System.Drawing.Point(6, 332);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(29, 13);
            this.labelDay.TabIndex = 26;
            this.labelDay.Text = "Day:";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Enabled = false;
            this.labelTime.Location = new System.Drawing.Point(180, 332);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(33, 13);
            this.labelTime.TabIndex = 25;
            this.labelTime.Text = "Time:";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Enabled = false;
            this.labelDate.Location = new System.Drawing.Point(5, 303);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(33, 13);
            this.labelDate.TabIndex = 24;
            this.labelDate.Text = "Date:";
            // 
            // labelDays
            // 
            this.labelDays.AutoSize = true;
            this.labelDays.Enabled = false;
            this.labelDays.Location = new System.Drawing.Point(6, 332);
            this.labelDays.Name = "labelDays";
            this.labelDays.Size = new System.Drawing.Size(34, 13);
            this.labelDays.TabIndex = 23;
            this.labelDays.Text = "Days:";
            // 
            // numericUpDownDays
            // 
            this.numericUpDownDays.Enabled = false;
            this.numericUpDownDays.Location = new System.Drawing.Point(46, 328);
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
            this.comboBoxDay.Location = new System.Drawing.Point(44, 328);
            this.comboBoxDay.Name = "comboBoxDay";
            this.comboBoxDay.Size = new System.Drawing.Size(130, 21);
            this.comboBoxDay.TabIndex = 14;
            this.comboBoxDay.TabStop = false;
            // 
            // dateTimePickerTime
            // 
            this.dateTimePickerTime.Enabled = false;
            this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTime.Location = new System.Drawing.Point(219, 328);
            this.dateTimePickerTime.Name = "dateTimePickerTime";
            this.dateTimePickerTime.ShowUpDown = true;
            this.dateTimePickerTime.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerTime.TabIndex = 14;
            this.dateTimePickerTime.TabStop = false;
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Enabled = false;
            this.dateTimePickerDate.Location = new System.Drawing.Point(44, 300);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(310, 20);
            this.dateTimePickerDate.TabIndex = 14;
            this.dateTimePickerDate.TabStop = false;
            // 
            // numericUpDownMillisecondsInterval
            // 
            this.numericUpDownMillisecondsInterval.Enabled = false;
            this.numericUpDownMillisecondsInterval.Location = new System.Drawing.Point(318, 328);
            this.numericUpDownMillisecondsInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownMillisecondsInterval.Name = "numericUpDownMillisecondsInterval";
            this.numericUpDownMillisecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMillisecondsInterval.TabIndex = 15;
            this.numericUpDownMillisecondsInterval.TabStop = false;
            // 
            // numericUpDownHoursInterval
            // 
            this.numericUpDownHoursInterval.Enabled = false;
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(174, 328);
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
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(222, 328);
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
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(270, 328);
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
            this.textBoxConditionHelp.Size = new System.Drawing.Size(348, 43);
            this.textBoxConditionHelp.TabIndex = 11;
            this.textBoxConditionHelp.TabStop = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(12, 428);
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
            this.groupBoxCondition.Size = new System.Drawing.Size(360, 358);
            this.groupBoxCondition.TabIndex = 31;
            this.groupBoxCondition.TabStop = false;
            this.groupBoxCondition.Text = "Condition";
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.labelInterval);
            this.groupBoxAction.Controls.Add(this.labelDays);
            this.groupBoxAction.Controls.Add(this.numericUpDownMillisecondsInterval);
            this.groupBoxAction.Controls.Add(this.listBoxModuleItemList);
            this.groupBoxAction.Controls.Add(this.textBoxActionHelp);
            this.groupBoxAction.Controls.Add(this.listBoxAction);
            this.groupBoxAction.Controls.Add(this.numericUpDownMinutesInterval);
            this.groupBoxAction.Controls.Add(this.labelTriggerValue);
            this.groupBoxAction.Controls.Add(this.numericUpDownHoursInterval);
            this.groupBoxAction.Controls.Add(this.numericUpDownDays);
            this.groupBoxAction.Controls.Add(this.textBoxTriggerValue);
            this.groupBoxAction.Controls.Add(this.numericUpDownSecondsInterval);
            this.groupBoxAction.Location = new System.Drawing.Point(378, 58);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(366, 358);
            this.groupBoxAction.TabIndex = 32;
            this.groupBoxAction.TabStop = false;
            this.groupBoxAction.Text = "Action";
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Enabled = false;
            this.labelInterval.Location = new System.Drawing.Point(123, 332);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(45, 13);
            this.labelInterval.TabIndex = 34;
            this.labelInterval.Text = "Interval:";
            // 
            // textBoxActionHelp
            // 
            this.textBoxActionHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxActionHelp.BackColor = System.Drawing.Color.LightYellow;
            this.textBoxActionHelp.Location = new System.Drawing.Point(6, 19);
            this.textBoxActionHelp.Multiline = true;
            this.textBoxActionHelp.Name = "textBoxActionHelp";
            this.textBoxActionHelp.ReadOnly = true;
            this.textBoxActionHelp.Size = new System.Drawing.Size(354, 43);
            this.textBoxActionHelp.TabIndex = 33;
            this.textBoxActionHelp.TabStop = false;
            // 
            // FormTrigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(756, 454);
            this.Controls.Add(this.groupBoxAction);
            this.Controls.Add(this.labelPage);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.checkBoxActive);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            this.groupBoxCondition.ResumeLayout(false);
            this.groupBoxCondition.PerformLayout();
            this.groupBoxAction.ResumeLayout(false);
            this.groupBoxAction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEditorName;
        private System.Windows.Forms.TextBox textBoxTriggerName;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.ListBox listBoxCondition;
        private System.Windows.Forms.TextBox textBoxConditionHelp;
        private System.Windows.Forms.NumericUpDown numericUpDownMillisecondsInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;
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
        private System.Windows.Forms.TextBox textBoxTriggerValue;
        private System.Windows.Forms.Label labelTriggerValue;
        private System.Windows.Forms.GroupBox groupBoxCondition;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.TextBox textBoxActionHelp;
        private System.Windows.Forms.Label labelInterval;
    }
}