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
    public partial class 款式销售分析 : Form
    {
        DataTable panDT;
        DateTime panA;
        DateTime panB;
        Form panFORM;

        public 款式销售分析(DataTable t,DateTime a ,DateTime b,Form f)
        {
            panDT = t;
            panA = a;
            panB = b;
            panFORM = f;

            InitializeComponent();
        }

        private void 款式销售分析_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            DataView dv = new DataView();
            dv.Table = panDT;
            dv.Sort = "SLIANG";
            this.dataGridView1.DataSource = dv;

            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                if (i.Cells["kUSDataGridViewTextBoxColumn"].Value.ToString() == "总计:")
                {
                    i.DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 款式销售分析_FormClosed(object sender, FormClosedEventArgs e)
        {
            panFORM.TopMost = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 2)
            {
                return;
            }

            string kus = string.Empty;
            kus = dataGridView1["kUSDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (kus == "总计:")
            {
                return;
            }

            Form newform = new 报表.款式销售分析详细(panA, panB,kus);
            newform.MdiParent = this.MdiParent;
            newform.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 2)
            {
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            toolStripButton3.Enabled = false;

            Thread t = new Thread(new ThreadStart(isPrint));
            t.Start();
        }

        private delegate void d();

        private void isPrint()
        {
            try
            {
                GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                ext.Open(Application.StartupPath + @"\00S02.XLT");
                ext.FormCaption = "ColorBay";

                ext.SetCellText(4, "C", panA.ToShortDateString() + " 至 " + panB.ToShortDateString());
                ext.SetCellText(12, "L", xconfig.USER);

                int cr = 8;
                int dr = 10;

                foreach (DataRow i in panDT.Rows)
                {
                    if (i["KUS"].ToString() == "总计:")
                    {
                        continue;
                    }

                    ext.InsertRow(cr, cr);
                }

                foreach (DataRow i in panDT.Rows)
                {
                    if (i["KUS"].ToString() == "总计:")
                    {
                        continue;
                    }

                    ext.SetCellText(dr, "A", i["KUS"].ToString());
                    ext.SetCellText(dr, "D", i["SLIANG"].ToString());
                    ext.SetCellText(dr, "E", i["CBEI"].ToString());
                    ext.SetCellText(dr, "F", i["SSALE"].ToString());

                    dr++;
                }

                ext.DeleteRow(9);
                ext.DeleteRow(8);
                ext.DeleteRow(7);

                MSG.login.Close();

                ext.PrintPreview();
                ext.Close();

                if (this.InvokeRequired)
                {
                    d d = delegate
                    {
                        toolStripButton3.Enabled = true;
                    };
                    this.Invoke(d);
                }
                else
                {
                    toolStripButton3.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                
    }
}