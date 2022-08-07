//-----------------------------------------------------------------------
// <copyright file="FormSchedule.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for adding a new schedule or changing an existing schedule.</summary>
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
    /// <summary>
    /// A form for managing a schedule.
    /// </summary>
    public partial class FormSchedule : Form
    {
        private bool _ready;

        private readonly FormScreen _formScreen;
        private readonly FormRegion _formRegion;

        private readonly DataConvert _dataConvert;

        private readonly ToolTip _toolTip = new ToolTip();

        /// <summary>
        /// A collection of schedules.
        /// </summary>
        public ScheduleCollection ScheduleCollection { get; private set; }

        /// <summary>
        /// The screen capture interval for the schedule to use for overriding the main screen capture interval.
        /// </summary>
        public int ScreenCaptureInterval { get; set; }

        /// <summary>
        /// The schedule object to handle.
        /// </summary>
        public Schedule ScheduleObject { get; set; }

        /// <summary>
        /// An event handler for handling the start schedule event.
        /// </summary>
        public event EventHandler StartSchedule;

        /// <summary>
        /// An event handler for handling the stop schedule event.
        /// </summary>
        public event EventHandler StopSchedule;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public FormSchedule(FormScreen formScreen, FormRegion formRegion)
        {
            InitializeComponent();

            _formScreen = formScreen;
            _formRegion = formRegion;

            _dataConvert = new DataConvert();
            ScheduleCollection = new ScheduleCollection();
        }

        private void FormSchedule_Load(object sender, EventArgs e)
        {
            _ready = false;

            textBoxName.Focus();

            HelpMessage("This is where to configure a schedule to determine when screenshots should be taken");

            buttonStartSchedule.Enabled = false;
            buttonStopSchedule.Enabled = false;

            _toolTip.SetToolTip(checkBoxEnable, "The day and time values in this schedule will be considered if Enable is checked (turned on)");
            _toolTip.SetToolTip(dateTimePickerCaptureAt, "The time at which screenshots will be taken for a single capture cycle");
            _toolTip.SetToolTip(dateTimePickerStartAt, "The time at which a screen capture session will start running");
            _toolTip.SetToolTip(dateTimePickerStopAt, "The time at which a running screen capture session will stop");

            if (ScheduleObject != null)
            {
                Text = "Configure Schedule";

                textBoxName.Text = ScheduleObject.Name;
                checkBoxEnable.Checked = ScheduleObject.Enable;

                radioButtonOneTime.Checked = ScheduleObject.ModeOneTime;
                radioButtonPeriod.Checked = ScheduleObject.ModePeriod;

                dateTimePickerCaptureAt.Value = ScheduleObject.CaptureAt;
                dateTimePickerStartAt.Value = ScheduleObject.StartAt;
                dateTimePickerStopAt.Value = ScheduleObject.StopAt;

                decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(ScheduleObject.ScreenCaptureInterval)).Hours);
                decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(ScheduleObject.ScreenCaptureInterval)).Minutes);
                decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(ScheduleObject.ScreenCaptureInterval)).Seconds);

                numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;

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

                comboBoxLogic.SelectedIndex = ScheduleObject.Logic;

                textBoxNotes.Text = ScheduleObject.Notes;
            }
            else
            {
                Text = "Add Schedule";

                textBoxName.Text = "Schedule " + (ScheduleCollection.Count + 1);
                checkBoxEnable.Checked = true;

                dateTimePickerCaptureAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);
                dateTimePickerStartAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);
                dateTimePickerStopAt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0);

                decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(ScreenCaptureInterval)).Hours);
                decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(ScreenCaptureInterval)).Minutes);
                decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(ScreenCaptureInterval)).Seconds);

                numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;

                radioButtonOneTime.Checked = true;
                radioButtonPeriod.Checked = false;

                labelTakeScreenshotsOnce.Enabled = true;
                dateTimePickerCaptureAt.Enabled = true;

                labelTakeScreenshotsPeriod.Enabled = false;
                dateTimePickerStartAt.Enabled = false;
                labelAnd.Enabled = false;
                dateTimePickerStopAt.Enabled = false;
                labelLogic.Enabled = false;
                comboBoxLogic.Enabled = false;
                groupBoxInterval.Enabled = false;
                numericUpDownHoursInterval.Enabled = false;
                numericUpDownMinutesInterval.Enabled = false;
                numericUpDownSecondsInterval.Enabled = false;

                checkBoxWorkWeek.Checked = true;
                checkBoxWeekend.Checked = false;

                comboBoxLogic.SelectedIndex = 0;

                textBoxNotes.Text = string.Empty;

                ScheduleObject = CreateScheduleObject();

                ScheduleObject.IsNew = true;
            }

            comboBoxScope.Items.Clear();

            comboBoxScope.Items.Add("All Screens and Regions");
            comboBoxScope.Items.Add("All Screens");
            comboBoxScope.Items.Add("All Regions");

            foreach (Screen screen in _formScreen.ScreenCollection)
            {
                comboBoxScope.Items.Add(screen.Name);
            }

            foreach (Region region in _formRegion.RegionCollection)
            {
                comboBoxScope.Items.Add(region.Name);
            }

            comboBoxScope.SelectedIndex = 0;

            if (ScheduleObject != null && !string.IsNullOrEmpty(ScheduleObject.Scope) && comboBoxScope.Items.Contains(ScheduleObject.Scope))
            {
                comboBoxScope.SelectedIndex = comboBoxScope.Items.IndexOf(ScheduleObject.Scope);
            }

            CheckTimerEnabled();

            _ready = true;
        }

        private void HelpMessage(string message)
        {
            labelHelp.Text = "       " + message;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // We can't rely on ScheduleObject being null (or not being null)
            // because we create a new ScheduleObject when the user wants to
            // use "Period". So we have to check the form's title text.

            if (Text.Equals("Add Schedule"))
            {
                AddSchedule();
            }
            else if (Text.Equals("Configure Schedule"))
            {
                ChangeSchedule();
            }
        }

        private Schedule CreateScheduleObject()
        {
            int screenCaptureInterval = _dataConvert.ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                        (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value);

            Schedule schedule = new Schedule()
            {
                Name = textBoxName.Text,
                Enable = checkBoxEnable.Checked,
                ModeOneTime = radioButtonOneTime.Checked,
                ModePeriod = radioButtonPeriod.Checked,
                CaptureAt = dateTimePickerCaptureAt.Value,
                StartAt = dateTimePickerStartAt.Value,
                StopAt = dateTimePickerStopAt.Value,
                ScreenCaptureInterval = screenCaptureInterval,
                Monday = checkBoxMonday.Checked,
                Tuesday = checkBoxTuesday.Checked,
                Wednesday = checkBoxWednesday.Checked,
                Thursday = checkBoxThursday.Checked,
                Friday = checkBoxFriday.Checked,
                Saturday = checkBoxSaturday.Checked,
                Sunday = checkBoxSunday.Checked,
                Notes = textBoxNotes.Text,
                Scope = comboBoxScope.Text,
                Logic = comboBoxLogic.SelectedIndex
            };

            // Set the schedule's timer interval and make sure to not enable the timer until we're ready for it.
            schedule.Timer.Interval = screenCaptureInterval;
            schedule.Timer.Enabled = false;

            return schedule;
        }

        private void ModifyScheduleObject()
        {
            if (!_ready)
            {
                return;
            }

            ScheduleObject.Name = textBoxName.Text;
            ScheduleObject.Enable = checkBoxEnable.Checked;
            ScheduleObject.ModeOneTime = radioButtonOneTime.Checked;
            ScheduleObject.ModePeriod = radioButtonPeriod.Checked;
            ScheduleObject.CaptureAt = dateTimePickerCaptureAt.Value;
            ScheduleObject.StartAt = dateTimePickerStartAt.Value;
            ScheduleObject.StopAt = dateTimePickerStopAt.Value;

            int screenCaptureInterval = _dataConvert.ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                            (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value);

            ScheduleObject.ScreenCaptureInterval = screenCaptureInterval;

            ScheduleObject.Monday = checkBoxMonday.Checked;
            ScheduleObject.Tuesday = checkBoxTuesday.Checked;
            ScheduleObject.Wednesday = checkBoxWednesday.Checked;
            ScheduleObject.Thursday = checkBoxThursday.Checked;
            ScheduleObject.Friday = checkBoxFriday.Checked;
            ScheduleObject.Saturday = checkBoxSaturday.Checked;
            ScheduleObject.Sunday = checkBoxSunday.Checked;

            ScheduleObject.Notes = textBoxNotes.Text;
            ScheduleObject.Scope = comboBoxScope.Text;
            ScheduleObject.Logic = comboBoxLogic.SelectedIndex;
        }

        private void AddSchedule()
        {
            if (InputValid())
            {
                TrimInput();

                if (ScheduleCollection.GetByName(textBoxName.Text) == null)
                {
                    ScheduleCollection.Add(ScheduleObject);

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
                TrimInput();

                if (ScheduleCollection.GetByName(textBoxName.Text) != null && NameChanged())
                {
                    MessageBox.Show("A schedule with this name already exists.", "Duplicate Name Conflict",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Okay();
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrimInput()
        {
            textBoxName.Text = textBoxName.Text.Trim();
            textBoxNotes.Text = textBoxNotes.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxName.Text) &&
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

        private bool NameChanged()
        {
            if (ScheduleObject != null &&
                !ScheduleObject.Name.Equals(textBoxName.Text))
            {
                return true;
            }

            return false;
        }

        private void Okay()
        {
            ScheduleObject.Name = textBoxName.Text;
            ScheduleObject.CaptureAt = dateTimePickerCaptureAt.Value;
            ScheduleObject.StartAt = dateTimePickerStartAt.Value;
            ScheduleObject.StopAt = dateTimePickerStopAt.Value;

            int screenCaptureInterval = _dataConvert.ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value);

            ScheduleObject.ScreenCaptureInterval = screenCaptureInterval;

            DialogResult = DialogResult.OK;

            Close();
        }

        private void radioButtonOneTime_CheckedChanged(object sender, EventArgs e)
        {
            ModifyScheduleObject();

            if (radioButtonOneTime.Checked)
            {
                labelTakeScreenshotsOnce.Enabled = true;
                dateTimePickerCaptureAt.Enabled = true;

                labelTakeScreenshotsPeriod.Enabled = false;
                dateTimePickerStartAt.Enabled = false;
                labelAnd.Enabled = false;
                dateTimePickerStopAt.Enabled = false;
                labelLogic.Enabled = false;
                comboBoxLogic.Enabled = false;
                groupBoxInterval.Enabled = false;
                numericUpDownHoursInterval.Enabled = false;
                numericUpDownMinutesInterval.Enabled = false;
                numericUpDownSecondsInterval.Enabled = false;

                StopSchedule.Invoke(sender, e);

                buttonStartSchedule.Enabled = false;
                buttonStopSchedule.Enabled = false;
            }

            CheckTimerEnabled();
        }

        private void radioButtonPeriod_CheckedChanged(object sender, EventArgs e)
        {
            ModifyScheduleObject();

            if (radioButtonPeriod.Checked)
            {
                labelTakeScreenshotsOnce.Enabled = false;
                dateTimePickerCaptureAt.Enabled = false;

                labelTakeScreenshotsPeriod.Enabled = true;
                dateTimePickerStartAt.Enabled = true;
                labelAnd.Enabled = true;
                dateTimePickerStopAt.Enabled = true;
                labelLogic.Enabled = true;
                comboBoxLogic.Enabled = true;
                groupBoxInterval.Enabled = true;
                numericUpDownHoursInterval.Enabled = true;
                numericUpDownMinutesInterval.Enabled = true;
                numericUpDownSecondsInterval.Enabled = true;
            }

            CheckTimerEnabled();
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

            ModifyScheduleObject();
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

            ModifyScheduleObject();
        }

        private void checkBoxAnyDay_CheckedChanged(object sender, EventArgs e)
        {
            ModifyScheduleObject();
        }

        private void comboBoxScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModifyScheduleObject();
        }

        private void comboBoxLogic_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModifyScheduleObject();
        }

        private void buttonStartSchedule_Click(object sender, EventArgs e)
        {
            groupBoxInterval.Enabled = false;
            buttonStartSchedule.Enabled = false;
            buttonStopSchedule.Enabled = true;

            int screenCaptureInterval = _dataConvert.ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                            (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value);

            ScheduleObject.Timer.Tag = ScheduleObject;

            if (comboBoxLogic.SelectedIndex == 0)
            {
                ScheduleObject.ScreenCaptureInterval = screenCaptureInterval;
            }
            else if (comboBoxLogic.SelectedIndex == 1)
            {
                ScheduleObject.Timer.Interval = screenCaptureInterval;
                ScheduleObject.Scope = comboBoxScope.Text;                
            }

            StartSchedule.Invoke(sender, e);
        }

        private void buttonStopSchedule_Click(object sender, EventArgs e)
        {
            groupBoxInterval.Enabled = true;
            buttonStartSchedule.Enabled = true;
            buttonStopSchedule.Enabled = false;

            if (comboBoxLogic.SelectedIndex == 0)
            {
                ScheduleObject.Timer.Tag = null;
            }
            else if (comboBoxLogic.SelectedIndex == 1)
            {
                ScheduleObject.Timer.Tag = ScheduleObject;
            }

            StopSchedule.Invoke(sender, e);
        }

        /// <summary>
        /// Determines if we need the Start Schedule and Stop Schedule buttons enabled or not (based on the schedule's timer's Enabled property).
        /// </summary>
        private void CheckTimerEnabled()
        {
            buttonStartSchedule.Enabled = false;
            buttonStopSchedule.Enabled = false;

            if (radioButtonPeriod.Checked && ScheduleObject != null && ScheduleObject.Timer != null)
            {
                if (ScheduleObject.Timer.Enabled)
                {
                    buttonStartSchedule.Enabled = false;
                    buttonStopSchedule.Enabled = true;
                }
                else
                {
                    buttonStartSchedule.Enabled = true;
                    buttonStopSchedule.Enabled = false;
                }
            }
        }
    }
}