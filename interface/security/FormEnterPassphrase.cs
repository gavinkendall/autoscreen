//-----------------------------------------------------------------------
// <copyright file="FormEnterPassphrase.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for asking the user for the passphrase during a locked screen capture session.</summary>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.
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
        private Log _log;
        private Config _config;
        private Security _security;

        /// <summary>
        /// The empty constructor for the form.
        /// </summary>
        public FormEnterPassphrase(Security security, Config config, Log log)
        {
            InitializeComponent();

            _log = log;
            _config = config;
            _security = security;
        }

        private void FormEnterPassphrase_Load(object sender, EventArgs e)
        {
            labelHelp.Text = "       An incorrect passphrase will hide the interface and lock this session";

            textBoxPassphrase.Text = string.Empty;
            textBoxPassphrase.Focus();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPassphrase.Text)) return;

            textBoxPassphrase.Text = textBoxPassphrase.Text.Trim();

            if (_security.Hash(textBoxPassphrase.Text).Equals(_config.Settings.User.GetByKey("Passphrase", _config.Settings.DefaultSettings.Passphrase).Value))
            {
                _log.WriteDebugMessage("User successfully accessed Auto Screen Capture by " + Environment.UserName + " on " + Environment.MachineName);

                DialogResult = DialogResult.OK;
            }
            else
            {
                _log.WriteMessage("WARNING: There was an unauthorized access attempt for Auto Screen Capture! The user was " + Environment.UserName + " on " + Environment.MachineName);
            }

            Close();
        }

        private void TextChanged_textBoxPassphrase(object sender, EventArgs e)
        {
            buttonOK.Enabled = textBoxPassphrase.Text.Length > 0;
        }
    }
}