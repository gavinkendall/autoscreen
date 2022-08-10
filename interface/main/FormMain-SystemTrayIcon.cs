//-----------------------------------------------------------------------
// <copyright file="FormMain-SystemTrayIcon.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
            // This is to check if we're in the weird condition whereby the main form is considered Visible
            // but we haven't set the visibility ourselves yet. We can help the user by setting Visible to false
            // and setting _initialVisibilitySet to true so they don't have to select this option twice on initial load.
            // See OnVisibleChanged method for more information; especially if the user doesn't have any Triggers setup.
            if (!_initialVisibilitySet && Visible)
            {
                Visible = false;
                _initialVisibilitySet = true;
            }

            if (Visible)
            {
                toolStripMenuItemShowHideInterface.Text = "Hide Interface";
            }
            else
            {
                toolStripMenuItemShowHideInterface.Text = "Show Interface";
            }

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

                screenMenuItem.Click += new EventHandler(configureScreen_Click);

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

                regionMenuItem.Click += new EventHandler(configureRegion_Click);

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

                editorMenuItem.Click += new EventHandler(configureEditor_Click);

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

                scheduleMenuItem.Click += new EventHandler(configureSchedule_Click);

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

                macrotagMenuItem.Click += new EventHandler(configureMacroTag_Click);

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

                triggerMenuItem.Click += new EventHandler(configureTrigger_Click);

                toolStripMenuItemChangeTrigger.DropDown.Items.Add(triggerMenuItem);
            }

            if (_screenCapture.LockScreenCaptureSession)
            {
                // Hide the menu items that need to be hidden when a screen capture session is locked.
                toolStripSeparatorScreenCapture.Visible = false;
                toolStripMenuItemCommandLine.Visible = false;
                toolStripMenuItemOpenProgramFolder.Visible = false;
                toolStripMenuItemScreenCaptureStatus.Visible = false;
                toolStripMenuItemAdd.Visible = false;
                toolStripMenuItemConfigure.Visible = false;
                toolStripMenuItemSettings.Visible = false;
                toolStripMenuItemEmailSettings.Visible = false;
                toolStripMenuItemFileTransferSettings.Visible = false;
                toolStripMenuItemTools.Visible = false;
                toolStripSeparatorTools.Visible = false;
                toolStripMenuItemHelp.Visible = false;
                toolStripMenuItemRegionSelectAddRegion.Visible = false;

                // Hide the "Capture Now" memu items.
                toolStripMenuItemCaptureNowOptions.Visible = false;
                toolStripMenuItemCaptureNowArchive.Visible = false;
                toolStripMenuItemCaptureNowEdit.Visible = false;
                toolStripSeparatorCaptureNow.Visible = false;
            }
            else
            {
                // Show the menu items that need to be shown when a screen capture session is unlocked.
                toolStripSeparatorScreenCapture.Visible = true;
                toolStripMenuItemCommandLine.Visible = true;
                toolStripMenuItemOpenProgramFolder.Visible = true;
                toolStripMenuItemScreenCaptureStatus.Visible = true;
                toolStripMenuItemAdd.Visible = true;
                toolStripMenuItemConfigure.Visible = true;
                toolStripMenuItemHelp.Visible = true;
                toolStripMenuItemSettings.Visible = true;
                toolStripMenuItemEmailSettings.Visible = true;
                toolStripMenuItemSettings.Visible = true;
                toolStripMenuItemFileTransferSettings.Visible = true;
                toolStripMenuItemRegionSelectAddRegion.Visible = true;

                toolStripMenuItemTools.Visible = true;
                toolStripSeparatorTools.Visible = true;

                // Show the "Capture Now" memu items.
                toolStripMenuItemCaptureNowOptions.Visible = true;
                toolStripMenuItemCaptureNowArchive.Visible = true;
                toolStripMenuItemCaptureNowEdit.Visible = true;
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
            Setting confirmExitSetting = _config.Settings.User.GetByKey("ConfirmExit");

            if (confirmExitSetting != null)
            {
                bool confirmExit = Convert.ToBoolean(confirmExitSetting.Value);

                if (confirmExit)
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit Auto Screen Capture?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        ExitApplication();
                    }
                }
                else
                {
                    ExitApplication();
                }
            }
            else
            {
                ExitApplication();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                RunTriggersOfConditionType(TriggerConditionType.SystemTrayIconDoubleClick);
            }
        }

        private void ShowSystemTrayIcon()
        {
            notifyIcon.Visible = true;
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

                notifyIcon.Text = "Ready [" +
                    _formSetup.numericUpDownHoursInterval.Value.ToString("00") + ":" +
                    _formSetup.numericUpDownMinutesInterval.Value.ToString("00") + ":" +
                    _formSetup.numericUpDownSecondsInterval.Value.ToString("00") + "] " +
                    (_formSetup.checkBoxInitialScreenshot.Checked ? "[initial capture] " : string.Empty) +
                    (_screenCapture.OptimizeScreenCapture ? "[optimized (" + _formSetup.trackBarImageDiffTolerance.Value + "% tolerant)]" : "[not optimized]");

                if (_screenCapture.ApplicationError || _screenCapture.ApplicationWarning)
                {
                    if (_screenCapture.ApplicationError)
                    {
                        notifyIcon.Icon = Resources.autoscreen_error;
                        notifyIcon.Text = "The application encountered an error";

                        labelHelp.Image = Resources.warning;
                        labelHelp.BackColor = System.Drawing.Color.PaleVioletRed;
                        HelpMessage($"Please check \"{ _fileSystem.ErrorsFolder + _fileSystem.ErrorFile}\"");
                    }
                    else if (_screenCapture.ApplicationWarning)
                    {
                        notifyIcon.Icon = Resources.autoscreen_warning;
                        notifyIcon.Text = "Disk drive is running low on free disk space";
                    }
                }
                else
                {
                    if (_screenCapture.Running)
                    {
                        string takingScreenshotsMessage;

                        if (_screenCapture.OptimizeScreenCapture)
                        {
                            // System tray icon is green if taking optimized screenshots.
                            takingScreenshotsMessage = "Taking screenshots [optimized (" + _formSetup.trackBarImageDiffTolerance.Value + "% tolerant)]";
                            notifyIcon.Icon = Resources.autoscreen_running_optimized;
                        }
                        else
                        {
                            // System tray icon is blue if not taking optimized screenshots.
                            takingScreenshotsMessage = "Taking screenshots [not optimized]";
                            notifyIcon.Icon = Resources.autoscreen_running_not_optimized;
                        }

                        if (!_screenCapture.CaptureError)
                        {
                            string remainingTimeStr = takingScreenshotsMessage;

                            if (_screenCapture.DateTimeScreenshotsTaken <= DateTime.Now)
                            {
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
                    }
                    else
                    {
                        notifyIcon.Icon = Resources.autoscreen;
                    }

                    labelHelp.Image = Resources.about;
                    labelHelp.BackColor = System.Drawing.Color.LightYellow;
                }

                toolStripCycleCount.Text = $"Cycle: {_screenCapture.CycleCount}";
                toolStripInfo.Text = notifyIcon.Text;

                _formScreenCaptureStatusWithLabelSwitcher.labelScreenCaptureStatus.Text = notifyIcon.Text;
                _formScreenCaptureStatus.labelScreenCaptureStatus.Text = notifyIcon.Text;
            }
            catch (Exception)
            {
                // There's some sort of target invocation exception that happens early in the morning. I'm not sure why yet so let's catch it but not log it for now.
            }
        }
    }
}