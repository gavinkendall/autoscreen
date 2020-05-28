//-----------------------------------------------------------------------
// <copyright file="FormMain-Regions.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for handling regions.</summary>
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
            _formRegion.RegionObject = null;
            _formRegion.ImageFormatCollection = _imageFormatCollection;
            _formRegion.ScreenCapture = _screenCapture;
            _formRegion.TagCollection = _formTag.TagCollection;

            _formRegion.ShowDialog(this);

            if (_formRegion.DialogResult == DialogResult.OK)
            {
                BuildRegionsModule();
                BuildViewTabPages();

                _formRegion.RegionCollection.SaveToXmlFile();
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

                _formRegion.RegionCollection.SaveToXmlFile();
            }
        }

        /// <summary>
        /// Shows the "Change Region" window to enable the user to edit a chosen Region.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeRegion_Click(object sender, EventArgs e)
        {
            Region region = new Region();

            if (sender is Button)
            {
                Button buttonSelected = (Button)sender;
                region = (Region)buttonSelected.Tag;
            }

            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                region = (Region)toolStripMenuItemSelected.Tag;
            }

            _formRegion.RegionObject = region;
            _formRegion.ImageFormatCollection = _imageFormatCollection;
            _formRegion.ScreenCapture = _screenCapture;
            _formRegion.TagCollection = _formTag.TagCollection;

            _formRegion.ShowDialog(this);

            if (_formRegion.DialogResult == DialogResult.OK)
            {
                BuildRegionsModule();
                BuildViewTabPages();

                _formRegion.RegionCollection.SaveToXmlFile();
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

                    _formRegion.RegionCollection.SaveToXmlFile();
                }
            }
        }

        private void RunRegionCaptures()
        {
            try
            {
                foreach (Region region in _formRegion.RegionCollection)
                {
                    if (region.Active)
                    {
                        MacroParser.screenCapture = _screenCapture;

                        if (!string.IsNullOrEmpty(_screenCapture.ActiveWindowTitle))
                        {
                            // Do not contiune if the active window title needs to be checked and the active window title
                            // does not contain the text defined in "Active Window Title Capture Text" and CaptureNow is false.
                            // CaptureNow could be set to "true" during a "Capture Now / Archive" or "Capture Now / Edit" option
                            // so, in that case, we want to capture the screen and save the screenshot regardless of the title text.
                            if (checkBoxActiveWindowTitle.Checked && !string.IsNullOrEmpty(textBoxActiveWindowTitle.Text) &&
                                !_screenCapture.ActiveWindowTitle.ToLower().Contains(textBoxActiveWindowTitle.Text.ToLower()) &&
                                !_screenCapture.CaptureNow)
                            {
                                return;
                            }

                            _screenCapture.CaptureNow = false;

                            if (_screenCapture.GetScreenImages(-1, region.X, region.Y, region.Width, region.Height, region.Mouse, region.ResolutionRatio, out Bitmap bitmap))
                            {
                                if (_screenCapture.SaveScreenshot(
                                    path: FileSystem.CorrectScreenshotsFolderPath(MacroParser.ParseTags(region.Folder, _formTag.TagCollection)) + MacroParser.ParseTags(preview: false, region.Name, region.Macro, -1, region.Format, _screenCapture.ActiveWindowTitle, _formTag.TagCollection),
                                    format: region.Format,
                                    component: -1,
                                    screenshotType: ScreenshotType.Region,
                                    jpegQuality: region.JpegQuality,
                                    viewId: region.ViewId,
                                    bitmap: bitmap,
                                    label: checkBoxScreenshotLabel.Checked ? comboBoxScreenshotLabel.Text : string.Empty,
                                    windowTitle: _screenCapture.ActiveWindowTitle,
                                    processName: _screenCapture.ActiveWindowProcessName,
                                    screenshotCollection: _screenshotCollection
                                ))
                                {
                                    ScreenshotTakenWithSuccess();
                                }
                                else
                                {
                                    ScreenshotTakenWithFailure();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("FormMain-Regions::RunRegionCaptures", ex);
            }
        }
    }
}