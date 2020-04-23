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
            this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
            this.groupBoxTimeOfDay.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelEditorName
            // 
            this.labelEditorName.AutoSize = true;
            this.labelEditorName.Location = new System.Drawing.Point(12, 9);
            this.labelEditorName.Name = "labelEditorName";
            this.labelEditorName.Size = new System.Drawing.Size(38, 13);
            this.labelEditorName.TabIndex = 0;
            this.labelEditorName.Text = "Name:";
            // 
            // textBoxTagName
            // 
            this.textBoxTagName.Location = new System.Drawing.Point(56, 6);
            this.textBoxTagName.MaxLength = 50;
            this.textBoxTagName.Name = "textBoxTagName";
            this.textBoxTagName.Size = new System.Drawing.Size(221, 20);
            this.textBoxTagName.TabIndex = 0;
            this.textBoxTagName.TabStop = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(398, 216);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.TabStop = false;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(479, 216);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelTagType
            // 
            this.labelTagType.AutoSize = true;
            this.labelTagType.Location = new System.Drawing.Point(12, 35);
            this.labelTagType.Name = "labelTagType";
            this.labelTagType.Size = new System.Drawing.Size(34, 13);
            this.labelTagType.TabIndex = 0;
            this.labelTagType.Text = "Type:";
            // 
            // comboBoxType
            // 
            this.comboBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(56, 32);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(221, 21);
            this.comboBoxType.TabIndex = 0;
            this.comboBoxType.TabStop = false;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxType_SelectedIndexChanged);
            // 
            // labelDateTimeFormatValue
            // 
            this.labelDateTimeFormatValue.AutoSize = true;
            this.labelDateTimeFormatValue.Location = new System.Drawing.Point(12, 62);
            this.labelDateTimeFormatValue.Name = "labelDateTimeFormatValue";
            this.labelDateTimeFormatValue.Size = new System.Drawing.Size(126, 13);
            this.labelDateTimeFormatValue.TabIndex = 0;
            this.labelDateTimeFormatValue.Text = "Date/Time Format Value:";
            // 
            // textBoxDateTimeFormatValue
            // 
            this.textBoxDateTimeFormatValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDateTimeFormatValue.Location = new System.Drawing.Point(144, 59);
            this.textBoxDateTimeFormatValue.MaxLength = 50;
            this.textBoxDateTimeFormatValue.Name = "textBoxDateTimeFormatValue";
            this.textBoxDateTimeFormatValue.Size = new System.Drawing.Size(407, 20);
            this.textBoxDateTimeFormatValue.TabIndex = 0;
            this.textBoxDateTimeFormatValue.TabStop = false;
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
            this.groupBoxTimeOfDay.Location = new System.Drawing.Point(19, 85);
            this.groupBoxTimeOfDay.Name = "groupBoxTimeOfDay";
            this.groupBoxTimeOfDay.Size = new System.Drawing.Size(535, 125);
            this.groupBoxTimeOfDay.TabIndex = 0;
            this.groupBoxTimeOfDay.TabStop = false;
            this.groupBoxTimeOfDay.Text = "Time of Day";
            // 
            // checkBoxEveningExtendsToNextMorning
            // 
            this.checkBoxEveningExtendsToNextMorning.AutoSize = true;
            this.checkBoxEveningExtendsToNextMorning.Location = new System.Drawing.Point(74, 101);
            this.checkBoxEveningExtendsToNextMorning.Name = "checkBoxEveningExtendsToNextMorning";
            this.checkBoxEveningExtendsToNextMorning.Size = new System.Drawing.Size(180, 17);
            this.checkBoxEveningExtendsToNextMorning.TabIndex = 0;
            this.checkBoxEveningExtendsToNextMorning.TabStop = false;
            this.checkBoxEveningExtendsToNextMorning.Text = "Evening extends to next morning";
            this.checkBoxEveningExtendsToNextMorning.UseVisualStyleBackColor = true;
            // 
            // labelEvening
            // 
            this.labelEvening.AutoSize = true;
            this.labelEvening.Location = new System.Drawing.Point(6, 78);
            this.labelEvening.Name = "labelEvening";
            this.labelEvening.Size = new System.Drawing.Size(49, 13);
            this.labelEvening.TabIndex = 0;
            this.labelEvening.Text = "Evening:";
            // 
            // labelAfternoon
            // 
            this.labelAfternoon.AutoSize = true;
            this.labelAfternoon.Location = new System.Drawing.Point(6, 52);
            this.labelAfternoon.Name = "labelAfternoon";
            this.labelAfternoon.Size = new System.Drawing.Size(56, 13);
            this.labelAfternoon.TabIndex = 0;
            this.labelAfternoon.Text = "Afternoon:";
            // 
            // textBoxEveningValue
            // 
            this.textBoxEveningValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEveningValue.Location = new System.Drawing.Point(222, 75);
            this.textBoxEveningValue.MaxLength = 50;
            this.textBoxEveningValue.Name = "textBoxEveningValue";
            this.textBoxEveningValue.Size = new System.Drawing.Size(307, 20);
            this.textBoxEveningValue.TabIndex = 0;
            this.textBoxEveningValue.TabStop = false;
            // 
            // dateTimePickerEveningEnd
            // 
            this.dateTimePickerEveningEnd.CustomFormat = "HH:mm:ss";
            this.dateTimePickerEveningEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEveningEnd.Location = new System.Drawing.Point(148, 75);
            this.dateTimePickerEveningEnd.Name = "dateTimePickerEveningEnd";
            this.dateTimePickerEveningEnd.ShowUpDown = true;
            this.dateTimePickerEveningEnd.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerEveningEnd.TabIndex = 0;
            this.dateTimePickerEveningEnd.TabStop = false;
            // 
            // dateTimePickerEveningStart
            // 
            this.dateTimePickerEveningStart.CustomFormat = "HH:mm:ss";
            this.dateTimePickerEveningStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEveningStart.Location = new System.Drawing.Point(74, 75);
            this.dateTimePickerEveningStart.Name = "dateTimePickerEveningStart";
            this.dateTimePickerEveningStart.ShowUpDown = true;
            this.dateTimePickerEveningStart.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerEveningStart.TabIndex = 0;
            this.dateTimePickerEveningStart.TabStop = false;
            // 
            // textBoxAfternoonValue
            // 
            this.textBoxAfternoonValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAfternoonValue.Location = new System.Drawing.Point(222, 49);
            this.textBoxAfternoonValue.MaxLength = 50;
            this.textBoxAfternoonValue.Name = "textBoxAfternoonValue";
            this.textBoxAfternoonValue.Size = new System.Drawing.Size(307, 20);
            this.textBoxAfternoonValue.TabIndex = 0;
            this.textBoxAfternoonValue.TabStop = false;
            // 
            // dateTimePickerAfternoonEnd
            // 
            this.dateTimePickerAfternoonEnd.CustomFormat = "HH:mm:ss";
            this.dateTimePickerAfternoonEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerAfternoonEnd.Location = new System.Drawing.Point(148, 49);
            this.dateTimePickerAfternoonEnd.Name = "dateTimePickerAfternoonEnd";
            this.dateTimePickerAfternoonEnd.ShowUpDown = true;
            this.dateTimePickerAfternoonEnd.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerAfternoonEnd.TabIndex = 0;
            this.dateTimePickerAfternoonEnd.TabStop = false;
            // 
            // dateTimePickerAfternoonStart
            // 
            this.dateTimePickerAfternoonStart.CustomFormat = "HH:mm:ss";
            this.dateTimePickerAfternoonStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerAfternoonStart.Location = new System.Drawing.Point(74, 49);
            this.dateTimePickerAfternoonStart.Name = "dateTimePickerAfternoonStart";
            this.dateTimePickerAfternoonStart.ShowUpDown = true;
            this.dateTimePickerAfternoonStart.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerAfternoonStart.TabIndex = 0;
            this.dateTimePickerAfternoonStart.TabStop = false;
            // 
            // textBoxMorningValue
            // 
            this.textBoxMorningValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMorningValue.Location = new System.Drawing.Point(222, 23);
            this.textBoxMorningValue.MaxLength = 50;
            this.textBoxMorningValue.Name = "textBoxMorningValue";
            this.textBoxMorningValue.Size = new System.Drawing.Size(307, 20);
            this.textBoxMorningValue.TabIndex = 0;
            this.textBoxMorningValue.TabStop = false;
            // 
            // dateTimePickerMorningEnd
            // 
            this.dateTimePickerMorningEnd.CustomFormat = "HH:mm:ss";
            this.dateTimePickerMorningEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMorningEnd.Location = new System.Drawing.Point(148, 23);
            this.dateTimePickerMorningEnd.Name = "dateTimePickerMorningEnd";
            this.dateTimePickerMorningEnd.ShowUpDown = true;
            this.dateTimePickerMorningEnd.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerMorningEnd.TabIndex = 0;
            this.dateTimePickerMorningEnd.TabStop = false;
            // 
            // labelMorning
            // 
            this.labelMorning.AutoSize = true;
            this.labelMorning.Location = new System.Drawing.Point(6, 26);
            this.labelMorning.Name = "labelMorning";
            this.labelMorning.Size = new System.Drawing.Size(48, 13);
            this.labelMorning.TabIndex = 0;
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
            this.dateTimePickerMorningStart.TabIndex = 0;
            this.dateTimePickerMorningStart.TabStop = false;
            // 
            // checkBoxEnabled
            // 
            this.checkBoxEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxEnabled.AutoSize = true;
            this.checkBoxEnabled.Location = new System.Drawing.Point(489, 8);
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.checkBoxEnabled.Size = new System.Drawing.Size(65, 17);
            this.checkBoxEnabled.TabIndex = 1;
            this.checkBoxEnabled.TabStop = false;
            this.checkBoxEnabled.Text = "Enabled";
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            // 
            // FormTag
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(563, 248);
            this.Controls.Add(this.checkBoxEnabled);
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
        private System.Windows.Forms.CheckBox checkBoxEnabled;
    }
}