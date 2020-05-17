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
            this.textBoxMacroPreview = new System.Windows.Forms.TextBox();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.buttonMacroTags = new System.Windows.Forms.Button();
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
            this.labelName.Location = new System.Drawing.Point(12, 35);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(56, 32);
            this.textBoxName.MaxLength = 50;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(546, 20);
            this.textBoxName.TabIndex = 2;
            // 
            // labelMacro
            // 
            this.labelMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelMacro.AutoSize = true;
            this.labelMacro.Location = new System.Drawing.Point(229, 425);
            this.labelMacro.Name = "labelMacro";
            this.labelMacro.Size = new System.Drawing.Size(40, 13);
            this.labelMacro.TabIndex = 19;
            this.labelMacro.Text = "Macro:";
            // 
            // textBoxMacro
            // 
            this.textBoxMacro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMacro.Location = new System.Drawing.Point(273, 422);
            this.textBoxMacro.Name = "textBoxMacro";
            this.textBoxMacro.Size = new System.Drawing.Size(444, 20);
            this.textBoxMacro.TabIndex = 20;
            this.textBoxMacro.TextChanged += new System.EventHandler(this.updatePreviewMacro);
            this.textBoxMacro.MouseHover += new System.EventHandler(this.textBoxMacro_MouseHover);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.Black;
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(524, 311);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPreview.TabIndex = 5;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.Click += new System.EventHandler(this.updatePreviewImage);
            this.pictureBoxPreview.MouseHover += new System.EventHandler(this.pictureBoxPreview_MouseHover);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Location = new System.Drawing.Point(12, 420);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(99, 23);
            this.buttonOK.TabIndex = 22;
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
            this.buttonCancel.TabIndex = 23;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelFolder
            // 
            this.labelFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFolder.AutoSize = true;
            this.labelFolder.Location = new System.Drawing.Point(229, 399);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(39, 13);
            this.labelFolder.TabIndex = 16;
            this.labelFolder.Text = "Folder:";
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolder.Location = new System.Drawing.Point(273, 396);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(444, 20);
            this.textBoxFolder.TabIndex = 17;
            this.textBoxFolder.TextChanged += new System.EventHandler(this.updatePreviewMacro);
            this.textBoxFolder.MouseHover += new System.EventHandler(this.textBoxFolder_MouseHover);
            // 
            // buttonScreenBrowseFolder
            // 
            this.buttonScreenBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonScreenBrowseFolder.Image = global::AutoScreenCapture.Properties.Resources.openfolder;
            this.buttonScreenBrowseFolder.Location = new System.Drawing.Point(723, 394);
            this.buttonScreenBrowseFolder.Name = "buttonScreenBrowseFolder";
            this.buttonScreenBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonScreenBrowseFolder.TabIndex = 18;
            this.buttonScreenBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonScreenBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            this.buttonScreenBrowseFolder.MouseHover += new System.EventHandler(this.buttonScreenBrowseFolder_MouseHover);
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Location = new System.Drawing.Point(148, 22);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(51, 21);
            this.comboBoxFormat.TabIndex = 8;
            this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormat_SelectedIndexChanged);
            this.comboBoxFormat.MouseHover += new System.EventHandler(this.comboBoxFormat_MouseHover);
            // 
            // labelFormat
            // 
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new System.Drawing.Point(6, 25);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new System.Drawing.Size(42, 13);
            this.labelFormat.TabIndex = 7;
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
            this.numericUpDownResolutionRatio.TabIndex = 12;
            this.numericUpDownResolutionRatio.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownResolutionRatio.ValueChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // labelResolutionRatio
            // 
            this.labelResolutionRatio.AutoSize = true;
            this.labelResolutionRatio.Location = new System.Drawing.Point(6, 77);
            this.labelResolutionRatio.Name = "labelResolutionRatio";
            this.labelResolutionRatio.Size = new System.Drawing.Size(88, 13);
            this.labelResolutionRatio.TabIndex = 11;
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
            this.labelJpegQuality.TabIndex = 9;
            this.labelJpegQuality.Text = "JPEG Quality:";
            // 
            // checkBoxMouse
            // 
            this.checkBoxMouse.AutoSize = true;
            this.checkBoxMouse.Location = new System.Drawing.Point(9, 102);
            this.checkBoxMouse.Name = "checkBoxMouse";
            this.checkBoxMouse.Size = new System.Drawing.Size(130, 17);
            this.checkBoxMouse.TabIndex = 13;
            this.checkBoxMouse.Text = "Include mouse pointer";
            this.checkBoxMouse.UseVisualStyleBackColor = true;
            this.checkBoxMouse.CheckedChanged += new System.EventHandler(this.updatePreviewImage);
            this.checkBoxMouse.MouseHover += new System.EventHandler(this.checkBoxMouse_MouseHover);
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
            this.groupBoxImage.Location = new System.Drawing.Point(12, 110);
            this.groupBoxImage.Name = "groupBoxImage";
            this.groupBoxImage.Size = new System.Drawing.Size(205, 126);
            this.groupBoxImage.TabIndex = 6;
            this.groupBoxImage.TabStop = false;
            this.groupBoxImage.Text = "Image";
            // 
            // groupBoxComponent
            // 
            this.groupBoxComponent.Controls.Add(this.comboBoxScreenComponent);
            this.groupBoxComponent.Location = new System.Drawing.Point(12, 58);
            this.groupBoxComponent.Name = "groupBoxComponent";
            this.groupBoxComponent.Size = new System.Drawing.Size(205, 46);
            this.groupBoxComponent.TabIndex = 4;
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
            this.comboBoxScreenComponent.TabIndex = 5;
            this.comboBoxScreenComponent.SelectedIndexChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPreview.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxPreview.Controls.Add(this.textBoxMacroPreview);
            this.groupBoxPreview.Controls.Add(this.pictureBoxPreview);
            this.groupBoxPreview.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxPreview.Location = new System.Drawing.Point(223, 58);
            this.groupBoxPreview.Name = "groupBoxPreview";
            this.groupBoxPreview.Size = new System.Drawing.Size(530, 330);
            this.groupBoxPreview.TabIndex = 14;
            this.groupBoxPreview.TabStop = false;
            this.groupBoxPreview.Text = "Preview";
            // 
            // textBoxMacroPreview
            // 
            this.textBoxMacroPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxMacroPreview.Location = new System.Drawing.Point(3, 307);
            this.textBoxMacroPreview.Name = "textBoxMacroPreview";
            this.textBoxMacroPreview.ReadOnly = true;
            this.textBoxMacroPreview.Size = new System.Drawing.Size(524, 20);
            this.textBoxMacroPreview.TabIndex = 15;
            this.textBoxMacroPreview.TabStop = false;
            this.textBoxMacroPreview.MouseHover += new System.EventHandler(this.textBoxMacroPreview_MouseHover);
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
            this.checkBoxActive.CheckedChanged += new System.EventHandler(this.updatePreviewImage);
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
            this.labelHelp.Size = new System.Drawing.Size(749, 17);
            this.labelHelp.TabIndex = 0;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonMacroTags
            // 
            this.buttonMacroTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMacroTags.Image = global::AutoScreenCapture.Properties.Resources.brick;
            this.buttonMacroTags.Location = new System.Drawing.Point(723, 420);
            this.buttonMacroTags.Name = "buttonMacroTags";
            this.buttonMacroTags.Size = new System.Drawing.Size(27, 23);
            this.buttonMacroTags.TabIndex = 21;
            this.buttonMacroTags.UseVisualStyleBackColor = true;
            this.buttonMacroTags.Click += new System.EventHandler(this.buttonMacroTags_Click);
            this.buttonMacroTags.MouseHover += new System.EventHandler(this.buttonMacroTags_MouseHover);
            // 
            // FormScreen
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(756, 454);
            this.Controls.Add(this.checkBoxActive);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonMacroTags);
            this.Controls.Add(this.labelHelp);
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
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(772, 493);
            this.Name = "FormScreen";
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
            this.groupBoxPreview.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.TextBox textBoxMacroPreview;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Button buttonMacroTags;
    }
}