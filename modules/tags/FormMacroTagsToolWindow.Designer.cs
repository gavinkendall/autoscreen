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
            this.SuspendLayout();
            // 
            // listBoxMacroTags
            // 
            this.listBoxMacroTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMacroTags.FormattingEnabled = true;
            this.listBoxMacroTags.HorizontalScrollbar = true;
            this.listBoxMacroTags.Location = new System.Drawing.Point(0, 0);
            this.listBoxMacroTags.Name = "listBoxMacroTags";
            this.listBoxMacroTags.ScrollAlwaysVisible = true;
            this.listBoxMacroTags.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxMacroTags.Size = new System.Drawing.Size(184, 391);
            this.listBoxMacroTags.TabIndex = 0;
            this.listBoxMacroTags.TabStop = false;
            // 
            // FormMacroTagsToolWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(184, 391);
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
    }
}