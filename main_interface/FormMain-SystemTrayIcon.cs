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
        /// Displays the remaining time for when the next screenshot will be taken
        /// when the mouse pointer moves over the system tray icon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (_screenCapture.Running)
            {
                if (_screenCapture.PerformingMaintenance)
                {
                    notifyIcon.Text = "Performing maintenance ...";
                }
                else
                {
                    int remainingHours = _screenCapture.TimeRemainingForNextScreenshot.Hours;
                    int remainingMinutes = _screenCapture.TimeRemainingForNextScreenshot.Minutes;
                    int remainingSeconds = _screenCapture.TimeRemainingForNextScreenshot.Seconds;
                    int remainingMilliseconds = _screenCapture.TimeRemainingForNextScreenshot.Milliseconds;

                    string remainingHoursStr = (remainingHours > 0
                        ? remainingHours.ToString() + " hour" + (remainingHours > 1 ? "s" : string.Empty) + ", "
                        : string.Empty);
                    string remainingMinutesStr = (remainingMinutes > 0
                        ? remainingMinutes.ToString() + " minute" + (remainingMinutes > 1 ? "s" : string.Empty) + ", "
                        : string.Empty);

                    string remainingTimeStr = string.Empty;

                    if (remainingSeconds < 1)
                    {
                        remainingTimeStr = "0." + remainingMilliseconds.ToString() + " milliseconds";
                    }
                    else
                    {
                        remainingTimeStr = remainingHoursStr + remainingMinutesStr + remainingSeconds.ToString() +
                                           " second" + (remainingSeconds > 1 ? "s" : string.Empty) + " at " +
                                           _screenCapture.DateTimeNextCycle.ToLongTimeString();
                    }

                    notifyIcon.Text = "Next capture in " + remainingTimeStr;
                }
            }
            else
            {
                notifyIcon.Text = Settings.Application.GetByKey("Name", defaultValue: Settings.ApplicationName).Value.ToString();
            }
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                HideInterface();
            }
            else
            {
                ShowInterface();
            }
        }
    }
}