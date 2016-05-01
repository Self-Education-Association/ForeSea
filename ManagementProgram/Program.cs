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
