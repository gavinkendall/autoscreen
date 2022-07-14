//-----------------------------------------------------------------------
// <copyright file="FormMain-Triggers.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Methods for adding, removing, and changing triggers.</summary>
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
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// The timer to check enabled Triggers.
        /// This runs every second.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTriggerCheck_Tick(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now;

            // Process the list of triggers of condition type Date/Time, condition type Time, and condition type Day/Time.
            foreach (Trigger trigger in _formTrigger.TriggerCollection)
            {
                if (!trigger.Enable)
                {
                    continue;
                }

                if (trigger.ConditionType == TriggerConditionType.DateTime &&
                    trigger.Date.ToString(_macroParser.DateFormat).Equals(dtNow.ToString(_macroParser.DateFormat)) &&
                    trigger.Time.ToString(_macroParser.TimeFormatForTrigger).Equals(dtNow.ToString(_macroParser.TimeFormatForTrigger)))
                {
                    DoTriggerAction(trigger);
                }

                if (trigger.ConditionType == TriggerConditionType.Time &&
                    trigger.Time.ToString(_macroParser.TimeFormatForTrigger).Equals(dtNow.ToString(_macroParser.TimeFormatForTrigger)))
                {
                    DoTriggerAction(trigger);
                }

                if (trigger.ConditionType == TriggerConditionType.DayTime &&
                    trigger.Time.ToString(_macroParser.TimeFormatForTrigger).Equals(dtNow.ToString(_macroParser.TimeFormatForTrigger)))
                {
                    if (trigger.Day.Equals(dtNow.DayOfWeek.ToString()))
                    {
                        DoTriggerAction(trigger);
                    }

                    if (trigger.Day.Equals("Weekday") && (dtNow.DayOfWeek == DayOfWeek.Monday ||
                            dtNow.DayOfWeek == DayOfWeek.Tuesday ||
                            dtNow.DayOfWeek == DayOfWeek.Wednesday ||
                            dtNow.DayOfWeek == DayOfWeek.Thursday ||
                            dtNow.DayOfWeek == DayOfWeek.Friday))
                    {
                        DoTriggerAction(trigger);
                    }

                    if (trigger.Day.Equals("Weekend") && (dtNow.DayOfWeek == DayOfWeek.Saturday || dtNow.DayOfWeek == DayOfWeek.Sunday))
                    {
                        DoTriggerAction(trigger);
                    }
                }

                if (trigger.ConditionType == TriggerConditionType.DurationFromStartScreenCapture)
                {
                    switch (trigger.DurationType)
                    {
                        // Seconds
                        case 0:
                            if (dtStartScreenCapture.AddSeconds(trigger.Duration).Hour == dtNow.Hour &&
                                dtStartScreenCapture.AddSeconds(trigger.Duration).Minute == dtNow.Minute &&
                                dtStartScreenCapture.AddSeconds(trigger.Duration).Second == dtNow.Second)
                            {
                                DoTriggerAction(trigger);
                            }
                            break;

                        // Minutes
                        case 1:
                            if (dtStartScreenCapture.AddMinutes(trigger.Duration).Hour == dtNow.Hour &&
                                dtStartScreenCapture.AddMinutes(trigger.Duration).Minute == dtNow.Minute &&
                                dtStartScreenCapture.AddMinutes(trigger.Duration).Second == dtNow.Second)
                            {
                                DoTriggerAction(trigger);
                            }
                            break;

                        // Hours
                        case 2:
                            if (dtStartScreenCapture.AddHours(trigger.Duration).Hour == dtNow.Hour &&
                                dtStartScreenCapture.AddHours(trigger.Duration).Minute == dtNow.Minute &&
                                dtStartScreenCapture.AddHours(trigger.Duration).Second == dtNow.Second)
                            {
                                DoTriggerAction(trigger);
                            }
                            break;
                    }
                }

                if (trigger.ConditionType == TriggerConditionType.DurationFromStopScreenCapture)
                {
                    switch (trigger.DurationType)
                    {
                        // Seconds
                        case 0:
                            if (dtStopScreenCapture.AddSeconds(trigger.Duration).Hour == dtNow.Hour &&
                                dtStopScreenCapture.AddSeconds(trigger.Duration).Minute == dtNow.Minute &&
                                dtStopScreenCapture.AddSeconds(trigger.Duration).Second == dtNow.Second)
                            {
                                DoTriggerAction(trigger);
                            }
                            break;

                        // Minutes
                        case 1:
                            if (dtStopScreenCapture.AddMinutes(trigger.Duration).Hour == dtNow.Hour &&
                                dtStopScreenCapture.AddMinutes(trigger.Duration).Minute == dtNow.Minute &&
                                dtStopScreenCapture.AddMinutes(trigger.Duration).Second == dtNow.Second)
                            {
                                DoTriggerAction(trigger);
                            }
                            break;

                        // Hours
                        case 2:
                            if (dtStopScreenCapture.AddHours(trigger.Duration).Hour == dtNow.Hour &&
                                dtStopScreenCapture.AddHours(trigger.Duration).Minute == dtNow.Minute &&
                                dtStopScreenCapture.AddHours(trigger.Duration).Second == dtNow.Second)
                            {
                                DoTriggerAction(trigger);
                            }
                            break;
                    }
                }
            }

            if (_appStarted)
            {
                // We want to figure out the visibility for the system tray icon here so it doesn't appear too early if we happen to use -hide.
                notifyIcon.Visible = Convert.ToBoolean(_config.Settings.User.GetByKey("ShowSystemTrayIcon", _config.Settings.DefaultSettings.ShowSystemTrayIcon).Value);
                _log.WriteDebugMessage("ShowSystemTrayIcon = " + notifyIcon.Visible);

                _log.WriteDebugMessage("Running triggers of condition type ApplicationStartup");
                RunTriggersOfConditionType(TriggerConditionType.ApplicationStartup);

                // Okay the application started so we can set this back to false. I really hate doing this but for now it's easy to get
                // around the weird situation where an ApplicationStartup trigger needs to run after the command line options get parsed
                // (and one of thse options might be -hide which needs to disable triggers using the ShowInterface action).
                // For example we don't want any triggers showing the interface if we're starting the application with something like ...
                // autoscreen.exe -interval=00:00:05 -start -hide
                _appStarted = false;
            }
        }

        /// <summary>
        /// Shows the "Add Trigger" window to enable the user to add a chosen Trigger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTrigger_Click(object sender, EventArgs e)
        {
            ShowInterface();

            _formTrigger.TriggerObject = null;

            _formTrigger.EditorCollection = _formEditor.EditorCollection;
            _formTrigger.ScreenCollection = _formScreen.ScreenCollection;
            _formTrigger.RegionCollection = _formRegion.RegionCollection;
            _formTrigger.ScheduleCollection = _formSchedule.ScheduleCollection;
            _formTrigger.MacroTagCollection = _formMacroTag.MacroTagCollection;

            if (!_formTrigger.Visible)
            {
                _formTrigger.ShowDialog(this);
            }
            else
            {
                _formTrigger.Activate();
            }

            if (_formTrigger.DialogResult == DialogResult.OK)
            {
                BuildTriggersModule();

                if (!_formTrigger.TriggerCollection.SaveToXmlFile(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Removes the selected Triggers from the Triggers tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSelectedTriggers_Click(object sender, EventArgs e)
        {
            int countBeforeRemoval = _formTrigger.TriggerCollection.Count;

            foreach (Control control in tabPageTriggers.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Trigger trigger = _formTrigger.TriggerCollection.Get((Trigger)checkBox.Tag);
                        _formTrigger.TriggerCollection.Remove(trigger);
                    }
                }
            }

            if (countBeforeRemoval > _formTrigger.TriggerCollection.Count)
            {
                BuildTriggersModule();

                if (!_formTrigger.TriggerCollection.SaveToXmlFile(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Shows the "Change Trigger" window to enable the user to edit a chosen Trigger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeTrigger_Click(object sender, EventArgs e)
        {
            ShowInterface();

            Trigger trigger = new Trigger();

            if (sender is Button)
            {
                Button buttonSelected = (Button)sender;
                trigger = (Trigger)buttonSelected.Tag;
            }

            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                trigger = (Trigger)toolStripMenuItemSelected.Tag;
            }

            _formTrigger.TriggerObject = trigger;

            _formTrigger.EditorCollection = _formEditor.EditorCollection;
            _formTrigger.ScreenCollection = _formScreen.ScreenCollection;
            _formTrigger.RegionCollection = _formRegion.RegionCollection;
            _formTrigger.ScheduleCollection = _formSchedule.ScheduleCollection;
            _formTrigger.MacroTagCollection = _formMacroTag.MacroTagCollection;

            if (!_formTrigger.Visible)
            {
                _formTrigger.ShowDialog(this);
            }
            else
            {
                _formTrigger.Activate();
            }

            if (_formTrigger.DialogResult == DialogResult.OK)
            {
                BuildTriggersModule();

                if (!_formTrigger.TriggerCollection.SaveToXmlFile(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        private void RunTriggersOfConditionType(TriggerConditionType conditionType)
        {
            foreach (Trigger trigger in _formTrigger.TriggerCollection)
            {
                if (!trigger.Enable)
                {
                    continue;
                }

                // Don't show the interface on startup if AutoStartFromCommandLine is enabled.
                if (_screenCapture.AutoStartFromCommandLine &&
                    trigger.ConditionType == TriggerConditionType.ApplicationStartup &&
                    trigger.ActionType == TriggerActionType.ShowInterface)
                {
                    continue;
                }

                if (trigger.ConditionType == conditionType)
                {
                    DoTriggerAction(trigger);
                }
            }
        }

        private void DoTriggerAction(Trigger trigger)
        {
            // These actions need to directly correspond with the TriggerActionType class.
            switch (trigger.ActionType)
            {
                case TriggerActionType.ExitApplication:
                    ExitApplication();
                    break;

                case TriggerActionType.HideInterface:
                    HideInterface();
                    break;

                case TriggerActionType.RunEditor:
                    Editor editor = _formEditor.EditorCollection.GetByName(trigger.Value);
                    RunEditor(editor, _screenshotCollection.GetLastScreenshot());
                    break;

                case TriggerActionType.ShowInterface:
                    ShowInterface();
                    break;

                case TriggerActionType.StartScreenCapture:
                    StartScreenCapture();
                    break;

                case TriggerActionType.StopScreenCapture:
                    StopScreenCapture();
                    break;

                case TriggerActionType.EmailScreenshot:
                    EmailScreenshot(TriggerActionType.EmailScreenshot);
                    break;

                case TriggerActionType.SetScreenCaptureInterval:
                    timerScreenCapture.Stop();

                    _screenCapture.Interval = trigger.ScreenCaptureInterval;
                    timerScreenCapture.Interval = trigger.ScreenCaptureInterval;

                    decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(trigger.ScreenCaptureInterval)).Hours);
                    decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(trigger.ScreenCaptureInterval)).Minutes);
                    decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(trigger.ScreenCaptureInterval)).Seconds);

                    _formSetup.numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                    _formSetup.numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                    _formSetup.numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;

                    timerScreenCapture.Start();
                    break;

                case TriggerActionType.EnableScreen:
                    _formScreen.ScreenCollection.GetByName(trigger.Value).Enable = true;
                    BuildScreensModule();
                    break;

                case TriggerActionType.DisableScreen:
                    _formScreen.ScreenCollection.GetByName(trigger.Value).Enable = false;
                    BuildScreensModule();
                    break;

                case TriggerActionType.EnableRegion:
                    _formRegion.RegionCollection.GetByName(trigger.Value).Enable = true;
                    BuildRegionsModule();
                    break;

                case TriggerActionType.DisableRegion:
                    _formRegion.RegionCollection.GetByName(trigger.Value).Enable = false;
                    BuildRegionsModule();
                    break;

                case TriggerActionType.EnableSchedule:
                    _formSchedule.ScheduleCollection.GetByName(trigger.Value).Enable = true;
                    BuildSchedulesModule();
                    break;

                case TriggerActionType.DisableSchedule:
                    _formSchedule.ScheduleCollection.GetByName(trigger.Value).Enable = false;
                    BuildSchedulesModule();
                    break;

                case TriggerActionType.EnableMacroTag:
                    _formMacroTag.MacroTagCollection.GetByName(trigger.Value).Enable = true;
                    BuildMacroTagsModule();
                    break;

                case TriggerActionType.DisableMacroTag:
                    _formMacroTag.MacroTagCollection.GetByName(trigger.Value).Enable = false;
                    BuildMacroTagsModule();
                    break;

                case TriggerActionType.EnableTrigger:
                    _formTrigger.TriggerCollection.GetByName(trigger.Value).Enable = true;
                    BuildTriggersModule();
                    break;

                case TriggerActionType.DisableTrigger:
                    _formTrigger.TriggerCollection.GetByName(trigger.Value).Enable = false;
                    BuildTriggersModule();
                    break;

                case TriggerActionType.DeleteScreenshotsByDays:
                    _screenshotCollection.DeleteScreenshotsByDays(trigger.Days, trigger.Value, _macroParser, _formMacroTag.MacroTagCollection, _failedUploads);
                    break;

                case TriggerActionType.SetLabel:
                    ApplyLabel(trigger.Value);
                    break;

                case TriggerActionType.SetActiveWindowTitleAsMatch:
                    SetActiveWindowTitleAsMatch(trigger.Value);
                    break;

                case TriggerActionType.SetApplicationFocus:
                    _formSetup.SetApplicationFocus(trigger.Value);
                    break;

                case TriggerActionType.FileTransferScreenshot:
                    FileTransferScreenshot(TriggerActionType.FileTransferScreenshot);
                    break;

                case TriggerActionType.SetActiveWindowTitleAsNoMatch:
                    SetActiveWindowTitleAsNoMatch(trigger.Value);
                    break;

                case TriggerActionType.ShowSystemTrayIcon:
                    ShowSystemTrayIcon();
                    break;

                case TriggerActionType.HideSystemTrayIcon:
                    HideSystemTrayIcon();
                    break;

                case TriggerActionType.TakeScreenshot:
                    TakeScreenshot(captureNow: true);
                    break;

                case TriggerActionType.RegionSelectClipboard:
                    toolStripMenuItemRegionSelectClipboard_Click(null, null);
                    break;

                case TriggerActionType.RegionSelectClipboardAutoSave:
                    toolStripMenuItemRegionSelectClipboardAutoSave_Click(null, null);
                    break;

                case TriggerActionType.RegionSelectClipboardAutoSaveEdit:
                    toolStripMenuItemRegionSelectClipboardAutoSaveEdit_Click(null, null);
                    break;

                case TriggerActionType.RegionSelectClipboardFloatingScreenshot:
                    toolStripMenuItemRegionSelectClipboardFloatingScreenshot_Click(null, null);
                    break;

                case TriggerActionType.RegionSelectFloatingScreenshot:
                    toolStripMenuItemRegionSelectFloatingScreenshot_Click(null, null);
                    break;

                case TriggerActionType.ShowOrHideInterface:
                    toolStripMenuItemShowHideInterface_Click(null, null);
                    break;

                case TriggerActionType.StartOrStopScreenCapture:
                    if (_screenCapture.Running)
                    {
                        StopScreenCapture();
                    }
                    else
                    {
                        StartScreenCapture();
                    }
                    break;

                case TriggerActionType.RestartScreenCapture:
                    StopScreenCapture();
                    StartScreenCapture();
                    break;

                case TriggerActionType.DeleteScreenshotsByCycleCount:
                    _screenshotCollection.DeleteScreenshotsByCycleCount(trigger.CycleCount, _failedUploads);
                    break;

                case TriggerActionType.DeleteScreenshotsFromOldestCaptureCycle:
                    _screenshotCollection.DeleteScreenshotsFromOldestCaptureCycle(_failedUploads);
                    break;
            }
        }
    }
}