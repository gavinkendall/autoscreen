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
            listBoxHelpItems.Items.Add("Quick Start");
            listBoxHelpItems.Items.Add("License");
            listBoxHelpItems.Items.Add("Changelog");
            listBoxHelpItems.Items.Add("Getting Started");
            listBoxHelpItems.Items.Add("Screens");
            listBoxHelpItems.Items.Add("Regions");
            listBoxHelpItems.Items.Add("Editors");
            listBoxHelpItems.Items.Add("Schedules");
            listBoxHelpItems.Items.Add("Macro Tags");
            listBoxHelpItems.Items.Add("Triggers");
            listBoxHelpItems.Items.Add("Command Line");
            listBoxHelpItems.Items.Add("Common Setup Scenarios");
            listBoxHelpItems.Items.Add("Configuration File");

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
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.welcome;
                    break;

                case 1:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.quick_start;
                    break;

                case 2:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.license;
                    break;

                case 3:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.changelog;
                    break;

                case 4:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.getting_started;
                    break;

                case 5:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.screens;
                    break;

                case 6:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.regions;
                    break;

                case 7:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.editors;
                    break;

                case 8:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.schedules;
                    break;

                case 9:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.macro_tags;
                    break;

                case 10:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.triggers;
                    break;

                case 11:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.command_line1;
                    break;

                case 12:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.common_setup_scenarios;
                    break;

                case 13:
                    richTextBoxHelpText.SelectedRtf = Properties.Resources.configuration_file;
                    break;
            }
        }
    }
}
