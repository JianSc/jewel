using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Threading;

namespace Client
{
    public partial class 综合查询打印 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable cliDST = new clidata().Tables["goods"];
        Form panFORM;
        DataTable panDT;
        bool TJBOL = false;
        string TJSTAT = string.Empty;

        public 综合查询打印(ToolStripMenuItem menu, Form panform)
        {
            panFORM = panform;
            panMENU = menu;
            InitializeComponent();
            button4.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;

        }
        public 综合查询打印(ToolStripMenuItem menu, Form panform, bool tm)
        {
            panFORM = panform;
            panMENU = menu;
            InitializeComponent();
            if (tm)
            {
                button4.Visible = tm;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }
        }
        public 综合查询打印(DataTable dt,string tjstat)
        {
            panDT = dt;
            TJBOL = true;
            TJSTAT = tjstat;
            InitializeComponent();

            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
        }

        private void 综合查询打印_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            DataTable rkDST = new DataTable();
            DataTable ckDST = new DataTable();
            DataTable ckuDST = new DataTable();
            DataTable mdianDST = new DataTable();
            DataTable jliaoDST = new DataTable();
            DataTable sliaoDST=new DataTable();
            DataTable ssiDST=new DataTable();

            DateTime orDate1 = DateTime.Now.AddMonths(-1);
            DateTime orDate2 = DateTime.Now;

            dateTimePicker2.Value = orDate1;
            dateTimePicker1.Value = orDate2;
            dateTimePicker3.Value = orDate1;
            dateTimePicker4.Value = orDate2;

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(NAME) AS NAME FROM QCKU";
            config.conData.fill("sql", constr, cmdstr, ckuDST);
            cmdstr = "SELECT RTRIM(DH) AS DH FROM (SELECT DH,CONVERT(CHAR(10),SETTIME,120) AS SETTIME FROM QGOODS_CKD) DERIVEDTAB GROUP BY DH,SETTIME ORDER BY SETTIME DESC";
            config.conData.fill("sql", constr, cmdstr, ckDST);
            cmdstr = "SELECT RTRIM(DH) AS DH FROM (SELECT DH,CONVERT(CHAR(10),SETDATE,120) AS SETDATE FROM QGOODS_RKD) DERIVEDTAB GROUP BY DH,SETDATE ORDER BY SETDATE DESC";
            config.conData.fill("sql", constr, cmdstr, rkDST);
            cmdstr = "SELECT RTRIM(NAME) AS NAME FROM QMDIAN";
            config.conData.fill("sql", constr, cmdstr, mdianDST);

            //comboBox4.Items.Add("1");
            //comboBox5.Items.Add("2");
            //comboBox6.Items.Add("3");
            cmdstr = "SELECT RTRIM(NAME) AS NAME FROM QJLIAO";
            config.conData.fill("sql", constr, cmdstr, jliaoDST);
            cmdstr = "SELECT RTRIM(NAME) AS NAME FROM QSLIAO";
            config.conData.fill("sql", constr, cmdstr, sliaoDST);
            cmdstr = "SELECT RTRIM(NAME) AS NAME FROM QSSHI";
            config.conData.fill("sql", constr, cmdstr, ssiDST);

