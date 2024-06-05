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
using System.Net;

namespace Client.商品入库
{
    public partial class 商品入库添加 : Form
    {
        DataTable panDT;
        DataGridView panDGV;
        DataTable dstJL=xconfig.DST.Tables["金料"];
        DataTable dstSL = xconfig.DST.Tables["石料"];
        DataTable dstSSI = xconfig.DST.Tables["首饰"];
        DataTable dstXZ = xconfig.DST.Tables["形状"];
        DataTable dstSTONG = xconfig.DST.Tables["stong"];
        DataTable dstJD = xconfig.DST.Tables["净度"];
        DataTable dstQG = xconfig.DST.Tables["切工"];
        DataTable dstDW = xconfig.DST.Tables["单位"];
        DataTable dstYS = xconfig.DST.Tables["颜色"];
        string TM;

        public 商品入库添加(DataTable dt, DataGridView dgv)
        {
            panDT = dt;
            panDGV = dgv;

            mesk.xSocket mesk = new mesk.xSocket();
            IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
            int port = xconfig.SERVERPORT;
            string msgstr = xconfig.USER + "|入库|GETCODETM<EOF>";
            TM = mesk.Send(msgstr, ip, port);
            if (TM=="ERROR")
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "select rtrim(name) as name from qJLIAO";
            config.conData.fill("sql", constr, cmdstr, dstJL);
            cmdstr = "select rtrim(name) as name from qSLIAO";
            config.conData.fill("sql", constr, cmdstr, dstSL);
            cmdstr = "select rtrim(name) as name from qSSHI";
            config.conData.fill("sql", constr, cmdstr, dstSSI);
            cmdstr = "select rtrim(name) as name from qXZUANG";
            config.conData.fill("sql", constr, cmdstr, dstXZ);
            cmdstr = "select rtrim(name) as name from qJDU";
            config.conData.fill("sql", constr, cmdstr, dstJD);
            cmdstr = "select rtrim(name) as name from qQIG";
            config.conData.fill("sql", constr, cmdstr, dstQG);
            cmdstr = "select rtrim(name) as name from qDWEI";
            config.conData.fill("sql", constr, cmdstr, dstDW);
            cmdstr = "select rtrim(name) as name from qYSE";
            config.conData.fill("sql", constr, cmdstr, dstYS);
            dstSTONG.Rows.Clear();

            InitializeComponent();
        }

        //public 商品入库添加()
        //{
            
        //    InitializeComponent();
        //}

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 商品入库添加_Load(object sender, EventArgs e)
        {
            dataGridView1.Location = new Point(11, 300);
            dstSTONG.Rows.Clear();
            dataGridView1.DataSource = dstSTONG;
            this.Size = new Size(818, 326);

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            ssidlab.Text = TM;
            tmbox.Text = TM;

            jlbox.Items.Clear();
            slbox.Items.Clear();
            ssibox.Items.Clear();

            foreach (DataRow i in dstJL.Rows)
            {
                jlbox.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstSL.Rows)
            {
                slbox.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstSSI.Rows)
            {
                ssibox.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstDW.Rows)
            {
                dweibox.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstYS.Rows)
            {
                comboBox1.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstXZ.Rows)
            {
                comboBox2.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstQG.Rows)
            {
                comboBox3.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstJD.Rows)
            {
                comboBox4.Items.Add(i["name"].ToString());
            }

        }

