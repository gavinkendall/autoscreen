//-----------------------------------------------------------------------
// <copyright file="Security.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>This class is used for hashing the given passphrase.</summary>
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
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class for security-related methods.
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// Accepts plaintext and returns a SHA-512 hash of it.
        /// </summary>
        /// <param name="text">Any text to hash.</param>
        /// <returns>SHA-512 hash of the given text.</returns>
        public static string Hash(string text)
        {
            var sha512 = new SHA512Managed();
            return Regex.Replace(BitConverter.ToString(sha512.ComputeHash(Encoding.Default.GetBytes(text))), "-", "").ToLower();
        }
    }
}
