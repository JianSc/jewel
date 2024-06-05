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
    public partial class 发货撤单详细 : Form
    {
        DataTable theDST = new clidata().Tables["goods"];
        string panDH;
        DataTable panDST;
        DataGridView theDGV;
        DataTable ckuDST = new DataTable();

        public 发货撤单详细(string DH,DataTable dst)
        {
            panDST = dst;
            panDH = DH;
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(QGOODS.JLIAO) AS JLIAO,RTRIM(QGOODS.SLIAO) AS SLIAO,"
                    + "RTRIM(QGOODS.SSI) AS SSI,RTRIM(QGOODS.ZSHU) AS ZSHU,QKOU,RTRIM(QGOODS.KUS) AS KUS,RTRIM(QGOODS.HHAO) AS HHAO,RTRIM(QGOODS.DWEI) AS DWEI,"
                    + "QGOODS.SLIANG,QGOODS.JIANZ,QGOODS.JINZ,QGOODS.PJIANZ,QGOODS.BLU,QGOODS.ZSZ,QGOODS.ZSS,QGOODS.FSZ,QGOODS.FSS,QGOODS.CBEI,QGOODS.XSOU,"
                    + "QGOODS.BZHU,QGOODS.SLBOL,QGOODS.IMGBOL,RTRIM(QGOODS.JDU) AS JDU,RTRIM(QGOODS.YSE) AS YSE,RTRIM(QGOODS.XZUANG) AS XZUANG,"
                    + "RTRIM(QGOODS.QGONG) AS QGONG,QGOODS.XSTAT,RTRIM(QGOODS.CADI) AS CADI,RTRIM(QGOODS.DDH) AS DDH,CONVERT(CHAR(10),QGOODS.SETTIME,120) AS SETTIME,ID FROM "
                    + "QGOODS INNER JOIN QGOODS_CKD ON QGOODS.TM = QGOODS_CKD.TM "
                    + "WHERE (QGOODS_CKD.DH = '" + DH + "')";
            config.conData.fill("sql", constr, cmdstr, theDST);
            cmdstr = "SELECT RTRIM(NAME) AS NAME FROM QCKU";
            config.conData.fill("sql", constr, cmdstr, ckuDST);

            InitializeComponent();
        }

        private void 发货撤单详细_Load(object sender, EventArgs e)
        {
            theDGV = dataGridView1;
            theDGV.DataSource = theDST;
            toolStripTextBox1.Text = panDH;
            this.WindowState = FormWindowState.Maximized;

            foreach (DataRow i in ckuDST.Rows)
            {
                toolStripComboBox1.Items.Add(i["name"].ToString());
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).Rows.Count < 1) { return; }

            try
            {
                bool imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                string TM = ((DataGridView)sender)["tmDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();
                toolStripButton3.Enabled = imgbol;

                try
                {
                    if (panel1.Visible)
                    {
                        if (imgbol)
                        {
                            Image itemimg;
                            Bitmap itembmp;
                            itemimg = xconfig.netImgGET(xconfig.USER + "|发货撤单|GETBMP|" + TM + "<EOF>");
                            itembmp = new Bitmap(itemimg, pictureBox1.Size);
                            pictureBox1.Image = itembmp;
                        }
                        else
                        {
                            pictureBox1.Image = Properties.Resources.ImgGet_Err;
                        }
                    }
                }
                catch { pictureBox1.Image = Properties.Resources.ImgGet_Err; }
            }
            catch { }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (theDST.Rows.Count < 1) { return; }
            if (toolStripComboBox1.Text == "") { toolStripComboBox1.DroppedDown = true; return; }
            if (MessageBox.Show("即将进行的撤单操作将无法恢复！\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            string TM;
            TM = dataGridView1["tmDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (TM == "") { return; }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("QZ_CKDEL", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TM", TM));
                cmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                cmd.Parameters.Add(new SqlParameter("@CKU", toolStripComboBox1.Text));
                cmd.ExecuteNonQuery();
                conn.Close();

                DataRow[] dr = theDST.Select("TM='" + TM + "'", "");
                int rowsl = int.Parse(dr[0]["sliang"].ToString());
                DataRow[] pandr = panDST.Select("DH='" + panDH + "'", "");
                int pansl = int.Parse(pandr[0]["SL"].ToString());
                pandr[0]["SL"] = pansl - rowsl;
                theDST.Rows.Remove(dr[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}