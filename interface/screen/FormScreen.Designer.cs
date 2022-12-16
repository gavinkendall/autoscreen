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
            this.textBoxScreenName = new System.Windows.Forms.TextBox();
            this.labelMacro = new System.Windows.Forms.Label();
            this.textBoxMacro = new System.Windows.Forms.TextBox();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelFolder = new System.Windows.Forms.Label();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.buttonScreenBrowseFolder = new System.Windows.Forms.Button();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.numericUpDownJpegQuality = new System.Windows.Forms.NumericUpDown();
            this.labelJpegQuality = new System.Windows.Forms.Label();
            this.checkBoxMouse = new System.Windows.Forms.CheckBox();
            this.groupBoxImageAttributes = new System.Windows.Forms.GroupBox();
            this.labelImageDifferenceTolerance = new System.Windows.Forms.Label();
            this.numericUpDownImageDifferenceTolerance = new System.Windows.Forms.NumericUpDown();
            this.labelResolutionRatio = new System.Windows.Forms.Label();
            this.numericUpDownResolutionRatio = new System.Windows.Forms.NumericUpDown();
            this.groupBoxDisplayProperties = new System.Windows.Forms.GroupBox();
            this.comboBoxScreenCaptureMethod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAutoAdapt = new System.Windows.Forms.CheckBox();
            this.labelPositionAndSize = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.labelX = new System.Windows.Forms.Label();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.labelY = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelScreenComponent = new System.Windows.Forms.Label();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.labelScreenSource = new System.Windows.Forms.Label();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.comboBoxScreenSource = new System.Windows.Forms.ComboBox();
            this.comboBoxScreenComponent = new System.Windows.Forms.ComboBox();
            this.groupBoxPreview = new System.Windows.Forms.GroupBox();
            this.textBoxMacroPreview = new System.Windows.Forms.TextBox();
            this.checkBoxEnable = new System.Windows.Forms.CheckBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.buttonMacroTags = new System.Windows.Forms.Button();
            this.checkBoxEncrypt = new System.Windows.Forms.CheckBox();
            this.groupBoxSecurity = new System.Windows.Forms.GroupBox();
            this.labelSecurity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJpegQuality)).BeginInit();
            this.groupBoxImageAttributes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageDifferenceTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionRatio)).BeginInit();
            this.groupBoxDisplayProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            this.groupBoxPreview.SuspendLayout();
            this.groupBoxSecurity.SuspendLayout();
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
            // textBoxScreenName
            // 
            this.textBoxScreenName.Location = new System.Drawing.Point(56, 32);
            this.textBoxScreenName.MaxLength = 50;
            this.textBoxScreenName.Name = "textBoxScreenName";
            this.textBoxScreenName.Size = new System.Drawing.Size(546, 20);
            this.textBoxScreenName.TabIndex = 2;
            // 
            // labelMacro
            // 
            this.labelMacro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelMacro.AutoSize = true;
            this.labelMacro.Location = new System.Drawing.Point(229, 537);
            this.labelMacro.Name = "labelMacro";
            this.labelMacro.Size = new System.Drawing.Size(40, 13);
            this.labelMacro.TabIndex = 32;
            this.labelMacro.Text = "Macro:";
            // 
            // textBoxMacro
            // 
            this.textBoxMacro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMacro.Location = new System.Drawing.Point(273, 534);
            this.textBoxMacro.Name = "textBoxMacro";
            this.textBoxMacro.Size = new System.Drawing.Size(633, 20);
            this.textBoxMacro.TabIndex = 33;
            this.textBoxMacro.TextChanged += new System.EventHandler(this.updatePreviewMacro);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.Color.Black;
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(713, 423);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPreview.TabIndex = 5;
            this.pictureBoxPreview.TabStop = false;
            this.pictureBoxPreview.Click += new System.EventHandler(this.updatePreviewImage);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Location = new System.Drawing.Point(12, 532);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(99, 23);
            this.buttonOK.TabIndex = 35;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(117, 532);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 36;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelFolder
            // 
            this.labelFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFolder.AutoSize = true;
            this.labelFolder.Location = new System.Drawing.Point(229, 511);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(39, 13);
            this.labelFolder.TabIndex = 29;
            this.labelFolder.Text = "Folder:";
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolder.Location = new System.Drawing.Point(273, 508);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(633, 20);
            this.textBoxFolder.TabIndex = 30;
            this.textBoxFolder.TextChanged += new System.EventHandler(this.updatePreviewMacro);
            // 
            // buttonScreenBrowseFolder
            // 
            this.buttonScreenBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonScreenBrowseFolder.Image = global::AutoScreenCapture.Properties.Resources.openfolder;
            this.buttonScreenBrowseFolder.Location = new System.Drawing.Point(912, 506);
            this.buttonScreenBrowseFolder.Name = "buttonScreenBrowseFolder";
            this.buttonScreenBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonScreenBrowseFolder.TabIndex = 31;
            this.buttonScreenBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonScreenBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Location = new System.Drawing.Point(6, 19);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(51, 21);
            this.comboBoxFormat.TabIndex = 20;
            this.comboBoxFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormat_SelectedIndexChanged);
            // 
            // numericUpDownJpegQuality
            // 
            this.numericUpDownJpegQuality.Location = new System.Drawing.Point(148, 20);
            this.numericUpDownJpegQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownJpegQuality.Name = "numericUpDownJpegQuality";
            this.numericUpDownJpegQuality.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownJpegQuality.TabIndex = 22;
            this.numericUpDownJpegQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelJpegQuality
            // 
            this.labelJpegQuality.AutoSize = true;
            this.labelJpegQuality.Location = new System.Drawing.Point(70, 22);
            this.labelJpegQuality.Name = "labelJpegQuality";
            this.labelJpegQuality.Size = new System.Drawing.Size(72, 13);
            this.labelJpegQuality.TabIndex = 21;
            this.labelJpegQuality.Text = "JPEG Quality:";
            // 
            // checkBoxMouse
            // 
            this.checkBoxMouse.AutoSize = true;
            this.checkBoxMouse.Location = new System.Drawing.Point(6, 102);
            this.checkBoxMouse.Name = "checkBoxMouse";
            this.checkBoxMouse.Size = new System.Drawing.Size(130, 17);
            this.checkBoxMouse.TabIndex = 25;
            this.checkBoxMouse.Text = "Include mouse pointer";
            this.checkBoxMouse.UseVisualStyleBackColor = true;
            this.checkBoxMouse.CheckedChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // groupBoxImageAttributes
            // 
            this.groupBoxImageAttributes.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxImageAttributes.Controls.Add(this.labelImageDifferenceTolerance);
            this.groupBoxImageAttributes.Controls.Add(this.numericUpDownImageDifferenceTolerance);
            this.groupBoxImageAttributes.Controls.Add(this.labelResolutionRatio);
            this.groupBoxImageAttributes.Controls.Add(this.numericUpDownResolutionRatio);
            this.groupBoxImageAttributes.Controls.Add(this.checkBoxMouse);
            this.groupBoxImageAttributes.Controls.Add(this.labelJpegQuality);
            this.groupBoxImageAttributes.Controls.Add(this.numericUpDownJpegQuality);
            this.groupBoxImageAttributes.Controls.Add(this.comboBoxFormat);
            this.groupBoxImageAttributes.Location = new System.Drawing.Point(12, 283);
            this.groupBoxImageAttributes.Name = "groupBoxImageAttributes";
            this.groupBoxImageAttributes.Size = new System.Drawing.Size(205, 125);
            this.groupBoxImageAttributes.TabIndex = 18;
            this.groupBoxImageAttributes.TabStop = false;
            this.groupBoxImageAttributes.Text = "Image Attributes";
            // 
            // labelImageDifferenceTolerance
            // 
            this.labelImageDifferenceTolerance.AutoSize = true;
            this.labelImageDifferenceTolerance.Location = new System.Drawing.Point(6, 74);
            this.labelImageDifferenceTolerance.Name = "labelImageDifferenceTolerance";
            this.labelImageDifferenceTolerance.Size = new System.Drawing.Size(142, 13);
            this.labelImageDifferenceTolerance.TabIndex = 42;
            this.labelImageDifferenceTolerance.Text = "Image Difference Tolerance:";
            // 
            // numericUpDownImageDifferenceTolerance
            // 
            this.numericUpDownImageDifferenceTolerance.Location = new System.Drawing.Point(148, 72);
            this.numericUpDownImageDifferenceTolerance.Name = "numericUpDownImageDifferenceTolerance";
            this.numericUpDownImageDifferenceTolerance.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownImageDifferenceTolerance.TabIndex = 41;
            // 
            // labelResolutionRatio
            // 
            this.labelResolutionRatio.AutoSize = true;
            this.labelResolutionRatio.Location = new System.Drawing.Point(6, 48);
            this.labelResolutionRatio.Name = "labelResolutionRatio";
            this.labelResolutionRatio.Size = new System.Drawing.Size(88, 13);
            this.labelResolutionRatio.TabIndex = 28;
            this.labelResolutionRatio.Text = "Resolution Ratio:";
            // 
            // numericUpDownResolutionRatio
            // 
            this.numericUpDownResolutionRatio.Location = new System.Drawing.Point(148, 46);
            this.numericUpDownResolutionRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownResolutionRatio.Name = "numericUpDownResolutionRatio";
            this.numericUpDownResolutionRatio.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownResolutionRatio.TabIndex = 27;
            this.numericUpDownResolutionRatio.TabStop = false;
            this.numericUpDownResolutionRatio.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownResolutionRatio.ValueChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // groupBoxDisplayProperties
            // 
            this.groupBoxDisplayProperties.Controls.Add(this.comboBoxScreenCaptureMethod);
            this.groupBoxDisplayProperties.Controls.Add(this.label1);
            this.groupBoxDisplayProperties.Controls.Add(this.checkBoxAutoAdapt);
            this.groupBoxDisplayProperties.Controls.Add(this.labelPositionAndSize);
            this.groupBoxDisplayProperties.Controls.Add(this.labelWidth);
            this.groupBoxDisplayProperties.Controls.Add(this.numericUpDownHeight);
            this.groupBoxDisplayProperties.Controls.Add(this.labelX);
            this.groupBoxDisplayProperties.Controls.Add(this.numericUpDownWidth);
            this.groupBoxDisplayProperties.Controls.Add(this.labelY);
            this.groupBoxDisplayProperties.Controls.Add(this.labelHeight);
            this.groupBoxDisplayProperties.Controls.Add(this.labelScreenComponent);
            this.groupBoxDisplayProperties.Controls.Add(this.numericUpDownX);
            this.groupBoxDisplayProperties.Controls.Add(this.labelScreenSource);
            this.groupBoxDisplayProperties.Controls.Add(this.numericUpDownY);
            this.groupBoxDisplayProperties.Controls.Add(this.comboBoxScreenSource);
            this.groupBoxDisplayProperties.Controls.Add(this.comboBoxScreenComponent);
            this.groupBoxDisplayProperties.Location = new System.Drawing.Point(12, 58);
            this.groupBoxDisplayProperties.Name = "groupBoxDisplayProperties";
            this.groupBoxDisplayProperties.Size = new System.Drawing.Size(205, 219);
            this.groupBoxDisplayProperties.TabIndex = 4;
            this.groupBoxDisplayProperties.TabStop = false;
            this.groupBoxDisplayProperties.Text = "Display Properties";
            // 
            // comboBoxScreenCaptureMethod
            // 
            this.comboBoxScreenCaptureMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScreenCaptureMethod.DropDownWidth = 250;
            this.comboBoxScreenCaptureMethod.FormattingEnabled = true;
            this.comboBoxScreenCaptureMethod.Location = new System.Drawing.Point(98, 79);
            this.comboBoxScreenCaptureMethod.Name = "comboBoxScreenCaptureMethod";
            this.comboBoxScreenCaptureMethod.Size = new System.Drawing.Size(101, 21);
            this.comboBoxScreenCaptureMethod.TabIndex = 9;
            this.comboBoxScreenCaptureMethod.SelectedIndexChanged += new System.EventHandler(this.updatePositionAndSize);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Capture Method:";
            // 
            // checkBoxAutoAdapt
            // 
            this.checkBoxAutoAdapt.AutoSize = true;
            this.checkBoxAutoAdapt.Location = new System.Drawing.Point(9, 196);
            this.checkBoxAutoAdapt.Name = "checkBoxAutoAdapt";
            this.checkBoxAutoAdapt.Size = new System.Drawing.Size(194, 17);
            this.checkBoxAutoAdapt.TabIndex = 18;
            this.checkBoxAutoAdapt.Text = "Automatically adapt to display setup";
            this.checkBoxAutoAdapt.UseVisualStyleBackColor = true;
            this.checkBoxAutoAdapt.CheckedChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // labelPositionAndSize
            // 
            this.labelPositionAndSize.AutoSize = true;
            this.labelPositionAndSize.Location = new System.Drawing.Point(4, 110);
            this.labelPositionAndSize.Name = "labelPositionAndSize";
            this.labelPositionAndSize.Size = new System.Drawing.Size(78, 13);
            this.labelPositionAndSize.TabIndex = 9;
            this.labelPositionAndSize.Text = "Position / Size:";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(2, 168);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(38, 13);
            this.labelWidth.TabIndex = 14;
            this.labelWidth.Text = "Width:";
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.Location = new System.Drawing.Point(144, 166);
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
            this.numericUpDownHeight.TabIndex = 17;
            this.numericUpDownHeight.ValueChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(17, 136);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(17, 13);
            this.labelX.TabIndex = 10;
            this.labelX.Text = "X:";
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(40, 166);
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
            this.numericUpDownWidth.TabIndex = 15;
            this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(121, 136);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(17, 13);
            this.labelY.TabIndex = 12;
            this.labelY.Text = "Y:";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(97, 168);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(41, 13);
            this.labelHeight.TabIndex = 16;
            this.labelHeight.Text = "Height:";
            // 
            // labelScreenComponent
            // 
            this.labelScreenComponent.AutoSize = true;
            this.labelScreenComponent.Location = new System.Drawing.Point(6, 55);
            this.labelScreenComponent.Name = "labelScreenComponent";
            this.labelScreenComponent.Size = new System.Drawing.Size(64, 13);
            this.labelScreenComponent.TabIndex = 7;
            this.labelScreenComponent.Text = "Component:";
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Location = new System.Drawing.Point(40, 134);
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
            this.numericUpDownX.TabIndex = 11;
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // labelScreenSource
            // 
            this.labelScreenSource.AutoSize = true;
            this.labelScreenSource.Location = new System.Drawing.Point(6, 28);
            this.labelScreenSource.Name = "labelScreenSource";
            this.labelScreenSource.Size = new System.Drawing.Size(44, 13);
            this.labelScreenSource.TabIndex = 5;
            this.labelScreenSource.Text = "Source:";
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Location = new System.Drawing.Point(144, 134);
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
            this.numericUpDownY.TabIndex = 13;
            this.numericUpDownY.ValueChanged += new System.EventHandler(this.updatePreviewImage);
            // 
            // comboBoxScreenSource
            // 
            this.comboBoxScreenSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScreenSource.DropDownWidth = 250;
            this.comboBoxScreenSource.FormattingEnabled = true;
            this.comboBoxScreenSource.Location = new System.Drawing.Point(98, 25);
            this.comboBoxScreenSource.Name = "comboBoxScreenSource";
            this.comboBoxScreenSource.Size = new System.Drawing.Size(101, 21);
            this.comboBoxScreenSource.TabIndex = 6;
            this.comboBoxScreenSource.SelectedIndexChanged += new System.EventHandler(this.comboBoxScreenSource_SelectedIndexChanged);
            // 
            // comboBoxScreenComponent
            // 
            this.comboBoxScreenComponent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScreenComponent.DropDownWidth = 250;
            this.comboBoxScreenComponent.FormattingEnabled = true;
            this.comboBoxScreenComponent.Location = new System.Drawing.Point(98, 52);
            this.comboBoxScreenComponent.Name = "comboBoxScreenComponent";
            this.comboBoxScreenComponent.Size = new System.Drawing.Size(101, 21);
            this.comboBoxScreenComponent.TabIndex = 8;
            this.comboBoxScreenComponent.SelectedIndexChanged += new System.EventHandler(this.updatePositionAndSize);
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
            this.groupBoxPreview.Size = new System.Drawing.Size(719, 442);
            this.groupBoxPreview.TabIndex = 26;
            this.groupBoxPreview.TabStop = false;
            this.groupBoxPreview.Text = "Preview";
            // 
            // textBoxMacroPreview
            // 
            this.textBoxMacroPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxMacroPreview.Location = new System.Drawing.Point(3, 419);
            this.textBoxMacroPreview.Name = "textBoxMacroPreview";
            this.textBoxMacroPreview.ReadOnly = true;
            this.textBoxMacroPreview.Size = new System.Drawing.Size(713, 20);
            this.textBoxMacroPreview.TabIndex = 28;
            this.textBoxMacroPreview.TabStop = false;
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxEnable.AutoSize = true;
            this.checkBoxEnable.Location = new System.Drawing.Point(877, 34);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(59, 17);
            this.checkBoxEnable.TabIndex = 3;
            this.checkBoxEnable.Text = "Enable";
            this.checkBoxEnable.UseVisualStyleBackColor = true;
            this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.updatePreviewImage);
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
            this.labelHelp.Size = new System.Drawing.Size(941, 17);
            this.labelHelp.TabIndex = 0;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonMacroTags
            // 
            this.buttonMacroTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMacroTags.Image = global::AutoScreenCapture.Properties.Resources.lightbulb;
            this.buttonMacroTags.Location = new System.Drawing.Point(912, 532);
            this.buttonMacroTags.Name = "buttonMacroTags";
            this.buttonMacroTags.Size = new System.Drawing.Size(27, 23);
            this.buttonMacroTags.TabIndex = 34;
            this.buttonMacroTags.UseVisualStyleBackColor = true;
            this.buttonMacroTags.Click += new System.EventHandler(this.buttonMacroTags_Click);
            // 
            // checkBoxEncrypt
            // 
            this.checkBoxEncrypt.AutoSize = true;
            this.checkBoxEncrypt.Location = new System.Drawing.Point(9, 19);
            this.checkBoxEncrypt.Name = "checkBoxEncrypt";
            this.checkBoxEncrypt.Size = new System.Drawing.Size(122, 17);
            this.checkBoxEncrypt.TabIndex = 37;
            this.checkBoxEncrypt.Text = "Encrypt screenshots";
            this.checkBoxEncrypt.UseVisualStyleBackColor = true;
            // 
            // groupBoxSecurity
            // 
            this.groupBoxSecurity.Controls.Add(this.labelSecurity);
            this.groupBoxSecurity.Controls.Add(this.checkBoxEncrypt);
            this.groupBoxSecurity.Location = new System.Drawing.Point(12, 414);
            this.groupBoxSecurity.Name = "groupBoxSecurity";
            this.groupBoxSecurity.Size = new System.Drawing.Size(205, 86);
            this.groupBoxSecurity.TabIndex = 38;
            this.groupBoxSecurity.TabStop = false;
            this.groupBoxSecurity.Text = "Security";
            // 
            // labelSecurity
            // 
            this.labelSecurity.BackColor = System.Drawing.Color.LightYellow;
            this.labelSecurity.Location = new System.Drawing.Point(6, 37);
            this.labelSecurity.Name = "labelSecurity";
            this.labelSecurity.Size = new System.Drawing.Size(193, 46);
            this.labelSecurity.TabIndex = 38;
            this.labelSecurity.Text = "Applications will not be able to open encrypted screenshots. Anyone with the key " +
    "for a screenshot can decrypt it.";
            // 
            // FormScreen
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(945, 566);
            this.Controls.Add(this.groupBoxSecurity);
            this.Controls.Add(this.checkBoxEnable);
            this.Controls.Add(this.textBoxScreenName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonMacroTags);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.groupBoxPreview);
            this.Controls.Add(this.groupBoxDisplayProperties);
            this.Controls.Add(this.groupBoxImageAttributes);
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
            this.MinimumSize = new System.Drawing.Size(961, 605);
            this.Name = "FormScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormScreen_FormClosing);
            this.Load += new System.EventHandler(this.FormScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJpegQuality)).EndInit();
            this.groupBoxImageAttributes.ResumeLayout(false);
            this.groupBoxImageAttributes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageDifferenceTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolutionRatio)).EndInit();
            this.groupBoxDisplayProperties.ResumeLayout(false);
            this.groupBoxDisplayProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            this.groupBoxPreview.ResumeLayout(false);
            this.groupBoxPreview.PerformLayout();
            this.groupBoxSecurity.ResumeLayout(false);
            this.groupBoxSecurity.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxScreenName;
        private System.Windows.Forms.Label labelMacro;
        private System.Windows.Forms.TextBox textBoxMacro;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelFolder;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.Button buttonScreenBrowseFolder;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.NumericUpDown numericUpDownJpegQuality;
        private System.Windows.Forms.Label labelJpegQuality;
        private System.Windows.Forms.CheckBox checkBoxMouse;
        private System.Windows.Forms.GroupBox groupBoxImageAttributes;
        private System.Windows.Forms.GroupBox groupBoxDisplayProperties;
        private System.Windows.Forms.GroupBox groupBoxPreview;
        private System.Windows.Forms.ComboBox comboBoxScreenComponent;
        private System.Windows.Forms.CheckBox checkBoxEnable;
        private System.Windows.Forms.TextBox textBoxMacroPreview;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Button buttonMacroTags;
        private System.Windows.Forms.Label labelScreenComponent;
        private System.Windows.Forms.Label labelScreenSource;
        private System.Windows.Forms.ComboBox comboBoxScreenSource;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.Label labelPositionAndSize;
        private System.Windows.Forms.CheckBox checkBoxAutoAdapt;
        private System.Windows.Forms.ComboBox comboBoxScreenCaptureMethod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxEncrypt;
        private System.Windows.Forms.GroupBox groupBoxSecurity;
        private System.Windows.Forms.Label labelSecurity;
        private System.Windows.Forms.Label labelResolutionRatio;
        private System.Windows.Forms.NumericUpDown numericUpDownResolutionRatio;
        private System.Windows.Forms.Label labelImageDifferenceTolerance;
        private System.Windows.Forms.NumericUpDown numericUpDownImageDifferenceTolerance;
    }
}