using System;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Exits the application from the system tray icon menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_toolStripMenuItemExit(object sender, EventArgs e)
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