//-----------------------------------------------------------------------
// <copyright file="FormScreen.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
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
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class for handling screens.
    /// </summary>
    public partial class FormScreen : Form
    {
        private int _autoAdaptIndex;
        private bool _autoAdaptUsesEnumDisplaySettings;

        private Log _log;
        private Config _config;
        private MacroParser _macroParser;
        private ScreenCapture _screenCapture;
        private FileSystem _fileSystem;

        private ToolTip _toolTip = new ToolTip();
        private FormMacroTagsToolWindow _formMacroTags;

        /// <summary>
        /// A collection of screens.
        /// </summary>
        public ScreenCollection ScreenCollection { get; }

        /// <summary>
        /// A collection of regions to be used for selecting a source.
        /// </summary>
        public RegionCollection RegionCollection { get; set; }

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
        public MacroTagCollection MacroTagCollection { get; set; }

        /// <summary>
        /// Access to screen capture methods.
        /// </summary>
        public ScreenCapture ScreenCapture { get; set; }

        /// <summary>
        /// Constructor for FormScreen.
        /// </summary>
        public FormScreen(ScreenCapture screenCapture, MacroParser macroParser, FileSystem fileSystem, Config config, Log log)
        {
            InitializeComponent();

            _log = log;
            _config = config;
            _macroParser = macroParser;
            _screenCapture = screenCapture;
            _fileSystem = fileSystem;

            ScreenCollection = new ScreenCollection();
            RegionCollection = new RegionCollection();
        }

        private void FormScreen_Load(object sender, EventArgs e)
        {
            textBoxScreenName.Focus();

            HelpMessage("This is where to configure a screen capture. Select a source, component, and capture method then adjust the display properties and image attributes");

            _toolTip.SetToolTip(checkBoxMouse, "You can include the mouse pointer in your screenshots if the \"Include mouse pointer\" option is checked");
            _toolTip.SetToolTip(comboBoxFormat, "Change the image format for the screenshots taken by this screen capture. JPEG is the recommended image format");
            _toolTip.SetToolTip(checkBoxEnable, "You can capture this screen if Enable is checked (turned on)");
            _toolTip.SetToolTip(buttonScreenBrowseFolder, "Browse for a folder where screenshots of this screen capture will be saved to");
            _toolTip.SetToolTip(buttonMacroTags, "Open a list of available macro tags. You can keep the Macro Tags window open while you modify your filename pattern in the Macro field");
            _toolTip.SetToolTip(checkBoxAutoAdapt, "The position and size will automatically adjust based on changes to your display setup");
            _toolTip.SetToolTip(checkBoxEncrypt, "Screenshots can be encrypted so only you can view each screenshot with Auto Screen Capture");

            int imageDiffTolerance = Convert.ToInt32(_config.Settings.User.GetByKey("ImageDiffTolerance").Value);
            bool optimizeScreenCapture = Convert.ToBoolean(_config.Settings.User.GetByKey("OptimizeScreenCapture").Value);

            if (optimizeScreenCapture)
            {
                numericUpDownImageDifferenceTolerance.Enabled = true;
            }
            else
            {
                numericUpDownImageDifferenceTolerance.Enabled = false;
            }

            _autoAdaptUsesEnumDisplaySettings = Convert.ToBoolean(_config.Settings.User.GetByKey("AutoAdaptUsesEnumDisplaySettings").Value);

            comboBoxFormat.Items.Clear();

            labelScreenSource.Enabled = true;
            labelScreenComponent.Enabled = true;

            comboBoxScreenSource.Enabled = true;
            comboBoxScreenComponent.Enabled = true;

            labelX.Enabled = true;
            labelY.Enabled = true;
            labelWidth.Enabled = true;
            labelHeight.Enabled = true;

            numericUpDownX.Enabled = true;
            numericUpDownY.Enabled = true;
            numericUpDownWidth.Enabled = true;
            numericUpDownHeight.Enabled = true;

            pictureBoxPreview.Image = null;

            foreach (ImageFormat imageFormat in ImageFormatCollection)
            {
                comboBoxFormat.Items.Add(imageFormat.Name);
            }

            comboBoxScreenSource.Items.Clear();
            comboBoxScreenSource.Items.Add("Auto Screen Capture");
            comboBoxScreenSource.Items.Add("EnumDisplaySettings (user32.dll)");
            comboBoxScreenSource.Items.Add("System.Windows.Forms.Screen (.NET)");

            comboBoxScreenCaptureMethod.Items.Clear();
            comboBoxScreenCaptureMethod.Items.Add("System.Drawing.Graphics.CopyFromScreen (.NET)");
            comboBoxScreenCaptureMethod.Items.Add("BitBlt (gdi32.dll)");

            if (ScreenObject != null)
            {
                Text = "Configure Screen";

                textBoxScreenName.Text = ScreenObject.Name;
                textBoxFolder.Text = _fileSystem.CorrectScreenshotsFolderPath(ScreenObject.Folder);
                textBoxMacro.Text = ScreenObject.Macro;
                comboBoxScreenSource.SelectedIndex = ScreenObject.Source;
                comboBoxScreenCaptureMethod.SelectedIndex = ScreenObject.CaptureMethod;
                comboBoxFormat.SelectedItem = ScreenObject.Format.Name;
                numericUpDownJpegQuality.Value = ScreenObject.JpegQuality;
                numericUpDownImageDifferenceTolerance.Value = ScreenObject.ImageDiffTolerance;
                checkBoxMouse.Checked = ScreenObject.Mouse;
                checkBoxEnable.Checked = ScreenObject.Enable;
                numericUpDownX.Value = ScreenObject.X;
                numericUpDownY.Value = ScreenObject.Y;
                numericUpDownWidth.Value = ScreenObject.Width;
                numericUpDownHeight.Value = ScreenObject.Height;

                // AutoAdapt
                _autoAdaptIndex = ScreenObject.AutoAdaptIndex;
                checkBoxAutoAdapt.Checked = ScreenObject.AutoAdapt;

                checkBoxEncrypt.Checked = ScreenObject.Encrypt;
            }
            else
            {
                Text = "Add Screen";

                textBoxScreenName.Text = "Screen " + (ScreenCollection.Count + 1);
                textBoxFolder.Text = _fileSystem.ScreenshotsFolder;
                textBoxMacro.Text = _fileSystem.FilenamePattern;
                comboBoxScreenSource.SelectedIndex = 0;
                comboBoxScreenCaptureMethod.SelectedIndex = 0;
                comboBoxFormat.SelectedItem = ScreenCapture.ImageFormat;
                numericUpDownJpegQuality.Value = 100;
                numericUpDownImageDifferenceTolerance.Value = imageDiffTolerance;
                checkBoxMouse.Checked = true;
                checkBoxEnable.Checked = true;
                numericUpDownX.Value = 0;
                numericUpDownY.Value = 0;
                numericUpDownWidth.Value = 0;
                numericUpDownHeight.Value = 0;

                // AutoAdapt
                int nextAdaptIndexToUseForNewScreen = 0;

                foreach (Screen screen in ScreenCollection)
                {
                    if (screen.AutoAdapt)
                    {
                        nextAdaptIndexToUseForNewScreen++;
                    }
                }

                _autoAdaptIndex = nextAdaptIndexToUseForNewScreen;
                checkBoxAutoAdapt.Checked = false;

                checkBoxEncrypt.Checked = false;
            }

            UpdatePreviewMacro();
            UpdatePreviewImage(_screenCapture);
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
                AddScreen();
            }
        }

        private void AddScreen()
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
                        Folder = _fileSystem.CorrectScreenshotsFolderPath(textBoxFolder.Text),
                        Macro = textBoxMacro.Text,
                        Component = comboBoxScreenComponent.SelectedIndex,
                        Format = ImageFormatCollection.GetByName(comboBoxFormat.Text),
                        JpegQuality = (int)numericUpDownJpegQuality.Value,
                        ImageDiffTolerance = (int)numericUpDownImageDifferenceTolerance.Value,
                        Mouse = checkBoxMouse.Checked,
                        Enable = checkBoxEnable.Checked,
                        X = (int)numericUpDownX.Value,
                        Y = (int)numericUpDownY.Value,
                        Width = (int)numericUpDownWidth.Value,
                        Height = (int)numericUpDownHeight.Value,
                        Source = comboBoxScreenSource.SelectedIndex,
                        AutoAdapt = checkBoxAutoAdapt.Checked,
                        CaptureMethod = comboBoxScreenCaptureMethod.SelectedIndex,
                        Encrypt = checkBoxEncrypt.Checked,
                        ResolutionRatio = (int)numericUpDownResolutionRatio.Value
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
                        ScreenCollection.Get(ScreenObject).Folder = _fileSystem.CorrectScreenshotsFolderPath(textBoxFolder.Text);
                        ScreenCollection.Get(ScreenObject).Macro = textBoxMacro.Text;
                        ScreenCollection.Get(ScreenObject).Component = comboBoxScreenComponent.SelectedIndex;
                        ScreenCollection.Get(ScreenObject).Format = ImageFormatCollection.GetByName(comboBoxFormat.Text);
                        ScreenCollection.Get(ScreenObject).JpegQuality = (int)numericUpDownJpegQuality.Value;
                        ScreenCollection.Get(ScreenObject).ImageDiffTolerance = (int)numericUpDownImageDifferenceTolerance.Value;
                        ScreenCollection.Get(ScreenObject).Mouse = checkBoxMouse.Checked;
                        ScreenCollection.Get(ScreenObject).Enable = checkBoxEnable.Checked;
                        ScreenCollection.Get(ScreenObject).X = (int)numericUpDownX.Value;
                        ScreenCollection.Get(ScreenObject).Y = (int)numericUpDownY.Value;
                        ScreenCollection.Get(ScreenObject).Width = (int)numericUpDownWidth.Value;
                        ScreenCollection.Get(ScreenObject).Height = (int)numericUpDownHeight.Value;
                        ScreenCollection.Get(ScreenObject).Source = comboBoxScreenSource.SelectedIndex;
                        ScreenCollection.Get(ScreenObject).AutoAdapt = checkBoxAutoAdapt.Checked;
                        ScreenCollection.Get(ScreenObject).CaptureMethod = comboBoxScreenCaptureMethod.SelectedIndex;
                        ScreenCollection.Get(ScreenObject).Encrypt = checkBoxEncrypt.Checked;
                        ScreenCollection.Get(ScreenObject).ResolutionRatio = (int)numericUpDownResolutionRatio.Value;

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
                 ScreenObject.ImageDiffTolerance != (int)numericUpDownImageDifferenceTolerance.Value ||
                 !ScreenObject.Mouse.Equals(checkBoxMouse.Checked) ||
                 !ScreenObject.Enable.Equals(checkBoxEnable.Checked) ||
                 ScreenObject.X != (int)numericUpDownX.Value ||
                 ScreenObject.Y != (int)numericUpDownY.Value ||
                 ScreenObject.Width != (int)numericUpDownWidth.Value ||
                 ScreenObject.Height != (int)numericUpDownHeight.Value ||
                 ScreenObject.Source != comboBoxScreenSource.SelectedIndex ||
                 ScreenObject.AutoAdapt != checkBoxAutoAdapt.Checked ||
                 !ScreenObject.Encrypt.Equals(checkBoxEncrypt.Checked) ||
                 ScreenObject.ResolutionRatio != (int)numericUpDownResolutionRatio.Value))
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
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                textBoxFolder.Text = folderBrowser.SelectedPath;
            }
        }

        /// <summary>
        /// Update the preview image.
        /// </summary>
        /// <param name="screenCapture"></param>
        public void UpdatePreviewImage(ScreenCapture screenCapture)
        {
            try
            {
                // The mouse pointer gets really weird if we go under 100 resolution ratio
                // so disable the mouse checkbox control to indicate we can't show the mouse pointer.
                if (numericUpDownResolutionRatio.Value == 100)
                {
                    checkBoxMouse.Enabled = true;
                }
                else
                {
                    checkBoxMouse.Enabled = false;
                }

                if (checkBoxAutoAdapt.Checked)
                {
                    labelScreenSource.Enabled = false;
                    labelScreenComponent.Enabled = false;

                    comboBoxScreenSource.Enabled = false;
                    comboBoxScreenComponent.Enabled = false;

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
                    labelScreenSource.Enabled = true;
                    labelScreenComponent.Enabled = true;

                    comboBoxScreenSource.Enabled = true;
                    comboBoxScreenComponent.Enabled = true;

                    labelX.Enabled = true;
                    labelY.Enabled = true;
                    labelWidth.Enabled = true;
                    labelHeight.Enabled = true;

                    numericUpDownX.Enabled = true;
                    numericUpDownY.Enabled = true;
                    numericUpDownWidth.Enabled = true;
                    numericUpDownHeight.Enabled = true;
                }

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

                pictureBoxPreview.Image = null;

                if (checkBoxEnable.Checked)
                {
                    // This is for when the option "Automatically adapt to display setup" is enabled.
                    // We need to show the position and size based on what Windows provides.
                    if (checkBoxAutoAdapt.Checked)
                    {
                        int x = 0;
                        int y = 0;
                        int width = 0;
                        int height = 0;

                        for (int i = 0; i < System.Windows.Forms.Screen.AllScreens.Length; i++)
                        {
                            System.Windows.Forms.Screen screenFromWindows = System.Windows.Forms.Screen.AllScreens[i];

                            if (i == _autoAdaptIndex)
                            {
                                if (_autoAdaptUsesEnumDisplaySettings)
                                {
                                    ScreenCapture.DeviceOptions deviceResolution = _screenCapture.GetDevice(screenFromWindows);

                                    x = screenFromWindows.Bounds.X;
                                    y = screenFromWindows.Bounds.Y;
                                    width = deviceResolution.width;
                                    height = deviceResolution.height;
                                }
                                else
                                {
                                    x = screenFromWindows.Bounds.X;
                                    y = screenFromWindows.Bounds.Y;
                                    width = screenFromWindows.Bounds.Width;
                                    height = screenFromWindows.Bounds.Height;
                                }

                                break;
                            }
                        }

                        pictureBoxPreview.Image = screenCapture.GetScreenBitmap(
                            comboBoxScreenSource.SelectedIndex,
                            comboBoxScreenComponent.SelectedIndex,
                            comboBoxScreenCaptureMethod.SelectedIndex,
                            x,
                            y,
                            width,
                            height,
                            (int)numericUpDownResolutionRatio.Value,
                            checkBoxMouse.Checked
                        );
                    }
                    else
                    {
                        // The Source is "Auto Screen Capture" and the Component is "Active Window".
                        if (comboBoxScreenSource.SelectedIndex == 0 && comboBoxScreenComponent.SelectedIndex == 0)
                        {
                            pictureBoxPreview.Image = screenCapture.GetActiveWindowBitmap((int)numericUpDownResolutionRatio.Value, checkBoxMouse.Checked);
                        }
                        else
                        {
                            pictureBoxPreview.Image = screenCapture.GetScreenBitmap(
                                comboBoxScreenSource.SelectedIndex,
                                comboBoxScreenComponent.SelectedIndex,
                                comboBoxScreenCaptureMethod.SelectedIndex,
                                (int)numericUpDownX.Value,
                                (int)numericUpDownY.Value,
                                (int)numericUpDownWidth.Value,
                                (int)numericUpDownHeight.Value,
                                (int)numericUpDownResolutionRatio.Value,
                                checkBoxMouse.Checked
                            );
                        }
                    }

                    UpdatePreviewMacro();
                }
                else
                {
                    textBoxMacroPreview.ForeColor = System.Drawing.Color.White;
                    textBoxMacroPreview.BackColor = System.Drawing.Color.Black;
                    textBoxMacroPreview.Text = "[Enable option is off. No screenshots of this screen will be taken during a running screen capture session]";
                }
            }
            catch (Exception ex)
            {
                _log.WriteExceptionMessage("FormScreen::UpdatePreviewImage", ex);
            }
        }

        private void UpdatePreviewMacro()
        {
            textBoxMacroPreview.ForeColor = System.Drawing.Color.Black;
            textBoxMacroPreview.BackColor = System.Drawing.Color.LightYellow;

            Screen screen = new Screen
            {
                Name = textBoxScreenName.Text,
                X = (int)numericUpDownX.Value,
                Y = (int)numericUpDownY.Value,
                Width = (int)numericUpDownWidth.Value,
                Height = (int)numericUpDownHeight.Value,
                Format = ImageFormatCollection.GetByName(comboBoxFormat.Text)
            };

            string label = _config.Settings.User.GetByKey("ScreenshotLabel").Value.ToString();

            textBoxMacroPreview.Text = _macroParser.ParseTags(preview: true, textBoxFolder.Text, screen, Text, Assembly.GetExecutingAssembly().GetName().Name, label, MacroTagCollection, _log) +
                _macroParser.ParseTags(preview: true, textBoxMacro.Text, screen, Text, Assembly.GetExecutingAssembly().GetName().Name, label, MacroTagCollection, _log);
        }

        private void comboBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFormat.Text.Equals("JPEG"))
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
            if (comboBoxScreenCaptureMethod.SelectedIndex == 0)
            {
                labelResolutionRatio.Enabled = true;
                numericUpDownResolutionRatio.Enabled = true;
            }
            else
            {
                labelResolutionRatio.Enabled = false;
                numericUpDownResolutionRatio.Enabled = false;
            }

            string component = comboBoxScreenComponent.Text;

            Regex rgxPosition = new Regex(@"X:(?<X>-?\d+) Y:(?<Y>-?\d+)");
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
                _formMacroTags = new FormMacroTagsToolWindow(MacroTagCollection, _macroParser, _log);
                _formMacroTags.Show();
            }
            else
            {
                _formMacroTags.BringToFront();
            }
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
                    // We also don't care if it's a Screen that's using AutoAdapt.
                    if ((screen.Component == 0 && screen.Source == 0) || screen.AutoAdapt)
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

            // EnumDisplaySettings (user32.dll)
            if (comboBoxScreenSource.SelectedIndex == 1)
            {
                foreach (System.Windows.Forms.Screen screenFromWindows in System.Windows.Forms.Screen.AllScreens)
                {
                    ScreenCapture.DeviceOptions deviceOptions = _screenCapture.GetDevice(screenFromWindows);

                    comboBoxScreenComponent.Items.Add("\"" + deviceOptions.screen.DeviceName + "\" X:" + deviceOptions.screen.Bounds.X + " Y:" + deviceOptions.screen.Bounds.Y + " (" + deviceOptions.width + "x" + deviceOptions.height + ")");
                }
            }

            // System.Windows.Forms.Screen (.NET)
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