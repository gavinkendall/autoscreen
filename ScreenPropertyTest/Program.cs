using System;
using System.Windows;
using System.Windows.Forms;

namespace ScreenPropertyTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*** Primary Screen (Bounds) ***");
            Console.WriteLine("Width: " + Screen.PrimaryScreen.Bounds.Width);
            Console.WriteLine("Height: " + Screen.PrimaryScreen.Bounds.Height);

            Console.WriteLine("\n*** Primary Screen (Working Area) ***");
            Console.WriteLine("Width: " + Screen.PrimaryScreen.WorkingArea.Width);
            Console.WriteLine("Height: " + Screen.PrimaryScreen.WorkingArea.Height);

            Console.WriteLine("\n*** Virtual Screen (SysParam) ***");
            Console.WriteLine("Width: " + SystemParameters.VirtualScreenWidth);
            Console.WriteLine("Height: " + SystemParameters.VirtualScreenHeight);

            Console.WriteLine("\n*** Virtual Screen (SysInfo) ***");
            Console.WriteLine("Width: " + SystemInformation.VirtualScreen.Width);
            Console.WriteLine("Height: " + SystemInformation.VirtualScreen.Height);

            Console.ReadLine();
        }
    }
}
