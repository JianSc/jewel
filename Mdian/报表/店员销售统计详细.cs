using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mdian.报表
{
    public partial class 店员销售统计详细 : Form
    {
        Button panBT;
        Form panFORM;
        DateTime a;
        DateTime b;
        DataTable panDT;

        public 店员销售统计详细(Button bt,Form fm,DateTime a_items,DateTime b_items,DataTable dt)
        {
            panDT = dt;
            panBT = bt;
            panFORM = fm;
            a = a_items;
            b = b_items;
            InitializeComponent();
        }

        private void 店员销售统计详细_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = panDT;
            config.conDGV.DGVAutoID(dataGridView1, "phao");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView1, "phao");
        }

        private void 店员销售统计详细_FormClosed(object sender, FormClosedEventArgs e)
        {
            panBT.Enabled = true;
            panFORM.TopMost = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            try
            {
                GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                ext.Open(Application.StartupPath + @"\00I01.xlt");
                ext.FormCaption = "ColorBay";

                ext.SetCellText(1, 1, "日期");
                ext.SetCellText(1, 2, "员工");
                ext.SetCellText(1, 3, "销售数量");
                ext.SetCellText(1, 4, "销售金额");

                int j = 2;
                foreach (DataGridViewRow i in dataGridView1.Rows)
                {
                    ext.SetCellText(j, 1, i.Cells[1].Value.ToString());
                    ext.SetCellText(j, 2, i.Cells[2].Value.ToString());
                    ext.SetCellText(j, 3, i.Cells[3].Value.ToString());
                    ext.SetCellText(j, 4, i.Cells[4].Value.ToString());
                    j++;
                }

                ext.ShowExcel();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            string username = dataGridView1["uSERDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (username == string.Empty)
            {
                return;
            }

            Form itemform = new 店员销售详细S(a, b, username);
            itemform.MdiParent = this.MdiParent;
            itemform.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 查看详细销售记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton5_Click(toolStripButton5, new EventArgs());
        }
    }
}