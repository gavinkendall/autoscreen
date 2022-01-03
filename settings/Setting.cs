//-----------------------------------------------------------------------
// <copyright file="Setting.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing an application setting.</summary>
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
namespace AutoScreenCapture
{
    /// <summary>
    /// A class representing a setting (whether it be an application setting or a user setting).
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Empty constructor for a Setting object.
        /// </summary>
        public Setting()
        {
        }

        /// <summary>
        /// A constructor for a Setting that accepts a key (which must be unique) and any object as its value.
        /// </summary>
        /// <param name="key">The key for the Setting.</param>
        /// <param name="value">The value for the Setting.</param>
        public Setting(string key, object value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// A string value representing a unique key for a setting.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// An object value containing the value of the setting.
        /// </summary>
        public object Value { get; set; }
    }
}