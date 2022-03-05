//-----------------------------------------------------------------------
// <copyright file="EmailManager.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class for handling everything to do with email.</summary>
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
using System.Text.RegularExpressions;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class for handling everything to do with email.
    /// </summary>
    public class EmailManager
    {
        // A simple regex for validating an email address.
        // This does not comply to any strict ISO standards.
        private const string REGEX_EMAIL_ADDRESS = @"^.+@.+\..{2,3}$";

        private Log _log;
        private MailMessage _mailMessage;
        private SmtpClient _smtpClient;

        /// <summary>
        /// Gets a collection of email addresses based on the given text (that should contain at least one email address or multiple email addresses).
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private MailAddressCollection GetMailAddressesFromText(string text)
        {
            MailAddressCollection mailAddresses = new MailAddressCollection();

            if (!string.IsNullOrEmpty(text))
            {
                text = text.Trim();

                string[] emailAddressStrArray = null;

                if (text.Contains(";") && text.Contains(","))
                {
                    _log.WriteErrorMessage("Provided list of email addresses contain multiple separators. Please use either ; or , to separate each email address not both");

                    return mailAddresses;
                }

                if (text.Contains(";"))
                {
                    emailAddressStrArray = text.Split(';');
                }
                
                if (text.Contains(","))
                {
                    emailAddressStrArray = text.Split(',');
                }

                // Multiple email addresses were provided.
                if (emailAddressStrArray != null)
                {
                    foreach (string emailAddressStr in emailAddressStrArray)
                    {
                        string emailAddressStrTrimmed = emailAddressStr.Trim();

                        if (Regex.IsMatch(emailAddressStrTrimmed, REGEX_EMAIL_ADDRESS))
                        {
                            mailAddresses.Add(new MailAddress(emailAddressStrTrimmed));
                        }
                        else
                        {
                            _log.WriteDebugMessage($"\"{emailAddressStrTrimmed}\" is not an email address so it was not included");
                        }
                    }
                }
                else
                {
                    // A single email address was provided.
                    if (Regex.IsMatch(text, REGEX_EMAIL_ADDRESS))
                    {
                        mailAddresses.Add(new MailAddress(text));
                    }
                    else
                    {
                        _log.WriteDebugMessage($"\"{text}\" is not an email address so it was not included");
                    }
                }
            }

            return mailAddresses;
        }

        /// <summary>
        /// Empty constructor of the Email Manager class.
        /// </summary>
        public EmailManager(Log log)
        {
            _log = log;
        }

        /// <summary>
        /// Gets the number of attachments in the email message.
        /// </summary>
        public int AttachmentsCount
        {
            get
            {
                if (_mailMessage != null && _mailMessage.Attachments != null)
                {
                    return _mailMessage.Attachments.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Composes an email message assuming that null/empty checks have already been considered for the required input.
        /// </summary>
        /// <param name="from">The sender email address of who's sending the email message.</param>
        /// <param name="to">An email address of a recipient or a list of recipient email addresses (separated by either ; or ,) to receive the email message.</param>
        /// <param name="cc">An email address of a recipient or a list of recipient email addresses (separated by either ; or ,) to receive the email message as a carbon copy (CC).</param>
        /// <param name="bcc">An email address of a recipient or a list of recipient email addresses (separated by either ; or ,) to receive the email message as a Blind Carbon Copy (BCC).</param>
        /// <param name="subject">The subject of the email message.</param>
        /// <param name="body">The body of the email message.</param>
        /// <param name="isBodyHtml">Determines if the email message body contains HTML.</param>
        /// <param name="ssl">Determines if the email message should be sent using SSL.</param>
        /// <param name="host">The host address of the email server.</param>
        /// <param name="port">The port number of the email server.</param>
        /// <param name="username">The username to use for authenticating with the email server.</param>
        /// <param name="password">The password to use for authenticating with the email server.</param>
        public void ComposeEmailMessage(string from, string to, string cc, string bcc, string subject, string body, bool isBodyHtml, bool ssl, string host, int port, string username, string password)
        {
            _log.WriteDebugMessage("Composing email message");

            _mailMessage = new MailMessage
            {
                From = new MailAddress(from)
            };

            foreach (MailAddress mailAddressTo in GetMailAddressesFromText(to))
            {
                _mailMessage.To.Add(mailAddressTo);

                _log.WriteDebugMessage($"Included email address \"{mailAddressTo}\" in TO field");
            }

            if (!string.IsNullOrEmpty(cc))
            {
                foreach (MailAddress mailAddressCC in GetMailAddressesFromText(cc))
                {
                    _mailMessage.CC.Add(mailAddressCC);

                    _log.WriteDebugMessage($"Included email address \"{mailAddressCC}\" in CC field");
                }
            }

            if (!string.IsNullOrEmpty(bcc))
            {
                foreach (MailAddress mailAddressBCC in GetMailAddressesFromText(bcc))
                {
                    _mailMessage.Bcc.Add(mailAddressBCC);

                    _log.WriteDebugMessage($"Included email address \"{mailAddressBCC}\" in BCC field");
                }
            }

            if (!string.IsNullOrEmpty(subject))
            {
                _mailMessage.Subject = subject;

                _log.WriteDebugMessage($"Email message has subject \"{subject}\"");
            }
            else
            {
                _mailMessage.Subject = "(no subject)";

                _log.WriteDebugMessage("Email message has no subject");
            }

            if (!string.IsNullOrEmpty(body))
            {
                _mailMessage.Body = body;

                _log.WriteDebugMessage("Body of email message has been set");
            }

            _mailMessage.IsBodyHtml = isBodyHtml;

            if (isBodyHtml)
            {
                _log.WriteDebugMessage("Body of email message is in HTML format");
            }
            else
            {
                _log.WriteDebugMessage("Body of email message is not in HTML format");
            }

            if (ssl)
            {
                _log.WriteDebugMessage("Email message will be sent securely because SSL was set");
            }
            else
            {
                _log.WriteDebugMessage("Email message will be sent insecurely because SSL was not set");
            }

            _smtpClient = new SmtpClient(host, port)
            {
                EnableSsl = ssl,
                Credentials = new NetworkCredential(username, password)
            };

            _log.WriteDebugMessage("SMTP client has been prepared. Email message has been composed and ready to be sent");
        }

        /// <summary>
        /// Adds a file as an attachment to the email message based on the filepath of the file to attach.
        /// </summary>
        /// <param name="filepath">The filepath of the file to attach to the email message.</param>
        public void AddFileAttachmentByFilePath(string filepath)
        {
            _mailMessage.Attachments.Add(new Attachment(filepath));

            _log.WriteDebugMessage($"The file at path \"{filepath}\" has been added to the email message as an attachment");
        }

        /// <summary>
        /// Returns the necessary question to ask the user about sending the email message based on the properties of the composed email message.
        /// </summary>
        /// <returns>The question to ask the user.</returns>
        public string AskQuestionAboutSendingEmailMessage()
        {
            string to = string.Empty;
            string cc = string.Empty;
            string bcc = string.Empty;
            string question = string.Empty;

            if (_smtpClient != null && _mailMessage != null)
            {
                question = $"Do you want to send this email message from {_mailMessage.From}";

                if (_mailMessage.To.Count > 0)
                {
                    question += " to ";

                    foreach (MailAddress mailAddress in _mailMessage.To)
                    {
                        to += mailAddress.Address + ", ";
                    }

                    // Remove the trailing ", " at the end;
                    to = to.Substring(0, to.Length - 2);

                    question += to;
                }

                if (_mailMessage.CC.Count > 0)
                {
                    question += " and carbon copied to ";

                    foreach (MailAddress mailAddress in _mailMessage.CC)
                    {
                        cc += mailAddress.Address + ", ";
                    }

                    // Remove the trailing ", " at the end;
                    cc = cc.Substring(0, cc.Length - 2);

                    question += cc;
                }

                if (_mailMessage.Bcc.Count > 0)
                {
                    question += " and blind carbon copied to ";

                    foreach (MailAddress mailAddress in _mailMessage.Bcc)
                    {
                        bcc += mailAddress.Address + ", ";
                    }

                    // Remove the trailing ", " at the end;
                    bcc = bcc.Substring(0, bcc.Length - 2);

                    question += bcc;
                }

                question += $" using {_smtpClient.Host}:{_smtpClient.Port}?";
            }

            return question;
        }

        /// <summary>
        /// Disposes of the SMTP client (if it was previously available when composing an email message).
        /// </summary>
        public void DisposeSmtpClient()
        {
            if (_smtpClient != null)
            {
                _smtpClient.Dispose();
            }
        }

        /// <summary>
        /// Sends the email message. The email message must have been composed before this method is called to send the email message.
        /// </summary>
        public bool SendEmailMessage()
        {
            try
            {
                if (_smtpClient != null && _mailMessage != null)
                {
                    _smtpClient.Send(_mailMessage);

                    return true;
                }
                else
                {
                    _log.WriteDebugMessage("Email not sent. The SMTP client or the mail message has not been initialized");

                    return false;
                }
            }
            catch (Exception ex)
            {
                _log.WriteErrorMessage("Email Manager has encountered an error");
                _log.WriteErrorMessage(ex.Message);

                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    _log.WriteErrorMessage(ex.InnerException.Message);
                }

                _log.WriteErrorMessage(ex.StackTrace);

                return false;
            }
        }
    }
}
