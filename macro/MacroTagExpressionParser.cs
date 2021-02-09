//-----------------------------------------------------------------------
// <copyright file="MacroTagExpressionParser.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Parses a formatted date/time value to figure out what operators to apply (whether it be + or -) for adding or removing time.</summary>
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
using System.Text.RegularExpressions;

namespace AutoScreenCapture
{
    /// <summary>
    /// Parses macro tag expressions.
    /// </summary>
    public class MacroTagExpressionParser
    {
        // A macro tag expression is surrounded by curly braces and gets interpreted to return a result.
        // At the moment I'm only interested in implementing date/time format expressions.
        //
        // For example ...
        // {year-1}            1 year behind the given DateTime
        // {minute+5}          5 minutes ahead the given DateTime
        private readonly string DateTimeFormatTagExpressionRegex = @"^\{(?<DateTimePart>year|month|day|hour|minute|second)(?<Operator>[\-\+])(?<Value>\d{1,5})\}(?<DateTimeFormat>\[.+\])?$";

        public MacroTagExpressionParser()
        {

        }

        /// <summary>
        /// Parses macro tag expressions for date/time format.
        /// </summary>
        /// <param name="dateTime">The date/time object to parse.</param>
        /// <param name="tagExpression">The tag expression to use on the date/time object.</param>
        /// <returns>A parsed macro string value.</returns>
        public string ParseTagExpressionForDateTimeFormat(DateTime dateTime, string tagExpression, MacroParser macroParser)
        {
            string result = tagExpression;

            if (Regex.IsMatch(tagExpression, DateTimeFormatTagExpressionRegex))
            {
                string dateTimePart = Regex.Match(tagExpression, DateTimeFormatTagExpressionRegex).Groups["DateTimePart"].Value;
                string @operator = Regex.Match(tagExpression, DateTimeFormatTagExpressionRegex).Groups["Operator"].Value;
                int @value = Convert.ToInt32(Regex.Match(tagExpression, DateTimeFormatTagExpressionRegex).Groups["Value"].Value);
                string dateTimeFormat = Regex.Match(tagExpression, DateTimeFormatTagExpressionRegex).Groups["DateTimeFormat"].Value.TrimStart('[').TrimEnd(']');

                if (@operator.Equals("-"))
                {
                    if (dateTimePart.Equals("year"))
                    {
                        dateTime = dateTime.AddYears(-@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.YearFormat) : dateTime.ToString(dateTimeFormat);
                    }

                    if (dateTimePart.Equals("month"))
                    {
                        dateTime = dateTime.AddMonths(-@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.MonthFormat) : dateTime.ToString(dateTimeFormat);
                    }

                    if (dateTimePart.Equals("day"))
                    {
                        dateTime = dateTime.AddDays(-@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.DayFormat) : dateTime.ToString(dateTimeFormat);
                    }

                    if (dateTimePart.Equals("hour"))
                    {
                        dateTime = dateTime.AddHours(-@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.HourFormat) : dateTime.ToString(dateTimeFormat);
                    }

                    if (dateTimePart.Equals("minute"))
                    {
                        dateTime = dateTime.AddMinutes(-@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.MinuteFormat) : dateTime.ToString(dateTimeFormat);
                    }

                    if (dateTimePart.Equals("second"))
                    {
                        dateTime = dateTime.AddSeconds(-@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.SecondFormat) : dateTime.ToString(dateTimeFormat);
                    }
                }

                if (@operator.Equals("+"))
                {
                    if (dateTimePart.Equals("year"))
                    {
                        dateTime = dateTime.AddYears(+@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.YearFormat) : dateTime.ToString(dateTimeFormat);
                    }

                    if (dateTimePart.Equals("month"))
                    {
                        dateTime = dateTime.AddMonths(+@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.MonthFormat) : dateTime.ToString(dateTimeFormat);
                    }

                    if (dateTimePart.Equals("day"))
                    {
                        dateTime = dateTime.AddDays(+@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.DayFormat) : dateTime.ToString(dateTimeFormat);
                    }

                    if (dateTimePart.Equals("hour"))
                    {
                        dateTime = dateTime.AddHours(+@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.HourFormat) : dateTime.ToString(dateTimeFormat);
                    }

                    if (dateTimePart.Equals("minute"))
                    {
                        dateTime = dateTime.AddMinutes(+@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.MinuteFormat) : dateTime.ToString(dateTimeFormat);
                    }

                    if (dateTimePart.Equals("second"))
                    {
                        dateTime = dateTime.AddSeconds(+@value);
                        result = string.IsNullOrEmpty(dateTimeFormat) ? dateTime.ToString(macroParser.SecondFormat) : dateTime.ToString(dateTimeFormat);
                    }
                }
            }

            return result;
        }
    }
}
