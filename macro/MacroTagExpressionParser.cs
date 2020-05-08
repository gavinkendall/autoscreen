//-----------------------------------------------------------------------
// <copyright file="MacroTagFunctionParser.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Parses a formatted date/time value to figure out what operators to apply (whether it be + or -) for adding or removing time.</summary>
//-----------------------------------------------------------------------
using System;
using System.Text.RegularExpressions;

namespace AutoScreenCapture
{
    /// <summary>
    /// Parses macro tag expressions.
    /// </summary>
    public static class MacroTagExpressionParser
    {
        // A macro tag expression is surrounded by curly braces and gets interpreted to return a result.
        // At the moment I'm only interested in implementing date/time format expressions.
        //
        // For example ...
        // {year-1}            1 year behind the given DateTime
        // {minute+5}          5 minutes ahead the given DateTime
        private static readonly string DateTimeFormatTagExpressionRegex = @"^\{(?<DateTimePart>year|month|day|hour|minute|second)(?<Operator>[\-\+])(?<Value>\d{1,5})\}$";

        /// <summary>
        /// Parses macro tag expressions for date/time format.
        /// </summary>
        /// <param name="dateTime">The date/time object to parse.</param>
        /// <param name="tagExpression">The tag expression to use on the date/time object.</param>
        /// <returns>A parsed macro string value.</returns>
        public static string ParseTagExpressionForDateTimeFormat(DateTime dateTime, string tagExpression)
        {
            string result = tagExpression;

            if (Regex.IsMatch(tagExpression, DateTimeFormatTagExpressionRegex))
            {
                string dateTimePart = Regex.Match(tagExpression, DateTimeFormatTagExpressionRegex).Groups["DateTimePart"].Value;
                string @operator = Regex.Match(tagExpression, DateTimeFormatTagExpressionRegex).Groups["Operator"].Value;
                int @value = Convert.ToInt32(Regex.Match(tagExpression, DateTimeFormatTagExpressionRegex).Groups["Value"].Value);

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
