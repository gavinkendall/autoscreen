using System;
using System.Windows.Forms;
using AutoScreenCapture.Properties;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Exits the application.
        /// </summary>
        private void ExitApplication()
        {
            Log.WriteMessage("Exiting application");

            if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
            {
                Log.WriteMessage("Screen capture session is locked. Challenging user to enter correct passphrase to unlock");
                formEnterPassphrase.ShowDialog(this);
            }

            // This is intentional. Do not rewrite these statements as an if/else
            // because as soon as lockScreenCaptureSession is set to false we want
            // to continue with normal functionality.
            if (!ScreenCapture.LockScreenCaptureSession)
            {
                Log.WriteMessage("Running triggers of condition type ApplicationExit");
                RunTriggersOfConditionType(TriggerConditionType.ApplicationExit);

                Settings.User.GetByKey("StringPassphrase", defaultValue: false).Value = string.Empty;
                SaveSettings();

                DisableStopCapture();
                EnableStartCapture();

                _screenCapture.Count = 0;
                _screenCapture.Running = false;

                SystemTrayIconStatusNormal();
                HideSystemTrayIcon();

                Log.WriteMessage("Hiding interface on clean application exit");
                HideInterface();

                Log.WriteMessage("Saving screenshots on clean application exit");
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
    }
}
