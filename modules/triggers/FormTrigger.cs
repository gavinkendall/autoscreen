//-----------------------------------------------------------------------
// <copyright file="FormTrigger.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public partial class FormTrigger : Form
    {
        public TriggerCollection TriggerCollection { get; } = new TriggerCollection();

        public Trigger TriggerObject { get; set; }

        public EditorCollection EditorCollection { get; set; }

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
                comboBoxCondition.SelectedIndex = (int)TriggerObject.ConditionType;
                comboBoxAction.SelectedIndex = (int)TriggerObject.ActionType;
                comboBoxEditor.SelectedIndex = comboBoxEditor.Items.IndexOf(TriggerObject.Editor);
            }
            else
            {
                Text = "Add New Trigger";

                textBoxTriggerName.Text = string.Empty;
            }
        }

        private void Click_buttonCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void Click_buttonOK(object sender, EventArgs e)
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
                    TriggerCollection.Add(new Trigger(textBoxTriggerName.Text,
                        (TriggerConditionType)comboBoxCondition.SelectedIndex,
                        (TriggerActionType)comboBoxAction.SelectedIndex,
                        comboBoxEditor.Enabled ? comboBoxEditor.SelectedItem.ToString() : string.Empty));

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
                        TriggerCollection.Get(TriggerObject).ConditionType = (TriggerConditionType)comboBoxCondition.SelectedIndex;
                        TriggerCollection.Get(TriggerObject).ActionType = (TriggerActionType)comboBoxAction.SelectedIndex;

                        if (comboBoxEditor.Enabled)
                        {
                            TriggerCollection.Get(TriggerObject).Editor = comboBoxEditor.SelectedItem.ToString();
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
                ((int)TriggerObject.ConditionType != comboBoxCondition.SelectedIndex ||
                (int)TriggerObject.ActionType != comboBoxAction.SelectedIndex ||
                !TriggerObject.Editor.Equals(comboBoxEditor.SelectedItem.ToString())))
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
            comboBoxCondition.Items.Clear();

            // Whatever changes you make here will need to reflect the exact order in which the conditions are listed in the TriggerConditionType class.
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ApplicationStartup, "Application Startup").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ApplicationExit, "Application Exit").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.InterfaceClosing, "Interface Closing").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.InterfaceHiding, "Interface Hiding").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.InterfaceShowing, "Interface Showing").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.LimitReached, "Limit Reached").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ScreenCaptureStarted, "Screen Capture Started").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ScreenCaptureStopped, "Screen Capture Stopped").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ScreenshotTaken, "Screenshot Taken").Description);

            comboBoxCondition.SelectedIndex = 0;
        }

        private void LoadActions()
        {
            comboBoxAction.Items.Clear();

            // Whatever changes you make here will need to reflect the exact order in which the actions are listed in the TriggerActionType class.
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.DisablePreview, "Disable Preview").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.DisableSchedule, "Disable Schedule").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.EnableDebugMode, "Enable Debug Mode").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.EnablePreview, "Enable Preview").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.EnableSchedule, "Enable Schedule").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.ExitApplication, "Exit Application").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.HideInterface, "Hide Interface").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.RunEditor, "Run Editor").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.ShowInterface, "Show Interface").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.StartScreenCapture, "Start Screen Capture").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.StopScreenCapture, "Stop Screen Capture").Description);

            comboBoxAction.SelectedIndex = 0;
        }

        private void LoadEditors()
        {
            comboBoxEditor.Items.Clear();

            comboBoxEditor.Items.Add(string.Empty);

            foreach (Editor editor in EditorCollection)
            {
                if (editor != null && File.Exists(editor.Application))
                {
                    comboBoxEditor.Items.Add(editor.Name);
                }
            }

            comboBoxEditor.SelectedIndex = 0;
        }

        private void comboBoxCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableOrDisableEditors();
        }

        private void comboBoxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableOrDisableEditors();
        }

        private void EnableOrDisableEditors()
        {
            if (comboBoxAction.SelectedIndex == (int)TriggerActionType.RunEditor &&
                (comboBoxCondition.SelectedIndex == (int)TriggerConditionType.ScreenCaptureStopped ||
                comboBoxCondition.SelectedIndex == (int)TriggerConditionType.ScreenshotTaken))
            {
                comboBoxEditor.Enabled = true;
            }
            else
            {
                comboBoxEditor.SelectedIndex = 0;
                comboBoxEditor.Enabled = false;
            }
        }
    }
}
