namespace AutoScreenCapture
{
    partial class FormRegionSelectCommandDeck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegionSelectCommandDeck));
            this.buttonRegionSelectClipboard = new System.Windows.Forms.Button();
            this.buttonRegionSelectClipboardAutoSave = new System.Windows.Forms.Button();
            this.buttonRegionSelectClipboardAutoSaveEdit = new System.Windows.Forms.Button();
            this.buttonRegionSelectClipboardFloatingScreenshot = new System.Windows.Forms.Button();
            this.buttonRegionSelectFloatingScreenshot = new System.Windows.Forms.Button();
            this.buttonRegionSelectNewRegion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRegionSelectClipboard
            // 
            this.buttonRegionSelectClipboard.Location = new System.Drawing.Point(12, 12);
            this.buttonRegionSelectClipboard.Name = "buttonRegionSelectClipboard";
            this.buttonRegionSelectClipboard.Size = new System.Drawing.Size(331, 23);
            this.buttonRegionSelectClipboard.TabIndex = 0;
            this.buttonRegionSelectClipboard.Text = "Region Select / Clipboard";
            this.buttonRegionSelectClipboard.UseVisualStyleBackColor = true;
            this.buttonRegionSelectClipboard.Click += new System.EventHandler(this.buttonRegionSelectClipboard_Click);
            // 
            // buttonRegionSelectClipboardAutoSave
            // 
            this.buttonRegionSelectClipboardAutoSave.Location = new System.Drawing.Point(12, 41);
            this.buttonRegionSelectClipboardAutoSave.Name = "buttonRegionSelectClipboardAutoSave";
            this.buttonRegionSelectClipboardAutoSave.Size = new System.Drawing.Size(331, 23);
            this.buttonRegionSelectClipboardAutoSave.TabIndex = 1;
            this.buttonRegionSelectClipboardAutoSave.Text = "Region Select / Clipboard / Auto Save";
            this.buttonRegionSelectClipboardAutoSave.UseVisualStyleBackColor = true;
            this.buttonRegionSelectClipboardAutoSave.Click += new System.EventHandler(this.buttonRegionSelectClipboardAutoSave_Click);
            // 
            // buttonRegionSelectClipboardAutoSaveEdit
            // 
            this.buttonRegionSelectClipboardAutoSaveEdit.Location = new System.Drawing.Point(12, 70);
            this.buttonRegionSelectClipboardAutoSaveEdit.Name = "buttonRegionSelectClipboardAutoSaveEdit";
            this.buttonRegionSelectClipboardAutoSaveEdit.Size = new System.Drawing.Size(331, 23);
            this.buttonRegionSelectClipboardAutoSaveEdit.TabIndex = 2;
            this.buttonRegionSelectClipboardAutoSaveEdit.Text = "Region Select / Clipboard / Auto Save / Edit";
            this.buttonRegionSelectClipboardAutoSaveEdit.UseVisualStyleBackColor = true;
            this.buttonRegionSelectClipboardAutoSaveEdit.Click += new System.EventHandler(this.buttonRegionSelectClipboardAutoSaveEdit_Click);
            // 
            // buttonRegionSelectClipboardFloatingScreenshot
            // 
            this.buttonRegionSelectClipboardFloatingScreenshot.Location = new System.Drawing.Point(12, 99);
            this.buttonRegionSelectClipboardFloatingScreenshot.Name = "buttonRegionSelectClipboardFloatingScreenshot";
            this.buttonRegionSelectClipboardFloatingScreenshot.Size = new System.Drawing.Size(331, 23);
            this.buttonRegionSelectClipboardFloatingScreenshot.TabIndex = 3;
            this.buttonRegionSelectClipboardFloatingScreenshot.Text = "Region Select / Clipboard / Floating Screenshot";
            this.buttonRegionSelectClipboardFloatingScreenshot.UseVisualStyleBackColor = true;
            this.buttonRegionSelectClipboardFloatingScreenshot.Click += new System.EventHandler(this.buttonRegionSelectClipboardFloatingScreenshot_Click);
            // 
            // buttonRegionSelectFloatingScreenshot
            // 
            this.buttonRegionSelectFloatingScreenshot.Location = new System.Drawing.Point(12, 128);
            this.buttonRegionSelectFloatingScreenshot.Name = "buttonRegionSelectFloatingScreenshot";
            this.buttonRegionSelectFloatingScreenshot.Size = new System.Drawing.Size(331, 23);
            this.buttonRegionSelectFloatingScreenshot.TabIndex = 4;
            this.buttonRegionSelectFloatingScreenshot.Text = "Region Select / Floating Screenshot";
            this.buttonRegionSelectFloatingScreenshot.UseVisualStyleBackColor = true;
            this.buttonRegionSelectFloatingScreenshot.Click += new System.EventHandler(this.buttonRegionSelectFloatingScreenshot_Click);
            // 
            // buttonRegionSelectNewRegion
            // 
            this.buttonRegionSelectNewRegion.Location = new System.Drawing.Point(12, 157);
            this.buttonRegionSelectNewRegion.Name = "buttonRegionSelectNewRegion";
            this.buttonRegionSelectNewRegion.Size = new System.Drawing.Size(331, 23);
            this.buttonRegionSelectNewRegion.TabIndex = 5;
            this.buttonRegionSelectNewRegion.Text = "Region Select / New Region";
            this.buttonRegionSelectNewRegion.UseVisualStyleBackColor = true;
            this.buttonRegionSelectNewRegion.Click += new System.EventHandler(this.buttonRegionSelectNewRegion_Click);
            // 
            // FormRegionSelectCommandDeck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 190);
            this.Controls.Add(this.buttonRegionSelectNewRegion);
            this.Controls.Add(this.buttonRegionSelectFloatingScreenshot);
            this.Controls.Add(this.buttonRegionSelectClipboardFloatingScreenshot);
            this.Controls.Add(this.buttonRegionSelectClipboardAutoSaveEdit);
            this.Controls.Add(this.buttonRegionSelectClipboardAutoSave);
            this.Controls.Add(this.buttonRegionSelectClipboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRegionSelectCommandDeck";
            this.Text = "Auto Screen Capture - Region Select Command Deck";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRegionSelectCommandDeck_FormClosing);
            this.Load += new System.EventHandler(this.FormRegionSelectCommandDeck_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRegionSelectClipboard;
        private System.Windows.Forms.Button buttonRegionSelectClipboardAutoSave;
        private System.Windows.Forms.Button buttonRegionSelectClipboardAutoSaveEdit;
        private System.Windows.Forms.Button buttonRegionSelectClipboardFloatingScreenshot;
        private System.Windows.Forms.Button buttonRegionSelectFloatingScreenshot;
        private System.Windows.Forms.Button buttonRegionSelectNewRegion;
    }
}