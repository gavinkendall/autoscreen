﻿//-----------------------------------------------------------------------
// <copyright file="FormMain-Convert.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>All the methods for converting data.</summary>
//-----------------------------------------------------------------------
using System;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Converts the given hours, minutes, seconds, and milliseconds into an aggregate milliseconds value.
        /// </summary>
        /// <param name="hours">The number of hours to be converted.</param>
        /// <param name="minutes">The number of minutes to be converted.</param>
        /// <param name="seconds">The number of seconds to be converted.</param>
        /// <param name="milliseconds">The number of milliseconds to be converted.</param>
        /// <returns></returns>
        private int ConvertIntoMilliseconds(int hours, int minutes, int seconds, int milliseconds)
        {
            return DataConvert.ConvertIntoMilliseconds(hours, minutes, seconds, milliseconds);
        }

        /// <summary>
        /// Converts the string representation of a date into a DateTime object. Used by the RunDateSearch thread so we can set bolded dates in the calendar.
        /// </summary>
        /// <param name="date">A string representation of a date (such as "2019-02-06").</param>
        /// <returns>A DateTime object based on the provided date string.</returns>
        private DateTime ConvertDateStringToDateTime(string date)
        {
            return DataConvert.ConvertDateStringToDateTime(date);
        }
    }
}
