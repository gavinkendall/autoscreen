namespace AutoScreenCapture
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using AutoScreenCapture.Properties;

    public partial class FormMain : Form
    {
        private void ComboBoxScreenshotLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem label in toolStripMenuItemApplyLabel.DropDownItems)
            {
                label.Checked = label.Text.Equals(comboBoxScreenshotLabel.Text);
            }
        }

        private void PopulateLabelList()
        {
            try
            {
                if (ScreenCapture.LockScreenCaptureSession)
                {
                    toolStripSeparatorApplyLabel.Visible = false;
                    toolStripMenuItemApplyLabel.Visible = false;

                    return;
                }

                toolStripSeparatorApplyLabel.Visible = true;
                toolStripMenuItemApplyLabel.Visible = true;

                List<string> labels = _screenshotCollection.GetLabels();
                labels.Sort();

                comboBoxScreenshotLabel.DataSource = labels;
                comboBoxScreenshotLabel.Text = Settings.User.GetByKey("StringScreenshotLabel", defaultValue: string.Empty).Value.ToString();

                toolStripMenuItemApplyLabel.DropDownItems.Clear();

                foreach (string label in labels)
                {
                    ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem()
                    {
                        Text = label,
                        Checked = label.Equals(comboBoxScreenshotLabel.Text),
                        CheckOnClick = true
                    };

                    toolStripMenuItem.Click += new EventHandler(Click_applyLabel);

                    toolStripMenuItemApplyLabel.DropDownItems.Add(toolStripMenuItem);
                }
            }
            catch (Exception ex)
            {
                Log.Write("FormMain::PopulateLabelList", ex);
            }
        }
    }
}