namespace AutoScreenCapture
{
    partial class FormScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScreen));
            this.labelScreenName = new System.Windows.Forms.Label();
            this.textBoxScreenName = new System.Windows.Forms.TextBox();
            this.labelScreenMacro = new System.Windows.Forms.Label();
            this.textBoxScreenMacro = new System.Windows.Forms.TextBox();
            this.pictureBoxScreenPreview = new System.Windows.Forms.PictureBox();
            this.labelScreenX = new System.Windows.Forms.Label();
            this.labelScreenY = new System.Windows.Forms.Label();
            this.labelScreenWidth = new System.Windows.Forms.Label();
            this.labelScreenHeight = new System.Windows.Forms.Label();
            this.numericUpDownScreenX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownScreenY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownScreenWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownScreenHeight = new System.Windows.Forms.NumericUpDown();
            this.buttonScreenOK = new System.Windows.Forms.Button();
            this.buttonScreenCancel = new System.Windows.Forms.Button();
            this.timerScreenPreview = new System.Windows.Forms.Timer(this.components);
            this.labelScreenFolder = new System.Windows.Forms.Label();
            this.textBoxScreenFolder = new System.Windows.Forms.TextBox();
            this.buttonScreenBrowseFolder = new System.Windows.Forms.Button();
            this.comboBoxScreenFormat = new System.Windows.Forms.ComboBox();
            this.labelScreenFormat = new System.Windows.Forms.Label();
            this.numericUpDownScreenResolutionRatio = new System.Windows.Forms.NumericUpDown();
            this.labelScreenResolutionRatio = new System.Windows.Forms.Label();
            this.numericUpDownScreenJpegQuality = new System.Windows.Forms.NumericUpDown();
            this.labelScreenJpegQuality = new System.Windows.Forms.Label();
            this.checkBoxScreenMouse = new System.Windows.Forms.CheckBox();
            this.groupBoxScreenImage = new System.Windows.Forms.GroupBox();
            this.groupBoxScreenPosition = new System.Windows.Forms.GroupBox();
            this.groupBoxScreenSize = new System.Windows.Forms.GroupBox();
            this.groupBoxScreenPreview = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenResolutionRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenJpegQuality)).BeginInit();
            this.groupBoxScreenImage.SuspendLayout();
            this.groupBoxScreenPosition.SuspendLayout();
            this.groupBoxScreenSize.SuspendLayout();
            this.groupBoxScreenPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelScreenName
            // 
            this.labelScreenName.AutoSize = true;
            this.labelScreenName.Location = new System.Drawing.Point(9, 9);
            this.labelScreenName.Name = "labelScreenName";
            this.labelScreenName.Size = new System.Drawing.Size(38, 13);
            this.labelScreenName.TabIndex = 0;
            this.labelScreenName.Text = "Name:";
            // 
            // textBoxScreenName
            // 
            this.textBoxScreenName.Location = new System.Drawing.Point(53, 6);
            this.textBoxScreenName.MaxLength = 50;
            this.textBoxScreenName.Name = "textBoxScreenName";
            this.textBoxScreenName.Size = new System.Drawing.Size(318, 20);
            this.textBoxScreenName.TabIndex = 1;
            // 
            // labelScreenMacro
            // 
            this.labelScreenMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelScreenMacro.AutoSize = true;
            this.labelScreenMacro.Location = new System.Drawing.Point(223, 516);
            this.labelScreenMacro.Name = "labelScreenMacro";
            this.labelScreenMacro.Size = new System.Drawing.Size(40, 13);
            this.labelScreenMacro.TabIndex = 0;
            this.labelScreenMacro.Text = "Macro:";
            // 
            // textBoxScreenMacro
            // 
            this.textBoxScreenMacro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScreenMacro.Location = new System.Drawing.Point(267, 513);
            this.textBoxScreenMacro.Name = "textBoxScreenMacro";
            this.textBoxScreenMacro.Size = new System.Drawing.Size(505, 20);
            this.textBoxScreenMacro.TabIndex = 17;
            // 
            // pictureBoxScreenPreview
            // 
            this.pictureBoxScreenPreview.BackColor = System.Drawing.Color.Black;
            this.pictureBoxScreenPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScreenPreview.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxScreenPreview.Name = "pictureBoxScreenPreview";
            this.pictureBoxScreenPreview.Size = new System.Drawing.Size(552, 424);
            this.pictureBoxScreenPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreenPreview.TabIndex = 5;
            this.pictureBoxScreenPreview.TabStop = false;
            // 
            // labelScreenX
            // 
            this.labelScreenX.AutoSize = true;
            this.labelScreenX.Location = new System.Drawing.Point(21, 22);
            this.labelScreenX.Name = "labelScreenX";
            this.labelScreenX.Size = new System.Drawing.Size(17, 13);
            this.labelScreenX.TabIndex = 0;
            this.labelScreenX.Text = "X:";
            // 
            // labelScreenY
            // 
            this.labelScreenY.AutoSize = true;
            this.labelScreenY.Location = new System.Drawing.Point(125, 22);
            this.labelScreenY.Name = "labelScreenY";
            this.labelScreenY.Size = new System.Drawing.Size(17, 13);
            this.labelScreenY.TabIndex = 0;
            this.labelScreenY.Text = "Y:";
            // 
            // labelScreenWidth
            // 
            this.labelScreenWidth.AutoSize = true;
            this.labelScreenWidth.Location = new System.Drawing.Point(6, 22);
            this.labelScreenWidth.Name = "labelScreenWidth";
            this.labelScreenWidth.Size = new System.Drawing.Size(38, 13);
            this.labelScreenWidth.TabIndex = 0;
            this.labelScreenWidth.Text = "Width:";
            // 
            // labelScreenHeight
            // 
            this.labelScreenHeight.AutoSize = true;
            this.labelScreenHeight.Location = new System.Drawing.Point(101, 22);
            this.labelScreenHeight.Name = "labelScreenHeight";
            this.labelScreenHeight.Size = new System.Drawing.Size(41, 13);
            this.labelScreenHeight.TabIndex = 0;
            this.labelScreenHeight.Text = "Height:";
            // 
            // numericUpDownScreenX
            // 
            this.numericUpDownScreenX.Location = new System.Drawing.Point(44, 20);
            this.numericUpDownScreenX.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownScreenX.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownScreenX.Name = "numericUpDownScreenX";
            this.numericUpDownScreenX.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownScreenX.TabIndex = 3;
            // 
            // numericUpDownScreenY
            // 
            this.numericUpDownScreenY.Location = new System.Drawing.Point(148, 20);
            this.numericUpDownScreenY.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownScreenY.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownScreenY.Name = "numericUpDownScreenY";
            this.numericUpDownScreenY.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownScreenY.TabIndex = 4;
            // 
            // numericUpDownScreenWidth
            // 
            this.numericUpDownScreenWidth.Location = new System.Drawing.Point(44, 20);
            this.numericUpDownScreenWidth.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownScreenWidth.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownScreenWidth.Name = "numericUpDownScreenWidth";
            this.numericUpDownScreenWidth.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownScreenWidth.TabIndex = 6;
            // 
            // numericUpDownScreenHeight
            // 
            this.numericUpDownScreenHeight.Location = new System.Drawing.Point(148, 20);
            this.numericUpDownScreenHeight.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownScreenHeight.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownScreenHeight.Name = "numericUpDownScreenHeight";
            this.numericUpDownScreenHeight.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownScreenHeight.TabIndex = 7;
            // 
            // buttonScreenOK
            // 
            this.buttonScreenOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonScreenOK.Location = new System.Drawing.Point(12, 534);
            this.buttonScreenOK.Name = "buttonScreenOK";
            this.buttonScreenOK.Size = new System.Drawing.Size(99, 23);
            this.buttonScreenOK.TabIndex = 18;
            this.buttonScreenOK.Text = "OK";
            this.buttonScreenOK.UseVisualStyleBackColor = true;
            this.buttonScreenOK.Click += new System.EventHandler(this.Click_buttonScreenOK);
            // 
            // buttonScreenCancel
            // 
            this.buttonScreenCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonScreenCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonScreenCancel.Location = new System.Drawing.Point(118, 534);
            this.buttonScreenCancel.Name = "buttonScreenCancel";
            this.buttonScreenCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonScreenCancel.TabIndex = 19;
            this.buttonScreenCancel.Text = "Cancel";
            this.buttonScreenCancel.UseVisualStyleBackColor = true;
            this.buttonScreenCancel.Click += new System.EventHandler(this.Click_buttonScreenCancel);
            // 
            // timerScreenPreview
            // 
            this.timerScreenPreview.Enabled = true;
            this.timerScreenPreview.Interval = 500;
            this.timerScreenPreview.Tick += new System.EventHandler(this.Tick_timerScreenPreview);
            // 
            // labelScreenFolder
            // 
            this.labelScreenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelScreenFolder.AutoSize = true;
            this.labelScreenFolder.Location = new System.Drawing.Point(223, 490);
            this.labelScreenFolder.Name = "labelScreenFolder";
            this.labelScreenFolder.Size = new System.Drawing.Size(39, 13);
            this.labelScreenFolder.TabIndex = 0;
            this.labelScreenFolder.Text = "Folder:";
            // 
            // textBoxScreenFolder
            // 
            this.textBoxScreenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScreenFolder.Location = new System.Drawing.Point(267, 487);
            this.textBoxScreenFolder.Name = "textBoxScreenFolder";
            this.textBoxScreenFolder.Size = new System.Drawing.Size(472, 20);
            this.textBoxScreenFolder.TabIndex = 15;
            // 
            // buttonScreenBrowseFolder
            // 
            this.buttonScreenBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonScreenBrowseFolder.Location = new System.Drawing.Point(745, 485);
            this.buttonScreenBrowseFolder.Name = "buttonScreenBrowseFolder";
            this.buttonScreenBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonScreenBrowseFolder.TabIndex = 16;
            this.buttonScreenBrowseFolder.Text = "...";
            this.buttonScreenBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonScreenBrowseFolder.Click += new System.EventHandler(this.buttonScreenBrowseFolder_Click);
            // 
            // comboBoxScreenFormat
            // 
            this.comboBoxScreenFormat.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxScreenFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScreenFormat.FormattingEnabled = true;
            this.comboBoxScreenFormat.Location = new System.Drawing.Point(148, 22);
            this.comboBoxScreenFormat.Name = "comboBoxScreenFormat";
            this.comboBoxScreenFormat.Size = new System.Drawing.Size(51, 21);
            this.comboBoxScreenFormat.TabIndex = 9;
            // 
            // labelScreenFormat
            // 
            this.labelScreenFormat.AutoSize = true;
            this.labelScreenFormat.Location = new System.Drawing.Point(6, 25);
            this.labelScreenFormat.Name = "labelScreenFormat";
            this.labelScreenFormat.Size = new System.Drawing.Size(42, 13);
            this.labelScreenFormat.TabIndex = 0;
            this.labelScreenFormat.Text = "Format:";
            // 
            // numericUpDownScreenResolutionRatio
            // 
            this.numericUpDownScreenResolutionRatio.Location = new System.Drawing.Point(148, 75);
            this.numericUpDownScreenResolutionRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownScreenResolutionRatio.Name = "numericUpDownScreenResolutionRatio";
            this.numericUpDownScreenResolutionRatio.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownScreenResolutionRatio.TabIndex = 11;
            this.numericUpDownScreenResolutionRatio.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelScreenResolutionRatio
            // 
            this.labelScreenResolutionRatio.AutoSize = true;
            this.labelScreenResolutionRatio.Location = new System.Drawing.Point(6, 77);
            this.labelScreenResolutionRatio.Name = "labelScreenResolutionRatio";
            this.labelScreenResolutionRatio.Size = new System.Drawing.Size(88, 13);
            this.labelScreenResolutionRatio.TabIndex = 0;
            this.labelScreenResolutionRatio.Text = "Resolution Ratio:";
            // 
            // numericUpDownScreenJpegQuality
            // 
            this.numericUpDownScreenJpegQuality.Location = new System.Drawing.Point(148, 49);
            this.numericUpDownScreenJpegQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownScreenJpegQuality.Name = "numericUpDownScreenJpegQuality";
            this.numericUpDownScreenJpegQuality.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownScreenJpegQuality.TabIndex = 10;
            this.numericUpDownScreenJpegQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelScreenJpegQuality
            // 
            this.labelScreenJpegQuality.AutoSize = true;
            this.labelScreenJpegQuality.Location = new System.Drawing.Point(6, 51);
            this.labelScreenJpegQuality.Name = "labelScreenJpegQuality";
            this.labelScreenJpegQuality.Size = new System.Drawing.Size(72, 13);
            this.labelScreenJpegQuality.TabIndex = 0;
            this.labelScreenJpegQuality.Text = "JPEG Quality:";
            // 
            // checkBoxScreenMouse
            // 
            this.checkBoxScreenMouse.AutoSize = true;
            this.checkBoxScreenMouse.Location = new System.Drawing.Point(9, 102);
            this.checkBoxScreenMouse.Name = "checkBoxScreenMouse";
            this.checkBoxScreenMouse.Size = new System.Drawing.Size(130, 17);
            this.checkBoxScreenMouse.TabIndex = 12;
            this.checkBoxScreenMouse.Text = "Include mouse pointer";
            this.checkBoxScreenMouse.UseVisualStyleBackColor = true;
            // 
            // groupBoxScreenImage
            // 
            this.groupBoxScreenImage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxScreenImage.Controls.Add(this.labelScreenResolutionRatio);
            this.groupBoxScreenImage.Controls.Add(this.numericUpDownScreenResolutionRatio);
            this.groupBoxScreenImage.Controls.Add(this.labelScreenFormat);
            this.groupBoxScreenImage.Controls.Add(this.checkBoxScreenMouse);
            this.groupBoxScreenImage.Controls.Add(this.labelScreenJpegQuality);
            this.groupBoxScreenImage.Controls.Add(this.numericUpDownScreenJpegQuality);
            this.groupBoxScreenImage.Controls.Add(this.comboBoxScreenFormat);
            this.groupBoxScreenImage.Location = new System.Drawing.Point(12, 142);
            this.groupBoxScreenImage.Name = "groupBoxScreenImage";
            this.groupBoxScreenImage.Size = new System.Drawing.Size(205, 126);
            this.groupBoxScreenImage.TabIndex = 8;
            this.groupBoxScreenImage.TabStop = false;
            this.groupBoxScreenImage.Text = "Image";
            // 
            // groupBoxScreenPosition
            // 
            this.groupBoxScreenPosition.Controls.Add(this.labelScreenX);
            this.groupBoxScreenPosition.Controls.Add(this.labelScreenY);
            this.groupBoxScreenPosition.Controls.Add(this.numericUpDownScreenX);
            this.groupBoxScreenPosition.Controls.Add(this.numericUpDownScreenY);
            this.groupBoxScreenPosition.Location = new System.Drawing.Point(12, 38);
            this.groupBoxScreenPosition.Name = "groupBoxScreenPosition";
            this.groupBoxScreenPosition.Size = new System.Drawing.Size(205, 46);
            this.groupBoxScreenPosition.TabIndex = 2;
            this.groupBoxScreenPosition.TabStop = false;
            this.groupBoxScreenPosition.Text = "Position";
            // 
            // groupBoxScreenSize
            // 
            this.groupBoxScreenSize.Controls.Add(this.labelScreenWidth);
            this.groupBoxScreenSize.Controls.Add(this.numericUpDownScreenHeight);
            this.groupBoxScreenSize.Controls.Add(this.numericUpDownScreenWidth);
            this.groupBoxScreenSize.Controls.Add(this.labelScreenHeight);
            this.groupBoxScreenSize.Location = new System.Drawing.Point(12, 90);
            this.groupBoxScreenSize.Name = "groupBoxScreenSize";
            this.groupBoxScreenSize.Size = new System.Drawing.Size(205, 46);
            this.groupBoxScreenSize.TabIndex = 5;
            this.groupBoxScreenSize.TabStop = false;
            this.groupBoxScreenSize.Text = "Size";
            // 
            // groupBoxScreenPreview
            // 
            this.groupBoxScreenPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScreenPreview.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxScreenPreview.Controls.Add(this.pictureBoxScreenPreview);
            this.groupBoxScreenPreview.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxScreenPreview.Location = new System.Drawing.Point(223, 38);
            this.groupBoxScreenPreview.Name = "groupBoxScreenPreview";
            this.groupBoxScreenPreview.Size = new System.Drawing.Size(558, 443);
            this.groupBoxScreenPreview.TabIndex = 0;
            this.groupBoxScreenPreview.TabStop = false;
            this.groupBoxScreenPreview.Text = "Preview";
            // 
            // FormScreen
            // 
            this.AcceptButton = this.buttonScreenOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this.buttonScreenCancel;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBoxScreenPreview);
            this.Controls.Add(this.groupBoxScreenSize);
            this.Controls.Add(this.groupBoxScreenPosition);
            this.Controls.Add(this.groupBoxScreenImage);
            this.Controls.Add(this.buttonScreenBrowseFolder);
            this.Controls.Add(this.textBoxScreenFolder);
            this.Controls.Add(this.labelScreenFolder);
            this.Controls.Add(this.buttonScreenCancel);
            this.Controls.Add(this.buttonScreenOK);
            this.Controls.Add(this.textBoxScreenMacro);
            this.Controls.Add(this.labelScreenMacro);
            this.Controls.Add(this.textBoxScreenName);
            this.Controls.Add(this.labelScreenName);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "FormScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenResolutionRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScreenJpegQuality)).EndInit();
            this.groupBoxScreenImage.ResumeLayout(false);
            this.groupBoxScreenImage.PerformLayout();
            this.groupBoxScreenPosition.ResumeLayout(false);
            this.groupBoxScreenPosition.PerformLayout();
            this.groupBoxScreenSize.ResumeLayout(false);
            this.groupBoxScreenSize.PerformLayout();
            this.groupBoxScreenPreview.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelScreenName;
        private System.Windows.Forms.TextBox textBoxScreenName;
        private System.Windows.Forms.Label labelScreenMacro;
        private System.Windows.Forms.TextBox textBoxScreenMacro;
        private System.Windows.Forms.PictureBox pictureBoxScreenPreview;
        private System.Windows.Forms.Label labelScreenX;
        private System.Windows.Forms.Label labelScreenY;
        private System.Windows.Forms.Label labelScreenWidth;
        private System.Windows.Forms.Label labelScreenHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownScreenX;
        private System.Windows.Forms.NumericUpDown numericUpDownScreenY;
        private System.Windows.Forms.NumericUpDown numericUpDownScreenWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownScreenHeight;
        private System.Windows.Forms.Button buttonScreenOK;
        private System.Windows.Forms.Button buttonScreenCancel;
        private System.Windows.Forms.Timer timerScreenPreview;
        private System.Windows.Forms.Label labelScreenFolder;
        private System.Windows.Forms.TextBox textBoxScreenFolder;
        private System.Windows.Forms.Button buttonScreenBrowseFolder;
        private System.Windows.Forms.ComboBox comboBoxScreenFormat;
        private System.Windows.Forms.Label labelScreenFormat;
        private System.Windows.Forms.NumericUpDown numericUpDownScreenResolutionRatio;
        private System.Windows.Forms.Label labelScreenResolutionRatio;
        private System.Windows.Forms.NumericUpDown numericUpDownScreenJpegQuality;
        private System.Windows.Forms.Label labelScreenJpegQuality;
        private System.Windows.Forms.CheckBox checkBoxScreenMouse;
        private System.Windows.Forms.GroupBox groupBoxScreenImage;
        private System.Windows.Forms.GroupBox groupBoxScreenPosition;
        private System.Windows.Forms.GroupBox groupBoxScreenSize;
        private System.Windows.Forms.GroupBox groupBoxScreenPreview;
    }
}