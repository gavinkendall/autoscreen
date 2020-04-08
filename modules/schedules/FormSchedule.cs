//-----------------------------------------------------------------------
// <copyright file="FormSchedule.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormSchedule : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public ScheduleCollection ScheduleCollection { get; } = new ScheduleCollection();

        /// <summary>
        /// 
        /// </summary>
        public Schedule ScheduleObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FormSchedule()
        {
            InitializeComponent();
        }

        private void FormSchedule_Load(object sender, EventArgs e)
        {
            if (ScheduleObject != null)
            {
                Text = "Change Schedule";

                textBoxScheduleName.Text = ScheduleObject.Name;
                checkBoxEnabled.Checked = ScheduleObject.Enabled;
            }
            else
            {
                Text = "Add New Schedule";

                textBoxScheduleName.Text = "Schedule " + (ScheduleCollection.Count + 1);
                checkBoxEnabled.Checked = true;
            }
        }

        private void Click_buttonCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void Click_buttonOK(object sender, EventArgs e)
        {
            if (ScheduleObject != null)
            {
                ChangeSchedule();
            }
            else
            {
                AddNewSchedule();
            }
        }

        private void AddNewSchedule()
        {
            if (InputValid())
            {
                TrimInput();

                if (ScheduleCollection.GetByName(textBoxScheduleName.Text) == null)
                {
                    ScheduleCollection.Add(new Schedule(
                        textBoxScheduleName.Text,
                        checkBoxEnabled.Checked));

                    Okay();
                }
                else
                {
                    MessageBox.Show("A schedule with this name already exists.", "Duplicate Name Conflict",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeSchedule()
        {
            if (InputValid())
            {
                if (NameChanged() || InputChanged())
                {
                    TrimInput();

                    if (ScheduleCollection.GetByName(textBoxScheduleName.Text) != null && NameChanged())
                    {
                        MessageBox.Show("A schedule with this name already exists.", "Duplicate Name Conflict",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ScheduleCollection.Get(ScheduleObject).Name = textBoxScheduleName.Text;
                        ScheduleCollection.Get(ScheduleObject).Enabled = checkBoxEnabled.Checked;

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
            textBoxScheduleName.Text = textBoxScheduleName.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxScheduleName.Text))
            {
                return true;
            }

            return false;
        }

        private bool InputChanged()
        {
            if (ScheduleObject != null &&
                (!ScheduleObject.Enabled.Equals(checkBoxEnabled.Checked)))
            {
                return true;
            }

            return false;
        }

        private bool NameChanged()
        {
            if (ScheduleObject != null &&
                !ScheduleObject.Name.Equals(textBoxScheduleName.Text))
            {
                return true;
            }

            return false;
        }

        private void Okay()
        {
            DialogResult = DialogResult.OK;

            Close();
        }
    }
}