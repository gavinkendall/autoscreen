namespace AutoScreenCapture
{
    partial class FormScreenCaptureStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScreenCaptureStatus));
            this.labelScreenCaptureStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelScreenCaptureStatus
            // 
            this.labelScreenCaptureStatus.BackColor = System.Drawing.Color.LightYellow;
            this.labelScreenCaptureStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelScreenCaptureStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScreenCaptureStatus.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScreenCaptureStatus.Location = new System.Drawing.Point(0, 0);
            this.labelScreenCaptureStatus.Name = "labelScreenCaptureStatus";
            this.labelScreenCaptureStatus.Size = new System.Drawing.Size(608, 31);
            this.labelScreenCaptureStatus.TabIndex = 0;
            this.labelScreenCaptureStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormScreenCaptureStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(608, 31);
            this.Controls.Add(this.labelScreenCaptureStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(624, 70);
            this.Name = "FormScreenCaptureStatus";
            this.ShowInTaskbar = false;
            this.Text = "Screen Capture Status";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormInformationWindow_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Label for Screen Capture Status form.
        /// </summary>
        public System.Windows.Forms.Label labelScreenCaptureStatus;
    }
}