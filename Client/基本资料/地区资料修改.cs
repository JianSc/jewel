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
    public partial class 地区资料修改 : Form
    {
        DataTable panDT;
        TreeView panTV;
        string panID;

        public 地区资料修改(DataTable dat,TreeView trv,string ID)
        {
            InitializeComponent();
            panID = ID;
            panDT = dat;
            panTV = trv;
        }

        private void 地区资料修改_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.comboBox1.Items.Clear();

            DataRow[] thedr = panDT.Select("[lv]=1", "[name]");
            for (int i = 0; i < thedr.Length; i++)
            {
                this.comboBox1.Items.Add(thedr[i]["name"].ToString());
            }

            DataRow[] itemdr1 = panDT.Select("[name]='" + panID + "'", "");
            if (itemdr1.Length < 1)
            {
                this.Close();
                return;
            }

            textBox1.Text = panID;

            string panid = itemdr1[0]["sid"].ToString();
            if (panid != "" && panid!="0")
            {
                DataRow[] itemdr2 = panDT.Select("[id]='" + panid + "'", "");
                if (itemdr2.Length > 0)
                {
                    this.comboBox1.Text = itemdr2[0]["name"].ToString();
                }
            }
            else if (panid == "0")
            {
                comboBox1.Enabled = false;
            }
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
            try
            {
                string thesid="0";
                if (comboBox1.Text != "")
                {
                    DataRow[] thedr1 = panDT.Select("[name]='" + comboBox1.Text + "'", "");
                    if (thedr1.Length > 0)
                    {
                        thesid = thedr1[0]["id"].ToString();
                    }
                }
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("update [qDQU] set [name]='" + textBox1.Text.Trim() + "',[sid]='" + thesid + "' where [name]='" + panID + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                DataRow[] itemdr3 = panDT.Select("[name]='" + panID + "'", "");
                if (itemdr3.Length > 0)
                {
                    itemdr3[0]["name"] = textBox1.Text.Trim();
                    itemdr3[0]["sid"] = thesid;
                }
            }
            catch (Exception ex)
            {
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
            this.Close();
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