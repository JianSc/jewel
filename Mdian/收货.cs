using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mdian
{
    public partial class 收货 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable cliDST = new theDST().Tables["发货单列表"];
        DataGridView cliDGV;
        Form panFORM;

        public 收货(ToolStripMenuItem menu,Form panform)
        {
            panMENU = menu;
            panFORM = panform;

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(DH) AS DH,RTRIM([USER]) AS [USER],SETTIME AS XTIME,SUM(SL) AS SL FROM "
            + "(SELECT DH,[USER],CONVERT(CHAR(10),SETTIME,120) AS SETTIME,SL,MDIAN FROM QGOODS_CKD WHERE (XSTAT=1) AND (MDIAN='" + xconfig.MDIAN + "')) DERIVEDTBL "
            + "GROUP BY DH,[USER],SETTIME ORDER BY SETTIME DESC";
            config.conData.fill("sql", constr, cmdstr, cliDST);

            InitializeComponent();
        }

        private void 收货_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            cliDGV = dataGridView1;
            cliDGV.DataSource = cliDST;
        }

        private void 收货_FormClosed(object sender, FormClosedEventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cliDST.Rows.Count < 1) { return; }

            string msgstr = xconfig.USER + "|收货接单|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("连接服务器错误！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string dh;
            dh = dataGridView1["dHDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (dh == "") { return; }
            Form itemform = new 收货详细(dh,this);
            itemform.MdiParent = panFORM;
            this.TopMost = false;
            itemform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cliDST.Rows.Count < 1) { return; }
            if (MessageBox.Show("请再次确认！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            string dh = dataGridView1["dHDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            if (dh == "") { return; }

            DataTable newDT = new DataTable();

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(QGOODS.TM) AS TM FROM QGOODS INNER JOIN "
            + "QGOODS_CKD ON QGOODS.TM=QGOODS_CKD.TM "
            + "WHERE (QGOODS_CKD.XSTAT=1) AND (QGOODS_CKD.DH='" + dh + "')";
            config.conData.fill("sql", constr, cmdstr, newDT);

            if (newDT.Rows.Count < 1) { return; }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;

                cmd = new SqlCommand("UPDATE QGOODS_CKD SET XSTAT=0,MDIANUSER='" + xconfig.USER + "' WHERE (XSTAT=1) AND (DH='" + dh + "')", conn);
                cmd.ExecuteNonQuery();

                foreach (DataRow i in newDT.Rows)
                {
                    cmd = new SqlCommand("QZ_CKD_SH", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", i["TM"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                    cmd.Parameters.Add(new SqlParameter("@MDIAN", xconfig.MDIAN));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

                DataRow[] itemdr = cliDST.Select("DH='" + dh + "'", "");
                if (itemdr.Length > 0) { cliDST.Rows.Remove(itemdr[0]); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}