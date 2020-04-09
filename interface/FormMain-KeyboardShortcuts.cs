using System;
using System.Drawing;
using System.Windows.Forms;
using AutoScreenCapture.Properties;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        private void hotKey_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Key == Keys.Z)
            {
                StartScreenCapture();
            }

            if (e.Key == Keys.X)
            {
                StopScreenCapture();
            }

            if (e.Key == Keys.A)
            {
                CaptureNowArchive();
            }

            if (e.Key == Keys.E)
            {
                CaptureNowEdit();
            }
        }
    }
}