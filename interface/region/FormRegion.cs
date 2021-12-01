//-----------------------------------------------------------------------
// <copyright file="FormRegion.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for adding a new region or changing an existing region.</summary>
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
using System.Collections.Generic;
using System.Reflection;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class for handling regions.
    /// </summary>
    public partial class FormRegion : Form
    {
        private Log _log;
        private FileSystem _fileSystem;
        private MacroParser _macroParser;
        private ScreenCapture _screenCapture;

        private ToolTip _toolTip = new ToolTip();
        private FormMacroTagsToolWindow _formMacroTags;
        private FormRegionSelectWithMouse _formRegionSelectWithMouse;

        /// <summary>
        /// A collection of regions.
        /// </summary>
        public RegionCollection RegionCollection { get; } = new RegionCollection();

        /// <summary>
        /// The current region object this form handles when creating a new region or changing a region.
        /// </summary>
        public Region RegionObject { get; set; }

        /// <summary>
        /// A collection of image formats.
        /// </summary>
        public ImageFormatCollection ImageFormatCollection { get; set; }

        /// <summary>
        /// A collection of tags to be used for macro parsing.
        /// </summary>
        public MacroTagCollection TagCollection { get; set; }

        private readonly Dictionary<int, System.Windows.Forms.Screen> ScreenDictionary;

        /// <summary>
        /// Constructor for FormRegion.
        /// </summary>
        public FormRegion(ScreenCapture screenCapture, MacroParser macroParser, FileSystem fileSystem, Log log)
        {
            InitializeComponent();

            _log = log;
            _macroParser = macroParser;
            _screenCapture = screenCapture;
            _fileSystem = fileSystem;

            ScreenDictionary = new Dictionary<int, System.Windows.Forms.Screen>();
        }

        private void FormRegion_Load(object sender, EventArgs e)
        {
            textBoxRegionName.Focus();

            HelpMessage("This is where to configure a region capture. Change the X, Y, Width, and Height properties while watching Preview");

            _toolTip.SetToolTip(checkBoxMouse, "You can include the mouse pointer in your screenshots if the \"Include mouse pointer\" option is checked");
            _toolTip.SetToolTip(comboBoxFormat, "Change the image format for the screenshots taken by this region capture. JPEG is the recommended image format");
            _toolTip.SetToolTip(comboBoxScreenTemplate, "Acquire the width and height from an available screen to import as the width and height for your region capture");
            _toolTip.SetToolTip(checkBoxEnable, "You can capture this region if Enable is checked (turned on)");
            _toolTip.SetToolTip(buttonBrowseFolder, "Browse for a folder where screenshots of this region capture will be saved to");
            _toolTip.SetToolTip(buttonMacroTags, "Open a list of available macro tags. You can keep the Macro Tags window open while you modify your macro");

            ScreenDictionary.Clear();
            comboBoxScreenTemplate.Items.Clear();

            int component = 1;

            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                ScreenDictionary.Add(component, screen);
                component++;
            }

            // *** Screen Template ***
            comboBoxScreenTemplate.Items.Add(string.Empty);

            for (int i = 1; i <= ScreenDictionary.Count; i++)
            {
                System.Windows.Forms.Screen screen = ScreenDictionary[i];

                comboBoxScreenTemplate.Items.Add("\"" + screen.DeviceName + "\" X:" + screen.Bounds.X + " Y:" + screen.Bounds.Y + " (" + screen.Bounds.Width + "x" + screen.Bounds.Height + ")");
            }

            comboBoxScreenTemplate.SelectedIndex = 0;
            // ***********************

            comboBoxFormat.Items.Clear();

            foreach (ImageFormat imageFormat in ImageFormatCollection)
            {
                comboBoxFormat.Items.Add(imageFormat.Name);
            }

            if (RegionObject != null)
            {
                Text = "Change Region";

                textBoxRegionName.Text = RegionObject.Name;
                textBoxFolder.Text = _fileSystem.CorrectScreenshotsFolderPath(RegionObject.Folder);
                textBoxMacro.Text = RegionObject.Macro;
                comboBoxFormat.SelectedItem = RegionObject.Format.Name;
                numericUpDownJpegQuality.Value = RegionObject.JpegQuality;
                checkBoxMouse.Checked = RegionObject.Mouse;
                numericUpDownX.Value = RegionObject.X;
                numericUpDownY.Value = RegionObject.Y;
                numericUpDownWidth.Value = RegionObject.Width;
                numericUpDownHeight.Value = RegionObject.Height;
                checkBoxEnable.Checked = RegionObject.Enable;
                checkBoxEncrypt.Checked = RegionObject.Encrypt;
            }
            else
            {
                Text = "Add Region";

                textBoxRegionName.Text = "Region " + (RegionCollection.Count + 1);
                textBoxFolder.Text = _fileSystem.ScreenshotsFolder;
                textBoxMacro.Text = _macroParser.DefaultMacro;
                comboBoxFormat.SelectedItem = ScreenCapture.DefaultImageFormat;
                numericUpDownJpegQuality.Value = 100;
                checkBoxMouse.Checked = true;
                numericUpDownX.Value = 0;
                numericUpDownY.Value = 0;
                numericUpDownWidth.Value = 800;
                numericUpDownHeight.Value = 600;
                checkBoxEnable.Checked = true;
                checkBoxEncrypt.Checked = false;
            }

            UpdatePreviewMacro();
            UpdatePreviewImage(_screenCapture);
        }

        private void HelpMessage(string message)
        {
            labelHelp.Text = "       " + message;
        }

        private void buttonRegionCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonRegionOK_Click(object sender, EventArgs e)
        {
            if (RegionObject != null)
            {
                ChangeRegion();
            }
            else
            {
                AddRegion();
            }
        }

        private void AddRegion()
        {
            if (InputValid())
            {
                TrimInput();

                if (RegionCollection.GetByName(textBoxRegionName.Text) == null)
                {
                    RegionCollection.Add(new Region()
                    {
                        ViewId = Guid.NewGuid(),
                        Name = textBoxRegionName.Text,
                        Folder = _fileSystem.CorrectScreenshotsFolderPath(textBoxFolder.Text),
                        Macro = textBoxMacro.Text,
                        Format = ImageFormatCollection.GetByName(comboBoxFormat.Text),
                        JpegQuality = (int)numericUpDownJpegQuality.Value,
                        Mouse = checkBoxMouse.Checked,
                        X = (int)numericUpDownX.Value,
                        Y = (int)numericUpDownY.Value,
                        Width = (int)numericUpDownWidth.Value,
                        Height = (int)numericUpDownHeight.Value,
                        Enable = checkBoxEnable.Checked,
                        Encrypt = checkBoxEncrypt.Checked
                    });

                    Okay();
                }
                else
                {
                    MessageBox.Show("A region with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("A region with this name already exists.", "Duplicate Name Conflict",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        RegionCollection.Get(RegionObject).Name = textBoxRegionName.Text;
                        RegionCollection.Get(RegionObject).Folder = _fileSystem.CorrectScreenshotsFolderPath(textBoxFolder.Text);
                        RegionCollection.Get(RegionObject).Macro = textBoxMacro.Text;
                        RegionCollection.Get(RegionObject).Format = ImageFormatCollection.GetByName(comboBoxFormat.Text);
                        RegionCollection.Get(RegionObject).JpegQuality = (int) numericUpDownJpegQuality.Value;
                        RegionCollection.Get(RegionObject).Mouse = checkBoxMouse.Checked;
                        RegionCollection.Get(RegionObject).X = (int) numericUpDownX.Value;
                        RegionCollection.Get(RegionObject).Y = (int) numericUpDownY.Value;
                        RegionCollection.Get(RegionObject).Width = (int) numericUpDownWidth.Value;
                        RegionCollection.Get(RegionObject).Height = (int) numericUpDownHeight.Value;
                        RegionCollection.Get(RegionObject).Enable = checkBoxEnable.Checked;
                        RegionCollection.Get(RegionObject).Encrypt = checkBoxEncrypt.Checked;

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
            textBoxFolder.Text = textBoxFolder.Text.Trim();
            textBoxMacro.Text = textBoxMacro.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxRegionName.Text) &&
                !string.IsNullOrEmpty(textBoxFolder.Text) &&
                !string.IsNullOrEmpty(textBoxMacro.Text))
            {
                return true;
            }

            return false;
        }

        private bool InputChanged()
        {
            if (RegionObject != null &&
                (!RegionObject.Folder.Equals(textBoxFolder.Text) ||
                 !RegionObject.Macro.Equals(textBoxMacro.Text) ||
                 !RegionObject.Format.Equals(comboBoxFormat.SelectedItem) ||
                 RegionObject.JpegQuality != (int)numericUpDownJpegQuality.Value ||
                 !RegionObject.Mouse.Equals(checkBoxMouse.Checked) ||
                 RegionObject.X != (int)numericUpDownX.Value ||
                 RegionObject.Y != (int)numericUpDownY.Value ||
                 RegionObject.Width != (int)numericUpDownWidth.Value ||
                 RegionObject.Height != (int)numericUpDownHeight.Value ||
                 RegionObject.Enable.Equals(checkBoxEnable.Checked) ||
                 !RegionObject.Encrypt.Equals(checkBoxEncrypt.Checked)))
            {
                return true;
            }

            return false;
        }

        private bool NameChanged()
        {
            if (RegionObject != null &&
                !RegionObject.Name.Equals(textBoxRegionName.Text))
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

        private void buttonRegionBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();

            if (browser.ShowDialog() == DialogResult.OK)
            {
                textBoxFolder.Text = browser.SelectedPath;
            }
        }

        private void UpdatePreviewImage(ScreenCapture screenCapture)
        {
            try
            {
                if (checkBoxEnable.Checked)
                {
                    pictureBoxPreview.Image = screenCapture.GetScreenBitmap(
                        -1,
                        -1,
                        0,
                        (int)numericUpDownX.Value,
                        (int)numericUpDownY.Value,
                        (int)numericUpDownWidth.Value,
                        (int)numericUpDownHeight.Value,
                        checkBoxMouse.Checked
                    );

                    UpdatePreviewMacro();
                }
                else
                {
                    pictureBoxPreview.Image = null;

                    textBoxMacroPreview.ForeColor = System.Drawing.Color.White;
                    textBoxMacroPreview.BackColor = System.Drawing.Color.Black;
                    textBoxMacroPreview.Text = "[Enable option is off. No screenshots of this region will be taken during a running screen capture session]";
                }
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("FormRegion::UpdatePreviewImage", ex);
            }
        }

        private void UpdatePreviewMacro()
        {
            textBoxMacroPreview.ForeColor = System.Drawing.Color.Black;
            textBoxMacroPreview.BackColor = System.Drawing.Color.LightYellow;

            Region region = new Region
            {
                Name = textBoxRegionName.Text,
                X = (int)numericUpDownX.Value,
                Y = (int)numericUpDownY.Value,
                Width = (int)numericUpDownWidth.Value,
                Height = (int)numericUpDownHeight.Value,
                Format = ImageFormatCollection.GetByName(comboBoxFormat.Text)
            };

            textBoxMacroPreview.Text = _macroParser.ParseTags(preview: true, textBoxFolder.Text, region, Text, Assembly.GetExecutingAssembly().GetName().Name, TagCollection, _log) +
                _macroParser.ParseTags(preview: true, textBoxMacro.Text, region, Text, Assembly.GetExecutingAssembly().GetName().Name, TagCollection, _log);
        }

        private void comboBoxRegionScreenTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ScreenDictionary.ContainsKey(comboBoxScreenTemplate.SelectedIndex))
            {
                System.Windows.Forms.Screen screen = ScreenDictionary[comboBoxScreenTemplate.SelectedIndex];

                numericUpDownX.Value = screen.Bounds.X;
                numericUpDownY.Value = screen.Bounds.Y;
                numericUpDownWidth.Value = screen.Bounds.Width;
                numericUpDownHeight.Value = screen.Bounds.Height;

                comboBoxScreenTemplate.SelectedIndex = 0;
            }
        }

        private void comboBoxRegionFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFormat.Text.Equals("JPEG"))
            {
                numericUpDownJpegQuality.Enabled = true;
            }
            else
            {
                numericUpDownJpegQuality.Enabled = false;
            }

            UpdatePreviewMacro();
        }

        private void updatePreviewImage(object sender, EventArgs e)
        {
            UpdatePreviewImage(_screenCapture);
        }

        private void updatePreviewMacro(object sender, EventArgs e)
        {
            UpdatePreviewMacro();
        }

        private void buttonMacroTags_Click(object sender, EventArgs e)
        {
            if (_formMacroTags == null || _formMacroTags.IsDisposed)
            {
                _formMacroTags = new FormMacroTagsToolWindow(TagCollection, _macroParser, _log);
                _formMacroTags.Show();
            }
            else
            {
                _formMacroTags.BringToFront();
            }
        }

        private void FormRegion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_formMacroTags != null)
            {
                _formMacroTags.Close();
            }
        }

        private void buttonRegionSelect_Click(object sender, EventArgs e)
        {
            _formRegionSelectWithMouse = new FormRegionSelectWithMouse();
            _formRegionSelectWithMouse.MouseSelectionCompleted += _formRegionSelectWithMouse_RegionSelectMouseSelectionCompleted;
            _formRegionSelectWithMouse.LoadCanvas();

            Cursor = Cursors.Arrow;
        }

        private void _formRegionSelectWithMouse_RegionSelectMouseSelectionCompleted(object sender, EventArgs e)
        {
            int x = _formRegionSelectWithMouse.outputX + 1;
            int y = _formRegionSelectWithMouse.outputY + 1;
            int width = _formRegionSelectWithMouse.outputWidth - 2;
            int height = _formRegionSelectWithMouse.outputHeight - 2;

            numericUpDownX.Value = x;
            numericUpDownY.Value = y;
            numericUpDownWidth.Value = width;
            numericUpDownHeight.Value = height;
        }
    }
}