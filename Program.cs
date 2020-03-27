//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            using (new Mutex(false, ((GuidAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute), false)).Value, out bool createdNew))
            {
                if (createdNew)
                {
                    Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain(args));
                }
            }
        }
    }
}