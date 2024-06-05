using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Client.报表
{
    public partial class 门店销售状况统计 : Form
    {
        ToolStripMenuItem panM;
        Form panF;
        public 门店销售状况统计(ToolStripMenuItem m,Form f)
        {
            panM = m;
            panF = f;
            InitializeComponent();
        }

        DataTable thedt = new clidata().Tables["门店发销退统计"];

        private void 门店销售状况统计_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            dateTimePicker2.Text = DateTime.Now.ToShortDateString();
            dateTimePicker1.Text = DateTime.Now.AddMonths(-1).ToShortDateString();

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(NAME) AS NAME FROM QMDIAN", conn);
                da.Fill(thedt);
                conn.Close();

                this.dataGridView1.DataSource = thedt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 门店销售状况统计_FormClosed(object sender, FormClosedEventArgs e)
        {
            panM.Enabled = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            DateTime timeA = DateTime.Parse(dateTimePicker1.Text + " 00:00:00");
            DateTime timeB = DateTime.Parse(dateTimePicker2.Text + " 23:59:59");

            try
            {
                for (int i = 0; i < thedt.Rows.Count; i++)
                {
                    SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                    conn.Open();
                    string mdianstr = thedt.Rows[i]["NAME"].ToString();

                    SqlCommand cmd = new SqlCommand("SELECT COUNT(SL) AS XSL FROM QGOODS_CKD WHERE (MDIAN='" + mdianstr + "') AND (SETTIME BETWEEN '" + timeA + "' AND '" + timeB + "')", conn);
                    string h = cmd.ExecuteScalar().ToString();
                    thedt.Rows[i]["a1"] = h;
                    cmd = new SqlCommand("SELECT COUNT(SLIANG) AS SL FROM QGOODS_SALES WHERE (MDIAN='" + mdianstr + "') AND (SETTIME BETWEEN '" + timeA + "' AND '" + timeB + "')", conn);
                    h = cmd.ExecuteScalar().ToString();
                    thedt.Rows[i]["a2"] = h;
                    cmd = new SqlCommand("SELECT COUNT(SLIANG) AS SL FROM QGOODS_SALES_BACK WHERE (MDIAN='" + mdianstr + "') AND (SETTIME BETWEEN '" + timeA + "' AND '" + timeB + "')", conn);
                    h = cmd.ExecuteScalar().ToString();
                    thedt.Rows[i]["a3"] = h;
                    cmd = new SqlCommand("SELECT COUNT(SLIANG) AS SL FROM QGOODS_BACK WHERE (MDIAN='" + mdianstr + "') AND (SETTIME BETWEEN '" + timeA + "' AND '" + timeB + "')", conn);
                    h = cmd.ExecuteScalar().ToString();
                    thedt.Rows[i]["a4"] = h;
                    
                    cmd.Dispose();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();
        }
    }
}