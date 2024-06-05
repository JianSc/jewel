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
    public partial class 入库单撤单详细 : Form
    {
        DataTable panDT;
        DataGridView panDGV;
        DataTable theDT = new clidata().Tables["goods"];
        string panDH;

        public 入库单撤单详细(DataTable idt,DataGridView idgv,string idh)
        {
            panDT = idt;
            panDGV = idgv;
            panDH = idh;
            InitializeComponent();
        }

        private void 入库单撤单详细_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            MSG.login.Show("正在载入，请稍候.....");
            Application.DoEvents();

            toolStripLabel3.Text = panDH;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(QGOODS.JLIAO) AS JLIAO,RTRIM(QGOODS.SLIAO) AS SLIAO,"
                    + "RTRIM(QGOODS.SSI) AS SSI,RTRIM(QGOODS.ZSHU) AS ZSHU,QKOU,RTRIM(QGOODS.KUS) AS KUS,RTRIM(QGOODS.HHAO) AS HHAO,RTRIM(QGOODS.DWEI) AS DWEI,"
                    + "QGOODS.SLIANG,QGOODS.JIANZ,QGOODS.JINZ,QGOODS.PJIANZ,QGOODS.BLU,QGOODS.ZSZ,QGOODS.ZSS,QGOODS.FSZ,QGOODS.FSS,QGOODS.CBEI,QGOODS.XSOU,"
                    + "QGOODS.BZHU,QGOODS.SLBOL,QGOODS.IMGBOL,RTRIM(QGOODS.JDU) AS JDU,RTRIM(QGOODS.YSE) AS YSE,RTRIM(QGOODS.XZUANG) AS XZUANG,"
                    + "RTRIM(QGOODS.QGONG) AS QGONG,QGOODS.XSTAT,RTRIM(QGOODS.CADI) AS CADI,RTRIM(QGOODS.DDH) AS DDH,CONVERT(CHAR(10),QGOODS.SETTIME,120) AS SETTIME,ID FROM "
                    + "QGOODS INNER JOIN QGOODS_RKD ON QGOODS.TM = QGOODS_RKD.TM "
                    + "WHERE (QGOODS_RKD.DH = '" + panDH + "')", conn);
                theDT.Clear();
                cmd.Fill(theDT);
                conn.Close();
                foreach (DataRow i in theDT.Rows)
                {
                    if (i["qkou"].ToString() == "0")
                    {
                        i["qkou"] = "";
                    }
                }
                dataGridView1.DataSource = theDT;
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();

        }

        private void toolStripLabel3_TextChanged(object sender, EventArgs e)
        {
            this.Text = ((ToolStripLabel)sender).Text;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (theDT.Rows.Count < 1)
            {
                return;
            }

            if (MessageBox.Show("即将进行的撤单操作将无法恢复！\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }

            string msg = xconfig.USER + "|撤单|CONNECT<EOF>";
            xconfig.netSend(msg);

            string theTM;
            theTM = dataGridView1["tmDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("QZ_GOODSDEL", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TM", theTM));
                cmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                cmd.ExecuteNonQuery();
                conn.Close();

                DataRow[] thedr = theDT.Select("tm='" + theTM + "'", "");
                int theint = int.Parse(thedr[0]["sliang"].ToString());
                theDT.Rows.Remove(thedr[0]);
                DataRow[] pandr = panDT.Select("dh='" + panDH + "'", "");
                int panint = int.Parse(pandr[0]["sl"].ToString());
                int newint = panint - theint;
                pandr[0]["sl"] = newint.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).Rows.Count < 1) { return; }
            string tm;
            tm = ((DataGridView)sender)["tmDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();
            if (tm == "")
            {
                return;
            }

            bool imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
            toolStripButton3.Enabled = imgbol;

            if (panel1.Visible)
            {
                if (imgbol)
                {
                    Image itemimg;
                    Bitmap itembmp;
                    itemimg = xconfig.netImgGET(xconfig.USER + "|撤单详细|GETBMP|" + tm + "<EOF>");
                    itembmp = new Bitmap(itemimg, pictureBox1.Size);
                    pictureBox1.Image = itembmp;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.ImgGet_Err;
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }
    }
}