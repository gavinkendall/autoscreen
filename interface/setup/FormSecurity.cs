using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormSecurity : Form
{
        private Config _config;
        private ScreenCapture _screenCapture;
        private Security _security;

    public FormSecurity(Config config, ScreenCapture screenCapture, Security security)
    {
        InitializeComponent();

            _config = config;
            _screenCapture = screenCapture;
            _security = security;
    }

        /// <summary>
        /// Sets the passphrase chosen by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetPassphrase_Click(object sender, EventArgs e)
        {
            if (textBoxPassphrase.Text.Length > 0)
            {
                textBoxPassphrase.Text = textBoxPassphrase.Text.Trim();

                _config.Settings.User.SetValueByKey("Passphrase", _security.Hash(textBoxPassphrase.Text));

                //SaveSettings();

                textBoxPassphrase.Clear();

                _screenCapture.LockScreenCaptureSession = true;

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
