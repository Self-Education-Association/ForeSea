﻿/*
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
using System.Linq;
using System.Windows.Forms;

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
            try
            {
                if ((enterIDBox.Text.Count() != 9) || !IsNumeric(enterIDBox.Text)) throw new Exception("请输入正确的学号。");
                string name = Student.CheckIsOK(int.Parse(enterIDBox.Text));
                if (name == null) return;
                if (MessageBox.Show("你的姓名是：" + name + "\n你确认要签到么？", "确认签到", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                Normal normal = new Normal(new Student(int.Parse(enterIDBox.Text), false));
                normal.Show();
                Hide();
            }
            catch (Exception ex)
            {
                Print.Show(ex.Message);
            }
            finally
            {
                enterIDBox.Text = "";
                enterIDBox.Focus();
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
