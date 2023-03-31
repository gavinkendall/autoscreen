//-----------------------------------------------------------------------
// <copyright file="FormAutoScreenCaptureForBeginners.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A tool for people who are unfamiliar with Auto Screen Capture.</summary>
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
    public partial class FormAutoScreenCaptureForBeginners : Form
    {
        private Config _config;

        /// <summary>
        /// A tool for people who are unfamiliar with Auto Screen Capture.
        /// </summary>
        public FormAutoScreenCaptureForBeginners(Config config)
        {
            InitializeComponent();

            _config = config;
        }

        private void FormAutoScreenCaptureForBeginners_Load(object sender, EventArgs e)
        {
            Setting showScreenCaptureStatusOnStartSetting = _config.Settings.User.GetByKey("ShowScreenCaptureStatusOnStart");

            if (showScreenCaptureStatusOnStartSetting != null)
            {
                checkBoxShowScreenCaptureStatusOnStart.Checked = Convert.ToBoolean(showScreenCaptureStatusOnStartSetting.Value);
            }

            Setting showScreenshotsFolderOnStopSetting = _config.Settings.User.GetByKey("ShowScreenshotsFolderOnStop");

            if (showScreenshotsFolderOnStopSetting != null)
            {
                checkBoxShowScreenshotsFolderOnStop.Checked = Convert.ToBoolean(showScreenshotsFolderOnStopSetting.Value);
            }
        }

        private void FormAutoScreenCaptureForBeginners_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void buttonScreenshotsFolderBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                textBoxScreenshotsFolder.Text = folderBrowser.SelectedPath;
            }
        }

        private void checkBoxShowScreenCaptureStatusOnStart_CheckedChanged(object sender, EventArgs e)
        {
            _config.Settings.User.SetValueByKey("ShowScreenCaptureStatusOnStart", checkBoxShowScreenCaptureStatusOnStart.Checked);
            _config.Settings.User.Save(_config.Settings, _config.FileSystem);
        }

        private void checkBoxShowScreenshotsFolderOnStop_CheckedChanged(object sender, EventArgs e)
        {
            _config.Settings.User.SetValueByKey("ShowScreenshotsFolderOnStop", checkBoxShowScreenshotsFolderOnStop.Checked);
            _config.Settings.User.Save(_config.Settings, _config.FileSystem);
        }
    }
}
