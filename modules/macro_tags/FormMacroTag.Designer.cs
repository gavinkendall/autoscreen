namespace AutoScreenCapture
{
    partial class FormMacroTag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMacroTag));
            this.labelEditorName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelTagType = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelDateTimeFormatValue = new System.Windows.Forms.Label();
            this.textBoxDateTimeFormatValue = new System.Windows.Forms.TextBox();
            this.groupBoxTimeRange = new System.Windows.Forms.GroupBox();
            this.labelMacro4 = new System.Windows.Forms.Label();
            this.textBoxMacro4Macro = new System.Windows.Forms.TextBox();
            this.dateTimePickerMacro4End = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerMacro4Start = new System.Windows.Forms.DateTimePicker();
            this.labelMacro3 = new System.Windows.Forms.Label();
            this.labelMacro2 = new System.Windows.Forms.Label();
            this.textBoxMacro3Macro = new System.Windows.Forms.TextBox();
            this.dateTimePickerMacro3End = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerMacro3Start = new System.Windows.Forms.DateTimePicker();
            this.textBoxMacro2Macro = new System.Windows.Forms.TextBox();
            this.dateTimePickerMacro2End = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerMacro2Start = new System.Windows.Forms.DateTimePicker();
            this.textBoxMacro1Macro = new System.Windows.Forms.TextBox();
            this.dateTimePickerMacro1End = new System.Windows.Forms.DateTimePicker();
            this.labelMacro1 = new System.Windows.Forms.Label();
            this.dateTimePickerMacro1Start = new System.Windows.Forms.DateTimePicker();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.groupBoxTimeRange.SuspendLayout();
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
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(56, 32);
            this.textBoxName.MaxLength = 50;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(546, 20);
            this.textBoxName.TabIndex = 2;
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
            // labelTagType
            // 
            this.labelTagType.AutoSize = true;
            this.labelTagType.Location = new System.Drawing.Point(9, 61);
            this.labelTagType.Name = "labelTagType";
            this.labelTagType.Size = new System.Drawing.Size(34, 13);
            this.labelTagType.TabIndex = 4;
            this.labelTagType.Text = "Type:";
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(12, 81);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(243, 21);
            this.comboBoxType.TabIndex = 5;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxType_SelectedIndexChanged);
            // 
            // labelDateTimeFormatValue
            // 
            this.labelDateTimeFormatValue.AutoSize = true;
            this.labelDateTimeFormatValue.Location = new System.Drawing.Point(9, 118);
            this.labelDateTimeFormatValue.Name = "labelDateTimeFormatValue";
            this.labelDateTimeFormatValue.Size = new System.Drawing.Size(126, 13);
            this.labelDateTimeFormatValue.TabIndex = 8;
            this.labelDateTimeFormatValue.Text = "Date/Time Format Value:";
            // 
            // textBoxDateTimeFormatValue
            // 
            this.textBoxDateTimeFormatValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDateTimeFormatValue.Location = new System.Drawing.Point(141, 115);
            this.textBoxDateTimeFormatValue.MaxLength = 50;
            this.textBoxDateTimeFormatValue.Name = "textBoxDateTimeFormatValue";
            this.textBoxDateTimeFormatValue.Size = new System.Drawing.Size(600, 20);
            this.textBoxDateTimeFormatValue.TabIndex = 9;
            // 
            // groupBoxTimeRange
            // 
            this.groupBoxTimeRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTimeRange.Controls.Add(this.labelMacro4);
            this.groupBoxTimeRange.Controls.Add(this.textBoxMacro4Macro);
            this.groupBoxTimeRange.Controls.Add(this.dateTimePickerMacro4End);
            this.groupBoxTimeRange.Controls.Add(this.dateTimePickerMacro4Start);
            this.groupBoxTimeRange.Controls.Add(this.labelMacro3);
            this.groupBoxTimeRange.Controls.Add(this.labelMacro2);
            this.groupBoxTimeRange.Controls.Add(this.textBoxMacro3Macro);
            this.groupBoxTimeRange.Controls.Add(this.dateTimePickerMacro3End);
            this.groupBoxTimeRange.Controls.Add(this.dateTimePickerMacro3Start);
            this.groupBoxTimeRange.Controls.Add(this.textBoxMacro2Macro);
            this.groupBoxTimeRange.Controls.Add(this.dateTimePickerMacro2End);
            this.groupBoxTimeRange.Controls.Add(this.dateTimePickerMacro2Start);
            this.groupBoxTimeRange.Controls.Add(this.textBoxMacro1Macro);
            this.groupBoxTimeRange.Controls.Add(this.dateTimePickerMacro1End);
            this.groupBoxTimeRange.Controls.Add(this.labelMacro1);
            this.groupBoxTimeRange.Controls.Add(this.dateTimePickerMacro1Start);
            this.groupBoxTimeRange.Location = new System.Drawing.Point(16, 141);
            this.groupBoxTimeRange.Name = "groupBoxTimeRange";
            this.groupBoxTimeRange.Size = new System.Drawing.Size(728, 125);
            this.groupBoxTimeRange.TabIndex = 10;
            this.groupBoxTimeRange.TabStop = false;
            this.groupBoxTimeRange.Text = "Time Range";
            // 
            // labelMacro4
            // 
            this.labelMacro4.AutoSize = true;
            this.labelMacro4.Location = new System.Drawing.Point(6, 101);
            this.labelMacro4.Name = "labelMacro4";
            this.labelMacro4.Size = new System.Drawing.Size(49, 13);
            this.labelMacro4.TabIndex = 23;
            this.labelMacro4.Text = "Macro 4:";
            // 
            // textBoxMacro4Macro
            // 
            this.textBoxMacro4Macro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMacro4Macro.Location = new System.Drawing.Point(222, 98);
            this.textBoxMacro4Macro.MaxLength = 50;
            this.textBoxMacro4Macro.Name = "textBoxMacro4Macro";
            this.textBoxMacro4Macro.Size = new System.Drawing.Size(500, 20);
            this.textBoxMacro4Macro.TabIndex = 26;
            // 
            // dateTimePickerMacro4End
            // 
            this.dateTimePickerMacro4End.CustomFormat = "HH:mm:ss";
            this.dateTimePickerMacro4End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMacro4End.Location = new System.Drawing.Point(148, 98);
            this.dateTimePickerMacro4End.Name = "dateTimePickerMacro4End";
            this.dateTimePickerMacro4End.ShowUpDown = true;
            this.dateTimePickerMacro4End.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerMacro4End.TabIndex = 25;
            // 
            // dateTimePickerMacro4Start
            // 
            this.dateTimePickerMacro4Start.CustomFormat = "HH:mm:ss";
            this.dateTimePickerMacro4Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMacro4Start.Location = new System.Drawing.Point(74, 98);
            this.dateTimePickerMacro4Start.Name = "dateTimePickerMacro4Start";
            this.dateTimePickerMacro4Start.ShowUpDown = true;
            this.dateTimePickerMacro4Start.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerMacro4Start.TabIndex = 24;
            // 
            // labelMacro3
            // 
            this.labelMacro3.AutoSize = true;
            this.labelMacro3.Location = new System.Drawing.Point(6, 75);
            this.labelMacro3.Name = "labelMacro3";
            this.labelMacro3.Size = new System.Drawing.Size(49, 13);
            this.labelMacro3.TabIndex = 19;
            this.labelMacro3.Text = "Macro 3:";
            // 
            // labelMacro2
            // 
            this.labelMacro2.AutoSize = true;
            this.labelMacro2.Location = new System.Drawing.Point(6, 49);
            this.labelMacro2.Name = "labelMacro2";
            this.labelMacro2.Size = new System.Drawing.Size(49, 13);
            this.labelMacro2.TabIndex = 15;
            this.labelMacro2.Text = "Macro 2:";
            // 
            // textBoxMacro3Macro
            // 
            this.textBoxMacro3Macro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMacro3Macro.Location = new System.Drawing.Point(222, 72);
            this.textBoxMacro3Macro.MaxLength = 50;
            this.textBoxMacro3Macro.Name = "textBoxMacro3Macro";
            this.textBoxMacro3Macro.Size = new System.Drawing.Size(500, 20);
            this.textBoxMacro3Macro.TabIndex = 22;
            // 
            // dateTimePickerMacro3End
            // 
            this.dateTimePickerMacro3End.CustomFormat = "HH:mm:ss";
            this.dateTimePickerMacro3End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMacro3End.Location = new System.Drawing.Point(148, 72);
            this.dateTimePickerMacro3End.Name = "dateTimePickerMacro3End";
            this.dateTimePickerMacro3End.ShowUpDown = true;
            this.dateTimePickerMacro3End.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerMacro3End.TabIndex = 21;
            // 
            // dateTimePickerMacro3Start
            // 
            this.dateTimePickerMacro3Start.CustomFormat = "HH:mm:ss";
            this.dateTimePickerMacro3Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMacro3Start.Location = new System.Drawing.Point(74, 72);
            this.dateTimePickerMacro3Start.Name = "dateTimePickerMacro3Start";
            this.dateTimePickerMacro3Start.ShowUpDown = true;
            this.dateTimePickerMacro3Start.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerMacro3Start.TabIndex = 20;
            // 
            // textBoxMacro2Macro
            // 
            this.textBoxMacro2Macro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMacro2Macro.Location = new System.Drawing.Point(222, 46);
            this.textBoxMacro2Macro.MaxLength = 50;
            this.textBoxMacro2Macro.Name = "textBoxMacro2Macro";
            this.textBoxMacro2Macro.Size = new System.Drawing.Size(500, 20);
            this.textBoxMacro2Macro.TabIndex = 18;
            // 
            // dateTimePickerMacro2End
            // 
            this.dateTimePickerMacro2End.CustomFormat = "HH:mm:ss";
            this.dateTimePickerMacro2End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMacro2End.Location = new System.Drawing.Point(148, 46);
            this.dateTimePickerMacro2End.Name = "dateTimePickerMacro2End";
            this.dateTimePickerMacro2End.ShowUpDown = true;
            this.dateTimePickerMacro2End.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerMacro2End.TabIndex = 17;
            // 
            // dateTimePickerMacro2Start
            // 
            this.dateTimePickerMacro2Start.CustomFormat = "HH:mm:ss";
            this.dateTimePickerMacro2Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMacro2Start.Location = new System.Drawing.Point(74, 46);
            this.dateTimePickerMacro2Start.Name = "dateTimePickerMacro2Start";
            this.dateTimePickerMacro2Start.ShowUpDown = true;
            this.dateTimePickerMacro2Start.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerMacro2Start.TabIndex = 16;
            // 
            // textBoxMacro1Macro
            // 
            this.textBoxMacro1Macro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMacro1Macro.Location = new System.Drawing.Point(222, 20);
            this.textBoxMacro1Macro.MaxLength = 50;
            this.textBoxMacro1Macro.Name = "textBoxMacro1Macro";
            this.textBoxMacro1Macro.Size = new System.Drawing.Size(500, 20);
            this.textBoxMacro1Macro.TabIndex = 14;
            // 
            // dateTimePickerMacro1End
            // 
            this.dateTimePickerMacro1End.CustomFormat = "HH:mm:ss";
            this.dateTimePickerMacro1End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMacro1End.Location = new System.Drawing.Point(148, 20);
            this.dateTimePickerMacro1End.Name = "dateTimePickerMacro1End";
            this.dateTimePickerMacro1End.ShowUpDown = true;
            this.dateTimePickerMacro1End.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerMacro1End.TabIndex = 13;
            // 
            // labelMacro1
            // 
            this.labelMacro1.AutoSize = true;
            this.labelMacro1.Location = new System.Drawing.Point(6, 23);
            this.labelMacro1.Name = "labelMacro1";
            this.labelMacro1.Size = new System.Drawing.Size(49, 13);
            this.labelMacro1.TabIndex = 11;
            this.labelMacro1.Text = "Macro 1:";
            // 
            // dateTimePickerMacro1Start
            // 
            this.dateTimePickerMacro1Start.CustomFormat = "HH:mm:ss";
            this.dateTimePickerMacro1Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMacro1Start.Location = new System.Drawing.Point(74, 20);
            this.dateTimePickerMacro1Start.Name = "dateTimePickerMacro1Start";
            this.dateTimePickerMacro1Start.ShowUpDown = true;
            this.dateTimePickerMacro1Start.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerMacro1Start.TabIndex = 12;
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
            this.labelHelp.Size = new System.Drawing.Size(752, 17);
            this.labelHelp.TabIndex = 0;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNotes.Location = new System.Drawing.Point(9, 287);
            this.textBoxNotes.MaxLength = 500;
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNotes.Size = new System.Drawing.Size(735, 118);
            this.textBoxNotes.TabIndex = 28;
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(9, 271);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(38, 13);
            this.labelNotes.TabIndex = 27;
            this.labelNotes.Text = "Notes:";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(268, 61);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(63, 13);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(271, 81);
            this.textBoxDescription.MaxLength = 100;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(470, 20);
            this.textBoxDescription.TabIndex = 7;
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
            this.Controls.Add(this.groupBoxTimeRange);
            this.Controls.Add(this.textBoxDateTimeFormatValue);
            this.Controls.Add(this.labelDateTimeFormatValue);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.labelTagType);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelEditorName);
            this.Controls.Add(this.textBoxName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(772, 493);
            this.Name = "FormTag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormMacroTag_Load);
            this.groupBoxTimeRange.ResumeLayout(false);
            this.groupBoxTimeRange.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEditorName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelTagType;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelDateTimeFormatValue;
        private System.Windows.Forms.TextBox textBoxDateTimeFormatValue;
        private System.Windows.Forms.GroupBox groupBoxTimeRange;
        private System.Windows.Forms.Label labelMacro3;
        private System.Windows.Forms.Label labelMacro2;
        private System.Windows.Forms.TextBox textBoxMacro3Macro;
        private System.Windows.Forms.DateTimePicker dateTimePickerMacro3End;
        private System.Windows.Forms.DateTimePicker dateTimePickerMacro3Start;
        private System.Windows.Forms.TextBox textBoxMacro2Macro;
        private System.Windows.Forms.DateTimePicker dateTimePickerMacro2End;
        private System.Windows.Forms.DateTimePicker dateTimePickerMacro2Start;
        private System.Windows.Forms.TextBox textBoxMacro1Macro;
        private System.Windows.Forms.DateTimePicker dateTimePickerMacro1End;
        private System.Windows.Forms.Label labelMacro1;
        private System.Windows.Forms.DateTimePicker dateTimePickerMacro1Start;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelMacro4;
        private System.Windows.Forms.TextBox textBoxMacro4Macro;
        private System.Windows.Forms.DateTimePicker dateTimePickerMacro4End;
        private System.Windows.Forms.DateTimePicker dateTimePickerMacro4Start;
    }
}