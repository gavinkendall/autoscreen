//-----------------------------------------------------------------------
// <copyright file="Editor.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2022 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing an image editing application.</summary>
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
    /// An editor is essentially an application (with arguments) that Auto Screen Capture will run when the user wants to edit a screenshot image.
    /// </summary>
    public class Editor
    {
        /// <summary>
        /// Gets or sets the name of the editor.
        /// </summary>
        /// <value>The name of the editor.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the executable associated with the editor.
        /// </summary>
        /// <value>The executable associated with the editor.</value>
        public string Application { get; set; }

        /// <summary>
        /// Gets or sets the arguments that will be given to the executable.
        /// </summary>
        /// <value>The arguments that will be given to the executable.</value>
        public string Arguments { get; set; }

        /// <summary>
        /// Notes for the user to write in whatever they need to.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Editor"/> class.
        /// </summary>
        public Editor()
        {
            Notes = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Editor"/> class.
        /// </summary>
        /// <param name="name">The name of the editor.</param>
        /// <param name="application">The executable associated with the editor.</param>
        /// <param name="arguments">The arguments that will be given to the executable.</param>
        public Editor(string name, string application, string arguments)
        {
            Name = name;
            Arguments = arguments;
            Application = application;
            Notes = string.Empty;
        }
    }
}