            foreach (DataRow i in jliaoDST.Rows)
            {
                comboBox4.Items.Add(i["NAME"].ToString());
            }
            foreach (DataRow i in sliaoDST.Rows)
            {
                comboBox5.Items.Add(i["NAME"].ToString());
            }
            foreach (DataRow i in ssiDST.Rows)
            {
                comboBox6.Items.Add(i["NAME"].ToString());
            }
            foreach (DataRow i in rkDST.Rows)
            {
                comboBox2.Items.Add(i["DH"].ToString());
            }
            foreach (DataRow i in ckDST.Rows)
            {
                comboBox1.Items.Add(i["DH"].ToString());
            }
            foreach (DataRow i in ckuDST.Rows)
            {
                comboBox3.Items.Add(i["NAME"].ToString());
            }
            foreach (DataRow i in mdianDST.Rows)
            {
                comboBox3.Items.Add(i["NAME"].ToString());
            }
        }

        private void 综合查询打印_FormClosed(object sender, FormClosedEventArgs e)
        {
            string msgstr = xconfig.USER + "|闲置|USERCLOSE<EOF>";
            xconfig.netSend(msgstr);

            try
            {
                panMENU.Enabled = true;
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool itembol = ((CheckBox)sender).Checked;
            radioButton1.Enabled = itembol;
            radioButton2.Enabled = itembol;
            //comboBox1.Enabled = itembol;
            //comboBox2.Enabled = itembol;
            bool rad1 = radioButton1.Checked;
            bool rad2 = radioButton2.Checked;
            if (itembol)
            {
                comboBox1.Enabled = rad2;
                comboBox2.Enabled = rad1;
            }
            else
            {
                comboBox1.Enabled = itembol;
                comboBox2.Enabled = itembol;
            }

            if (itembol)
            {
                panel3.Enabled = false;
            }
            else
            {
                panel3.Enabled = true;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool dstsetDATA()
        {
            bool tabol = checkBox1.Checked;
            cliDST.Clear();

            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked && !checkBox6.Checked && !checkBox7.Checked && !checkBox8.Checked && !checkBox9.Checked && !checkBox10.Checked && !checkBox11.Checked && !checkBox12.Checked && !checkBox13.Checked && !checkBox14.Checked && !checkBox15.Checked)
            {
                return false;
            }

            if (tabol)
            {
                string dataFrom;
                string selectDH;

                if (radioButton1.Checked)
                {
                    dataFrom = "QGOODS_RKD";
                    selectDH = comboBox2.Text;
                    if (selectDH == "") { comboBox2.Focus(); return false; }
                }
                else
                {
                    dataFrom = "QGOODS_CKD";
                    selectDH = comboBox1.Text;
                    if (selectDH == "") { comboBox1.Focus(); return false; }
                }

                MSG.login.Show();
                Application.DoEvents();
                string cmdstr;
                cmdstr = "SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,"
                + "RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,"
                + "ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,RTRIM(JDU) AS JDU,RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,"
                + "RTRIM(QGONG) AS QGONG,QGOODS.BLU,JJA,PJJA,ZSJA,ZSJE,FSJA,FSJE,JGDJ,OTHER,JGSH,ID FROM QGOODS INNER JOIN " + dataFrom + " ON QGOODS.TM=" + dataFrom + ".TM WHERE (" + dataFrom + ".DH='" + selectDH + "')";
                string constr;
                constr = xconfig.CONNSTR;
                config.conData.fill("sql", constr, cmdstr, cliDST);
                MSG.login.Close();
                return true;
            }
            else
            {
                if (checkBox2.Checked)
                {
                    if (textBox1.Text == "") { textBox1.Focus(); return false ; }
                    if (textBox2.Text == "") { textBox2.Focus(); return false ; }
                }
                if (checkBox3.Checked)
                {
                    if (textBox3.Text == "") { textBox3.Focus(); return false ; }
                    if (textBox4.Text == "") { textBox4.Focus(); return false ; }
                }
                if (checkBox4.Checked)
                {
                    if (textBox5.Text == "") { textBox5.Focus(); return false ; }
                    if (textBox6.Text == "") { textBox6.Focus(); return false ; }
                }
                if (checkBox5.Checked)
                {
                    if (textBox7.Text == "") { textBox7.Focus(); return false ; }
                    if (textBox8.Text == "") { textBox8.Focus(); return false ; }
                }
                if (checkBox8.Checked)
                {
                    if (comboBox3.Text == "") { comboBox3.DroppedDown = true; return false; }
                }
                if (checkBox9.Checked)
                {
                    if (textBox9.Text == "") { textBox9.Focus(); return false; }
                    if (textBox10.Text == "") { textBox10.Focus(); return false; }
                }
                if (checkBox10.Checked)
                {
                    if (textBox11.Text == "") { textBox11.Focus(); return false; }
                    if (textBox12.Text == "") { textBox12.Focus(); return false; }
                }
                if (checkBox11.Checked)
                {
                    if (textBox13.Text == "") { textBox13.Focus(); return false; }
                }
                if (checkBox12.Checked)
                {
                    if (comboBox4.Text == string.Empty) { comboBox4.Focus(); comboBox4.DroppedDown = true; return false; }
                }
                if (checkBox13.Checked)
                {
                    if (comboBox5.Text == string.Empty) { comboBox5.Focus(); comboBox5.DroppedDown = true; return false; }
                }
                if (checkBox14.Checked)
                {
                    if (comboBox6.Text == string.Empty) { comboBox6.Focus(); comboBox6.DroppedDown = true; return false; }
                }
                if (checkBox15.Checked)
                {
                    if (textBox14.Text == string.Empty) { textBox14.Focus(); return false; }
                }

                string FormStr, WhereStr;
                FormStr = "FROM QGOODS";
                WhereStr = " WHERE (1=1)";
                if (checkBox2.Checked)
                {
                    WhereStr += "AND (CBEI BETWEEN '" + textBox1.Text + "' AND '" + textBox2.Text + "') ";
                }
                if (checkBox3.Checked)
                {
                    WhereStr += "AND (XSOU BETWEEN '" + textBox3.Text + "' AND '" + textBox4.Text + "') ";
                }
                if (checkBox4.Checked)
                {
                    WhereStr += "AND (JIANZ BETWEEN '" + textBox5.Text + "' AND '" + textBox6.Text + "') ";
                }
                if (checkBox5.Checked)
                {
                    WhereStr += "AND (JINZ BETWEEN '" + textBox7.Text + "' AND '" + textBox8.Text + "') ";
                }
                if (checkBox6.Checked)
                {
                    DateTime de1, de2;
                    de1 = DateTime.Parse(dateTimePicker2.Text + " 00:00:00");
                    de2 = DateTime.Parse(dateTimePicker1.Text + " 23:59:59");

                    FormStr +=" INNER JOIN QGOODS_RKD ON QGOODS.TM=QGOODS_RKD.TM";
                    WhereStr += " AND (QGOODS_RKD.SETDATE BETWEEN '" + de1 + "' AND '" + de2 + "')";
                }
                if (checkBox7.Checked)
                {
                    DateTime de1, de2;
                    de1 = DateTime.Parse(dateTimePicker3.Text + " 00:00:00");
                    de2 = DateTime.Parse(dateTimePicker4.Text + " 23:59:59");

                    FormStr += " INNER JOIN QGOODS_SALES ON QGOODS.TM=QGOODS_SALES.TM";
                    WhereStr += " AND (QGOODS_SALES.SETTIME BETWEEN '" + de1 + "' AND '" + de2 + "')";
                }
                if (checkBox8.Checked)
                {
                    string CkuStr;
                    CkuStr = comboBox3.Text;

                    FormStr += " INNER JOIN QGOODS_CKU ON QGOODS.TM=QGOODS_CKU.TM";
                    WhereStr += " AND (QGOODS_CKU.NAME='" + CkuStr + "') AND (QGOODS.XSTAT=1)";
                }
                if (checkBox9.Checked)
                {
                    WhereStr += " AND (QGOODS.ZSZ BETWEEN '" + textBox9.Text + "' AND '" + textBox10.Text + "')";
                }
                if (checkBox10.Checked)
                {
                    FormStr += " INNER JOIN QGOODS_SALES ON QGOODS.TM=QGOODS_SALES.TM";
                    WhereStr += " AND (QGOODS_SALES.SSALE BETWEEN '" + textBox11.Text + "' AND '" + textBox12.Text + "')";
                }
                if (checkBox11.Checked)
                {
                    WhereStr += " AND (QGOODS.KUS='" + textBox13.Text + "')";
                }
                if (checkBox12.Checked)
                {
                    WhereStr += " AND (QGOODS.JLIAO='" + comboBox4.Text + "')";
                }
                if (checkBox13.Checked)
                {
                    WhereStr += " AND (QGOODS.SLIAO='" + comboBox5.Text + "')";
                }
                if (checkBox14.Checked)
                {
                    WhereStr += " AND (QGOODS.SSI='" + comboBox6.Text + "')";
                }
                if (checkBox15.Checked)
                {
                    WhereStr += " AND (QGOODS.TM='" + textBox14.Text + "')";
                }

                MSG.login.Show();
                Application.DoEvents();
                string constr, cmdstr;
                constr = xconfig.CONNSTR;
                cmdstr = "SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,"
                + "RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,"
                + "ZSZ,ZSS,FSZ,FSS,QGOODS.CBEI,XSOU,BZHU,SLBOL,IMGBOL,RTRIM(JDU) AS JDU,RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,"
                + "RTRIM(QGONG) AS QGONG,QGOODS.BLU,JJA,PJJA,ZSJA,ZSJE,FSJA,FSJE,JGDJ,OTHER,JGSH,ID " + FormStr + WhereStr;

                config.conData.fill("sql", constr, cmdstr, cliDST);
                MSG.login.Close();
                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!dstsetDATA())
            {
                return;
            }

            if (cliDST.Rows.Count < 1)
            {
                MessageBox.Show("没有找到相关资料！\n请重新选择查询条件。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (TJBOL)
            {

                if (TJSTAT == "cbei")
                {
                    panDT.Rows.Clear();
                    foreach (DataRow i in cliDST.Rows)
                    {
                        DataRow j = panDT.NewRow();
                        j["TM"] = i["TM"].ToString();
                        j["JJA"] = i["JJA"].ToString();
                        j["PJJA"] = i["PJJA"].ToString();
                        j["ZSJA"] = i["ZSJA"].ToString();
                        j["ZSJE"] = i["ZSJE"].ToString();
                        j["FSJA"] = i["FSJA"].ToString();
                        j["FSJE"] = i["FSJE"].ToString();
                        j["JGDJ"] = i["JGDJ"].ToString();
                        j["OTHER"] = i["OTHER"].ToString();
                        j["JGSH"] = i["JGSH"].ToString();
                        j["ZSS"] = i["ZSS"].ToString();
                        j["ZSZ"] = i["ZSZ"].ToString();
                        j["FSS"] = i["FSS"].ToString();
                        j["FSZ"] = i["FSZ"].ToString();
                        j["JLIAO"] = i["JLIAO"].ToString();
                        j["SLIAO"] = i["SLIAO"].ToString();
                        j["SSI"] = i["SSI"].ToString();
                        j["CBEI"] = i["CBEI"].ToString();
                        j["JINZ"] = i["JINZ"].ToString();
                        panDT.Rows.Add(j);
                    }
                    this.Close();
                }
                else if (TJSTAT == "xsou")
                {
                    panDT.Rows.Clear();
                    foreach (DataRow i in cliDST.Rows)
                    {
                        DataRow j = panDT.NewRow();
                        j["TM"] = i["TM"].ToString();
                        j["MONY"] = i["XSOU"].ToString();
                        j["BLU"] = i["BLU"].ToString();
                        j["CBEI"] = i["CBEI"].ToString();
                        j["SLIAO"] = i["SLIAO"].ToString();
                        j["JLIAO"] = i["JLIAO"].ToString();
                        j["SSI"] = i["SSI"].ToString();
                        j["IMGBOL"] = i["IMGBOL"].ToString();
                        j["BZHU"] = i["BZHU"].ToString();
                        panDT.Rows.Add(j);
                    }
                    this.Close();
                }
            }
            else
            {
                Form itemform = new 综合查询打印详细(cliDST);
                itemform.MdiParent = panFORM;
                itemform.Show();
                this.Close();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            bool rad1 = radioButton1.Checked;
            bool rad2 = radioButton2.Checked;

            comboBox2.Enabled = rad1;
            comboBox1.Enabled = rad2;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool check = ((CheckBox)sender).Checked;

            label1.Enabled = check;
            label2.Enabled = check;
            textBox1.Enabled = check;
            textBox2.Enabled = check;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            bool check = ((CheckBox)sender).Checked;
            label3.Enabled = check;
            label4.Enabled = check;
            textBox3.Enabled = check;
            textBox4.Enabled = check;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            bool check = ((CheckBox)sender).Checked;
            label5.Enabled = check;
            label6.Enabled = check;
            textBox5.Enabled = check;
            textBox6.Enabled = check;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            bool check = ((CheckBox)sender).Checked;
            label7.Enabled = check;
            label8.Enabled = check;
            textBox7.Enabled = check;
            textBox8.Enabled = check;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            bool check = ((CheckBox)sender).Checked;
            label9.Enabled = check;
            label10.Enabled = check;
            dateTimePicker1.Enabled = check;
            dateTimePicker2.Enabled = check;

            if (check)
            {
                checkBox7.Checked = false;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            bool check = ((CheckBox)sender).Checked;
            label11.Enabled = check;
            label12.Enabled = check;
            dateTimePicker3.Enabled = check;
            dateTimePicker4.Enabled = check;

            if (check)
            {
                checkBox6.Checked = false;
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            bool check = ((CheckBox)sender).Checked;
            label13.Enabled = check;
            comboBox3.Enabled = check;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keystr = e.KeyChar.ToString();

            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{Tab}");
            }
            else if (key == 8)
            {
                e.Handled = false;
            }
            else
            {

                Regex regex = new Regex(@"\d|\.");
                if (!regex.Match(e.KeyChar.ToString()).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string textstr = ((TextBox)sender).Text;

            Regex regex = new Regex(@"^\d+$|^\d+\.\d{1,3}$");
            if (!regex.Match(textstr).Success)
            {
                ((TextBox)sender).Text = "";
                ((TextBox)sender).Focus();
                //((TextBox)sender).BackColor = Color.MistyRose;
            }

            ((TextBox)sender).BackColor = Color.White;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.LightGreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!dstsetDATA())
            {
                return;
            }

            if (cliDST.Rows.Count < 1)
            {
                MessageBox.Show("没有找到相关资料！\n请重新选择查询条件。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MSG.login.ShowIMG();
            Application.DoEvents();
            button2.Enabled = false;

            Thread t = new Thread(new ThreadStart(print));
            t.Start();
        }

        private delegate void d();

        private void print()
        {
            try
            {
                Thread.Sleep(500);
                GoldPrinter.ExcelAccess extPrint = new GoldPrinter.ExcelAccess();
                extPrint.Open(Application.StartupPath + @"\00S01.xlt");
                extPrint.FormCaption = "ColorBay";
                extPrint.IsVisibledExcel = false;

                int tmprow = 8;
                int setrow = 10;

                extPrint.SetCellText(12, "L", xconfig.USER);

                foreach (DataRow i in cliDST.Rows)
                {
                    extPrint.InsertRow(tmprow, tmprow);
                    //extPrint.SetRowHeight(tmprow, 18.75f);
                }

                int j = 0;
                foreach (DataRow i in cliDST.Rows)
                {
                    extPrint.SetCellText(setrow + j, "A", i["TM"].ToString());
                    extPrint.SetCellText(setrow + j, "B", i["NAME"].ToString());
                    extPrint.SetCellText(setrow + j, "D", i["ZSHU"].ToString());
                    extPrint.SetCellText(setrow + j, "E", i["KUS"].ToString());
                    extPrint.SetCellText(setrow + j, "F", i["SLIANG"].ToString());
                    extPrint.SetCellText(setrow + j, "G", i["DWEI"].ToString());
                    extPrint.SetCellText(setrow + j, "K", i["CBEI"].ToString());
                    extPrint.SetCellText(setrow + j, "L", i["XSOU"].ToString());
                    extPrint.SetCellText(setrow + j, "I", i["ZSHI"].ToString());
                    extPrint.SetCellText(setrow + j, "J", i["FSHI"].ToString());
                    extPrint.SetCellText(setrow + j, "H", i["JINZ"].ToString());
                    j++;
                }

                extPrint.DeleteRow(9);
                extPrint.DeleteRow(8);
                extPrint.DeleteRow(7);

                MSG.login.CloseIMG();

                extPrint.PrintPreview();
                extPrint.Close();

                if (button2.InvokeRequired) { d d = delegate { button2.Enabled = true; }; button2.Invoke(d); }
                else { button2.Enabled = true; }
            }
            catch (Exception ex)
            {
                MSG.login.CloseIMG();
                if (button2.InvokeRequired) { d d = delegate { button2.Enabled = true; }; button2.Invoke(d); }
                else { button2.Enabled = true; }
                MessageBox.Show(ex.Message);
            }
        }

        private void 综合查询打印_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            bool chkbol = ((CheckBox)sender).Checked;
            label16.Enabled = chkbol;
            label17.Enabled = chkbol;
            textBox11.Enabled = chkbol;
            textBox12.Enabled = chkbol;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            bool chkbol = ((CheckBox)sender).Checked;
            label14.Enabled = chkbol;
            label15.Enabled = chkbol;
            textBox9.Enabled = chkbol;
            textBox10.Enabled = chkbol;
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            bool chkbol = ((CheckBox)sender).Checked;
            label18.Enabled = chkbol;
            textBox13.Enabled = chkbol;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!dstsetDATA())
            {
                return;
            }

            if (cliDST.Rows.Count < 1)
            {
                MessageBox.Show("没有找到相关资料！\n请重新选择查询条件。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!File.Exists(Application.StartupPath+@"\TM.mdb"))
            {
                MessageBox.Show("条码文件不存在！\n请联系管理员。","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            int j = 0;
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                OleDbConnection oleconn = new OleDbConnection(xconfig.oldstr("TM"));
                oleconn.Open();
                OleDbCommand olecmd = new OleDbCommand("DELETE FROM TM", oleconn);
                olecmd.ExecuteNonQuery();

                foreach (DataRow i in cliDST.Rows)
                {
                    cmd = new SqlCommand("SELECT RTRIM(JLIAO) AS NAME_O,RTRIM(SLIAO) AS NAME_G,RTRIM(SSI) AS NAME_C,"
                        + "XSOU AS AMOUNT,TM AS ID,JINZ AS WEIGHT_G,QKOU AS [SIZE],ZSS,ZSZ,FSS,FSZ,RTRIM(KUS) AS TYPE_ID,RTRIM(ZSHU) AS BOOK_ID,RTRIM(JDU) AS CLEAN,RTRIM(YSE) AS COLOR FROM QGOODS WHERE (TM='" + i["TM"].ToString() + "')", conn);
                    SqlDataReader rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        string size_o;
                        if (rs["SIZE"].ToString() == "0")
                        {
                            size_o = string.Empty;
                        }
                        else
                        {
                            size_o = rs["SIZE"].ToString();
                        }
                        olecmd = new OleDbCommand("INSERT INTO TM(NAME_O,NAME_G,NAME_C,AMOUNT,ID,WEIGHT_G,[SIZE],STONE_M,STONE_A,TYPE_ID,BOOK_ID,CLEAN,COLOR)VALUES("
                            + "'" + rs["NAME_O"].ToString()
                        + "','" + rs["NAME_G"].ToString()
                        + "','" + rs["NAME_C"].ToString()
                        + "','" + rs["AMOUNT"].ToString()
                        + "','" + rs["ID"].ToString()
                        + "','" + rs["WEIGHT_G"].ToString()
                        + "','" + size_o
                        + "','" + rs["ZSZ"].ToString() + "/" + rs["ZSS"].ToString()
                        + "','" + rs["FSZ"].ToString() + "/" + rs["FSS"].ToString()
                        + "','" + rs["TYPE_ID"].ToString()
                        + "','" + rs["BOOK_ID"].ToString()
                        + "','" + rs["CLEAN"].ToString()
                        + "','" + rs["COLOR"].ToString()
                        + "')", oleconn);
                        olecmd.ExecuteNonQuery();
                        j++;
                    }
                    rs.Close();
                }
                oleconn.Close();
                conn.Close();
                MSG.login.Close();
                MessageBox.Show("已导出 " + j.ToString() + " 个条码。");
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }                
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            bool chkbol = ((CheckBox)sender).Checked;
            comboBox4.Enabled = chkbol;
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            bool chkbol = ((CheckBox)sender).Checked;
            comboBox5.Enabled = chkbol;
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            bool chkbol = ((CheckBox)sender).Checked;
            comboBox6.Enabled = chkbol;
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                e.Handled = false;
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

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            bool thisbol = ((CheckBox)sender).Checked;

            label19.Enabled = thisbol;
            textBox14.Enabled = thisbol;
        }

    }
}