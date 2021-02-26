namespace AutoScreenCapture
{
    partial class FormHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHelp));
            this.listBoxHelpItems = new System.Windows.Forms.ListBox();
            this.richTextBoxHelpText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // listBoxHelpItems
            // 
            this.listBoxHelpItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxHelpItems.FormattingEnabled = true;
            this.listBoxHelpItems.IntegralHeight = false;
            this.listBoxHelpItems.Location = new System.Drawing.Point(12, 12);
            this.listBoxHelpItems.Name = "listBoxHelpItems";
            this.listBoxHelpItems.Size = new System.Drawing.Size(165, 368);
            this.listBoxHelpItems.TabIndex = 0;
            this.listBoxHelpItems.TabStop = false;
            this.listBoxHelpItems.SelectedIndexChanged += new System.EventHandler(this.listBoxHelpItems_SelectedIndexChanged);
            // 
            // richTextBoxHelpText
            // 
            this.richTextBoxHelpText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxHelpText.BackColor = System.Drawing.Color.White;
            this.richTextBoxHelpText.CausesValidation = false;
            this.richTextBoxHelpText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.richTextBoxHelpText.DetectUrls = false;
            this.richTextBoxHelpText.Location = new System.Drawing.Point(183, 12);
            this.richTextBoxHelpText.Name = "richTextBoxHelpText";
            this.richTextBoxHelpText.ReadOnly = true;
            this.richTextBoxHelpText.Size = new System.Drawing.Size(493, 368);
            this.richTextBoxHelpText.TabIndex = 0;
            this.richTextBoxHelpText.TabStop = false;
            this.richTextBoxHelpText.Text = "";
            // 
            // FormHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 394);
            this.Controls.Add(this.richTextBoxHelpText);
            this.Controls.Add(this.listBoxHelpItems);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(704, 433);
            this.Name = "FormHelp";
            this.Text = "Auto Screen Capture - Help (BETA)";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHelp_FormClosing);
            this.Load += new System.EventHandler(this.FormHelp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxHelpItems;
        private System.Windows.Forms.RichTextBox richTextBoxHelpText;
    }
}