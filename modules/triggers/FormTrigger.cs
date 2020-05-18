//-----------------------------------------------------------------------
// <copyright file="FormTrigger.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for adding a new trigger or changing an existing trigger.</summary>
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
        /// Empty constructor.
        /// </summary>
        public FormTrigger()
        {
            InitializeComponent();
        }

        private void FormTrigger_Load(object sender, EventArgs e)
        {
            LoadEditors();
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

                if (listBoxEditor.Items.Count > 0 && TriggerObject.Editor != null)
                {
                    listBoxEditor.SelectedIndex = listBoxEditor.Items.IndexOf(TriggerObject.Editor);
                }
            }
            else
            {
                Text = "Add New Trigger";

                textBoxTriggerName.Text = "Trigger " + (TriggerCollection.Count + 1);
                checkBoxActive.Checked = true;
            }
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
                    Trigger trigger = new Trigger()
                    {
                        Name = textBoxTriggerName.Text,
                        ConditionType = (TriggerConditionType)listBoxCondition.SelectedIndex,
                        ActionType = (TriggerActionType)listBoxAction.SelectedIndex,
                        Editor = listBoxEditor.SelectedItem.ToString(),
                        Active = checkBoxActive.Checked,
                        Date = dateTimePickerDate.Value,
                        Time = dateTimePickerTime.Value
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

                        if (listBoxEditor.Enabled)
                        {
                            TriggerCollection.Get(TriggerObject).Editor = listBoxEditor.SelectedItem.ToString();
                        }
                        else
                        {
                            TriggerCollection.Get(TriggerObject).Editor = string.Empty;
                        }

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
            if (TriggerObject != null &&
                ((int)TriggerObject.ConditionType != listBoxCondition.SelectedIndex ||
                (int)TriggerObject.ActionType != listBoxAction.SelectedIndex ||
                (listBoxEditor.Items.Count > 0 && TriggerObject.Editor != null &&
                listBoxEditor.SelectedItem != null &&
                !TriggerObject.Editor.Equals(listBoxEditor.SelectedItem.ToString())) ||
                TriggerObject.Active != checkBoxActive.Checked ||
                TriggerObject.Date != dateTimePickerDate.Value ||
                TriggerObject.Time != dateTimePickerTime.Value))
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

            Close();
        }

        private void LoadConditions()
        {
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
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ScreenshotTaken, "Screenshot Taken").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.DateTime, "Date/Time").Description);
            listBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.Time, "Time").Description);

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

            listBoxAction.SelectedIndex = 0;
        }

        private void LoadEditors()
        {
            listBoxEditor.Items.Clear();

            foreach (Editor editor in EditorCollection)
            {
                if (editor != null && FileSystem.FileExists(editor.Application))
                {
                    listBoxEditor.Items.Add(editor.Name);
                }
            }

            listBoxEditor.SelectedIndex = 0;
        }

        private void listBoxCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableOrDisableDateTime();
            EnableOrDisableEditors();
        }

        private void listBoxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableOrDisableEditors();
        }

        private void EnableOrDisableEditors()
        {
            if (listBoxAction.SelectedIndex == (int)TriggerActionType.RunEditor &&
                (listBoxCondition.SelectedIndex == (int)TriggerConditionType.ScreenCaptureStopped ||
                listBoxCondition.SelectedIndex == (int)TriggerConditionType.ScreenshotTaken))
            {
                labelEditor.Enabled = true;
                listBoxEditor.Enabled = true;
            }
            else
            {
                listBoxEditor.SelectedIndex = 0;

                labelEditor.Enabled = false;
                listBoxEditor.Enabled = false;
            }
        }

        private void EnableOrDisableDateTime()
        {
            if (listBoxCondition.SelectedIndex == (int)TriggerConditionType.DateTime)
            {
                dateTimePickerDate.Enabled = true;
                dateTimePickerTime.Enabled = true;

                labelDate.Enabled = true;
                labelTime.Enabled = true;
            }
            else if (listBoxCondition.SelectedIndex == (int)TriggerConditionType.Time)
            {
                dateTimePickerDate.Enabled = false;
                dateTimePickerTime.Enabled = true;

                labelDate.Enabled = false;
                labelTime.Enabled = true;
            }
            else
            {
                dateTimePickerDate.Enabled = false;
                dateTimePickerTime.Enabled = false;

                labelDate.Enabled = false;
                labelTime.Enabled = false;
            }
        }
    }
}