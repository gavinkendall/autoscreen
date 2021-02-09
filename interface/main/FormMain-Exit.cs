//-----------------------------------------------------------------------
// <copyright file="FormMain-Exit.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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

                if (_screenCapture.LockScreenCaptureSession && !_formEnterPassphrase.Visible)
                {
                    _log.WriteDebugMessage("Screen capture session is locked. Challenging user to enter correct passphrase to unlock");
                    _formEnterPassphrase.ShowDialog(this);
                }

                // This is intentional. Do not rewrite these statements as an if/else
                // because as soon as lockScreenCaptureSession is set to false we want
                // to continue with normal functionality.
                if (!_screenCapture.LockScreenCaptureSession)
                {
                    _log.WriteDebugMessage("Running triggers of condition type ApplicationExit");
                    RunTriggersOfConditionType(TriggerConditionType.ApplicationExit);

                    // This is no longer the first run of the application when exiting.
                    _config.Settings.User.SetValueByKey("FirstRun", false);

                    _config.Settings.User.GetByKey("Passphrase", _config.Settings.DefaultSettings.Passphrase).Value = string.Empty;
                    SaveSettings();

                    DisableStopCapture();
                    EnableStartCapture();

                    if (_sftpClient != null && _sftpClient.IsConnected)
                    {
                        _sftpClient.Disconnect();
                    }

                    _screenCapture.Count = 0;
                    _screenCapture.Running = false;

                    HideSystemTrayIcon();

                    _log.WriteDebugMessage("Hiding interface on clean application exit");
                    HideInterface();

                    _log.WriteDebugMessage("Saving screenshots on clean application exit");
                    _screenshotCollection.SaveToXmlFile((int)numericUpDownKeepScreenshotsForDays.Value, _macroParser, _config);

                    if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                    {
                        runDateSearchThread.CancelAsync();
                    }

                    if (runScreenshotSearchThread != null && runScreenshotSearchThread.IsBusy)
                    {
                        runScreenshotSearchThread.CancelAsync();
                    }

                    _log.WriteMessage("Bye!");

                    // Exit.
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                _screenCapture.ApplicationError = true;
                _log.WriteExceptionMessage("FormMain-Exit::ExitApplication", ex);
            }
        }
    }
}
