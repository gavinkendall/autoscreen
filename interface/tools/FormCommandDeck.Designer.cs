namespace AutoScreenCapture
{
    partial class FormCommandDeck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCommandDeck));
            this.buttonRegionSelectClipboard = new System.Windows.Forms.Button();
            this.buttonRegionSelectClipboardAutoSave = new System.Windows.Forms.Button();
            this.buttonRegionSelectClipboardAutoSaveEdit = new System.Windows.Forms.Button();
            this.buttonRegionSelectClipboardFloatingScreenshot = new System.Windows.Forms.Button();
            this.buttonRegionSelectFloatingScreenshot = new System.Windows.Forms.Button();
            this.buttonRegionSelectAddRegion = new System.Windows.Forms.Button();
            this.groupBoxRegionSelect = new System.Windows.Forms.GroupBox();
            this.buttonStartStopScreenCapture = new System.Windows.Forms.Button();
            this.buttonCaptureNow = new System.Windows.Forms.Button();
            this.buttonCaptureNowEdit = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.groupBoxRegionSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRegionSelectClipboard
            // 
            this.buttonRegionSelectClipboard.Location = new System.Drawing.Point(7, 17);
            this.buttonRegionSelectClipboard.Name = "buttonRegionSelectClipboard";
            this.buttonRegionSelectClipboard.Size = new System.Drawing.Size(165, 23);
            this.buttonRegionSelectClipboard.TabIndex = 0;
            this.buttonRegionSelectClipboard.Text = "Clipboard";
            this.buttonRegionSelectClipboard.UseVisualStyleBackColor = true;
            this.buttonRegionSelectClipboard.Click += new System.EventHandler(this.buttonRegionSelectClipboard_Click);
            // 
            // buttonRegionSelectClipboardAutoSave
            // 
            this.buttonRegionSelectClipboardAutoSave.Location = new System.Drawing.Point(7, 46);
            this.buttonRegionSelectClipboardAutoSave.Name = "buttonRegionSelectClipboardAutoSave";
            this.buttonRegionSelectClipboardAutoSave.Size = new System.Drawing.Size(165, 23);
            this.buttonRegionSelectClipboardAutoSave.TabIndex = 1;
            this.buttonRegionSelectClipboardAutoSave.Text = "Clipboard / Auto Save";
            this.buttonRegionSelectClipboardAutoSave.UseVisualStyleBackColor = true;
            this.buttonRegionSelectClipboardAutoSave.Click += new System.EventHandler(this.buttonRegionSelectClipboardAutoSave_Click);
            // 
            // buttonRegionSelectClipboardAutoSaveEdit
            // 
            this.buttonRegionSelectClipboardAutoSaveEdit.Location = new System.Drawing.Point(7, 75);
            this.buttonRegionSelectClipboardAutoSaveEdit.Name = "buttonRegionSelectClipboardAutoSaveEdit";
            this.buttonRegionSelectClipboardAutoSaveEdit.Size = new System.Drawing.Size(165, 23);
            this.buttonRegionSelectClipboardAutoSaveEdit.TabIndex = 2;
            this.buttonRegionSelectClipboardAutoSaveEdit.Text = "Clipboard / Auto Save / Edit";
            this.buttonRegionSelectClipboardAutoSaveEdit.UseVisualStyleBackColor = true;
            this.buttonRegionSelectClipboardAutoSaveEdit.Click += new System.EventHandler(this.buttonRegionSelectClipboardAutoSaveEdit_Click);
            // 
            // buttonRegionSelectClipboardFloatingScreenshot
            // 
            this.buttonRegionSelectClipboardFloatingScreenshot.Location = new System.Drawing.Point(7, 104);
            this.buttonRegionSelectClipboardFloatingScreenshot.Name = "buttonRegionSelectClipboardFloatingScreenshot";
            this.buttonRegionSelectClipboardFloatingScreenshot.Size = new System.Drawing.Size(165, 23);
            this.buttonRegionSelectClipboardFloatingScreenshot.TabIndex = 3;
            this.buttonRegionSelectClipboardFloatingScreenshot.Text = "Clipboard / Floating Screenshot";
            this.buttonRegionSelectClipboardFloatingScreenshot.UseVisualStyleBackColor = true;
            this.buttonRegionSelectClipboardFloatingScreenshot.Click += new System.EventHandler(this.buttonRegionSelectClipboardFloatingScreenshot_Click);
            // 
            // buttonRegionSelectFloatingScreenshot
            // 
            this.buttonRegionSelectFloatingScreenshot.Location = new System.Drawing.Point(7, 133);
            this.buttonRegionSelectFloatingScreenshot.Name = "buttonRegionSelectFloatingScreenshot";
            this.buttonRegionSelectFloatingScreenshot.Size = new System.Drawing.Size(165, 23);
            this.buttonRegionSelectFloatingScreenshot.TabIndex = 4;
            this.buttonRegionSelectFloatingScreenshot.Text = "Floating Screenshot";
            this.buttonRegionSelectFloatingScreenshot.UseVisualStyleBackColor = true;
            this.buttonRegionSelectFloatingScreenshot.Click += new System.EventHandler(this.buttonRegionSelectFloatingScreenshot_Click);
            // 
            // buttonRegionSelectAddRegion
            // 
            this.buttonRegionSelectAddRegion.Location = new System.Drawing.Point(7, 162);
            this.buttonRegionSelectAddRegion.Name = "buttonRegionSelectAddRegion";
            this.buttonRegionSelectAddRegion.Size = new System.Drawing.Size(165, 23);
            this.buttonRegionSelectAddRegion.TabIndex = 5;
            this.buttonRegionSelectAddRegion.Text = "Add Region";
            this.buttonRegionSelectAddRegion.UseVisualStyleBackColor = true;
            this.buttonRegionSelectAddRegion.Click += new System.EventHandler(this.buttonRegionSelectAddRegion_Click);
            // 
            // groupBoxRegionSelect
            // 
            this.groupBoxRegionSelect.Controls.Add(this.buttonRegionSelectAddRegion);
            this.groupBoxRegionSelect.Controls.Add(this.buttonRegionSelectClipboard);
            this.groupBoxRegionSelect.Controls.Add(this.buttonRegionSelectFloatingScreenshot);
            this.groupBoxRegionSelect.Controls.Add(this.buttonRegionSelectClipboardAutoSave);
            this.groupBoxRegionSelect.Controls.Add(this.buttonRegionSelectClipboardFloatingScreenshot);
            this.groupBoxRegionSelect.Controls.Add(this.buttonRegionSelectClipboardAutoSaveEdit);
            this.groupBoxRegionSelect.Location = new System.Drawing.Point(5, 36);
            this.groupBoxRegionSelect.Name = "groupBoxRegionSelect";
            this.groupBoxRegionSelect.Size = new System.Drawing.Size(179, 190);
            this.groupBoxRegionSelect.TabIndex = 6;
            this.groupBoxRegionSelect.TabStop = false;
            this.groupBoxRegionSelect.Text = "Region Select";
            // 
            // buttonStartStopScreenCapture
            // 
            this.buttonStartStopScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.start_screen_capture;
            this.buttonStartStopScreenCapture.Location = new System.Drawing.Point(11, 6);
            this.buttonStartStopScreenCapture.Name = "buttonStartStopScreenCapture";
            this.buttonStartStopScreenCapture.Size = new System.Drawing.Size(37, 24);
            this.buttonStartStopScreenCapture.TabIndex = 7;
            this.buttonStartStopScreenCapture.TabStop = false;
            this.buttonStartStopScreenCapture.UseVisualStyleBackColor = true;
            // 
            // buttonCaptureNow
            // 
            this.buttonCaptureNow.Image = global::AutoScreenCapture.Properties.Resources.capture_archive;
            this.buttonCaptureNow.Location = new System.Drawing.Point(54, 6);
            this.buttonCaptureNow.Name = "buttonCaptureNow";
            this.buttonCaptureNow.Size = new System.Drawing.Size(37, 24);
            this.buttonCaptureNow.TabIndex = 8;
            this.buttonCaptureNow.TabStop = false;
            this.buttonCaptureNow.UseVisualStyleBackColor = true;
            // 
            // buttonCaptureNowEdit
            // 
            this.buttonCaptureNowEdit.Image = global::AutoScreenCapture.Properties.Resources.capture_edit;
            this.buttonCaptureNowEdit.Location = new System.Drawing.Point(97, 6);
            this.buttonCaptureNowEdit.Name = "buttonCaptureNowEdit";
            this.buttonCaptureNowEdit.Size = new System.Drawing.Size(37, 24);
            this.buttonCaptureNowEdit.TabIndex = 9;
            this.buttonCaptureNowEdit.TabStop = false;
            this.buttonCaptureNowEdit.UseVisualStyleBackColor = true;
            // 
            // buttonExit
            // 
            this.buttonExit.Image = global::AutoScreenCapture.Properties.Resources.exit;
            this.buttonExit.Location = new System.Drawing.Point(140, 6);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(37, 24);
            this.buttonExit.TabIndex = 10;
            this.buttonExit.TabStop = false;
            this.buttonExit.UseVisualStyleBackColor = true;
            // 
            // FormCommandDeck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 230);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonCaptureNowEdit);
            this.Controls.Add(this.buttonCaptureNow);
            this.Controls.Add(this.buttonStartStopScreenCapture);
            this.Controls.Add(this.groupBoxRegionSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCommandDeck";
            this.Text = "Auto Screen Capture";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRegionSelectCommandDeck_FormClosing);
            this.Load += new System.EventHandler(this.FormRegionSelectCommandDeck_Load);
            this.groupBoxRegionSelect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRegionSelectClipboard;
        private System.Windows.Forms.Button buttonRegionSelectClipboardAutoSave;
        private System.Windows.Forms.Button buttonRegionSelectClipboardAutoSaveEdit;
        private System.Windows.Forms.Button buttonRegionSelectClipboardFloatingScreenshot;
        private System.Windows.Forms.Button buttonRegionSelectFloatingScreenshot;
        private System.Windows.Forms.Button buttonRegionSelectAddRegion;
        private System.Windows.Forms.GroupBox groupBoxRegionSelect;

        /// <summary>
        /// A button for either starting a screen capture session or stopping the currently running screen capture session.
        /// </summary>
        public System.Windows.Forms.Button buttonStartStopScreenCapture;
        
        /// <summary>
        /// A button for initiating "Capture Now / Archive".
        /// </summary>
        public System.Windows.Forms.Button buttonCaptureNow;

        /// <summary>
        /// A button for initiating "Capture Now / Edit".
        /// </summary>
        public System.Windows.Forms.Button buttonCaptureNowEdit;

        /// <summary>
        /// A button for exiting the application.
        /// </summary>
        public System.Windows.Forms.Button buttonExit;
    }
}