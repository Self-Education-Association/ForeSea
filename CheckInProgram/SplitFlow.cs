using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckInProgram
{
    public partial class SplitFlow : Form
    {
        public SplitFlow()
        {
            InitializeComponent();
        }

        private void SplitFlow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Alt)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Escape && e.Alt)
            {
                e.Handled = true;
                return;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (IDTextBox.PasswordChar=='*')
                {
                    if (Program.TryAuthenticate(IDTextBox.Text, NameTextBox.Text))
                        Application.Exit();
                    else
                        throw new Exception("管理员登陆失败！");
                }
                if ((IDTextBox.Text.Count() != 9) || !IsNumeric(IDTextBox.Text)) throw new Exception("请输入正确的学号。");
                int result = Student.SplitIsOK(int.Parse(IDTextBox.Text), NameTextBox.Text);
                if (result == -99)
                    throw new Exception("请检查学号和姓名是否输入错误。");
                if (result == 99)
                    throw new Exception("分流系统运行中，请自习的同学于上课15分钟后再尝试登陆！");
                if (result>0)
                {
                    Normal normal = new Normal(new Student(int.Parse(IDTextBox.Text), true));
                    normal.Show();
                    Hide();
                }
                if (--result<0)
                {
                    Print.infomsg("分流系统登陆成功，稍后将尝试为你自动签到，你可以开始你的学习了！\n若到上课时间仍没有自动签到，请使用桌面上的签到程序手动签到。", "登陆成功");
                    Hide();
                    CheckInTimer.Interval = -result * 60 * 1000;
                    CheckInTimer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                IDTextBox.Text = "";
                NameTextBox.Text = "";
                IDTextBox.PasswordChar = new char();
                NameTextBox.PasswordChar = new char();
                IDTextBox.Focus();
                Print.show(ex.Message);
            }
            finally
            {
                Program.conn.Close();
            }
        }
        private static bool IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg
                = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");
            return reg.IsMatch(str);
        }

        private void CheckInTimer_Tick(object sender, EventArgs e)
        {
            CheckInTimer.Enabled = false;
            Normal normal = new Normal(new Student(int.Parse(IDTextBox.Text), false));
            normal.Show();
            Hide();
        }

        private void IDTextBox_TextChanged(object sender, EventArgs e)
        {
            if (IDTextBox.Text.Length >= 2)
                if (IDTextBox.Text.Substring(0, 2) == "st")
                {
                    IDTextBox.PasswordChar = '*';
                    NameTextBox.PasswordChar = '*';
                }
        }
    }
}
