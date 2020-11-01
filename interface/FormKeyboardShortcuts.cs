//-----------------------------------------------------------------------
// <copyright file="FormKeyboardShortcuts.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for handling labels.</summary>
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
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormKeyboardShortcuts : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public FormKeyboardShortcuts()
        {
            InitializeComponent();
        }

        private void FormKeyboardShortcuts_Load(object sender, EventArgs e)
        {
            checkBoxUseKeyboardShortcuts.Focus();

            checkBoxUseKeyboardShortcuts.Checked = Convert.ToBoolean(Settings.User.GetByKey("UseKeyboardShortcuts", DefaultSettings.UseKeyboardShortcuts).Value);
            
            comboBoxKeyboardShortcutStartScreenCaptureModifier1.Items.Clear();
            comboBoxKeyboardShortcutStartScreenCaptureModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutStartScreenCaptureModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutStartScreenCaptureModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutStartScreenCaptureModifier2.Items.Clear();
            comboBoxKeyboardShortcutStartScreenCaptureModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutStartScreenCaptureModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutStartScreenCaptureModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutStopScreenCaptureModifier1.Items.Clear();
            comboBoxKeyboardShortcutStopScreenCaptureModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutStopScreenCaptureModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutStopScreenCaptureModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutStopScreenCaptureModifier2.Items.Clear();
            comboBoxKeyboardShortcutStopScreenCaptureModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutStopScreenCaptureModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutStopScreenCaptureModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Items.Clear();
            comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Items.Clear();
            comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutCaptureNowEditModifier1.Items.Clear();
            comboBoxKeyboardShortcutCaptureNowEditModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutCaptureNowEditModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutCaptureNowEditModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutCaptureNowEditModifier2.Items.Clear();
            comboBoxKeyboardShortcutCaptureNowEditModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutCaptureNowEditModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutCaptureNowEditModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Items.Clear();
            comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Items.Clear();
            comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Items.Clear();
            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Items.Clear();
            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutRegionSelectEditModifier1.Items.Clear();
            comboBoxKeyboardShortcutRegionSelectEditModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutRegionSelectEditModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutRegionSelectEditModifier1.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            comboBoxKeyboardShortcutRegionSelectEditModifier2.Items.Clear();
            comboBoxKeyboardShortcutRegionSelectEditModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Alt.ToString());
            comboBoxKeyboardShortcutRegionSelectEditModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Control.ToString());
            comboBoxKeyboardShortcutRegionSelectEditModifier2.Items.Add(AutoScreenCapture.ModifierKeys.Shift.ToString());

            // Map the modifier key enum value from the provided user setting to the combo box control's selected index.
            comboBoxKeyboardShortcutStartScreenCaptureModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutStartScreenCaptureModifier1, Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureModifier1", DefaultSettings.KeyboardShortcutStartScreenCaptureModifier1).Value.ToString());
            comboBoxKeyboardShortcutStartScreenCaptureModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutStartScreenCaptureModifier2, Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureModifier2", DefaultSettings.KeyboardShortcutStartScreenCaptureModifier2).Value.ToString());
            comboBoxKeyboardShortcutStopScreenCaptureModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutStopScreenCaptureModifier1, Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureModifier1", DefaultSettings.KeyboardShortcutStopScreenCaptureModifier1).Value.ToString());
            comboBoxKeyboardShortcutStopScreenCaptureModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutStopScreenCaptureModifier2, Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureModifier2", DefaultSettings.KeyboardShortcutStopScreenCaptureModifier2).Value.ToString());
            comboBoxKeyboardShortcutCaptureNowArchiveModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutCaptureNowArchiveModifier1, Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveModifier1", DefaultSettings.KeyboardShortcutCaptureNowArchiveModifier1).Value.ToString());
            comboBoxKeyboardShortcutCaptureNowArchiveModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutCaptureNowArchiveModifier2, Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveModifier2", DefaultSettings.KeyboardShortcutCaptureNowArchiveModifier2).Value.ToString());
            comboBoxKeyboardShortcutCaptureNowEditModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutCaptureNowEditModifier1, Settings.User.GetByKey("KeyboardShortcutCaptureNowEditModifier1", DefaultSettings.KeyboardShortcutCaptureNowEditModifier1).Value.ToString());
            comboBoxKeyboardShortcutCaptureNowEditModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutCaptureNowEditModifier2, Settings.User.GetByKey("KeyboardShortcutCaptureNowEditModifier2", DefaultSettings.KeyboardShortcutCaptureNowEditModifier2).Value.ToString());
            comboBoxKeyboardShortcutRegionSelectClipboardModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectClipboardModifier1, Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardModifier1", DefaultSettings.KeyboardShortcutRegionSelectClipboardModifier1).Value.ToString());
            comboBoxKeyboardShortcutRegionSelectClipboardModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectClipboardModifier2, Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardModifier2", DefaultSettings.KeyboardShortcutRegionSelectClipboardModifier2).Value.ToString());
            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1, Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveModifier1", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveModifier1).Value.ToString());
            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2, Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveModifier2", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveModifier2).Value.ToString());
            comboBoxKeyboardShortcutRegionSelectEditModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectEditModifier1, Settings.User.GetByKey("KeyboardShortcutRegionSelectEditModifier1", DefaultSettings.KeyboardShortcutRegionSelectEditModifier1).Value.ToString());
            comboBoxKeyboardShortcutRegionSelectEditModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectEditModifier2, Settings.User.GetByKey("KeyboardShortcutRegionSelectEditModifier2", DefaultSettings.KeyboardShortcutRegionSelectEditModifier2).Value.ToString());

            textBoxKeyboardShortcutStartScreenCaptureKey.Text = Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureKey", DefaultSettings.KeyboardShortcutStartScreenCaptureKey).Value.ToString().ToUpper();
            textBoxKeyboardShortcutStopScreenCaptureKey.Text = Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureKey", DefaultSettings.KeyboardShortcutStopScreenCaptureKey).Value.ToString().ToUpper();
            textBoxKeyboardShortcutCaptureNowArchiveKey.Text = Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveKey", DefaultSettings.KeyboardShortcutCaptureNowArchiveKey).Value.ToString().ToUpper();
            textBoxKeyboardShortcutCaptureNowEditKey.Text = Settings.User.GetByKey("KeyboardShortcutCaptureNowEditKey", DefaultSettings.KeyboardShortcutCaptureNowEditKey).Value.ToString().ToUpper();
            textBoxKeyboardShortcutRegionSelectClipboardKey.Text = Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardKey", DefaultSettings.KeyboardShortcutRegionSelectClipboardKey).Value.ToString().ToUpper();
            textBoxKeyboardShortcutRegionSelectAutoSaveKey.Text = Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveKey", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveKey).Value.ToString().ToUpper();
            textBoxKeyboardShortcutRegionSelectEditKey.Text = Settings.User.GetByKey("KeyboardShortcutRegionSelectEditKey", DefaultSettings.KeyboardShortcutRegionSelectEditKey).Value.ToString().ToUpper();
        }

        private int MapModifierKeyFromUserSetting(ComboBox comboBox, string userSetting)
        {
            AutoScreenCapture.ModifierKeys modifierKey = GetModifierKeyFromUserSetting(userSetting);

            if (modifierKey != AutoScreenCapture.ModifierKeys.None)
            {
                return GetComboBoxIndex(comboBox, userSetting);
            }

            return 0;
        }

        private AutoScreenCapture.ModifierKeys GetModifierKeyFromUserSetting(string userSetting)
        {
            AutoScreenCapture.ModifierKeys parsedModifierKey;

            if (Enum.TryParse(userSetting, false, out parsedModifierKey))
            {
                return parsedModifierKey;
            }

            return AutoScreenCapture.ModifierKeys.None;
        }

        private int GetComboBoxIndex(ComboBox comboBox, string userSetting)
        {
            return comboBox.Items.IndexOf(userSetting);
        }

        private void checkBoxUseKeyboardShortcuts_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseKeyboardShortcuts.Checked)
            {
                // Start Screen Capture
                labelStartScreenCapture.Enabled = true;
                comboBoxKeyboardShortcutStartScreenCaptureModifier1.Enabled = true;
                comboBoxKeyboardShortcutStartScreenCaptureModifier2.Enabled = true;
                textBoxKeyboardShortcutStartScreenCaptureKey.Enabled = true;

                // Stop Screen Capture
                labelStopScreenCapture.Enabled = true;
                comboBoxKeyboardShortcutStopScreenCaptureModifier1.Enabled = true;
                comboBoxKeyboardShortcutStopScreenCaptureModifier2.Enabled = true;
                textBoxKeyboardShortcutStopScreenCaptureKey.Enabled = true;

                // Capture Now / Archive
                labelCaptureNowArchive.Enabled = true;
                comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Enabled = true;
                comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Enabled = true;
                textBoxKeyboardShortcutCaptureNowArchiveKey.Enabled = true;

                // Capture Now / Edit
                labelCaptureNowEdit.Enabled = true;
                comboBoxKeyboardShortcutCaptureNowEditModifier1.Enabled = true;
                comboBoxKeyboardShortcutCaptureNowEditModifier2.Enabled = true;
                textBoxKeyboardShortcutCaptureNowEditKey.Enabled = true;

                // Region Select / Clipboard
                labelRegionSelectClipboard.Enabled = true;
                comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Enabled = true;
                comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Enabled = true;
                textBoxKeyboardShortcutRegionSelectClipboardKey.Enabled = true;

                // Region Select / Auto Save
                labelRegionSelectAutoSave.Enabled = true;
                comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Enabled = true;
                comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Enabled = true;
                textBoxKeyboardShortcutRegionSelectAutoSaveKey.Enabled = true;

                // Region Select / Edit
                labelRegionSelectEdit.Enabled = true;
                comboBoxKeyboardShortcutRegionSelectEditModifier1.Enabled = true;
                comboBoxKeyboardShortcutRegionSelectEditModifier2.Enabled = true;
                textBoxKeyboardShortcutRegionSelectEditKey.Enabled = true;
            }
            else
            {
                // Start Screen Capture
                labelStartScreenCapture.Enabled = false;
                comboBoxKeyboardShortcutStartScreenCaptureModifier1.Enabled = false;
                comboBoxKeyboardShortcutStartScreenCaptureModifier2.Enabled = false;
                textBoxKeyboardShortcutStartScreenCaptureKey.Enabled = false;

                // Stop Screen Capture
                labelStopScreenCapture.Enabled = false;
                comboBoxKeyboardShortcutStopScreenCaptureModifier1.Enabled = false;
                comboBoxKeyboardShortcutStopScreenCaptureModifier2.Enabled = false;
                textBoxKeyboardShortcutStopScreenCaptureKey.Enabled = false;

                // Capture Now / Archive
                labelCaptureNowArchive.Enabled = false;
                comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Enabled = false;
                comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Enabled = false;
                textBoxKeyboardShortcutCaptureNowArchiveKey.Enabled = false;

                // Capture Now / Edit
                labelCaptureNowEdit.Enabled = false;
                comboBoxKeyboardShortcutCaptureNowEditModifier1.Enabled = false;
                comboBoxKeyboardShortcutCaptureNowEditModifier2.Enabled = false;
                textBoxKeyboardShortcutCaptureNowEditKey.Enabled = false;

                // Region Select / Clipboard
                labelRegionSelectClipboard.Enabled = false;
                comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Enabled = false;
                comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Enabled = false;
                textBoxKeyboardShortcutRegionSelectClipboardKey.Enabled = false;

                // Region Select / Auto Save
                labelRegionSelectAutoSave.Enabled = false;
                comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Enabled = false;
                comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Enabled = false;
                textBoxKeyboardShortcutRegionSelectAutoSaveKey.Enabled = false;

                // Region Select / Edit
                labelRegionSelectEdit.Enabled = false;
                comboBoxKeyboardShortcutRegionSelectEditModifier1.Enabled = false;
                comboBoxKeyboardShortcutRegionSelectEditModifier2.Enabled = false;
                textBoxKeyboardShortcutRegionSelectEditKey.Enabled = false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxKeyboardShortcutStartScreenCaptureModifier1.Text.Equals(comboBoxKeyboardShortcutStartScreenCaptureModifier2.Text) ||
                comboBoxKeyboardShortcutStopScreenCaptureModifier1.Text.Equals(comboBoxKeyboardShortcutStopScreenCaptureModifier2.Text) ||
                comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Text.Equals(comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Text) ||
                comboBoxKeyboardShortcutCaptureNowEditModifier1.Text.Equals(comboBoxKeyboardShortcutCaptureNowEditModifier2.Text) ||
                comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Text.Equals(comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Text) ||
                comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Text.Equals(comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Text) ||
                comboBoxKeyboardShortcutRegionSelectEditModifier1.Text.Equals(comboBoxKeyboardShortcutRegionSelectEditModifier2.Text))
            {
                MessageBox.Show("The first and second modifier keys (such as Alt, Control, and Shift) cannot equal each other.", "Equal Modifier Keys", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Settings.User.SetValueByKey("UseKeyboardShortcuts", checkBoxUseKeyboardShortcuts.Checked.ToString());

                Settings.User.SetValueByKey("KeyboardShortcutStartScreenCaptureModifier1", comboBoxKeyboardShortcutStartScreenCaptureModifier1.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutStartScreenCaptureModifier2", comboBoxKeyboardShortcutStartScreenCaptureModifier2.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutStopScreenCaptureModifier1", comboBoxKeyboardShortcutStopScreenCaptureModifier1.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutStopScreenCaptureModifier2", comboBoxKeyboardShortcutStopScreenCaptureModifier2.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutCaptureNowArchiveModifier1", comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutCaptureNowArchiveModifier2", comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutCaptureNowEditModifier1", comboBoxKeyboardShortcutCaptureNowEditModifier1.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutCaptureNowEditModifier2", comboBoxKeyboardShortcutCaptureNowEditModifier2.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutRegionSelectClipboardModifier1", comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutRegionSelectClipboardModifier2", comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutRegionSelectAutoSaveModifier1", comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutRegionSelectAutoSaveModifier2", comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutRegionSelectEditModifier1", comboBoxKeyboardShortcutRegionSelectEditModifier1.Text.ToString());
                Settings.User.SetValueByKey("KeyboardShortcutRegionSelectEditModifier2", comboBoxKeyboardShortcutRegionSelectEditModifier2.Text.ToString());

                Settings.User.SetValueByKey("KeyboardShortcutStartScreenCaptureKey", textBoxKeyboardShortcutStartScreenCaptureKey.Text.ToString().ToUpper());
                Settings.User.SetValueByKey("KeyboardShortcutStopScreenCaptureKey", textBoxKeyboardShortcutStopScreenCaptureKey.Text.ToString().ToUpper());
                Settings.User.SetValueByKey("KeyboardShortcutCaptureNowArchiveKey", textBoxKeyboardShortcutCaptureNowArchiveKey.Text.ToString().ToUpper());
                Settings.User.SetValueByKey("KeyboardShortcutCaptureNowEditKey", textBoxKeyboardShortcutCaptureNowEditKey.Text.ToString().ToUpper());
                Settings.User.SetValueByKey("KeyboardShortcutRegionSelectClipboardKey", textBoxKeyboardShortcutRegionSelectClipboardKey.Text.ToString().ToUpper());
                Settings.User.SetValueByKey("KeyboardShortcutRegionSelectAutoSaveKey", textBoxKeyboardShortcutRegionSelectAutoSaveKey.Text.ToString().ToUpper());
                Settings.User.SetValueByKey("KeyboardShortcutRegionSelectEditKey", textBoxKeyboardShortcutRegionSelectEditKey.Text.ToString().ToUpper());

                Settings.User.Save();

                DialogResult = DialogResult.OK;

                Close();
            }
        }
    }
}
