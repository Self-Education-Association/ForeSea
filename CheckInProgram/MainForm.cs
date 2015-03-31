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
                    return "认证失败：请输入正确的学号！\n\r错误代码501";
                }
                SqlCommand cmd = new SqlCommand("dbo.CheckInCheckIsOK", Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NChar, 9));
                cmd.Parameters.Add(new SqlParameter("@ip", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@lesson", SqlDbType.SmallInt));
                cmd.Parameters.Add(new SqlParameter("@room", SqlDbType.NVarChar, 50));
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters["@lesson"].Direction = ParameterDirection.Output;
                cmd.Parameters["@room"].Direction = ParameterDirection.Output;
                cmd.Parameters["@id"].Value = enterIDBox.Text;
                cmd.Parameters["@ip"].Value = Program.GetLocalIp();
                Program.conn.Open();
                cmd.ExecuteScalar();
                Program.sName = (string)cmd.Parameters["@result"].Value;
                Program.sID = (string)cmd.Parameters["@id"].Value;
                if (cmd.Parameters["@room"].Value != DBNull.Value)
                    Program.sRoom = (string)cmd.Parameters["@room"].Value;
                if (cmd.Parameters["@lesson"].Value != DBNull.Value)
                    Program.sLesson = (short)cmd.Parameters["@lesson"].Value;
                return cmd.Parameters["@result"].Value.ToString();
            }
            catch(Exception ex)
            {
                return "认证失败：" + ex.Message + "\n\r错误代码60X";
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
                if (ifCheckIsOK.Length >= 15)
                {
                    Program.Error(ifCheckIsOK);
                    return;
                }
                if (MessageBox.Show("你的姓名是："+ifCheckIsOK+"\n你确认要签到么？","确认签到",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)
                {
                    return;
                }
                SqlCommand cmd = new SqlCommand("dbo.CheckInDoCheckIn", Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@ip", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.VarChar, 50));
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters["@id"].Value = enterIDBox.Text;
                cmd.Parameters["@ip"].Value = Program.GetLocalIp();
                Program.conn.Open();
                cmd.ExecuteScalar();
                if ((string)cmd.Parameters["@result"].Value == "正常" || (string)cmd.Parameters["@result"].Value == "迟到" || (string)cmd.Parameters["@result"].Value == "换机成功")
                {
                    Program.sState = (string)cmd.Parameters["@result"].Value;
                    MessageBox.Show("你已经签到！你的状态是：" + Program.sState + "，可以开始你的学习了！", "签到成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form normal = new Normal(Program.sID, Program.sName, Program.sState, Program.sRoom);
                    normal.Show();
                    this.Hide();
                    return;
                }
                Program.Error((string)cmd.Parameters["@result"].Value);
                return;
            }
            catch(Exception ex)
            {
                Program.Error(ex.Message);
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
                SqlCommand cmd = new SqlCommand("dbo.CheckInRun", Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ip", SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar, 9));
                cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@state", SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@room", SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@lesson", SqlDbType.NVarChar, 50));
                cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                cmd.Parameters["@name"].Direction = ParameterDirection.Output;
                cmd.Parameters["@state"].Direction = ParameterDirection.Output;
                cmd.Parameters["@room"].Direction = ParameterDirection.Output;
                cmd.Parameters["@lesson"].Direction = ParameterDirection.Output;
                cmd.Parameters["@ip"].Value = Program.GetLocalIp();
                Program.conn.Open();
                cmd.ExecuteScalar();
                if (cmd.Parameters["@id"].Value.ToString() != "0")
                {
                    Program.sID = (string)cmd.Parameters["@id"].Value;
                    Program.sName = (string)cmd.Parameters["@name"].Value;
                    Program.sState = (string)cmd.Parameters["@state"].Value;
                    Program.sRoom = (string)cmd.Parameters["@room"].Value;
                    Form normal = new Normal(Program.sID, Program.sName, Program.sState, Program.sRoom);
                    this.Hide();
                    normal.Show();
                }
                if (cmd.Parameters["@room"].Value == DBNull.Value)
                {
                    Program.Error("启动失败:本机不是签到机器！\n\r错误代码101");
                    Application.Exit();
                }
                    enterIDBox.Focus();
            }
            catch (Exception ex)
            {
                Program.Error("启动失败：" + ex.Message + "\n\r错误代码60X");
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
