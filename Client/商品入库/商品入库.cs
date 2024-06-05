using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Data.OleDb;
using System.IO;
using System.Threading;

namespace Client.商品入库
{
    public partial class 商品入库 : Form
    {
        DataTable dstGYS = xconfig.DST.Tables["供应商"];
        DataTable dstCKU = xconfig.DST.Tables["仓库"];
        DataTable dstGOODS = xconfig.DST.Tables["goods"];
        DataTable dstSTONG = xconfig.DST.Tables["stong"];
        camACP.Cam theCAM;
        ToolStripMenuItem panMENU;

        //Point panel1point, panel2point2;

        public 商品入库(ToolStripMenuItem menu)
        {
            panMENU = menu;
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "select id,rtrim(name) as name,rtrim([user]) as [user],rtrim(tel) as tel"
            + ",rtrim(czhen) as czhen,rtrim(dzhi) as dzhi,rtrim(email) as email,rtrim(dqusen) as dqusen,"
            + "rtrim(dqusi) as dqusi,convert(char(10),[time],120) as [time] from qGYSHANG";
            config.conData.fill("sql", constr, cmdstr, dstGYS);
            cmdstr = "select rtrim(name) as name from qCKU";
            config.conData.fill("sql", constr, cmdstr, dstCKU);
            cmdstr = "select id,rtrim(tm) as tm,"
                    + "rtrim(jliao) as jliao,rtrim(sliao) as sliao,rtrim(ssi) as ssi,"
                    + "qkou,rtrim(zshu) as zshu,rtrim(kus) as kus,rtrim(hhao) as hhao,"
                    + "rtrim(dwei) as dwei,sliang,jianz,jinz,pjianz,blu,zsz,zss,fsz,fss,"
                    + "cbei,xsou,rtrim(bzhu) as bzhu,slbol,imgbol,rtrim(yse) as yse,rtrim(xzuang) as xzuang,"
                    + "rtrim(qgong) as qgong,rtrim(jdu) as jdu from goods order by id desc";
            constr = xconfig.oldstr("goods", "cnsdjian");
            config.conData.fill("acc", constr, cmdstr, dstGOODS);
            //cmdstr = "select rtrim(cliid) as cliid,rtrim(zfshi) as zfshi,rtrim(name) as name"
            //+ ",sliang,zliang,rtrim(xzuang) as xzuang,rtrim(jdu) as jdu,rtrim(qig) as qig from stong";
            //dstSTONG.Rows.Clear();
            //config.conData.fill("acc",constr,cmdstr,dstSTONG);

            InitializeComponent();
        }

