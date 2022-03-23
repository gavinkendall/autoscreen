//-----------------------------------------------------------------------
// <copyright file="SftpClient.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A wrapper of Renci SSH Net.</summary>
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
using System.IO;

namespace Gavin.Kendall.SFTP
{
    /// <summary>
    /// An SFTP client to handle SFTP operations.
    /// </summary>
    public class SftpClient
    {
        private Renci.SshNet.SftpClient _sftpClient;

        /// <summary>
        /// An SFTP client to handle SFTP operations.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port number.</param>
        /// <param name="username">The username to use to connect to the host.</param>
        /// <param name="password">The password to use to connect to the host.</param>
        public SftpClient(string host, int port, string username, string password)
        {
            _sftpClient = new Renci.SshNet.SftpClient(host, port, username, password);
        }

        /// <summary>
        /// Connects to the host.
        /// </summary>
        /// <returns>Returns true if connection was successful or false if connection was not successful.</returns>
        public bool Connect()
        {
            try
            {
                _sftpClient.Connect();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Disconnects from the SFTP server.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                _sftpClient.Disconnect();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Determines if we are connected to the SFTP server.
        /// </summary>
        public bool IsConnected { get { return _sftpClient.IsConnected; } }

        /// <summary>
        /// Uploads a file to the SFTP server.
        /// </summary>
        /// <param name="sourcePath">The local path of the file to read from on the client.</param>
        /// <param name="destinationPath">The remote path to write to on the server.</param>
        /// <returns>True if the upload was successful otherwise false if the upload failed.</returns>
        public bool UploadFile(string sourcePath, string destinationPath)
        {
            try
            {
                using (var fileStream = File.OpenRead(sourcePath))
                {
                    _sftpClient.UploadFile(fileStream, destinationPath, true);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
