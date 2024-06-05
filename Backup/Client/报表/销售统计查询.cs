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
    public partial class 销售统计查询 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable thisDT = new clidata().Tables["销售统计"];
        Form panFR;

        public 销售统计查询(ToolStripMenuItem t,Form f)
        {
            panFR = f;
            panMENU = t;
            InitializeComponent();
        }

        private void 销售统计查询_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            this.TopMost = true;

            int maxyear, minyear;
            maxyear = DateTime.Now.Year;
            minyear = maxyear - 10;
            for (int i = 0; i < 11; i++)
            {
                int years = maxyear - i;
                comboBox1.Items.Add(years.ToString());
            }
            for (int i = 1; i < 13; i++)
            {
                comboBox2.Items.Add(i.ToString());
            }

            DataTable mdaindt = new DataTable();
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM([NAME]) AS [NAME] FROM QMDIAN";
            config.conData.fill("sql", constr, cmdstr, mdaindt);

            comboBox3.Items.Add("");
            comboBox4.Items.Add("");

            foreach (DataRow i in mdaindt.Rows)
            {
                comboBox3.Items.Add(i["NAME"].ToString());
                comboBox4.Items.Add(i["NAME"].ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime a, b;
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

                string cmd2 = string.Empty;
                if (comboBox3.Text.Trim() != string.Empty)
                {
                    cmd2 = " AND (MDIAN='" + comboBox3.Text + "')";
                }

                a = xconfig.DateTime_Max_Min(comboBox1.Text, comboBox2.Text, false);
                b = xconfig.DateTime_Max_Min(comboBox1.Text, comboBox2.Text, true);
                string constr = xconfig.CONNSTR;
                string cmdstr = "SELECT RTRIM(QGOODS_SALES.TM) AS TM,QGOODS_SALES.CBEI AS CBEI,SALE,SSALE,RTRIM(MDIAN) AS MDIAN,RTRIM(QGOODS_SALES.[USER]) AS [USER],RTRIM(QGOODS_SALES.KHU) AS KHU,CONVERT(CHAR(10),QGOODS_SALES.SETTIME,120) AS SETTIME,ZKOU AS ZK,RTRIM(SBM) AS SBM,RTRIM(QGOODS_SALES.DWEI) AS DWEI,QGOODS_SALES.SLIANG,"
                + "RTRIM(QGOODS.JLIAO) AS JLIAO,RTRIM(QGOODS.SLIAO) AS SLIAO,RTRIM(QGOODS.SSI) AS SSI,QGOODS.JINZ,QGOODS.JIANZ,QGOODS.ZSS,QGOODS.ZSZ,QGOODS.FSZ,QGOODS.FSS,QGOODS.IMGBOL,RTRIM(QGOODS.KUS) AS KUS FROM QGOODS_SALES "
                + "INNER JOIN QGOODS ON QGOODS.TM=QGOODS_SALES.TM WHERE (QGOODS_SALES.SETTIME BETWEEN '" + a + "' AND '" + b + "')";
                cmdstr += cmd2;

                config.conData.fill("sql", constr, cmdstr, thisDT);
                if (thisDT.Rows.Count > 0)
                {
                    string xsl, jianz, jinz, cbei, sale, ssale;
                    xsl = thisDT.Compute("sum(sliang)", "").ToString();
                    jianz = thisDT.Compute("sum(jianz)", "").ToString();
                    jinz = thisDT.Compute("sum(jinz)", "").ToString();
                    cbei = thisDT.Compute("sum(cbei)", "").ToString();
                    sale = thisDT.Compute("sum(sale)", "").ToString();
                    ssale = thisDT.Compute("sum(ssale)", "").ToString();

                    DataRow i = thisDT.NewRow();
                    i["sbm"] = "合计:";
                    i["sliang"] = xsl.ToString();
                    i["jianz"] = jianz.ToString();
                    i["jinz"] = jinz.ToString();
                    i["cbei"] = cbei.ToString();
                    i["sale"] = sale.ToString();
                    i["ssale"] = ssale.ToString();
                    thisDT.Rows.Add(i);
                }

                this.TopMost = false;
                Form itemform = new 报表.销售统计(thisDT, this);
                itemform.MdiParent = panFR;
                itemform.Show();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                //if (comboBox1.Text == string.Empty)
                //{
                //    comboBox1.DroppedDown = true;
                //    return;
                //}
                //if (comboBox2.Text == string.Empty)
                //{
                //    comboBox2.DroppedDown = true;
                //    return;
                //}

                string cmd2 = string.Empty;
                if (comboBox4.Text.Trim() != string.Empty)
                {
                    cmd2 = " AND (MDIAN='" + comboBox4.Text + "')";
                }

                a = DateTime.Parse(dateTimePicker1.Text + " 00:00:00");
                b = DateTime.Parse(dateTimePicker2.Text + " 23:59:59");
                string constr = xconfig.CONNSTR;
                string cmdstr = "SELECT RTRIM(QGOODS_SALES.TM) AS TM,QGOODS_SALES.CBEI AS CBEI,SALE,SSALE,RTRIM(MDIAN) AS MDIAN,RTRIM(QGOODS_SALES.[USER]) AS [USER],RTRIM(QGOODS_SALES.KHU) AS KHU,CONVERT(CHAR(10),QGOODS_SALES.SETTIME,120) AS SETTIME,ZKOU AS ZK,RTRIM(SBM) AS SBM,RTRIM(QGOODS_SALES.DWEI) AS DWEI,QGOODS_SALES.SLIANG,"
                + "RTRIM(QGOODS.JLIAO) AS JLIAO,RTRIM(QGOODS.SLIAO) AS SLIAO,RTRIM(QGOODS.SSI) AS SSI,QGOODS.JINZ,QGOODS.JIANZ,QGOODS.ZSS,QGOODS.ZSZ,QGOODS.FSZ,QGOODS.FSS,QGOODS.IMGBOL,RTRIM(QGOODS.KUS) AS KUS FROM QGOODS_SALES "
                + "INNER JOIN QGOODS ON QGOODS.TM=QGOODS_SALES.TM WHERE (QGOODS_SALES.SETTIME BETWEEN '" + a + "' AND '" + b + "')";
                cmdstr += cmd2;

                config.conData.fill("sql", constr, cmdstr, thisDT);
                if (thisDT.Rows.Count > 0)
                {
                    string xsl, jianz, jinz, cbei, sale, ssale;
                    xsl = thisDT.Compute("sum(sliang)", "").ToString();
                    jianz = thisDT.Compute("sum(jianz)", "").ToString();
                    jinz = thisDT.Compute("sum(jinz)", "").ToString();
                    cbei = thisDT.Compute("sum(cbei)", "").ToString();
                    sale = thisDT.Compute("sum(sale)", "").ToString();
                    ssale = thisDT.Compute("sum(ssale)", "").ToString();

                    DataRow i = thisDT.NewRow();
                    i["sbm"] = "合计:";
                    i["sliang"] = xsl.ToString();
                    i["jianz"] = jianz.ToString();
                    i["jinz"] = jinz.ToString();
                    i["cbei"] = cbei.ToString();
                    i["sale"] = sale.ToString();
                    i["ssale"] = ssale.ToString();
                    thisDT.Rows.Add(i);
                }

                this.TopMost = false;
                Form itemform = new 报表.销售统计(thisDT, this);
                itemform.MdiParent = panFR;
                itemform.Show();
            }
            
        }

        private void 销售统计查询_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}