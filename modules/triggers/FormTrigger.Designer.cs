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
            this.labelDate = new System.Windows.Forms.Label();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelInterval = new System.Windows.Forms.Label();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMillisecondsInterval = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
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
            this.textBoxTriggerName.Size = new System.Drawing.Size(546, 20);
            this.textBoxTriggerName.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Location = new System.Drawing.Point(12, 420);
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
            this.buttonCancel.Location = new System.Drawing.Point(117, 420);
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
            this.listBoxModuleItemList.Size = new System.Drawing.Size(235, 290);
            this.listBoxModuleItemList.TabIndex = 9;
            this.listBoxModuleItemList.MouseHover += new System.EventHandler(this.listBoxModuleItemList_MouseHover);
            // 
            // labelModuleItemList
            // 
            this.labelModuleItemList.AutoSize = true;
            this.labelModuleItemList.Location = new System.Drawing.Point(509, 59);
            this.labelModuleItemList.Name = "labelModuleItemList";
            this.labelModuleItemList.Size = new System.Drawing.Size(244, 13);
            this.labelModuleItemList.TabIndex = 6;
            this.labelModuleItemList.Text = "Editor, Screen, Region, Schedule, Tag, or Trigger:";
            // 
            // dateTimePickerTime
            // 
            this.dateTimePickerTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerTime.CustomFormat = "HH:mm:ss";
            this.dateTimePickerTime.Enabled = false;
            this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTime.Location = new System.Drawing.Point(179, 376);
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
            this.listBoxCondition.Size = new System.Drawing.Size(235, 290);
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
            this.listBoxAction.Size = new System.Drawing.Size(235, 290);
            this.listBoxAction.TabIndex = 8;
            this.listBoxAction.SelectedIndexChanged += new System.EventHandler(this.listBoxAction_SelectedIndexChanged);
            this.listBoxAction.MouseHover += new System.EventHandler(this.listBoxAction_MouseHover);
            // 
            // labelDate
            // 
            this.labelDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(9, 378);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(33, 13);
            this.labelDate.TabIndex = 10;
            this.labelDate.Text = "Date:";
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerDate.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dateTimePickerDate.Enabled = false;
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Location = new System.Drawing.Point(48, 376);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(79, 20);
            this.dateTimePickerDate.TabIndex = 11;
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(140, 378);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(33, 13);
            this.labelTime.TabIndex = 12;
            this.labelTime.Text = "Time:";
            // 
            // labelInterval
            // 
            this.labelInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(259, 378);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(45, 13);
            this.labelInterval.TabIndex = 14;
            this.labelInterval.Text = "Interval:";
            // 
            // numericUpDownHoursInterval
            // 
            this.numericUpDownHoursInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(310, 376);
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
            this.numericUpDownMinutesInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(358, 376);
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
            this.numericUpDownSecondsInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(406, 376);
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
            this.numericUpDownMillisecondsInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownMillisecondsInterval.Location = new System.Drawing.Point(454, 376);
            this.numericUpDownMillisecondsInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownMillisecondsInterval.Name = "numericUpDownMillisecondsInterval";
            this.numericUpDownMillisecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMillisecondsInterval.TabIndex = 18;
            // 
            // FormTrigger
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(756, 454);
            this.Controls.Add(this.numericUpDownMillisecondsInterval);
            this.Controls.Add(this.numericUpDownSecondsInterval);
            this.Controls.Add(this.numericUpDownMinutesInterval);
            this.Controls.Add(this.numericUpDownHoursInterval);
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.listBoxAction);
            this.Controls.Add(this.listBoxCondition);
            this.Controls.Add(this.dateTimePickerTime);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(772, 493);
            this.Name = "FormTrigger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormTrigger_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
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
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownMillisecondsInterval;
    }
}