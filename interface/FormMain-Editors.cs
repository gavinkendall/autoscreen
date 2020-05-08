//-----------------------------------------------------------------------
// <copyright file="FormMain-Editors.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
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
            formEditor.EditorObject = null;

            formEditor.ShowDialog(this);

            if (formEditor.DialogResult == DialogResult.OK)
            {
                BuildEditorsModule();
                BuildViewTabPages();
                BuildScreenshotPreviewContextualMenu();

                formEditor.EditorCollection.SaveToXmlFile();
            }
        }

        /// <summary>
        /// Removes the selected Editors from the Editors tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSelectedEditors_Click(object sender, EventArgs e)
        {
            int countBeforeRemoval = formEditor.EditorCollection.Count;

            foreach (Control control in tabPageEditors.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Editor editor = formEditor.EditorCollection.Get((Editor)checkBox.Tag);
                        formEditor.EditorCollection.Remove(editor);
                    }
                }
            }

            if (countBeforeRemoval > formEditor.EditorCollection.Count)
            {
                BuildEditorsModule();
                BuildViewTabPages();
                BuildScreenshotPreviewContextualMenu();

                formEditor.EditorCollection.SaveToXmlFile();
            }
        }

        /// <summary>
        /// Runs the chosen image editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runEditor_Click(object sender, EventArgs e)
        {
            if (listBoxScreenshots.SelectedIndex > -1)
            {
                Editor editor = formEditor.EditorCollection.GetByName(sender.ToString());
                RunEditor(editor);
            }
        }

        /// <summary>
        /// Shows the "Change Editor" window to enable the user to edit a chosen Editor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeEditor_Click(object sender, EventArgs e)
        {
            Button buttonSelected = (Button)sender;

            if (buttonSelected.Tag != null)
            {
                formEditor.EditorObject = (Editor)buttonSelected.Tag;

                formEditor.ShowDialog(this);

                if (formEditor.DialogResult == DialogResult.OK)
                {
                    BuildEditorsModule();
                    BuildViewTabPages();
                    BuildScreenshotPreviewContextualMenu();

                    formEditor.EditorCollection.SaveToXmlFile();
                }
            }
        }

        /// <summary>
        /// Executes a chosen image editor from the interface.
        /// </summary>
        /// <param name="editor">The image editor to execute.</param>
        private void RunEditor(Editor editor)
        {
            if (editor != null)
            {
                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Screen))
                {
                    Screen screen = (Screen)tabControlViews.SelectedTab.Tag;
                    RunEditor(editor, _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, screen.ViewId));
                }

                if (tabControlViews.SelectedTab.Tag.GetType() == typeof(Region))
                {
                    Region region = (Region)tabControlViews.SelectedTab.Tag;
                    RunEditor(editor, _screenshotCollection.GetScreenshot(Slideshow.SelectedSlide.Name, region.ViewId));
                }
            }
        }

        /// <summary>
        /// Executes a chosen image editor from a Trigger to open the last screenshot taken with the image editor.
        /// </summary>
        /// <param name="editor">The image editor to execute.</param>
        /// <param name="triggerActionType">The trigger's action type.</param>
        private void RunEditor(Editor editor, TriggerActionType triggerActionType)
        {
            if (editor != null && triggerActionType == TriggerActionType.RunEditor)
            {
                Screenshot screenshot = _screenshotCollection.Get(_screenshotCollection.Count - 1);

                if (screenshot != null && screenshot.Slide != null && !string.IsNullOrEmpty(screenshot.Path))
                {
                    Log.WriteDebugMessage("Running editor (based on TriggerActionType.RunEditor) \"" + editor.Name + "\" using screenshot path \"" + screenshot.Path + "\"");

                    RunEditor(editor, screenshot);
                }
            }
        }

        /// <summary>
        /// Runs the editor using the specified screenshot.
        /// </summary>
        /// <param name="editor">The editor to use.</param>
        /// <param name="screenshot">The screenshot to use.</param>
        private void RunEditor(Editor editor, Screenshot screenshot)
        {
            // Execute the chosen image editor. If the %screenshot% argument happens to be included
            // then we'll use that argument as the screenshot file path when executing the image editor.
            if (editor != null && (screenshot != null && !string.IsNullOrEmpty(screenshot.Path) && FileSystem.FileExists(screenshot.Path)))
            {
                Log.WriteDebugMessage("Starting process for editor \"" + editor.Name + "\" ...");
                Log.WriteDebugMessage("Application: " + editor.Application);
                Log.WriteDebugMessage("Arguments before %screenshot% tag replacement: " + editor.Arguments);
                Log.WriteDebugMessage("Arguments after %screenshot% tag replacement: " + editor.Arguments.Replace("%screenshot%", "\"" + screenshot.Path + "\""));

                _ = Process.Start(editor.Application, editor.Arguments.Replace("%screenshot%", "\"" + screenshot.Path + "\""));
            }
        }
    }
}
