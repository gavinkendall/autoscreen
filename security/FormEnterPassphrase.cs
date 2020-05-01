//-----------------------------------------------------------------------
// <copyright file="FormEnterPassphrase.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// A form for challenging the user for the correct passphrase when the screen capture session is locked.
    /// </summary>
    public partial class FormEnterPassphrase : Form
    {
        /// <summary>
        /// The empty constructor for the form.
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPassphrase.Text)) return;

            if (Security.Hash(textBoxPassphrase.Text).Equals(Settings.User.GetByKey("StringPassphrase", defaultValue: string.Empty).Value))
            {
                Log.WriteDebugMessage("Screen capture session was successfully unlocked by " + Environment.UserName + " on " + Environment.MachineName);

                ScreenCapture.LockScreenCaptureSession = false;
                Close();
            }
            else
            {
                Log.WriteMessage("WARNING: There was an attempt to unlock the running screen capture session! The user was " + Environment.UserName + " on " + Environment.MachineName);

                textBoxPassphrase.Clear();
                textBoxPassphrase.Focus();
            }
        }

        private void TextChanged_textBoxPassphrase(object sender, EventArgs e)
        {
            buttonUnlock.Enabled = textBoxPassphrase.Text.Length > 0;
        }
    }
}