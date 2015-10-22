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
        Student student;
        public Normal(Student studentInput)
        {
            InitializeComponent();
            IDLabel.Text = studentInput.id.ToString();
            nameLabel.Text = studentInput.name;
            stateLabel.Text = Print.state(studentInput.state);
            roomLabel.Text = studentInput.room;
            keepTimer.Interval = int.Parse(Program.KP) * 60 * 1000;
            checkTimer.Interval = int.Parse(Program.Overtime) * 60 * 1000;
            student = studentInput;
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
                int result = databaseTransport("dbo.SP_CheckIn_Keep");
                switch(result)
                {
                    case 400:
                    case 402:
                        Print.show(result);
                        break;
                    case 401:
                        break;
                }
            }
            catch(Exception ex)
            {
                Print.show(ex.Message);
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
                Program.student.state = 3;
                int result = databaseTransport("dbo.SP_CheckIn_NotHere");
                switch (result)
                {
                    case 600:
                    case 602:
                        Print.show(result);
                        break;
                    case 601:
                        break;
                }
            }
            catch(Exception ex)
            {
                Print.show(ex.Message);
            }
        }

        private void signOutButton_Click(object sender, EventArgs e)
        {
            try
            {
                coldDownTimer.Enabled = true;
                signOutButton.Enabled = false;
                signOutButton.Text = "两分钟后可以点击";
                int result = databaseTransport("dbo.SP_CheckIn_DoCheckOut");
                switch (result)
                {
                    case 700:
                    case 702:
                    case 703:
                        Print.show(result);
                        break;
                    case 701:
                        keepTimer.Enabled = false;
                        MessageBox.Show("注销成功，你已经完成了本堂课的学习！", "注销成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                        break;
                }
            }
            catch(Exception ex)
            {
                Print.show(ex.Message);
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("更换机器将导致本机下线，且规定时间内若没有在新的机器上线，你将被视为旷课，确定要更换机器么？\n\r注意：不能在同一台机器上使用换机功能上下机！", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int result = databaseTransport("dbo.SP_CheckIn_Change");
                    switch (result)
                    {
                        case 700:
                        case 702:
                            Print.show(result);
                            break;
                        case 701:
                            Print.infomsg("换机成功，请在规定时间内在新的机器上线。", "换机成功");
                            Application.Exit();
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                Print.show(ex.Message);
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
                int result = databaseTransport("dbo.SP_CheckIn_Keep");
                switch (result)
                {
                    case 400:
                    case 402:
                        Print.show(result);
                        break;
                    case 401:
                        break;
                }
            }
            catch (Exception ex)
            {
                Print.show(ex.Message);
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

        private int databaseTransport(string sp)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sp, Program.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@result", SqlDbType.SmallInt));
                cmd.Parameters["@result"].Direction = ParameterDirection.Output;
                cmd.Parameters["@id"].Value = Program.student.id;
                Program.conn.Open();
                cmd.ExecuteNonQuery();
                return int.Parse(cmd.Parameters["@result"].Value.ToString());
            }
            catch (Exception ex)
            {
                Print.show(ex.Message);
                return 0;
            }
            finally
            {
                Program.conn.Close();
            }
        }

        private void queryButton_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Program.conn;
            cmd.CommandText = "SELECT Normal,Late,Truency FROM View_List_CheckIn WHERE ID=" + student.id;
            SqlDataReader reader = cmd.ExecuteReader();
            Print.infomsg(string.Format("除本次上课记录外，你还有{0}次正常记录,{1}迟到记录,{2}旷课记录，如有问题请联系值班员查询详细记录。", reader[0], reader[1], reader[2]), "查询结果");
        }
    }
}
