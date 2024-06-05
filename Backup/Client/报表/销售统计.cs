using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Client.报表
{
    public partial class 销售统计 : Form
    {
        DataTable panDT;
        Form panFR;

        public 销售统计(DataTable t,Form f)
        {
            panFR = f;
            panDT = t;
            InitializeComponent();
        }

        private void 销售统计_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            dataGridView1.DataSource = panDT;

            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                if (i.Cells["tMDataGridViewTextBoxColumn"].Value.ToString() == string.Empty)
                {
                    i.DefaultCellStyle.BackColor = Color.SkyBlue;
                }
            }
        }

        private void 销售统计_FormClosed(object sender, FormClosedEventArgs e)
        {
            panFR.TopMost = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            toolStripButton2.Enabled = false;

            MSG.login.ShowIMG();
            Application.DoEvents();

            Thread t = new Thread(new ThreadStart(show));
            t.Start();

        }

        private delegate void d();
        private void show()
        {
            GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
            ext.Open(Application.StartupPath + @"\00I01.xlt");
            ext.FormCaption = "ColorBay";

            ext.SetCellText(1, "A", "条码");
            ext.SetCellText(1, "B", "品名");
            ext.SetCellText(1, "C", "金料");
            ext.SetCellText(1, "D", "石料");
            ext.SetCellText(1, "E", "首饰类别");
            ext.SetCellText(1, "F", "件重");
            ext.SetCellText(1, "G", "金重");
            ext.SetCellText(1, "H", "主石");
            ext.SetCellText(1, "I", "辅石");
            ext.SetCellText(1, "J", "成本");
            ext.SetCellText(1, "K", "售价");
            ext.SetCellText(1, "L", "折扣");
            ext.SetCellText(1, "M", "实售价");
            ext.SetCellText(1, "N", "销售日期");
            ext.SetCellText(1, "O", "证书");
            ext.SetCellText(1, "P", "门店名称");
            ext.SetCellText(1, "Q", "单据识别码");
            ext.SetCellText(1, "R", "数量");
            ext.SetCellText(1, "S", "单位");
            ext.SetCellText(1, "T", "客户");
            ext.SetCellText(1, "U", "操作员");

            int r = 2;
            foreach (DataRow i in panDT.Rows)
            {
                if (i["TM"].ToString() != string.Empty)
                {
                    ext.SetCellText(r, "A", i["TM"].ToString());
                    ext.SetCellText(r, "B", i["NAME"].ToString());
                    ext.SetCellText(r, "C", i["JLIAO"].ToString());
                    ext.SetCellText(r, "D", i["SLIAO"].ToString());
                    ext.SetCellText(r, "E", i["SSI"].ToString());
                    ext.SetCellText(r, "F", i["JIANZ"].ToString());
                    ext.SetCellText(r, "G", i["JINZ"].ToString());
                    ext.SetCellText(r, "H", i["ZSHI"].ToString());
                    ext.SetCellText(r, "I", i["FSHI"].ToString());
                    ext.SetCellText(r, "J", i["CBEI"].ToString());
                    ext.SetCellText(r, "K", i["SALE"].ToString());
                    ext.SetCellText(r, "L", i["ZK"].ToString());
                    ext.SetCellText(r, "M", i["SSALE"].ToString());
                    ext.SetCellText(r, "N", i["SETTIME"].ToString());
                    ext.SetCellText(r, "O", i["ZSHU"].ToString());
                    ext.SetCellText(r, "P", i["MDIAN"].ToString());
                    ext.SetCellText(r, "Q", i["SBM"].ToString());
                    ext.SetCellText(r, "R", i["SLIANG"].ToString());
                    ext.SetCellText(r, "S", i["DWEI"].ToString());
                    ext.SetCellText(r, "T", i["KHU"].ToString());
                    ext.SetCellText(r, "U", i["USER"].ToString());
                    r++;
                }
            }
            ext.ShowExcel();

            MSG.login.CloseIMG();

            if (this.InvokeRequired) { d d = delegate { toolStripButton2.Enabled = true; }; this.Invoke(d); }
            else { toolStripButton2.Enabled = true; }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (((DataGridView)sender).Rows.Count < 1)
                {
                    return;
                }

                bool imgbol = bool.Parse(((DataGridView)sender)["iMGBOLDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                string TM = ((DataGridView)sender)["tMDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

                toolStripButton3.Enabled = imgbol;

                if (panel2.Visible)
                {
                    if (imgbol)
                    {
                        Image img;
                        Bitmap bmp;
                        img = xconfig.netImgGET(xconfig.USER + "|报表|GETBMP|" + TM + "<EOF>");
                        bmp = new Bitmap(img, pictureBox1.Size);
                        pictureBox1.Image = bmp;
                    }
                    else
                    {
                        pictureBox1.Image = Properties.Resources.ImgGet_Err;
                    }
                }
            }
            catch { }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
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
    }
}