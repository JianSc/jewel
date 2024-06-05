using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Client.门店发货
{
    public partial class 导入单据 : Form
    {
        DataTable panDT;
        DataTable rkDT = new clidata().Tables["入库单列表"];
        DataTable thDT = new clidata().Tables["入库单列表"];
        Label panLB;
        public 导入单据(DataTable dt,Label lb)
        {
            panLB = lb;
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            DateTime TIME1,TIME2;
            TIME1 = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
            TIME2 = DateTime.Parse(DateTime.Now.AddYears(-1).ToShortDateString () + " 00:00:00");
            cmdstr = "SELECT RTRIM(DH) AS DH,RTRIM([USER]) AS [USER],SUM(SL) AS SL,SETDATE AS XTIME FROM (SELECT DH,CONVERT(CHAR(10),SETDATE,120) AS SETDATE,[USER],QGOODS_RKD.SL FROM QGOODS_RKD INNER JOIN QGOODS ON QGOODS_RKD.TM=QGOODS.TM WHERE (SETDATE BETWEEN '" + TIME2 + "' AND '" + TIME1 + "') AND (QGOODS.XSTAT=1)) DERIVEDTBL "
            + " GROUP BY DH,[USER],SETDATE";
            config.conData.fill("sql", constr, cmdstr, rkDT);
            cmdstr = "SELECT RTRIM(DH) AS DH,RTRIM([USER]) AS [USER],SUM(SL) AS SL,SETTIME AS XTIME FROM (SELECT DH,CONVERT(CHAR(10),QGOODS_BACK.SETTIME,120) AS SETTIME,[USER],QGOODS_BACK.SLIANG AS SL FROM QGOODS_BACK INNER JOIN QGOODS ON QGOODS_BACK.TM=QGOODS.TM WHERE (QGOODS_BACK.SETTIME BETWEEN '" + TIME2 + "' AND '" + TIME1 + "') AND (QGOODS.XSTAT=1) AND (QGOODS_BACK.DH<>'{撤退}') AND (QGOODS_BACK.DH<>'') AND (QGOODS_BACK.USERS<>'')) A "
            + " GROUP BY DH,[USER],SETTIME";
            config.conData.fill("sql", constr, cmdstr, thDT);
            panDT = dt;
            InitializeComponent();
        }

        private void 导入单据_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT DH FROM QGOODS_RKD_DR", conn);
            SqlDataReader rs = cmd.ExecuteReader();
            while (rs.Read())
            {
                string dh = rs["DH"].ToString().Trim();
                DataRow[] dr = rkDT.Select("DH='" + dh + "'", "");
                foreach (DataRow i in dr)
                {
                    rkDT.Rows.Remove(i);
                }
            }
            rs.Close();
            cmd = new SqlCommand("SELECT DH FROM QGOODS_BACK_DR", conn);
            rs = cmd.ExecuteReader();
            while (rs.Read())
            {
                string dh = rs["DH"].ToString().Trim();
                DataRow[] dr = thDT.Select("DH='" + dh + "'", "");
                foreach (DataRow i in dr)
                {
                    thDT.Rows.Remove(i);
                }
            }
            rs.Close();
            conn.Close();

            DataView itemdv = new DataView();
            itemdv.Table = rkDT;
            itemdv.Sort = "ID DESC";
            DataView tdv = new DataView();
            tdv.Table = thDT;
            tdv.Sort = "ID DESC";
            dataGridView1.DataSource = itemdv;
            dataGridView2.DataSource = tdv;
            config.conDGV.DGVAutoID(dataGridView1, "phao");
            config.conDGV.DGVAutoID(dataGridView2, "phao2");
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int rows = ((DataGridView)sender).Rows.Count;
            if (rows > 9)
            {
                pictureBox1.Visible = false;
            }
            else if (rows < 10)
            {
                pictureBox1.Visible = true;
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView1_RowsAdded(dataGridView1, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rkDT.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show();

            string DH = dataGridView1["dHDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (DH == "") { return; }

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,"
                    + "RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,"
                    + "RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,"
                    + "CBEI,XSOU,BZHU,SLBOL,IMGBOL,RTRIM(JDU) AS JDU,RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,"
                    + "RTRIM(QGONG) AS QGONG FROM QGOODS INNER JOIN QGOODS_RKD ON QGOODS.TM=QGOODS_RKD.TM WHERE (QGOODS_RKD.DH='" + DH + "') AND (QGOODS.XSTAT=1)";
            config.conData.fill("sql", constr, cmdstr, panDT);
            panLB.Text = DH;

            this.Close();
            MSG.login.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (thDT.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show();

            string DH = dataGridView2["dHDataGridViewTextBoxColumn1", dataGridView2.CurrentCell.RowIndex].Value.ToString();
            if (DH == "") { return; }

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,"
                    + "RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,"
                    + "RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,QGOODS.SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,"
                    + "CBEI,XSOU,BZHU,SLBOL,IMGBOL,RTRIM(JDU) AS JDU,RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,"
                    + "RTRIM(QGONG) AS QGONG FROM QGOODS INNER JOIN QGOODS_BACK ON QGOODS.TM=QGOODS_BACK.TM WHERE (QGOODS_BACK.DH='" + DH + "') AND (QGOODS.XSTAT=1)";
            config.conData.fill("sql", constr, cmdstr, panDT);
            panLB.Text = DH;

            this.Close();
            MSG.login.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView1, "phao");
            config.conDGV.DGVAutoID(dataGridView2, "phao2");
        }
    }
}