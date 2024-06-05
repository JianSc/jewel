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
    public partial class 客户积分详细 : Form
    {
        Form panFR;
        string panDH = string.Empty;
        DataTable panDT = new clidata().Tables["客户积分"];

        public 客户积分详细(Form f,string DH)
        {
            panFR = f;
            panDH = DH;
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 客户积分详细_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.TopMost = true;

            //17,18

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT JF,CONVERT(CHAR(10),SETTIME,120) AS SRI FROM QGOODS_CUSTJF_LOG WHERE (KH='" + panDH + "')", conn);
                da.Fill(panDT);
                conn.Close();

                this.dataGridView1.DataSource = panDT;

                if (panDT.Rows.Count > 17)
                {
                    pictureBox1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void 客户积分详细_FormClosed(object sender, FormClosedEventArgs e)
        {
            panFR.TopMost = true;
        }
    }
}