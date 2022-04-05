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
            this.richTextBoxAboutApplication = new System.Windows.Forms.RichTextBox();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.richTextBoxBladeDetails = new System.Windows.Forms.RichTextBox();
            this.tabControlAbout = new System.Windows.Forms.TabControl();
            this.tabPageAboutApplication = new System.Windows.Forms.TabPage();
            this.tabPageAboutLicense = new System.Windows.Forms.TabPage();
            this.richTextBoxAboutLicense = new System.Windows.Forms.RichTextBox();
            this.tabPageAboutLibraryUsage = new System.Windows.Forms.TabPage();
            this.tabControlAboutLibraryUsage = new System.Windows.Forms.TabControl();
            this.tabPageAboutLibraryUsageGavinKendallSftp = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPageAboutLibraryUsageILMerge = new System.Windows.Forms.TabPage();
            this.richTextBoxAboutLibraryUsageILMerge = new System.Windows.Forms.RichTextBox();
            this.tabPageAboutLibraryUsageRenciSshNet = new System.Windows.Forms.TabPage();
            this.richTextBoxAboutLibraryUsageRenciSshNet = new System.Windows.Forms.RichTextBox();
            this.tabPageAboutSpecialThanks = new System.Windows.Forms.TabPage();
            this.richTextBoxAboutSpecialThanks = new System.Windows.Forms.RichTextBox();
            this.tabPageAboutContact = new System.Windows.Forms.TabPage();
            this.richTextBoxAboutContact = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.tabControlAbout.SuspendLayout();
            this.tabPageAboutApplication.SuspendLayout();
            this.tabPageAboutLicense.SuspendLayout();
            this.tabPageAboutLibraryUsage.SuspendLayout();
            this.tabControlAboutLibraryUsage.SuspendLayout();
            this.tabPageAboutLibraryUsageGavinKendallSftp.SuspendLayout();
            this.tabPageAboutLibraryUsageILMerge.SuspendLayout();
            this.tabPageAboutLibraryUsageRenciSshNet.SuspendLayout();
            this.tabPageAboutSpecialThanks.SuspendLayout();
            this.tabPageAboutContact.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxAboutApplication
            // 
            this.richTextBoxAboutApplication.BackColor = System.Drawing.Color.White;
            this.richTextBoxAboutApplication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAboutApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxAboutApplication.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxAboutApplication.Name = "richTextBoxAboutApplication";
            this.richTextBoxAboutApplication.ReadOnly = true;
            this.richTextBoxAboutApplication.Size = new System.Drawing.Size(569, 505);
            this.richTextBoxAboutApplication.TabIndex = 1;
            this.richTextBoxAboutApplication.TabStop = false;
            this.richTextBoxAboutApplication.Text = resources.GetString("richTextBoxAboutApplication.Text");
            this.richTextBoxAboutApplication.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox_LinkClicked);
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
            // tabControlAbout
            // 
            this.tabControlAbout.Controls.Add(this.tabPageAboutApplication);
            this.tabControlAbout.Controls.Add(this.tabPageAboutLicense);
            this.tabControlAbout.Controls.Add(this.tabPageAboutLibraryUsage);
            this.tabControlAbout.Controls.Add(this.tabPageAboutSpecialThanks);
            this.tabControlAbout.Controls.Add(this.tabPageAboutContact);
            this.tabControlAbout.Location = new System.Drawing.Point(223, 2);
            this.tabControlAbout.Name = "tabControlAbout";
            this.tabControlAbout.SelectedIndex = 0;
            this.tabControlAbout.Size = new System.Drawing.Size(583, 537);
            this.tabControlAbout.TabIndex = 5;
            // 
            // tabPageAboutApplication
            // 
            this.tabPageAboutApplication.Controls.Add(this.richTextBoxAboutApplication);
            this.tabPageAboutApplication.Location = new System.Drawing.Point(4, 22);
            this.tabPageAboutApplication.Name = "tabPageAboutApplication";
            this.tabPageAboutApplication.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAboutApplication.Size = new System.Drawing.Size(575, 511);
            this.tabPageAboutApplication.TabIndex = 0;
            this.tabPageAboutApplication.Text = "Application";
            this.tabPageAboutApplication.UseVisualStyleBackColor = true;
            // 
            // tabPageAboutLicense
            // 
            this.tabPageAboutLicense.Controls.Add(this.richTextBoxAboutLicense);
            this.tabPageAboutLicense.Location = new System.Drawing.Point(4, 22);
            this.tabPageAboutLicense.Name = "tabPageAboutLicense";
            this.tabPageAboutLicense.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAboutLicense.Size = new System.Drawing.Size(575, 511);
            this.tabPageAboutLicense.TabIndex = 1;
            this.tabPageAboutLicense.Text = "License";
            this.tabPageAboutLicense.UseVisualStyleBackColor = true;
            // 
            // richTextBoxAboutLicense
            // 
            this.richTextBoxAboutLicense.BackColor = System.Drawing.Color.White;
            this.richTextBoxAboutLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAboutLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxAboutLicense.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxAboutLicense.Name = "richTextBoxAboutLicense";
            this.richTextBoxAboutLicense.ReadOnly = true;
            this.richTextBoxAboutLicense.Size = new System.Drawing.Size(569, 505);
            this.richTextBoxAboutLicense.TabIndex = 2;
            this.richTextBoxAboutLicense.TabStop = false;
            this.richTextBoxAboutLicense.Text = resources.GetString("richTextBoxAboutLicense.Text");
            this.richTextBoxAboutLicense.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox_LinkClicked);
            // 
            // tabPageAboutLibraryUsage
            // 
            this.tabPageAboutLibraryUsage.Controls.Add(this.tabControlAboutLibraryUsage);
            this.tabPageAboutLibraryUsage.Location = new System.Drawing.Point(4, 22);
            this.tabPageAboutLibraryUsage.Name = "tabPageAboutLibraryUsage";
            this.tabPageAboutLibraryUsage.Size = new System.Drawing.Size(575, 511);
            this.tabPageAboutLibraryUsage.TabIndex = 4;
            this.tabPageAboutLibraryUsage.Text = "Library Usage";
            this.tabPageAboutLibraryUsage.UseVisualStyleBackColor = true;
            // 
            // tabControlAboutLibraryUsage
            // 
            this.tabControlAboutLibraryUsage.Controls.Add(this.tabPageAboutLibraryUsageGavinKendallSftp);
            this.tabControlAboutLibraryUsage.Controls.Add(this.tabPageAboutLibraryUsageILMerge);
            this.tabControlAboutLibraryUsage.Controls.Add(this.tabPageAboutLibraryUsageRenciSshNet);
            this.tabControlAboutLibraryUsage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAboutLibraryUsage.Location = new System.Drawing.Point(0, 0);
            this.tabControlAboutLibraryUsage.Name = "tabControlAboutLibraryUsage";
            this.tabControlAboutLibraryUsage.SelectedIndex = 0;
            this.tabControlAboutLibraryUsage.Size = new System.Drawing.Size(575, 511);
            this.tabControlAboutLibraryUsage.TabIndex = 0;
            // 
            // tabPageAboutLibraryUsageGavinKendallSftp
            // 
            this.tabPageAboutLibraryUsageGavinKendallSftp.Controls.Add(this.richTextBox1);
            this.tabPageAboutLibraryUsageGavinKendallSftp.Location = new System.Drawing.Point(4, 22);
            this.tabPageAboutLibraryUsageGavinKendallSftp.Name = "tabPageAboutLibraryUsageGavinKendallSftp";
            this.tabPageAboutLibraryUsageGavinKendallSftp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAboutLibraryUsageGavinKendallSftp.Size = new System.Drawing.Size(567, 485);
            this.tabPageAboutLibraryUsageGavinKendallSftp.TabIndex = 2;
            this.tabPageAboutLibraryUsageGavinKendallSftp.Text = "Gavin Kendall SFTP";
            this.tabPageAboutLibraryUsageGavinKendallSftp.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(561, 479);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // tabPageAboutLibraryUsageILMerge
            // 
            this.tabPageAboutLibraryUsageILMerge.Controls.Add(this.richTextBoxAboutLibraryUsageILMerge);
            this.tabPageAboutLibraryUsageILMerge.Location = new System.Drawing.Point(4, 22);
            this.tabPageAboutLibraryUsageILMerge.Name = "tabPageAboutLibraryUsageILMerge";
            this.tabPageAboutLibraryUsageILMerge.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAboutLibraryUsageILMerge.Size = new System.Drawing.Size(567, 485);
            this.tabPageAboutLibraryUsageILMerge.TabIndex = 0;
            this.tabPageAboutLibraryUsageILMerge.Text = "ILMerge";
            this.tabPageAboutLibraryUsageILMerge.UseVisualStyleBackColor = true;
            // 
            // richTextBoxAboutLibraryUsageILMerge
            // 
            this.richTextBoxAboutLibraryUsageILMerge.BackColor = System.Drawing.Color.White;
            this.richTextBoxAboutLibraryUsageILMerge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAboutLibraryUsageILMerge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxAboutLibraryUsageILMerge.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxAboutLibraryUsageILMerge.Name = "richTextBoxAboutLibraryUsageILMerge";
            this.richTextBoxAboutLibraryUsageILMerge.ReadOnly = true;
            this.richTextBoxAboutLibraryUsageILMerge.Size = new System.Drawing.Size(561, 479);
            this.richTextBoxAboutLibraryUsageILMerge.TabIndex = 3;
            this.richTextBoxAboutLibraryUsageILMerge.TabStop = false;
            this.richTextBoxAboutLibraryUsageILMerge.Text = resources.GetString("richTextBoxAboutLibraryUsageILMerge.Text");
            // 
            // tabPageAboutLibraryUsageRenciSshNet
            // 
            this.tabPageAboutLibraryUsageRenciSshNet.Controls.Add(this.richTextBoxAboutLibraryUsageRenciSshNet);
            this.tabPageAboutLibraryUsageRenciSshNet.Location = new System.Drawing.Point(4, 22);
            this.tabPageAboutLibraryUsageRenciSshNet.Name = "tabPageAboutLibraryUsageRenciSshNet";
            this.tabPageAboutLibraryUsageRenciSshNet.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAboutLibraryUsageRenciSshNet.Size = new System.Drawing.Size(567, 485);
            this.tabPageAboutLibraryUsageRenciSshNet.TabIndex = 1;
            this.tabPageAboutLibraryUsageRenciSshNet.Text = "Renci SSH Net";
            this.tabPageAboutLibraryUsageRenciSshNet.UseVisualStyleBackColor = true;
            // 
            // richTextBoxAboutLibraryUsageRenciSshNet
            // 
            this.richTextBoxAboutLibraryUsageRenciSshNet.BackColor = System.Drawing.Color.White;
            this.richTextBoxAboutLibraryUsageRenciSshNet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAboutLibraryUsageRenciSshNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxAboutLibraryUsageRenciSshNet.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxAboutLibraryUsageRenciSshNet.Name = "richTextBoxAboutLibraryUsageRenciSshNet";
            this.richTextBoxAboutLibraryUsageRenciSshNet.ReadOnly = true;
            this.richTextBoxAboutLibraryUsageRenciSshNet.Size = new System.Drawing.Size(561, 479);
            this.richTextBoxAboutLibraryUsageRenciSshNet.TabIndex = 4;
            this.richTextBoxAboutLibraryUsageRenciSshNet.TabStop = false;
            this.richTextBoxAboutLibraryUsageRenciSshNet.Text = resources.GetString("richTextBoxAboutLibraryUsageRenciSshNet.Text");
            // 
            // tabPageAboutSpecialThanks
            // 
            this.tabPageAboutSpecialThanks.Controls.Add(this.richTextBoxAboutSpecialThanks);
            this.tabPageAboutSpecialThanks.Location = new System.Drawing.Point(4, 22);
            this.tabPageAboutSpecialThanks.Name = "tabPageAboutSpecialThanks";
            this.tabPageAboutSpecialThanks.Size = new System.Drawing.Size(575, 511);
            this.tabPageAboutSpecialThanks.TabIndex = 2;
            this.tabPageAboutSpecialThanks.Text = "Special Thanks";
            this.tabPageAboutSpecialThanks.UseVisualStyleBackColor = true;
            // 
            // richTextBoxAboutSpecialThanks
            // 
            this.richTextBoxAboutSpecialThanks.BackColor = System.Drawing.Color.White;
            this.richTextBoxAboutSpecialThanks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAboutSpecialThanks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxAboutSpecialThanks.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxAboutSpecialThanks.Name = "richTextBoxAboutSpecialThanks";
            this.richTextBoxAboutSpecialThanks.ReadOnly = true;
            this.richTextBoxAboutSpecialThanks.Size = new System.Drawing.Size(575, 511);
            this.richTextBoxAboutSpecialThanks.TabIndex = 2;
            this.richTextBoxAboutSpecialThanks.TabStop = false;
            this.richTextBoxAboutSpecialThanks.Text = resources.GetString("richTextBoxAboutSpecialThanks.Text");
            // 
            // tabPageAboutContact
            // 
            this.tabPageAboutContact.Controls.Add(this.richTextBoxAboutContact);
            this.tabPageAboutContact.Location = new System.Drawing.Point(4, 22);
            this.tabPageAboutContact.Name = "tabPageAboutContact";
            this.tabPageAboutContact.Size = new System.Drawing.Size(575, 511);
            this.tabPageAboutContact.TabIndex = 3;
            this.tabPageAboutContact.Text = "Contact";
            this.tabPageAboutContact.UseVisualStyleBackColor = true;
            // 
            // richTextBoxAboutContact
            // 
            this.richTextBoxAboutContact.BackColor = System.Drawing.Color.White;
            this.richTextBoxAboutContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxAboutContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxAboutContact.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxAboutContact.Name = "richTextBoxAboutContact";
            this.richTextBoxAboutContact.ReadOnly = true;
            this.richTextBoxAboutContact.Size = new System.Drawing.Size(575, 511);
            this.richTextBoxAboutContact.TabIndex = 2;
            this.richTextBoxAboutContact.TabStop = false;
            this.richTextBoxAboutContact.Text = resources.GetString("richTextBoxAboutContact.Text");
            this.richTextBoxAboutContact.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox_LinkClicked);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 539);
            this.Controls.Add(this.tabControlAbout);
            this.Controls.Add(this.pictureBoxBanner);
            this.Controls.Add(this.richTextBoxBladeDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(781, 526);
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Screen Capture 2.4.2.2 (\"Blade\")";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAbout_FormClosing);
            this.Load += new System.EventHandler(this.FormAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.tabControlAbout.ResumeLayout(false);
            this.tabPageAboutApplication.ResumeLayout(false);
            this.tabPageAboutLicense.ResumeLayout(false);
            this.tabPageAboutLibraryUsage.ResumeLayout(false);
            this.tabControlAboutLibraryUsage.ResumeLayout(false);
            this.tabPageAboutLibraryUsageGavinKendallSftp.ResumeLayout(false);
            this.tabPageAboutLibraryUsageILMerge.ResumeLayout(false);
            this.tabPageAboutLibraryUsageRenciSshNet.ResumeLayout(false);
            this.tabPageAboutSpecialThanks.ResumeLayout(false);
            this.tabPageAboutContact.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxAboutApplication;
        private System.Windows.Forms.PictureBox pictureBoxBanner;
        private System.Windows.Forms.RichTextBox richTextBoxBladeDetails;
        private System.Windows.Forms.TabControl tabControlAbout;
        private System.Windows.Forms.TabPage tabPageAboutApplication;
        private System.Windows.Forms.TabPage tabPageAboutLicense;
        private System.Windows.Forms.RichTextBox richTextBoxAboutLicense;
        private System.Windows.Forms.TabPage tabPageAboutSpecialThanks;
        private System.Windows.Forms.RichTextBox richTextBoxAboutSpecialThanks;
        private System.Windows.Forms.TabPage tabPageAboutContact;
        private System.Windows.Forms.RichTextBox richTextBoxAboutContact;
        private System.Windows.Forms.TabPage tabPageAboutLibraryUsage;
        private System.Windows.Forms.TabControl tabControlAboutLibraryUsage;
        private System.Windows.Forms.TabPage tabPageAboutLibraryUsageILMerge;
        private System.Windows.Forms.RichTextBox richTextBoxAboutLibraryUsageILMerge;
        private System.Windows.Forms.TabPage tabPageAboutLibraryUsageRenciSshNet;
        private System.Windows.Forms.RichTextBox richTextBoxAboutLibraryUsageRenciSshNet;
        private System.Windows.Forms.TabPage tabPageAboutLibraryUsageGavinKendallSftp;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}