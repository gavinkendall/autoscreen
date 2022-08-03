//-----------------------------------------------------------------------
// <copyright file="FormMain-Tags.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Methods for adding, removing, and changing macro tags.</summary>
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
        /// Shows the "Add Macro Tag" window to enable the user to add a chosen Macro Tag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addMacroTag_Click(object sender, EventArgs e)
        {
            ShowInterface();

            _formMacroTag.MacroTagObject = null;

            if (!_formMacroTag.Visible)
            {
                _formMacroTag.ShowDialog(this);
            }
            else
            {
                _formMacroTag.Activate();
            }

            if (_formMacroTag.DialogResult == DialogResult.OK)
            {
                BuildMacroTagsModule();

                if (!_formMacroTag.MacroTagCollection.SaveToXmlFile(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Removes the selected Macro Tags from the Macro Tags tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSelectedMacroTags_Click(object sender, EventArgs e)
        {
            int countBeforeRemoval = _formMacroTag.MacroTagCollection.Count;

            foreach (Control control in tabPageMacroTags.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        MacroTag trigger = _formMacroTag.MacroTagCollection.Get((MacroTag)checkBox.Tag);
                        _formMacroTag.MacroTagCollection.Remove(trigger);
                    }
                }
            }

            if (countBeforeRemoval > _formMacroTag.MacroTagCollection.Count)
            {
                BuildMacroTagsModule();

                if (!_formMacroTag.MacroTagCollection.SaveToXmlFile(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Shows the "Configure Macro Tag" window to enable the user to edit a chosen Macro Tag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configureMacroTag_Click(object sender, EventArgs e)
        {
            ShowInterface();

            MacroTag macroTag = new MacroTag(_macroParser);

            if (sender is Button)
            {
                Button buttonSelected = (Button)sender;
                macroTag = (MacroTag)buttonSelected.Tag;
            }

            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                macroTag = (MacroTag)toolStripMenuItemSelected.Tag;
            }

            _formMacroTag.MacroTagObject = macroTag;

            if (!_formMacroTag.Visible)
            {
                _formMacroTag.ShowDialog(this);
            }
            else
            {
                _formMacroTag.Activate();
            }

            if (_formMacroTag.DialogResult == DialogResult.OK)
            {
                BuildMacroTagsModule();

                if (!_formMacroTag.MacroTagCollection.SaveToXmlFile(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }
    }
}
