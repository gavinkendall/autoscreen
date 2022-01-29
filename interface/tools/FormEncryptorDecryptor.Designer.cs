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
            this.labelFilter = new System.Windows.Forms.Label();
            this.comboBoxFilterValue = new System.Windows.Forms.ComboBox();
            this.comboBoxFilterType = new System.Windows.Forms.ComboBox();
            this.dateTimePickerScreenshotsEndTimeRange = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerScreenshotsStartTimeRange = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewScreenshots = new System.Windows.Forms.DataGridView();
            this.labelScreenshotsHelp = new System.Windows.Forms.Label();
            this.buttonDecryptScreenshots = new System.Windows.Forms.Button();
            this.buttonEncryptScreenshots = new System.Windows.Forms.Button();
            this.dateTimePickerScreenshotsEndDateRange = new System.Windows.Forms.DateTimePicker();
            this.labelScreenshotsEndDateTimeRange = new System.Windows.Forms.Label();
            this.labelScreenshotsStartDateTimeRange = new System.Windows.Forms.Label();
            this.dateTimePickerScreenshotsStartDateRange = new System.Windows.Forms.DateTimePicker();
            this.tabPageFile = new System.Windows.Forms.TabPage();
            this.buttonDecryptFile = new System.Windows.Forms.Button();
            this.buttonEncryptFile = new System.Windows.Forms.Button();
            this.textBoxFileKey = new System.Windows.Forms.TextBox();
            this.labelKey = new System.Windows.Forms.Label();
            this.labelFilepath = new System.Windows.Forms.Label();
            this.textBoxFilepath = new System.Windows.Forms.TextBox();
            this.labelFileHelp = new System.Windows.Forms.Label();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.labelText = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.textBoxTextKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDecryptText = new System.Windows.Forms.Button();
            this.buttonEncryptText = new System.Windows.Forms.Button();
            this.labelTextHelp = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl.SuspendLayout();
            this.tabPageScreenshots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScreenshots)).BeginInit();
            this.tabPageFile.SuspendLayout();
            this.tabPageText.SuspendLayout();
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
            this.tabControl.Size = new System.Drawing.Size(853, 452);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
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
            this.tabPageScreenshots.Controls.Add(this.buttonDecryptScreenshots);
            this.tabPageScreenshots.Controls.Add(this.buttonEncryptScreenshots);
            this.tabPageScreenshots.Controls.Add(this.dateTimePickerScreenshotsEndDateRange);
            this.tabPageScreenshots.Controls.Add(this.labelScreenshotsEndDateTimeRange);
            this.tabPageScreenshots.Controls.Add(this.labelScreenshotsStartDateTimeRange);
            this.tabPageScreenshots.Controls.Add(this.dateTimePickerScreenshotsStartDateRange);
            this.tabPageScreenshots.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreenshots.Name = "tabPageScreenshots";
            this.tabPageScreenshots.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScreenshots.Size = new System.Drawing.Size(845, 426);
            this.tabPageScreenshots.TabIndex = 0;
            this.tabPageScreenshots.Text = "Screenshots";
            this.tabPageScreenshots.UseVisualStyleBackColor = true;
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
            this.dataGridViewScreenshots.Size = new System.Drawing.Size(826, 322);
            this.dataGridViewScreenshots.TabIndex = 35;
            this.dataGridViewScreenshots.TabStop = false;
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
            this.labelScreenshotsHelp.Text = "Specify a date/time range and select a filter then click either the Encrypt Scree" +
    "nshots or Decrypt Screenshots button.";
            // 
            // buttonDecryptScreenshots
            // 
            this.buttonDecryptScreenshots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDecryptScreenshots.Location = new System.Drawing.Point(167, 397);
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
            this.buttonEncryptScreenshots.Location = new System.Drawing.Point(11, 397);
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
            this.tabPageFile.Controls.Add(this.buttonDecryptFile);
            this.tabPageFile.Controls.Add(this.buttonEncryptFile);
            this.tabPageFile.Controls.Add(this.textBoxFileKey);
            this.tabPageFile.Controls.Add(this.labelKey);
            this.tabPageFile.Controls.Add(this.labelFilepath);
            this.tabPageFile.Controls.Add(this.textBoxFilepath);
            this.tabPageFile.Controls.Add(this.labelFileHelp);
            this.tabPageFile.Location = new System.Drawing.Point(4, 22);
            this.tabPageFile.Name = "tabPageFile";
            this.tabPageFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFile.Size = new System.Drawing.Size(845, 426);
            this.tabPageFile.TabIndex = 1;
            this.tabPageFile.Text = "File";
            this.tabPageFile.UseVisualStyleBackColor = true;
            // 
            // buttonDecryptFile
            // 
            this.buttonDecryptFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDecryptFile.Location = new System.Drawing.Point(167, 397);
            this.buttonDecryptFile.Name = "buttonDecryptFile";
            this.buttonDecryptFile.Size = new System.Drawing.Size(150, 23);
            this.buttonDecryptFile.TabIndex = 41;
            this.buttonDecryptFile.TabStop = false;
            this.buttonDecryptFile.Text = "Decrypt File";
            this.buttonDecryptFile.UseVisualStyleBackColor = true;
            this.buttonDecryptFile.Click += new System.EventHandler(this.buttonDecryptFile_Click);
            // 
            // buttonEncryptFile
            // 
            this.buttonEncryptFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEncryptFile.Location = new System.Drawing.Point(11, 397);
            this.buttonEncryptFile.Name = "buttonEncryptFile";
            this.buttonEncryptFile.Size = new System.Drawing.Size(150, 23);
            this.buttonEncryptFile.TabIndex = 40;
            this.buttonEncryptFile.TabStop = false;
            this.buttonEncryptFile.Text = "Encrypt File";
            this.buttonEncryptFile.UseVisualStyleBackColor = true;
            this.buttonEncryptFile.Click += new System.EventHandler(this.buttonEncryptFile_Click);
            // 
            // textBoxFileKey
            // 
            this.textBoxFileKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFileKey.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxFileKey.Location = new System.Drawing.Point(59, 23);
            this.textBoxFileKey.Name = "textBoxFileKey";
            this.textBoxFileKey.Size = new System.Drawing.Size(778, 25);
            this.textBoxFileKey.TabIndex = 39;
            this.textBoxFileKey.TabStop = false;
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Location = new System.Drawing.Point(6, 29);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(28, 13);
            this.labelKey.TabIndex = 38;
            this.labelKey.Text = "Key:";
            // 
            // labelFilepath
            // 
            this.labelFilepath.AutoSize = true;
            this.labelFilepath.Location = new System.Drawing.Point(6, 60);
            this.labelFilepath.Name = "labelFilepath";
            this.labelFilepath.Size = new System.Drawing.Size(47, 13);
            this.labelFilepath.TabIndex = 37;
            this.labelFilepath.Text = "Filepath:";
            // 
            // textBoxFilepath
            // 
            this.textBoxFilepath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilepath.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxFilepath.Location = new System.Drawing.Point(59, 54);
            this.textBoxFilepath.Name = "textBoxFilepath";
            this.textBoxFilepath.Size = new System.Drawing.Size(778, 25);
            this.textBoxFilepath.TabIndex = 36;
            this.textBoxFilepath.TabStop = false;
            // 
            // labelFileHelp
            // 
            this.labelFileHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFileHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelFileHelp.Location = new System.Drawing.Point(6, 3);
            this.labelFileHelp.Name = "labelFileHelp";
            this.labelFileHelp.Size = new System.Drawing.Size(833, 17);
            this.labelFileHelp.TabIndex = 35;
            this.labelFileHelp.Text = "Specify a date/time range and select a filter then click either the Encrypt Scree" +
    "nshots or Decrypt Screenshots button.";
            // 
            // tabPageText
            // 
            this.tabPageText.Controls.Add(this.labelText);
            this.tabPageText.Controls.Add(this.textBoxText);
            this.tabPageText.Controls.Add(this.textBoxTextKey);
            this.tabPageText.Controls.Add(this.label1);
            this.tabPageText.Controls.Add(this.buttonDecryptText);
            this.tabPageText.Controls.Add(this.buttonEncryptText);
            this.tabPageText.Controls.Add(this.labelTextHelp);
            this.tabPageText.Location = new System.Drawing.Point(4, 22);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Size = new System.Drawing.Size(845, 426);
            this.tabPageText.TabIndex = 2;
            this.tabPageText.Text = "Text";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(8, 58);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(31, 13);
            this.labelText.TabIndex = 46;
            this.labelText.Text = "Text:";
            // 
            // textBoxText
            // 
            this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxText.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxText.Location = new System.Drawing.Point(11, 74);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxText.Size = new System.Drawing.Size(826, 317);
            this.textBoxText.TabIndex = 45;
            this.textBoxText.TabStop = false;
            this.textBoxText.WordWrap = false;
            // 
            // textBoxTextKey
            // 
            this.textBoxTextKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTextKey.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxTextKey.Location = new System.Drawing.Point(59, 23);
            this.textBoxTextKey.Name = "textBoxTextKey";
            this.textBoxTextKey.Size = new System.Drawing.Size(778, 25);
            this.textBoxTextKey.TabIndex = 44;
            this.textBoxTextKey.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Key:";
            // 
            // buttonDecryptText
            // 
            this.buttonDecryptText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDecryptText.Location = new System.Drawing.Point(167, 397);
            this.buttonDecryptText.Name = "buttonDecryptText";
            this.buttonDecryptText.Size = new System.Drawing.Size(150, 23);
            this.buttonDecryptText.TabIndex = 42;
            this.buttonDecryptText.TabStop = false;
            this.buttonDecryptText.Text = "Decrypt Text";
            this.buttonDecryptText.UseVisualStyleBackColor = true;
            this.buttonDecryptText.Click += new System.EventHandler(this.buttonDecryptText_Click);
            // 
            // buttonEncryptText
            // 
            this.buttonEncryptText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEncryptText.Location = new System.Drawing.Point(11, 397);
            this.buttonEncryptText.Name = "buttonEncryptText";
            this.buttonEncryptText.Size = new System.Drawing.Size(150, 23);
            this.buttonEncryptText.TabIndex = 41;
            this.buttonEncryptText.TabStop = false;
            this.buttonEncryptText.Text = "Encrypt Text";
            this.buttonEncryptText.UseVisualStyleBackColor = true;
            this.buttonEncryptText.Click += new System.EventHandler(this.buttonEncryptText_Click);
            // 
            // labelTextHelp
            // 
            this.labelTextHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTextHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelTextHelp.Location = new System.Drawing.Point(6, 3);
            this.labelTextHelp.Name = "labelTextHelp";
            this.labelTextHelp.Size = new System.Drawing.Size(833, 17);
            this.labelTextHelp.TabIndex = 35;
            this.labelTextHelp.Text = "Specify a date/time range and select a filter then click either the Encrypt Scree" +
    "nshots or Decrypt Screenshots button.";
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 452);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(853, 22);
            this.statusStrip.Stretch = false;
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // FormEncryptorDecryptor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 474);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
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
            this.tabPageFile.ResumeLayout(false);
            this.tabPageFile.PerformLayout();
            this.tabPageText.ResumeLayout(false);
            this.tabPageText.PerformLayout();
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
        private System.Windows.Forms.Label labelFileHelp;
        private System.Windows.Forms.Label labelTextHelp;
        private System.Windows.Forms.TextBox textBoxFilepath;
        private System.Windows.Forms.Label labelFilepath;
        private System.Windows.Forms.TextBox textBoxFileKey;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Button buttonDecryptFile;
        private System.Windows.Forms.Button buttonEncryptFile;
        private System.Windows.Forms.Button buttonDecryptText;
        private System.Windows.Forms.Button buttonEncryptText;
        private System.Windows.Forms.TextBox textBoxTextKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.TextBox textBoxText;
    }
}