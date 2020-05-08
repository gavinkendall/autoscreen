//-----------------------------------------------------------------------
// <copyright file="FormEditor.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormEditor : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public EditorCollection EditorCollection { get; } = new EditorCollection();

        /// <summary>
        /// 
        /// </summary>
        public Editor EditorObject { get; set; }

        private readonly string defaultArguments = "%screenshot%";

        private ComponentResourceManager resources = new ComponentResourceManager(typeof(FormEditor));

        /// <summary>
        /// 
        /// </summary>
        public FormEditor()
        {
            InitializeComponent();
        }

        private void FormEditor_Load(object sender, EventArgs e)
        {
            checkBoxMakeDefaultEditor.Checked = false;

            if (EditorObject != null)
            {
                Text = "Change Editor";
                Icon = Icon.ExtractAssociatedIcon(EditorObject.Application);

                textBoxEditorName.Text = EditorObject.Name;
                textBoxEditorApplication.Text = EditorObject.Application;
                textBoxEditorArguments.Text = EditorObject.Arguments;

                string defaultEditor = Settings.User.GetByKey("StringDefaultEditor", defaultValue: string.Empty).Value.ToString();

                if (EditorObject.Name.Equals(defaultEditor))
                {
                    checkBoxMakeDefaultEditor.Checked = true;
                }
            }
            else
            {
                Text = "Add New Editor";
                Icon = (Icon)(resources.GetObject("$this.Icon"));

                textBoxEditorName.Text = "Editor " + (EditorCollection.Count + 1);
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
            if (checkBoxMakeDefaultEditor.Checked && !string.IsNullOrEmpty(textBoxEditorName.Text))
            {
                Settings.User.GetByKey("StringDefaultEditor", defaultValue: string.Empty).Value = textBoxEditorName.Text;
                Settings.User.Save();
            }

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

                if (ApplicationExists())
                {
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
                else
                {
                    MessageBox.Show($"Could not find \"{textBoxEditorApplication.Text}\".", "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid input for each field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeEditor()
        {
            if (InputValid())
            {
                if (NameChanged() || InputChanged())
                {
                    TrimInput();

                    if (ApplicationExists())
                    {
                        if (EditorCollection.GetByName(textBoxEditorName.Text) != null && NameChanged())
                        {
                            MessageBox.Show("An editor with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            EditorCollection.Get(EditorObject).Application = textBoxEditorApplication.Text;
                            EditorCollection.Get(EditorObject).Arguments = textBoxEditorArguments.Text;
                            EditorCollection.Get(EditorObject).Name = textBoxEditorName.Text;

                            Okay();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Could not find \"{textBoxEditorApplication.Text}\".", "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            textBoxEditorName.Text = textBoxEditorName.Text.Trim();
            textBoxEditorApplication.Text = textBoxEditorApplication.Text.Trim();
            textBoxEditorArguments.Text = textBoxEditorArguments.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxEditorName.Text) &&
                !string.IsNullOrEmpty(textBoxEditorApplication.Text))
            {
                return true;
            }

            return false;
        }

        private bool InputChanged()
        {
            if (EditorObject != null &&
                (!EditorObject.Application.Equals(textBoxEditorApplication.Text) ||
                    !EditorObject.Arguments.Equals(textBoxEditorArguments.Text)))
            {
                return true;
            }

            return false;
        }

        private bool NameChanged()
        {
            if (EditorObject != null &&
                !EditorObject.Name.Equals(textBoxEditorName.Text))
            {
                return true;
            }

            return false;
        }

        private bool ApplicationExists()
        {
            if (FileSystem.FileExists(textBoxEditorApplication.Text))
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

        private void buttonChooseEditor_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                FilterIndex = 0,
                Multiselect = false,
                AddExtension = false,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "Applications (*.exe)|*.exe|Batch Scripts (*.bat)|*.bat|PowerShell Scripts (*.ps1)|*.ps1|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Icon = Icon.ExtractAssociatedIcon(openFileDialog.FileName);
                textBoxEditorApplication.Text = openFileDialog.FileName;
            }
        }
    }
}