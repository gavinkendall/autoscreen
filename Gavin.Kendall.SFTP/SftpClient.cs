using System;
using System.IO;

namespace Gavin.Kendall.SFTP
{
    public class SftpClient
    {
        private Renci.SshNet.SftpClient _sftpClient;

        public SftpClient(string host, int port, string username, string password)
        {
            _sftpClient = new Renci.SshNet.SftpClient(host, port, username, password);
        }

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
