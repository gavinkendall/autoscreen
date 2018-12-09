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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.timerRegionPreview = new System.Windows.Forms.Timer(this.components);
            this.labelRegionPreview = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRegionPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // labelRegionName
            // 
            this.labelRegionName.AutoSize = true;
            this.labelRegionName.Location = new System.Drawing.Point(12, 14);
            this.labelRegionName.Name = "labelRegionName";
            this.labelRegionName.Size = new System.Drawing.Size(38, 13);
            this.labelRegionName.TabIndex = 0;
            this.labelRegionName.Text = "Name:";
            // 
            // textBoxRegionName
            // 
            this.textBoxRegionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRegionName.Location = new System.Drawing.Point(80, 11);
            this.textBoxRegionName.MaxLength = 50;
            this.textBoxRegionName.Name = "textBoxRegionName";
            this.textBoxRegionName.Size = new System.Drawing.Size(456, 20);
            this.textBoxRegionName.TabIndex = 1;
            // 
            // labelRegionMacro
            // 
            this.labelRegionMacro.AutoSize = true;
            this.labelRegionMacro.Location = new System.Drawing.Point(12, 40);
            this.labelRegionMacro.Name = "labelRegionMacro";
            this.labelRegionMacro.Size = new System.Drawing.Size(40, 13);
            this.labelRegionMacro.TabIndex = 0;
            this.labelRegionMacro.Text = "Macro:";
            // 
            // textBoxRegionMacro
            // 
            this.textBoxRegionMacro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRegionMacro.Location = new System.Drawing.Point(80, 37);
            this.textBoxRegionMacro.Name = "textBoxRegionMacro";
            this.textBoxRegionMacro.Size = new System.Drawing.Size(456, 20);
            this.textBoxRegionMacro.TabIndex = 2;
            // 
            // pictureBoxRegionPreview
            // 
            this.pictureBoxRegionPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxRegionPreview.BackColor = System.Drawing.Color.Black;
            this.pictureBoxRegionPreview.Location = new System.Drawing.Point(12, 96);
            this.pictureBoxRegionPreview.Name = "pictureBoxRegionPreview";
            this.pictureBoxRegionPreview.Size = new System.Drawing.Size(620, 332);
            this.pictureBoxRegionPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRegionPreview.TabIndex = 5;
            this.pictureBoxRegionPreview.TabStop = false;
            // 
            // labelRegionX
            // 
            this.labelRegionX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRegionX.AutoSize = true;
            this.labelRegionX.Location = new System.Drawing.Point(201, 72);
            this.labelRegionX.Name = "labelRegionX";
            this.labelRegionX.Size = new System.Drawing.Size(17, 13);
            this.labelRegionX.TabIndex = 0;
            this.labelRegionX.Text = "X:";
            // 
            // labelRegionY
            // 
            this.labelRegionY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRegionY.AutoSize = true;
            this.labelRegionY.Location = new System.Drawing.Point(275, 72);
            this.labelRegionY.Name = "labelRegionY";
            this.labelRegionY.Size = new System.Drawing.Size(17, 13);
            this.labelRegionY.TabIndex = 0;
            this.labelRegionY.Text = "Y:";
            // 
            // labelRegionWidth
            // 
            this.labelRegionWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRegionWidth.AutoSize = true;
            this.labelRegionWidth.Location = new System.Drawing.Point(349, 72);
            this.labelRegionWidth.Name = "labelRegionWidth";
            this.labelRegionWidth.Size = new System.Drawing.Size(38, 13);
            this.labelRegionWidth.TabIndex = 0;
            this.labelRegionWidth.Text = "Width:";
            // 
            // labelRegionHeight
            // 
            this.labelRegionHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRegionHeight.AutoSize = true;
            this.labelRegionHeight.Location = new System.Drawing.Point(444, 72);
            this.labelRegionHeight.Name = "labelRegionHeight";
            this.labelRegionHeight.Size = new System.Drawing.Size(41, 13);
            this.labelRegionHeight.TabIndex = 0;
            this.labelRegionHeight.Text = "Height:";
            // 
            // numericUpDownRegionX
            // 
            this.numericUpDownRegionX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownRegionX.Location = new System.Drawing.Point(224, 70);
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
            this.numericUpDownRegionX.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownRegionX.TabIndex = 3;
            // 
            // numericUpDownRegionY
            // 
            this.numericUpDownRegionY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownRegionY.Location = new System.Drawing.Point(298, 70);
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
            this.numericUpDownRegionY.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownRegionY.TabIndex = 4;
            // 
            // numericUpDownRegionWidth
            // 
            this.numericUpDownRegionWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownRegionWidth.Location = new System.Drawing.Point(393, 70);
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
            this.numericUpDownRegionWidth.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownRegionWidth.TabIndex = 5;
            // 
            // numericUpDownRegionHeight
            // 
            this.numericUpDownRegionHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownRegionHeight.Location = new System.Drawing.Point(491, 70);
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
            this.numericUpDownRegionHeight.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownRegionHeight.TabIndex = 6;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(557, 9);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.Click_buttonOK);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(557, 40);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.Click_buttonCancel);
            // 
            // timerRegionPreview
            // 
            this.timerRegionPreview.Enabled = true;
            this.timerRegionPreview.Interval = 500;
            this.timerRegionPreview.Tick += new System.EventHandler(this.Tick_timerRegionPreview);
            // 
            // labelRegionPreview
            // 
            this.labelRegionPreview.AutoSize = true;
            this.labelRegionPreview.Location = new System.Drawing.Point(12, 72);
            this.labelRegionPreview.Name = "labelRegionPreview";
            this.labelRegionPreview.Size = new System.Drawing.Size(48, 13);
            this.labelRegionPreview.TabIndex = 9;
            this.labelRegionPreview.Text = "Preview:";
            // 
            // FormRegion
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(644, 440);
            this.Controls.Add(this.labelRegionPreview);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.numericUpDownRegionHeight);
            this.Controls.Add(this.numericUpDownRegionWidth);
            this.Controls.Add(this.numericUpDownRegionY);
            this.Controls.Add(this.numericUpDownRegionX);
            this.Controls.Add(this.labelRegionHeight);
            this.Controls.Add(this.labelRegionWidth);
            this.Controls.Add(this.labelRegionY);
            this.Controls.Add(this.labelRegionX);
            this.Controls.Add(this.pictureBoxRegionPreview);
            this.Controls.Add(this.textBoxRegionMacro);
            this.Controls.Add(this.labelRegionMacro);
            this.Controls.Add(this.textBoxRegionName);
            this.Controls.Add(this.labelRegionName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(660, 479);
            this.Name = "FormRegion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormRegion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRegionPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegionHeight)).EndInit();
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
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Timer timerRegionPreview;
        private System.Windows.Forms.Label labelRegionPreview;
    }
}