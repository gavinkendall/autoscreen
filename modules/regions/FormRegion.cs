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

        public ImageFormatCollection ImageFormatCollection { get; set; }

        public FormRegion()
        {
            InitializeComponent();
        }

        private void FormRegion_Load(object sender, EventArgs e)
        {
            comboBoxRegionFormat.Items.Clear();

            foreach (ImageFormat imageFormat in ImageFormatCollection)
            {
                comboBoxRegionFormat.Items.Add(imageFormat.Name);
            }

            if (RegionObject != null)
            {
                Text = "Change Region";

                textBoxRegionName.Text = RegionObject.Name;
                textBoxRegionFolder.Text = RegionObject.Folder;
                textBoxRegionMacro.Text = RegionObject.Macro;
                comboBoxRegionFormat.SelectedItem = RegionObject.Format.Name;
                numericUpDownRegionJpegQuality.Value = RegionObject.JpegQuality;
                numericUpDownRegionResolutionRatio.Value = RegionObject.ResolutionRatio;
                checkBoxRegionMouse.Checked = RegionObject.Mouse;
                numericUpDownRegionX.Value = RegionObject.X;
                numericUpDownRegionY.Value = RegionObject.Y;
                numericUpDownRegionWidth.Value = RegionObject.Width;
                numericUpDownRegionHeight.Value = RegionObject.Height;
            }
            else
            {
                Text = "Add New Region";

                textBoxRegionName.Text = string.Empty;
                textBoxRegionFolder.Text = FileSystem.ScreenshotsFolder;
                textBoxRegionMacro.Text = MacroParser.RegionMacro;
                comboBoxRegionFormat.SelectedItem = ScreenCapture.DefaultImageFormat;
                numericUpDownRegionJpegQuality.Value = 100;
                numericUpDownRegionResolutionRatio.Value = 100;
                checkBoxRegionMouse.Checked = true;
                numericUpDownRegionX.Value = 0;
                numericUpDownRegionY.Value = 0;
                numericUpDownRegionWidth.Value = 800;
                numericUpDownRegionHeight.Value = 600;
            }

            timerRegionPreview.Enabled = true;
        }

        private void Click_buttonRegionCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void Click_buttonRegionOK(object sender, EventArgs e)
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
                    RegionCollection.Add(new Region(
                        textBoxRegionName.Text,
                        textBoxRegionFolder.Text,
                        textBoxRegionMacro.Text,
                        ImageFormatCollection.GetByName(comboBoxRegionFormat.Text),
                        (int)numericUpDownRegionJpegQuality.Value,
                        (int)numericUpDownRegionResolutionRatio.Value,
                        checkBoxRegionMouse.Checked,
                        (int)numericUpDownRegionX.Value,
                        (int)numericUpDownRegionY.Value,
                        (int)numericUpDownRegionWidth.Value,
                        (int)numericUpDownRegionHeight.Value));

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
                        RegionCollection.Get(RegionObject).Folder = textBoxRegionFolder.Text;
                        RegionCollection.Get(RegionObject).Macro = textBoxRegionMacro.Text;
                        RegionCollection.Get(RegionObject).Format = ImageFormatCollection.GetByName(comboBoxRegionFormat.Text);
                        RegionCollection.Get(RegionObject).JpegQuality = (int)numericUpDownRegionJpegQuality.Value;
                        RegionCollection.Get(RegionObject).ResolutionRatio = (int) numericUpDownRegionResolutionRatio.Value;
                        RegionCollection.Get(RegionObject).Mouse = checkBoxRegionMouse.Checked;
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
            textBoxRegionFolder.Text = textBoxRegionFolder.Text.Trim();
            textBoxRegionMacro.Text = textBoxRegionMacro.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxRegionName.Text) &&
                !string.IsNullOrEmpty(textBoxRegionFolder.Text) &&
                !string.IsNullOrEmpty(textBoxRegionMacro.Text))
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
            if (RegionObject != null &&
                (!RegionObject.Folder.Equals(textBoxRegionFolder.Text) ||
                 !RegionObject.Macro.Equals(textBoxRegionMacro.Text) ||
                 !RegionObject.Format.Equals(comboBoxRegionFormat.SelectedItem) ||
                 RegionObject.JpegQuality != (int)numericUpDownRegionJpegQuality.Value ||
                 RegionObject.ResolutionRatio != (int)numericUpDownRegionResolutionRatio.Value ||
                 !RegionObject.Mouse.Equals(checkBoxRegionMouse.Checked) ||
                 RegionObject.X != (int)numericUpDownRegionX.Value ||
                 RegionObject.Y != (int)numericUpDownRegionY.Value ||
                 RegionObject.Width != (int)numericUpDownRegionWidth.Value ||
                 RegionObject.Height != (int)numericUpDownRegionHeight.Value))
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
            UpdatePreview();
        }

        private void buttonRegionBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();

            if (browser.ShowDialog() == DialogResult.OK)
            {
                textBoxRegionFolder.Text = browser.SelectedPath;
            }
        }

        private void UpdatePreview()
        {
            pictureBoxRegionPreview.Image = ScreenCapture.GetScreenBitmap(
                (int)numericUpDownRegionX.Value,
                (int)numericUpDownRegionY.Value,
                (int)numericUpDownRegionWidth.Value,
                (int)numericUpDownRegionHeight.Value,
                (int)numericUpDownRegionResolutionRatio.Value,
                checkBoxRegionMouse.Checked
            );
        }

        private void FormRegion_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerRegionPreview.Enabled = false;
        }
    }
}