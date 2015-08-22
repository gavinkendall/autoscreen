/////////////////////////////////////////////////////////////////////////////////
// GK.Tools.Security
// Provides applications with encryption, hashing, and password creation.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//
// Wednesday, 23 January 2008 - Wednesday, 3 September 2008
// Copyright 2008 Gavin Kendall
//

using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AutoScreenCapture2
{
    /// <summary>
    /// A class that provides applications with encryption, hashing, and password creation.
    /// </summary>
    public class Security
    {
        private string defaultKey;
        private ArrayList errorLog;
        private int defaultRndByteLength;
        private int defaultPasswordLength;
        private string defaultAcceptableChars;

        /// <summary>
        /// A class that provides applications with encryption, hashing, and password creation.
        /// </summary>
        public Security()
        {
            try
            {
                defaultRndByteLength = 8;
                errorLog = new ArrayList();
                defaultPasswordLength = 16;
                defaultAcceptableChars = "0123456789!@#$%^&*()-_+=ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                defaultKey = Environment.MachineName;
            }
            catch (Exception)
            {
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Gets information about the author of this class.
        /// </summary>
        public string Credits
        {
            get
            {
                return "GK.Tools.dll\nGK.Tools.Security\ngavinkendall@gmail.com\nhttp://gir.slampt.net/~gavin/\nCopyright 2008 Gavin Kendall";
            }
        }

        /// <summary>
        /// Gets the error log that may contain a number of error messages
        /// populated by various exceptions.
        /// </summary>
        public ArrayList ErrorLog
        {
            get
            {
                return errorLog;
            }
        }

        /// <summary>
        /// Gets the last error message populated by an exception.
        /// </summary>
        public string LastError
        {
            get
            {
                if (errorLog.Count > 0)
                {
                    if (errorLog[(errorLog.Count - 1)] != null)
                    {
                        return errorLog[(errorLog.Count - 1)].ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets or sets the key with a password. The key is used when encrypting and decrypting text.
        /// Decrypt a string using the same key that encrypted the string.
        /// Set the key with a password before using the key otherwise
        /// the key will obtain its own password.
        /// </summary>
        public string Key
        {
            set
            {
                defaultKey = value;
            }

            get
            {
                return defaultKey;
            }
        }

        /// <summary>
        /// Returns an MD5 hash of the supplied text.
        /// </summary>
        /// <param name="text">The text to hash.</param>
        /// <returns>The hashed string.</returns>
        public string MD5(string text)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                return Regex.Replace(BitConverter.ToString(md5.ComputeHash(ASCIIEncoding.Default.GetBytes(text))), "-", "").ToLower();
            }
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Returns an SHA512 (SHA-2) hash of the supplied text.
        /// </summary>
        /// <param name="text">The text to hash.</param>
        /// <returns>The hashed string.</returns>
        public string SHA512(string text)
        {
            try
            {
                SHA512Managed sha512 = new SHA512Managed();
                return Regex.Replace(BitConverter.ToString(sha512.ComputeHash(ASCIIEncoding.Default.GetBytes(text))), "-", "").ToLower();
            }
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Encrypts the supplied string using the key that
        /// was previously set with the Key property.
        /// </summary>
        /// <param name="text">The text to encrypt.</param>
        /// <returns>The encrypted text.</returns>
        public string Encrypt(string text)
        {
            try
            {
                return Encrypt(text, defaultKey);
            }
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Encrypts the supplied string using a key.
        /// </summary>
        /// <param name="text">The text to encrypt.</param>
        /// <param name="key">The key to use.</param>
        /// <returns>The encrypted text.</returns>
        public string Encrypt(string text, string key)
        {
            try
            {
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                return Convert.ToBase64String(Encrypt(Encoding.Unicode.GetBytes(text), pdb.GetBytes(32), pdb.GetBytes(16)));
            }
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Decrypts the supplied string using the key
        /// that was previously set with the Key property.
        /// </summary>
        /// <param name="text">The text to decrypt.</param>
        /// <returns>The decrypted text.</returns>
        public string Decrypt(string text)
        {
            try
            {
                return Decrypt(text, defaultKey);
            }
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Decrypts the supplied encrypted string using a key. Please be aware that
        /// the key which encrypted the text needs to be used to decrypt the text.
        /// </summary>
        /// <param name="text">The encrypted text to decrypt.</param>
        /// <param name="key">The key to use.</param>
        /// <returns>The decrypted text.</returns>
        public string Decrypt(string text, string key)
        {
            try
            {
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                return Encoding.Unicode.GetString(Decrypt(Convert.FromBase64String(text), pdb.GetBytes(32), pdb.GetBytes(16)));
            }
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Internal encryption method.
        /// </summary>
        /// <param name="text">The text to encrypt.</param>
        /// <param name="key">The key to use.</param>
        /// <param name="iv">The initialization vector to use.</param>
        /// <returns>The encrypted text.</returns>
        private byte[] Encrypt(byte[] text, byte[] key, byte[] iv)
        {
            try
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
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Internal decryption method.
        /// </summary>
        /// <param name="text">The text to decrypt.</param>
        /// <param name="key">The key to use.</param>
        /// <param name="iv">The initialization vector to use.</param>
        /// <returns>The decrypted text.</returns>
        private byte[] Decrypt(byte[] text, byte[] key, byte[] iv)
        {
            try
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
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Returns the checksum of a file.
        /// </summary>
        /// <param name="filename">The filename of the file to check.</param>
        /// <returns>The MD5 sum hash of the file.</returns>
        public string MD5Sum(string filename)
        {
            try
            {
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + filename, FileMode.Open);
                string result = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(fs)).Replace("-", "").ToLower();
                fs.Close();
                return result;
            }
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Generates a random password using the default password length of 16.
        /// </summary>
        /// <returns>A random password.</returns>
        public string GenerateRandomPassword()
        {
            try
            {
                return GenerateRandomPassword(defaultPasswordLength);
            }
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Generates a random password using the supplied password length.
        /// </summary>
        /// <param name="passwordLength">The length of the password.</param>
        /// <returns>A random password.</returns>
        public string GenerateRandomPassword(int passwordLength)
        {
            try
            {
                return GenerateRandomPassword(passwordLength, defaultAcceptableChars);
            }
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Generates a random password using the acceptable characters provided.
        /// </summary>
        /// <param name="acceptableChars">A series of acceptable characters to use when generating the password.</param>
        /// <returns>A random password.</returns>
        public string GenerateRandomPassword(string acceptableChars)
        {
            try
            {
                return GenerateRandomPassword(defaultPasswordLength, acceptableChars);
            }
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Generates a random password using the supplied password length and a series of acceptable characters.
        /// </summary>
        /// <param name="passwordLength">The length of the password.</param>
        /// <param name="acceptableChars">A series of acceptable characters to use when generating the password.</param>
        /// <returns>A random password.</returns>
        public string GenerateRandomPassword(int passwordLength, string acceptableChars)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                for (int i = 0; i < passwordLength; i++)
                {
                    byte[] rndbytes = new byte[defaultRndByteLength];
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    rng.GetBytes(rndbytes);
                    str.Append(acceptableChars[new Random(BitConverter.ToInt32(rndbytes, 0)).Next(acceptableChars.Length)]);
                }

                return str.ToString();
            }
            catch (Exception ex)
            {
                errorLog.Add(ex.ToString());
                return null;
            }
        }
    }
}