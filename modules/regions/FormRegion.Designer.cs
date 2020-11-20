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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegion));
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxRegionName = new System.Windows.Forms.TextBox();
            this.labelMacro = new System.Windows.Forms.Label();
            this.textBoxMacro = new System.Windows.Forms.TextBox();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelFolder = new System.Windows.Forms.Label();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.buttonBrowseFolder = new System.Windows.Forms.Button();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.labelFormat = new System.Windows.Forms.Label();
            this.numericUpDownResolutionRatio = new System.Windows.Forms.NumericUpDown();
            this.labelResolutionRatio = new System.Windows.Forms.Label();
            this.numericUpDownJpegQuality = new System.Windows.Forms.NumericUpDown();
            this.labelJpegQuality = new System.Windows.Forms.Label();
            this.checkBoxMouse = new System.Windows.Forms.CheckBox();
            this.groupBoxImageAttributes = new System.Windows.Forms.GroupBox();
            this.groupBoxPosition = new System.Windows.Forms.GroupBox();
            this.groupBoxSize = new System.Windows.Forms.GroupBox();
            this.groupBoxPreview = new System.Windows.Forms.GroupBox();
            this.textBoxMacroPreview = new System.Windows.Forms.TextBox();
            this.groupBoxScreenTemplate = new System.Windows.Forms.GroupBox();
            this.comboBoxScreenTemplate = new System.Windows.Forms.ComboBox();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.buttonMacroTags = new System.Windows.Forms.Button();
            this.buttonRegionSelect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJpegQuality)).BeginInit();
            this.groupBoxImageAttributes.SuspendLayout();
            this.groupBoxPosition.SuspendLayout();
            this.groupBoxSize.SuspendLayout();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxScreenTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(9, 35);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Name:";
            // 
            // textBoxRegionName
            // 
            this.textBoxRegionName.Location = new System.Drawing.Point(56, 32);
            this.textBoxRegionName.MaxLength = 50;
            this.textBoxRegionName.Name = "textBoxRegionName";
            this.textBoxRegionName.Size = new System.Drawing.Size(546, 20);
            this.textBoxRegionName.TabIndex = 2;
            // 
            // labelMacro
            // 
            this.labelMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelMacro.AutoSize = true;
            this.labelMacro.Location = new System.Drawing.Point(229, 425);
            this.labelMacro.Name = "labelMacro";
            this.labelMacro.Size = new System.Drawing.Size(40, 13);
            this.labelMacro.TabIndex = 32;
            this.labelMacro.Text = "Macro:";
            // 
            // textBoxMacro
            // 
            this.textBoxMacro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMacro.Location = new System.Drawing.Point(273, 422);
            this.textBoxMacro.Name = "textBoxMacro";
            this.textBoxMacro.Size = new System.Drawing.Size(444, 20);
            this.textBoxMacro.TabIndex = 33;
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
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(21, 22);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(17, 13);
            this.labelX.TabIndex = 6;
            this.labelX.Text = "X:";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(125, 22);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(17, 13);
            this.labelY.TabIndex = 8;
            this.labelY.Text = "Y:";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(6, 22);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(38, 13);
            this.labelWidth.TabIndex = 11;
            this.labelWidth.Text = "Width:";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(101, 22);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(41, 13);
            this.labelHeight.TabIndex = 13;
            this.labelHeight.Text = "Height:";
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Location = new System.Drawing.Point(44, 20);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownX.TabIndex = 7;
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Location = new System.Drawing.Point(148, 20);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownY.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownY.TabIndex = 9;
            this.numericUpDownY.ValueChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(44, 20);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownWidth.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownWidth.TabIndex = 12;
            this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.Location = new System.Drawing.Point(148, 20);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            0});
            this.numericUpDownHeight.Minimum = new decimal(new int[] {
            -402653185,
            -1613725636,
            54210108,
            -2147483648});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownHeight.TabIndex = 14;
            this.numericUpDownHeight.ValueChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Location = new System.Drawing.Point(12, 420);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(99, 23);
            this.buttonOK.TabIndex = 35;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonRegionOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(117, 420);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 36;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonRegionCancel_Click);
            // 
            // labelFolder
            // 
            this.labelFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFolder.AutoSize = true;
            this.labelFolder.Location = new System.Drawing.Point(229, 399);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(39, 13);
            this.labelFolder.TabIndex = 29;
            this.labelFolder.Text = "Folder:";
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolder.Location = new System.Drawing.Point(273, 396);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(444, 20);
            this.textBoxFolder.TabIndex = 30;
            this.textBoxFolder.TextChanged += new System.EventHandler(this.updatePreviewMacro);
            this.textBoxFolder.MouseHover += new System.EventHandler(this.textBoxFolder_MouseHover);
            // 
            // buttonBrowseFolder
            // 
            this.buttonBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseFolder.Image = global::AutoScreenCapture.Properties.Resources.openfolder;
            this.buttonBrowseFolder.Location = new System.Drawing.Point(723, 394);
            this.buttonBrowseFolder.Name = "buttonBrowseFolder";
            this.buttonBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonBrowseFolder.TabIndex = 31;
            this.buttonBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonRegionBrowseFolder_Click);
            this.buttonBrowseFolder.MouseHover += new System.EventHandler(this.buttonBrowseFolder_MouseHover);
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Location = new System.Drawing.Point(148, 22);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(51, 21);
            this.comboBoxFormat.TabIndex = 19;
            this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxRegionFormat_SelectedIndexChanged);
            this.comboBoxFormat.MouseHover += new System.EventHandler(this.comboBoxFormat_MouseHover);
            // 
            // labelFormat
            // 
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new System.Drawing.Point(6, 25);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new System.Drawing.Size(42, 13);
            this.labelFormat.TabIndex = 18;
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
            this.numericUpDownResolutionRatio.TabIndex = 23;
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
            this.labelResolutionRatio.TabIndex = 22;
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
            this.numericUpDownJpegQuality.TabIndex = 21;
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
            this.labelJpegQuality.TabIndex = 20;
            this.labelJpegQuality.Text = "JPEG Quality:";
            // 
            // checkBoxMouse
            // 
            this.checkBoxMouse.AutoSize = true;
            this.checkBoxMouse.Location = new System.Drawing.Point(9, 102);
            this.checkBoxMouse.Name = "checkBoxMouse";
            this.checkBoxMouse.Size = new System.Drawing.Size(130, 17);
            this.checkBoxMouse.TabIndex = 24;
            this.checkBoxMouse.Text = "Include mouse pointer";
            this.checkBoxMouse.UseVisualStyleBackColor = true;
            this.checkBoxMouse.CheckedChanged += new System.EventHandler(this.updatePreviewImage);
            this.checkBoxMouse.MouseHover += new System.EventHandler(this.checkBoxMouse_MouseHover);
            // 
            // groupBoxImageAttributes
            // 
            this.groupBoxImageAttributes.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxImageAttributes.Controls.Add(this.labelResolutionRatio);
            this.groupBoxImageAttributes.Controls.Add(this.numericUpDownResolutionRatio);
            this.groupBoxImageAttributes.Controls.Add(this.labelFormat);
            this.groupBoxImageAttributes.Controls.Add(this.checkBoxMouse);
            this.groupBoxImageAttributes.Controls.Add(this.labelJpegQuality);
            this.groupBoxImageAttributes.Controls.Add(this.numericUpDownJpegQuality);
            this.groupBoxImageAttributes.Controls.Add(this.comboBoxFormat);
            this.groupBoxImageAttributes.Location = new System.Drawing.Point(12, 262);
            this.groupBoxImageAttributes.Name = "groupBoxImageAttributes";
            this.groupBoxImageAttributes.Size = new System.Drawing.Size(205, 126);
            this.groupBoxImageAttributes.TabIndex = 17;
            this.groupBoxImageAttributes.TabStop = false;
            this.groupBoxImageAttributes.Text = "Image Attributes";
            // 
            // groupBoxPosition
            // 
            this.groupBoxPosition.Controls.Add(this.labelX);
            this.groupBoxPosition.Controls.Add(this.labelY);
            this.groupBoxPosition.Controls.Add(this.numericUpDownX);
            this.groupBoxPosition.Controls.Add(this.numericUpDownY);
            this.groupBoxPosition.Location = new System.Drawing.Point(12, 103);
            this.groupBoxPosition.Name = "groupBoxPosition";
            this.groupBoxPosition.Size = new System.Drawing.Size(205, 46);
            this.groupBoxPosition.TabIndex = 5;
            this.groupBoxPosition.TabStop = false;
            this.groupBoxPosition.Text = "Position";
            // 
            // groupBoxSize
            // 
            this.groupBoxSize.Controls.Add(this.labelWidth);
            this.groupBoxSize.Controls.Add(this.numericUpDownHeight);
            this.groupBoxSize.Controls.Add(this.numericUpDownWidth);
            this.groupBoxSize.Controls.Add(this.labelHeight);
            this.groupBoxSize.Location = new System.Drawing.Point(12, 152);
            this.groupBoxSize.Name = "groupBoxSize";
            this.groupBoxSize.Size = new System.Drawing.Size(205, 46);
            this.groupBoxSize.TabIndex = 10;
            this.groupBoxSize.TabStop = false;
            this.groupBoxSize.Text = "Size";
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
            this.groupBoxPreview.TabIndex = 27;
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
            this.textBoxMacroPreview.TabIndex = 28;
            this.textBoxMacroPreview.TabStop = false;
            this.textBoxMacroPreview.MouseHover += new System.EventHandler(this.textBoxMacroPreview_MouseHover);
            // 
            // groupBoxScreenTemplate
            // 
            this.groupBoxScreenTemplate.Controls.Add(this.comboBoxScreenTemplate);
            this.groupBoxScreenTemplate.Location = new System.Drawing.Point(12, 205);
            this.groupBoxScreenTemplate.Name = "groupBoxScreenTemplate";
            this.groupBoxScreenTemplate.Size = new System.Drawing.Size(205, 47);
            this.groupBoxScreenTemplate.TabIndex = 15;
            this.groupBoxScreenTemplate.TabStop = false;
            this.groupBoxScreenTemplate.Text = "Import Screen Dimensions";
            // 
            // comboBoxScreenTemplate
            // 
            this.comboBoxScreenTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScreenTemplate.FormattingEnabled = true;
            this.comboBoxScreenTemplate.Location = new System.Drawing.Point(6, 19);
            this.comboBoxScreenTemplate.Name = "comboBoxScreenTemplate";
            this.comboBoxScreenTemplate.Size = new System.Drawing.Size(193, 21);
            this.comboBoxScreenTemplate.TabIndex = 16;
            this.comboBoxScreenTemplate.SelectedIndexChanged += new System.EventHandler(this.comboBoxRegionScreenTemplate_SelectedIndexChanged);
            this.comboBoxScreenTemplate.MouseHover += new System.EventHandler(this.comboBoxScreenTemplate_MouseHover);
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
            this.labelHelp.Size = new System.Drawing.Size(752, 17);
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
            this.buttonMacroTags.TabIndex = 34;
            this.buttonMacroTags.UseVisualStyleBackColor = true;
            this.buttonMacroTags.Click += new System.EventHandler(this.buttonMacroTags_Click);
            this.buttonMacroTags.MouseHover += new System.EventHandler(this.buttonMacroTags_MouseHover);
            // 
            // buttonRegionSelect
            // 
            this.buttonRegionSelect.Image = global::AutoScreenCapture.Properties.Resources.region_select;
            this.buttonRegionSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRegionSelect.Location = new System.Drawing.Point(12, 69);
            this.buttonRegionSelect.Name = "buttonRegionSelect";
            this.buttonRegionSelect.Size = new System.Drawing.Size(205, 23);
            this.buttonRegionSelect.TabIndex = 4;
            this.buttonRegionSelect.Text = "Region Select";
            this.buttonRegionSelect.UseVisualStyleBackColor = true;
            this.buttonRegionSelect.Click += new System.EventHandler(this.buttonRegionSelect_Click);
            // 
            // FormRegion
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(756, 454);
            this.Controls.Add(this.buttonRegionSelect);
            this.Controls.Add(this.buttonMacroTags);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.checkBoxActive);
            this.Controls.Add(this.groupBoxScreenTemplate);
            this.Controls.Add(this.groupBoxPreview);
            this.Controls.Add(this.groupBoxSize);
            this.Controls.Add(this.groupBoxPosition);
            this.Controls.Add(this.groupBoxImageAttributes);
            this.Controls.Add(this.buttonBrowseFolder);
            this.Controls.Add(this.textBoxFolder);
            this.Controls.Add(this.labelFolder);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxMacro);
            this.Controls.Add(this.labelMacro);
            this.Controls.Add(this.textBoxRegionName);
            this.Controls.Add(this.labelName);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(772, 493);
            this.Name = "FormRegion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRegion_FormClosing);
            this.Load += new System.EventHandler(this.FormRegion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJpegQuality)).EndInit();
            this.groupBoxImageAttributes.ResumeLayout(false);
            this.groupBoxImageAttributes.PerformLayout();
            this.groupBoxPosition.ResumeLayout(false);
            this.groupBoxPosition.PerformLayout();
            this.groupBoxSize.ResumeLayout(false);
            this.groupBoxSize.PerformLayout();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.groupBoxScreenTemplate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxRegionName;
        private System.Windows.Forms.Label labelMacro;
        private System.Windows.Forms.TextBox textBoxMacro;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelFolder;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.Button buttonBrowseFolder;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.Label labelFormat;
        private System.Windows.Forms.NumericUpDown numericUpDownResolutionRatio;
        private System.Windows.Forms.Label labelResolutionRatio;
        private System.Windows.Forms.NumericUpDown numericUpDownJpegQuality;
        private System.Windows.Forms.Label labelJpegQuality;
        private System.Windows.Forms.CheckBox checkBoxMouse;
        private System.Windows.Forms.GroupBox groupBoxImageAttributes;
        private System.Windows.Forms.GroupBox groupBoxPosition;
        private System.Windows.Forms.GroupBox groupBoxSize;
        private System.Windows.Forms.GroupBox groupBoxPreview;
        private System.Windows.Forms.GroupBox groupBoxScreenTemplate;
        private System.Windows.Forms.ComboBox comboBoxScreenTemplate;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.TextBox textBoxMacroPreview;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Button buttonMacroTags;
        private System.Windows.Forms.Button buttonRegionSelect;
    }
}