using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormActiveWindowTitle : Form
    {
        public FormActiveWindowTitle()
        {
            InitializeComponent();
        }

        private void checkBoxActiveWindowTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxActiveWindowTitle.Checked)
            {
                textBoxActiveWindowTitle.Enabled = true;
                radioButtonCaseSensitiveMatch.Enabled = true;
                radioButtonCaseInsensitiveMatch.Enabled = true;
                radioButtonRegularExpressionMatch.Enabled = true;
            }
            else
            {
                textBoxActiveWindowTitle.Enabled = false;
                radioButtonCaseSensitiveMatch.Enabled = false;
                radioButtonCaseInsensitiveMatch.Enabled = false;
                radioButtonRegularExpressionMatch.Enabled = false;
            }
        }
    }
}
