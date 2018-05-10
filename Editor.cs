//-----------------------------------------------------------------------
// <copyright file="Editor.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary>A class representing an image editing application.</summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    /// <summary>
    /// An editor is essentially an application (with arguments) that Auto Screen Capture will run when the user wants to edit a screenshot image.
    /// </summary>
    public class Editor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Editor"/> class.
        /// </summary>
        public Editor()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Editor"/> class.
        /// </summary>
        /// <param name="name">The name of the editor.</param>
        /// <param name="application">The executable associated with the editor.</param>
        /// <param name="arguments">The arguments that will be given to the executable.</param>
        public Editor(string name, string application, string arguments)
        {
            this.Name = name;
            this.Arguments = arguments;
            this.Application = application;
        }

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
    }
}