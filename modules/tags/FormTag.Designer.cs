namespace AutoScreenCapture
{
    partial class FormTag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTag));
            this.labelEditorName = new System.Windows.Forms.Label();
            this.textBoxTagName = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelTagType = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelDateTimeFormatValue = new System.Windows.Forms.Label();
            this.textBoxDateTimeFormatValue = new System.Windows.Forms.TextBox();
            this.groupBoxTimeOfDay = new System.Windows.Forms.GroupBox();
            this.checkBoxEveningExtendsToNextMorning = new System.Windows.Forms.CheckBox();
            this.labelEvening = new System.Windows.Forms.Label();
            this.labelAfternoon = new System.Windows.Forms.Label();
            this.textBoxEveningValue = new System.Windows.Forms.TextBox();
            this.dateTimePickerEveningEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEveningStart = new System.Windows.Forms.DateTimePicker();
            this.textBoxAfternoonValue = new System.Windows.Forms.TextBox();
            this.dateTimePickerAfternoonEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerAfternoonStart = new System.Windows.Forms.DateTimePicker();
            this.textBoxMorningValue = new System.Windows.Forms.TextBox();
            this.dateTimePickerMorningEnd = new System.Windows.Forms.DateTimePicker();
            this.labelMorning = new System.Windows.Forms.Label();
            this.dateTimePickerMorningStart = new System.Windows.Forms.DateTimePicker();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.groupBoxTimeOfDay.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelEditorName
            // 
            this.labelEditorName.AutoSize = true;
            this.labelEditorName.Location = new System.Drawing.Point(12, 35);
            this.labelEditorName.Name = "labelEditorName";
            this.labelEditorName.Size = new System.Drawing.Size(38, 13);
            this.labelEditorName.TabIndex = 1;
            this.labelEditorName.Text = "Name:";
            // 
            // textBoxTagName
            // 
            this.textBoxTagName.Location = new System.Drawing.Point(56, 32);
            this.textBoxTagName.MaxLength = 50;
            this.textBoxTagName.Name = "textBoxTagName";
            this.textBoxTagName.Size = new System.Drawing.Size(546, 20);
            this.textBoxTagName.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(12, 420);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(99, 23);
            this.buttonOK.TabIndex = 26;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(117, 420);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 27;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelTagType
            // 
            this.labelTagType.AutoSize = true;
            this.labelTagType.Location = new System.Drawing.Point(9, 114);
            this.labelTagType.Name = "labelTagType";
            this.labelTagType.Size = new System.Drawing.Size(34, 13);
            this.labelTagType.TabIndex = 6;
            this.labelTagType.Text = "Type:";
            // 
            // comboBoxType
            // 
            this.comboBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(53, 111);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(414, 21);
            this.comboBoxType.TabIndex = 7;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxType_SelectedIndexChanged);
            // 
            // labelDateTimeFormatValue
            // 
            this.labelDateTimeFormatValue.AutoSize = true;
            this.labelDateTimeFormatValue.Location = new System.Drawing.Point(9, 141);
            this.labelDateTimeFormatValue.Name = "labelDateTimeFormatValue";
            this.labelDateTimeFormatValue.Size = new System.Drawing.Size(126, 13);
            this.labelDateTimeFormatValue.TabIndex = 8;
            this.labelDateTimeFormatValue.Text = "Date/Time Format Value:";
            // 
            // textBoxDateTimeFormatValue
            // 
            this.textBoxDateTimeFormatValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDateTimeFormatValue.Location = new System.Drawing.Point(141, 138);
            this.textBoxDateTimeFormatValue.MaxLength = 50;
            this.textBoxDateTimeFormatValue.Name = "textBoxDateTimeFormatValue";
            this.textBoxDateTimeFormatValue.Size = new System.Drawing.Size(600, 20);
            this.textBoxDateTimeFormatValue.TabIndex = 9;
            // 
            // groupBoxTimeOfDay
            // 
            this.groupBoxTimeOfDay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTimeOfDay.Controls.Add(this.checkBoxEveningExtendsToNextMorning);
            this.groupBoxTimeOfDay.Controls.Add(this.labelEvening);
            this.groupBoxTimeOfDay.Controls.Add(this.labelAfternoon);
            this.groupBoxTimeOfDay.Controls.Add(this.textBoxEveningValue);
            this.groupBoxTimeOfDay.Controls.Add(this.dateTimePickerEveningEnd);
            this.groupBoxTimeOfDay.Controls.Add(this.dateTimePickerEveningStart);
            this.groupBoxTimeOfDay.Controls.Add(this.textBoxAfternoonValue);
            this.groupBoxTimeOfDay.Controls.Add(this.dateTimePickerAfternoonEnd);
            this.groupBoxTimeOfDay.Controls.Add(this.dateTimePickerAfternoonStart);
            this.groupBoxTimeOfDay.Controls.Add(this.textBoxMorningValue);
            this.groupBoxTimeOfDay.Controls.Add(this.dateTimePickerMorningEnd);
            this.groupBoxTimeOfDay.Controls.Add(this.labelMorning);
            this.groupBoxTimeOfDay.Controls.Add(this.dateTimePickerMorningStart);
            this.groupBoxTimeOfDay.Location = new System.Drawing.Point(16, 164);
            this.groupBoxTimeOfDay.Name = "groupBoxTimeOfDay";
            this.groupBoxTimeOfDay.Size = new System.Drawing.Size(728, 125);
            this.groupBoxTimeOfDay.TabIndex = 10;
            this.groupBoxTimeOfDay.TabStop = false;
            this.groupBoxTimeOfDay.Text = "Time of Day";
            // 
            // checkBoxEveningExtendsToNextMorning
            // 
            this.checkBoxEveningExtendsToNextMorning.AutoSize = true;
            this.checkBoxEveningExtendsToNextMorning.Location = new System.Drawing.Point(74, 101);
            this.checkBoxEveningExtendsToNextMorning.Name = "checkBoxEveningExtendsToNextMorning";
            this.checkBoxEveningExtendsToNextMorning.Size = new System.Drawing.Size(180, 17);
            this.checkBoxEveningExtendsToNextMorning.TabIndex = 23;
            this.checkBoxEveningExtendsToNextMorning.Text = "Evening extends to next morning";
            this.checkBoxEveningExtendsToNextMorning.UseVisualStyleBackColor = true;
            // 
            // labelEvening
            // 
            this.labelEvening.AutoSize = true;
            this.labelEvening.Location = new System.Drawing.Point(6, 78);
            this.labelEvening.Name = "labelEvening";
            this.labelEvening.Size = new System.Drawing.Size(49, 13);
            this.labelEvening.TabIndex = 19;
            this.labelEvening.Text = "Evening:";
            // 
            // labelAfternoon
            // 
            this.labelAfternoon.AutoSize = true;
            this.labelAfternoon.Location = new System.Drawing.Point(6, 52);
            this.labelAfternoon.Name = "labelAfternoon";
            this.labelAfternoon.Size = new System.Drawing.Size(56, 13);
            this.labelAfternoon.TabIndex = 15;
            this.labelAfternoon.Text = "Afternoon:";
            // 
            // textBoxEveningValue
            // 
            this.textBoxEveningValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEveningValue.Location = new System.Drawing.Point(222, 75);
            this.textBoxEveningValue.MaxLength = 50;
            this.textBoxEveningValue.Name = "textBoxEveningValue";
            this.textBoxEveningValue.Size = new System.Drawing.Size(500, 20);
            this.textBoxEveningValue.TabIndex = 22;
            // 
            // dateTimePickerEveningEnd
            // 
            this.dateTimePickerEveningEnd.CustomFormat = "HH:mm:ss";
            this.dateTimePickerEveningEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEveningEnd.Location = new System.Drawing.Point(148, 75);
            this.dateTimePickerEveningEnd.Name = "dateTimePickerEveningEnd";
            this.dateTimePickerEveningEnd.ShowUpDown = true;
            this.dateTimePickerEveningEnd.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerEveningEnd.TabIndex = 21;
            // 
            // dateTimePickerEveningStart
            // 
            this.dateTimePickerEveningStart.CustomFormat = "HH:mm:ss";
            this.dateTimePickerEveningStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEveningStart.Location = new System.Drawing.Point(74, 75);
            this.dateTimePickerEveningStart.Name = "dateTimePickerEveningStart";
            this.dateTimePickerEveningStart.ShowUpDown = true;
            this.dateTimePickerEveningStart.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerEveningStart.TabIndex = 20;
            // 
            // textBoxAfternoonValue
            // 
            this.textBoxAfternoonValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAfternoonValue.Location = new System.Drawing.Point(222, 49);
            this.textBoxAfternoonValue.MaxLength = 50;
            this.textBoxAfternoonValue.Name = "textBoxAfternoonValue";
            this.textBoxAfternoonValue.Size = new System.Drawing.Size(500, 20);
            this.textBoxAfternoonValue.TabIndex = 18;
            // 
            // dateTimePickerAfternoonEnd
            // 
            this.dateTimePickerAfternoonEnd.CustomFormat = "HH:mm:ss";
            this.dateTimePickerAfternoonEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerAfternoonEnd.Location = new System.Drawing.Point(148, 49);
            this.dateTimePickerAfternoonEnd.Name = "dateTimePickerAfternoonEnd";
            this.dateTimePickerAfternoonEnd.ShowUpDown = true;
            this.dateTimePickerAfternoonEnd.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerAfternoonEnd.TabIndex = 17;
            // 
            // dateTimePickerAfternoonStart
            // 
            this.dateTimePickerAfternoonStart.CustomFormat = "HH:mm:ss";
            this.dateTimePickerAfternoonStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerAfternoonStart.Location = new System.Drawing.Point(74, 49);
            this.dateTimePickerAfternoonStart.Name = "dateTimePickerAfternoonStart";
            this.dateTimePickerAfternoonStart.ShowUpDown = true;
            this.dateTimePickerAfternoonStart.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerAfternoonStart.TabIndex = 16;
            // 
            // textBoxMorningValue
            // 
            this.textBoxMorningValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMorningValue.Location = new System.Drawing.Point(222, 23);
            this.textBoxMorningValue.MaxLength = 50;
            this.textBoxMorningValue.Name = "textBoxMorningValue";
            this.textBoxMorningValue.Size = new System.Drawing.Size(500, 20);
            this.textBoxMorningValue.TabIndex = 14;
            // 
            // dateTimePickerMorningEnd
            // 
            this.dateTimePickerMorningEnd.CustomFormat = "HH:mm:ss";
            this.dateTimePickerMorningEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMorningEnd.Location = new System.Drawing.Point(148, 23);
            this.dateTimePickerMorningEnd.Name = "dateTimePickerMorningEnd";
            this.dateTimePickerMorningEnd.ShowUpDown = true;
            this.dateTimePickerMorningEnd.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerMorningEnd.TabIndex = 13;
            // 
            // labelMorning
            // 
            this.labelMorning.AutoSize = true;
            this.labelMorning.Location = new System.Drawing.Point(6, 26);
            this.labelMorning.Name = "labelMorning";
            this.labelMorning.Size = new System.Drawing.Size(48, 13);
            this.labelMorning.TabIndex = 11;
            this.labelMorning.Text = "Morning:";
            // 
            // dateTimePickerMorningStart
            // 
            this.dateTimePickerMorningStart.CustomFormat = "HH:mm:ss";
            this.dateTimePickerMorningStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMorningStart.Location = new System.Drawing.Point(74, 23);
            this.dateTimePickerMorningStart.Name = "dateTimePickerMorningStart";
            this.dateTimePickerMorningStart.ShowUpDown = true;
            this.dateTimePickerMorningStart.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerMorningStart.TabIndex = 12;
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
            this.labelHelp.Size = new System.Drawing.Size(749, 17);
            this.labelHelp.TabIndex = 0;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNotes.Location = new System.Drawing.Point(9, 313);
            this.textBoxNotes.MaxLength = 500;
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNotes.Size = new System.Drawing.Size(735, 92);
            this.textBoxNotes.TabIndex = 25;
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(9, 297);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(38, 13);
            this.labelNotes.TabIndex = 24;
            this.labelNotes.Text = "Notes:";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(9, 64);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(63, 13);
            this.labelDescription.TabIndex = 4;
            this.labelDescription.Text = "Description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(12, 80);
            this.textBoxDescription.MaxLength = 100;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(735, 20);
            this.textBoxDescription.TabIndex = 5;
            // 
            // FormTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(756, 454);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.checkBoxActive);
            this.Controls.Add(this.groupBoxTimeOfDay);
            this.Controls.Add(this.textBoxDateTimeFormatValue);
            this.Controls.Add(this.labelDateTimeFormatValue);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.labelTagType);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelEditorName);
            this.Controls.Add(this.textBoxTagName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(772, 493);
            this.Name = "FormTag";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormTag_Load);
            this.groupBoxTimeOfDay.ResumeLayout(false);
            this.groupBoxTimeOfDay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEditorName;
        private System.Windows.Forms.TextBox textBoxTagName;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelTagType;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelDateTimeFormatValue;
        private System.Windows.Forms.TextBox textBoxDateTimeFormatValue;
        private System.Windows.Forms.GroupBox groupBoxTimeOfDay;
        private System.Windows.Forms.Label labelEvening;
        private System.Windows.Forms.Label labelAfternoon;
        private System.Windows.Forms.TextBox textBoxEveningValue;
        private System.Windows.Forms.DateTimePicker dateTimePickerEveningEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerEveningStart;
        private System.Windows.Forms.TextBox textBoxAfternoonValue;
        private System.Windows.Forms.DateTimePicker dateTimePickerAfternoonEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerAfternoonStart;
        private System.Windows.Forms.TextBox textBoxMorningValue;
        private System.Windows.Forms.DateTimePicker dateTimePickerMorningEnd;
        private System.Windows.Forms.Label labelMorning;
        private System.Windows.Forms.DateTimePicker dateTimePickerMorningStart;
        private System.Windows.Forms.CheckBox checkBoxEveningExtendsToNextMorning;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
    }
}