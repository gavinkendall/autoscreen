//-----------------------------------------------------------------------
// <copyright file="FormMain-SystemTrayIcon.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Methods for what happens when the user selects a menu item from the system tray icon or for displaying information in the system tray icon's tooltip.</summary>
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

            if (ScreenCapture.LockScreenCaptureSession)
            {
                // Hide the "Email Settings", "File Transfer Settings", and "Screen Capture Status" menu items.
                toolStripMenuItemEmailSettings.Visible = false;
                toolStripMenuItemFileTransferSettings.Visible = false;
                toolStripMenuItemScreenCaptureStatus.Visible = false;
                toolStripSeparatorTools.Visible = false;

                // Hide the "Capture Now" memu items.
                toolStripMenuItemCaptureNowEdit.Visible = false;
                toolStripMenuItemCaptureNowArchive.Visible = false;
                toolStripSeparatorCaptureNow.Visible = false;
            }
            else
            {
                // Show the "Email Settings", "File Transfer Settings", and "Screen Capture Status" menu items.
                toolStripMenuItemEmailSettings.Visible = true;
                toolStripMenuItemFileTransferSettings.Visible = true;
                toolStripMenuItemScreenCaptureStatus.Visible = true;
                toolStripSeparatorTools.Visible = true;

                // Show the "Capture Now" memu items.
                toolStripMenuItemCaptureNowEdit.Visible = true;
                toolStripMenuItemCaptureNowArchive.Visible = true;
                toolStripSeparatorCaptureNow.Visible = true;
            }
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
            // Show the balloon tip only if the system tray icon is visible and the "FirstRun" setting returns "True".

            bool.TryParse(Settings.User.GetByKey("FirstRun", DefaultSettings.FirstRun).Value.ToString(), out bool firstRun);

            if (notifyIcon.Visible && firstRun)
            {
                notifyIcon.ShowBalloonTip(balloonTipTimeout, Settings.ApplicationName, message, ToolTipIcon.Info);
            }
        }

        private void HideSystemTrayIcon()
        {
            notifyIcon.Visible = false;
        }

        private void ShowInfo()
        {
            try
            {
                if (ScreenCapture.LockScreenCaptureSession)
                {
                    notifyIcon.Text = string.Empty;

                    return;
                }

                notifyIcon.Text = "Ready to start taking screenshots";

                if (_screenCapture.ApplicationError || _screenCapture.ApplicationWarning)
                {
                    if (_screenCapture.ApplicationError)
                    {
                        notifyIcon.Icon = Resources.autoscreen_error;
                        notifyIcon.Text = "The application encountered an error";

                        labelHelp.Image = Resources.warning;
                        labelHelp.BackColor = System.Drawing.Color.PaleVioletRed;
                        HelpMessage($"Please check \"{ FileSystem.DebugFolder + FileSystem.ErrorFile}\"");
                    }
                    else if (_screenCapture.ApplicationWarning)
                    {
                        notifyIcon.Icon = Resources.autoscreen_warning;
                        notifyIcon.Text = "A drive being used is running low on available disk space";
                    }
                }
                else
                {
                    if (_screenCapture.Running)
                    {
                        notifyIcon.Icon = Resources.autoscreen_running;

                        if (!_screenCapture.CaptureError)
                        {
                            string remainingTimeStr = "Taking screenshots";

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
                        }
                    }
                    else
                    {
                        notifyIcon.Icon = Resources.autoscreen;
                    }

                    labelHelp.Image = Resources.about;
                    labelHelp.BackColor = System.Drawing.Color.LightYellow;
                }

                toolStripInfo.Text = notifyIcon.Text;
                _formScreenCaptureStatus.Text = notifyIcon.Text;
            }
            catch (Exception)
            {
                // There's some sort of target invocation exception that happens early in the morning. I'm not sure why yet so let's catch it but not log it for now.
            }
        }
    }
}