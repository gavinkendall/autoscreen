//-----------------------------------------------------------------------
// <copyright file="FormScreenCaptureStatusWithLabelSwitcher.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The Label Switcher tool.</summary>
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
    /// The Label Switcher tool.
    /// </summary>
    public partial class FormScreenCaptureStatusWithLabelSwitcher : Form
    {
        private Config _config;
        private FileSystem _fileSystem;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="config">Configuration.</param>
        /// <param name="fileSystem">File system.</param>
        public FormScreenCaptureStatusWithLabelSwitcher(Config config, FileSystem fileSystem)
        {
            InitializeComponent();

            _config = config;
            _fileSystem = fileSystem;
        }

        private void FormLabelSwitcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        /// <summary>
        /// Changes the screenshot label based on what's selected in the drop-down control list and applies the selected label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxLabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLabels.SelectedItem != null)
            {
                _config.Settings.User.SetValueByKey("ApplyScreenshotLabel", true);
                _config.Settings.User.SetValueByKey("ScreenshotLabel", comboBoxLabels.SelectedItem);
                _config.Settings.User.Save(_config.Settings, _fileSystem);
            }
        }

        private void buttonAddScreenshotLabelToList_Click(object sender, EventArgs e)
        {
            string labelToAdd = textBoxNewLabel.Text.Trim();

            if (string.IsNullOrEmpty(labelToAdd))
            {
                return;
            }

            textBoxNewLabel.Clear();

            if (!comboBoxLabels.Items.Contains(labelToAdd))
            {
                comboBoxLabels.Items.Add(labelToAdd);

                comboBoxLabels.SelectedItem = labelToAdd;
            }
        }
    }
}
