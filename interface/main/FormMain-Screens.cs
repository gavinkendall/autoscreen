//-----------------------------------------------------------------------
// <copyright file="FormMain-Screens.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for when screens are added, removed, or changed.</summary>
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
using System.Drawing;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Shows the "Add Screen" window to enable the user to add a chosen Screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addScreen_Click(object sender, EventArgs e)
        {
            ShowInterface();

            _formScreen.ScreenObject = null;
            _formScreen.ImageFormatCollection = _imageFormatCollection;
            _formScreen.TagCollection = _formMacroTag.MacroTagCollection;

            if (!_formScreen.Visible)
            {
                _formScreen.ShowDialog(this);
            }
            else
            {
                _formScreen.Activate();
            }

            if (_formScreen.DialogResult == DialogResult.OK)
            {
                BuildScreensModule();
                BuildViewTabPages();

                if (!_formScreen.ScreenCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Removes the selected Screens from the Screens tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeSelectedScreens_Click(object sender, EventArgs e)
        {
            int countBeforeRemoval = _formScreen.ScreenCollection.Count;

            foreach (Control control in tabPageScreens.Controls)
            {
                if (control.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)control;

                    if (checkBox.Checked)
                    {
                        Screen screen = _formScreen.ScreenCollection.Get((Screen)checkBox.Tag);
                        _formScreen.ScreenCollection.Remove(screen);
                    }
                }
            }

            if (countBeforeRemoval > _formScreen.ScreenCollection.Count)
            {
                BuildScreensModule();
                BuildViewTabPages();

                if (!_formScreen.ScreenCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        /// <summary>
        /// Shows the "Change Screen" window to enable the user to edit a chosen Screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void configureScreen_Click(object sender, EventArgs e)
        {
            ShowInterface();

            Screen screen = new Screen();

            if (sender is Button)
            {
                Button buttonSelected = (Button)sender;
                screen = (Screen)buttonSelected.Tag;
            }

            if (sender is ToolStripButton)
            {
                ToolStripButton buttonSelected = (ToolStripButton)sender;
                screen = (Screen)buttonSelected.Tag;
            }

            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                screen = (Screen)toolStripMenuItemSelected.Tag;
            }

            _formScreen.ScreenObject = screen;
            _formScreen.ImageFormatCollection = _imageFormatCollection;
            _formScreen.TagCollection = _formMacroTag.MacroTagCollection;

            if (!_formScreen.Visible)
            {
                _formScreen.ShowDialog(this);
            }
            else
            {
                _formScreen.Activate();
            }

            if (_formScreen.DialogResult == DialogResult.OK)
            {
                BuildScreensModule();
                BuildViewTabPages();

                if (!_formScreen.ScreenCollection.SaveToXmlFile(_config.Settings, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        private void RunScreenCaptures(string scope, string macroOverride)
        {
            try
            {
                foreach (Screen screen in _formScreen.ScreenCollection)
                {
                    if (!string.IsNullOrEmpty(scope) &&
                        (scope.Equals("All Screens and Regions") ||
                        scope.Equals("All Screens") ||
                        scope.Equals(screen.Name)))
                    {
                        if (screen.Enable)
                        {
                            if (screen.Source == 0 && screen.Component == 0 && !screen.AutoAdapt) // Active Window
                            {
                                if (_screenCapture.GetScreenImages(screen.Source, screen.Component, screen.CaptureMethod, autoAdapt: false, 0, 0, 0, 0, screen.ResolutionRatio, mouse: false, out Bitmap bitmap))
                                {
                                    if (!SaveScreenshot(bitmap, screen, macroOverride))
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    _log.WriteDebugMessage($"No image was captured for active window {screen.Name}");
                                }
                            }
                            else // Screen (regardless of how many displays there are)
                            {
                                // Check to see if we need to get the position and size of whatever available screen is being used at this time.
                                AutoAdapt(screen, out int x, out int y, out int width, out int height);

                                if (_screenCapture.GetScreenImages(screen.Source, screen.Component, screen.CaptureMethod, screen.AutoAdapt,
                                    x,
                                    y,
                                    width,
                                    height,
                                    screen.ResolutionRatio,
                                    screen.Mouse,
                                    out Bitmap bitmap))
                                {
                                    if (!SaveScreenshot(bitmap, screen, macroOverride))
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    _log.WriteDebugMessage($"No image was captured for screen {screen.Name}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-Screens::RunScreenCaptures", ex);
            }
        }

        /// <summary>
        /// Adjusts the position and size based on the selected component if AutoAdapt is enabled.
        /// This is useful for when your display setup changes over time while a screen capture session is running.
        /// </summary>
        /// <param name="screen">The screen to use.</param>
        /// <param name="x">The X value.</param>
        /// <param name="y">The Y value.</param>
        /// <param name="width">The Width value.</param>
        /// <param name="height">The Height value.</param>
        private void AutoAdapt(Screen screen, out int x, out int y, out int width, out int height)
        {
            x = 0;
            y = 0;
            width = 0;
            height = 0;

            if (screen.AutoAdapt)
            {
                for (int i = 0; i < System.Windows.Forms.Screen.AllScreens.Length; i++)
                {
                    System.Windows.Forms.Screen screenFromWindows = System.Windows.Forms.Screen.AllScreens[i];
                    ScreenCapture.DeviceOptions deviceResolution = _screenCapture.GetDevice(screenFromWindows);

                    if (i == screen.AutoAdaptIndex)
                    {
                        x = screenFromWindows.Bounds.X;
                        y = screenFromWindows.Bounds.Y;
                        width = deviceResolution.width;
                        height = deviceResolution.height;

                        break;
                    }
                }
            }
            else
            {
                x = screen.X;
                y = screen.Y;
                width = screen.Width;
                height = screen.Height;
            }
        }
    }
}