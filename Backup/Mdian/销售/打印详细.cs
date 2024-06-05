using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Mdian.销售
{
    public partial class 打印详细 : Form
    {
        DataTable panDT;
        Form panFORM;
        string TIME = string.Empty;
        string SBM = string.Empty;
        string USER = string.Empty;
        string YHUI = string.Empty;
        string ZK = string.Empty;

        public 打印详细(DataTable dt,Form fr,string t,string s,string u,string y,string z)
        {
            TIME = t;
            SBM = s;
            USER = u;
            panDT = dt;
            panFORM = fr;
            YHUI = y;
            ZK = z;
            InitializeComponent();
        }

        private void 打印详细_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = panDT;

            toolStripTextBox1.Text = SBM;
            toolStripTextBox2.Text = TIME;
            toolStripTextBox3.Text = YHUI;
            toolStripTextBox4.Text = ZK;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 打印详细_FormClosed(object sender, FormClosedEventArgs e)
        {
            panFORM.TopMost = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            Thread t = new Thread(new ThreadStart(derived));
            t.Start();
        }

        private void derived()
        {
            try
            {
                GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                ext.Open(Application.StartupPath + @"\00I01.xlt");
                ext.FormCaption = "ColorBay";

                ext.SetCellText(1, "A", "条码");
                ext.SetCellText(1, "B", "品名");
                ext.SetCellText(1, "C", "证书");
                ext.SetCellText(1, "D", "款号");
                ext.SetCellText(1, "E", "件重");
                ext.SetCellText(1, "F", "金重");
                ext.SetCellText(1, "G", "配件重");
                ext.SetCellText(1, "H", "主石");
                ext.SetCellText(1, "I", "辅石");
                ext.SetCellText(1, "J", "售价");
                ext.SetCellText(1, "K", "折扣");
                ext.SetCellText(1, "L", "实售价");
                ext.SetCellText(1, "M", "数量");
                ext.SetCellText(1, "N", "单位");
                ext.SetCellText(1, "O", "金料");
                ext.SetCellText(1, "P", "石料");
                ext.SetCellText(1, "Q", "首饰类别");

                int j = 2;
                foreach (DataRow i in panDT.Rows)
                {
                    ext.SetCellText(j, "A", i["TM"].ToString());
                    ext.SetCellText(j, "B", i["NAME"].ToString());
                    ext.SetCellText(j, "C", i["ZSHU"].ToString());
                    ext.SetCellText(j, "D", i["KUS"].ToString());
                    ext.SetCellText(j, "E", i["JIANZ"].ToString());
                    ext.SetCellText(j, "F", i["JINZ"].ToString());
                    ext.SetCellText(j, "G", i["PJIANZ"].ToString());
                    ext.SetCellText(j, "H", i["ZSHI"].ToString());
                    ext.SetCellText(j, "I", i["FSHI"].ToString());
                    ext.SetCellText(j, "J", i["XSOU"].ToString());
                    ext.SetCellText(j, "K", i["ZK"].ToString());
                    ext.SetCellText(j, "L", i["SXSOU"].ToString());
                    ext.SetCellText(j, "M", i["SLIANG"].ToString());
                    ext.SetCellText(j, "N", i["DWEI"].ToString());
                    ext.SetCellText(j, "O", i["JLIAO"].ToString());
                    ext.SetCellText(j, "P", i["SLIAO"].ToString());
                    ext.SetCellText(j, "Q", i["SSI"].ToString());
                    j++;
                }

                ext.ShowExcel();

                MSG.login.Close();
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            Thread t = new Thread(new ThreadStart(print));
            t.Start();

        }

        private void print()
        {
            try
            {
                GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                ext.Open(Application.StartupPath + @"\00X01.xlt");
                ext.FormCaption = "ColorBay";

                ext.SetCellText(4, "B", TIME);
                ext.SetCellText(4, "K", SBM);
                ext.SetCellText(12, "K", USER);

                int copyrow = 8;
                int r = 10;

                foreach (DataRow i in panDT.Rows)
                {
                    ext.InsertRow(copyrow, copyrow);
                }

                foreach (DataRow i in panDT.Rows)
                {
                    ext.SetCellText(r, "A", i["TM"].ToString());
                    ext.SetCellText(r, "B", i["NAME"].ToString());
                    ext.SetCellText(r, "C", i["ZSHU"].ToString());
                    ext.SetCellText(r, "D", i["SLIANG"].ToString());
                    ext.SetCellText(r, "E", i["DWEI"].ToString());
                    ext.SetCellText(r, "F", i["KUS"].ToString());
                    ext.SetCellText(r, "G", i["JINZ"].ToString());
                    ext.SetCellText(r, "H", i["ZSHI"].ToString());
                    ext.SetCellText(r, "I", i["FSHI"].ToString());
                    ext.SetCellText(r, "J", i["XSOU"].ToString());
                    ext.SetCellText(r, "K", i["ZK"].ToString());
                    ext.SetCellText(r, "L", i["SXSOU"].ToString());
                    r++;
                }

                ext.DeleteRow(9);
                ext.DeleteRow(8);
                ext.DeleteRow(7);

                MSG.login.Close();
                ext.PrintPreview();
                ext.Close();
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                bool imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                toolStripButton4.Enabled = imgbol;
                string TM = ((DataGridView)sender)["tmDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

                if (panel1.Visible)
                {
                    xconfig.GetIMG(xconfig.USER + "|盘点|GETBMP|" + TM + "<EOF>", pictureBox1, imgbol, TM, tmLabel, jdLabel);
                }
            }
            catch { }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

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
            

    }
}