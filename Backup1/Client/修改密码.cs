using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Client
{
    public partial class 修改密码 : Form
    {
        ToolStripMenuItem panMENU;
        public 修改密码(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        private void 修改密码_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            toolmsgstr.Text = "";

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

            if (textBox1.Text != xconfig.PWD)
            {
                toolmsgstr.Text = "原始密码错误！";
                textBox1.Text = "";
                textBox1.Focus();
                return;
            }

            try
            {
                string msgstr = xconfig.USER + "|修改密码|SETPWD|" + xconfig.USER + "," + xconfig.FunMD5(textBox2.Text.Trim()) + "<EOF>";
                mesk.xSocket mesk = new mesk.xSocket();
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                bool msgbol;
                string msgbolstr;
                msgbolstr = mesk.Send(msgstr, ip, port);
                if (msgbolstr == "ERROR")
                {
                    msgbol = false;
                }
                else
                {
                    msgbol = bool.Parse(msgbolstr);
                }
                if (!msgbol)
                {
                    toolmsgstr.Text = "密码修改失败！";
                }
                else
                {
                    toolmsgstr.Text = "密码修改成功！";
                    button1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.MistyRose;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }

        private void 修改密码_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
            string msgstr = xconfig.USER + "|闲置|CONNECT<EOF>";
            xconfig.netSend(msgstr);
        }

        private void 修改密码_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}