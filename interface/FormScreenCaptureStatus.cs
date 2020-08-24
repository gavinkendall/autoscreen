//-----------------------------------------------------------------------
// <copyright file="FormScreenCaptureStatus.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for asking the user for the passphrase during a locked screen capture session.</summary>
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
    /// A form to show screen capture status.
    /// </summary>
    public partial class FormScreenCaptureStatus : Form
    {
        /// <summary>
        /// A small fixed tool window that shows screen capture status in its title bar.
        /// </summary>
        public FormScreenCaptureStatus()
        {
            InitializeComponent();

            
        }

        private void FormInformationWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
