//-----------------------------------------------------------------------
// <copyright file="FormEditor.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form for adding a new editor or changing an existing editor.</summary>
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// The form for managing image editors.
    /// </summary>
    public partial class FormEditor : Form
    {
        private Log _log;
        private Config _config;
        private FileSystem _fileSystem;

        /// <summary>
        /// A collection of editors.
        /// </summary>
        public EditorCollection EditorCollection { get; } = new EditorCollection();

        /// <summary>
        /// The editor object to handle.
        /// </summary>
        public Editor EditorObject { get; set; }

        private readonly string defaultArguments = "%filepath%";

        private ComponentResourceManager resources = new ComponentResourceManager(typeof(FormEditor));

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public FormEditor(Config config, FileSystem fileSystem, Log log)
        {
            InitializeComponent();

            _log = log;
            _config = config;
            _fileSystem = fileSystem;
        }

        private void FormEditor_Load(object sender, EventArgs e)
        {
            textBoxName.Focus();

            HelpMessage("This is where to configure an image editor for editing screenshots");

            checkBoxMakeDefaultEditor.Checked = false;

            if (EditorObject != null)
            {
                Text = "Change Editor";

                if (!string.IsNullOrEmpty(EditorObject.Application) &&
                    _fileSystem.FileExists(EditorObject.Application))
                {
                    Icon = Icon.ExtractAssociatedIcon(EditorObject.Application);
                }
                else
                {
                    Icon = (Icon)(resources.GetObject("$this.Icon"));
                }

                textBoxName.Text = EditorObject.Name;
                textBoxApplication.Text = EditorObject.Application;
                textBoxArguments.Text = EditorObject.Arguments;

                string defaultEditor = _config.Settings.User.GetByKey("DefaultEditor", _config.Settings.DefaultSettings.DefaultEditor).Value.ToString();

                if (EditorObject.Name.Equals(defaultEditor))
                {
                    checkBoxMakeDefaultEditor.Checked = true;
                }

                textBoxNotes.Text = EditorObject.Notes;
            }
            else
            {
                Text = "Add New Editor";
                Icon = (Icon)(resources.GetObject("$this.Icon"));

                textBoxName.Text = "Editor " + (EditorCollection.Count + 1);
                textBoxApplication.Text = string.Empty;
                textBoxArguments.Text = defaultArguments;
                textBoxNotes.Text = string.Empty;
            }
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
            if (checkBoxMakeDefaultEditor.Checked && !string.IsNullOrEmpty(textBoxName.Text))
            {
                _config.Settings.User.GetByKey("DefaultEditor", _config.Settings.DefaultSettings.DefaultEditor).Value = textBoxName.Text;
                _config.Settings.User.Save(_config.Settings, _fileSystem);
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
                    if (EditorCollection.GetByName(textBoxName.Text) == null)
                    {
                        Editor editor = new Editor()
                        {
                            Name = textBoxName.Text,
                            Application = textBoxApplication.Text,
                            Arguments = textBoxArguments.Text,
                            Notes = textBoxNotes.Text
                        };

                        EditorCollection.Add(editor);

                        Okay();
                    }
                    else
                    {
                        MessageBox.Show("An editor with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"Could not find \"{textBoxApplication.Text}\".", "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        if (EditorCollection.GetByName(textBoxName.Text) != null && NameChanged())
                        {
                            MessageBox.Show("An editor with this name already exists.", "Duplicate Name Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            EditorCollection.Get(EditorObject).Application = textBoxApplication.Text;
                            EditorCollection.Get(EditorObject).Arguments = textBoxArguments.Text;
                            EditorCollection.Get(EditorObject).Name = textBoxName.Text;
                            EditorCollection.Get(EditorObject).Notes = textBoxNotes.Text;

                            Okay();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Could not find \"{textBoxApplication.Text}\".", "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            textBoxApplication.Text = textBoxApplication.Text.Trim();
            textBoxArguments.Text = textBoxArguments.Text.Trim();
            textBoxNotes.Text = textBoxNotes.Text.Trim();
        }

        private bool InputValid()
        {
            if (!string.IsNullOrEmpty(textBoxName.Text) &&
                !string.IsNullOrEmpty(textBoxApplication.Text))
            {
                return true;
            }

            return false;
        }

        private bool InputChanged()
        {
            if (EditorObject != null &&
                (!EditorObject.Application.Equals(textBoxApplication.Text) ||
                    !EditorObject.Arguments.Equals(textBoxArguments.Text)) ||
                    !EditorObject.Notes.Equals(textBoxNotes.Text))
            {
                return true;
            }

            return false;
        }

        private bool NameChanged()
        {
            if (EditorObject != null &&
                !EditorObject.Name.Equals(textBoxName.Text))
            {
                return true;
            }

            return false;
        }

        private bool ApplicationExists()
        {
            if (_fileSystem.FileExists(textBoxApplication.Text))
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
                if (!string.IsNullOrEmpty(openFileDialog.FileName) &&
                    _fileSystem.FileExists(openFileDialog.FileName))
                {
                    Icon = Icon.ExtractAssociatedIcon(openFileDialog.FileName);
                }
                else
                {
                    Icon = (Icon)(resources.GetObject("$this.Icon"));
                }

                textBoxApplication.Text = openFileDialog.FileName;
            }
        }

        private void textBoxName_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The name for the editor");
        }

        private void checkBoxMakeDefaultEditor_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("When checked it will make this editor the default editor");
        }

        private void textBoxApplication_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The path of the application or script to use when editing screenshots");
        }

        private void buttonChooseEditor_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("Browse for an application or script");
        }

        private void textBoxArguments_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("The command line arguments of the application. Include the %filepath% argument so the filepath of the screenshot can be passed to the application");
        }

        private void textBoxNotes_MouseHover(object sender, EventArgs e)
        {
            HelpMessage("An area for you to keep notes about the editor");
        }
    }
}