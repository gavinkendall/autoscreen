//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.0
// autoscreen.FormEditor.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Sunday, 25 March 2018

using System;
using System.Windows.Forms;

namespace autoscreen
{
    public partial class FormEditor : Form
    {
        public Editor EditorObject { get; set; }

        private readonly string defaultArguments = "%screenshot%";

        public FormEditor()
        {
            InitializeComponent();
        }

        private void FormEditor_Load(object sender, EventArgs e)
        {
            if (EditorObject != null)
            {
                Text = "Change Editor";

                textBoxEditorName.Text = EditorObject.Name;
                textBoxEditorApplication.Text = EditorObject.Application;
                textBoxEditorArguments.Text = EditorObject.Arguments;
            }
            else
            {
                Text = "Add New Editor ...";

                textBoxEditorName.Text = string.Empty;
                textBoxEditorApplication.Text = string.Empty;
                textBoxEditorArguments.Text = defaultArguments;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (EditorObject != null)
            {
                ChangeEditor();
            }
            else
            {
                AddNewEditor();
            }
        }

        private void AddNewEditor()
        {
            if (InputValid())
            {
                TrimInput();

                EditorCollection.Add(new Editor(textBoxEditorName.Text, textBoxEditorApplication.Text, textBoxEditorArguments.Text));

                Okay();
            }
        }

        private void ChangeEditor()
        {
            if (InputChanged())
            {
                TrimInput();

                EditorCollection.GetByName(EditorObject.Name).Application = textBoxEditorApplication.Text;
                EditorCollection.GetByName(EditorObject.Name).Arguments = textBoxEditorArguments.Text;
                EditorCollection.GetByName(EditorObject.Name).Name = textBoxEditorName.Text;

                Okay();
            }
            else
            {
                Close();
            }
        }

        private void TrimInput()
        {
            if (InputValid())
            {
                textBoxEditorName.Text = textBoxEditorName.Text.Trim();
                textBoxEditorApplication.Text = textBoxEditorApplication.Text.Trim();
                textBoxEditorArguments.Text = textBoxEditorArguments.Text.Trim();
            }
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxEditorName.Text) &&
                !string.IsNullOrEmpty(textBoxEditorApplication.Text) &&
                !string.IsNullOrEmpty(textBoxEditorArguments.Text))
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
            if ((InputValid() && EditorObject != null) &&
                (!EditorObject.Name.Equals(textBoxEditorName.Text) ||
                    !EditorObject.Application.Equals(textBoxEditorApplication.Text) ||
                    !EditorObject.Arguments.Equals(textBoxEditorArguments.Text)))
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

        private void buttonChooseEditor_Click(object sender, EventArgs e)
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