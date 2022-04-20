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
            catch
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
            catch
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
        /// <param name="destinationFolderPath">The remote folder path to write to on the server.</param>
        /// <param name="destinationFilename">The remote filename to write to on the server.</param>
        /// <param name="isLinux">Determines if we are connecting to a Linux server (if so then backslashes in the destination folder path are replaced with forward slashes).</param>
        /// <returns>True if the upload was successful otherwise false if the upload failed.</returns>
        public bool UploadFile(string sourcePath, string destinationFolderPath, string destinationFilename, bool isLinux = true)
        {
            try
            {
                string initialWorkingDirectory = _sftpClient.WorkingDirectory;

                if (!string.IsNullOrEmpty(destinationFolderPath))
                {
                    if (isLinux)
                    {
                        destinationFolderPath = destinationFolderPath.Replace("\\", "/");

                        foreach (string folder in destinationFolderPath.Split('/'))
                        {
                            CreateRemoteDirectory(folder);

                            _sftpClient.ChangeDirectory(folder);
                        }
                    }
                    else
                    {
                        CreateRemoteDirectory(destinationFolderPath);

                        _sftpClient.ChangeDirectory(destinationFolderPath);

                    }
                }

                using (var fileStream = File.OpenRead(sourcePath))
                {
                    _sftpClient.UploadFile(fileStream, destinationFilename, true);
                }

                // We have to make sure that we change directory back to the initial working directory
                // so we avoid creating sub-folders within sub-folders. We always want to start from the root
                // with each call of the UploadFile method.
                _sftpClient.ChangeDirectory(initialWorkingDirectory);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Create a directory on the remote server. Catch any exception errors.
        /// </summary>
        /// <param name="folder">The folder to create on the remote server.</param>
        private void CreateRemoteDirectory(string folder)
        {
            try
            {
                _sftpClient.CreateDirectory(folder);
            }
            catch
            {

            }
        }
    }
}
