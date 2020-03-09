//-----------------------------------------------------------------------
// <copyright file="Date.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class Date
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime Value { get; set; }

        public bool Saved { get; set; }

        public Date()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Date(DateTime value)
        {
            Value = value;
        }
    }
}
