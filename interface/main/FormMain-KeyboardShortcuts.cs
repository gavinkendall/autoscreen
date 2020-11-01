//-----------------------------------------------------------------------
// <copyright file="FormMain-KeyboardShortcuts.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>This is where we handle the key press even for a particular keyboard shortcut.</summary>
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
    public partial class FormMain : Form
    {
        private void toolStripSplitButtonKeyboardShortcuts_ButtonClick(object sender, EventArgs e)
        {
            _formKeyboardShortcuts.ShowDialog(this);

            if (_formKeyboardShortcuts.DialogResult == DialogResult.OK)
            {
                RegisterKeyboardShortcuts();
            }
        }

        private void RegisterKeyboardShortcuts()
        {
            try
            {
                _hotKeyMap.UnregisterHotKeys();

                if (Convert.ToBoolean(Settings.User.GetByKey("UseKeyboardShortcuts", DefaultSettings.UseKeyboardShortcuts).Value.ToString()))
                {
                    string keyboardShortcutStartScreenCaptureModifier1UserSetting = Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureModifier1", DefaultSettings.KeyboardShortcutStartScreenCaptureModifier1).Value.ToString();
                    string keyboardShortcutStartScreenCaptureModifier2UserSetting = Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureModifier2", DefaultSettings.KeyboardShortcutStartScreenCaptureModifier2).Value.ToString();
                    _keyboardShortcutStartScreenCaptureKeyUserSetting = Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureKey", DefaultSettings.KeyboardShortcutStartScreenCaptureKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutStartScreenCaptureModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutStartScreenCaptureModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutStartScreenCaptureModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutStartScreenCaptureModifier2UserSetting);

                    string keyboardShortcutStopScreenCaptureModifier1UserSetting = Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureModifier1", DefaultSettings.KeyboardShortcutStopScreenCaptureModifier1).Value.ToString();
                    string keyboardShortcutStopScreenCaptureModifier2UserSetting = Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureModifier2", DefaultSettings.KeyboardShortcutStopScreenCaptureModifier2).Value.ToString();
                    _keyboardShortcutStopScreenCaptureKeyUserSetting = Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureKey", DefaultSettings.KeyboardShortcutStopScreenCaptureKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutStopScreenCaptureModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutStopScreenCaptureModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutStopScreenCaptureModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutStopScreenCaptureModifier2UserSetting);

                    string keyboardShortcutCaptureNowArchiveModifier1UserSetting = Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveModifier1", DefaultSettings.KeyboardShortcutCaptureNowArchiveModifier1).Value.ToString();
                    string keyboardShortcutCaptureNowArchiveModifier2UserSetting = Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveModifier2", DefaultSettings.KeyboardShortcutCaptureNowArchiveModifier2).Value.ToString();
                    _keyboardShortcutCaptureNowArchiveKeyUserSetting = Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveKey", DefaultSettings.KeyboardShortcutCaptureNowArchiveKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutCaptureNowArchiveModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutCaptureNowArchiveModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutCaptureNowArchiveModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutCaptureNowArchiveModifier2UserSetting);

                    string keyboardShortcutCaptureNowEditModifier1UserSetting = Settings.User.GetByKey("KeyboardShortcutCaptureNowEditModifier1", DefaultSettings.KeyboardShortcutCaptureNowEditModifier1).Value.ToString();
                    string keyboardShortcutCaptureNowEditModifier2UserSetting = Settings.User.GetByKey("KeyboardShortcutCaptureNowEditModifier2", DefaultSettings.KeyboardShortcutCaptureNowEditModifier2).Value.ToString();
                    _keyboardShortcutCaptureNowEditKeyUserSetting = Settings.User.GetByKey("KeyboardShortcutCaptureNowEditKey", DefaultSettings.KeyboardShortcutCaptureNowEditKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutCaptureNowEditModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutCaptureNowEditModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutCaptureNowEditModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutCaptureNowEditModifier2UserSetting);

                    string keyboardShortcutRegionSelectClipboardModifier1UserSetting = Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardModifier1", DefaultSettings.KeyboardShortcutRegionSelectClipboardModifier1).Value.ToString();
                    string keyboardShortcutRegionSelectClipboardModifier2UserSetting = Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardModifier2", DefaultSettings.KeyboardShortcutRegionSelectClipboardModifier2).Value.ToString();
                    _keyboardShortcutRegionSelectClipboardKeyUserSetting = Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardKey", DefaultSettings.KeyboardShortcutRegionSelectClipboardKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutRegionSelectClipboardModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutRegionSelectClipboardModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutRegionSelectClipboardModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutRegionSelectClipboardModifier2UserSetting);

                    string keyboardShortcutRegionSelectAutoSaveModifier1UserSetting = Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveModifier1", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveModifier1).Value.ToString();
                    string keyboardShortcutRegionSelectAutoSaveModifier2UserSetting = Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveModifier2", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveModifier2).Value.ToString();
                    _keyboardShortcutRegionSelectAutoSaveKeyUserSetting = Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveKey", DefaultSettings.KeyboardShortcutRegionSelectAutoSaveKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutRegionSelectAutoSaveModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutRegionSelectAutoSaveModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutRegionSelectAutoSaveModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutRegionSelectAutoSaveModifier2UserSetting);

                    string keyboardShortcutRegionSelectEditModifier1UserSetting = Settings.User.GetByKey("KeyboardShortcutRegionSelectEditModifier1", DefaultSettings.KeyboardShortcutRegionSelectEditModifier1).Value.ToString();
                    string keyboardShortcutRegionSelectEditModifier2UserSetting = Settings.User.GetByKey("KeyboardShortcutRegionSelectEditModifier2", DefaultSettings.KeyboardShortcutRegionSelectEditModifier2).Value.ToString();
                    _keyboardShortcutRegionSelectEditKeyUserSetting = Settings.User.GetByKey("KeyboardShortcutRegionSelectEditKey", DefaultSettings.KeyboardShortcutRegionSelectEditKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutRegionSelectEditModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutRegionSelectEditModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutRegionSelectEditModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutRegionSelectEditModifier2UserSetting);

                    _hotKeyMap.RegisterHotKey(keyboardShortcutStartScreenCaptureModifier1 | keyboardShortcutStartScreenCaptureModifier2, GetKeyFromUserSetting(_keyboardShortcutStartScreenCaptureKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutStopScreenCaptureModifier1 | keyboardShortcutStartScreenCaptureModifier2, GetKeyFromUserSetting(_keyboardShortcutStopScreenCaptureKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutCaptureNowArchiveModifier1 | keyboardShortcutCaptureNowArchiveModifier2, GetKeyFromUserSetting(_keyboardShortcutCaptureNowArchiveKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutCaptureNowEditModifier1 | keyboardShortcutCaptureNowEditModifier2, GetKeyFromUserSetting(_keyboardShortcutCaptureNowEditKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutRegionSelectClipboardModifier1 | keyboardShortcutRegionSelectClipboardModifier2, GetKeyFromUserSetting(_keyboardShortcutRegionSelectClipboardKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutRegionSelectAutoSaveModifier1 | keyboardShortcutRegionSelectAutoSaveModifier2, GetKeyFromUserSetting(_keyboardShortcutRegionSelectAutoSaveKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutRegionSelectEditModifier1 | keyboardShortcutRegionSelectEditModifier2, GetKeyFromUserSetting(_keyboardShortcutRegionSelectEditKeyUserSetting));

                    toolStripMenuItemStartScreenCapture.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutStartScreenCaptureModifier1UserSetting, keyboardShortcutStartScreenCaptureModifier2UserSetting, _keyboardShortcutStartScreenCaptureKeyUserSetting);
                    toolStripMenuItemStopScreenCapture.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutStopScreenCaptureModifier1UserSetting, keyboardShortcutStopScreenCaptureModifier2UserSetting, _keyboardShortcutStopScreenCaptureKeyUserSetting);
                    toolStripMenuItemCaptureNowArchive.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutCaptureNowArchiveModifier1UserSetting, keyboardShortcutCaptureNowArchiveModifier2UserSetting, _keyboardShortcutCaptureNowArchiveKeyUserSetting);
                    toolStripMenuItemCaptureNowEdit.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutCaptureNowEditModifier1UserSetting, keyboardShortcutCaptureNowEditModifier2UserSetting, _keyboardShortcutCaptureNowEditKeyUserSetting);
                    toolStripMenuItemRegionSelectClipboard.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutRegionSelectClipboardModifier1UserSetting, keyboardShortcutRegionSelectClipboardModifier2UserSetting, _keyboardShortcutRegionSelectClipboardKeyUserSetting);
                    toolStripMenuItemRegionSelectAutoSave.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutRegionSelectAutoSaveModifier1UserSetting, keyboardShortcutRegionSelectAutoSaveModifier2UserSetting, _keyboardShortcutRegionSelectAutoSaveKeyUserSetting);
                    toolStripMenuItemRegionSelectEdit.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutRegionSelectEditModifier1UserSetting, keyboardShortcutRegionSelectEditModifier2UserSetting, _keyboardShortcutRegionSelectEditKeyUserSetting);
                }
                else
                {
                    toolStripMenuItemStartScreenCapture.ShortcutKeys = Keys.None;
                    toolStripMenuItemStopScreenCapture.ShortcutKeys = Keys.None;
                    toolStripMenuItemCaptureNowArchive.ShortcutKeys = Keys.None;
                    toolStripMenuItemCaptureNowEdit.ShortcutKeys = Keys.None;
                    toolStripMenuItemRegionSelectClipboard.ShortcutKeys = Keys.None;
                    toolStripMenuItemRegionSelectAutoSave.ShortcutKeys = Keys.None;
                    toolStripMenuItemRegionSelectEdit.ShortcutKeys = Keys.None;
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                Log.WriteExceptionMessage("FormMain-KeyboardShortcuts::RegisterKeyboardShortcuts", ex);
            }
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

        private Keys GetKeyFromUserSetting(string userSetting)
        {
            Keys parsedKey;

            if (Enum.TryParse(userSetting, false, out parsedKey))
            {
                return parsedKey;
            }

            return Keys.None;
        }

        private Keys GetKeysFromUserSettings(string key1, string key2, string key3)
        {
            Keys parsedKey1, parsedKey2, parsedKey3;

            if (Enum.TryParse(key1, false, out parsedKey1) &&
                Enum.TryParse(key2, false, out parsedKey2) &&
                Enum.TryParse(key3, false, out parsedKey3))
            {
                return parsedKey1 | parsedKey2 | parsedKey3;
            }

            return Keys.None;
        }

        private void hotKey_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutStartScreenCaptureKeyUserSetting))
            {
                StartScreenCapture();
            }

            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutStopScreenCaptureKeyUserSetting))
            {
                StopScreenCapture();
            }

            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutCaptureNowArchiveKeyUserSetting))
            {
                CaptureNowArchive();
            }

            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutCaptureNowEditKeyUserSetting))
            {
                CaptureNowEdit();
            }

            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutRegionSelectClipboardKeyUserSetting))
            {
                toolStripMenuItemRegionSelectClipboard_Click(sender, e);
            }

            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutRegionSelectAutoSaveKeyUserSetting))
            {
                toolStripMenuItemRegionSelectAutoSave_Click(sender, e);
            }

            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutRegionSelectEditKeyUserSetting))
            {
                toolStripMenuItemRegionSelectEdit_Click(sender, e);
            }
        }
    }
}