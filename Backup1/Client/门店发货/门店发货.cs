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

namespace Client.门店发货
{
    public partial class 门店发货 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable theDST = new clidata().Tables["goods"];
        DataGridView theDGV;
        string extFilename = string.Empty;

        public 门店发货(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        private void 门店发货_Load(object sender, EventArgs e)
        {
            theDGV = dataGridView1;
            theDGV.DataSource = theDST;
            this.WindowState = FormWindowState.Maximized;

            DataTable itemdt = new clidata().Tables["门店"];
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(NAME) AS NAME,ID FROM QMDIAN";
            config.conData.fill("sql", constr, cmdstr, itemdt);

            foreach (DataRow i in itemdt.Rows)
            {
                comboBox1.Items.Add(i["name"].ToString());
            }

            string CKDH;
            mesk.xSocket mesk = new mesk.xSocket();
            IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
            int port = xconfig.SERVERPORT;
            string msg = xconfig.USER + "|门店发货|GETCODECKD<EOF>";
            CKDH = mesk.Send(msg, ip, port);

            textBox1.Text = CKDH;
        }

        private void 门店发货_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;

            string msgstr = xconfig.USER + "|闲置|CONNECT<EOF>";
            xconfig.netSend(msgstr);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Enabled = false;
            panel2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tm = textBox2.Text;
            if (tm == "")
            {
                textBox2.Focus();
                return;
            }

            string msgstr = xconfig.USER + "|门店发货|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow[] itemdr = theDST.Select("tm='" + tm + "'", "");
            if (itemdr.Length > 0)
            {
                MessageBox.Show("条码已存在！");
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(TM) AS TM,RTRIM(JLIAO) AS JLIAO,"
                    + "RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,"
                    + "RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,"
                    + "CBEI,XSOU,BZHU,SLBOL,IMGBOL,RTRIM(JDU) AS JDU,RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,"
                    + "RTRIM(QGONG) AS QGONG,RTRIM(CADI) AS CADI,ID FROM QGOODS WHERE (TM='" + tm + "') AND (XSTAT=1)", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    DataRow thedr = theDST.NewRow();
                    thedr["tm"] = rs["tm"].ToString();
                    thedr["jliao"] = rs["jliao"].ToString();
                    thedr["sliao"] = rs["sliao"].ToString();
                    if (rs["qkou"].ToString() != "0") { thedr["qkou"] = rs["qkou"].ToString(); }
                    thedr["kus"] = rs["kus"].ToString();
                    thedr["ssi"] = rs["ssi"].ToString();
                    thedr["dwei"] = rs["dwei"].ToString();
                    thedr["sliang"] = rs["sliang"].ToString();
                    thedr["jianz"] = rs["jianz"].ToString();
                    thedr["jinz"] = rs["jinz"].ToString();
                    thedr["pjianz"] = rs["pjianz"].ToString();
                    thedr["blu"] = rs["blu"].ToString();
                    thedr["zsz"] = rs["zsz"].ToString();
                    thedr["zss"] = rs["zss"].ToString();
                    thedr["fsz"] = rs["fsz"].ToString();
                    thedr["fss"] = rs["fss"].ToString();
                    thedr["cbei"] = rs["cbei"].ToString();
                    thedr["xsou"] = rs["xsou"].ToString();
                    thedr["bzhu"] = rs["bzhu"].ToString();
                    thedr["slbol"] = rs["slbol"].ToString();
                    thedr["imgbol"] = rs["imgbol"].ToString();
                    thedr["jdu"] = rs["jdu"].ToString();
                    thedr["yse"] = rs["yse"].ToString();
                    thedr["xzuang"] = rs["xzuang"].ToString();
                    thedr["qgong"] = rs["qgong"].ToString();
                    //thedr["jies"] = rs["jies"].ToString();
                    //thedr["cadi"] = rs["cadi"].ToString();
                    theDST.Rows.Add(thedr);
                    textBox2.Text = "";
                }
                else
                {
                    textBox2.Text = "";
                    timer1.Enabled = false;
                    timer1.Enabled = true;
                    panel2.Visible = true;
                }
                rs.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                bool imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                toolStripButton5.Enabled = imgbol;
                if (panel3.Visible)
                {
                    toolStripButton5_Click(toolStripButton5, new EventArgs());
                }
            }
            catch { }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (theDST.Rows.Count < 1)
            {
                return;
            }
            bool imgbol = bool.Parse(dataGridView1["imgbolDataGridViewCheckBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString());
            if (!imgbol)
            {
                return;
            }

            panel3.Visible = true;
            string tm = dataGridView1["tmDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            string msgstr = xconfig.USER + "|门店发货|GETBMP|" + tm + "<EOF>";
            Image itemimg = xconfig.netImgGET(msgstr);
            Bitmap itembmp = new Bitmap(itemimg, pictureBox1.Size);
            pictureBox1.Image = itembmp;

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            toolStripButton5.Enabled = false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                button1_Click(button1, new EventArgs());
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (theDST.Rows.Count < 1)
            {
                return;
            }

            string tm = dataGridView1["tmDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            DataRow[] dr = theDST.Select("TM='" + tm + "'", "");
            theDST.Rows.Remove(dr[0]);
        }

        string privetMDIAN;

        private void toolStripButton1_Click(object sender, EventArgs e)
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

            toolStripButton1.Enabled = false;

            bool msgbol;
            string msgstr = xconfig.USER + "|门店发货|CONNECT<EOF>";
            msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripButton1.Enabled = true;
                return;
            }

            string DH;
            mesk.xSocket mesk = new mesk.xSocket();
            IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
            int port = xconfig.SERVERPORT;
            string msg = xconfig.USER + "|门店发货|GETCODECKD|GET<EOF>";
            DH = mesk.Send(msg, ip, port);
            if (DH == "ERROR")
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripButton1.Enabled = true;
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;

                foreach (DataRow i in theDST.Rows)
                {
                    string TM, USER, MDIAN, SL, JIESS;
                    TM = i["TM"].ToString();
                    SL = i["SLIANG"].ToString();
                    JIESS = i["JIES"].ToString();
                    if (JIESS == string.Empty) { JIESS = i["XSOU"].ToString(); }
                    USER = xconfig.USER;
                    MDIAN = comboBox1.Text;

                    cmd = new SqlCommand("QZ_CKDH_LOG", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", TM));
                    cmd.Parameters.Add(new SqlParameter("@USER", USER));
                    cmd.Parameters.Add(new SqlParameter("@MDIAN", MDIAN));
                    cmd.Parameters.Add(new SqlParameter("@SL", SL));
                    cmd.Parameters.Add(new SqlParameter("@DH", DH));
                    string thisdh = label5.Text;
                    if (thisdh.Substring(0, 1).ToString() == "R")
                    {
                        cmd.Parameters.Add(new SqlParameter("@RAC", "TRUE"));
                    }
                    else if (thisdh.Substring(0, 1).ToString() == "T")
                    {
                        cmd.Parameters.Add(new SqlParameter("@RAC", "FALSE"));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@RAC", "NULL"));
                        //label5.Text = "{发}";
                    }

                    cmd.Parameters.Add(new SqlParameter("@RKDH", label5.Text));
                    cmd.Parameters.Add(new SqlParameter("@JIES", JIESS));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

                textBox1.Text = DH;
                textBox1.Enabled = false;
                privetMDIAN = comboBox1.Text;
                comboBox1.Enabled = false;

                toolStripButton2.Enabled = true;
                toolStripButton4.Enabled = false;

                MessageBox.Show("发货完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                toolStripButton1.Enabled = true;
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (toolStripButton1.Enabled)
            {
                return;
            }

            if (theDST.Rows.Count < 1)
            {
                return;
            }

            panel4.Visible = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                extFilename = "00C01.xlt";
            }
            else if (radioButton2.Checked)
            {
                extFilename = "00C02.xlt";
            }
            else if (radioButton3.Checked)
            {
                extFilename = "00C03.xlt";
            }
            else if (radioButton4.Checked)
            {
                extFilename = "00C04.xlt";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            radioButton1_CheckedChanged(radioButton1, new EventArgs());
            
            try
            {
                string XTIME, theDH, MDIAN, USER;
                XTIME = DateTime.Now.ToShortDateString();
                theDH = textBox1.Text;
                MDIAN = privetMDIAN;
                USER = xconfig.USER;

                GoldPrinter.ExcelAccess exlprint = new GoldPrinter.ExcelAccess();
                exlprint.Open(Application.StartupPath + @"\" + extFilename);
                exlprint.IsVisibledExcel = false;
                exlprint.FormCaption = "ColorBay";

                int rowcopy = 9;
                int rowstart = 11;

                exlprint.SetCellText(4, "C", XTIME);
                exlprint.SetCellText(5, "C", XTIME);
                exlprint.SetCellText(4, "K", theDH);
                exlprint.SetCellText(5, "J", MDIAN);
                switch (extFilename)
                {
                    case "00C01.xlt":
                        exlprint.SetCellText(13, "L", USER);
                        break;
                    case "00C02.xlt":
                        exlprint.SetCellText(13, "L", USER);
                        break;
                    case "00C03.xlt":
                        exlprint.SetCellText(13, "L", USER);
                        break;
                    case "00C04.xlt":
                        exlprint.SetCellText(13, "L", USER);
                        break;
                }
                
                DataTable newDT = theDST;

                foreach (DataRow i in newDT.Rows)
                {
                    exlprint.InsertRow(rowcopy, rowcopy);
                    //exlprint.SetRowHeight(rowcopy, 18.75f);
                }

                int j = 0;
                foreach (DataRow i in newDT.Rows)
                {
                    exlprint.SetCellText(rowstart + j, "A", i["TM"].ToString());
                    exlprint.SetCellText(rowstart + j, "B", i["NAME"].ToString());
                    exlprint.SetCellText(rowstart + j, "D", i["ZSHU"].ToString());
                    exlprint.SetCellText(rowstart + j, "E", i["KUS"].ToString());
                    exlprint.SetCellText(rowstart + j, "F", i["SLIANG"].ToString());
                    exlprint.SetCellText(rowstart + j, "G", i["DWEI"].ToString());
                    exlprint.SetCellText(rowstart + j, "H", i["JINZ"].ToString());
                    switch (extFilename)
                    {
                        case "00C01.xlt":
                            exlprint.SetCellText(rowstart + j, "K", i["CBEI"].ToString());
                            exlprint.SetCellText(rowstart + j, "L", i["XSOU"].ToString());
                            exlprint.SetCellText(rowstart + j, "I", i["ZSHI"].ToString());
                            exlprint.SetCellText(rowstart + j, "J", i["FSHI"].ToString());
                            break;
                        case "00C02.xlt":
                            exlprint.SetCellText(rowstart + j, "K", i["CBEI"].ToString());
                            //exlprint.SetCellText(rowstart + j, "J", i["XSOU"].ToString());
                            exlprint.SetCellText(rowstart + j, "I", i["ZSHI"].ToString());
                            exlprint.SetCellText(rowstart + j, "J", i["FSHI"].ToString());
                            break;
                        case "00C03.xlt":
                            //exlprint.SetCellText(rowstart + j, "I", i["CBEI"].ToString());
                            exlprint.SetCellText(rowstart + j, "K", i["XSOU"].ToString());
                            exlprint.SetCellText(rowstart + j, "I", i["ZSHI"].ToString());
                            exlprint.SetCellText(rowstart + j, "J", i["FSHI"].ToString());
                            exlprint.SetCellText(rowstart + j, "L", i["JIES"].ToString());
                            break;
                        case "00C04.xlt":
                            //exlprint.SetCellText(rowstart + j, "K", i["CBEI"].ToString());
                            exlprint.SetCellText(rowstart + j, "K", i["XSOU"].ToString());
                            exlprint.SetCellText(rowstart + j, "I", i["ZSHI"].ToString());
                            exlprint.SetCellText(rowstart + j, "J", i["FSHI"].ToString());
                            break;
                    }
                    j++;
                }

                exlprint.DeleteRow(10);
                exlprint.DeleteRow(9);
                exlprint.DeleteRow(8);

                MSG.login.Close();
                exlprint.PrintPreview();
                exlprint.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            new 导入单据(theDST,label5).ShowDialog();
            //foreach (DataRow i in theDST.Rows)
            //{
            //    if (i["JIES"].ToString() == string.Empty)
            //    {
            //        i["JIES"] = i["XSOU"].ToString();
            //    }
            //}
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            string sl = theDST.Compute("sum(sliang)", "").ToString();
            string zsz = theDST.Compute("sum(zsz)", "").ToString();
            string fsz = theDST.Compute("sum(fsz)", "").ToString();
            string zss = theDST.Compute("sum(zss)", "").ToString();
            string fss = theDST.Compute("sum(fss)", "").ToString();
            string cbei = theDST.Compute("sum(cbei)", "").ToString();
            string xsou = theDST.Compute("sum(xsou)", "").ToString();
            string jiess = theDST.Compute("sum(jies)","jies not is null").ToString();

            sllab.Text = sl;
            zsilab.Text = zsz + "/" + zss;
            fslab.Text = fsz + "/" + fss;
            label13.Text = cbei;
            label14.Text = xsou;
            label15.Text = jiess;

            panel5.Visible = true;
            ((ToolStripButton)sender).Enabled = false;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            toolStripButton7.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }
            if (textBox3.Text == string.Empty)
            {
                textBox3.Focus();
                return;
            }

            Regex regex = new Regex(@"^\d{1,2}$|^\d{1,2}\.\d{1,2}");
            if (!regex.Match(textBox3.Text).Success)
            {
                textBox3.Text = string.Empty;
                textBox3.Focus();
                return;
            }

            double isdou1 = 1;
            try
            {
                isdou1 = double.Parse(textBox3.Text);
            }
            catch
            {
                isdou1 = 1;
            }

            foreach (DataRow i in theDST.Rows)
            {
                double isdou2 = double.Parse(i["xsou"].ToString());
                double isdou3 = isdou1 * isdou2;
                isdou3 = (int)(isdou3 * 100);
                isdou3 = isdou3 / 100.00;
                i["jies"] = isdou3;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"\d|\.");
            if (!regex.Match(e.KeyChar.ToString()).Success)
            {
                e.Handled = true;
            }
        }
    }
}