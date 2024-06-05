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
    public partial class 售价调价记妹 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable thisDT = new clidata().Tables["售价调价记录"];

        public 售价调价记妹(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        private void 售价调价记妹_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void 售价调价记妹_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void 售价调价记妹_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == string.Empty)
            {
                textBox3.Focus();
                return;
            }

            try
            {
                string TM = textBox3.Text.Trim();
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI FROM QGOODS WHERE (TM='" + TM + "')", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    textBox1.Text = rs["TM"].ToString();
                    textBox2.Text = rs["JLIAO"].ToString() + rs["SLIAO"].ToString() + rs["SSI"].ToString();
                }
                else
                {
                    pictureBox2.Visible = true;
                }
                rs.Close();
                conn.Close();

                string constr, cmdstr;
                constr = xconfig.CONNSTR;
                cmdstr = "SELECT RTRIM(DH) AS DH,RTRIM(TM) AS TM,MONY,CONVERT(CHAR(10),SETDATE,120) AS SETDATE,RTRIM([USER]) AS [USER],RTRIM(STAT) AS STAT,BLU,CBEI FROM QGOODS_XSED_LOG WHERE (TM='" + TM + "')";
                config.conData.fill("sql", constr, cmdstr, thisDT);

                if (thisDT.Rows.Count > 12)
                {
                    pictureBox1.Visible = false;
                }
                else if (thisDT.Rows.Count < 13)
                {
                    pictureBox1.Visible = true;
                }

                DataView dv = new DataView();
                dv.Table = thisDT;

                dataGridView1.DataSource = dv;

                textBox3.Text = "";
                textBox3.Focus();
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
            pictureBox2.Visible = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}