﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckInProgram
{
    public partial class QueryForm : Form
    {
        public QueryForm()
        {
            InitializeComponent();
        }

        private void QueryForm_Load(object sender, EventArgs e)
        {
            // TODO:  这行代码将数据加载到表“workOnlineDataSet._CheckIn_StudentDetails”中。您可以根据需要移动或删除它。
            this.checkIn_StudentDetailsTableAdapter.FillByID(this.workOnlineDataSet._CheckIn_StudentDetails,Program.sID);
        }
    }
}
