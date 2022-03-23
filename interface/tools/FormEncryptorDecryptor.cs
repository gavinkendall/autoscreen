//-----------------------------------------------------------------------
// <copyright file="FormEncryptorDecryptor.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The Encryptor / Decryptor tool for encrypting/decrypting screenshots, files, and text.</summary>
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
using System.Windows.Forms;
using System.Xml;

namespace AutoScreenCapture
{
    /// <summary>
    /// The Encryptor / Decryptor tool for encrypting/decrypting screenshots, files, and text.
    /// </summary>
    public partial class FormEncryptorDecryptor : Form
    {
        private readonly Log _log;
        private readonly Security _security;
        private readonly FileSystem _fileSystem;
        private readonly ScreenshotCollection _screenshotCollection;

        private readonly BackgroundWorker runScreenshotLoad = null;
        private readonly BackgroundWorker runScreenshotsEncryption = null;
        private readonly BackgroundWorker runScreenshotsDecryption = null;

        private delegate void RunScreenshotLoadDelegate();
        private delegate void RunScreenshotsEncryptionDelegate();
        private delegate void RunScreenshotsDecryptionDelegate();

        private int _totalNodeLoadCount;
        private readonly int _screenshotsLoadLimit;

        /// <summary>
        /// When the "Encryptor / Decryptor" tool has finished encrypting screenshots.
        /// </summary>
        public EventHandler screenshotsEncrypted;

        /// <summary>
        /// When the "Encryptor / Decryptor" tool has finished decrypting screenshots.
        /// </summary>
        public EventHandler screenshotsDecrypted;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="log">The logging class.</param>
        /// <param name="config">Configuration.</param>
        /// <param name="security">Security.</param>
        /// <param name="fileSystem">File system.</param>
        /// <param name="screenshotCollection">Screenshot collection.</param>
        public FormEncryptorDecryptor(Log log, Config config, Security security, FileSystem fileSystem, ScreenshotCollection screenshotCollection)
        {
            InitializeComponent();

            _screenshotsLoadLimit = Convert.ToInt32(config.Settings.Application.GetByKey("ScreenshotsLoadLimit", config.Settings.DefaultSettings.ScreenshotsLoadLimit).Value);

            _log = log;
            _security = security;
            _fileSystem = fileSystem;
            _screenshotCollection = screenshotCollection;

            runScreenshotLoad = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };

            runScreenshotLoad.DoWork += new DoWorkEventHandler(DoWork_runScreenshotLoad);
            runScreenshotLoad.RunWorkerCompleted += RunScreenshotLoad_RunWorkerCompleted;

            runScreenshotsEncryption = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };

            runScreenshotsEncryption.DoWork += new DoWorkEventHandler(DoWork_runScreenshotsEncryption);
            runScreenshotsEncryption.RunWorkerCompleted += RunScreenshotsEncryption_RunWorkerCompleted;

