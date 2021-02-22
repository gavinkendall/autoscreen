//-----------------------------------------------------------------------
// <copyright file="FormEmailSettings.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form to configure SMTP settings.</summary>
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
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// Email Settings
    /// </summary>
    public partial class FormEmailSettings : Form
    {
        Config _config;
        FileSystem _fileSystem;
        Log _log;

        /// <summary>
        ///  Email Settings
        /// </summary>
        public FormEmailSettings(Config config, FileSystem fileSystem, Log log)
        {
            InitializeComponent();

            _config = config;
            _fileSystem = fileSystem;
            _log = log;
        }

        private void FormEmailSettings_Load(object sender, EventArgs e)
        {
            textBoxHost.Text = Settings.SMTP.GetByKey("EmailServerHost", _config.Settings.DefaultSettings.EmailServerHost).Value.ToString();
            numericUpDownPort.Value = Convert.ToInt32(Settings.SMTP.GetByKey("EmailServerPort", _config.Settings.DefaultSettings.EmailServerPort).Value);
            checkBoxEnableSSL.Checked = Convert.ToBoolean(Settings.SMTP.GetByKey("EmailServerEnableSSL", _config.Settings.DefaultSettings.EmailServerEnableSSL).Value);
            checkBoxPrompt.Checked = Convert.ToBoolean(Settings.SMTP.GetByKey("EmailPrompt", _config.Settings.DefaultSettings.EmailPrompt).Value);
            textBoxUsername.Text = Settings.SMTP.GetByKey("EmailClientUsername", _config.Settings.DefaultSettings.EmailClientUsername).Value.ToString();
            textBoxPassword.Text = Settings.SMTP.GetByKey("EmailClientPassword", _config.Settings.DefaultSettings.EmailClientPassword).Value.ToString();
            textBoxFrom.Text = Settings.SMTP.GetByKey("EmailMessageFrom", _config.Settings.DefaultSettings.EmailMessageFrom).Value.ToString();
            textBoxTo.Text = Settings.SMTP.GetByKey("EmailMessageTo", _config.Settings.DefaultSettings.EmailMessageTo).Value.ToString();
            textBoxCC.Text = Settings.SMTP.GetByKey("EmailMessageCC", _config.Settings.DefaultSettings.EmailMessageCC).Value.ToString();
            textBoxBCC.Text = Settings.SMTP.GetByKey("EmailMessageBCC", _config.Settings.DefaultSettings.EmailMessageBCC).Value.ToString();
            textBoxSubject.Text = Settings.SMTP.GetByKey("EmailMessageSubject", _config.Settings.DefaultSettings.EmailMessageSubject).Value.ToString();
            textBoxBody.Text = Settings.SMTP.GetByKey("EmailMessageBody", _config.Settings.DefaultSettings.EmailMessageBody).Value.ToString();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Settings.SMTP.GetByKey("EmailServerHost", _config.Settings.DefaultSettings.EmailServerHost).Value = textBoxHost.Text;
            Settings.SMTP.GetByKey("EmailServerPort", _config.Settings.DefaultSettings.EmailServerPort).Value = numericUpDownPort.Value;
            Settings.SMTP.GetByKey("EmailServerEnableSSL", _config.Settings.DefaultSettings.EmailServerEnableSSL).Value = checkBoxEnableSSL.Checked;
            Settings.SMTP.GetByKey("EmailPrompt", _config.Settings.DefaultSettings.EmailPrompt).Value = checkBoxPrompt.Checked;
            Settings.SMTP.GetByKey("EmailClientUsername", _config.Settings.DefaultSettings.EmailClientUsername).Value = textBoxUsername.Text;
            Settings.SMTP.GetByKey("EmailClientPassword", _config.Settings.DefaultSettings.EmailClientPassword).Value = textBoxPassword.Text;
            Settings.SMTP.GetByKey("EmailMessageFrom", _config.Settings.DefaultSettings.EmailMessageFrom).Value = textBoxFrom.Text;
            Settings.SMTP.GetByKey("EmailMessageTo", _config.Settings.DefaultSettings.EmailMessageTo).Value = textBoxTo.Text;
            Settings.SMTP.GetByKey("EmailMessageCC", _config.Settings.DefaultSettings.EmailMessageCC).Value = textBoxCC.Text;
            Settings.SMTP.GetByKey("EmailMessageBCC", _config.Settings.DefaultSettings.EmailMessageBCC).Value = textBoxBCC.Text;
            Settings.SMTP.GetByKey("EmailMessageSubject", _config.Settings.DefaultSettings.EmailMessageSubject).Value = textBoxSubject.Text;
            Settings.SMTP.GetByKey("EmailMessageBody", _config.Settings.DefaultSettings.EmailMessageBody).Value = textBoxBody.Text;

            Settings.SMTP.Save(_config.Settings, _fileSystem, _log);

            DialogResult = DialogResult.OK;

            Close();
        }

        private void buttonSendTestEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxHost.Text) ||
                    (numericUpDownPort.Value < numericUpDownPort.Minimum || numericUpDownPort.Value > numericUpDownPort.Maximum) ||
                    string.IsNullOrEmpty(textBoxUsername.Text) ||
                    string.IsNullOrEmpty(textBoxPassword.Text) ||
                    string.IsNullOrEmpty(textBoxFrom.Text) ||
                    string.IsNullOrEmpty(textBoxTo.Text))
                {
                    MessageBox.Show("Please make sure that at least the Host, Port, Username, Password, From, and To fields are correct.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(textBoxFrom.Text)
                };

                mailMessage.To.Add(textBoxTo.Text);

                if (!string.IsNullOrEmpty(textBoxCC.Text))
                {
                    mailMessage.CC.Add(textBoxCC.Text);
                }

                if (!string.IsNullOrEmpty(textBoxBCC.Text))
                {
                    mailMessage.Bcc.Add(textBoxBCC.Text);
                }

                if (!string.IsNullOrEmpty(textBoxSubject.Text))
                {
                    mailMessage.Subject = textBoxSubject.Text;
                }

                if (!string.IsNullOrEmpty(textBoxBody.Text))
                {
                    mailMessage.Body = textBoxBody.Text;
                }

                mailMessage.IsBodyHtml = false;

                var smtpClient = new SmtpClient(textBoxHost.Text, (int)numericUpDownPort.Value)
                {
                    EnableSsl = checkBoxEnableSSL.Checked,
                    Credentials = new NetworkCredential(textBoxUsername.Text, textBoxPassword.Text)
                };

                if (checkBoxPrompt.Checked)
                {
                    DialogResult dialogResult = MessageBox.Show($"Do you want to send this email message from \"{textBoxFrom.Text}\" to \"{textBoxTo.Text}\" using \"{textBoxHost.Text}:{numericUpDownPort.Value}\"?", "Email Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        buttonSendTestEmail.Text = "Sending ...";
                        buttonSendTestEmail.Enabled = false;

                        smtpClient.Send(mailMessage);

                        MessageBox.Show($"The email message has been sent from \"{textBoxFrom.Text}\" to \"{textBoxTo.Text}\".", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        smtpClient.Dispose();

                        return;
                    }
                }
                else
                {
                    buttonSendTestEmail.Text = "Sending ...";
                    buttonSendTestEmail.Enabled = false;

                    smtpClient.Send(mailMessage);

                    MessageBox.Show($"The email message has been sent from \"{textBoxFrom.Text}\" to \"{textBoxTo.Text}\".", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                smtpClient.Dispose();

                buttonSendTestEmail.Text = "Send Test Email";
                buttonSendTestEmail.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("The email message could not be sent.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
