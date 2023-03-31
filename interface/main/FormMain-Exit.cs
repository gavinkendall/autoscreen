//-----------------------------------------------------------------------
// <copyright file="FormMain-Exit.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for exiting the application.</summary>
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
        /// <summary>
        /// Exits the application.
        /// </summary>
        private void ExitApplication()
        {
            try
            {
                _log.WriteMessage("Exiting application");

                string passphrase = _config.Settings.User.GetByKey("Passphrase").Value.ToString();

                if (!string.IsNullOrEmpty(passphrase))
                {
                    _screenCapture.LockScreenCaptureSession = true;

                    if (!_formEnterPassphrase.Visible)
                    {
                        _formEnterPassphrase.Text = "Auto Screen Capture - Enter Passphrase (Exit Application)";
                        _formEnterPassphrase.ShowDialog(this);
                    }
                    else
                    {
                        _formEnterPassphrase.Activate();
                    }

                    if (_formEnterPassphrase.DialogResult != DialogResult.OK)
                    {
                        _log.WriteErrorMessage("Passphrase incorrect or not entered. Cannot exit application. Screen capture session has been locked. Interface is now hidden");

                        HideInterface();

                        return;
                    }
                }

                if (_screenshotCollection != null)
                {
                    _log.WriteDebugMessage("Saving screenshot references on clean application exit");
                    _screenshotCollection.SaveToXmlFile(_config);
                }

                _log.WriteMessage("Bye!");

                _log.WriteDebugMessage("Running triggers of condition type ApplicationExit");
                RunTriggersOfConditionType(TriggerConditionType.ApplicationExit);

                // Exit.
                Environment.Exit(0);
            }
            catch (Exception)
            {
                Environment.Exit(1);
            }
        }
    }
}
