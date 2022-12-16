//-----------------------------------------------------------------------
// <copyright file="FormMain-ScreenCapture.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods related to capturing screens and what we should do when buttons are clicked or menu items are selected.</summary>
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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        private FormRegionSelectWithMouse _formRegionSelectWithMouse;

        /// <summary>
        /// Returns the screen capture interval. This value will be used as the screen capture timer's interval property.
        /// </summary>
        /// <returns></returns>
        private int GetScreenCaptureInterval()
        {
            return ConvertIntoMilliseconds((int)_formSetup.numericUpDownHoursInterval.Value,
                (int)_formSetup.numericUpDownMinutesInterval.Value, (int)_formSetup.numericUpDownSecondsInterval.Value, (int)_formSetup.numericUpDownMillisecondsInterval.Value);
        }

        /// <summary>
        /// Figures out if the "Start Capture" controls should be enabled or disabled.
        /// </summary>
        private void EnableStartCapture()
        {
            if (GetScreenCaptureInterval() > 0)
            {
                if (tabControlViews.TabCount > 0 && tabControlViews.SelectedTab != null && tabControlViews.SelectedTab.Name.Equals("tabPageDashboard"))
                {
                    ToolStrip toolStripDashboard = (ToolStrip)tabControlViews.SelectedTab.Controls["toolStripDashboard"];
                    ToolStripButton startScreenCaptureButton = (ToolStripButton)toolStripDashboard.Items["dashboardStartScreenCapture"];
                    startScreenCaptureButton.Enabled = true;
                }

                toolStripDropDownButtonStartScreenCapture.Enabled = true;
                toolStripMenuItemStartScreenCapture.Enabled = true;
                toolStripMainMenuItemStartScreenCapture.Enabled = true;

                _formAutoScreenCaptureForBeginners.buttonStartScreenCapture.Enabled = true;
            }
            else
            {
                DisableStartCapture();
            }
        }

        /// <summary>
        /// Enables the "Stop Capture" controls.
        /// </summary>
        private void EnableStopScreenCapture()
        {
            if (tabControlViews.TabCount > 0 && tabControlViews.SelectedTab != null && tabControlViews.SelectedTab.Name.Equals("tabPageDashboard"))
            {
                ToolStrip toolStripDashboard = (ToolStrip)tabControlViews.SelectedTab.Controls["toolStripDashboard"];
                ToolStripButton stopScreenCaptureButton = (ToolStripButton)toolStripDashboard.Items["dashboardStopScreenCapture"];
                stopScreenCaptureButton.Enabled = true;
            }

            toolStripDropDownButtonStopScreenCapture.Enabled = true;
            toolStripMenuItemStopScreenCapture.Enabled = true;
            toolStripMainMenuItemStopScreenCapture.Enabled = true;

            _formAutoScreenCaptureForBeginners.buttonStopScreenCapture.Enabled = true;
        }

        /// <summary>
        /// Disables the "Stop Capture" controls.
        /// </summary>
        private void DisableStopCapture()
        {
            if (tabControlViews.TabCount > 0 && tabControlViews.SelectedTab != null && tabControlViews.SelectedTab.Name.Equals("tabPageDashboard"))
            {
                ToolStrip toolStripDashboard = (ToolStrip)tabControlViews.SelectedTab.Controls["toolStripDashboard"];
                ToolStripButton stopScreenCaptureButton = (ToolStripButton)toolStripDashboard.Items["dashboardStopScreenCapture"];
                stopScreenCaptureButton.Enabled = false;
            }

            toolStripDropDownButtonStopScreenCapture.Enabled = false;
            toolStripMenuItemStopScreenCapture.Enabled = false;
            toolStripMainMenuItemStopScreenCapture.Enabled = false;

            _formAutoScreenCaptureForBeginners.buttonStopScreenCapture.Enabled = false;
        }

        /// <summary>
        /// Disables the "Start Capture" controls.
        /// </summary>
        private void DisableStartCapture()
        {
            if (tabControlViews.TabCount > 0 && tabControlViews.SelectedTab != null && tabControlViews.SelectedTab.Name.Equals("tabPageDashboard"))
            {
                ToolStrip toolStripDashboard = (ToolStrip)tabControlViews.SelectedTab.Controls["toolStripDashboard"];
                ToolStripButton startScreenCaptureButton = (ToolStripButton)toolStripDashboard.Items["dashboardStartScreenCapture"];
                startScreenCaptureButton.Enabled = false;
            }

            toolStripDropDownButtonStartScreenCapture.Enabled = false;
            toolStripMenuItemStartScreenCapture.Enabled = false;
            toolStripMainMenuItemStartScreenCapture.Enabled = false;

            _formAutoScreenCaptureForBeginners.buttonStartScreenCapture.Enabled = false;
        }

        /// <summary>
        /// Checks the capture limit when the checkbox is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged_checkBoxCaptureLimit(object sender, EventArgs e)
        {
            CaptureLimitCheck();
        }

        /// <summary>
        /// Checks the capture limit.
        /// </summary>
        private void CaptureLimitCheck()
        {
            if (_formSetup.checkBoxCaptureLimit.Checked)
            {
                _screenCapture.CycleCount = 0;
                _screenCapture.Limit = (int)_formSetup.numericUpDownCaptureLimit.Value;
            }
        }

        /// <summary>
        /// Takes a screenshot of each available region and screen.
        /// </summary>
        /// <param name="scope">Defines the screen capture scope of what will be captured (such as "All Screens and Regions", "All Screens", "All Regions", or the name of a Screen or Region).</param>
        /// <param name="captureNow">Determines if screen capture should occur immediately.</param>
        /// <param name="initiatedByUser">Determines if the screen capture was initiated by the user.</param>
        private void TakeScreenshot(string scope = "All Screens and Regions", bool captureNow = false, bool initiatedByUser = false)
        {
            _log.WriteDebugMessage($"Taking screenshot using scope={scope}, captureNow={captureNow}, initiatedByUser={initiatedByUser}");

            try
            {
                // Test to see if we can get images of the screen before continuing.
                if (_screenCapture.GetScreenImages(0, 0, 0, autoAdapt: false, 0, 0, 0, 0, 0, false, out _))
                {
                    _macroParser.screenCapture = _screenCapture;

                    _screenCapture.CycleCount++;
                    _screenCapture.Scope = scope;
                    _screenCapture.CaptureNow = captureNow;

                    // We can use this to override the value of any macro from a screen or region (if we need to).
                    // At the moment it's being used by the Capture Now Macro from Capture Now Options.
                    string macroOverride = string.Empty;

                    // Whenever the user selects "Capture Now / Archive" or "Capture Now / Edit".
                    if (captureNow && initiatedByUser)
                    {
                        // The default Capture Now macro if we were unable to get the value from the CaptureNowMacro key
                        // since this might be from an older version of the application when the CaptureNowMacro key didn't exist yet.
                        string captureNowMacro = @"%date%\%name%\%date%_%time%.%format%";

                        Setting captureNowMacroSetting = _config.Settings.User.GetByKey("CaptureNowMacro");

                        if (captureNowMacroSetting != null)
                        {
                            captureNowMacro = _config.Settings.User.GetByKey("CaptureNowMacro").Value.ToString();
                        }

                        if (!string.IsNullOrEmpty(captureNowMacro))
                        {
                            macroOverride = captureNowMacro;
                        }

                        // Keep a count of how many times the user has used "Capture Now" so we can include it in a Macro Tag.
                        _screenCapture.CaptureNowCount++;
                    }

                    DateTime dtNow = DateTime.Now;

                    _screenCapture.DateTimeScreenshotsTaken = dtNow;

                    if (!captureNow)
                    {
                        _screenCapture.DateTimePreviousCycle = dtNow;
                    }

                    _formSetup.DoApplicationFocus();

                    _screenCapture.ActiveWindowTitle = _screenCapture.GetActiveWindowTitle();

                    _screenCapture.ActiveWindowProcessName = _screenCapture.GetActiveWindowProcessName();

                    if (!string.IsNullOrEmpty(_formSetup.textBoxActiveWindowTitle.Text))
                    {
                        if (_formSetup.checkBoxActiveWindowTitleComparisonCheck.Checked && !ActiveWindowTitleMatchText())
                        {
                            return;
                        }

                        if (_formSetup.checkBoxActiveWindowTitleComparisonCheckReverse.Checked && !ActiveWindowTitleDoesNotMatchText())
                        {
                            return;
                        }
                    }

                    RunRegionCaptures(scope, macroOverride);

                    RunScreenCaptures(scope, macroOverride);
                }

                _log.WriteDebugMessage("Running triggers of condition type CaptureCycleElapsed");

                RunTriggersOfConditionType(TriggerConditionType.CaptureCycleElapsed);
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-ScreenCapture::TakeScreenshot", ex);
            }
        }

        /// <summary>
        /// Starts a screen capture session with the default screen capture interval.
        /// </summary>
        /// <param name="scope">Defines the screen capture scope of what will be captured (such as "All Screens and Regions", "All Screens", "All Regions", or the name of a Screen or Region).</param>
        private void StartScreenCapture(string scope = "All Screens and Regions")
        {
            int screenCaptureInterval = GetScreenCaptureInterval();

            StartScreenCapture(screenCaptureInterval, scope);
        }

        /// <summary>
        /// Starts a screen capture session with a defined screen capture interval.
        /// </summary>
        /// <param name="screenCaptureInterval">The screen capture interval to use.</param>
        /// <param name="scope">Defines the screen capture scope of what will be captured (such as "All Screens and Regions", "All Screens", "All Regions", or the name of a Screen or Region).</param>
        private void StartScreenCapture(int screenCaptureInterval, string scope = "All Screens and Regions")
        {
            try
            {
                // Default to "All Screens and Regions" if provided scope is empty regardless of default value.
                if (string.IsNullOrEmpty(scope))
                {
                    scope = "All Screens and Regions";
                }

                if (!_screenCapture.Running && screenCaptureInterval > 0)
                {
                   // If there was an application error just forget about it for now
                    // and reset the ApplicationError flag when starting a screen capture session.
                    _screenCapture.ApplicationError = false;

                    // Increment the number of times the user has started a screen capture session.
                    int startScreenCaptureCount = Convert.ToInt32(_config.Settings.User.GetByKey("StartScreenCaptureCount").Value);
                    startScreenCaptureCount++;
                    _config.Settings.User.SetValueByKey("StartScreenCaptureCount", startScreenCaptureCount);

                    if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                    {
                        _screenCapture.ApplicationError = true;
                    }

                    // Stop the date search thread if it's busy.
                    if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                    {
                        runDateSearchThread.CancelAsync();
                    }

                    // Stop the slide search thread if it's busy.
                    if (runScreenshotSearchThread != null && runScreenshotSearchThread.IsBusy)
                    {
                        runScreenshotSearchThread.CancelAsync();
                    }

                    DisableStartCapture();
                    EnableStopScreenCapture();

                    // Setup the properties for the screen capture class.
                    _screenCapture.Interval = screenCaptureInterval;
                    _screenCapture.Limit = _formSetup.checkBoxCaptureLimit.Checked ? (int)_formSetup.numericUpDownCaptureLimit.Value : 0;

                    _log.WriteMessage($"Starting screen capture using scope={scope}");

                    dtStartScreenCapture = DateTime.Now;

                    _screenCapture.Running = true;
                    _screenCapture.ApplicationWarning = false;

                    _formCommandDeck.buttonStartStopScreenCapture.Image = Properties.Resources.stop_screen_capture;
                    _formScreenCaptureStatus.buttonStartStopScreenCapture.Image = Properties.Resources.stop_screen_capture;
                    _formScreenCaptureStatusWithLabelSwitcher.buttonStartStopScreenCapture.Image = Properties.Resources.stop_screen_capture;

                    _screenCapture.DateTimeStartCapture = DateTime.Now;

                    if (_formSetup.checkBoxInitialScreenshot.Checked)
                    {
                        _log.WriteMessage($"Taking initial screenshots using scope={scope}");

                        TakeScreenshot(scope);
                    }

                    // Start taking screenshots.

                    timerScreenCapture.Interval = screenCaptureInterval;

                    timerScreenCapture.Enabled = true;
                    timerScreenCapture.Start();

                    _log.WriteDebugMessage("Running triggers of condition type ScreenCaptureStarted");
                    RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStarted);
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-ScreenCapture::StartScreenCapture", ex);
            }
        }

        /// <summary>
        /// Stops the screen capture session that's currently running.
        /// </summary>
        private void StopScreenCapture()
        {
            try
            {
                if (_screenCapture.Running)
                {
                    _log.WriteMessage("Stopping screen capture");

                    string passphrase = _config.Settings.User.GetByKey("Passphrase").Value.ToString();

                    if (!string.IsNullOrEmpty(passphrase))
                    {
                        _screenCapture.LockScreenCaptureSession = true;

                        if (!_formEnterPassphrase.Visible)
                        {
                            _formEnterPassphrase.Text = "Auto Screen Capture - Enter Passphrase (Stop Screen Capture)";
                            _formEnterPassphrase.ShowDialog(this);
                        }
                        else
                        {
                            _formEnterPassphrase.Activate();
                        }

                        if (_formEnterPassphrase.DialogResult != DialogResult.OK)
                        {
                            _log.WriteErrorMessage("Passphrase incorrect or not entered. Cannot stop screen capture session. Screen capture session has been locked. Interface is now hidden");

                            HideInterface();

                            return;
                        }

                        _screenCapture.LockScreenCaptureSession = false;
                    }

                    DisableStopCapture();
                    EnableStartCapture();

                    dtStopScreenCapture = DateTime.Now;

                    _screenCapture.CycleCount = 0;
                    _screenCapture.Running = false;
                    _screenCapture.DateTimePreviousCycle = DateTime.MinValue;

                    timerScreenCapture.Stop();
                    timerScreenCapture.Enabled = false;

                    _formCommandDeck.buttonStartStopScreenCapture.Image = Properties.Resources.start_screen_capture;
                    _formScreenCaptureStatus.buttonStartStopScreenCapture.Image = Properties.Resources.start_screen_capture;
                    _formScreenCaptureStatusWithLabelSwitcher.buttonStartStopScreenCapture.Image = Properties.Resources.start_screen_capture;

                    SearchFilterValues();
                    SearchDates();

                    _log.WriteDebugMessage("Running triggers of condition type ScreenCaptureStopped");
                    RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStopped);
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-ScreenCapture::StopScreenCapture", ex);
            }
        }

        private void CaptureNowArchive()
        {
            TakeScreenshot(captureNow: true, initiatedByUser: true);
        }

        private void CaptureNowEdit()
        {
            string defaultEditor = _config.Settings.User.GetByKey("DefaultEditor").Value.ToString();

            if (string.IsNullOrEmpty(defaultEditor))
            {
                return;
            }

            TakeScreenshot(captureNow: true, initiatedByUser: true);

            Editor editor = _formEditor.EditorCollection.GetByName(defaultEditor);

            if (editor != null)
            {
                RunEditor(editor, _screenCapture.DateTimeScreenshotsTaken);
            }
        }

        /// <summary>
        /// Saves the screenshot.
        /// </summary>
        /// <param name="bitmap">The bitmap image to use.</param>
        /// <param name="screenOrRegion">The screen or the region to use.</param>
        /// <param name="macroOverride">The macro to use to override any macro from a screen or region so if this value is empty we'll use the macro from the screen or region.</param>
        /// <returns>Returns true if we were successful at saving the screenshot. Returns false if we were unsuccessful at saving the screenshot.</returns>
        private bool SaveScreenshot(Bitmap bitmap, object screenOrRegion, string macroOverride)
        {
            if (bitmap == null || screenOrRegion == null)
            {
                return false;
            }

            _log.WriteDebugMessage("Running triggers of condition type BeforeScreenshotTaken");

            RunTriggersOfConditionType(TriggerConditionType.BeforeScreenshotTaken);

            Guid viewId = Guid.Empty;
            string folder = string.Empty;
            string macro = string.Empty;
            ImageFormat format = new ImageFormat("JPEG", "jpeg");
            int jpegQuality = 100;
            int imageDiffTolerance = 0;
            bool encrypt = false;

            if (screenOrRegion is Screen screen)
            {
                viewId = screen.ViewId;
                folder = screen.Folder;
                macro = screen.Macro;
                format = screen.Format;
                jpegQuality = screen.JpegQuality;
                imageDiffTolerance = screen.ImageDiffTolerance;
                encrypt = screen.Encrypt;
            }

            if (screenOrRegion is Region region)
            {
                viewId = region.ViewId;
                folder = region.Folder;
                macro = region.Macro;
                format = region.Format;
                jpegQuality = region.JpegQuality;
                imageDiffTolerance = region.ImageDiffTolerance;
                encrypt = region.Encrypt;
            }

            string label = string.Empty;
            bool applyScreenshotLabel = Convert.ToBoolean(_config.Settings.User.GetByKey("ApplyScreenshotLabel").Value);

            if (applyScreenshotLabel)
            {
                label = _config.Settings.User.GetByKey("ScreenshotLabel").Value.ToString();
            }

            string folderPath = _fileSystem.CorrectScreenshotsFolderPath(_macroParser.ParseTags(preview: false, folder, screenOrRegion, _screenCapture.ActiveWindowTitle, _screenCapture.ActiveWindowProcessName, label, _formMacroTag.MacroTagCollection, _log));
            string macroPath = _macroParser.ParseTags(preview: false, macro, screenOrRegion, _screenCapture.ActiveWindowTitle, _screenCapture.ActiveWindowProcessName, label, _formMacroTag.MacroTagCollection, _log);

            // If we have a macro being given to this method replace the value of "macroPath" with the value of "macroOverride".
            // This is currently being used by the Capture Now Macro from Capture Now Options.
            if (!string.IsNullOrEmpty(macroOverride))
            {
                macroPath = _macroParser.ParseTags(preview: false, macroOverride, screenOrRegion, _screenCapture.ActiveWindowTitle, _screenCapture.ActiveWindowProcessName, label, _formMacroTag.MacroTagCollection, _log);
            }

            // The screenshot's entire path consists of the folder path and the macro (which is just the filename with all of the macro tags parsed; in other words you could have "C:\screenshots\%date%.%format%" where %date% and %format% are macro tags for the filename's macro).
            string screenshotPath = folderPath + macroPath;

            if (!string.IsNullOrEmpty(screenshotPath))
            {
                int filepathLengthLimit = Convert.ToInt32(_config.Settings.Application.GetByKey("FilepathLengthLimit").Value);

                if (!string.IsNullOrEmpty(screenshotPath))
                {
                    if (screenshotPath.Length >= filepathLengthLimit)
                    {
                        _log.WriteMessage($"File path length exceeds the configured length of {filepathLengthLimit} characters so value was truncated. Correct the value for the FilepathLengthLimit application setting to prevent truncation");
                        
                        // Truncate the full path in such a way that we can still view the image in the appropriate image format.
                        // For example, if this was a JPEG image then the extension would be ".jpeg" so truncate considering the length of the extension
                        // and then append the extension to the end of the truncated filepath.
                        screenshotPath = screenshotPath.Substring(0, filepathLengthLimit - (format.Extension.Length + 2)); // The +2 is just in case we're handling an encrypted/decrypted version of the file (which could include "-e" (for encrypted) or "-d" (for decrypted) at the end of the filename).
                        screenshotPath += format.Extension;
                    }
                }
            }

            Screenshot screenshot = new Screenshot(_screenCapture.ActiveWindowTitle, _screenCapture.DateTimeScreenshotsTaken, _macroParser, _config)
            {
                ViewId = viewId,
                FilePath = screenshotPath,
                FolderPath = folderPath,
                MacroPath = macroPath,
                Bitmap = bitmap,
                Format = format,
                ProcessName = _screenCapture.ActiveWindowProcessName + ".exe",
                Label = label,
                Encrypt = encrypt
            };

            int errorLevel = _screenCapture.SaveScreenshot(_security, jpegQuality, imageDiffTolerance, screenshot, _screenshotCollection);

            // ScreenSavingErrorLevels enum written by Oskars Grauzis (https://github.com/grauziitisos)
            bool screenSaved = errorLevel == (int)ScreenSavingErrorLevels.None ||
                    // Here we list the error levels that should not stop the capturing session. Writing !=0 is shorther than == flag
                    (errorLevel & (int)ScreenSavingErrorLevels.ImageDiffNotSignificant) != 0 ||
                    (errorLevel & (int)ScreenSavingErrorLevels.PathLengthExceeded) != 0 ||
                    (errorLevel & (int)ScreenSavingErrorLevels.DriveNotReady) != 0;

            // Just return false if a hash duplicate has been found because technically the screenshot hasn't been saved and we don't
            // want to be calling the ScreenshotTakenWithSuccess method (which will run Triggers with the AfterScreenshotTaken condition).
            if (errorLevel == (int)ScreenSavingErrorLevels.ImageDiffNotSignificant)
            {
                return false;
            }

            if (screenSaved)
            {
                ScreenshotTakenWithSuccess();

                return true;
            }
            else
            {
                ScreenshotTakenWithFailure();

                return false;
            }
        }

        /// <summary>
        /// The screenshot was successfully taken and saved.
        /// </summary>
        private void ScreenshotTakenWithSuccess()
        {
            _log.WriteDebugMessage("Running triggers of condition type AfterScreenshotTaken");

            RunTriggersOfConditionType(TriggerConditionType.AfterScreenshotTaken);
        }

        /// <summary>
        /// The screenshot was unsucessfully taken and saved.
        /// </summary>
        private void ScreenshotTakenWithFailure()
        {
            _log.WriteMessage("Application encountered error while taking a screenshot. Stopping screen capture");

            StopScreenCapture();
        }

        /// <summary>
        /// Starts a screen capture session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemStartScreenCapture_Click(object sender, EventArgs e)
        {
            StartScreenCapture();
        }

        /// <summary>
        /// Stops the currently running screen capture session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemStopScreenCapture_Click(object sender, EventArgs e)
        {
            StopScreenCapture();
        }

        /// <summary>
        /// Takes screenshots and saves them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCaptureNowArchive_Click(object sender, EventArgs e)
        {
            CaptureNowArchive();
        }

        /// <summary>
        /// Takes screenshots, saves them, and then opens those screenshots in the default editor. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCaptureNowEdit_Click(object sender, EventArgs e)
        {
            CaptureNowEdit();
        }

        /// <summary>
        /// Shows the Capture Now Options dialog box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemCaptureNowOptions_Click(object sender, EventArgs e)
        {
            if (!_formCaptureNowOptions.Visible)
            {
                _formCaptureNowOptions.ShowDialog(this);
            }
            else
            {
                _formCaptureNowOptions.Activate();
            }
        }

        /// <summary>
        /// Shows the Region Select Options dialog box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRegionSelectOptions_Click(object sender, EventArgs e)
        {
            if (!_formRegionSelectOptions.Visible)
            {
                _formRegionSelectOptions.ShowDialog(this);
            }
            else
            {
                _formRegionSelectOptions.Activate();
            }
        }

        /// <summary>
        /// Gets the bitmap image from the Region Select canvas.
        /// </summary>
        /// <returns>The bitmap image from the Region Select canvas.</returns>
        private Bitmap GetBitmapFromRegionSelect()
        {
            int x = _formRegionSelectWithMouse.outputX;
            int y = _formRegionSelectWithMouse.outputY;
            int width = _formRegionSelectWithMouse.outputWidth;
            int height = _formRegionSelectWithMouse.outputHeight;

            if (_screenCapture.GetScreenImages(source: -1, component: -1, captureMethod: 1, autoAdapt: false, x, y, width, height, resolutionRatio: 100, mouse: false, out Bitmap bitmap))
            {
                return bitmap;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Shows a mouse-driven region selection canvas so you can select a region and then have the captured image sent to the clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRegionSelectClipboard_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_screenCapture);
            _formRegionSelectWithMouse.LoadCanvas(sendToClipboard: true);
        }

        /// <summary>
        /// Shows a mouse-driven region selection canvas so you can select a region, have the captured image be sent to the clipboard, and then auto-saved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRegionSelectClipboardAutoSave_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_screenCapture);
            _formRegionSelectWithMouse.MouseSelectionCompleted += _formRegionSelectWithMouse_RegionSelectClipboardAutoSaveMouseSelectionCompleted;
            _formRegionSelectWithMouse.LoadCanvas(sendToClipboard: true);
        }

        /// <summary>
        /// Shows a mouse-driven region selection canvas so you can select a region, have the captured image be sent to the clipboard, auto-saved, and then opened with the default image editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRegionSelectClipboardAutoSaveEdit_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_screenCapture);
            _formRegionSelectWithMouse.MouseSelectionCompleted += _formRegionSelectWithMouse_RegionSelectClipboardAutoSaveEditMouseSelectionCompleted;
            _formRegionSelectWithMouse.LoadCanvas(sendToClipboard: true);
        }

        /// <summary>
        /// Shows a mouse-driven region selection canvas so you can select a region, have the captured image be sent to the clipboard, and then shown in a floating window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRegionSelectClipboardFloatingScreenshot_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_screenCapture);
            _formRegionSelectWithMouse.MouseSelectionCompleted += _formRegionSelectWithMouse_RegionSelectClipboardFloatingScreenshotMouseSelectionCompleted;
            _formRegionSelectWithMouse.LoadCanvas(sendToClipboard: true);
        }

        /// <summary>
        /// Shows a mouse-driven region selection canvas so you can select a region and have the captured image shown in a floating window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRegionSelectFloatingScreenshot_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_screenCapture);
            _formRegionSelectWithMouse.MouseSelectionCompleted += _formRegionSelectWithMouse_RegionSelectFloatingScreenshotMouseSelectionCompleted;
            _formRegionSelectWithMouse.LoadCanvas(sendToClipboard: false);
        }

        private void toolStripMenuItemRegionSelectAddRegion_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_screenCapture);
            _formRegionSelectWithMouse.MouseSelectionCompleted += _formRegionSelectWithMouse_RegionSelectAddRegionMouseSelectionCompleted;
            _formRegionSelectWithMouse.LoadCanvas(sendToClipboard: false);
        }

        private void toolStripMenuItemRegionSelectAddRegionExpress_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_screenCapture);
            _formRegionSelectWithMouse.MouseSelectionCompleted += _formRegionSelectWithMouse_RegionSelectAddRegionExpressMouseSelectionCompleted;
            _formRegionSelectWithMouse.LoadCanvas(sendToClipboard: false);
        }

        /// <summary>
        /// The event method used by "Region Select -> Clipboard / Auto Save".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _formRegionSelectWithMouse_RegionSelectClipboardAutoSaveMouseSelectionCompleted(object sender, EventArgs e)
        {
            Bitmap bitmap = GetBitmapFromRegionSelect();

            if (bitmap != null)
            {
                string autoSaveFolder = _config.Settings.User.GetByKey("AutoSaveFolder").Value.ToString();
                string autoSaveMacro = _config.Settings.User.GetByKey("AutoSaveMacro").Value.ToString();

                // We're about to get the value from the AutoSaveFormat key (which might not be available if we're using settings
                // from an older version of the application) so default to whatever ScreenCapture.ImageFormat provides (most likely JPEG).
                string autoSaveFormat = ScreenCapture.ImageFormat;

                Setting autoSaveFormatSetting = _config.Settings.User.GetByKey("AutoSaveFormat");

                if (autoSaveFormatSetting != null)
                {
                    autoSaveFormat = _config.Settings.User.GetByKey("AutoSaveFormat").Value.ToString();
                }

                ImageFormat imageFormat = new ImageFormat(autoSaveFormat, "." + autoSaveFormat.ToLower());

                DateTime dtNow = DateTime.Now;

                _screenCapture.DateTimeScreenshotsTaken = dtNow;
                _screenCapture.ActiveWindowTitle = "*** Auto Screen Capture - Region Select / Auto Save ***";

                Region region = new Region()
                {
                    ViewId = new Guid(),
                    Name = string.Empty,
                    JpegQuality = 100,
                    Format = imageFormat,
                    Folder = autoSaveFolder,
                    Macro = autoSaveMacro
                };

                SaveScreenshot(bitmap, region, string.Empty);
            }
        }

        /// <summary>
        /// The event method for "Region Select -> Clipboard / Auto Save / Edit".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _formRegionSelectWithMouse_RegionSelectClipboardAutoSaveEditMouseSelectionCompleted(object sender, EventArgs e)
        {
            // Get the name of the default image editor.
            string defaultEditor = _config.Settings.User.GetByKey("DefaultEditor").Value.ToString();

            if (string.IsNullOrEmpty(defaultEditor))
            {
                return;
            }

            // Save the screenshot as an image file using the Auto Save event method.
            _formRegionSelectWithMouse_RegionSelectClipboardAutoSaveMouseSelectionCompleted(sender, e);

            // Run the default image editor.
            Editor editor = _formEditor.EditorCollection.GetByName(defaultEditor);

            if (editor != null)
            {
                RunEditor(editor, _screenshotCollection.GetLastScreenshot());
            }
        }

        /// <summary>
        /// The event method for "Region Select -> Clipboard / Floating Screenshot".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _formRegionSelectWithMouse_RegionSelectClipboardFloatingScreenshotMouseSelectionCompleted(object sender, EventArgs e)
        {
            Bitmap bitmap = GetBitmapFromRegionSelect();

            if (bitmap != null)
            {
                FormFloatingScreenshot formFloatingScreenshot = new FormFloatingScreenshot(bitmap);
                formFloatingScreenshot.Show();
            }
        }

        /// <summary>
        /// The event method for "Region Select -> Floating Screenshot".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _formRegionSelectWithMouse_RegionSelectFloatingScreenshotMouseSelectionCompleted(object sender, EventArgs e)
        {
            Bitmap bitmap = GetBitmapFromRegionSelect();

            if (bitmap != null)
            {
                FormFloatingScreenshot formFloatingScreenshot = new FormFloatingScreenshot(bitmap);
                formFloatingScreenshot.Show();
            }
        }

        /// <summary>
        /// The event method for "Region Select -> Add Region".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _formRegionSelectWithMouse_RegionSelectAddRegionMouseSelectionCompleted(object sender, EventArgs e)
        {
            Bitmap bitmap = GetBitmapFromRegionSelect();

            if (bitmap != null)
            {
                _formRegion.X = _formRegionSelectWithMouse.outputX;
                _formRegion.Y = _formRegionSelectWithMouse.outputY;
                _formRegion.Width = _formRegionSelectWithMouse.outputWidth;
                _formRegion.Height = _formRegionSelectWithMouse.outputHeight;

                addRegion_Click(sender, e);
            }
        }

        /// <summary>
        /// The event method for "Region Select -> Add Region (Express)".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _formRegionSelectWithMouse_RegionSelectAddRegionExpressMouseSelectionCompleted(object sender, EventArgs e)
        {
            Bitmap bitmap = GetBitmapFromRegionSelect();

            if (bitmap != null)
            {
                _formRegion.RegionCollection.Add(new Region()
                {
                    ViewId = Guid.NewGuid(),
                    Name = "Region " + (_formRegion.RegionCollection.Count + 1),
                    Folder = _fileSystem.ScreenshotsFolder,
                    Macro = _fileSystem.FilenamePattern,
                    Format = _imageFormatCollection.GetByName(ScreenCapture.ImageFormat),
                    JpegQuality = 100,
                    Mouse = true,
                    X = _formRegionSelectWithMouse.outputX,
                    Y = _formRegionSelectWithMouse.outputY,
                    Width = _formRegionSelectWithMouse.outputWidth,
                    Height = _formRegionSelectWithMouse.outputHeight,
                    Enable = true,
                    Encrypt = false
                });

                BuildRegionsModule();
                BuildViewTabPages();

                if (!_formRegion.RegionCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// The timer for taking screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerScreenCapture_Tick(object sender, EventArgs e)
        {
            if (_screenCapture.Running)
            {
                if (_screenCapture.Limit >= ScreenCapture.CAPTURE_LIMIT_MIN &&
                    _screenCapture.Limit <= ScreenCapture.CAPTURE_LIMIT_MAX)
                {
                    if (_screenCapture.CycleCount < _screenCapture.Limit)
                    {
                        TakeScreenshot(_screenCapture.Scope);
                    }

                    if (_screenCapture.CycleCount == _screenCapture.Limit)
                    {
                        // Reset the count to avoid falling into an infinite loop.
                        _screenCapture.CycleCount = 0;

                        _log.WriteDebugMessage("Running triggers of condition type LimitReached");
                        RunTriggersOfConditionType(TriggerConditionType.LimitReached);
                    }
                }
                else
                {
                    TakeScreenshot(_screenCapture.Scope);
                }
            }
            else
            {
                StopScreenCapture();
            }
        }

        private bool ActiveWindowTitleMatchText()
        {
            try
            {
                if (!string.IsNullOrEmpty(_screenCapture.ActiveWindowTitle))
                {
                    if (_formSetup.radioButtonCaseSensitiveMatch.Checked)
                    {
                        return _screenCapture.ActiveWindowTitle.Contains(_formSetup.textBoxActiveWindowTitle.Text);
                    }
                    else if (_formSetup.radioButtonCaseInsensitiveMatch.Checked)
                    {
                        return _screenCapture.ActiveWindowTitle.ToLower().Contains(_formSetup.textBoxActiveWindowTitle.Text.ToLower());
                    }
                    else if (_formSetup.radioButtonRegularExpressionMatch.Checked)
                    {
                        return Regex.IsMatch(_screenCapture.ActiveWindowTitle, _formSetup.textBoxActiveWindowTitle.Text);
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("FormMain-ScreenCapture::ActiveWindowTitleMatchText", ex);

                return false;
            }
        }

        private bool ActiveWindowTitleDoesNotMatchText()
        {
            try
            {
                if (!string.IsNullOrEmpty(_screenCapture.ActiveWindowTitle))
                {
                    _formSetup.textBoxActiveWindowTitle.Text = _formSetup.textBoxActiveWindowTitle.Text.Trim();

                    if (_formSetup.radioButtonCaseSensitiveMatch.Checked)
                    {
                        return !_screenCapture.ActiveWindowTitle.Contains(_formSetup.textBoxActiveWindowTitle.Text);
                    }
                    else if (_formSetup.radioButtonCaseInsensitiveMatch.Checked)
                    {
                        return !_screenCapture.ActiveWindowTitle.ToLower().Contains(_formSetup.textBoxActiveWindowTitle.Text.ToLower());
                    }
                    else if (_formSetup.radioButtonRegularExpressionMatch.Checked)
                    {
                        return !Regex.IsMatch(_screenCapture.ActiveWindowTitle, _formSetup.textBoxActiveWindowTitle.Text);
                    }
                    
                }

                return false;
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("FormMain-ScreenCapture::ActiveWindowTitleDoesNotMatchText", ex);

                return false;
            }
        }

        private void SetActiveWindowTitleAsMatch(string activeWindowTitle)
        {
            if (string.IsNullOrEmpty(activeWindowTitle))
            {
                _config.Settings.User.SetValueByKey("ActiveWindowTitleCaptureCheck", false);

                _formSetup.checkBoxActiveWindowTitleComparisonCheck.Checked = false;
            }
            else
            {
                activeWindowTitle = activeWindowTitle.Trim();

                _config.Settings.User.SetValueByKey("ActiveWindowTitleCaptureCheck", true);
                _config.Settings.User.SetValueByKey("ActiveWindowTitleCaptureText", activeWindowTitle);

                _formSetup.checkBoxActiveWindowTitleComparisonCheck.Checked = true;
                _formSetup.textBoxActiveWindowTitle.Text = activeWindowTitle;

                _screenCapture.ActiveWindowTitle = activeWindowTitle;
            }

            if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
            {
                _screenCapture.ApplicationError = true;
            }
        }

        private void SetActiveWindowTitleAsNoMatch(string activeWindowTitle)
        {
            if (string.IsNullOrEmpty(activeWindowTitle))
            {
                _config.Settings.User.SetValueByKey("ActiveWindowTitleNoMatchCheck", false);

                _formSetup.checkBoxActiveWindowTitleComparisonCheckReverse.Checked = false;
            }
            else
            {
                activeWindowTitle = activeWindowTitle.Trim();

                _config.Settings.User.SetValueByKey("ActiveWindowTitleNoMatchCheck", true);
                _config.Settings.User.SetValueByKey("ActiveWindowTitleCaptureText", activeWindowTitle);

                _formSetup.checkBoxActiveWindowTitleComparisonCheckReverse.Checked = true;
                _formSetup.textBoxActiveWindowTitle.Text = activeWindowTitle;

                _screenCapture.ActiveWindowTitle = activeWindowTitle;
            }

            if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
            {
                _screenCapture.ApplicationError = true;
            }
        }

        private void buttonStartStopScreenCapture_Click(object sender, EventArgs e)
        {
            if (_screenCapture.Running)
            {
                StopScreenCapture();
            }
            else
            {
                StartScreenCapture();
            }
        }

        private void buttonStartScreenCaptureForBeginners_Click(object sender, EventArgs e)
        {
            _formSetup.textBoxScreenshotsFolder.Text = _formAutoScreenCaptureForBeginners.textBoxScreenshotsFolder.Text;
            _formSetup.textBoxFilenamePattern.Text = _formAutoScreenCaptureForBeginners.textBoxFilenamePattern.Text;
            _formSetup.numericUpDownHoursInterval.Value = _formAutoScreenCaptureForBeginners.numericUpDownHoursInterval.Value;
            _formSetup.numericUpDownMinutesInterval.Value = _formAutoScreenCaptureForBeginners.numericUpDownMinutesInterval.Value;
            _formSetup.numericUpDownSecondsInterval.Value = _formAutoScreenCaptureForBeginners.numericUpDownSecondsInterval.Value;
            _formSetup.numericUpDownMillisecondsInterval.Value = _formAutoScreenCaptureForBeginners.numericUpDownMillisecondsInterval.Value;

            foreach (Screen screen in _formScreen.ScreenCollection)
            {
                screen.Folder = _formSetup.textBoxScreenshotsFolder.Text;
                screen.Macro = _formSetup.textBoxFilenamePattern.Text;
            }

            _formScreen.ScreenCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log);

            foreach (Region region in _formRegion.RegionCollection)
            {
                region.Folder = _formSetup.textBoxScreenshotsFolder.Text;
                region.Macro = _formSetup.textBoxFilenamePattern.Text;
            }

            _formRegion.RegionCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log);

            // Show the "Screen Capture Status" window.
            Setting showScreenCaptureStatusOnStartSetting = _config.Settings.User.GetByKey("ShowScreenCaptureStatusOnStart");

            if (showScreenCaptureStatusOnStartSetting != null)
            {
                bool showScreenCaptureStatusOnStart = Convert.ToBoolean(showScreenCaptureStatusOnStartSetting.Value);

                if (showScreenCaptureStatusOnStart)
                {
                    toolStripMenuItemScreenCaptureStatus_Click(sender, e);
                }
            }

            StartScreenCapture();
        }

        private void buttonStopScreenCaptureForBeginners_Click(object sender, EventArgs e)
        {
            StopScreenCapture();

            Setting showScreenshotsFolderOnStopSetting = _config.Settings.User.GetByKey("ShowScreenshotsFolderOnStop");

            if (showScreenshotsFolderOnStopSetting != null)
            {
                bool showScreenshotsFolderOnStop = Convert.ToBoolean(showScreenshotsFolderOnStopSetting.Value);

                if (showScreenshotsFolderOnStop)
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo("explorer.exe");
                    processStartInfo.Arguments = _macroParser.ParseTags(_formAutoScreenCaptureForBeginners.textBoxScreenshotsFolder.Text, _formMacroTag.MacroTagCollection, _log);

                    Process process = new Process
                    {
                        StartInfo = processStartInfo,
                    };

                    process.Start();
                }
            }
        }

        private void buttonExitAutoScreenCaptureForBeginners_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }
    }
}