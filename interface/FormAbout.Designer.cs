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
            this.tabControlAbout = new System.Windows.Forms.TabControl();
            this.tabPageApplication = new System.Windows.Forms.TabPage();
            this.richTextBoxApplication = new System.Windows.Forms.RichTextBox();
            this.tabPageLicense = new System.Windows.Forms.TabPage();
            this.richTextBoxLicense = new System.Windows.Forms.RichTextBox();
            this.tabPageChangelog = new System.Windows.Forms.TabPage();
            this.richTextBoxChangelog = new System.Windows.Forms.RichTextBox();
            this.tabPageDeveloper = new System.Windows.Forms.TabPage();
            this.richTextBoxDeveloper = new System.Windows.Forms.RichTextBox();
            this.tabPageReleaseNotes = new System.Windows.Forms.TabPage();
            this.richTextBoxReleaseNotes = new System.Windows.Forms.RichTextBox();
            this.tabControlAbout.SuspendLayout();
            this.tabPageApplication.SuspendLayout();
            this.tabPageLicense.SuspendLayout();
            this.tabPageChangelog.SuspendLayout();
            this.tabPageDeveloper.SuspendLayout();
            this.tabPageReleaseNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAbout
            // 
            this.tabControlAbout.Controls.Add(this.tabPageApplication);
            this.tabControlAbout.Controls.Add(this.tabPageReleaseNotes);
            this.tabControlAbout.Controls.Add(this.tabPageChangelog);
            this.tabControlAbout.Controls.Add(this.tabPageLicense);
            this.tabControlAbout.Controls.Add(this.tabPageDeveloper);
            this.tabControlAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAbout.Location = new System.Drawing.Point(0, 0);
            this.tabControlAbout.Name = "tabControlAbout";
            this.tabControlAbout.SelectedIndex = 0;
            this.tabControlAbout.Size = new System.Drawing.Size(553, 319);
            this.tabControlAbout.TabIndex = 0;
            this.tabControlAbout.TabStop = false;
            // 
            // tabPageApplication
            // 
            this.tabPageApplication.Controls.Add(this.richTextBoxApplication);
            this.tabPageApplication.Location = new System.Drawing.Point(4, 22);
            this.tabPageApplication.Name = "tabPageApplication";
            this.tabPageApplication.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageApplication.Size = new System.Drawing.Size(545, 293);
            this.tabPageApplication.TabIndex = 0;
            this.tabPageApplication.Text = "Application";
            this.tabPageApplication.UseVisualStyleBackColor = true;
            // 
            // richTextBoxApplication
            // 
            this.richTextBoxApplication.BackColor = System.Drawing.Color.White;
            this.richTextBoxApplication.DetectUrls = false;
            this.richTextBoxApplication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxApplication.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxApplication.Name = "richTextBoxApplication";
            this.richTextBoxApplication.ReadOnly = true;
            this.richTextBoxApplication.Size = new System.Drawing.Size(539, 287);
            this.richTextBoxApplication.TabIndex = 0;
            this.richTextBoxApplication.TabStop = false;
            this.richTextBoxApplication.Text = resources.GetString("richTextBoxApplication.Text");
            // 
            // tabPageLicense
            // 
            this.tabPageLicense.Controls.Add(this.richTextBoxLicense);
            this.tabPageLicense.Location = new System.Drawing.Point(4, 22);
            this.tabPageLicense.Name = "tabPageLicense";
            this.tabPageLicense.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLicense.Size = new System.Drawing.Size(545, 293);
            this.tabPageLicense.TabIndex = 1;
            this.tabPageLicense.Text = "License";
            this.tabPageLicense.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLicense
            // 
            this.richTextBoxLicense.BackColor = System.Drawing.Color.White;
            this.richTextBoxLicense.DetectUrls = false;
            this.richTextBoxLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxLicense.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxLicense.Name = "richTextBoxLicense";
            this.richTextBoxLicense.ReadOnly = true;
            this.richTextBoxLicense.Size = new System.Drawing.Size(539, 287);
            this.richTextBoxLicense.TabIndex = 0;
            this.richTextBoxLicense.TabStop = false;
            this.richTextBoxLicense.Text = resources.GetString("richTextBoxLicense.Text");
            // 
            // tabPageChangelog
            // 
            this.tabPageChangelog.Controls.Add(this.richTextBoxChangelog);
            this.tabPageChangelog.Location = new System.Drawing.Point(4, 22);
            this.tabPageChangelog.Name = "tabPageChangelog";
            this.tabPageChangelog.Size = new System.Drawing.Size(545, 293);
            this.tabPageChangelog.TabIndex = 2;
            this.tabPageChangelog.Text = "Changelog";
            this.tabPageChangelog.UseVisualStyleBackColor = true;
            // 
            // richTextBoxChangelog
            // 
            this.richTextBoxChangelog.BackColor = System.Drawing.Color.White;
            this.richTextBoxChangelog.DetectUrls = false;
            this.richTextBoxChangelog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxChangelog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxChangelog.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxChangelog.Name = "richTextBoxChangelog";
            this.richTextBoxChangelog.ReadOnly = true;
            this.richTextBoxChangelog.Size = new System.Drawing.Size(545, 293);
            this.richTextBoxChangelog.TabIndex = 1;
            this.richTextBoxChangelog.TabStop = false;
            this.richTextBoxChangelog.Text = resources.GetString("richTextBoxChangelog.Text");
            this.richTextBoxChangelog.WordWrap = false;
            // 
            // tabPageDeveloper
            // 
            this.tabPageDeveloper.Controls.Add(this.richTextBoxDeveloper);
            this.tabPageDeveloper.Location = new System.Drawing.Point(4, 22);
            this.tabPageDeveloper.Name = "tabPageDeveloper";
            this.tabPageDeveloper.Size = new System.Drawing.Size(545, 293);
            this.tabPageDeveloper.TabIndex = 3;
            this.tabPageDeveloper.Text = "Developer";
            this.tabPageDeveloper.UseVisualStyleBackColor = true;
            // 
            // richTextBoxDeveloper
            // 
            this.richTextBoxDeveloper.BackColor = System.Drawing.Color.White;
            this.richTextBoxDeveloper.DetectUrls = false;
            this.richTextBoxDeveloper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDeveloper.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxDeveloper.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxDeveloper.Name = "richTextBoxDeveloper";
            this.richTextBoxDeveloper.ReadOnly = true;
            this.richTextBoxDeveloper.Size = new System.Drawing.Size(545, 293);
            this.richTextBoxDeveloper.TabIndex = 1;
            this.richTextBoxDeveloper.TabStop = false;
            this.richTextBoxDeveloper.Text = "";
            // 
            // tabPageReleaseNotes
            // 
            this.tabPageReleaseNotes.Controls.Add(this.richTextBoxReleaseNotes);
            this.tabPageReleaseNotes.Location = new System.Drawing.Point(4, 22);
            this.tabPageReleaseNotes.Name = "tabPageReleaseNotes";
            this.tabPageReleaseNotes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReleaseNotes.Size = new System.Drawing.Size(545, 293);
            this.tabPageReleaseNotes.TabIndex = 3;
            this.tabPageReleaseNotes.Text = "Release Notes";
            this.tabPageReleaseNotes.UseVisualStyleBackColor = true;
            // 
            // richTextBoxReleaseNotes
            // 
            this.richTextBoxReleaseNotes.BackColor = System.Drawing.Color.White;
            this.richTextBoxReleaseNotes.DetectUrls = false;
            this.richTextBoxReleaseNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxReleaseNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxReleaseNotes.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxReleaseNotes.Name = "richTextBoxReleaseNotes";
            this.richTextBoxReleaseNotes.ReadOnly = true;
            this.richTextBoxReleaseNotes.Size = new System.Drawing.Size(539, 287);
            this.richTextBoxReleaseNotes.TabIndex = 1;
            this.richTextBoxReleaseNotes.TabStop = false;
            this.richTextBoxReleaseNotes.Text = "";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 319);
            this.Controls.Add(this.tabControlAbout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(569, 358);
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Auto Screen Capture";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAbout_FormClosing);
            this.tabControlAbout.ResumeLayout(false);
            this.tabPageApplication.ResumeLayout(false);
            this.tabPageLicense.ResumeLayout(false);
            this.tabPageChangelog.ResumeLayout(false);
            this.tabPageDeveloper.ResumeLayout(false);
            this.tabPageReleaseNotes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlAbout;
        private System.Windows.Forms.TabPage tabPageApplication;
        private System.Windows.Forms.TabPage tabPageLicense;
        private System.Windows.Forms.RichTextBox richTextBoxApplication;
        private System.Windows.Forms.RichTextBox richTextBoxLicense;
        private System.Windows.Forms.TabPage tabPageChangelog;
        private System.Windows.Forms.RichTextBox richTextBoxChangelog;
        private System.Windows.Forms.TabPage tabPageDeveloper;
        private System.Windows.Forms.RichTextBox richTextBoxDeveloper;
        private System.Windows.Forms.TabPage tabPageReleaseNotes;
        private System.Windows.Forms.RichTextBox richTextBoxReleaseNotes;
    }
}