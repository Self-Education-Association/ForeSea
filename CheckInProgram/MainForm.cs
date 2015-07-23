using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Diagnostics;

namespace CheckInProgram
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = Program.Name +" "+ Program.Version;
            poweredLabel.Text = Program.Powered;
            moreInfoLinkLabel.Text = Program.LinkLabel;
            string updateString=Program.UpdateString;
            if (Program.ShowUpdate=="1")
            {
                updateString = System.Text.RegularExpressions.Regex.Unescape(updateString);
                MessageBox.Show(updateString, "更新信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void moreInfoLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Program.LinkUrl);
        }

        private void signInButton_Click(object sender, EventArgs e)
        {
            doCheckIn(checkIsOK());
        }
        private string checkIsOK()
        {
            try
            {
                if ( (enterIDBox.Text.Count() != 9) || !IsNumeric(enterIDBox.Text))
                {
                    Print.show("认证失败，请输入正确的学号。");
                    return "";
                }
                SqlCommand cmd = new SqlCommand("dbo.SP_CheckIn_Check", Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ip", SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 20));
                cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.SmallInt));
                cmd.Parameters["@name"].Direction = ParameterDirection.Output;
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters["@id"].Value = enterIDBox.Text;
                cmd.Parameters["@ip"].Value = Program.GetLocalIp();
                Program.conn.Open();
                cmd.ExecuteScalar();
                int result = (int)cmd.Parameters["@result"].Value;
                switch (result)
                {
                    case 200:
                    case 202:
                    case 203:
                    case 204:
                        Print.show(result);
                        break;
                    case 201:
                        return (string)cmd.Parameters["@name"].Value;
                    default:
                        Print.show("未知操作代码。");
                        break;
                }
                return "";
            }
            catch(Exception ex)
            {
                Print.show(ex.Message);
                return "";
            }
            finally
            {
                Program.conn.Close();
            }
        }
        private void doCheckIn(string ifCheckIsOK)
        {
            try
            {
                if (MessageBox.Show("你的姓名是："+ifCheckIsOK+"\n你确认要签到么？","确认签到",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)
                {
                    return;
                }
                SqlCommand cmd = new SqlCommand("dbo.SP_CheckIn_DoCheckIn", Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ip", SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar, 20));
                cmd.Parameters.Add(new SqlParameter("@state", SqlDbType.TinyInt));
                cmd.Parameters.Add(new SqlParameter("@room", SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.SmallInt));
                cmd.Parameters["@name"].Direction = ParameterDirection.Output;
                cmd.Parameters["@state"].Direction = ParameterDirection.Output;
                cmd.Parameters["@room"].Direction = ParameterDirection.Output;
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters["@id"].Value = enterIDBox.Text;
                cmd.Parameters["@ip"].Value = Program.GetLocalIp();
                Program.conn.Open();
                cmd.ExecuteScalar();
                int result = (int)cmd.Parameters["@result"].Value;
                Program.student = new Student((int)cmd.Parameters["@id"].Value, (string)cmd.Parameters["@name"].Value, (int)cmd.Parameters["@state"].Value, (string)cmd.Parameters["@room"].Value);
                switch (result)
                {
                    case 300:
                    case 302:
                    case 303:
                    case 312:
                    case 313:
                        Print.show(result);
                        break;
                    case 301:
                    case 311:
                        Print.infomsg("签到成功，你可以开始你的学习了！", "签到成功");
                        Normal normal = new Normal(Program.student);
                        normal.Show();
                        this.Hide();
                        return;
                }
            }
            catch(Exception ex)
            {
                Print.show(ex.Message);
            }
            finally
            {
                enterIDBox.Text = "";
                Program.conn.Close();
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.SP_CheckIn_Run", Program.conn);
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
                cmd.Parameters["@ip"].Value = Program.GetLocalIp();
                Program.conn.Open();
                cmd.ExecuteScalar();
                int result = (int)cmd.Parameters["@result"].Value;
                Program.student=new Student((int)cmd.Parameters["@id"].Value,(string)cmd.Parameters["@name"].Value,(int)cmd.Parameters["@state"].Value,(string)cmd.Parameters["@room"].Value);
                switch (result)
                {
                    case 100:
                    case 102:
                        Print.show(result);
                        break;
                    case 103:
                        Normal normal = new Normal(Program.student);
                        normal.Show();
                        this.Hide();
                        return;
                }
                enterIDBox.Focus();
            }
            catch (Exception ex)
            {
                Print.show(ex.Message);
                Application.Exit();
            }
            finally
            {
                Program.conn.Close();
            }
        }

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private static bool IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg
                = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");
            return reg.IsMatch(str);
        }
    }
}
