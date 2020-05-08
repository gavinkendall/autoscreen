//-----------------------------------------------------------------------
// <copyright file="FormMain-SystemTrayIcon.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Methods for what happens when the user selects a menu item from the system tray icon or for displaying information in the system tray icon's tooltip.</summary>
//-----------------------------------------------------------------------
using AutoScreenCapture.Properties;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        private const int BALLOON_TIP_TIMEOUT = 20000;

        private void ContextMenuStripSystemTrayIcon_Opening(object sender, CancelEventArgs e)
        {
            PopulateLabelList();
        }

        /// <summary>
        /// Exits the application from the system tray icon menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Visible)
            {
                HideInterface();
            }
            else
            {
                ShowInterface();
            }
        }

        private void SystemTrayBalloonMessage(string message)
        {
            SystemTrayBalloonMessage(message, BALLOON_TIP_TIMEOUT);
        }

        private void SystemTrayBalloonMessage(string message, int balloonTipTimeout)
        {
            // Show the balloon tip only if the system tray icon is visible and the "BoolFirstRun" setting returns "True".

            bool.TryParse(Settings.User.GetByKey("BoolFirstRun", defaultValue: true).Value.ToString(), out bool firstRun);

            if (notifyIcon.Visible && firstRun)
            {
                notifyIcon.ShowBalloonTip(balloonTipTimeout, Settings.ApplicationName, message, ToolTipIcon.Info);
            }
        }

        private void HideSystemTrayIcon()
        {
            notifyIcon.Visible = false;
        }

        private void SystemTrayIconStatusNormal()
        {
            notifyIcon.Icon = Resources.autoscreen;
        }

        private void SystemTrayIconStatusRunning()
        {
            if (!checkBoxInitialScreenshot.Checked && timerScreenCapture.Interval > BALLOON_TIP_TIMEOUT)
            {
                SystemTrayBalloonMessage("The system tray icon turns green when taking screenshots. To stop, right-click on the icon and select Stop Screen Capture");
            }

            notifyIcon.Icon = Resources.autoscreen_running;
        }

        private void ShowInfo()
        {
            notifyIcon.Text = string.Empty;
            toolStripInfo.Text = string.Empty;

            if (_screenCapture.Running && !_screenCapture.CaptureError)
            {
                string remainingTimeStr = string.Empty;

                int remainingHours = _screenCapture.TimeRemainingForNextScreenshot.Hours;
                int remainingMinutes = _screenCapture.TimeRemainingForNextScreenshot.Minutes;
                int remainingSeconds = _screenCapture.TimeRemainingForNextScreenshot.Seconds;

                string remainingHoursStr = (remainingHours > 0
                    ? remainingHours.ToString() + " hour" + (remainingHours > 1 ? "s" : string.Empty) + ", "
                    : string.Empty);

                string remainingMinutesStr = (remainingMinutes > 0
                    ? remainingMinutes.ToString() + " minute" + (remainingMinutes > 1 ? "s" : string.Empty) + ", "
                    : string.Empty);

                string remainingSecondsStr = (remainingSeconds > 0
                    ? remainingSeconds.ToString() + " second" + (remainingSeconds > 1 ? "s" : string.Empty)
                    : string.Empty);

                if (remainingSeconds > 0)
                {
                    remainingTimeStr = "Next capture in " +
                        remainingHoursStr + remainingMinutesStr + remainingSecondsStr + " at " +
                        _screenCapture.DateTimeNextCycle.ToLongTimeString();
                }

                notifyIcon.Text = remainingTimeStr;
                toolStripInfo.Text = remainingTimeStr;
            }
        }
    }
}