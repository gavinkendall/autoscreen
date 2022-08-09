namespace AutoScreenCapture
{
    partial class FormDynamicRegexValidator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDynamicRegexValidator));
            this.labelRegularExpression = new System.Windows.Forms.Label();
            this.labelTestValue = new System.Windows.Forms.Label();
            this.textBoxRegularExpression = new System.Windows.Forms.TextBox();
            this.textBoxTestValue = new System.Windows.Forms.TextBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelRegularExpression
            // 
            this.labelRegularExpression.AutoSize = true;
            this.labelRegularExpression.Location = new System.Drawing.Point(12, 36);
            this.labelRegularExpression.Name = "labelRegularExpression";
            this.labelRegularExpression.Size = new System.Drawing.Size(101, 13);
            this.labelRegularExpression.TabIndex = 0;
            this.labelRegularExpression.Text = "Regular Expression:";
            // 
            // labelTestValue
            // 
            this.labelTestValue.AutoSize = true;
            this.labelTestValue.Location = new System.Drawing.Point(12, 68);
            this.labelTestValue.Name = "labelTestValue";
            this.labelTestValue.Size = new System.Drawing.Size(61, 13);
            this.labelTestValue.TabIndex = 1;
            this.labelTestValue.Text = "Test Value:";
            // 
            // textBoxRegularExpression
            // 
            this.textBoxRegularExpression.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRegularExpression.Location = new System.Drawing.Point(119, 33);
            this.textBoxRegularExpression.Name = "textBoxRegularExpression";
            this.textBoxRegularExpression.Size = new System.Drawing.Size(410, 25);
            this.textBoxRegularExpression.TabIndex = 1;
            this.textBoxRegularExpression.TextChanged += new System.EventHandler(this.textBoxRegularExpression_TextChanged);
            // 
            // textBoxTestValue
            // 
            this.textBoxTestValue.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTestValue.Location = new System.Drawing.Point(119, 63);
            this.textBoxTestValue.Name = "textBoxTestValue";
            this.textBoxTestValue.Size = new System.Drawing.Size(410, 25);
            this.textBoxTestValue.TabIndex = 2;
            this.textBoxTestValue.TextChanged += new System.EventHandler(this.textBoxText_TextChanged);
            // 
            // labelHelp
            // 
            this.labelHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHelp.AutoEllipsis = true;
            this.labelHelp.BackColor = System.Drawing.Color.LightYellow;
            this.labelHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelHelp.Image = global::AutoScreenCapture.Properties.Resources.about;
            this.labelHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelHelp.Location = new System.Drawing.Point(2, 4);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(557, 17);
            this.labelHelp.TabIndex = 0;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 95);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(541, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 0;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // FormDynamicRegexValidator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 117);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.textBoxTestValue);
            this.Controls.Add(this.textBoxRegularExpression);
            this.Controls.Add(this.labelTestValue);
            this.Controls.Add(this.labelRegularExpression);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(557, 118);
            this.Name = "FormDynamicRegexValidator";
            this.Text = "Dynamic Regex Validator";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDynamicRegexValidator_FormClosing);
            this.Load += new System.EventHandler(this.FormDynamicRegexValidator_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRegularExpression;
        private System.Windows.Forms.Label labelTestValue;
        private System.Windows.Forms.TextBox textBoxRegularExpression;
        private System.Windows.Forms.TextBox textBoxTestValue;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}