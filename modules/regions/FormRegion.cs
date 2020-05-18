//-----------------------------------------------------------------------
// <copyright file="FormRegion.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for adding a new region or changing an existing region.</summary>
//-----------------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class for handling regions.
    /// </summary>
    public partial class FormRegion : Form
    {
        private FormMacroTagsToolWindow _formMacroTags;

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
        public TagCollection TagCollection { get; set; }

        /// <summary>
        /// Access to screen capture methods.
        /// </summary>
        public ScreenCapture ScreenCapture { get; set; }

        private readonly Dictionary<int, System.Windows.Forms.Screen> ScreenDictionary = new Dictionary<int, System.Windows.Forms.Screen>();

        /// <summary>
        /// Constructor for FormRegion.
        /// </summary>
        public FormRegion()
        {
            InitializeComponent();
        }

        private void FormRegion_Load(object sender, EventArgs e)
        {
            textBoxName.Focus();

            HelpMessage("This is where to configure a region capture. Change the X, Y, Width, and Height properties while watching Preview");

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
                comboBoxScreenTemplate.Items.Add("Screen " + i + " (" + screen.Bounds.Width + " x " + screen.Bounds.Height + ")");
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

                textBoxName.Text = RegionObject.Name;
                textBoxFolder.Text = FileSystem.CorrectScreenshotsFolderPath(RegionObject.Folder);
                textBoxMacro.Text = RegionObject.Macro;
                comboBoxFormat.SelectedItem = RegionObject.Format.Name;
                numericUpDownJpegQuality.Value = RegionObject.JpegQuality;
                numericUpDownResolutionRatio.Value = RegionObject.ResolutionRatio;
                checkBoxMouse.Checked = RegionObject.Mouse;
                numericUpDownX.Value = RegionObject.X;
                numericUpDownY.Value = RegionObject.Y;
                numericUpDownWidth.Value = RegionObject.Width;
                numericUpDownHeight.Value = RegionObject.Height;
                checkBoxActive.Checked = RegionObject.Active;
                checkBoxActiveWindowTitle.Checked = RegionObject.ActiveWindowTitleCaptureCheck;
                textBoxActiveWindowTitle.Text = RegionObject.ActiveWindowTitleCaptureText;
            }
            else
            {
                Text = "Add New Region";

                textBoxName.Text = "Region " + (RegionCollection.Count + 1);
                textBoxFolder.Text = FileSystem.ScreenshotsFolder;
                textBoxMacro.Text = MacroParser.DefaultMacro;
                comboBoxFormat.SelectedItem = ScreenCapture.DefaultImageFormat;
                numericUpDownJpegQuality.Value = 100;
                numericUpDownResolutionRatio.Value = 100;
                checkBoxMouse.Checked = true;
                numericUpDownX.Value = 0;
                numericUpDownY.Value = 0;
                numericUpDownWidth.Value = 800;
                numericUpDownHeight.Value = 600;
                checkBoxActive.Checked = true;
                checkBoxActiveWindowTitle.Checked = false;
                textBoxActiveWindowTitle.Text = string.Empty;
            }

            UpdatePreviewMacro();
            UpdatePreviewImage(ScreenCapture);
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
                AddNewRegion();
            }
        }

        private void AddNewRegion()
        {
            if (InputValid())
            {
                TrimInput();

                if (RegionCollection.GetByName(textBoxName.Text) == null)
                {
                    RegionCollection.Add(new Region()
                    {
                        Name = textBoxName.Text,
                        Folder = FileSystem.CorrectScreenshotsFolderPath(textBoxFolder.Text),
                        Macro = textBoxMacro.Text,
                        Format = ImageFormatCollection.GetByName(comboBoxFormat.Text),
                        JpegQuality = (int)numericUpDownJpegQuality.Value,
                        ResolutionRatio = (int)numericUpDownResolutionRatio.Value,
                        Mouse = checkBoxMouse.Checked,
                        X = (int)numericUpDownX.Value,
                        Y = (int)numericUpDownY.Value,
                        Width = (int)numericUpDownWidth.Value,
                        Height = (int)numericUpDownHeight.Value,
                        Active = checkBoxActive.Checked,
                        ActiveWindowTitleCaptureCheck = checkBoxActiveWindowTitle.Checked,
                        ActiveWindowTitleCaptureText = textBoxActiveWindowTitle.Text
                    });

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

                    if (RegionCollection.GetByName(textBoxName.Text) != null && NameChanged())
                    {
                        MessageBox.Show("A region with this name already exists.", "Duplicate Name Conflict",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        RegionCollection.Get(RegionObject).Name = textBoxName.Text;
                        RegionCollection.Get(RegionObject).Folder = FileSystem.CorrectScreenshotsFolderPath(textBoxFolder.Text);
                        RegionCollection.Get(RegionObject).Macro = textBoxMacro.Text;
                        RegionCollection.Get(RegionObject).Format = ImageFormatCollection.GetByName(comboBoxFormat.Text);
                        RegionCollection.Get(RegionObject).JpegQuality = (int) numericUpDownJpegQuality.Value;
                        RegionCollection.Get(RegionObject).ResolutionRatio = (int) numericUpDownResolutionRatio.Value;
                        RegionCollection.Get(RegionObject).Mouse = checkBoxMouse.Checked;
                        RegionCollection.Get(RegionObject).X = (int) numericUpDownX.Value;
                        RegionCollection.Get(RegionObject).Y = (int) numericUpDownY.Value;
                        RegionCollection.Get(RegionObject).Width = (int) numericUpDownWidth.Value;
                        RegionCollection.Get(RegionObject).Height = (int) numericUpDownHeight.Value;
                        RegionCollection.Get(RegionObject).Active = checkBoxActive.Checked;
                        RegionCollection.Get(RegionObject).ActiveWindowTitleCaptureCheck = checkBoxActiveWindowTitle.Checked;
                        RegionCollection.Get(RegionObject).ActiveWindowTitleCaptureText = textBoxActiveWindowTitle.Text;

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
            textBoxFolder.Text = textBoxFolder.Text.Trim();
            textBoxMacro.Text = textBoxMacro.Text.Trim();
            textBoxActiveWindowTitle.Text = textBoxActiveWindowTitle.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxName.Text) &&
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
                 RegionObject.ResolutionRatio != (int)numericUpDownResolutionRatio.Value ||
                 !RegionObject.Mouse.Equals(checkBoxMouse.Checked) ||
                 RegionObject.X != (int)numericUpDownX.Value ||
                 RegionObject.Y != (int)numericUpDownY.Value ||
                 RegionObject.Width != (int)numericUpDownWidth.Value ||
                 RegionObject.Height != (int)numericUpDownHeight.Value ||
                 RegionObject.Active.Equals(checkBoxActive.Checked)))
            {
                return true;
            }

            return false;
        }

        private bool NameChanged()
        {
            if (RegionObject != null &&
                !RegionObject.Name.Equals(textBoxName.Text))
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
                if (checkBoxActive.Checked)
                {
                    pictureBoxPreview.Image = screenCapture.GetScreenBitmap(
                        (int)numericUpDownX.Value,
                        (int)numericUpDownY.Value,
                        (int)numericUpDownWidth.Value,
                        (int)numericUpDownHeight.Value,
                        (int)numericUpDownResolutionRatio.Value,
                        checkBoxMouse.Checked
                    );

                    UpdatePreviewMacro();
                }
                else
                {
                    pictureBoxPreview.Image = null;

                    textBoxMacroPreview.ForeColor = System.Drawing.Color.White;
                    textBoxMacroPreview.BackColor = System.Drawing.Color.Black;
                    textBoxMacroPreview.Text = "[Active option is off. No screenshots of this region will be taken during a running screen capture session]";
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("FormRegion::UpdatePreview", ex);
            }
        }

        private void UpdatePreviewMacro()
        {
            textBoxMacroPreview.ForeColor = System.Drawing.Color.Black;
            textBoxMacroPreview.BackColor = System.Drawing.Color.LightYellow;

            textBoxMacroPreview.Text = MacroParser.ParseTags(textBoxFolder.Text, TagCollection) +
                MacroParser.ParseTags(preview: true, textBoxName.Text, textBoxMacro.Text, 1,
                ImageFormatCollection.GetByName(comboBoxFormat.Text), Text, TagCollection);
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
            if (comboBoxFormat.Text.Equals(ImageFormatSpec.NAME_JPEG))
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
            UpdatePreviewImage(ScreenCapture);
        }

        private void updatePreviewMacro(object sender, EventArgs e)
        {
            UpdatePreviewMacro();
        }

        private void buttonMacroTags_Click(object sender, EventArgs e)
        {
            if (_formMacroTags == null || _formMacroTags.IsDisposed)
            {
                _formMacroTags = new FormMacroTagsToolWindow(TagCollection);
                _formMacroTags.Show();
            }
            else
            {
                _formMacroTags.BringToFront();
            }
        }

        private void checkBoxMouse_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("You can include the mouse pointer in your screenshots if the \"Include mouse pointer\" option is checked");
        }

        private void comboBoxFormat_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Change the image format for the screenshots taken by this region capture. JPEG is the recommended image format");
        }

        private void comboBoxScreenTemplate_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Acquire the width and height from an available screen to import as the width and height for your region capture");
        }

        private void checkBoxActive_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("You can capture this region if Active is checked (turned on)");
        }

        private void textBoxFolder_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The folder where to store the files of the screenshots being taken");
        }

        private void buttonBrowseFolder_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Browse for a folder where screenshots of this region capture will be saved to");
        }

        private void textBoxMacro_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Macro tags are used for acquiring information associated with a particular tag (such as %date% and %time% for the current date and time)");
        }

        private void buttonMacroTags_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Open a list of available macro tags. You can keep the Macro Tags window open while you modify your macro");
        }

        private void pictureBoxPreview_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("A preview of what will be captured during a running screen capture session. Click to update the preview image");
        }

        private void textBoxMacroPreview_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("A preview of how your files will be named. Use macro tags (such as %date% and %time%) in the Macro field to customize the filename pattern");
        }

        private void checkBoxActiveWindowTitle_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("If checked then the text you define will be compared with the active window title");
        }

        private void textBoxActiveWindowTitle_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The text to compare with the active window title. If it contains the defined text then this region will be captured. An empty field will be ignored");
        }

        private void FormRegion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_formMacroTags != null)
            {
                _formMacroTags.Close();
            }
        }

        private void checkBoxActiveWindowTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxActiveWindowTitle.Checked)
            {
                textBoxActiveWindowTitle.Enabled = true;
            }
            else
            {
                textBoxActiveWindowTitle.Enabled = false;
            }
        }
    }
}