        private void 商品入库_Load(object sender, EventArgs e)
        {
            //this.MaximumSize = this.Size;
            //this.MinimumSize = this.Size;
            this.WindowState = FormWindowState.Maximized;
            label7.Text = "";
            theCAM = new camACP.Cam(camIMG.Handle, 0, 0, camIMG.Size.Width, camIMG.Size.Height);

            try
            {
                string rkdh;
                mesk.xSocket mesk = new mesk.xSocket();
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                string msgstr = xconfig.USER + "|入库|GETCODERKD<EOF>";
                rkdh = mesk.Send(msgstr, ip, port);
                if (rkdh == "ERROR")
                {
                    MessageBox.Show("服务器连接失败！\n单号获取失败请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
                foreach (DataRow i in dstGOODS.Rows)
                {
                    if (i["qkou"].ToString() == "0")
                    {
                        i["qkou"] = "";
                    }
                }
                dataGridView1.DataSource = dstGOODS;
                config.conDGV.DGVAutoID(dataGridView1, "phao");
                rkdhbox.Text = rkdh;
                comboBox1.Items.Clear();
                foreach (DataRow i in dstGYS.Rows)
                {
                    comboBox1.Items.Add(i["name"].ToString());
                }
                comboBox2.Items.Clear();
                foreach (DataRow i in dstCKU.Rows)
                {
                    comboBox2.Items.Add(i["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|入库|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new 商品入库添加(dstGOODS, dataGridView1).ShowDialog();
            this.Focus();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                bool xsbol = bool.Parse(((DataGridView)sender)["slbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                bool imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());

                toolStripButton7.Enabled = imgbol;
                toolStripButton8.Enabled = xsbol;

                if (!panel1.Visible)
                {
                    toolStripButton11.Enabled = true;
                }

                if (!toolStripButton11.Enabled)
                {
                    string TM = ((DataGridView)sender)["tmDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();
                    if (TM != "")
                    {
                        label7.Text = TM;
                    }
                }

                if (panel2.Visible)
                {
                    toolStripButton7_Click(toolStripButton7, new EventArgs());
                }
            }
            catch { }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

        }

        private void 已对ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            ((DataGridViewRow)dataGridView1.CurrentRow).DefaultCellStyle.BackColor = Color.Empty;

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }
            ((DataGridViewRow)dataGridView1.CurrentRow).DefaultCellStyle.BackColor = Color.Green;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Visible = false;
                toolStripButton11.Enabled = true;
                theCAM.Stop();
            }
            catch { }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (!toolStripButton3.Enabled) { return; }
            toolStripButton11.Enabled = false;
            panel1.Visible = true;
            string theID = dataGridView1["tmDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            label7.Text = theID;
            theCAM.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label7.Text == "")
            {
                return;
            }
            if (!toolStripButton3.Enabled)
            {
                panel1.Visible = false;
                toolStripButton11.Enabled = false;
                theCAM.Stop();
                return;
            }
            string msg = xconfig.USER + "|入库|CONNECT<EOF>";
            xconfig.netSend(msg);
            string path = Application.StartupPath;
            string theID = dataGridView1["idDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
            if (theID == "") { return; }
            //path = path.Replace(@"\", @"\\");
            theCAM.GrabImage(path + "\\Item\\" + label7.Text + ".bmp");

            try
            {
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("goods", "cnsdjian"));
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("update goods set "
                    + "imgbol = true where id = " + theID , conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                DataRow[] thedr = dstGOODS.Select("id='" + theID + "'", "");
                if (thedr.Length > 0)
                {
                    thedr[0]["imgbol"] = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == Keys.F12.ToString())
            {
                if (panel1.Visible)
                {
                    button1_Click(button1, new EventArgs());
                }
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (!panel2.Visible)
            {
                panel2.Visible = true;
            }
            string TM = dataGridView1["tmDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            if (toolStripButton3.Enabled)
            {
                if (!File.Exists(Application.StartupPath + @"\\Item\\" + TM + ".bmp"))
                {
                    pictureBox1.Image = Properties.Resources.ImgGet_Err;
                    return;
                }
                FileStream fs = File.OpenRead(Application.StartupPath + "\\Item\\" + TM + ".bmp");
                byte[] imgbyte = new byte[fs.Length];
                if (imgbyte.Length < 1)
                {
                    pictureBox1.Image = Properties.Resources.ImgGet_Err;
                    return;
                }
                fs.Read(imgbyte, 0, imgbyte.Length);
                fs.Close();
                MemoryStream stream = new MemoryStream(imgbyte);
                Image byimg = Image.FromStream(stream);
                Bitmap bitmap = new Bitmap(byimg, pictureBox1.Size);
                pictureBox1.Image = bitmap;
            }
            else
            {
                Image itemimg = xconfig.netImgGET(xconfig.USER + "|入库|GETBMP|" + TM + "<EOF>");
                Bitmap itembmp = new Bitmap(itemimg, pictureBox1.Size);
                pictureBox1.Image = itembmp;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if(rkdhbox.Text == "" || rkdhbox.Text == "ERROR")
            {
                return;
            }

            if(comboBox1.Text == "")
            {
                comboBox1.DroppedDown = true;
                return;
            }

            if(comboBox2.Text == "")
            {
                comboBox2.DroppedDown = true;
                return;
            }

            if (dstGOODS.Rows.Count < 1)
            {
                return;
            }

            ((ToolStripButton)sender).Enabled = false;

            bool selectDATA = false;
            MSG.login.Show("请在检查条码的唯一性.....");
            Application.DoEvents();
            SqlConnection conn= new SqlConnection(xconfig.CONNSTR);
            conn.Open();
            foreach(DataRow i in dstGOODS.Rows)
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM QGOODS_RKD WHERE TM='"+i["TM"].ToString() +"'",conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if(rs.Read())
                {
                    MessageBox.Show("条码为:[" + rs["TM"].ToString().Trim() + "]的商品于[" + DateTime.Parse(rs["SETDATE"].ToString().Trim()).ToShortDateString()
                    + "]由[" + rs["USER"].ToString().Trim() + "]入库\n\n单号为:[" + rs["DH"].ToString().Trim() + "]");
                    foreach (DataGridViewRow j in dataGridView1.Rows)
                    {
                        if (j.Cells["tmDataGridViewTextBoxColumn"].Value.ToString() == i["TM"].ToString())
                        {
                            j.DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                    ((ToolStripButton)sender).Enabled = true;
                    selectDATA = true;
                    //return;
                }
                rs.Close();
            }
            conn.Close();
            MSG.login.Close();

            if (!selectDATA)
            {
                Thread t = new Thread(new ThreadStart(addGOODS));
                t.Start();
            }

        }

        private delegate void d();
        private void addGOODS()
        {
            string RKDH;
            mesk.xSocket mesks = new mesk.xSocket();
            IPAddress IP = IPAddress.Parse(xconfig.SERVERIP);
            int PORT = xconfig.SERVERPORT;
            string msgstr = xconfig.USER + "|入库|GETCODERKD|GET<EOF>";
            string GetMSG = mesks.Send(msgstr, IP, PORT);
            if (GetMSG == "ERROR" || GetMSG == "")
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或联系管理员。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (this.InvokeRequired) { d d = delegate { toolStripButton3.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton3.Enabled = true; }
                return;
            }
            RKDH = GetMSG;

            MSG.login.Show("请稍候，正在上传图片...");
            Application.DoEvents();
            Thread.Sleep(300);

            DataRow[] dr = dstGOODS.Select("imgbol = true", "");

            for (int i = 0; i < dr.Length; i++)
            {
                int j = i + 1;
                string TM = dr[i]["tm"].ToString();
                MSG.login.Close();
                //Application.DoEvents();
                MSG.login.Show("[" + j.ToString() + @"/" + dr.Length.ToString() + "]  当前进度:正在上传 [" + TM + "]...");
                Application.DoEvents();
                bool filebol = File.Exists(Application.StartupPath + @"\\Item\\" + TM + ".bmp");
                if (!filebol)
                {
                    continue;
                }
                FileStream fs = File.OpenRead(Application.StartupPath + @"\\Item\\" + TM + ".bmp");
                if (fs.Length < 1)
                {
                    continue;
                }

                mesk.xSocket mesk = new mesk.xSocket();
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                string msg = xconfig.USER + "|入库|SETBMP|" + TM + "," + fs.Length.ToString() + "<EOF>";
                fs.Close();
                bool setbol = mesk.ImgSend(msg, ip, port, TM);
                if (setbol)
                {
                    File.Delete(Application.StartupPath + @"\\Item\\" + TM + ".bmp");
                }
            }

            MSG.login.Close();
            MSG.login.Show("请稍候，正在写入数据...");
            Application.DoEvents();
            try
            {
                string tm, jliao, sliao, ssi, zshu;
                double qkou;
                string kus, hhao, dwei;
                int sliang;
                double jianz, jinz, pjianz, blu, zsz;
                int zss;
                double fsz;
                int fss;
                double cbei, xsou;
                string bzhu;
                bool slbol, imgbol;
                string jdu, yse, xzuang, qgong, cadi, ddh, gys, kuc, user, rkdh;
                string jja, pjja, zsja, zsje, fsja, fsje, jgdj, other, jgsh;

                ddh = string.Empty;
                gys = string.Empty;
                kuc = string.Empty;
                cadi = string.Empty;

                rkdh = RKDH;
                if (this.InvokeRequired)
                {
                    d d = delegate
                    {
                        ddh = textBox1.Text;
                        gys = comboBox1.Text;
                        kuc = comboBox2.Text;
                        cadi = textBox2.Text;
                    };
                    this.Invoke(d);
                }
                else
                {
                    ddh = textBox1.Text;
                    gys = comboBox1.Text;
                    kuc = comboBox2.Text;
                    cadi = textBox2.Text;
                }
                user = xconfig.USER;

                string constr = xconfig.oldstr("goods", "cnsdjian");
                string cmdstr = "select rtrim(cliid) as cliid,rtrim(zfshi) as zfshi,rtrim(name) as name"
                              + ",sliang,zliang,rtrim(xzuang) as xzuang,rtrim(jdu) as jdu,rtrim(qig) as qig from stong";
                dstSTONG.Rows.Clear();
                config.conData.fill("acc", constr, cmdstr, dstSTONG);

                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                string connTIME = conn.ConnectionTimeout.ToString();
                conn.Open();
                foreach (DataRow i in dstGOODS.Rows)
                {
                    if (bool.Parse(i["slbol"].ToString()))
                    {
                        DataRow[] thedr = dstSTONG.Select("cliid = '" + i["tm"].ToString().Trim() + "'", "");

                        foreach (DataRow j in thedr)
                        {
                            SqlCommand stcmd = new SqlCommand("insert into [qSTONG]("
                                + "cliid,zfshi,name,sliang,zliang,xzuang,jdu,qig"
                                + ")values('"
                                + j["cliid"].ToString().Trim() + "','"
                                + j["zfshi"].ToString().Trim() + "','"
                                + j["name"].ToString().Trim() + "',"
                                + j["sliang"].ToString() + ","
                                + j["zliang"].ToString() + ",'"
                                + j["xzuang"].ToString().Trim() + "','"
                                + j["jdu"].ToString().Trim() + "','"
                                + j["qig"].ToString().Trim() + "')", conn);
                            //stcmd.CommandTimeout = 60;
                            stcmd.ExecuteNonQuery();
                        }
                    }

                    tm = i["tm"].ToString();
                    jliao = i["jliao"].ToString();
                    sliao = i["sliao"].ToString();
                    ssi = i["ssi"].ToString();
                    if (i["qkou"].ToString() == "")
                    {
                        qkou = 0;
                    }
                    else
                    {
                        qkou = double.Parse(i["qkou"].ToString());
                    }

                    zshu = i["zshu"].ToString();
                    kus = i["kus"].ToString();
                    hhao = i["hhao"].ToString();
                    dwei = i["dwei"].ToString();
                    sliang = int.Parse(i["sliang"].ToString());
                    jianz = double.Parse(i["jianz"].ToString());
                    jinz = double.Parse(i["jinz"].ToString());
                    pjianz = double.Parse(i["pjianz"].ToString());
                    blu = double.Parse(i["blu"].ToString());
                    zsz = double.Parse(i["zsz"].ToString());
                    zss = int.Parse(i["zss"].ToString());
                    fsz = double.Parse(i["fsz"].ToString());
                    fss = int.Parse(i["fss"].ToString());
                    cbei = double.Parse(i["cbei"].ToString());
                    xsou = double.Parse(i["xsou"].ToString());
                    bzhu = i["bzhu"].ToString();
                    slbol = bool.Parse(i["slbol"].ToString());
                    imgbol = bool.Parse(i["imgbol"].ToString());
                    jdu = i["jdu"].ToString();
                    yse = i["yse"].ToString();
                    xzuang = i["xzuang"].ToString();
                    qgong = i["qgong"].ToString();
                    jja = i["jja"].ToString();
                    pjja = i["pjja"].ToString();
                    zsja = i["zsja"].ToString();
                    zsje = i["zsje"].ToString();
                    fsja = i["fsja"].ToString();
                    fsje = i["fsje"].ToString();
                    jgdj = i["jgdj"].ToString();
                    other = i["other"].ToString();
                    jgsh = i["jgsh"].ToString();

                    SqlCommand cmd = new SqlCommand("QZ_GOODSADD", conn);
                    //cmd.CommandTimeout = 60;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@tm", tm));
                    cmd.Parameters.Add(new SqlParameter("@jliao", jliao));
                    cmd.Parameters.Add(new SqlParameter("@sliao", sliao));
                    cmd.Parameters.Add(new SqlParameter("@ssi", ssi));
                    cmd.Parameters.Add(new SqlParameter("@qkou", qkou));
                    cmd.Parameters.Add(new SqlParameter("@zshu", zshu));
                    cmd.Parameters.Add(new SqlParameter("@kus", kus));
                    cmd.Parameters.Add(new SqlParameter("@hhao", hhao));
                    cmd.Parameters.Add(new SqlParameter("@dwei", dwei));
                    cmd.Parameters.Add(new SqlParameter("@sliang", sliang));
                    cmd.Parameters.Add(new SqlParameter("@jianz", jianz));
                    cmd.Parameters.Add(new SqlParameter("@jinz", jinz));
                    cmd.Parameters.Add(new SqlParameter("@pjianz", pjianz));
                    cmd.Parameters.Add(new SqlParameter("@blu", blu));
                    cmd.Parameters.Add(new SqlParameter("@zsz", zsz));
                    cmd.Parameters.Add(new SqlParameter("@zss", zss));
                    cmd.Parameters.Add(new SqlParameter("@fsz", fsz));
                    cmd.Parameters.Add(new SqlParameter("@fss", fss));
                    cmd.Parameters.Add(new SqlParameter("@cbei", cbei));
                    cmd.Parameters.Add(new SqlParameter("@xsou", xsou));
                    cmd.Parameters.Add(new SqlParameter("@bzhu", bzhu));
                    cmd.Parameters.Add(new SqlParameter("@slbol", slbol));
                    cmd.Parameters.Add(new SqlParameter("@imgbol", imgbol));
                    cmd.Parameters.Add(new SqlParameter("@jdu", jdu));
                    cmd.Parameters.Add(new SqlParameter("@yse", yse));
                    cmd.Parameters.Add(new SqlParameter("@xzuang", xzuang));
                    cmd.Parameters.Add(new SqlParameter("@qgong", qgong));
                    cmd.Parameters.Add(new SqlParameter("@cadi", cadi));
                    cmd.Parameters.Add(new SqlParameter("@rkdh", rkdh));
                    cmd.Parameters.Add(new SqlParameter("@ddh", ddh));
                    cmd.Parameters.Add(new SqlParameter("@gys", gys));
                    cmd.Parameters.Add(new SqlParameter("@kuc", kuc));
                    cmd.Parameters.Add(new SqlParameter("@user", user));
                    cmd.Parameters.Add(new SqlParameter("@jja", jja));
                    cmd.Parameters.Add(new SqlParameter("@pjja", pjja));
                    cmd.Parameters.Add(new SqlParameter("@zsja", zsja));
                    cmd.Parameters.Add(new SqlParameter("@zsje", zsje));
                    cmd.Parameters.Add(new SqlParameter("@fsja", fsja));
                    cmd.Parameters.Add(new SqlParameter("@fsje", fsje));
                    cmd.Parameters.Add(new SqlParameter("@jgdj", jgdj));
                    cmd.Parameters.Add(new SqlParameter("@other", other));
                    cmd.Parameters.Add(new SqlParameter("@jgsh", jgsh));
                    cmd.ExecuteNonQuery();
                    //Thread.Sleep(10);

                }

                conn.Close();

                OleDbConnection oleconn;
                oleconn = new OleDbConnection(xconfig.oldstr("goods", "cnsdjian"));
                oleconn.Open();
                OleDbCommand olecmd;
                olecmd = new OleDbCommand("DELETE FROM GOODS", oleconn);
                olecmd.ExecuteNonQuery();
                olecmd = new OleDbCommand("DELETE FROM STONG", oleconn);
                olecmd.ExecuteNonQuery();
                oleconn.Close();

                if (this.InvokeRequired)
                {
                    d d = delegate
                    {
                        toolStripButton11.Enabled = false;
                        toolStripButton9.Enabled = true;
                        toolStripButton5.Enabled = true;
                        toolStripButton1.Enabled = false;
                        toolStripButton2.Enabled = false;
                        toolStripButton10.Enabled = false;
                        textBox1.ReadOnly = true;
                        textBox2.ReadOnly = true;
                        comboBox1.Enabled = false;
                        comboBox2.Enabled = false;
                    };
                    this.Invoke(d);
                }
                else
                {
                    toolStripButton11.Enabled = false;
                    toolStripButton9.Enabled = true;
                    toolStripButton5.Enabled = true;
                    toolStripButton1.Enabled = false;
                    toolStripButton2.Enabled = false;
                    toolStripButton10.Enabled = false;
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (this.InvokeRequired) { d d = delegate { toolStripButton3.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton3.Enabled = true; }
            }

            MSG.login.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dstGOODS.Rows.Count < 1)
            {
                return;
            }

            string msg = xconfig.USER + "|入库|CONNECT<EOF>";
            xconfig.netSend(msg);

            string tm = dataGridView1["tmDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            new 商品入库修改(dstGOODS, dataGridView1, tm).ShowDialog();
            this.Focus();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|入库|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msg);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请稍候再试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dstGOODS.Rows.Count < 1)
            {
                return;
            }

            string tm = dataGridView1["tmDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (tm == "")
            {
                return;
            }

            if(MessageBox.Show("即将删除条码号为：[" + tm + "]的入库数据！\n是否继续？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("goods", "cnsdjian"));
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("delete from goods where tm='" + tm + "'", conn);
                cmd.ExecuteNonQuery();
                cmd = new OleDbCommand("delete from stong where cliid='" + tm + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                DataRow[] dr = dstGOODS.Select("tm='" + tm + "'", "");
                foreach (DataRow i in dr)
                {
                    dstGOODS.Rows.Remove(i);
                }

                bool file = File.Exists(Application.StartupPath + "\\Item\\" + tm + ".bmp");
                if (file)
                {
                    File.Delete(Application.StartupPath + "\\Item\\" + tm + ".bmp");
                }

                config.conDGV.DGVAutoID(dataGridView1, "phao");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|入库|CONNECT<EOF>";
            xconfig.netSend(msg);
            panel3.Visible = true;
            string sl = dstGOODS.Compute("sum(sliang)", "").ToString();
            string zsz = dstGOODS.Compute("sum(zsz)", "").ToString();
            string fsz = dstGOODS.Compute("sum(fsz)", "").ToString();
            string zss = dstGOODS.Compute("sum(zss)", "").ToString();
            string fss = dstGOODS.Compute("sum(fss)", "").ToString();
            string cbei = dstGOODS.Compute("sum(cbei)", "").ToString();
            string xsou = dstGOODS.Compute("sum(xsou)", "").ToString();

            sllab.Text = sl;
            zsilab.Text = zsz + "/" + zss;
            fslab.Text = fsz + "/" + fss;
            label13.Text = cbei;
            label14.Text = xsou;

            ((ToolStripButton)sender).Enabled = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            toolStripButton6.Enabled = true;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!toolStripButton6.Enabled)
            {
                panel3.Visible = true;
                string sl = dstGOODS.Compute("sum(sliang)", "").ToString();
                string zsz = dstGOODS.Compute("sum(zsz)", "").ToString();
                string fsz = dstGOODS.Compute("sum(fsz)", "").ToString();
                string zss = dstGOODS.Compute("sum(zss)", "").ToString();
                string fss = dstGOODS.Compute("sum(fss)", "").ToString();
                string cbei = dstGOODS.Compute("sum(cbei)", "").ToString();
                string xsou = dstGOODS.Compute("sum(xsou)", "").ToString();

                sllab.Text = sl;
                zsilab.Text = zsz + "/" + zss;
                fslab.Text = fsz + "/" + fss;
                label13.Text = cbei;
                label14.Text = xsou;
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView1_RowsAdded(sender, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private delegate void dat();

        private void print()
        {
            try
            {
                GoldPrinter.ExcelAccess exl = new GoldPrinter.ExcelAccess();
                exl.Open(Application.StartupPath + @"\00R01.xlt");
                exl.IsVisibledExcel = false;
                exl.FormCaption = "启智软件";

                int datatemprow = 9;
                int datastartrow = 11;

                string time = DateTime.Now.ToShortDateString();
                exl.SetCellText(4, "C", time);
                exl.SetCellText(5, "C", time);
                if(rkdhbox.InvokeRequired)
                {
                    dat d = delegate{
                        exl.SetCellText(4,"K",rkdhbox.Text);
                    };
                    rkdhbox.Invoke(d);
                }else{
                    exl.SetCellText(4,"K",rkdhbox.Text);
                }
                if (comboBox1.InvokeRequired)
                {
                    dat d = delegate
                    {
                        exl.SetCellText(5, "J", comboBox1.Text);
                    };
                    comboBox1.Invoke(d);
                }
                else
                {
                    exl.SetCellText(5, "J", comboBox1.Text);
                }
                exl.SetCellText(13, "K", xconfig.USER);

                for (int i = 0; i < dstGOODS.Rows.Count; i++)
                {
                    exl.InsertRow(datatemprow, datatemprow);
                    //exl.SetRowHeight(datatemprow, 18.75f);
                }

                for (int i = 0; i < dstGOODS.Rows.Count; i++)
                {
                    exl.SetCellText(datastartrow + i, "A", dstGOODS.Rows[i]["tm"].ToString());
                    exl.SetCellText(datastartrow + i, "B", dstGOODS.Rows[i]["name"].ToString());
                    exl.SetCellText(datastartrow + i, "D", dstGOODS.Rows[i]["zshu"].ToString());
                    exl.SetCellText(datastartrow + i, "E", dstGOODS.Rows[i]["kus"].ToString());
                    exl.SetCellText(datastartrow + i, "F", dstGOODS.Rows[i]["sliang"].ToString());
                    exl.SetCellText(datastartrow + i, "G", dstGOODS.Rows[i]["dwei"].ToString());
                    exl.SetCellText(datastartrow + i, "K", dstGOODS.Rows[i]["cbei"].ToString());
                    exl.SetCellText(datastartrow + i, "L", dstGOODS.Rows[i]["xsou"].ToString());
                    exl.SetCellText(datastartrow + i, "I", dstGOODS.Rows[i]["zshi"].ToString());
                    exl.SetCellText(datastartrow + i, "J", dstGOODS.Rows[i]["fshi"].ToString());
                    exl.SetCellText(datastartrow + i, "H", dstGOODS.Rows[i]["jinz"].ToString());
                    //exl.SetCellText(datastartrow + i, "K", dstGOODS.Rows[i]["bzhu"].ToString());
                }

                exl.DeleteRow(10);
                exl.DeleteRow(9);
                exl.DeleteRow(8);

                MSG.login.Close();
                exl.PrintPreview();
                exl.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            MSG.login.Show("正在进行数据重组，请耐心等候.....");
            Application.DoEvents();

            Thread t = new Thread(new ThreadStart(print));
            t.Start();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|入库|CONNECT<EOF>";
            xconfig.netSend(msg);
            try
            {
                string data = null;
                mesk.xSocket mesks = new mesk.xSocket();
                string mess = "TEST|0|GETJHDB<EOF>";
                data = mesks.Send(mess, IPAddress.Parse(xconfig.SERVERIP), xconfig.SERVERPORT);
                //MessageBox.Show(data);
                string[] tem = data.Split('|');
                if (tem[0] == "NULL" || tem[0] == "ERROR")
                {
                    MessageBox.Show("金汇数据库连接出错", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                xconfig.JHSQLIP = tem[1];
                xconfig.JHSQLUSER = tem[2];
                xconfig.JHSQLPWD = tem[3];

            }
            catch
            {
                MessageBox.Show("未知错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //MSG.login.Show();
            //Application.DoEvents();
            new 数据导入(dstGOODS, dataGridView1).ShowDialog();
            //this.Focus();
        }

        private void 商品入库_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
            string msgstr = xconfig.USER + "|闲置|CONNECT<EOF>";
            xconfig.netSend(msgstr);
        }
    }
}