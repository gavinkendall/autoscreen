namespace AutoScreenCapture
{
    partial class FormRegionSelectWithMouse
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
            this.pictureBoxMouseCanvas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMouseCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxMouseCanvas
            // 
            this.pictureBoxMouseCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxMouseCanvas.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMouseCanvas.Name = "pictureBoxMouseCanvas";
            this.pictureBoxMouseCanvas.Size = new System.Drawing.Size(284, 261);
            this.pictureBoxMouseCanvas.TabIndex = 0;
            this.pictureBoxMouseCanvas.TabStop = false;
            this.pictureBoxMouseCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMouseCanvas_MouseDown);
            this.pictureBoxMouseCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMouseCanvas_MouseMove);
            this.pictureBoxMouseCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMouseCanvas_MouseUp);
            // 
            // FormRegionSelectWithMouse
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxMouseCanvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FormRegionSelectWithMouse";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMouseCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMouseCanvas;
    }
}