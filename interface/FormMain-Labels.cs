//-----------------------------------------------------------------------
// <copyright file="FormMain-Labels.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AutoScreenCapture
{
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
                Log.WriteDebugMessage(":: PopulateLabelList Start ::");

                List<string> labels = new List<string>();
                labels = _screenshotCollection.GetFilterValueList("Label");

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

                Log.WriteDebugMessage(":: PopulateLabelList End ::");
            }
            catch (Exception ex)
            {
                Log.WriteExceptionMessage("FormMain-Labels::PopulateLabelList", ex);
            }
        }
    }
}