﻿namespace AutoScreenCapture
{
    partial class FormLabelSwitcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLabelSwitcher));
            this.comboBoxLabels = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxLabels
            // 
            this.comboBoxLabels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLabels.FormattingEnabled = true;
            this.comboBoxLabels.Location = new System.Drawing.Point(3, 4);
            this.comboBoxLabels.Name = "comboBoxLabels";
            this.comboBoxLabels.Size = new System.Drawing.Size(602, 21);
            this.comboBoxLabels.TabIndex = 0;
            this.comboBoxLabels.SelectedIndexChanged += new System.EventHandler(this.comboBoxLabels_SelectedIndexChanged);
            // 
            // FormLabelSwitcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 28);
            this.Controls.Add(this.comboBoxLabels);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormLabelSwitcher";
            this.Text = "Label Switcher";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLabelSwitcher_FormClosing);
            this.ResumeLayout(false);

    }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public System.Windows.Forms.ComboBox comboBoxLabels;
    }
}