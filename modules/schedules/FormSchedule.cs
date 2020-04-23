//-----------------------------------------------------------------------
// <copyright file="FormSchedule.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormSchedule : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public ScheduleCollection ScheduleCollection { get; } = new ScheduleCollection();

        /// <summary>
        /// 
        /// </summary>
        public Schedule ScheduleObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FormSchedule()
        {
            InitializeComponent();
        }

        private void FormSchedule_Load(object sender, EventArgs e)
        {
            if (ScheduleObject != null)
            {
                Text = "Change Schedule";

                textBoxScheduleName.Text = ScheduleObject.Name;
                checkBoxEnabled.Checked = ScheduleObject.Enabled;

                radioButtonOneTime.Checked = ScheduleObject.ModeOneTime;
                radioButtonPeriod.Checked = ScheduleObject.ModePeriod;

                dateTimePickerSingleShot.Value = ScheduleObject.CaptureAt;
                dateTimePickerScheduleStartAt.Value = ScheduleObject.StartAt;
                dateTimePickerScheduleStopAt.Value = ScheduleObject.StopAt;

                checkBoxMonday.Checked = ScheduleObject.Monday;
                checkBoxTuesday.Checked = ScheduleObject.Tuesday;
                checkBoxWednesday.Checked = ScheduleObject.Wednesday;
                checkBoxThursday.Checked = ScheduleObject.Thursday;
                checkBoxFriday.Checked = ScheduleObject.Friday;
                checkBoxSaturday.Checked = ScheduleObject.Saturday;
                checkBoxSunday.Checked = ScheduleObject.Sunday;

                if (checkBoxMonday.Checked &&
                    checkBoxTuesday.Checked &&
                    checkBoxWednesday.Checked &&
                    checkBoxThursday.Checked &&
                    checkBoxFriday.Checked)
                {
                    checkBoxWorkWeek.Checked = true;
                }

                if (checkBoxSaturday.Checked &&
                    checkBoxSunday.Checked)
                {
                    checkBoxWeekend.Checked = true;
                }
            }
            else
            {
                Text = "Add New Schedule";

                textBoxScheduleName.Text = "Schedule " + (ScheduleCollection.Count + 1);
                checkBoxEnabled.Checked = true;

                dateTimePickerSingleShot.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);
                dateTimePickerScheduleStartAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);
                dateTimePickerScheduleStopAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0);

                radioButtonOneTime.Checked = true;
                radioButtonPeriod.Checked = false;

                labelTakeScreenshotsOnce.Enabled = true;
                dateTimePickerSingleShot.Enabled = true;

                labelTakeScreenshotsPeriod.Enabled = false;
                dateTimePickerScheduleStartAt.Enabled = false;
                labelAnd.Enabled = false;
                dateTimePickerScheduleStopAt.Enabled = false;

                checkBoxWorkWeek.Checked = true;
                checkBoxWeekend.Checked = true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ScheduleObject != null)
            {
                ChangeSchedule();
            }
            else
            {
                AddNewSchedule();
            }
        }

        private void AddNewSchedule()
        {
            if (InputValid())
            {
                TrimInput();

                if (ScheduleCollection.GetByName(textBoxScheduleName.Text) == null)
                {
                    Schedule schedule = new Schedule()
                    {
                        Name = textBoxScheduleName.Text,
                        Enabled = checkBoxEnabled.Checked,
                        ModeOneTime = radioButtonOneTime.Checked,
                        ModePeriod = radioButtonPeriod.Checked,
                        CaptureAt = dateTimePickerSingleShot.Value,
                        StartAt = dateTimePickerScheduleStartAt.Value,
                        StopAt = dateTimePickerScheduleStopAt.Value,
                        Monday = checkBoxMonday.Checked,
                        Tuesday = checkBoxTuesday.Checked,
                        Wednesday = checkBoxWednesday.Checked,
                        Thursday = checkBoxThursday.Checked,
                        Friday = checkBoxFriday.Checked,
                        Saturday = checkBoxSaturday.Checked,
                        Sunday = checkBoxSunday.Checked
                    };

                    ScheduleCollection.Add(schedule);

                    Okay();
                }
                else
                {
                    MessageBox.Show("A schedule with this name already exists.", "Duplicate Name Conflict",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeSchedule()
        {
            if (InputValid())
            {
                if (NameChanged() || InputChanged())
                {
                    TrimInput();

                    if (ScheduleCollection.GetByName(textBoxScheduleName.Text) != null && NameChanged())
                    {
                        MessageBox.Show("A schedule with this name already exists.", "Duplicate Name Conflict",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ScheduleCollection.Get(ScheduleObject).Name = textBoxScheduleName.Text;
                        ScheduleCollection.Get(ScheduleObject).Enabled = checkBoxEnabled.Checked;
                        ScheduleCollection.Get(ScheduleObject).ModeOneTime = radioButtonOneTime.Checked;
                        ScheduleCollection.Get(ScheduleObject).ModePeriod = radioButtonPeriod.Checked;
                        ScheduleCollection.Get(ScheduleObject).CaptureAt = dateTimePickerSingleShot.Value;
                        ScheduleCollection.Get(ScheduleObject).StartAt = dateTimePickerScheduleStartAt.Value;
                        ScheduleCollection.Get(ScheduleObject).StopAt = dateTimePickerScheduleStopAt.Value;
                        ScheduleCollection.Get(ScheduleObject).Monday = checkBoxMonday.Checked;
                        ScheduleCollection.Get(ScheduleObject).Tuesday = checkBoxTuesday.Checked;
                        ScheduleCollection.Get(ScheduleObject).Wednesday = checkBoxWednesday.Checked;
                        ScheduleCollection.Get(ScheduleObject).Thursday = checkBoxThursday.Checked;
                        ScheduleCollection.Get(ScheduleObject).Friday = checkBoxFriday.Checked;
                        ScheduleCollection.Get(ScheduleObject).Saturday = checkBoxSaturday.Checked;
                        ScheduleCollection.Get(ScheduleObject).Sunday = checkBoxSunday.Checked;

                        Okay();
                    }
                }
                else
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrimInput()
        {
            textBoxScheduleName.Text = textBoxScheduleName.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxScheduleName.Text) &&
                (checkBoxMonday.Checked ||
                checkBoxTuesday.Checked ||
                checkBoxWednesday.Checked ||
                checkBoxThursday.Checked ||
                checkBoxFriday.Checked ||
                checkBoxSaturday.Checked ||
                checkBoxSunday.Checked))
            {
                return true;
            }

            return false;
        }

        private bool InputChanged()
        {
            if (ScheduleObject != null &&
                (!ScheduleObject.Enabled.Equals(checkBoxEnabled.Checked) ||
                !ScheduleObject.CaptureAt.Equals(dateTimePickerSingleShot.Value) ||
                !ScheduleObject.StartAt.Equals(dateTimePickerScheduleStartAt.Value) ||
                !ScheduleObject.StopAt.Equals(dateTimePickerScheduleStopAt.Value) ||
                !ScheduleObject.Monday.Equals(checkBoxMonday.Checked) ||
                !ScheduleObject.Tuesday.Equals(checkBoxTuesday.Checked) ||
                !ScheduleObject.Wednesday.Equals(checkBoxWednesday.Checked) ||
                !ScheduleObject.Thursday.Equals(checkBoxThursday.Checked) ||
                !ScheduleObject.Friday.Equals(checkBoxFriday.Checked) ||
                !ScheduleObject.Saturday.Equals(checkBoxSaturday.Checked) ||
                !ScheduleObject.Sunday.Equals(checkBoxSunday.Checked)))
            {
                return true;
            }

            return false;
        }

        private bool NameChanged()
        {
            if (ScheduleObject != null &&
                !ScheduleObject.Name.Equals(textBoxScheduleName.Text))
            {
                return true;
            }

            return false;
        }

        private void Okay()
        {
            DialogResult = DialogResult.OK;

            Close();
        }

        private void radioButtonOneTime_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOneTime.Checked)
            {
                labelTakeScreenshotsOnce.Enabled = true;
                dateTimePickerSingleShot.Enabled = true;

                labelTakeScreenshotsPeriod.Enabled = false;
                dateTimePickerScheduleStartAt.Enabled = false;
                labelAnd.Enabled = false;
                dateTimePickerScheduleStopAt.Enabled = false;
            }
        }

        private void radioButtonPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPeriod.Checked)
            {
                labelTakeScreenshotsOnce.Enabled = false;
                dateTimePickerSingleShot.Enabled = false;

                labelTakeScreenshotsPeriod.Enabled = true;
                dateTimePickerScheduleStartAt.Enabled = true;
                labelAnd.Enabled = true;
                dateTimePickerScheduleStopAt.Enabled = true;
            }
        }

        private void checkBoxWorkWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWorkWeek.Checked)
            {
                checkBoxMonday.Checked = true;
                checkBoxTuesday.Checked = true;
                checkBoxWednesday.Checked = true;
                checkBoxThursday.Checked = true;
                checkBoxFriday.Checked = true;
            }
            else
            {
                checkBoxMonday.Checked = false;
                checkBoxTuesday.Checked = false;
                checkBoxWednesday.Checked = false;
                checkBoxThursday.Checked = false;
                checkBoxFriday.Checked = false;
            }
        }

        private void checkBoxWeekend_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWeekend.Checked)
            {
                checkBoxSaturday.Checked = true;
                checkBoxSunday.Checked = true;
            }
            else
            {
                checkBoxSaturday.Checked = false;
                checkBoxSunday.Checked = false;
            }
        }
    }
}