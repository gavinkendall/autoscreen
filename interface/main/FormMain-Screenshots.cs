//-----------------------------------------------------------------------
// <copyright file="FormMain-Screenshots.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods handling screenshots in the interface.</summary>
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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Saves screenshots and refreshes the calendar every five minutes (300000 milliseconds).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerPerformMaintenance_Tick(object sender, EventArgs e)
        {
            RunTriggersOfConditionType(TriggerConditionType.BeforeScreenshotSaved);

            // Save screenshots.
            SaveScreenshots();

            RunTriggersOfConditionType(TriggerConditionType.AfterScreenshotSaved);

            // Refresh the calendar.
            SearchDates();
        }

        /// <summary>
        /// Shows the list of screenshots when a date on the calendar has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateSelected_monthCalendar(object sender, DateRangeEventArgs e)
        {
            ShowScreenshots();
        }

        private void screenshotMetadata_Click(object sender, EventArgs e)
        {
            if (!_formScreenshotMetadata.Visible)
            {
                _formScreenshotMetadata.Show();
            }
            else
            {
                _formScreenshotMetadata.Focus();
                _formScreenshotMetadata.BringToFront();
            }
        }

        /// <summary>
        /// Emails screenshots using the EmailSever and EmailMessage settings in user settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emailScreenshot_Click(object sender, EventArgs e)
        {
            Screenshot screenshot = null;

            Slide selectedSlide = Slideshow.SelectedSlide;

            if (selectedSlide != null && listBoxScreenshots.SelectedIndex > -1)
            {
                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
                {
                    var screen = (Screen)tabControlViews.SelectedTab.Tag;

                    screenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, screen.ViewId);
                }

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
                {
                    var region = (Region)tabControlViews.SelectedTab.Tag;

                    screenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, region.ViewId);
                }

                bool.TryParse(Settings.Application.GetByKey("EmailPrompt", DefaultSettings.EmailPrompt).Value.ToString(), out bool prompt);

                if (EmailScreenshot(screenshot, prompt))
                {
                    MessageBox.Show("Successfully emailed screenshot.", "Email Successfully Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Screenshot was not sent.", "Email Not Sent", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Email settings are configured but no image is available to email.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sends a screenshot to a file server depending on user settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileTransferScreenshot_Click(object sender, EventArgs e)
        {
            Screenshot screenshot = null;

            Slide selectedSlide = Slideshow.SelectedSlide;

            if (selectedSlide != null && listBoxScreenshots.SelectedIndex > -1)
            {
                // *** Auto Screen Capture - Region Select / Auto Save ***
                //screenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, Guid.Empty);

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
                {
                    var screen = (Screen)tabControlViews.SelectedTab.Tag;

                    screenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, screen.ViewId);
                }

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
                {
                    var region = (Region)tabControlViews.SelectedTab.Tag;

                    screenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, region.ViewId);
                }

                if (FileTransferScreenshot(screenshot))
                {
                    MessageBox.Show("Successfully uploaded screenshot to file server.", "File Transfer Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Screenshot did not upload to file server.", "File Transfer Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("File Transfer settings are configured but no image is available to send to the file server.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveScreenshots()
        {
            if (runSaveScreenshotsThread == null)
            {
                runSaveScreenshotsThread = new BackgroundWorker
                {
                    WorkerReportsProgress = false,
                    WorkerSupportsCancellation = true
                };

                runSaveScreenshotsThread.DoWork += new DoWorkEventHandler(DoWork_runSaveScreenshotsThread);
            }

            if (!runSaveScreenshotsThread.IsBusy)
            {
                runSaveScreenshotsThread.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Searches for screenshots.
        /// </summary>
        private void SearchScreenshots()
        {
            Log.WriteDebugMessage("Searching for screenshots");

            Slideshow.Index = 0;
            Slideshow.Count = 0;

            listBoxScreenshots.BeginUpdate();

            listBoxScreenshots.DataSource = null;

            if (runScreenshotSearchThread == null)
            {
                runScreenshotSearchThread = new BackgroundWorker
                {
                    WorkerReportsProgress = false,
                    WorkerSupportsCancellation = true
                };

                runScreenshotSearchThread.DoWork += new DoWorkEventHandler(DoWork_runScreenshotSearchThread);
            }

            if (!runScreenshotSearchThread.IsBusy)
            {
                runScreenshotSearchThread.RunWorkerAsync();
            }

            listBoxScreenshots.EndUpdate();
        }

        /// <summary>
        /// This thread is responsible for finding slides.
        /// </summary>
        /// <param name="e"></param>
        private void RunSlideSearch(DoWorkEventArgs e)
        {
            if (listBoxScreenshots.InvokeRequired)
            {
                listBoxScreenshots.Invoke(new RunSlideSearchDelegate(RunSlideSearch), new object[] { e });
            }
            else
            {
                if (_screenshotCollection != null)
                {
                    listBoxScreenshots.DisplayMember = "Value";
                    listBoxScreenshots.ValueMember = "Name";
                    listBoxScreenshots.Sorted = true;
                    listBoxScreenshots.DataSource = _screenshotCollection.GetSlides(comboBoxFilterType.Text, comboBoxFilterValue.Text, monthCalendar.SelectionStart.ToString(MacroParser.DateFormat));

                    // Show the last set of screenshots only if the user is on today's list.
                    if (listBoxScreenshots.Items.Count > 0 && monthCalendar.SelectionStart.Date.Equals(DateTime.Now.Date))
                    {
                        listBoxScreenshots.SelectedIndex = listBoxScreenshots.Items.Count - 1;
                    }
                }
            }
        }

        private void RunSaveScreenshots(DoWorkEventArgs e)
        {
            _screenshotCollection.SaveToXmlFile();
        }

        /// <summary>
        /// Whenever the user clicks on a screenshot in the list of screenshots then make sure to update the appropriate image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChanged_listBoxScreenshots(object sender, EventArgs e)
        {
            Slideshow.Index = listBoxScreenshots.SelectedIndex;
            Slideshow.Count = listBoxScreenshots.Items.Count;

            ShowScreenshotBySlideIndex();
        }

        private void ShowScreenshotBySlideIndex()
        {
            _formScreenshotMetadata.textBoxLabel.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotTitle.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotFormat.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotWidth.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotHeight.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotDate.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotTime.Text = string.Empty;

            // Dashboard
            if (tabControlViews.TabCount > 0 && tabControlViews.SelectedTab != null && tabControlViews.SelectedTab.Name.Equals("tabPageDashboard"))
            {
                FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)tabControlViews.SelectedTab.Controls["flowLayoutPanel"];

                int i = 1;

                foreach (GroupBox groupBox in flowLayoutPanel.Controls)
                {
                    Screenshot selectedScreenshot = new Screenshot();

                    if (Slideshow.Index >= 0 && Slideshow.Index <= (Slideshow.Count - 1))
                    {
                        Slideshow.SelectedSlide = (Slide)listBoxScreenshots.Items[Slideshow.Index];

                        PictureBox pictureBox = (PictureBox)groupBox.Controls["pictureBox" + i];

                        // *** Auto Screen Capture - Region Select / Auto Save ***
                        //selectedScreenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, Guid.Empty);

                        if (groupBox.Tag.GetType() == typeof(Screen))
                        {
                            Screen screen = (Screen)groupBox.Tag;
                            selectedScreenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, screen.ViewId);
                        }

                        if (groupBox.Tag.GetType() == typeof(Region))
                        {
                            Region region = (Region)groupBox.Tag;
                            selectedScreenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, region.ViewId);
                        }

                        if (!string.IsNullOrEmpty(selectedScreenshot.Path))
                        {
                            pictureBox.Image = _screenCapture.GetImageByPath(selectedScreenshot.Path);
                        }
                        else
                        {
                            pictureBox.Image = null;
                        }
                    }

                    i++;
                }

                return;
            }

            // Screens and Regions
            if (tabControlViews.TabCount > 0 && tabControlViews.SelectedTab != null)
            {
                TabPage selectedTabPage = tabControlViews.SelectedTab;

                ToolStrip toolStrip = (ToolStrip)selectedTabPage.Controls[selectedTabPage.Name + "toolStrip"];

                ToolStripTextBox toolStripTextBox = (ToolStripTextBox)toolStrip.Items[selectedTabPage.Name + "toolStripTextBoxFilename"];

                PictureBox pictureBox = (PictureBox)selectedTabPage.Controls[selectedTabPage.Name + "pictureBox"];

                Screenshot selectedScreenshot = new Screenshot();

                if (Slideshow.Index >= 0 && Slideshow.Index <= (Slideshow.Count - 1))
                {
                    Slideshow.SelectedSlide = (Slide)listBoxScreenshots.Items[Slideshow.Index];

                    // *** Auto Screen Capture - Region Select / Auto Save ***
                    //selectedScreenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, Guid.Empty);

                    if (selectedTabPage.Tag.GetType() == typeof(Screen))
                    {
                        Screen screen = (Screen)selectedTabPage.Tag;
                        selectedScreenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, screen.ViewId);
                    }

                    if (selectedTabPage.Tag.GetType() == typeof(Region))
                    {
                        Region region = (Region)selectedTabPage.Tag;
                        selectedScreenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, region.ViewId);
                    }
                }

                string path = selectedScreenshot.Path;

                if (!string.IsNullOrEmpty(path))
                {
                    toolStripTextBox.Text = FileSystem.GetFileName(path);
                    toolStripTextBox.ToolTipText = path;

                    string dirName = FileSystem.GetDirectoryName(path);

                    if (!string.IsNullOrEmpty(dirName))
                    {
                        if (FileSystem.DirectoryExists(dirName) && FileSystem.FileExists(path))
                        {
                            toolStripTextBox.BackColor = Color.PaleGreen;
                        }
                        else
                        {
                            toolStripTextBox.BackColor = Color.PaleVioletRed;
                            toolStripTextBox.ToolTipText = $"Could not find or access image file at path \"{path}\"";
                        }
                    }

                    pictureBox.Image = _screenCapture.GetImageByPath(path);

                    if (pictureBox.Image != null)
                    {
                        _formScreenshotMetadata.textBoxLabel.Text = selectedScreenshot.Label;
                        _formScreenshotMetadata.textBoxScreenshotTitle.Text = selectedScreenshot.WindowTitle;
                        _formScreenshotMetadata.textBoxScreenshotFormat.Text = selectedScreenshot.Format.Name;

                        _formScreenshotMetadata.textBoxScreenshotWidth.Text = pictureBox.Image.Width.ToString();
                        _formScreenshotMetadata.textBoxScreenshotHeight.Text = pictureBox.Image.Height.ToString();

                        _formScreenshotMetadata.textBoxScreenshotDate.Text = selectedScreenshot.Date;
                        _formScreenshotMetadata.textBoxScreenshotTime.Text = selectedScreenshot.Time;
                    }
                }
                else
                {
                    toolStripTextBox.Text = string.Empty;
                    toolStripTextBox.BackColor = Color.LightYellow;
                    toolStripTextBox.ToolTipText = string.Empty;

                    pictureBox.Image = null;
                }
            }
        }

        /// <summary>
        /// Shows the list of screenshots.
        /// </summary>
        private void ShowScreenshots()
        {
            SearchScreenshots();

            if (!tabControlModules.SelectedTab.Name.Equals("tabPageScreenshots"))
            {
                tabControlModules.SelectedTab = tabControlModules.TabPages["tabPageScreenshots"];
            }

            ShowScreenshotBySlideIndex();
        }

        /// <summary>
        /// Runs the slide search thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_runScreenshotSearchThread(object sender, DoWorkEventArgs e)
        {
            RunSlideSearch(e);
        }

        private void DoWork_runSaveScreenshotsThread(object sender, DoWorkEventArgs e)
        {
            RunSaveScreenshots(e);
        }

        /// <summary>
        /// Opens Windows Explorer to show the location of the selected screenshot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showScreenshotLocation_Click(object sender, EventArgs e)
        {
            if (listBoxScreenshots.SelectedIndex > -1)
            {
                Screenshot selectedScreenshot = new Screenshot();

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
                {
                    Screen screen = (Screen)tabControlViews.SelectedTab.Tag;
                    selectedScreenshot =
                        _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, screen.ViewId);
                }

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
                {
                    Region region = (Region)tabControlViews.SelectedTab.Tag;
                    selectedScreenshot =
                        _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, region.ViewId);
                }

                if (selectedScreenshot != null && !string.IsNullOrEmpty(selectedScreenshot.Path) &&
                    FileSystem.FileExists(selectedScreenshot.Path))
                {
                    Process.Start(FileSystem.FileManager, "/select,\"" + selectedScreenshot.Path + "\"");
                }
            }
        }

        /// <summary>
        /// Builds the sub-menus for the contextual menu that appears when the user right-clicks on the selected screenshot.
        /// </summary>
        private void BuildScreenshotPreviewContextualMenu()
        {
            contextMenuStripScreenshot.Items.Clear();

            ToolStripMenuItem showScreenshotLocationToolStripItem = new ToolStripMenuItem("Show Screenshot Location");
            showScreenshotLocationToolStripItem.Click +=
                new EventHandler(showScreenshotLocation_Click);

            ToolStripMenuItem addEditorToolStripItem = new ToolStripMenuItem("Add Editor");
            addEditorToolStripItem.Click += new EventHandler(addEditor_Click);

            contextMenuStripScreenshot.Items.Add(showScreenshotLocationToolStripItem);
            contextMenuStripScreenshot.Items.Add(new ToolStripSeparator());
            contextMenuStripScreenshot.Items.Add(addEditorToolStripItem);

            foreach (Editor editor in _formEditor.EditorCollection)
            {
                if (editor != null && FileSystem.FileExists(editor.Application))
                {
                    // ****************** EDITORS LIST IN CONTEXTUAL MENU *************************
                    // Add the Editor to the screenshot preview contextual menu.

                    contextMenuStripScreenshot.Items.Add(editor.Name,
                        Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), runEditor_Click);
                    // ****************************************************************************
                }
            }
        }

        /// <summary>
        /// Emails a screenshot using the SMTP settings configured in the user settings file.
        /// </summary>
        /// <param name="screenshot">The screenshot to email.</param>
        /// <param name="prompt">Determines if we should prompt the user with a confirmation dialog box.</param>
        private bool EmailScreenshot(Screenshot screenshot, bool prompt)
        {
            try
            {
                Log.WriteDebugMessage("Screenshot attempting to be emailed");

                if (screenshot == null || string.IsNullOrEmpty(screenshot.Path))
                {
                    Log.WriteDebugMessage("Cannot email screenshot because screenshot is either null or path is empty");

                    return false;
                }

                Log.WriteDebugMessage("Attempting to email screenshot \"" + screenshot.Path + "\"");

                string host = Settings.SMTP.GetByKey("EmailServerHost", DefaultSettings.EmailServerHost).Value.ToString();

                Log.WriteDebugMessage("Host = " + host);

                int.TryParse(Settings.SMTP.GetByKey("EmailServerPort", DefaultSettings.EmailServerPort).Value.ToString(), out int port);

                Log.WriteDebugMessage("Port = " + port);

                bool.TryParse(Settings.SMTP.GetByKey("EmailServerEnableSSL", DefaultSettings.EmailServerEnableSSL).Value.ToString(), out bool ssl);

                Log.WriteDebugMessage("SSL = " + ssl);

                Log.WriteDebugMessage("Prompt = " + prompt);

                string username = Settings.SMTP.GetByKey("EmailClientUsername", DefaultSettings.EmailClientUsername).Value.ToString();

                Log.WriteDebugMessage("Username = " + username);

                string password = Settings.SMTP.GetByKey("EmailClientPassword", DefaultSettings.EmailClientPassword).Value.ToString();

                if (string.IsNullOrEmpty(password))
                {
                    Log.WriteDebugMessage("Password = [empty]");
                }
                else
                {
                    Log.WriteDebugMessage("Password = [I'm not going to log this so check the user settings file]");
                }

                string from = Settings.SMTP.GetByKey("EmailMessageFrom", DefaultSettings.EmailMessageFrom).Value.ToString();

                Log.WriteDebugMessage("From = " + from);

                string to = Settings.SMTP.GetByKey("EmailMessageTo", DefaultSettings.EmailMessageTo).Value.ToString();

                Log.WriteDebugMessage("To = " + to);

                string cc = Settings.SMTP.GetByKey("EmailMessageCC", DefaultSettings.EmailMessageCC).Value.ToString();

                Log.WriteDebugMessage("CC = " + cc);

                string bcc = Settings.SMTP.GetByKey("EmailMessageBCC", DefaultSettings.EmailMessageBCC).Value.ToString();

                Log.WriteDebugMessage("BCC = " + bcc);

                string subject = Settings.SMTP.GetByKey("EmailMessageSubject", DefaultSettings.EmailMessageSubject).Value.ToString();

                Log.WriteDebugMessage("Subject = " + subject);

                string body = Settings.SMTP.GetByKey("EmailMessageBody", DefaultSettings.EmailMessageBody).Value.ToString();

                Log.WriteDebugMessage("Body = " + body);

                if (string.IsNullOrEmpty(host) ||
                    string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(@from) ||
                    string.IsNullOrEmpty(to))
                {
                    Log.WriteDebugMessage("Host, Username, Password, From, or To is empty");

                    return false;
                }

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(@from)
                };

                mailMessage.To.Add(to);

                if (!string.IsNullOrEmpty(cc))
                {
                    mailMessage.CC.Add(cc);
                }

                if (!string.IsNullOrEmpty(bcc))
                {
                    mailMessage.Bcc.Add(bcc);
                }

                if (!string.IsNullOrEmpty(subject))
                {
                    mailMessage.Subject = subject;
                }

                if (!string.IsNullOrEmpty(body))
                {
                    mailMessage.Body = body;
                }

                mailMessage.IsBodyHtml = false;

                mailMessage.Attachments.Add(new Attachment(screenshot.Path));

                if (mailMessage.Attachments == null || mailMessage.Attachments.Count <= 0)
                {
                    return false;
                }

                Log.WriteDebugMessage("Added screenshot as attachment");

                var smtpClient = new SmtpClient(host, port)
                {
                    EnableSsl = ssl,
                    Credentials = new NetworkCredential(username, password)
                };

                Log.WriteDebugMessage("SMTP client prepared");

                if (prompt)
                {
                    DialogResult dialogResult = MessageBox.Show($"Do you want to email this screenshot from \"{from}\" to \"{to}\" using \"{host}:{port}\"?", "Email Screenshot", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        Log.WriteDebugMessage("Sending email with prompt confirmation");

                        smtpClient.Send(mailMessage);

                        Log.WriteDebugMessage("Email sent");
                    }
                    else
                    {
                        smtpClient.Dispose();

                        return false;
                    }
                }
                else
                {
                    Log.WriteDebugMessage("Sending email without prompt confirmation");

                    smtpClient.Send(mailMessage);

                    Log.WriteDebugMessage("Email sent");
                }

                smtpClient.Dispose();

                Log.WriteDebugMessage("SMTP client disposed");

                return true;
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;

                Log.WriteExceptionMessage("FormMain-Screenshots::EmailScreenshot", ex);

                return false;
            }
        }

        private void EmailScreenshot(TriggerActionType triggerActionType)
        {
            if (triggerActionType == TriggerActionType.EmailScreenshot && _screenCapture.Running)
            {
                Screenshot lastScreenshotOfThisView = _screenshotCollection.GetLastScreenshotOfView(_screenshotCollection.LastViewId);

                if (lastScreenshotOfThisView != null && lastScreenshotOfThisView.Slide != null && !string.IsNullOrEmpty(lastScreenshotOfThisView.Path) && !string.IsNullOrEmpty(lastScreenshotOfThisView.Hash))
                {
                    if (_screenshotCollection.OptimizeScreenCapture)
                    {
                        if (!_screenshotCollection.EmailedScreenshotHashList.Contains(lastScreenshotOfThisView.Hash))
                        {
                            if (EmailScreenshot(lastScreenshotOfThisView, prompt: false))
                            {
                                _screenshotCollection.EmailedScreenshotHashList.Add(lastScreenshotOfThisView.Hash);
                            }
                        }
                    }
                    else
                    {
                        EmailScreenshot(lastScreenshotOfThisView, prompt: false);
                    }
                }
            }
        }

        /// <summary>
        /// Uploads a screenshot to a file server.
        /// </summary>
        /// <param name="screenshot">The screenshot to upload.</param>
        /// <returns>True if the upload was successful otherwise false if the upload failed.</returns>
        private bool FileTransferScreenshot(Screenshot screenshot)
        {
            try
            {
                Log.WriteDebugMessage("Screenshot attempting to transfer to file server");

                if (screenshot == null || string.IsNullOrEmpty(screenshot.Path))
                {
                    Log.WriteDebugMessage("Cannot upload screenshot to file server because screenshot is either null or path is empty");

                    return false;
                }

                Log.WriteDebugMessage("Attempting to upload screenshot \"" + screenshot.Path + "\" to file server");

                string host = Settings.SFTP.GetByKey("FileTransferServerHost", DefaultSettings.FileTransferServerHost).Value.ToString();

                Log.WriteDebugMessage("Host = " + host);

                int.TryParse(Settings.SFTP.GetByKey("FileTransferServerPort", DefaultSettings.FileTransferServerPort).Value.ToString(), out int port);

                Log.WriteDebugMessage("Port = " + port);

                string username = Settings.SFTP.GetByKey("FileTransferClientUsername", DefaultSettings.FileTransferClientUsername).Value.ToString();

                Log.WriteDebugMessage("Username = " + username);

                string password = Settings.SFTP.GetByKey("FileTransferClientPassword", DefaultSettings.FileTransferClientPassword).Value.ToString();

                if (string.IsNullOrEmpty(password))
                {
                    Log.WriteDebugMessage("Password = [empty]");
                }
                else
                {
                    Log.WriteDebugMessage("Password = [I'm not going to log this so check the user settings file]");
                }

                if (string.IsNullOrEmpty(host) ||
                    string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(password))
                {
                    Log.WriteDebugMessage("Host, Username, or Password is empty");

                    return false;
                }

                if (_sftpClient == null)
                {
                    _sftpClient = new Gavin.Kendall.SFTP.SftpClient(host, port, username, password);
                }

                Log.WriteDebugMessage("Attempting to connect to file server");

                if (!_sftpClient.IsConnected)
                {
                    if (_sftpClient.Connect())
                    {
                        Log.WriteDebugMessage("Connection to file server established");
                    }
                    else
                    {
                        Log.WriteDebugMessage("Could not establish a connection with the file server");

                        return false;
                    }
                }
                
                // Make sure we are connected to the file server. If we were not connected earlier then a connection request would have been sent prior to this check.
                if (_sftpClient.IsConnected)
                {
                    string destinationPath = System.IO.Path.GetFileName(screenshot.Path);
                    
                    Log.WriteDebugMessage("Attempting to upload screenshot to file server");
                    Log.WriteDebugMessage("Source: " + screenshot.Path);
                    Log.WriteDebugMessage("Destination: " + destinationPath);

                    if (_sftpClient.UploadFile(screenshot.Path, destinationPath))
                    {
                        Log.WriteDebugMessage("Successfully uploaded screenshot");
                    }
                    else
                    {
                        Log.WriteDebugMessage("Failed to upload screenshot");

                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;

                Log.WriteExceptionMessage("FormMain-Screenshots::FileTransferScreenshot", ex);

                return false;
            }
        }

        private void tabControlViews_Selected(object sender, TabControlEventArgs e)
        {
            ShowScreenshotBySlideIndex();
        }

        private void comboBoxFilterValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFilterValue.SelectedIndex > 0)
            {
                SearchDates();
                ShowScreenshots();
            }
        }

        private void buttonRefreshFilterValues_Click(object sender, EventArgs e)
        {
            SearchFilterValues();
            SearchDates();
            ShowScreenshots();
        }
    }
}