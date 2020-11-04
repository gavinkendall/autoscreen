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

        public bool UploadFile(string path)
        {
            try
            {
                using (var fileStream = File.OpenRead(path))
                {
                    _sftpClient.UploadFile(fileStream, path, true);
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
