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
            comboBoxType.Items.Add("Time of Day");
            comboBoxType.Items.Add("Date/Time Format Expression");

            if (TagObject != null)
            {
                Text = "Change Tag";

                textBoxName.Text = TagObject.Name;

                textBoxDescription.Text = TagObject.Description;

                comboBoxType.SelectedIndex = (int)TagObject.Type;
                textBoxDateTimeFormatValue.Text = TagObject.DateTimeFormatValue;

                dateTimePickerMorningStart.Value = TagObject.TimeOfDayMorningStart;
                dateTimePickerMorningEnd.Value = TagObject.TimeOfDayMorningEnd;

                dateTimePickerAfternoonStart.Value = TagObject.TimeOfDayAfternoonStart;
                dateTimePickerAfternoonEnd.Value = TagObject.TimeOfDayAfternoonEnd;

                dateTimePickerEveningStart.Value = TagObject.TimeOfDayEveningStart;
                dateTimePickerEveningEnd.Value = TagObject.TimeOfDayEveningEnd;

                textBoxMorningValue.Text = TagObject.TimeOfDayMorningValue;
                textBoxAfternoonValue.Text = TagObject.TimeOfDayAfternoonValue;
                textBoxEveningValue.Text = TagObject.TimeOfDayEveningValue;

                checkBoxEveningExtendsToNextMorning.Checked = TagObject.EveningExtendsToNextMorning;

                checkBoxActive.Checked = TagObject.Active;

                textBoxNotes.Text = TagObject.Notes;
            }
            else
            {
                Text = "Add New Tag";

                Tag tag = new Tag();

                textBoxName.Text = "%tag" + (TagCollection.Count + 1) + "%";

                textBoxDescription.Text = "Please provide a brief summary for the purpose of this macro tag";

                comboBoxType.SelectedIndex = 0;
                textBoxDateTimeFormatValue.Text = tag.DateTimeFormatValue;

                dateTimePickerMorningStart.Value = tag.TimeOfDayMorningStart;
                dateTimePickerMorningEnd.Value = tag.TimeOfDayMorningEnd;

                dateTimePickerAfternoonStart.Value = tag.TimeOfDayAfternoonStart;
                dateTimePickerAfternoonEnd.Value = tag.TimeOfDayAfternoonEnd;

                dateTimePickerEveningStart.Value = tag.TimeOfDayEveningStart;
                dateTimePickerEveningEnd.Value = tag.TimeOfDayEveningEnd;

                textBoxMorningValue.Text = tag.TimeOfDayMorningValue;
                textBoxAfternoonValue.Text = tag.TimeOfDayAfternoonValue;
                textBoxEveningValue.Text = tag.TimeOfDayEveningValue;

                checkBoxEveningExtendsToNextMorning.Checked = tag.EveningExtendsToNextMorning;

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
                AddNewTag();
            }
        }

        private void AddNewTag()
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
                        dateTimePickerMorningStart.Value,
                        dateTimePickerMorningEnd.Value,
                        textBoxMorningValue.Text,
                        dateTimePickerAfternoonStart.Value,
                        dateTimePickerAfternoonEnd.Value,
                        textBoxAfternoonValue.Text,
                        dateTimePickerEveningStart.Value,
                        dateTimePickerEveningEnd.Value,
                        textBoxEveningValue.Text,
                        checkBoxEveningExtendsToNextMorning.Checked,
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
                        TagCollection.Get(TagObject).TimeOfDayMorningStart = dateTimePickerMorningStart.Value;
                        TagCollection.Get(TagObject).TimeOfDayMorningEnd = dateTimePickerMorningEnd.Value;
                        TagCollection.Get(TagObject).TimeOfDayMorningValue = textBoxMorningValue.Text;
                        TagCollection.Get(TagObject).TimeOfDayAfternoonStart = dateTimePickerAfternoonStart.Value;
                        TagCollection.Get(TagObject).TimeOfDayAfternoonEnd = dateTimePickerAfternoonEnd.Value;
                        TagCollection.Get(TagObject).TimeOfDayAfternoonValue = textBoxAfternoonValue.Text;
                        TagCollection.Get(TagObject).TimeOfDayEveningStart = dateTimePickerEveningStart.Value;
                        TagCollection.Get(TagObject).TimeOfDayEveningEnd = dateTimePickerEveningEnd.Value;
                        TagCollection.Get(TagObject).TimeOfDayEveningValue = textBoxEveningValue.Text;
                        TagCollection.Get(TagObject).EveningExtendsToNextMorning = checkBoxEveningExtendsToNextMorning.Checked;
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
            textBoxMorningValue.Text = textBoxMorningValue.Text.Trim();
            textBoxAfternoonValue.Text = textBoxAfternoonValue.Text.Trim();
            textBoxEveningValue.Text = textBoxEveningValue.Text.Trim();
            textBoxNotes.Text = textBoxNotes.Text.Trim();

            if (!textBoxName.Text.StartsWith("%"))
                textBoxName.Text = "%" + textBoxName.Text;

            if (!textBoxName.Text.EndsWith("%"))
                textBoxName.Text += "%";
        }

        private bool InputValid()
        {
            if (textBoxDateTimeFormatValue.Enabled || groupBoxTimeOfDay.Enabled)
            {
                if (!string.IsNullOrEmpty(textBoxName.Text) &&
                    !string.IsNullOrEmpty(textBoxDescription.Text) &&
                    textBoxDateTimeFormatValue.Enabled &&
                    !string.IsNullOrEmpty(textBoxDateTimeFormatValue.Text))
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(textBoxName.Text) &&
                    !string.IsNullOrEmpty(textBoxDescription.Text) &&
                    groupBoxTimeOfDay.Enabled &&
                    !string.IsNullOrEmpty(textBoxMorningValue.Text) &&
                    !string.IsNullOrEmpty(textBoxAfternoonValue.Text) &&
                    !string.IsNullOrEmpty(textBoxEveningValue.Text))
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

                if (groupBoxTimeOfDay.Enabled &&
                    !TagObject.TimeOfDayMorningStart.Equals(dateTimePickerMorningStart.Value) ||
                    !TagObject.TimeOfDayMorningEnd.Equals(dateTimePickerMorningEnd.Value) ||
                    !TagObject.TimeOfDayMorningValue.Equals(textBoxMorningValue.Text) ||
                    !TagObject.TimeOfDayAfternoonStart.Equals(dateTimePickerAfternoonStart.Value) ||
                    !TagObject.TimeOfDayAfternoonEnd.Equals(dateTimePickerAfternoonEnd.Value) ||
                    !TagObject.TimeOfDayAfternoonValue.Equals(textBoxAfternoonValue.Text) ||
                    !TagObject.TimeOfDayEveningStart.Equals(dateTimePickerEveningStart.Value) ||
                    !TagObject.TimeOfDayEveningEnd.Equals(dateTimePickerEveningEnd.Value) ||
                    !TagObject.TimeOfDayEveningValue.Equals(textBoxEveningValue.Text) ||
                    !TagObject.EveningExtendsToNextMorning.Equals(checkBoxEveningExtendsToNextMorning.Checked))
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

            groupBoxTimeOfDay.Enabled = false;

            labelMorning.Enabled = false;
            dateTimePickerMorningStart.Enabled = false;
            dateTimePickerMorningEnd.Enabled = false;
            textBoxMorningValue.Enabled = false;

            labelAfternoon.Enabled = false;
            dateTimePickerAfternoonStart.Enabled = false;
            dateTimePickerAfternoonEnd.Enabled = false;
            textBoxAfternoonValue.Enabled = false;

            labelEvening.Enabled = false;
            dateTimePickerEveningStart.Enabled = false;
            dateTimePickerEveningEnd.Enabled = false;
            textBoxEveningValue.Enabled = false;

            checkBoxEveningExtendsToNextMorning.Enabled = false;

            TagType tagType = (TagType) comboBoxType.SelectedIndex;

            if (tagType.Equals(TagType.DateTimeFormat) ||
                tagType.Equals(TagType.DateTimeFormatExpression))
            {
                labelDateTimeFormatValue.Enabled = true;
                textBoxDateTimeFormatValue.Enabled = true;
            }

            if (tagType.Equals(TagType.TimeOfDay))
            {
                groupBoxTimeOfDay.Enabled = true;

                labelMorning.Enabled = true;
                dateTimePickerMorningStart.Enabled = true;
                dateTimePickerMorningEnd.Enabled = true;
                textBoxMorningValue.Enabled = true;

                labelAfternoon.Enabled = true;
                dateTimePickerAfternoonStart.Enabled = true;
                dateTimePickerAfternoonEnd.Enabled = true;
                textBoxAfternoonValue.Enabled = true;

                labelEvening.Enabled = true;
                dateTimePickerEveningStart.Enabled = true;
                dateTimePickerEveningEnd.Enabled = true;
                textBoxEveningValue.Enabled = true;

                checkBoxEveningExtendsToNextMorning.Enabled = true;
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

        private void textBoxMorningValue_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The macro to use for the time range that represents the morning");
        }

        private void textBoxAfternoonValue_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The macro to use for the time range that represents the afternoon");
        }

        private void textBoxEveningValue_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The macro to use for the time range that represents the evening");
        }

        private void checkBoxEveningExtendsToNextMorning_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Determines if the macro used for the evening time range should also apply to the early hours of the next morning (such as from 11pm to 3am)");
        }

        private void textBoxNotes_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("An area for you to keep notes about the macro tag");
        }
    }
}