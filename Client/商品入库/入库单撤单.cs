using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Client.商品入库
{
    public partial class 入库单撤单 : Form
    {
        DataTable panDT = xconfig.DST.Tables["入库单列表"];
        DataGridView panDGV;
        ToolStripMenuItem panMENU;
        Form panFORM;

        public 入库单撤单(ToolStripMenuItem menu,Form panform)
        {
            panMENU = menu;
            panFORM = panform;

            try
            {
                string constr, cmdstr;
                constr = xconfig.CONNSTR;
                panDGV = dataGridView1;
                DateTime time1 = DateTime.Parse(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 23:59:59");
                DateTime time2 = DateTime.Parse(DateTime.Now.AddDays(-7).ToShortDateString() + " 00:00:00");
                cmdstr = "SELECT RTRIM(DH) AS DH,RTRIM([USER]) AS [USER],SUM(SL) AS SL,SETDATE AS XTIME FROM (SELECT DH,CONVERT(CHAR(10),SETDATE,120) AS SETDATE,[USER],QGOODS_RKD.SL FROM QGOODS_RKD INNER JOIN QGOODS ON QGOODS_RKD.TM=QGOODS.TM WHERE (SETDATE BETWEEN '" + time2 + "' AND '" + time1 + "') AND (QGOODS.XSTAT=1)) DERIVEDTBL "
            + " GROUP BY DH,[USER],SETDATE";
                panDT.Clear();
                config.conData.fill("sql", constr, cmdstr, panDT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return;
            }

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 入库单撤单_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            DataView itemdv = new DataView();
            itemdv.Table = panDT;
            itemdv.Sort = "id desc";
            dataGridView1.DataSource = itemdv;
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
            dataGridView1_RowsAdded(sender, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panDT.Rows.Count < 1)
            {
                return;
            }

            string msg = xconfig.USER + "|撤单|CONNECT<EOF>";
            xconfig.netSend(msg);

            string delDH;
            delDH = dataGridView1["dHDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (MessageBox.Show("即将进行的撤单操作将无法恢复！\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }

            MSG.login.Show("请稍候，正在执行操作.....");
            Application.DoEvents();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

            DataTable delRKD = new DataTable();
            delRKD.Clear();
            string constr,cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT DH,TM FROM QGOODS_RKD WHERE DH='" + delDH + "'";
            config.conData.fill("sql", constr, cmdstr, delRKD);
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                foreach (DataRow i in delRKD.Rows)
                {
                    string temTM = i["TM"].ToString();
                    cmd = new SqlCommand("QZ_GOODSDEL", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", temTM));
                    cmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                DataRow[] itemdr = panDT.Select("DH='" + delDH + "'", "");
                foreach (DataRow i in itemdr)
                {
                    panDT.Rows.Remove(i);
                }
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panDT.Rows.Count < 1)
            {
                return;
            }

            string msg = xconfig.USER + "|撤单|CONNECT<EOF>";
            xconfig.netSend(msg);

            string itemDH;
            itemDH = dataGridView1["dHDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            Form itemform = new 入库单撤单详细(panDT, panDGV, itemDH);
            itemform.MdiParent = panFORM;
            itemform.Show();
            this.Close();
        }

        private void 入库单撤单_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;

            string msg = xconfig.USER + "|闲置|CONNECT<EOF>";
            xconfig.netSend(msg);
        }

        private void 入库单撤单_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}