//-----------------------------------------------------------------------
// <copyright file="FormEncryptorDecryptor.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The Encryptor / Decryptor tool for encrypting/decrypting screenshots, files, and text.</summary>
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// The Encryptor / Decryptor tool for encrypting/decrypting screenshots, files, and text.
    /// </summary>
    public partial class FormEncryptorDecryptor : Form
    {
        private Security _security;
        private ScreenshotCollection _screenshotCollection;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="security">Security.</param>
        /// <param name="screenshotCollection">Screenshot collection.</param>
        public FormEncryptorDecryptor(Security security, ScreenshotCollection screenshotCollection)
        {
            InitializeComponent();

            _security = security;
            _screenshotCollection = screenshotCollection;
        }

        private void FormEncryptorDecryptor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
