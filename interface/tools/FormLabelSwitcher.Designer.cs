namespace AutoScreenCapture
{
    partial class FormLabelSwitcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLabelSwitcher));
            this.comboBoxLabels = new System.Windows.Forms.ComboBox();
            this.buttonStartStopScreenCapture = new System.Windows.Forms.Button();
            this.labelScreenCaptureStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxLabels
            // 
            this.comboBoxLabels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLabels.FormattingEnabled = true;
            this.comboBoxLabels.Location = new System.Drawing.Point(47, 36);
            this.comboBoxLabels.Name = "comboBoxLabels";
            this.comboBoxLabels.Size = new System.Drawing.Size(561, 21);
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
            // FormLabelSwitcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 61);
            this.Controls.Add(this.labelScreenCaptureStatus);
            this.Controls.Add(this.buttonStartStopScreenCapture);
            this.Controls.Add(this.comboBoxLabels);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormLabelSwitcher";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLabelSwitcher_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxLabels;

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.Button buttonStartStopScreenCapture;
        public System.Windows.Forms.Label labelScreenCaptureStatus;
    }
}