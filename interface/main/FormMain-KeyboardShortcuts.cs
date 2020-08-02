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

                if (Convert.ToBoolean(Settings.User.GetByKey("BoolUseKeyboardShortcuts", DefaultSettings.BoolUseKeyboardShortcuts).Value.ToString()))
                {
                    string keyboardShortcutStartScreenCaptureModifier1UserSetting = Settings.User.GetByKey("StringKeyboardShortcutStartScreenCaptureModifier1", DefaultSettings.StringKeyboardShortcutStartScreenCaptureModifier1).Value.ToString();
                    string keyboardShortcutStartScreenCaptureModifier2UserSetting = Settings.User.GetByKey("StringKeyboardShortcutStartScreenCaptureModifier2", DefaultSettings.StringKeyboardShortcutStartScreenCaptureModifier2).Value.ToString();
                    _keyboardShortcutStartScreenCaptureKeyUserSetting = Settings.User.GetByKey("StringKeyboardShortcutStartScreenCaptureKey", DefaultSettings.StringKeyboardShortcutStartScreenCaptureKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutStartScreenCaptureModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutStartScreenCaptureModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutStartScreenCaptureModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutStartScreenCaptureModifier2UserSetting);

                    string keyboardShortcutStopScreenCaptureModifier1UserSetting = Settings.User.GetByKey("StringKeyboardShortcutStopScreenCaptureModifier1", DefaultSettings.StringKeyboardShortcutStopScreenCaptureModifier1).Value.ToString();
                    string keyboardShortcutStopScreenCaptureModifier2UserSetting = Settings.User.GetByKey("StringKeyboardShortcutStopScreenCaptureModifier2", DefaultSettings.StringKeyboardShortcutStopScreenCaptureModifier2).Value.ToString();
                    _keyboardShortcutStopScreenCaptureKeyUserSetting = Settings.User.GetByKey("StringKeyboardShortcutStopScreenCaptureKey", DefaultSettings.StringKeyboardShortcutStopScreenCaptureKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutStopScreenCaptureModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutStopScreenCaptureModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutStopScreenCaptureModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutStopScreenCaptureModifier2UserSetting);

                    string keyboardShortcutCaptureNowArchiveModifier1UserSetting = Settings.User.GetByKey("StringKeyboardShortcutCaptureNowArchiveModifier1", DefaultSettings.StringKeyboardShortcutCaptureNowArchiveModifier1).Value.ToString();
                    string keyboardShortcutCaptureNowArchiveModifier2UserSetting = Settings.User.GetByKey("StringKeyboardShortcutCaptureNowArchiveModifier2", DefaultSettings.StringKeyboardShortcutCaptureNowArchiveModifier2).Value.ToString();
                    _keyboardShortcutCaptureNowArchiveKeyUserSetting = Settings.User.GetByKey("StringKeyboardShortcutCaptureNowArchiveKey", DefaultSettings.StringKeyboardShortcutCaptureNowArchiveKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutCaptureNowArchiveModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutCaptureNowArchiveModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutCaptureNowArchiveModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutCaptureNowArchiveModifier2UserSetting);

                    string keyboardShortcutCaptureNowEditModifier1UserSetting = Settings.User.GetByKey("StringKeyboardShortcutCaptureNowEditModifier1", DefaultSettings.StringKeyboardShortcutCaptureNowEditModifier1).Value.ToString();
                    string keyboardShortcutCaptureNowEditModifier2UserSetting = Settings.User.GetByKey("StringKeyboardShortcutCaptureNowEditModifier2", DefaultSettings.StringKeyboardShortcutCaptureNowEditModifier2).Value.ToString();
                    _keyboardShortcutCaptureNowEditKeyUserSetting = Settings.User.GetByKey("StringKeyboardShortcutCaptureNowEditKey", DefaultSettings.StringKeyboardShortcutCaptureNowEditKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutCaptureNowEditModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutCaptureNowEditModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutCaptureNowEditModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutCaptureNowEditModifier2UserSetting);

                    string keyboardShortcutRegionSelectClipboardModifier1UserSetting = Settings.User.GetByKey("StringKeyboardShortcutRegionSelectClipboardModifier1", DefaultSettings.StringKeyboardShortcutRegionSelectClipboardModifier1).Value.ToString();
                    string keyboardShortcutRegionSelectClipboardModifier2UserSetting = Settings.User.GetByKey("StringKeyboardShortcutRegionSelectClipboardModifier2", DefaultSettings.StringKeyboardShortcutRegionSelectClipboardModifier2).Value.ToString();
                    _keyboardShortcutRegionSelectClipboardKeyUserSetting = Settings.User.GetByKey("StringKeyboardShortcutRegionSelectClipboardKey", DefaultSettings.StringKeyboardShortcutRegionSelectClipboardKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutRegionSelectClipboardModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutRegionSelectClipboardModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutRegionSelectClipboardModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutRegionSelectClipboardModifier2UserSetting);

                    string keyboardShortcutRegionSelectAutoSaveModifier1UserSetting = Settings.User.GetByKey("StringKeyboardShortcutRegionSelectAutoSaveModifier1", DefaultSettings.StringKeyboardShortcutRegionSelectAutoSaveModifier1).Value.ToString();
                    string keyboardShortcutRegionSelectAutoSaveModifier2UserSetting = Settings.User.GetByKey("StringKeyboardShortcutRegionSelectAutoSaveModifier2", DefaultSettings.StringKeyboardShortcutRegionSelectAutoSaveModifier2).Value.ToString();
                    _keyboardShortcutRegionSelectAutoSaveKeyUserSetting = Settings.User.GetByKey("StringKeyboardShortcutRegionSelectAutoSaveKey", DefaultSettings.StringKeyboardShortcutRegionSelectAutoSaveKey).Value.ToString();
                    AutoScreenCapture.ModifierKeys keyboardShortcutRegionSelectAutoSaveModifier1 = GetModifierKeyFromUserSetting(keyboardShortcutRegionSelectAutoSaveModifier1UserSetting);
                    AutoScreenCapture.ModifierKeys keyboardShortcutRegionSelectAutoSaveModifier2 = GetModifierKeyFromUserSetting(keyboardShortcutRegionSelectAutoSaveModifier2UserSetting);

                    _hotKeyMap.RegisterHotKey(keyboardShortcutStartScreenCaptureModifier1 | keyboardShortcutStartScreenCaptureModifier2, GetKeyFromUserSetting(_keyboardShortcutStartScreenCaptureKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutStopScreenCaptureModifier1 | keyboardShortcutStartScreenCaptureModifier2, GetKeyFromUserSetting(_keyboardShortcutStopScreenCaptureKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutCaptureNowArchiveModifier1 | keyboardShortcutCaptureNowArchiveModifier2, GetKeyFromUserSetting(_keyboardShortcutCaptureNowArchiveKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutCaptureNowEditModifier1 | keyboardShortcutCaptureNowEditModifier2, GetKeyFromUserSetting(_keyboardShortcutCaptureNowEditKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutRegionSelectClipboardModifier1 | keyboardShortcutRegionSelectClipboardModifier2, GetKeyFromUserSetting(_keyboardShortcutRegionSelectClipboardKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutRegionSelectAutoSaveModifier1 | keyboardShortcutRegionSelectAutoSaveModifier2, GetKeyFromUserSetting(_keyboardShortcutRegionSelectAutoSaveKeyUserSetting));

                    toolStripMenuItemStartScreenCapture.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutStartScreenCaptureModifier1UserSetting, keyboardShortcutStartScreenCaptureModifier2UserSetting, _keyboardShortcutStartScreenCaptureKeyUserSetting);
                    toolStripMenuItemStopScreenCapture.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutStopScreenCaptureModifier1UserSetting, keyboardShortcutStopScreenCaptureModifier2UserSetting, _keyboardShortcutStopScreenCaptureKeyUserSetting);
                    toolStripMenuItemCaptureNowArchive.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutCaptureNowArchiveModifier1UserSetting, keyboardShortcutCaptureNowArchiveModifier2UserSetting, _keyboardShortcutCaptureNowArchiveKeyUserSetting);
                    toolStripMenuItemCaptureNowEdit.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutCaptureNowEditModifier1UserSetting, keyboardShortcutCaptureNowEditModifier2UserSetting, _keyboardShortcutCaptureNowEditKeyUserSetting);
                    toolStripMenuItemRegionSelectClipboard.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutRegionSelectClipboardModifier1UserSetting, keyboardShortcutRegionSelectClipboardModifier2UserSetting, _keyboardShortcutRegionSelectClipboardKeyUserSetting);
                    toolStripMenuItemRegionSelectAutoSave.ShortcutKeys = GetKeysFromUserSettings(keyboardShortcutRegionSelectAutoSaveModifier1UserSetting, keyboardShortcutRegionSelectAutoSaveModifier2UserSetting, _keyboardShortcutRegionSelectAutoSaveKeyUserSetting);
                }
                else
                {
                    toolStripMenuItemStartScreenCapture.ShortcutKeys = Keys.None;
                    toolStripMenuItemStopScreenCapture.ShortcutKeys = Keys.None;
                    toolStripMenuItemCaptureNowArchive.ShortcutKeys = Keys.None;
                    toolStripMenuItemCaptureNowEdit.ShortcutKeys = Keys.None;
                    toolStripMenuItemRegionSelectClipboard.ShortcutKeys = Keys.None;
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
        }
    }
}