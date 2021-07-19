namespace AutoScreenCapture
{
    partial class FormActiveWindowTitle
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
            this.groupBoxActiveWindowTitle = new System.Windows.Forms.GroupBox();
            this.buttonDynamicRegexValidator = new System.Windows.Forms.Button();
            this.radioButtonRegularExpressionMatch = new System.Windows.Forms.RadioButton();
            this.radioButtonCaseSensitiveMatch = new System.Windows.Forms.RadioButton();
            this.radioButtonCaseInsensitiveMatch = new System.Windows.Forms.RadioButton();
            this.textBoxActiveWindowTitle = new System.Windows.Forms.TextBox();
            this.checkBoxActiveWindowTitle = new System.Windows.Forms.CheckBox();
            this.groupBoxActiveWindowTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxActiveWindowTitle
            // 
            this.groupBoxActiveWindowTitle.Controls.Add(this.buttonDynamicRegexValidator);
            this.groupBoxActiveWindowTitle.Controls.Add(this.radioButtonRegularExpressionMatch);
            this.groupBoxActiveWindowTitle.Controls.Add(this.radioButtonCaseSensitiveMatch);
            this.groupBoxActiveWindowTitle.Controls.Add(this.radioButtonCaseInsensitiveMatch);
            this.groupBoxActiveWindowTitle.Controls.Add(this.textBoxActiveWindowTitle);
            this.groupBoxActiveWindowTitle.Controls.Add(this.checkBoxActiveWindowTitle);
            this.groupBoxActiveWindowTitle.Location = new System.Drawing.Point(149, 79);
            this.groupBoxActiveWindowTitle.Name = "groupBoxActiveWindowTitle";
            this.groupBoxActiveWindowTitle.Size = new System.Drawing.Size(205, 139);
            this.groupBoxActiveWindowTitle.TabIndex = 1;
            this.groupBoxActiveWindowTitle.TabStop = false;
            this.groupBoxActiveWindowTitle.Text = "Active Window Title";
            // 
            // buttonDynamicRegexValidator
            // 
            this.buttonDynamicRegexValidator.Image = global::AutoScreenCapture.Properties.Resources.lightbulb;
            this.buttonDynamicRegexValidator.Location = new System.Drawing.Point(176, 111);
            this.buttonDynamicRegexValidator.Name = "buttonDynamicRegexValidator";
            this.buttonDynamicRegexValidator.Size = new System.Drawing.Size(23, 23);
            this.buttonDynamicRegexValidator.TabIndex = 0;
            this.buttonDynamicRegexValidator.TabStop = false;
            this.buttonDynamicRegexValidator.UseVisualStyleBackColor = true;
            // 
            // radioButtonRegularExpressionMatch
            // 
            this.radioButtonRegularExpressionMatch.AutoSize = true;
            this.radioButtonRegularExpressionMatch.Location = new System.Drawing.Point(6, 114);
            this.radioButtonRegularExpressionMatch.Name = "radioButtonRegularExpressionMatch";
            this.radioButtonRegularExpressionMatch.Size = new System.Drawing.Size(149, 17);
            this.radioButtonRegularExpressionMatch.TabIndex = 0;
            this.radioButtonRegularExpressionMatch.Text = "Regular Expression Match";
            this.radioButtonRegularExpressionMatch.UseVisualStyleBackColor = true;
            // 
            // radioButtonCaseSensitiveMatch
            // 
            this.radioButtonCaseSensitiveMatch.AutoSize = true;
            this.radioButtonCaseSensitiveMatch.Location = new System.Drawing.Point(6, 68);
            this.radioButtonCaseSensitiveMatch.Name = "radioButtonCaseSensitiveMatch";
            this.radioButtonCaseSensitiveMatch.Size = new System.Drawing.Size(128, 17);
            this.radioButtonCaseSensitiveMatch.TabIndex = 0;
            this.radioButtonCaseSensitiveMatch.Text = "Case Sensitive Match";
            this.radioButtonCaseSensitiveMatch.UseVisualStyleBackColor = true;
            // 
            // radioButtonCaseInsensitiveMatch
            // 
            this.radioButtonCaseInsensitiveMatch.AutoSize = true;
            this.radioButtonCaseInsensitiveMatch.Location = new System.Drawing.Point(6, 91);
            this.radioButtonCaseInsensitiveMatch.Name = "radioButtonCaseInsensitiveMatch";
            this.radioButtonCaseInsensitiveMatch.Size = new System.Drawing.Size(135, 17);
            this.radioButtonCaseInsensitiveMatch.TabIndex = 0;
            this.radioButtonCaseInsensitiveMatch.Text = "Case Insensitive Match";
            this.radioButtonCaseInsensitiveMatch.UseVisualStyleBackColor = true;
            // 
            // textBoxActiveWindowTitle
            // 
            this.textBoxActiveWindowTitle.Enabled = false;
            this.textBoxActiveWindowTitle.Location = new System.Drawing.Point(6, 42);
            this.textBoxActiveWindowTitle.MaxLength = 500;
            this.textBoxActiveWindowTitle.Name = "textBoxActiveWindowTitle";
            this.textBoxActiveWindowTitle.Size = new System.Drawing.Size(193, 20);
            this.textBoxActiveWindowTitle.TabIndex = 0;
            this.textBoxActiveWindowTitle.TabStop = false;
            // 
            // checkBoxActiveWindowTitle
            // 
            this.checkBoxActiveWindowTitle.AutoSize = true;
            this.checkBoxActiveWindowTitle.Location = new System.Drawing.Point(6, 19);
            this.checkBoxActiveWindowTitle.Name = "checkBoxActiveWindowTitle";
            this.checkBoxActiveWindowTitle.Size = new System.Drawing.Size(185, 17);
            this.checkBoxActiveWindowTitle.TabIndex = 0;
            this.checkBoxActiveWindowTitle.TabStop = false;
            this.checkBoxActiveWindowTitle.Text = "Capture only if the title contains ...";
            this.checkBoxActiveWindowTitle.UseVisualStyleBackColor = true;
            // 
            // FormActiveWindowTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 351);
            this.Controls.Add(this.groupBoxActiveWindowTitle);
            this.Name = "FormActiveWindowTitle";
            this.Text = "FormActiveWindowTitle";
            this.groupBoxActiveWindowTitle.ResumeLayout(false);
            this.groupBoxActiveWindowTitle.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBoxActiveWindowTitle;
    private System.Windows.Forms.Button buttonDynamicRegexValidator;
    public System.Windows.Forms.RadioButton radioButtonRegularExpressionMatch;
    public System.Windows.Forms.RadioButton radioButtonCaseSensitiveMatch;
    public System.Windows.Forms.RadioButton radioButtonCaseInsensitiveMatch;
    public System.Windows.Forms.TextBox textBoxActiveWindowTitle;
    public System.Windows.Forms.CheckBox checkBoxActiveWindowTitle;
}
}