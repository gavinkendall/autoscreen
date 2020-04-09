using System;
using System.Drawing;
using System.Windows.Forms;
using AutoScreenCapture.Properties;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        private void HelpMessage(string message)
        {
            labelHelp.Text = "       " + message;
        }

        private void checkBoxInitialScreenshot_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Initial Capture takes screenshots immediately before the timer has begun when a screen capture session has started");
        }

        private void checkBoxCaptureLimit_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The limit option stops a running screen capture session once a specified number of screen capture cycles has been reached");
        }

        private void checkBoxScreenshotLabel_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Applying a label to every screenshot enables you to later filter by that label");
        }

        private void textBoxPassphrase_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Please remember your passphrase before you click on the Lock button as it will be required to unlock a running screen capture session");
        }

        private void comboBoxFilterType_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Choose a type of filter. This could be a label or the title of an active window from an application that was captured during a session");
        }

        private void comboBoxFilterValue_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Select a filter value based on the chosen filter type. This will show you what days in the calendar are associated with the filter value");
        }

        private void buttonRefreshFilterValues_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Click this button to refresh the list of filter values");
        }

        private void labelKeepScreenshots_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Specify the number of days screenshots should be kept until they are automatically deleted");
        }
    }
}
