using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;

namespace Client.退货
{
    public partial class 门店退货 : Form
    {
        DataTable thisDT = new clidata().Tables["门店退货"];
        DataTable ckuDT = new DataTable();
        Form panFORM;
        ToolStripMenuItem panMENU;

        public 门店退货(ToolStripMenuItem menu,Form panform)
        {
            panFORM = panform;
            panMENU = menu;
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT A.SETTIME,A.[USER],A.MDIAN,A.SLIANG,A.SBM,A.DH,A.USERS,B.NTXT FROM (SELECT SETTIME,[USER],MDIAN,SUM(SLIANG) AS SLIANG,SBM,DH,USERS FROM (SELECT RTRIM([USER]) AS [USER],CONVERT(CHAR(10),SETTIME,120) AS SETTIME,RTRIM(MDIAN) AS MDIAN,SLIANG,RTRIM(SBM) AS SBM,RTRIM(DH) AS DH,RTRIM(USERS) AS USERS FROM QGOODS_BACK) DERIVEDTBL "
            + "GROUP BY SBM,SETTIME,[USER],[USERS],DH,MDIAN) A INNER JOIN QGOODS_BACK_TXT B ON A.SBM=B.SBM";
            config.conData.fill("sql", constr, cmdstr, thisDT);
            cmdstr = "SELECT RTRIM(NAME) AS NAME FROM QCKU";
            config.conData.fill("sql", constr, cmdstr, ckuDT);
            InitializeComponent();
        }

        private void 门店退货_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            DataView dv = new DataView();
            dv.Table = thisDT;
            dv.Sort = "ID DESC";
            dataGridView1.DataSource = dv;
            config.conDGV.DGVAutoID(dataGridView1, "phao");

            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                if (i.Cells["dHDataGridViewTextBoxColumn"].Value.ToString() != string.Empty)
                {
                    i.DefaultCellStyle.BackColor = Color.LightBlue;
                }
                if (i.Cells["dHDataGridViewTextBoxColumn"].Value.ToString() == "{撤退}")
                {
                    i.DefaultCellStyle.BackColor = Color.LightSalmon;
                }
            }

            if (dataGridView1.Rows.Count > 9)
            {
                pictureBox1.Visible = false;
            }

            foreach (DataRow i in ckuDT.Rows)
            {
                comboBox1.Items.Add(i["NAME"].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            string DH = dataGridView1["dHDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            bool dhbol;
            if (DH == string.Empty)
            {
                dhbol = true;
            }
            else
            {
                dhbol = false;
            }

            Form itemform = new 退货.退货详细(thisDT, dataGridView1["SBM", dataGridView1.CurrentCell.RowIndex].Value.ToString(), DH, dhbol,this);
            itemform.MdiParent = panFORM;
            this.TopMost = false;
            itemform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            if (comboBox1.Text == string.Empty)
            {
                comboBox1.DroppedDown = true;
                return;
            }

            string SBM = dataGridView1["SBM", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            if (SBM == "")
            {
                MessageBox.Show("未知错误！\n\n识别码不能为空。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("请再次确认！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }

            string DH;
            IPAddress IP = IPAddress.Parse(xconfig.SERVERIP);
            int PORT = xconfig.SERVERPORT;
            mesk.xSocket mesk = new mesk.xSocket();
            DH = mesk.Send(xconfig.USER + "|接退货单|GETCODETHD|GET<EOF>", IP, PORT);
            if (DH == "ERROR"||DH==string.Empty)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataGridViewRow idr = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

            DataTable itemdt = new DataTable();
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(TM) AS TM,ID FROM QGOODS_BACK WHERE (SBM='" + SBM + "')";
            config.conData.fill("sql", constr, cmdstr, itemdt);

            SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
            conn.Open();
            SqlCommand cmd;
            foreach (DataRow i in itemdt.Rows)
            {
                cmd = new SqlCommand("QGOODS_BACK_GET", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", i["ID"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@TM", i["TM"].ToString()));
                cmd.Parameters.Add(new SqlParameter("@SBM", SBM));
                cmd.Parameters.Add(new SqlParameter("@DH", DH));
                cmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                cmd.Parameters.Add(new SqlParameter("@CKU", comboBox1.Text));
                cmd.ExecuteNonQuery();
            }
            conn.Close();

            button2.Enabled = false;
            button4.Enabled = false;
            idr.Cells["dHDataGridViewTextBoxColumn"].Value = DH;
            idr.Cells["USERS"].Value = xconfig.USER;

            idr.DefaultCellStyle.BackColor = Color.LightBlue;
        }

        private void 门店退货_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void 门店退货_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            string DH = ((DataGridView)sender)["dHDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

            if (DH == string.Empty)
            {
                button2.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            if (MessageBox.Show("注意:\n\n此操作将退货信息退回给门店，\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
            {
                return;
            }

            string n_sbm = string.Empty;
            n_sbm = dataGridView1["SBM", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (n_sbm == string.Empty)
            {
                return;
            }

            DataTable n_dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(TM) AS TM,RTRIM(MDIAN) AS MDIAN FROM QGOODS_BACK WHERE (SBM='" + n_sbm + "')", conn);
                da.Fill(n_dt);

                SqlCommand cmd;
                foreach (DataRow i in n_dt.Rows)
                {
                    cmd = new SqlCommand("GOODS_BACK_REK", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", i["TM"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                    cmd.Parameters.Add(new SqlParameter("@MDIAN", i["MDIAN"].ToString()));
                    cmd.ExecuteNonQuery();
                }

                conn.Close();

                DataGridViewRow idr = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                idr.Cells["dHDataGridViewTextBoxColumn"].Value = "{撤退}";
                idr.DefaultCellStyle.BackColor = Color.LightSalmon;
                idr.Cells["USERS"].Value = xconfig.USER;

                button4.Enabled = false;
                button2.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable adt = new DataTable();
            SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT A.TM FROM QGOODS_BACK A INNER JOIN QGOODS_CKU B ON A.TM=B.TM WHERE A.DH='T01100034'",conn);
            da.Fill(adt);

            foreach (DataRow i in adt.Rows)
            {
                if (i["TM"].ToString() != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE QGOODS_CKU SET NAME='南海广场（加盟店）' WHERE TM='" + i["TM"].ToString() + "'",conn);
                    cmd.ExecuteNonQuery();
                }
            }

            conn.Close();

        }
    }
}