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
using System.Configuration;
using CheckInProgram;

namespace CheckInProgram
{
    public partial class Normal : Form
    {
        public Normal(string id,string name,string state,string room)
        {
            InitializeComponent();
            IDLabel.Text = id;
            nameLabel.Text = name;
            stateLabel.Text = state;
            roomLabel.Text = room;
            keepTimer.Interval = int.Parse(Program.KP) * 60 * 1000;
            checkTimer.Interval = int.Parse(Program.Overtime) * 60 * 1000;
        }

        private void moreInfoLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Program.LinkUrl);
        }

        private void Normal_Load(object sender, EventArgs e)
        {
            poweredLabel.Text = Program.Powered;
            moreInfoLinkLabel.Text = Program.LinkLabel;
            if (int.Parse(Program.ShowPage) == 1 && MessageBox.Show(Program.PageLabel, "重要通知", MessageBoxButtons.OK, MessageBoxIcon.Information)==DialogResult.OK)
            {
                System.Diagnostics.Process.Start(Program.PageUrl);
            }
        }

        private void Normal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void keepTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Random ran = new Random();
                int i = ran.Next(0, 100);
                if (i >= int.Parse(Program.UCP))
                {
                    checkTimer.Enabled = true;
                    checkIfThere();
                    return;
                }
                SqlCommand cmd = new SqlCommand("dbo.CheckInKeep",Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NChar, 9));
                cmd.Parameters["@id"].Value = Program.sID;
                Program.conn.Open();
                cmd.ExecuteScalar();
            }
            catch(Exception ex)
            {
                Program.Error(ex.Message);
            }
            finally
            {
                Program.conn.Close();
            }
        }

        private void checkTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                keepTimer.Enabled = false;
                signOutButton.Enabled = false;
                changeButton.Enabled = false;
                checkIfThereButton.Visible = false;
                this.BackColor = Color.Red;
                stateLabel.Text = "确认操作超时";
                Program.sState = "旷课";
                SqlCommand cmd = new SqlCommand("",Program.conn);
                cmd.CommandText = "UPDATE dbo.[CheckIn.StudentDetails] SET [State]='旷课', [Note]='规定时间内未选择确认对话框' WHERE lesson=" + Program.sLesson + " AND id=" + Program.sID + " AND [State]='在线'";
                Program.conn.Open();
                cmd.ExecuteScalar();
            }
            catch(Exception ex)
            {
                Program.Error(ex.Message);
            }
            finally
            {
                Program.conn.Close();
            }
        }

        private void signOutButton_Click(object sender, EventArgs e)
        {
            try
            {
                coldDownTimer.Enabled = true;
                signOutButton.Enabled = false;
                signOutButton.Text = "两分钟后可以点击";
                SqlCommand cmd = new SqlCommand("dbo.CheckInCheckOut", Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NChar, 9));
                cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@reason", SqlDbType.NVarChar, 50));
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters["@reason"].Direction = ParameterDirection.Output;
                cmd.Parameters["@id"].Value = Program.sID;
                Program.conn.Open();
                cmd.ExecuteScalar();
                if(cmd.Parameters["@result"].Value == DBNull.Value)
                {
                    Program.Error("注销失败：无法找到你的记录，请立即联系值班员！\n\r错误代码502");
                    Program.conn.Close();
                    return;
                }
                if(cmd.Parameters["@reason"].Value != DBNull.Value)
                {
                    Program.Error("注销失败，因为" + cmd.Parameters["@reason"].Value + "，目前你的状态是" + cmd.Parameters["@result"].Value + "\n\r错误代码503");
                    Program.conn.Close();
                    if (ifExit(cmd.Parameters["@reason"].Value.ToString().Substring(0,4)))
                    {
                        Application.Exit();
                    }
                    return;
                }
                string show = "注销成功，本堂课你的状态是" + cmd.Parameters["@result"].Value;
                MessageBox.Show(show, "注销成功",MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.conn.Close();
                Application.Exit();
            }
            catch(Exception ex)
            {
                Program.Error(ex.Message);
            }
            finally
            {
                Program.conn.Close();
            }
        }

        private void leaveButton_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (MessageBox.Show("提前下线会导致本次上课记录被标注为早退，确定要提前下线么？","确认",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("",Program.conn);
                    cmd.CommandText = "UPDATE dbo.[CheckIn.StudentDetails] SET [State]='旷课', [Note]='早退' WHERE lesson=" + Program.sLesson + " AND id=" + Program.sID + " AND [State]='在线'";
                    Program.conn.Open();
                    cmd.ExecuteScalar();
                    Program.conn.Close();
                    Application.Exit();
                }
            }
            catch(Exception ex)
            {
                Program.Error(ex.Message);
            }
            finally
            {
                Program.conn.Close();
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("更换机器将导致本机下线，且规定时间内若没有在新的机器上线，你将被视为旷课，确定要更换机器么？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("dbo.CheckInChange", Program.conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NChar, 9));
                    cmd.Parameters["@id"].Value = Program.sID;
                    Program.conn.Open();
                    cmd.ExecuteScalar();
                    Program.conn.Close();
                    Application.Exit();
                }
            }
            catch(Exception ex)
            {
                Program.Error(ex.Message);
            }
            finally
            {
                Program.conn.Close();
            }
        }

        private void Normal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Alt )
            {
                e.Handled = true;
                return;
            }
        }
        private static bool ifExit(string reason)
        {
            if (reason == "现在不是")
                return false;
            return true;
        }
        private void checkIfThere()
        {
            checkIfThereButton.Visible = true;
            this.BackColor = Color.LightBlue;
            this.Location = new Point(556, 268);
        }

        private void checkIfThereButton_Click(object sender, EventArgs e)
        {
            checkIfThereButton.Visible = false;
            this.BackColor = Color.FromArgb(255, 255, 192);
            this.Location = new Point(0, 0);
            checkTimer.Enabled = false;
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.CheckInKeep", Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NChar, 9));
                cmd.Parameters["@id"].Value = Program.sID;
                Program.conn.Open();
                cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Program.Error(ex.Message);
            }
            finally
            {
                Program.conn.Close();
            }
        }

        private void coldDownTimer_Tick(object sender, EventArgs e)
        {
            coldDownTimer.Enabled = false;
            signOutButton.Enabled = true;
            signOutButton.Text = "注销";
        }
    }
}
