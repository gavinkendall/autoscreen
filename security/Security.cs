//-----------------------------------------------------------------------
// <copyright file="Security.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>This class is used for hashing the given passphrase and providing encryption and decryption methods for text and files.</summary>
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
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class for security-related methods.
    /// </summary>
    public class Security
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Security()
        {

        }

        /// <summary>
        /// Accepts plaintext and returns a SHA-512 hash of it.
        /// </summary>
        /// <param name="text">Any text to hash.</param>
        /// <returns>SHA-512 hash of the given text.</returns>
        public string Hash(string text)
        {
            var sha512 = new SHA512Managed();
            return Regex.Replace(BitConverter.ToString(sha512.ComputeHash(Encoding.Default.GetBytes(text))), "-", "").ToLower();
        }

        /// <summary>
        /// Encrypts the supplied string using a key.
        /// </summary>
        /// <param name="text">The text to encrypt.</param>
        /// <param name="key">The key to use.</param>
        /// <returns>The encrypted text.</returns>
        public string EncryptText(string text, string key)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            return Convert.ToBase64String(EncryptText(Encoding.Unicode.GetBytes(text), pdb.GetBytes(32), pdb.GetBytes(16)));
        }

        /// <summary>
        /// Decrypts the supplied encrypted string using a key. Please be aware that
        /// the key which encrypted the text needs to be used to decrypt the text.
        /// </summary>
        /// <param name="text">The encrypted text to decrypt.</param>
        /// <param name="key">The key to use.</param>
        /// <returns>The decrypted text.</returns>
        public string DecryptText(string text, string key)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            return Encoding.Unicode.GetString(DecryptText(Convert.FromBase64String(text), pdb.GetBytes(32), pdb.GetBytes(16)));
        }

        /// <summary>
        /// Encrypt a file by providing the filepath of the source (the original file) and the destination (the encrypted file that will be written). A Base64 encoded key is returned after encryption. This is the key used during encryption.
        /// </summary>
        /// <param name="source">The filepath of the file to encrypt.</param>
        /// <param name="destination">The filepath of the file that has been encrypted.</param>
        /// <returns>The key (Base64 encoded) that was used for encryption.</returns>
        public string EncryptFile(string source, string destination)
        {
            try
            {
                string keyBase64Encoded = string.Empty;

                using (var sourceStream = File.OpenRead(source))
                {
                    using (var destinationStream = File.Create(destination))
                    {
                        using (var provider = new AesCryptoServiceProvider())
                        {
                            provider.Padding = PaddingMode.PKCS7;

                            using (var cryptoTransform = provider.CreateEncryptor())
                            {
                                using (var cryptoStream = new CryptoStream(destinationStream, cryptoTransform, CryptoStreamMode.Write))
                                {
                                    destinationStream.Write(provider.IV, 0, provider.IV.Length);
                                    sourceStream.CopyTo(cryptoStream);

                                    keyBase64Encoded = Convert.ToBase64String(provider.Key);
                                }
                            }
                        }
                    }
                }

                return keyBase64Encoded;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Decrypt an encrypted file (source) to write to a file that is decrypted (destination) using the given key. Returns true if the decryption was successful. Returns false if the decryption failed.
        /// </summary>
        /// <param name="source">The filepath of the encrypted file.</param>
        /// <param name="destination">The filepath of the file that has been decrypted.</param>
        /// <param name="key">The key to use to decrypt the file.</param>
        public void DecryptFile(string source, string destination, string key)
        {
            try
            {
                byte[] keyByteArray = Convert.FromBase64String(key);

                using (var sourceStream = File.OpenRead(source))
                {
                    using (var destinationStream = File.Create(destination))
                    {
                        using (var provider = new AesCryptoServiceProvider())
                        {
                            provider.Padding = PaddingMode.PKCS7;

                            var IV = new byte[provider.IV.Length];
                            sourceStream.Read(IV, 0, IV.Length);

                            using (var cryptoTransform = provider.CreateDecryptor(keyByteArray, IV))
                            {
                                using (var cryptoStream = new CryptoStream(sourceStream, cryptoTransform, CryptoStreamMode.Read))
                                {
                                    cryptoStream.CopyTo(destinationStream);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Internal encryption method.
        /// </summary>
        /// <param name="text">The text to encrypt.</param>
        /// <param name="key">The key to use.</param>
        /// <param name="iv">The initialization vector to use.</param>
        /// <returns>The encrypted text.</returns>
        private byte[] EncryptText(byte[] text, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(text, 0, text.Length);
            cs.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// Internal decryption method.
        /// </summary>
        /// <param name="text">The text to decrypt.</param>
        /// <param name="key">The key to use.</param>
        /// <param name="iv">The initialization vector to use.</param>
        /// <returns>The decrypted text.</returns>
        private byte[] DecryptText(byte[] text, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(text, 0, text.Length);
            cs.Close();
            return ms.ToArray();
        }
    }
}
