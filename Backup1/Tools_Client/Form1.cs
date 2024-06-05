using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace Tools_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            try
            {
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("data", "cnsdjian"));
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT SERVERIP,SERVERPORT FROM CONFIG", conn);
                OleDbDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    textBox1.Text = rs["SERVERIP"].ToString();
                    textBox2.Text = rs["SERVERPORT"].ToString();
                }
                rs.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                textBox1.Focus();
                return;
            }

            try
            {
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("data", "cnsdjian"));
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("UPDATE CONFIG SET SERVERIP='" + textBox1.Text + "',SERVERPORT=" + textBox2.Text, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.MistyRose;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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
                Regex regex = new Regex(@"\d");
                if (!regex.Match(e.KeyChar.ToString()).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox1_Leave1(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = "";
            }
        }
    }
}