//-----------------------------------------------------------------------
// <copyright file="FormScreen.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for adding a new screen or changing an existing screen.</summary>
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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class for handling screens.
    /// </summary>
    public partial class FormScreen : Form
    {
        private FormMacroTagsToolWindow _formMacroTags;

        /// <summary>
        /// A collection of screens.
        /// </summary>
        public ScreenCollection ScreenCollection { get; } = new ScreenCollection();

        /// <summary>
        /// A collection of regions to be used for selecting a source.
        /// </summary>
        public RegionCollection RegionCollection { get; set; } = new RegionCollection();

        /// <summary>
        /// The current screen object this form handles when creating a new screen or changing a screen.
        /// </summary>
        public Screen ScreenObject { get; set; }

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

        /// <summary>
        /// Constructor for FormScreen.
        /// </summary>
        public FormScreen()
        {
            InitializeComponent();
        }

        private void FormScreen_Load(object sender, EventArgs e)
        {
            textBoxScreenName.Focus();

            HelpMessage("This is where to configure a screen capture. Select an available screen from the Component drop-down menu and keep an eye on Preview");

            comboBoxFormat.Items.Clear();

            pictureBoxPreview.Image = null;

            foreach (ImageFormat imageFormat in ImageFormatCollection)
            {
                comboBoxFormat.Items.Add(imageFormat.Name);
            }

            comboBoxScreenSource.Items.Clear();
            comboBoxScreenSource.Items.Add("Auto Screen Capture");
            comboBoxScreenSource.Items.Add("Graphics Card");
            comboBoxScreenSource.Items.Add("Operating System");

            if (ScreenObject != null)
            {
                Text = "Change Screen";

                textBoxScreenName.Text = ScreenObject.Name;
                textBoxFolder.Text = FileSystem.CorrectScreenshotsFolderPath(ScreenObject.Folder);
                textBoxMacro.Text = ScreenObject.Macro;
                comboBoxScreenSource.SelectedIndex = ScreenObject.Source;
                comboBoxFormat.SelectedItem = ScreenObject.Format.Name;
                numericUpDownJpegQuality.Value = ScreenObject.JpegQuality;
                numericUpDownResolutionRatio.Value = ScreenObject.ResolutionRatio;
                checkBoxMouse.Checked = ScreenObject.Mouse;
                checkBoxActive.Checked = ScreenObject.Active;
                numericUpDownX.Value = ScreenObject.X;
                numericUpDownY.Value = ScreenObject.Y;
                numericUpDownWidth.Value = ScreenObject.Width;
                numericUpDownHeight.Value = ScreenObject.Height;
            }
            else
            {
                Text = "Add New Screen";

                textBoxScreenName.Text = "Screen " + (ScreenCollection.Count + 1);
                textBoxFolder.Text = FileSystem.ScreenshotsFolder;
                textBoxMacro.Text = MacroParser.DefaultMacro;
                comboBoxScreenSource.SelectedIndex = 1;
                comboBoxFormat.SelectedItem = ScreenCapture.DefaultImageFormat;
                numericUpDownJpegQuality.Value = 100;
                numericUpDownResolutionRatio.Value = 100;
                checkBoxMouse.Checked = true;
                checkBoxActive.Checked = true;
                numericUpDownX.Value = 0;
                numericUpDownY.Value = 0;
                numericUpDownWidth.Value = 0;
                numericUpDownHeight.Value = 0;
            }

            UpdatePreviewMacro();
            UpdatePreviewImage(ScreenCapture);
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
            if (ScreenObject != null)
            {
                ChangeScreen();
            }
            else
            {
                AddNewScreen();
            }
        }

        private void AddNewScreen()
        {
            if (InputValid())
            {
                TrimInput();

                if (ScreenCollection.GetByName(textBoxScreenName.Text) == null)
                {
                    ScreenCollection.Add(new Screen()
                    {
                        ViewId = Guid.NewGuid(),
                        Name = textBoxScreenName.Text,
                        Folder = FileSystem.CorrectScreenshotsFolderPath(textBoxFolder.Text),
                        Macro = textBoxMacro.Text,
                        Component = comboBoxScreenComponent.SelectedIndex,
                        Format = ImageFormatCollection.GetByName(comboBoxFormat.Text),
                        JpegQuality = (int)numericUpDownJpegQuality.Value,
                        ResolutionRatio = (int)numericUpDownResolutionRatio.Value,
                        Mouse = checkBoxMouse.Checked,
                        Active = checkBoxActive.Checked,
                        X = (int)numericUpDownX.Value,
                        Y = (int)numericUpDownY.Value,
                        Width = (int)numericUpDownWidth.Value,
                        Height = (int)numericUpDownHeight.Value,
                        Source = comboBoxScreenSource.SelectedIndex
                    });

                    Okay();
                }
                else
                {
                    MessageBox.Show("A screen with this name already exists.", "Duplicate Name Conflict",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ChangeScreen()
        {
            if (InputValid())
            {
                if (NameChanged() || InputChanged())
                {
                    TrimInput();

                    if (ScreenCollection.GetByName(textBoxScreenName.Text) != null && NameChanged())
                    {
                        MessageBox.Show("A screen with this name already exists.", "Duplicate Name Conflict",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ScreenCollection.Get(ScreenObject).Name = textBoxScreenName.Text;
                        ScreenCollection.Get(ScreenObject).Folder = FileSystem.CorrectScreenshotsFolderPath(textBoxFolder.Text);
                        ScreenCollection.Get(ScreenObject).Macro = textBoxMacro.Text;
                        ScreenCollection.Get(ScreenObject).Component = comboBoxScreenComponent.SelectedIndex;
                        ScreenCollection.Get(ScreenObject).Format = ImageFormatCollection.GetByName(comboBoxFormat.Text);
                        ScreenCollection.Get(ScreenObject).JpegQuality = (int)numericUpDownJpegQuality.Value;
                        ScreenCollection.Get(ScreenObject).ResolutionRatio = (int)numericUpDownResolutionRatio.Value;
                        ScreenCollection.Get(ScreenObject).Mouse = checkBoxMouse.Checked;
                        ScreenCollection.Get(ScreenObject).Active = checkBoxActive.Checked;
                        ScreenCollection.Get(ScreenObject).X = (int)numericUpDownX.Value;
                        ScreenCollection.Get(ScreenObject).Y = (int)numericUpDownY.Value;
                        ScreenCollection.Get(ScreenObject).Width = (int)numericUpDownWidth.Value;
                        ScreenCollection.Get(ScreenObject).Height = (int)numericUpDownHeight.Value;
                        ScreenCollection.Get(ScreenObject).Source = comboBoxScreenSource.SelectedIndex;

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
            textBoxScreenName.Text = textBoxScreenName.Text.Trim();
            textBoxFolder.Text = textBoxFolder.Text.Trim();
            textBoxMacro.Text = textBoxMacro.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxScreenName.Text) &&
                !string.IsNullOrEmpty(textBoxFolder.Text) &&
                !string.IsNullOrEmpty(textBoxMacro.Text))
            {
                return true;
            }

            return false;
        }

        private bool InputChanged()
        {
            if (ScreenObject != null &&
                (!ScreenObject.Folder.Equals(textBoxFolder.Text) ||
                 !ScreenObject.Macro.Equals(textBoxMacro.Text) ||
                 ScreenObject.Component != comboBoxScreenComponent.SelectedIndex ||
                 !ScreenObject.Format.Equals(comboBoxFormat.SelectedItem) ||
                 ScreenObject.JpegQuality != (int)numericUpDownJpegQuality.Value ||
                 ScreenObject.ResolutionRatio != (int)numericUpDownResolutionRatio.Value ||
                 !ScreenObject.Mouse.Equals(checkBoxMouse.Checked) ||
                 !ScreenObject.Active.Equals(checkBoxActive.Checked) ||
                 ScreenObject.X != (int)numericUpDownX.Value ||
                 ScreenObject.Y != (int)numericUpDownY.Value ||
                 ScreenObject.Width != (int)numericUpDownWidth.Value ||
                 ScreenObject.Height != (int)numericUpDownHeight.Value) ||
                 ScreenObject.Source != comboBoxScreenSource.SelectedIndex)
            {
                return true;
            }

            return false;
        }

        private bool NameChanged()
        {
            if (ScreenObject != null &&
                !ScreenObject.Name.Equals(textBoxScreenName.Text))
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

        private void buttonBrowseFolder_Click(object sender, EventArgs e)
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
                // The Source is "Auto Screen Capture" and the Component is "Active Window".
                if (comboBoxScreenSource.SelectedIndex == 0 && comboBoxScreenComponent.SelectedIndex == 0)
                {
                    labelX.Enabled = false;
                    labelY.Enabled = false;
                    labelWidth.Enabled = false;
                    labelHeight.Enabled = false;

                    numericUpDownX.Enabled = false;
                    numericUpDownY.Enabled = false;
                    numericUpDownWidth.Enabled = false;
                    numericUpDownHeight.Enabled = false;
                }
                else
                {
                    labelX.Enabled = true;
                    labelY.Enabled = true;
                    labelWidth.Enabled = true;
                    labelHeight.Enabled = true;

                    numericUpDownX.Enabled = true;
                    numericUpDownY.Enabled = true;
                    numericUpDownWidth.Enabled = true;
                    numericUpDownHeight.Enabled = true;
                }

                if (checkBoxActive.Checked)
                {
                    // The Source is "Auto Screen Capture" and the Component is "Active Window".
                    if (comboBoxScreenSource.SelectedIndex == 0 && comboBoxScreenComponent.SelectedIndex == 0)
                    {
                        pictureBoxPreview.Image = screenCapture.GetActiveWindowBitmap();
                    }
                    else
                    {
                        pictureBoxPreview.Image = screenCapture.GetScreenBitmap(
                                (int)numericUpDownX.Value,
                                (int)numericUpDownY.Value,
                                (int)numericUpDownWidth.Value,
                                (int)numericUpDownHeight.Value,
                                (int)numericUpDownResolutionRatio.Value,
                                checkBoxMouse.Checked
                            );
                    }

                    UpdatePreviewMacro();
                }
                else
                {
                    pictureBoxPreview.Image = null;

                    textBoxMacroPreview.ForeColor = System.Drawing.Color.White;
                    textBoxMacroPreview.BackColor = System.Drawing.Color.Black;
                    textBoxMacroPreview.Text = "[Active option is off. No screenshots of this screen will be taken during a running screen capture session]";
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("FormScreen::UpdatePreview", ex);
            }
        }

        private void UpdatePreviewMacro()
        {
            textBoxMacroPreview.ForeColor = System.Drawing.Color.Black;
            textBoxMacroPreview.BackColor = System.Drawing.Color.LightYellow;

            textBoxMacroPreview.Text = MacroParser.ParseTags(config: false, textBoxFolder.Text, TagCollection) +
                MacroParser.ParseTags(preview: true, config: false, textBoxScreenName.Text, textBoxMacro.Text, 1,
                ImageFormatCollection.GetByName(comboBoxFormat.Text), Text, TagCollection);
        }

        private void comboBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFormat.Text.Equals(ImageFormatSpec.NAME_JPEG))
            {
                numericUpDownJpegQuality.Enabled = true;
            }
            else
            {
                numericUpDownJpegQuality.Enabled = false;
            }

            updatePreviewMacro(sender, e);
        }

        private void updatePositionAndSize(object sender, EventArgs e)
        {
            string component = comboBoxScreenComponent.Text;

            Regex rgxPosition = new Regex(@"X:(?<X>\d+) Y:(?<Y>\d+)");
            Regex rgxSize = new Regex(@"\((?<Width>\d+)x(?<Height>\d+)\)");

            if (rgxPosition.IsMatch(component) && rgxSize.IsMatch(component))
            {
                numericUpDownX.Value = Convert.ToInt32(rgxPosition.Match(component).Groups["X"].Value);
                numericUpDownY.Value = Convert.ToInt32(rgxPosition.Match(component).Groups["Y"].Value);

                numericUpDownWidth.Value = Convert.ToInt32(rgxSize.Match(component).Groups["Width"].Value);
                numericUpDownHeight.Value = Convert.ToInt32(rgxSize.Match(component).Groups["Height"].Value);
            }

            updatePreviewImage(sender, e);
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
            HelpMessage("Change the image format for the screenshots taken by this screen capture. JPEG is the recommended image format");
        }

        private void checkBoxActive_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("You can capture this screen if Active is checked (turned on)");
        }

        private void textBoxFolder_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The folder where to store the files of the screenshots being taken");
        }

        private void buttonScreenBrowseFolder_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Browse for a folder where screenshots of this screen capture will be saved to");
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
            HelpMessage("The text to compare with the active window title. If it contains the defined text then this screen will be captured. An empty field will be ignored");
        }

        private void FormScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_formMacroTags != null)
            {
                _formMacroTags.Close();
            }
        }

        private void comboBoxScreenSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxScreenComponent.Items.Clear();

            // Auto Screen Capture
            if (comboBoxScreenSource.SelectedIndex == 0)
            {
                comboBoxScreenComponent.Items.Add("Active Window");

                // Add screens from screens.xml file.
                foreach (Screen screen in ScreenCollection)
                {
                    // We don't want a Screen object that's null.
                    if (screen == null)
                    {
                        continue;
                    }

                    // We don't care if it's a Screen using Active Window as its Component because that's already available in the list.
                    if (screen.Component == 0 && screen.Source == 0)
                    {
                        continue;
                    }

                    comboBoxScreenComponent.Items.Add("\"" + screen.Name + "\" X:" + screen.X + " Y:" + screen.Y + " (" + screen.Width + "x" + screen.Height + ")");
                }

                // Add regions from regions.xml file.
                foreach (Region region in RegionCollection)
                {
                    // We don't want a Region object that's null.
                    if (region == null)
                    {
                        continue;
                    }

                    comboBoxScreenComponent.Items.Add("\"" + region.Name + "\" X:" + region.X + " Y:" + region.Y + " (" + region.Width + "x" + region.Height + ")");
                }
            }

            // Graphics Card
            if (comboBoxScreenSource.SelectedIndex == 1)
            {
                foreach (System.Windows.Forms.Screen screenFromWindows in System.Windows.Forms.Screen.AllScreens)
                {
                    ScreenCapture.DeviceResolution deviceResolution = ScreenCapture.GetDeviceResolution(screenFromWindows);

                    comboBoxScreenComponent.Items.Add("\"" + deviceResolution.screen.DeviceName + "\" X:" + deviceResolution.screen.Bounds.X + " Y:" + deviceResolution.screen.Bounds.Y + " (" + deviceResolution.width + "x" + deviceResolution.height + ")");
                }
            }

            // Operating System
            if (comboBoxScreenSource.SelectedIndex == 2)
            {
                foreach (System.Windows.Forms.Screen screenFromWindows in System.Windows.Forms.Screen.AllScreens)
                {
                    comboBoxScreenComponent.Items.Add("\"" + screenFromWindows.DeviceName + "\" X:" + screenFromWindows.Bounds.X + " Y:" + screenFromWindows.Bounds.Y + " (" + screenFromWindows.Bounds.Width + "x" + screenFromWindows.Bounds.Height + ")");
                }
            }

            if (ScreenObject != null && ScreenObject.Component < comboBoxScreenComponent.Items.Count)
            {
                comboBoxScreenComponent.SelectedIndex = ScreenObject.Component;
            }
            else
            {
                comboBoxScreenComponent.SelectedIndex = 0;
            }
        }
    }
}