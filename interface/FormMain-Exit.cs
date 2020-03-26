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
            Log.Write("Exiting application");

            if (ScreenCapture.LockScreenCaptureSession && !formEnterPassphrase.Visible)
            {
                Log.Write("Screen capture session is locked. Challenging user to enter correct passphrase to unlock");
                formEnterPassphrase.ShowDialog(this);
            }

            // This is intentional. Do not rewrite these statements as an if/else
            // because as soon as lockScreenCaptureSession is set to false we want
            // to continue with normal functionality.
            if (!ScreenCapture.LockScreenCaptureSession)
            {
                Log.Write("Running triggers of condition type ApplicationExit");
                RunTriggersOfConditionType(TriggerConditionType.ApplicationExit);

                Settings.User.GetByKey("StringPassphrase", defaultValue: false).Value = string.Empty;
                SaveSettings();

                DisableStopCapture();
                EnableStartCapture();

                _screenCapture.Count = 0;
                _screenCapture.Running = false;

                notifyIcon.Icon = Resources.autoscreen;

                // Hide the system tray icon.
                notifyIcon.Visible = false;

                Log.Write("Hiding interface on clean application exit");
                HideInterface();

                Log.Write("Saving screenshots on clean application exit");
                _screenshotCollection.SaveToXmlFile((int)numericUpDownKeepScreenshotsForDays.Value);

                if (runDateSearchThread != null && runDateSearchThread.IsBusy)
                {
                    runDateSearchThread.CancelAsync();
                }

                if (runScreenshotSearchThread != null && runScreenshotSearchThread.IsBusy)
                {
                    runScreenshotSearchThread.CancelAsync();
                }

                Log.Write("Bye!");

                // Exit.
                Environment.Exit(0);
            }
        }
    }
}
