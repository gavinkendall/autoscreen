//-----------------------------------------------------------------------
// <copyright file="FormHelp.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Shows a help system for Auto Screen Capture.</summary>
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
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// Shows a help system for Auto Screen Capture.
    /// </summary>
    public partial class FormHelp : Form
    {
        /// <summary>
        /// Shows a help system for Auto Screen Capture.
        /// </summary>
        public FormHelp()
        {
            InitializeComponent();
        }

        private void FormHelp_Load(object sender, System.EventArgs e)
        {
            listBoxHelpItems.Items.Add("Welcome");
            listBoxHelpItems.Items.Add("License");
            listBoxHelpItems.Items.Add("Changelog");
            listBoxHelpItems.Items.Add("Getting Started");
            listBoxHelpItems.Items.Add("Screens");
            listBoxHelpItems.Items.Add("Regions");
            listBoxHelpItems.Items.Add("Editors");

            listBoxHelpItems.SelectedIndex = 0;
        }

        private void FormHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void listBoxHelpItems_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            richTextBoxHelpText.Clear();

            switch (listBoxHelpItems.SelectedIndex)
            {
                case 0:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.help_0;
                    break;

                case 1:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.help_1;
                    break;

                case 2:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.help_2;
                    break;

                case 3:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.help_3;
                    break;

                case 4:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.help_4;
                    break;

                case 5:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.help_5;
                    break;

                case 6:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.help_6;
                    break;
            }
        }
    }
}
