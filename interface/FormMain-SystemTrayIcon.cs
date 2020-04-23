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

        /// <summary>
        /// Displays the remaining time for when the next screenshot will be taken
        /// when the mouse pointer moves over the system tray icon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            ShowInfo();
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

        private void SystemTrayBalloonTip(string message)
        {
            notifyIcon.ShowBalloonTip(20000, Settings.ApplicationName, message, ToolTipIcon.Info);
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
            if (notifyIcon.Visible && !checkBoxInitialScreenshot.Checked && timerScreenCapture.Interval > BALLOON_TIP_TIMEOUT)
            {
                SystemTrayBalloonTip("The system tray icon turns green when taking screenshots. To stop, right-click on the icon and select Stop Screen Capture.");
            }

            notifyIcon.Icon = Resources.autoscreen_running;
        }

        private void ShowInfo()
        {
            notifyIcon.Text = string.Empty;

            if (_screenCapture.Running)
            {
                if (_screenCapture.PerformingMaintenance)
                {
                    notifyIcon.Text = "Performing maintenance ...";
                }
                else
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
                }
            }

            toolStripInfo.Text = notifyIcon.Text;
        }
    }
}