/*
Copyright [2016] [puyang.c@foxmail.com]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.DirectoryServices; 

namespace CheckInProgram
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            KillOhterProcess();
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.SP_CheckIn_Run", conn);
                int result = Run(cmd);
                switch (result)
                {
                    case 101:
                        if (args.Count() != 0)
                        {
                            foreach (string arg in args)
                            {
                                if (arg == "fullscreen" && GetSplitStatus() == true)
                                    Application.Run(new SplitFlow());
                            }
                        }
                        else
                            Application.Run(new MainForm());
                        break;
                    case 103:
                        Application.Run(new Normal(new Student(cmd.Parameters["@id"].Value, cmd.Parameters["@name"].Value, cmd.Parameters["@state"].Value, cmd.Parameters["@room"].Value)));
                        break;
                    case 105:
                        return;
                    default:
                        Print.Show(result);
                        break;
                }
            }
            catch (Exception ex)
            {
                Print.Show(ex.Message);
                return;
            }
            finally
            {
                conn.Close();
            }
            //if(GetSplitStatus()==false)
            //{
            //    MessageBox.Show("分流已关闭");
            //}
            //else
            //{
            //    MessageBox.Show("分流已启动");
            //}
            //Application.Run(new Normal(new Student(0,"",0,"")));
        }
        static public string GetLocalIp()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
            IPAddress localaddr = localhost.AddressList[0];
            foreach (IPAddress ip in localhost.AddressList )
            {
                if (ip.ToString().Split('.')[0] == "10")
                {
                    return ip.ToString();
                }
            }
            Print.Show("这不是签到机器！");
            Application.Exit();
            return "";
        }

        private static bool GetSplitStatus()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.SP_CheckIn_GetSplitStatus",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@result",SqlDbType.TinyInt));
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                conn.Open();
                cmd.ExecuteScalar();
                conn.Close();
                if (int.Parse(cmd.Parameters["@result"].Value.ToString()) == 0)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        static public int Run(SqlCommand cmd)
        {
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ip", SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 20));
                cmd.Parameters.Add(new SqlParameter("@state", SqlDbType.TinyInt));
                cmd.Parameters.Add(new SqlParameter("@room", SqlDbType.NVarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.SmallInt));
                cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                cmd.Parameters["@name"].Direction = ParameterDirection.Output;
                cmd.Parameters["@state"].Direction = ParameterDirection.Output;
                cmd.Parameters["@room"].Direction = ParameterDirection.Output;
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters["@ip"].Value = GetLocalIp();
                string[] ip = cmd.Parameters["@ip"].Value.ToString().Split('.');
                //区分考试区电脑
                if (ip[2] == "12" && int.Parse(ip[3]) >= 109)
                    return 105;
                conn.Open();
                cmd.ExecuteScalar();
                conn.Close();
                return int.Parse(cmd.Parameters["@result"].Value.ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        static public void KillOhterProcess()
        {
            Process[] ps = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (ps.Length > 1)
            {
                foreach (Process item in ps)
                {
                    if (item.StartTime != Process.GetCurrentProcess().StartTime)
                        item.Kill();
                }
            }
        }

        public static bool TryAuthenticate(string username, string userpwd)
        {
            bool isLogin = false;
            string domain = "uibe.edu";
            if (username == "duty")
                return false;
            if (username == @"uibe\duty")
                return false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry(string.Format("LDAP://uibe.edu/OU=Users,OU=SEA团队,OU=2014外语自学中心用户,OU=过度,DC=uibe,DC=edu", domain), username, userpwd);
                entry.RefreshCache();
                isLogin = true;
            }
            catch (Exception)
            {
                isLogin = false;
            }
            return isLogin;
        }
        static public string Version = ConfigurationManager.AppSettings["Version"];
        static public string Name = ConfigurationManager.AppSettings["Name"];
        static public string Author = ConfigurationManager.AppSettings["Author"];
        static public string ConnectString = @"Server=10.1.1.68\seasqlserver;Database=ForeSea;User ID=checkin;Password=seacheckin;Trusted_Connection=false;";
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
    }
}
