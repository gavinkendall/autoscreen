//-----------------------------------------------------------------------
// <copyright file="FormRegionSelectCommandDeck.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The Region Select Command Deck tool.</summary>
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
    /// <summary>
    /// Region Select Command Deck
    /// </summary>
    public partial class FormRegionSelectCommandDeck : Form
    {
        /// <summary>
        /// Region Select Command Deck
        /// </summary>
        public FormRegionSelectCommandDeck()
        {
            InitializeComponent();
        }

        private void FormRegionSelectCommandDeck_Load(object sender, EventArgs e)
        {

        }

        private void FormRegionSelectCommandDeck_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void buttonRegionSelectClipboard_Click(object sender, EventArgs e)
        {

        }

        private void buttonRegionSelectClipboardAutoSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonRegionSelectClipboardAutoSaveEdit_Click(object sender, EventArgs e)
        {

        }

        private void buttonRegionSelectClipboardFloatingScreenshot_Click(object sender, EventArgs e)
        {

        }

        private void buttonRegionSelectFloatingScreenshot_Click(object sender, EventArgs e)
        {

        }

        private void buttonRegionSelectNewRegion_Click(object sender, EventArgs e)
        {

        }
    }
}
