//-----------------------------------------------------------------------
// <copyright file="FormDynamicRegexValidator.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The Dynamic Regex Validator tool.</summary>
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
using System.Text.RegularExpressions;

namespace AutoScreenCapture
{
    /// <summary>
    /// The Dynamic Regex Validator tool.
    /// </summary>
    public partial class FormDynamicRegexValidator : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public FormDynamicRegexValidator()
        {
            InitializeComponent();
        }

        private void FormDynamicRegexValidator_Load(object sender, EventArgs e)
        {
            CheckRegex();

            HelpMessage("Enter the regex and test value. The text input fields will turn green if the regex and test value match");
        }

        private void FormDynamicRegexValidator_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        /// <summary>
        /// Shows a help message.
        /// </summary>
        /// <param name="message">The message to show as a help message.</param>
        private void HelpMessage(string message)
        {
            labelHelp.Text = "       " + message;
        }

        private void textBoxRegularExpression_TextChanged(object sender, System.EventArgs e)
        {
            CheckRegex();
        }

        private void textBoxText_TextChanged(object sender, System.EventArgs e)
        {
            CheckRegex();
        }

        /// <summary>
        /// Checks the provided regular expression pattern compared to the given text and indicates if it's a match or not a match.
        /// </summary>
        private void CheckRegex()
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxRegularExpression.Text) ||
                    string.IsNullOrEmpty(textBoxTestValue.Text))
                {
                    toolStripStatusLabel.Text = "Empty";
                    textBoxRegularExpression.BackColor = System.Drawing.Color.LightYellow;
                    textBoxTestValue.BackColor = System.Drawing.Color.LightYellow;

                    return;
                }

                if (Regex.IsMatch(textBoxTestValue.Text, textBoxRegularExpression.Text))
                {
                    toolStripStatusLabel.Text = "Match";
                    textBoxRegularExpression.BackColor = System.Drawing.Color.LightGreen;
                    textBoxTestValue.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    toolStripStatusLabel.Text = "No Match";
                    textBoxRegularExpression.BackColor = System.Drawing.Color.PaleVioletRed;
                    textBoxTestValue.BackColor = System.Drawing.Color.PaleVioletRed;
                }
            }
            catch (Exception)
            {
                toolStripStatusLabel.Text = "Error";
                textBoxRegularExpression.BackColor = System.Drawing.Color.PaleVioletRed;
                textBoxTestValue.BackColor = System.Drawing.Color.PaleVioletRed;
            }
        }
    }
}
