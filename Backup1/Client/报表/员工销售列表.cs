using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Client.报表
{
    public partial class 员工销售列表 : Form
    {
        DataTable panDT;
        Form panFR;
        DateTime a, b;
        public 员工销售列表(DataTable d,Form f,DateTime aa,DateTime bb)
        {
            a = aa;
            b = bb;
            panDT = d;
            panFR = f;
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 员工销售列表_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            DataView dv = new DataView();
            dv.Table = panDT;
            dv.Sort = "SSALE DESC";
            dataGridView1.DataSource = dv;

            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                i.Cells["SETTIME"].Value = "[" + a.ToShortDateString() + " -> " + b.ToShortDateString() + "]";
            }

            config.conDGV.DGVAutoID(dataGridView1, "phao");
        }

        private void 员工销售列表_FormClosed(object sender, FormClosedEventArgs e)
        {
            panFR.TopMost = true;
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView1, "phao");
            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                i.Cells["SETTIME"].Value = "[" + a.ToShortDateString() + " -> " + b.ToShortDateString() + "]";
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            MSG.login.ShowIMG();
            Application.DoEvents();
            toolStripButton3.Enabled = false;

            Thread t = new Thread(new ThreadStart(show));
            t.Start();
        }

        private delegate void d();
        private void show()
        {
            DataGridView g = new DataGridView();
            if (dataGridView1.InvokeRequired)
            {
                d d = delegate
                {
                    g = dataGridView1;
                };
                dataGridView1.Invoke(d);
            }
            else
            {
                g = dataGridView1;
            }

            GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
            ext.Open(Application.StartupPath + @"\00I01.xlt");
            ext.FormCaption = "ColorBay";

            ext.SetCellText(1, "A", "员工姓名");
            ext.SetCellText(1, "B", "时间段");
            ext.SetCellText(1, "C", "销售数量");
            ext.SetCellText(1, "D", "销售金额");

            int r = 2;
            foreach (DataGridViewRow i in g.Rows)
            {
                ext.SetCellText(r, "A", i.Cells["uSERDataGridViewTextBoxColumn"].Value.ToString());
                ext.SetCellText(r, "B", i.Cells["SETTIME"].Value.ToString());
                ext.SetCellText(r, "C", i.Cells["sLIANGDataGridViewTextBoxColumn"].Value.ToString());
                ext.SetCellText(r, "D", i.Cells["sSALEDataGridViewTextBoxColumn"].Value.ToString());
                r++;
            }

            MSG.login.CloseIMG();
            if (this.InvokeRequired) { d d = delegate { toolStripButton3.Enabled = true; }; this.Invoke(d); }
            else { toolStripButton3.Enabled = true; }
            ext.ShowExcel();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            string user = dataGridView1["uSERDataGridViewTextBoxColumn",dataGridView1.CurrentCell.RowIndex].Value.ToString();

            DataTable t = new clidata().Tables["销售统计"];
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(QGOODS_SALES.TM) AS TM,RTRIM(QGOODS.SLIAO) AS SLIAO,RTRIM(QGOODS.JLIAO) AS JLIAO,RTRIM(QGOODS.SSI) AS SSI,QGOODS.JINZ,QGOODS.JIANZ,QGOODS.ZSS,QGOODS.ZSZ,QGOODS.FSS,QGOODS.FSZ,QGOODS.IMGBOL,QGOODS_SALES.CBEI,SALE,ZKOU AS ZK,SSALE,CONVERT(CHAR(10),QGOODS_SALES.SETTIME,120) AS SETTIME,RTRIM(KHU) AS KHU,RTRIM([USER]) AS [USER],RTRIM(QGOODS.KUS) AS KUS,RTRIM(QGOODS.ZSHU) AS ZSHU,RTRIM(MDIAN) AS MDIAN,RTRIM(SBM) AS SBM,QGOODS_SALES.SLIANG,RTRIM(QGOODS_SALES.DWEI) AS DWEI FROM "
            + "QGOODS_SALES INNER JOIN QGOODS ON QGOODS.TM=QGOODS_SALES.TM WHERE (QGOODS_SALES.SETTIME BETWEEN '" + a + "' AND '" + b + "') AND (QGOODS_SALES.[USER]='" + user + "')";
            config.conData.fill("sql", constr, cmdstr, t);

            Form itemform = new 报表.员工销售明细(t);
            itemform.MdiParent = this.MdiParent;
            itemform.Show();

        }
    }
}