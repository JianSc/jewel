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
    public partial class 退货详细 : Form
    {
        DataTable panDT;
        string SBM = string.Empty;
        string DH = string.Empty;
        bool BOL;
        DataTable thisDT = new clidata().Tables["goods"];
        DataTable ckuDT = new DataTable();
        Form panform;
        
        public 退货详细(DataTable dt,string sbm,string dh,bool bol,Form f)
        {
            panDT = dt;
            SBM = sbm;
            DH = dh;
            BOL = bol;
            panform = f;

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(NAME) AS NAME FROM QCKU";
            config.conData.fill("sql", constr, cmdstr, ckuDT);

            InitializeComponent();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 退货详细_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            toolStripButton1.Enabled = BOL;
            dataGridView1.DataSource = thisDT;

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,"
            + "RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,QGOODS.SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,IMGBOL,RTRIM(YSE) AS YSE,"
            + "RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM QGOODS INNER JOIN QGOODS_BACK ON QGOODS.TM=QGOODS_BACK.TM WHERE (QGOODS_BACK.SBM='" + SBM + "')";
            config.conData.fill("sql", constr, cmdstr, thisDT);

            foreach (DataRow i in ckuDT.Rows)
            {
                toolStripComboBox1.Items.Add(i["NAME"].ToString());
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridViewRow i = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

            i.DefaultCellStyle.BackColor = Color.Green;
        }

        private void 未选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow i = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];

            i.DefaultCellStyle.BackColor = Color.Empty;
        }

        private void 清空选中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                i.DefaultCellStyle.BackColor = Color.Empty;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            if (toolStripComboBox1.Text == string.Empty)
            {
                toolStripComboBox1.DroppedDown = true;
                return;
            }

            string dh = string.Empty;
            IPAddress IP = IPAddress.Parse(xconfig.SERVERIP);
            int PORT = xconfig.SERVERPORT;
            mesk.xSocket mesk = new mesk.xSocket();
            dh = mesk.Send(xconfig.USER + "|退货接单|GETCODETHD|GET<EOF>", IP, PORT);
            if (dh == "ERROR" || dh == string.Empty)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                foreach (DataRow i in thisDT.Rows)
                {
                    cmd = new SqlCommand("QGOODS_BACK_GET", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", i["TM"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@SBM", SBM));
                    cmd.Parameters.Add(new SqlParameter("@DH", dh));
                    cmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                    cmd.Parameters.Add(new SqlParameter("@CKU", toolStripComboBox1.Text));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

                DataRow[] dr = panDT.Select("SBM='" + SBM + "'", "");
                foreach (DataRow i in dr)
                {
                    i["DH"] = dh;
                    i["USERS"] = xconfig.USER;
                }

                this.DH = dh;
                toolStripButton1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            bool imgbol;
            imgbol = bool.Parse(dataGridView1["imgbol", dataGridView1.CurrentCell.RowIndex].Value.ToString());
            string TM = dataGridView1["tm",dataGridView1.CurrentCell.RowIndex].Value.ToString();

            toolStripButton3.Enabled = imgbol;

            if (panel1.Visible)
            {
                if (imgbol)
                {
                    Image itemimg;
                    Bitmap itembmp;
                    itemimg = xconfig.netImgGET(xconfig.USER + "|退货接单|GETBMP|" + TM + "<EOF>");
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
            if (!panel1.Visible)
            {
                panel1.Visible = true;
            }
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            try{
                GoldPrinter.ExcelAccess exl = new GoldPrinter.ExcelAccess();
                exl.Open(Application.StartupPath + @"\00I01.xlt");
                exl.FormCaption = "ColorBay";

                exl.SetCellText(1, "A", "条码");
                exl.SetCellText(1, "B", "数量");
                exl.SetCellText(1, "C", "门店/仓库");
                exl.SetCellText(1, "D", "单号");
                exl.SetCellText(1, "E", "证书号");
                exl.SetCellText(1, "F", "金料");
                exl.SetCellText(1, "G", "石料");
                exl.SetCellText(1, "H", "首饰类别");
                exl.SetCellText(1, "I", "品名");
                exl.SetCellText(1, "J", "款号");
                exl.SetCellText(1, "K", "成本");
                exl.SetCellText(1, "L", "售价");
                exl.SetCellText(1, "M", "供应商");
                exl.SetCellText(1, "N", "日期");
                exl.SetCellText(1, "O", "主石");
                exl.SetCellText(1, "P", "辅石");
                exl.SetCellText(1, "Q", "件重");
                exl.SetCellText(1, "R", "金重");
                exl.SetCellText(1, "S", "圈口");
                exl.SetCellText(1, "T", "净度");
                exl.SetCellText(1, "U", "颜色");


                int j = 1;
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                SqlDataReader rs;
                foreach (DataRow i in thisDT.Rows)
                {
                    exl.SetCellText(j + 1, "A", i["TM"].ToString());
                    //exl.SetRowHeight(j + 1, 18.75f);
                    string tm = i["TM"].ToString();
                    exl.SetCellText(j + 1, "B", i["SLIANG"].ToString());
                    cmd = new SqlCommand("SELECT RTRIM(NAME) AS NAME FROM QGOODS_CKU WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        exl.SetCellText(j + 1, "C", rs["NAME"].ToString());
                    }
                    rs.Close();
                    cmd = new SqlCommand("SELECT RTRIM(DH) AS DH FROM QGOODS_RKD WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        exl.SetCellText(j + 1, "D", rs["DH"].ToString());
                    }
                    rs.Close();
                    exl.SetCellText(j + 1, "E", i["ZSHU"].ToString());
                    exl.SetCellText(j + 1, "F", i["JLIAO"].ToString());
                    exl.SetCellText(j + 1, "G", i["SLIAO"].ToString());
                    exl.SetCellText(j + 1, "H", i["SSI"].ToString());
                    exl.SetCellText(j + 1, "I", i["NAME"].ToString());
                    exl.SetCellText(j + 1, "J", i["KUS"].ToString());
                    exl.SetCellText(j + 1, "K", i["CBEI"].ToString());
                    exl.SetCellText(j + 1, "L", i["XSOU"].ToString());
                    cmd = new SqlCommand("SELECT RTRIM(NAME) AS NAME FROM QGOODS_GYS_LIST WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        exl.SetCellText(j + 1, "M", rs["NAME"].ToString());
                    }
                    rs.Close();
                    cmd = new SqlCommand("SELECT CONVERT(CHAR(10),SETTIME,120) AS SETTIME FROM QGOODS WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        exl.SetCellText(j + 1, "N", rs["SETTIME"].ToString());
                    }
                    rs.Close();
                    exl.SetCellText(j + 1, "O", i["ZSHI"].ToString());
                    exl.SetCellText(j + 1, "P", i["FSHI"].ToString());
                    exl.SetCellText(j + 1, "Q", i["JIANZ"].ToString());
                    exl.SetCellText(j + 1, "R", i["JINZ"].ToString());
                    if (i["QKOU"].ToString() != "0")
                    {
                        exl.SetCellText(j + 1, "S", i["QKOU"].ToString());
                    }
                    exl.SetCellText(j + 1, "T", i["JDU"].ToString());
                    exl.SetCellText(j + 1, "U", i["YSE"].ToString());
                    j++;
                }
                conn.Close();
                exl.ShowExcel();
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void 退货详细_FormClosed(object sender, FormClosedEventArgs e)
        {
            panform.TopMost = true;
        }
    }
}