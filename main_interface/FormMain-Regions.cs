namespace AutoScreenCapture
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using AutoScreenCapture.Properties;

    public partial class FormMain : Form
    {
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