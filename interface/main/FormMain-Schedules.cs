//-----------------------------------------------------------------------
// <copyright file="FormMain-Schedules.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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

                // Parse commands issued externally via the command line.
                if (FileSystem.FileExists(FileSystem.CommandFile))
                {
                    string[] args = FileSystem.ReadFromFile(FileSystem.CommandFile);

                    if (args.Length > 0)
                    {
                        ParseCommandLineArguments(args);
                    }
                }
                else
                {
                    FileSystem.CreateFile(FileSystem.CommandFile);
                }

                // Displays the next time screenshots are going to be captured in the system tray icon's tool tip.
                ShowInfo();

                // Process the list of schedules we need to consider.
                foreach (Schedule schedule in _formSchedule.ScheduleCollection)
                {
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
                            if ((dtNow.Hour == schedule.StartAt.Hour) &&
                                (dtNow.Minute == schedule.StartAt.Minute) &&
                                (dtNow.Second == schedule.StartAt.Second))
                            {
                                StartScreenCapture();
                            }

                            if ((dtNow.Hour == schedule.StopAt.Hour) &&
                                (dtNow.Minute == schedule.StopAt.Minute) &&
                                (dtNow.Second == schedule.StopAt.Second))
                            {
                                StopScreenCapture();
                            }
                        }
                    }
                }

                // Process the list of triggers of condition type Date/Time and condition type Time.
                foreach (Trigger trigger in _formTrigger.TriggerCollection)
                {
                    if (trigger.ConditionType == TriggerConditionType.DateTime &&
                        trigger.Date.ToString(MacroParser.DateFormat).Equals(dtNow.ToString(MacroParser.DateFormat)) &&
                        trigger.Time.ToString(MacroParser.TimeFormatForTrigger).Equals(dtNow.ToString(MacroParser.TimeFormatForTrigger)))
                    {
                        DoTriggerAction(trigger);
                    }

                    if (trigger.ConditionType == TriggerConditionType.Time &&
                        trigger.Time.ToString(MacroParser.TimeFormatForTrigger).Equals(dtNow.ToString(MacroParser.TimeFormatForTrigger)))
                    {
                        DoTriggerAction(trigger);
                    }
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                Log.WriteExceptionMessage("FormMain-Schedules::timerScheduledCapture_Tick", ex);
            }
        }

        private void addSchedule_Click(object sender, EventArgs e)
        {
            _formSchedule.ScheduleObject = null;

            _formSchedule.ShowDialog(this);

            if (_formSchedule.DialogResult == DialogResult.OK)
            {
                BuildSchedulesModule();

                if (!_formSchedule.ScheduleCollection.SaveToXmlFile())
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

                if (!_formSchedule.ScheduleCollection.SaveToXmlFile())
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        private void changeSchedule_Click(object sender, EventArgs e)
        {
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

            _formSchedule.ShowDialog(this);

            if (_formSchedule.DialogResult == DialogResult.OK)
            {
                BuildSchedulesModule();

                if (!_formSchedule.ScheduleCollection.SaveToXmlFile())
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }
    }
}
