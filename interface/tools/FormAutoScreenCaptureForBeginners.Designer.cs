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
            this.labelFilenamePattern = new System.Windows.Forms.Label();
            this.textBoxFilenamePattern = new System.Windows.Forms.TextBox();
            this.labelFilenamePatternHelp = new System.Windows.Forms.Label();
            this.buttonExitApplication = new System.Windows.Forms.Button();
            this.checkBoxShowScreenCaptureStatusOnStart = new System.Windows.Forms.CheckBox();
            this.checkBoxShowScreenshotsFolderOnStop = new System.Windows.Forms.CheckBox();
            this.numericUpDownMillisecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.labelMillisecondsInterval = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButYoureAPanda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).BeginInit();
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
            this.labelScreenshotsFolderHelp.Size = new System.Drawing.Size(537, 40);
            this.labelScreenshotsFolderHelp.TabIndex = 34;
            this.labelScreenshotsFolderHelp.Text = "This is the folder where all of your screenshots will be saved. It will replace a" +
    "ll folder paths for every Screen and Region.";
            // 
            // textBoxScreenshotsFolder
            // 
            this.textBoxScreenshotsFolder.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxScreenshotsFolder.Location = new System.Drawing.Point(285, 88);
            this.textBoxScreenshotsFolder.Name = "textBoxScreenshotsFolder";
            this.textBoxScreenshotsFolder.Size = new System.Drawing.Size(504, 25);
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
            this.buttonScreenshotsFolderBrowseFolder.Location = new System.Drawing.Point(796, 89);
            this.buttonScreenshotsFolderBrowseFolder.Name = "buttonScreenshotsFolderBrowseFolder";
            this.buttonScreenshotsFolderBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonScreenshotsFolderBrowseFolder.TabIndex = 37;
            this.buttonScreenshotsFolderBrowseFolder.TabStop = false;
            this.buttonScreenshotsFolderBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonScreenshotsFolderBrowseFolder.Click += new System.EventHandler(this.buttonScreenshotsFolderBrowseFolder_Click);
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInterval.Location = new System.Drawing.Point(285, 266);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(72, 18);
            this.labelInterval.TabIndex = 38;
            this.labelInterval.Text = "Interval";
            // 
            // labelHoursInterval
            // 
            this.labelHoursInterval.AutoSize = true;
            this.labelHoursInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHoursInterval.Location = new System.Drawing.Point(336, 344);
            this.labelHoursInterval.Name = "labelHoursInterval";
            this.labelHoursInterval.Size = new System.Drawing.Size(48, 18);
            this.labelHoursInterval.TabIndex = 39;
            this.labelHoursInterval.Text = "hours";
            // 
            // numericUpDownHoursInterval
            // 
            this.numericUpDownHoursInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(288, 342);
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
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(406, 342);
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
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(537, 342);
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
            this.labelMinutesInterval.Location = new System.Drawing.Point(454, 344);
            this.labelMinutesInterval.Name = "labelMinutesInterval";
            this.labelMinutesInterval.Size = new System.Drawing.Size(64, 18);
            this.labelMinutesInterval.TabIndex = 43;
            this.labelMinutesInterval.Text = "minutes";
            // 
            // labelSecondsInterval
            // 
            this.labelSecondsInterval.AutoSize = true;
            this.labelSecondsInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSecondsInterval.Location = new System.Drawing.Point(585, 344);
            this.labelSecondsInterval.Name = "labelSecondsInterval";
            this.labelSecondsInterval.Size = new System.Drawing.Size(64, 18);
            this.labelSecondsInterval.TabIndex = 44;
            this.labelSecondsInterval.Text = "seconds";
            // 
            // labelIntervalHelp
            // 
            this.labelIntervalHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelIntervalHelp.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIntervalHelp.Location = new System.Drawing.Point(285, 295);
            this.labelIntervalHelp.Name = "labelIntervalHelp";
            this.labelIntervalHelp.Size = new System.Drawing.Size(537, 40);
            this.labelIntervalHelp.TabIndex = 45;
            this.labelIntervalHelp.Text = "How slow or fast you want screenshots to be taken.";
            // 
            // buttonStartScreenCapture
            // 
            this.buttonStartScreenCapture.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonStartScreenCapture.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartScreenCapture.Location = new System.Drawing.Point(12, 444);
            this.buttonStartScreenCapture.Name = "buttonStartScreenCapture";
            this.buttonStartScreenCapture.Size = new System.Drawing.Size(267, 46);
            this.buttonStartScreenCapture.TabIndex = 46;
            this.buttonStartScreenCapture.TabStop = false;
            this.buttonStartScreenCapture.Text = "Start Screen Capture";
            this.buttonStartScreenCapture.UseVisualStyleBackColor = false;
            // 
            // buttonStopScreenCapture
            // 
            this.buttonStopScreenCapture.BackColor = System.Drawing.Color.PaleVioletRed;
            this.buttonStopScreenCapture.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStopScreenCapture.Location = new System.Drawing.Point(285, 444);
            this.buttonStopScreenCapture.Name = "buttonStopScreenCapture";
            this.buttonStopScreenCapture.Size = new System.Drawing.Size(267, 46);
            this.buttonStopScreenCapture.TabIndex = 47;
            this.buttonStopScreenCapture.TabStop = false;
            this.buttonStopScreenCapture.Text = "Stop Screen Capture";
            this.buttonStopScreenCapture.UseVisualStyleBackColor = false;
            // 
            // labelFilenamePattern
            // 
            this.labelFilenamePattern.AutoSize = true;
            this.labelFilenamePattern.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilenamePattern.Location = new System.Drawing.Point(285, 140);
            this.labelFilenamePattern.Name = "labelFilenamePattern";
            this.labelFilenamePattern.Size = new System.Drawing.Size(320, 18);
            this.labelFilenamePattern.TabIndex = 48;
            this.labelFilenamePattern.Text = "Filename Pattern (we call it a \"Macro\")";
            // 
            // textBoxFilenamePattern
            // 
            this.textBoxFilenamePattern.Font = new System.Drawing.Font("Consolas", 11.25F);
            this.textBoxFilenamePattern.Location = new System.Drawing.Point(285, 216);
            this.textBoxFilenamePattern.Name = "textBoxFilenamePattern";
            this.textBoxFilenamePattern.Size = new System.Drawing.Size(537, 25);
            this.textBoxFilenamePattern.TabIndex = 49;
            this.textBoxFilenamePattern.TabStop = false;
            // 
            // labelFilenamePatternHelp
            // 
            this.labelFilenamePatternHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelFilenamePatternHelp.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilenamePatternHelp.Location = new System.Drawing.Point(285, 169);
            this.labelFilenamePatternHelp.Name = "labelFilenamePatternHelp";
            this.labelFilenamePatternHelp.Size = new System.Drawing.Size(537, 40);
            this.labelFilenamePatternHelp.TabIndex = 50;
            this.labelFilenamePatternHelp.Text = "The filename pattern for each file that will be saved for each screenshot. Filepa" +
    "ths for Screens and Regions will be replaced.";
            // 
            // buttonExitApplication
            // 
            this.buttonExitApplication.BackColor = System.Drawing.SystemColors.Control;
            this.buttonExitApplication.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExitApplication.Location = new System.Drawing.Point(558, 444);
            this.buttonExitApplication.Name = "buttonExitApplication";
            this.buttonExitApplication.Size = new System.Drawing.Size(267, 46);
            this.buttonExitApplication.TabIndex = 51;
            this.buttonExitApplication.TabStop = false;
            this.buttonExitApplication.Text = "Exit Auto Screen Capture";
            this.buttonExitApplication.UseVisualStyleBackColor = false;
            // 
            // checkBoxShowScreenCaptureStatusOnStart
            // 
            this.checkBoxShowScreenCaptureStatusOnStart.AutoSize = true;
            this.checkBoxShowScreenCaptureStatusOnStart.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxShowScreenCaptureStatusOnStart.Location = new System.Drawing.Point(285, 387);
            this.checkBoxShowScreenCaptureStatusOnStart.Name = "checkBoxShowScreenCaptureStatusOnStart";
            this.checkBoxShowScreenCaptureStatusOnStart.Size = new System.Drawing.Size(307, 22);
            this.checkBoxShowScreenCaptureStatusOnStart.TabIndex = 52;
            this.checkBoxShowScreenCaptureStatusOnStart.Text = "Show Screen Capture Status On Start";
            this.checkBoxShowScreenCaptureStatusOnStart.UseVisualStyleBackColor = true;
            this.checkBoxShowScreenCaptureStatusOnStart.CheckedChanged += new System.EventHandler(this.checkBoxShowScreenCaptureStatusOnStart_CheckedChanged);
            // 
            // checkBoxShowScreenshotsFolderOnStop
            // 
            this.checkBoxShowScreenshotsFolderOnStop.AutoSize = true;
            this.checkBoxShowScreenshotsFolderOnStop.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxShowScreenshotsFolderOnStop.Location = new System.Drawing.Point(285, 415);
            this.checkBoxShowScreenshotsFolderOnStop.Name = "checkBoxShowScreenshotsFolderOnStop";
            this.checkBoxShowScreenshotsFolderOnStop.Size = new System.Drawing.Size(275, 22);
            this.checkBoxShowScreenshotsFolderOnStop.TabIndex = 53;
            this.checkBoxShowScreenshotsFolderOnStop.Text = "Show Screenshots Folder On Stop";
            this.checkBoxShowScreenshotsFolderOnStop.UseVisualStyleBackColor = true;
            this.checkBoxShowScreenshotsFolderOnStop.CheckedChanged += new System.EventHandler(this.checkBoxShowScreenshotsFolderOnStop_CheckedChanged);
            // 
            // numericUpDownMillisecondsInterval
            // 
            this.numericUpDownMillisecondsInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownMillisecondsInterval.Location = new System.Drawing.Point(664, 342);
            this.numericUpDownMillisecondsInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownMillisecondsInterval.Name = "numericUpDownMillisecondsInterval";
            this.numericUpDownMillisecondsInterval.Size = new System.Drawing.Size(50, 25);
            this.numericUpDownMillisecondsInterval.TabIndex = 54;
            this.numericUpDownMillisecondsInterval.TabStop = false;
            // 
            // labelMillisecondsInterval
            // 
            this.labelMillisecondsInterval.AutoSize = true;
            this.labelMillisecondsInterval.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMillisecondsInterval.Location = new System.Drawing.Point(720, 344);
            this.labelMillisecondsInterval.Name = "labelMillisecondsInterval";
            this.labelMillisecondsInterval.Size = new System.Drawing.Size(104, 18);
            this.labelMillisecondsInterval.TabIndex = 55;
            this.labelMillisecondsInterval.Text = "milliseconds";
            // 
            // FormAutoScreenCaptureForBeginners
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 499);
            this.Controls.Add(this.labelMillisecondsInterval);
            this.Controls.Add(this.numericUpDownMillisecondsInterval);
            this.Controls.Add(this.checkBoxShowScreenshotsFolderOnStop);
            this.Controls.Add(this.checkBoxShowScreenCaptureStatusOnStart);
            this.Controls.Add(this.buttonExitApplication);
            this.Controls.Add(this.labelFilenamePatternHelp);
            this.Controls.Add(this.textBoxFilenamePattern);
            this.Controls.Add(this.labelFilenamePattern);
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
            this.Load += new System.EventHandler(this.FormAutoScreenCaptureForBeginners_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButYoureAPanda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxButYoureAPanda;
        private System.Windows.Forms.Label labelScreenshotsFolderHelp;
        private System.Windows.Forms.Label labelScreenshotsFolder;
        private System.Windows.Forms.Button buttonScreenshotsFolderBrowseFolder;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.Label labelHoursInterval;
        private System.Windows.Forms.Label labelMinutesInterval;
        private System.Windows.Forms.Label labelSecondsInterval;
        private System.Windows.Forms.Label labelMillisecondsInterval;
        private System.Windows.Forms.Label labelIntervalHelp;
        private System.Windows.Forms.Label labelFilenamePattern;
        private System.Windows.Forms.Label labelFilenamePatternHelp;
        private System.Windows.Forms.CheckBox checkBoxShowScreenCaptureStatusOnStart;
        private System.Windows.Forms.CheckBox checkBoxShowScreenshotsFolderOnStop;

        /// <summary>
        /// The screenshots folder.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxScreenshotsFolder;

        /// <summary>
        /// The filename pattern.
        /// </summary>
        public System.Windows.Forms.TextBox textBoxFilenamePattern;

        /// <summary>
        /// Hours.
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;

        /// <summary>
        /// Minutes.
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;

        /// <summary>
        /// Seconds.
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;

        /// <summary>
        /// Milliseconds.
        /// </summary>
        public System.Windows.Forms.NumericUpDown numericUpDownMillisecondsInterval;

        /// <summary>
        /// Start Screen Capture.
        /// </summary>
        public System.Windows.Forms.Button buttonStartScreenCapture;

        /// <summary>
        /// Stop Screen Capture.
        /// </summary>
        public System.Windows.Forms.Button buttonStopScreenCapture;

        /// <summary>
        /// Exit Auto Screen Capture.
        /// </summary>
        public System.Windows.Forms.Button buttonExitApplication;
    }
}