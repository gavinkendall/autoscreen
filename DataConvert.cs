//-----------------------------------------------------------------------
// <copyright file="DataConvert.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2023 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for converting data.</summary>
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

namespace AutoScreenCapture
{
    /// <summary>
    /// All the methods for converting data.
    /// </summary>
    public class DataConvert
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataConvert()
        {

        }

        /// <summary>
        /// Converts the given hours, minutes, seconds, and milliseconds into an aggregate milliseconds value.
        /// </summary>
        /// <param name="hours">The number of hours to be converted.</param>
        /// <param name="minutes">The number of minutes to be converted.</param>
        /// <param name="seconds">The number of seconds to be converted.</param>
        /// <param name="milliseconds">The number of milliseconds to be converted.</param>
        /// <returns></returns>
        public int ConvertIntoMilliseconds(int hours, int minutes, int seconds, int milliseconds)
        {
            return 1000 * (hours * 3600 + minutes * 60 + seconds) + milliseconds;
        }

        /// <summary>
        /// Converts the string representation of a date into a DateTime object. Used by the RunDateSearch thread so we can set bolded dates in the calendar.
        /// </summary>
        /// <param name="date">A string representation of a date (such as "2019-02-06").</param>
        /// <returns>A DateTime object based on the provided date string.</returns>
        public DateTime ConvertDateStringToDateTime(string date)
        {
            return new DateTime(System.Convert.ToInt32(date.Substring(0, 4)), System.Convert.ToInt32(date.Substring(5, 2)), System.Convert.ToInt32(date.Substring(8, 2)));
        }
    }
}
