﻿//-----------------------------------------------------------------------
// <copyright file="FormSetup.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>The form to manage Setup options.</summary>
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
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormSetup : Form
    {
        private Log _log;
        private Security _security;
        private Config _config;
        private FileSystem _fileSystem;
        private ScreenCapture _screenCapture;
        private FormScreenCaptureStatusWithLabelSwitcher _formLabelSwitcher;
        private FormScreen _formScreen;
        private FormRegion _formRegion;
        private MacroTagCollection _macroTagCollection;
        private MacroParser _macroParser;
        private ScreenshotCollection _screenshotCollection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log">The log to use.</param>
        /// <param name="security">The securiy class to use.</param>
        /// <param name="config">The config class to use.</param>
        /// <param name="fileSystem">The file system to use.</param>
        /// <param name="screenCapture">The screen capture class to use.</param>
        /// <param name="formLabelSwitcher">The Label Switcher tool.</param>
        /// <param name="formScreen">The Screen form.</param>
        /// <param name="formRegion">The Region form.</param>
        /// <param name="macroTagCollection">The macro tags collection to use.</param>
        /// <param name="macroParser">The macro parser to use.</param>
        /// <param name="screenshotCollection">The screenshot collection to use.</param>
        public FormSetup(Log log, Security security, Config config, FileSystem fileSystem, ScreenCapture screenCapture, FormScreenCaptureStatusWithLabelSwitcher formLabelSwitcher, FormScreen formScreen, FormRegion formRegion, MacroTagCollection macroTagCollection, MacroParser macroParser, ScreenshotCollection screenshotCollection)
        {
            InitializeComponent();

            _log = log;
            _security = security;
            _config = config;
            _fileSystem = fileSystem;
            _screenCapture = screenCapture;
            _formLabelSwitcher = formLabelSwitcher;
            _formScreen = formScreen;
            _formRegion = formRegion;
            _macroTagCollection = macroTagCollection;
            _macroParser = macroParser;
            _screenshotCollection = screenshotCollection;
        }

        private void FormSetup_Load(object sender, EventArgs e)
        {
            // Screenshots Folder
            textBoxScreenshotsFolder.Text = _fileSystem.ScreenshotsFolder;

            // Filename Pattern
            textBoxFilenamePattern.Text = _fileSystem.FilenamePattern;

            // Macro Tags
            listBoxMacroTags.Items.Clear();

            foreach (MacroTag macroTag in _macroTagCollection)
            {
                listBoxMacroTags.Items.Add(macroTag.Name + " (" + macroTag.Description + ")");
            }

            checkBoxUseKeyboardShortcuts.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("UseKeyboardShortcuts", false, true).Value);

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
            comboBoxKeyboardShortcutStartScreenCaptureModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutStartScreenCaptureModifier1, _config.Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureModifier1").Value.ToString());
            comboBoxKeyboardShortcutStartScreenCaptureModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutStartScreenCaptureModifier2, _config.Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureModifier2").Value.ToString());
            comboBoxKeyboardShortcutStopScreenCaptureModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutStopScreenCaptureModifier1, _config.Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureModifier1").Value.ToString());
            comboBoxKeyboardShortcutStopScreenCaptureModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutStopScreenCaptureModifier2, _config.Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureModifier2").Value.ToString());
            comboBoxKeyboardShortcutCaptureNowArchiveModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutCaptureNowArchiveModifier1, _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveModifier1").Value.ToString());
            comboBoxKeyboardShortcutCaptureNowArchiveModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutCaptureNowArchiveModifier2, _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveModifier2").Value.ToString());
            comboBoxKeyboardShortcutCaptureNowEditModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutCaptureNowEditModifier1, _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowEditModifier1").Value.ToString());
            comboBoxKeyboardShortcutCaptureNowEditModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutCaptureNowEditModifier2, _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowEditModifier2").Value.ToString());
            comboBoxKeyboardShortcutRegionSelectClipboardModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectClipboardModifier1, _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardModifier1").Value.ToString());
            comboBoxKeyboardShortcutRegionSelectClipboardModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectClipboardModifier2, _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardModifier2").Value.ToString());
            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1, _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveModifier1").Value.ToString());
            comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2, _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveModifier2").Value.ToString());
            comboBoxKeyboardShortcutRegionSelectEditModifier1.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectEditModifier1, _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectEditModifier1").Value.ToString());
            comboBoxKeyboardShortcutRegionSelectEditModifier2.SelectedIndex = MapModifierKeyFromUserSetting(comboBoxKeyboardShortcutRegionSelectEditModifier2, _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectEditModifier2").Value.ToString());

            textBoxKeyboardShortcutStartScreenCaptureKey.Text = _config.Settings.User.GetByKey("KeyboardShortcutStartScreenCaptureKey").Value.ToString().ToUpper();
            textBoxKeyboardShortcutStopScreenCaptureKey.Text = _config.Settings.User.GetByKey("KeyboardShortcutStopScreenCaptureKey").Value.ToString().ToUpper();
            textBoxKeyboardShortcutCaptureNowArchiveKey.Text = _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowArchiveKey").Value.ToString().ToUpper();
            textBoxKeyboardShortcutCaptureNowEditKey.Text = _config.Settings.User.GetByKey("KeyboardShortcutCaptureNowEditKey").Value.ToString().ToUpper();
            textBoxKeyboardShortcutRegionSelectClipboardKey.Text = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectClipboardKey").Value.ToString().ToUpper();
            textBoxKeyboardShortcutRegionSelectAutoSaveKey.Text = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectAutoSaveKey").Value.ToString().ToUpper();
            textBoxKeyboardShortcutRegionSelectEditKey.Text = _config.Settings.User.GetByKey("KeyboardShortcutRegionSelectEditKey").Value.ToString().ToUpper();

            // Security
            textBoxPassphraseHash.Text = _config.Settings.User.GetByKey("Passphrase").Value.ToString();

            Setting passphraseLastUpdatedSetting = _config.Settings.User.GetByKey("PassphraseLastUpdated");

            // To maintain backwards compatibility with 2.4 when upgrading from 2.4 to 2.5 and using an old 2.4 version of autoscreen.conf
            if (passphraseLastUpdatedSetting != null)
            {
                string passphraseLastUpdated = _config.Settings.User.GetByKey("PassphraseLastUpdated").Value.ToString();
                labelLastUpdated.Text = "Last updated: " + passphraseLastUpdated;
            }

            // Optimize Screen Capture
            checkBoxOptimizeScreenCapture.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("OptimizeScreenCapture", false, true).Value);
            trackBarImageDiffTolerance.Value = Convert.ToInt32(_config.Settings.User.GetByKey("ImageDiffTolerance", 20, true).Value);
            labelSelectedImageDiffTolerance.Text = trackBarImageDiffTolerance.Value.ToString() + "%";
            CheckEnabledStatusOnOptimizeScreenCaptureControls();

            // Application Focus
            RefreshApplicationFocusList();

            HelpMessage("This is where to configure your screen capture settings");
        }

        private void FormSetup_Shown(object sender, EventArgs e)
        {
            bool applyScreenshotLabel = Convert.ToBoolean(_config.Settings.User.GetByKey("ApplyScreenshotLabel", false, true).Value);
            string screenshotLabel = _config.Settings.User.GetByKey("ScreenshotLabel", string.Empty, true).Value.ToString();

            checkBoxScreenshotLabel.Checked = applyScreenshotLabel;

            if (!string.IsNullOrEmpty(screenshotLabel))
            {
                if (!listBoxScreenshotLabel.Items.Contains(screenshotLabel))
                {
                    listBoxScreenshotLabel.Items.Add(screenshotLabel);
                }

                if (!_formLabelSwitcher.comboBoxLabels.Items.Contains(screenshotLabel))
                {
                    _formLabelSwitcher.comboBoxLabels.Items.Add(screenshotLabel);
                }

                listBoxScreenshotLabel.SelectedItem = screenshotLabel;
            }

            // To maintain backwards compatibility with 2.4 when upgrading from 2.4 to 2.5 and using an old 2.4 version of autoscreen.conf
            string imageFormat = "JPEG";
            Setting imageFormatSetting = _config.Settings.User.GetByKey("ImageFormat");

            if (imageFormatSetting != null)
            {
                imageFormat = _config.Settings.User.GetByKey("ImageFormat").Value.ToString();
            }

            // Image Format
            switch (imageFormat)
            {
                case "BMP":
                    radioButtonImageFormatBmp.Checked = true;
                    break;

                case "EMF":
                    radioButtonImageFormatEmf.Checked = true;
                    break;

                case "GIF":
                    radioButtonImageFormatGif.Checked = true;
                    break;

                case "JPEG":
                    radioButtonImageFormatJpeg.Checked = true;
                    break;

                case "PNG":
                    radioButtonImageFormatPng.Checked = true;
                    break;

                case "TIFF":
                    radioButtonImageFormatTiff.Checked = true;
                    break;

                case "WMF":
                    radioButtonImageFormatWmf.Checked = true;
                    break;
            }
        }

        private void HelpMessage(string message)
        {
            labelHelp.Text = "       " + message;
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
                _config.Settings.User.SetValueByKey("UseKeyboardShortcuts", checkBoxUseKeyboardShortcuts.Checked.ToString());

                _config.Settings.User.SetValueByKey("KeyboardShortcutStartScreenCaptureModifier1", comboBoxKeyboardShortcutStartScreenCaptureModifier1.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutStartScreenCaptureModifier2", comboBoxKeyboardShortcutStartScreenCaptureModifier2.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutStopScreenCaptureModifier1", comboBoxKeyboardShortcutStopScreenCaptureModifier1.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutStopScreenCaptureModifier2", comboBoxKeyboardShortcutStopScreenCaptureModifier2.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutCaptureNowArchiveModifier1", comboBoxKeyboardShortcutCaptureNowArchiveModifier1.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutCaptureNowArchiveModifier2", comboBoxKeyboardShortcutCaptureNowArchiveModifier2.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutCaptureNowEditModifier1", comboBoxKeyboardShortcutCaptureNowEditModifier1.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutCaptureNowEditModifier2", comboBoxKeyboardShortcutCaptureNowEditModifier2.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutRegionSelectClipboardModifier1", comboBoxKeyboardShortcutRegionSelectClipboardModifier1.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutRegionSelectClipboardModifier2", comboBoxKeyboardShortcutRegionSelectClipboardModifier2.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutRegionSelectAutoSaveModifier1", comboBoxKeyboardShortcutRegionSelectAutoSaveModifier1.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutRegionSelectAutoSaveModifier2", comboBoxKeyboardShortcutRegionSelectAutoSaveModifier2.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutRegionSelectEditModifier1", comboBoxKeyboardShortcutRegionSelectEditModifier1.Text.ToString());
                _config.Settings.User.SetValueByKey("KeyboardShortcutRegionSelectEditModifier2", comboBoxKeyboardShortcutRegionSelectEditModifier2.Text.ToString());

                _config.Settings.User.SetValueByKey("KeyboardShortcutStartScreenCaptureKey", textBoxKeyboardShortcutStartScreenCaptureKey.Text.ToString().ToUpper());
                _config.Settings.User.SetValueByKey("KeyboardShortcutStopScreenCaptureKey", textBoxKeyboardShortcutStopScreenCaptureKey.Text.ToString().ToUpper());
                _config.Settings.User.SetValueByKey("KeyboardShortcutCaptureNowArchiveKey", textBoxKeyboardShortcutCaptureNowArchiveKey.Text.ToString().ToUpper());
                _config.Settings.User.SetValueByKey("KeyboardShortcutCaptureNowEditKey", textBoxKeyboardShortcutCaptureNowEditKey.Text.ToString().ToUpper());
                _config.Settings.User.SetValueByKey("KeyboardShortcutRegionSelectClipboardKey", textBoxKeyboardShortcutRegionSelectClipboardKey.Text.ToString().ToUpper());
                _config.Settings.User.SetValueByKey("KeyboardShortcutRegionSelectAutoSaveKey", textBoxKeyboardShortcutRegionSelectAutoSaveKey.Text.ToString().ToUpper());
                _config.Settings.User.SetValueByKey("KeyboardShortcutRegionSelectEditKey", textBoxKeyboardShortcutRegionSelectEditKey.Text.ToString().ToUpper());

                _fileSystem.ScreenshotsFolder = textBoxScreenshotsFolder.Text;
                _config.Settings.User.SetValueByKey("ScreenshotsFolder", textBoxScreenshotsFolder.Text);

                _fileSystem.FilenamePattern = textBoxFilenamePattern.Text;
                _config.Settings.User.SetValueByKey("FilenamePattern", textBoxFilenamePattern.Text);

                _config.Settings.User.SetValueByKey("ApplyScreenshotLabel", checkBoxScreenshotLabel.Checked);

                if (radioButtonImageFormatBmp.Checked)
                {
                    ScreenCapture.ImageFormat = "BMP";
                    _config.Settings.User.SetValueByKey("ImageFormat", "BMP");
                }

                if (radioButtonImageFormatEmf.Checked)
                {
                    ScreenCapture.ImageFormat = "EMF";
                    _config.Settings.User.SetValueByKey("ImageFormat", "EMF");
                }

                if (radioButtonImageFormatGif.Checked)
                {
                    ScreenCapture.ImageFormat = "GIF";
                    _config.Settings.User.SetValueByKey("ImageFormat", "GIF");
                }

                if (radioButtonImageFormatJpeg.Checked)
                {
                    ScreenCapture.ImageFormat = "JPEG";
                    _config.Settings.User.SetValueByKey("ImageFormat", "JPEG");
                }

                if (radioButtonImageFormatPng.Checked)
                {
                    ScreenCapture.ImageFormat = "PNG";
                    _config.Settings.User.SetValueByKey("ImageFormat", "PNG");
                }

                if (radioButtonImageFormatTiff.Checked)
                {
                    ScreenCapture.ImageFormat = "TIFF";
                    _config.Settings.User.SetValueByKey("ImageFormat", "TIFF");
                }

                if (radioButtonImageFormatWmf.Checked)
                {
                    ScreenCapture.ImageFormat = "WMF";
                    _config.Settings.User.SetValueByKey("ImageFormat", "WMF");
                }

                // Optimize Screen Capture
                _config.Settings.User.SetValueByKey("OptimizeScreenCapture", checkBoxOptimizeScreenCapture.Checked);
                _config.Settings.User.SetValueByKey("ImageDiffTolerance", trackBarImageDiffTolerance.Value);

                _config.Settings.User.Save(_config.Settings, _fileSystem);

                DialogResult = DialogResult.OK;

                Close();
            }
        }

        private int MapModifierKeyFromUserSetting(ComboBox comboBox, string userSetting)
        {
            ModifierKeys modifierKey = GetModifierKeyFromUserSetting(userSetting);

            if (modifierKey != AutoScreenCapture.ModifierKeys.None)
            {
                return GetComboBoxIndex(comboBox, userSetting);
            }

            return 0;
        }

        private ModifierKeys GetModifierKeyFromUserSetting(string userSetting)
        {
            ModifierKeys parsedModifierKey;

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

        private void buttonApplicationFocusTest_Click(object sender, EventArgs e)
        {
            DoApplicationFocus();
        }

        private void buttonApplicationFocusRefresh_Click(object sender, EventArgs e)
        {
            RefreshApplicationFocusList();
        }

        /// <summary>
        /// Shows the necessary tab page.
        /// </summary>
        /// <param name="tabPageToShow">The name of the tab page to show.</param>
        public void ShowTabPage(string tabPageToShow)
        {
            for (int i = 0; i < tabControlSetup.TabPages.Count; i++)
            {
                if (tabControlSetup.TabPages[i].Text.Equals(tabPageToShow))
                {
                    tabControlSetup.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshApplicationFocusList()
        {
            listBoxProcessList.Items.Clear();
            listBoxProcessList.Sorted = true;

            foreach (Process process in Process.GetProcesses())
            {
                if (!listBoxProcessList.Items.Contains(process.ProcessName))
                {
                    listBoxProcessList.Items.Add(process.ProcessName);
                }
            }

            string applicationFocus = _config.Settings.User.GetByKey("ApplicationFocus", string.Empty, true).Value.ToString();

            if (string.IsNullOrEmpty(applicationFocus))
            {
                checkBoxEnableApplicationFocus.Checked = false;

                return;
            }
            else
            {
                checkBoxEnableApplicationFocus.Checked = true;
            }

            if (!listBoxProcessList.Items.Contains(applicationFocus))
            {
                listBoxProcessList.Items.Add(applicationFocus);
            }

            listBoxProcessList.SelectedIndex = listBoxProcessList.Items.IndexOf(applicationFocus);

            numericUpDownApplicationFocusDelayBefore.Value = Convert.ToInt32(_config.Settings.User.GetByKey("ApplicationFocusDelayBefore", 0, true).Value);
            numericUpDownApplicationFocusDelayAfter.Value = Convert.ToInt32(_config.Settings.User.GetByKey("ApplicationFocusDelayAfter", 0, true).Value);
        }

        /// <summary>
        /// Perform application focus.
        /// </summary>
        public void DoApplicationFocus()
        {
            if (!checkBoxEnableApplicationFocus.Checked || listBoxProcessList.SelectedItem == null)
            {
                return;
            }

            int delayBefore = (int)numericUpDownApplicationFocusDelayBefore.Value;
            int delayAfter = (int)numericUpDownApplicationFocusDelayAfter.Value;

            if (delayBefore > 0)
            {
                System.Threading.Thread.Sleep(delayBefore);
            }

            _screenCapture.SetApplicationFocus(listBoxProcessList.SelectedItem.ToString());

            if (delayAfter > 0)
            {
                System.Threading.Thread.Sleep(delayAfter);
            }
        }

        /// <summary>
        /// Sets the application focus on a particular application.
        /// </summary>
        /// <param name="applicationFocus">The name of the application to force focus on.</param>
        public void SetApplicationFocus(string applicationFocus)
        {
            if (string.IsNullOrEmpty(applicationFocus))
            {
                _config.Settings.User.SetValueByKey("ApplicationFocus", string.Empty);

                checkBoxEnableApplicationFocus.Checked = false;
            }
            else
            {
                applicationFocus = applicationFocus.Trim();

                _config.Settings.User.SetValueByKey("ApplicationFocus", applicationFocus);

                if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                {
                    _screenCapture.ApplicationError = true;
                }

                checkBoxEnableApplicationFocus.Checked = true;
            }

            RefreshApplicationFocusList();

            DoApplicationFocus();
        }

        private void buttonSetPassphrase_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPassphrase.Text))
            {
                MessageBox.Show("Passphrase cannot be empty.", "Empty Passphrase", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            string passphraseLastUpdated = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss");

            _config.Settings.User.SetValueByKey("Passphrase", _security.Hash(textBoxPassphrase.Text));
            _config.Settings.User.SetValueByKey("PassphraseLastUpdated", passphraseLastUpdated);

            _config.Settings.User.Save(_config.Settings, _fileSystem);

            textBoxPassphraseHash.Text = _config.Settings.User.GetByKey("Passphrase", string.Empty, true).Value.ToString();

            //_security.Key = textBoxPassphrase.Text;
            textBoxPassphrase.Clear();

            labelLastUpdated.Text = "Last updated: " + passphraseLastUpdated;
        }

        private void buttonClearPassphrase_Click(object sender, EventArgs e)
        {
            string passphraseLastUpdated = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss");

            _config.Settings.User.SetValueByKey("Passphrase", string.Empty);
            _config.Settings.User.SetValueByKey("PassphraseLastUpdated", passphraseLastUpdated);

            _config.Settings.User.Save(_config.Settings, _fileSystem);

            textBoxPassphraseHash.Text = _config.Settings.User.GetByKey("Passphrase", string.Empty, true).Value.ToString();

            //_security.Key = string.Empty;
            textBoxPassphrase.Clear();

            labelLastUpdated.Text = "Last updated: " + passphraseLastUpdated;

            textBoxPassphrase.Text = string.Empty;
        }

        private void buttonAddScreenshotLabelToList_Click(object sender, EventArgs e)
        {
            string labelToAdd = textBoxScreenshotLabel.Text.Trim();

            if (string.IsNullOrEmpty(labelToAdd))
            {
                return;
            }

            textBoxScreenshotLabel.Clear();

            if (!listBoxScreenshotLabel.Items.Contains(labelToAdd))
            {
                listBoxScreenshotLabel.Items.Add(labelToAdd);
            }

            listBoxScreenshotLabel.SelectedItem = labelToAdd;

            if (!_formLabelSwitcher.comboBoxLabels.Items.Contains(labelToAdd))
            {
                _formLabelSwitcher.comboBoxLabels.Items.Add(labelToAdd);
            }

            _formLabelSwitcher.comboBoxLabels.SelectedItem = labelToAdd;
        }

        private void listBoxScreenshotLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _config.Settings.User.SetValueByKey("ScreenshotLabel", listBoxScreenshotLabel.SelectedItem.ToString());

            _formLabelSwitcher.comboBoxLabels.SelectedItem = listBoxScreenshotLabel.SelectedItem;
        }

        private void checkBoxActiveWindowTitleComparisonCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxActiveWindowTitleComparisonCheck.Checked)
            {
                checkBoxActiveWindowTitleComparisonCheckReverse.Checked = false;
            }
            else
            {
                checkBoxActiveWindowTitleComparisonCheckReverse.Checked = true;
            }

            TestMatchOptionsForActiveWindowTitle();
        }

        private void checkBoxActiveWindowTitleComparisonCheckReverse_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxActiveWindowTitleComparisonCheckReverse.Checked)
            {
                checkBoxActiveWindowTitleComparisonCheck.Checked = false;
            }
            else
            {
                checkBoxActiveWindowTitleComparisonCheck.Checked = true;
            }

            TestMatchOptionsForActiveWindowTitle();
        }

        private void textBoxActiveWindowTitle_TextChanged(object sender, EventArgs e)
        {
            TestMatchOptionsForActiveWindowTitle();
        }

        private void textBoxActiveWindowTitleTest_TextChanged(object sender, EventArgs e)
        {
            TestMatchOptionsForActiveWindowTitle();
        }

        private void radioButtonCaseSensitiveMatch_CheckedChanged(object sender, EventArgs e)
        {
            TestMatchOptionsForActiveWindowTitle();
        }

        private void radioButtonCaseInsensitiveMatch_CheckedChanged(object sender, EventArgs e)
        {
            TestMatchOptionsForActiveWindowTitle();
        }

        private void radioButtonRegularExpressionMatch_CheckedChanged(object sender, EventArgs e)
        {
            TestMatchOptionsForActiveWindowTitle();
        }

        private void TestMatchOptionsForActiveWindowTitle()
        {
            if (string.IsNullOrEmpty(textBoxActiveWindowTitle.Text) || string.IsNullOrEmpty(textBoxActiveWindowTitleTest.Text))
            {
                labelMatchTestResult.BackColor = System.Drawing.Color.LightYellow;
                labelMatchTestResult.Text = "Test Result: (empty)";

                return;
            }

            if (checkBoxActiveWindowTitleComparisonCheck.Checked)
            {
                if (ActiveWindowTitleMatchText())
                {
                    labelMatchTestResult.BackColor = System.Drawing.Color.LightGreen;
                    labelMatchTestResult.Text = "Test Result: Match Succeeded";
                }
                else
                {
                    labelMatchTestResult.BackColor = System.Drawing.Color.PaleVioletRed;
                    labelMatchTestResult.Text = "Test Result: Match Failed";
                }
            }

            if (checkBoxActiveWindowTitleComparisonCheckReverse.Checked)
            {
                if (ActiveWindowTitleDoesNotMatchText())
                {
                    labelMatchTestResult.BackColor = System.Drawing.Color.LightGreen;
                    labelMatchTestResult.Text = "Test Result: No Match Succeeded";
                }
                else
                {
                    labelMatchTestResult.BackColor = System.Drawing.Color.PaleVioletRed;
                    labelMatchTestResult.Text = "Test Result: No Match Failed";
                }
            }
        }

        private bool ActiveWindowTitleMatchText()
        {
            try
            {
                string activeWindowTitleComparisonText = textBoxActiveWindowTitle.Text;
                string activeWindowTitleTestText = textBoxActiveWindowTitleTest.Text;

                activeWindowTitleComparisonText = activeWindowTitleComparisonText.Trim();
                activeWindowTitleTestText = activeWindowTitleTestText.Trim();

                if (!string.IsNullOrEmpty(activeWindowTitleTestText) && !string.IsNullOrEmpty(activeWindowTitleComparisonText))
                {
                    if (radioButtonCaseSensitiveMatch.Checked)
                    {
                        return activeWindowTitleTestText.Contains(activeWindowTitleComparisonText);
                    }
                    else if (radioButtonCaseInsensitiveMatch.Checked)
                    {
                        return activeWindowTitleTestText.ToLower().Contains(activeWindowTitleComparisonText.ToLower());
                    }
                    else if (radioButtonRegularExpressionMatch.Checked)
                    {
                        return Regex.IsMatch(activeWindowTitleTestText, activeWindowTitleComparisonText);
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool ActiveWindowTitleDoesNotMatchText()
        {
            try
            {
                string activeWindowTitleComparisonText = textBoxActiveWindowTitle.Text;
                string activeWindowTitleTestText = textBoxActiveWindowTitleTest.Text;

                activeWindowTitleComparisonText = activeWindowTitleComparisonText.Trim();
                activeWindowTitleTestText = activeWindowTitleTestText.Trim();

                if (!string.IsNullOrEmpty(activeWindowTitleTestText) && !string.IsNullOrEmpty(activeWindowTitleComparisonText))
                {
                    if (radioButtonCaseSensitiveMatch.Checked)
                    {
                        return !activeWindowTitleTestText.Contains(activeWindowTitleComparisonText);
                    }
                    else if (radioButtonCaseInsensitiveMatch.Checked)
                    {
                        return !activeWindowTitleTestText.ToLower().Contains(activeWindowTitleComparisonText.ToLower());
                    }
                    else if (radioButtonRegularExpressionMatch.Checked)
                    {
                        return !Regex.IsMatch(activeWindowTitleTestText, activeWindowTitleComparisonText);
                    }

                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private void checkBoxOptimizeScreenCapture_CheckedChanged(object sender, EventArgs e)
        {
            _screenCapture.OptimizeScreenCapture = checkBoxOptimizeScreenCapture.Checked;

            CheckEnabledStatusOnOptimizeScreenCaptureControls();
        }

        private void CheckEnabledStatusOnOptimizeScreenCaptureControls()
        {
            if (checkBoxOptimizeScreenCapture.Checked)
            {
                labelImageDifference.Enabled = true;
                trackBarImageDiffTolerance.Enabled = true;
                labelImageDiffTolerance.Enabled = true;
                labelSelectedImageDiffTolerance.Enabled = true;
                buttonOptimizeScreenCaptureApplyToAllScreens.Enabled = true;
                buttonOptimizeScreenCaptureApplyToAllRegions.Enabled = true;
            }
            else
            {
                labelImageDifference.Enabled = false;
                trackBarImageDiffTolerance.Enabled = false;
                labelImageDiffTolerance.Enabled = false;
                labelSelectedImageDiffTolerance.Enabled = false;
                buttonOptimizeScreenCaptureApplyToAllScreens.Enabled = false;
                buttonOptimizeScreenCaptureApplyToAllRegions.Enabled = false;
            }
        }

        private void checkBoxEnableApplicationFocus_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnableApplicationFocus.Checked)
            {
                listBoxProcessList.Enabled = true;

                labelApplicationFocusDelayBefore.Enabled = true;
                labelApplicationFocusDelayAfter.Enabled = true;

                numericUpDownApplicationFocusDelayBefore.Enabled = true;
                numericUpDownApplicationFocusDelayAfter.Enabled = true;

                buttonApplicationFocusTest.Enabled = true;
                buttonApplicationFocusRefresh.Enabled = true;
            }
            else
            {
                listBoxProcessList.Enabled = false;

                labelApplicationFocusDelayBefore.Enabled = false;
                labelApplicationFocusDelayAfter.Enabled = false;

                numericUpDownApplicationFocusDelayBefore.Enabled = false;
                numericUpDownApplicationFocusDelayAfter.Enabled = false;

                buttonApplicationFocusTest.Enabled = false;
                buttonApplicationFocusRefresh.Enabled = false;

                listBoxProcessList.SelectedItem = null;

                _config.Settings.User.SetValueByKey("ApplicationFocus", string.Empty);
            }
        }

        private void listBoxProcessList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProcessList.SelectedItem == null)
            {
                return;
            }

            _config.Settings.User.GetByKey("ApplicationFocus").Value = listBoxProcessList.SelectedItem.ToString();
        }

        private void buttonScreenshotsFolderBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                textBoxScreenshotsFolder.Text = folderBrowser.SelectedPath;
            }
        }

        private void buttonScreenshotsFolderApplyToAllScreens_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxScreenshotsFolder.Text))
            {
                return;
            }

            foreach (Screen screen in _formScreen.ScreenCollection)
            {
                screen.Folder = textBoxScreenshotsFolder.Text;
            }

            _formScreen.ScreenCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log);

            MessageBox.Show("All screens are now using the folder path \"" + textBoxScreenshotsFolder.Text + "\"", "Folder Path Applied To All Screens", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonScreenshotsFolderApplyToAllRegions_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxScreenshotsFolder.Text))
            {
                return;
            }

            foreach (Region region in _formRegion.RegionCollection)
            {
                region.Folder = textBoxScreenshotsFolder.Text;
            }

            _formRegion.RegionCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log);

            MessageBox.Show("All regions are now using the folder path \"" + textBoxScreenshotsFolder.Text + "\"", "Folder Path Applied To All Regions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBoxFilenamePattern_TextChanged(object sender, EventArgs e)
        {
            textBoxMacroPreview.ForeColor = System.Drawing.Color.Black;
            textBoxMacroPreview.BackColor = System.Drawing.Color.LightYellow;

            textBoxMacroPreview.Text = _macroParser.ParseTags(preview: true, textBoxFilenamePattern.Text, null, Text, Assembly.GetExecutingAssembly().GetName().Name, null, _macroTagCollection, _log);
        }

        private void buttonFilenamePatternApplyToAllScreens_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFilenamePattern.Text))
            {
                return;
            }

            foreach (Screen screen in _formScreen.ScreenCollection)
            {
                screen.Macro = textBoxFilenamePattern.Text;
            }

            _formScreen.ScreenCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log);

            MessageBox.Show("All screens are now using the filename pattern \"" + textBoxFilenamePattern.Text + "\"", "Filename Pattern Applied To All Screens", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonFilenamePatternApplyToAllRegions_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFilenamePattern.Text))
            {
                return;
            }

            foreach (Region region in _formRegion.RegionCollection)
            {
                region.Macro = textBoxFilenamePattern.Text;
            }

            _formRegion.RegionCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log);

            MessageBox.Show("All regions are now using the filename pattern \"" + textBoxFilenamePattern.Text + "\"", "Filename Pattern Applied To All Regions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonImageFormatApplyToAllScreens_Click(object sender, EventArgs e)
        {
            foreach (Screen screen in _formScreen.ScreenCollection)
            {
                if (radioButtonImageFormatBmp.Checked)
                {
                    ScreenCapture.ImageFormat = "BMP";
                    screen.Format = new ImageFormat("BMP", ".bmp");
                }

                if (radioButtonImageFormatEmf.Checked)
                {
                    ScreenCapture.ImageFormat = "EMF";
                    screen.Format = new ImageFormat("EMF", ".emf");
                }

                if (radioButtonImageFormatGif.Checked)
                {
                    ScreenCapture.ImageFormat = "GIF";
                    screen.Format = new ImageFormat("GIF", ".gif");
                }

                if (radioButtonImageFormatJpeg.Checked)
                {
                    ScreenCapture.ImageFormat = "JPEG";
                    screen.Format = new ImageFormat("JPEG", ".jpeg");
                }

                if (radioButtonImageFormatPng.Checked)
                {
                    ScreenCapture.ImageFormat = "PNG";
                    screen.Format = new ImageFormat("PNG", ".png");
                }

                if (radioButtonImageFormatTiff.Checked)
                {
                    ScreenCapture.ImageFormat = "TIFF";
                    screen.Format = new ImageFormat("TIFF", ".tiff");
                }

                if (radioButtonImageFormatWmf.Checked)
                {
                    ScreenCapture.ImageFormat = "WMF";
                    screen.Format = new ImageFormat("WMF", ".wmf");
                }
            }

            _formScreen.ScreenCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log);

            MessageBox.Show("All screens are now using the image format \"" + ScreenCapture.ImageFormat + "\"", "Image Format Applied To All Screens", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonImageFormatApplyToAllRegions_Click(object sender, EventArgs e)
        {
            foreach (Region region in _formRegion.RegionCollection)
            {
                if (radioButtonImageFormatBmp.Checked)
                {
                    ScreenCapture.ImageFormat = "BMP";
                    region.Format = new ImageFormat("BMP", ".bmp");
                }

                if (radioButtonImageFormatEmf.Checked)
                {
                    ScreenCapture.ImageFormat = "EMF";
                    region.Format = new ImageFormat("EMF", ".emf");
                }

                if (radioButtonImageFormatGif.Checked)
                {
                    ScreenCapture.ImageFormat = "GIF";
                    region.Format = new ImageFormat("GIF", ".gif");
                }

                if (radioButtonImageFormatJpeg.Checked)
                {
                    ScreenCapture.ImageFormat = "JPEG";
                    region.Format = new ImageFormat("JPEG", ".jpeg");
                }

                if (radioButtonImageFormatPng.Checked)
                {
                    ScreenCapture.ImageFormat = "PNG";
                    region.Format = new ImageFormat("PNG", ".png");
                }

                if (radioButtonImageFormatTiff.Checked)
                {
                    ScreenCapture.ImageFormat = "TIFF";
                    region.Format = new ImageFormat("TIFF", ".tiff");
                }

                if (radioButtonImageFormatWmf.Checked)
                {
                    ScreenCapture.ImageFormat = "WMF";
                    region.Format = new ImageFormat("WMF", ".wmf");
                }
            }

            _formRegion.RegionCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log);

            MessageBox.Show("All regions are now using the image format \"" + ScreenCapture.ImageFormat + "\"", "Image Format Applied To All Regions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void trackBarImageDifference_Scroll(object sender, EventArgs e)
        {
            labelSelectedImageDiffTolerance.Text = trackBarImageDiffTolerance.Value.ToString() + "%";
        }

        private void buttonOptimizeScreenCaptureApplyToAllScreens_Click(object sender, EventArgs e)
        {
            foreach (Screen screen in _formScreen.ScreenCollection)
            {
                screen.ImageDiffTolerance = trackBarImageDiffTolerance.Value;
            }

            _formScreen.ScreenCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log);

            MessageBox.Show("All screens are now using the image difference tolerance of " + trackBarImageDiffTolerance.Value + "%", "Image Difference Tolerance Applied To All Screens", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonOptimizeScreenCaptureApplyToAllRegions_Click(object sender, EventArgs e)
        {
            foreach (Region region in _formRegion.RegionCollection)
            {
                region.ImageDiffTolerance = trackBarImageDiffTolerance.Value;
            }

            _formRegion.RegionCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log);

            MessageBox.Show("All regions are now using the image difference tolerance of " + trackBarImageDiffTolerance.Value + "%", "Image Difference Tolerance Applied To All Regions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
