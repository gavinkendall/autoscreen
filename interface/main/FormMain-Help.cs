﻿//-----------------------------------------------------------------------
// <copyright file="FormMain-Help.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
        int _helpTipIndex = 0;
        private ToolTip _toolTip = new ToolTip();
        private readonly List<string> _helpTips = new List<string>();

        private void LoadHelpTips()
        {
            _helpTips.Add("\"Command Deck\" is a small floating window providing the application's most common controls such as \"Start/Stop Screen Capture\" and \"Capture Now / Archive\"");
            _helpTips.Add("You can find \"Command Deck\" in either the Control, Region Select, or Tools menu. It's a useful tool that also displays the screen capture status");
            _helpTips.Add("\"Screen Capture Status\" (from the Control menu) not only shows you the status of the application but now includes a \"Start/Stop Screen Capture\" button");
            _helpTips.Add("If you also use labels then it's recommended to use the \"Screen Capture Status With Label Switcher\" tool. This is in the Control and Tools menu");
            _helpTips.Add("The Command Deck provides Region Select options if you toggle the \"Region Select\" button. These are the same options available in the Region Select menu");
            _helpTips.Add("The \"Add Region (Express)\" option immediately adds a new Region based on the selected area of a screen without showing the Add Region dialog window");
            
            // Tool tips for various controls.
            _toolTip.SetToolTip(comboBoxFilterType, "Choose a type of filter. This could be a label or the title of an active window from an application that was captured during a session");
            _toolTip.SetToolTip(comboBoxFilterValue, "Select a filter value based on the chosen filter type. This will show you what days in the calendar are associated with the filter value");
            _toolTip.SetToolTip(buttonRefreshFilterValues, "Click this button to refresh the list of filter values");
        }

        /// <summary>
        /// The timer to check help tips issued by classes that aren't part of FormMain and also to show the tool tip in the system tray icon.
        /// This runs every second.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerHelpTip_Tick(object sender, EventArgs e)
        {
            // Display help tip message from any class that isn't part of FormMain.
            if (!string.IsNullOrEmpty(HelpTip.Message) && !_screenCapture.ApplicationError)
            {
                RestartHelpTipTimer();

                HelpMessage(HelpTip.Message);
                HelpTip.Message = string.Empty;
            }

            // Displays the next time screenshots are going to be captured
            // in the system tray icon's tool tip, the main interface, and information window.
            ShowInfo();
        }

        private void timerShowNextHelpTip_Tick(object sender, EventArgs e)
        {
            if (_helpTipIndex < _helpTips.Count && !_screenCapture.ApplicationError)
            {
                HelpMessage(_helpTips[_helpTipIndex]);
                _helpTipIndex++;
            }

            if (_helpTipIndex == _helpTips.Count)
            {
                _helpTipIndex = 0;
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

        /// <summary>
        /// Shows the "Auto Screen Capture - Help" form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripDropDownButtonHelp_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selectedMenuItem = (ToolStripMenuItem)sender;

            _formHelp.ShowSection(selectedMenuItem.Text);

            if (!_formHelp.Visible)
            {
                _formHelp.Show();
            }
            else
            {
                _formHelp.WindowState = FormWindowState.Normal;
                _formHelp.Activate();
            }
        }
    }
}
