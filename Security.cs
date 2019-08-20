//-----------------------------------------------------------------------
// <copyright file="Security.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace AutoScreenCapture
{
    public static class Security
    {
        public static string Hash(string text)
        {
            SHA512Managed sha512 = new SHA512Managed();
            return Regex.Replace(BitConverter.ToString(sha512.ComputeHash(ASCIIEncoding.Default.GetBytes(text))), "-", "").ToLower();
        }
    }
}
