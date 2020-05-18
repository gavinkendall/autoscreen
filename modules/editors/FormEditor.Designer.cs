namespace AutoScreenCapture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditor));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxEditorName = new System.Windows.Forms.TextBox();
            this.labelEditorName = new System.Windows.Forms.Label();
            this.labelEditorApplication = new System.Windows.Forms.Label();
            this.textBoxEditorApplication = new System.Windows.Forms.TextBox();
            this.buttonChooseEditor = new System.Windows.Forms.Button();
            this.labelEditorArguments = new System.Windows.Forms.Label();
            this.textBoxEditorArguments = new System.Windows.Forms.TextBox();
            this.checkBoxMakeDefaultEditor = new System.Windows.Forms.CheckBox();
            this.labelHelp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(117, 420);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Location = new System.Drawing.Point(12, 420);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(99, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxEditorName
            // 
            this.textBoxEditorName.Location = new System.Drawing.Point(56, 32);
            this.textBoxEditorName.MaxLength = 50;
            this.textBoxEditorName.Name = "textBoxEditorName";
            this.textBoxEditorName.Size = new System.Drawing.Size(546, 20);
            this.textBoxEditorName.TabIndex = 1;
            // 
            // labelEditorName
            // 
            this.labelEditorName.AutoSize = true;
            this.labelEditorName.Location = new System.Drawing.Point(9, 35);
            this.labelEditorName.Name = "labelEditorName";
            this.labelEditorName.Size = new System.Drawing.Size(38, 13);
            this.labelEditorName.TabIndex = 0;
            this.labelEditorName.Text = "Name:";
            // 
            // labelEditorApplication
            // 
            this.labelEditorApplication.AutoSize = true;
            this.labelEditorApplication.Location = new System.Drawing.Point(113, 162);
            this.labelEditorApplication.Name = "labelEditorApplication";
            this.labelEditorApplication.Size = new System.Drawing.Size(62, 13);
            this.labelEditorApplication.TabIndex = 0;
            this.labelEditorApplication.Text = "Application:";
            // 
            // textBoxEditorApplication
            // 
            this.textBoxEditorApplication.Location = new System.Drawing.Point(181, 159);
            this.textBoxEditorApplication.Name = "textBoxEditorApplication";
            this.textBoxEditorApplication.Size = new System.Drawing.Size(318, 20);
            this.textBoxEditorApplication.TabIndex = 2;
            // 
            // buttonChooseEditor
            // 
            this.buttonChooseEditor.Location = new System.Drawing.Point(505, 159);
            this.buttonChooseEditor.Name = "buttonChooseEditor";
            this.buttonChooseEditor.Size = new System.Drawing.Size(27, 20);
            this.buttonChooseEditor.TabIndex = 3;
            this.buttonChooseEditor.Text = "...";
            this.buttonChooseEditor.UseVisualStyleBackColor = true;
            this.buttonChooseEditor.Click += new System.EventHandler(this.buttonChooseEditor_Click);
            // 
            // labelEditorArguments
            // 
            this.labelEditorArguments.AutoSize = true;
            this.labelEditorArguments.Location = new System.Drawing.Point(113, 188);
            this.labelEditorArguments.Name = "labelEditorArguments";
            this.labelEditorArguments.Size = new System.Drawing.Size(60, 13);
            this.labelEditorArguments.TabIndex = 0;
            this.labelEditorArguments.Text = "Arguments:";
            // 
            // textBoxEditorArguments
            // 
            this.textBoxEditorArguments.Location = new System.Drawing.Point(181, 185);
            this.textBoxEditorArguments.Name = "textBoxEditorArguments";
            this.textBoxEditorArguments.Size = new System.Drawing.Size(351, 20);
            this.textBoxEditorArguments.TabIndex = 4;
            // 
            // checkBoxMakeDefaultEditor
            // 
            this.checkBoxMakeDefaultEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMakeDefaultEditor.AutoSize = true;
            this.checkBoxMakeDefaultEditor.Location = new System.Drawing.Point(691, 34);
            this.checkBoxMakeDefaultEditor.Name = "checkBoxMakeDefaultEditor";
            this.checkBoxMakeDefaultEditor.Size = new System.Drawing.Size(60, 17);
            this.checkBoxMakeDefaultEditor.TabIndex = 7;
            this.checkBoxMakeDefaultEditor.Text = "Default";
            this.checkBoxMakeDefaultEditor.UseVisualStyleBackColor = true;
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
            this.labelHelp.Size = new System.Drawing.Size(752, 17);
            this.labelHelp.TabIndex = 8;
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormEditor
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(756, 454);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.checkBoxMakeDefaultEditor);
            this.Controls.Add(this.textBoxEditorArguments);
            this.Controls.Add(this.labelEditorArguments);
            this.Controls.Add(this.buttonChooseEditor);
            this.Controls.Add(this.textBoxEditorApplication);
            this.Controls.Add(this.labelEditorApplication);
            this.Controls.Add(this.labelEditorName);
            this.Controls.Add(this.textBoxEditorName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(772, 493);
            this.Name = "FormEditor";
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
        private System.Windows.Forms.CheckBox checkBoxMakeDefaultEditor;
        private System.Windows.Forms.Label labelHelp;
    }
}