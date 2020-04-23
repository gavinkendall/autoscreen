//-----------------------------------------------------------------------
// <copyright file="FormScreen.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormScreen : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public ScreenCollection ScreenCollection { get; } = new ScreenCollection();

        /// <summary>
        /// 
        /// </summary>
        public Screen ScreenObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ImageFormatCollection ImageFormatCollection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ScreenCapture ScreenCapture { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, System.Windows.Forms.Screen> ScreenDictionary = new Dictionary<int, System.Windows.Forms.Screen>();

        /// <summary>
        /// 
        /// </summary>
        public FormScreen()
        {
            InitializeComponent();

            RefreshScreenDictionary();
        }

        private void FormScreen_Load(object sender, EventArgs e)
        {
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

                textBoxName.Text = ScreenObject.Name;
                textBoxFolder.Text = FileSystem.CorrectScreenshotsFolderPath(ScreenObject.Folder);
                textBoxMacro.Text = ScreenObject.Macro;

                if (ScreenObject.Component < comboBoxScreenComponent.Items.Count)
                {
                    SetControls(enabled: true);
                    comboBoxScreenComponent.SelectedIndex = ScreenObject.Component;
                }
                else
                {
                    SetControls(enabled: false);
                }

                comboBoxFormat.SelectedItem = ScreenObject.Format.Name;
                numericUpDownJpegQuality.Value = ScreenObject.JpegQuality;
                numericUpDownResolutionRatio.Value = ScreenObject.ResolutionRatio;
                checkBoxMouse.Checked = ScreenObject.Mouse;
                checkBoxEnabled.Checked = ScreenObject.Enabled;
            }
            else
            {
                Text = "Add New Screen";

                textBoxName.Text = "Screen " + (ScreenCollection.Count + 1);
                textBoxFolder.Text = FileSystem.ScreenshotsFolder;
                textBoxMacro.Text = MacroParser.DefaultMacro;
                comboBoxScreenComponent.SelectedIndex = 0;
                comboBoxFormat.SelectedItem = ScreenCapture.DefaultImageFormat;
                numericUpDownJpegQuality.Value = 100;
                numericUpDownResolutionRatio.Value = 100;
                checkBoxMouse.Checked = true;
                checkBoxEnabled.Checked = true;
            }

            timerScreenPreview.Enabled = true;
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
        /// 
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

                if (ScreenCollection.GetByName(textBoxName.Text) == null)
                {
                    ScreenCollection.Add(new Screen(
                        textBoxName.Text,
                        FileSystem.CorrectScreenshotsFolderPath(textBoxFolder.Text),
                        textBoxMacro.Text,
                        comboBoxScreenComponent.SelectedIndex,
                        ImageFormatCollection.GetByName(comboBoxFormat.Text),
                        (int)numericUpDownJpegQuality.Value,
                        (int)numericUpDownResolutionRatio.Value,
                        checkBoxMouse.Checked,
                        checkBoxEnabled.Checked));

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

                    if (ScreenCollection.GetByName(textBoxName.Text) != null && NameChanged())
                    {
                        MessageBox.Show("A screen with this name already exists.", "Duplicate Name Conflict",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ScreenCollection.Get(ScreenObject).Name = textBoxName.Text;
                        ScreenCollection.Get(ScreenObject).Folder = FileSystem.CorrectScreenshotsFolderPath(textBoxFolder.Text);
                        ScreenCollection.Get(ScreenObject).Macro = textBoxMacro.Text;
                        ScreenCollection.Get(ScreenObject).Component = comboBoxScreenComponent.SelectedIndex;
                        ScreenCollection.Get(ScreenObject).Format = ImageFormatCollection.GetByName(comboBoxFormat.Text);
                        ScreenCollection.Get(ScreenObject).JpegQuality = (int) numericUpDownJpegQuality.Value;
                        ScreenCollection.Get(ScreenObject).ResolutionRatio = (int) numericUpDownResolutionRatio.Value;
                        ScreenCollection.Get(ScreenObject).Mouse = checkBoxMouse.Checked;
                        ScreenCollection.Get(ScreenObject).Enabled = checkBoxEnabled.Checked;

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
            if (ScreenObject != null &&
                (!ScreenObject.Folder.Equals(textBoxFolder.Text) ||
                 !ScreenObject.Macro.Equals(textBoxMacro.Text) ||
                 ScreenObject.Component != comboBoxScreenComponent.SelectedIndex ||
                 !ScreenObject.Format.Equals(comboBoxFormat.SelectedItem) ||
                 ScreenObject.JpegQuality != (int)numericUpDownJpegQuality.Value ||
                 ScreenObject.ResolutionRatio != (int)numericUpDownResolutionRatio.Value ||
                 !ScreenObject.Mouse.Equals(checkBoxMouse.Checked) ||
                 !ScreenObject.Enabled.Equals(checkBoxEnabled.Checked)))
            {
                return true;
            }

            return false;
        }

        private bool NameChanged()
        {
            if (ScreenObject != null &&
                !ScreenObject.Name.Equals(textBoxName.Text))
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

        private void Tick_timerPreview(object sender, EventArgs e)
        {
            UpdatePreview(ScreenCapture);
        }

        private void buttonBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();

            if (browser.ShowDialog() == DialogResult.OK)
            {
                textBoxFolder.Text = browser.SelectedPath;
            }
        }

        private void UpdatePreview(ScreenCapture screenCapture)
        {
            try
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

                System.GC.Collect();
            }
            catch (Exception ex)
            {
                Log.Write("FormScreen::UpdatePreview", ex);
            }
        }

        private void FormScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerScreenPreview.Enabled = false;
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

        private void SetControls(bool enabled)
        {
            buttonOK.Enabled = enabled;
            textBoxName.Enabled = enabled;
            textBoxMacro.Enabled = enabled;
            checkBoxMouse.Enabled = enabled;
            textBoxFolder.Enabled = enabled;
            comboBoxFormat.Enabled = enabled;
            comboBoxScreenComponent.Enabled = enabled;
            numericUpDownJpegQuality.Enabled = enabled;
            numericUpDownResolutionRatio.Enabled = enabled;
            checkBoxEnabled.Enabled = enabled;
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
        }
    }
}