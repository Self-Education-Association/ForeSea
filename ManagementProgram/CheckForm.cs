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
    public partial class CheckForm : Form
    {
        public CheckForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            username = nameTextBox.Text;
            userpwd = pwdTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
        public string username;
        public string userpwd;
    }
}
