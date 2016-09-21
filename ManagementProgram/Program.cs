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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;

namespace ManagementProgram
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
            if (Program.TryAuthenticate() == false)
                return;
            Application.Run(new MainForm());
        }
        public static bool TryAuthenticate()
        {
            bool isLogin = false;
            CheckForm cf = new CheckForm();
            if (cf.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            string domain = "uibe.edu";
            try
            {
                DirectoryEntry entry = new DirectoryEntry(string.Format("LDAP://uibe.edu/OU=Users,OU=SEA团队,OU=2014外语自学中心用户,OU=过度,DC=uibe,DC=edu", domain), cf.username, cf.userpwd);
                entry.RefreshCache();
                isLogin = true;
            }
            catch (Exception)
            {
                isLogin = false;
            }
            return isLogin;
        }
    }
}
