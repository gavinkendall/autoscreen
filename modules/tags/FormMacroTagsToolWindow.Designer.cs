namespace AutoScreenCapture
{
    partial class FormMacroTagsToolWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMacroTagsToolWindow));
            this.listBoxMacroTags = new System.Windows.Forms.ListBox();
            this.labelHelpInfoIcon = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxMacroTags
            // 
            this.listBoxMacroTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMacroTags.FormattingEnabled = true;
            this.listBoxMacroTags.HorizontalScrollbar = true;
            this.listBoxMacroTags.Location = new System.Drawing.Point(3, 46);
            this.listBoxMacroTags.Name = "listBoxMacroTags";
            this.listBoxMacroTags.ScrollAlwaysVisible = true;
            this.listBoxMacroTags.Size = new System.Drawing.Size(357, 212);
            this.listBoxMacroTags.TabIndex = 3;
            this.listBoxMacroTags.TabStop = false;
            this.listBoxMacroTags.SelectedIndexChanged += new System.EventHandler(this.listBoxMacroTags_SelectedIndexChanged);
            // 
            // labelHelpInfoIcon
            // 
            this.labelHelpInfoIcon.AutoEllipsis = true;
            this.labelHelpInfoIcon.BackColor = System.Drawing.Color.LightYellow;
            this.labelHelpInfoIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelHelpInfoIcon.Image = global::AutoScreenCapture.Properties.Resources.about;
            this.labelHelpInfoIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelHelpInfoIcon.Location = new System.Drawing.Point(0, 0);
            this.labelHelpInfoIcon.Name = "labelHelpInfoIcon";
            this.labelHelpInfoIcon.Size = new System.Drawing.Size(22, 43);
            this.labelHelpInfoIcon.TabIndex = 0;
            this.labelHelpInfoIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelHelp
            // 
            this.labelHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHelp.AutoEllipsis = true;
            this.labelHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelHelp.Location = new System.Drawing.Point(22, 0);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(338, 43);
            this.labelHelp.TabIndex = 1;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormMacroTagsToolWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(360, 261);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.labelHelpInfoIcon);
            this.Controls.Add(this.listBoxMacroTags);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "FormMacroTagsToolWindow";
            this.Text = "Macro Tags";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormMacroTagsToolWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxMacroTags;
        private System.Windows.Forms.Label labelHelpInfoIcon;
        private System.Windows.Forms.Label labelHelp;
    }
}