using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace Client.报表
{
    public partial class 款式销售分析查询 : Form
    {
        DataTable thedt = new clidata().Tables["款式销售分析"];
        ToolStripMenuItem panMENU;
        Form panFORM;

        public 款式销售分析查询(ToolStripMenuItem m,Form f)
        {
            panMENU = m;
            panFORM = f;
            InitializeComponent();
        }

        private void 款式销售分析查询_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            comboBox1.Items.Add("");
            comboBox2.Items.Add("");

            dateTimePicker1.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            dateTimePicker2.Text = DateTime.Now.ToShortDateString();

            int year = DateTime.Now.Year;
            for (int i = 0; i < 13; i++)
            {
                int j = year - i;
                comboBox1.Items.Add(j.ToString());
            }
            for (int i = 1; i < 13; i++)
            {
                comboBox2.Items.Add(i.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constr = string.Empty;
            DateTime a = DateTime.Now;
            DateTime b = DateTime.Now;

            if (tabControl1.SelectedIndex == 0)
            {
                if (comboBox1.Text == string.Empty)
                {
                    comboBox1.DroppedDown = true;
                    return;
                }
                else if (comboBox2.Text == string.Empty)
                {
                    comboBox2.DroppedDown = true;
                }

                a = xconfig.DateTime_Max_Min(comboBox1.Text, comboBox2.Text, false);
                b = xconfig.DateTime_Max_Min(comboBox1.Text, comboBox2.Text, true);

                constr = "SELECT RTRIM(QGOODS.KUS) AS KUS,SUM(QGOODS_SALES.SLIANG) AS SLIANG,SUM(QGOODS_SALES.CBEI) AS CBEI,"
                + "SUM(QGOODS_SALES.SSALE) AS SSALE FROM QGOODS_SALES INNER JOIN QGOODS ON QGOODS.TM=QGOODS_SALES.TM "
                + "WHERE (QGOODS_SALES.SETTIME BETWEEN '" + a + "' AND '" + b + "') GROUP BY KUS";
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                a = DateTime.Parse(dateTimePicker1.Text + " 00:00:00");
                b = DateTime.Parse(dateTimePicker2.Text + " 23:59:59");

                constr = "SELECT RTRIM(QGOODS.KUS) AS KUS,SUM(QGOODS_SALES.SLIANG) AS SLIANG,SUM(QGOODS_SALES.CBEI) AS CBEI,"
                + "SUM(QGOODS_SALES.SSALE) AS SSALE FROM QGOODS_SALES INNER JOIN QGOODS ON QGOODS.TM=QGOODS_SALES.TM "
                + "WHERE (QGOODS_SALES.SETTIME BETWEEN '" + a + "' AND '" + b + "') GROUP BY KUS";
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(constr, conn);
                thedt.Rows.Clear();
                da.Fill(thedt);
                conn.Close();

                int sling;
                decimal cbei, xsou;
                if (thedt.Rows.Count < 1)
                {
                    sling = 0;
                    cbei = 0;
                    xsou = 0;
                }
                else
                {
                    sling = int.Parse(thedt.Compute("SUM(SLIANG)", "").ToString());
                    cbei = decimal.Parse(thedt.Compute("SUM(CBEI)", "").ToString());
                    xsou = decimal.Parse(thedt.Compute("SUM(SSALE)", "").ToString());
                }

                DataRow r = thedt.NewRow();
                r["KUS"] = "总计:";
                r["SLIANG"] = sling;
                r["CBEI"] = cbei;
                r["SSALE"] = xsou;
                thedt.Rows.Add(r);

                Form newform = new 报表.款式销售分析(thedt, a, b,this);
                newform.MdiParent = panFORM;
                this.TopMost = false;
                newform.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 款式销售分析查询_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }
    }
}