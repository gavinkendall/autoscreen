//-----------------------------------------------------------------------
// <copyright file="MacroTag.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    public class MacroTag
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public MacroTag(string name, string description)
        {
            Name = name;
            Description = !string.IsNullOrEmpty(name) ? name + " - " + description : string.Empty;
        }
    }
}
