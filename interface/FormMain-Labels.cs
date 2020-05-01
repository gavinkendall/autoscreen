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
        private void applyLabel_Click(object sender, EventArgs e)
        {
            if (sender != null && sender is ToolStripDropDownItem)
            {
                ToolStripDropDownItem toolStripDropDownItem = (ToolStripDropDownItem)sender;

                if (!string.IsNullOrEmpty(toolStripDropDownItem.Text))
                {
                    checkBoxScreenshotLabel.Checked = true;
                    comboBoxScreenshotLabel.Text = toolStripDropDownItem.Text;
                    SaveSettings();
                }
            }
        }

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
                List<string> labels = new List<string>();
                labels = _screenshotCollection.GetFilterValueList("label");

                if (ScreenCapture.LockScreenCaptureSession || labels.Count == 0)
                {
                    toolStripSeparatorApplyLabel.Visible = false;
                    toolStripMenuItemApplyLabel.Visible = false;

                    return;
                }

                toolStripSeparatorApplyLabel.Visible = true;
                toolStripMenuItemApplyLabel.Visible = true;

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

                    toolStripMenuItem.Click += new EventHandler(applyLabel_Click);

                    toolStripMenuItemApplyLabel.DropDownItems.Add(toolStripMenuItem);
                }
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("FormMain::PopulateLabelList", ex);
            }
        }
    }
}