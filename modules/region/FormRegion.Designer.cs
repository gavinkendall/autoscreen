namespace AutoScreenCapture
{
    partial class FormRegion
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegion));
            this.labelRegionName = new System.Windows.Forms.Label();
            this.textBoxRegionName = new System.Windows.Forms.TextBox();
            this.labelRegionMacro = new System.Windows.Forms.Label();
            this.textBoxRegionMacro = new System.Windows.Forms.TextBox();
            this.pictureBoxRegionPreview = new System.Windows.Forms.PictureBox();
            this.labelRegionX = new System.Windows.Forms.Label();
            this.labelRegionY = new System.Windows.Forms.Label();
            this.labelRegionWidth = new System.Windows.Forms.Label();
            this.labelRegionHeight = new System.Windows.Forms.Label();
            this.numericUpDownRegionX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRegionY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRegionWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRegionHeight = new System.Windows.Forms.NumericUpDown();
            this.buttonRegionOK = new System.Windows.Forms.Button();
            this.buttonRegionCancel = new System.Windows.Forms.Button();
            this.timerRegionPreview = new System.Windows.Forms.Timer(this.components);
            this.labelRegionFolder = new System.Windows.Forms.Label();
            this.textBoxRegionFolder = new System.Windows.Forms.TextBox();
            this.buttonRegionBrowseFolder = new System.Windows.Forms.Button();
            this.comboBoxRegionFormat = new System.Windows.Forms.ComboBox();
            this.labelRegionFormat = new System.Windows.Forms.Label();
            this.numericUpDownRegionResolutionRatio = new System.Windows.Forms.NumericUpDown();
            this.labelRegionResolutionRatio = new System.Windows.Forms.Label();
            this.numericUpDownRegionJpegQuality = new System.Windows.Forms.NumericUpDown();
            this.labelRegionJpegQuality = new System.Windows.Forms.Label();
            this.checkBoxRegionMouse = new System.Windows.Forms.CheckBox();
            this.groupBoxRegionImage = new System.Windows.Forms.GroupBox();
            this.groupBoxRegionPosition = new System.Windows.Forms.GroupBox();
            this.groupBoxRegionSize = new System.Windows.Forms.GroupBox();
            this.groupBoxRegionPreview = new System.Windows.Forms.GroupBox();
            this.groupBoxRegionScreenTemplate = new System.Windows.Forms.GroupBox();
            this.comboBoxRegionScreenTemplate = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRegionPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionResolutionRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionJpegQuality)).BeginInit();
            this.groupBoxRegionImage.SuspendLayout();
            this.groupBoxRegionPosition.SuspendLayout();
            this.groupBoxRegionSize.SuspendLayout();
            this.groupBoxRegionPreview.SuspendLayout();
            this.groupBoxRegionScreenTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelRegionName
            // 
            this.labelRegionName.AutoSize = true;
            this.labelRegionName.Location = new System.Drawing.Point(9, 9);
            this.labelRegionName.Name = "labelRegionName";
            this.labelRegionName.Size = new System.Drawing.Size(38, 13);
            this.labelRegionName.TabIndex = 0;
            this.labelRegionName.Text = "Name:";
            // 
            // textBoxRegionName
            // 
            this.textBoxRegionName.Location = new System.Drawing.Point(53, 6);
            this.textBoxRegionName.MaxLength = 50;
            this.textBoxRegionName.Name = "textBoxRegionName";
            this.textBoxRegionName.Size = new System.Drawing.Size(318, 20);
            this.textBoxRegionName.TabIndex = 1;
            // 
            // labelRegionMacro
            // 
            this.labelRegionMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRegionMacro.AutoSize = true;
            this.labelRegionMacro.Location = new System.Drawing.Point(223, 516);
            this.labelRegionMacro.Name = "labelRegionMacro";
            this.labelRegionMacro.Size = new System.Drawing.Size(40, 13);
            this.labelRegionMacro.TabIndex = 0;
            this.labelRegionMacro.Text = "Macro:";
            // 
            // textBoxRegionMacro
            // 
            this.textBoxRegionMacro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRegionMacro.Location = new System.Drawing.Point(267, 513);
            this.textBoxRegionMacro.Name = "textBoxRegionMacro";
            this.textBoxRegionMacro.Size = new System.Drawing.Size(505, 20);
            this.textBoxRegionMacro.TabIndex = 17;
            // 
            // pictureBoxRegionPreview
            // 
            this.pictureBoxRegionPreview.BackColor = System.Drawing.Color.Black;
            this.pictureBoxRegionPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxRegionPreview.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxRegionPreview.Name = "pictureBoxRegionPreview";
            this.pictureBoxRegionPreview.Size = new System.Drawing.Size(552, 424);
            this.pictureBoxRegionPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRegionPreview.TabIndex = 5;
            this.pictureBoxRegionPreview.TabStop = false;
            // 
            // labelRegionX
            // 
            this.labelRegionX.AutoSize = true;
            this.labelRegionX.Location = new System.Drawing.Point(21, 22);
            this.labelRegionX.Name = "labelRegionX";
            this.labelRegionX.Size = new System.Drawing.Size(17, 13);
            this.labelRegionX.TabIndex = 0;
            this.labelRegionX.Text = "X:";
            // 
            // labelRegionY
            // 
            this.labelRegionY.AutoSize = true;
            this.labelRegionY.Location = new System.Drawing.Point(125, 22);
            this.labelRegionY.Name = "labelRegionY";
            this.labelRegionY.Size = new System.Drawing.Size(17, 13);
            this.labelRegionY.TabIndex = 0;
            this.labelRegionY.Text = "Y:";
            // 
            // labelRegionWidth
            // 
            this.labelRegionWidth.AutoSize = true;
            this.labelRegionWidth.Location = new System.Drawing.Point(6, 22);
            this.labelRegionWidth.Name = "labelRegionWidth";
            this.labelRegionWidth.Size = new System.Drawing.Size(38, 13);
            this.labelRegionWidth.TabIndex = 0;
            this.labelRegionWidth.Text = "Width:";
            // 
            // labelRegionHeight
            // 
            this.labelRegionHeight.AutoSize = true;
            this.labelRegionHeight.Location = new System.Drawing.Point(101, 22);
            this.labelRegionHeight.Name = "labelRegionHeight";
            this.labelRegionHeight.Size = new System.Drawing.Size(41, 13);
            this.labelRegionHeight.TabIndex = 0;
            this.labelRegionHeight.Text = "Height:";
            // 
            // numericUpDownRegionX
            // 
            this.numericUpDownRegionX.Location = new System.Drawing.Point(44, 20);
            this.numericUpDownRegionX.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownRegionX.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownRegionX.Name = "numericUpDownRegionX";
            this.numericUpDownRegionX.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownRegionX.TabIndex = 3;
            // 
            // numericUpDownRegionY
            // 
            this.numericUpDownRegionY.Location = new System.Drawing.Point(148, 20);
            this.numericUpDownRegionY.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownRegionY.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownRegionY.Name = "numericUpDownRegionY";
            this.numericUpDownRegionY.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownRegionY.TabIndex = 4;
            // 
            // numericUpDownRegionWidth
            // 
            this.numericUpDownRegionWidth.Location = new System.Drawing.Point(44, 20);
            this.numericUpDownRegionWidth.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownRegionWidth.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownRegionWidth.Name = "numericUpDownRegionWidth";
            this.numericUpDownRegionWidth.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownRegionWidth.TabIndex = 6;
            // 
            // numericUpDownRegionHeight
            // 
            this.numericUpDownRegionHeight.Location = new System.Drawing.Point(148, 20);
            this.numericUpDownRegionHeight.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownRegionHeight.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownRegionHeight.Name = "numericUpDownRegionHeight";
            this.numericUpDownRegionHeight.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownRegionHeight.TabIndex = 7;
            // 
            // buttonRegionOK
            // 
            this.buttonRegionOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRegionOK.Location = new System.Drawing.Point(12, 534);
            this.buttonRegionOK.Name = "buttonRegionOK";
            this.buttonRegionOK.Size = new System.Drawing.Size(99, 23);
            this.buttonRegionOK.TabIndex = 18;
            this.buttonRegionOK.Text = "OK";
            this.buttonRegionOK.UseVisualStyleBackColor = true;
            this.buttonRegionOK.Click += new System.EventHandler(this.Click_buttonRegionOK);
            // 
            // buttonRegionCancel
            // 
            this.buttonRegionCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRegionCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonRegionCancel.Location = new System.Drawing.Point(118, 534);
            this.buttonRegionCancel.Name = "buttonRegionCancel";
            this.buttonRegionCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonRegionCancel.TabIndex = 19;
            this.buttonRegionCancel.Text = "Cancel";
            this.buttonRegionCancel.UseVisualStyleBackColor = true;
            this.buttonRegionCancel.Click += new System.EventHandler(this.Click_buttonRegionCancel);
            // 
            // timerRegionPreview
            // 
            this.timerRegionPreview.Enabled = true;
            this.timerRegionPreview.Interval = 500;
            this.timerRegionPreview.Tick += new System.EventHandler(this.Tick_timerRegionPreview);
            // 
            // labelRegionFolder
            // 
            this.labelRegionFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRegionFolder.AutoSize = true;
            this.labelRegionFolder.Location = new System.Drawing.Point(223, 490);
            this.labelRegionFolder.Name = "labelRegionFolder";
            this.labelRegionFolder.Size = new System.Drawing.Size(39, 13);
            this.labelRegionFolder.TabIndex = 0;
            this.labelRegionFolder.Text = "Folder:";
            // 
            // textBoxRegionFolder
            // 
            this.textBoxRegionFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRegionFolder.Location = new System.Drawing.Point(267, 487);
            this.textBoxRegionFolder.Name = "textBoxRegionFolder";
            this.textBoxRegionFolder.Size = new System.Drawing.Size(472, 20);
            this.textBoxRegionFolder.TabIndex = 15;
            // 
            // buttonRegionBrowseFolder
            // 
            this.buttonRegionBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRegionBrowseFolder.Location = new System.Drawing.Point(745, 485);
            this.buttonRegionBrowseFolder.Name = "buttonRegionBrowseFolder";
            this.buttonRegionBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonRegionBrowseFolder.TabIndex = 16;
            this.buttonRegionBrowseFolder.Text = "...";
            this.buttonRegionBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonRegionBrowseFolder.Click += new System.EventHandler(this.buttonRegionBrowseFolder_Click);
            // 
            // comboBoxRegionFormat
            // 
            this.comboBoxRegionFormat.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxRegionFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRegionFormat.FormattingEnabled = true;
            this.comboBoxRegionFormat.Location = new System.Drawing.Point(148, 22);
            this.comboBoxRegionFormat.Name = "comboBoxRegionFormat";
            this.comboBoxRegionFormat.Size = new System.Drawing.Size(51, 21);
            this.comboBoxRegionFormat.TabIndex = 9;
            // 
            // labelRegionFormat
            // 
            this.labelRegionFormat.AutoSize = true;
            this.labelRegionFormat.Location = new System.Drawing.Point(6, 25);
            this.labelRegionFormat.Name = "labelRegionFormat";
            this.labelRegionFormat.Size = new System.Drawing.Size(42, 13);
            this.labelRegionFormat.TabIndex = 0;
            this.labelRegionFormat.Text = "Format:";
            // 
            // numericUpDownRegionResolutionRatio
            // 
            this.numericUpDownRegionResolutionRatio.Location = new System.Drawing.Point(148, 75);
            this.numericUpDownRegionResolutionRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRegionResolutionRatio.Name = "numericUpDownRegionResolutionRatio";
            this.numericUpDownRegionResolutionRatio.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownRegionResolutionRatio.TabIndex = 11;
            this.numericUpDownRegionResolutionRatio.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelRegionResolutionRatio
            // 
            this.labelRegionResolutionRatio.AutoSize = true;
            this.labelRegionResolutionRatio.Location = new System.Drawing.Point(6, 77);
            this.labelRegionResolutionRatio.Name = "labelRegionResolutionRatio";
            this.labelRegionResolutionRatio.Size = new System.Drawing.Size(88, 13);
            this.labelRegionResolutionRatio.TabIndex = 0;
            this.labelRegionResolutionRatio.Text = "Resolution Ratio:";
            // 
            // numericUpDownRegionJpegQuality
            // 
            this.numericUpDownRegionJpegQuality.Location = new System.Drawing.Point(148, 49);
            this.numericUpDownRegionJpegQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRegionJpegQuality.Name = "numericUpDownRegionJpegQuality";
            this.numericUpDownRegionJpegQuality.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownRegionJpegQuality.TabIndex = 10;
            this.numericUpDownRegionJpegQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelRegionJpegQuality
            // 
            this.labelRegionJpegQuality.AutoSize = true;
            this.labelRegionJpegQuality.Location = new System.Drawing.Point(6, 51);
            this.labelRegionJpegQuality.Name = "labelRegionJpegQuality";
            this.labelRegionJpegQuality.Size = new System.Drawing.Size(72, 13);
            this.labelRegionJpegQuality.TabIndex = 0;
            this.labelRegionJpegQuality.Text = "JPEG Quality:";
            // 
            // checkBoxRegionMouse
            // 
            this.checkBoxRegionMouse.AutoSize = true;
            this.checkBoxRegionMouse.Location = new System.Drawing.Point(9, 102);
            this.checkBoxRegionMouse.Name = "checkBoxRegionMouse";
            this.checkBoxRegionMouse.Size = new System.Drawing.Size(130, 17);
            this.checkBoxRegionMouse.TabIndex = 12;
            this.checkBoxRegionMouse.Text = "Include mouse pointer";
            this.checkBoxRegionMouse.UseVisualStyleBackColor = true;
            // 
            // groupBoxRegionImage
            // 
            this.groupBoxRegionImage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxRegionImage.Controls.Add(this.labelRegionResolutionRatio);
            this.groupBoxRegionImage.Controls.Add(this.numericUpDownRegionResolutionRatio);
            this.groupBoxRegionImage.Controls.Add(this.labelRegionFormat);
            this.groupBoxRegionImage.Controls.Add(this.checkBoxRegionMouse);
            this.groupBoxRegionImage.Controls.Add(this.labelRegionJpegQuality);
            this.groupBoxRegionImage.Controls.Add(this.numericUpDownRegionJpegQuality);
            this.groupBoxRegionImage.Controls.Add(this.comboBoxRegionFormat);
            this.groupBoxRegionImage.Location = new System.Drawing.Point(12, 142);
            this.groupBoxRegionImage.Name = "groupBoxRegionImage";
            this.groupBoxRegionImage.Size = new System.Drawing.Size(205, 126);
            this.groupBoxRegionImage.TabIndex = 8;
            this.groupBoxRegionImage.TabStop = false;
            this.groupBoxRegionImage.Text = "Image";
            // 
            // groupBoxRegionPosition
            // 
            this.groupBoxRegionPosition.Controls.Add(this.labelRegionX);
            this.groupBoxRegionPosition.Controls.Add(this.labelRegionY);
            this.groupBoxRegionPosition.Controls.Add(this.numericUpDownRegionX);
            this.groupBoxRegionPosition.Controls.Add(this.numericUpDownRegionY);
            this.groupBoxRegionPosition.Location = new System.Drawing.Point(12, 38);
            this.groupBoxRegionPosition.Name = "groupBoxRegionPosition";
            this.groupBoxRegionPosition.Size = new System.Drawing.Size(205, 46);
            this.groupBoxRegionPosition.TabIndex = 2;
            this.groupBoxRegionPosition.TabStop = false;
            this.groupBoxRegionPosition.Text = "Position";
            // 
            // groupBoxRegionSize
            // 
            this.groupBoxRegionSize.Controls.Add(this.labelRegionWidth);
            this.groupBoxRegionSize.Controls.Add(this.numericUpDownRegionHeight);
            this.groupBoxRegionSize.Controls.Add(this.numericUpDownRegionWidth);
            this.groupBoxRegionSize.Controls.Add(this.labelRegionHeight);
            this.groupBoxRegionSize.Location = new System.Drawing.Point(12, 90);
            this.groupBoxRegionSize.Name = "groupBoxRegionSize";
            this.groupBoxRegionSize.Size = new System.Drawing.Size(205, 46);
            this.groupBoxRegionSize.TabIndex = 5;
            this.groupBoxRegionSize.TabStop = false;
            this.groupBoxRegionSize.Text = "Size";
            // 
            // groupBoxRegionPreview
            // 
            this.groupBoxRegionPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRegionPreview.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxRegionPreview.Controls.Add(this.pictureBoxRegionPreview);
            this.groupBoxRegionPreview.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxRegionPreview.Location = new System.Drawing.Point(223, 38);
            this.groupBoxRegionPreview.Name = "groupBoxRegionPreview";
            this.groupBoxRegionPreview.Size = new System.Drawing.Size(558, 443);
            this.groupBoxRegionPreview.TabIndex = 0;
            this.groupBoxRegionPreview.TabStop = false;
            this.groupBoxRegionPreview.Text = "Preview";
            // 
            // groupBoxRegionScreenTemplate
            // 
            this.groupBoxRegionScreenTemplate.Controls.Add(this.comboBoxRegionScreenTemplate);
            this.groupBoxRegionScreenTemplate.Location = new System.Drawing.Point(12, 274);
            this.groupBoxRegionScreenTemplate.Name = "groupBoxRegionScreenTemplate";
            this.groupBoxRegionScreenTemplate.Size = new System.Drawing.Size(205, 47);
            this.groupBoxRegionScreenTemplate.TabIndex = 13;
            this.groupBoxRegionScreenTemplate.TabStop = false;
            this.groupBoxRegionScreenTemplate.Text = "Screen Template";
            // 
            // comboBoxRegionScreenTemplate
            // 
            this.comboBoxRegionScreenTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRegionScreenTemplate.FormattingEnabled = true;
            this.comboBoxRegionScreenTemplate.Location = new System.Drawing.Point(6, 19);
            this.comboBoxRegionScreenTemplate.Name = "comboBoxRegionScreenTemplate";
            this.comboBoxRegionScreenTemplate.Size = new System.Drawing.Size(193, 21);
            this.comboBoxRegionScreenTemplate.TabIndex = 14;
            // 
            // FormRegion
            // 
            this.AcceptButton = this.buttonRegionOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this.buttonRegionCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBoxRegionScreenTemplate);
            this.Controls.Add(this.groupBoxRegionPreview);
            this.Controls.Add(this.groupBoxRegionSize);
            this.Controls.Add(this.groupBoxRegionPosition);
            this.Controls.Add(this.groupBoxRegionImage);
            this.Controls.Add(this.buttonRegionBrowseFolder);
            this.Controls.Add(this.textBoxRegionFolder);
            this.Controls.Add(this.labelRegionFolder);
            this.Controls.Add(this.buttonRegionCancel);
            this.Controls.Add(this.buttonRegionOK);
            this.Controls.Add(this.textBoxRegionMacro);
            this.Controls.Add(this.labelRegionMacro);
            this.Controls.Add(this.textBoxRegionName);
            this.Controls.Add(this.labelRegionName);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "FormRegion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormRegion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRegionPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionResolutionRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionJpegQuality)).EndInit();
            this.groupBoxRegionImage.ResumeLayout(false);
            this.groupBoxRegionImage.PerformLayout();
            this.groupBoxRegionPosition.ResumeLayout(false);
            this.groupBoxRegionPosition.PerformLayout();
            this.groupBoxRegionSize.ResumeLayout(false);
            this.groupBoxRegionSize.PerformLayout();
            this.groupBoxRegionPreview.ResumeLayout(false);
            this.groupBoxRegionScreenTemplate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRegionName;
        private System.Windows.Forms.TextBox textBoxRegionName;
        private System.Windows.Forms.Label labelRegionMacro;
        private System.Windows.Forms.TextBox textBoxRegionMacro;
        private System.Windows.Forms.PictureBox pictureBoxRegionPreview;
        private System.Windows.Forms.Label labelRegionX;
        private System.Windows.Forms.Label labelRegionY;
        private System.Windows.Forms.Label labelRegionWidth;
        private System.Windows.Forms.Label labelRegionHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownRegionX;
        private System.Windows.Forms.NumericUpDown numericUpDownRegionY;
        private System.Windows.Forms.NumericUpDown numericUpDownRegionWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownRegionHeight;
        private System.Windows.Forms.Button buttonRegionOK;
        private System.Windows.Forms.Button buttonRegionCancel;
        private System.Windows.Forms.Timer timerRegionPreview;
        private System.Windows.Forms.Label labelRegionFolder;
        private System.Windows.Forms.TextBox textBoxRegionFolder;
        private System.Windows.Forms.Button buttonRegionBrowseFolder;
        private System.Windows.Forms.ComboBox comboBoxRegionFormat;
        private System.Windows.Forms.Label labelRegionFormat;
        private System.Windows.Forms.NumericUpDown numericUpDownRegionResolutionRatio;
        private System.Windows.Forms.Label labelRegionResolutionRatio;
        private System.Windows.Forms.NumericUpDown numericUpDownRegionJpegQuality;
        private System.Windows.Forms.Label labelRegionJpegQuality;
        private System.Windows.Forms.CheckBox checkBoxRegionMouse;
        private System.Windows.Forms.GroupBox groupBoxRegionImage;
        private System.Windows.Forms.GroupBox groupBoxRegionPosition;
        private System.Windows.Forms.GroupBox groupBoxRegionSize;
        private System.Windows.Forms.GroupBox groupBoxRegionPreview;
        private System.Windows.Forms.GroupBox groupBoxRegionScreenTemplate;
        private System.Windows.Forms.ComboBox comboBoxRegionScreenTemplate;
    }
}