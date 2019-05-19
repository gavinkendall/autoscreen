//-----------------------------------------------------------------------
// <copyright file="FormEnterPassphrase.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public partial class FormEnterPassphrase : Form
    {
        public ScreenCapture screenCapture { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FormEnterPassphrase()
        {
            InitializeComponent();
        }

        private void FormEnterPassphrase_Load(object sender, EventArgs e)
        {
            textBoxPassphrase.Text = string.Empty;
            textBoxPassphrase.Focus();
        }

        private void Click_buttonCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void Click_buttonUnlock(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPassphrase.Text))
            {
                if (textBoxPassphrase.Text.Equals(Settings.User.GetByKey("Passphrase", defaultValue: string.Empty).Value))
                {
                    ScreenCapture.LockScreenCaptureSession = false;
                    Close();
                }
                else
                {
                    textBoxPassphrase.Clear();
                    textBoxPassphrase.Focus();
                }
            }
        }

        private void TextChanged_textBoxPassphrase(object sender, EventArgs e)
        {
            if (textBoxPassphrase.Text.Length > 0)
            {
                buttonUnlock.Enabled = true;
            }
            else
            {
                buttonUnlock.Enabled = false;
            }
        }
    }
}