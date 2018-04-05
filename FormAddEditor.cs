//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.0
// autoscreen.FormAddEditor.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Sunday, 25 March 2018

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace autoscreen
{
    public partial class FormAddEditor : Form
    {
        public FormAddEditor()
        {
            InitializeComponent();
        }

        private void FormAddEditor_Load(object sender, EventArgs e)
        {
            textBoxEditorName.Text = string.Empty;
            textBoxEditorApplication.Text = string.Empty;
            textBoxEditorArguments.Text = "%screenshot%";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddEditor(textBoxEditorName.Text, textBoxEditorApplication.Text, textBoxEditorArguments.Text);
            Close();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            RunEditor(textBoxEditorName.Text, textBoxEditorApplication.Text, textBoxEditorArguments.Text);
            Close();
        }

        private void AddEditor(string name, string application, string arguments)
        {
            DialogResult = DialogResult.OK;

            if (!string.IsNullOrEmpty(name) &&
                !string.IsNullOrEmpty(application) &&
                !string.IsNullOrEmpty(arguments))
            {
                EditorCollection.Add(new Editor(name, application, arguments));
            }
        }

        private void RunEditor(string name, string application, string arguments)
        {
            AddEditor(name, application, arguments);

            Screenshot selectedScreenshot = ScreenshotCollection.GetBySlidename(Slideshow.SelectedSlide, Slideshow.SelectedScreen == 0 ? 1 : Slideshow.SelectedScreen);

            if (selectedScreenshot != null && !string.IsNullOrEmpty(selectedScreenshot.Path) && File.Exists(selectedScreenshot.Path))
            {
                Process.Start(application, arguments.Replace("%screenshot%", selectedScreenshot.Path));
            }
        }

        private void buttonOpenEditor_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.FilterIndex = 0;
            openFileDialog.Multiselect = false;
            openFileDialog.AddExtension = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = "Applications (*.exe)|*.exe";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxEditorApplication.Text = openFileDialog.FileName;
            }
        }
    }
}