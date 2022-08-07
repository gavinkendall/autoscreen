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
        private int _currentBanner = 0;

        /// <summary>
        /// A form showing information about Auto Screen Capture.
        /// </summary>
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {

        }

        private void FormAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            _currentBanner = 0;

            pictureBoxBanner.Image = Properties.Resources.about_0;

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
                case 1:
                    pictureBoxBanner.Image = Properties.Resources.about_1;
                    break;

                case 2:
                    pictureBoxBanner.Image = Properties.Resources.about_2;
                    break;

                case 3:
                    pictureBoxBanner.Image = Properties.Resources.about_3;
                    break;

                case 4:
                    pictureBoxBanner.Image = Properties.Resources.about_4;
                    break;

                default:
                    pictureBoxBanner.Image = Properties.Resources.about_0;
                    _currentBanner = 0;
                    break;
            }
        }
    }
}