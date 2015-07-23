using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Configuration;
using System.Diagnostics;

namespace CheckInProgram
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                Print.show("程序已经打开，请不要重复点击。");
                return;
            }
            Application.Run(new MainForm());
        }
        static public string GetLocalIp()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostByName(hostname);
            IPAddress localaddr = localhost.AddressList[0];
            foreach (IPAddress ip in localhost.AddressList )
            {
                if (ip.ToString().Split('.')[0] == "10")
                {
                    return ip.ToString();
                }
            }
            Print.show("这不是签到机器！");
            Application.Exit();
            return "";
        }
        static public string Version = ConfigurationManager.AppSettings["Version"];
        static public string Name = ConfigurationManager.AppSettings["Name"];
        static public string Author = ConfigurationManager.AppSettings["Author"];
        static public string ConnectString = @"Data Source=(localdb)\ProjectsV12;Initial Catalog=ForeSea;Integrated Security=True;";
        static public string Powered = ConfigurationManager.AppSettings["Powered"];
        static public string LinkLabel = ConfigurationManager.AppSettings["LinkLabel"];
        static public string LinkUrl = ConfigurationManager.AppSettings["LinkUrl"];
        static public string ShowUpdate = ConfigurationManager.AppSettings["ShowUpdate"];
        static public string UpdateString = ConfigurationManager.AppSettings["UpdateString"];
        static public string ShowPage = ConfigurationManager.AppSettings["ShowPage"];
        static public string PageLabel = ConfigurationManager.AppSettings["PageLabel"];
        static public string PageUrl = ConfigurationManager.AppSettings["PageUrl"];
        static public string UCP = ConfigurationManager.AppSettings["UnCheckProbability"];
        static public string KP = ConfigurationManager.AppSettings["KeepFrequency"];
        static public string Overtime = ConfigurationManager.AppSettings["OverTime"];
        static public SqlConnection conn = new SqlConnection(Program.ConnectString);
        static public Student student;
    }
}
