//-----------------------------------------------------------------------
// <copyright file="FormTrigger.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class FormTrigger : Form
    {
        public Trigger TriggerObject { get; set; }

        public FormTrigger()
        {
            InitializeComponent();
        }

        private void FormTrigger_Load(object sender, EventArgs e)
        {
            textBoxTriggerName.Focus();

            if (TriggerObject != null)
            {
                Text = "Change Trigger";

                textBoxTriggerName.Text = TriggerObject.Name;
            }
            else
            {
                Text = "Add New Trigger";

                textBoxTriggerName.Text = string.Empty;
            }
        }

        private void Click_buttonCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void Click_buttonOK(object sender, EventArgs e)
        {
            if (TriggerObject != null)
            {
                ChangeTrigger();
            }
            else
            {
                AddNewTrigger();
            }
        }

        private void AddNewTrigger()
        {
            if (InputValid())
            {
                TrimInput();

                if (TriggerCollection.GetByName(textBoxTriggerName.Text) == null)
                {
                    TriggerCollection.Add(new Trigger(textBoxTriggerName.Text));

                    Okay();
                }
                else
                {
                    MessageBox.Show("A trigger with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeTrigger()
        {
            if (InputValid())
            {
                if (NameChanged() || InputChanged())
                {
                    TrimInput();

                    if (TriggerCollection.GetByName(textBoxTriggerName.Text) != null && NameChanged())
                    {
                        MessageBox.Show("A trigger with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        TriggerCollection.Get(TriggerObject).Name = textBoxTriggerName.Text;

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
            textBoxTriggerName.Text = textBoxTriggerName.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxTriggerName.Text))
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
            if (TriggerObject != null &&
                (!TriggerObject.Name.Equals(textBoxTriggerName.Text)))
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
            if (TriggerObject != null &&
                !TriggerObject.Name.Equals(textBoxTriggerName.Text))
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
