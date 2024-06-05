using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Client.分析
{
    public partial class 成本调价记录 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable panDT = new clidata().Tables["成本查询"];
        public 成本调价记录(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        private void 成本调价记录_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = panDT;
        }

        private void 成本调价记录_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Focus();
                return;
            }

            string TM = textBox3.Text.Trim();

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmds = new SqlCommand("SELECT RTRIM(TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI FROM QGOODS WHERE (TM='" + TM + "')", conn);
                SqlDataReader rs = cmds.ExecuteReader();
                if (rs.Read())
                {
                    textBox1.Text = rs["TM"].ToString();
                    textBox2.Text = rs["JLIAO"].ToString() + rs["SLIAO"].ToString() + rs["SSI"].ToString();
                }
                else
                {
                    pictureBox1.Visible = true;
                }
                rs.Close();

                SqlDataAdapter cmd = new SqlDataAdapter("SELECT RTRIM(TM) AS TM,CBEI,CONVERT(CHAR(10),SETDATE,120) AS SETDATE,RTRIM([USER]) AS [USER],RTRIM(STAT) AS STAT,RTRIM(ZSHI) AS ZSHI,RTRIM(FSHI) AS FSHI,JJA,PJJA,ZSJA,ZSJE,FSJA,FSJE,JGDJ,OTHER,JGSH FROM QGOODS_CBEI_LOG WHERE (TM='" + TM + "')", conn);
                panDT.Clear();
                cmd.Fill(panDT);
                conn.Close();

                textBox3.SelectAll();

                if (panDT.Rows.Count < 1)
                {
                    pictureBox1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                button1_Click(button1, new EventArgs());
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }
    }
}