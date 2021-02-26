//-----------------------------------------------------------------------
// <copyright file="FormMain-Editors.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for handling editors.</summary>
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
using System.Diagnostics;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Shows the "Add Editor" window to enable the user to add a chosen Editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addEditor_Click(object sender, EventArgs e)
        {
            ShowInterface();

            _formEditor.EditorObject = null;

            if (!_formEditor.Visible)
            {
                _formEditor.ShowDialog(this);
            }
            else
            {
                _formEditor.Focus();
                _formEditor.BringToFront();
            }

            if (_formEditor.DialogResult == DialogResult.OK)
            {
                BuildEditorsModule();
                BuildViewTabPages();
                BuildScreenshotPreviewContextualMenu();

                if (!_formEditor.EditorCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Removes the selected Editors from the Editors tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSelectedEditors_Click(object sender, EventArgs e)
        {
            int countBeforeRemoval = _formEditor.EditorCollection.Count;

            foreach (Control control in tabPageEditors.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Editor editor = _formEditor.EditorCollection.Get((Editor)checkBox.Tag);
                        _formEditor.EditorCollection.Remove(editor);
                    }
                }
            }

            if (countBeforeRemoval > _formEditor.EditorCollection.Count)
            {
                BuildEditorsModule();
                BuildViewTabPages();
                BuildScreenshotPreviewContextualMenu();

                if (!_formEditor.EditorCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Runs the chosen image editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runEditor_Click(object sender, EventArgs e)
        {
            Editor editor = _formEditor.EditorCollection.GetByName(sender.ToString());

            if (!RunEditor(editor, _slideShow.SelectedSlide))
            {
                MessageBox.Show("No image is available to edit.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Shows the "Change Editor" window to enable the user to edit a chosen Editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeEditor_Click(object sender, EventArgs e)
        {
            ShowInterface();

            Editor editor = new Editor();

            if (sender is Button)
            {
                Button buttonSelected = (Button)sender;
                editor = (Editor)buttonSelected.Tag;
            }

            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                editor = (Editor)toolStripMenuItemSelected.Tag;
            }

            _formEditor.EditorObject = editor;

            _formEditor.ShowDialog(this);

            if (_formEditor.DialogResult == DialogResult.OK)
            {
                BuildEditorsModule();
                BuildViewTabPages();
                BuildScreenshotPreviewContextualMenu();

                if (!_formEditor.EditorCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Executes a chosen image editor from the interface.
        /// </summary>
        /// <param name="editor">The image editor to execute.</param>
        /// <param name="slide">The slide to use when running the editor.</param>
        private bool RunEditor(Editor editor, Slide slide)
        {
            if (editor != null && slide != null)
            {
                Screenshot selectedScreenshot = null;

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
                {
                    Screen screen = (Screen)tabControlViews.SelectedTab.Tag;
                    selectedScreenshot = _screenshotCollection.GetScreenshot(slide.Name, screen.ViewId);
                }
                
                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
                {
                    Region region = (Region)tabControlViews.SelectedTab.Tag;
                    selectedScreenshot = _screenshotCollection.GetScreenshot(slide.Name, region.ViewId);
                }

                if (selectedScreenshot.ViewId.Equals(Guid.Empty))
                {
                    // *** Auto Screen Capture - Region Select / Auto Save ***
                    selectedScreenshot = _screenshotCollection.GetScreenshot(slide.Name, Guid.Empty);
                }

                if (selectedScreenshot != null)
                {
                    return RunEditor(editor, selectedScreenshot);
                }
            }

            return false;
        }

        /// <summary>
        /// Executes a chosen image editor from a Trigger to open the last set of screenshots taken with the image editor.
        /// </summary>
        /// <param name="editor">The image editor to execute.</param>
        /// <param name="triggerActionType">The trigger's action type.</param>
        private void RunEditor(Editor editor, TriggerActionType triggerActionType)
        {
            if (editor != null && triggerActionType == TriggerActionType.RunEditor)
            {
                // Assume we're going to be passing in the path of the screenshot image to the program.
                if (editor.Arguments != null && editor.Arguments.Contains("%filepath%"))
                {
                    DateTime dt = _screenCapture.DateTimeScreenshotsTaken;

                    foreach (Screenshot screenshot in _screenshotCollection.GetScreenshots(dt.ToString(_macroParser.DateFormat), dt.ToString(_macroParser.TimeFormat)))
                    {
                        if (screenshot != null && screenshot.Slide != null && !string.IsNullOrEmpty(screenshot.Path))
                        {
                            _log.WriteDebugMessage("Running editor (based on TriggerActionType.RunEditor) \"" + editor.Name + "\" using screenshot path \"" + screenshot.Path + "\"");

                            if (!RunEditor(editor, screenshot))
                            {
                                _log.WriteDebugMessage("Running editor failed. Perhaps the filepath of the screenshot file is no longer available");
                            }
                        }
                    }
                }
                else
                {
                    // Just run the program without passing in the path of the screenshot.
                    if (!RunEditor(editor))
                    {
                        _log.WriteDebugMessage("Running editor failed.");
                    }
                }
            }
        }

        /// <summary>
        /// Runs the editor using the specified screenshot.
        /// </summary>
        /// <param name="editor">The editor to use.</param>
        /// <param name="screenshot">The screenshot to use.</param>
        /// <returns>Determines if the editor was executed successfully or not.</returns>
        private bool RunEditor(Editor editor, Screenshot screenshot)
        {
            // Execute the chosen image editor. If the %filepath% argument happens to be included
            // then we'll use that argument as the screenshot file path when executing the image editor.
            if (editor != null && (screenshot != null && !string.IsNullOrEmpty(screenshot.Path) &&
                _fileSystem.FileExists(editor.Application) && _fileSystem.FileExists(screenshot.Path)))
            {
                _log.WriteDebugMessage("Starting process for editor \"" + editor.Name + "\" ...");
                _log.WriteDebugMessage("Application: " + editor.Application);
                _log.WriteDebugMessage("Arguments before %filepath% tag replacement: " + editor.Arguments);
                _log.WriteDebugMessage("Arguments after %filepath% tag replacement: " + editor.Arguments.Replace("%filepath%", "\"" + screenshot.Path + "\""));

                _ = Process.Start(editor.Application, editor.Arguments.Replace("%filepath%", "\"" + screenshot.Path + "\""));

                // We successfully opened the editor with the given screenshot path.
                return true;
            }

            // We failed to open the editor with the given screenshot path.
            return false;
        }

        /// <summary>
        /// Runs an editor.
        /// </summary>
        /// <param name="editor">The editor to run.</param>
        /// <returns>Determines if the editor was executed successfully or not.</returns>
        private bool RunEditor(Editor editor)
        {
            try
            {
                if (editor != null && _fileSystem.FileExists(editor.Application))
                {
                    if (editor.Arguments != null && !string.IsNullOrEmpty(editor.Arguments))
                    {
                        _ = Process.Start(editor.Application, editor.Arguments);
                    }
                    else
                    {
                        _ = Process.Start(editor.Application);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("FormMain-Editors::RunEditor", ex);

                return false;
            }
        }
    }
}
