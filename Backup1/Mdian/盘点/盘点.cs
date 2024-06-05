using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Mdian.盘点
{
    public partial class 盘点 : Form
    {
        ToolStripMenuItem panMenu;
        DataTable thisDT = new theDST().Tables["goods"];

        public 盘点(ToolStripMenuItem menu)
        {
            panMenu = menu;
            clidstfill();
            InitializeComponent();
        }

        private void clidstfill()
        {
            string constr, cmdstr;
            constr = xconfig.oldstr("make", "cnsdjian");
            cmdstr = "SELECT RTRIM(TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,"
            + "RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM GOODS";
            config.conData.fill("acc", constr, cmdstr, thisDT);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            if (!netMSGconnect())
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("即将执行清空盘点表的任务！\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                try
                {
                    OleDbConnection conn = new OleDbConnection(xconfig.oldstr("make", "cnsdjian"));
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand("DELETE FROM GOODS", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    clidstfill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void 盘点_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = thisDT;
            config.conDGV.DGVAutoID(dataGridView1, "phao");
        }

        private void 盘点_FormClosed(object sender, FormClosedEventArgs e)
        {
            string msgstr = xconfig.USER + "|闲置|CONNECT<EOF>";
            xconfig.netSend(msgstr);

            panMenu.Enabled = true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!panel2.Visible)
            {
                panel2.Visible = true;
            }
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                button1_Click(button1, new EventArgs());
            }
        }

        private bool netMSGconnect()
        {
            string msgstr = xconfig.USER + "|库存盘点|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            return msgbol;
        }

        private bool netMSGconnect(string msg)
        {
            string msgstr = msg;
            bool msgbol = xconfig.netSend(msgstr);
            return msgbol;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Focus();
                return;
            }

            if (!netMSGconnect())
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string itemTM;
            itemTM = textBox1.Text;
            DataRow[] dr = thisDT.Select("TM='" + itemTM + "'", "");
            if (dr.Length > 0)
            {
                panel3.Visible = true;
                textBox1.Focus();
                textBox1.SelectAll();
                return;
            }

            string TM, JLIAO, SLIAO, SSI, QKOU, ZSHU, KUS, HHAO, DWEI, SLIANG, JIANZ, JINZ, PJIANZ, BLU, ZSZ, ZSS, FSZ, FSS, CBEI, XSOU, BZHU, SLBOL, IMGBOL, YSE, XZUANG, QGONG, JDU;
            TM = string.Empty;
            JLIAO = string.Empty;
            SLIAO = string.Empty;
            SSI = string.Empty;
            QKOU = "0";
            ZSHU = string.Empty;
            KUS = string.Empty;
            HHAO = string.Empty;
            DWEI = string.Empty;
            SLIANG = "0";
            JIANZ = "0";
            JINZ = "0";
            PJIANZ = "0";
            BLU = "1";
            ZSZ = "0";
            ZSS = "0";
            FSZ = "0";
            FSS = "0";
            CBEI = "0";
            XSOU = "0";
            BZHU = string.Empty;
            SLBOL = "false";
            IMGBOL = "false";
            YSE = string.Empty;
            XZUANG = string.Empty;
            QGONG = string.Empty;
            JDU = string.Empty;

            bool RSBOL = false;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,"
                + "RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM QGOODS WHERE TM='" + textBox1.Text.Trim() + "'", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    TM = rs["TM"].ToString();
                    JLIAO = rs["JLIAO"].ToString();
                    SLIAO = rs["SLIAO"].ToString();
                    SSI = rs["SSI"].ToString();
                    QKOU = rs["QKOU"].ToString();
                    ZSHU = rs["ZSHU"].ToString();
                    KUS = rs["KUS"].ToString();
                    HHAO = rs["HHAO"].ToString();
                    DWEI = rs["DWEI"].ToString();
                    SLIANG = rs["SLIANG"].ToString();
                    JIANZ = rs["JIANZ"].ToString();
                    JINZ = rs["JINZ"].ToString();
                    PJIANZ = rs["PJIANZ"].ToString();
                    BLU = rs["BLU"].ToString();
                    ZSZ = rs["ZSZ"].ToString();
                    ZSS = rs["ZSS"].ToString();
                    FSZ = rs["FSZ"].ToString();
                    FSS = rs["FSS"].ToString();
                    CBEI = rs["CBEI"].ToString();
                    XSOU = rs["XSOU"].ToString();
                    BZHU = rs["BZHU"].ToString();
                    SLBOL = rs["SLBOL"].ToString();
                    IMGBOL = rs["IMGBOL"].ToString();
                    YSE = rs["YSE"].ToString();
                    XZUANG = rs["XZUANG"].ToString();
                    QGONG = rs["QGONG"].ToString();
                    JDU = rs["JDU"].ToString();
                    RSBOL = true;
                }
                else
                {
                    TM = textBox1.Text;
                    RSBOL = false;
                }
                rs.Close();
                conn.Close();

                if (RSBOL)
                {
                    try
                    {
                        OleDbConnection xconn = new OleDbConnection(xconfig.oldstr("make", "cnsdjian"));
                        xconn.Open();
                        OleDbCommand xcmd = new OleDbCommand("INSERT INTO GOODS("
                            + "TM,JLIAO,SLIAO,SSI,QKOU,ZSHU,KUS,HHAO,DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,YSE,XZUANG,QGONG,JDU"
                            + ")VALUES('"
                            + TM + "','" + JLIAO + "','" + SLIAO + "','" + SSI + "'," + QKOU + ",'" + ZSHU + "','" + KUS + "','" + HHAO + "','" + DWEI + "',"
                            + SLIANG + "," + JINZ + "," + JIANZ + "," + PJIANZ + "," + BLU + "," + ZSZ + "," + ZSS + "," + FSZ + "," + FSS + "," + CBEI + "," + XSOU + ",'" + BZHU + "'," + SLBOL + "," + IMGBOL + ",'"
                            + YSE + "','" + XZUANG + "','" + QGONG + "','" + JDU + "')", xconn);
                        xcmd.ExecuteNonQuery();
                        xconn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    OleDbConnection xxconn = new OleDbConnection(xconfig.oldstr("make", "cnsdjian"));
                    xxconn.Open();
                    OleDbCommand xxcmd = new OleDbCommand("INSERT INTO GOODS(TM)VALUES('" + TM + "')", xxconn);
                    xxcmd.ExecuteNonQuery();
                    xxconn.Close();
                }

                string itemcon, itemcmd;
                itemcon = xconfig.oldstr("make", "cnsdjian");
                itemcmd = "SELECT RTRIM(TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,"
                + "RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM GOODS";
                config.conData.fill("acc", itemcon, itemcmd, thisDT);
                config.conDGV.DGVAutoID(dataGridView1, "phao");

                textBox1.Text = "";
                textBox1.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            
            if(thisDT.Rows.Count <1)
            {
                return;
            }

            //if (!netMSGconnect())
            //{
            //    MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            MSG.login.Show();
            Application.DoEvents();

            try
            {
                GoldPrinter.ExcelAccess extshow = new GoldPrinter.ExcelAccess();
                extshow.Open(Application.StartupPath + @"\00I01.xlt");
                extshow.FormCaption = "ColorBay";

                extshow.SetCellText(1, "A", "TM");
                extshow.SetCellText(1, "B", "数量");
                extshow.SetCellText(1, "C", "证书号");
                extshow.SetCellText(1, "D", "金料");
                extshow.SetCellText(1, "E", "石料");
                extshow.SetCellText(1, "F", "首饰类别");
                extshow.SetCellText(1, "G", "品名");
                extshow.SetCellText(1, "H", "款号");
                extshow.SetCellText(1, "I", "售价");
                extshow.SetCellText(1, "J", "主石");
                extshow.SetCellText(1, "K", "辅石");
                extshow.SetCellText(1, "L", "件重");
                extshow.SetCellText(1, "M", "金重");
                extshow.SetCellText(1, "N", "圈口");
                extshow.SetCellText(1, "O", "净度");
                extshow.SetCellText(1, "P", "颜色");

                int j = 1;
                foreach (DataRow i in thisDT.Rows)
                {
                    extshow.SetCellText(j + 1, "A", i["TM"].ToString());
                    string tm = i["TM"].ToString();
                    extshow.SetCellText(j + 1, "B", i["SLIANG"].ToString());
                    extshow.SetCellText(j + 1, "C", i["ZSHU"].ToString());
                    extshow.SetCellText(j + 1, "D", i["JLIAO"].ToString());
                    extshow.SetCellText(j + 1, "E", i["SLIAO"].ToString());
                    extshow.SetCellText(j + 1, "F", i["SSI"].ToString());
                    extshow.SetCellText(j + 1, "G", i["NAME"].ToString());
                    extshow.SetCellText(j + 1, "H", i["KUS"].ToString());
                    extshow.SetCellText(j + 1, "I", i["XSOU"].ToString());
                    extshow.SetCellText(j + 1, "J", i["ZSHI"].ToString());
                    extshow.SetCellText(j + 1, "K", i["FSHI"].ToString());
                    extshow.SetCellText(j + 1, "L", i["JIANZ"].ToString());
                    extshow.SetCellText(j + 1, "M", i["JINZ"].ToString());
                    if (i["QKOU"].ToString() != "0")
                    {
                        extshow.SetCellText(j + 1, "N", i["QKOU"].ToString());
                    }
                    extshow.SetCellText(j + 1, "O", i["JDU"].ToString());
                    extshow.SetCellText(j + 1, "P", i["YSE"].ToString());
                    j++;
                }
                extshow.ShowExcel();
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            string msgstr = xconfig.USER + "|库存盘点|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MSG.login.Close();
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string panCKU = xconfig.MDIAN;
            try
            {
                DataTable itemDST = new theDST().Tables["goods"];
                DataTable thisDST = new theDST().Tables["goods"];
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,"
                + "RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM QGOODS INNER JOIN QGOODS_CKU ON QGOODS.TM=QGOODS_CKU.TM WHERE (QGOODS_CKU.NAME='" + panCKU + "') AND (QGOODS.XSTAT=1)", conn);
                da.Fill(itemDST);
                conn.Close();

                foreach (DataRow i in thisDT.Rows)
                {
                    DataRow[] dr = itemDST.Select("TM='" + i["TM"].ToString() + "'", "");
                    if (dr.Length < 1)
                    {
                        thisDST.ImportRow(i);
                    }
                }

                Form newform = new 盘盈表(thisDST,panCKU);
                newform.MdiParent = this.MdiParent;
                newform.Show();

            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }
            MSG.login.Close();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            string msgstr = xconfig.USER + "|门店盘点|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MSG.login.Close();
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string panCKU = xconfig.MDIAN;
            try
            {
                DataTable itemDST = new theDST().Tables["goods"];
                DataTable thisDST = new theDST().Tables["goods"];
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,"
                + "RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM QGOODS INNER JOIN QGOODS_CKU ON QGOODS.TM=QGOODS_CKU.TM WHERE (QGOODS_CKU.NAME='" + panCKU + "') AND (QGOODS.XSTAT=1)", conn);
                da.Fill(itemDST);
                conn.Close();

                foreach (DataRow i in itemDST.Rows)
                {
                    DataRow[] dr = thisDT.Select("TM='" + i["TM"].ToString() + "'", "");
                    if (dr.Length < 1)
                    {
                        thisDST.ImportRow(i);
                    }
                }

                Form newform = new 盘亏表(thisDST, panCKU);
                newform.MdiParent = this.MdiParent;
                newform.Show();

            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }
            MSG.login.Close();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).Rows.Count < 1) { return; }

            try
            {
                bool imgbol = bool.Parse(((DataGridView)sender)["dataGridViewCheckBoxColumn2", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                string TM = ((DataGridView)sender)["dataGridViewTextBoxColumn2", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

                if (TM == "") { return; }

                toolStripButton4.Enabled = imgbol;

                if (panel2.Visible)
                {
                    if (imgbol)
                    {
                        Image itemimg;
                        Bitmap itembmp;
                        itemimg = xconfig.netImgGET(xconfig.USER + "|库存盘点|GETBMP|" + TM + "<EOF>");
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            if (MessageBox.Show("即将执行删除操作！\n是否继续？\n\n(此操作删除单条数据，如需清空请点击[新建盘点]。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            if (!netMSGconnect())
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string TM;
            TM = dataGridView1["dataGridViewTextBoxColumn2", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (TM == "") { return; }

            try
            {
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("make", "cnsdjian"));
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("DELETE FROM GOODS WHERE (TM='" + TM + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                DataRow[] dr = thisDT.Select("TM='" + TM + "'", "");
                foreach (DataRow i in dr) { thisDT.Rows.Remove(i); };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!netMSGconnect())
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.DefaultExt = "xls";
                openfile.Filter = "Microsoft Excel files (*.xls)|*.xls";
                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    string fileurl = openfile.FileName;
                    DataTable newDT = new DataTable();
                    newDT.TableName = "ELS";

                    string elsconstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileurl + ";Extended Properties='Excel 8.0;'";
                    OleDbConnection elscon = new OleDbConnection(elsconstr);
                    elscon.Open();
                    OleDbDataAdapter elsda = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", elscon);
                    elsda.Fill(newDT);
                    elscon.Close();

                    int addrow = 0;

                    OleDbConnection xconn = new OleDbConnection(xconfig.oldstr("make", "cnsdjian"));
                    xconn.Open();
                    OleDbCommand xcmd;
                    foreach (DataRow i in newDT.Rows)
                    {
                        string TM;
                        TM = i["TM"].ToString();
                        DataRow[] dr = thisDT.Select("TM='" + TM + "'", "");
                        if (dr.Length > 0)
                        {
                            addrow++;
                            MessageBox.Show("条码为 [" + TM + "] 的商品已存在！\n系统不允许存在相同条码的商品存在，请认真核查！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                        xcmd = new OleDbCommand("INSERT INTO GOODS(TM)VALUES('" + TM + "')", xconn);
                        xcmd.ExecuteNonQuery();
                    }
                    xconn.Close();

                    string constr, cmdstr;
                    constr = xconfig.oldstr("make", "cnsdjian");
                    cmdstr = "SELECT RTRIM(TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,"
                    + "RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM GOODS";
                    config.conData.fill("acc", constr, cmdstr, thisDT);
                    config.conDGV.DGVAutoID(dataGridView1, "phao");

                    int newaddorw = newDT.Rows.Count-addrow;
                    MessageBox.Show("数据导入完毕！\n\n总计：[" + newDT.Rows.Count.ToString() + "]条记录，成功导入：[" + newaddorw.ToString() + "]条记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                OleDbConnection oleconn = new OleDbConnection(xconfig.oldstr("make", "cnsdjian"));
                oleconn.Open();
                OleDbCommand olecmd;
                foreach (DataRow i in thisDT.Rows)
                {
                    if (i["NAME"].ToString() == "")
                    {
                        string TM = i["TM"].ToString();
                        cmd = new SqlCommand("SELECT RTRIM(TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,"
                    + "RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM QGOODS WHERE (TM='" + TM + "')", conn);
                        SqlDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            olecmd = new OleDbCommand("UPDATE GOODS SET JLIAO='" + rs["JLIAO"].ToString()
                            + "',SLIAO='" + rs["SLIAO"].ToString() + "',SSI='" + rs["SSI"].ToString() + "',QKOU='" + rs["QKOU"].ToString() + "',ZSHU='" + rs["ZSHU"].ToString() + "',KUS='" + rs["KUS"].ToString() + "',HHAO='" + rs["HHAO"].ToString() + "',DWEI='" + rs["DWEI"].ToString() + "',SLIANG='" + rs["SLIANG"].ToString() + "',JIANZ='" + rs["JIANZ"].ToString()
                            + "',JINZ='" + rs["JINZ"].ToString() + "',PJIANZ='" + rs["PJIANZ"].ToString() + "',BLU='" + rs["BLU"].ToString() + "',ZSZ='" + rs["ZSZ"].ToString() + "',ZSS='" + rs["ZSS"].ToString() + "',FSZ='" + rs["FSZ"].ToString() + "',FSS='" + rs["FSS"].ToString() + "',CBEI='" + rs["CBEI"].ToString() + "',XSOU='" + rs["XSOU"].ToString() + "',BZHU='" + rs["BZHU"].ToString()
                            + "',SLBOL=" + bool.Parse(rs["SLBOL"].ToString()) + ",IMGBOL=" + bool.Parse(rs["IMGBOL"].ToString()) + ",YSE='" + rs["YSE"].ToString()
                            + "',XZUANG='" + rs["XZUANG"].ToString() + "',QGONG='" + rs["QGONG"].ToString() + "',JDU='" + rs["JDU"].ToString() + "' WHERE (TM='" + TM + "')", oleconn);
                            olecmd.ExecuteNonQuery();
                        }
                        rs.Close();
                    }
                }

                oleconn.Close();
                conn.Close();

                string constr, cmdstr;
                constr = xconfig.oldstr("make", "cnsdjian");
                cmdstr = "SELECT RTRIM(TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,"
                + "RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM GOODS";
                config.conData.fill("acc", constr, cmdstr, thisDT);
                //dataGridView1.DataSource = cliDST;
                config.conDGV.DGVAutoID(dataGridView1, "phao");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

    }
}