//-----------------------------------------------------------------------
// <copyright file="FormRegionSelectOptions.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The dialog box to manage Region Select options.</summary>
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
    /// The dialog box to manage Region Select options.
    /// </summary>
    public partial class FormRegionSelectOptions : Form
    {
        private Config _config;
        private FileSystem _fileSystem;
        private ImageFormatCollection _imageFormatCollection;

        /// <summary>
        /// The dialog box to manage Region Select options.
        /// </summary>
        /// <param name="config">The configuration set to use.</param>
        /// <param name="fileSystem">The file system to use.</param>
        /// <param name="imageFormatCollection">A collection of image formats.</param>
        public FormRegionSelectOptions(Config config, FileSystem fileSystem, ImageFormatCollection imageFormatCollection)
        {
            _config = config;
            _fileSystem = fileSystem;
            _imageFormatCollection = imageFormatCollection;

            InitializeComponent();
        }

        private void FormRegionSelectOptions_Load(object sender, EventArgs e)
        {
            LoadOptions();
        }

        private void LoadOptions()
        {
            textBoxAutoSaveFolder.Text = _config.Settings.User.GetByKey("AutoSaveFolder", _config.Settings.DefaultSettings.AutoSaveFolder).Value.ToString();
            textBoxAutoSaveMacro.Text = _config.Settings.User.GetByKey("AutoSaveMacro", _config.Settings.DefaultSettings.AutoSaveMacro).Value.ToString();

            comboBoxFormat.Items.Clear();

            foreach (ImageFormat imageFormat in _imageFormatCollection)
            {
                comboBoxFormat.Items.Add(imageFormat.Name);
            }

            comboBoxFormat.SelectedIndex = comboBoxFormat.Items.IndexOf(_config.Settings.User.GetByKey("AutoSaveFormat", _config.Settings.DefaultSettings.AutoSaveFormat).Value.ToString());
        }

        private void SaveOptions()
        {
            _config.Settings.User.GetByKey("AutoSaveFolder", _config.Settings.DefaultSettings.AutoSaveFolder).Value = textBoxAutoSaveFolder.Text.Trim();
            _config.Settings.User.GetByKey("AutoSaveMacro", _config.Settings.DefaultSettings.AutoSaveMacro).Value = textBoxAutoSaveMacro.Text.Trim();
            _config.Settings.User.GetByKey("AutoSaveFormat", _config.Settings.DefaultSettings.AutoSaveFormat).Value = comboBoxFormat.Text;

            _config.Settings.User.Save(_config.Settings, _fileSystem);
        }

        /// <summary>
        /// Shows a folder selection dialog box for "Region Select / Auto Save".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();

            if (browser.ShowDialog() == DialogResult.OK)
            {
                textBoxAutoSaveFolder.Text = browser.SelectedPath;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveOptions();
            Close();
        }
    }
}
