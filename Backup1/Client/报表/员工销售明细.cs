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
    public partial class 员工销售明细 : Form
    {
        DataTable panDT;

        public 员工销售明细(DataTable d)
        {
            panDT = d;
            InitializeComponent();
        }

        private void 员工销售明细_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = panDT;

            config.conDGV.DGVAutoID(dataGridView1, "phao");

            DataRow r = panDT.NewRow();
            r["TM"] = "总计:";
            r["SSALE"] = panDT.Compute("SUM(SSALE)", "").ToString();
            r["CBEI"] = panDT.Compute("SUM(CBEI)", "").ToString();
            r["SLIANG"] = panDT.Compute("SUM(SLIANG)", "").ToString();
            panDT.Rows.Add(r);

            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                if (i.Cells["tMDataGridViewTextBoxColumn"].Value.ToString() == "总计:")
                {
                    i.DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            MSG.login.ShowIMG();
            Application.DoEvents();

            toolStripButton2.Enabled = false;
            Thread t = new Thread(new ThreadStart(show));
            t.Start();
        }

        private delegate void d();
        private void show()
        {
            DataGridView g = new DataGridView();
            if (dataGridView1.InvokeRequired)
            {
                d d = delegate
                {
                    g = dataGridView1;
                };
                dataGridView1.Invoke(d);
            }
            else
            {
                g = dataGridView1;
            }

            GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
            ext.Open(Application.StartupPath + @"\00I01.xlt");
            ext.FormCaption = "ColorBay";

            for (int i = 1; i <= g.Columns.Count; i++)
            {
                if (i == 1||i==4) { continue; }
                ext.SetCellText(1, i,g.Columns[i-1].HeaderText);
            }

            int r = 2;
            foreach (DataGridViewRow i in g.Rows)
            {
                if (i.Cells["tMDataGridViewTextBoxColumn"].Value.ToString() == "总计:")
                {
                    continue;
                }

                for (int j = 1; j <= g.Columns.Count; j++)
                {
                    if (j == 1||j==4) { continue; }
                    ext.SetCellText(r, j, i.Cells[j - 1].Value.ToString());
                }
                r++;
            }

            ext.DeleteColumn(4);
            ext.DeleteColumn(1);

            if (this.InvokeRequired) { d d = delegate { toolStripButton2.Enabled = true; }; this.Invoke(d); }
            else { toolStripButton2.Enabled = true; }

            MSG.login.CloseIMG();
            ext.ShowExcel();
            //ext.Close();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            try
            {
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
            if (!panel2.Visible)
            {
                panel2.Visible = true;
            }

            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

    }
}