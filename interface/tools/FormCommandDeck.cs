//-----------------------------------------------------------------------
// <copyright file="FormCommandDeck.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The Command Deck tool.</summary>
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
    /// Command Deck
    /// </summary>
    public partial class FormCommandDeck : Form
    {
        /// <summary>
        /// Event handler for Region Select -> Clipboard
        /// </summary>
        public event EventHandler Clipboard;

        /// <summary>
        /// Event handler for Region Select -> Clipboard / Auto Save
        /// </summary>
        public event EventHandler ClipboardAutoSave;

        /// <summary>
        /// Event handler for Region Select -> Clipboard / Auto Save / Edit
        /// </summary>
        public event EventHandler ClipboardAutoSaveEdit;

        /// <summary>
        /// Event handler for Region Select -> Clipboard / Floating Screenshot
        /// </summary>
        public event EventHandler ClipboardFloatingScreenshot;

        /// <summary>
        /// Event handler for Region Select -> Floating Screenshot
        /// </summary>
        public event EventHandler FloatingScreenshot;

        /// <summary>
        /// Event handler for Region Select -> Add Region
        /// </summary>
        public event EventHandler AddRegion;

        /// <summary>
        /// Event handler for Region Select -> Add Region (Express)
        /// </summary>
        public event EventHandler AddRegionExpress;

        /// <summary>
        /// Region Select Command Deck
        /// </summary>
        public FormCommandDeck()
        {
            InitializeComponent();
        }

        private void FormRegionSelectCommandDeck_Load(object sender, EventArgs e)
        {
            Height = 87;
        }

        private void FormRegionSelectCommandDeck_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void buttonRegionSelectClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.Invoke(sender, e);
        }

        private void buttonRegionSelectClipboardAutoSave_Click(object sender, EventArgs e)
        {
            ClipboardAutoSave.Invoke(sender, e);
        }

        private void buttonRegionSelectClipboardAutoSaveEdit_Click(object sender, EventArgs e)
        {
            ClipboardAutoSaveEdit.Invoke(sender, e);
        }

        private void buttonRegionSelectClipboardFloatingScreenshot_Click(object sender, EventArgs e)
        {
            ClipboardFloatingScreenshot.Invoke(sender, e);
        }

        private void buttonRegionSelectFloatingScreenshot_Click(object sender, EventArgs e)
        {
            FloatingScreenshot.Invoke(sender, e);
        }

        private void buttonRegionSelectAddRegion_Click(object sender, EventArgs e)
        {
            AddRegion.Invoke(sender, e);
        }

        private void buttonRegionSelectAddRegionExpress_Click(object sender, EventArgs e)
        {
            AddRegionExpress.Invoke(sender, e);
        }

        private void buttonShowHideRegionSelect_Click(object sender, EventArgs e)
        {
            if (Height == 87)
            {
                Height = 319;
            }
            else if (Height == 319)
            {
                Height = 87;
            }
        }
    }
}
