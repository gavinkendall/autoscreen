//-----------------------------------------------------------------------
// <copyright file="FormMain-Security.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Sets the passphrase chosen by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetPassphrase_Click(object sender, EventArgs e)
        {
            if (textBoxPassphrase.Text.Length > 0)
            {
                Settings.User.GetByKey("StringPassphrase", defaultValue: string.Empty).Value = Security.Hash(textBoxPassphrase.Text);
                SaveSettings();

                textBoxPassphrase.Clear();

                ScreenCapture.LockScreenCaptureSession = true;

                MessageBox.Show("The passphrase you entered has been securely stored as a SHA-512 hash and your session has been locked.", "Passphrase Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Determines when we enable the "Set" button for passphrase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextChanged_textBoxPassphrase(object sender, EventArgs e)
        {
            if (textBoxPassphrase.Text.Length > 0)
            {
                buttonSetPassphrase.Enabled = true;
            }
            else
            {
                buttonSetPassphrase.Enabled = false;
            }
        }
    }
}
