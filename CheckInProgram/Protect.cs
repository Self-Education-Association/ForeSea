using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Daemon
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static int Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            while (WaitForExitCode(@"E:\EntityFrameworkTest\Main\bin\Debug\Main.exe") != 0)
            {
                //Console.WriteLine("程序异常退出");
                
            }
            Console.ReadKey();
            return 0;
        }

        private static int WaitForExitCode(string filePath)
        {
            Process process = Process.Start(filePath);
            process.WaitForExit();
            return process.ExitCode;
        }
    }
}
