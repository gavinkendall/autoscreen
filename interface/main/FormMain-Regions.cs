//-----------------------------------------------------------------------
// <copyright file="FormMain-Regions.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for handling regions.</summary>
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
using System.Drawing;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Shows the "Add Region" window to enable the user to add a chosen Region.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRegion_Click(object sender, EventArgs e)
        {
            ShowInterface();

            _formRegion.RegionObject = null;
            _formRegion.ImageFormatCollection = _imageFormatCollection;
            _formRegion.TagCollection = _formMacroTag.MacroTagCollection;

            if (!_formRegion.Visible)
            {
                _formRegion.ShowDialog(this);
            }
            else
            {
                _formRegion.Activate();
            }

            if (_formRegion.DialogResult == DialogResult.OK)
            {
                BuildRegionsModule();
                BuildViewTabPages();

                if (!_formRegion.RegionCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Removes the selected Regions from the Regions tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSelectedRegions_Click(object sender, EventArgs e)
        {
            int countBeforeRemoval = _formRegion.RegionCollection.Count;

            foreach (Control control in tabPageRegions.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Region region = _formRegion.RegionCollection.Get((Region)checkBox.Tag);
                        _formRegion.RegionCollection.Remove(region);
                    }
                }
            }

            if (countBeforeRemoval > _formRegion.RegionCollection.Count)
            {
                BuildRegionsModule();
                BuildViewTabPages();

                if (!_formRegion.RegionCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Shows the "Change Region" window to enable the user to edit a chosen Region.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configureRegion_Click(object sender, EventArgs e)
        {
            ShowInterface();

            Region region = new Region();

            if (sender is Button)
            {
                Button buttonSelected = (Button)sender;
                region = (Region)buttonSelected.Tag;
            }

            if (sender is ToolStripButton)
            {
                ToolStripButton buttonSelected = (ToolStripButton)sender;
                region = (Region)buttonSelected.Tag;
            }

            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                region = (Region)toolStripMenuItemSelected.Tag;
            }

            _formRegion.RegionObject = region;
            _formRegion.ImageFormatCollection = _imageFormatCollection;
            _formRegion.TagCollection = _formMacroTag.MacroTagCollection;

            _formRegion.ShowDialog(this);

            if (_formRegion.DialogResult == DialogResult.OK)
            {
                BuildRegionsModule();
                BuildViewTabPages();

                if (!_formRegion.RegionCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        private void removeRegion_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                Region regionSelected = (Region)toolStripMenuItemSelected.Tag;

                DialogResult dialogResult = MessageBox.Show("Do you want to remove the region named \"" + regionSelected.Name + "\"?", "Remove Region", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Region region = _formRegion.RegionCollection.Get(regionSelected);
                    _formRegion.RegionCollection.Remove(region);

                    BuildRegionsModule();
                    BuildViewTabPages();

                    if (!_formRegion.RegionCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                    {
                        _screenCapture.ApplicationError = true;
                    }
                }
            }
        }

        private void RunRegionCaptures()
        {
            try
            {
                foreach (Region region in _formRegion.RegionCollection)
                {
                    if (region.Enable)
                    {
                        if (_screenCapture.GetScreenImages(source: -1, component: -1, captureMethod: 1, autoAdapt: false, region.X, region.Y, region.Width, region.Height, resolutionRatio: 100, region.Mouse, out Bitmap bitmap))
                        {
                            if (!SaveScreenshot(bitmap, region))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            _log.WriteDebugMessage($"No image was captured for region {region.Name}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-Regions::RunRegionCaptures", ex);
            }
        }
    }
}