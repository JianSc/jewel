using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mdian.报表
{
    public partial class 销售统计选择 : Form
    {
        ToolStripMenuItem panMENU;
        Form panFORM;

        public 销售统计选择(ToolStripMenuItem menu,Form panform)
        {
            panFORM=panform;
            panMENU = menu;
            InitializeComponent();
        }

        private void 销售统计选择_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            dateTimePicker1.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            dateTimePicker2.Text= DateTime.Now.ToShortDateString();

            string yearstr = DateTime.Now.Year.ToString();
            string monthsstr = DateTime.Now.Month.ToString();

            int yearint = int.Parse(yearstr);
            int monthint = int.Parse(monthsstr);

            for (int i = 0; i < 5; i++)
            {
                int j = yearint - i;
                comboBox1.Items.Add(j.ToString());
            }
            for (int i = 1; i < 13; i++)
            {
                comboBox2.Items.Add(i.ToString());
            }
        }

        private void 销售统计选择_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (comboBox1.Text == string.Empty)
                {
                    comboBox1.DroppedDown = true;
                    return;
                }
                if (comboBox2.Text == string.Empty)
                {
                    comboBox2.DroppedDown = true;
                    return;
                }
                DateTime a;
                DateTime date1 = DateTime.Parse(comboBox1.Text + "-" + comboBox2.Text + "-01 00:00:00");
                string date2str = comboBox1.Text + "-" + comboBox2.Text + "-31 23:59:59";

                if (!DateTime.TryParse(date2str, out a))
                {
                    date2str = comboBox1.Text + "-" + comboBox2.Text + "-30 23:59:59";
                    if (!DateTime.TryParse(date2str, out a))
                    {
                        date2str = comboBox1.Text + "-" + comboBox2.Text + "-29 23:59:59";
                        if (!DateTime.TryParse(date2str, out a))
                        {
                            date2str = comboBox1.Text + "-" + comboBox2.Text + "-28 23:59:59";
                        }
                    }
                }
                DateTime date2 = DateTime.Parse(date2str);

                DataTable dt = new theDST().Tables["单据分析"];

                try
                {
                    SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT A.SETTIME,A.SBM,SUM(SLIANG) AS SLIANG,CAST(SUM(SSALE) AS DECIMAL(19,2)) AS SALES,KHU,KEHUID,"
                        + "[USER],B.YH AS YHUI,C.ZK FROM (SELECT CONVERT(CHAR(10),SETTIME,120) AS SETTIME,RTRIM(KHU) AS KHU,RTRIM(KEHUID) AS KEHUID,RTRIM(SBM) AS SBM,"
                        + "SLIANG,SSALE,RTRIM([USER]) AS [USER] FROM QGOODS_SALES WHERE (SETTIME BETWEEN '" + date1 + "' AND '" + date2 + "') AND (MDIAN = '" + xconfig.MDIAN + "')) A "
                        + "INNER JOIN QGOODS_YHUI B ON A.SBM=B.SBM INNER JOIN QGOODS_CUST_ZK C ON A.SBM=C.SBM GROUP BY A.SETTIME,A.SBM,[USER],YH,ZK,KEHUID,KHU", conn);
                    da.Fill(dt);
                    conn.Close();

                    button1.Enabled = false;
                    this.TopMost = false;
                    Form itemform = new 单据分析(dt, button1, this);
                    itemform.MdiParent = panFORM;
                    itemform.Show();
                    //this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (tabControl1.SelectedIndex == 1)
            {
                DateTime date1 = DateTime.Parse(dateTimePicker1.Text + " 00:00:00");
                DateTime date2 = DateTime.Parse(dateTimePicker2.Text + " 23:59:59");

                DataTable dt = new theDST().Tables["单据分析"];

                try
                {
                    SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT A.SETTIME,A.SBM,SUM(SLIANG) AS SLIANG,CAST(SUM(SSALE) AS DECIMAL(19,2)) AS SALES,KHU,KEHUID,"
                        + "[USER],B.YH AS YHUI,C.ZK FROM (SELECT CONVERT(CHAR(10),SETTIME,120) AS SETTIME,RTRIM(KHU) AS KHU,RTRIM(KEHUID) AS KEHUID,RTRIM(SBM) AS SBM,"
                        + "SLIANG,SSALE,RTRIM([USER]) AS [USER] FROM QGOODS_SALES WHERE (SETTIME BETWEEN '" + date1 + "' AND '" + date2 + "')) A "
                        + "INNER JOIN QGOODS_YHUI B ON A.SBM=B.SBM INNER JOIN QGOODS_CUST_ZK C ON A.SBM=C.SBM GROUP BY A.SETTIME,A.SBM,[USER],YH,ZK,KEHUID,KHU", conn);
                    da.Fill(dt);
                    conn.Close();

                    button1.Enabled = false;
                    this.TopMost = false;
                    Form itemform = new 单据分析(dt, button1, this);
                    itemform.MdiParent = panFORM;
                    itemform.Show();
                    //this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}