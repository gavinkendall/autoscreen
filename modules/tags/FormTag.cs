//-----------------------------------------------------------------------
// <copyright file="FormTag.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
    public partial class FormTag : Form
    {
        /// <summary>
        /// A collection of macro tags.
        /// </summary>
        public TagCollection TagCollection { get; } = new TagCollection();

        /// <summary>
        /// The macro tag object we're handling.
        /// </summary>
        public Tag TagObject { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public FormTag()
        {
            InitializeComponent();
        }

        private void FormTag_Load(object sender, EventArgs e)
        {
            textBoxName.Focus();

            HelpMessage("This is where to configure a macro tag; special text that is replaced by an appropriate value (such as %time% is replaced with the current time)");

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

            if (TagObject != null)
            {
                Text = "Change Tag";

                textBoxName.Text = TagObject.Name;

                textBoxDescription.Text = TagObject.Description;

                comboBoxType.SelectedIndex = (int)TagObject.Type;
                textBoxDateTimeFormatValue.Text = TagObject.DateTimeFormatValue;

                dateTimePickerMacro1Start.Value = TagObject.TimeRangeMacro1Start;
                dateTimePickerMacro1End.Value = TagObject.TimeRangeMacro1End;

                dateTimePickerMacro2Start.Value = TagObject.TimeRangeMacro2Start;
                dateTimePickerMacro2End.Value = TagObject.TimeRangeMacro2End;

                dateTimePickerMacro3Start.Value = TagObject.TimeRangeMacro3Start;
                dateTimePickerMacro3End.Value = TagObject.TimeRangeMacro3End;

                dateTimePickerMacro4Start.Value = TagObject.TimeRangeMacro4Start;
                dateTimePickerMacro4End.Value = TagObject.TimeRangeMacro4End;

                textBoxMacro1Macro.Text = TagObject.TimeRangeMacro1Macro;
                textBoxMacro2Macro.Text = TagObject.TimeRangeMacro2Macro;
                textBoxMacro3Macro.Text = TagObject.TimeRangeMacro3Macro;
                textBoxMacro4Macro.Text = TagObject.TimeRangeMacro4Macro;

                checkBoxActive.Checked = TagObject.Active;

                textBoxNotes.Text = TagObject.Notes;
            }
            else
            {
                Text = "Add Tag";

                Tag tag = new Tag();

                textBoxName.Text = "%tag" + (TagCollection.Count + 1) + "%";

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
            if (TagObject != null)
            {
                ChangeTag();
            }
            else
            {
                AddTag();
            }
        }

        private void AddTag()
        {
            if (InputValid())
            {
                TrimInput();

                if (TagCollection.GetByName(textBoxName.Text) == null)
                {
                    TagCollection.Add(new Tag(textBoxName.Text,
                        textBoxDescription.Text,
                        (TagType)comboBoxType.SelectedIndex,
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
                    MessageBox.Show("A tag with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeTag()
        {
            if (InputValid())
            {
                if (NameChanged() || InputChanged())
                {
                    TrimInput();

                    if (TagCollection.GetByName(textBoxName.Text) != null && NameChanged())
                    {
                        MessageBox.Show("A tag with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        TagCollection.Get(TagObject).Name = textBoxName.Text;
                        TagCollection.Get(TagObject).Description = textBoxDescription.Text;
                        TagCollection.Get(TagObject).Type = (TagType)comboBoxType.SelectedIndex;
                        TagCollection.Get(TagObject).DateTimeFormatValue = textBoxDateTimeFormatValue.Text;
                        TagCollection.Get(TagObject).TimeRangeMacro1Start = dateTimePickerMacro1Start.Value;
                        TagCollection.Get(TagObject).TimeRangeMacro1End = dateTimePickerMacro1End.Value;
                        TagCollection.Get(TagObject).TimeRangeMacro1Macro = textBoxMacro1Macro.Text;
                        TagCollection.Get(TagObject).TimeRangeMacro2Start = dateTimePickerMacro2Start.Value;
                        TagCollection.Get(TagObject).TimeRangeMacro2End = dateTimePickerMacro2End.Value;
                        TagCollection.Get(TagObject).TimeRangeMacro2Macro = textBoxMacro2Macro.Text;
                        TagCollection.Get(TagObject).TimeRangeMacro3Start = dateTimePickerMacro3Start.Value;
                        TagCollection.Get(TagObject).TimeRangeMacro3End = dateTimePickerMacro3End.Value;
                        TagCollection.Get(TagObject).TimeRangeMacro3Macro = textBoxMacro3Macro.Text;
                        TagCollection.Get(TagObject).TimeRangeMacro4Start = dateTimePickerMacro4Start.Value;
                        TagCollection.Get(TagObject).TimeRangeMacro4End = dateTimePickerMacro4End.Value;
                        TagCollection.Get(TagObject).TimeRangeMacro4Macro = textBoxMacro4Macro.Text;
                        TagCollection.Get(TagObject).Active = checkBoxActive.Checked;
                        TagCollection.Get(TagObject).Notes = textBoxNotes.Text;

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

            if (TagObject != null)
            {
                if (!TagObject.Description.Equals(textBoxDescription.Text) ||
                    !TagObject.Notes.Equals(textBoxNotes.Text) ||
                    TagObject.Active != checkBoxActive.Checked ||
                    (int)TagObject.Type != comboBoxType.SelectedIndex)
                {
                    changed = true;
                }

                if (textBoxDateTimeFormatValue.Enabled &&
                    !string.IsNullOrEmpty(textBoxDateTimeFormatValue.Text) &&
                    !TagObject.DateTimeFormatValue.Equals(textBoxDateTimeFormatValue.Text))
                {
                    changed = true;
                }

                if (groupBoxTimeRange.Enabled &&
                    !TagObject.TimeRangeMacro1Start.Equals(dateTimePickerMacro1Start.Value) ||
                    !TagObject.TimeRangeMacro1End.Equals(dateTimePickerMacro1End.Value) ||
                    !TagObject.TimeRangeMacro1Macro.Equals(textBoxMacro1Macro.Text) ||
                    !TagObject.TimeRangeMacro2Start.Equals(dateTimePickerMacro2Start.Value) ||
                    !TagObject.TimeRangeMacro2End.Equals(dateTimePickerMacro2End.Value) ||
                    !TagObject.TimeRangeMacro2Macro.Equals(textBoxMacro2Macro.Text) ||
                    !TagObject.TimeRangeMacro3Start.Equals(dateTimePickerMacro3Start.Value) ||
                    !TagObject.TimeRangeMacro3End.Equals(dateTimePickerMacro3End.Value) ||
                    !TagObject.TimeRangeMacro3Macro.Equals(textBoxMacro3Macro.Text) ||
                    !TagObject.TimeRangeMacro4Start.Equals(dateTimePickerMacro4Start.Value) ||
                    !TagObject.TimeRangeMacro4End.Equals(dateTimePickerMacro4End.Value) ||
                    !TagObject.TimeRangeMacro4Macro.Equals(textBoxMacro4Macro.Text))
                {
                    changed = true;
                }
            }

            return changed;
        }

        private bool NameChanged()
        {
            if (TagObject != null &&
                !TagObject.Name.Equals(textBoxName.Text))
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

            TagType tagType = (TagType) comboBoxType.SelectedIndex;

            if (tagType.Equals(TagType.DateTimeFormat) ||
                tagType.Equals(TagType.DateTimeFormatExpression))
            {
                labelDateTimeFormatValue.Enabled = true;
                textBoxDateTimeFormatValue.Enabled = true;
            }

            if (tagType.Equals(TagType.TimeRange))
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

        private void textBoxTagName_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The name for the macro tag. Please make sure to surround the tag name with % (such as %time%)");
        }

        private void checkBoxActive_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Filenames containing this macro tag will be parsed if Active is checked (turned on)");
        }

        private void comboBoxType_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The type of macro tag depends on what information will be acquired for it");
        }

        private void textBoxDescription_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The macro tag requires a description to summarize the tag's purpose");
        }

        private void textBoxDateTimeFormatValue_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Parse a date format (yyyy = year, MM = month, dd = day), time format (HH = hour, mm = minute, ss = second, fff = millisecond), or expression ({day+7})");
        }

        private void textBoxNotes_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("An area for you to keep notes about the macro tag");
        }
    }
}