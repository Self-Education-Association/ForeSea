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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementProgram
{
    public partial class SelectCheckInForm : Form
    {
        public SelectCheckInForm()
        {
            InitializeComponent();
        }

        private void studentDetailsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.studentDetailsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.workOnlineDataSet);

        }

        private void studentDetailsBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            if (Program.TryAuthenticate() == false)
                return;
            this.Validate();
            this.studentDetailsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.workOnlineDataSet);

        }

        private void SelectCheckInForm_Load(object sender, EventArgs e)
        {
            
        }

        private void SelectCheckInForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // TODO:  这行代码将数据加载到表“workOnlineDataSet._CheckIn_StudentDetails”中。您可以根据需要移动或删除它。
            this.checkIn_StudentDetailsTableAdapter.Fill(this.workOnlineDataSet._CheckIn_StudentDetails,idTextBox.Text);
        }
    }
}
