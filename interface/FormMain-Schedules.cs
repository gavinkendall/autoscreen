using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// The timer used for monitoring externally issued commands, running Schedules, and displaying capture information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerScheduledCapture_Tick(object sender, EventArgs e)
        {
            if (Directory.Exists(FileSystem.CommandFolder))
            {
                foreach (string file in Directory.GetFiles(FileSystem.CommandFolder))
                {
                    if (file.EndsWith("debug") && File.Exists(file))
                    {
                        File.Delete(file);
                        Log.DebugMode = !Log.DebugMode;
                        Settings.Application.GetByKey("DebugMode", defaultValue: false).Value = Log.DebugMode;
                        Settings.Application.Save();
                        break;
                    }

                    if (file.EndsWith("debug_on") && File.Exists(file))
                    {
                        File.Delete(file);
                        Log.DebugMode = true;
                        Settings.Application.GetByKey("DebugMode", defaultValue: false).Value = true;
                        Settings.Application.Save();
                        break;
                    }

                    if (file.EndsWith("debug_off") && File.Exists(file))
                    {
                        File.Delete(file);
                        Log.DebugMode = false;
                        Settings.Application.GetByKey("DebugMode", defaultValue: false).Value = false;
                        Settings.Application.Save();
                        break;
                    }

                    if (file.EndsWith("log") && File.Exists(file))
                    {
                        File.Delete(file);
                        Log.LoggingEnabled = !Log.LoggingEnabled;
                        Settings.Application.GetByKey("Logging", defaultValue: false).Value = Log.LoggingEnabled;
                        Settings.Application.Save();
                        break;
                    }

                    if (file.EndsWith("log_on") && File.Exists(file))
                    {
                        File.Delete(file);
                        Log.LoggingEnabled = true;
                        Settings.Application.GetByKey("Logging", defaultValue: false).Value = true;
                        Settings.Application.Save();
                        break;
                    }

                    if (file.EndsWith("log_off") && File.Exists(file))
                    {
                        File.Delete(file);
                        Log.LoggingEnabled = false;
                        Settings.Application.GetByKey("Logging", defaultValue: false).Value = false;
                        Settings.Application.Save();
                        break;
                    }

                    if (file.EndsWith("capture") && File.Exists(file))
                    {
                        File.Delete(file);
                        TakeScreenshot(captureNow: true);
                        break;
                    }

                    if (file.EndsWith("start") && File.Exists(file))
                    {
                        File.Delete(file);
                        StartScreenCapture();
                        break;
                    }

                    if (file.EndsWith("stop") && File.Exists(file))
                    {
                        File.Delete(file);
                        StopScreenCapture();
                        break;
                    }

                    if (file.EndsWith("exit") && File.Exists(file))
                    {
                        File.Delete(file);
                        ExitApplication();
                        break;
                    }
                }
            }

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

        private void addSchedule_Click(object sender, EventArgs e)
        {
            formSchedule.ScheduleObject = null;

            formSchedule.ShowDialog(this);

            if (formSchedule.DialogResult == DialogResult.OK)
            {
                BuildSchedulesModule();

                formSchedule.ScheduleCollection.SaveToXmlFile();
            }
        }

        private void removeSelectedSchedules_Click(object sender, EventArgs e)
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
