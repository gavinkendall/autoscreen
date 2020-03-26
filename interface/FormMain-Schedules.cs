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
        /// The timer used for starting scheduled screen capture sessions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScheduledCaptureStart(object sender, EventArgs e)
        {
            // This timer needs to loop over the Schedules to determine start and stop times.

            //if (checkBoxScheduleStartAt.Checked)
            //{
            //    if (checkBoxScheduleOnTheseDays.Checked)
            //    {
            //        if (((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && checkBoxSaturday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && checkBoxSunday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Monday && checkBoxMonday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && checkBoxTuesday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && checkBoxWednesday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && checkBoxThursday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Friday && checkBoxFriday.Checked)) &&
            //            ((DateTime.Now.Hour == dateTimePickerScheduleStartAt.Value.Hour) &&
            //             (DateTime.Now.Minute == dateTimePickerScheduleStartAt.Value.Minute) &&
            //             (DateTime.Now.Second == dateTimePickerScheduleStartAt.Value.Second)))
            //        {
            //            StartScreenCapture();
            //        }
            //    }
            //    else
            //    {
            //        if ((DateTime.Now.Hour == dateTimePickerScheduleStartAt.Value.Hour) &&
            //            (DateTime.Now.Minute == dateTimePickerScheduleStartAt.Value.Minute) &&
            //            (DateTime.Now.Second == dateTimePickerScheduleStartAt.Value.Second))
            //        {
            //            StartScreenCapture();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// The timer used for Schedules and displaying capture information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick_timerScheduledCapture(object sender, EventArgs e)
        {
            ShowInfo();

            //if (checkBoxScheduleStopAt.Checked)
            //{
            //    if (checkBoxScheduleOnTheseDays.Checked)
            //    {
            //        if (((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && checkBoxSaturday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && checkBoxSunday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Monday && checkBoxMonday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && checkBoxTuesday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && checkBoxWednesday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && checkBoxThursday.Checked) ||
            //             (DateTime.Now.DayOfWeek == DayOfWeek.Friday && checkBoxFriday.Checked)) &&
            //            ((DateTime.Now.Hour == dateTimePickerScheduleStopAt.Value.Hour) &&
            //             (DateTime.Now.Minute == dateTimePickerScheduleStopAt.Value.Minute) &&
            //             (DateTime.Now.Second == dateTimePickerScheduleStopAt.Value.Second)))
            //        {
            //            StopScreenCapture();
            //        }
            //    }
            //    else
            //    {
            //        if ((DateTime.Now.Hour == dateTimePickerScheduleStopAt.Value.Hour) &&
            //            (DateTime.Now.Minute == dateTimePickerScheduleStopAt.Value.Minute) &&
            //            (DateTime.Now.Second == dateTimePickerScheduleStopAt.Value.Second))
            //        {
            //            StopScreenCapture();
            //        }
            //    }
            //}
        }

        private void Click_addSchedule(object sender, EventArgs e)
        {

        }

        private void Click_removeSelectedSchedules(object sender, EventArgs e)
        {

        }

        private void Click_changeSchedule(object sender, EventArgs e)
        {

        }
    }
}
