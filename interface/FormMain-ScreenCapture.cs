//-----------------------------------------------------------------------
// <copyright file="FormMain-ScreenCapture.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods related to capturing screens and what we should do when buttons are clicked or menu items are selected.</summary>
//-----------------------------------------------------------------------
using System;
using System.Windows.Forms;
using AutoScreenCapture.Properties;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
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
        /// Starts a screen capture session.
        /// </summary>
        private void StartScreenCapture()
        {
            try
            {
                Log.WriteDebugMessage("Starting a screen capture session");

                int screenCaptureInterval = GetScreenCaptureInterval();

                if (!_screenCapture.Running && screenCaptureInterval > 0)
                {
                    // Increment the number of times the user has started a screen capture session.
                    int startScreenCaptureCount = Convert.ToInt32(Settings.User.GetByKey("IntStartScreenCaptureCount", DefaultSettings.IntStartScreenCaptureCount).Value);
                    startScreenCaptureCount++;
                    Settings.User.SetValueByKey("IntStartScreenCaptureCount", startScreenCaptureCount);

                    // Turn off "BoolFirstRun" after the first run of a screen capture session so we longer show balloon tips.
                    if (startScreenCaptureCount > 1)
                    {
                        Settings.User.SetValueByKey("BoolFirstRun", false);
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

                    if (Settings.User.GetByKey("StringPassphrase", DefaultSettings.StringPassphrase).Value.ToString().Length > 0)
                    {
                        ScreenCapture.LockScreenCaptureSession = true;
                    }
                    else
                    {
                        ScreenCapture.LockScreenCaptureSession = false;
                    }

                    Log.WriteMessage("Starting screen capture");

                    _screenCapture.Running = true;

                    _screenCapture.DateTimeStartCapture = DateTime.Now;

                    if (checkBoxInitialScreenshot.Checked)
                    {
                        Log.WriteMessage("Taking initial screenshots");

                        TakeScreenshot(captureNow: false);
                    }

                    // Start taking screenshots.

                    timerScreenCapture.Interval = screenCaptureInterval;

                    if (!_screenCapture.ApplicationError && !checkBoxInitialScreenshot.Checked && screenCaptureInterval > BALLOON_TIP_TIMEOUT)
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
                    Settings.User.GetByKey("StringPassphrase", DefaultSettings.StringPassphrase).Value = string.Empty;
                    SaveSettings();

                    DisableStopCapture();
                    EnableStartCapture();

                    _screenCapture.Count = 0;
                    _screenCapture.Running = false;

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
                Log.WriteExceptionMessage("FormMain-ScreenCapture::StopScreenCapture", ex);
            }
        }

        private void CaptureNowArchive()
        {
            TakeScreenshot(captureNow: true);
        }

        private void CaptureNowEdit()
        {
            string defaultEditor = Settings.User.GetByKey("StringDefaultEditor", DefaultSettings.StringDefaultEditor).Value.ToString();

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
            FormRegionSelectWithMouse _formRegionSelectWithMouse = new FormRegionSelectWithMouse();
            _formRegionSelectWithMouse.LoadCanvas(outputMode: 1); // 1 is for saving the captured image to the clipboard
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