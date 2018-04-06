namespace autoscreen
{
    partial class FormEditor
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxEditorName = new System.Windows.Forms.TextBox();
            this.labelEditorName = new System.Windows.Forms.Label();
            this.labelEditorApplication = new System.Windows.Forms.Label();
            this.textBoxEditorApplication = new System.Windows.Forms.TextBox();
            this.buttonChooseEditor = new System.Windows.Forms.Button();
            this.labelEditorArguments = new System.Windows.Forms.Label();
            this.textBoxEditorArguments = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(356, 84);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(275, 84);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.TabStop = false;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxEditorName
            // 
            this.textBoxEditorName.Location = new System.Drawing.Point(80, 6);
            this.textBoxEditorName.MaxLength = 50;
            this.textBoxEditorName.Name = "textBoxEditorName";
            this.textBoxEditorName.Size = new System.Drawing.Size(318, 20);
            this.textBoxEditorName.TabIndex = 0;
            this.textBoxEditorName.TabStop = false;
            // 
            // labelEditorName
            // 
            this.labelEditorName.AutoSize = true;
            this.labelEditorName.Location = new System.Drawing.Point(12, 9);
            this.labelEditorName.Name = "labelEditorName";
            this.labelEditorName.Size = new System.Drawing.Size(38, 13);
            this.labelEditorName.TabIndex = 5;
            this.labelEditorName.Text = "Name:";
            // 
            // labelEditorApplication
            // 
            this.labelEditorApplication.AutoSize = true;
            this.labelEditorApplication.Location = new System.Drawing.Point(12, 35);
            this.labelEditorApplication.Name = "labelEditorApplication";
            this.labelEditorApplication.Size = new System.Drawing.Size(62, 13);
            this.labelEditorApplication.TabIndex = 5;
            this.labelEditorApplication.Text = "Application:";
            // 
            // textBoxEditorApplication
            // 
            this.textBoxEditorApplication.Location = new System.Drawing.Point(80, 32);
            this.textBoxEditorApplication.Name = "textBoxEditorApplication";
            this.textBoxEditorApplication.Size = new System.Drawing.Size(318, 20);
            this.textBoxEditorApplication.TabIndex = 1;
            this.textBoxEditorApplication.TabStop = false;
            // 
            // buttonChooseEditor
            // 
            this.buttonChooseEditor.Location = new System.Drawing.Point(404, 32);
            this.buttonChooseEditor.Name = "buttonChooseEditor";
            this.buttonChooseEditor.Size = new System.Drawing.Size(27, 20);
            this.buttonChooseEditor.TabIndex = 2;
            this.buttonChooseEditor.TabStop = false;
            this.buttonChooseEditor.Text = "...";
            this.buttonChooseEditor.UseVisualStyleBackColor = true;
            this.buttonChooseEditor.Click += new System.EventHandler(this.buttonChooseEditor_Click);
            // 
            // labelEditorArguments
            // 
            this.labelEditorArguments.AutoSize = true;
            this.labelEditorArguments.Location = new System.Drawing.Point(12, 61);
            this.labelEditorArguments.Name = "labelEditorArguments";
            this.labelEditorArguments.Size = new System.Drawing.Size(60, 13);
            this.labelEditorArguments.TabIndex = 6;
            this.labelEditorArguments.Text = "Arguments:";
            // 
            // textBoxEditorArguments
            // 
            this.textBoxEditorArguments.Location = new System.Drawing.Point(80, 58);
            this.textBoxEditorArguments.Name = "textBoxEditorArguments";
            this.textBoxEditorArguments.Size = new System.Drawing.Size(351, 20);
            this.textBoxEditorArguments.TabIndex = 7;
            this.textBoxEditorArguments.TabStop = false;
            // 
            // FormEditor
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(443, 114);
            this.Controls.Add(this.textBoxEditorArguments);
            this.Controls.Add(this.labelEditorArguments);
            this.Controls.Add(this.buttonChooseEditor);
            this.Controls.Add(this.textBoxEditorApplication);
            this.Controls.Add(this.labelEditorApplication);
            this.Controls.Add(this.labelEditorName);
            this.Controls.Add(this.textBoxEditorName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxEditorName;
        private System.Windows.Forms.Label labelEditorName;
        private System.Windows.Forms.Label labelEditorApplication;
        private System.Windows.Forms.TextBox textBoxEditorApplication;
        private System.Windows.Forms.Button buttonChooseEditor;
        private System.Windows.Forms.Label labelEditorArguments;
        private System.Windows.Forms.TextBox textBoxEditorArguments;
    }
}