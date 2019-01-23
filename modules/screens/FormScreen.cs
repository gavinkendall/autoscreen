//-----------------------------------------------------------------------
// <copyright file="FormScreen.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.Windows.Forms;

    public partial class FormScreen : Form
    {
        public ScreenCollection ScreenCollection { get; } = new ScreenCollection();

        public Screen ScreenObject { get; set; }

        public ImageFormatCollection ImageFormatCollection { get; set; }

        public FormScreen()
        {
            InitializeComponent();
        }

        private void FormScreen_Load(object sender, EventArgs e)
        {
            comboBoxScreenFormat.Items.Clear();

            foreach (ImageFormat imageFormat in ImageFormatCollection)
            {
                comboBoxScreenFormat.Items.Add(imageFormat.Name);
            }

            if (ScreenObject != null)
            {
                Text = "Change Screen";

                textBoxScreenName.Text = ScreenObject.Name;
                textBoxScreenFolder.Text = ScreenObject.Folder;
                textBoxScreenMacro.Text = ScreenObject.Macro;
                comboBoxScreenFormat.SelectedItem = ScreenObject.Format.Name;
                numericUpDownScreenJpegQuality.Value = ScreenObject.JpegQuality;
                numericUpDownScreenResolutionRatio.Value = ScreenObject.ResolutionRatio;
                checkBoxScreenMouse.Checked = ScreenObject.Mouse;
            }
            else
            {
                Text = "Add New Screen";

                textBoxScreenName.Text = string.Empty;
                textBoxScreenFolder.Text = FileSystem.ScreenshotsFolder;
                textBoxScreenMacro.Text = MacroParser.ScreenMacro;
                comboBoxScreenFormat.SelectedItem = ScreenCapture.DefaultImageFormat;
                numericUpDownScreenJpegQuality.Value = 100;
                numericUpDownScreenResolutionRatio.Value = 100;
                checkBoxScreenMouse.Checked = true;
            }

            UpdatePreview();
        }

        private void Click_buttonScreenCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void Click_buttonScreenOK(object sender, EventArgs e)
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
                    ScreenCollection.Add(new Screen(
                        textBoxScreenName.Text,
                        textBoxScreenFolder.Text,
                        textBoxScreenMacro.Text,
                        ImageFormatCollection.GetByName(comboBoxScreenFormat.Text),
                        (int)numericUpDownScreenJpegQuality.Value,
                        (int)numericUpDownScreenResolutionRatio.Value,
                        checkBoxScreenMouse.Checked));

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
                        MessageBox.Show("A screen with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ScreenCollection.Get(ScreenObject).Name = textBoxScreenName.Text;
                        ScreenCollection.Get(ScreenObject).Folder = textBoxScreenFolder.Text;
                        ScreenCollection.Get(ScreenObject).Macro = textBoxScreenMacro.Text;
                        ScreenCollection.Get(ScreenObject).Format = ImageFormatCollection.GetByName(comboBoxScreenFormat.Text);
                        ScreenCollection.Get(ScreenObject).JpegQuality = (int)numericUpDownScreenJpegQuality.Value;
                        ScreenCollection.Get(ScreenObject).ResolutionRatio = (int) numericUpDownScreenResolutionRatio.Value;
                        ScreenCollection.Get(ScreenObject).Mouse = checkBoxScreenMouse.Checked;

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
            textBoxScreenFolder.Text = textBoxScreenFolder.Text.Trim();
            textBoxScreenMacro.Text = textBoxScreenMacro.Text.Trim();

            //if (!textBoxScreenFolder.Text.EndsWith(@"\"))
            //{
            //    textBoxScreenFolder.Text += @"\";
            //}
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxScreenName.Text) &&
                !string.IsNullOrEmpty(textBoxScreenFolder.Text) &&
                !string.IsNullOrEmpty(textBoxScreenMacro.Text))
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
            if (ScreenObject != null &&
                (!ScreenObject.Folder.Equals(textBoxScreenFolder.Text) ||
                 !ScreenObject.Macro.Equals(textBoxScreenMacro.Text) ||
                 !ScreenObject.Format.Equals(comboBoxScreenFormat.SelectedItem) ||
                 ScreenObject.JpegQuality != (int)numericUpDownScreenJpegQuality.Value ||
                 ScreenObject.ResolutionRatio != (int)numericUpDownScreenResolutionRatio.Value ||
                 !ScreenObject.Mouse.Equals(checkBoxScreenMouse.Checked)))
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
            if (ScreenObject != null &&
                !ScreenObject.Name.Equals(textBoxScreenName.Text))
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

        private void Tick_timerScreenPreview(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void buttonScreenBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();

            if (browser.ShowDialog() == DialogResult.OK)
            {
                textBoxScreenFolder.Text = browser.SelectedPath;
            }
        }

        private void UpdatePreview()
        {
            pictureBoxScreenPreview.Image = ScreenCapture.GetScreenBitmap(
                (int)numericUpDownScreenX.Value,
                (int)numericUpDownScreenY.Value,
                (int)numericUpDownScreenWidth.Value,
                (int)numericUpDownScreenHeight.Value,
                (int)numericUpDownScreenResolutionRatio.Value,
                checkBoxScreenMouse.Checked
            );
        }
    }
}