            runScreenshotsDecryption = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };

            runScreenshotsDecryption.DoWork += new DoWorkEventHandler(DoWork_runScreenshotsDecryption);
            runScreenshotsDecryption.RunWorkerCompleted += RunScreenshotsDecryption_RunWorkerCompleted;
        }

        private void FormEncryptorDecryptor_Load(object sender, EventArgs e)
        {
            XmlNode minDateNode = _screenshotCollection.GetMinDateFromXMLDocument();

            if (minDateNode != null)
            {
                dateTimePickerScreenshotsStartDateRange.Value = DateTime.Parse(minDateNode.FirstChild.Value).Date;
            }
            else
            {
                dateTimePickerScreenshotsStartDateRange.Value = DateTime.Now;
            }

            dateTimePickerScreenshotsEndDateRange.Value = DateTime.Now;

            dateTimePickerScreenshotsStartTimeRange.Value = DateTime.Parse("00:00:00.000");
            dateTimePickerScreenshotsEndTimeRange.Value = DateTime.Parse("23:59:59.999");

            comboBoxFilterValue.SelectedIndexChanged += new EventHandler(control_ValueChanged);

            dateTimePickerScreenshotsStartDateRange.ValueChanged += new EventHandler(control_ValueChanged);
            dateTimePickerScreenshotsEndDateRange.ValueChanged += new EventHandler(control_ValueChanged);

            dateTimePickerScreenshotsStartTimeRange.ValueChanged += new EventHandler(control_ValueChanged);
            dateTimePickerScreenshotsEndTimeRange.ValueChanged += new EventHandler(control_ValueChanged);

            LoadScreenshots();
        }

        private void FormEncryptorDecryptor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        /// <summary>
        /// Runs the screenshot loading thread to load screenshots into the data grid view.
        /// </summary>
        private void LoadScreenshots()
        {
            _totalNodeLoadCount = 0;

            toolStripStatusLabel.Text = "Loading screenshots between " + dateTimePickerScreenshotsStartDateRange.Value.Date.ToString("yyyy-MM-dd") + " and " + dateTimePickerScreenshotsEndDateRange.Value.Date.ToString("yyyy-MM-dd");

            if (!runScreenshotLoad.IsBusy)
            {
                runScreenshotLoad.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Encrypts the screenshots in the specified date range.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEncryptScreenshots_Click(object sender, EventArgs e)
        {
            if (dataGridViewScreenshots.Rows.Count == 0)
            {
                return;
            }

            if (!runScreenshotsEncryption.IsBusy)
            {
                buttonEncryptScreenshots.Enabled = false;

                toolStripStatusLabel.Text = "Encrypting screenshots between " + dateTimePickerScreenshotsStartDateRange.Value.Date.ToString("yyyy-MM-dd") + " and " + dateTimePickerScreenshotsEndDateRange.Value.Date.ToString("yyyy-MM-dd");

                runScreenshotsEncryption.RunWorkerAsync(e);
            }
        }

        /// <summary>
        /// Decrypts the screenshots in the specified date range.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDecryptScreenshots_Click(object sender, EventArgs e)
        {
            if (dataGridViewScreenshots.Rows.Count == 0)
            {
                return;
            }

            if (!runScreenshotsDecryption.IsBusy)
            {
                buttonDecryptScreenshots.Enabled = false;

                toolStripStatusLabel.Text = "Decrypting screenshots between " + dateTimePickerScreenshotsStartDateRange.Value.Date.ToString("yyyy-MM-dd") + " and " + dateTimePickerScreenshotsEndDateRange.Value.Date.ToString("yyyy-MM-dd");

                runScreenshotsDecryption.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Gets screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_runScreenshotLoad(object sender, DoWorkEventArgs e)
        {
            RunScreenshotLoad();
        }

        /// <summary>
        /// Encrypts the screenshots in the specified date range.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_runScreenshotsEncryption(object sender, DoWorkEventArgs e)
        {
            RunScreenshotsEncryption();
        }

        /// <summary>
        /// Decrypts the screenshots in the specified date range.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork_runScreenshotsDecryption(object sender, DoWorkEventArgs e)
        {
            RunScreenshotsDecryption();
        }

        /// <summary>
        /// Gets screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunScreenshotLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnScreenshotLoadCompleted(sender, e);
        }

        /// <summary>
        /// Encrypts the screenshots in the specified date range.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunScreenshotsEncryption_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnScreenshotsEncrypted(sender, e);
        }

        /// <summary>
        /// Decrypts the screenshots in the specified date range.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunScreenshotsDecryption_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnScreenshotsDecrypted(sender ,e);
        }

        /// <summary>
        /// The method executed by the thread that loads screenshots into the data grid view.
        /// </summary>
        private void RunScreenshotLoad()
        {
            if (dataGridViewScreenshots.InvokeRequired)
            {
                dataGridViewScreenshots.Invoke(new RunScreenshotLoadDelegate(RunScreenshotLoad));
            }
            else
            {
                foreach (string dateStr in _screenshotCollection.GetDatesByFilter(comboBoxFilterType.Text, comboBoxFilterValue.Text))
                {
                    if (DateTime.Parse(dateStr) >= dateTimePickerScreenshotsStartDateRange.Value &&
                        DateTime.Parse(dateStr) <= dateTimePickerScreenshotsEndDateRange.Value)
                    {
                        _screenshotCollection.LoadXmlFileAndAddScreenshots(dateStr, _screenshotsLoadLimit, out int nodeLoadCount, out int errorCode);

                        _totalNodeLoadCount += nodeLoadCount;

                        if (_totalNodeLoadCount >= _screenshotsLoadLimit)
                        {
                            return;
                        }

                        if (errorCode == 2)
                        {
                            return;
                        }
                    }
                }

                if (_screenshotCollection.Count == 0)
                {
                    return;
                }

                dataGridViewScreenshots.DataSource = _screenshotCollection.GetScreenshots(dateTimePickerScreenshotsStartDateRange.Value,
                        dateTimePickerScreenshotsEndDateRange.Value,
                        dateTimePickerScreenshotsStartTimeRange.Value,
                        dateTimePickerScreenshotsEndTimeRange.Value,
                        comboBoxFilterType.Text, comboBoxFilterValue.Text);

                for (int i = 0; i < dataGridViewScreenshots.Columns.Count; i++)
                {
                    dataGridViewScreenshots.Columns[i].Visible = false;
                }

                dataGridViewScreenshots.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewScreenshots.Columns["Time"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewScreenshots.Columns["Path"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewScreenshots.Columns["Encrypted"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dataGridViewScreenshots.Columns["Key"].Visible = true;
                dataGridViewScreenshots.Columns["Date"].Visible = true;
                dataGridViewScreenshots.Columns["Time"].Visible = true;
                dataGridViewScreenshots.Columns["Path"].Visible = true;
                dataGridViewScreenshots.Columns["Label"].Visible = true;
                dataGridViewScreenshots.Columns["Encrypted"].Visible = true;
                dataGridViewScreenshots.Columns["WindowTitle"].Visible = true;
                dataGridViewScreenshots.Columns["ProcessName"].Visible = true;

                dataGridViewScreenshots.Columns["Date"].DisplayIndex = 0;
                dataGridViewScreenshots.Columns["Time"].DisplayIndex = 1;
                dataGridViewScreenshots.Columns["Encrypted"].DisplayIndex = 2;
                dataGridViewScreenshots.Columns["Key"].DisplayIndex = 3;
                dataGridViewScreenshots.Columns["Path"].DisplayIndex = 4;
                dataGridViewScreenshots.Columns["ProcessName"].DisplayIndex = 5;
                dataGridViewScreenshots.Columns["WindowTitle"].DisplayIndex = 6;
                dataGridViewScreenshots.Columns["Label"].DisplayIndex = 8; // This needs to be 8 so it appears after "Window Title". I tried 7 but it's doesn't work. I'm not sure why.

                dataGridViewScreenshots.Columns["ProcessName"].HeaderText = "Process Name";
                dataGridViewScreenshots.Columns["WindowTitle"].HeaderText = "Window Title";
            }
        }

        /// <summary>
        /// Encrypts the screenshots in the specified date range.
        /// </summary>
        private void RunScreenshotsEncryption()
        {
            if (comboBoxFilterType.InvokeRequired || comboBoxFilterValue.InvokeRequired)
            {
                if (comboBoxFilterType.InvokeRequired)
                {
                    comboBoxFilterType.Invoke(new RunScreenshotsEncryptionDelegate(RunScreenshotsEncryption));
                }

                if (comboBoxFilterValue.InvokeRequired)
                {
                    comboBoxFilterValue.Invoke(new RunScreenshotsEncryptionDelegate(RunScreenshotsEncryption));
                }
            }
            else
            {
                foreach (Screenshot screenshot in _screenshotCollection.GetScreenshots(dateTimePickerScreenshotsStartDateRange.Value,
                    dateTimePickerScreenshotsEndDateRange.Value,
                    dateTimePickerScreenshotsStartTimeRange.Value,
                    dateTimePickerScreenshotsEndTimeRange.Value,
                    comboBoxFilterType.Text,
                    comboBoxFilterValue.Text))
                {
                    if (!screenshot.Encrypted)
                    {
                        string key = _security.EncryptFile(screenshot.Path, screenshot.Path + "-encrypted");

                        if (!string.IsNullOrEmpty(key))
                        {
                            if (_fileSystem.FileExists(screenshot.Path))
                            {
                                if (_fileSystem.DeleteFile(screenshot.Path))
                                {
                                    _fileSystem.MoveFile(screenshot.Path + "-encrypted", screenshot.Path);

                                    screenshot.Key = key;
                                    screenshot.Encrypt = false;
                                    screenshot.Encrypted = true;

                                    UpdateScreenshotCollection(screenshot);
                                }
                            }
                        }
                        else
                        {
                            _log.WriteMessage("WARNING: Error with file encryption for \"" + screenshot.Path + "\"");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts the screenshots in the specified date range.
        /// </summary>
        private void RunScreenshotsDecryption()
        {
            if (comboBoxFilterType.InvokeRequired || comboBoxFilterValue.InvokeRequired)
            {
                if (comboBoxFilterType.InvokeRequired)
                {
                    comboBoxFilterType.Invoke(new RunScreenshotsDecryptionDelegate(RunScreenshotsDecryption));
                }

                if (comboBoxFilterValue.InvokeRequired)
                {
                    comboBoxFilterValue.Invoke(new RunScreenshotsDecryptionDelegate(RunScreenshotsDecryption));
                }
            }
            else
            {
                foreach (Screenshot screenshot in _screenshotCollection.GetScreenshots(dateTimePickerScreenshotsStartDateRange.Value,
                    dateTimePickerScreenshotsEndDateRange.Value,
                    dateTimePickerScreenshotsStartTimeRange.Value,
                    dateTimePickerScreenshotsEndTimeRange.Value,
                    comboBoxFilterType.Text,
                    comboBoxFilterValue.Text))
                {
                    if (screenshot.Encrypted)
                    {
                        try
                        {
                            _security.DecryptFile(screenshot.Path, screenshot.Path + "-decrypted", screenshot.Key);
                        }
                        catch (Exception ex)
                        {
                            _log.WriteMessage("WARNING: Error with file decryption for \"" + screenshot.Path + "\". Exception is " + ex);
                        }

                        if (_fileSystem.FileExists(screenshot.Path))
                        {
                            if (_fileSystem.DeleteFile(screenshot.Path))
                            {
                                _fileSystem.MoveFile(screenshot.Path + "-decrypted", screenshot.Path);

                                screenshot.Key = string.Empty;
                                screenshot.Encrypted = false;

                                UpdateScreenshotCollection(screenshot);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the screenshot collection with the given changed screenshot.
        /// </summary>
        /// <param name="screenshot">The changed screenshot to be given to the collection</param>
        private void UpdateScreenshotCollection(Screenshot screenshot)
        {
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
        }

        /// <summary>
        /// What to do when screenshots have been loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnScreenshotLoadCompleted(object sender, EventArgs e)
        {
            if (_totalNodeLoadCount >= _screenshotsLoadLimit)
            {
                dataGridViewScreenshots.DataSource = null;

                toolStripStatusLabel.Text = "Total number of screenshots to be loaded exceeds configured maximum number allowed in load. Please filter appropriately or reduce date range";
            }
            else
            {
                toolStripStatusLabel.Text = dataGridViewScreenshots.Rows.Count + " screenshots loaded";
            }
        }

        /// <summary>
        /// What to do when screenshots have been encrypted.
        /// </summary>
        protected void OnScreenshotsEncrypted(object sender, EventArgs e)
        {
            buttonEncryptScreenshots.Enabled = true;

            toolStripStatusLabel.Text = "Screenshots encrypted";

            dataGridViewScreenshots.Refresh();

            screenshotsEncrypted?.Invoke(sender, e);
        }

        /// <summary>
        /// What to do when screenshots have been decrypted.
        /// </summary>
        protected void OnScreenshotsDecrypted(object sender, EventArgs e)
        {
            buttonDecryptScreenshots.Enabled = true;

            toolStripStatusLabel.Text = "Screenshots decrypted";

            dataGridViewScreenshots.Refresh();

            screenshotsDecrypted?.Invoke(sender, e);
        }

        private void control_ValueChanged(object sender, EventArgs e)
        {
            LoadScreenshots();
        }

        private void comboBoxFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxFilterType.Text))
            {
                if (comboBoxFilterType.SelectedItem != null && !string.IsNullOrEmpty(comboBoxFilterType.Text))
                {
                    List<string> filterValueList = _screenshotCollection.GetFilterValueList(comboBoxFilterType.Text);
                    filterValueList.Sort();

                    comboBoxFilterValue.DataSource = filterValueList;

                    comboBoxFilterValue.Enabled = true;
                }
            }
            else
            {
                comboBoxFilterValue.DataSource = null;

                comboBoxFilterValue.Enabled = false;
            }

            LoadScreenshots();
        }

        private void buttonEncryptFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFilepath.Text))
            {
                MessageBox.Show("No filepath provided. Please provide the filepath to a file in the Filepath field.", "Empty Filepath", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (_fileSystem.FileExists(textBoxFilepath.Text))
            {
                textBoxFileKey.Text = _security.EncryptFile(textBoxFilepath.Text, textBoxFilepath.Text + "-encrypted");

                if (_fileSystem.FileExists(textBoxFilepath.Text))
                {
                    if (_fileSystem.DeleteFile(textBoxFilepath.Text))
                    {
                        _fileSystem.MoveFile(textBoxFilepath.Text + "-encrypted", textBoxFilepath.Text);

                        listBoxHistory.Items.Add("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] File \"" + textBoxFilepath.Text + "\" encrypted using key \"" + textBoxFileKey.Text + "\"");

                        listBoxHistory.SelectedIndex = (listBoxHistory.Items.Count - 1);

                        toolStripStatusLabel.Text = "File encrypted";
                    }
                }
            }
        }

        private void buttonDecryptFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFilepath.Text))
            {
                MessageBox.Show("No filepath provided. Please provide the filepath to a file in the Filepath field.", "Empty Filepath", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrEmpty(textBoxFileKey.Text))
            {
                MessageBox.Show("No key has been provided to decrypt the file. Please provide the key that was used to encrypt the file in the Key field to ensure that the file can be decrypted.", "Key Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (_fileSystem.FileExists(textBoxFilepath.Text))
            {
                try
                {
                    _security.DecryptFile(textBoxFilepath.Text, textBoxFilepath.Text + "-decrypted", textBoxFileKey.Text);

                    if (_fileSystem.FileExists(textBoxFilepath.Text))
                    {
                        if (_fileSystem.DeleteFile(textBoxFilepath.Text))
                        {
                            _fileSystem.MoveFile(textBoxFilepath.Text + "-decrypted", textBoxFilepath.Text);

                            listBoxHistory.Items.Add("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] File \"" + textBoxFilepath.Text + "\" decrypted using key \"" + textBoxFileKey.Text + "\"");

                            listBoxHistory.SelectedIndex = (listBoxHistory.Items.Count - 1);

                            textBoxFileKey.Text = string.Empty;

                            toolStripStatusLabel.Text = "File decrypted";
                        }
                    }
                }
                catch
                {
                    toolStripStatusLabel.Text = "An error was encountered";
                }
            }
        }

        private void buttonEncryptText_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxText.Text))
            {
                MessageBox.Show("No text provided. Please provide text to encrypt.", "No Text", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrEmpty(textBoxTextKey.Text))
            {
                MessageBox.Show("No key provided. Please provide a key to use for encrypting the text.", "Key Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            textBoxText.Text = _security.EncryptText(textBoxText.Text, textBoxTextKey.Text);

            toolStripStatusLabel.Text = "Text encrypted";
        }

        private void buttonDecryptText_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxText.Text))
                {
                    MessageBox.Show("No text provided. Please provide encrypted text to decrypt.", "No Text", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (string.IsNullOrEmpty(textBoxTextKey.Text))
                {
                    MessageBox.Show("No key provided. Please provide the key that was used to encrypt the text for decrypting the encrypted text.", "Key Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                textBoxText.Text = _security.DecryptText(textBoxText.Text, textBoxTextKey.Text);

                toolStripStatusLabel.Text = "Text decrypted";
            }
            catch
            {
                toolStripStatusLabel.Text = "An error was encountered";
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = string.Empty;
        }
    }
}
