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
            this.tabPageDeveloper = new System.Windows.Forms.TabPage();
            this.richTextBoxDeveloper = new System.Windows.Forms.RichTextBox();
            this.tabPageLicense = new System.Windows.Forms.TabPage();
            this.richTextBoxLicense = new System.Windows.Forms.RichTextBox();
            this.tabPageChangelog = new System.Windows.Forms.TabPage();
            this.richTextBoxChangelog = new System.Windows.Forms.RichTextBox();
            this.tabPageWhatsNew = new System.Windows.Forms.TabPage();
            this.richTextBoxWhatsNew = new System.Windows.Forms.RichTextBox();
            this.tabControlAbout.SuspendLayout();
            this.tabPageDeveloper.SuspendLayout();
            this.tabPageLicense.SuspendLayout();
            this.tabPageChangelog.SuspendLayout();
            this.tabPageWhatsNew.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAbout
            // 
            this.tabControlAbout.Controls.Add(this.tabPageDeveloper);
            this.tabControlAbout.Controls.Add(this.tabPageLicense);
            this.tabControlAbout.Controls.Add(this.tabPageChangelog);
            this.tabControlAbout.Controls.Add(this.tabPageWhatsNew);
            this.tabControlAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAbout.Location = new System.Drawing.Point(0, 0);
            this.tabControlAbout.Name = "tabControlAbout";
            this.tabControlAbout.SelectedIndex = 0;
            this.tabControlAbout.Size = new System.Drawing.Size(463, 288);
            this.tabControlAbout.TabIndex = 0;
            this.tabControlAbout.TabStop = false;
            // 
            // tabPageDeveloper
            // 
            this.tabPageDeveloper.Controls.Add(this.richTextBoxDeveloper);
            this.tabPageDeveloper.Location = new System.Drawing.Point(4, 22);
            this.tabPageDeveloper.Name = "tabPageDeveloper";
            this.tabPageDeveloper.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDeveloper.Size = new System.Drawing.Size(455, 262);
            this.tabPageDeveloper.TabIndex = 0;
            this.tabPageDeveloper.Text = "Developer";
            this.tabPageDeveloper.UseVisualStyleBackColor = true;
            // 
            // richTextBoxDeveloper
            // 
            this.richTextBoxDeveloper.DetectUrls = false;
            this.richTextBoxDeveloper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDeveloper.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxDeveloper.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxDeveloper.Name = "richTextBoxDeveloper";
            this.richTextBoxDeveloper.ReadOnly = true;
            this.richTextBoxDeveloper.Size = new System.Drawing.Size(449, 256);
            this.richTextBoxDeveloper.TabIndex = 0;
            this.richTextBoxDeveloper.TabStop = false;
            this.richTextBoxDeveloper.Text = resources.GetString("richTextBoxDeveloper.Text");
            // 
            // tabPageLicense
            // 
            this.tabPageLicense.Controls.Add(this.richTextBoxLicense);
            this.tabPageLicense.Location = new System.Drawing.Point(4, 22);
            this.tabPageLicense.Name = "tabPageLicense";
            this.tabPageLicense.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLicense.Size = new System.Drawing.Size(455, 262);
            this.tabPageLicense.TabIndex = 1;
            this.tabPageLicense.Text = "License";
            this.tabPageLicense.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLicense
            // 
            this.richTextBoxLicense.DetectUrls = false;
            this.richTextBoxLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxLicense.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxLicense.Name = "richTextBoxLicense";
            this.richTextBoxLicense.ReadOnly = true;
            this.richTextBoxLicense.Size = new System.Drawing.Size(449, 256);
            this.richTextBoxLicense.TabIndex = 0;
            this.richTextBoxLicense.TabStop = false;
            this.richTextBoxLicense.Text = resources.GetString("richTextBoxLicense.Text");
            // 
            // tabPageChangelog
            // 
            this.tabPageChangelog.Controls.Add(this.richTextBoxChangelog);
            this.tabPageChangelog.Location = new System.Drawing.Point(4, 22);
            this.tabPageChangelog.Name = "tabPageChangelog";
            this.tabPageChangelog.Size = new System.Drawing.Size(455, 262);
            this.tabPageChangelog.TabIndex = 2;
            this.tabPageChangelog.Text = "Changelog";
            this.tabPageChangelog.UseVisualStyleBackColor = true;
            // 
            // richTextBoxChangelog
            // 
            this.richTextBoxChangelog.DetectUrls = false;
            this.richTextBoxChangelog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxChangelog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxChangelog.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxChangelog.Name = "richTextBoxChangelog";
            this.richTextBoxChangelog.ReadOnly = true;
            this.richTextBoxChangelog.Size = new System.Drawing.Size(455, 262);
            this.richTextBoxChangelog.TabIndex = 1;
            this.richTextBoxChangelog.TabStop = false;
            this.richTextBoxChangelog.Text = resources.GetString("richTextBoxChangelog.Text");
            this.richTextBoxChangelog.WordWrap = false;
            // 
            // tabPageWhatsNew
            // 
            this.tabPageWhatsNew.Controls.Add(this.richTextBoxWhatsNew);
            this.tabPageWhatsNew.Location = new System.Drawing.Point(4, 22);
            this.tabPageWhatsNew.Name = "tabPageWhatsNew";
            this.tabPageWhatsNew.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWhatsNew.Size = new System.Drawing.Size(455, 262);
            this.tabPageWhatsNew.TabIndex = 3;
            this.tabPageWhatsNew.Text = "What\'s New";
            this.tabPageWhatsNew.UseVisualStyleBackColor = true;
            // 
            // richTextBoxWhatsNew
            // 
            this.richTextBoxWhatsNew.DetectUrls = false;
            this.richTextBoxWhatsNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxWhatsNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxWhatsNew.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxWhatsNew.Name = "richTextBoxWhatsNew";
            this.richTextBoxWhatsNew.ReadOnly = true;
            this.richTextBoxWhatsNew.Size = new System.Drawing.Size(449, 256);
            this.richTextBoxWhatsNew.TabIndex = 1;
            this.richTextBoxWhatsNew.TabStop = false;
            this.richTextBoxWhatsNew.Text = resources.GetString("richTextBoxWhatsNew.Text");
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 288);
            this.Controls.Add(this.tabControlAbout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(479, 327);
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Auto Screen Capture";
            this.tabControlAbout.ResumeLayout(false);
            this.tabPageDeveloper.ResumeLayout(false);
            this.tabPageLicense.ResumeLayout(false);
            this.tabPageChangelog.ResumeLayout(false);
            this.tabPageWhatsNew.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlAbout;
        private System.Windows.Forms.TabPage tabPageDeveloper;
        private System.Windows.Forms.TabPage tabPageLicense;
        private System.Windows.Forms.RichTextBox richTextBoxDeveloper;
        private System.Windows.Forms.RichTextBox richTextBoxLicense;
        private System.Windows.Forms.TabPage tabPageChangelog;
        private System.Windows.Forms.RichTextBox richTextBoxChangelog;
        private System.Windows.Forms.TabPage tabPageWhatsNew;
        private System.Windows.Forms.RichTextBox richTextBoxWhatsNew;
    }
}