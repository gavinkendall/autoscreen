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
                (int)_formSetup.numericUpDownMinutesInterval.Value, (int)_formSetup.numericUpDownSecondsInterval.Value,
                (int)_formSetup.numericUpDownMillisecondsInterval.Value);
        }

        /// <summary>
        /// Figures out if the "Start Capture" controls should be enabled or disabled.
        /// </summary>
        private void EnableStartCapture()
        {
            if (GetScreenCaptureInterval() > 0)
            {
                toolStripDropDownButtonStartScreenCapture.Enabled = true;
                toolStripMenuItemStartScreenCapture.Enabled = true;
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
            toolStripDropDownButtonStopScreenCapture.Enabled = true;
            toolStripMenuItemStopScreenCapture.Enabled = true;
        }

        /// <summary>
        /// Disables the "Stop Capture" controls.
        /// </summary>
        private void DisableStopCapture()
        {
            toolStripDropDownButtonStopScreenCapture.Enabled = false;
            toolStripMenuItemStopScreenCapture.Enabled = false;
        }

        /// <summary>
        /// Disables the "Start Capture" controls.
        /// </summary>
        private void DisableStartCapture()
        {
            toolStripDropDownButtonStartScreenCapture.Enabled = false;
            toolStripMenuItemStartScreenCapture.Enabled = false;
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
                _screenCapture.Count = 0;
                _screenCapture.Limit = (int)_formSetup.numericUpDownCaptureLimit.Value;
            }
        }

        /// <summary>
        /// Takes a screenshot of each available region and screen.
        /// </summary>
        private void TakeScreenshot(bool captureNow)
        {
            RunTriggersOfConditionType(TriggerConditionType.BeforeScreenshotTaken);

            // Test to see if we can get images of the screen before continuing.
            if (_screenCapture.GetScreenImages(0, 0, 0, autoAdapt: false, 0, 0, 0, 0, 0, false, out _))
            {
                _macroParser.screenCapture = _screenCapture;

                _screenCapture.Count++;
                _screenCapture.CaptureNow = captureNow;

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

                RunRegionCaptures();

                RunScreenCaptures();
            }
        }

        /// <summary>
        /// Starts a screen capture session with the default screen capture interval.
        /// </summary>
        private void StartScreenCapture()
        {
            int screenCaptureInterval = GetScreenCaptureInterval();

            StartScreenCapture(screenCaptureInterval);
        }

        /// <summary>
        /// Starts a screen capture session with a defined screen capture interval.
        /// </summary>
        /// <param name="screenCaptureInterval">The screen capture interval to use.</param>
        private void StartScreenCapture(int screenCaptureInterval)
        {
            try
            {
                if (!_screenCapture.Running && screenCaptureInterval > 0)
                {
                    // Increment the number of times the user has started a screen capture session.
                    int startScreenCaptureCount = Convert.ToInt32(_config.Settings.User.GetByKey("StartScreenCaptureCount", _config.Settings.DefaultSettings.StartScreenCaptureCount).Value);
                    startScreenCaptureCount++;
                    _config.Settings.User.SetValueByKey("StartScreenCaptureCount", startScreenCaptureCount);

                    // Turn off "FirstRun" on the first run.
                    if (startScreenCaptureCount == 1)
                    {
                        _config.Settings.User.SetValueByKey("FirstRun", false);
                    }

                    SaveSettings();

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

                    _log.WriteMessage("Starting screen capture");

                    _screenCapture.Running = true;
                    _screenCapture.ApplicationWarning = false;

                    _formLabelSwitcher.buttonStartStopScreenCapture.Image = Properties.Resources.stop_screen_capture;

                    _screenCapture.DateTimeStartCapture = DateTime.Now;

                    if (_formSetup.checkBoxInitialScreenshot.Checked)
                    {
                        _log.WriteMessage("Taking initial screenshots");

                        TakeScreenshot(captureNow: false);
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
                _log.WriteMessage("Stopping screen capture");

                string passphrase = _config.Settings.User.GetByKey("Passphrase", _config.Settings.DefaultSettings.Passphrase).Value.ToString();

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

                        return;
                    }

                    _screenCapture.LockScreenCaptureSession = false;
                }

                DisableStopCapture();
                EnableStartCapture();

                _screenCapture.Count = 0;
                _screenCapture.Running = false;
                _screenCapture.DateTimePreviousCycle = DateTime.MinValue;

                timerScreenCapture.Stop();
                timerScreenCapture.Enabled = false;

                _formLabelSwitcher.buttonStartStopScreenCapture.Image = Properties.Resources.start_screen_capture;

                SearchFilterValues();
                SearchDates();

                _log.WriteDebugMessage("Running triggers of condition type ScreenCaptureStopped");
                RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStopped);
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-ScreenCapture::StopScreenCapture", ex);
            }
        }

        private void CaptureNowArchive()
        {
            TakeScreenshot(captureNow: true);
        }

        private void CaptureNowEdit()
        {
            string defaultEditor = _config.Settings.User.GetByKey("DefaultEditor", _config.Settings.DefaultSettings.DefaultEditor).Value.ToString();

            if (string.IsNullOrEmpty(defaultEditor))
            {
                return;
            }

            TakeScreenshot(captureNow: true);

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
        /// <returns>Returns true if we were successful at saving the screenshot. Returns false if we were unsuccessful at saving the screenshot.</returns>
        private bool SaveScreenshot(Bitmap bitmap, object screenOrRegion)
        {
            if (bitmap == null || screenOrRegion == null)
            {
                return false;
            }

            Guid viewId = Guid.Empty;
            string folder = string.Empty;
            string macro = string.Empty;
            ImageFormat format = new ImageFormat("JPEG", "jpeg");
            int jpegQuality = 100;
            bool encrypt = false;

            if (screenOrRegion is Screen screen)
            {
                viewId = screen.ViewId;
                folder = screen.Folder;
                macro = screen.Macro;
                format = screen.Format;
                jpegQuality = screen.JpegQuality;
                encrypt = screen.Encrypt;
            }

            if (screenOrRegion is Region region)
            {
                viewId = region.ViewId;
                folder = region.Folder;
                macro = region.Macro;
                format = region.Format;
                jpegQuality = region.JpegQuality;
                encrypt = region.Encrypt;
            }

            string label = string.Empty;
            bool applyScreenshotLabel = Convert.ToBoolean(_config.Settings.User.GetByKey("ApplyScreenshotLabel", _config.Settings.DefaultSettings.ApplyScreenshotLabel).Value);

            if (applyScreenshotLabel)
            {
                label = _config.Settings.User.GetByKey("ScreenshotLabel", _config.Settings.DefaultSettings.ScreenshotLabel).Value.ToString();
            }

            // The screenshot's entire path consists of the folder path and the macro (which is just the filename with all of the macro tags parsed; in other words you could have "C:\screenshots\%date%.%format%" where %date% and %format% are macro tags for the filename's macro).
            string path = _fileSystem.CorrectScreenshotsFolderPath(_macroParser.ParseTags(preview: false, folder, screenOrRegion, _screenCapture.ActiveWindowTitle, _screenCapture.ActiveWindowProcessName, label, _formMacroTag.MacroTagCollection, _log)) + // Folder path
                _macroParser.ParseTags(preview: false, macro, screenOrRegion, _screenCapture.ActiveWindowTitle, _screenCapture.ActiveWindowProcessName, label, _formMacroTag.MacroTagCollection, _log); // Filename path

            Screenshot screenshot = new Screenshot(_screenCapture.ActiveWindowTitle, _screenCapture.DateTimeScreenshotsTaken, _macroParser, _config)
            {
                ViewId = viewId,
                Path = path,
                Bitmap = bitmap,
                Format = format,
                ProcessName = _screenCapture.ActiveWindowProcessName + ".exe",
                Label = label,
                Encrypt = encrypt
            };

            int errorLevel = _screenCapture.SaveScreenshot(_security, jpegQuality, screenshot, _screenshotCollection);

            // ScreenSavingErrorLevels enum written by Oskars Grauzis (https://github.com/grauziitisos)
            bool screenSaved = errorLevel == (int)ScreenSavingErrorLevels.None ||
                    // Here we list the error levels that should not stop the capturing session. Writing !=0 is shorther than == flag
                    (errorLevel & (int)ScreenSavingErrorLevels.HashDuplicate) != 0 ||
                    (errorLevel & (int)ScreenSavingErrorLevels.PathLengthExceeded) != 0 ||
                    (errorLevel & (int)ScreenSavingErrorLevels.DriveNotReady) != 0;

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
            _log.WriteDebugMessage("Running triggers of condition type ScreenshotTaken");

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
        /// Shows the Region Select Options form.
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
            int x = _formRegionSelectWithMouse.outputX + 1;
            int y = _formRegionSelectWithMouse.outputY + 1;
            int width = _formRegionSelectWithMouse.outputWidth - 2;
            int height = _formRegionSelectWithMouse.outputHeight - 2;

            if (_screenCapture.GetScreenImages(-1, -1, 0, autoAdapt: false, x, y, width, height, resolutionRatio: 100, mouse: false, out Bitmap bitmap))
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
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_log, _screenCapture);
            _formRegionSelectWithMouse.LoadCanvas(sendToClipboard: true);
        }

        /// <summary>
        /// Shows a mouse-driven region selection canvas so you can select a region, have the captured image be sent to the clipboard, and then auto-saved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRegionSelectClipboardAutoSave_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_log, _screenCapture);
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
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_log, _screenCapture);
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
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_log, _screenCapture);
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
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse(_log, _screenCapture);
            _formRegionSelectWithMouse.MouseSelectionCompleted += _formRegionSelectWithMouse_RegionSelectFloatingScreenshotMouseSelectionCompleted;
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
                string autoSaveFolder = _config.Settings.User.GetByKey("AutoSaveFolder", _config.Settings.DefaultSettings.AutoSaveFolder).Value.ToString();
                string autoSaveMacro = _config.Settings.User.GetByKey("AutoSaveMacro", _config.Settings.DefaultSettings.AutoSaveMacro).Value.ToString();
                string autoSaveFormat = _config.Settings.User.GetByKey("AutoSaveFormat", _config.Settings.DefaultSettings.AutoSaveFormat).Value.ToString();

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

                SaveScreenshot(bitmap, region);
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
            string defaultEditor = _config.Settings.User.GetByKey("DefaultEditor", _config.Settings.DefaultSettings.DefaultEditor).Value.ToString();

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
                    if (_screenCapture.Count < _screenCapture.Limit)
                    {
                        TakeScreenshot(captureNow: false);
                    }

                    if (_screenCapture.Count == _screenCapture.Limit)
                    {
                        _log.WriteDebugMessage("Running triggers of condition type LimitReached");
                        RunTriggersOfConditionType(TriggerConditionType.LimitReached);
                    }
                }
                else
                {
                    TakeScreenshot(captureNow: false);
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

        private void _formLabelSwitcher_buttonStartStopScreenCapture_Click(object sender, EventArgs e)
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
    }
}