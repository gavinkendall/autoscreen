using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// The timer used for Schedules and displaying capture information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScheduledCapture(object sender, EventArgs e)
        {
            ShowInfo();

            foreach(Schedule schedule in formSchedule.ScheduleCollection)
            {
                DateTime dtNow = DateTime.Now;

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
        }

        private void Click_addSchedule(object sender, EventArgs e)
        {
            formSchedule.ScheduleObject = null;

            formSchedule.ShowDialog(this);

            if (formSchedule.DialogResult == DialogResult.OK)
            {
                BuildSchedulesModule();

                formSchedule.ScheduleCollection.SaveToXmlFile();
            }
        }

        private void Click_removeSelectedSchedules(object sender, EventArgs e)
        {
            int countBeforeRemoval = formSchedule.ScheduleCollection.Count;

            foreach (Control control in tabPageSchedules.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Schedule schedule = formSchedule.ScheduleCollection.Get((Schedule)checkBox.Tag);
                        formSchedule.ScheduleCollection.Remove(schedule);
                    }
                }
            }

            if (countBeforeRemoval > formSchedule.ScheduleCollection.Count)
            {
                BuildSchedulesModule();

                formSchedule.ScheduleCollection.SaveToXmlFile();
            }
        }

        private void Click_changeSchedule(object sender, EventArgs e)
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

            formSchedule.ScheduleObject = schedule;

            formSchedule.ShowDialog(this);

            if (formSchedule.DialogResult == DialogResult.OK)
            {
                BuildSchedulesModule();

                formSchedule.ScheduleCollection.SaveToXmlFile();
            }
        }
    }
}
