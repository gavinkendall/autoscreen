//-----------------------------------------------------------------------
// <copyright file="FormMain-Screenshots.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        // A dictionary of failed uploads containing filepaths and hosts.
        private Dictionary<string, string> _failedUploads = new Dictionary<string, string>();

        /// <summary>
        /// Saves screenshot references every five minutes (300000 milliseconds).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerPerformMaintenance_Tick(object sender, EventArgs e)
        {
            RunTriggersOfConditionType(TriggerConditionType.BeforeScreenshotReferencesSaved);

            // Save screenshot references.
            SaveScreenshotReferences();

            RunTriggersOfConditionType(TriggerConditionType.AfterScreenshotReferencesSaved);
        }

        /// <summary>
        /// Shows the list of screenshots when a date on the calendar has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateSelected_monthCalendar(object sender, DateRangeEventArgs e)
        {
            if (!runDateSearchThread.IsBusy)
            {
                _config.Settings.User.SetValueByKey("SelectedCalendarDay", monthCalendar.SelectionStart.ToString());
            }

            ShowScreenshots();
        }

        private void screenshotMetadata_Click(object sender, EventArgs e)
        {
            if (!_formScreenshotMetadata.Visible)
            {
                _formScreenshotMetadata.Show(this);
            }
            else
            {
                _formScreenshotMetadata.Activate();
            }
        }

        /// <summary>
        /// Encrypts/Decrypts screenshot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void encryptDecryptScreenshot_Click(object sender, EventArgs e)
        {
            Screenshot screenshot = null;

            Slide selectedSlide = _slideShow.SelectedSlide;

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

                if (!_fileSystem.FileExists(screenshot.FilePath))
                {
                    return;
                }

                if (screenshot.Encrypted)
                {
                    try
                    {
                        _security.DecryptFile(screenshot.FilePath, screenshot.FilePath + "-decrypted", screenshot.Key);
                    }
                    catch (Exception ex)
                    {
                        _log.WriteMessage("WARNING: Error with file decryption for \"" + screenshot.FilePath + "\". Exception is " + ex);
                    }

                    if (_fileSystem.FileExists(screenshot.FilePath))
                    {
                        if (_fileSystem.DeleteFile(screenshot.FilePath))
                        {
                            _fileSystem.MoveFile(screenshot.FilePath + "-decrypted", screenshot.FilePath);

                            screenshot.Key = string.Empty;
                            screenshot.Encrypted = false;
                        }
                        else
                        {
                            MessageBox.Show("Cannot decrypt the file. It may be in use by another process.", "I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }
                    }
                }
                else
                {
                    string key = _security.EncryptFile(screenshot.FilePath, screenshot.FilePath + "-encrypted");

                    if (!string.IsNullOrEmpty(key))
                    {
                        if (_fileSystem.FileExists(screenshot.FilePath))
                        {
                            if (_fileSystem.DeleteFile(screenshot.FilePath))
                            {
                                _fileSystem.MoveFile(screenshot.FilePath + "-encrypted", screenshot.FilePath);

                                screenshot.Key = key;
                                screenshot.Encrypt = false;
                                screenshot.Encrypted = true;
                            }
                            else
                            {
                                MessageBox.Show("Cannot encrypt the file. It may be in use by another process.", "I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;
                            }
                        }
                    }
                    else
                    {
                        _log.WriteMessage("WARNING: Error with file encryption for \"" + screenshot.FilePath + "\"");
                    }
                }

                // We need to make sure this screenshot's reference is saved to the screenshots.xml file so set this
                // property to false whenever a screenshot has been encrypted or decrypted (because its data has changed).
                screenshot.ReferenceSaved = false;

                // Because this screenshot's data has changed there's a collection and an internal XML document that needs to be updated so the best way
                // to do this is by removing the "old" screenshot from the collection and then adding the "new" screenshot (with the updated data) back in.
                // There are XML nodes to manage and a few other things to take care of. It's not a simple operation.

                // Remove the screenshot from the screenshot collection.
                _screenshotCollection.Remove(screenshot);

                // Add it back in.
                _screenshotCollection.Add(screenshot);

                ShowScreenshotBySlideIndex();
            }
        }

        /// <summary>
        /// Emails screenshots using the EmailSever and EmailMessage settings in user settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emailScreenshot_Click(object sender, EventArgs e)
        {
            Screenshot selectedScreenshot = null;

            Slide selectedSlide = _slideShow.SelectedSlide;

            if (selectedSlide != null && listBoxScreenshots.SelectedIndex > -1)
            {
                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
                {
                    var screen = (Screen)tabControlViews.SelectedTab.Tag;

                    selectedScreenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, screen.ViewId);
                }

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
                {
                    var region = (Region)tabControlViews.SelectedTab.Tag;

                    selectedScreenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, region.ViewId);
                }

                if (selectedScreenshot.ViewId.Equals(Guid.Empty))
                {
                    // *** Auto Screen Capture - Region Select / Auto Save ***
                    selectedScreenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, Guid.Empty);
                }

                bool.TryParse(_config.Settings.SMTP.GetByKey("EmailPrompt", _config.Settings.DefaultSettings.EmailPrompt).Value.ToString(), out bool prompt);

                if (EmailScreenshot(selectedScreenshot, prompt))
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
            Screenshot selectedScreenshot = null;

            Slide selectedSlide = _slideShow.SelectedSlide;

            if (selectedSlide != null && listBoxScreenshots.SelectedIndex > -1)
            {
                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
                {
                    var screen = (Screen)tabControlViews.SelectedTab.Tag;

                    selectedScreenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, screen.ViewId);
                }

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
                {
                    var region = (Region)tabControlViews.SelectedTab.Tag;

                    selectedScreenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, region.ViewId);
                }

                if (selectedScreenshot.ViewId.Equals(Guid.Empty))
                {
                    // *** Auto Screen Capture - Region Select / Auto Save ***
                    selectedScreenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, Guid.Empty);
                }

                if (FileTransferScreenshot(selectedScreenshot))
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

        private void SaveScreenshotReferences()
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
            _log.WriteDebugMessage("Searching for screenshots");

            _slideShow.Index = 0;
            _slideShow.Count = 0;

            listBoxScreenshots.BeginUpdate();

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
                    listBoxScreenshots.DataSource = _screenshotCollection.GetSlides(filterType: comboBoxFilterType.Text, filterValue: comboBoxFilterValue.Text,
                        date: monthCalendar.SelectionStart.ToString(_macroParser.DateFormat),
                        config: _config,
                        slideValueToDisplay: comboBoxFilterType.Text); // Give the currently selected filter type as the slide value to display

                    int selectedSlideIndex = Convert.ToInt32(_config.Settings.User.GetByKey("SelectedSlideIndex", 0).Value);

                    if (listBoxScreenshots.Items.Count > 0)
                    {
                        if (selectedSlideIndex == -1)
                        {
                            selectedSlideIndex = 0;
                        }

                        if (selectedSlideIndex >= listBoxScreenshots.Items.Count)
                        {
                            selectedSlideIndex = listBoxScreenshots.Items.Count - 1;
                        }

                        listBoxScreenshots.SelectedIndex = selectedSlideIndex;
                    }
                }
            }
        }

        private void RunSaveScreenshots(DoWorkEventArgs e)
        {
            _screenshotCollection.SaveToXmlFile(_config);
        }

        /// <summary>
        /// Whenever the user clicks on a screenshot in the list of screenshots then make sure to update the appropriate image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChanged_listBoxScreenshots(object sender, EventArgs e)
        {
            if (!runScreenshotSearchThread.IsBusy)
            {
                // Save the selected slide index.
                _config.Settings.User.SetValueByKey("SelectedSlideIndex", listBoxScreenshots.SelectedIndex);
            }

            _slideShow.Index = listBoxScreenshots.SelectedIndex;
            _slideShow.Count = listBoxScreenshots.Items.Count;

            ShowScreenshotBySlideIndex();
        }

        private void ShowScreenshotBySlideIndex()
        {
            if (_preview)
            {
                toolStripDropDownButtonPreview.BackColor = Color.Black;
                toolStripDropDownButtonPreview.ForeColor = Color.White;
            }
            else
            {
                toolStripDropDownButtonPreview.BackColor = Color.White;
                toolStripDropDownButtonPreview.ForeColor = Color.Black;
            }

            ClearScreenshotMetadataFields();

            // Dashboard
            if (tabControlViews.TabCount > 0 && tabControlViews.SelectedTab != null && tabControlViews.SelectedTab.Name.Equals("tabPageDashboard"))
            {
                FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)tabControlViews.SelectedTab.Controls["flowLayoutPanel"];

                int i = 1;

                foreach (GroupBox groupBox in flowLayoutPanel.Controls)
                {
                    PictureBox pictureBox = (PictureBox)groupBox.Controls["pictureBox" + i];

                    pictureBox.Image = null;

                    // Preview
                    if (_preview)
                    {
                        if ((groupBox.Tag is Screen screen && screen.Enable) ||
                            (groupBox.Tag is Region region && region.Enable))
                        {
                            pictureBox.Image = DoPreview(groupBox.Tag);
                        }
                    }
                    else
                    {
                        groupBox.ForeColor = Color.Black;

                        Screenshot selectedScreenshot = new Screenshot();

                        if (_slideShow.Index >= 0 && _slideShow.Index <= (_slideShow.Count - 1))
                        {
                            _slideShow.SelectedSlide = (Slide)listBoxScreenshots.Items[_slideShow.Index];

                            Guid viewId = Guid.Empty;

                            if (groupBox.Tag.GetType() == typeof(Screen))
                            {
                                Screen screen = (Screen)groupBox.Tag;
                                viewId = screen.ViewId;
                            }

                            if (groupBox.Tag.GetType() == typeof(Region))
                            {
                                Region region = (Region)groupBox.Tag;
                                viewId = region.ViewId;
                            }

                            selectedScreenshot = _screenshotCollection.GetScreenshot(_slideShow.SelectedSlide.Name, viewId);

                            if (selectedScreenshot.ViewId.Equals(Guid.Empty))
                            {
                                // *** Auto Screen Capture - Region Select / Auto Save ***
                                selectedScreenshot = _screenshotCollection.GetScreenshot(_slideShow.SelectedSlide.Name, Guid.Empty);
                            }

                            // The screenshot may not have been found because it was not in the collection due to optimization (OptimizeScreenCapture).
                            // So get the earliest screenshot of this view (working backwards from the current index) that has a valid path so we can still show something.
                            if (selectedScreenshot.Slide == null)
                            {
                                for (int j = 0; (_slideShow.Index - j) >= 0; j++)
                                {
                                    _slideShow.SelectedSlide = (Slide)listBoxScreenshots.Items[(_slideShow.Index - j)];

                                    if (_slideShow.SelectedSlide != null)
                                    {
                                        selectedScreenshot = _screenshotCollection.GetScreenshot(_slideShow.SelectedSlide.Name, viewId);

                                        if (!string.IsNullOrEmpty(selectedScreenshot.FilePath))
                                        {
                                            // Change the font color to dark red to indicate that this is an outdated screenshot we're showing.
                                            groupBox.ForeColor = Color.DarkRed;

                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(selectedScreenshot != null ? selectedScreenshot.FilePath : string.Empty))
                        {
                            if (selectedScreenshot.Encrypted)
                            {
                                if (_fileSystem.FileExists(selectedScreenshot.FilePath))
                                {
                                    try
                                    {
                                        _security.DecryptFile(selectedScreenshot.FilePath, selectedScreenshot.FilePath + "-decrypted", selectedScreenshot.Key);
                                        pictureBox.Image = _screenCapture.GetImageByPath(selectedScreenshot.FilePath + "-decrypted");
                                    }
                                    catch (Exception ex)
                                    {
                                        // Write an error to the error file in th debug directory regardless if debug mode or logging is enabled or disabled.
                                        // We want to force write this error out to the error file. The image will remain black to indicate that an error was encountered.
                                        _log.Write($"Decryption failed for \"{selectedScreenshot.FilePath}\". Exception: {ex}", writeError: true, null);
                                    }

                                    if (_fileSystem.FileExists(selectedScreenshot.FilePath + "-decrypted"))
                                    {
                                        _fileSystem.DeleteFile(selectedScreenshot.FilePath + "-decrypted");
                                    }
                                }
                            }
                            else
                            {
                                pictureBox.Image = _screenCapture.GetImageByPath(selectedScreenshot.FilePath);
                            }
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

                ToolStripLabel toolStripLabel = (ToolStripLabel)toolStrip.Items[selectedTabPage.Name + "toolStripLabelFilename"];

                ToolStripTextBox toolStripTextBox = (ToolStripTextBox)toolStrip.Items[selectedTabPage.Name + "toolStripTextBoxFilename"];

                ToolStripSplitButton toolStripSplitButtonEdit = (ToolStripSplitButton)toolStrip.Items[selectedTabPage.Name + "toolStripSplitButtonEdit"];

                ToolStripButton toolStripButtonEncryptDecrypt = (ToolStripButton)toolStrip.Items[selectedTabPage.Name + "toolStripButtonEncryptDecrypt"];

                ToolStripItem toolstripButtonOpenFolder = (ToolStripItem)toolStrip.Items[selectedTabPage.Name + "toolstripButtonOpenFolder"];

                PictureBox pictureBox = (PictureBox)selectedTabPage.Controls[selectedTabPage.Name + "pictureBox"];

                pictureBox.Image = null;

                // Preview
                if (_preview)
                {
                    if ((selectedTabPage.Tag is Screen screen && screen.Enable) ||
                        (selectedTabPage.Tag is Region region && region.Enable))
                    {
                        pictureBox.Image = DoPreview(selectedTabPage.Tag);
                    }

                    _formScreenshotMetadata.toolStripStatusLabelScreenshotMetadata.Text = "Preview is on. There is no saved screenshot to show until Preview is turned off.";
                }
                else
                {
                    toolStrip.ForeColor = Color.Black;

                    Screenshot selectedScreenshot = new Screenshot();

                    if (_slideShow.Index >= 0 && _slideShow.Index <= (_slideShow.Count - 1))
                    {
                        _slideShow.SelectedSlide = (Slide)listBoxScreenshots.Items[_slideShow.Index];

                        Guid viewId = Guid.Empty;

                        if (selectedTabPage.Tag.GetType() == typeof(Screen))
                        {
                            Screen screen = (Screen)selectedTabPage.Tag;
                            viewId = screen.ViewId;
                        }

                        if (selectedTabPage.Tag.GetType() == typeof(Region))
                        {
                            Region region = (Region)selectedTabPage.Tag;
                            viewId = region.ViewId;
                        }

                        selectedScreenshot = _screenshotCollection.GetScreenshot(_slideShow.SelectedSlide.Name, viewId);

                        if (selectedScreenshot != null && selectedScreenshot.ViewId.Equals(Guid.Empty))
                        {
                            // *** Auto Screen Capture - Region Select / Auto Save ***
                            selectedScreenshot = _screenshotCollection.GetScreenshot(_slideShow.SelectedSlide.Name, Guid.Empty);
                        }

                        // The screenshot may not have been found because it was not in the collection due to optimization (OptimizeScreenCapture).
                        // So get the earliest screenshot of this view that has a valid path so we can still show something.
                        if (selectedScreenshot.Slide == null)
                        {
                            for (int j = 0; (_slideShow.Index - j) >= 0; j++)
                            {
                                _slideShow.SelectedSlide = (Slide)listBoxScreenshots.Items[(_slideShow.Index - j)];

                                if (_slideShow.SelectedSlide != null)
                                {
                                    selectedScreenshot = _screenshotCollection.GetScreenshot(_slideShow.SelectedSlide.Name, viewId);

                                    if (!string.IsNullOrEmpty(selectedScreenshot.FilePath))
                                    {
                                        // Change the font color to dark red to indicate that this is an outdated screenshot we're showing.
                                        toolStrip.ForeColor = Color.DarkRed;

                                        break;
                                    }
                                }
                            }
                        }
                    }

                    string path = selectedScreenshot != null ? selectedScreenshot.FilePath : string.Empty;

                    if (!string.IsNullOrEmpty(path))
                    {
                        toolStripTextBox.Text = _fileSystem.GetFileName(path);
                        toolStripTextBox.ToolTipText = path;

                        string dirName = _fileSystem.GetDirectoryName(path);

                        if (!string.IsNullOrEmpty(selectedScreenshot != null ? selectedScreenshot.FilePath : string.Empty))
                        {
                            if (selectedScreenshot.Encrypted)
                            {
                                toolStripLabel.Text = "File [encrypted]:";

                                toolStripTextBox.ForeColor = Color.White;

                                if (!string.IsNullOrEmpty(dirName))
                                {
                                    if (_fileSystem.DirectoryExists(dirName) && _fileSystem.FileExists(path))
                                    {
                                        toolStripTextBox.BackColor = Color.Black;
                                    }
                                    else
                                    {
                                        toolStripTextBox.BackColor = Color.PaleVioletRed;
                                        toolStripTextBox.ToolTipText = $"Could not find or access image file at path \"{path}\"";

                                        _formScreenshotMetadata.textBoxScreenshotPath.Text = path;
                                        _formScreenshotMetadata.toolStripStatusLabelScreenshotMetadata.Text = "This screenshot could not be found. Perhaps it was deleted or the reference is incorrect.";
                                    }
                                }

                                toolStripButtonEncryptDecrypt.Text = "Decrypt";
                                toolStripButtonEncryptDecrypt.ToolTipText = "Decrypt the screenshot";
                                toolStripButtonEncryptDecrypt.Image = Properties.Resources.unlock;

                                if (_fileSystem.FileExists(selectedScreenshot.FilePath))
                                {
                                    try
                                    {
                                        _security.DecryptFile(selectedScreenshot.FilePath, selectedScreenshot.FilePath + "-decrypted", selectedScreenshot.Key);
                                        pictureBox.Image = _screenCapture.GetImageByPath(selectedScreenshot.FilePath + "-decrypted");
                                    }
                                    catch (Exception ex)
                                    {
                                        // Write an error to the error file in the debug directory regardless if debug mode or logging is enabled or disabled.
                                        // We want to force write this error out to the error file. The image will remain black to indicate that an error was encountered.
                                        _log.Write($"Decryption failed for \"{selectedScreenshot.FilePath}\". Exception: {ex}", writeError: true, null);
                                    }

                                    if (_fileSystem.FileExists(selectedScreenshot.FilePath + "-decrypted"))
                                    {
                                        _fileSystem.DeleteFile(selectedScreenshot.FilePath + "-decrypted");
                                    }
                                }
                            }
                            else
                            {
                                toolStripLabel.Text = "File:";

                                toolStripTextBox.ForeColor = Color.Black;

                                if (!string.IsNullOrEmpty(dirName))
                                {
                                    if (_fileSystem.DirectoryExists(dirName) && _fileSystem.FileExists(path))
                                    {
                                        toolStripTextBox.BackColor = Color.PaleGreen;
                                    }
                                    else
                                    {
                                        toolStripTextBox.BackColor = Color.PaleVioletRed;
                                        toolStripTextBox.ToolTipText = $"Could not find or access image file at path \"{path}\"";

                                        _formScreenshotMetadata.textBoxScreenshotPath.Text = path;
                                        _formScreenshotMetadata.toolStripStatusLabelScreenshotMetadata.Text = "This screenshot could not be found. Perhaps it was deleted or the reference is incorrect.";
                                    }
                                }

                                toolStripButtonEncryptDecrypt.Text = "Encrypt";
                                toolStripButtonEncryptDecrypt.ToolTipText = "Encrypt the screenshot";
                                toolStripButtonEncryptDecrypt.Image = Properties.Resources._lock;

                                pictureBox.Image = _screenCapture.GetImageByPath(selectedScreenshot.FilePath);
                            }
                        }

                        if (pictureBox.Image != null)
                        {
                            toolStripSplitButtonEdit.Enabled = true;
                            toolStripButtonEncryptDecrypt.Enabled = true;

                            // We can't encrypt/decrypt screenshots from Auto Save so disable the button.
                            if (selectedScreenshot.WindowTitle.Equals("*** Auto Screen Capture - Region Select / Auto Save ***"))
                            {
                                toolStripButtonEncryptDecrypt.Enabled = false;
                            }

                            _formScreenshotMetadata.textBoxScreenshotLabel.Text = selectedScreenshot.Label;
                            _formScreenshotMetadata.textBoxScreenshotTitle.Text = selectedScreenshot.WindowTitle;
                            _formScreenshotMetadata.textBoxScreenshotFormat.Text = selectedScreenshot.Format.Name;

                            _formScreenshotMetadata.textBoxScreenshotWidth.Text = pictureBox.Image.Width.ToString();
                            _formScreenshotMetadata.textBoxScreenshotHeight.Text = pictureBox.Image.Height.ToString();

                            _formScreenshotMetadata.textBoxScreenshotDate.Text = selectedScreenshot.Date;
                            _formScreenshotMetadata.textBoxScreenshotTime.Text = selectedScreenshot.Time;

                            _formScreenshotMetadata.textBoxScreenshotPath.Text = selectedScreenshot.FilePath;

                            _formScreenshotMetadata.textBoxScreenshotHash.Text = selectedScreenshot.Hash;

                            _formScreenshotMetadata.textBoxScreenshotProcessName.Text = selectedScreenshot.ProcessName;

                            if (selectedScreenshot.Encrypted)
                            {
                                _formScreenshotMetadata.textBoxScreenshotKey.Text = selectedScreenshot.Key;
                                _formScreenshotMetadata.toolStripStatusLabelScreenshotMetadata.Text = "This screenshot is encrypted. It will be difficult for other applications to use it.";
                            }
                            else
                            {
                                _formScreenshotMetadata.toolStripStatusLabelScreenshotMetadata.Text = "This screenshot is not encrypted. You can use this screenshot in other applications.";
                            }
                        }

                        toolstripButtonOpenFolder.Enabled = true;
                    }
                    else
                    {
                        toolStripSplitButtonEdit.Enabled = false;
                        toolStripButtonEncryptDecrypt.Enabled = false;

                        toolStripTextBox.Text = string.Empty;
                        toolStripTextBox.BackColor = Color.LightYellow;
                        toolStripTextBox.ToolTipText = string.Empty;
                        toolstripButtonOpenFolder.Enabled = false;
                        
                        _formScreenshotMetadata.toolStripStatusLabelScreenshotMetadata.Text = "A screenshot could not be found. Perhaps a screenshot has yet to be captured for this component.";
                    }
                }
            }
        }

        /// <summary>
        /// Clears all the read-only text fields and status strip text in Screenshot Metadata.
        /// </summary>
        private void ClearScreenshotMetadataFields()
        {
            _formScreenshotMetadata.textBoxScreenshotLabel.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotTitle.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotFormat.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotWidth.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotHeight.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotDate.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotTime.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotKey.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotPath.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotProcessName.Text = string.Empty;
            _formScreenshotMetadata.textBoxScreenshotHash.Text = string.Empty;

            _formScreenshotMetadata.toolStripStatusLabelScreenshotMetadata.Text = string.Empty;
        }

        /// <summary>
        /// Shows the list of screenshots.
        /// </summary>
        private void ShowScreenshots()
        {
            SearchScreenshots();

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
                    selectedScreenshot = _screenshotCollection.GetScreenshot(_slideShow.SelectedSlide.Name, screen.ViewId);
                }

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
                {
                    Region region = (Region)tabControlViews.SelectedTab.Tag;
                    selectedScreenshot = _screenshotCollection.GetScreenshot(_slideShow.SelectedSlide.Name, region.ViewId);
                }

                if (selectedScreenshot.ViewId.Equals(Guid.Empty))
                {
                    // *** Auto Screen Capture - Region Select / Auto Save ***
                    selectedScreenshot = _screenshotCollection.GetScreenshot(_slideShow.SelectedSlide.Name, Guid.Empty);
                }

                if (selectedScreenshot != null && !string.IsNullOrEmpty(selectedScreenshot.FilePath) &&
                    _fileSystem.FileExists(selectedScreenshot.FilePath))
                {
                    Process.Start(_fileSystem.FileManager, "/select,\"" + selectedScreenshot.FilePath + "\"");
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
            showScreenshotLocationToolStripItem.Click += new EventHandler(showScreenshotLocation_Click);

            contextMenuStripScreenshot.Items.Add(showScreenshotLocationToolStripItem);
            contextMenuStripScreenshot.Items.Add(new ToolStripSeparator());

            foreach (Editor editor in _formEditor.EditorCollection)
            {
                if (editor != null && _fileSystem.FileExists(editor.Application))
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
                _log.WriteDebugMessage("Emailing screenshots");

                if (screenshot == null || string.IsNullOrEmpty(screenshot.FilePath))
                {
                    _log.WriteDebugMessage("Cannot email screenshot because screenshot is either null or path is empty");

                    return false;
                }

                _log.WriteDebugMessage("Attempting to email screenshot \"" + screenshot.FilePath + "\"");

                string host = _config.Settings.SMTP.GetByKey("EmailServerHost", _config.Settings.DefaultSettings.EmailServerHost).Value.ToString();

                _log.WriteDebugMessage("Host = " + host);

                int.TryParse(_config.Settings.SMTP.GetByKey("EmailServerPort", _config.Settings.DefaultSettings.EmailServerPort).Value.ToString(), out int port);

                _log.WriteDebugMessage("Port = " + port);

                bool.TryParse(_config.Settings.SMTP.GetByKey("EmailServerEnableSSL", _config.Settings.DefaultSettings.EmailServerEnableSSL).Value.ToString(), out bool ssl);

                _log.WriteDebugMessage("SSL = " + ssl);

                _log.WriteDebugMessage("Prompt = " + prompt);

                string username = _config.Settings.SMTP.GetByKey("EmailClientUsername", _config.Settings.DefaultSettings.EmailClientUsername).Value.ToString();

                _log.WriteDebugMessage("Username = " + username);

                string password = _config.Settings.SMTP.GetByKey("EmailClientPassword", _config.Settings.DefaultSettings.EmailClientPassword).Value.ToString();

                if (string.IsNullOrEmpty(password))
                {
                    _log.WriteDebugMessage("Password = [empty]");
                }
                else
                {
                    _log.WriteDebugMessage("Password = [I'm not revealing this here]");
                }

                string from = _config.Settings.SMTP.GetByKey("EmailMessageFrom", _config.Settings.DefaultSettings.EmailMessageFrom).Value.ToString();

                _log.WriteDebugMessage("From = " + from);

                string to = _config.Settings.SMTP.GetByKey("EmailMessageTo", _config.Settings.DefaultSettings.EmailMessageTo).Value.ToString();

                _log.WriteDebugMessage("To = " + to);

                string cc = _config.Settings.SMTP.GetByKey("EmailMessageCC", _config.Settings.DefaultSettings.EmailMessageCC).Value.ToString();

                _log.WriteDebugMessage("CC = " + cc);

                string bcc = _config.Settings.SMTP.GetByKey("EmailMessageBCC", _config.Settings.DefaultSettings.EmailMessageBCC).Value.ToString();

                _log.WriteDebugMessage("BCC = " + bcc);

                string subject = _config.Settings.SMTP.GetByKey("EmailMessageSubject", _config.Settings.DefaultSettings.EmailMessageSubject).Value.ToString();

                _log.WriteDebugMessage("Subject = " + subject);

                string body = _config.Settings.SMTP.GetByKey("EmailMessageBody", _config.Settings.DefaultSettings.EmailMessageBody).Value.ToString();

                _log.WriteDebugMessage("Body = " + body);

                if (string.IsNullOrEmpty(host) ||
                    string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(@from) ||
                    string.IsNullOrEmpty(to))
                {
                    _log.WriteDebugMessage("Host, Username, Password, From, or To is empty");

                    return false;
                }

                _emailManager.ComposeEmailMessage(@from,
                    to,
                    cc,
                    bcc,
                    subject,
                    body,
                    isBodyHtml: false,
                    ssl,
                    host,
                    port,
                    username,
                    password);

                _log.WriteDebugMessage("SMTP client prepared and email message composed");

                _emailManager.AddFileAttachmentByFilePath(screenshot.FilePath);

                if (_emailManager.AttachmentsCount == 0)
                {
                    return false;
                }

                _log.WriteDebugMessage("Added screenshot as attachment");

                if (prompt)
                {
                    DialogResult dialogResult = MessageBox.Show(_emailManager.AskQuestionAboutSendingEmailMessage(), "Email Screenshot", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        _log.WriteDebugMessage("Sending email with prompt confirmation");

                        if (_emailManager.SendEmailMessage())
                        {
                            _log.WriteDebugMessage("Email sent");
                        }
                        else
                        {
                            _log.WriteDebugMessage("Email not sent because an error was encountered");
                        }
                    }
                    else
                    {
                        _emailManager.DisposeSmtpClient();

                        return false;
                    }
                }
                else
                {
                    _log.WriteDebugMessage("Sending email without prompt confirmation");

                    if (_emailManager.SendEmailMessage())
                    {
                        _log.WriteDebugMessage("Email sent");
                    }
                    else
                    {
                        _log.WriteDebugMessage("Email not sent because an error was encountered");
                    }
                }

                _emailManager.DisposeSmtpClient();

                _log.WriteDebugMessage("SMTP client disposed");

                return true;
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;

                _log.WriteExceptionMessage("FormMain-Screenshots::EmailScreenshot", ex);

                return false;
            }
        }

        private void EmailScreenshot(TriggerActionType triggerActionType)
        {
            if (triggerActionType == TriggerActionType.EmailScreenshot)
            {
                Screenshot lastScreenshotOfThisView = _screenshotCollection.GetLastScreenshotOfView(_screenshotCollection.LastViewId);

                if (lastScreenshotOfThisView != null && lastScreenshotOfThisView.Slide != null && !string.IsNullOrEmpty(lastScreenshotOfThisView.FilePath))
                {
                    if (_screenCapture.OptimizeScreenCapture)
                    {
                        if (!string.IsNullOrEmpty(lastScreenshotOfThisView.Hash) && !_screenshotCollection.EmailedScreenshotHashList.Contains(lastScreenshotOfThisView.Hash))
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
                _log.WriteDebugMessage("Screenshot attempting to transfer to file server");

                if (screenshot == null || string.IsNullOrEmpty(screenshot.FilePath))
                {
                    _log.WriteDebugMessage("Cannot upload screenshot to file server because screenshot is either null or path is empty");

                    return false;
                }

                _log.WriteDebugMessage("Attempting to upload screenshot \"" + screenshot.FilePath + "\" to file server");

                string host = _config.Settings.SFTP.GetByKey("FileTransferServerHost", _config.Settings.DefaultSettings.FileTransferServerHost).Value.ToString();

                _log.WriteDebugMessage("Host = " + host);

                int.TryParse(_config.Settings.SFTP.GetByKey("FileTransferServerPort", _config.Settings.DefaultSettings.FileTransferServerPort).Value.ToString(), out int port);

                _log.WriteDebugMessage("Port = " + port);

                string username = _config.Settings.SFTP.GetByKey("FileTransferClientUsername", _config.Settings.DefaultSettings.FileTransferClientUsername).Value.ToString();

                _log.WriteDebugMessage("Username = " + username);

                string password = _config.Settings.SFTP.GetByKey("FileTransferClientPassword", _config.Settings.DefaultSettings.FileTransferClientPassword).Value.ToString();

                if (string.IsNullOrEmpty(password))
                {
                    _log.WriteDebugMessage("Password = [empty]");
                }
                else
                {
                    _log.WriteDebugMessage("Password = [I'm not revealing this here]");
                }

                if (string.IsNullOrEmpty(host) ||
                    string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(password))
                {
                    _log.WriteDebugMessage("Host, Username, or Password is empty");

                    return false;
                }

                if (_sftpClient == null)
                {
                    _sftpClient = new Gavin.Kendall.SFTP.SftpClient(host, port, username, password);
                }

                _log.WriteDebugMessage("Attempting to connect to file server");

                if (!_sftpClient.IsConnected)
                {
                    if (_sftpClient.Connect())
                    {
                        _log.WriteDebugMessage("Connection to file server established");
                    }
                    else
                    {
                        _log.WriteDebugMessage("Could not establish a connection with the file server");

                        if (Convert.ToBoolean(_config.Settings.User.GetByKey("SFTPKeepFailedUploads", _config.Settings.DefaultSettings.SFTPKeepFailedUploads).Value))
                        {
                            // Add the screenshot filepath and host to a dictionary of failed uploads so we'll attempt to upload the screenshot for that host later.
                            if (!_failedUploads.ContainsKey(screenshot.FilePath))
                            {
                                _log.WriteDebugMessage($"Screenshot ({screenshot.FilePath}) has been added to the dictionary of failed uploads because a connection to the file server could not be established at this time so an attempt to retry the upload will occur in the next capture cycle");

                                _failedUploads.Add(screenshot.FilePath, host);
                            }
                        }

                        return false;
                    }
                }
                
                // Make sure we are connected to the file server. If we were not connected earlier then a connection request would have been sent prior to this check.
                if (_sftpClient.IsConnected)
                {
                    // This is necessary since we are going to be removing entries from the _failedUploads dictionary while enumerating the collection
                    // so instead of modifying the collection that we're enumerating it's better to simply enumerate over a copy of the filepaths.
                    string[] pathsOfFailedUploads = new string[_failedUploads.Count];
                    _failedUploads.Keys.CopyTo(pathsOfFailedUploads, 0);

                    foreach (string path in pathsOfFailedUploads)
                    {
                        if (_failedUploads.ContainsKey(path))
                        {
                            if (_failedUploads.TryGetValue(path, out string hostAssociatedWithPath))
                            {
                                _log.WriteDebugMessage($"Screenshot ({path}) was found in dictionary of failed uploads");

                                // Make sure that the host associated with the path is equal to the host we're currently handling
                                // so we don't accidentally upload the screenshot to the wrong SFTP server just in case the user changes hosts.
                                if (hostAssociatedWithPath.Equals(host))
                                {
                                    if (UploadScreenshot(host, path, path))
                                    {
                                        _log.WriteDebugMessage($"This screenshot previously failed to upload. It has now been successfully uploaded to {host}");
                                    }
                                }
                                else
                                {
                                    _log.WriteDebugMessage($"This screenshot previously failed to upload. It could not be uploaded to {host} at the moment because the screenshot is intended for {hostAssociatedWithPath}");
                                }
                            }
                        }
                    }

                    // Upload the screenshot we're currently handling.
                    return UploadScreenshot(host, screenshot.FilePath, screenshot.MacroPath);
                }

                return false;
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;

                _log.WriteExceptionMessage("FormMain-Screenshots::FileTransferScreenshot", ex);

                // We can't simply say that the SFTP client is now disconnected so we just need to nullify the SFTP client.
                _sftpClient = null;

                return false;
            }
        }

        /// <summary>
        /// Uploads a screenshot image file to the SFTP server given the provided local filepath and macro path of the screenshot.
        /// </summary>
        /// <param name="host">The hostname for the SFTP server.</param>
        /// <param name="path">The local filepath of the screenshot.</param>
        /// <param name="macroPath">The screenshot's macro path (filename pattern) which will be used for the remote directory on the SFTP server.</param>
        /// <returns>True if the upload was successful. False if the upload failed.</returns>
        private bool UploadScreenshot(string host, string path, string macroPath)
        {
            string destinationFolder = Path.GetDirectoryName(macroPath);
            string destinationFilename = Path.GetFileName(macroPath);

            bool isLinuxServer = Convert.ToBoolean(_config.Settings.SFTP.GetByKey("FileTransferIsLinuxServer", _config.Settings.DefaultSettings.FileTransferIsLinuxServer).Value);

            _log.WriteDebugMessage("Attempting to upload screenshot to file server");
            _log.WriteDebugMessage("Source (Local) Path: " + path);
            _log.WriteDebugMessage("Destination (Remote) Folder Path: " + destinationFolder);
            _log.WriteDebugMessage("Destination (Remote) Filename: " + destinationFilename);
            _log.WriteDebugMessage("Is Linux Server: " + isLinuxServer.ToString());

            if (_sftpClient.UploadFile(path, destinationFolder, destinationFilename, isLinuxServer))
            {
                _log.WriteDebugMessage("Successfully uploaded screenshot");

                if (Convert.ToBoolean(_config.Settings.User.GetByKey("SFTPDeleteLocalFileAfterSuccessfulUpload", _config.Settings.DefaultSettings.SFTPDeleteLocalFileAfterSuccessfulUpload).Value))
                {
                    if (_fileSystem.FileExists(path))
                    {
                        _fileSystem.DeleteFile(path);

                        _log.WriteDebugMessage($"Screenshot ({path}) deleted locally because of successful upload to file server");
                    }
                }

                // Remove the path from the dictionary of failed uploads if we were successful in uploading the file to the SFTP server.
                if (_failedUploads.ContainsKey(path))
                {
                    _log.WriteDebugMessage($"Screenshot ({path}) has been removed from the dictionary of failed uploads because the upload was successful");

                    _failedUploads.Remove(path);
                }
            }
            else
            {
                _log.WriteDebugMessage("Failed to upload screenshot");

                if (Convert.ToBoolean(_config.Settings.User.GetByKey("SFTPKeepFailedUploads", _config.Settings.DefaultSettings.SFTPKeepFailedUploads).Value))
                {
                    // Add the screenshot filepath and host to a dictionary of failed uploads so we'll attempt to upload the screenshot later.
                    if (!_failedUploads.ContainsKey(path))
                    {
                        _log.WriteDebugMessage($"Screenshot ({path}) has been added to the dictionary of failed uploads because the upload failed at this time so an attempt to retry the upload will occur in the next capture cycle");

                        _failedUploads.Add(path, host);
                    }
                }

                // We can't simply say that the SFTP client is now disconnected so we just need to nullify the SFTP client.
                _sftpClient = null;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Uploads a screenshot to an SFTP server based on a trigger action type.
        /// </summary>
        /// <param name="triggerActionType">The trigger action type to use.</param>
        private void FileTransferScreenshot(TriggerActionType triggerActionType)
        {
            if (triggerActionType == TriggerActionType.FileTransferScreenshot && _screenCapture.Running)
            {
                Screenshot lastScreenshotOfThisView = _screenshotCollection.GetLastScreenshotOfView(_screenshotCollection.LastViewId);

                if (lastScreenshotOfThisView != null && lastScreenshotOfThisView.Slide != null && !string.IsNullOrEmpty(lastScreenshotOfThisView.FilePath))
                {
                    if (FileTransferScreenshot(lastScreenshotOfThisView))
                    {
                        _log.WriteDebugMessage($"The screenshot ({lastScreenshotOfThisView.FilePath}) was successfully uploaded");
                    }
                    else
                    {
                        _log.WriteDebugMessage($"The screenshot ({lastScreenshotOfThisView.FilePath}) failed to upload");
                    }
                }
            }
        }

        private void tabControlViews_Selected(object sender, TabControlEventArgs e)
        {
            ClearScreenshotMetadataFields();

            int screenTabCount = _formScreen.ScreenCollection.Count;
            int regionTabCount = _formRegion.RegionCollection.Count;

            if (tabControlViews.TabCount == (screenTabCount + regionTabCount + 1))
            {
                _config.Settings.User.SetValueByKey("SelectedTabPageIndex", tabControlViews.SelectedIndex);

                ShowScreenshotBySlideIndex();
            }
        }

        private void buttonRefreshFilterValues_Click(object sender, EventArgs e)
        {
            SearchFilterValues();
            SearchDates();
            ShowScreenshots();
        }

        /// <summary>
        /// Returns the appropriate bitmap image based on the given object.
        /// </summary>
        /// <param name="tag">This sould be the tag from a TabPage, Screen, or Region.</param>
        /// <returns>A bitmap image representing a preview of the given object.</returns>
        private Bitmap DoPreview(object tag)
        {
            Bitmap returnedBitmap = null;

            if (tag.GetType() == typeof(Screen))
            {
                Screen screen = (Screen)tag;

                AutoAdapt(screen, out int x, out int y, out int width, out int height);

                // Active Window
                if (screen.Source == 0 && screen.Component == 0 && !screen.AutoAdapt)
                {
                    returnedBitmap = _screenCapture.GetActiveWindowBitmap(screen.ResolutionRatio, screen.Mouse);
                }
                else
                {
                    // Screen
                    returnedBitmap = _screenCapture.GetScreenBitmap(screen.Source, screen.Component, screen.CaptureMethod, x, y, width, height, screen.ResolutionRatio, screen.Mouse);
                }
            }

            if (tag.GetType() == typeof(Region))
            {
                Region region = (Region)tag;

                returnedBitmap = _screenCapture.GetScreenBitmap(source: -1, component: -1, captureMethod: 1, region.X, region.Y, region.Width, region.Height, resolutionRatio: 100, region.Mouse);
            }

            return returnedBitmap;
        }

        /// <summary>
        /// When the "Encryptor / Decryptor" tool has finished encrypting screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScreenshotsEncrypted(object sender, EventArgs e)
        {
            ShowScreenshotBySlideIndex();
        }

        /// <summary>
        /// When the "Encryptor / Decryptor" tool has finished decrypting screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScreenshotsDecrypted(object sender, EventArgs e)
        {
            ShowScreenshotBySlideIndex();
        }
    }
}