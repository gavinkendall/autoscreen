namespace AutoScreenCapture
{
    /// <summary>
    /// A tool for people who are unfamiliar with Auto Screen Capture.
    /// </summary>
    partial class FormAutoScreenCaptureForBeginners
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutoScreenCaptureForBeginners));
            this.pictureBoxButYoureAPanda = new System.Windows.Forms.PictureBox();
            this.labelScreenshotsFolderHelp = new System.Windows.Forms.Label();
            this.textBoxScreenshotsFolder = new System.Windows.Forms.TextBox();
            this.labelScreenshotsFolder = new System.Windows.Forms.Label();
            this.buttonScreenshotsFolderBrowseFolder = new System.Windows.Forms.Button();
            this.labelInterval = new System.Windows.Forms.Label();
            this.labelHoursInterval = new System.Windows.Forms.Label();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.labelMinutesInterval = new System.Windows.Forms.Label();
            this.labelSecondsInterval = new System.Windows.Forms.Label();
            this.labelIntervalHelp = new System.Windows.Forms.Label();
            this.buttonStartScreenCapture = new System.Windows.Forms.Button();
            this.buttonStopScreenCapture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButYoureAPanda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxButYoureAPanda
            // 
            this.pictureBoxButYoureAPanda.BackColor = System.Drawing.Color.Black;
            this.pictureBoxButYoureAPanda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxButYoureAPanda.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxButYoureAPanda.Image")));
            this.pictureBoxButYoureAPanda.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxButYoureAPanda.Name = "pictureBoxButYoureAPanda";
            this.pictureBoxButYoureAPanda.Size = new System.Drawing.Size(267, 426);
            this.pictureBoxButYoureAPanda.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxButYoureAPanda.TabIndex = 0;
            this.pictureBoxButYoureAPanda.TabStop = false;
            // 
            // labelScreenshotsFolderHelp
            // 
            this.labelScreenshotsFolderHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelScreenshotsFolderHelp.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScreenshotsFolderHelp.Location = new System.Drawing.Point(285, 42);
            this.labelScreenshotsFolderHelp.Name = "labelScreenshotsFolderHelp";
            this.labelScreenshotsFolderHelp.Size = new System.Drawing.Size(504, 40);
            this.labelScreenshotsFolderHelp.TabIndex = 34;
            this.labelScreenshotsFolderHelp.Text = "This is the folder where all of your screenshots will be saved. Use the yellow fo" +
    "lder button to change it.";
            // 
            // textBoxScreenshotsFolder
            // 
            this.textBoxScreenshotsFolder.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxScreenshotsFolder.Location = new System.Drawing.Point(285, 88);
            this.textBoxScreenshotsFolder.Name = "textBoxScreenshotsFolder";
            this.textBoxScreenshotsFolder.Size = new System.Drawing.Size(470, 25);
            this.textBoxScreenshotsFolder.TabIndex = 35;
            this.textBoxScreenshotsFolder.TabStop = false;
            // 
            // labelScreenshotsFolder
            // 
            this.labelScreenshotsFolder.AutoSize = true;
            this.labelScreenshotsFolder.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScreenshotsFolder.Location = new System.Drawing.Point(285, 12);
            this.labelScreenshotsFolder.Name = "labelScreenshotsFolder";
            this.labelScreenshotsFolder.Size = new System.Drawing.Size(152, 18);
            this.labelScreenshotsFolder.TabIndex = 36;
            this.labelScreenshotsFolder.Text = "Screenshots Folder";
            // 
            // buttonScreenshotsFolderBrowseFolder
            // 
            this.buttonScreenshotsFolderBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonScreenshotsFolderBrowseFolder.Image = global::AutoScreenCapture.Properties.Resources.openfolder;
            this.buttonScreenshotsFolderBrowseFolder.Location = new System.Drawing.Point(761, 89);
            this.buttonScreenshotsFolderBrowseFolder.Name = "buttonScreenshotsFolderBrowseFolder";
            this.buttonScreenshotsFolderBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonScreenshotsFolderBrowseFolder.TabIndex = 37;
            this.buttonScreenshotsFolderBrowseFolder.TabStop = false;
            this.buttonScreenshotsFolderBrowseFolder.UseVisualStyleBackColor = true;
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInterval.Location = new System.Drawing.Point(285, 136);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(72, 18);
            this.labelInterval.TabIndex = 38;
            this.labelInterval.Text = "Interval";
            // 
            // labelHoursInterval
            // 
            this.labelHoursInterval.AutoSize = true;
            this.labelHoursInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHoursInterval.Location = new System.Drawing.Point(336, 214);
            this.labelHoursInterval.Name = "labelHoursInterval";
            this.labelHoursInterval.Size = new System.Drawing.Size(48, 18);
            this.labelHoursInterval.TabIndex = 39;
            this.labelHoursInterval.Text = "hours";
            // 
            // numericUpDownHoursInterval
            // 
            this.numericUpDownHoursInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(288, 212);
            this.numericUpDownHoursInterval.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownHoursInterval.Name = "numericUpDownHoursInterval";
            this.numericUpDownHoursInterval.Size = new System.Drawing.Size(42, 25);
            this.numericUpDownHoursInterval.TabIndex = 40;
            this.numericUpDownHoursInterval.TabStop = false;
            // 
            // numericUpDownMinutesInterval
            // 
            this.numericUpDownMinutesInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(406, 212);
            this.numericUpDownMinutesInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownMinutesInterval.Name = "numericUpDownMinutesInterval";
            this.numericUpDownMinutesInterval.Size = new System.Drawing.Size(42, 25);
            this.numericUpDownMinutesInterval.TabIndex = 41;
            this.numericUpDownMinutesInterval.TabStop = false;
            // 
            // numericUpDownSecondsInterval
            // 
            this.numericUpDownSecondsInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(537, 212);
            this.numericUpDownSecondsInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSecondsInterval.Name = "numericUpDownSecondsInterval";
            this.numericUpDownSecondsInterval.Size = new System.Drawing.Size(42, 25);
            this.numericUpDownSecondsInterval.TabIndex = 42;
            this.numericUpDownSecondsInterval.TabStop = false;
            // 
            // labelMinutesInterval
            // 
            this.labelMinutesInterval.AutoSize = true;
            this.labelMinutesInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinutesInterval.Location = new System.Drawing.Point(454, 214);
            this.labelMinutesInterval.Name = "labelMinutesInterval";
            this.labelMinutesInterval.Size = new System.Drawing.Size(64, 18);
            this.labelMinutesInterval.TabIndex = 43;
            this.labelMinutesInterval.Text = "minutes";
            // 
            // labelSecondsInterval
            // 
            this.labelSecondsInterval.AutoSize = true;
            this.labelSecondsInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSecondsInterval.Location = new System.Drawing.Point(585, 214);
            this.labelSecondsInterval.Name = "labelSecondsInterval";
            this.labelSecondsInterval.Size = new System.Drawing.Size(64, 18);
            this.labelSecondsInterval.TabIndex = 44;
            this.labelSecondsInterval.Text = "seconds";
            // 
            // labelIntervalHelp
            // 
            this.labelIntervalHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelIntervalHelp.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIntervalHelp.Location = new System.Drawing.Point(285, 165);
            this.labelIntervalHelp.Name = "labelIntervalHelp";
            this.labelIntervalHelp.Size = new System.Drawing.Size(504, 40);
            this.labelIntervalHelp.TabIndex = 45;
            this.labelIntervalHelp.Text = "How slow or fast you want screenshots to be taken.";
            // 
            // buttonStartScreenCapture
            // 
            this.buttonStartScreenCapture.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonStartScreenCapture.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartScreenCapture.Location = new System.Drawing.Point(285, 392);
            this.buttonStartScreenCapture.Name = "buttonStartScreenCapture";
            this.buttonStartScreenCapture.Size = new System.Drawing.Size(249, 46);
            this.buttonStartScreenCapture.TabIndex = 46;
            this.buttonStartScreenCapture.TabStop = false;
            this.buttonStartScreenCapture.Text = "Start Screen Capture";
            this.buttonStartScreenCapture.UseVisualStyleBackColor = false;
            // 
            // buttonStopScreenCapture
            // 
            this.buttonStopScreenCapture.BackColor = System.Drawing.Color.PaleVioletRed;
            this.buttonStopScreenCapture.Enabled = false;
            this.buttonStopScreenCapture.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStopScreenCapture.Location = new System.Drawing.Point(540, 392);
            this.buttonStopScreenCapture.Name = "buttonStopScreenCapture";
            this.buttonStopScreenCapture.Size = new System.Drawing.Size(249, 46);
            this.buttonStopScreenCapture.TabIndex = 47;
            this.buttonStopScreenCapture.TabStop = false;
            this.buttonStopScreenCapture.Text = "Stop Screen Capture";
            this.buttonStopScreenCapture.UseVisualStyleBackColor = false;
            // 
            // FormAutoScreenCaptureForBeginners
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonStopScreenCapture);
            this.Controls.Add(this.buttonStartScreenCapture);
            this.Controls.Add(this.labelIntervalHelp);
            this.Controls.Add(this.labelHoursInterval);
            this.Controls.Add(this.numericUpDownHoursInterval);
            this.Controls.Add(this.numericUpDownMinutesInterval);
            this.Controls.Add(this.numericUpDownSecondsInterval);
            this.Controls.Add(this.labelMinutesInterval);
            this.Controls.Add(this.labelSecondsInterval);
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.buttonScreenshotsFolderBrowseFolder);
            this.Controls.Add(this.labelScreenshotsFolder);
            this.Controls.Add(this.textBoxScreenshotsFolder);
            this.Controls.Add(this.labelScreenshotsFolderHelp);
            this.Controls.Add(this.pictureBoxButYoureAPanda);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAutoScreenCaptureForBeginners";
            this.Text = "Auto Screen Capture For Beginners";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAutoScreenCaptureForBeginners_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButYoureAPanda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxButYoureAPanda;
        private System.Windows.Forms.Label labelScreenshotsFolderHelp;
        private System.Windows.Forms.TextBox textBoxScreenshotsFolder;
        private System.Windows.Forms.Label labelScreenshotsFolder;
        private System.Windows.Forms.Button buttonScreenshotsFolderBrowseFolder;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.Label labelHoursInterval;
        public System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;
        public System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;
        public System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;
        private System.Windows.Forms.Label labelMinutesInterval;
        private System.Windows.Forms.Label labelSecondsInterval;
        private System.Windows.Forms.Label labelIntervalHelp;
        private System.Windows.Forms.Button buttonStartScreenCapture;
        private System.Windows.Forms.Button buttonStopScreenCapture;
    }
}