using System;
using System.Drawing;
using System.Windows.Forms;

namespace CheckInProgram
{
    public partial class Normal : Form
    {
        Student Student;
        public Normal(Student student)
        {
            InitializeComponent();
            IDLabel.Text = student.ID.ToString();
            NameLabel.Text = student.Name;
            StateLabel.Text = Print.state(student.State);
            RoomLabel.Text = student.Room;
            KeepTimer.Interval = int.Parse(Program.KP) * 60 * 1000;
            CheckTimer.Interval = int.Parse(Program.Overtime) * 60 * 1000;
            Student = student;
        }

        private void moreInfoLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Program.LinkUrl);
        }

        private void Normal_Load(object sender, EventArgs e)
        {
            PoweredLabel.Text = Program.Powered;
            MoreInfoLinkLabel.Text = Program.LinkLabel;
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
                    CheckTimer.Enabled = true;
                    CheckIfThere();
                    return;
                }
                Student.DatabaseTransport("dbo.SP_CheckIn_Keep");
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
                KeepTimer.Enabled = false;
                SignOutButton.Enabled = false;
                ChangeButton.Enabled = false;
                CheckIfThereButton.Visible = false;
                this.BackColor = Color.Red;
                StateLabel.Text = "确认操作超时";
                Student.DatabaseTransport("dbo.SP_CheckIn_NotHere");
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
                ColdDownTimer.Enabled = true;
                SignOutButton.Enabled = false;
                SignOutButton.Text = "两分钟后可以点击";
                if (Student.DatabaseTransport("dbo.SP_CheckIn_DoCheckOut"))
                {
                    KeepTimer.Enabled = false;
                    MessageBox.Show("注销成功，你已经完成了本堂课的学习！", "注销成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
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
                    if (Student.DatabaseTransport("dbo.SP_CheckIn_Change"))
                    {
                        Print.infomsg("换机成功，请在规定时间内在新的机器上线。", "换机成功");
                        Application.Exit();
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

        private void CheckIfThere()
        {
            CheckIfThereButton.Visible = true;
            this.BackColor = Color.LightBlue;
            this.Location = new Point(556, 268);
        }

        private void checkIfThereButton_Click(object sender, EventArgs e)
        {
            CheckIfThereButton.Visible = false;
            this.BackColor = Color.FromArgb(255, 255, 192);
            this.Location = new Point(0, 0);
            CheckTimer.Enabled = false;
            try
            {
                Student.DatabaseTransport("dbo.SP_CheckIn_Keep");
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
            ColdDownTimer.Enabled = false;
            SignOutButton.Enabled = true;
            SignOutButton.Text = "注销";
        }

        private void queryButton_Click(object sender, EventArgs e)
        {
            Student.Query();
        }
    }
}
