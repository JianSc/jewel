using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;

namespace Mdian.销售
{
    public partial class 销售 : Form
    {
        DataTable cliDST = new theDST().Tables["销售表"];
        ToolStripMenuItem panMENU;
        DataTable userDST = new DataTable();

        public 销售(ToolStripMenuItem menu)
        {
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM([NAME]) AS [NAME] FROM QYGONG WHERE (MDIAN='" + xconfig.MDIAN + "')";
            config.conData.fill("sql", constr, cmdstr, userDST);
            panMENU = menu;
            InitializeComponent();
        }

        private void 销售_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.Items.Add("");

            foreach (DataRow i in userDST.Rows)
            {
                toolStripComboBox1.Items.Add(i["NAME"].ToString());
            }
            this.WindowState = FormWindowState.Maximized;
            label1.Parent = panel1;
            dataGridView1.DataSource = cliDST;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 销售_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool change = ((CheckBox)sender).Checked;
            textBox4.Enabled = change;
            if (change) { textBox4.Focus(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Focus();
                return;
            }

            string TM;
            TM = textBox1.Text.Trim();

            DataRow[] dr = cliDST.Select("TM='" + TM + "'", "");
            if (dr.Length > 0)
            {
                MessageBox.Show("这个条码已存在！");
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,"
                    + "RTRIM(SSI) AS SSI,RTRIM(ZSHU) AS ZSHU,QKOU,RTRIM(KUS) AS KUS,JIANZ,"
                    + "JINZ,PJIANZ,ZSZ,ZSS,FSZ,FSS,XSOU,CBEI,IMGBOL,RTRIM(JDU) AS JDU,RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,"
                    + "RTRIM(QGONG) AS QGONG,ID,RTRIM(DWEI) AS DWEI,SLIANG FROM QGOODS INNER JOIN QGOODS_CKU ON "
                    + "QGOODS.TM=QGOODS_CKU.TM WHERE (QGOODS_CKU.TM='" + TM + "') AND (QGOODS_CKU.NAME='" + xconfig.MDIAN + "') AND (QGOODS.XSTAT=1)", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    DataRow newdr = cliDST.NewRow();
                    newdr["TM"] = rs["TM"].ToString();
                    //newdr["NAME"] = rs["jliao"].ToString() + rs["sliao"].ToString() + rs["ssi"].ToString();
                    newdr["QKOU"] = rs["qkou"].ToString();
                    newdr["zshu"] = rs["zshu"].ToString();
                    newdr["kus"] = rs["kus"].ToString();
                    newdr["jianz"] = rs["jianz"].ToString();
                    newdr["jinz"] = rs["jinz"].ToString();
                    newdr["pjianz"] = rs["pjianz"].ToString();
                    newdr["zshi"] = rs["zsz"].ToString() + "/" + rs["zss"].ToString();
                    newdr["fshi"] = rs["fsz"].ToString() + "/" + rs["fss"].ToString();
                    newdr["xsou"] = rs["xsou"].ToString();
                    newdr["imgbol"] = rs["imgbol"].ToString();
                    newdr["jdu"] = rs["jdu"].ToString();
                    newdr["yse"] = rs["yse"].ToString();
                    newdr["xzuang"] = rs["xzuang"].ToString();
                    newdr["qgong"] = rs["qgong"].ToString();
                    newdr["jliao"] = rs["jliao"].ToString();
                    newdr["sliao"] = rs["sliao"].ToString();
                    newdr["ssi"] = rs["ssi"].ToString();
                    newdr["sxsou"] = rs["xsou"].ToString();
                    newdr["sliang"] = rs["sliang"].ToString();
                    newdr["dwei"] = rs["dwei"].ToString();
                    newdr["cbei"] = rs["cbei"].ToString();
                    cliDST.Rows.Add(newdr);
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                else
                {
                    panel2.Visible = true;
                }
                conn.Close();

                string sxsou;
                sxsou = cliDST.Compute("sum(sxsou)", "").ToString();
                textBox2.Text = sxsou;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void panel2_VisibleChanged(object sender, EventArgs e)
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
            panel2.Visible = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                button1_Click(button1, new EventArgs());
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton1.Enabled = false;
        }

        public static void dataNUM(DataGridView dgr, int rowindex,int e)
        {
            try
            {
                DataGridViewRow i = dgr.Rows[rowindex];
                double xsou, sxsou, zk;
                xsou = double.Parse(i.Cells["dataGridViewTextBoxColumn11"].Value.ToString());
                sxsou = double.Parse(i.Cells["sxsou"].Value.ToString());
                zk = double.Parse(i.Cells["zk"].Value.ToString());
                if (e == 8)
                {
                    if (zk < xconfig.MYZK)
                    {
                        i.Cells["sxsou"].Value = xsou.ToString();
                        i.Cells["zk"].Value = "1";
                    }
                    else
                    {
                        sxsou = (double)((int)(xsou * zk));
                        i.Cells["sxsou"].Value = sxsou.ToString();
                    }
                }
                else if (e == 9)
                {
                    zk = sxsou / xsou;
                    int zk1 = (int)(zk * 100);
                    zk = zk1 / 100.00;
                    if (zk < xconfig.MYZK)
                    {
                        i.Cells["sxsou"].Value = xsou.ToString();
                        i.Cells["zk"].Value = "1";
                    }
                    else
                    {
                        i.Cells["zk"].Value = zk.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataNUM(dataGridView1, dataGridView1.CurrentCell.RowIndex, e.ColumnIndex);
            toolStripButton1.Enabled = true;

            string sxsou;
            sxsou = cliDST.Compute("sum(sxsou)", "").ToString();
            textBox2.Text = sxsou;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "1.0";
            }

            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            textBox3.BackColor = Color.White;

            double myzk ;
            myzk = double.Parse(textBox3.Text);
            if(myzk < xconfig.MYZK)
            {
                textBox3.BackColor =Color.MistyRose;
                return;
            }

            int j=0;
            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                i.Cells["zk"].Value = myzk.ToString();
                dataNUM(dataGridView1, j, 8);
                j++;
            }

            string sxsou;
            sxsou = cliDST.Compute("sum(sxsou)", "").ToString();
            textBox2.Text = sxsou;

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keystr = e.KeyChar.ToString();
            Regex regex = new Regex(@"\d|\.");
            if (!regex.Match(keystr).Success)
            {
                e.Handled = true;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            string str = ((TextBox)sender).Text;
            Regex regex = new Regex(@"^\d$|^\d\.\d{1,2}$");
            if (!regex.Match(str).Success)
            {
                ((TextBox)sender).Text = "1.0";
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (cliDST.Rows.Count < 1)
            {
                return;
            }
            else if (cliDST.Rows.Count > 7)
            {
                MessageBox.Show("套打格式:\n每个销售单商品不能超过6件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (toolStripComboBox1.Text == string.Empty && toolStripComboBox1.Visible )
            {
                toolStripComboBox1.DroppedDown = true;
                return;
            }

            if (checkBox1.Checked)
            {
                if (textBox4.Text == "")
                {
                    textBox4.Focus();
                    return;
                }
            }

            string msgstr = xconfig.USER + "|销售开单|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请与检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string DH;
            mesk.xSocket mesk = new mesk.xSocket();
            IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
            int port = xconfig.SERVERPORT;
            msgstr = xconfig.USER + "|销售开单|GETTIMEDH<EOF>";
            DH = mesk.Send(msgstr, ip, port);
            if (DH == "ERROR")
            {
                MessageBox.Show("服务器连接不正常！\n请与检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string kehu = string.Empty;
            int Yhui = 0;
            try
            {
                if (textBox5.Text == string.Empty)
                {
                    Yhui = 0;
                }
                else
                {
                    Yhui = int.Parse(textBox5.Text);
                }
            }
            catch
            {
                Yhui = 0;
            }
            double HZK = 1;
            try
            {
                if (textBox6.Text == string.Empty)
                {
                    HZK = 1;
                }
                else
                {
                    HZK = double.Parse(textBox6.Text);
                }
            }
            catch
            {
                HZK = 1;
            }

            if (!checkBox1.Checked)
            {
                kehu = "普通客户";
                string khu = string.Empty;

                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                cmd = new SqlCommand("QZ_YHUI", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@KHUID", khu));
                cmd.Parameters.Add(new SqlParameter("@SBM", DH));
                cmd.Parameters.Add(new SqlParameter("@JF", Yhui));
                cmd.Parameters.Add(new SqlParameter("@MDIAN", xconfig.MDIAN));
                cmd.Parameters.Add(new SqlParameter("@ZK", HZK));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                string khu;
                khu = textBox4.Text.Trim();
                bool khubol;
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(KHUHAO) AS KHUHAO FROM QKHU WHERE (KHUHAO='" + khu + "')", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    khubol = true;
                }
                else
                {
                    khubol = false;
                }
                rs.Close();

                cmd = new SqlCommand("QZ_YHUI", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@KHUID", khu));
                cmd.Parameters.Add(new SqlParameter("@SBM", DH));
                cmd.Parameters.Add(new SqlParameter("@JF", Yhui));
                cmd.Parameters.Add(new SqlParameter("@MDIAN", xconfig.MDIAN));
                cmd.Parameters.Add(new SqlParameter("@ZK", HZK));
                cmd.ExecuteNonQuery();

                if (khubol)
                {
                    cmd = new SqlCommand("QZ_KHU_JF", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@KHAO", khu));
                    cmd.Parameters.Add(new SqlParameter("@JF", textBox2.Text));
                    cmd.Parameters.Add(new SqlParameter("@MDIAN", xconfig.MDIAN));
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    conn.Close();
                    new 客户资料添加(khu).ShowDialog();
                    return;
                }

                //if(

                conn.Close();

                kehu = khu;

            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                foreach (DataRow i in cliDST.Rows)
                {
                    string TM, CBEI, SALE, SSALE, MDIANs, USER, KHU, ZKOU, SBM,DWEI,SLIANG;
                    TM = i["tm"].ToString();
                    CBEI = i["cbei"].ToString();
                    SALE = i["xsou"].ToString();
                    SSALE = i["sxsou"].ToString();
                    MDIANs = xconfig.MDIAN;
                    if (toolStripComboBox1.Visible)
                    {
                        USER = toolStripComboBox1.Text;
                    }
                    else
                    {
                        USER = xconfig.USER;
                    }
                    KHU = kehu;
                    ZKOU = i["zk"].ToString();
                    DWEI = i["dwei"].ToString();
                    SLIANG = i["sliang"].ToString();
                    SBM = DH;
                    cmd = new SqlCommand("QZ_SALES", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", TM));
                    cmd.Parameters.Add(new SqlParameter("@CBEI", CBEI));
                    cmd.Parameters.Add(new SqlParameter("@SALE", SALE));
                    cmd.Parameters.Add(new SqlParameter("@SSALE", SSALE));
                    cmd.Parameters.Add(new SqlParameter("@MDIAN", MDIANs));
                    cmd.Parameters.Add(new SqlParameter("@USER", USER));
                    cmd.Parameters.Add(new SqlParameter("@KHU", KHU));
                    cmd.Parameters.Add(new SqlParameter("@ZKOU", ZKOU));
                    cmd.Parameters.Add(new SqlParameter("@SBM", SBM));
                    cmd.Parameters.Add(new SqlParameter("@DWEI", DWEI));
                    cmd.Parameters.Add(new SqlParameter("@SLIANG", SLIANG));
                    cmd.Parameters.Add(new SqlParameter("@DQUSEN", xconfig.DQUSEN));
                    cmd.Parameters.Add(new SqlParameter("@DQUSI", xconfig.DQUSI));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            toolStripTextBox1.Text = DH;
            textBox4.Enabled = false;
            toolStripButton1.Enabled = false;
            dataGridView1.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = false;
            checkBox1.Enabled = false;
            toolStripButton3.Enabled = true;
            toolStripButton5.Enabled = false;
            toolStripButton8.Enabled = false;
            toolStripButton6.Enabled = false;
            toolStripComboBox1.Enabled = false;

            MessageBox.Show("销售成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyint = xconfig.ASC(e.KeyChar.ToString());
            if (keyint != 8)
            {
                Regex regex = new Regex(@"\d");
                string str = e.KeyChar.ToString();
                if (!regex.Match(str).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                bool imgbol;
                imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                toolStripButton4.Enabled = imgbol;
                string TM = ((DataGridView)sender)["dataGridViewTextBoxColumn1", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

                if (panel3.Visible)
                {
                    xconfig.GetIMG(xconfig.USER + "|销售|GETBMP|" + TM + "<EOF>", pictureBox1, imgbol, TM, tmLabel, jdLabel);
                }
            }
            catch { }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!panel3.Visible)
            {
                panel3.Visible = true;
            }

            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (cliDST.Rows.Count < 1)
            {
                return;
            }

            string TM = dataGridView1["dataGridViewTextBoxColumn1", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            DataRow[] dr = cliDST.Select("TM='" + TM + "'", "");
            foreach (DataRow i in dr)
            {
                cliDST.Rows.Remove(i);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            MSG.login.ShowIMG();
            Application.DoEvents();
            toolStripButton3.Enabled = false;

            Thread t = new Thread(new ThreadStart(print));
            t.Start();
        }

        private delegate void d();
        private void print()
        {
            try
            {
                GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                ext.Open(Application.StartupPath + @"\00X03.xlt");
                ext.FormCaption = "Colorbay";

                int l1 = 12;
                int l2 = 30;
                int l3 = 47;

                string users = string.Empty;
                string dh = toolStripTextBox1.Text;
                if (toolStripComboBox1.Visible)
                {
                    users = toolStripComboBox1.Text;
                }
                else
                {
                    users = xconfig.USER;
                }

                ext.SetCellText(6, "C", xconfig.MDIAN);
                ext.SetCellText(7, "C", xconfig.TEL);
                ext.SetCellText(8, "C", xconfig.DZHI);
                ext.SetCellText(6, "G", dh);
                ext.SetCellText(7, "G", DateTime.Now.ToShortDateString());
                ext.SetCellText(8, "G", users);

                ext.SetCellText(25, "C", xconfig.MDIAN);
                ext.SetCellText(26, "C", xconfig.TEL);
                ext.SetCellText(27, "C", xconfig.DZHI);
                ext.SetCellText(25, "G", dh);
                ext.SetCellText(26, "G", DateTime.Now.ToShortDateString());
                ext.SetCellText(27, "G", users);

                ext.SetCellText(42, "C", xconfig.MDIAN);
                ext.SetCellText(43, "C", xconfig.TEL);
                ext.SetCellText(44, "C", xconfig.DZHI);
                ext.SetCellText(42, "G", dh);
                ext.SetCellText(43, "G", DateTime.Now.ToShortDateString());
                ext.SetCellText(44, "G", users);

                foreach (DataRow i in cliDST.Rows)
                {
                    if (i["TM"].ToString() == string.Empty)
                    {
                        continue;
                    }

                    ext.SetCellText(l1, "B", i["TM"].ToString());
                    ext.SetCellText(l1, "D", i["NAME"].ToString());
                    ext.SetCellText(l1, "E", i["SLIANG"].ToString());
                    ext.SetCellText(l1, "F", i["SXSOU"].ToString());
                    ext.SetCellText(l2, "B", i["TM"].ToString());
                    ext.SetCellText(l2, "D", i["NAME"].ToString());
                    ext.SetCellText(l2, "E", i["SLIANG"].ToString());
                    ext.SetCellText(l2, "F", i["SXSOU"].ToString());
                    ext.SetCellText(l3, "B", i["TM"].ToString());
                    ext.SetCellText(l3, "D", i["NAME"].ToString());
                    ext.SetCellText(l3, "E", i["SLIANG"].ToString());
                    ext.SetCellText(l3, "F", i["SXSOU"].ToString());
                    l1++;
                    l2++;
                    l3++;
                }

                string HJ;
                HJ = cliDST.Compute("SUM(SXSOU)", "TM<>''").ToString();
                double H1, H2, H3;
                try
                {
                    H1 = double.Parse(textBox5.Text);
                }
                catch { H1 = 0; }
                try
                {
                    H2 = double.Parse(textBox6.Text);
                }
                catch { H2 = 0; }
                try
                {
                    H3 = double.Parse(HJ);
                }
                catch { H3 = 0; }
                double H4 = (H3 - H1) * H2;
                int H5 = int.Parse(H4.ToString());

                ext.SetCellText(21, "G", H5.ToString());
                ext.SetCellText(38, "G", H5.ToString());
                ext.SetCellText(55, "G", H5.ToString());

                string msg = string.Empty;
                if (textBox5.Text != string.Empty)
                {
                    msg += "会员优惠:" + textBox5.Text + "元";
                }
                if (textBox5.Text != string.Empty && textBox6.Text != string.Empty)
                {
                    msg += "|";
                }
                if (textBox6.Text != string.Empty && textBox6.Text != "1")
                {
                    msg += "会员折扣:" + textBox6.Text;
                }

                ext.SetCellText(19, "B", msg);
                ext.SetCellText(37, "B", msg);
                ext.SetCellText(54, "B", msg);

                MSG.login.CloseIMG();
                Application.DoEvents();
                ext.Print();

                if (this.InvokeRequired)
                {
                    d d = delegate
                    {
                        toolStripButton3.Enabled = true;
                    };
                    this.Invoke(d);
                }
                else
                {
                    toolStripButton3.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|销售开单|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msg);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请与检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!checkBox1.Checked)
            {
                //MessageBox.Show("请开启客户参与积分功能,并填入正确的客户卡号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkBox1.Checked && textBox4.Text == string.Empty)
            {
                //textBox4.Focus();
                return;
            }

            string jf = textBox4.Text.Trim();

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT KH FROM QGOODS_CUSTJF WHERE KH='" + jf + "'", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (!rs.Read())
                {
                    MessageBox.Show("这个客户以前没有参与过积分！\n系统中找不到积分信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MSG.login.Show();
                    Application.DoEvents();

                    Form newform = new 积分查看(jf, textBox5,checkBox1,textBox4);
                    newform.ShowDialog();
                }
                rs.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Form newform = new 销售(panMENU);
            newform.MdiParent = this.MdiParent;
            newform.Show();
            this.Close();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|销售开单|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msg);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路，或联系系统管理员。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new insertCard(textBox4, textBox6, checkBox1).ShowDialog();
        }
    }
}
