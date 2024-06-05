using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace Mdian.销售
{
    public partial class 销售打印总览 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable thisDT = new theDST().Tables["销售总览"];
        DataTable sdt = new theDST().Tables["销售表"];
        Form panFORM;

        public 销售打印总览(ToolStripMenuItem menu,Form fr)
        {
            panFORM = fr;
            panMENU = menu;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 销售打印总览_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            Thread newtrd = new Thread(new ThreadStart(datafill));
            newtrd.Start();
            
        }

        private delegate void tdl();

        private void datafill()
        {
            try
            {
                Thread.Sleep(500);

                 string constr, cmdstr;
                constr = xconfig.CONNSTR;
                cmdstr = "SELECT A.SETTIME,SUM(A.SLIANG) AS SLIANG,SUM(A.SSALE) AS SSALE,A.SBM,A.[USER],B.YH AS YHUI,C.ZK AS USERZK FROM ("
                +"SELECT CONVERT(CHAR(10),SETTIME,120) AS SETTIME,SLIANG,SSALE,RTRIM(SBM) AS SBM,RTRIM([USER]) AS [USER] FROM "
                +"QGOODS_SALES WHERE MDIAN='"+xconfig.MDIAN+"') A INNER JOIN QGOODS_YHUI B ON A.SBM=B.SBM INNER JOIN "
                +"QGOODS_CUST_ZK C ON A.SBM=C.SBM GROUP BY A.SETTIME,A.SBM,A.[USER],B.YH,C.ZK";
                config.conData.fill("sql", constr, cmdstr, thisDT);

                if (dataGridView1.InvokeRequired)
                {
                    tdl d = delegate
                    {
                        DataView dv = new DataView();
                        dv.Table = thisDT;
                        dv.Sort = "id desc";
                        dataGridView1.DataSource = dv;

                        config.conDGV.DGVAutoID(dataGridView1, "phao");
                        if (dataGridView1.Rows.Count > 9)
                        {
                            if (pictureBox1.InvokeRequired)
                            {
                                tdl dd = delegate { pictureBox1.Visible = false; };
                                pictureBox1.Invoke(dd);
                            }
                            else { pictureBox1.Visible = false; }
                        }
                    };
                    dataGridView1.Invoke(d);
                }
                if (this.InvokeRequired)
                {
                    tdl d = delegate { this.Cursor = Cursors.Default; };
                    this.Invoke(d);
                }
                //if (button1.InvokeRequired)
                //{
                //    tdl d = delegate { button1.Enabled = true; };
                //    button1.Invoke(d);
                //}
                //if (button2.InvokeRequired)
                //{
                //    tdl d = delegate { button2.Enabled = true; };
                //    button2.Invoke(d);
                //}
                //if (button3.InvokeRequired)
                //{
                //    tdl d = delegate { button3.Enabled = true; };
                //    button3.Invoke(d);
                //}

                if (panel1.InvokeRequired)
                {
                    tdl d = delegate { panel1.Visible = false; };
                    panel1.Invoke(d);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 销售打印总览_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        bool printbol = false;
        string SBM=string.Empty;
        string user = string.Empty;
        string time = string.Empty;
        string YHUIs = string.Empty;
        string ZKs = string.Empty;

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            DataGridViewRow i = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            SBM = i.Cells["sBMDataGridViewTextBoxColumn"].Value.ToString();
            user = i.Cells["uSERDataGridViewTextBoxColumn"].Value.ToString();
            time = i.Cells["sETTIMEDataGridViewTextBoxColumn"].Value.ToString();
            YHUIs = i.Cells["YHUI"].Value.ToString();
            ZKs = i.Cells["USERZK"].Value.ToString();
            
            panel1.Visible = true;
            this.Cursor = Cursors.WaitCursor;

            button3.Enabled = false;
            printbol = true;
            Thread t = new Thread(new ThreadStart(print));
            t.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            DataGridViewRow i = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            SBM = i.Cells["sBMDataGridViewTextBoxColumn"].Value.ToString();
            user = i.Cells["uSERDataGridViewTextBoxColumn"].Value.ToString();
            time = i.Cells["sETTIMEDataGridViewTextBoxColumn"].Value.ToString();
            YHUIs = i.Cells["YHUI"].Value.ToString();
            ZKs = i.Cells["USERZK"].Value.ToString();
            panel1.Visible = true;
            this.Cursor = Cursors.WaitCursor;

            button1.Enabled = false;
            printbol = false;
            Thread t = new Thread(new ThreadStart(print));
            t.Start();
        }

        private delegate void d();
        private void print()
        {
            try
            {
                string constr, cmdstr;
                constr = xconfig.CONNSTR;
                cmdstr = "SELECT RTRIM(QGOODS_SALES.TM) AS TM,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,JIANZ,JINZ,PJIANZ,QGOODS_SALES.SALE AS XSOU,IMGBOL,RTRIM(JDU) AS JDU,RTRIM(YSE) AS YSE,"
                + "RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,ZSS,ZSZ,FSS,FSZ,QGOODS_SALES.SSALE AS SXSOU,QGOODS_SALES.ZKOU AS ZK,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QGOODS_SALES.SLIANG,RTRIM(QGOODS_SALES.DWEI) AS DWEI "
                + "FROM QGOODS INNER JOIN QGOODS_SALES ON QGOODS.TM=QGOODS_SALES.TM WHERE (QGOODS_SALES.MDIAN='" + xconfig.MDIAN + "') AND (SBM='" + SBM + "')";
                config.conData.fill("sql", constr, cmdstr, sdt);
                foreach (DataRow i in sdt.Rows)
                {
                    i["ZSHI"] = i["ZSZ"].ToString() + "/" + i["ZSS"].ToString();
                    i["FSHI"] = i["FSZ"].ToString() + "/" + i["FSS"].ToString();
                }
                if (this.InvokeRequired)
                {
                    tdl d = delegate { this.TopMost = false; };
                    this.Invoke(d);
                }
                else
                {
                    this.TopMost = false;
                }

                if (this.InvokeRequired)
                {
                    tdl d = delegate { this.TopMost = false; };
                    this.Invoke(d);
                }
                if (printbol)
                {
                    if (this.InvokeRequired)
                    {
                        tdl d = delegate
                        {
                            Form itemform = new 打印详细(sdt, this, time, SBM, user, YHUIs, ZKs);
                            itemform.MdiParent = panFORM;
                            itemform.Show();
                        };
                        this.Invoke(d);
                    }
                }
                else
                {

                    GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                    ext.Open(Application.StartupPath + @"\00X03.xlt");
                    ext.FormCaption = "Colorbay";

                    int l1 = 12;
                    int l2 = 30;
                    int l3 = 47;

                    //string dh = toolStripTextBox1.Text;

                    ext.SetCellText(6, "C", xconfig.MDIAN);
                    ext.SetCellText(7, "C", xconfig.TEL);
                    ext.SetCellText(8, "C", xconfig.DZHI);
                    ext.SetCellText(6, "G", SBM);
                    ext.SetCellText(7, "G", time);
                    ext.SetCellText(8, "G", user);

                    ext.SetCellText(25, "C", xconfig.MDIAN);
                    ext.SetCellText(26, "C", xconfig.TEL);
                    ext.SetCellText(27, "C", xconfig.DZHI);
                    ext.SetCellText(25, "G", SBM);
                    ext.SetCellText(26, "G", time);
                    ext.SetCellText(27, "G", user);

                    ext.SetCellText(42, "C", xconfig.MDIAN);
                    ext.SetCellText(43, "C", xconfig.TEL);
                    ext.SetCellText(44, "C", xconfig.DZHI);
                    ext.SetCellText(42, "G", SBM);
                    ext.SetCellText(43, "G", time);
                    ext.SetCellText(44, "G", user);

                    foreach (DataRow i in sdt.Rows)
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
                    HJ = sdt.Compute("SUM(SXSOU)", "TM<>''").ToString();
                    double H1, H2, H3;
                    try
                    {
                        H1 = double.Parse(YHUIs);
                    }
                    catch { H1 = 0; }
                    try
                    {
                        H2 = double.Parse(ZKs);
                    }
                    catch { H2 = 0; }
                    try
                    {
                        H3 = double.Parse(HJ);
                    }
                    catch { H3 = 0; }
                    double H4 = (H3 - H1) * H2;
                    int H5 = int.Parse(H4.ToString()) ;

                    ext.SetCellText(21, "G", H5.ToString());
                    ext.SetCellText(38, "G", H5.ToString());
                    ext.SetCellText(55, "G", H5.ToString());

                    string msg = string.Empty;
                    if (YHUIs != string.Empty && YHUIs != "0")
                    {
                        msg += "顾客优惠:" + YHUIs + "元";
                    }
                    if (YHUIs != string.Empty && ZKs != string.Empty)
                    {
                        msg += "|";
                    }
                    if (ZKs != string.Empty && ZKs != "1")
                    {
                        msg += "顾客折扣:" + ZKs;
                    }

                    ext.SetCellText(19, "B", msg);
                    ext.SetCellText(37, "B", msg);
                    ext.SetCellText(54, "B", msg);

                    MSG.login.CloseIMG();
                    Application.DoEvents();
                    ext.Print();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (this.InvokeRequired)
                {
                    tdl d = delegate { this.TopMost = true; };
                    this.Invoke(d);
                }
            }

            if (this.InvokeRequired)
            {
                tdl d = delegate { this.Cursor = Cursors.Default; };
                this.Invoke(d);
            }
            if (panel1.InvokeRequired)
            {
                tdl d = delegate { panel1.Visible = false; };
                panel1.Invoke(d);
            }
            if (button3.InvokeRequired)
            {
                tdl d = delegate { button3.Enabled = true; };
                button3.Invoke(d);
            }
            if (button1.InvokeRequired)
            {
                tdl d = delegate { button1.Enabled = true; };
                button1.Invoke(d);
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView1, "phao");
        }
                
    }
}