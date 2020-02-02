//-----------------------------------------------------------------------
// <copyright file="FormTag.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public partial class FormTag : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public TagCollection TagCollection { get; } = new TagCollection();

        /// <summary>
        /// 
        /// </summary>
        public Tag TagObject { get; set; }

        /// <summary>
        /// 
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
            }
            else
            {
                Text = "Add New Tag";

                textBoxTagName.Text = string.Empty;
                comboBoxType.SelectedIndex = 0;
                textBoxDateTimeFormatValue.Text = string.Empty;

                Tag tag = new Tag();

                dateTimePickerMorningStart.Value = tag.TimeOfDayMorningStart;
                dateTimePickerMorningEnd.Value = tag.TimeOfDayMorningEnd;

                dateTimePickerAfternoonStart.Value = tag.TimeOfDayAfternoonStart;
                dateTimePickerAfternoonEnd.Value = tag.TimeOfDayAfternoonEnd;

                dateTimePickerEveningStart.Value = tag.TimeOfDayEveningStart;
                dateTimePickerEveningEnd.Value = tag.TimeOfDayEveningEnd;

                textBoxMorningValue.Text = tag.TimeOfDayMorningValue;
                textBoxAfternoonValue.Text = tag.TimeOfDayAfternoonValue;
                textBoxEveningValue.Text = tag.TimeOfDayEveningValue;
            }
        }

        private void Click_buttonCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void Click_buttonOK(object sender, EventArgs e)
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
                        (TagType)comboBoxType.SelectedIndex));

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
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxTagName.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InputChanged()
        {
            if (TagObject != null && ((int)TagObject.Type != comboBoxType.SelectedIndex))
            {
                return true;
            }
            else
            {
                return false;
            }
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

            TagType tagType = (TagType) comboBoxType.SelectedIndex;

            if (tagType.Equals(TagType.DateTimeFormat))
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
            }
        }
    }
}