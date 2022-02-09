namespace AutoScreenCapture
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.richTextBoxApplication = new System.Windows.Forms.RichTextBox();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.richTextBoxContributors = new System.Windows.Forms.RichTextBox();
            this.richTextBoxBladeDetails = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxApplication
            // 
            this.richTextBoxApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxApplication.BackColor = System.Drawing.Color.White;
            this.richTextBoxApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxApplication.Location = new System.Drawing.Point(223, 1);
            this.richTextBoxApplication.Name = "richTextBoxApplication";
            this.richTextBoxApplication.ReadOnly = true;
            this.richTextBoxApplication.Size = new System.Drawing.Size(585, 538);
            this.richTextBoxApplication.TabIndex = 1;
            this.richTextBoxApplication.TabStop = false;
            this.richTextBoxApplication.Text = resources.GetString("richTextBoxApplication.Text");
            this.richTextBoxApplication.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxApplication_LinkClicked);
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.BackColor = System.Drawing.Color.Black;
            this.pictureBoxBanner.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxBanner.Image")));
            this.pictureBoxBanner.Location = new System.Drawing.Point(2, 2);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(215, 203);
            this.pictureBoxBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBanner.TabIndex = 2;
            this.pictureBoxBanner.TabStop = false;
            this.pictureBoxBanner.Click += new System.EventHandler(this.pictureBoxBanner_Click);
            // 
            // richTextBoxContributors
            // 
            this.richTextBoxContributors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxContributors.BackColor = System.Drawing.Color.White;
            this.richTextBoxContributors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxContributors.Location = new System.Drawing.Point(2, 211);
            this.richTextBoxContributors.Name = "richTextBoxContributors";
            this.richTextBoxContributors.ReadOnly = true;
            this.richTextBoxContributors.Size = new System.Drawing.Size(215, 328);
            this.richTextBoxContributors.TabIndex = 3;
            this.richTextBoxContributors.TabStop = false;
            this.richTextBoxContributors.Text = resources.GetString("richTextBoxContributors.Text");
            // 
            // richTextBoxBladeDetails
            // 
            this.richTextBoxBladeDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxBladeDetails.BackColor = System.Drawing.Color.White;
            this.richTextBoxBladeDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxBladeDetails.Location = new System.Drawing.Point(2, 211);
            this.richTextBoxBladeDetails.Name = "richTextBoxBladeDetails";
            this.richTextBoxBladeDetails.ReadOnly = true;
            this.richTextBoxBladeDetails.Size = new System.Drawing.Size(215, 328);
            this.richTextBoxBladeDetails.TabIndex = 4;
            this.richTextBoxBladeDetails.TabStop = false;
            this.richTextBoxBladeDetails.Text = "";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 539);
            this.Controls.Add(this.richTextBoxContributors);
            this.Controls.Add(this.pictureBoxBanner);
            this.Controls.Add(this.richTextBoxApplication);
            this.Controls.Add(this.richTextBoxBladeDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(781, 526);
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Screen Capture 2.4.1.2 (\"Blade\")";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAbout_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxApplication;
        private System.Windows.Forms.PictureBox pictureBoxBanner;
        private System.Windows.Forms.RichTextBox richTextBoxContributors;
        private System.Windows.Forms.RichTextBox richTextBoxBladeDetails;
    }
}