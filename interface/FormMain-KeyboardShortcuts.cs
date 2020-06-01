//-----------------------------------------------------------------------
// <copyright file="FormMain-KeyboardShortcuts.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>This is where we handle the key press even for a particular keyboard shortcut.</summary>
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
    public partial class FormMain : Form
    {
        private void hotKey_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Key == Keys.Z)
            {
                StartScreenCapture();
            }

            if (e.Key == Keys.X)
            {
                StopScreenCapture();
            }

            if (e.Key == Keys.A)
            {
                CaptureNowArchive();
            }

            if (e.Key == Keys.E)
            {
                CaptureNowEdit();
            }

            if (e.Key == Keys.S)
            {
                toolStripMenuItemRegionSelectClipboard_Click(sender, e);
            }
        }
    }
}