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
    public partial class 成本单件添加 : Form
    {
        DataTable panDT;

        public 成本单件添加(DataTable dt)
        {
            panDT = dt;
            InitializeComponent();
        }

        private void 成本单件添加_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            toolStripStatusLabel1.Text="";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                textBox1.Focus();
                return;
            }

            toolStripStatusLabel1.Text="";
            textBox1.BackColor = Color.White;

            DataRow[] dr = panDT.Select("TM='" + textBox1.Text.Trim() + "'", "");
            if (dr.Length > 0)
            {
                toolStripStatusLabel1.Text = "这个条码已存在！";
                textBox1.Text = "";
                textBox1.Focus();
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(TM) AS TM,JJA,PJJA,ZSJA,ZSJE,FSJA,FSJE,JGDJ,OTHER,JGSH,ZSS,ZSZ,FSS,FSZ,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,CBEI,JINZ FROM QGOODS WHERE (TM='" + textBox1.Text.Trim() + "')", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    DataRow i = panDT.NewRow();
                    i["TM"] = rs["TM"].ToString();
                    i["JJA"] = rs["JJA"].ToString();
                    i["PJJA"] = rs["PJJA"].ToString();
                    i["ZSJA"] = rs["ZSJA"].ToString();
                    i["ZSJE"] = rs["ZSJE"].ToString();
                    i["FSJA"] = rs["FSJA"].ToString();
                    i["FSJE"] = rs["FSJE"].ToString();
                    i["JGDJ"] = rs["JGDJ"].ToString();
                    i["OTHER"] = rs["OTHER"].ToString();
                    i["JGSH"] = rs["JGSH"].ToString();
                    i["ZSS"] = rs["ZSS"].ToString();
                    i["ZSZ"] = rs["ZSZ"].ToString();
                    i["FSZ"] = rs["FSZ"].ToString();
                    i["FSS"] = rs["FSS"].ToString();
                    i["JLIAO"] = rs["JLIAO"].ToString();
                    i["SLIAO"] = rs["SLIAO"].ToString();
                    i["SSI"] = rs["SSI"].ToString();
                    i["JINZ"] = rs["JINZ"].ToString();
                    i["CBEI"] = rs["CBEI"].ToString();
                    panDT.Rows.Add(i);
                }
                else
                {
                    toolStripStatusLabel1.Text = "没有找到这个条码的数据！";
                    textBox1.BackColor = Color.MistyRose;
                }
                rs.Close();
                conn.Close();

                textBox1.Text = "";
                textBox1.BackColor = Color.White;
                textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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