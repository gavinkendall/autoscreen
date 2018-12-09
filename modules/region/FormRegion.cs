//-----------------------------------------------------------------------
// <copyright file="FormRegion.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.Windows.Forms;

    public partial class FormRegion : Form
    {
        public RegionCollection RegionCollection { get; } = new RegionCollection();

        public Region RegionObject { get; set; }

        public FormRegion()
        {
            InitializeComponent();
        }

        private void FormRegion_Load(object sender, EventArgs e)
        {
            if (RegionObject != null)
            {
                Text = "Change Region";

                textBoxRegionName.Text = RegionObject.Name;
                textBoxRegionMacro.Text = RegionObject.Macro;
                numericUpDownRegionX.Value = RegionObject.X;
                numericUpDownRegionY.Value = RegionObject.Y;
                numericUpDownRegionWidth.Value = RegionObject.Width;
                numericUpDownRegionHeight.Value = RegionObject.Height;
            }
            else
            {
                Text = "Add New Region";

                textBoxRegionName.Text = string.Empty;
                textBoxRegionMacro.Text = MacroParser.RegionMacro;
                numericUpDownRegionX.Value = 0;
                numericUpDownRegionY.Value = 0;
                numericUpDownRegionWidth.Value = 800;
                numericUpDownRegionHeight.Value = 600;
            }
        }

        private void Click_buttonCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void Click_buttonOK(object sender, EventArgs e)
        {
            if (RegionObject != null)
            {
                ChangeRegion();
            }
            else
            {
                AddNewRegion();
            }
        }

        private void AddNewRegion()
        {
            if (InputValid())
            {
                TrimInput();

                if (RegionCollection.GetByName(textBoxRegionName.Text) == null)
                {
                    RegionCollection.Add(new Region(textBoxRegionName.Text, (int)numericUpDownRegionX.Value,
                        (int)numericUpDownRegionY.Value, (int)numericUpDownRegionWidth.Value,
                        (int)numericUpDownRegionHeight.Value, textBoxRegionMacro.Text));

                    Okay();
                }
                else
                {
                    MessageBox.Show("A region with this name already exists.", "Duplicate Name Conflict",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ChangeRegion()
        {
            if (InputValid())
            {
                if (NameChanged() || InputChanged())
                {
                    TrimInput();

                    if (RegionCollection.GetByName(textBoxRegionName.Text) != null && NameChanged())
                    {
                        MessageBox.Show("A region with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        RegionCollection.Get(RegionObject).Name = textBoxRegionName.Text;
                        RegionCollection.Get(RegionObject).Macro = textBoxRegionMacro.Text;
                        RegionCollection.Get(RegionObject).X = (int)numericUpDownRegionX.Value;
                        RegionCollection.Get(RegionObject).Y = (int)numericUpDownRegionY.Value;
                        RegionCollection.Get(RegionObject).Width = (int)numericUpDownRegionWidth.Value;
                        RegionCollection.Get(RegionObject).Height = (int)numericUpDownRegionHeight.Value;

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
            textBoxRegionName.Text = textBoxRegionName.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxRegionName.Text))
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
            if (RegionObject != null)
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
            if (RegionObject != null &&
                !RegionObject.Name.Equals(textBoxRegionName.Text))
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

        private void Tick_timerRegionPreview(object sender, EventArgs e)
        {
            pictureBoxRegionPreview.Image = ScreenCapture.GetScreenBitmap(
                (int)numericUpDownRegionX.Value,
                (int)numericUpDownRegionY.Value,
                (int)numericUpDownRegionWidth.Value,
                (int)numericUpDownRegionHeight.Value,
                ScreenCapture.IMAGE_RESOLUTION_RATIO_MAX,
                ScreenCapture.ImageFormat,
                true
            );
        }
    }
}