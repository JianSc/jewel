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
    public partial class 款式销售分析详细 : Form
    {
        DateTime a, b;
        DataTable thedt = new clidata().Tables["款式销售分析详细"];
        string pankus;

        public 款式销售分析详细(DateTime ab,DateTime bb,string k)
        {
            a = ab;
            b = bb;
            pankus = k;
            InitializeComponent();
        }

        private void 款式销售分析详细_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            this.dataGridView1.DataSource = thedt;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(QGOODS_SALES.TM) AS TM,RTRIM(QGOODS.KUS) AS KUS,"
                    + "CONVERT(CHAR(10),QGOODS_SALES.SETTIME,120) AS SETTIME,QGOODS_SALES.CBEI,SSALE,ZKOU,QGOODS_SALES.SLIANG,RTRIM(KHU) AS KHU,"
                    + "RTRIM(MDIAN) AS MDIAN,RTRIM([USER]) AS [USER] FROM QGOODS_SALES INNER JOIN QGOODS ON "
                    + "QGOODS.TM=QGOODS_SALES.TM WHERE (QGOODS_SALES.SETTIME BETWEEN '" + a + "' AND '" + b + "') AND (QGOODS.KUS = '" + pankus + "')", conn);
                da.Fill(thedt);
                conn.Close();

                int sling;
                double cbei, ssale;
                sling = int.Parse(thedt.Compute("sum(sliang)", "").ToString());
                cbei = double.Parse(thedt.Compute("sum(cbei)", "").ToString());
                ssale = double.Parse(thedt.Compute("sum(ssale)", "").ToString());

                DataRow r = thedt.NewRow();
                r["TM"] = "合计:";
                r["SLIANG"] = sling;
                r["CBEI"] = cbei;
                r["SSALE"] = ssale;
                thedt.Rows.Add(r);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                if (i.Cells["tMDataGridViewTextBoxColumn"].Value.ToString() == "合计:")
                {
                    i.DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}