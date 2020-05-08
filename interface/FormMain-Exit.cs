//-----------------------------------------------------------------------
// <copyright file="FormMain-Exit.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for exiting the application.</summary>
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
                Log.WriteDebugMessage(":: ParseCommandLineArguments Start ::");

                Log.WriteMessage("Exiting application");

                if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
                {
                    Log.WriteDebugMessage("Screen capture session is locked. Challenging user to enter correct passphrase to unlock");
                    formEnterPassphrase.ShowDialog(this);
                }

                // This is intentional. Do not rewrite these statements as an if/else
                // because as soon as lockScreenCaptureSession is set to false we want
                // to continue with normal functionality.
                if (!ScreenCapture.LockScreenCaptureSession)
                {
                    Log.WriteDebugMessage("Running triggers of condition type ApplicationExit");
                    RunTriggersOfConditionType(TriggerConditionType.ApplicationExit);

                    // This is no longer the first run of the application when exiting.
                    Settings.User.SetValueByKey("BoolFirstRun", false);

                    Settings.User.GetByKey("StringPassphrase", defaultValue: false).Value = string.Empty;
                    SaveSettings();

                    DisableStopCapture();
                    EnableStartCapture();

                    _screenCapture.Count = 0;
                    _screenCapture.Running = false;

                    SystemTrayIconStatusNormal();
                    HideSystemTrayIcon();

                    Log.WriteDebugMessage("Hiding interface on clean application exit");
                    HideInterface();

                    Log.WriteDebugMessage("Saving screenshots on clean application exit");
                    _screenshotCollection.SaveToXmlFile((int)numericUpDownKeepScreenshotsForDays.Value);

                    if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                    {
                        runDateSearchThread.CancelAsync();
                    }

                    if (runScreenshotSearchThread != null && runScreenshotSearchThread.IsBusy)
                    {
                        runScreenshotSearchThread.CancelAsync();
                    }

                    Log.WriteMessage("Bye!");

                    // Exit.
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("FormMain-Exit::ExitApplication", ex);
            }
        }
    }
}
