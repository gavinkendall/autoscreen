//-----------------------------------------------------------------------
// <copyright file="FormTrigger.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
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
        public MacroTagCollection TagCollection { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
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

                comboBoxDay.SelectedIndex = comboBoxDay.Items.IndexOf(TriggerObject.Day);

                if (!string.IsNullOrEmpty(TriggerObject.Value))
                {
                    if (listBoxModuleItemList.Items.Count > 0)
                    {
                        listBoxModuleItemList.SelectedIndex = listBoxModuleItemList.Items.IndexOf(TriggerObject.Value);
                    }
                    else
                    {
                        textBoxTriggerValue.Text = TriggerObject.Value;
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

        private void AddTrigger()
        {
            if (InputValid())
            {
                TrimInput();

                if (TriggerCollection.GetByName(textBoxTriggerName.Text) == null)
                {
                    int screenCaptureInterval = _dataConvert.ConvertIntoMilliseconds((int)numericUpDownHoursInterval.Value,
                        (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value,
                        (int)numericUpDownMillisecondsInterval.Value);

                    Trigger trigger = new Trigger()
                    {
                        Name = textBoxTriggerName.Text,
                        ConditionType = (TriggerConditionType)listBoxCondition.SelectedIndex,
                        ActionType = (TriggerActionType)listBoxAction.SelectedIndex,
                        Enable = checkBoxEnable.Checked,
                        Date = dateTimePickerDate.Value,
                        Time = dateTimePickerTime.Value,
                        Day = comboBoxDay.Text,
                        Days = (int)numericUpDownDays.Value,
                        ScreenCaptureInterval = screenCaptureInterval
                    };

                    if (textBoxTriggerValue.Enabled)
                    {
                        trigger.Value = textBoxTriggerValue.Text.Trim();
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
                    TriggerCollection.Get(TriggerObject).Time = dateTimePickerTime.Value;
                    TriggerCollection.Get(TriggerObject).Day = comboBoxDay.Text;
                    TriggerCollection.Get(TriggerObject).Days = (int)numericUpDownDays.Value;

                    if (textBoxTriggerValue.Enabled)
                    {
                        TriggerCollection.Get(TriggerObject).Value = textBoxTriggerValue.Text.Trim();
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
                        (int)numericUpDownMinutesInterval.Value, (int)numericUpDownSecondsInterval.Value,
                        (int)numericUpDownMillisecondsInterval.Value);

                    TriggerCollection.Get(TriggerObject).ScreenCaptureInterval = screenCaptureInterval;

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
                MessageBox.Show("Please ensure that the application's email (SMTP) settings are correctly configured in order to automatically email screenshots to the intended recipient. It is important that you do not use this application to spam people. Thank you.\n\nThe settings are located in " + _fileSystem.SmtpSettingsFile, "Check Email Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.BeforeScreenshotReferencesSaved, "Before Screenshot References Saved").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.AfterScreenshotReferencesSaved, "After Screenshot References Saved").Description);

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
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.DeleteScreenshots, "Delete Screenshots").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.SetLabel, "Apply Label").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.SetActiveWindowTitleAsMatch, "Set Active Window Title As Match").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.SetApplicationFocus, "Set Application Focus").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.FileTransferScreenshot, "File Transfer Screenshot (SFTP)").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.SetActiveWindowTitleAsNoMatch, "Set Active Window Title As No Match").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.ShowSystemTrayIcon, "Show System Tray Icon").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.HideSystemTrayIcon, "Hide System Tray Icon").Description);
            listBoxAction.Items.Add(new TriggerAction(TriggerActionType.TakeScreenshot, "Take Screenshot").Description);

            listBoxAction.SelectedIndex = 0;
        }

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
                foreach (MacroTag tag in TagCollection)
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

        private void listBoxCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowConditionHelpText();

            labelDate.Enabled = false;
            labelTime.Enabled = false;
            labelDay.Enabled = false;
            dateTimePickerDate.Enabled = false;
            dateTimePickerTime.Enabled = false;
            comboBoxDay.Enabled = false;

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
        }

        private void listBoxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadModuleItems();

            ShowActionHelpText();

            labelDays.Enabled = false;
            labelInterval.Enabled = false;
            labelTriggerValue.Enabled = false;

            textBoxTriggerValue.Enabled = false;
            textBoxTriggerValue.Text = string.Empty;

            numericUpDownHoursInterval.Enabled = false;
            numericUpDownMinutesInterval.Enabled = false;
            numericUpDownSecondsInterval.Enabled = false;
            numericUpDownMillisecondsInterval.Enabled = false;

            numericUpDownDays.Value = 30;
            numericUpDownDays.Enabled = false;

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.SetScreenCaptureInterval)
            {
                labelInterval.Enabled = true;
                numericUpDownHoursInterval.Enabled = true;
                numericUpDownMinutesInterval.Enabled = true;
                numericUpDownSecondsInterval.Enabled = true;
                numericUpDownMillisecondsInterval.Enabled = true;
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.DeleteScreenshots)
            {
                labelTriggerValue.Text = "Delete Folder";
                labelTriggerValue.Enabled = true;
                textBoxTriggerValue.Enabled = true;

                labelDays.Enabled = true;
                numericUpDownDays.Enabled = true;

                // Suggest some values to help out the user. These are the default values used by the "Keep screenshots for 30 days" trigger.
                if (TriggerObject == null)
                {
                    numericUpDownDays.Value = 30;
                    textBoxTriggerValue.Text = _fileSystem.DefaultScreenshotsFolder + "$date[yyyy-MM-dd]$";
                }
                else if (TriggerObject.ActionType == TriggerActionType.DeleteScreenshots)
                {
                    numericUpDownDays.Value = TriggerObject.Days;
                    textBoxTriggerValue.Text = TriggerObject.Value;
                }
            }

            if (listBoxAction.SelectedIndex == (int)TriggerActionType.SetLabel ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.SetActiveWindowTitleAsMatch ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.SetActiveWindowTitleAsNoMatch ||
                listBoxAction.SelectedIndex == (int)TriggerActionType.SetApplicationFocus)
            {
                labelTriggerValue.Enabled = true;
                textBoxTriggerValue.Enabled = true;

                if (listBoxAction.SelectedIndex == (int)TriggerActionType.SetLabel)
                {
                    labelTriggerValue.Text = "Label:";
                }

                if (listBoxAction.SelectedIndex == (int)TriggerActionType.SetActiveWindowTitleAsMatch ||
                    listBoxAction.SelectedIndex == (int)TriggerActionType.SetActiveWindowTitleAsNoMatch)
                {
                    labelTriggerValue.Text = "Active Window Title:";
                }

                if (listBoxAction.SelectedIndex == (int)TriggerActionType.SetApplicationFocus)
                {
                    labelTriggerValue.Text = "Application Focus:";
                }

                if (TriggerObject != null &&
                    (TriggerObject.ActionType == TriggerActionType.SetLabel ||
                    TriggerObject.ActionType == TriggerActionType.SetActiveWindowTitleAsMatch ||
                    TriggerObject.ActionType == TriggerActionType.SetActiveWindowTitleAsNoMatch ||
                    TriggerObject.ActionType == TriggerActionType.SetApplicationFocus))
                {
                    numericUpDownDays.Value = TriggerObject.Days;
                    textBoxTriggerValue.Text = TriggerObject.Value;
                }
            }

            DetermineModuleListEnable();
        }

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
                    textBoxConditionHelp.Text = "When the number of screen capture cycles reach the specified limit.";
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
            }
        }

        private void ShowActionHelpText()
        {
            switch (listBoxAction.SelectedIndex)
            {
                // Exit Application
                case 0:
                    textBoxActionHelp.Text = "Exit the application.";
                    break;

                // Hide Interface
                case 1:
                    textBoxActionHelp.Text = "Hide the application's main interface window.";
                    break;

                // Run Editor
                case 2:
                    textBoxActionHelp.Text = "Run a specified Editor.";
                    break;

                // Show Interface
                case 3:
                    textBoxActionHelp.Text = "Show the application's main interface window.";
                    break;

                // Start Screen Capture
                case 4:
                    textBoxActionHelp.Text = "Start a screen capture session.";
                    break;

                // Stop Screen Capture
                case 5:
                    textBoxActionHelp.Text = "Stop the currently running screen capture session.";
                    break;

                // Email Screenshot (SMTP)
                case 6:
                    textBoxActionHelp.Text = "Email screenshots using the configured email server settings.";
                    break;

                // Set Screen Capture Interval
                case 7:
                    textBoxActionHelp.Text = "Set the timer's screen capture interval.";
                    break;

                // Enable Screen
                case 8:
                    textBoxActionHelp.Text = "Enable a specified Screen.";
                    break;

                // Enable Region
                case 9:
                    textBoxActionHelp.Text = "Enable a specified Region.";
                    break;

                // Enable Schedule
                case 10:
                    textBoxActionHelp.Text = "Enable a specified Schedule.";
                    break;

                // Enable Macro Tag
                case 11:
                    textBoxActionHelp.Text = "Enable a specified Macro Tag.";
                    break;

                // Enable Trigger
                case 12:
                    textBoxActionHelp.Text = "Enable a specified Trigger.";
                    break;

                // Disable Screen
                case 13:
                    textBoxActionHelp.Text = "Disable a specified Screen.";
                    break;

                // Disable Region
                case 14:
                    textBoxActionHelp.Text = "Disable a specified Region.";
                    break;

                // Disable Schedule
                case 15:
                    textBoxActionHelp.Text = "Disable a specified Schedule.";
                    break;

                // Disable Macro Tag
                case 16:
                    textBoxActionHelp.Text = "Disable a specified Macro Tag.";
                    break;

                // Disable Trigger
                case 17:
                    textBoxActionHelp.Text = "Disable a specified Trigger.";
                    break;

                // Delete Screenshots
                case 18:
                    textBoxActionHelp.Text = "Delete screenshots after a specified number of days. Use \"Days 0\" for today's date. It is recommended to use $date[yyyy-MM-dd]$ in the Delete Folder field if you want to delete old date-stamped folders.";
                    break;

                // Apply Label
                case 19:
                    textBoxActionHelp.Text = "Apply a specified label to each screenshot during a screen capture session.";
                    break;

                // Set Active Window Title As Match
                case 20:
                    textBoxActionHelp.Text = "Set the text used for comparison against the title of the active window and match on the given text.";
                    break;

                // Set Application Focus
                case 21:
                    textBoxActionHelp.Text = "Set the name of the process to be forced into focus.";
                    break;

                // File Transfer Screenshot (SFTP)
                case 22:
                    textBoxActionHelp.Text = "Transfer screenshots to a file server using the configured File Transfer settings.";
                    break;

                // Set Active Window Title As No Match
                case 23:
                    textBoxActionHelp.Text = "Set the text used for comparison against the title of the active window and do not match on the given text.";
                    break;

                // Show System Tray Icon
                case 24:
                    textBoxActionHelp.Text = "Show the system tray icon.";
                    break;

                // Hide System Tray Icon
                case 25:
                    textBoxActionHelp.Text = "Hide the system tray icon.";
                    break;

                // Take Screenshot
                case 26:
                    textBoxActionHelp.Text = "Take a set of screenshots.";
                    break;
            }
        }
    }
}