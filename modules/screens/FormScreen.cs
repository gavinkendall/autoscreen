//-----------------------------------------------------------------------
// <copyright file="FormScreen.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for adding a new screen or changing an existing screen.</summary>
//-----------------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.Collections.Generic;

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
        /// A dictionary of available screens.
        /// </summary>
        public Dictionary<int, System.Windows.Forms.Screen> ScreenDictionary = new Dictionary<int, System.Windows.Forms.Screen>();

        /// <summary>
        /// Constructor for FormScreen.
        /// </summary>
        public FormScreen()
        {
            InitializeComponent();

            RefreshScreenDictionary();
        }

        private void FormScreen_Load(object sender, EventArgs e)
        {
            textBoxScreenName.Focus();

            HelpMessage("This is where to configure a screen capture. Select an available screen from the Component drop-down menu and keep an eye on Preview");

            comboBoxFormat.Items.Clear();
            comboBoxScreenComponent.Items.Clear();

            pictureBoxPreview.Image = null;

            foreach (ImageFormat imageFormat in ImageFormatCollection)
            {
                comboBoxFormat.Items.Add(imageFormat.Name);
            }

            comboBoxScreenComponent.Items.Add("Active Window");

            for (int i = 1; i <= ScreenDictionary.Count; i++)
            {
                System.Windows.Forms.Screen screen = ScreenDictionary[i];
                comboBoxScreenComponent.Items.Add("Screen " + i + " (" + screen.Bounds.Width + " x " + screen.Bounds.Height + ")");
            }

            if (ScreenObject != null)
            {
                Text = "Change Screen";

                textBoxScreenName.Text = ScreenObject.Name;
                textBoxFolder.Text = FileSystem.CorrectScreenshotsFolderPath(ScreenObject.Folder);
                textBoxMacro.Text = ScreenObject.Macro;

                if (ScreenObject.Component < comboBoxScreenComponent.Items.Count)
                {
                    comboBoxScreenComponent.SelectedIndex = ScreenObject.Component;
                }
                else
                {
                    comboBoxScreenComponent.SelectedIndex = 0;
                    MessageBox.Show("The configured screen component has an invalid index since it is not available on this system. The component has therefore been set to Active Window.", "Invalid Screen Index", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                comboBoxFormat.SelectedItem = ScreenObject.Format.Name;
                numericUpDownJpegQuality.Value = ScreenObject.JpegQuality;
                numericUpDownResolutionRatio.Value = ScreenObject.ResolutionRatio;
                checkBoxMouse.Checked = ScreenObject.Mouse;
                checkBoxActive.Checked = ScreenObject.Active;
                checkBoxActiveWindowTitle.Checked = ScreenObject.ActiveWindowTitleCaptureCheck;
                textBoxActiveWindowTitle.Text = ScreenObject.ActiveWindowTitleCaptureText;
            }
            else
            {
                Text = "Add New Screen";

                textBoxScreenName.Text = "Screen " + (ScreenCollection.Count + 1);
                textBoxFolder.Text = FileSystem.ScreenshotsFolder;
                textBoxMacro.Text = MacroParser.DefaultMacro;
                comboBoxScreenComponent.SelectedIndex = 0;
                comboBoxFormat.SelectedItem = ScreenCapture.DefaultImageFormat;
                numericUpDownJpegQuality.Value = 100;
                numericUpDownResolutionRatio.Value = 100;
                checkBoxMouse.Checked = true;
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

        /// <summary>
        /// Updates the screen dictionary with the available screens.
        /// </summary>
        public void RefreshScreenDictionary()
        {
            ScreenDictionary.Clear();

            int component = 1;

            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                ScreenDictionary.Add(component, screen);
                component++;
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
                        Name = textBoxScreenName.Text,
                        Folder = FileSystem.CorrectScreenshotsFolderPath(textBoxFolder.Text),
                        Macro = textBoxMacro.Text,
                        Component = comboBoxScreenComponent.SelectedIndex,
                        Format = ImageFormatCollection.GetByName(comboBoxFormat.Text),
                        JpegQuality = (int)numericUpDownJpegQuality.Value,
                        ResolutionRatio = (int)numericUpDownResolutionRatio.Value,
                        Mouse = checkBoxMouse.Checked,
                        Active = checkBoxActive.Checked,
                        ActiveWindowTitleCaptureCheck = checkBoxActiveWindowTitle.Checked,
                        ActiveWindowTitleCaptureText = textBoxActiveWindowTitle.Text
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
                        ScreenCollection.Get(ScreenObject).JpegQuality = (int) numericUpDownJpegQuality.Value;
                        ScreenCollection.Get(ScreenObject).ResolutionRatio = (int) numericUpDownResolutionRatio.Value;
                        ScreenCollection.Get(ScreenObject).Mouse = checkBoxMouse.Checked;
                        ScreenCollection.Get(ScreenObject).Active = checkBoxActive.Checked;
                        ScreenCollection.Get(ScreenObject).ActiveWindowTitleCaptureCheck = checkBoxActiveWindowTitle.Checked;
                        ScreenCollection.Get(ScreenObject).ActiveWindowTitleCaptureText = textBoxActiveWindowTitle.Text;

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
            textBoxActiveWindowTitle.Text = textBoxActiveWindowTitle.Text.Trim();
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
                 !ScreenObject.Active.Equals(checkBoxActive.Checked)))
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
                if (checkBoxActive.Checked)
                {
                    if (comboBoxScreenComponent.SelectedIndex == 0)
                    {
                        pictureBoxPreview.Image = screenCapture.GetActiveWindowBitmap();
                    }
                    else
                    {
                        System.Windows.Forms.Screen screen = GetScreenByIndex(comboBoxScreenComponent.SelectedIndex);

                        pictureBoxPreview.Image = screen != null
                            ? screenCapture.GetScreenBitmap(
                                screen.Bounds.X,
                                screen.Bounds.Y,
                                screen.Bounds.Width,
                                screen.Bounds.Height,
                                (int)numericUpDownResolutionRatio.Value,
                                checkBoxMouse.Checked
                            )
                            : null;
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

            textBoxMacroPreview.Text = MacroParser.ParseTags(textBoxFolder.Text, TagCollection) +
                MacroParser.ParseTags(preview: true, textBoxScreenName.Text, textBoxMacro.Text, 1,
                ImageFormatCollection.GetByName(comboBoxFormat.Text), Text, TagCollection);
        }

        private System.Windows.Forms.Screen GetScreenByIndex(int index)
        {
            try
            {
                System.Windows.Forms.Screen screen = ScreenDictionary[index];
                return screen;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
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