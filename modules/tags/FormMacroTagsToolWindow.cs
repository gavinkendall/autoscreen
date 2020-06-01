//-----------------------------------------------------------------------
// <copyright file="FormMacroTagsToolWindow.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
        private TagCollection _tagCollection;

        /// <summary>
        /// Empty constructor for the Macro Tags tool window.
        /// </summary>
        public FormMacroTagsToolWindow(TagCollection tagCollection)
        {
            InitializeComponent();

            _tagCollection = tagCollection;
        }

        private void FormMacroTagsToolWindow_Load(object sender, System.EventArgs e)
        {
            List<string> macroTags = new List<string>();

            foreach (Tag tag in _tagCollection.Collection)
            {
                macroTags.Add(tag.Name);
            }

            listBoxMacroTags.DataSource = macroTags;

            if (_tagCollection.Collection.Count > 0)
            {
                listBoxMacroTags.SelectedIndex = 0;
            }
        }

        private void listBoxMacroTags_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Tag tag = _tagCollection.GetByName((string)listBoxMacroTags.SelectedItem);

            labelHelp.Text = MacroParser.ParseTags(tag.Description, _tagCollection);
        }
    }
}
