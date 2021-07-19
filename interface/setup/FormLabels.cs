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
    public partial class FormLabels : Form
{
    public FormLabels()
    {
        InitializeComponent();
    }

        //private void applyLabel_Click(object sender, EventArgs e)
        //{
        //    if (sender != null && sender is ToolStripDropDownItem)
        //    {
        //        ToolStripDropDownItem toolStripDropDownItem = (ToolStripDropDownItem)sender;

        //        if (!string.IsNullOrEmpty(toolStripDropDownItem.Text))
        //        {
        //            checkBoxScreenshotLabel.Checked = true;
        //            comboBoxScreenshotLabel.Text = toolStripDropDownItem.Text;
        //            SaveSettings();
        //        }
        //    }
        //}

        //private void ComboBoxScreenshotLabel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    foreach (ToolStripMenuItem label in toolStripMenuItemApplyLabel.DropDownItems)
        //    {
        //        label.Checked = label.Text.Equals(comboBoxScreenshotLabel.Text);
        //    }
        //}

        //private void PopulateLabelList()
        //{
        //    try
        //    {
        //        List<string> labels = new List<string>();
        //        labels = _screenshotCollection.GetFilterValueList("Label");

        //        if (_screenCapture.LockScreenCaptureSession || labels.Count == 0)
        //        {
        //            toolStripSeparatorApplyLabel.Visible = false;
        //            toolStripMenuItemApplyLabel.Visible = false;

        //            return;
        //        }

        //        toolStripSeparatorApplyLabel.Visible = true;
        //        toolStripMenuItemApplyLabel.Visible = true;

        //        labels.Sort();

        //        comboBoxScreenshotLabel.DataSource = labels;
        //        comboBoxScreenshotLabel.Text = _config.Settings.User.GetByKey("ScreenshotLabel", _config.Settings.DefaultSettings.ScreenshotLabel).Value.ToString();

        //        toolStripMenuItemApplyLabel.DropDownItems.Clear();

        //        foreach (string label in labels)
        //        {
        //            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem()
        //            {
        //                Text = label,
        //                Checked = label.Equals(comboBoxScreenshotLabel.Text),
        //                CheckOnClick = true
        //            };

        //            toolStripMenuItem.Click += new EventHandler(applyLabel_Click);

        //            toolStripMenuItemApplyLabel.DropDownItems.Add(toolStripMenuItem);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _screenCapture.ApplicationError = true;
        //        _log.WriteExceptionMessage("FormMain-Labels::PopulateLabelList", ex);
        //    }
        //}

        //private void ApplyLabel(string label)
        //{
        //    if (string.IsNullOrEmpty(label))
        //    {
        //        _config.Settings.User.SetValueByKey("ApplyScreenshotLabel", false);

        //        checkBoxScreenshotLabel.Checked = false;
        //    }
        //    else
        //    {
        //        label = label.Trim();

        //        _config.Settings.User.SetValueByKey("ApplyScreenshotLabel", true);
        //        _config.Settings.User.SetValueByKey("ScreenshotLabel", label);

        //        if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
        //        {
        //            _screenCapture.ApplicationError = true;
        //        }

        //        checkBoxScreenshotLabel.Checked = true;
        //        comboBoxScreenshotLabel.Text = label;
        //    }
        //}
    }
}
