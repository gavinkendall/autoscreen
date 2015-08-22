//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// autoscreen.FormAddEditor.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 27 November 2013

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

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

            string selectedFile = FileSystem.GetImageFilePath(Slideshow.SelectedSlide, Slideshow.SelectedScreen);

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
