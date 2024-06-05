using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Client.调价
{
    public partial class 售价单件添加 : Form
    {
        DataTable panDT;

        public 售价单件添加(DataTable dt)
        {
            panDT = dt;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                textBox1.Focus();
                textBox1.BackColor = Color.MistyRose;
                return;
            }

            string TM = textBox1.Text.Trim();
            toolStripStatusLabel1.Text="";
            DataRow[] itemrow = panDT.Select("TM='" + TM + "'", "");
            if (itemrow.Length > 0)
            {
                toolStripStatusLabel1.Text = "这个条码已存在！";
                textBox1.Focus();
                textBox1.BackColor = Color.MistyRose;
                textBox1.SelectAll();
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,RTRIM(TM) AS TM,XSOU AS MONY,BLU,CBEI,IMGBOL,BZHU FROM QGOODS WHERE (TM='" + TM + "')", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    DataRow i = panDT.NewRow();
                    i["TM"] = rs["TM"].ToString();
                    i["MONY"] = rs["MONY"].ToString();
                    i["BLU"] = rs["BLU"].ToString();
                    i["CBEI"] = rs["CBEI"].ToString();
                    i["SLIAO"] = rs["SLIAO"].ToString();
                    i["JLIAO"] = rs["JLIAO"].ToString();
                    i["SSI"] = rs["SSI"].ToString();
                    i["IMGBOL"] = rs["IMGBOL"].ToString();
                    i["BZHU"] = rs["BZHU"].ToString();
                    panDT.Rows.Add(i);
                }
                else
                {
                    toolStripStatusLabel1.Text = "没有找到这件货品的资料！";
                }

                rs.Close();
                conn.Close();
                textBox1.Focus();
                textBox1.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void 售价单件添加_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            toolStripStatusLabel1.Text="";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                button1_Click(button1, new EventArgs());
            }
        }
    }
}