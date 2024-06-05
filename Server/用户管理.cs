using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Server
{
    public partial class 用户管理 : Form
    {
        DataTable thisDT = new serData().Tables["User"];
        public 用户管理()
        {
            InitializeComponent();
        }

        private void 用户管理_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            dataGridView1.DataSource = thisDT;

            try
            {
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("data", "cnsdjian"));
                conn.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT ID,NAME,PASSWORD as PWD FROM [USER] WHERE (NAME<>'admin')", conn);
                da.Fill(thisDT);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == string.Empty)
            {
                textBox2.Focus();
                return;
            }

            string xuser = textBox1.Text.Trim();
            DataRow[] dr = thisDT.Select("NAME='" + xuser + "'", "");

            if (dr.Length > 0)
            {
                textBox1.BackColor = Color.MistyRose;
                textBox1.Focus();
                return;
            }

            try
            {
                string name, password;
                name = textBox1.Text.Trim();
                password = xconfig.FunMD5(textBox2.Text.Trim());
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("data", "cnsdjian"));
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("insert into [user]([name],[password])values('"+name+"','"+password+"')", conn);
                cmd.ExecuteNonQuery();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT ID,NAME,PASSWORD AS PWD FROM [USER] WHERE (NAME<>'admin')", conn);
                thisDT.Clear();
                da.Fill(thisDT);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox1.Focus();
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }
            string id = dataGridView1["ID", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            try
            {
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("data", "cnsdjian"));
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("DELETE FROM [USER] WHERE ID=" + id, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                DataRow[] dr = thisDT.Select("ID='" + id + "'", "");
                foreach (DataRow i in dr)
                {
                    thisDT.Rows.Remove(i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                button1_Click(button1, new EventArgs());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }
    }
}