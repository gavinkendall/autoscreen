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
        /// <summary>
        /// Takes a screenshot of each available region and screen.
        /// </summary>
        private void TakeScreenshot()
        {
            formScreen.RefreshScreenDictionary();

            if (_screenCapture.GetScreenImages(0, 0, 0, 0, 0, false, 100, out Bitmap bitmap))
            {
                _screenCapture.Count++;

                _screenCapture.DateTimePreviousCycle = DateTime.Now;

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

                    TakeScreenshot();
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
                        TakeScreenshot();
                    }

                    if (_screenCapture.Count == _screenCapture.Limit)
                    {
                        Log.Write("Running triggers of condition type LimitReached");
                        RunTriggersOfConditionType(TriggerConditionType.LimitReached);
                    }
                }
                else
                {
                    TakeScreenshot();
                }
            }
            else
            {
                StopScreenCapture();
            }
        }
    }
}