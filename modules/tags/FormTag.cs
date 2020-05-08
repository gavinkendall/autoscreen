//-----------------------------------------------------------------------
// <copyright file="FormTag.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for adding a new macro tag or changing an existing macro tag.</summary>
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

                textBoxTagName.Text = TagObject.Name;
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

                checkBoxEnabled.Checked = TagObject.Enabled;
            }
            else
            {
                Text = "Add New Tag";

                Tag tag = new Tag();

                textBoxTagName.Text = "%tag" + (TagCollection.Count + 1) + "%";
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

                checkBoxEnabled.Checked = true;
            }
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

                if (TagCollection.GetByName(textBoxTagName.Text) == null)
                {
                    TagCollection.Add(new Tag(textBoxTagName.Text,
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
                        checkBoxEnabled.Checked
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

                    if (TagCollection.GetByName(textBoxTagName.Text) != null && NameChanged())
                    {
                        MessageBox.Show("A tag with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        TagCollection.Get(TagObject).Name = textBoxTagName.Text;
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
                        TagCollection.Get(TagObject).Enabled = checkBoxEnabled.Checked;

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
            textBoxTagName.Text = textBoxTagName.Text.Trim();
            textBoxDateTimeFormatValue.Text = textBoxDateTimeFormatValue.Text.Trim();
            textBoxMorningValue.Text = textBoxMorningValue.Text.Trim();
            textBoxAfternoonValue.Text = textBoxAfternoonValue.Text.Trim();
            textBoxEveningValue.Text = textBoxEveningValue.Text.Trim();

            if (!textBoxTagName.Text.StartsWith("%"))
                textBoxTagName.Text = "%" + textBoxTagName.Text;

            if (!textBoxTagName.Text.EndsWith("%"))
                textBoxTagName.Text += "%";
        }

        private bool InputValid()
        {
            if (textBoxDateTimeFormatValue.Enabled || groupBoxTimeOfDay.Enabled)
            {
                if (!string.IsNullOrEmpty(textBoxTagName.Text) &&
                    textBoxDateTimeFormatValue.Enabled &&
                    !string.IsNullOrEmpty(textBoxDateTimeFormatValue.Text))
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(textBoxTagName.Text) &&
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
                if (!string.IsNullOrEmpty(textBoxTagName.Text))
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
                if (TagObject.Enabled != checkBoxEnabled.Checked)
                {
                    changed = true;
                }

                if (((int)TagObject.Type != comboBoxType.SelectedIndex))
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
                !TagObject.Name.Equals(textBoxTagName.Text))
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
    }
}