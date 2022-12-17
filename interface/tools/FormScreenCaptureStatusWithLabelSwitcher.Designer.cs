namespace AutoScreenCapture
{
    partial class FormScreenCaptureStatusWithLabelSwitcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScreenCaptureStatusWithLabelSwitcher));
            this.comboBoxLabels = new System.Windows.Forms.ComboBox();
            this.buttonStartStopScreenCapture = new System.Windows.Forms.Button();
            this.labelScreenCaptureStatus = new System.Windows.Forms.Label();
            this.textBoxNewLabel = new System.Windows.Forms.TextBox();
            this.buttonAddScreenshotLabelToList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxLabels
            // 
            this.comboBoxLabels.DropDownHeight = 300;
            this.comboBoxLabels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLabels.DropDownWidth = 700;
            this.comboBoxLabels.FormattingEnabled = true;
            this.comboBoxLabels.IntegralHeight = false;
            this.comboBoxLabels.Location = new System.Drawing.Point(434, 36);
            this.comboBoxLabels.Name = "comboBoxLabels";
            this.comboBoxLabels.Size = new System.Drawing.Size(162, 21);
            this.comboBoxLabels.Sorted = true;
            this.comboBoxLabels.TabIndex = 0;
            this.comboBoxLabels.TabStop = false;
            this.comboBoxLabels.SelectedIndexChanged += new System.EventHandler(this.comboBoxLabels_SelectedIndexChanged);
            // 
            // buttonStartStopScreenCapture
            // 
            this.buttonStartStopScreenCapture.Image = global::AutoScreenCapture.Properties.Resources.start_screen_capture;
            this.buttonStartStopScreenCapture.Location = new System.Drawing.Point(4, 34);
            this.buttonStartStopScreenCapture.Name = "buttonStartStopScreenCapture";
            this.buttonStartStopScreenCapture.Size = new System.Drawing.Size(37, 24);
            this.buttonStartStopScreenCapture.TabIndex = 1;
            this.buttonStartStopScreenCapture.TabStop = false;
            this.buttonStartStopScreenCapture.UseVisualStyleBackColor = true;
            // 
            // labelScreenCaptureStatus
            // 
            this.labelScreenCaptureStatus.BackColor = System.Drawing.Color.LightYellow;
            this.labelScreenCaptureStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelScreenCaptureStatus.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScreenCaptureStatus.Location = new System.Drawing.Point(0, 0);
            this.labelScreenCaptureStatus.Name = "labelScreenCaptureStatus";
            this.labelScreenCaptureStatus.Size = new System.Drawing.Size(608, 31);
            this.labelScreenCaptureStatus.TabIndex = 2;
            this.labelScreenCaptureStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxNewLabel
            // 
            this.textBoxNewLabel.Location = new System.Drawing.Point(47, 36);
            this.textBoxNewLabel.Name = "textBoxNewLabel";
            this.textBoxNewLabel.Size = new System.Drawing.Size(300, 20);
            this.textBoxNewLabel.TabIndex = 3;
            this.textBoxNewLabel.TabStop = false;
            // 
            // buttonAddScreenshotLabelToList
            // 
            this.buttonAddScreenshotLabelToList.Location = new System.Drawing.Point(353, 34);
            this.buttonAddScreenshotLabelToList.Name = "buttonAddScreenshotLabelToList";
            this.buttonAddScreenshotLabelToList.Size = new System.Drawing.Size(75, 23);
            this.buttonAddScreenshotLabelToList.TabIndex = 9;
            this.buttonAddScreenshotLabelToList.TabStop = false;
            this.buttonAddScreenshotLabelToList.Text = "Add To List";
            this.buttonAddScreenshotLabelToList.UseVisualStyleBackColor = true;
            this.buttonAddScreenshotLabelToList.Click += new System.EventHandler(this.buttonAddScreenshotLabelToList_Click);
            // 
            // FormScreenCaptureStatusWithLabelSwitcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 61);
            this.Controls.Add(this.buttonAddScreenshotLabelToList);
            this.Controls.Add(this.textBoxNewLabel);
            this.Controls.Add(this.labelScreenCaptureStatus);
            this.Controls.Add(this.buttonStartStopScreenCapture);
            this.Controls.Add(this.comboBoxLabels);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormScreenCaptureStatusWithLabelSwitcher";
            this.Text = "Screen Capture Status With Label Switcher";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLabelSwitcher_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>
        /// A combo box for containing the labels that the user can switch between.
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxLabels;

        /// <summary>
        /// A button for either starting a screen capture session or stopping the currently running screen capture session.
        /// </summary>
        public System.Windows.Forms.Button buttonStartStopScreenCapture;

        /// <summary>
        /// The status of the current screen capture session.
        /// </summary>
        public System.Windows.Forms.Label labelScreenCaptureStatus;
        private System.Windows.Forms.TextBox textBoxNewLabel;
        private System.Windows.Forms.Button buttonAddScreenshotLabelToList;
    }
}