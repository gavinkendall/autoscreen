//-----------------------------------------------------------------------
// <copyright file="FormMacroTag.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for adding a new macro tag or changing an existing macro tag.</summary>
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
    /// A form for managing a macro tag.
    /// </summary>
    public partial class FormMacroTag : Form
    {
        private ToolTip _toolTip = new ToolTip();

        private MacroParser _macroParser;

        /// <summary>
        /// A collection of macro tags.
        /// </summary>
        public MacroTagCollection MacroTagCollection { get; } = new MacroTagCollection();

        /// <summary>
        /// The macro tag object we're handling.
        /// </summary>
        public MacroTag MacroTagObject { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public FormMacroTag(MacroParser macroParser)
        {
            InitializeComponent();

            _macroParser = macroParser;
        }

        private void FormMacroTag_Load(object sender, EventArgs e)
        {
            textBoxName.Focus();

            HelpMessage("This is where to configure a macro tag which will be used when the filepath of a screenshot is parsed");

            _toolTip.SetToolTip(checkBoxActive, "The filepath containing this macro tag will be parsed if Active is checked (turned on)");
            _toolTip.SetToolTip(comboBoxType, "The type of macro tag depends on what information will be acquired for it");

            comboBoxType.Items.Clear();
            comboBoxType.Items.Add("Screen Name");
            comboBoxType.Items.Add("Screen Number");
            comboBoxType.Items.Add("Image Format");
            comboBoxType.Items.Add("Screen Capture Cycle Count");
            comboBoxType.Items.Add("Active Window Title");
            comboBoxType.Items.Add("Date/Time Format");
            comboBoxType.Items.Add("User");
            comboBoxType.Items.Add("Machine");
            comboBoxType.Items.Add("Time Range");
            comboBoxType.Items.Add("Date/Time Format Expression");
            comboBoxType.Items.Add("Quarter Year");

            if (MacroTagObject != null)
            {
                Text = "Change Macro Tag";

                textBoxName.Text = MacroTagObject.Name;

                textBoxDescription.Text = MacroTagObject.Description;

                comboBoxType.SelectedIndex = (int)MacroTagObject.Type;
                textBoxDateTimeFormatValue.Text = MacroTagObject.DateTimeFormatValue;

                dateTimePickerMacro1Start.Value = MacroTagObject.TimeRangeMacro1Start;
                dateTimePickerMacro1End.Value = MacroTagObject.TimeRangeMacro1End;

                dateTimePickerMacro2Start.Value = MacroTagObject.TimeRangeMacro2Start;
                dateTimePickerMacro2End.Value = MacroTagObject.TimeRangeMacro2End;

                dateTimePickerMacro3Start.Value = MacroTagObject.TimeRangeMacro3Start;
                dateTimePickerMacro3End.Value = MacroTagObject.TimeRangeMacro3End;

                dateTimePickerMacro4Start.Value = MacroTagObject.TimeRangeMacro4Start;
                dateTimePickerMacro4End.Value = MacroTagObject.TimeRangeMacro4End;

                textBoxMacro1Macro.Text = MacroTagObject.TimeRangeMacro1Macro;
                textBoxMacro2Macro.Text = MacroTagObject.TimeRangeMacro2Macro;
                textBoxMacro3Macro.Text = MacroTagObject.TimeRangeMacro3Macro;
                textBoxMacro4Macro.Text = MacroTagObject.TimeRangeMacro4Macro;

                checkBoxActive.Checked = MacroTagObject.Active;

                textBoxNotes.Text = MacroTagObject.Notes;
            }
            else
            {
                Text = "Add Macro Tag";

                MacroTag tag = new MacroTag(_macroParser);

                textBoxName.Text = "%macrotag" + (MacroTagCollection.Count + 1) + "%";

                textBoxDescription.Text = "Please provide a brief summary for the purpose of this macro tag";

                comboBoxType.SelectedIndex = 0;
                textBoxDateTimeFormatValue.Text = tag.DateTimeFormatValue;

                dateTimePickerMacro1Start.Value = tag.TimeRangeMacro1Start;
                dateTimePickerMacro1End.Value = tag.TimeRangeMacro1End;

                dateTimePickerMacro2Start.Value = tag.TimeRangeMacro2Start;
                dateTimePickerMacro2End.Value = tag.TimeRangeMacro2End;

                dateTimePickerMacro3Start.Value = tag.TimeRangeMacro3Start;
                dateTimePickerMacro3End.Value = tag.TimeRangeMacro3End;

                dateTimePickerMacro4Start.Value = tag.TimeRangeMacro4Start;
                dateTimePickerMacro4End.Value = tag.TimeRangeMacro4End;

                textBoxMacro1Macro.Text = tag.TimeRangeMacro1Macro;
                textBoxMacro2Macro.Text = tag.TimeRangeMacro2Macro;
                textBoxMacro3Macro.Text = tag.TimeRangeMacro3Macro;
                textBoxMacro4Macro.Text = tag.TimeRangeMacro4Macro;

                checkBoxActive.Checked = true;

                textBoxNotes.Text = string.Empty;
            }
        }

        private void HelpMessage(string message)
        {
            labelHelp.Text = "       " + message;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (MacroTagObject != null)
            {
                ChangeMacroTag();
            }
            else
            {
                AddMacroTag();
            }
        }

        private void AddMacroTag()
        {
            if (InputValid())
            {
                TrimInput();

                if (MacroTagCollection.GetByName(textBoxName.Text) == null)
                {
                    MacroTagCollection.Add(new MacroTag(_macroParser, textBoxName.Text,
                        textBoxDescription.Text,
                        (MacroTagType)comboBoxType.SelectedIndex,
                        textBoxDateTimeFormatValue.Text,
                        dateTimePickerMacro1Start.Value,
                        dateTimePickerMacro1End.Value,
                        textBoxMacro1Macro.Text,
                        dateTimePickerMacro2Start.Value,
                        dateTimePickerMacro2End.Value,
                        textBoxMacro2Macro.Text,
                        dateTimePickerMacro3Start.Value,
                        dateTimePickerMacro3End.Value,
                        textBoxMacro3Macro.Text,
                        dateTimePickerMacro4Start.Value,
                        dateTimePickerMacro4End.Value,
                        textBoxMacro4Macro.Text,
                        checkBoxActive.Checked,
                        textBoxNotes.Text
                        ));

                    Okay();
                }
                else
                {
                    MessageBox.Show("A macro tag with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeMacroTag()
        {
            if (InputValid())
            {
                if (NameChanged() || InputChanged())
                {
                    TrimInput();

                    if (MacroTagCollection.GetByName(textBoxName.Text) != null && NameChanged())
                    {
                        MessageBox.Show("A macro tag with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MacroTagCollection.Get(MacroTagObject).Name = textBoxName.Text;
                        MacroTagCollection.Get(MacroTagObject).Description = textBoxDescription.Text;
                        MacroTagCollection.Get(MacroTagObject).Type = (MacroTagType)comboBoxType.SelectedIndex;
                        MacroTagCollection.Get(MacroTagObject).DateTimeFormatValue = textBoxDateTimeFormatValue.Text;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro1Start = dateTimePickerMacro1Start.Value;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro1End = dateTimePickerMacro1End.Value;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro1Macro = textBoxMacro1Macro.Text;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro2Start = dateTimePickerMacro2Start.Value;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro2End = dateTimePickerMacro2End.Value;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro2Macro = textBoxMacro2Macro.Text;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro3Start = dateTimePickerMacro3Start.Value;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro3End = dateTimePickerMacro3End.Value;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro3Macro = textBoxMacro3Macro.Text;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro4Start = dateTimePickerMacro4Start.Value;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro4End = dateTimePickerMacro4End.Value;
                        MacroTagCollection.Get(MacroTagObject).TimeRangeMacro4Macro = textBoxMacro4Macro.Text;
                        MacroTagCollection.Get(MacroTagObject).Active = checkBoxActive.Checked;
                        MacroTagCollection.Get(MacroTagObject).Notes = textBoxNotes.Text;

                        Okay();
                    }
                }
                else
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrimInput()
        {
            textBoxName.Text = textBoxName.Text.Trim();
            textBoxDescription.Text = textBoxDescription.Text.Trim();
            textBoxDateTimeFormatValue.Text = textBoxDateTimeFormatValue.Text.Trim();
            textBoxMacro1Macro.Text = textBoxMacro1Macro.Text.Trim();
            textBoxMacro2Macro.Text = textBoxMacro2Macro.Text.Trim();
            textBoxMacro3Macro.Text = textBoxMacro3Macro.Text.Trim();
            textBoxMacro4Macro.Text = textBoxMacro4Macro.Text.Trim();
            textBoxNotes.Text = textBoxNotes.Text.Trim();

            if (!textBoxName.Text.StartsWith("%"))
                textBoxName.Text = "%" + textBoxName.Text;

            if (!textBoxName.Text.EndsWith("%"))
                textBoxName.Text += "%";
        }

        private bool InputValid()
        {
            if (textBoxDateTimeFormatValue.Enabled)
            {
                if (!string.IsNullOrEmpty(textBoxName.Text) &&
                    !string.IsNullOrEmpty(textBoxDescription.Text) &&
                    textBoxDateTimeFormatValue.Enabled &&
                    !string.IsNullOrEmpty(textBoxDateTimeFormatValue.Text))
                {
                    return true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(textBoxName.Text) &&
                    !string.IsNullOrEmpty(textBoxDescription.Text))
                {
                    return true;
                }
            }

            return false;
        }

        private bool InputChanged()
        {
            bool changed = false;

            if (MacroTagObject != null)
            {
                if (!MacroTagObject.Description.Equals(textBoxDescription.Text) ||
                    !MacroTagObject.Notes.Equals(textBoxNotes.Text) ||
                    MacroTagObject.Active != checkBoxActive.Checked ||
                    (int)MacroTagObject.Type != comboBoxType.SelectedIndex)
                {
                    changed = true;
                }

                if (textBoxDateTimeFormatValue.Enabled &&
                    !string.IsNullOrEmpty(textBoxDateTimeFormatValue.Text) &&
                    !MacroTagObject.DateTimeFormatValue.Equals(textBoxDateTimeFormatValue.Text))
                {
                    changed = true;
                }

                if (groupBoxTimeRange.Enabled &&
                    !MacroTagObject.TimeRangeMacro1Start.Equals(dateTimePickerMacro1Start.Value) ||
                    !MacroTagObject.TimeRangeMacro1End.Equals(dateTimePickerMacro1End.Value) ||
                    !MacroTagObject.TimeRangeMacro1Macro.Equals(textBoxMacro1Macro.Text) ||
                    !MacroTagObject.TimeRangeMacro2Start.Equals(dateTimePickerMacro2Start.Value) ||
                    !MacroTagObject.TimeRangeMacro2End.Equals(dateTimePickerMacro2End.Value) ||
                    !MacroTagObject.TimeRangeMacro2Macro.Equals(textBoxMacro2Macro.Text) ||
                    !MacroTagObject.TimeRangeMacro3Start.Equals(dateTimePickerMacro3Start.Value) ||
                    !MacroTagObject.TimeRangeMacro3End.Equals(dateTimePickerMacro3End.Value) ||
                    !MacroTagObject.TimeRangeMacro3Macro.Equals(textBoxMacro3Macro.Text) ||
                    !MacroTagObject.TimeRangeMacro4Start.Equals(dateTimePickerMacro4Start.Value) ||
                    !MacroTagObject.TimeRangeMacro4End.Equals(dateTimePickerMacro4End.Value) ||
                    !MacroTagObject.TimeRangeMacro4Macro.Equals(textBoxMacro4Macro.Text))
                {
                    changed = true;
                }
            }

            return changed;
        }

        private bool NameChanged()
        {
            if (MacroTagObject != null &&
                !MacroTagObject.Name.Equals(textBoxName.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Okay()
        {
            DialogResult = DialogResult.OK;

            Close();
        }

        private void ComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelDateTimeFormatValue.Enabled = false;
            textBoxDateTimeFormatValue.Enabled = false;

            groupBoxTimeRange.Enabled = false;

            labelMacro1.Enabled = false;
            dateTimePickerMacro1Start.Enabled = false;
            dateTimePickerMacro1End.Enabled = false;
            textBoxMacro1Macro.Enabled = false;

            labelMacro2.Enabled = false;
            dateTimePickerMacro2Start.Enabled = false;
            dateTimePickerMacro2End.Enabled = false;
            textBoxMacro2Macro.Enabled = false;

            labelMacro3.Enabled = false;
            dateTimePickerMacro3Start.Enabled = false;
            dateTimePickerMacro3End.Enabled = false;
            textBoxMacro3Macro.Enabled = false;

            labelMacro4.Enabled = false;
            dateTimePickerMacro4Start.Enabled = false;
            dateTimePickerMacro4End.Enabled = false;
            textBoxMacro4Macro.Enabled = false;

            MacroTagType tagType = (MacroTagType) comboBoxType.SelectedIndex;

            if (tagType.Equals(MacroTagType.DateTimeFormat) ||
                tagType.Equals(MacroTagType.DateTimeFormatExpression))
            {
                labelDateTimeFormatValue.Enabled = true;
                textBoxDateTimeFormatValue.Enabled = true;
            }

            if (tagType.Equals(MacroTagType.TimeRange))
            {
                groupBoxTimeRange.Enabled = true;

                labelMacro1.Enabled = true;
                dateTimePickerMacro1Start.Enabled = true;
                dateTimePickerMacro1End.Enabled = true;
                textBoxMacro1Macro.Enabled = true;

                labelMacro2.Enabled = true;
                dateTimePickerMacro2Start.Enabled = true;
                dateTimePickerMacro2End.Enabled = true;
                textBoxMacro2Macro.Enabled = true;

                labelMacro3.Enabled = true;
                dateTimePickerMacro3Start.Enabled = true;
                dateTimePickerMacro3End.Enabled = true;
                textBoxMacro3Macro.Enabled = true;

                labelMacro4.Enabled = true;
                dateTimePickerMacro4Start.Enabled = true;
                dateTimePickerMacro4End.Enabled = true;
                textBoxMacro4Macro.Enabled = true;
            }
        }
    }
}