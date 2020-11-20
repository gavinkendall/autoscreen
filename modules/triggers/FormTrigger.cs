//-----------------------------------------------------------------------
// <copyright file="FormTrigger.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for adding a new trigger or changing an existing trigger.</summary>
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
    /// A form for managing a trigger.
    /// </summary>
    public partial class FormTrigger : Form
    {
        /// <summary>
        /// A collection of triggers.
        /// </summary>
        public TriggerCollection TriggerCollection { get; } = new TriggerCollection();

        /// <summary>
        /// The trigger object to handle.
        /// </summary>
        public Trigger TriggerObject { get; set; }

        /// <summary>
        /// A collection of editors.
        /// </summary>
        public EditorCollection EditorCollection { get; set; }

        /// <summary>
        /// A collection of screens.
        /// </summary>
        public ScreenCollection ScreenCollection { get; set; }

        /// <summary>
        /// A collection of regions.
        /// </summary>
        public RegionCollection RegionCollection { get; set; }

        /// <summary>
        /// A collection of schedules.
        /// </summary>
        public ScheduleCollection ScheduleCollection { get; set; }

        /// <summary>
        /// A collection of tags.
        /// </summary>
        public TagCollection TagCollection { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public FormTrigger()
        {
            InitializeComponent();
        }

        private void FormTrigger_Load(object sender, EventArgs e)
        {
            textBoxTriggerName.Focus();

            HelpMessage("An action can be performed based on a specific condition (Screenshot Taken and Run Editor will edit taken screenshots with a chosen editor)");

            LoadActions();
            LoadConditions();

            if (TriggerObject != null)
            {
                Text = "Change Trigger";

                textBoxTriggerName.Text = TriggerObject.Name;
                listBoxCondition.SelectedIndex = (int)TriggerObject.ConditionType;
                listBoxAction.SelectedIndex = (int)TriggerObject.ActionType;
                checkBoxActive.Checked = TriggerObject.Active;

                dateTimePickerDate.Value = TriggerObject.Date;
                dateTimePickerTime.Value = TriggerObject.Time;

                if (string.IsNullOrEmpty(TriggerObject.Day))
                {
                    TriggerObject.Day = "Weekday";
                }

                comboBoxDay.SelectedIndex = comboBoxDay.Items.IndexOf(TriggerObject.Day);

                if (listBoxModuleItemList.Items.Count > 0 && TriggerObject.ModuleItem != null)
                {
                    listBoxModuleItemList.SelectedIndex = listBoxModuleItemList.Items.IndexOf(TriggerObject.ModuleItem);
                }

                decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(TriggerObject.ScreenCaptureInterval)).Hours);
                decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(TriggerObject.ScreenCaptureInterval)).Minutes);
                decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(TriggerObject.ScreenCaptureInterval)).Seconds);
                decimal screenCaptureIntervalMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(TriggerObject.ScreenCaptureInterval)).Milliseconds);

                numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;
            }
            else
            {
                Text = "Add New Trigger";

                textBoxTriggerName.Text = "Trigger " + (TriggerCollection.Count + 1);
                checkBoxActive.Checked = true;
                dateTimePickerDate.Value = DateTime.Now;
                dateTimePickerTime.Value = DateTime.Now;

                numericUpDownHoursInterval.Value = 0;
                numericUpDownMinutesInterval.Value = 0;
                numericUpDownSecondsInterval.Value = 0;
                numericUpDownMillisecondsInterval.Value = 0;
            }
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
            if (TriggerObject != null)
            {
                ChangeTrigger();
            }
            else
            {
                AddNewTrigger();
            }
        }

        private void AddNewTrigger()
        {
            if (InputValid())
            {
                TrimInput();

                if (TriggerCollection.GetByName(textBoxTriggerName.Text) == null)
                {
                    int screenCaptureInterval = DataConvert.ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                        (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value,
                        (int)numericUpDownMillisecondsInterval.Value);

                    Trigger trigger = new Trigger()
                    {
                        Name = textBoxTriggerName.Text,
                        ConditionType = (TriggerConditionType)listBoxCondition.SelectedIndex,
                        ActionType = (TriggerActionType)listBoxAction.SelectedIndex,
                        Active = checkBoxActive.Checked,
                        Date = dateTimePickerDate.Value,
                        Time = dateTimePickerTime.Value,
                        Day = comboBoxDay.Text,
                        ScreenCaptureInterval = screenCaptureInterval,
                        ModuleItem = listBoxModuleItemList.SelectedItem != null ? listBoxModuleItemList.SelectedItem.ToString() : string.Empty
                    };

                    TriggerCollection.Add(trigger);

                    Okay();
                }
                else
                {
                    MessageBox.Show("A trigger with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeTrigger()
        {
            if (InputValid())
            {
                if (NameChanged() || InputChanged())
                {
                    TrimInput();

                    if (TriggerCollection.GetByName(textBoxTriggerName.Text) != null && NameChanged())
                    {
                        MessageBox.Show("A trigger with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        TriggerCollection.Get(TriggerObject).Name = textBoxTriggerName.Text;
                        TriggerCollection.Get(TriggerObject).ConditionType = (TriggerConditionType)listBoxCondition.SelectedIndex;
                        TriggerCollection.Get(TriggerObject).ActionType = (TriggerActionType)listBoxAction.SelectedIndex;
                        TriggerCollection.Get(TriggerObject).Active = checkBoxActive.Checked;
                        TriggerCollection.Get(TriggerObject).Date = dateTimePickerDate.Value;
                        TriggerCollection.Get(TriggerObject).Time = dateTimePickerTime.Value;
                        TriggerCollection.Get(TriggerObject).Day = comboBoxDay.Text;

                        if (listBoxModuleItemList.SelectedItem != null)
                        {
                            TriggerCollection.Get(TriggerObject).ModuleItem = listBoxModuleItemList.SelectedItem.ToString();
                        }
                        else
                        {
                            TriggerCollection.Get(TriggerObject).ModuleItem = string.Empty;
                        }

                        int screenCaptureInterval = DataConvert.ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                            (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value,
                            (int)numericUpDownMillisecondsInterval.Value);

                        TriggerCollection.Get(TriggerObject).ScreenCaptureInterval = screenCaptureInterval;

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
            textBoxTriggerName.Text = textBoxTriggerName.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxTriggerName.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InputChanged()
        {
            int screenCaptureInterval = DataConvert.ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                            (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value,
                            (int)numericUpDownMillisecondsInterval.Value);

            if (TriggerObject != null &&
                ((int)TriggerObject.ConditionType != listBoxCondition.SelectedIndex ||
                (int)TriggerObject.ActionType != listBoxAction.SelectedIndex ||
                (listBoxModuleItemList.Items.Count > 0 && TriggerObject.ModuleItem != null &&
                listBoxModuleItemList.SelectedItem != null &&
                !TriggerObject.ModuleItem.Equals(listBoxModuleItemList.SelectedItem.ToString())) ||
                TriggerObject.Active != checkBoxActive.Checked ||
                TriggerObject.Date != dateTimePickerDate.Value ||
                TriggerObject.Time != dateTimePickerTime.Value ||
                !TriggerObject.ScreenCaptureInterval.Equals(screenCaptureInterval)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool NameChanged()
        {
            if (TriggerObject != null &&
                !TriggerObject.Name.Equals(textBoxTriggerName.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Okay()
        {
            DialogResult = DialogResult.OK;

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.EmailScreenshot)
            {
                MessageBox.Show("Please ensure that the application's email (SMTP) settings are correctly configured in order to automatically email screenshots to the intended recipient. It is important that you do not use this application to spam people. Thank you.\n\nThe settings are prefixed with \"Email\" and located in the following XML document used for application-wide settings ...\n" + FileSystem.ApplicationSettingsFile, "Check Email Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Close();
        }

        private void LoadConditions()
        {
            comboBoxDay.SelectedIndex = 0;

            listBoxCondition.Items.Clear();

            // Whatever changes you make here will need to reflect the exact order in which the conditions are listed in the TriggerConditionType class.
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ApplicationStartup, "Application Startup").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ApplicationExit, "Application Exit").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.InterfaceClosing, "Interface Closing").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.InterfaceHiding, "Interface Hiding").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.InterfaceShowing, "Interface Showing").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.LimitReached, "Limit Reached").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ScreenCaptureStarted, "Screen Capture Started").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ScreenCaptureStopped, "Screen Capture Stopped").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.AfterScreenshotTaken, "After Screenshot Taken").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.DateTime, "Date/Time").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.Time, "Time").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.DayTime, "Day/Time").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.BeforeScreenshotTaken, "Before Screenshot Taken").Description);

            listBoxCondition.SelectedIndex = 0;
        }

        private void LoadActions()
        {
            listBoxAction.Items.Clear();

            // Whatever changes you make here will need to reflect the exact order in which the actions are listed in the TriggerActionType class.
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.ExitApplication, "Exit Application").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.HideInterface, "Hide Interface").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.RunEditor, "Run Editor").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.ShowInterface, "Show Interface").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.StartScreenCapture, "Start Screen Capture").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.StopScreenCapture, "Stop Screen Capture").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.EmailScreenshot, "Email Screenshot").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.SetScreenCaptureInterval, "Set Screen Capture Interval").Description);

            // All the actions involving activating Screens, Regions, Schedules, Tags, and Triggers.
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.ActivateScreen, "Activate Screen").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.ActivateRegion, "Activate Region").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.ActivateSchedule, "Activate Schedule").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.ActivateTag, "Activate Tag").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.ActivateTrigger, "Activate Trigger").Description);

            // All the actions involving deactivating Screens, Regions, Schedules, Tags, and Triggers.
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DeactivateScreen, "Deactivate Screen").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DeactivateRegion, "Deactivate Region").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DeactivateSchedule, "Deactivate Schedule").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DeactivateTag, "Deactivate Tag").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DeactivateTrigger, "Deactivate Trigger").Description);

            listBoxAction.SelectedIndex = 0;
        }

        private void LoadModuleItems()
        {
            listBoxModuleItemList.Items.Clear();

            labelModuleItemList.Text = "Editor, Screen, Region, Schedule, Tag, or Trigger:";

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.RunEditor)
            {
                labelModuleItemList.Text = "Editor:";

                foreach (Editor editor in EditorCollection)
                {
                    if (editor != null && FileSystem.FileExists(editor.Application))
                    {
                        listBoxModuleItemList.Items.Add(editor.Name);
                    }
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.ActivateScreen ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.DeactivateScreen)
            {
                labelModuleItemList.Text = "Screen:";

                foreach (Screen screen in ScreenCollection)
                {
                    if (screen != null)
                    {
                        listBoxModuleItemList.Items.Add(screen.Name);
                    }
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.ActivateRegion ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.DeactivateRegion)
            {
                labelModuleItemList.Text = "Region:";

                foreach (Region region in RegionCollection)
                {
                    if (region != null)
                    {
                        listBoxModuleItemList.Items.Add(region.Name);
                    }
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.ActivateSchedule ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.DeactivateSchedule)
            {
                labelModuleItemList.Text = "Schedule:";

                foreach (Schedule schedule in ScheduleCollection)
                {
                    if (schedule != null)
                    {
                        listBoxModuleItemList.Items.Add(schedule.Name);
                    }
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.ActivateTag ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.DeactivateTag)
            {
                labelModuleItemList.Text = "Tag:";

                foreach (Tag tag in TagCollection)
                {
                    if (tag != null)
                    {
                        listBoxModuleItemList.Items.Add(tag.Name);
                    }
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.ActivateTrigger ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.DeactivateTrigger)
            {
                labelModuleItemList.Text = "Trigger:";

                foreach (Trigger trigger in TriggerCollection)
                {
                    if (trigger != null)
                    {
                        listBoxModuleItemList.Items.Add(trigger.Name);
                    }
                }
            }

            if (listBoxModuleItemList.Items.Count > 0)
            {
                listBoxModuleItemList.SelectedIndex = 0;
            }
        }

        private void listBoxCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableOrDisableDateTime();
        }

        private void listBoxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableOrDisableScreenCaptureInterval();
            LoadModuleItems();
        }

        private void EnableOrDisableDateTime()
        {
            dateTimePickerDate.Enabled = false;
            dateTimePickerTime.Enabled = false;
            comboBoxDay.Enabled = false;

            labelDate.Enabled = false;
            labelTime.Enabled = false;
            labelDay.Enabled = false;

            if (listBoxCondition.SelectedIndex == (int)TriggerConditionType.DateTime)
            {
                dateTimePickerDate.Enabled = true;
                dateTimePickerTime.Enabled = true;

                labelDate.Enabled = true;
                labelTime.Enabled = true;
            }
            
            if (listBoxCondition.SelectedIndex == (int)TriggerConditionType.Time)
            {
                dateTimePickerTime.Enabled = true;
                labelTime.Enabled = true;
            }

            if (listBoxCondition.SelectedIndex == (int)TriggerConditionType.DayTime)
            {
                dateTimePickerTime.Enabled = true;
                comboBoxDay.Enabled = true;

                labelTime.Enabled = true;
                labelDay.Enabled = true;
            }
        }

        private void EnableOrDisableScreenCaptureInterval()
        {
            if (listBoxAction.SelectedIndex == (int)TriggerActionType.SetScreenCaptureInterval)
            {
                labelInterval.Enabled = true;
                numericUpDownHoursInterval.Enabled = true;
                numericUpDownMinutesInterval.Enabled = true;
                numericUpDownSecondsInterval.Enabled = true;
                numericUpDownMillisecondsInterval.Enabled = true;
            }
            else
            {
                labelInterval.Enabled = false;
                numericUpDownHoursInterval.Enabled = false;
                numericUpDownMinutesInterval.Enabled = false;
                numericUpDownSecondsInterval.Enabled = false;
                numericUpDownMillisecondsInterval.Enabled = false;
            }
        }

        private void checkBoxActive_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The trigger's condition will be considered and its associated action will be performed if Active is checked (turned on)");
        }

        private void listBoxCondition_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("A list of available trigger conditions. Select a condition that best represents the event you're interested in (such as Screenshot Taken)");
        }

        private void listBoxAction_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("A list of available trigger actions. Select an action to perform based on the selected condition that occurs (such as Run Editor)");
        }

        private void listBoxModuleItemList_MouseHover(object sender, EventArgs e)
        {
            string moduleItemType = string.Empty;

            switch(listBoxAction.SelectedIndex)
            {
                case (int)TriggerActionType.RunEditor:
                    moduleItemType = "editors";
                    break;

                case (int)TriggerActionType.ActivateScreen:
                case (int)TriggerActionType.DeactivateScreen:
                    moduleItemType = "screens";
                    break;

                case (int)TriggerActionType.ActivateRegion:
                case (int)TriggerActionType.DeactivateRegion:
                    moduleItemType = "regions";
                    break;

                case (int)TriggerActionType.ActivateSchedule:
                case (int)TriggerActionType.DeactivateSchedule:
                    moduleItemType = "schedules";
                    break;

                case (int)TriggerActionType.ActivateTag:
                case (int)TriggerActionType.DeactivateTag:
                    moduleItemType = "tags";
                    break;

                case (int)TriggerActionType.ActivateTrigger:
                case (int)TriggerActionType.DeactivateTrigger:
                    moduleItemType = "triggers";
                    break;
            }

            if (!string.IsNullOrEmpty(moduleItemType))
            {
                HelpMessage("A list of available " + moduleItemType);
            }
        }
    }
}