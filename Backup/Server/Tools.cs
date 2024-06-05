using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.OleDb;

namespace Server
{
    public partial class Tools : Form
    {
        public Tools()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Tools_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            try
            {
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("data", "cnsdjian"));
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT DBIP,DBUSER,DBPASSWORD,YDBIP,YDBUSER,YDBPASSWORD,JHSERVERPWD,JHSERVERIP,JHSERVERUSER,CBB,MBB,YDBIP2 FROM DBCONFIG", conn);
                OleDbDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    textBox1.Text = rs["DBIP"].ToString();
                    textBox2.Text = rs["DBUSER"].ToString();
                    textBox3.Text = rs["DBPASSWORD"].ToString();
                    textBox9.Text = rs["YDBIP"].ToString();
                    textBox8.Text = rs["YDBUSER"].ToString();
                    textBox7.Text = rs["YDBPASSWORD"].ToString();
                    textBox12.Text = rs["JHSERVERIP"].ToString();
                    textBox11.Text = rs["JHSERVERUSER"].ToString();
                    textBox10.Text = rs["JHSERVERPWD"].ToString();
                    textBox13.Text = rs["CBB"].ToString();
                    textBox14.Text = rs["MBB"].ToString();
                    textBox15.Text = rs["YDBIP2"].ToString();
                } 
                rs.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else if (key == 8)
            {
                e.Handled = false;
            }
            else
            {
                Regex regex = new Regex(@"\d|\.");
                if (!regex.Match(e.KeyChar.ToString()).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            string str = ((TextBox)sender).Text;
            Regex regex = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty)
            {
                tabControl1.SelectedIndex = 0;
                textBox1.Focus();
                return;
            }

            if (textBox9.Text == string.Empty || textBox8.Text == string.Empty || textBox7.Text == string.Empty || textBox15.Text == string.Empty)
            {
                tabControl1.SelectedIndex = 1;
                textBox9.Focus();
                return;
            }
            if (textBox12.Text == string.Empty || textBox11.Text == string.Empty || textBox10.Text == string.Empty)
            {
                tabControl1.SelectedIndex = 2;
                textBox12.Focus();
                return;
            }
            if (textBox13.Text == string.Empty || textBox14.Text == string.Empty)
            {
                tabControl1.SelectedIndex = 3;
                textBox13.Focus();
                return;
            }

            string DBIP, DBUSER, DBPASSWORD, YDBIP, YDBUSER, YDBPASSWORD, JHSERVERIP, JHSERVERUSER, JHSERVERPWD, CBB, MBB, YDBIP2;
            DBIP = textBox1.Text;
            DBUSER = textBox2.Text;
            DBPASSWORD = textBox3.Text;
            YDBIP = textBox9.Text;
            YDBUSER = textBox8.Text;
            YDBPASSWORD = textBox7.Text;
            JHSERVERIP = textBox12.Text;
            JHSERVERUSER = textBox11.Text;
            JHSERVERPWD = textBox10.Text;
            CBB = textBox13.Text;
            MBB = textBox14.Text;
            YDBIP2 = textBox15.Text;

            try
            {
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("data", "cnsdjian"));
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("UPDATE DBCONFIG SET "
                    + "DBIP='" + DBIP + "',DBUSER='" + DBUSER + "',DBPASSWORD='" + DBPASSWORD + "',YDBIP='" + YDBIP + "',YDBIP2='"+ YDBIP2 + "',YDBUSER='" + YDBUSER + "',"
                    + "YDBPASSWORD='" + YDBPASSWORD + "',JHSERVERIP='" + JHSERVERIP + "',JHSERVERUSER='" + JHSERVERUSER + "',JHSERVERPWD='" + JHSERVERPWD + "',CBB='"+CBB+"',MBB='"+MBB+"'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else if (key == 8)
            {
                e.Handled = false;
            }
            else
            {
                Regex regex = new Regex(@"\d|\.");
                if (!regex.Match(e.KeyChar.ToString()).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox15_Leave(object sender, EventArgs e)
        {
            string str = ((TextBox)sender).Text;
            Regex regex = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = "";
            }
        }
    }
}