namespace AutoScreenCapture
{
    partial class FormSecurity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSecurity));
            this.groupBoxSecurity = new System.Windows.Forms.GroupBox();
            this.labelPasswordDescription = new System.Windows.Forms.Label();
            this.buttonSetPassphrase = new System.Windows.Forms.Button();
            this.textBoxPassphrase = new System.Windows.Forms.TextBox();
            this.groupBoxSecurity.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSecurity
            // 
            this.groupBoxSecurity.Controls.Add(this.labelPasswordDescription);
            this.groupBoxSecurity.Controls.Add(this.buttonSetPassphrase);
            this.groupBoxSecurity.Controls.Add(this.textBoxPassphrase);
            this.groupBoxSecurity.Location = new System.Drawing.Point(243, 116);
            this.groupBoxSecurity.Name = "groupBoxSecurity";
            this.groupBoxSecurity.Size = new System.Drawing.Size(205, 110);
            this.groupBoxSecurity.TabIndex = 1;
            this.groupBoxSecurity.TabStop = false;
            this.groupBoxSecurity.Text = "Security";
            // 
            // labelPasswordDescription
            // 
            this.labelPasswordDescription.Location = new System.Drawing.Point(7, 21);
            this.labelPasswordDescription.Name = "labelPasswordDescription";
            this.labelPasswordDescription.Size = new System.Drawing.Size(186, 55);
            this.labelPasswordDescription.TabIndex = 0;
            this.labelPasswordDescription.Text = "This passphrase will be required whenever screen capturing is stopped, this inter" +
    "face is shown, or the application is exiting:";
            // 
            // buttonSetPassphrase
            // 
            this.buttonSetPassphrase.Enabled = false;
            this.buttonSetPassphrase.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetPassphrase.Image")));
            this.buttonSetPassphrase.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSetPassphrase.Location = new System.Drawing.Point(131, 80);
            this.buttonSetPassphrase.Name = "buttonSetPassphrase";
            this.buttonSetPassphrase.Size = new System.Drawing.Size(68, 23);
            this.buttonSetPassphrase.TabIndex = 0;
            this.buttonSetPassphrase.TabStop = false;
            this.buttonSetPassphrase.Text = "Lock";
            this.buttonSetPassphrase.UseVisualStyleBackColor = true;
            // 
            // textBoxPassphrase
            // 
            this.textBoxPassphrase.Location = new System.Drawing.Point(6, 82);
            this.textBoxPassphrase.MaxLength = 30;
            this.textBoxPassphrase.Name = "textBoxPassphrase";
            this.textBoxPassphrase.Size = new System.Drawing.Size(119, 20);
            this.textBoxPassphrase.TabIndex = 0;
            this.textBoxPassphrase.TabStop = false;
            // 
            // FormSecurity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 343);
            this.Controls.Add(this.groupBoxSecurity);
            this.Name = "FormSecurity";
            this.Text = "FormSecurity";
            this.groupBoxSecurity.ResumeLayout(false);
            this.groupBoxSecurity.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBoxSecurity;
    private System.Windows.Forms.Label labelPasswordDescription;
    private System.Windows.Forms.Button buttonSetPassphrase;
    public System.Windows.Forms.TextBox textBoxPassphrase;
}
}