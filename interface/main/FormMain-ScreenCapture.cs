//-----------------------------------------------------------------------
// <copyright file="FormMain-ScreenCapture.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
            return ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value,
                (int)numericUpDownMillisecondsInterval.Value);
        }

        /// <summary>
        /// Figures out if the "Start Capture" controls should be enabled or disabled.
        /// </summary>
        private void EnableStartCapture()
        {
            if (GetScreenCaptureInterval() > 0)
            {
                toolStripSplitButtonStartScreenCapture.Enabled = true;
                toolStripMenuItemStartScreenCapture.Enabled = true;

                groupBoxCaptureDelay.Enabled = true;
                numericUpDownHoursInterval.Enabled = true;
                checkBoxInitialScreenshot.Enabled = true;
                numericUpDownMinutesInterval.Enabled = true;

                labelLimit.Enabled = true;
                checkBoxCaptureLimit.Enabled = true;

                numericUpDownCaptureLimit.Enabled = true;
                numericUpDownSecondsInterval.Enabled = true;
                numericUpDownMillisecondsInterval.Enabled = true;

                labelKeepScreenshots.Enabled = true;
                labelDays.Enabled = true;
                numericUpDownKeepScreenshotsForDays.Enabled = true;

                checkBoxScreenshotLabel.Enabled = true;
                comboBoxScreenshotLabel.Enabled = true;

                groupBoxActiveWindowTitle.Enabled = true;
                checkBoxActiveWindowTitle.Enabled = true;
                textBoxActiveWindowTitle.Enabled = true;
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
            toolStripSplitButtonStopScreenCapture.Enabled = true;
            toolStripMenuItemStopScreenCapture.Enabled = true;

            groupBoxCaptureDelay.Enabled = false;
            numericUpDownHoursInterval.Enabled = false;
            checkBoxInitialScreenshot.Enabled = false;
            numericUpDownMinutesInterval.Enabled = false;

            labelLimit.Enabled = false;
            checkBoxCaptureLimit.Enabled = false;

            numericUpDownCaptureLimit.Enabled = false;
            numericUpDownSecondsInterval.Enabled = false;
            numericUpDownMillisecondsInterval.Enabled = false;

            labelKeepScreenshots.Enabled = false;
            labelDays.Enabled = false;
            numericUpDownKeepScreenshotsForDays.Enabled = false;

            checkBoxScreenshotLabel.Enabled = false;
            comboBoxScreenshotLabel.Enabled = false;

            groupBoxActiveWindowTitle.Enabled = false;
            checkBoxActiveWindowTitle.Enabled = false;
            textBoxActiveWindowTitle.Enabled = false;
        }

        /// <summary>
        /// Disables the "Stop Capture" controls.
        /// </summary>
        private void DisableStopCapture()
        {
            toolStripSplitButtonStopScreenCapture.Enabled = false;
            toolStripMenuItemStopScreenCapture.Enabled = false;
        }

        /// <summary>
        /// Disables the "Start Capture" controls.
        /// </summary>
        private void DisableStartCapture()
        {
            toolStripSplitButtonStartScreenCapture.Enabled = false;
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
            if (checkBoxCaptureLimit.Checked)
            {
                numericUpDownCaptureLimit.Enabled = true;

                _screenCapture.Count = 0;
                _screenCapture.Limit = (int)numericUpDownCaptureLimit.Value;
            }
            else
            {
                numericUpDownCaptureLimit.Enabled = false;
            }
        }

        /// <summary>
        /// Takes a screenshot of each available region and screen.
        /// </summary>
        private void TakeScreenshot(bool captureNow)
        {
            _formScreen.RefreshScreenDictionary();

            if (_screenCapture.GetScreenImages(0, 0, 0, 0, 0, false, 100, out _))
            {
                _screenCapture.Count++;
                _screenCapture.CaptureNow = captureNow;

                DateTime dtNow = DateTime.Now;

                _screenCapture.DateTimeScreenshotsTaken = dtNow;

                if (!captureNow)
                {
                    _screenCapture.DateTimePreviousCycle = dtNow;
                }

                ScreenCapture.SetApplicationFocus(comboBoxProcessList.Text);

                _screenCapture.ActiveWindowTitle = _screenCapture.GetActiveWindowTitle();

                _screenCapture.ActiveWindowProcessName = _screenCapture.GetActiveWindowProcessName();

                RunRegionCaptures();

                RunScreenCaptures();
            }
        }

        private void ScreenshotTakenWithSuccess()
        {
            Log.WriteDebugMessage("Running triggers of condition type ScreenshotTaken");
            RunTriggersOfConditionType(TriggerConditionType.ScreenshotTaken);
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
                Log.WriteDebugMessage("Starting a screen capture session");

                if (!_screenCapture.Running && screenCaptureInterval > 0)
                {
                    // Increment the number of times the user has started a screen capture session.
                    int startScreenCaptureCount = Convert.ToInt32(Settings.User.GetByKey("StartScreenCaptureCount", DefaultSettings.StartScreenCaptureCount).Value);
                    startScreenCaptureCount++;
                    Settings.User.SetValueByKey("StartScreenCaptureCount", startScreenCaptureCount);

                    // Turn off "FirstRun" after the first run of a screen capture session so we longer show balloon tips.
                    if (startScreenCaptureCount > 1)
                    {
                        Settings.User.SetValueByKey("FirstRun", false);
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
                    _screenCapture.Limit = checkBoxCaptureLimit.Checked ? (int)numericUpDownCaptureLimit.Value : 0;

                    if (Settings.User.GetByKey("Passphrase", DefaultSettings.Passphrase).Value.ToString().Length > 0)
                    {
                        ScreenCapture.LockScreenCaptureSession = true;
                    }
                    else
                    {
                        ScreenCapture.LockScreenCaptureSession = false;
                    }

                    Log.WriteMessage("Starting screen capture");

                    _screenCapture.Running = true;
                    _screenCapture.ApplicationWarning = false;

                    _screenCapture.DateTimeStartCapture = DateTime.Now;

                    if (checkBoxInitialScreenshot.Checked)
                    {
                        Log.WriteMessage("Taking initial screenshots");

                        TakeScreenshot(captureNow: false);
                    }

                    // Start taking screenshots.

                    timerScreenCapture.Interval = screenCaptureInterval;

                    if (notifyIcon.Visible && !_screenCapture.ApplicationError && !checkBoxInitialScreenshot.Checked && screenCaptureInterval > BALLOON_TIP_TIMEOUT)
                    {
                        SystemTrayBalloonMessage("The system tray icon turns green when taking screenshots. To stop, right-click on the icon and select Stop Screen Capture");
                    }

                    timerScreenCapture.Enabled = true;
                    timerScreenCapture.Start();

                    Log.WriteDebugMessage("Running triggers of condition type ScreenCaptureStarted");
                    RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStarted);
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                Log.WriteExceptionMessage("FormMain-ScreenCapture::StartScreenCapture", ex);
            }
        }

        /// <summary>
        /// Stops the screen capture session that's currently running.
        /// </summary>
        private void StopScreenCapture()
        {
            try
            {
                Log.WriteMessage("Stopping screen capture");

                if (ScreenCapture.LockScreenCaptureSession && !_formEnterPassphrase.Visible)
                {
                    Log.WriteDebugMessage("Screen capture session is locked. Challenging user to enter correct passphrase to unlock");
                    _formEnterPassphrase.ShowDialog(this);
                }

                // This is intentional. Do not rewrite these statements as an if/else
                // because as soon as lockScreenCaptureSession is set to false we want
                // to continue with normal functionality.
                if (!ScreenCapture.LockScreenCaptureSession)
                {
                    Settings.User.GetByKey("Passphrase", DefaultSettings.Passphrase).Value = string.Empty;
                    SaveSettings();

                    DisableStopCapture();
                    EnableStartCapture();

                    _screenCapture.Count = 0;
                    _screenCapture.Running = false;
                    _screenCapture.DateTimePreviousCycle = DateTime.MinValue;

                    timerScreenCapture.Stop();
                    timerScreenCapture.Enabled = false;

                    SearchFilterValues();
                    SearchDates();

                    Log.WriteDebugMessage("Running triggers of condition type ScreenCaptureStopped");
                    RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStopped);
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                Log.WriteExceptionMessage("FormMain-ScreenCapture::StopScreenCapture", ex);
            }
        }

        private void CaptureNowArchive()
        {
            TakeScreenshot(captureNow: true);
        }

        private void CaptureNowEdit()
        {
            string defaultEditor = Settings.User.GetByKey("DefaultEditor", DefaultSettings.DefaultEditor).Value.ToString();

            if (string.IsNullOrEmpty(defaultEditor))
            {
                return;
            }

            TakeScreenshot(captureNow: true);

            Editor editor = _formEditor.EditorCollection.GetByName(defaultEditor);

            if (editor != null)
            {
                RunEditor(editor, TriggerActionType.RunEditor);
            }
        }

        private void ScreenshotTakenWithFailure()
        {
            Log.WriteMessage("Application encountered error while taking a screenshot. Stopping screen capture");
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
        /// Shows a mouse-driven region selection canvas so you can select a region and then save the captured image to the clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRegionSelectClipboard_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse();
            _formRegionSelectWithMouse.LoadCanvas(outputMode: 1); // 1 is for saving the captured image to the clipboard
        }

        /// <summary>
        /// Shows a mouse-driven region selection canvas so you can select a region and then auto-save the captured image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRegionSelectAutoSave_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse();
            _formRegionSelectWithMouse.MouseSelectionCompleted += _formRegionSelectWithMouse_RegionSelectAutoSaveMouseSelectionCompleted;
            _formRegionSelectWithMouse.LoadCanvas(outputMode: 0); // 0 is for acquiring the dimensions and resolution
        }

        /// <summary>
        /// Shows a mouse-driven region selection canvas so you can select a region and then edit the captured image with the default image editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRegionSelectEdit_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse();
            _formRegionSelectWithMouse.MouseSelectionCompleted += _formRegionSelectWithMouse_RegionSelectEditMouseSelectionCompleted;
            _formRegionSelectWithMouse.LoadCanvas(outputMode: 0); // 0 is for acquiring the dimensions and resolution
        }

        /// <summary>
        /// The event method used by "Region Select / Auto Save".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _formRegionSelectWithMouse_RegionSelectAutoSaveMouseSelectionCompleted(object sender, EventArgs e)
        {
            int x = _formRegionSelectWithMouse.outputX + 1;
            int y = _formRegionSelectWithMouse.outputY + 1;
            int width = _formRegionSelectWithMouse.outputWidth - 2;
            int height = _formRegionSelectWithMouse.outputHeight - 2;

            string autoSaveFolder = textBoxAutoSaveFolder.Text;
            string autoSaveMacro = textBoxAutoSaveMacro.Text;

            ImageFormat imageFormat = new ImageFormat(ImageFormatSpec.NAME_JPEG, ImageFormatSpec.EXTENSION_JPEG);

            if (_screenCapture.GetScreenImages(-1, x, y, width, height, mouse: false, resolutionRatio: 100, out Bitmap bitmap))
            {
                DateTime dtNow = DateTime.Now;

                _screenCapture.DateTimeScreenshotsTaken = dtNow;
                _screenCapture.ActiveWindowTitle = "*** Auto Screen Capture - Region Select / Auto Save ***";

                if (_screenCapture.SaveScreenshot(
                    path: FileSystem.CorrectScreenshotsFolderPath(MacroParser.ParseTags(config: false, autoSaveFolder, _formTag.TagCollection)) + MacroParser.ParseTags(preview: false, config: false, DateTime.Now.ToString(MacroParser.DateFormat), autoSaveMacro, -1, imageFormat, _screenCapture.ActiveWindowTitle, _formTag.TagCollection),
                    format: imageFormat,
                    component: -1,
                    screenshotType: ScreenshotType.Region,
                    jpegQuality: 100,
                    viewId: new Guid(),
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

        /// <summary>
        /// The event method for "Region Select / Edit".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _formRegionSelectWithMouse_RegionSelectEditMouseSelectionCompleted(object sender, EventArgs e)
        {
            // Get the name of the default image editor.
            string defaultEditor = Settings.User.GetByKey("DefaultEditor", DefaultSettings.DefaultEditor).Value.ToString();

            if (string.IsNullOrEmpty(defaultEditor))
            {
                return;
            }

            // Save the screenshot as an image file using the Auto Save event method.
            _formRegionSelectWithMouse_RegionSelectAutoSaveMouseSelectionCompleted(sender, e);

            // Run the default image editor.
            Editor editor = _formEditor.EditorCollection.GetByName(defaultEditor);

            if (editor != null)
            {
                RunEditor(editor, TriggerActionType.RunEditor);
            }
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
                        Log.WriteDebugMessage("Running triggers of condition type LimitReached");
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
    }
}