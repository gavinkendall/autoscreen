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
            }
            else
            {
                Text = "Add New Tag";

                textBoxTagName.Text = string.Empty;
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
    }
}