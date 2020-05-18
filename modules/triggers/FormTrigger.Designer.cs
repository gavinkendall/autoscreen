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
            this.listBoxEditor = new System.Windows.Forms.ListBox();
            this.labelEditor = new System.Windows.Forms.Label();
            this.dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
            this.listBoxCondition = new System.Windows.Forms.ListBox();
            this.listBoxAction = new System.Windows.Forms.ListBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.labelTime = new System.Windows.Forms.Label();
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
            this.buttonOK.TabIndex = 14;
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
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelTriggerCondition
            // 
            this.labelTriggerCondition.AutoSize = true;
            this.labelTriggerCondition.Location = new System.Drawing.Point(9, 90);
            this.labelTriggerCondition.Name = "labelTriggerCondition";
            this.labelTriggerCondition.Size = new System.Drawing.Size(54, 13);
            this.labelTriggerCondition.TabIndex = 8;
            this.labelTriggerCondition.Text = "Condition:";
            // 
            // labelTriggerAction
            // 
            this.labelTriggerAction.AutoSize = true;
            this.labelTriggerAction.Location = new System.Drawing.Point(259, 90);
            this.labelTriggerAction.Name = "labelTriggerAction";
            this.labelTriggerAction.Size = new System.Drawing.Size(40, 13);
            this.labelTriggerAction.TabIndex = 9;
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
            // listBoxEditor
            // 
            this.listBoxEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxEditor.Enabled = false;
            this.listBoxEditor.FormattingEnabled = true;
            this.listBoxEditor.Location = new System.Drawing.Point(512, 108);
            this.listBoxEditor.Name = "listBoxEditor";
            this.listBoxEditor.Size = new System.Drawing.Size(235, 290);
            this.listBoxEditor.TabIndex = 13;
            // 
            // labelEditor
            // 
            this.labelEditor.AutoSize = true;
            this.labelEditor.Location = new System.Drawing.Point(509, 90);
            this.labelEditor.Name = "labelEditor";
            this.labelEditor.Size = new System.Drawing.Size(37, 13);
            this.labelEditor.TabIndex = 10;
            this.labelEditor.Text = "Editor:";
            // 
            // dateTimePickerTime
            // 
            this.dateTimePickerTime.CustomFormat = "HH:mm:ss";
            this.dateTimePickerTime.Enabled = false;
            this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTime.Location = new System.Drawing.Point(179, 58);
            this.dateTimePickerTime.Name = "dateTimePickerTime";
            this.dateTimePickerTime.ShowUpDown = true;
            this.dateTimePickerTime.Size = new System.Drawing.Size(68, 20);
            this.dateTimePickerTime.TabIndex = 7;
            // 
            // listBoxCondition
            // 
            this.listBoxCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxCondition.FormattingEnabled = true;
            this.listBoxCondition.Location = new System.Drawing.Point(12, 108);
            this.listBoxCondition.Name = "listBoxCondition";
            this.listBoxCondition.Size = new System.Drawing.Size(235, 290);
            this.listBoxCondition.TabIndex = 11;
            this.listBoxCondition.SelectedIndexChanged += new System.EventHandler(this.listBoxCondition_SelectedIndexChanged);
            // 
            // listBoxAction
            // 
            this.listBoxAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxAction.FormattingEnabled = true;
            this.listBoxAction.Location = new System.Drawing.Point(262, 108);
            this.listBoxAction.Name = "listBoxAction";
            this.listBoxAction.Size = new System.Drawing.Size(235, 290);
            this.listBoxAction.TabIndex = 12;
            this.listBoxAction.SelectedIndexChanged += new System.EventHandler(this.listBoxAction_SelectedIndexChanged);
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(9, 63);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(33, 13);
            this.labelDate.TabIndex = 4;
            this.labelDate.Text = "Date:";
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dateTimePickerDate.Enabled = false;
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Location = new System.Drawing.Point(56, 58);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(79, 20);
            this.dateTimePickerDate.TabIndex = 5;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(140, 63);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(33, 13);
            this.labelTime.TabIndex = 6;
            this.labelTime.Text = "Time:";
            // 
            // FormTrigger
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(756, 454);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.listBoxAction);
            this.Controls.Add(this.listBoxCondition);
            this.Controls.Add(this.dateTimePickerTime);
            this.Controls.Add(this.labelEditor);
            this.Controls.Add(this.listBoxEditor);
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
        private System.Windows.Forms.ListBox listBoxEditor;
        private System.Windows.Forms.Label labelEditor;
        private System.Windows.Forms.DateTimePicker dateTimePickerTime;
        private System.Windows.Forms.ListBox listBoxCondition;
        private System.Windows.Forms.ListBox listBoxAction;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label labelTime;
    }
}