using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
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
                Error("程序已经打开，请不要重复点击！\n\r错误代码103");
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
            throw new Exception("这不是签到机器！");
        }
        static public void Error(string error)
        {
            try
            {
                error = System.Text.RegularExpressions.Regex.Unescape(error);
                SqlCommand insert = new SqlCommand("dbo.CheckInError", conn);
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.Add(new SqlParameter("@eid", SqlDbType.Char, 3));
                insert.Parameters.Add(new SqlParameter("@ip", SqlDbType.VarChar, 15));
                insert.Parameters.Add(new SqlParameter("@sid", SqlDbType.VarChar, 20));
                insert.Parameters.Add(new SqlParameter("@desc", SqlDbType.NText));
                if (error.Length <= 3)
                    insert.Parameters["@eid"].Value = "60X";
                else if (error.Length > 3)
                    insert.Parameters["@eid"].Value = error.Substring(error.Length - 3, 3);
                insert.Parameters["@ip"].Value = GetLocalIp();
                insert.Parameters["@sid"].Value = sID;
                insert.Parameters["@desc"].Value = error;
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                insert.ExecuteScalar();
                conn.Close();
                MessageBox.Show(error, "签到失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                return;
            }
            finally
            {
                Program.conn.Close();
            }
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
        static public string sName = "";
        static public string sID = "";
        static public string sState = "";
        static public string sRoom = "";
        static public short sLesson = 0;
        static public SqlConnection conn = new SqlConnection(Program.ConnectString);
    }
}
