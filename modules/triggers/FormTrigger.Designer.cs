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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelTriggerCondition = new System.Windows.Forms.Label();
            this.labelTriggerAction = new System.Windows.Forms.Label();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.listBoxModuleItemList = new System.Windows.Forms.ListBox();
            this.labelModuleItemList = new System.Windows.Forms.Label();
            this.dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
            this.listBoxCondition = new System.Windows.Forms.ListBox();
            this.listBoxAction = new System.Windows.Forms.ListBox();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMillisecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.labelDay = new System.Windows.Forms.Label();
            this.comboBoxDay = new System.Windows.Forms.ComboBox();
            this.labelDays = new System.Windows.Forms.Label();
            this.numericUpDownDays = new System.Windows.Forms.NumericUpDown();
            this.labelHours = new System.Windows.Forms.Label();
            this.labelMinutes = new System.Windows.Forms.Label();
            this.labelSeconds = new System.Windows.Forms.Label();
            this.groupBoxDateTime = new System.Windows.Forms.GroupBox();
            this.groupBoxInterval = new System.Windows.Forms.GroupBox();
            this.textBoxIntervalPreview = new System.Windows.Forms.TextBox();
            this.labelMilliseconds = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).BeginInit();
            this.groupBoxDateTime.SuspendLayout();
            this.groupBoxInterval.SuspendLayout();
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
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Location = new System.Drawing.Point(12, 423);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(99, 23);
            this.buttonOK.TabIndex = 19;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(117, 423);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelTriggerCondition
            // 
            this.labelTriggerCondition.AutoSize = true;
            this.labelTriggerCondition.Location = new System.Drawing.Point(9, 59);
            this.labelTriggerCondition.Name = "labelTriggerCondition";
            this.labelTriggerCondition.Size = new System.Drawing.Size(54, 13);
            this.labelTriggerCondition.TabIndex = 4;
            this.labelTriggerCondition.Text = "Condition:";
            // 
            // labelTriggerAction
            // 
            this.labelTriggerAction.AutoSize = true;
            this.labelTriggerAction.Location = new System.Drawing.Point(259, 59);
            this.labelTriggerAction.Name = "labelTriggerAction";
            this.labelTriggerAction.Size = new System.Drawing.Size(40, 13);
            this.labelTriggerAction.TabIndex = 5;
            this.labelTriggerAction.Text = "Action:";
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Location = new System.Drawing.Point(691, 34);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(56, 17);
            this.checkBoxActive.TabIndex = 3;
            this.checkBoxActive.Text = "Active";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            this.checkBoxActive.MouseHover += new System.EventHandler(this.checkBoxActive_MouseHover);
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
            // listBoxModuleItemList
            // 
            this.listBoxModuleItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxModuleItemList.FormattingEnabled = true;
            this.listBoxModuleItemList.Location = new System.Drawing.Point(512, 77);
            this.listBoxModuleItemList.Name = "listBoxModuleItemList";
            this.listBoxModuleItemList.Size = new System.Drawing.Size(235, 212);
            this.listBoxModuleItemList.TabIndex = 9;
            this.listBoxModuleItemList.MouseHover += new System.EventHandler(this.listBoxModuleItemList_MouseHover);
            // 
            // labelModuleItemList
            // 
            this.labelModuleItemList.AutoSize = true;
            this.labelModuleItemList.Location = new System.Drawing.Point(509, 59);
            this.labelModuleItemList.Name = "labelModuleItemList";
            this.labelModuleItemList.Size = new System.Drawing.Size(0, 13);
            this.labelModuleItemList.TabIndex = 6;
            // 
            // dateTimePickerTime
            // 
            this.dateTimePickerTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerTime.CustomFormat = "HH:mm:ss";
            this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTime.Location = new System.Drawing.Point(160, 16);
            this.dateTimePickerTime.Name = "dateTimePickerTime";
            this.dateTimePickerTime.ShowUpDown = true;
            this.dateTimePickerTime.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerTime.TabIndex = 13;
            // 
            // listBoxCondition
            // 
            this.listBoxCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxCondition.FormattingEnabled = true;
            this.listBoxCondition.Location = new System.Drawing.Point(12, 77);
            this.listBoxCondition.Name = "listBoxCondition";
            this.listBoxCondition.Size = new System.Drawing.Size(235, 212);
            this.listBoxCondition.TabIndex = 7;
            this.listBoxCondition.SelectedIndexChanged += new System.EventHandler(this.listBoxCondition_SelectedIndexChanged);
            this.listBoxCondition.MouseHover += new System.EventHandler(this.listBoxCondition_MouseHover);
            // 
            // listBoxAction
            // 
            this.listBoxAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxAction.FormattingEnabled = true;
            this.listBoxAction.Location = new System.Drawing.Point(262, 77);
            this.listBoxAction.Name = "listBoxAction";
            this.listBoxAction.Size = new System.Drawing.Size(235, 212);
            this.listBoxAction.TabIndex = 8;
            this.listBoxAction.SelectedIndexChanged += new System.EventHandler(this.listBoxAction_SelectedIndexChanged);
            this.listBoxAction.MouseHover += new System.EventHandler(this.listBoxAction_MouseHover);
            // 
            // labelDateTime
            // 
            this.labelDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDateTime.AutoSize = true;
            this.labelDateTime.Location = new System.Drawing.Point(6, 22);
            this.labelDateTime.Name = "labelDateTime";
            this.labelDateTime.Size = new System.Drawing.Size(61, 13);
            this.labelDateTime.TabIndex = 10;
            this.labelDateTime.Text = "Date/Time:";
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerDate.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Location = new System.Drawing.Point(73, 16);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(79, 20);
            this.dateTimePickerDate.TabIndex = 11;
            // 
            // numericUpDownHoursInterval
            // 
            this.numericUpDownHoursInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(6, 23);
            this.numericUpDownHoursInterval.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownHoursInterval.Name = "numericUpDownHoursInterval";
            this.numericUpDownHoursInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownHoursInterval.TabIndex = 15;
            // 
            // numericUpDownMinutesInterval
            // 
            this.numericUpDownMinutesInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(6, 49);
            this.numericUpDownMinutesInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownMinutesInterval.Name = "numericUpDownMinutesInterval";
            this.numericUpDownMinutesInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMinutesInterval.TabIndex = 16;
            // 
            // numericUpDownSecondsInterval
            // 
            this.numericUpDownSecondsInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(116, 23);
            this.numericUpDownSecondsInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSecondsInterval.Name = "numericUpDownSecondsInterval";
            this.numericUpDownSecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSecondsInterval.TabIndex = 17;
            // 
            // numericUpDownMillisecondsInterval
            // 
            this.numericUpDownMillisecondsInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownMillisecondsInterval.Location = new System.Drawing.Point(116, 49);
            this.numericUpDownMillisecondsInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownMillisecondsInterval.Name = "numericUpDownMillisecondsInterval";
            this.numericUpDownMillisecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMillisecondsInterval.TabIndex = 18;
            // 
            // labelDay
            // 
            this.labelDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDay.AutoSize = true;
            this.labelDay.Location = new System.Drawing.Point(6, 52);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(29, 13);
            this.labelDay.TabIndex = 21;
            this.labelDay.Text = "Day:";
            // 
            // comboBoxDay
            // 
            this.comboBoxDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.comboBoxDay.Location = new System.Drawing.Point(41, 48);
            this.comboBoxDay.Name = "comboBoxDay";
            this.comboBoxDay.Size = new System.Drawing.Size(77, 21);
            this.comboBoxDay.TabIndex = 22;
            // 
            // labelDays
            // 
            this.labelDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDays.AutoSize = true;
            this.labelDays.Location = new System.Drawing.Point(124, 52);
            this.labelDays.Name = "labelDays";
            this.labelDays.Size = new System.Drawing.Size(34, 13);
            this.labelDays.TabIndex = 23;
            this.labelDays.Text = "Days:";
            // 
            // numericUpDownDays
            // 
            this.numericUpDownDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownDays.Location = new System.Drawing.Point(160, 49);
            this.numericUpDownDays.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownDays.Name = "numericUpDownDays";
            this.numericUpDownDays.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownDays.TabIndex = 24;
            // 
            // labelHours
            // 
            this.labelHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(54, 25);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(33, 13);
            this.labelHours.TabIndex = 25;
            this.labelHours.Text = "hours";
            // 
            // labelMinutes
            // 
            this.labelMinutes.AutoSize = true;
            this.labelMinutes.Location = new System.Drawing.Point(54, 51);
            this.labelMinutes.Name = "labelMinutes";
            this.labelMinutes.Size = new System.Drawing.Size(43, 13);
            this.labelMinutes.TabIndex = 26;
            this.labelMinutes.Text = "minutes";
            // 
            // labelSeconds
            // 
            this.labelSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSeconds.AutoSize = true;
            this.labelSeconds.Location = new System.Drawing.Point(164, 25);
            this.labelSeconds.Name = "labelSeconds";
            this.labelSeconds.Size = new System.Drawing.Size(47, 13);
            this.labelSeconds.TabIndex = 27;
            this.labelSeconds.Text = "seconds";
            // 
            // groupBoxDateTime
            // 
            this.groupBoxDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDateTime.Controls.Add(this.labelDateTime);
            this.groupBoxDateTime.Controls.Add(this.dateTimePickerTime);
            this.groupBoxDateTime.Controls.Add(this.dateTimePickerDate);
            this.groupBoxDateTime.Controls.Add(this.numericUpDownDays);
            this.groupBoxDateTime.Controls.Add(this.labelDay);
            this.groupBoxDateTime.Controls.Add(this.labelDays);
            this.groupBoxDateTime.Controls.Add(this.comboBoxDay);
            this.groupBoxDateTime.Location = new System.Drawing.Point(512, 70);
            this.groupBoxDateTime.Name = "groupBoxDateTime";
            this.groupBoxDateTime.Size = new System.Drawing.Size(235, 79);
            this.groupBoxDateTime.TabIndex = 28;
            this.groupBoxDateTime.TabStop = false;
            // 
            // groupBoxInterval
            // 
            this.groupBoxInterval.Controls.Add(this.textBoxIntervalPreview);
            this.groupBoxInterval.Controls.Add(this.labelMilliseconds);
            this.groupBoxInterval.Controls.Add(this.numericUpDownHoursInterval);
            this.groupBoxInterval.Controls.Add(this.labelHours);
            this.groupBoxInterval.Controls.Add(this.numericUpDownMillisecondsInterval);
            this.groupBoxInterval.Controls.Add(this.labelSeconds);
            this.groupBoxInterval.Controls.Add(this.numericUpDownMinutesInterval);
            this.groupBoxInterval.Controls.Add(this.labelMinutes);
            this.groupBoxInterval.Controls.Add(this.numericUpDownSecondsInterval);
            this.groupBoxInterval.Location = new System.Drawing.Point(512, 155);
            this.groupBoxInterval.Name = "groupBoxInterval";
            this.groupBoxInterval.Size = new System.Drawing.Size(235, 107);
            this.groupBoxInterval.TabIndex = 29;
            this.groupBoxInterval.TabStop = false;
            this.groupBoxInterval.Text = "Interval";
            // 
            // textBoxIntervalPreview
            // 
            this.textBoxIntervalPreview.Location = new System.Drawing.Point(6, 78);
            this.textBoxIntervalPreview.Name = "textBoxIntervalPreview";
            this.textBoxIntervalPreview.ReadOnly = true;
            this.textBoxIntervalPreview.Size = new System.Drawing.Size(222, 20);
            this.textBoxIntervalPreview.TabIndex = 30;
            // 
            // labelMilliseconds
            // 
            this.labelMilliseconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMilliseconds.AutoSize = true;
            this.labelMilliseconds.Location = new System.Drawing.Point(164, 51);
            this.labelMilliseconds.Name = "labelMilliseconds";
            this.labelMilliseconds.Size = new System.Drawing.Size(63, 13);
            this.labelMilliseconds.TabIndex = 28;
            this.labelMilliseconds.Text = "milliseconds";
            // 
            // FormTrigger
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(756, 454);
            this.Controls.Add(this.groupBoxInterval);
            this.Controls.Add(this.groupBoxDateTime);
            this.Controls.Add(this.listBoxAction);
            this.Controls.Add(this.listBoxCondition);
            this.Controls.Add(this.labelModuleItemList);
            this.Controls.Add(this.listBoxModuleItemList);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.checkBoxActive);
            this.Controls.Add(this.labelTriggerAction);
            this.Controls.Add(this.labelTriggerCondition);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelEditorName);
            this.Controls.Add(this.textBoxTriggerName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(772, 493);
            this.Name = "FormTrigger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormTrigger_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDays)).EndInit();
            this.groupBoxDateTime.ResumeLayout(false);
            this.groupBoxDateTime.PerformLayout();
            this.groupBoxInterval.ResumeLayout(false);
            this.groupBoxInterval.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEditorName;
        private System.Windows.Forms.TextBox textBoxTriggerName;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelTriggerCondition;
        private System.Windows.Forms.Label labelTriggerAction;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.ListBox listBoxModuleItemList;
        private System.Windows.Forms.Label labelModuleItemList;
        private System.Windows.Forms.DateTimePicker dateTimePickerTime;
        private System.Windows.Forms.ListBox listBoxCondition;
        private System.Windows.Forms.ListBox listBoxAction;
        private System.Windows.Forms.Label labelDateTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMillisecondsInterval;
        private System.Windows.Forms.Label labelDay;
        private System.Windows.Forms.ComboBox comboBoxDay;
        private System.Windows.Forms.Label labelDays;
        private System.Windows.Forms.NumericUpDown numericUpDownDays;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.Label labelMinutes;
        private System.Windows.Forms.Label labelSeconds;
        private System.Windows.Forms.GroupBox groupBoxDateTime;
        private System.Windows.Forms.GroupBox groupBoxInterval;
        private System.Windows.Forms.Label labelMilliseconds;
        private System.Windows.Forms.TextBox textBoxIntervalPreview;
    }
}