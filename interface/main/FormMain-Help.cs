//-----------------------------------------------------------------------
// <copyright file="FormMain-Help.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for handling help tips in the help bar at the top of the main interface window.</summary>
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
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        int helpTipIndex = 0;
        private readonly List<string> helpTips = new List<string>();

        private void LoadHelpTips()
        {
            helpTips.Add("Help tips are displayed here in the help bar. You can either click this help bar to show the next tip or wait until the next tip is shown");
            helpTips.Add("Start Screen Capture begins a session of taking screenshots at the specified interval. The application will run in your system tray");
            helpTips.Add("The calendar shows you which days screenshots were taken. Any date shown in bold indicates screenshots were taken on that day");
            helpTips.Add("Below the calendar are modules such as Setup, Screenshots, Screens, Regions, and Editors. Scroll through the modules to become familiar with them");
            helpTips.Add("The Setup module is used to specify the frequency at which screenshots will be taken and what label to apply to each screenshot");
            helpTips.Add("The Screenshots module displays the list of screenshots taken on the day selected from the calendar");
            helpTips.Add("The Regions module displays a list of regions. Add, remove, or change regions from the Regions module");
            helpTips.Add("Use the Configure drop-down menu to add, remove, or change a Screen or Region. You can add multiple regions each with its own set of properties");
            helpTips.Add("You can add as many editors as you want but only one editor can be set as the default editor");
            helpTips.Add("Use the Macro field of a Screen or Region to define the filename pattern for each file. A macro can be very useful for keeping files organized");
            helpTips.Add("Macro tags can represent the current date and time. You can also acquire the name of the user or computer. Have a look in the Tags module");
            helpTips.Add("There are keyboard shortcuts you can use to manually take screenshots in your own time rather than wait for the next screen capture cycle");
            helpTips.Add("You can change the behaviour of the application by changing the triggers. Each trigger performs a certain action based on a particular condition");
        }

        private void timerShowNextHelpTip_Tick(object sender, EventArgs e)
        {
            if (helpTipIndex < helpTips.Count)
            {
                HelpMessage(helpTips[helpTipIndex]);
                helpTipIndex++;
            }

            if (helpTipIndex == helpTips.Count)
            {
                helpTipIndex = 0;
            }
        }

        private void labelHelp_Click(object sender, EventArgs e)
        {
            timerShowNextHelpTip_Tick(sender, e);
            RestartHelpTipTimer();
        }

        private void RestartHelpTipTimer()
        {
            timerShowNextHelpTip.Stop();
            timerShowNextHelpTip.Start();
        }

        private void HelpMessage(string message)
        {
            labelHelp.Text = "       " + message;
        }

        private void checkBoxInitialScreenshot_MouseHover(object sender, EventArgs e)
        {
            RestartHelpTipTimer();
            HelpMessage("Initial Capture takes screenshots immediately before the timer has begun when a screen capture session has started");
        }

        private void checkBoxCaptureLimit_MouseHover(object sender, EventArgs e)
        {
            RestartHelpTipTimer();
            HelpMessage("The limit option stops a running screen capture session once a specified number of screen capture cycles has been reached");
        }

        private void checkBoxScreenshotLabel_MouseHover(object sender, EventArgs e)
        {
            RestartHelpTipTimer();
            HelpMessage("Applying a label to every screenshot enables you to later filter by that label");
        }

        private void textBoxPassphrase_MouseHover(object sender, EventArgs e)
        {
            RestartHelpTipTimer();
            HelpMessage("Please remember your passphrase before you click on the Lock button as it will be required to unlock a running screen capture session");
        }

        private void comboBoxFilterType_MouseHover(object sender, EventArgs e)
        {
            RestartHelpTipTimer();
            HelpMessage("Choose a type of filter. This could be a label or the title of an active window from an application that was captured during a session");
        }

        private void comboBoxFilterValue_MouseHover(object sender, EventArgs e)
        {
            RestartHelpTipTimer();
            HelpMessage("Select a filter value based on the chosen filter type. This will show you what days in the calendar are associated with the filter value");
        }

        private void buttonRefreshFilterValues_MouseHover(object sender, EventArgs e)
        {
            RestartHelpTipTimer();
            HelpMessage("Click this button to refresh the list of filter values");
        }

        private void labelKeepScreenshots_MouseHover(object sender, EventArgs e)
        {
            RestartHelpTipTimer();
            HelpMessage("Specify the number of days screenshots should be kept until they are automatically deleted");
        }
    }
}
