//-----------------------------------------------------------------------
// <copyright file="Trigger.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing a trigger.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public class Trigger
    {
        public Trigger()
        {

        }

        public Trigger(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
