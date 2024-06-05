using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;

namespace Client.库退
{
    public partial class 库退 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable thisDT = new clidata().Tables["goods"];

        public 库退(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 库退_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = thisDT;
        }

        private void 库退_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                textBox1.Focus();
                return;
            }

            DataRow[] dr = thisDT.Select("TM='" + textBox1.Text.Trim() + "'", "");
            if (dr.Length > 0)
            {
                label3.Text = "这个条码已经存在！";
                panel3.Visible = true;
                return;
            }

            string TM = textBox1.Text;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,IMGBOL,RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU "
                    + "FROM QGOODS WHERE (XSTAT=1) AND (TM='" + TM + "')", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    DataRow i = thisDT.NewRow();
                    i["TM"] = rs["TM"].ToString();
                    i["JLIAO"] = rs["JLIAO"].ToString();
                    i["SLIAO"] = rs["SLIAO"].ToString();
                    i["SSI"] = rs["SSI"].ToString();
                    i["QKOU"] = rs["QKOU"].ToString();
                    i["ZSHU"] = rs["ZSHU"].ToString();
                    i["KUS"] = rs["KUS"].ToString();
                    i["HHAO"] = rs["HHAO"].ToString();
                    i["DWEI"] = rs["DWEI"].ToString();
                    i["SLIANG"] = rs["SLIANG"].ToString();
                    i["JIANZ"] = rs["JIANZ"].ToString();
                    i["JINZ"] = rs["JINZ"].ToString();
                    i["PJIANZ"] = rs["PJIANZ"].ToString();
                    i["BLU"] = rs["BLU"].ToString();
                    i["ZSZ"] = rs["ZSZ"].ToString();
                    i["ZSS"] = rs["ZSS"].ToString();
                    i["FSZ"] = rs["FSZ"].ToString();
                    i["FSS"] = rs["FSS"].ToString();
                    i["CBEI"] = rs["CBEI"].ToString();
                    i["XSOU"] = rs["XSOU"].ToString();
                    i["IMGBOL"] = rs["IMGBOL"].ToString();
                    i["YSE"] = rs["YSE"].ToString();
                    i["XZUANG"] = rs["XZUANG"].ToString();
                    i["QGONG"] = rs["QGONG"].ToString();
                    i["JDU"] = rs["JDU"].ToString();
                    thisDT.Rows.Add(i);
                }
                else
                {
                    label3.Text = "没有找到这件商品！";
                    panel3.Visible = true;
                }
                rs.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                button1_Click(button1, new EventArgs());
            }
            else if (key == 8)
            {
                e.Handled = false;
            }
            else
            {
                Regex regex = new Regex(@"\d");
                if (!regex.Match(e.KeyChar.ToString()).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == string.Empty)
            {
                return;
            }

            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                i.Cells["yy"].Value = textBox2.Text;
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                bool imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                string TM = ((DataGridView)sender)["tmDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

                toolStripButton2.Enabled = imgbol;

                if (panel2.Visible)
                {
                    if (imgbol)
                    {
                        Image itemimg;
                        Bitmap itembmp;
                        itemimg = xconfig.netImgGET(xconfig.USER + "|库退|GETBMP|" + TM + "<EOF>");
                        itembmp = new Bitmap(itemimg, pictureBox1.Size);
                        pictureBox1.Image = itembmp;
                    }
                    else
                    {
                        pictureBox1.Image = Properties.Resources.ImgGet_Err;
                    }
                }
            }
            catch { }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            if (!panel2.Visible)
            {
                panel2.Visible = true;
            }

            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show("正在写入数据，请稍候.....");
            Application.DoEvents();

            try
            {
                GoldPrinter.ExcelAccess extshow = new GoldPrinter.ExcelAccess();
                extshow.Open(Application.StartupPath + @"\00I01.xlt");
                extshow.FormCaption = "ColorBay";

                extshow.SetCellText(1, "A", "条码");
                extshow.SetCellText(1, "B", "数量");
                extshow.SetCellText(1, "C", "门店/仓库");
                extshow.SetCellText(1, "D", "单号");
                extshow.SetCellText(1, "E", "证书号");
                extshow.SetCellText(1, "F", "金料");
                extshow.SetCellText(1, "G", "石料");
                extshow.SetCellText(1, "H", "首饰类别");
                extshow.SetCellText(1, "I", "品名");
                extshow.SetCellText(1, "J", "款号");
                extshow.SetCellText(1, "K", "成本");
                extshow.SetCellText(1, "L", "售价");
                extshow.SetCellText(1, "M", "供应商");
                extshow.SetCellText(1, "N", "日期");
                extshow.SetCellText(1, "O", "主石");
                extshow.SetCellText(1, "P", "辅石");
                extshow.SetCellText(1, "Q", "件重");
                extshow.SetCellText(1, "R", "金重");
                extshow.SetCellText(1, "S", "圈口");
                extshow.SetCellText(1, "T", "净度");
                extshow.SetCellText(1, "U", "颜色");
                extshow.SetCellText(1, "V", "库退原因");


                int j = 1;
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                SqlDataReader rs;
                foreach (DataRow i in thisDT.Rows)
                {
                    extshow.SetCellText(j + 1, "A", i["TM"].ToString());
                    //extshow.SetRowHeight(j + 1, 18.75f);
                    string tm = i["TM"].ToString();
                    extshow.SetCellText(j + 1, "B", i["SLIANG"].ToString());
                    cmd = new SqlCommand("SELECT RTRIM(NAME) AS NAME FROM QGOODS_CKU WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        extshow.SetCellText(j + 1, "C", rs["NAME"].ToString());
                    }
                    rs.Close();
                    cmd = new SqlCommand("SELECT RTRIM(DH) AS DH FROM QGOODS_RKD WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        extshow.SetCellText(j + 1, "D", rs["DH"].ToString());
                    }
                    rs.Close();
                    extshow.SetCellText(j + 1, "E", i["ZSHU"].ToString());
                    extshow.SetCellText(j + 1, "F", i["JLIAO"].ToString());
                    extshow.SetCellText(j + 1, "G", i["SLIAO"].ToString());
                    extshow.SetCellText(j + 1, "H", i["SSI"].ToString());
                    extshow.SetCellText(j + 1, "I", i["NAME"].ToString());
                    extshow.SetCellText(j + 1, "J", i["KUS"].ToString());
                    extshow.SetCellText(j + 1, "K", i["CBEI"].ToString());
                    extshow.SetCellText(j + 1, "L", i["XSOU"].ToString());
                    cmd = new SqlCommand("SELECT RTRIM(NAME) AS NAME FROM QGOODS_GYS_LIST WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        extshow.SetCellText(j + 1, "M", rs["NAME"].ToString());
                    }
                    rs.Close();
                    cmd = new SqlCommand("SELECT CONVERT(CHAR(10),SETTIME,120) AS SETTIME FROM QGOODS WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        extshow.SetCellText(j + 1, "N", rs["SETTIME"].ToString());
                    }
                    rs.Close();
                    extshow.SetCellText(j + 1, "O", i["ZSHI"].ToString());
                    extshow.SetCellText(j + 1, "P", i["FSHI"].ToString());
                    extshow.SetCellText(j + 1, "Q", i["JIANZ"].ToString());
                    extshow.SetCellText(j + 1, "R", i["JINZ"].ToString());
                    if (i["QKOU"].ToString() != "0")
                    {
                        extshow.SetCellText(j + 1, "S", i["QKOU"].ToString());
                    }
                    extshow.SetCellText(j + 1, "T", i["JDU"].ToString());
                    extshow.SetCellText(j + 1, "U", i["YSE"].ToString());
                    extshow.SetCellText(j + 1, "V", dataGridView1["yy", j - 1].Value.ToString());
                    j++;
                }
                conn.Close();
                extshow.ShowExcel();
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();
        }

        private void panel3_VisibleChanged(object sender, EventArgs e)
        {
            bool visbol = ((Panel)sender).Visible;
            if (visbol)
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            string SBM;
            IPAddress IP = IPAddress.Parse(xconfig.SERVERIP);
            int PORT = xconfig.SERVERPORT;
            mesk.xSocket mesk = new mesk.xSocket();
            SBM = mesk.Send(xconfig.USER + "|库退|GETTIMEDH<EOF>", IP, PORT);
            if (SBM == string.Empty || SBM == "ERROR")
            {
                MessageBox.Show("服务器连接出错！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                foreach (DataGridViewRow i in dataGridView1.Rows)
                {
                    string tm, user, yy, sbm,sliang;
                    tm = string.Empty;
                    user = string.Empty;
                    yy = string.Empty;
                    sbm = string.Empty;
                    sliang = "0";
                    tm = i.Cells["tmDataGridViewTextBoxColumn"].Value.ToString();
                    user = xconfig.USER;
                    try { yy = i.Cells["yy"].Value.ToString(); }
                    catch { yy = string.Empty; }
                    sbm = SBM;
                    sliang = i.Cells["sliangDataGridViewTextBoxColumn"].Value.ToString();

                    cmd = new SqlCommand("QZ_GOODS_KUT", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", tm));
                    cmd.Parameters.Add(new SqlParameter("@USER", user));
                    cmd.Parameters.Add(new SqlParameter("@YY", yy));
                    cmd.Parameters.Add(new SqlParameter("@SBM", sbm));
                    cmd.Parameters.Add(new SqlParameter("@SLIANG", sliang));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

                toolStripButton1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                dataGridView1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}