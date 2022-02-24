//-----------------------------------------------------------------------
// <copyright file="FormAbout.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form showing information about Auto Screen Capture.</summary>
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
    /// <summary>
    /// A form showing information about Auto Screen Capture.
    /// </summary>
    public partial class FormAbout : Form
    {
        private int _currentBanner = -1;

        /// <summary>
        /// A form showing information about Auto Screen Capture.
        /// </summary>
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            richTextBoxBladeDetails.Text = "\"Blade\"" + Environment.NewLine;
            richTextBoxBladeDetails.Text += "Release Date: August 21, 1998" + Environment.NewLine;
            richTextBoxBladeDetails.Text += "Rating: R (strong violence)" + Environment.NewLine;
            richTextBoxBladeDetails.Text += "Genres: Action, Horror, Sci-Fi" + Environment.NewLine + Environment.NewLine;
            richTextBoxBladeDetails.Text += "Director: Stephen Norrington" + Environment.NewLine;
            richTextBoxBladeDetails.Text += "Writer: David S. Goyer" + Environment.NewLine + Environment.NewLine;
            richTextBoxBladeDetails.Text += "Starring:" + Environment.NewLine;
            richTextBoxBladeDetails.Text += "Wesley Snipes (Blade)" + Environment.NewLine;
            richTextBoxBladeDetails.Text += "Kris Kristofferson (Whistler)" + Environment.NewLine;
            richTextBoxBladeDetails.Text += "Stephen Dorff (Deacon Frost)" + Environment.NewLine;
            richTextBoxBladeDetails.Text += "N'Bushe Wright (Karen)" + Environment.NewLine;
            richTextBoxBladeDetails.Text += "Donal Logue (Quinn)" + Environment.NewLine;
            richTextBoxBladeDetails.Text += "Udo Kier (Dragonetti)" + Environment.NewLine;
        }

        private void FormAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            _currentBanner = -1;

            pictureBoxBanner.Image = Properties.Resources.blade;

            e.Cancel = true;
            Hide();
        }

        private void richTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void pictureBoxBanner_Click(object sender, System.EventArgs e)
        {
            _currentBanner++;

            switch (_currentBanner)
            {
                case 0:
                    pictureBoxBanner.Image = Properties.Resources.blade0;
                    break;

                case 1:
                    pictureBoxBanner.Image = Properties.Resources.blade1;
                    break;

                case 2:
                    pictureBoxBanner.Image = Properties.Resources.blade2;
                    break;

                case 3:
                    pictureBoxBanner.Image = Properties.Resources.blade3;
                    break;

                case 4:
                    pictureBoxBanner.Image = Properties.Resources.blade4;
                    break;

                case 5:
                    pictureBoxBanner.Image = Properties.Resources.blade5;
                    break;

                case 6:
                    pictureBoxBanner.Image = Properties.Resources.blade6;
                    break;

                default:
                    pictureBoxBanner.Image = Properties.Resources.blade;
                    _currentBanner = -1;
                    break;
            }
        }
    }
}