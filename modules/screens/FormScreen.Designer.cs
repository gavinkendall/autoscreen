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
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelMacro = new System.Windows.Forms.Label();
            this.textBoxMacro = new System.Windows.Forms.TextBox();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelFolder = new System.Windows.Forms.Label();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.buttonScreenBrowseFolder = new System.Windows.Forms.Button();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.labelFormat = new System.Windows.Forms.Label();
            this.numericUpDownResolutionRatio = new System.Windows.Forms.NumericUpDown();
            this.labelResolutionRatio = new System.Windows.Forms.Label();
            this.numericUpDownJpegQuality = new System.Windows.Forms.NumericUpDown();
            this.labelJpegQuality = new System.Windows.Forms.Label();
            this.checkBoxMouse = new System.Windows.Forms.CheckBox();
            this.groupBoxImage = new System.Windows.Forms.GroupBox();
            this.groupBoxComponent = new System.Windows.Forms.GroupBox();
            this.comboBoxScreenComponent = new System.Windows.Forms.ComboBox();
            this.groupBoxPreview = new System.Windows.Forms.GroupBox();
            this.timerScreenPreview = new System.Windows.Forms.Timer(this.components);
            this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJpegQuality)).BeginInit();
            this.groupBoxImage.SuspendLayout();
            this.groupBoxComponent.SuspendLayout();
            this.groupBoxPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(9, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(53, 6);
            this.textBoxName.MaxLength = 50;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(318, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // labelMacro
            // 
            this.labelMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelMacro.AutoSize = true;
            this.labelMacro.Location = new System.Drawing.Point(229, 502);
            this.labelMacro.Name = "labelMacro";
            this.labelMacro.Size = new System.Drawing.Size(40, 13);
            this.labelMacro.TabIndex = 0;
            this.labelMacro.Text = "Macro:";
            // 
            // textBoxMacro
            // 
            this.textBoxMacro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMacro.Location = new System.Drawing.Point(273, 499);
            this.textBoxMacro.Name = "textBoxMacro";
            this.textBoxMacro.Size = new System.Drawing.Size(505, 20);
            this.textBoxMacro.TabIndex = 17;
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.Black;
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(552, 408);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPreview.TabIndex = 5;
            this.pictureBoxPreview.TabStop = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(574, 525);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(99, 23);
            this.buttonOK.TabIndex = 18;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(679, 525);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 19;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelFolder
            // 
            this.labelFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFolder.AutoSize = true;
            this.labelFolder.Location = new System.Drawing.Point(229, 476);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(39, 13);
            this.labelFolder.TabIndex = 0;
            this.labelFolder.Text = "Folder:";
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolder.Location = new System.Drawing.Point(273, 473);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(472, 20);
            this.textBoxFolder.TabIndex = 15;
            // 
            // buttonScreenBrowseFolder
            // 
            this.buttonScreenBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonScreenBrowseFolder.Location = new System.Drawing.Point(751, 471);
            this.buttonScreenBrowseFolder.Name = "buttonScreenBrowseFolder";
            this.buttonScreenBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonScreenBrowseFolder.TabIndex = 16;
            this.buttonScreenBrowseFolder.Text = "...";
            this.buttonScreenBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonScreenBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Location = new System.Drawing.Point(148, 22);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(51, 21);
            this.comboBoxFormat.TabIndex = 9;
            this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormat_SelectedIndexChanged);
            // 
            // labelFormat
            // 
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new System.Drawing.Point(6, 25);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new System.Drawing.Size(42, 13);
            this.labelFormat.TabIndex = 0;
            this.labelFormat.Text = "Format:";
            // 
            // numericUpDownResolutionRatio
            // 
            this.numericUpDownResolutionRatio.Location = new System.Drawing.Point(148, 75);
            this.numericUpDownResolutionRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownResolutionRatio.Name = "numericUpDownResolutionRatio";
            this.numericUpDownResolutionRatio.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownResolutionRatio.TabIndex = 11;
            this.numericUpDownResolutionRatio.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelResolutionRatio
            // 
            this.labelResolutionRatio.AutoSize = true;
            this.labelResolutionRatio.Location = new System.Drawing.Point(6, 77);
            this.labelResolutionRatio.Name = "labelResolutionRatio";
            this.labelResolutionRatio.Size = new System.Drawing.Size(88, 13);
            this.labelResolutionRatio.TabIndex = 0;
            this.labelResolutionRatio.Text = "Resolution Ratio:";
            // 
            // numericUpDownJpegQuality
            // 
            this.numericUpDownJpegQuality.Location = new System.Drawing.Point(148, 49);
            this.numericUpDownJpegQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownJpegQuality.Name = "numericUpDownJpegQuality";
            this.numericUpDownJpegQuality.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownJpegQuality.TabIndex = 10;
            this.numericUpDownJpegQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelJpegQuality
            // 
            this.labelJpegQuality.AutoSize = true;
            this.labelJpegQuality.Location = new System.Drawing.Point(6, 51);
            this.labelJpegQuality.Name = "labelJpegQuality";
            this.labelJpegQuality.Size = new System.Drawing.Size(72, 13);
            this.labelJpegQuality.TabIndex = 0;
            this.labelJpegQuality.Text = "JPEG Quality:";
            // 
            // checkBoxMouse
            // 
            this.checkBoxMouse.AutoSize = true;
            this.checkBoxMouse.Location = new System.Drawing.Point(9, 102);
            this.checkBoxMouse.Name = "checkBoxMouse";
            this.checkBoxMouse.Size = new System.Drawing.Size(130, 17);
            this.checkBoxMouse.TabIndex = 12;
            this.checkBoxMouse.Text = "Include mouse pointer";
            this.checkBoxMouse.UseVisualStyleBackColor = true;
            // 
            // groupBoxImage
            // 
            this.groupBoxImage.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxImage.Controls.Add(this.labelResolutionRatio);
            this.groupBoxImage.Controls.Add(this.numericUpDownResolutionRatio);
            this.groupBoxImage.Controls.Add(this.labelFormat);
            this.groupBoxImage.Controls.Add(this.checkBoxMouse);
            this.groupBoxImage.Controls.Add(this.labelJpegQuality);
            this.groupBoxImage.Controls.Add(this.numericUpDownJpegQuality);
            this.groupBoxImage.Controls.Add(this.comboBoxFormat);
            this.groupBoxImage.Location = new System.Drawing.Point(12, 90);
            this.groupBoxImage.Name = "groupBoxImage";
            this.groupBoxImage.Size = new System.Drawing.Size(205, 126);
            this.groupBoxImage.TabIndex = 8;
            this.groupBoxImage.TabStop = false;
            this.groupBoxImage.Text = "Image";
            // 
            // groupBoxComponent
            // 
            this.groupBoxComponent.Controls.Add(this.comboBoxScreenComponent);
            this.groupBoxComponent.Location = new System.Drawing.Point(12, 38);
            this.groupBoxComponent.Name = "groupBoxComponent";
            this.groupBoxComponent.Size = new System.Drawing.Size(205, 46);
            this.groupBoxComponent.TabIndex = 2;
            this.groupBoxComponent.TabStop = false;
            this.groupBoxComponent.Text = "Component";
            // 
            // comboBoxScreenComponent
            // 
            this.comboBoxScreenComponent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScreenComponent.FormattingEnabled = true;
            this.comboBoxScreenComponent.Location = new System.Drawing.Point(6, 19);
            this.comboBoxScreenComponent.Name = "comboBoxScreenComponent";
            this.comboBoxScreenComponent.Size = new System.Drawing.Size(193, 21);
            this.comboBoxScreenComponent.TabIndex = 3;
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPreview.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxPreview.Controls.Add(this.pictureBoxPreview);
            this.groupBoxPreview.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxPreview.Location = new System.Drawing.Point(223, 38);
            this.groupBoxPreview.Name = "groupBoxPreview";
            this.groupBoxPreview.Size = new System.Drawing.Size(558, 427);
            this.groupBoxPreview.TabIndex = 0;
            this.groupBoxPreview.TabStop = false;
            this.groupBoxPreview.Text = "Preview";
            // 
            // timerScreenPreview
            // 
            this.timerScreenPreview.Interval = 500;
            this.timerScreenPreview.Tick += new System.EventHandler(this.Tick_timerPreview);
            // 
            // checkBoxEnabled
            // 
            this.checkBoxEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxEnabled.AutoSize = true;
            this.checkBoxEnabled.Location = new System.Drawing.Point(707, 8);
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.checkBoxEnabled.Size = new System.Drawing.Size(65, 17);
            this.checkBoxEnabled.TabIndex = 0;
            this.checkBoxEnabled.TabStop = false;
            this.checkBoxEnabled.Text = "Enabled";
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            // 
            // FormScreen
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(784, 560);
            this.Controls.Add(this.checkBoxEnabled);
            this.Controls.Add(this.groupBoxPreview);
            this.Controls.Add(this.groupBoxComponent);
            this.Controls.Add(this.groupBoxImage);
            this.Controls.Add(this.buttonScreenBrowseFolder);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.labelFolder);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxMacro);
            this.Controls.Add(this.labelMacro);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "FormScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormScreen_FormClosing);
            this.Load += new System.EventHandler(this.FormScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJpegQuality)).EndInit();
            this.groupBoxImage.ResumeLayout(false);
            this.groupBoxImage.PerformLayout();
            this.groupBoxComponent.ResumeLayout(false);
            this.groupBoxPreview.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelMacro;
        private System.Windows.Forms.TextBox textBoxMacro;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelFolder;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.Button buttonScreenBrowseFolder;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.Label labelFormat;
        private System.Windows.Forms.NumericUpDown numericUpDownResolutionRatio;
        private System.Windows.Forms.Label labelResolutionRatio;
        private System.Windows.Forms.NumericUpDown numericUpDownJpegQuality;
        private System.Windows.Forms.Label labelJpegQuality;
        private System.Windows.Forms.CheckBox checkBoxMouse;
        private System.Windows.Forms.GroupBox groupBoxImage;
        private System.Windows.Forms.GroupBox groupBoxComponent;
        private System.Windows.Forms.GroupBox groupBoxPreview;
        private System.Windows.Forms.ComboBox comboBoxScreenComponent;
        private System.Windows.Forms.Timer timerScreenPreview;
        private System.Windows.Forms.CheckBox checkBoxEnabled;
    }
}