//-----------------------------------------------------------------------
// <copyright file="FormMain-Screens.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
            _formScreen.ScreenObject = null;
            _formScreen.ImageFormatCollection = _imageFormatCollection;
            _formScreen.TagCollection = _formTag.TagCollection;

            if (!_formScreen.Visible)
            {
                _formScreen.ShowDialog(this);
            }
            else
            {
                _formScreen.Focus();
                _formScreen.BringToFront();
            }

            if (_formScreen.DialogResult == DialogResult.OK)
            {
                BuildScreensModule();
                BuildViewTabPages();

                if (!_formScreen.ScreenCollection.SaveToXmlFile(_config, _fileSystem, _log))
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

                if (!_formScreen.ScreenCollection.SaveToXmlFile(_config, _fileSystem, _log))
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
        private void changeScreen_Click(object sender, EventArgs e)
        {
            Screen screen = new Screen();

            if (sender is Button)
            {
                Button buttonSelected = (Button)sender;
                screen = (Screen)buttonSelected.Tag;
            }

            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                screen = (Screen)toolStripMenuItemSelected.Tag;
            }

            _formScreen.ScreenObject = screen;
            _formScreen.ImageFormatCollection = _imageFormatCollection;
            _formScreen.TagCollection = _formTag.TagCollection;

            _formScreen.ShowDialog(this);

            if (_formScreen.DialogResult == DialogResult.OK)
            {
                BuildScreensModule();
                BuildViewTabPages();

                if (!_formScreen.ScreenCollection.SaveToXmlFile(_config, _fileSystem, _log))
                {
                    _screenCapture.ApplicationError = true;
                }
            }
        }

        private void removeScreen_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem toolStripMenuItemSelected = (ToolStripMenuItem)sender;
                Screen screenSelected = (Screen)toolStripMenuItemSelected.Tag;

                DialogResult dialogResult = MessageBox.Show("Do you want to remove the screen named \"" + screenSelected.Name + "\"?", "Remove Screen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Screen screen = _formScreen.ScreenCollection.Get(screenSelected);
                    _formScreen.ScreenCollection.Remove(screen);

                    BuildScreensModule();
                    BuildViewTabPages();

                    if (!_formScreen.ScreenCollection.SaveToXmlFile(_config, _fileSystem, _log))
                    {
                        _screenCapture.ApplicationError = true;
                    }
                }
            }
        }

        private void RunScreenCaptures()
        {
            try
            {
                if (_formScreen.ScreenCollection.Count == 0)
                {
                    _log.WriteErrorMessage("The screen collection is empty and needs to be initialized");

                    return;
                }

                foreach (Screen screen in _formScreen.ScreenCollection)
                {
                    if (screen.Active)
                    {
                        if (screen.Source == 0 && screen.Component == 0) // Active Window
                        {
                            if (_screenCapture.GetScreenImages(screen.Source, screen.Component, 0, 0, 0, 0, false, out Bitmap bitmap))
                            {
                                if (!SaveScreenshot(bitmap, screen))
                                {
                                    continue;
                                }
                            }
                        }
                        else // Screen (regardless of how many displays there are)
                        {
                            if (_screenCapture.GetScreenImages(screen.Source, screen.Component,
                                screen.X,
                                screen.Y,
                                screen.Width,
                                screen.Height,
                                screen.Mouse,
                                out Bitmap bitmap))
                            {
                                if (!SaveScreenshot(bitmap, screen))
                                {
                                    continue;
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
    }
}