using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace Mdian.盘点
{
    public partial class 盘亏表 : Form
    {
        DataTable panDT;
        string panCKU;

        public 盘亏表(DataTable dt,string cku)
        {
            panDT = dt;
            panCKU = cku;
            InitializeComponent();
        }

        private void 盘亏表_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = panDT;
            config.conDGV.DGVAutoID(dataGridView1, "phao");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!panel1.Visible)
            {
                panel1.Visible = true;
            }
            if (panDT.Rows.Count < 1)
            {
                return;
            }
            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            try
            {
                bool imgbol;
                imgbol = bool.Parse(((DataGridView)sender)["dataGridViewCheckBoxColumn2", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());

                string TM;
                TM = ((DataGridView)sender)["dataGridViewTextBoxColumn2", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

                toolStripButton3.Enabled = imgbol;

                if (panel1.Visible)
                {
                    xconfig.GetIMG(xconfig.USER + "|盘点|GETBMP|" + TM + "<EOF>", pictureBox1, imgbol, TM, tmLabel, jdLabel);
                }
            }
            catch { }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
           if(panDT.Rows.Count <1)
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
                foreach (DataRow i in panDT.Rows)
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (panDT.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show("正在进行数据重组，请耐心等候.....");
            Application.DoEvents();

            toolStripButton1.Enabled = false;
            Thread t = new Thread(new ThreadStart(print));
            t.Start();
        }

        private delegate void d();

        private void print()
        {
            try
            {
                GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                ext.Open(Application.StartupPath + @"\00P02.xlt");
                ext.FormCaption = "ColorBay";

                ext.SetCellText(4, "B", DateTime.Now.ToShortDateString());
                ext.SetCellText(12, "K", xconfig.USER);

                int rowcopy = 8;
                int r = 10;
                foreach (DataRow i in panDT.Rows)
                {
                    ext.InsertRow(rowcopy, rowcopy);
                }

                foreach (DataRow i in panDT.Rows)
                {
                    ext.SetCellText(r, "A", i["TM"].ToString());
                    ext.SetCellText(r, "B", i["NAME"].ToString());
                    ext.SetCellText(r, "C", i["KUS"].ToString());
                    ext.SetCellText(r, "D", i["SLIANG"].ToString());
                    ext.SetCellText(r, "E", i["DWEI"].ToString());
                    ext.SetCellText(r, "F", i["JIANZ"].ToString());
                    ext.SetCellText(r, "G", i["JINZ"].ToString());
                    ext.SetCellText(r, "H", i["PJIANZ"].ToString());
                    ext.SetCellText(r, "I", i["ZSHI"].ToString());
                    ext.SetCellText(r, "J", i["FSHI"].ToString());
                    ext.SetCellText(r, "K", i["XSOU"].ToString());
                    r++;
                }

                ext.DeleteRow(9);
                ext.DeleteRow(8);
                ext.DeleteRow(7);

                MSG.login.Close();

                ext.PrintPreview();
                ext.Close();

                if (this.InvokeRequired) { d d = delegate { toolStripButton1.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton1.Enabled = true; }
            }
            catch (Exception ex)
            {
                if (this.InvokeRequired) { d d = delegate { toolStripButton1.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton1.Enabled = true; }
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}