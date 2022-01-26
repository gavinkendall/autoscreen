namespace AutoScreenCapture
{
    partial class FormEncryptorDecryptor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEncryptorDecryptor));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageScreenshots = new System.Windows.Forms.TabPage();
            this.tabPageFile = new System.Windows.Forms.TabPage();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.dateTimePickerScreenshotsStartDateRange = new System.Windows.Forms.DateTimePicker();
            this.labelScreenshotsStartDateRange = new System.Windows.Forms.Label();
            this.labelScreenshotsEndDateRange = new System.Windows.Forms.Label();
            this.dateTimePickerScreenshotsEndDateRange = new System.Windows.Forms.DateTimePicker();
            this.buttonEncryptScreenshots = new System.Windows.Forms.Button();
            this.buttonDecryptScreenshots = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageScreenshots.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageScreenshots);
            this.tabControl.Controls.Add(this.tabPageFile);
            this.tabControl.Controls.Add(this.tabPageText);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 450);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageScreenshots
            // 
            this.tabPageScreenshots.Controls.Add(this.buttonDecryptScreenshots);
            this.tabPageScreenshots.Controls.Add(this.buttonEncryptScreenshots);
            this.tabPageScreenshots.Controls.Add(this.dateTimePickerScreenshotsEndDateRange);
            this.tabPageScreenshots.Controls.Add(this.labelScreenshotsEndDateRange);
            this.tabPageScreenshots.Controls.Add(this.labelScreenshotsStartDateRange);
            this.tabPageScreenshots.Controls.Add(this.dateTimePickerScreenshotsStartDateRange);
            this.tabPageScreenshots.Location = new System.Drawing.Point(4, 22);
            this.tabPageScreenshots.Name = "tabPageScreenshots";
            this.tabPageScreenshots.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScreenshots.Size = new System.Drawing.Size(792, 424);
            this.tabPageScreenshots.TabIndex = 0;
            this.tabPageScreenshots.Text = "Screenshots";
            this.tabPageScreenshots.UseVisualStyleBackColor = true;
            // 
            // tabPageFile
            // 
            this.tabPageFile.Location = new System.Drawing.Point(4, 22);
            this.tabPageFile.Name = "tabPageFile";
            this.tabPageFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFile.Size = new System.Drawing.Size(792, 424);
            this.tabPageFile.TabIndex = 1;
            this.tabPageFile.Text = "File";
            this.tabPageFile.UseVisualStyleBackColor = true;
            // 
            // tabPageText
            // 
            this.tabPageText.Location = new System.Drawing.Point(4, 22);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Size = new System.Drawing.Size(792, 424);
            this.tabPageText.TabIndex = 2;
            this.tabPageText.Text = "Text";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerScreenshotsStartDateRange
            // 
            this.dateTimePickerScreenshotsStartDateRange.Location = new System.Drawing.Point(11, 96);
            this.dateTimePickerScreenshotsStartDateRange.Name = "dateTimePickerScreenshotsStartDateRange";
            this.dateTimePickerScreenshotsStartDateRange.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerScreenshotsStartDateRange.TabIndex = 0;
            this.dateTimePickerScreenshotsStartDateRange.TabStop = false;
            // 
            // labelScreenshotsStartDateRange
            // 
            this.labelScreenshotsStartDateRange.AutoSize = true;
            this.labelScreenshotsStartDateRange.Location = new System.Drawing.Point(8, 80);
            this.labelScreenshotsStartDateRange.Name = "labelScreenshotsStartDateRange";
            this.labelScreenshotsStartDateRange.Size = new System.Drawing.Size(93, 13);
            this.labelScreenshotsStartDateRange.TabIndex = 1;
            this.labelScreenshotsStartDateRange.Text = "Start Date Range:";
            // 
            // labelScreenshotsEndDateRange
            // 
            this.labelScreenshotsEndDateRange.AutoSize = true;
            this.labelScreenshotsEndDateRange.Location = new System.Drawing.Point(257, 80);
            this.labelScreenshotsEndDateRange.Name = "labelScreenshotsEndDateRange";
            this.labelScreenshotsEndDateRange.Size = new System.Drawing.Size(90, 13);
            this.labelScreenshotsEndDateRange.TabIndex = 2;
            this.labelScreenshotsEndDateRange.Text = "End Date Range:";
            // 
            // dateTimePickerScreenshotsEndDateRange
            // 
            this.dateTimePickerScreenshotsEndDateRange.Location = new System.Drawing.Point(260, 96);
            this.dateTimePickerScreenshotsEndDateRange.Name = "dateTimePickerScreenshotsEndDateRange";
            this.dateTimePickerScreenshotsEndDateRange.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerScreenshotsEndDateRange.TabIndex = 3;
            this.dateTimePickerScreenshotsEndDateRange.TabStop = false;
            // 
            // buttonEncryptScreenshots
            // 
            this.buttonEncryptScreenshots.Location = new System.Drawing.Point(11, 266);
            this.buttonEncryptScreenshots.Name = "buttonEncryptScreenshots";
            this.buttonEncryptScreenshots.Size = new System.Drawing.Size(150, 23);
            this.buttonEncryptScreenshots.TabIndex = 4;
            this.buttonEncryptScreenshots.TabStop = false;
            this.buttonEncryptScreenshots.Text = "Encrypt Screenshots";
            this.buttonEncryptScreenshots.UseVisualStyleBackColor = true;
            // 
            // buttonDecryptScreenshots
            // 
            this.buttonDecryptScreenshots.Location = new System.Drawing.Point(167, 266);
            this.buttonDecryptScreenshots.Name = "buttonDecryptScreenshots";
            this.buttonDecryptScreenshots.Size = new System.Drawing.Size(150, 23);
            this.buttonDecryptScreenshots.TabIndex = 5;
            this.buttonDecryptScreenshots.TabStop = false;
            this.buttonDecryptScreenshots.Text = "Decrypt Screenshots";
            this.buttonDecryptScreenshots.UseVisualStyleBackColor = true;
            // 
            // FormEncryptorDecryptor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormEncryptorDecryptor";
            this.Text = "Auto Screen Capture - Encryptor / Decryptor";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEncryptorDecryptor_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPageScreenshots.ResumeLayout(false);
            this.tabPageScreenshots.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageScreenshots;
        private System.Windows.Forms.TabPage tabPageFile;
        private System.Windows.Forms.TabPage tabPageText;
        private System.Windows.Forms.DateTimePicker dateTimePickerScreenshotsEndDateRange;
        private System.Windows.Forms.Label labelScreenshotsEndDateRange;
        private System.Windows.Forms.Label labelScreenshotsStartDateRange;
        private System.Windows.Forms.DateTimePicker dateTimePickerScreenshotsStartDateRange;
        private System.Windows.Forms.Button buttonDecryptScreenshots;
        private System.Windows.Forms.Button buttonEncryptScreenshots;
    }
}