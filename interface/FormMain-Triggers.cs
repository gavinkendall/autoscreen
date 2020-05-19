//-----------------------------------------------------------------------
// <copyright file="FormMain-Triggers.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Methods for adding, removing, and changing triggers.</summary>
//-----------------------------------------------------------------------
using System;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Shows the "Add Trigger" window to enable the user to add a chosen Trigger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTrigger_Click(object sender, EventArgs e)
        {
            formTrigger.TriggerObject = null;

            formTrigger.EditorCollection = formEditor.EditorCollection;

            formTrigger.ShowDialog(this);

            if (formTrigger.DialogResult == DialogResult.OK)
            {
                BuildTriggersModule();

                formTrigger.TriggerCollection.SaveToXmlFile();
            }
        }

        /// <summary>
        /// Removes the selected Triggers from the Triggers tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSelectedTriggers_Click(object sender, EventArgs e)
        {
            int countBeforeRemoval = formTrigger.TriggerCollection.Count;

            foreach (Control control in tabPageTriggers.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Trigger trigger = formTrigger.TriggerCollection.Get((Trigger)checkBox.Tag);
                        formTrigger.TriggerCollection.Remove(trigger);
                    }
                }
            }

            if (countBeforeRemoval > formTrigger.TriggerCollection.Count)
            {
                BuildTriggersModule();

                formTrigger.TriggerCollection.SaveToXmlFile();
            }
        }

        /// <summary>
        /// Shows the "Change Trigger" window to enable the user to edit a chosen Trigger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeTrigger_Click(object sender, EventArgs e)
        {
            Button buttonSelected = (Button)sender;

            if (buttonSelected.Tag != null)
            {
                formTrigger.TriggerObject = (Trigger)buttonSelected.Tag;

                formTrigger.EditorCollection = formEditor.EditorCollection;

                formTrigger.ShowDialog(this);

                if (formTrigger.DialogResult == DialogResult.OK)
                {
                    BuildTriggersModule();

                    formTrigger.TriggerCollection.SaveToXmlFile();
                }
            }
        }

        private void RunTriggersOfConditionType(TriggerConditionType conditionType)
        {
            DateTime dtNow = DateTime.Now;

            foreach (Trigger trigger in formTrigger.TriggerCollection)
            {
                if (!trigger.Active)
                {
                    continue;
                }

                // Don't show the interface on startup if AutoStartFromCommandLine is enabled.
                if (ScreenCapture.AutoStartFromCommandLine &&
                    trigger.ConditionType == TriggerConditionType.ApplicationStartup &&
                    trigger.ActionType == TriggerActionType.ShowInterface)
                {
                    continue;
                }

                if (trigger.ConditionType == conditionType)
                {
                    DoTriggerAction(trigger);
                }
            }
        }

        private void DoTriggerAction(Trigger trigger)
        {
            // These actions need to directly correspond with the TriggerActionType class.
            switch (trigger.ActionType)
            {
                case TriggerActionType.ExitApplication:
                    ExitApplication();
                    break;

                case TriggerActionType.HideInterface:
                    HideInterface();
                    break;

                case TriggerActionType.RunEditor:
                    Editor editor = formEditor.EditorCollection.GetByName(trigger.Editor);
                    RunEditor(editor, TriggerActionType.RunEditor);
                    break;

                case TriggerActionType.ShowInterface:
                    ShowInterface();
                    break;

                case TriggerActionType.StartScreenCapture:
                    StartScreenCapture();
                    break;

                case TriggerActionType.StopScreenCapture:
                    StopScreenCapture();
                    break;

                case TriggerActionType.EmailScreenshot:
                    EmailScreenshot(TriggerActionType.EmailScreenshot);
                    break;

                case TriggerActionType.SetScreenCaptureInterval:
                    _screenCapture.Interval = trigger.ScreenCaptureInterval;

                    decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(trigger.ScreenCaptureInterval)).Hours);
                    decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(trigger.ScreenCaptureInterval)).Minutes);
                    decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(trigger.ScreenCaptureInterval)).Seconds);
                    decimal screenCaptureIntervalMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(trigger.ScreenCaptureInterval)).Milliseconds);

                    numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                    numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                    numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                    numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;
                    break;
            }
        }
    }
}