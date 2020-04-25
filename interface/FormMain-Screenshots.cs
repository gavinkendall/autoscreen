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
        /// Saves screenshots every five minutes (300000 milliseconds).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerPerformMaintenance_Tick(object sender, EventArgs e)
        {
            _screenCapture.PerformingMaintenance = true;

            // Save and delete old screenshots.
            SaveScreenshots();

            // Refresh the calendar.
            SearchDates();

            _screenCapture.PerformingMaintenance = false;
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

            if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
            {
                var screen = (Screen)tabControlViews.SelectedTab.Tag;

                screenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, screen.ViewId);
            }

            if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
            {
                var region = (Region)tabControlViews.SelectedTab.Tag;

                screenshot = _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, region.ViewId);
            }

            EmailScreenshot(screenshot, prompt: true);
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

            GC.Collect();
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
                if (listBoxScreenshots.Items.Count > 0 && monthCalendar.SelectionStart.ToString(MacroParser.DateFormat).Equals(DateTime.Now.ToString(MacroParser.DateFormat)))
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
                if (screenshot == null || string.IsNullOrEmpty(screenshot.Path)) return;

                string from = Settings.Application.GetByKey("EmailMessageFrom", string.Empty).Value.ToString();
                string to = Settings.Application.GetByKey("EmailMessageTo", string.Empty).Value.ToString();
                string cc = Settings.Application.GetByKey("EmailMessageCC", string.Empty).Value.ToString();
                string bcc = Settings.Application.GetByKey("EmailMessageBCC", string.Empty).Value.ToString();
                string subject = Settings.Application.GetByKey("EmailMessageSubject", string.Empty).Value.ToString();
                string body = Settings.Application.GetByKey("EmailMessageBody", string.Empty).Value.ToString();

                if (string.IsNullOrEmpty(@from) || string.IsNullOrEmpty(to)) return;

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(@from);
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

                string host = Settings.Application.GetByKey("EmailServerHost", string.Empty).Value.ToString();
                int port = Convert.ToInt32(Settings.Application.GetByKey("EmailServerPort", string.Empty).Value);

                string username = Settings.Application.GetByKey("EmailClientUsername", string.Empty).Value.ToString();
                string password = Settings.Application.GetByKey("EmailClientPassword", string.Empty).Value.ToString();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return;

                var smtpClient = new SmtpClient(host, port);

                smtpClient.EnableSsl = Convert.ToBoolean(Settings.Application.GetByKey("EmailServerEnableSSL", string.Empty).Value);
                smtpClient.Credentials = new NetworkCredential(username, password);

                if (prompt)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want to email this screenshot?", "Email Screenshot", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        smtpClient.Send(mailMessage);
                    }
                }
                else
                {
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("FormMain::EmailScreenshot", ex);
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