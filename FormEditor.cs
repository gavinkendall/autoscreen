//////////////////////////////////////////////////////////
// Auto Screen Capture 2.1.2
// autoscreen.FormEditor.cs
//
// Developed by Gavin Kendall
// Thursday, 15 May 2008 - Thursday, 5 April 2018

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace autoscreen
{
    public partial class FormEditor : Form
    {
        public Editor EditorObject { get; set; }

        private readonly string defaultArguments = "%screenshot%";

        private ComponentResourceManager resources = new ComponentResourceManager(typeof(FormEditor));

        public FormEditor()
        {
            InitializeComponent();
        }

        private void FormEditor_Load(object sender, EventArgs e)
        {
            textBoxEditorName.Focus();

            if (EditorObject != null)
            {
                Text = "Change Editor";
                Icon = Icon.ExtractAssociatedIcon(EditorObject.Application);

                textBoxEditorName.Text = EditorObject.Name;
                textBoxEditorApplication.Text = EditorObject.Application;
                textBoxEditorArguments.Text = EditorObject.Arguments;
            }
            else
            {
                Text = "Add New Editor ...";
                Icon = (Icon)(resources.GetObject("$this.Icon"));

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

                if (EditorCollection.GetByName(textBoxEditorName.Text) == null)
                {
                    EditorCollection.Add(new Editor(textBoxEditorName.Text, textBoxEditorApplication.Text, textBoxEditorArguments.Text));

                    Okay();
                }
                else
                {
                    MessageBox.Show("An editor with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void ChangeEditor()
        {
            if (InputChanged())
            {
                TrimInput();

                if (EditorCollection.GetByName(textBoxEditorName.Text) == null)
                {
                    EditorCollection.Get(EditorObject).Application = textBoxEditorApplication.Text;
                    EditorCollection.Get(EditorObject).Arguments = textBoxEditorArguments.Text;
                    EditorCollection.Get(EditorObject).Name = textBoxEditorName.Text;

                    Okay();
                }
                else
                {
                    MessageBox.Show("An editor with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                Icon = Icon.ExtractAssociatedIcon(openFileDialog.FileName);
                textBoxEditorApplication.Text = openFileDialog.FileName;
            }
        }
    }
}