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
            formRegion.RegionObject = null;
            formRegion.ImageFormatCollection = _imageFormatCollection;
            formRegion.screenCapture = _screenCapture;

            formRegion.ShowDialog(this);

            if (formRegion.DialogResult == DialogResult.OK)
            {
                BuildRegionsModule();
                BuildViewTabPages();

                formRegion.RegionCollection.SaveToXmlFile();
            }
        }

        /// <summary>
        /// Removes the selected Regions from the Regions tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSelectedRegions_Click(object sender, EventArgs e)
        {
            int countBeforeRemoval = formRegion.RegionCollection.Count;

            foreach (Control control in tabPageRegions.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Region region = formRegion.RegionCollection.Get((Region)checkBox.Tag);
                        formRegion.RegionCollection.Remove(region);
                    }
                }
            }

            if (countBeforeRemoval > formRegion.RegionCollection.Count)
            {
                BuildRegionsModule();
                BuildViewTabPages();

                formRegion.RegionCollection.SaveToXmlFile();
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

            formRegion.RegionObject = region;
            formRegion.ImageFormatCollection = _imageFormatCollection;
            formRegion.screenCapture = _screenCapture;

            formRegion.ShowDialog(this);

            if (formRegion.DialogResult == DialogResult.OK)
            {
                BuildRegionsModule();
                BuildViewTabPages();

                formRegion.RegionCollection.SaveToXmlFile();
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
                    Region region = formRegion.RegionCollection.Get(regionSelected);
                    formRegion.RegionCollection.Remove(region);

                    BuildRegionsModule();
                    BuildViewTabPages();

                    formRegion.RegionCollection.SaveToXmlFile();
                }
            }
        }

        private void RunRegionCaptures()
        {
            Log.Write("Running region captures");

            foreach (Region region in formRegion.RegionCollection)
            {
                if (region.Enabled)
                {
                    MacroParser.screenCapture = _screenCapture;

                    if (!string.IsNullOrEmpty(_screenCapture.ActiveWindowTitle))
                    {
                        if (_screenCapture.GetScreenImages(-1, region.X, region.Y, region.Width, region.Height, region.Mouse, region.ResolutionRatio, out Bitmap bitmap))
                        {
                            if (_screenCapture.TakeScreenshot(
                                path: FileSystem.CorrectScreenshotsFolderPath(MacroParser.ParseTagsForFolderPath(region.Folder, formTag.TagCollection)) + MacroParser.ParseTagsForFilePath(region.Name, region.Macro, -1, region.Format, _screenCapture.ActiveWindowTitle, formTag.TagCollection),
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
    }
}