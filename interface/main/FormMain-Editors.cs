//-----------------------------------------------------------------------
// <copyright file="FormMain-Editors.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
                _formEditor.Activate();
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
            string editorName = sender.ToString();

            if (editorName.Equals("Edit"))
            {
                editorName = _config.Settings.User.GetByKey("DefaultEditor", _config.Settings.DefaultSettings.DefaultEditor).Value.ToString();
            }

            Editor editor = _formEditor.EditorCollection.GetByName(editorName);

            if (!RunEditor(editor, _slideShow.SelectedSlide))
            {
                MessageBox.Show("No image is available to edit.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Shows the "Configure Editor" window to enable the user to edit a chosen Editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configureEditor_Click(object sender, EventArgs e)
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

            if (!_formEditor.Visible)
            {
                _formEditor.ShowDialog(this);
            }
            else
            {
                _formEditor.Activate();
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
        /// Executes a chosen image editor from the interface.
        /// </summary>
        /// <param name="editor">The image editor to execute.</param>
        /// <param name="selectedSlide">The slide to use when running the editor.</param>
        private bool RunEditor(Editor editor, Slide selectedSlide)
        {
            if (editor != null && selectedSlide != null)
            {
                Screenshot selectedScreenshot = null;

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
                {
                    Screen screen = (Screen)tabControlViews.SelectedTab.Tag;
                    selectedScreenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, screen.ViewId);
                }
                
                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
                {
                    Region region = (Region)tabControlViews.SelectedTab.Tag;
                    selectedScreenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, region.ViewId);
                }

                if (selectedScreenshot == null)
                {
                    return false;
                }
                else
                {
                    if (selectedScreenshot.ViewId.Equals(Guid.Empty))
                    {
                        // *** Auto Screen Capture - Region Select / Auto Save ***
                        selectedScreenshot = _screenshotCollection.GetScreenshot(selectedSlide.Name, Guid.Empty);
                    }

                    if (selectedScreenshot.Encrypted)
                    {
                        MessageBox.Show("This screenshot was encrypted by Auto Screen Capture.\n\nThe application you have selected to open this screenshot may have a problem reading the file.", "Encrypted Screenshot", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    return RunEditor(editor, selectedScreenshot);
                }
            }

            return false;
        }

        /// <summary>
        /// Runs the editor for each screenshot taken at a specified date and time.
        /// </summary>
        /// <param name="editor">The editor to run.</param>
        /// <param name="dateTime">The date and time to use in finding the screenshots that match with that date and time.</param>
        private void RunEditor(Editor editor, DateTime dateTime)
        {
            if (editor != null)
            {
                System.Collections.Generic.List<Screenshot> screenshots = _screenshotCollection.GetScreenshots(dateTime.ToString(_macroParser.DateFormat), dateTime.ToString(_macroParser.TimeFormat));

                foreach (Screenshot screenshot in screenshots)
                {
                    if (screenshot != null && screenshot.Slide != null && !string.IsNullOrEmpty(screenshot.FilePath))
                    {
                        _log.WriteDebugMessage("Running editor (based on TriggerActionType.RunEditor) \"" + editor.Name + "\" using screenshot path \"" + screenshot.FilePath + "\"");

                        if (!RunEditor(editor, screenshot))
                        {
                            _log.WriteDebugMessage("Running editor failed. Perhaps the filepath of the screenshot file is no longer available");
                        }
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
            // Execute the chosen image editor. If the $filepath$ argument happens to be included
            // then we'll use that argument as the screenshot file path when executing the image editor.
            if (editor != null && (screenshot != null && !string.IsNullOrEmpty(screenshot.FilePath) &&
                _fileSystem.FileExists(editor.Application) && _fileSystem.FileExists(screenshot.FilePath)))
            {
                _log.WriteDebugMessage("Starting process for editor \"" + editor.Name + "\" ...");
                _log.WriteDebugMessage("Application: " + editor.Application);
                _log.WriteDebugMessage("Arguments: " + editor.Arguments.Replace("$filepath$", "\"" + screenshot.FilePath + "\""));

                _ = Process.Start(editor.Application, editor.Arguments.Replace("$filepath$", "\"" + screenshot.FilePath + "\""));

                // We successfully opened the editor with the given screenshot path.
                return true;
            }

            // We failed to open the editor with the given screenshot path.
            return false;
        }
    }
}
