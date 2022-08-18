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
        /// The timer to check enabled Schedules.
        /// This runs every second.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerScheduleCheck_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime dtNow = DateTime.Now;

                // Special Schedule
                if (_formSchedule.ScheduleCollection.SpecialScheduleActivated)
                {
                    if (_formSchedule.ScheduleCollection.SpecialScheduleModeOneTime)
                    {
                        if ((dtNow.Hour == _formSchedule.ScheduleCollection.SpecialScheduleCaptureAt.Hour) &&
                            (dtNow.Minute == _formSchedule.ScheduleCollection.SpecialScheduleCaptureAt.Minute))
                        {
                            TakeScreenshot(captureNow: true);

                            _formSchedule.ScheduleCollection.SpecialScheduleActivated = false;
                        }
                    }

                    if (_formSchedule.ScheduleCollection.SpecialScheduleModePeriod)
                    {
                        if ((dtNow.Hour == _formSchedule.ScheduleCollection.SpecialScheduleStartAt.Hour) &&
                            (dtNow.Minute == _formSchedule.ScheduleCollection.SpecialScheduleStartAt.Minute))
                        {
                            StartScreenCapture(_formSchedule.ScheduleCollection.SpecialScheduleScreenCaptureInterval);
                        }

                        if ((dtNow.Hour == _formSchedule.ScheduleCollection.SpecialScheduleStopAt.Hour) &&
                            (dtNow.Minute == _formSchedule.ScheduleCollection.SpecialScheduleStopAt.Minute))
                        {
                            StopScreenCapture();

                            _formSchedule.ScheduleCollection.SpecialScheduleActivated = false;
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
                                TakeScreenshot(schedule.Scope, captureNow: true);
                            }
                        }

                        if (schedule.ModePeriod)
                        {
                            if (schedule.Logic == 0)
                            {
                                if ((dtNow.Hour == schedule.StartAt.Hour) &&
                                    (dtNow.Minute == schedule.StartAt.Minute) &&
                                    (dtNow.Second == schedule.StartAt.Second))
                                {
                                    StartScreenCapture(schedule.ScreenCaptureInterval, schedule.Scope);
                                }

                                if ((dtNow.Hour == schedule.StopAt.Hour) &&
                                    (dtNow.Minute == schedule.StopAt.Minute) &&
                                    (dtNow.Second == schedule.StopAt.Second))
                                {
                                    StopScreenCapture();
                                }
                            }

                            if (schedule.Logic == 1)
                            {
                                if ((dtNow.Hour == schedule.StartAt.Hour) &&
                                    (dtNow.Minute == schedule.StartAt.Minute) &&
                                    (dtNow.Second == schedule.StartAt.Second))
                                {
                                    StartSchedule(schedule);
                                }

                                if ((dtNow.Hour == schedule.StopAt.Hour) &&
                                    (dtNow.Minute == schedule.StopAt.Minute) &&
                                    (dtNow.Second == schedule.StopAt.Second))
                                {
                                    StopSchedule(schedule);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-Schedules::timerScheduleCheck_Tick", ex);
            }
        }

        /// <summary>
        /// An event handler to take screenshots whenever a schedule's interval's "tick" has elapsed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleTimer_Tick(object sender, EventArgs e)
        {
            if (sender.GetType().Equals(typeof(Timer)))
            {
                Timer timer = (Timer)sender;

                if (timer.Tag != null && timer.Tag.GetType().Equals(typeof(Schedule)))
                {
                    Schedule schedule = (Schedule)timer.Tag;

                    if (schedule.Enable)
                    {
                        TakeScreenshot(schedule.Scope, captureNow: true);
                    }
                }
            }
        }

        private void addSchedule_Click(object sender, EventArgs e)
        {
            ShowInterface();

            _formSchedule.ScheduleObject = null;

            int screenCaptureInterval = _dataConvert.ConvertIntoMilliseconds((int)_formSetup.numericUpDownHoursInterval.Value,
                        (int)_formSetup.numericUpDownMinutesInterval.Value, (int)_formSetup.numericUpDownSecondsInterval.Value);

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
                if (_formSchedule.ScheduleObject != null)
                {
                    // Initialize the Tick event but keep it disabled for now.
                    _formSchedule.ScheduleObject.Timer.Tag = _formSchedule.ScheduleObject;
                    _formSchedule.ScheduleObject.Timer.Enabled = false;
                }

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

        /// <summary>
        /// Shows the "Configure Schedule" window to enable the user to edit a chosen Schedule.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configureSchedule_Click(object sender, EventArgs e)
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

        private void StartSchedule(Schedule schedule)
        {
            if (schedule != null)
            {
                // This schedule is being started with the intention that it controls the main timer and overrides its interval and scope.
                if (schedule.Logic == 0)
                {
                    // Start the main timer.
                    StartScreenCapture(schedule.ScreenCaptureInterval, schedule.Scope);
                }

                // This schedule is being started with the intention that it runs with its own independent interval and scope.
                if (schedule.Logic == 1)
                {
                    if (!schedule.Timer.Enabled)
                    {
                        // Dynamically change the controls of the schedule in the Schedules module list.
                        TextBox scheduleTextBox = (TextBox)tabControlModules.Controls["tabPageSchedules"].Controls[schedule.Name + "textBoxObjectName"];
                        scheduleTextBox.BackColor = System.Drawing.Color.PaleGreen;

                        Button scheduleButton = (Button)tabControlModules.Controls["tabPageSchedules"].Controls[schedule.Name + "buttonScheduleTimer"];
                        scheduleButton.Image = Properties.Resources.stop_screen_capture;

                        // Subscribe to the Tick event only if the schedule is new.
                        // Existing schedules would have already had their Tick events subscribed during LoadSettings.
                        // If we subscribe to the Tick event again for an existing schedule we end up creating a duplicate screenshot for every tick.
                        if (schedule.IsNew)
                        {
                            schedule.Timer.Tick += ScheduleTimer_Tick;
                        }
                        else
                        {
                            // For an existing schedule.
                            // Unsubscribe from "Start Schedule" and then subscribe to "Stop Schedule".
                            scheduleButton.Click -= ScheduleModuleList_StartSchedule;
                            scheduleButton.Click += ScheduleModuleList_StopSchedule;
                        }

                        schedule.Timer.Enabled = true;
                        schedule.Timer.Start();
                    }
                }
            }
        }

        private void StopSchedule(Schedule schedule)
        {
            if (schedule != null)
            {
                // Checking the Timer's Tag determines if this schedule is controlling the application's main timer or if the
                // schedule is acting indepdently on its own timer.
                if (schedule.Timer.Tag == null)
                {
                    // Stop the main timer.
                    StopScreenCapture();
                }
                else
                {
                    if (schedule.Timer.Enabled)
                    {
                        _screenCapture.CycleCount = 0;

                        // Dynamically change the controls of the schedule in the Schedules module list.
                        TextBox scheduleTextBox = (TextBox)tabControlModules.Controls["tabPageSchedules"].Controls[schedule.Name + "textBoxObjectName"];
                        scheduleTextBox.BackColor = System.Drawing.Color.LightYellow;

                        Button scheduleButton = (Button)tabControlModules.Controls["tabPageSchedules"].Controls[schedule.Name + "buttonScheduleTimer"];
                        scheduleButton.Image = Properties.Resources.start_screen_capture;

                        schedule.Timer.Stop();
                        schedule.Timer.Enabled = false;

                        // Unsubscribe to the Tick event only if the schedule is new.
                        if (schedule.IsNew)
                        {
                            schedule.Timer.Tick -= ScheduleTimer_Tick;
                        }
                        else
                        {
                            // For an existing schedule.
                            // Unsubscribe from "Stop Schedule" and then subscribe to "Start Schedule".
                            scheduleButton.Click -= ScheduleModuleList_StopSchedule;
                            scheduleButton.Click += ScheduleModuleList_StartSchedule;
                        }
                    }
                }
            }
        }

        private void _formSchedule_StartSchedule(object sender, EventArgs e)
        {
            StartSchedule(_formSchedule.ScheduleObject);
        }

        private void _formSchedule_StopSchedule(object sender, EventArgs e)
        {
            StopSchedule(_formSchedule.ScheduleObject);
        }

        private void ScheduleModuleList_StartSchedule(object sender, EventArgs e)
        {
            Button buttonStartSchedule = (Button)sender;

            Schedule scheduleToStart = (Schedule)buttonStartSchedule.Tag;
            scheduleToStart.IsNew = false;

            StartSchedule(scheduleToStart);
        }

        private void ScheduleModuleList_StopSchedule(object sender, EventArgs e)
        {
            Button buttonStopSchedule = (Button)sender;

            StopSchedule((Schedule)buttonStopSchedule.Tag);
        }
    }
}
