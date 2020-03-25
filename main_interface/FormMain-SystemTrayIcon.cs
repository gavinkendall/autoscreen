using System.Windows.Forms;

namespace AutoScreenCapture
{
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
    }
}