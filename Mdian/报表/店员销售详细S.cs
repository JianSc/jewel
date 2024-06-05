using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace Mdian.报表
{
    public partial class 店员销售详细S : Form
    {
        DateTime a, b;
        DataTable panDT = new theDST().Tables["销售统计表"];
        string USERNAME;

        public 店员销售详细S(DateTime a_items,DateTime b_items,string username)
        {
            USERNAME = username;
            a = a_items;
            b = b_items;
            InitializeComponent();
        }

        private void 店员销售详细S_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            toolStripTextBox1.Text = USERNAME;
            dataGridView1.DataSource = panDT;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(QGOODS_SALES.TM) AS TM,RTRIM(KHU) AS KHU,RTRIM(SBM) AS SBM,CONVERT(CHAR(10),QGOODS_SALES.SETTIME,120) AS SETTIME,RTRIM(QGOODS_SALES.DWEI) AS DWEI,"
                    + "QGOODS.IMGBOL,SALE,SSALE,QGOODS_SALES.SLIANG,RTRIM(QGOODS.SLIAO) AS SLIAO,RTRIM(QGOODS.JLIAO) AS JLIAO,RTRIM(QGOODS.SSI) AS SSI FROM QGOODS_SALES INNER JOIN QGOODS ON QGOODS.TM = QGOODS_SALES.TM "
                    + "WHERE (QGOODS_SALES.MDIAN='" + xconfig.MDIAN + "') AND ([USER]='" + USERNAME + "') AND (QGOODS_SALES.SETTIME BETWEEN '" + a + "' AND '" + b + "')", conn);
                da.Fill(panDT);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
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
                bool imgbol = bool.Parse(dataGridView1["iMGBOLDataGridViewCheckBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString());

                toolStripButton4.Enabled = imgbol;
                string TM = dataGridView1["tMDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }
            toolStripButton2.Enabled = false;

            Thread t = new Thread(new ThreadStart(theed));
            t.Start();
            
        }

        private delegate void d();

        private void theed()
        {

            GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
            ext.Open(Application.StartupPath + @"\00I01.xlt");
            ext.FormCaption = "ColorBay";

            int r = 2;

            ext.SetCellText(1, "A", "条码");
            ext.SetCellText(1, "B", "品名");
            ext.SetCellText(1, "C", "单据识别码");
            ext.SetCellText(1, "D", "日期");
            ext.SetCellText(1, "E", "数量");
            ext.SetCellText(1, "F", "单位");
            ext.SetCellText(1, "G", "售价");
            ext.SetCellText(1, "H", "折扣");
            ext.SetCellText(1, "I", "实售价");
            ext.SetCellText(1, "J", "客户");

            foreach (DataRow i in panDT.Rows)
            {
                ext.SetCellText(r, "A", i["TM"].ToString());
                ext.SetCellText(r, "B", i["NAME"].ToString());
                ext.SetCellText(r, "C", i["SBM"].ToString());
                ext.SetCellText(r, "D", i["SETTIME"].ToString());
                ext.SetCellText(r, "E", i["SLIANG"].ToString());
                ext.SetCellText(r, "F", i["DWEI"].ToString());
                ext.SetCellText(r, "G", i["SALE"].ToString());
                ext.SetCellText(r, "H", i["ZKOU"].ToString());
                ext.SetCellText(r, "I", i["SSALE"].ToString());
                ext.SetCellText(r, "J", i["KHU"].ToString());
                r++;
            }

            MSG.login.Close();
            ext.ShowExcel();
            if (this.InvokeRequired) { d d = delegate { toolStripButton2.Enabled = true; }; this.Invoke(d); }
            else { toolStripButton2.Enabled = true; }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}