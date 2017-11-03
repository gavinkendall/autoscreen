//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.6.5
// autoscreen.FormAddEditor.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Friday, 3 November 2017

using System;
using System.IO;
using System.Diagnostics;
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
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddEditor(textBoxEditorName.Text, textBoxEditorApplication.Text, textBoxEditorArguments.Text);
            this.Close();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            RunEditor(textBoxEditorName.Text, textBoxEditorApplication.Text, textBoxEditorArguments.Text);
            this.Close();
        }

        private void AddEditor(string name, string application, string arguments)
        {
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

            string selectedFile = FileSystem.GetImageFilePath(Slideshow.SelectedSlide, Slideshow.SelectedScreen == 0 ? 1 : Slideshow.SelectedScreen);

            if (File.Exists(selectedFile))
            {
                Process.Start(application, arguments.Replace("%screenshot%", selectedFile));
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
