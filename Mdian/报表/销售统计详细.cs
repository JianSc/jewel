using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mdian.报表
{
    public partial class 销售统计详细 : Form
    {
        DataTable panDT;
        string panDH;

        public 销售统计详细(DataTable dt,string dh)
        {
            panDT = dt;
            panDH = dh;
            InitializeComponent();
        }

        private void 销售统计详细_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = panDT;
            toolStripTextBox1.Text = panDH;

            config.conDGV.DGVAutoID(dataGridView1, "phao");
            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                if (i.Cells["tMDataGridViewTextBoxColumn"].Value.ToString() == string.Empty)
                {
                    i.DefaultCellStyle.BackColor = Color.Coral;
                }
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }
            try
            {
                bool imgbol = bool.Parse(((DataGridView)sender)["iMGBOLDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                string TM = ((DataGridView)sender)["tMDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();
                toolStripButton4.Enabled = imgbol;
                if (panel1.Visible)
                {
                    xconfig.GetIMG(xconfig.USER + "|报表|GETBMP|" + TM + "<EOF>", pictureBox1, imgbol, TM, tmLabel, jdLabel);
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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            try
            {
                GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                ext.Open(Application.StartupPath + @"\00I01.xlt");
                ext.FormCaption = "ColorBay";
                ext.SetCellText (1,"A","条码");
                ext.SetCellText(1,"B","品名");
                ext.SetCellText(1,"C","售价");
                ext.SetCellText(1,"D","实售价");
                ext.SetCellText(1,"E","日期");
                ext.SetCellText(1,"F","员工");
                ext.SetCellText(1,"G","客户");
                ext.SetCellText(1,"H","识别码");
                ext.SetCellText(1,"I","折扣");
                ext.SetCellText(1,"J","数量");
                ext.SetCellText(1,"K","单位");

                int j=2;
                foreach(DataRow i in panDT.Rows)
                {
                    if (i["TM"].ToString() == string.Empty)
                    {
                        continue;
                    }
                    ext.SetCellText(j,"A",i["TM"].ToString());
                    ext.SetCellText(j,"B",i["NAME"].ToString());
                    ext.SetCellText(j,"C",i["SALE"].ToString());
                    ext.SetCellText(j,"D",i["SSALE"].ToString());
                    ext.SetCellText(j,"E",i["SETTIME"].ToString());
                    ext.SetCellText(j,"F",i["USER"].ToString());
                    ext.SetCellText(j,"G",i["KHU"].ToString());
                    ext.SetCellText(j,"H",i["SBM"].ToString());
                    ext.SetCellText(j,"I",i["ZKOU"].ToString());
                    ext.SetCellText(j,"J",i["SLIANG"].ToString());
                    ext.SetCellText(j,"K",i["DWEI"].ToString());
                    j++;
                }

                MSG.login.Close();
                ext.ShowExcel();
            }catch(Exception ex){
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void 销售统计详细_FormClosed(object sender, FormClosedEventArgs e)
        {
            //panBT.Enabled = true;
            //panFORM.TopMost = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}