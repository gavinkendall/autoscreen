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
        private void RunScreenCaptures()
        {
            Log.Write("Running screen captures");

            foreach (Screen screen in formScreen.ScreenCollection)
            {
                if (screen.Enabled)
                {
                    if (screen.Component == 0)
                    {
                        MacroParser.screenCapture = _screenCapture;

                        // Active Window
                        if (!string.IsNullOrEmpty(_screenCapture.ActiveWindowTitle))
                        {
                            if (_screenCapture.GetScreenImages(screen.Component, 0, 0, 0, 0, false, screen.ResolutionRatio, out Bitmap bitmap))
                            {
                                if (_screenCapture.TakeScreenshot(
                                    path: FileSystem.CorrectScreenshotsFolderPath(MacroParser.ParseTagsForFolderPath(screen.Folder, formTag.TagCollection)) + MacroParser.ParseTagsForFilePath(screen.Name, screen.Macro, screen.Component, screen.Format, _screenCapture.ActiveWindowTitle, formTag.TagCollection),
                                    format: screen.Format,
                                    component: screen.Component,
                                    screenshotType: ScreenshotType.ActiveWindow,
                                    jpegQuality: screen.JpegQuality,
                                    viewId: screen.ViewId,
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
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (formScreen.ScreenDictionary.ContainsKey(screen.Component))
                        {
                            MacroParser.screenCapture = _screenCapture;

                            if (!string.IsNullOrEmpty(_screenCapture.ActiveWindowTitle))
                            {
                                // Screen X
                                if (_screenCapture.GetScreenImages(screen.Component,
                                    formScreen.ScreenDictionary[screen.Component].Bounds.X,
                                    formScreen.ScreenDictionary[screen.Component].Bounds.Y,
                                    formScreen.ScreenDictionary[screen.Component].Bounds.Width,
                                    formScreen.ScreenDictionary[screen.Component].Bounds.Height, screen.Mouse, screen.ResolutionRatio, out Bitmap bitmap))
                                {
                                    if (_screenCapture.TakeScreenshot(
                                        path: FileSystem.CorrectScreenshotsFolderPath(MacroParser.ParseTagsForFolderPath(screen.Folder, formTag.TagCollection)) + MacroParser.ParseTagsForFilePath(screen.Name, screen.Macro, screen.Component, screen.Format, _screenCapture.ActiveWindowTitle, formTag.TagCollection),
                                        format: screen.Format,
                                        component: screen.Component,
                                        screenshotType: ScreenshotType.Screen,
                                        jpegQuality: screen.JpegQuality,
                                        viewId: screen.ViewId,
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
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}