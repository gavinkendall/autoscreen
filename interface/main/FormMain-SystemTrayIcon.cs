//-----------------------------------------------------------------------
// <copyright file="FormMain-SystemTrayIcon.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
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
        private void ContextMenuStripSystemTrayIcon_Opening(object sender, CancelEventArgs e)
        {
            PopulateLabelList();

            // Change Screen
            toolStripMenuItemChangeScreen.DropDown.Items.Clear();
            foreach (Screen screen in _formScreen.ScreenCollection)
            {
                ToolStripMenuItem screenMenuItem = new ToolStripMenuItem
                {
                    Text = screen.Name,
                    Tag = screen
                };

                screenMenuItem.Click += new EventHandler(changeScreen_Click);

                toolStripMenuItemChangeScreen.DropDown.Items.Add(screenMenuItem);
            }

            // Change Region
            toolStripMenuItemChangeRegion.DropDown.Items.Clear();
            foreach (Region region in _formRegion.RegionCollection)
            {
                ToolStripMenuItem regionMenuItem = new ToolStripMenuItem
                {
                    Text = region.Name,
                    Tag = region
                };

                regionMenuItem.Click += new EventHandler(changeRegion_Click);

                toolStripMenuItemChangeRegion.DropDown.Items.Add(regionMenuItem);
            }

            // Change Editor
            toolStripMenuItemChangeEditor.DropDown.Items.Clear();
            foreach (Editor editor in _formEditor.EditorCollection)
            {
                ToolStripMenuItem editorMenuItem = new ToolStripMenuItem
                {
                    Text = editor.Name,
                    Tag = editor
                };

                editorMenuItem.Click += new EventHandler(changeEditor_Click);

                toolStripMenuItemChangeEditor.DropDown.Items.Add(editorMenuItem);
            }

            // Change Schedule
            toolStripMenuItemChangeSchedule.DropDown.Items.Clear();
            foreach (Schedule schedule in _formSchedule.ScheduleCollection)
            {
                ToolStripMenuItem scheduleMenuItem = new ToolStripMenuItem
                {
                    Text = schedule.Name,
                    Tag = schedule
                };

                scheduleMenuItem.Click += new EventHandler(changeSchedule_Click);

                toolStripMenuItemChangeSchedule.DropDown.Items.Add(scheduleMenuItem);
            }

            // Change Macro Tag
            toolStripMenuItemChangeMacroTag.DropDown.Items.Clear();
            foreach (MacroTag macrotag in _formMacroTag.MacroTagCollection)
            {
                ToolStripMenuItem macrotagMenuItem = new ToolStripMenuItem
                {
                    Text = macrotag.Name,
                    Tag = macrotag
                };

                macrotagMenuItem.Click += new EventHandler(changeMacroTag_Click);

                toolStripMenuItemChangeMacroTag.DropDown.Items.Add(macrotagMenuItem);
            }

            // Change Trigger
            toolStripMenuItemChangeTrigger.DropDown.Items.Clear();
            foreach (Trigger trigger in _formTrigger.TriggerCollection)
            {
                ToolStripMenuItem triggerMenuItem = new ToolStripMenuItem
                {
                    Text = trigger.Name,
                    Tag = trigger
                };

                triggerMenuItem.Click += new EventHandler(changeTrigger_Click);

                toolStripMenuItemChangeTrigger.DropDown.Items.Add(triggerMenuItem);
            }

            if (_screenCapture.LockScreenCaptureSession)
            {
                // Hide the "Screen Capture Status", "Add", "Change", "Settings", and "Tools" menu items.
                toolStripMenuItemScreenCaptureStatus.Visible = false;
                toolStripMenuItemAdd.Visible = false;
                toolStripMenuItemChange.Visible = false;
                toolStripMenuItemSettings.Visible = false;
                toolStripMenuItemEmailSettings.Visible = false;
                toolStripMenuItemFileTransferSettings.Visible = false;
                toolStripMenuItemTools.Visible = false;
                toolStripSeparatorTools.Visible = false;

                // Hide the "Capture Now" memu items.
                toolStripMenuItemCaptureNowEdit.Visible = false;
                toolStripMenuItemCaptureNowArchive.Visible = false;
                toolStripSeparatorCaptureNow.Visible = false;
            }
            else
            {
                // Show the "Add", "Change", "Settings", and "Screen Capture Status" menu items.
                toolStripMenuItemAdd.Visible = true;
                toolStripMenuItemChange.Visible = true;

                if (Convert.ToBoolean(_config.Settings.Application.GetByKey("AllowUserToConfigureEmailSettings", _config.Settings.DefaultSettings.AllowUserToConfigureEmailSettings).Value))
                {
                    toolStripMenuItemSettings.Visible = true;
                    toolStripMenuItemEmailSettings.Visible = true;
                }

                if (Convert.ToBoolean(_config.Settings.Application.GetByKey("AllowUserToConfigureFileTransferSettings", _config.Settings.DefaultSettings.AllowUserToConfigureFileTransferSettings).Value))
                {
                    toolStripMenuItemSettings.Visible = true;
                    toolStripMenuItemFileTransferSettings.Visible = true;
                }

                toolStripMenuItemScreenCaptureStatus.Visible = true;
                toolStripMenuItemTools.Visible = true;
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
            toolStripMenuItemShowHideInterface_Click(sender, e);
        }

        private void HideSystemTrayIcon()
        {
            notifyIcon.Visible = false;
        }

        private void ShowInfo()
        {
            try
            {
                if (_screenCapture.LockScreenCaptureSession)
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
                        HelpMessage($"Please check \"{ _fileSystem.DebugFolder + _fileSystem.ErrorFile}\"");
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