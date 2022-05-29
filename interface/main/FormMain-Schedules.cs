//-----------------------------------------------------------------------
// <copyright file="FormMain-Schedules.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for handling schedules.</summary>
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
        /// The timer used for checking help tips, monitoring externally-issued commands, running Schedules, and displaying capture information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerScheduledCapture_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime dtNow = DateTime.Now;

                // Display help tip message from any class that isn't part of FormMain.
                if (!string.IsNullOrEmpty(HelpTip.Message))
                {
                    RestartHelpTipTimer();

                    HelpMessage(HelpTip.Message);
                    HelpTip.Message = string.Empty;
                }

                ParseCommandLineArguments();

                // Displays the next time screenshots are going to be captured
                // in the system tray icon's tool tip, the main interface, and information window.
                ShowInfo();

                // Special Schedule
                if (_formSchedule.ScheduleCollection.SpecialScheduleEnabled)
                {
                    if (_formSchedule.ScheduleCollection.SpecialScheduleModeOneTime)
                    {
                        if ((dtNow.Hour == _formSchedule.ScheduleCollection.SpecialScheduleCaptureAt.Hour) &&
                            (dtNow.Minute == _formSchedule.ScheduleCollection.SpecialScheduleCaptureAt.Minute) &&
                            (dtNow.Second == _formSchedule.ScheduleCollection.SpecialScheduleCaptureAt.Second))
                        {
                            TakeScreenshot(captureNow: true);

                            _formSchedule.ScheduleCollection.SpecialScheduleEnabled = false;
                        }
                    }

                    if (_formSchedule.ScheduleCollection.SpecialScheduleModePeriod)
                    {
                        if ((dtNow.Hour == _formSchedule.ScheduleCollection.SpecialScheduleStartAt.Hour) &&
                            (dtNow.Minute == _formSchedule.ScheduleCollection.SpecialScheduleStartAt.Minute) &&
                            (dtNow.Second == _formSchedule.ScheduleCollection.SpecialScheduleStartAt.Second))
                        {
                            StartScreenCapture(_formSchedule.ScheduleCollection.SpecialScheduleScreenCaptureInterval);
                        }

                        if ((dtNow.Hour == _formSchedule.ScheduleCollection.SpecialScheduleStopAt.Hour) &&
                            (dtNow.Minute == _formSchedule.ScheduleCollection.SpecialScheduleStopAt.Minute) &&
                            (dtNow.Second == _formSchedule.ScheduleCollection.SpecialScheduleStopAt.Second))
                        {
                            StopScreenCapture();

                            _formSchedule.ScheduleCollection.SpecialScheduleEnabled = false;
                        }
                    }
                }

                // Process the list of schedules we need to consider.
                foreach (Schedule schedule in _formSchedule.ScheduleCollection)
                {
                    if (!schedule.Enable)
                    {
                        continue;
                    }

                    _screenCapture.Scope = schedule.Scope;

                    if ((dtNow.DayOfWeek == DayOfWeek.Monday && schedule.Monday) ||
                        (dtNow.DayOfWeek == DayOfWeek.Tuesday && schedule.Tuesday) ||
                        (dtNow.DayOfWeek == DayOfWeek.Wednesday && schedule.Wednesday) ||
                        (dtNow.DayOfWeek == DayOfWeek.Thursday && schedule.Thursday) ||
                        (dtNow.DayOfWeek == DayOfWeek.Friday && schedule.Friday) ||
                        (dtNow.DayOfWeek == DayOfWeek.Saturday && schedule.Saturday) ||
                        (dtNow.DayOfWeek == DayOfWeek.Sunday && schedule.Sunday))
                    {
                        if (schedule.ModeOneTime)
                        {
                            if ((dtNow.Hour == schedule.CaptureAt.Hour) &&
                                (dtNow.Minute == schedule.CaptureAt.Minute) &&
                                (dtNow.Second == schedule.CaptureAt.Second))
                            {
                                TakeScreenshot(captureNow: true);
                            }
                        }

                        if (schedule.ModePeriod)
                        {
                            if (schedule.Type == 0)
                            {
                                if ((dtNow.Hour == schedule.StartAt.Hour) &&
                                (dtNow.Minute == schedule.StartAt.Minute) &&
                                (dtNow.Second == schedule.StartAt.Second))
                                {
                                    StartScreenCapture(schedule.ScreenCaptureInterval);
                                }

                                if ((dtNow.Hour == schedule.StopAt.Hour) &&
                                    (dtNow.Minute == schedule.StopAt.Minute) &&
                                    (dtNow.Second == schedule.StopAt.Second))
                                {
                                    StopScreenCapture();
                                }
                            }

                            if (schedule.Type == 1)
                            {
                                if ((dtNow.Hour == schedule.StopAt.Hour) &&
                                    (dtNow.Minute == schedule.StopAt.Minute) &&
                                    (dtNow.Second == schedule.StopAt.Second))
                                {
                                    TakeScreenshot(captureNow: true);

                                    // Do not proceed. Simply continue to next iteration.
                                    continue;
                                }

                                // The value of CaptureNextIntervalStep should have already been set as the start time when the schedule was created or changed.
                                if ((dtNow.Hour == schedule.CaptureNextIntervalStep.Hour) &&
                                    (dtNow.Minute == schedule.CaptureNextIntervalStep.Minute) &&
                                    (dtNow.Second == schedule.CaptureNextIntervalStep.Second))
                                {
                                    TakeScreenshot(captureNow: true);

                                    // Set the CaptureNextIntervalStep value based on the specified scheduled interval.
                                    schedule.CaptureNextIntervalStep = schedule.CaptureNextIntervalStep.AddMilliseconds(schedule.ScreenCaptureInterval);
                                }
                            }
                        }
                    }
                }

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
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-Schedules::timerScheduledCapture_Tick", ex);
            }
        }

        private void addSchedule_Click(object sender, EventArgs e)
        {
            ShowInterface();

            _formSchedule.ScheduleObject = null;

            int screenCaptureInterval = _dataConvert.ConvertIntoMilliseconds((int)_formSetup.numericUpDownHoursInterval.Value,
                        (int)_formSetup.numericUpDownMinutesInterval.Value, (int)_formSetup.numericUpDownSecondsInterval.Value, 0);

            _formSchedule.ScreenCaptureInterval = screenCaptureInterval;

            if (!_formSchedule.Visible)
            {
                _formSchedule.ShowDialog(this);
            }
            else
            {
                _formSchedule.Activate();
            }

            if (_formSchedule.DialogResult == DialogResult.OK)
            {
                BuildSchedulesModule();

                if (!_formSchedule.ScheduleCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        private void removeSelectedSchedules_Click(object sender, EventArgs e)
        {
            int countBeforeRemoval = _formSchedule.ScheduleCollection.Count;

            foreach (Control control in tabPageSchedules.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Schedule schedule = _formSchedule.ScheduleCollection.Get((Schedule)checkBox.Tag);
                        _formSchedule.ScheduleCollection.Remove(schedule);
                    }
                }
            }

            if (countBeforeRemoval > _formSchedule.ScheduleCollection.Count)
            {
                BuildSchedulesModule();

                if (!_formSchedule.ScheduleCollection.SaveToXmlFile(_config.Settings,_fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        private void changeSchedule_Click(object sender, EventArgs e)
        {
            ShowInterface();

            Schedule schedule = new Schedule();

            if (sender is Button)
            {
                Button buttonSelected = (Button)sender;
                schedule = (Schedule)buttonSelected.Tag;
            }

            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                schedule = (Schedule)toolStripMenuItemSelected.Tag;
            }

            _formSchedule.ScheduleObject = schedule;

            if (!_formSchedule.Visible)
            {
                _formSchedule.ShowDialog(this);
            }
            else
            {
                _formSchedule.Activate();
            }

            if (_formSchedule.DialogResult == DialogResult.OK)
            {
                BuildSchedulesModule();

                if (!_formSchedule.ScheduleCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }
    }
}
