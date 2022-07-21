
namespace AutoScreenCapture
{
    partial class FormCaptureNowOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCaptureNowOptions));
            this.labelCaptureNowMacro = new System.Windows.Forms.Label();
            this.textBoxCaptureNowMacro = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelCaptureNowMacro
            // 
            this.labelCaptureNowMacro.AutoSize = true;
            this.labelCaptureNowMacro.Location = new System.Drawing.Point(12, 9);
            this.labelCaptureNowMacro.Name = "labelCaptureNowMacro";
            this.labelCaptureNowMacro.Size = new System.Drawing.Size(383, 13);
            this.labelCaptureNowMacro.TabIndex = 0;
            this.labelCaptureNowMacro.Text = "Macro for whenever you use \"Capture Now / Archive\" or \"Capture Now / Edit\":";
            // 
            // textBoxCaptureNowMacro
            // 
            this.textBoxCaptureNowMacro.Location = new System.Drawing.Point(12, 25);
            this.textBoxCaptureNowMacro.Name = "textBoxCaptureNowMacro";
            this.textBoxCaptureNowMacro.Size = new System.Drawing.Size(470, 20);
            this.textBoxCaptureNowMacro.TabIndex = 1;
            this.textBoxCaptureNowMacro.TabStop = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(407, 105);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(326, 105);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormCaptureNowOptions
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(494, 140);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxCaptureNowMacro);
            this.Controls.Add(this.labelCaptureNowMacro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCaptureNowOptions";
            this.ShowInTaskbar = false;
            this.Text = "Capture Now Options";
            this.Load += new System.EventHandler(this.FormCaptureNowOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelCaptureNowMacro;
    private System.Windows.Forms.TextBox textBoxCaptureNowMacro;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.Button buttonSave;
}
}