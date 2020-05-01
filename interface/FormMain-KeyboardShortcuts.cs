//-----------------------------------------------------------------------
// <copyright file="FormMain-KeyboardShortcuts.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System.Windows.Forms;

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