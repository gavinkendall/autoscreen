//-----------------------------------------------------------------------
// <copyright file="FormMain-Labels.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for handling labels.</summary>
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
using System.Collections.Generic;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        private void applyLabel_Click(object sender, EventArgs e)
        {
            if (sender != null && sender is ToolStripDropDownItem)
            {
                ToolStripDropDownItem toolStripDropDownItem = (ToolStripDropDownItem)sender;

                if (!string.IsNullOrEmpty(toolStripDropDownItem.Text))
                {
                    checkBoxScreenshotLabel.Checked = true;
                    comboBoxScreenshotLabel.Text = toolStripDropDownItem.Text;
                    SaveSettings();
                }
            }
        }

        private void ComboBoxScreenshotLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem label in toolStripMenuItemApplyLabel.DropDownItems)
            {
                label.Checked = label.Text.Equals(comboBoxScreenshotLabel.Text);
            }
        }

        private void PopulateLabelList()
        {
            try
            {
                List<string> labels = new List<string>();
                labels = _screenshotCollection.GetFilterValueList("Label");

                if (_screenCapture.LockScreenCaptureSession || labels.Count == 0)
                {
                    toolStripSeparatorApplyLabel.Visible = false;
                    toolStripMenuItemApplyLabel.Visible = false;

                    return;
                }

                toolStripSeparatorApplyLabel.Visible = true;
                toolStripMenuItemApplyLabel.Visible = true;

                labels.Sort();

                comboBoxScreenshotLabel.DataSource = labels;
                comboBoxScreenshotLabel.Text = _config.Settings.User.GetByKey("ScreenshotLabel", _config.Settings.DefaultSettings.ScreenshotLabel).Value.ToString();

                toolStripMenuItemApplyLabel.DropDownItems.Clear();

                foreach (string label in labels)
                {
                    ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem()
                    {
                        Text = label,
                        Checked = label.Equals(comboBoxScreenshotLabel.Text),
                        CheckOnClick = true
                    };

                    toolStripMenuItem.Click += new EventHandler(applyLabel_Click);

                    toolStripMenuItemApplyLabel.DropDownItems.Add(toolStripMenuItem);
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-Labels::PopulateLabelList", ex);
            }
        }

        private void ApplyLabel(string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                _config.Settings.User.SetValueByKey("ApplyScreenshotLabel", false);

                checkBoxScreenshotLabel.Checked = false;
            }
            else
            {
                label = label.Trim();

                _config.Settings.User.SetValueByKey("ApplyScreenshotLabel", true);
                _config.Settings.User.SetValueByKey("ScreenshotLabel", label);

                if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                {
                    _screenCapture.ApplicationError = true;
                }

                checkBoxScreenshotLabel.Checked = true;
                comboBoxScreenshotLabel.Text = label;
            }
        }
    }
}