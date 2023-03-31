//-----------------------------------------------------------------------
// <copyright file="FormTrigger.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
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
        private FileSystem _fileSystem;
        private DataConvert _dataConvert;

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
        public MacroTagCollection MacroTagCollection { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fileSystem">File system.</param>
        public FormTrigger(FileSystem fileSystem)
        {
            InitializeComponent();

            _fileSystem = fileSystem;
            _dataConvert = new DataConvert();
        }

        private void FormTrigger_Load(object sender, EventArgs e)
        {
            HelpMessage("A trigger is used to perform a certain action based on a specified condition");

            LoadActions();
            LoadConditions();

            if (TriggerObject != null)
            {
                Text = "Configure Trigger";

                textBoxTriggerName.Text = TriggerObject.Name;
                listBoxCondition.SelectedIndex = (int)TriggerObject.ConditionType;
                listBoxAction.SelectedIndex = (int)TriggerObject.ActionType;
                checkBoxEnable.Checked = TriggerObject.Enable;

                dateTimePickerDate.Value = TriggerObject.Date;
                dateTimePickerTime.Value = TriggerObject.Time;

                if (string.IsNullOrEmpty(TriggerObject.Day))
                {
                    TriggerObject.Day = "Weekday";
                }

                numericUpDownDays.Value = TriggerObject.Days;
                numericUpDownCycleCount.Value = TriggerObject.CycleCount;

                comboBoxDay.SelectedIndex = comboBoxDay.Items.IndexOf(TriggerObject.Day);

                if (!string.IsNullOrEmpty(TriggerObject.Value))
                {
                    if (listBoxModuleItemList.Items.Count > 0)
                    {
                        listBoxModuleItemList.SelectedIndex = listBoxModuleItemList.Items.IndexOf(TriggerObject.Value);
                    }
                    else
                    {
                        if (textBoxLabel.Enabled)
                        {
                            textBoxLabel.Text = TriggerObject.Value;
                        }
                        else if (textBoxApplicationFocus.Enabled)
                        {
                            textBoxApplicationFocus.Text = TriggerObject.Value;
                        }
                        else if (textBoxActiveWindowTitle.Enabled)
                        {
                            textBoxActiveWindowTitle.Text = TriggerObject.Value;
                        }
                        else if (textBoxDeleteFolder.Enabled)
                        {
                            textBoxDeleteFolder.Text = TriggerObject.Value;
                        }
                    }
                }

                decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(TriggerObject.ScreenCaptureInterval)).Hours);
                decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(TriggerObject.ScreenCaptureInterval)).Minutes);
                decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(TriggerObject.ScreenCaptureInterval)).Seconds);
                decimal screenCaptureIntervalMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(TriggerObject.ScreenCaptureInterval)).Milliseconds);

                numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                numericUpDownDuration.Value = TriggerObject.Duration;
                comboBoxDuration.SelectedIndex = TriggerObject.DurationType;
            }
            else
            {
                Text = "Add Trigger";

                textBoxTriggerName.Text = "Trigger " + (TriggerCollection.Count + 1);
                checkBoxEnable.Checked = true;

                dateTimePickerDate.Value = DateTime.Now;
                dateTimePickerTime.Value = DateTime.Now;

                numericUpDownHoursInterval.Value = 0;
                numericUpDownMinutesInterval.Value = 0;
                numericUpDownSecondsInterval.Value = 0;

                numericUpDownDuration.Value = 0;
                comboBoxDuration.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Shows a help message.
        /// </summary>
        /// <param name="message">The message to show as a help message.</param>
        private void HelpMessage(string message)
        {
            labelHelp.Text = "       " + message;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            if (TriggerObject != null)
            {
                ChangeTrigger();
            }
            else
            {
                AddTrigger();
            }
        }

        /// <summary>
        /// Adds a trigger to the collection of triggers.
        /// </summary>
        private void AddTrigger()
        {
            if (InputValid())
            {
                TrimInput();

                if (TriggerCollection.GetByName(textBoxTriggerName.Text) == null)
                {
                    int screenCaptureInterval = _dataConvert.ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                        (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value, (int)numericUpDownMillisecondsInterval.Value);

                    Trigger trigger = new Trigger()
                    {
                        Name = textBoxTriggerName.Text,
                        ConditionType = (TriggerConditionType)listBoxCondition.SelectedIndex,
                        ActionType = (TriggerActionType)listBoxAction.SelectedIndex,
                        Enable = checkBoxEnable.Checked,
                        Date = dateTimePickerDate.Value,
                        Time = new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day, dateTimePickerTime.Value.Hour, dateTimePickerTime.Value.Minute, 0),
                        Day = comboBoxDay.Text,
                        Days = (int)numericUpDownDays.Value,
                        CycleCount = (int)numericUpDownCycleCount.Value,
                        ScreenCaptureInterval = screenCaptureInterval,
                        Duration = (int)numericUpDownDuration.Value,
                        DurationType = comboBoxDuration.SelectedIndex
                    };

                    if (textBoxLabel.Enabled)
                    {
                        trigger.Value = textBoxLabel.Text.Trim();
                    }
                    else if (textBoxApplicationFocus.Enabled)
                    {
                        trigger.Value = textBoxApplicationFocus.Text.Trim();
                    }
                    else if (textBoxActiveWindowTitle.Enabled)
                    {
                        trigger.Value = textBoxActiveWindowTitle.Text.Trim();
                    }
                    else if (textBoxDeleteFolder.Enabled)
                    {
                        trigger.Value = textBoxDeleteFolder.Text.Trim();
                    }
                    else
                    {
                        trigger.Value = listBoxModuleItemList.SelectedItem != null ? listBoxModuleItemList.SelectedItem.ToString() : string.Empty;
                    }

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

        /// <summary>
        /// Changes the configuration of the trigger.
        /// </summary>
        private void ChangeTrigger()
        {
            if (InputValid())
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
                    TriggerCollection.Get(TriggerObject).Enable = checkBoxEnable.Checked;
                    TriggerCollection.Get(TriggerObject).Date = dateTimePickerDate.Value;
                    TriggerCollection.Get(TriggerObject).Time = new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day, dateTimePickerTime.Value.Hour, dateTimePickerTime.Value.Minute, 0);
                    TriggerCollection.Get(TriggerObject).Day = comboBoxDay.Text;
                    TriggerCollection.Get(TriggerObject).Days = (int)numericUpDownDays.Value;
                    TriggerCollection.Get(TriggerObject).CycleCount = (int)numericUpDownCycleCount.Value;
                    TriggerCollection.Get(TriggerObject).Duration = (int)numericUpDownDuration.Value;
                    TriggerCollection.Get(TriggerObject).DurationType = comboBoxDuration.SelectedIndex;

                    if (textBoxLabel.Enabled)
                    {
                        TriggerCollection.Get(TriggerObject).Value = textBoxLabel.Text.Trim();
                    }
                    else if (textBoxApplicationFocus.Enabled)
                    {
                        TriggerCollection.Get(TriggerObject).Value = textBoxApplicationFocus.Text.Trim();
                    }
                    else if (textBoxActiveWindowTitle.Enabled)
                    {
                        TriggerCollection.Get(TriggerObject).Value = textBoxActiveWindowTitle.Text.Trim();
                    }
                    else if (textBoxDeleteFolder.Enabled)
                    {
                        TriggerCollection.Get(TriggerObject).Value = textBoxDeleteFolder.Text.Trim();
                    }
                    else
                    {
                        if (listBoxModuleItemList.SelectedItem != null)
                        {
                            TriggerCollection.Get(TriggerObject).Value = listBoxModuleItemList.SelectedItem.ToString();
                        }
                        else
                        {
                            TriggerCollection.Get(TriggerObject).Value = string.Empty;
                        }
                    }

                    int screenCaptureInterval = _dataConvert.ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                        (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value, (int)numericUpDownMillisecondsInterval.Value);

                    TriggerCollection.Get(TriggerObject).ScreenCaptureInterval = screenCaptureInterval;

                    Okay();
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Trims the input.
        /// </summary>
        private void TrimInput()
        {
            textBoxTriggerName.Text = textBoxTriggerName.Text.Trim();
        }

        /// <summary>
        /// Validates the input.
        /// </summary>
        /// <returns>True if the input is valid. False if the input is not valid.</returns>
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

        /// <summary>
        /// Determines if the trigger's name changed.
        /// </summary>
        /// <returns>True if the trigger's name changed. False if the trigger's name doesn't change.</returns>
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

        /// <summary>
        /// What to do when the user clicks on the OK button.
        /// </summary>
        private void Okay()
        {
            DialogResult = DialogResult.OK;

            Close();
        }

        /// <summary>
        /// Populates the list of conditions.
        /// </summary>
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
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.BeforeScreenshotReferencesSaved, "Before Screenshot References Saved").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.AfterScreenshotReferencesSaved, "After Screenshot References Saved").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.SystemTrayIconDoubleClick, "System Tray Icon Double Click").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.CaptureCycleElapsed, "Capture Cycle Elapsed").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.DurationFromStartScreenCapture, "Duration From Start Screen Capture").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.DurationFromStopScreenCapture, "Duration From Stop Screen Capture").Description);

            listBoxCondition.SelectedIndex = 0;
        }

        /// <summary>
        /// Populates the list of actions.
        /// </summary>
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
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.EmailScreenshot, "Email Screenshot (SMTP)").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.SetScreenCaptureInterval, "Set Screen Capture Interval").Description);

            // All the actions involving activating Screens, Regions, Schedules, Tags, and Triggers.
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.EnableScreen, "Enable Screen").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.EnableRegion, "Enable Region").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.EnableSchedule, "Enable Schedule").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.EnableMacroTag, "Enable Macro Tag").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.EnableTrigger, "Enable Trigger").Description);

            // All the actions involving deactivating Screens, Regions, Schedules, Tags, and Triggers.
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DisableScreen, "Disable Screen").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DisableRegion, "Disable Region").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DisableSchedule, "Disable Schedule").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DisableMacroTag, "Disable Macro Tag").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DisableTrigger, "Disable Trigger").Description);

            // More actions.
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DeleteScreenshotsByDays, "Delete Screenshots By Days").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.SetLabel, "Apply Label").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.SetActiveWindowTitleAsMatch, "Set Active Window Title As Match").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.SetApplicationFocus, "Set Application Focus").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.FileTransferScreenshot, "File Transfer Screenshot (SFTP)").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.SetActiveWindowTitleAsNoMatch, "Set Active Window Title As No Match").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.ShowSystemTrayIcon, "Show System Tray Icon").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.HideSystemTrayIcon, "Hide System Tray Icon").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.TakeScreenshot, "Take Screenshot").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.RegionSelectClipboard, "Region Select Clipboard").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.RegionSelectClipboardAutoSave, "Region Select Clipboard Auto Save").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.RegionSelectClipboardAutoSaveEdit, "Region Select Clipboard Auto Save Edit").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.RegionSelectClipboardFloatingScreenshot, "Region Select Clipboard Floating Screenshot").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.RegionSelectFloatingScreenshot, "Region Select Floating Screenshot").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.ShowOrHideInterface, "Show Interface or Hide Interface").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.StartOrStopScreenCapture, "Start Screen Capture or Stop Screen Capture").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.RestartScreenCapture, "Restart Screen Capture").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DeleteScreenshotsByCycleCount, "Delete Screenshots By Cycle Count").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DeleteScreenshotsFromOldestCaptureCycle, "Delete Screenshots From Oldest Capture Cycle").Description);

            listBoxAction.SelectedIndex = 0;
        }

        /// <summary>
        /// Populates the list of editors, screens, regions, schedules, macro tags, or triggers (depending on what type of module has been selected).
        /// </summary>
        private void LoadModuleItems()
        {
            listBoxModuleItemList.Items.Clear();

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.RunEditor)
            {
                foreach (Editor editor in EditorCollection)
                {
                    if (editor != null && _fileSystem.FileExists(editor.Application))
                    {
                        listBoxModuleItemList.Items.Add(editor.Name);
                    }
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.EnableScreen ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.DisableScreen)
            {
                foreach (Screen screen in ScreenCollection)
                {
                    if (screen != null)
                    {
                        listBoxModuleItemList.Items.Add(screen.Name);
                    }
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.EnableRegion ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.DisableRegion)
            {
                foreach (Region region in RegionCollection)
                {
                    if (region != null)
                    {
                        listBoxModuleItemList.Items.Add(region.Name);
                    }
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.EnableSchedule ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.DisableSchedule)
            {
                foreach (Schedule schedule in ScheduleCollection)
                {
                    if (schedule != null)
                    {
                        listBoxModuleItemList.Items.Add(schedule.Name);
                    }
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.EnableMacroTag ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.DisableMacroTag)
            {
                foreach (MacroTag tag in MacroTagCollection)
                {
                    if (tag != null)
                    {
                        listBoxModuleItemList.Items.Add(tag.Name);
                    }
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.EnableTrigger ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.DisableTrigger)
            {
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

        /// <summary>
        /// Determines what controls to enable or disable based on the condition being selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowConditionHelpText();

            labelDate.Enabled = false;
            labelTime.Enabled = false;
            labelDay.Enabled = false;
            dateTimePickerDate.Enabled = false;
            dateTimePickerTime.Enabled = false;
            comboBoxDay.Enabled = false;

            labelDuration.Enabled = false;
            numericUpDownDuration.Enabled = false;
            comboBoxDuration.Enabled = false;

            if (listBoxCondition.SelectedIndex == (int)TriggerConditionType.DateTime)
            {
                labelDate.Enabled = true;
                labelTime.Enabled = true;
                dateTimePickerDate.Enabled = true;
                dateTimePickerTime.Enabled = true;
            }

            if (listBoxCondition.SelectedIndex == (int)TriggerConditionType.Time)
            {
                labelTime.Enabled = true;
                dateTimePickerTime.Enabled = true;
            }

            if (listBoxCondition.SelectedIndex == (int)TriggerConditionType.DayTime)
            {
                labelDay.Enabled = true;
                labelTime.Enabled = true;
                dateTimePickerTime.Enabled = true;
                comboBoxDay.Enabled = true;
            }

            if (listBoxCondition.SelectedIndex == (int)TriggerConditionType.DurationFromStartScreenCapture ||
                listBoxCondition.SelectedIndex == (int)TriggerConditionType.DurationFromStopScreenCapture)
            {
                labelDuration.Enabled = true;
                numericUpDownDuration.Enabled = true;
                comboBoxDuration.Enabled = true;
            }
        }

        /// <summary>
        /// Determines what controls to enable or disable based on the action being selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadModuleItems();

            ShowActionHelpText();

            labelLabel.Enabled = false;
            textBoxLabel.Enabled = false;

            labelApplicationFocus.Enabled = false;
            textBoxApplicationFocus.Enabled = false;

            labelActiveWindowTitle.Enabled = false;
            textBoxActiveWindowTitle.Enabled = false;

            groupBoxDeleteScreenshots.Enabled = false;
            labelCycleCount.Enabled = false;
            numericUpDownCycleCount.Enabled = false;
            labelDays.Enabled = false;
            numericUpDownDays.Enabled = false;
            labelDeleteFolder.Enabled = false;
            textBoxDeleteFolder.Enabled = false;

            groupBoxInterval.Enabled = false;

            numericUpDownHoursInterval.Enabled = false;
            numericUpDownMinutesInterval.Enabled = false;
            numericUpDownSecondsInterval.Enabled = false;
            numericUpDownMillisecondsInterval.Enabled = false;

            numericUpDownDays.Value = 30;

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.SetScreenCaptureInterval)
            {
                groupBoxInterval.Enabled = true;
                numericUpDownHoursInterval.Enabled = true;
                numericUpDownMinutesInterval.Enabled = true;
                numericUpDownSecondsInterval.Enabled = true;
                numericUpDownMillisecondsInterval.Enabled = true;
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.DeleteScreenshotsByDays)
            {
                groupBoxDeleteScreenshots.Enabled = true;
                labelDays.Enabled = true;
                numericUpDownDays.Enabled = true;
                labelDeleteFolder.Enabled = true;
                textBoxDeleteFolder.Enabled = true;

                // Suggest some values to help out the user. These are the default values used by the "Keep screenshots for 30 days" trigger.
                if (TriggerObject == null)
                {
                    numericUpDownDays.Value = 30;
                    textBoxDeleteFolder.Text = _fileSystem.ScreenshotsFolder + "$date[yyyy-MM-dd]$";
                }
                else if (TriggerObject.ActionType == TriggerActionType.DeleteScreenshotsByDays)
                {
                    numericUpDownDays.Value = TriggerObject.Days;
                    textBoxDeleteFolder.Text = TriggerObject.Value;
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.SetLabel ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.SetActiveWindowTitleAsMatch ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.SetActiveWindowTitleAsNoMatch ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.SetApplicationFocus)
            {
                if (listBoxAction.SelectedIndex == (int)TriggerActionType.SetLabel)
                {
                    labelLabel.Enabled = true;
                    textBoxLabel.Enabled = true;

                    if (TriggerObject != null)
                    {
                        textBoxLabel.Text = TriggerObject.Value;
                    }
                }

                if (listBoxAction.SelectedIndex == (int)TriggerActionType.SetApplicationFocus)
                {
                    labelApplicationFocus.Enabled = true;
                    textBoxApplicationFocus.Enabled = true;

                    if (TriggerObject != null)
                    {
                        textBoxApplicationFocus.Text = TriggerObject.Value;
                    }
                }

                if (listBoxAction.SelectedIndex == (int)TriggerActionType.SetActiveWindowTitleAsMatch ||
                    listBoxAction.SelectedIndex == (int)TriggerActionType.SetActiveWindowTitleAsNoMatch)
                {
                    labelActiveWindowTitle.Enabled = true;
                    textBoxActiveWindowTitle.Enabled = true;

                    if (TriggerObject != null)
                    {
                        textBoxActiveWindowTitle.Text = TriggerObject.Value;
                    }
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.DeleteScreenshotsByCycleCount)
            {
                groupBoxDeleteScreenshots.Enabled = true;
                labelCycleCount.Enabled = true;
                numericUpDownCycleCount.Enabled = true;
            }

            DetermineModuleListEnable();
        }

        /// <summary>
        /// Determines if we should enable the list of modules (this could be a list of screens, regions, schedules, macro tags, or triggers).
        /// </summary>
        private void DetermineModuleListEnable()
        {
            if (listBoxAction.SelectedIndex == (int)TriggerActionType.RunEditor ||
                        listBoxAction.SelectedIndex == (int)TriggerActionType.EnableScreen ||
                        listBoxAction.SelectedIndex == (int)TriggerActionType.EnableRegion ||
                        listBoxAction.SelectedIndex == (int)TriggerActionType.EnableSchedule ||
                        listBoxAction.SelectedIndex == (int)TriggerActionType.EnableMacroTag ||
                        listBoxAction.SelectedIndex == (int)TriggerActionType.EnableTrigger ||
                        listBoxAction.SelectedIndex == (int)TriggerActionType.DisableScreen ||
                        listBoxAction.SelectedIndex == (int)TriggerActionType.DisableRegion ||
                        listBoxAction.SelectedIndex == (int)TriggerActionType.DisableSchedule ||
                        listBoxAction.SelectedIndex == (int)TriggerActionType.DisableMacroTag ||
                        listBoxAction.SelectedIndex == (int)TriggerActionType.DisableTrigger)
            {
                listBoxModuleItemList.Enabled = true;
            }
            else
            {
                listBoxModuleItemList.Enabled = false;
            }
        }

        /// <summary>
        /// Displays the help text based on the selected condition.
        /// </summary>
        private void ShowConditionHelpText()
        {
            switch (listBoxCondition.SelectedIndex)
            {
                // Application Startup
                case 0:
                    textBoxConditionHelp.Text = "When the application is started.";
                    break;

                // Application Exit
                case 1:
                    textBoxConditionHelp.Text = "When the application is going to exit.";
                    break;

                // Interface Closing
                case 2:
                    textBoxConditionHelp.Text = "When the application's main interface window is going to close.";
                    break;

                // Interface Hiding
                case 3:
                    textBoxConditionHelp.Text = "When the application's main interface window is about to be hidden.";
                    break;

                // Interface Showing
                case 4:
                    textBoxConditionHelp.Text = "When the application's main interface window is about to be shown.";
                    break;

                // Limit Reached
                case 5:
                    textBoxConditionHelp.Text = "When the number of screen capture cycles reach the limit as specified by the Limit option in Setup.";
                    break;

                // Screen Capture Started
                case 6:
                    textBoxConditionHelp.Text = "When a screen capture session starts.";
                    break;

                // Screen Capture Stopped
                case 7:
                    textBoxConditionHelp.Text = "When the currently running screen capture session stops.";
                    break;

                // After Screenshot Taken
                case 8:
                    textBoxConditionHelp.Text = "When a screenshot has been taken after it has been saved to disk.";
                    break;

                // Date/Time
                case 9:
                    textBoxConditionHelp.Text = "When a specified date and time is encountered.";
                    break;

                // Time
                case 10:
                    textBoxConditionHelp.Text = "When a specified time is encountered.";
                    break;

                // Day/Time
                case 11:
                    textBoxConditionHelp.Text = "When a specified day and time is encountered.";
                    break;

                // Before Screenshot Taken
                case 12:
                    textBoxConditionHelp.Text = "When a screenshot is about to be taken before it has been saved to disk.";
                    break;

                // Before Screenshot References Saved
                case 13:
                    textBoxConditionHelp.Text = "When screenshot references are about to be saved to disk during a recurring five minute maintenance timer.";
                    break;

                // After Screenshot References Saved
                case 14:
                    textBoxConditionHelp.Text = "When screenshot references have been saved to disk during a recurring five minute maintenance timer.";
                    break;

                // System Tray Icon Double Click
                case 15:
                    textBoxConditionHelp.Text = "When you double-click on the system tray icon.";
                    break;

                // CaptureCycleElapsed
                case 16:
                    textBoxConditionHelp.Text = "When the capture cycle elapses.";
                    break;

                // DurationFromStartScreenCapture
                case 17:
                    textBoxConditionHelp.Text = "When a specified duration has been reached from starting a screen capture session.";
                    break;

                // DurationFromStopScreenCapture
                case 18:
                    textBoxConditionHelp.Text = "When a specified duration has been reached from stopping the currently running screen capture session.";
                    break;
            }
        }

        /// <summary>
        /// Displays the help text based on the selected action.
        /// </summary>
        private void ShowActionHelpText()
        {
            switch (listBoxAction.SelectedIndex)
            {
                // ExitApplication
                case 0:
                    textBoxActionHelp.Text = "Exit the application.";
                    break;

                // HideInterface
                case 1:
                    textBoxActionHelp.Text = "Hide the application's main interface window.";
                    break;

                // RunEditor
                case 2:
                    textBoxActionHelp.Text = "Run a specified Editor.";
                    break;

                // ShowInterface
                case 3:
                    textBoxActionHelp.Text = "Show the application's main interface window.";
                    break;

                // StartScreenCapture
                case 4:
                    textBoxActionHelp.Text = "Start a screen capture session.";
                    break;

                // StopScreenCapture
                case 5:
                    textBoxActionHelp.Text = "Stop the currently running screen capture session.";
                    break;

                // EmailScreenshot
                case 6:
                    textBoxActionHelp.Text = "Email the latest screenshot using the configured email server settings.";
                    break;

                // SetScreenCaptureInterval
                case 7:
                    textBoxActionHelp.Text = "Set the timer's screen capture interval.";
                    break;

                // EnableScreen
                case 8:
                    textBoxActionHelp.Text = "Enable a specified Screen.";
                    break;

                // EnableRegion
                case 9:
                    textBoxActionHelp.Text = "Enable a specified Region.";
                    break;

                // EnableSchedule
                case 10:
                    textBoxActionHelp.Text = "Enable a specified Schedule.";
                    break;

                // EnableMacroTag
                case 11:
                    textBoxActionHelp.Text = "Enable a specified Macro Tag.";
                    break;

                // EnableTrigger
                case 12:
                    textBoxActionHelp.Text = "Enable a specified Trigger.";
                    break;

                // DisableScreen
                case 13:
                    textBoxActionHelp.Text = "Disable a specified Screen.";
                    break;

                // DisableRegion
                case 14:
                    textBoxActionHelp.Text = "Disable a specified Region.";
                    break;

                // DisableSchedule
                case 15:
                    textBoxActionHelp.Text = "Disable a specified Schedule.";
                    break;

                // DisableMacroTag
                case 16:
                    textBoxActionHelp.Text = "Disable a specified Macro Tag.";
                    break;

                // DisableTrigger
                case 17:
                    textBoxActionHelp.Text = "Disable a specified Trigger.";
                    break;

                // DeleteScreenshotsByDays
                case 18:
                    textBoxActionHelp.Text = "Delete screenshots that are older than a specified number of days from today's date. If you want to constantly keep 10 days of fresh screenshots then set the Days value to 10 so that each day's collection of screenshots older than 10 days ago will be deleted. Use 0 for the value of Days if you want to delete screenshots all the time. You can also delete a specified folder. It is recommended to use $date[yyyy-MM-dd]$ in the Delete Folder field if you want to delete screenshots in date-stamped folders.";
                    break;

                // SetLabel
                case 19:
                    textBoxActionHelp.Text = "Apply a specified label to each screenshot during a screen capture session.";
                    break;

                // SetActiveWindowTitleAsMatch
                case 20:
                    textBoxActionHelp.Text = "Set the text used for comparison against the title of the active window and match on the given text.";
                    break;

                // SetApplicationFocus
                case 21:
                    textBoxActionHelp.Text = "Set the name of the process to be forced into focus.";
                    break;

                // FileTransferScreenshot
                case 22:
                    textBoxActionHelp.Text = "Transfer the latest screenshot to a file server using the configured File Transfer settings.";
                    break;

                // SetActiveWindowTitleAsNoMatch
                case 23:
                    textBoxActionHelp.Text = "Set the text used for comparison against the title of the active window and do not match on the given text.";
                    break;

                // ShowSystemTrayIcon
                case 24:
                    textBoxActionHelp.Text = "Show the system tray icon.";
                    break;

                // HideSystemTrayIcon
                case 25:
                    textBoxActionHelp.Text = "Hide the system tray icon.";
                    break;

                // TakeScreenshot
                case 26:
                    textBoxActionHelp.Text = "Take a set of screenshots.";
                    break;

                // RegionSelectClipboard
                case 27:
                    textBoxActionHelp.Text = "Perform the same action as if you had selected Clipboard from Region Select.";
                    break;

                // RegionSelectClipboardAutoSave
                case 28:
                    textBoxActionHelp.Text = "Perform the same action as if you had selected Clipboard/Auto Save from Region Select.";
                    break;

                // RegionSelectClipboardAutoSaveEdit
                case 29:
                    textBoxActionHelp.Text = "Perform the same action as if you had selected Clipboard/Auto Save/Edit from Region Select.";
                    break;

                // RegionSelectClipboardFloatingScreenshot
                case 30:
                    textBoxActionHelp.Text = "Perform the same action as if you had selected Clipboard/Floating Screenshot from Region Select.";
                    break;

                // RegionSelectFloatingScreenshot
                case 31:
                    textBoxActionHelp.Text = "Perform the same action as if you had selected Floating Screenshot from Region Select.";
                    break;

                // ShowOrHideInterface
                case 32:
                    textBoxActionHelp.Text = "Show or hide the interface depending on its current visibility state.";
                    break;

                // StartOrStopScreenCapture
                case 33:
                    textBoxActionHelp.Text = "Start or stop screen capture depending on the current state. This will start a session if the session is idle (ready to be started) or stop a session if the session is already running.";
                    break;

                // RestartScreenCapture
                case 34:
                    textBoxActionHelp.Text = "Restart the screen capture session. This is useful if you will be changing the interval during a running screen capture session and need the session to restart using the new interval. This is a simple stop and start operation.";
                    break;

                // DeleteScreenshotsByCycleCount
                case 35:
                    textBoxActionHelp.Text = "\"Wiping the slate clean\". When combined with the Capture Cycle Elapsed condition this will constantly delete all screenshots after the specified number of screen capture cycles have elapsed during a running screen capture session. So with a Cycle Count of 10 and a screen capture interval of 1 second the effect is like watching screenshots being created until the 10th screenshot then seeing all of them getting wiped out every 10 seconds.";
                    break;

                // DeleteScreenshotsFromOldestCaptureCycle
                case 36:
                    textBoxActionHelp.Text = "\"Rolling delete; like a car's dash cam\". When combined with the Capture Cycle Elapsed condition this will perform a rolling delete during a running screen capture session. Run a screen capture session for a while first to define the required set of cycles and then trigger this action to start doing the rolling delete.";
                    break;
            }
        }
    }
}