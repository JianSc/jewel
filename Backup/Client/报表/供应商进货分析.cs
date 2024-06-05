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
    public partial class 供应商进货分析 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable thisDT = new clidata().Tables["供应商进货分析"];

        public 供应商进货分析(ToolStripMenuItem m)
        {
            panMENU = m;
            InitializeComponent();
        }

        private void 供应商进货分析_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = thisDT;
        }

        private void 供应商进货分析_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            new 供应商进货分析查询(thisDT).ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            toolStripButton2.Enabled = false;
            MSG.login.ShowIMG();

            Thread t = new Thread(new ThreadStart(exlshow));
            t.Start();
        }

        private delegate void d();

        private void exlshow()
        {
            GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
            ext.Open(Application.StartupPath + @"\00I01.xlt");
            ext.FormCaption = "ColorBay";

            ext.SetCellText(1, "A", "日期");
            ext.SetCellText(1, "B", "供应商名称");
            ext.SetCellText(1, "C", "入库单号");
            ext.SetCellText(1, "D", "数量");
            ext.SetCellText(1, "E", "件重");
            ext.SetCellText(1, "F", "金重");
            ext.SetCellText(1, "G", "成本");

            int r = 2;
            foreach (DataRow i in thisDT.Rows)
            {
                ext.SetCellText(r, "A", i["SETTIME"].ToString());
                ext.SetCellText(r, "B", i["NAME"].ToString());
                ext.SetCellText(r, "C", i["DH"].ToString());
                ext.SetCellText(r, "D", i["SLIANG"].ToString());
                ext.SetCellText(r, "E", i["JIANZ"].ToString());
                ext.SetCellText(r, "F", i["JINZ"].ToString());
                ext.SetCellText(r, "G", i["CBEI"].ToString());
                r++;
            }

            MSG.login.CloseIMG();
            ext.ShowExcel();

            if (this.InvokeRequired) { d d = delegate { toolStripButton2.Enabled = true; }; this.Invoke(d); }
            else { toolStripButton2.Enabled = true; }
        }

    }
}