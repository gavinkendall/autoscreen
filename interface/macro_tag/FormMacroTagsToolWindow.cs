//-----------------------------------------------------------------------
// <copyright file="FormMacroTagsToolWindow.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A list of available macro tags.</summary>
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
using System.Collections.Generic;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// A form for listing the available macro tags.
    /// </summary>
    public partial class FormMacroTagsToolWindow : Form
    {
        private Log _log;
        private MacroParser _macroParser;
        private MacroTagCollection _macroTagCollection;

        /// <summary>
        /// Empty constructor for the Macro Tags tool window.
        /// </summary>
        public FormMacroTagsToolWindow(MacroTagCollection tagCollection, MacroParser macroParser, Log log)
        {
            InitializeComponent();

            _log = log;
            _macroParser = macroParser;
            _macroTagCollection = tagCollection;
        }

        private void FormMacroTagsToolWindow_Load(object sender, System.EventArgs e)
        {
            List<string> macroTags = new List<string>();

            foreach (MacroTag tag in _macroTagCollection.Collection)
            {
                macroTags.Add(tag.Name);
            }

            listBoxMacroTags.DataSource = macroTags;

            if (_macroTagCollection.Collection.Count > 0)
            {
                listBoxMacroTags.SelectedIndex = 0;
            }
        }

        private void listBoxMacroTags_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            MacroTag tag = _macroTagCollection.GetByName((string)listBoxMacroTags.SelectedItem);

            labelHelp.Text = _macroParser.ParseTags(tag.Description,_macroTagCollection, _log);
        }
    }
}
