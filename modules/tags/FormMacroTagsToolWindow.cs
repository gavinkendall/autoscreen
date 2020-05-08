//-----------------------------------------------------------------------
// <copyright file="FormMacroTagsToolWindow.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A list of available macro tags.</summary>
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

            macroTags.Sort();

            listBoxMacroTags.DataSource = macroTags;
        }
    }
}
