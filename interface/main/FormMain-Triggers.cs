//-----------------------------------------------------------------------
// <copyright file="FormMain-Triggers.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
            ShowInterface();

            _formTrigger.TriggerObject = null;

            _formTrigger.EditorCollection = _formEditor.EditorCollection;
            _formTrigger.ScreenCollection = _formScreen.ScreenCollection;
            _formTrigger.RegionCollection = _formRegion.RegionCollection;
            _formTrigger.ScheduleCollection = _formSchedule.ScheduleCollection;
            _formTrigger.MacroTagCollection = _formMacroTag.MacroTagCollection;

            if (!_formTrigger.Visible)
            {
                _formTrigger.ShowDialog(this);
            }
            else
            {
                _formTrigger.Activate();
            }

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
            ShowInterface();

            Trigger trigger = new Trigger();

            if (sender is Button)
            {
                Button buttonSelected = (Button)sender;
                trigger = (Trigger)buttonSelected.Tag;
            }

            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                trigger = (Trigger)toolStripMenuItemSelected.Tag;
            }

            _formTrigger.TriggerObject = trigger;

            _formTrigger.EditorCollection = _formEditor.EditorCollection;
            _formTrigger.ScreenCollection = _formScreen.ScreenCollection;
            _formTrigger.RegionCollection = _formRegion.RegionCollection;
            _formTrigger.ScheduleCollection = _formSchedule.ScheduleCollection;
            _formTrigger.MacroTagCollection = _formMacroTag.MacroTagCollection;

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

        private void RunTriggersOfConditionType(TriggerConditionType conditionType)
        {
            foreach (Trigger trigger in _formTrigger.TriggerCollection)
            {
                if (!trigger.Enable)
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
                    Editor editor = _formEditor.EditorCollection.GetByName(trigger.Value);
                    RunEditor(editor, _screenshotCollection.GetLastScreenshot());
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

                    _formSetup.numericUpDownHoursInterval.Value = screenCaptureIntervalHours;
                    _formSetup.numericUpDownMinutesInterval.Value = screenCaptureIntervalMinutes;
                    _formSetup.numericUpDownSecondsInterval.Value = screenCaptureIntervalSeconds;
                    _formSetup.numericUpDownMillisecondsInterval.Value = screenCaptureIntervalMilliseconds;

                    timerScreenCapture.Start();
                    break;

                case TriggerActionType.EnableScreen:
                    _formScreen.ScreenCollection.GetByName(trigger.Value).Enable = true;
                    BuildScreensModule();
                    break;

                case TriggerActionType.DisableScreen:
                    _formScreen.ScreenCollection.GetByName(trigger.Value).Enable = false;
                    BuildScreensModule();
                    break;

                case TriggerActionType.EnableRegion:
                    _formRegion.RegionCollection.GetByName(trigger.Value).Enable = true;
                    BuildRegionsModule();
                    break;

                case TriggerActionType.DisableRegion:
                    _formRegion.RegionCollection.GetByName(trigger.Value).Enable = false;
                    BuildRegionsModule();
                    break;

                case TriggerActionType.EnableSchedule:
                    _formSchedule.ScheduleCollection.GetByName(trigger.Value).Enable = true;
                    BuildSchedulesModule();
                    break;

                case TriggerActionType.DisableSchedule:
                    _formSchedule.ScheduleCollection.GetByName(trigger.Value).Enable = false;
                    BuildSchedulesModule();
                    break;

                case TriggerActionType.EnableMacroTag:
                    _formMacroTag.MacroTagCollection.GetByName(trigger.Value).Enable = true;
                    BuildMacroTagsModule();
                    break;

                case TriggerActionType.DisableMacroTag:
                    _formMacroTag.MacroTagCollection.GetByName(trigger.Value).Enable = false;
                    BuildMacroTagsModule();
                    break;

                case TriggerActionType.EnableTrigger:
                    _formTrigger.TriggerCollection.GetByName(trigger.Value).Enable = true;
                    BuildTriggersModule();
                    break;

                case TriggerActionType.DisableTrigger:
                    _formTrigger.TriggerCollection.GetByName(trigger.Value).Enable = false;
                    BuildTriggersModule();
                    break;

                case TriggerActionType.DeleteScreenshots:
                    _screenshotCollection.DeleteScreenshots(trigger.Days, trigger.Value, _macroParser, _formMacroTag.MacroTagCollection, _log);
                    break;

                case TriggerActionType.SetLabel:
                    ApplyLabel(trigger.Value);
                    break;

                case TriggerActionType.SetActiveWindowTitleAsMatch:
                    SetActiveWindowTitleAsMatch(trigger.Value);
                    break;

                case TriggerActionType.SetApplicationFocus:
                    _formSetup.SetApplicationFocus(trigger.Value);
                    break;

                case TriggerActionType.FileTransferScreenshot:
                    FileTransferScreenshot(TriggerActionType.FileTransferScreenshot);
                    break;

                case TriggerActionType.SetActiveWindowTitleAsNoMatch:
                    SetActiveWindowTitleAsNoMatch(trigger.Value);
                    break;

                case TriggerActionType.ShowSystemTrayIcon:
                    ShowSystemTrayIcon();
                    break;

                case TriggerActionType.HideSystemTrayIcon:
                    HideSystemTrayIcon();
                    break;

                case TriggerActionType.TakeScreenshot:
                    TakeScreenshot(captureNow: true);
                    break;

                case TriggerActionType.RegionSelectClipboard:
                    toolStripMenuItemRegionSelectClipboard_Click(null, null);
                    break;

                case TriggerActionType.RegionSelectClipboardAutoSave:
                    toolStripMenuItemRegionSelectClipboardAutoSave_Click(null, null);
                    break;

                case TriggerActionType.RegionSelectClipboardAutoSaveEdit:
                    toolStripMenuItemRegionSelectClipboardAutoSaveEdit_Click(null, null);
                    break;

                case TriggerActionType.RegionSelectClipboardFloatingScreenshot:
                    toolStripMenuItemRegionSelectClipboardFloatingScreenshot_Click(null, null);
                    break;

                case TriggerActionType.RegionSelectFloatingScreenshot:
                    toolStripMenuItemRegionSelectFloatingScreenshot_Click(null, null);
                    break;

                case TriggerActionType.ShowOrHideInterface:
                    toolStripMenuItemShowHideInterface_Click(null, null);
                    break;

                case TriggerActionType.StartOrStopScreenCapture:
                    if (_screenCapture.Running)
                    {
                        StopScreenCapture();
                    }
                    else
                    {
                        StartScreenCapture();
                    }
                    break;

                case TriggerActionType.RestartScreenCapture:
                    StopScreenCapture();
                    StartScreenCapture();
                    break;
            }
        }
    }
}