        camACP.Cam camstart;

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool checkbol = checkBox2.Checked;
            camstart = new camACP.Cam(Image.Handle,0,0,Image.Size.Width,Image.Size.Height);
            if (checkbol)
            {
                camstart.Start();
            }
            else
            {
                camstart.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (zsibox.Text == "") { zsibox.Focus(); return; }
            if (fsbox.Text == "") { fsbox.Focus(); return; }
            if (jinzhongbox.Text == "") { jinzhongbox.Focus(); return; }
            string[] item1 = zsibox.Text.Split(new char[] { '/' });
            string[] item2 = fsbox.Text.Split(new char[] { '/' });
            new 商品入库成本计算(cbenbox, item2[0], item1[0], jinzhongbox.Text, lsoubox, beilbox).ShowDialog();
            this.Focus();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView1.Rows.Count > 7)
            {
                pictureBox1.Visible = false;
            }
            config.conDGV.DGVAutoID(dataGridView1, "phao", 1);
            int a = dataGridView1.Rows.Count - 1;
            for (int i = 0; i < a; i++)
            {
                dataGridView1["cliid", i].Value = TM;
                ((DataGridViewComboBoxCell)dataGridView1["xzuang",i]).Items.Clear();
                ((DataGridViewComboBoxCell)dataGridView1["nameDataGridViewTextBoxColumn", i]).Items.Clear();
                ((DataGridViewComboBoxCell)dataGridView1["jdu", i]).Items.Clear();
                ((DataGridViewComboBoxCell)dataGridView1["qig", i]).Items.Clear();
                int b = dstSL.Rows.Count;
                for (int j = 0; j < b; j++)
                {
                    ((DataGridViewComboBoxCell)dataGridView1["nameDataGridViewTextBoxColumn", i]).Items.Add(dstSL.Rows[j]["name"].ToString());
                }
                int l = dstXZ.Rows.Count;
                for (int j = 0; j < l; j++)
                {
                    ((DataGridViewComboBoxCell)dataGridView1["xzuang", i]).Items.Add(dstXZ.Rows[j]["name"].ToString());
                }
                foreach (DataRow v in dstJD.Rows)
                {
                    ((DataGridViewComboBoxCell)dataGridView1["jdu", i]).Items.Add(v["name"].ToString());
                }
                foreach (DataRow v in dstQG.Rows)
                {
                    ((DataGridViewComboBoxCell)dataGridView1["qig", i]).Items.Add(v["name"].ToString());
                }
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dataGridView1.Rows.Count < 8)
            {
                pictureBox1.Visible = true;
            }
            dataGridView1_RowsAdded(dataGridView1, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool chebol = ((CheckBox)sender).Checked;
            if (chebol)
            {
                this.Size = new Size(818, 526);
                dataGridView1.Location = new Point(11, 265);
                this.MaximumSize = new Size(818, 526);
                this.MinimumSize = new Size(818, 526);
            }
            else
            {
                dataGridView1.Location = new Point(11, 300);
                this.Size = new Size(818, 326);
                this.MaximumSize = new Size(818, 326);
                this.MinimumSize = new Size(818, 326);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton1.Enabled = true;
            
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGridView1_CellEndEdit(dataGridView1, new DataGridViewCellEventArgs(0, 0));
            toolStripButton1.Enabled = false;
        }

        private void toolStripButton1_EnabledChanged(object sender, EventArgs e)
        {
            try
            {
                int zssl = 0;
                int fssl = 0;
                decimal zszl = 0;
                decimal fszl = 0;
                int s = dataGridView1.Rows.Count - 1;
                for (int i = 0; i < s; i++)
                {
                    if (dataGridView1["zfshiDataGridViewTextBoxColumn", i].Value.ToString() == "主石")
                    {
                        int itemsl = int.Parse(dataGridView1["sliangDataGridViewTextBoxColumn", i].Value.ToString());
                        zssl += itemsl;
                    }
                }
                for (int i = 0; i < s; i++)
                {
                    if (dataGridView1["zfshiDataGridViewTextBoxColumn", i].Value.ToString() == "辅石")
                    {
                        int itemsl = int.Parse(dataGridView1["sliangDataGridViewTextBoxColumn", i].Value.ToString());
                        fssl += itemsl;
                    }
                }
                for (int i = 0; i < s; i++)
                {
                    if (dataGridView1["zfshiDataGridViewTextBoxColumn", i].Value.ToString() == "主石")
                    {
                       decimal itemsl = decimal.Parse(dataGridView1["zliangDataGridViewTextBoxColumn", i].Value.ToString());
                        zszl += itemsl;
                    }
                }
                for (int i = 0; i < s; i++)
                {
                    if (dataGridView1["zfshiDataGridViewTextBoxColumn", i].Value.ToString() == "辅石")
                    {
                        decimal itemsl = decimal.Parse(dataGridView1["zliangDataGridViewTextBoxColumn", i].Value.ToString());
                        fszl += itemsl;
                    }
                }

                zsibox.Text = zszl.ToString() + "/" + zssl.ToString();
                fsbox.Text = fszl.ToString() + "/" + fssl.ToString();
            }
            catch{ return; }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (jlbox.Text == "")
            {
                jlbox.Focus();
                jlbox.DroppedDown = true;
                return;
            }
            if (slbox.Text == "")
            {
                slbox.Focus();
                slbox.DroppedDown = true;
                return;
            }
            if (ssibox.Text == "")
            {
                ssibox.Focus();
                ssibox.DroppedDown = true;
                return;
            }
            if (khaobox.Text == "")
            {
                khaobox.Focus();
                return;
            }
            if (zsibox.Text == "0/0" || zsibox.Text == "")
            {
                zsibox.Focus();
                return;
            }
            //if (fsbox.Text == "0/0" || fsbox.Text == "")
            //{
            //    fsbox.Focus();
            //    return;
            //}
            if (jinzhongbox.Text == "0.000" || jinzhongbox.Text == "")
            {
                jinzhongbox.Focus();
                return;
            }
            if (jianzhongbox.Text == "0.000" || jianzhongbox.Text == "")
            {
                jianzhongbox.Focus();
                return;
            }
            if (peijbox.Text == "") { peijbox.Text = "0.000"; }
            if (cbenbox.Text == "") { cbenbox.Text = "0.000"; }
            if (lsoubox.Text == "") { lsoubox.Text = "0.000"; }

            bool imgbol = checkBox2.Checked;
            bool stongbol = checkBox1.Checked;
            string tm, jliao, sliao, ssi, zshu, kus, hhao, dwei, bzhu, yse, xzuang, qgong, jdu;
            int  sliang, zss, fss;
            double jianz, jinz, pjianz, blu, zsz, fsz, cbei, xsou, qkou;
            tm = tmbox.Text.Trim();
            jliao = jlbox.Text.Trim();
            sliao = slbox.Text.Trim();
            ssi = ssibox.Text.Trim();
            if (qkoubox.Text == "")
            {
                qkou = 0;
            }
            else
            {
                qkou = double.Parse(qkoubox.Text.Trim());
            }
            yse = comboBox1.Text;
            xzuang = comboBox2.Text;
            qgong = comboBox3.Text;
            jdu = comboBox4.Text;
            zshu = zshubox.Text.Trim();
            kus = khaobox.Text.Trim();
            hhao = hhaobox.Text.Trim();
            dwei = dweibox.Text.Trim();
            sliang = int.Parse(slbox1.Text.Trim());
            jianz = double.Parse(jianzhongbox.Text.Trim());
            jinz = double.Parse(jinzhongbox.Text.Trim());
            pjianz = double.Parse(peijbox.Text.Trim());
            blu = double.Parse(beilbox.Text.Trim());
            if (zsibox.Text == "") { zsibox.Text = "0/0"; }
            if (fsbox.Text == "") { fsbox.Text = "0/0"; }
            string[] itemstr1 = zsibox.Text.Split(new char[] { '/' });
            string[] itemstr2 = fsbox.Text.Split(new char[] { '/' });
            zsz = double.Parse(itemstr1[0]);
            zss = int.Parse(itemstr1[1]);
            fsz = double.Parse(itemstr2[0]);
            fss = int.Parse(itemstr2[1]);
            cbei = double.Parse(cbenbox.Text.Trim());
            xsou = double.Parse(lsoubox.Text.Trim());
            bzhu = bzubox.Text.Trim();

            if (imgbol)
            {
                string path = Application.StartupPath + "\\Item\\" + TM + ".bmp";
                camstart.GrabImage(path);
            }

            if (stongbol)
            {
                if (dstSTONG.Rows.Count > 0)
                {
                    DataRow[] dr = dstSTONG.Select("zfshi='null'", "");
                    foreach (DataRow i in dr)
                    {
                        dstSTONG.Rows.Remove(i);
                    }
                    DataRow[] dr2 = dstSTONG.Select("sliang=0 and zliang=0", "");
                    foreach (DataRow i in dr2)
                    {
                        dstSTONG.Rows.Remove(i);
                    }
                    try
                    {
                        OleDbConnection conn = new OleDbConnection(xconfig.oldstr("goods", "cnsdjian"));
                        conn.Open();
                        OleDbDataAdapter dars = new OleDbDataAdapter("select * from stong", conn);
                        OleDbCommandBuilder cmd = new OleDbCommandBuilder(dars);
                        dars.Update(dstSTONG);
                        conn.Close();

                        //string constr = xconfig.oldstr("goods","cnsdjian");
                        //string cmdstr;
                        //cmdstr = "select rtrim(cliid) as cliid,rtrim(zfshi) as zfshi,rtrim(name) as name"
                        //        + ",sliang,zliang,rtrim(xzuang) as xzuang,rtrim(jdu) as jdu,rtrim(qig) as qig from stong";
                        //config.conData.fill("acc", constr, cmdstr, panSTONG);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            try
            {
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("goods", "cnsdjian"));
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("insert into goods("
                    + "tm,jliao,sliao,ssi,qkou,zshu,kus,hhao,dwei,sliang,jianz,jinz,pjianz,blu,zsz,zss,fsz,fss,cbei,xsou,bzhu,slbol,yse,xzuang,qgong,jdu,imgbol"
                    + ")values("
                    + "'" + tm + "',"
                    + "'" + jliao + "',"
                    + "'" + sliao + "',"
                    + "'" + ssi + "',"
                    + qkou + ","
                    + "'" + zshu + "',"
                    + "'" + kus + "',"
                    + "'" + hhao + "',"
                    + "'" + dwei + "',"
                    + sliang + ","
                    + jianz + ","
                    + jinz + ","
                    + pjianz + ","
                    + blu + ","
                    + zsz + ","
                    + zss + ","
                    + fsz + ","
                    + fss + ","
                    + cbei + ","
                    + xsou + ","
                    + "'" + bzhu + "',"
                    + stongbol + ","
                    + "'" + yse + "',"
                    + "'" + xzuang + "',"
                    + "'" + qgong + "',"
                    + "'" + jdu + "',"
                    + imgbol + ")", conn);
                cmd.ExecuteNonQuery();
                OleDbDataAdapter dars = new OleDbDataAdapter("select id,rtrim(tm) as tm,"
                    + "rtrim(jliao) as jliao,rtrim(sliao) as sliao,rtrim(ssi) as ssi,"
                    + "qkou,rtrim(zshu) as zshu,rtrim(kus) as kus,rtrim(hhao) as hhao,"
                    + "rtrim(dwei) as dwei,sliang,jianz,jinz,pjianz,blu,zsz,zss,fsz,fss,"
                    + "cbei,xsou,rtrim(bzhu) as bzhu,slbol,imgbol,rtrim(yse) as yse,rtrim(xzuang) as xzuang,"
                    + "rtrim(qgong) as qgong,rtrim(jdu) as jdu from goods order by id desc", conn);
                panDT.Clear();
                dars.Fill(panDT);
                conn.Close();

                if (!checkBox3.Checked)
                {
                    this.Close();
                }

                mesk.xSocket mesk = new mesk.xSocket();
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                string msgstr = xconfig.USER + "|入库|GETCODETM<EOF>";
                string gettm = mesk.Send(msgstr, ip, port);
                if (gettm == "ERROR" || gettm == "")
                {
                    MessageBox.Show("服务器已断开！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                tmbox.Text = gettm;
                jlbox.Text = "";
                slbox.Text = "";
                ssibox.Text = "";
                qkoubox.Text = "";
                zshubox.Text = "";
                khaobox.Text = "";
                hhaobox.Text = "";
                dweibox.Text = "";
                slbox.Text = "";
                jianzhongbox.Text = "0.000";
                jinzhongbox.Text = "0.000";
                peijbox.Text = "0.000";
                zsibox.Text = "0/0";
                fsbox.Text = "0/0";
                cbenbox.Text = "0.000";
                lsoubox.Text = "0.000";
                bzubox.Text = "";
                dstSTONG.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tmbox_Enter(object sender, EventArgs e)
        {
            string box = sender.GetType().ToString();
            if (box == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.MistyRose;
            }
            else if (box == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.MistyRose;
                ((TextBox)sender).SelectAll();
            }
        }

        private void tmbox_Leave(object sender, EventArgs e)
        {
            string box = sender.GetType().ToString();
            if (box == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.White;
            }
            else if (box == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.White;
            }
        }

        private void tmbox_Enter_1(object sender, EventArgs e)
        {
            jlbox.Focus();
        }

        private void jlbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{Tab}");
            }
            else if (key == 8)
            {
                e.Handled = false ;
            }
        }

        private void qkoubox_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void qkoubox_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^\d+$|^\d+\.\d{1}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = "0";
            }
            tmbox_Leave(sender, new EventArgs());
        }

        private void jianzhongbox_KeyPress(object sender, KeyPressEventArgs e)
        {
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
                Regex regex = new Regex(@"^\d$|^\.$");
                if (!regex.Match(e.KeyChar.ToString()).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void beilbox_Leave(object sender, EventArgs e)
        {
            string str = ((TextBox)sender).Text;
            Regex regex = new Regex(@"^\d+$|^\d+\.\d{1,2}$");
            if (!regex.Match(str).Success)
            {
                ((TextBox)sender).Text = "1.0";
            }
            tmbox_Leave(sender, new EventArgs());
        }

        private void peijbox_Leave(object sender, EventArgs e)
        {
            string str = ((TextBox)sender).Text;
            Regex regex = new Regex(@"^\d+$|^\d+\.\d{1,3}$");
            if (!regex.Match(str).Success)
            {
                ((TextBox)sender).Text = "0.000";
            }
            tmbox_Leave(sender, new EventArgs());
        }

        private void jianzhongbox_Leave(object sender, EventArgs e)
        {
            if (jianzhongbox.Text == "" || jianzhongbox.Text == "0") { jianzhongbox.Text = "0.000"; }
            if (jinzhongbox.Text == "" || jinzhongbox.Text == "0") { jinzhongbox.Text = "0.000"; }
            if (zsibox.Text == "") { zsibox.Text = "0/0"; }
            if (fsbox.Text == "") { fsbox.Text = "0/0"; }

            Regex regex = new Regex(@"^\d+$|^\d+\.\d{1,3}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = "0.000";
                return;
            }

            if (jinzhongbox.Text == "0.000" || jinzhongbox.Text == "0")
            {
                string[] item1 = zsibox.Text.Split(new char[] { '/' });
                string[] item2 = fsbox.Text.Split(new char[] { '/' });
                double a = double.Parse(((TextBox)sender).Text);
                double b = (double.Parse(item1[0]) + double.Parse(item2[0])) / 5;
                double c = a - b;
                if (c < 0)
                {
                    jinzhongbox.Text = "0.000";
                }
                else
                {
                    int d = (int)(c * 1000);
                    c = d / 1000.000;
                    jinzhongbox.Text = c.ToString();
                }
            }
            else
            {
                double a = double.Parse(((TextBox)sender).Text);
                double b = double.Parse(jinzhongbox.Text);
                double c = (a - b) * 5;
                int d = (int)(c * 1000);
                c = d / 1000.000;
                zsibox.Text = c.ToString() + "/1";
                fsbox.Text = "0/0";
                if (zsibox.Text == "0/1") { zsibox.Text = "0/0"; }
                if (fsbox.Text == "0/1") { fsbox.Text = "0/0"; }
            }
            tmbox_Leave(sender, new EventArgs());
        }

        private void jinzhongbox_Leave(object sender, EventArgs e)
        {
            if (jianzhongbox.Text == "" || jianzhongbox.Text == "0") { jianzhongbox.Text = "0.000"; }
            if (jinzhongbox.Text == "" || jinzhongbox.Text == "0") { jinzhongbox.Text = "0.000"; }
            if (zsibox.Text == "") { zsibox.Text = "0/0"; }
            if (fsbox.Text == "") { fsbox.Text = "0/0"; }

            Regex regex = new Regex(@"^\d+$|^\d+\.\d{1,3}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = "0.000";
                return;
            }

            if (zsibox.Text != "" || zsibox.Text != "0/0" || fsbox.Text != "" | fsbox.Text != "0/0")
            {
                string[] itemstr1 = zsibox.Text.Split(new char[] { '/' });
                string[] itemstr2 = fsbox.Text.Split(new char[] { '/' });
                double a = double.Parse(itemstr1[0]) + double.Parse(itemstr2[0]);
                double b = double.Parse(((TextBox)sender).Text);
                double c = a/5 + b;
                int d = (int)(c * 1000);
                c = d / 1000.000;
                jianzhongbox.Text = c.ToString();
            }
            else
            {
                jianzhongbox.Text = ((TextBox)sender).Text;
            }

            //if (jianzhongbox.Text == "0.000" || jianzhongbox.Text == "0")
            //{
            //    string[] item1 = zsibox.Text.Split(new char[] { '/' });
            //    string[] item2 = fsbox.Text.Split(new char[] { '/' });
            //    double a = double.Parse(((TextBox)sender).Text);
            //    double b = (double.Parse(item1[0]) + double.Parse(item2[0])) / 5;
            //    double c = a + b;
            //    int d = (int)(c * 1000);
            //    c = d / 1000.000;
            //    jianzhongbox.Text = c.ToString();
            //}
            //else
            //{
            //    double a = double.Parse(jianzhongbox.Text);
            //    double b = double.Parse(((TextBox)sender).Text);
            //    double c = (a - b) * 5;
            //    if (c < 0)
            //    {
            //        jianzhongbox.Text = b.ToString();
            //        zsibox.Text = "0/0";
            //        fsbox.Text = "0/0";
            //        return;
            //    }
            //    int d = (int)(c * 1000);
            //    c = d / 1000.000;
            //    zsibox.Text = c.ToString() + "/1";
            //    fsbox.Text = "0/0";
            //    if (zsibox.Text == "0/1") { zsibox.Text = "0/0"; }
            //    if (fsbox.Text == "0/1") { fsbox.Text = "0/0"; }
            //}
            tmbox_Leave(sender, new EventArgs());
        }

        private void zsibox_Leave(object sender, EventArgs e)
        {
            if (jianzhongbox.Text == "" || jianzhongbox.Text == "0") { jianzhongbox.Text = "0.000"; }
            if (jinzhongbox.Text == "" || jinzhongbox.Text == "0") { jinzhongbox.Text = "0.000"; }
            if (zsibox.Text == "") { zsibox.Text = "0/0"; }
            if (fsbox.Text == "") { fsbox.Text = "0/0"; }

            Regex regex = new Regex(@"^\d+$|^\d+\.\d{1,3}$");
            if (regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text += "/1";
            }

            regex = new Regex(@"^\d+\/\d+$|^\d+\.\d{1,3}\/\d+$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = "0/0";
                return;
            }

            if (!checkBox4.Checked)
            {
                if (jianzhongbox.Text != "" && jianzhongbox.Text != "0.000" && jianzhongbox.Text != "0")
                {
                    string[] item1 = zsibox.Text.Split(new char[] { '/' });
                    string[] item2 = fsbox.Text.Split(new char[] { '/' });
                    double a = double.Parse(jianzhongbox.Text);
                    double b = double.Parse(item1[0]) + double.Parse(item2[0]);
                    double c = a - b / 5;
                    int d = (int)(c * 1000);
                    c = d / 1000.000;
                    jinzhongbox.Text = c.ToString();

                    if (zsibox.Text == "0/1") { zsibox.Text = "0/0"; }
                    if (fsbox.Text == "0/1") { fsbox.Text = "0/0"; }
                    tmbox_Leave(sender, new EventArgs());
                    return;
                }
            }
            else
            {

                if (jinzhongbox.Text != "" && jinzhongbox.Text != "0.000" && jinzhongbox.Text != "0")
                {
                    string[] item1 = zsibox.Text.Split(new char[] { '/' });
                    string[] item2 = fsbox.Text.Split(new char[] { '/' });
                    double a = double.Parse(jinzhongbox.Text);
                    double b = double.Parse(item1[0]) + double.Parse(item2[0]);
                    double c = a + b / 5;
                    int d = (int)(c * 1000);
                    c = d / 1000.000;
                    jianzhongbox.Text = c.ToString();
                }
                else if (jianzhongbox.Text != "" && jianzhongbox.Text != "0.000" && jianzhongbox.Text != "0")
                {
                    string[] item1 = zsibox.Text.Split(new char[] { '/' });
                    string[] item2 = fsbox.Text.Split(new char[] { '/' });
                    double a = double.Parse(jianzhongbox.Text);
                    double b = double.Parse(item1[0]) + double.Parse(item2[0]);
                    double c = a - b / 5;
                    int d = (int)(c * 1000);
                    c = d / 1000.000;
                    jinzhongbox.Text = c.ToString();
                }
            }
            if (zsibox.Text == "0/1") { zsibox.Text = "0/0"; }
            if (fsbox.Text == "0/1") { fsbox.Text = "0/0"; }

            tmbox_Leave(sender, new EventArgs());

        }

        private void lsoubox_Leave(object sender, EventArgs e)
        {
            tmbox_Leave(sender, new EventArgs());

            if (((TextBox)sender).Text == "") { ((TextBox)sender).Text = "0.000"; }
            Regex regex = new Regex(@"^\d+$|^\d+\.\d{1,3}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = "0.000";
                return;
            }
        }

        private void cbenbox_Leave(object sender, EventArgs e)
        {
            tmbox_Leave(sender, new EventArgs());

            if (((TextBox)sender).Text == "") { ((TextBox)sender).Text = "0.000"; }
            Regex regex = new Regex(@"^\d+$|^\d+\.\d{1,3}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = "0.000";
                return;
            }
        }

    }
}