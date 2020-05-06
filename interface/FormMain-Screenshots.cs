﻿//-----------------------------------------------------------------------
// <copyright file="FormMain-Screenshots.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Deletes old screenshots and saves new screenshots every five minutes (300000 milliseconds).
        /// This is for maintenance purposes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerPerformMaintenance_Tick(object sender, EventArgs e)
        {
            // Save and delete old screenshots.
            // This can take about 10 minutes when the application is first started because we initialize
            // the thread after the first 5 minutes and then save screenshots in the next 5 minutes after that.
            // Screenshots will then be saved every 5 minutes after this initial 10 minute delay.
            SaveScreenshots();

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

        /// <summary>
        /// Emails screenshots using the EmailSever and EmailMessage settings in application settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emailScreenshot_Click(object sender, EventArgs e)
        {
            Screenshot screenshot = null;

            Slide selectedSlide = Slideshow.SelectedSlide;

            if (selectedSlide != null)
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

                EmailScreenshot(screenshot, prompt: true);
            }
            else
            {
                MessageBox.Show("SMTP settings are configured but no image is available to email.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else
            {
                if (!runSaveScreenshotsThread.IsBusy)
                {
                    runSaveScreenshotsThread.RunWorkerAsync();
                }
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
            else
            {
                if (!runScreenshotSearchThread.IsBusy)
                {
                    runScreenshotSearchThread.RunWorkerAsync();
                }
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
                listBoxScreenshots.DisplayMember = "Value";
                listBoxScreenshots.ValueMember = "Name";
                listBoxScreenshots.DataSource = _screenshotCollection.GetSlides(comboBoxFilterType.Text, comboBoxFilterValue.Text, monthCalendar.SelectionStart.ToString(MacroParser.DateFormat));

                // Show the last set of screenshots only if the user is on today's list.
                if (listBoxScreenshots.Items.Count > 0 && monthCalendar.SelectionStart.Date.Equals(DateTime.Now.Date))
                {
                    listBoxScreenshots.SelectedIndex = listBoxScreenshots.Items.Count - 1;
                }
            }
        }

        private void RunSaveScreenshots(DoWorkEventArgs e)
        {
            _screenshotCollection.SaveToXmlFile((int)numericUpDownKeepScreenshotsForDays.Value);
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
            textBoxLabel.Text = string.Empty;
            textBoxScreenshotTitle.Text = string.Empty;
            textBoxScreenshotFormat.Text = string.Empty;
            textBoxScreenshotWidth.Text = string.Empty;
            textBoxScreenshotHeight.Text = string.Empty;
            textBoxScreenshotDate.Text = string.Empty;
            textBoxScreenshotTime.Text = string.Empty;

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
                    toolStripTextBox.Text = Path.GetFileName(path);
                    toolStripTextBox.ToolTipText = path;

                    FileInfo fileInfo = new FileInfo(path);

                    if (fileInfo.Directory != null && fileInfo.Directory.Root.Exists)
                    {
                        string dirName = Path.GetDirectoryName(path);

                        if (!string.IsNullOrEmpty(dirName))
                        {
                            if (Directory.Exists(dirName) && File.Exists(path))
                            {
                                toolStripTextBox.BackColor = Color.PaleGreen;
                            }
                            else
                            {
                                toolStripTextBox.BackColor = Color.PaleVioletRed;
                                toolStripTextBox.ToolTipText = $"Could not find or access image file at path \"{path}\"";
                            }
                        }
                    }

                    pictureBox.Image = _screenCapture.GetImageByPath(path);

                    if (pictureBox.Image != null)
                    {
                        textBoxLabel.Text = selectedScreenshot.Label;
                        textBoxScreenshotTitle.Text = selectedScreenshot.WindowTitle;
                        textBoxScreenshotFormat.Text = selectedScreenshot.Format.Name;

                        textBoxScreenshotWidth.Text = pictureBox.Image.Width.ToString();
                        textBoxScreenshotHeight.Text = pictureBox.Image.Height.ToString();

                        textBoxScreenshotDate.Text = selectedScreenshot.Date;
                        textBoxScreenshotTime.Text = selectedScreenshot.Time;
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
                    File.Exists(selectedScreenshot.Path))
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
            contextMenuStripScreenshotPreview.Items.Clear();

            ToolStripMenuItem showScreenshotLocationToolStripItem = new ToolStripMenuItem("Show Screenshot Location");
            showScreenshotLocationToolStripItem.Click +=
                new EventHandler(showScreenshotLocation_Click);

            ToolStripMenuItem addNewEditorToolStripItem = new ToolStripMenuItem("Add New Editor ...");
            addNewEditorToolStripItem.Click += new EventHandler(addEditor_Click);

            contextMenuStripScreenshotPreview.Items.Add(showScreenshotLocationToolStripItem);
            contextMenuStripScreenshotPreview.Items.Add(new ToolStripSeparator());
            contextMenuStripScreenshotPreview.Items.Add(addNewEditorToolStripItem);

            foreach (Editor editor in formEditor.EditorCollection)
            {
                if (editor != null && File.Exists(editor.Application))
                {
                    // ****************** EDITORS LIST IN CONTEXTUAL MENU *************************
                    // Add the Editor to the screenshot preview contextual menu.

                    contextMenuStripScreenshotPreview.Items.Add(editor.Name,
                        Icon.ExtractAssociatedIcon(editor.Application).ToBitmap(), runEditor_Click);
                    // ****************************************************************************
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screenshot"></param>
        /// <param name="prompt"></param>
        private void EmailScreenshot(Screenshot screenshot, bool prompt)
        {
            try
            {
                Log.WriteDebugMessage(":: EmailScreenshot Start ::");

                if (screenshot == null || string.IsNullOrEmpty(screenshot.Path))
                {
                    Log.WriteDebugMessage("Cannot email screenshot because screenshot is either null or path is empty");
                    return;
                }

                Log.WriteDebugMessage("Attempting to email screenshot \"" + screenshot.Path + "\"");

                string host = Settings.Application.GetByKey("EmailServerHost", string.Empty).Value.ToString();

                Log.WriteDebugMessage("Host = " + host);

                int.TryParse(Settings.Application.GetByKey("EmailServerPort", defaultValue: 587).Value.ToString(), out int port);

                Log.WriteDebugMessage("Port = " + port);

                bool.TryParse(Settings.Application.GetByKey("EmailServerEnableSSL", defaultValue: true).Value.ToString(), out bool ssl);

                Log.WriteDebugMessage("SSL = " + ssl);

                bool.TryParse(Settings.Application.GetByKey("EmailPrompt", defaultValue: true).Value.ToString(), out prompt);

                Log.WriteDebugMessage("Prompt = " + prompt);

                string username = Settings.Application.GetByKey("EmailClientUsername", string.Empty).Value.ToString();

                Log.WriteDebugMessage("Username = " + username);

                string password = Settings.Application.GetByKey("EmailClientPassword", string.Empty).Value.ToString();

                if (string.IsNullOrEmpty(password))
                {
                    Log.WriteDebugMessage("Password = [empty]");
                }
                else
                {
                    Log.WriteDebugMessage("Password = [I'm not going to log this so check the application settings file]");
                }

                string from = Settings.Application.GetByKey("EmailMessageFrom", string.Empty).Value.ToString();

                Log.WriteDebugMessage("From = " + from);

                string to = Settings.Application.GetByKey("EmailMessageTo", string.Empty).Value.ToString();

                Log.WriteDebugMessage("To = " + to);

                string cc = Settings.Application.GetByKey("EmailMessageCC", string.Empty).Value.ToString();

                Log.WriteDebugMessage("CC = " + cc);

                string bcc = Settings.Application.GetByKey("EmailMessageBCC", string.Empty).Value.ToString();

                Log.WriteDebugMessage("BCC = " + bcc);

                string subject = Settings.Application.GetByKey("EmailMessageSubject", string.Empty).Value.ToString();

                Log.WriteDebugMessage("Subject = " + subject);

                string body = Settings.Application.GetByKey("EmailMessageBody", string.Empty).Value.ToString();

                Log.WriteDebugMessage("Body = " + body);

                if (string.IsNullOrEmpty(host) ||
                    string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(@from) ||
                    string.IsNullOrEmpty(to))
                {
                    Log.WriteDebugMessage("Host, Username, Password, From, or To is empty");

                    return;
                }

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(@from)
                };

                mailMessage.To.Add(to);

                if (!string.IsNullOrEmpty(cc))
                    mailMessage.CC.Add(cc);

                if (!string.IsNullOrEmpty(bcc))
                    mailMessage.Bcc.Add(bcc);

                if (!string.IsNullOrEmpty(subject))
                    mailMessage.Subject = subject;

                if (!string.IsNullOrEmpty(body))
                    mailMessage.Body = body;

                mailMessage.IsBodyHtml = false;

                mailMessage.Attachments.Add(new Attachment(screenshot.Path));

                if (mailMessage.Attachments == null || mailMessage.Attachments.Count <= 0) return;

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
                }
                else
                {
                    Log.WriteDebugMessage("Sending email without prompt confirmation");
                    smtpClient.Send(mailMessage);
                    Log.WriteDebugMessage("Email sent");
                }

                smtpClient.Dispose();
                Log.WriteDebugMessage("SMTP client disposed");

                Log.WriteDebugMessage(":: EmailScreenshot End ::");
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("FormMain-Screenshots::EmailScreenshot", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="triggerActionType"></param>
        private void EmailScreenshot(TriggerActionType triggerActionType)
        {
            if (triggerActionType == TriggerActionType.EmailScreenshot && _screenCapture.Running)
            {
                Screenshot screenshot = _screenshotCollection.Get(_screenshotCollection.Count - 1);

                if (screenshot != null && screenshot.Slide != null && !string.IsNullOrEmpty(screenshot.Path))
                {
                    EmailScreenshot(screenshot, prompt: false);
                }
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