using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormApplicationFocus : Form
    {
        private Config _config;
        private FileSystem _fileSystem;
        private ScreenCapture _screenCapture;

        public FormApplicationFocus(Config config, FileSystem fileSystem, ScreenCapture screenCapture)
        {
            InitializeComponent();

            _config = config;
            _fileSystem = fileSystem;
            _screenCapture = screenCapture;
        }

        private void buttonApplicationFocusTest_Click(object sender, EventArgs e)
        {
            //SaveSettings();

            DoApplicationFocus();
        }

        private void buttonApplicationFocusRefresh_Click(object sender, EventArgs e)
        {
            ///SaveSettings();

            RefreshApplicationFocusList();
        }

        public void RefreshApplicationFocusList()
        {
            comboBoxProcessList.Items.Clear();
            comboBoxProcessList.Sorted = true;

            comboBoxProcessList.Items.Add(string.Empty);

            foreach (Process process in Process.GetProcesses())
            {
                if (!comboBoxProcessList.Items.Contains(process.ProcessName))
                {
                    comboBoxProcessList.Items.Add(process.ProcessName);
                }
            }

            string applicationFocus = _config.Settings.User.GetByKey("ApplicationFocus", _config.Settings.DefaultSettings.ApplicationFocus).Value.ToString();

            if (string.IsNullOrEmpty(applicationFocus))
            {
                comboBoxProcessList.SelectedIndex = 0;

                return;
            }

            if (!comboBoxProcessList.Items.Contains(applicationFocus))
            {
                comboBoxProcessList.Items.Add(applicationFocus);
            }

            comboBoxProcessList.SelectedIndex = comboBoxProcessList.Items.IndexOf(applicationFocus);

            numericUpDownApplicationFocusDelayBefore.Value = Convert.ToInt32(_config.Settings.User.GetByKey("ApplicationFocusDelayBefore", _config.Settings.DefaultSettings.ApplicationFocusDelayBefore).Value);
            numericUpDownApplicationFocusDelayAfter.Value = Convert.ToInt32(_config.Settings.User.GetByKey("ApplicationFocusDelayAfter", _config.Settings.DefaultSettings.ApplicationFocusDelayAfter).Value);
        }

        public void DoApplicationFocus()
        {
            int delayBefore = (int)numericUpDownApplicationFocusDelayBefore.Value;
            int delayAfter = (int)numericUpDownApplicationFocusDelayAfter.Value;

            if (delayBefore > 0)
            {
                System.Threading.Thread.Sleep(delayBefore);
            }

            _screenCapture.SetApplicationFocus(comboBoxProcessList.Text);

            if (delayAfter > 0)
            {
                System.Threading.Thread.Sleep(delayAfter);
            }
        }

        public void SetApplicationFocus(string applicationFocus)
        {
            if (string.IsNullOrEmpty(applicationFocus))
            {
                _config.Settings.User.SetValueByKey("ApplicationFocus", string.Empty);
            }
            else
            {
                applicationFocus = applicationFocus.Trim();

                _config.Settings.User.SetValueByKey("ApplicationFocus", applicationFocus);

                if (!_config.Settings.User.Save(_config.Settings, _fileSystem))
                {
                    _screenCapture.ApplicationError = true;
                }
            }

            RefreshApplicationFocusList();

            DoApplicationFocus();
        }
    }
}
