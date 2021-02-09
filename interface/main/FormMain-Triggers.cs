//-----------------------------------------------------------------------
// <copyright file="FormMain-Triggers.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Methods for adding, removing, and changing triggers.</summary>
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
    public partial class FormMain : Form
    {
        /// <summary>
        /// Shows the "Add Trigger" window to enable the user to add a chosen Trigger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTrigger_Click(object sender, EventArgs e)
        {
            _formTrigger.TriggerObject = null;

            _formTrigger.EditorCollection = _formEditor.EditorCollection;
            _formTrigger.ScreenCollection = _formScreen.ScreenCollection;
            _formTrigger.RegionCollection = _formRegion.RegionCollection;
            _formTrigger.ScheduleCollection = _formSchedule.ScheduleCollection;
            _formTrigger.TagCollection = _formTag.TagCollection;

            _formTrigger.ShowDialog(this);

            if (_formTrigger.DialogResult == DialogResult.OK)
            {
                BuildTriggersModule();

                if (!_formTrigger.TriggerCollection.SaveToXmlFile(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Removes the selected Triggers from the Triggers tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSelectedTriggers_Click(object sender, EventArgs e)
        {
            int countBeforeRemoval = _formTrigger.TriggerCollection.Count;

            foreach (Control control in tabPageTriggers.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Trigger trigger = _formTrigger.TriggerCollection.Get((Trigger)checkBox.Tag);
                        _formTrigger.TriggerCollection.Remove(trigger);
                    }
                }
            }

            if (countBeforeRemoval > _formTrigger.TriggerCollection.Count)
            {
                BuildTriggersModule();

                if (!_formTrigger.TriggerCollection.SaveToXmlFile(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
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
                _formTrigger.TriggerObject = (Trigger)buttonSelected.Tag;

                _formTrigger.EditorCollection = _formEditor.EditorCollection;
                _formTrigger.ScreenCollection = _formScreen.ScreenCollection;
                _formTrigger.RegionCollection = _formRegion.RegionCollection;
                _formTrigger.ScheduleCollection = _formSchedule.ScheduleCollection;
                _formTrigger.TagCollection = _formTag.TagCollection;

                _formTrigger.ShowDialog(this);

                if (_formTrigger.DialogResult == DialogResult.OK)
                {
                    BuildTriggersModule();

                    if (!_formTrigger.TriggerCollection.SaveToXmlFile(_config, _fileSystem, _log))
                    {
                        _screenCapture.ApplicationError = true;
                    }
                }
            }
        }

        private void RunTriggersOfConditionType(TriggerConditionType conditionType)
        {
            DateTime dtNow = DateTime.Now;

            foreach (Trigger trigger in _formTrigger.TriggerCollection)
            {
                if (!trigger.Active)
                {
                    continue;
                }

                // Don't show the interface on startup if AutoStartFromCommandLine is enabled.
                if (_screenCapture.AutoStartFromCommandLine &&
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
                    Editor editor = _formEditor.EditorCollection.GetByName(trigger.ModuleItem);
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
                    timerScreenCapture.Stop();

                    _screenCapture.Interval = trigger.ScreenCaptureInterval;
                    timerScreenCapture.Interval = trigger.ScreenCaptureInterval;

                    decimal screenCaptureIntervalHours = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(trigger.ScreenCaptureInterval)).Hours);
                    decimal screenCaptureIntervalMinutes = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(trigger.ScreenCaptureInterval)).Minutes);
                    decimal screenCaptureIntervalSeconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(trigger.ScreenCaptureInterval)).Seconds);
                    decimal screenCaptureIntervalMilliseconds = Convert.ToDecimal(TimeSpan.FromMilliseconds(Convert.ToDouble(trigger.ScreenCaptureInterval)).Milliseconds);

                    numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                    numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                    numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                    numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                    timerScreenCapture.Start();
                    break;

                case TriggerActionType.ActivateScreen:
                    _formScreen.ScreenCollection.GetByName(trigger.ModuleItem).Active = true;
                    BuildScreensModule();
                    break;

                case TriggerActionType.DeactivateScreen:
                    _formScreen.ScreenCollection.GetByName(trigger.ModuleItem).Active = false;
                    BuildScreensModule();
                    break;

                case TriggerActionType.ActivateRegion:
                    _formRegion.RegionCollection.GetByName(trigger.ModuleItem).Active = true;
                    BuildRegionsModule();
                    break;

                case TriggerActionType.DeactivateRegion:
                    _formRegion.RegionCollection.GetByName(trigger.ModuleItem).Active = false;
                    BuildRegionsModule();
                    break;

                case TriggerActionType.ActivateSchedule:
                    _formSchedule.ScheduleCollection.GetByName(trigger.ModuleItem).Active = true;
                    BuildSchedulesModule();
                    break;

                case TriggerActionType.DeactivateSchedule:
                    _formSchedule.ScheduleCollection.GetByName(trigger.ModuleItem).Active = false;
                    BuildSchedulesModule();
                    break;

                case TriggerActionType.ActivateTag:
                    _formTag.TagCollection.GetByName(trigger.ModuleItem).Active = true;
                    BuildTagsModule();
                    break;

                case TriggerActionType.DeactivateTag:
                    _formTag.TagCollection.GetByName(trigger.ModuleItem).Active = false;
                    BuildTagsModule();
                    break;

                case TriggerActionType.ActivateTrigger:
                    _formTrigger.TriggerCollection.GetByName(trigger.ModuleItem).Active = true;
                    BuildTriggersModule();
                    break;

                case TriggerActionType.DeactivateTrigger:
                    _formTrigger.TriggerCollection.GetByName(trigger.ModuleItem).Active = false;
                    BuildTriggersModule();
                    break;
            }
        }
    }
}