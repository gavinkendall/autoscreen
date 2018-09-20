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
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class FormTrigger : Form
    {
        public Trigger TriggerObject { get; set; }

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

            textBoxTriggerName.Focus();
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
                        comboBoxEditor.SelectedItem.ToString()));

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
                        TriggerCollection.Get(TriggerObject).Editor = comboBoxEditor.SelectedItem.ToString();

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

            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ApplicationStartup, "Application Startup").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ApplicationExit, "Application Exit").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.LimitReached, "Limit Reached").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ScreenCaptureSessionStarted, "Screen Capture Session Started").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ScreenCaptureSessionStopped, "Screen Capture Session Stopped").Description);
            comboBoxCondition.Items.Add(new TriggerCondition(TriggerConditionType.ScreenshotTaken, "Screenshot Taken").Description);

            comboBoxCondition.SelectedIndex = 0;
        }

        private void LoadActions()
        {
            comboBoxAction.Items.Clear();

            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.QuitApplication, "Quit Application").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.RunEditor, "Run Editor").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.StartScreenCaptureSession, "Start Screen Capture Session").Description);
            comboBoxAction.Items.Add(new TriggerAction(TriggerActionType.StopScreenCaptureSession, "Stop Screen Capture Session").Description);

            comboBoxAction.SelectedIndex = 0;
        }

        private void LoadEditors()
        {
            comboBoxEditor.Items.Clear();

            for (int i = 0; i < EditorCollection.Count; i++)
            {
                Editor editor = EditorCollection.GetByIndex(i);

                if (editor != null && File.Exists(editor.Application))
                {
                    comboBoxEditor.Items.Add(editor.Name);
                }
            }

            comboBoxEditor.SelectedIndex = 0;
        }

        private void comboBoxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAction.SelectedIndex == (int)TriggerActionType.RunEditor)
            {
                comboBoxEditor.Enabled = true;
            }
            else
            {
                comboBoxEditor.Enabled = false;
            }
        }
    }
}
