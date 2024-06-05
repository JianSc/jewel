using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Client.基本资料
{
    public partial class 地区资料添加 : Form
    {
        DataTable panDT;
        TreeView panTV;

        public 地区资料添加(DataTable dat,TreeView trv)
        {
            InitializeComponent();
            panDT = dat;
            panTV = trv;
        }

        private void 地区资料添加_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            DataRow[] thedr = panDT.Select("[lv]=1", "[name]");
            for (int i = 0; i < thedr.Length; i++)
            {
                this.comboBox1.Items.Add(thedr[i]["name"].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow[] thedr = panDT.Select("[name]='" + textBox1.Text.Trim() + "'","");

            if(thedr.Length >0)
            {
                MessageBox.Show("资料已存在。","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox1.Focus();
                return;
            }

            try
            {
                int lvs, panid;
                panid = 0;
                if (comboBox1.Text == "")
                {
                    lvs = 1;
                    panid = 0;
                }
                else
                {
                    lvs = 2;
                    DataRow[] idr1 = panDT.Select("[name]='" + comboBox1.Text.Trim() + "'", "");
                    if (idr1.Length > 0)
                    {
                        panid = int.Parse(idr1[0]["id"].ToString());
                    }
                }
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into [qDQU]([name],[lv],[sid])values('" + textBox1.Text + "'," + lvs + "," + panid + ")", conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter dars = new SqlDataAdapter("select rtrim(name) as name,[lv],[id],[sid] from [qDQU]", conn);
                panDT.Clear();
                dars.Fill(panDT);
                conn.Close();
            }catch(Exception ex){
                MessageBox.Show(ex.Message);
            }

            DataRow[] dr1 = panDT.Select("[lv]=1", "[name]");
            panTV.Nodes.Clear();
            for (int i = 0; i < dr1.Length; i++)
            {
                TreeNode newtree = new TreeNode(dr1[i]["name"].ToString());
                panTV.Nodes.Add(newtree);
                DataRow[] dr2 = panDT.Select("[lv]=2 and [sid]='" + dr1[i]["id"].ToString() + "'", "[name]");
                for (int j = 0; j < dr2.Length; j++)
                {
                    newtree.Nodes.Add(dr2[j]["name"].ToString());
                }
            }

            if (!checkBox1.Checked)
            {
                this.Close();
            }

            DataRow[] newdr = panDT.Select("[lv]=1", "[name]");
            comboBox1.Items.Clear();
            for (int i = 0; i < newdr.Length; i++)
            {
                comboBox1.Items.Add(newdr[i]["name"].ToString());
            }

            textBox1.Text = "";
            textBox1.Focus();

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            string str = e.GetType().ToString();
            if (str == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.Gainsboro;
            }
            else if (str == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.Gainsboro;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string str = e.GetType().ToString();
            if (str == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.White;
            }
            else if (str == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.White;
            }
        }
    }
}