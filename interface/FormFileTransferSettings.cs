//-----------------------------------------------------------------------
// <copyright file="FormFileTransferSettings.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
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
        /// <summary>
        /// File Transfer Settings
        /// </summary>
        public FormFileTransferSettings()
        {
            InitializeComponent();
        }

        private void FormFileTransferSettings_Load(object sender, EventArgs e)
        {
            textBoxHost.Text = Settings.SFTP.GetByKey("FileTransferServerHost", DefaultSettings.FileTransferServerHost).Value.ToString();
            numericUpDownPort.Value = Convert.ToInt32(Settings.SFTP.GetByKey("FileTransferServerPort", DefaultSettings.FileTransferServerPort).Value);
            textBoxUsername.Text = Settings.SFTP.GetByKey("FileTransferClientUsername", DefaultSettings.FileTransferClientUsername).Value.ToString();
            textBoxPassword.Text = Settings.SFTP.GetByKey("FileTransferClientPassword", DefaultSettings.FileTransferClientPassword).Value.ToString();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Settings.SFTP.GetByKey("FileTransferServerHost", DefaultSettings.FileTransferServerHost).Value = textBoxHost.Text;
            Settings.SFTP.GetByKey("FileTransferServerPort", DefaultSettings.FileTransferServerPort).Value = numericUpDownPort.Value;
            Settings.SFTP.GetByKey("FileTransferClientUsername", DefaultSettings.FileTransferClientUsername).Value = textBoxUsername.Text;
            Settings.SFTP.GetByKey("FileTransferClientPassword", DefaultSettings.FileTransferClientPassword).Value = textBoxPassword.Text;

            Settings.SFTP.Save();

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

                Gavin.Kendall.SFTP.SftpClient sftpClient = new Gavin.Kendall.SFTP.SftpClient(textBoxHost.Text, (int)numericUpDownPort.Value, textBoxUsername.Text, textBoxPassword.Text);
                
                if (!sftpClient.Connect())
                {
                    MessageBox.Show("A connection could not be established with the file server.", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (sftpClient.IsConnected)
                {
                    MessageBox.Show("A connection was successfully established with the file server.", "Connection Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                sftpClient.Disconnect();
            }
            catch (Exception)
            {
                MessageBox.Show("An error was encountered when attempting to establish a connection with the file server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
