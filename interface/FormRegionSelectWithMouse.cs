//-----------------------------------------------------------------------
// <copyright file="FormRegionSelectWithMouse.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A form that covers all the available screens so we can do a mouse-driven region select.</summary>
//-----------------------------------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormRegionSelectWithMouse : Form
    {
        private int _selectX;
        private int _selectY;
        private int _selectWidth;
        private int _selectHeight;
        private Pen _selectPen;

        public int outputX;
        public int outputY;
        public int outputWidth;
        public int outputHeight;

        /// <summary>
        /// 
        /// </summary>
        public FormRegionSelectWithMouse()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        private int _outputMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void LoadCanvas(int outputMode)
        {
            _outputMode = outputMode;

            Top = 0;
            Left = 0;

            int width = 0;
            int height = 0;

            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                width += screen.Bounds.Width;
                height += screen.Bounds.Height;
            }

            WindowState = FormWindowState.Normal;
            Width = width;
            Height = height;

            Hide();

            Bitmap bitmap = new Bitmap(width, height);

            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            using (MemoryStream s = new MemoryStream())
            {
                bitmap.Save(s, System.Drawing.Imaging.ImageFormat.Bmp);
                pictureBoxMouseCanvas.Size = new Size(Width, Height);
                pictureBoxMouseCanvas.Image = Image.FromStream(s);
            }

            Show();

            Cursor = Cursors.Cross;
        }

        private void pictureBoxMouseCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBoxMouseCanvas.Image == null || _selectPen == null) return;

            pictureBoxMouseCanvas.Refresh();

            _selectWidth = e.X - _selectX;
            _selectHeight = e.Y - _selectY;

            pictureBoxMouseCanvas.CreateGraphics().DrawRectangle(_selectPen, _selectX, _selectY, _selectWidth, _selectHeight);
        }

        private void pictureBoxMouseCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _selectX = e.X;
                _selectY = e.Y;

                _selectPen = new Pen(Color.Yellow, 2);
                _selectPen.DashStyle = DashStyle.Dash;
            }

            pictureBoxMouseCanvas.Refresh();
        }

        private void pictureBoxMouseCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (pictureBoxMouseCanvas.Image == null || _selectPen == null) return;

            if (e.Button == MouseButtons.Left)
            {
                pictureBoxMouseCanvas.Refresh();

                _selectWidth = e.X - _selectX;
                _selectHeight = e.Y - _selectY;

                pictureBoxMouseCanvas.CreateGraphics().DrawRectangle(_selectPen, _selectX, _selectY, _selectWidth, _selectHeight);
            }

            switch (_outputMode)
            {
                case 0:
                    outputX = _selectX;
                    outputY = _selectY;
                    outputWidth = _selectWidth;
                    outputHeight = _selectHeight;
                    break;
                case 1:
                    SaveToClipboard();
                    break;
            }

            Close();
        }

        private void SaveToClipboard()
        {
            if (_selectWidth > 0)
            {
                Rectangle rect = new Rectangle(_selectX, _selectY, _selectWidth, _selectHeight);
                Bitmap bitmap = new Bitmap(pictureBoxMouseCanvas.Image, pictureBoxMouseCanvas.Width, pictureBoxMouseCanvas.Height);

                Bitmap img = new Bitmap(_selectWidth, _selectHeight);

                Graphics g = Graphics.FromImage(img);

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(bitmap, 0, 0, rect, GraphicsUnit.Pixel);

                Clipboard.SetImage(img);
            }
        }
    }
}
