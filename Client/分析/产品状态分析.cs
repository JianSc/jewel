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
    public partial class 产品状态分析 : Form
    {

        DataTable theDT = new clidata().Tables["产品状态"];
        DataGridView theDGV;
        ToolStripMenuItem panMENU;

        public 产品状态分析(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        private void 产品状态分析_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            theDGV = dataGridView1;
            theDGV.DataSource = theDT;
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label12.Text = "";
            label14.Text = "";
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //int rows = dataGridView1.Rows.Count;
            //if (rows > 17)
            //{
            //    pictureBox1.Visible = false;
            //}
            //else if (rows < 18)
            //{
            //    pictureBox1.Visible = true;
            //}
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView1_RowsAdded(sender, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            ((ToolStripTextBox)sender).SelectAll();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
            {
                toolStripTextBox1.Focus();
                return;
            }

            String theDH = toolStripTextBox1.Text.Trim();

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(TM) AS TM,RTRIM(STAT) AS STAT,RTRIM([USER]) AS [USER],"
                    + "RTRIM(MDIAN) AS MDIAN,RTRIM(MDIANUSER) AS MDIANUSER,CONVERT(CHAR(10),SETDATE,120) AS XTIME,SJ,SSJ,TJ,DH,CBEI FROM QGOODS_LOG WHERE (TM='" + theDH + "') ORDER BY SETDATE", conn);
                theDT.Clear();
                da.Fill(theDT);
                config.conDGV.DGVAutoID(theDGV, "phao");

                if (theDT.Rows.Count > 0)
                {
                    string kus, jianz, jinz, zshi, fshi, name_s, zshu;
                    SqlCommand cmd = new SqlCommand("SELECT RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,JIANZ,JINZ,ZSZ,ZSS,FSZ,FSS FROM QGOODS WHERE (TM='" + theDH + "')", conn);
                    SqlDataReader rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        kus = rs["KUS"].ToString();
                        jianz = rs["JIANZ"].ToString();
                        jinz = rs["JINZ"].ToString();
                        zshi = rs["ZSZ"].ToString() + "/" + rs["ZSS"].ToString();
                        fshi = rs["FSZ"].ToString() + "/" + rs["FSS"].ToString();
                        name_s = rs["JLIAO"].ToString() + rs["SLIAO"].ToString() + rs["SSI"].ToString();
                        zshu = rs["ZSHU"].ToString();

                        label6.Text = kus;
                        label7.Text = jianz;
                        label8.Text = jinz;
                        label9.Text = zshi;
                        label10.Text = fshi;
                        label12.Text = name_s;
                        label14.Text = zshu;
                    }
                    rs.Close();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 产品状态分析_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
            string msgstr = xconfig.USER + "|闲置|CONNECT<EOF>";
            xconfig.netSend(msgstr);
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((toolStripTextBox1.Text != ""))
            {
                int key = xconfig.ASC(e.KeyChar.ToString());
                if(key == 13)
                {
                    toolStripButton2_Click(toolStripButton2,new EventArgs());
                }
            }
        }

        private void 产品状态分析_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}