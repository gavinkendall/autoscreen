namespace AutoScreenCapture
{
    partial class FormInterval
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
            this.groupBoxCaptureDelay = new System.Windows.Forms.GroupBox();
            this.labelLimit = new System.Windows.Forms.Label();
            this.checkBoxInitialScreenshot = new System.Windows.Forms.CheckBox();
            this.numericUpDownCaptureLimit = new System.Windows.Forms.NumericUpDown();
            this.checkBoxCaptureLimit = new System.Windows.Forms.CheckBox();
            this.labelMillisecondsInterval = new System.Windows.Forms.Label();
            this.numericUpDownMillisecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.labelSecondsInterval = new System.Windows.Forms.Label();
            this.labelMinutesInterval = new System.Windows.Forms.Label();
            this.labelHoursInterval = new System.Windows.Forms.Label();
            this.numericUpDownSecondsInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinutesInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHoursInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBoxCaptureDelay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxCaptureDelay
            // 
            this.groupBoxCaptureDelay.Controls.Add(this.labelLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.checkBoxInitialScreenshot);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownCaptureLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.checkBoxCaptureLimit);
            this.groupBoxCaptureDelay.Controls.Add(this.labelMillisecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownMillisecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelSecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelMinutesInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.labelHoursInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownSecondsInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownMinutesInterval);
            this.groupBoxCaptureDelay.Controls.Add(this.numericUpDownHoursInterval);
            this.groupBoxCaptureDelay.Location = new System.Drawing.Point(80, 89);
            this.groupBoxCaptureDelay.Name = "groupBoxCaptureDelay";
            this.groupBoxCaptureDelay.Size = new System.Drawing.Size(205, 122);
            this.groupBoxCaptureDelay.TabIndex = 1;
            this.groupBoxCaptureDelay.TabStop = false;
            this.groupBoxCaptureDelay.Text = "Interval";
            // 
            // labelLimit
            // 
            this.labelLimit.AutoSize = true;
            this.labelLimit.Location = new System.Drawing.Point(171, 47);
            this.labelLimit.Name = "labelLimit";
            this.labelLimit.Size = new System.Drawing.Size(24, 13);
            this.labelLimit.TabIndex = 0;
            this.labelLimit.Text = "limit";
            // 
            // checkBoxInitialScreenshot
            // 
            this.checkBoxInitialScreenshot.AutoSize = true;
            this.checkBoxInitialScreenshot.Location = new System.Drawing.Point(110, 20);
            this.checkBoxInitialScreenshot.Name = "checkBoxInitialScreenshot";
            this.checkBoxInitialScreenshot.Size = new System.Drawing.Size(90, 17);
            this.checkBoxInitialScreenshot.TabIndex = 0;
            this.checkBoxInitialScreenshot.TabStop = false;
            this.checkBoxInitialScreenshot.Text = "Initial Capture";
            this.checkBoxInitialScreenshot.UseVisualStyleBackColor = true;
            // 
            // numericUpDownCaptureLimit
            // 
            this.numericUpDownCaptureLimit.Location = new System.Drawing.Point(127, 45);
            this.numericUpDownCaptureLimit.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownCaptureLimit.Name = "numericUpDownCaptureLimit";
            this.numericUpDownCaptureLimit.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownCaptureLimit.TabIndex = 0;
            this.numericUpDownCaptureLimit.TabStop = false;
            // 
            // checkBoxCaptureLimit
            // 
            this.checkBoxCaptureLimit.AutoSize = true;
            this.checkBoxCaptureLimit.Location = new System.Drawing.Point(110, 47);
            this.checkBoxCaptureLimit.Name = "checkBoxCaptureLimit";
            this.checkBoxCaptureLimit.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCaptureLimit.TabIndex = 0;
            this.checkBoxCaptureLimit.TabStop = false;
            this.checkBoxCaptureLimit.UseVisualStyleBackColor = true;
            // 
            // labelMillisecondsInterval
            // 
            this.labelMillisecondsInterval.AutoSize = true;
            this.labelMillisecondsInterval.Location = new System.Drawing.Point(54, 99);
            this.labelMillisecondsInterval.Name = "labelMillisecondsInterval";
            this.labelMillisecondsInterval.Size = new System.Drawing.Size(63, 13);
            this.labelMillisecondsInterval.TabIndex = 0;
            this.labelMillisecondsInterval.Text = "milliseconds";
            // 
            // numericUpDownMillisecondsInterval
            // 
            this.numericUpDownMillisecondsInterval.Location = new System.Drawing.Point(6, 97);
            this.numericUpDownMillisecondsInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownMillisecondsInterval.Name = "numericUpDownMillisecondsInterval";
            this.numericUpDownMillisecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMillisecondsInterval.TabIndex = 0;
            this.numericUpDownMillisecondsInterval.TabStop = false;
            // 
            // labelSecondsInterval
            // 
            this.labelSecondsInterval.AutoSize = true;
            this.labelSecondsInterval.Location = new System.Drawing.Point(54, 73);
            this.labelSecondsInterval.Name = "labelSecondsInterval";
            this.labelSecondsInterval.Size = new System.Drawing.Size(47, 13);
            this.labelSecondsInterval.TabIndex = 0;
            this.labelSecondsInterval.Text = "seconds";
            // 
            // labelMinutesInterval
            // 
            this.labelMinutesInterval.AutoSize = true;
            this.labelMinutesInterval.Location = new System.Drawing.Point(54, 47);
            this.labelMinutesInterval.Name = "labelMinutesInterval";
            this.labelMinutesInterval.Size = new System.Drawing.Size(43, 13);
            this.labelMinutesInterval.TabIndex = 0;
            this.labelMinutesInterval.Text = "minutes";
            // 
            // labelHoursInterval
            // 
            this.labelHoursInterval.AutoSize = true;
            this.labelHoursInterval.Location = new System.Drawing.Point(54, 21);
            this.labelHoursInterval.Name = "labelHoursInterval";
            this.labelHoursInterval.Size = new System.Drawing.Size(33, 13);
            this.labelHoursInterval.TabIndex = 0;
            this.labelHoursInterval.Text = "hours";
            // 
            // numericUpDownSecondsInterval
            // 
            this.numericUpDownSecondsInterval.Location = new System.Drawing.Point(6, 71);
            this.numericUpDownSecondsInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSecondsInterval.Name = "numericUpDownSecondsInterval";
            this.numericUpDownSecondsInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSecondsInterval.TabIndex = 0;
            this.numericUpDownSecondsInterval.TabStop = false;
            // 
            // numericUpDownMinutesInterval
            // 
            this.numericUpDownMinutesInterval.Location = new System.Drawing.Point(6, 45);
            this.numericUpDownMinutesInterval.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownMinutesInterval.Name = "numericUpDownMinutesInterval";
            this.numericUpDownMinutesInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownMinutesInterval.TabIndex = 0;
            this.numericUpDownMinutesInterval.TabStop = false;
            // 
            // numericUpDownHoursInterval
            // 
            this.numericUpDownHoursInterval.Location = new System.Drawing.Point(6, 19);
            this.numericUpDownHoursInterval.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownHoursInterval.Name = "numericUpDownHoursInterval";
            this.numericUpDownHoursInterval.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownHoursInterval.TabIndex = 0;
            this.numericUpDownHoursInterval.TabStop = false;
            // 
            // FormInterval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 300);
            this.Controls.Add(this.groupBoxCaptureDelay);
            this.Name = "FormInterval";
            this.Text = "FormInterval";
            this.groupBoxCaptureDelay.ResumeLayout(false);
            this.groupBoxCaptureDelay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCaptureLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMillisecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecondsInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHoursInterval)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBoxCaptureDelay;
    private System.Windows.Forms.Label labelLimit;
    public System.Windows.Forms.CheckBox checkBoxInitialScreenshot;
    public System.Windows.Forms.NumericUpDown numericUpDownCaptureLimit;
    public System.Windows.Forms.CheckBox checkBoxCaptureLimit;
    private System.Windows.Forms.Label labelMillisecondsInterval;
    public System.Windows.Forms.NumericUpDown numericUpDownMillisecondsInterval;
    private System.Windows.Forms.Label labelSecondsInterval;
    private System.Windows.Forms.Label labelMinutesInterval;
    private System.Windows.Forms.Label labelHoursInterval;
    public System.Windows.Forms.NumericUpDown numericUpDownSecondsInterval;
    public System.Windows.Forms.NumericUpDown numericUpDownMinutesInterval;
    public System.Windows.Forms.NumericUpDown numericUpDownHoursInterval;
}
}