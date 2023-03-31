//-----------------------------------------------------------------------
// <copyright file="FormMain-KeyboardShortcuts.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
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
        private void RegisterKeyboardShortcuts()
        {
            try
            {
                _hotKeyMap.UnregisterHotKeys();

                if (Convert.ToBoolean(_config.Settings.User.GetByKey("UseKeyboardShortcuts").Value.ToString()))
                {
                    _keyboardShortcutStartScreenCaptureModifier1UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureModifier1").Value.ToString();
                    _keyboardShortcutStartScreenCaptureModifier2UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureModifier2").Value.ToString();
                    _keyboardShortcutStartScreenCaptureKeyUserSetting = _config.Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureKey").Value.ToString();
                    ModifierKeys keyboardShortcutStartScreenCaptureModifier1 = GetModifierKeyFromUserSetting(_keyboardShortcutStartScreenCaptureModifier1UserSetting);
                    ModifierKeys keyboardShortcutStartScreenCaptureModifier2 = GetModifierKeyFromUserSetting(_keyboardShortcutStartScreenCaptureModifier2UserSetting);

                    _keyboardShortcutStopScreenCaptureModifier1UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureModifier1").Value.ToString();
                    _keyboardShortcutStopScreenCaptureModifier2UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureModifier2").Value.ToString();
                    _keyboardShortcutStopScreenCaptureKeyUserSetting = _config.Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureKey").Value.ToString();
                    ModifierKeys keyboardShortcutStopScreenCaptureModifier1 = GetModifierKeyFromUserSetting(_keyboardShortcutStopScreenCaptureModifier1UserSetting);
                    ModifierKeys keyboardShortcutStopScreenCaptureModifier2 = GetModifierKeyFromUserSetting(_keyboardShortcutStopScreenCaptureModifier2UserSetting);

                    _keyboardShortcutCaptureNowArchiveModifier1UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveModifier1").Value.ToString();
                    _keyboardShortcutCaptureNowArchiveModifier2UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveModifier2").Value.ToString();
                    _keyboardShortcutCaptureNowArchiveKeyUserSetting = _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveKey").Value.ToString();
                    ModifierKeys keyboardShortcutCaptureNowArchiveModifier1 = GetModifierKeyFromUserSetting(_keyboardShortcutCaptureNowArchiveModifier1UserSetting);
                    ModifierKeys keyboardShortcutCaptureNowArchiveModifier2 = GetModifierKeyFromUserSetting(_keyboardShortcutCaptureNowArchiveModifier2UserSetting);

                    _keyboardShortcutCaptureNowEditModifier1UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowEditModifier1").Value.ToString();
                    _keyboardShortcutCaptureNowEditModifier2UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowEditModifier2").Value.ToString();
                    _keyboardShortcutCaptureNowEditKeyUserSetting = _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowEditKey").Value.ToString();
                    ModifierKeys keyboardShortcutCaptureNowEditModifier1 = GetModifierKeyFromUserSetting(_keyboardShortcutCaptureNowEditModifier1UserSetting);
                    ModifierKeys keyboardShortcutCaptureNowEditModifier2 = GetModifierKeyFromUserSetting(_keyboardShortcutCaptureNowEditModifier2UserSetting);

                    _keyboardShortcutRegionSelectClipboardModifier1UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardModifier1").Value.ToString();
                    _keyboardShortcutRegionSelectClipboardModifier2UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardModifier2").Value.ToString();
                    _keyboardShortcutRegionSelectClipboardKeyUserSetting = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardKey").Value.ToString();
                    ModifierKeys keyboardShortcutRegionSelectClipboardModifier1 = GetModifierKeyFromUserSetting(_keyboardShortcutRegionSelectClipboardModifier1UserSetting);
                    ModifierKeys keyboardShortcutRegionSelectClipboardModifier2 = GetModifierKeyFromUserSetting(_keyboardShortcutRegionSelectClipboardModifier2UserSetting);

                    _keyboardShortcutRegionSelectAutoSaveModifier1UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveModifier1").Value.ToString();
                    _keyboardShortcutRegionSelectAutoSaveModifier2UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveModifier2").Value.ToString();
                    _keyboardShortcutRegionSelectAutoSaveKeyUserSetting = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveKey").Value.ToString();
                    ModifierKeys keyboardShortcutRegionSelectAutoSaveModifier1 = GetModifierKeyFromUserSetting(_keyboardShortcutRegionSelectAutoSaveModifier1UserSetting);
                    ModifierKeys keyboardShortcutRegionSelectAutoSaveModifier2 = GetModifierKeyFromUserSetting(_keyboardShortcutRegionSelectAutoSaveModifier2UserSetting);

                    _keyboardShortcutRegionSelectEditModifier1UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectEditModifier1").Value.ToString();
                    _keyboardShortcutRegionSelectEditModifier2UserSetting = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectEditModifier2").Value.ToString();
                    _keyboardShortcutRegionSelectEditKeyUserSetting = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectEditKey").Value.ToString();
                    ModifierKeys keyboardShortcutRegionSelectEditModifier1 = GetModifierKeyFromUserSetting(_keyboardShortcutRegionSelectEditModifier1UserSetting);
                    ModifierKeys keyboardShortcutRegionSelectEditModifier2 = GetModifierKeyFromUserSetting(_keyboardShortcutRegionSelectEditModifier2UserSetting);

                    _hotKeyMap.RegisterHotKey(keyboardShortcutStartScreenCaptureModifier1 | keyboardShortcutStartScreenCaptureModifier2, GetKeyFromUserSetting(_keyboardShortcutStartScreenCaptureKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutStopScreenCaptureModifier1 | keyboardShortcutStartScreenCaptureModifier2, GetKeyFromUserSetting(_keyboardShortcutStopScreenCaptureKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutCaptureNowArchiveModifier1 | keyboardShortcutCaptureNowArchiveModifier2, GetKeyFromUserSetting(_keyboardShortcutCaptureNowArchiveKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutCaptureNowEditModifier1 | keyboardShortcutCaptureNowEditModifier2, GetKeyFromUserSetting(_keyboardShortcutCaptureNowEditKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutRegionSelectClipboardModifier1 | keyboardShortcutRegionSelectClipboardModifier2, GetKeyFromUserSetting(_keyboardShortcutRegionSelectClipboardKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutRegionSelectAutoSaveModifier1 | keyboardShortcutRegionSelectAutoSaveModifier2, GetKeyFromUserSetting(_keyboardShortcutRegionSelectAutoSaveKeyUserSetting));
                    _hotKeyMap.RegisterHotKey(keyboardShortcutRegionSelectEditModifier1 | keyboardShortcutRegionSelectEditModifier2, GetKeyFromUserSetting(_keyboardShortcutRegionSelectEditKeyUserSetting));

                    toolStripMenuItemStartScreenCapture.ShortcutKeys = GetKeysFromUserSettings(_keyboardShortcutStartScreenCaptureModifier1UserSetting, _keyboardShortcutStartScreenCaptureModifier2UserSetting, _keyboardShortcutStartScreenCaptureKeyUserSetting);
                    toolStripMenuItemStopScreenCapture.ShortcutKeys = GetKeysFromUserSettings(_keyboardShortcutStopScreenCaptureModifier1UserSetting, _keyboardShortcutStopScreenCaptureModifier2UserSetting, _keyboardShortcutStopScreenCaptureKeyUserSetting);
                    toolStripMenuItemCaptureNowArchive.ShortcutKeys = GetKeysFromUserSettings(_keyboardShortcutCaptureNowArchiveModifier1UserSetting, _keyboardShortcutCaptureNowArchiveModifier2UserSetting, _keyboardShortcutCaptureNowArchiveKeyUserSetting);
                    toolStripMenuItemCaptureNowEdit.ShortcutKeys = GetKeysFromUserSettings(_keyboardShortcutCaptureNowEditModifier1UserSetting, _keyboardShortcutCaptureNowEditModifier2UserSetting, _keyboardShortcutCaptureNowEditKeyUserSetting);
                    toolStripMenuItemRegionSelectClipboard.ShortcutKeys = GetKeysFromUserSettings(_keyboardShortcutRegionSelectClipboardModifier1UserSetting, _keyboardShortcutRegionSelectClipboardModifier2UserSetting, _keyboardShortcutRegionSelectClipboardKeyUserSetting);
                    toolStripMenuItemRegionSelectClipboardAutoSave.ShortcutKeys = GetKeysFromUserSettings(_keyboardShortcutRegionSelectAutoSaveModifier1UserSetting, _keyboardShortcutRegionSelectAutoSaveModifier2UserSetting, _keyboardShortcutRegionSelectAutoSaveKeyUserSetting);
                    toolStripMenuItemRegionSelectClipboardAutoSaveEdit.ShortcutKeys = GetKeysFromUserSettings(_keyboardShortcutRegionSelectEditModifier1UserSetting, _keyboardShortcutRegionSelectEditModifier2UserSetting, _keyboardShortcutRegionSelectEditKeyUserSetting);
                }
                else
                {
                    toolStripMenuItemStartScreenCapture.ShortcutKeys = Keys.None;
                    toolStripMenuItemStopScreenCapture.ShortcutKeys = Keys.None;
                    toolStripMenuItemCaptureNowArchive.ShortcutKeys = Keys.None;
                    toolStripMenuItemCaptureNowEdit.ShortcutKeys = Keys.None;
                    toolStripMenuItemRegionSelectClipboard.ShortcutKeys = Keys.None;
                    toolStripMenuItemRegionSelectClipboardAutoSave.ShortcutKeys = Keys.None;
                    toolStripMenuItemRegionSelectClipboardAutoSaveEdit.ShortcutKeys = Keys.None;
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-KeyboardShortcuts::RegisterKeyboardShortcuts", ex);
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
            // Start Screen Capture
            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutStartScreenCaptureKeyUserSetting)
                && e.Modifier == GetModifierKeyFromUserSetting(_keyboardShortcutStartScreenCaptureModifier1UserSetting + ", " + _keyboardShortcutStartScreenCaptureModifier2UserSetting))
            {
                StartScreenCapture();
            }

            // Stop Screen Capture
            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutStopScreenCaptureKeyUserSetting)
                && e.Modifier == GetModifierKeyFromUserSetting(_keyboardShortcutStopScreenCaptureModifier1UserSetting + ", " + _keyboardShortcutStopScreenCaptureModifier2UserSetting))
            {
                StopScreenCapture();
            }

            // Capture Now Archive
            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutCaptureNowArchiveKeyUserSetting)
                && e.Modifier == GetModifierKeyFromUserSetting(_keyboardShortcutCaptureNowArchiveModifier1UserSetting + ", " + _keyboardShortcutCaptureNowArchiveModifier2UserSetting))
            {
                CaptureNowArchive();
            }

            // Capture Now Edit
            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutCaptureNowEditKeyUserSetting)
                && e.Modifier == GetModifierKeyFromUserSetting(_keyboardShortcutCaptureNowEditModifier1UserSetting + ", " + _keyboardShortcutCaptureNowEditModifier2UserSetting))
            {
                CaptureNowEdit();
            }

            // Region Select Clipboard
            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutRegionSelectClipboardKeyUserSetting)
                && e.Modifier == GetModifierKeyFromUserSetting(_keyboardShortcutRegionSelectClipboardModifier1UserSetting + ", " + _keyboardShortcutRegionSelectClipboardModifier2UserSetting))
            {
                toolStripMenuItemRegionSelectClipboard_Click(sender, e);
            }

            // Region Select Auto Save
            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutRegionSelectAutoSaveKeyUserSetting)
                && e.Modifier == GetModifierKeyFromUserSetting(_keyboardShortcutRegionSelectAutoSaveModifier1UserSetting + ", " + _keyboardShortcutRegionSelectAutoSaveModifier2UserSetting))
            {
                toolStripMenuItemRegionSelectClipboardAutoSave_Click(sender, e);
            }

            // Region Select Edit
            if (e.Key == GetKeyFromUserSetting(_keyboardShortcutRegionSelectEditKeyUserSetting)
                && e.Modifier == GetModifierKeyFromUserSetting(_keyboardShortcutRegionSelectEditModifier1UserSetting + ", " + _keyboardShortcutRegionSelectEditModifier2UserSetting))
            {
                toolStripMenuItemRegionSelectClipboardAutoSaveEdit_Click(sender, e);
            }
        }
    }
}