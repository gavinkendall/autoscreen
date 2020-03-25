//-----------------------------------------------------------------------
// <copyright file="MacroTagFunctionParser.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.Text.RegularExpressions;

namespace AutoScreenCapture
{
    /// <summary>
    /// 
    /// </summary>
    public static class MacroTagFunctionParser
    {
        // A macro tag function is surrounded by curly braces and gets interpreted to return a result.
        // At the moment I'm only interested in implementing date/time format functions.
        //
        // For example ...
        // {year-1}            1 year behind the given DateTime
        // {minute+5}          5 minutes ahead the given DateTime
        private static readonly string DateTimeFormatTagFunctionRegex = @"^\{(?<DateTimePart>year|month|day|hour|minute|second)(?<Operator>[\-\+])(?<Value>\d{1,5})\}$";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="tagFunction"></param>
        /// <returns></returns>
        public static string ParseTagFunctionForDateTimeFormat(DateTime dateTime, string tagFunction)
        {
            string result = tagFunction;

            if (Regex.IsMatch(tagFunction, DateTimeFormatTagFunctionRegex))
            {
                string dateTimePart = Regex.Match(tagFunction, DateTimeFormatTagFunctionRegex).Groups["DateTimePart"].Value;
                string @operator = Regex.Match(tagFunction, DateTimeFormatTagFunctionRegex).Groups["Operator"].Value;
                int @value = Convert.ToInt32(Regex.Match(tagFunction, DateTimeFormatTagFunctionRegex).Groups["Value"].Value);

                if (@operator.Equals("-"))
                {
                    if (dateTimePart.Equals("year"))
                    {
                        dateTime = dateTime.AddYears(-@value);
                        result = dateTime.ToString(MacroParser.YearFormat);
                    }

                    if (dateTimePart.Equals("month"))
                    {
                        dateTime = dateTime.AddMonths(-@value);
                        result = dateTime.ToString(MacroParser.MonthFormat);
                    }

                    if (dateTimePart.Equals("day"))
                    {
                        dateTime = dateTime.AddDays(-@value);
                        result = dateTime.ToString(MacroParser.DayFormat);
                    }

                    if (dateTimePart.Equals("hour"))
                    {
                        dateTime = dateTime.AddHours(-@value);
                        result = dateTime.ToString(MacroParser.HourFormat);
                    }

                    if (dateTimePart.Equals("minute"))
                    {
                        dateTime = dateTime.AddMinutes(-@value);
                        result = dateTime.ToString(MacroParser.MinuteFormat);
                    }

                    if (dateTimePart.Equals("second"))
                    {
                        dateTime = dateTime.AddSeconds(-@value);
                        result = dateTime.ToString(MacroParser.SecondFormat);
                    }
                }

                if (@operator.Equals("+"))
                {
                    if (dateTimePart.Equals("year"))
                    {
                        dateTime = dateTime.AddYears(+@value);
                        result = dateTime.ToString(MacroParser.YearFormat);
                    }

                    if (dateTimePart.Equals("month"))
                    {
                        dateTime = dateTime.AddMonths(+@value);
                        result = dateTime.ToString(MacroParser.MonthFormat);
                    }

                    if (dateTimePart.Equals("day"))
                    {
                        dateTime = dateTime.AddDays(+@value);
                        result = dateTime.ToString(MacroParser.DayFormat);
                    }

                    if (dateTimePart.Equals("hour"))
                    {
                        dateTime = dateTime.AddHours(+@value);
                        result = dateTime.ToString(MacroParser.HourFormat);
                    }

                    if (dateTimePart.Equals("minute"))
                    {
                        dateTime = dateTime.AddMinutes(+@value);
                        result = dateTime.ToString(MacroParser.MinuteFormat);
                    }

                    if (dateTimePart.Equals("second"))
                    {
                        dateTime = dateTime.AddSeconds(+@value);
                        result = dateTime.ToString(MacroParser.SecondFormat);
                    }
                }
            }

            return result;
        }
    }
}
