//-----------------------------------------------------------------------
// <copyright file="FormRegionSelectOptions.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The dialog box to manage Capture Now options.</summary>
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
    /// The dialog box to manage Capture Now options.
    /// </summary>
    public partial class FormCaptureNowOptions : Form
    {
        private Config _config;
        private FileSystem _fileSystem;

        /// <summary>
        /// The dialog box to manage Capture Now options.
        /// </summary>
        public FormCaptureNowOptions(Config config, FileSystem fileSystem)
        {
            _config = config;
            _fileSystem = fileSystem;

            InitializeComponent();
        }

        private void FormCaptureNowOptions_Load(object sender, System.EventArgs e)
        {
            // The default Capture Now macro if we were unable to get the value from the CaptureNowMacro key
            // since this might be from an older version of the application when the CaptureNowMacro key didn't exist yet.
            string captureNowMacro = @"%date%\%name%\%date%_%time%.%format%";

            Setting captureNowMacroSetting = _config.Settings.User.GetByKey("CaptureNowMacro");

            if (captureNowMacroSetting != null)
            {
                captureNowMacro = _config.Settings.User.GetByKey("CaptureNowMacro").Value.ToString();
            }

            textBoxCaptureNowMacro.Text = captureNowMacro;
        }

        private void Save()
        {
            _config.Settings.User.SetValueByKey("CaptureNowMacro", textBoxCaptureNowMacro.Text);

            _config.Settings.User.Save(_config.Settings, _fileSystem);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }
    }
}
