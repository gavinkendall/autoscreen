//-----------------------------------------------------------------------
// <copyright file="HelpTip.cs" company="Gavin Kendall">
//     Copyright (c) 2008-2021 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Help tips scroll through on a timer to display helpful information to the user.</summary>
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
    /// This class is used for passing a help tip message from any class that doesn't have access to the help bar.
    /// </summary>
    public static class HelpTip
    {
        /// <summary>
        /// Any message to be displayed in the help bar.
        /// </summary>
        public static string Message { get; set; }
    }
}
