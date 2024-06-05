using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Client
{
    public partial class 综合查询打印详细 : Form
    {
        DataTable panDST;

        public 综合查询打印详细(DataTable dst)
        {
            panDST = dst;
            InitializeComponent();
        }

        private void 综合查询打印详细_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = panDST;
            config.conDGV.DGVAutoID(dataGridView1, "phao");
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            bool dgvbol;
            dgvbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
            toolStripButton3.Enabled = dgvbol;

            string TM = ((DataGridView)sender)["tmDataGridViewTextBoxColumn",((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

            if (panel1.Visible)
            {
                if (dgvbol)
                {
                    Image itemimg;
                    Bitmap itembmp;
                    itemimg = xconfig.netImgGET(xconfig.USER + "|综合查询|GETBMP|" + TM + "<EOF>");
                    itembmp = new Bitmap(itemimg, pictureBox1.Size);
                    pictureBox1.Image = itembmp;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.ImgGet_Err;
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!panel1.Visible)
            {
                panel1.Visible = true;
            }

            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (panDST.Rows.Count < 1)
            {
                return;
            }

            MSG.login.ShowIMG();
            Application.DoEvents();
            toolStripButton2.Enabled = false;

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

                DataTable cliDST = panDST;

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

                if (this.InvokeRequired) { d d = delegate { toolStripButton2.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton2.Enabled = true; }
            }
            catch (Exception ex)
            {
                MSG.login.CloseIMG();
                if (this.InvokeRequired) { d d = delegate { toolStripButton2.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton2.Enabled = true; }
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (panDST.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show("正在写入数据，请稍候.....");
            Application.DoEvents();

            try
            {
                GoldPrinter.ExcelAccess newExt = new GoldPrinter.ExcelAccess();
                newExt.Open(Application.StartupPath + @"\00I01.xlt");
                newExt.FormCaption = "ColorBay";

                newExt.SetCellText(1, "A", "条码");
                newExt.SetCellText(1, "B", "品名");
                newExt.SetCellText(1, "C", "证书");
                newExt.SetCellText(1, "D", "款号");
                newExt.SetCellText(1, "E", "货号");
                newExt.SetCellText(1, "F", "数量");
                newExt.SetCellText(1, "G", "单位");
                newExt.SetCellText(1, "H", "圈口");
                newExt.SetCellText(1, "I", "件重");
                newExt.SetCellText(1, "J", "金重");
                newExt.SetCellText(1, "K", "配件重");
                newExt.SetCellText(1, "L", "主石");
                newExt.SetCellText(1, "M", "辅石");
                newExt.SetCellText(1, "N", "成本");
                newExt.SetCellText(1, "O", "售价");
                newExt.SetCellText(1, "P", "颜色");
                newExt.SetCellText(1, "Q", "形状");
                newExt.SetCellText(1, "R", "切工");
                newExt.SetCellText(1, "S", "净度");
                newExt.SetCellText(1, "T", "备注");

                newExt.SetRowHeight(1, 18.75f);
                newExt.SetColumnWidth("B", 22f);

                int j = 0;
                foreach (DataRow i in panDST.Rows)
                {
                    int k = 2;
                    newExt.SetCellText(k + j, "A", i["TM"].ToString());
                    newExt.SetCellText(k + j, "B", i["NAME"].ToString());
                    newExt.SetCellText(k + j, "C", i["ZSHU"].ToString());
                    newExt.SetCellText(k + j, "D", i["KUS"].ToString());
                    newExt.SetCellText(k + j, "E", i["HHAO"].ToString());
                    newExt.SetCellText(k + j, "F", i["SLIANG"].ToString());
                    newExt.SetCellText(k + j, "G", i["DWEI"].ToString());
                    newExt.SetCellText(k + j, "H", i["QKOU"].ToString());
                    newExt.SetCellText(k + j, "I", i["JIANZ"].ToString());
                    newExt.SetCellText(k + j, "J", i["JINZ"].ToString());
                    newExt.SetCellText(k + j, "K", i["PJIANZ"].ToString());
                    newExt.SetCellText(k + j, "L", i["ZSHI"].ToString());
                    newExt.SetCellText(k + j, "M", i["FSHI"].ToString());
                    newExt.SetCellText(k + j, "N", i["CBEI"].ToString());
                    newExt.SetCellText(k + j, "O", i["XSOU"].ToString());
                    newExt.SetCellText(k + j, "P", i["YSE"].ToString());
                    newExt.SetCellText(k + j, "Q", i["XZUANG"].ToString());
                    newExt.SetCellText(k + j, "R", i["QGONG"].ToString());
                    newExt.SetCellText(k + j, "S", i["JDU"].ToString());
                    newExt.SetCellText(k + j, "T", i["BZHU"].ToString());

                    newExt.SetRowHeight(k + j, 18.75f);
                    j++;
                }

                MSG.login.Close();
                newExt.ShowExcel();
                //newExt.Close();
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }
            MSG.login.Close();

        }
    }
}