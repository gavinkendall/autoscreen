namespace AutoScreenCapture
{
    partial class FormEncryptorDecryptor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEncryptorDecryptor));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageScreenshots = new System.Windows.Forms.TabPage();
            this.dateTimePickerScreenshotsEndTimeRange = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerScreenshotsStartTimeRange = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewScreenshots = new System.Windows.Forms.DataGridView();
            this.labelScreenshotsHelp = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonDecryptScreenshots = new System.Windows.Forms.Button();
            this.buttonEncryptScreenshots = new System.Windows.Forms.Button();
            this.dateTimePickerScreenshotsEndDateRange = new System.Windows.Forms.DateTimePicker();
            this.labelScreenshotsEndDateTimeRange = new System.Windows.Forms.Label();
            this.labelScreenshotsStartDateTimeRange = new System.Windows.Forms.Label();
            this.dateTimePickerScreenshotsStartDateRange = new System.Windows.Forms.DateTimePicker();
            this.tabPageFile = new System.Windows.Forms.TabPage();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.comboBoxFilterValue = new System.Windows.Forms.ComboBox();
            this.labelFilter = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageScreenshots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScreenshots)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageScreenshots);
            this.tabControl.Controls.Add(this.tabPageFile);
            this.tabControl.Controls.Add(this.tabPageText);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(853, 474);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageScreenshots
            // 
            this.tabPageScreenshots.Controls.Add(this.labelFilter);
            this.tabPageScreenshots.Controls.Add(this.comboBoxFilterValue);
            this.tabPageScreenshots.Controls.Add(this.comboBoxFilterType);
            this.tabPageScreenshots.Controls.Add(this.dateTimePickerScreenshotsEndTimeRange);
            this.tabPageScreenshots.Controls.Add(this.dateTimePickerScreenshotsStartTimeRange);
            this.tabPageScreenshots.Controls.Add(this.dataGridViewScreenshots);
            this.tabPageScreenshots.Controls.Add(this.labelScreenshotsHelp);
            this.tabPageScreenshots.Controls.Add(this.statusStrip);
            this.tabPageScreenshots.Controls.Add(this.buttonDecryptScreenshots);
            this.tabPageScreenshots.Controls.Add(this.buttonEncryptScreenshots);
            this.tabPageScreenshots.Controls.Add(this.dateTimePickerScreenshotsEndDateRange);
            this.tabPageScreenshots.Controls.Add(this.labelScreenshotsEndDateTimeRange);
            this.tabPageScreenshots.Controls.Add(this.labelScreenshotsStartDateTimeRange);
            this.tabPageScreenshots.Controls.Add(this.dateTimePickerScreenshotsStartDateRange);
            this.tabPageScreenshots.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreenshots.Name = "tabPageScreenshots";
            this.tabPageScreenshots.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScreenshots.Size = new System.Drawing.Size(845, 448);
            this.tabPageScreenshots.TabIndex = 0;
            this.tabPageScreenshots.Text = "Screenshots";
            this.tabPageScreenshots.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerScreenshotsEndTimeRange
            // 
            this.dateTimePickerScreenshotsEndTimeRange.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerScreenshotsEndTimeRange.Location = new System.Drawing.Point(539, 43);
            this.dateTimePickerScreenshotsEndTimeRange.Name = "dateTimePickerScreenshotsEndTimeRange";
            this.dateTimePickerScreenshotsEndTimeRange.ShowUpDown = true;
            this.dateTimePickerScreenshotsEndTimeRange.Size = new System.Drawing.Size(92, 20);
            this.dateTimePickerScreenshotsEndTimeRange.TabIndex = 37;
            this.dateTimePickerScreenshotsEndTimeRange.TabStop = false;
            this.dateTimePickerScreenshotsEndTimeRange.ValueChanged += new System.EventHandler(this.dateTimePickerScreenshots_ValueChanged);
            // 
            // dateTimePickerScreenshotsStartTimeRange
            // 
            this.dateTimePickerScreenshotsStartTimeRange.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerScreenshotsStartTimeRange.Location = new System.Drawing.Point(217, 43);
            this.dateTimePickerScreenshotsStartTimeRange.Name = "dateTimePickerScreenshotsStartTimeRange";
            this.dateTimePickerScreenshotsStartTimeRange.ShowUpDown = true;
            this.dateTimePickerScreenshotsStartTimeRange.Size = new System.Drawing.Size(92, 20);
            this.dateTimePickerScreenshotsStartTimeRange.TabIndex = 36;
            this.dateTimePickerScreenshotsStartTimeRange.TabStop = false;
            this.dateTimePickerScreenshotsStartTimeRange.ValueChanged += new System.EventHandler(this.dateTimePickerScreenshots_ValueChanged);
            // 
            // dataGridViewScreenshots
            // 
            this.dataGridViewScreenshots.AllowUserToAddRows = false;
            this.dataGridViewScreenshots.AllowUserToDeleteRows = false;
            this.dataGridViewScreenshots.AllowUserToOrderColumns = true;
            this.dataGridViewScreenshots.AllowUserToResizeRows = false;
            this.dataGridViewScreenshots.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewScreenshots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewScreenshots.Location = new System.Drawing.Point(11, 69);
            this.dataGridViewScreenshots.Name = "dataGridViewScreenshots";
            this.dataGridViewScreenshots.ReadOnly = true;
            this.dataGridViewScreenshots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewScreenshots.Size = new System.Drawing.Size(826, 318);
            this.dataGridViewScreenshots.TabIndex = 35;
            // 
            // labelScreenshotsHelp
            // 
            this.labelScreenshotsHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScreenshotsHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelScreenshotsHelp.Location = new System.Drawing.Point(6, 3);
            this.labelScreenshotsHelp.Name = "labelScreenshotsHelp";
            this.labelScreenshotsHelp.Size = new System.Drawing.Size(833, 17);
            this.labelScreenshotsHelp.TabIndex = 34;
            this.labelScreenshotsHelp.Text = "Specify a date/time range and then click either the Encrypt Screenshots or Decryp" +
    "t Screenshots button.";
            // 
            // statusStrip
            // 
            this.statusStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip.AutoSize = false;
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(3, 423);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(839, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.Stretch = false;
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // buttonDecryptScreenshots
            // 
            this.buttonDecryptScreenshots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDecryptScreenshots.Location = new System.Drawing.Point(167, 393);
            this.buttonDecryptScreenshots.Name = "buttonDecryptScreenshots";
            this.buttonDecryptScreenshots.Size = new System.Drawing.Size(150, 23);
            this.buttonDecryptScreenshots.TabIndex = 5;
            this.buttonDecryptScreenshots.TabStop = false;
            this.buttonDecryptScreenshots.Text = "Decrypt Screenshots";
            this.buttonDecryptScreenshots.UseVisualStyleBackColor = true;
            this.buttonDecryptScreenshots.Click += new System.EventHandler(this.buttonDecryptScreenshots_Click);
            // 
            // buttonEncryptScreenshots
            // 
            this.buttonEncryptScreenshots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEncryptScreenshots.Location = new System.Drawing.Point(11, 393);
            this.buttonEncryptScreenshots.Name = "buttonEncryptScreenshots";
            this.buttonEncryptScreenshots.Size = new System.Drawing.Size(150, 23);
            this.buttonEncryptScreenshots.TabIndex = 4;
            this.buttonEncryptScreenshots.TabStop = false;
            this.buttonEncryptScreenshots.Text = "Encrypt Screenshots";
            this.buttonEncryptScreenshots.UseVisualStyleBackColor = true;
            this.buttonEncryptScreenshots.Click += new System.EventHandler(this.buttonEncryptScreenshots_Click);
            // 
            // dateTimePickerScreenshotsEndDateRange
            // 
            this.dateTimePickerScreenshotsEndDateRange.Location = new System.Drawing.Point(333, 43);
            this.dateTimePickerScreenshotsEndDateRange.Name = "dateTimePickerScreenshotsEndDateRange";
            this.dateTimePickerScreenshotsEndDateRange.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerScreenshotsEndDateRange.TabIndex = 3;
            this.dateTimePickerScreenshotsEndDateRange.TabStop = false;
            this.dateTimePickerScreenshotsEndDateRange.ValueChanged += new System.EventHandler(this.dateTimePickerScreenshots_ValueChanged);
            // 
            // labelScreenshotsEndDateTimeRange
            // 
            this.labelScreenshotsEndDateTimeRange.AutoSize = true;
            this.labelScreenshotsEndDateTimeRange.Location = new System.Drawing.Point(330, 27);
            this.labelScreenshotsEndDateTimeRange.Name = "labelScreenshotsEndDateTimeRange";
            this.labelScreenshotsEndDateTimeRange.Size = new System.Drawing.Size(118, 13);
            this.labelScreenshotsEndDateTimeRange.TabIndex = 2;
            this.labelScreenshotsEndDateTimeRange.Text = "End Date/Time Range:";
            // 
            // labelScreenshotsStartDateTimeRange
            // 
            this.labelScreenshotsStartDateTimeRange.AutoSize = true;
            this.labelScreenshotsStartDateTimeRange.Location = new System.Drawing.Point(8, 27);
            this.labelScreenshotsStartDateTimeRange.Name = "labelScreenshotsStartDateTimeRange";
            this.labelScreenshotsStartDateTimeRange.Size = new System.Drawing.Size(121, 13);
            this.labelScreenshotsStartDateTimeRange.TabIndex = 1;
            this.labelScreenshotsStartDateTimeRange.Text = "Start Date/Time Range:";
            // 
            // dateTimePickerScreenshotsStartDateRange
            // 
            this.dateTimePickerScreenshotsStartDateRange.Location = new System.Drawing.Point(11, 43);
            this.dateTimePickerScreenshotsStartDateRange.Name = "dateTimePickerScreenshotsStartDateRange";
            this.dateTimePickerScreenshotsStartDateRange.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerScreenshotsStartDateRange.TabIndex = 0;
            this.dateTimePickerScreenshotsStartDateRange.TabStop = false;
            this.dateTimePickerScreenshotsStartDateRange.ValueChanged += new System.EventHandler(this.dateTimePickerScreenshots_ValueChanged);
            // 
            // tabPageFile
            // 
            this.tabPageFile.Location = new System.Drawing.Point(4, 22);
            this.tabPageFile.Name = "tabPageFile";
            this.tabPageFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFile.Size = new System.Drawing.Size(637, 321);
            this.tabPageFile.TabIndex = 1;
            this.tabPageFile.Text = "File";
            this.tabPageFile.UseVisualStyleBackColor = true;
            // 
            // tabPageText
            // 
            this.tabPageText.Location = new System.Drawing.Point(4, 22);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Size = new System.Drawing.Size(637, 321);
            this.tabPageText.TabIndex = 2;
            this.tabPageText.Text = "Text";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // comboBoxFilterType
            // 
            this.comboBoxFilterType.DropDownHeight = 100;
            this.comboBoxFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterType.DropDownWidth = 100;
            this.comboBoxFilterType.FormattingEnabled = true;
            this.comboBoxFilterType.IntegralHeight = false;
            this.comboBoxFilterType.Items.AddRange(new object[] {
            "",
            "Image Format",
            "Label",
            "Process Name",
            "Window Title"});
            this.comboBoxFilterType.Location = new System.Drawing.Point(655, 43);
            this.comboBoxFilterType.Name = "comboBoxFilterType";
            this.comboBoxFilterType.Size = new System.Drawing.Size(88, 21);
            this.comboBoxFilterType.TabIndex = 38;
            this.comboBoxFilterType.TabStop = false;
            this.comboBoxFilterType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterType_SelectedIndexChanged);
            // 
            // comboBoxFilterValue
            // 
            this.comboBoxFilterValue.DropDownHeight = 300;
            this.comboBoxFilterValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterValue.DropDownWidth = 700;
            this.comboBoxFilterValue.Enabled = false;
            this.comboBoxFilterValue.FormattingEnabled = true;
            this.comboBoxFilterValue.IntegralHeight = false;
            this.comboBoxFilterValue.Location = new System.Drawing.Point(749, 43);
            this.comboBoxFilterValue.Name = "comboBoxFilterValue";
            this.comboBoxFilterValue.Size = new System.Drawing.Size(88, 21);
            this.comboBoxFilterValue.TabIndex = 39;
            this.comboBoxFilterValue.TabStop = false;
            this.comboBoxFilterValue.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterValue_SelectedIndexChanged);
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(652, 27);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(32, 13);
            this.labelFilter.TabIndex = 40;
            this.labelFilter.Text = "Filter:";
            // 
            // FormEncryptorDecryptor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 474);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(869, 513);
            this.Name = "FormEncryptorDecryptor";
            this.Text = "Auto Screen Capture - Encryptor / Decryptor";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEncryptorDecryptor_FormClosing);
            this.Load += new System.EventHandler(this.FormEncryptorDecryptor_Load);
            this.Shown += new System.EventHandler(this.FormEncryptorDecryptor_Shown);
            this.tabControl.ResumeLayout(false);
            this.tabPageScreenshots.ResumeLayout(false);
            this.tabPageScreenshots.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScreenshots)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageScreenshots;
        private System.Windows.Forms.TabPage tabPageFile;
        private System.Windows.Forms.TabPage tabPageText;
        private System.Windows.Forms.DateTimePicker dateTimePickerScreenshotsEndDateRange;
        private System.Windows.Forms.Label labelScreenshotsEndDateTimeRange;
        private System.Windows.Forms.Label labelScreenshotsStartDateTimeRange;
        private System.Windows.Forms.DateTimePicker dateTimePickerScreenshotsStartDateRange;
        private System.Windows.Forms.Button buttonDecryptScreenshots;
        private System.Windows.Forms.Button buttonEncryptScreenshots;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label labelScreenshotsHelp;
        private System.Windows.Forms.DataGridView dataGridViewScreenshots;
        private System.Windows.Forms.DateTimePicker dateTimePickerScreenshotsEndTimeRange;
        private System.Windows.Forms.DateTimePicker dateTimePickerScreenshotsStartTimeRange;
        private System.Windows.Forms.ComboBox comboBoxFilterType;
        private System.Windows.Forms.ComboBox comboBoxFilterValue;
        private System.Windows.Forms.Label labelFilter;
    }
}