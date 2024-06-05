using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mdian
{
    public partial class 修改密码 : Form
    {
        DataTable panDT;
        public 修改密码(DataTable ygongdt)
        {
            panDT = ygongdt;
            InitializeComponent();
        }

        private void 修改密码_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            string msgstr = xconfig.USER + "|修改密码|CONNECT<EOF>";
            xconfig.netSend(msgstr);

            toolstr.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == "")
            {
                textBox2.Focus();
                return;
            }
            if (textBox3.Text == "")
            {
                textBox3.Focus();
                return;
            }
            if (textBox2.Text != textBox3.Text)
            {
                textBox2.Text = "";
                textBox3.Text = "";
                textBox2.Focus();
                return;
            }

            string pwd = xconfig.FunMD5(textBox1.Text);
            if (pwd != xconfig.PWD)
            {
                toolstr.Text = "密码不正确！";
                return;
            }

            pwd = xconfig.FunMD5(textBox2.Text);

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE QYGONG SET PWD='" + pwd + "' WHERE ID='" + xconfig.ID + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            xconfig.PWD = pwd;
            
            MessageBox.Show("密码修改成功！");
            this.Close();
        }

        private void 修改密码_FormClosed(object sender, FormClosedEventArgs e)
        {
            string msgstr = xconfig.USER + "|闲置|CONNECT<EOF>";
            xconfig.netSend(msgstr);
        }

    }
}