namespace AutoScreenCapture
{
    partial class FormApplicationFocus
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
            this.groupBoxApplicationFocus = new System.Windows.Forms.GroupBox();
            this.numericUpDownApplicationFocusDelayAfter = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownApplicationFocusDelayBefore = new System.Windows.Forms.NumericUpDown();
            this.labelApplicationFocusDelayAfter = new System.Windows.Forms.Label();
            this.labelApplicationFocusDelayBefore = new System.Windows.Forms.Label();
            this.buttonApplicationFocusTest = new System.Windows.Forms.Button();
            this.buttonApplicationFocusRefresh = new System.Windows.Forms.Button();
            this.comboBoxProcessList = new System.Windows.Forms.ComboBox();
            this.groupBoxApplicationFocus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayBefore)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxApplicationFocus
            // 
            this.groupBoxApplicationFocus.Controls.Add(this.numericUpDownApplicationFocusDelayAfter);
            this.groupBoxApplicationFocus.Controls.Add(this.numericUpDownApplicationFocusDelayBefore);
            this.groupBoxApplicationFocus.Controls.Add(this.labelApplicationFocusDelayAfter);
            this.groupBoxApplicationFocus.Controls.Add(this.labelApplicationFocusDelayBefore);
            this.groupBoxApplicationFocus.Controls.Add(this.buttonApplicationFocusTest);
            this.groupBoxApplicationFocus.Controls.Add(this.buttonApplicationFocusRefresh);
            this.groupBoxApplicationFocus.Controls.Add(this.comboBoxProcessList);
            this.groupBoxApplicationFocus.Location = new System.Drawing.Point(104, 77);
            this.groupBoxApplicationFocus.Name = "groupBoxApplicationFocus";
            this.groupBoxApplicationFocus.Size = new System.Drawing.Size(205, 128);
            this.groupBoxApplicationFocus.TabIndex = 1;
            this.groupBoxApplicationFocus.TabStop = false;
            this.groupBoxApplicationFocus.Text = "Application Focus";
            // 
            // numericUpDownApplicationFocusDelayAfter
            // 
            this.numericUpDownApplicationFocusDelayAfter.Location = new System.Drawing.Point(147, 71);
            this.numericUpDownApplicationFocusDelayAfter.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDownApplicationFocusDelayAfter.Name = "numericUpDownApplicationFocusDelayAfter";
            this.numericUpDownApplicationFocusDelayAfter.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownApplicationFocusDelayAfter.TabIndex = 0;
            this.numericUpDownApplicationFocusDelayAfter.TabStop = false;
            // 
            // numericUpDownApplicationFocusDelayBefore
            // 
            this.numericUpDownApplicationFocusDelayBefore.Location = new System.Drawing.Point(148, 47);
            this.numericUpDownApplicationFocusDelayBefore.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDownApplicationFocusDelayBefore.Name = "numericUpDownApplicationFocusDelayBefore";
            this.numericUpDownApplicationFocusDelayBefore.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownApplicationFocusDelayBefore.TabIndex = 0;
            this.numericUpDownApplicationFocusDelayBefore.TabStop = false;
            // 
            // labelApplicationFocusDelayAfter
            // 
            this.labelApplicationFocusDelayAfter.AutoSize = true;
            this.labelApplicationFocusDelayAfter.Location = new System.Drawing.Point(6, 75);
            this.labelApplicationFocusDelayAfter.Name = "labelApplicationFocusDelayAfter";
            this.labelApplicationFocusDelayAfter.Size = new System.Drawing.Size(127, 13);
            this.labelApplicationFocusDelayAfter.TabIndex = 0;
            this.labelApplicationFocusDelayAfter.Text = "Delay After (milliseconds):";
            // 
            // labelApplicationFocusDelayBefore
            // 
            this.labelApplicationFocusDelayBefore.AutoSize = true;
            this.labelApplicationFocusDelayBefore.Location = new System.Drawing.Point(6, 49);
            this.labelApplicationFocusDelayBefore.Name = "labelApplicationFocusDelayBefore";
            this.labelApplicationFocusDelayBefore.Size = new System.Drawing.Size(136, 13);
            this.labelApplicationFocusDelayBefore.TabIndex = 0;
            this.labelApplicationFocusDelayBefore.Text = "Delay Before (milliseconds):";
            // 
            // buttonApplicationFocusTest
            // 
            this.buttonApplicationFocusTest.Location = new System.Drawing.Point(6, 98);
            this.buttonApplicationFocusTest.Name = "buttonApplicationFocusTest";
            this.buttonApplicationFocusTest.Size = new System.Drawing.Size(92, 23);
            this.buttonApplicationFocusTest.TabIndex = 0;
            this.buttonApplicationFocusTest.TabStop = false;
            this.buttonApplicationFocusTest.Text = "Test";
            this.buttonApplicationFocusTest.UseVisualStyleBackColor = true;
            // 
            // buttonApplicationFocusRefresh
            // 
            this.buttonApplicationFocusRefresh.Location = new System.Drawing.Point(107, 98);
            this.buttonApplicationFocusRefresh.Name = "buttonApplicationFocusRefresh";
            this.buttonApplicationFocusRefresh.Size = new System.Drawing.Size(92, 23);
            this.buttonApplicationFocusRefresh.TabIndex = 0;
            this.buttonApplicationFocusRefresh.TabStop = false;
            this.buttonApplicationFocusRefresh.Text = "Refresh";
            this.buttonApplicationFocusRefresh.UseVisualStyleBackColor = true;
            // 
            // comboBoxProcessList
            // 
            this.comboBoxProcessList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcessList.FormattingEnabled = true;
            this.comboBoxProcessList.Location = new System.Drawing.Point(5, 19);
            this.comboBoxProcessList.Name = "comboBoxProcessList";
            this.comboBoxProcessList.Size = new System.Drawing.Size(193, 21);
            this.comboBoxProcessList.TabIndex = 0;
            this.comboBoxProcessList.TabStop = false;
            // 
            // FormApplicationFocus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 364);
            this.Controls.Add(this.groupBoxApplicationFocus);
            this.Name = "FormApplicationFocus";
            this.Text = "FormApplicationFocus";
            this.groupBoxApplicationFocus.ResumeLayout(false);
            this.groupBoxApplicationFocus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownApplicationFocusDelayBefore)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBoxApplicationFocus;
    public System.Windows.Forms.NumericUpDown numericUpDownApplicationFocusDelayAfter;
    public System.Windows.Forms.NumericUpDown numericUpDownApplicationFocusDelayBefore;
    public System.Windows.Forms.Label labelApplicationFocusDelayAfter;
    public System.Windows.Forms.Label labelApplicationFocusDelayBefore;
    public System.Windows.Forms.Button buttonApplicationFocusTest;
    public System.Windows.Forms.Button buttonApplicationFocusRefresh;
    public System.Windows.Forms.ComboBox comboBoxProcessList;
}
}