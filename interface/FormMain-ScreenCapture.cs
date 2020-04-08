using System;
using System.Drawing;
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
                checkBoxCaptureLimit.Enabled = true;
                numericUpDownCaptureLimit.Enabled = true;
                numericUpDownSecondsInterval.Enabled = true;
                numericUpDownMillisecondsInterval.Enabled = true;

                labelKeepScreenshots.Enabled = true;
                labelDays.Enabled = true;
                numericUpDownKeepScreenshotsForDays.Enabled = true;

                checkBoxScreenshotLabel.Enabled = true;
                comboBoxScreenshotLabel.Enabled = true;
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
            checkBoxCaptureLimit.Enabled = false;
            numericUpDownCaptureLimit.Enabled = false;
            numericUpDownSecondsInterval.Enabled = false;
            numericUpDownMillisecondsInterval.Enabled = false;

            labelKeepScreenshots.Enabled = false;
            labelDays.Enabled = false;
            numericUpDownKeepScreenshotsForDays.Enabled = false;

            checkBoxScreenshotLabel.Enabled = false;
            comboBoxScreenshotLabel.Enabled = false;
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
        private void TakeScreenshot(bool userInterruptCapture)
        {
            formScreen.RefreshScreenDictionary();

            if (_screenCapture.GetScreenImages(0, 0, 0, 0, 0, false, 100, out _))
            {
                _screenCapture.Count++;

                DateTime dtNow = DateTime.Now;

                _screenCapture.DateTimeScreenshotsTaken = dtNow;

                if (!userInterruptCapture)
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
            Log.Write("Running triggers of condition type ScreenshotTaken");
            RunTriggersOfConditionType(TriggerConditionType.ScreenshotTaken);
        }

        /// <summary>
        /// Starts a screen capture session.
        /// </summary>
        private void StartScreenCapture()
        {
            int screenCaptureInterval = GetScreenCaptureInterval();

            if (!_screenCapture.Running && screenCaptureInterval > 0)
            {
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
                _screenCapture.Delay = screenCaptureInterval;
                _screenCapture.Limit = checkBoxCaptureLimit.Checked ? (int)numericUpDownCaptureLimit.Value : 0;

                if (Settings.User.GetByKey("StringPassphrase", defaultValue: string.Empty).Value.ToString().Length > 0)
                {
                    ScreenCapture.LockScreenCaptureSession = true;
                }
                else
                {
                    ScreenCapture.LockScreenCaptureSession = false;
                }

                Log.Write("Starting screen capture");

                _screenCapture.Running = true;

                notifyIcon.Icon = Resources.autoscreen_running;

                _screenCapture.DateTimeStartCapture = DateTime.Now;

                if (checkBoxInitialScreenshot.Checked)
                {
                    Log.Write("Taking initial screenshots");

                    TakeScreenshot(userInterruptCapture: false);
                }

                // Start taking screenshots.

                timerScreenCapture.Interval = screenCaptureInterval;

                Log.Write("Running triggers of condition type ScreenCaptureStarted");
                RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStarted);
            }
        }

        /// <summary>
        /// Stops the screen capture session that's currently running.
        /// </summary>
        private void StopScreenCapture()
        {
            if (_screenCapture.Running)
            {
                Log.Write("Stopping screen capture");

                if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
                {
                    Log.Write("Screen capture session is locked. Challenging user to enter correct passphrase to unlock");
                    formEnterPassphrase.ShowDialog(this);
                }

                // This is intentional. Do not rewrite these statements as an if/else
                // because as soon as lockScreenCaptureSession is set to false we want
                // to continue with normal functionality.
                if (!ScreenCapture.LockScreenCaptureSession)
                {
                    Settings.User.GetByKey("StringPassphrase", defaultValue: false).Value = string.Empty;
                    SaveSettings();

                    DisableStopCapture();
                    EnableStartCapture();

                    _screenCapture.Count = 0;
                    _screenCapture.Running = false;
                    
                    notifyIcon.Icon = Resources.autoscreen;

                    SearchFilterValues();
                    SearchDates();

                    Log.Write("Running triggers of condition type ScreenCaptureStopped");
                    RunTriggersOfConditionType(TriggerConditionType.ScreenCaptureStopped);
                }
            }
        }

        private void CaptureNowArchive()
        {
            TakeScreenshot(userInterruptCapture: true);
        }

        private void CaptureNowEdit()
        {
            string defaultEditor = Settings.User.GetByKey("StringDefaultEditor", defaultValue: string.Empty).Value.ToString();

            if (string.IsNullOrEmpty(defaultEditor))
            {
                return;
            }

            TakeScreenshot(userInterruptCapture: true);

            Editor editor = formEditor.EditorCollection.GetByName(defaultEditor);

            if (editor != null)
            {
                RunEditor(editor, TriggerActionType.RunEditor);
            }
        }

        private void ScreenshotTakenWithFailure()
        {
            Log.Write("Application encountered error while taking a screenshot. Stopping screen capture");
            StopScreenCapture();
        }

        /// <summary>
        /// Starts a screen capture session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemStartScreenCapture(object sender, EventArgs e)
        {
            StartScreenCapture();
        }

        /// <summary>
        /// Stops the currently running screen capture session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemStopScreenCapture(object sender, EventArgs e)
        {
            StopScreenCapture();
        }

        /// <summary>
        /// Takes screenshots and saves them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemCaptureNowArchive(object sender, EventArgs e)
        {
            CaptureNowArchive();
        }

        /// <summary>
        /// Takes screenshots, saves them, and then opens those screenshots in the default editor. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemCaptureNowEdit(object sender, EventArgs e)
        {
            CaptureNowEdit();
        }

        /// <summary>
        /// The timer for taking screenshots.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScreenCapture(object sender, EventArgs e)
        {
            if (_screenCapture.Running)
            {
                if (_screenCapture.Limit >= ScreenCapture.CAPTURE_LIMIT_MIN &&
                    _screenCapture.Limit <= ScreenCapture.CAPTURE_LIMIT_MAX)
                {
                    if (_screenCapture.Count < _screenCapture.Limit)
                    {
                        TakeScreenshot(userInterruptCapture: false);
                    }

                    if (_screenCapture.Count == _screenCapture.Limit)
                    {
                        Log.Write("Running triggers of condition type LimitReached");
                        RunTriggersOfConditionType(TriggerConditionType.LimitReached);
                    }
                }
                else
                {
                    TakeScreenshot(userInterruptCapture: false);
                }
            }
            else
            {
                StopScreenCapture();
            }
        }
    }
}