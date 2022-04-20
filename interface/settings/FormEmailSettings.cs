//-----------------------------------------------------------------------
// <copyright file="FormEmailSettings.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// Email Settings
    /// </summary>
    public partial class FormEmailSettings : Form
    {
        private Config _config;
        private FileSystem _fileSystem;
        private Log _log;
        private EmailManager _emailManager;

        /// <summary>
        ///  Email Settings
        /// </summary>
        public FormEmailSettings(Config config, FileSystem fileSystem, Log log)
        {
            InitializeComponent();

            _config = config;
            _fileSystem = fileSystem;
            _log = log;

            _emailManager = new EmailManager(log);
        }

        private void FormEmailSettings_Load(object sender, EventArgs e)
        {
            HelpMessage("This is where to configure email settings such as the host, port number, username, password, recipients, and email message");

            textBoxHost.Text = _config.Settings.SMTP.GetByKey("EmailServerHost", _config.Settings.DefaultSettings.EmailServerHost).Value.ToString();
            numericUpDownPort.Value = Convert.ToInt32(_config.Settings.SMTP.GetByKey("EmailServerPort", _config.Settings.DefaultSettings.EmailServerPort).Value);
            checkBoxEnableSSL.Checked = Convert.ToBoolean(_config.Settings.SMTP.GetByKey("EmailServerEnableSSL", _config.Settings.DefaultSettings.EmailServerEnableSSL).Value);
            checkBoxPrompt.Checked = Convert.ToBoolean(_config.Settings.SMTP.GetByKey("EmailPrompt", _config.Settings.DefaultSettings.EmailPrompt).Value);
            textBoxUsername.Text = _config.Settings.SMTP.GetByKey("EmailClientUsername", _config.Settings.DefaultSettings.EmailClientUsername).Value.ToString();
            textBoxPassword.Text = _config.Settings.SMTP.GetByKey("EmailClientPassword", _config.Settings.DefaultSettings.EmailClientPassword).Value.ToString();
            textBoxFrom.Text = _config.Settings.SMTP.GetByKey("EmailMessageFrom", _config.Settings.DefaultSettings.EmailMessageFrom).Value.ToString();
            textBoxTo.Text = _config.Settings.SMTP.GetByKey("EmailMessageTo", _config.Settings.DefaultSettings.EmailMessageTo).Value.ToString();
            textBoxCC.Text = _config.Settings.SMTP.GetByKey("EmailMessageCC", _config.Settings.DefaultSettings.EmailMessageCC).Value.ToString();
            textBoxBCC.Text = _config.Settings.SMTP.GetByKey("EmailMessageBCC", _config.Settings.DefaultSettings.EmailMessageBCC).Value.ToString();
            textBoxSubject.Text = _config.Settings.SMTP.GetByKey("EmailMessageSubject", _config.Settings.DefaultSettings.EmailMessageSubject).Value.ToString();
            textBoxBody.Text = _config.Settings.SMTP.GetByKey("EmailMessageBody", _config.Settings.DefaultSettings.EmailMessageBody).Value.ToString();
        }

        private void HelpMessage(string message)
        {
            labelHelp.Text = "       " + message;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _config.Settings.SMTP.GetByKey("EmailServerHost", _config.Settings.DefaultSettings.EmailServerHost).Value = textBoxHost.Text;
            _config.Settings.SMTP.GetByKey("EmailServerPort", _config.Settings.DefaultSettings.EmailServerPort).Value = numericUpDownPort.Value;
            _config.Settings.SMTP.GetByKey("EmailServerEnableSSL", _config.Settings.DefaultSettings.EmailServerEnableSSL).Value = checkBoxEnableSSL.Checked;
            _config.Settings.SMTP.GetByKey("EmailPrompt", _config.Settings.DefaultSettings.EmailPrompt).Value = checkBoxPrompt.Checked;
            _config.Settings.SMTP.GetByKey("EmailClientUsername", _config.Settings.DefaultSettings.EmailClientUsername).Value = textBoxUsername.Text;
            _config.Settings.SMTP.GetByKey("EmailClientPassword", _config.Settings.DefaultSettings.EmailClientPassword).Value = textBoxPassword.Text;
            _config.Settings.SMTP.GetByKey("EmailMessageFrom", _config.Settings.DefaultSettings.EmailMessageFrom).Value = textBoxFrom.Text;
            _config.Settings.SMTP.GetByKey("EmailMessageTo", _config.Settings.DefaultSettings.EmailMessageTo).Value = textBoxTo.Text;
            _config.Settings.SMTP.GetByKey("EmailMessageCC", _config.Settings.DefaultSettings.EmailMessageCC).Value = textBoxCC.Text;
            _config.Settings.SMTP.GetByKey("EmailMessageBCC", _config.Settings.DefaultSettings.EmailMessageBCC).Value = textBoxBCC.Text;
            _config.Settings.SMTP.GetByKey("EmailMessageSubject", _config.Settings.DefaultSettings.EmailMessageSubject).Value = textBoxSubject.Text;
            _config.Settings.SMTP.GetByKey("EmailMessageBody", _config.Settings.DefaultSettings.EmailMessageBody).Value = textBoxBody.Text;

            _config.Settings.SMTP.Save(_config.Settings, _fileSystem);

            DialogResult = DialogResult.OK;

            Close();
        }

        private void comboBoxHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxHost.SelectedIndex > 0)
            {
                switch (comboBoxHost.SelectedIndex)
                {
                    case 1:
                        textBoxHost.Text = "smtp.gmail.com";
                        numericUpDownPort.Value = 587;
                        checkBoxEnableSSL.Checked = true;
                        break;
                    case 2:
                        textBoxHost.Text = "smtp-mail.outlook.com";
                        numericUpDownPort.Value = 587;
                        checkBoxEnableSSL.Checked = true;
                        break;

                    case 3:
                        textBoxHost.Text = "smtp.office365.com";
                        numericUpDownPort.Value = 587;
                        checkBoxEnableSSL.Checked = true;
                        break;
                }

                comboBoxHost.SelectedIndex = 0;
            }
        }

        private void textBoxHost_TextChanged(object sender, EventArgs e)
        {
            if (textBoxHost.Text.Trim().Equals("smtp.gmail.com"))
            {
                HelpMessage("Gmail requires less secure app access to be enabled for sending email with this application so please check Google account security settings");
            }

            if (textBoxHost.Text.Trim().Equals("smtp-mail.outlook.com"))
            {
                HelpMessage("Outlook accounts that are associated with a corporate email address may require Office 365 SMTP rather than Outlook SMTP");
            }

            if (textBoxHost.Text.Trim().Equals("smtp.office365.com"))
            {
                HelpMessage("Office 365 accounts may require authentication with an internal corporate email server. Multi-factor authentication does not work with this application");
            }
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

                _emailManager.ComposeEmailMessage(textBoxFrom.Text,
                    textBoxTo.Text,
                    textBoxCC.Text,
                    textBoxBCC.Text,
                    textBoxSubject.Text,
                    textBoxBody.Text,
                    isBodyHtml: false,
                    checkBoxEnableSSL.Checked,
                    textBoxHost.Text,
                    (int)numericUpDownPort.Value,
                    textBoxUsername.Text,
                    textBoxPassword.Text);

                if (checkBoxPrompt.Checked)
                {
                    DialogResult dialogResult = MessageBox.Show(_emailManager.AskQuestionAboutSendingEmailMessage(), "Email Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        buttonSendTestEmail.Text = "Sending ...";
                        buttonSendTestEmail.Enabled = false;

                        if (_emailManager.SendEmailMessage())
                        {
                            MessageBox.Show("The email message has been sent.", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("The email message was not sent because an error was encountered.", "Email Message Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        _emailManager.DisposeSmtpClient();

                        return;
                    }
                }
                else
                {
                    buttonSendTestEmail.Text = "Sending ...";
                    buttonSendTestEmail.Enabled = false;

                    if (_emailManager.SendEmailMessage())
                    {
                        MessageBox.Show("The email message has been sent.", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The email message was not sent because an error was encountered.", "Email Message Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                _emailManager.DisposeSmtpClient();

                buttonSendTestEmail.Text = "Send Test Email";
                buttonSendTestEmail.Enabled = true;
            }
            catch (Exception ex)
            {
                buttonSendTestEmail.Text = "Send Test Email";
                buttonSendTestEmail.Enabled = true;

                _log.WriteErrorMessage("Email Settings form has encountered an error");
                _log.WriteErrorMessage(ex.Message);

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    _log.WriteErrorMessage(ex.InnerException.Message);
                }

                _log.WriteErrorMessage(ex.StackTrace);

                MessageBox.Show("The email message could not be sent.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
