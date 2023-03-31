//-----------------------------------------------------------------------
// <copyright file="FormRegionSelectWithMouse.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form that covers all the available screens so we can do a mouse-driven region select.</summary>
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
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// A form that covers all the available screens so we can do a mouse-driven region select.
    /// </summary>
    public partial class FormRegionSelectWithMouse : Form
    {
        ScreenCapture _screenCapture;

        private int _selectX;
        private int _selectY;
        private int _selectWidth;
        private int _selectHeight;
        private Pen _selectPen;

        /// <summary>
        /// X output
        /// </summary>
        public int outputX;

        /// <summary>
        /// Y output
        /// </summary>
        public int outputY;

        /// <summary>
        /// Width output
        /// </summary>
        public int outputWidth;

        /// <summary>
        /// Height output
        /// </summary>
        public int outputHeight;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public FormRegionSelectWithMouse(ScreenCapture screenCapture)
        {
            InitializeComponent();

            _screenCapture = screenCapture;

            outputX = 0;
            outputY = 0;
            outputWidth = 0;
            outputHeight = 0;
        }

        /// <summary>
        /// An event handler for handling when the mouse selection has completed for the mouse-driven region capture.
        /// </summary>
        public event EventHandler MouseSelectionCompleted;

        /// <summary>
        /// Invokes the mouse selection completed event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompleteMouseSelection(object sender, EventArgs e)
        {
            MouseSelectionCompleted?.Invoke(sender, e);
        }

        /// <summary>
        /// Loads the canvas with the chosen output mode.
        /// </summary>
        public void LoadCanvas()
        {
            Top = 0;
            Left = 0;

            Width = 0;
            Height = 0;

            foreach (System.Windows.Forms.Screen windowsScreen in System.Windows.Forms.Screen.AllScreens)
            {
                if (windowsScreen.Bounds.X < Left)
                {
                    Left = windowsScreen.Bounds.X;
                }

                if (windowsScreen.Bounds.Y < Top)
                {
                    Top = windowsScreen.Bounds.Y;
                }

                Width += windowsScreen.Bounds.Width;
                Height += windowsScreen.Bounds.Height;
            }

            WindowState = FormWindowState.Normal;

            Hide();

            Bitmap bitmap = _screenCapture.GetScreenBitmap(source: -1, component: -1, captureMethod: 1, Left, Top, Width, Height, resolutionRatio: 100, mouse: false);

            using (MemoryStream s = new MemoryStream())
            {
                bitmap.Save(s, System.Drawing.Imaging.ImageFormat.Bmp);

                pictureBoxMouseCanvas.Size = new Size(Width, Height);
                pictureBoxMouseCanvas.Image = Image.FromStream(s);
            }

            bitmap.Dispose();

            Show();

            Cursor = Cursors.Cross;
        }

        /// <summary>
        /// Handles what happens when the user is moving the mouse pointer on the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMouseCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBoxMouseCanvas.Image == null || _selectPen == null)
            {
                return;
            }

            pictureBoxMouseCanvas.Refresh();

            _selectWidth = e.X - _selectX;
            _selectHeight = e.Y - _selectY;

            pictureBoxMouseCanvas.CreateGraphics().DrawRectangle(_selectPen, _selectX, _selectY, _selectWidth, _selectHeight);
        }

        /// <summary>
        /// Handles what happens when the user has the mouse button held down while dragging the mouse selection on the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMouseCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _selectX = e.X;
                _selectY = e.Y;

                _selectPen = new Pen(Color.Red, 2)
                {
                    DashStyle = DashStyle.Dash
                };
            }

            pictureBoxMouseCanvas.Refresh();
        }

        /// <summary>
        /// Selects a bitmap image that represents a selected area of the screen and calls the CompleteMouseSelection event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMouseCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (pictureBoxMouseCanvas.Image == null || _selectPen == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                pictureBoxMouseCanvas.Refresh();

                _selectWidth = e.X - _selectX;
                _selectHeight = e.Y - _selectY;
            }

            _selectX = (Left + _selectX);
            _selectY = (Top + _selectY);

            outputX = _selectX;
            outputY = _selectY;
            outputWidth = _selectWidth;
            outputHeight = _selectHeight;

            CompleteMouseSelection(sender, e);

            Cursor = Cursors.Arrow;

            Close();
        }
    }
}
