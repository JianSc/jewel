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
    public partial class 发货撤单 : Form
    {
        DataTable theDST = new clidata().Tables["发货单列表"];
        DataTable ckuDST = new clidata().Tables["仓库"];
        ToolStripMenuItem panMENU;
        DataGridView theDGV;
        Form panFORM;

        public 发货撤单(ToolStripMenuItem menu ,Form panform)
        {
            panFORM = panform;
            panMENU = menu;
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            DateTime time1 = DateTime.Parse(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 23:59:59");
            DateTime time2 = DateTime.Parse(DateTime.Now.AddDays(-7).ToShortDateString() + " 00:00:00");
            cmdstr = "SELECT RTRIM(DH) AS DH,RTRIM([USER]) AS [USER],SETTIME AS XTIME,SUM(SL) AS SL,RTRIM(MDIAN) AS MDIAN FROM "
            + "(SELECT DH,[USER],CONVERT(CHAR(10),SETTIME,120) AS SETTIME,SL,MDIAN FROM QGOODS_CKD WHERE (SETTIME BETWEEN '" + time2 + "' AND '" + time1 + "') AND (XSTAT=1)) DERIVEDTBL "
            + "GROUP BY DH,[USER],SETTIME,MDIAN";
            config.conData.fill("sql", constr, cmdstr, theDST);
            cmdstr = "SELECT RTRIM(NAME) AS NAME FROM QCKU";
            config.conData.fill("sql", constr, cmdstr, ckuDST);
            InitializeComponent();
        }

        private void 发货撤单_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            theDGV = dataGridView1;
            DataView itemdv = new DataView();
            itemdv.Table = theDST;
            itemdv.Sort = "ID DESC";
            theDGV.DataSource = itemdv;

            foreach (DataRow i in ckuDST.Rows)
            {
                comboBox1.Items.Add(i["name"].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (theDST.Rows.Count < 1)
            {
                return;
            }

            string DH = dataGridView1["dHDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (DH == "") { return; }

            Form itemform = new 发货撤单详细(DH, theDST);
            itemform.MdiParent = panFORM;
            itemform.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (theDST.Rows.Count < 1)
            {
                return;
            }

            if (comboBox1.Text == "")
            {
                comboBox1.DroppedDown = true;
                return;
            }

            string DH ;
            DH = dataGridView1["dHDataGridViewTextBoxColumn",dataGridView1.CurrentCell.RowIndex].Value.ToString();

            string msgstr = xconfig.USER + "|发货撤单|CONNECT<EOF>";
            bool msgbol;
            msgbol = xconfig.netSend(msgstr);

            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("即将进行的撤单操作将无法恢复！\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            MSG.login.Show("正在执行操作，请稍候.....");
            Application.DoEvents();

            DataTable itemDT = new DataTable();
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(TM) AS TM FROM QGOODS_CKD WHERE DH='" + DH + "'";
            config.conData.fill("sql", constr, cmdstr, itemDT);

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                foreach (DataRow i in itemDT.Rows)
                {
                    string TM, USER, CKU;
                    TM = i["TM"].ToString();
                    USER = xconfig.USER;
                    CKU = comboBox1.Text;

                    cmd = new SqlCommand("QZ_CKDEL", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", TM));
                    cmd.Parameters.Add(new SqlParameter("@USER", USER));
                    cmd.Parameters.Add(new SqlParameter("@CKU", CKU));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

                DataRow[] dr = theDST.Select("DH='" + DH + "'", "");
                if (dr.Length > 0) { theDST.Rows.Remove(dr[0]); }
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();
        }

        private void 发货撤单_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
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

        private void 发货撤单_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}