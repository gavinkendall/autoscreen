//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.3
// autoscreen.FormEnterPassphrase.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Thursday, 26 April 2018

using System;
using System.Windows.Forms;

namespace autoscreen
{
    public partial class FormEnterPassphrase : Form
    {
        public FormEnterPassphrase()
        {
            InitializeComponent();
        }

        private void FormEnterPassphrase_Load(object sender, EventArgs e)
        {
            textBoxPassphrase.Text = string.Empty;
            textBoxPassphrase.Focus();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPassphrase.Text))
            {
                if (textBoxPassphrase.Text.Equals(Properties.Settings.Default.Passphrase))
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

        private void textBoxPassphrase_TextChanged(object sender, EventArgs e)
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