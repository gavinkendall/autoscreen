namespace AutoScreenCapture
{
    partial class FormLabels
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
            this.checkBoxScreenshotLabel = new System.Windows.Forms.CheckBox();
            this.comboBoxScreenshotLabel = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // checkBoxScreenshotLabel
            // 
            this.checkBoxScreenshotLabel.AutoSize = true;
            this.checkBoxScreenshotLabel.Location = new System.Drawing.Point(298, 206);
            this.checkBoxScreenshotLabel.Name = "checkBoxScreenshotLabel";
            this.checkBoxScreenshotLabel.Size = new System.Drawing.Size(193, 17);
            this.checkBoxScreenshotLabel.TabIndex = 1;
            this.checkBoxScreenshotLabel.TabStop = false;
            this.checkBoxScreenshotLabel.Text = "Apply this label to each screenshot:";
            this.checkBoxScreenshotLabel.UseVisualStyleBackColor = true;
            // 
            // comboBoxScreenshotLabel
            // 
            this.comboBoxScreenshotLabel.FormattingEnabled = true;
            this.comboBoxScreenshotLabel.Location = new System.Drawing.Point(298, 224);
            this.comboBoxScreenshotLabel.MaxDropDownItems = 10;
            this.comboBoxScreenshotLabel.MaxLength = 500;
            this.comboBoxScreenshotLabel.Name = "comboBoxScreenshotLabel";
            this.comboBoxScreenshotLabel.Size = new System.Drawing.Size(205, 21);
            this.comboBoxScreenshotLabel.TabIndex = 2;
            this.comboBoxScreenshotLabel.TabStop = false;
            // 
            // FormLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBoxScreenshotLabel);
            this.Controls.Add(this.comboBoxScreenshotLabel);
            this.Name = "FormLabels";
            this.Text = "FormLabels";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.CheckBox checkBoxScreenshotLabel;
    public System.Windows.Forms.ComboBox comboBoxScreenshotLabel;
}
}