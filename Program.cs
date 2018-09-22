//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Gavin Kendall">
//     Copyright (c) Gavin Kendall. All rights reserved.
// </copyright>
// <author>Gavin Kendall</author>
// <summary></summary>
//-----------------------------------------------------------------------
namespace AutoScreenCapture
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Threading;

    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                if (!string.IsNullOrEmpty(arg) && arg.Equals("-debug"))
                {
                    Log.Enabled = true;

                    break;
                }
            }

            bool createdNew;
            using (new Mutex(false, ((GuidAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute), false)).Value, out createdNew))
            {
                if (createdNew)
                {
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain(args));
                }
                // Exit this application's duplicate process in case the user executes a second instance since we want to keep a single instance.
                else
                {
                    Log.Write("A duplicate instance of the application was found running. Exiting.");
                }
            }
        }
    }
}