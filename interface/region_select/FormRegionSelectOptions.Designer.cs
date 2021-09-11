namespace AutoScreenCapture
{
    partial class FormRegionSelectOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegionSelectOptions));
            this.buttonBrowseFolder = new System.Windows.Forms.Button();
            this.labelAutoSaveMacro = new System.Windows.Forms.Label();
            this.labelAutoSaveFolder = new System.Windows.Forms.Label();
            this.textBoxAutoSaveMacro = new System.Windows.Forms.TextBox();
            this.textBoxAutoSaveFolder = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelFormat = new System.Windows.Forms.Label();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonBrowseFolder
            // 
            this.buttonBrowseFolder.Image = global::AutoScreenCapture.Properties.Resources.openfolder;
            this.buttonBrowseFolder.Location = new System.Drawing.Point(455, 7);
            this.buttonBrowseFolder.Name = "buttonBrowseFolder";
            this.buttonBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.buttonBrowseFolder.TabIndex = 2;
            this.buttonBrowseFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseFolder.Click += new System.EventHandler(this.buttonBrowseFolder_Click);
            // 
            // labelAutoSaveMacro
            // 
            this.labelAutoSaveMacro.AutoSize = true;
            this.labelAutoSaveMacro.Location = new System.Drawing.Point(12, 38);
            this.labelAutoSaveMacro.Name = "labelAutoSaveMacro";
            this.labelAutoSaveMacro.Size = new System.Drawing.Size(40, 13);
            this.labelAutoSaveMacro.TabIndex = 0;
            this.labelAutoSaveMacro.Text = "Macro:";
            // 
            // labelAutoSaveFolder
            // 
            this.labelAutoSaveFolder.AutoSize = true;
            this.labelAutoSaveFolder.Location = new System.Drawing.Point(12, 12);
            this.labelAutoSaveFolder.Name = "labelAutoSaveFolder";
            this.labelAutoSaveFolder.Size = new System.Drawing.Size(39, 13);
            this.labelAutoSaveFolder.TabIndex = 0;
            this.labelAutoSaveFolder.Text = "Folder:";
            // 
            // textBoxAutoSaveMacro
            // 
            this.textBoxAutoSaveMacro.Location = new System.Drawing.Point(58, 35);
            this.textBoxAutoSaveMacro.Name = "textBoxAutoSaveMacro";
            this.textBoxAutoSaveMacro.Size = new System.Drawing.Size(424, 20);
            this.textBoxAutoSaveMacro.TabIndex = 3;
            // 
            // textBoxAutoSaveFolder
            // 
            this.textBoxAutoSaveFolder.Location = new System.Drawing.Point(58, 9);
            this.textBoxAutoSaveFolder.Name = "textBoxAutoSaveFolder";
            this.textBoxAutoSaveFolder.Size = new System.Drawing.Size(391, 20);
            this.textBoxAutoSaveFolder.TabIndex = 1;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(326, 105);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(407, 105);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelFormat
            // 
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new System.Drawing.Point(12, 64);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new System.Drawing.Size(42, 13);
            this.labelFormat.TabIndex = 0;
            this.labelFormat.Text = "Format:";
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Location = new System.Drawing.Point(58, 61);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(51, 21);
            this.comboBoxFormat.TabIndex = 4;
            // 
            // FormRegionSelectOptions
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(494, 140);
            this.Controls.Add(this.labelFormat);
            this.Controls.Add(this.comboBoxFormat);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonBrowseFolder);
            this.Controls.Add(this.labelAutoSaveMacro);
            this.Controls.Add(this.labelAutoSaveFolder);
            this.Controls.Add(this.textBoxAutoSaveMacro);
            this.Controls.Add(this.textBoxAutoSaveFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRegionSelectOptions";
            this.ShowInTaskbar = false;
            this.Text = "Region Select Options";
            this.Load += new System.EventHandler(this.FormRegionSelectOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonBrowseFolder;
    private System.Windows.Forms.Label labelAutoSaveMacro;
    private System.Windows.Forms.Label labelAutoSaveFolder;
    private System.Windows.Forms.TextBox textBoxAutoSaveMacro;
    private System.Windows.Forms.TextBox textBoxAutoSaveFolder;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelFormat;
        private System.Windows.Forms.ComboBox comboBoxFormat;
    }
}