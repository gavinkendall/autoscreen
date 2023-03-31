//-----------------------------------------------------------------------
// <copyright file="FormFileTransferSettings.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods handling screenshots in the interface.</summary>
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
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// File Transfer Settings
    /// </summary>
    public partial class FormFileTransferSettings : Form
    {
        private readonly Config _config;
        private readonly FileSystem _fileSystem;
        private readonly Log _log;

        /// <summary>
        /// File Transfer Settings
        /// </summary>
        public FormFileTransferSettings(Config config, FileSystem fileSystem, Log log)
        {
            InitializeComponent();

            _config = config;
            _fileSystem = fileSystem;
            _log = log;
        }

        private void FormFileTransferSettings_Load(object sender, EventArgs e)
        {
            textBoxHost.Text = _config.Settings.SFTP.GetByKey("FileTransferServerHost").Value.ToString();
            numericUpDownPort.Value = Convert.ToInt32(_config.Settings.SFTP.GetByKey("FileTransferServerPort").Value);
            textBoxUsername.Text = _config.Settings.SFTP.GetByKey("FileTransferClientUsername").Value.ToString();
            textBoxPassword.Text = _config.Settings.SFTP.GetByKey("FileTransferClientPassword").Value.ToString();

            // To maintain backwards compatibility with 2.4 when upgrading from 2.4 to 2.5 and using an old 2.4 version of autoscreen.conf
            Setting fileTransferIsLinuxServerSetting = _config.Settings.SFTP.GetByKey("FileTransferIsLinuxServer");

            if (fileTransferIsLinuxServerSetting != null)
            {
                checkBoxIsLinux.Checked = Convert.ToBoolean(_config.Settings.SFTP.GetByKey("FileTransferIsLinuxServer").Value);
            }
            else
            {
                checkBoxIsLinux.Checked = true;
            }

            checkBoxDeleteLocalFileAfterSuccessfulUpload.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("SFTPDeleteLocalFileAfterSuccessfulUpload", false, true).Value);
            checkBoxKeepFailedUploads.Checked = Convert.ToBoolean(_config.Settings.User.GetByKey("SFTPKeepFailedUploads", true, true).Value);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _config.Settings.SFTP.SetValueByKey("FileTransferServerHost", textBoxHost.Text);
            _config.Settings.SFTP.SetValueByKey("FileTransferServerPort", numericUpDownPort.Value);
            _config.Settings.SFTP.SetValueByKey("FileTransferClientUsername", textBoxUsername.Text);
            _config.Settings.SFTP.SetValueByKey("FileTransferClientPassword", textBoxPassword.Text);
            _config.Settings.SFTP.SetValueByKey("FileTransferIsLinuxServer", checkBoxIsLinux.Checked);

            _config.Settings.SFTP.Save(_config.Settings, _fileSystem);

            DialogResult = DialogResult.OK;

            Close();
        }

        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxHost.Text) ||
                    (numericUpDownPort.Value < numericUpDownPort.Minimum || numericUpDownPort.Value > numericUpDownPort.Maximum) ||
                    string.IsNullOrEmpty(textBoxUsername.Text) ||
                    string.IsNullOrEmpty(textBoxPassword.Text))
                {
                    MessageBox.Show("Please make sure that at least the Host, Port, Username, and Password fields are correct.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                buttonTestConnection.Text = "Connecting ...";
                buttonTestConnection.Enabled = false;

                Gavin.Kendall.SFTP.SftpClient sftpClient = new Gavin.Kendall.SFTP.SftpClient(textBoxHost.Text, (int)numericUpDownPort.Value, textBoxUsername.Text, textBoxPassword.Text);
                
                if (!sftpClient.Connect())
                {
                    MessageBox.Show("A connection could not be established with the file server.", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    buttonTestConnection.Text = "Test Connection";
                    buttonTestConnection.Enabled = true;

                    return;
                }

                if (sftpClient.IsConnected)
                {
                    MessageBox.Show("A connection was successfully established with the file server.", "Connection Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                sftpClient.Disconnect();

                buttonTestConnection.Text = "Test Connection";
                buttonTestConnection.Enabled = true;
            }
            catch (Exception ex)
            {
                buttonTestConnection.Text = "Test Connection";
                buttonTestConnection.Enabled = true;

                _log.WriteErrorMessage(ex.Message + "\n" + ex.StackTrace);

                MessageBox.Show("An error was encountered when attempting to establish a connection with the file server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
