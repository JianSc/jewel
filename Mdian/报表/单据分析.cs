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
    public partial class 单据分析 : Form
    {
        DataTable theDT;
        Form panFR;
        Button panBT;

        public 单据分析(DataTable t,Button b,Form f)
        {
            theDT = t;
            panFR = f;
            panBT = b;
            InitializeComponent();
        }

        private void 单据分析_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            if (theDT.Rows.Count > 0)
            {
                foreach (DataRow i in theDT.Rows)
                {
                    double y = double.Parse(i["YHUI"].ToString());
                    double z = double.Parse(i["ZK"].ToString());
                    double s = double.Parse(i["SALES"].ToString());
                    double m = s * z - y;
                    i["SSALES"] = m.ToString();
                }

                DataRow r = theDT.NewRow();
                r["SBM"]="总计:";
                r["SLIANG"] = theDT.Compute("SUM(SLIANG)", "");
                r["SSALES"] = theDT.Compute("SUM(SSALES)", "");
                theDT.Rows.Add(r);

                dataGridView1.DataSource = theDT;
                config.conDGV.DGVAutoID(dataGridView1, "phao",1);

                foreach (DataGridViewRow i in dataGridView1.Rows)
                {
                    if (i.Cells["sBMDataGridViewTextBoxColumn"].Value.ToString() == "总计:")
                    {
                        i.DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                }
            }
        }

        private void 单据分析_FormClosed(object sender, FormClosedEventArgs e)
        {
            panBT.Enabled = true;
            panFR.TopMost = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            string dh = dataGridView1["sBMDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            DataTable newDT = new theDST().Tables["销售统计表"];

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(QGOODS_SALES.TM) AS TM,IMGBOL,SALE,SSALE,RTRIM(KHU) AS KHU,CONVERT(CHAR(10),QGOODS_SALES.SETTIME,120) AS SETTIME,RTRIM([USER]) AS [USER],"
                    + "ZKOU,QGOODS_SALES.SLIANG,RTRIM(QGOODS_SALES.DWEI) AS DWEI,RTRIM(QGOODS.SLIAO) AS SLIAO,RTRIM(QGOODS.JLIAO) AS JLIAO,RTRIM(QGOODS.SSI) AS SSI FROM QGOODS_SALES INNER JOIN QGOODS ON "
                    + "QGOODS_SALES.TM=QGOODS.TM WHERE SBM='" + dh + "'", conn);
                newDT.Clear();
                da.Fill(newDT);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Form newForm = new 销售统计详细(newDT,dh);
            newForm.MdiParent = this.MdiParent;
            newForm.Show();


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

            Thread t = new Thread(new ThreadStart(daochu));
            t.Start();
        }

        private delegate void d();

        private void daochu()
        {
            try
            {
                GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                ext.Open(Application.StartupPath + @"\00I01.xlt");
                ext.FormCaption = "Colorbay";
                ext.SetCellText(1, "A", "日期");
                ext.SetCellText(1, "B", "销售单号");
                ext.SetCellText(1, "C", "数量");
                ext.SetCellText(1, "D", "售价");
                ext.SetCellText(1, "E", "折扣");
                ext.SetCellText(1, "F", "优惠");
                ext.SetCellText(1, "G", "实售价");
                ext.SetCellText(1, "H", "店员");

                int j = 2;
                foreach (DataRow i in theDT.Rows)
                {
                    if (i["SETTIME"].ToString() == string.Empty)
                    {
                        continue;
                    }
                    ext.SetCellText(j, "A", i["SETTIME"].ToString());
                    ext.SetCellText(j, "B", i["SBM"].ToString());
                    ext.SetCellText(j, "C", i["SLIANG"].ToString());
                    ext.SetCellText(j, "D", i["SALES"].ToString());
                    ext.SetCellText(j, "E", i["ZK"].ToString());
                    ext.SetCellText(j, "F", i["YHUI"].ToString());
                    ext.SetCellText(j, "G", i["SSALES"].ToString());
                    ext.SetCellText(j, "H", i["USER"].ToString());
                    j++;
                }

                ext.ShowExcel();

                MSG.login.CloseIMG();

                if (this.InvokeRequired)
                {
                    d d = delegate { toolStripButton2.Enabled = true; };
                    this.Invoke(d);
                }
                else
                {
                    toolStripButton2.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            MSG.login.ShowIMG();
            Application.DoEvents();
            toolStripButton3.Enabled = false;

            Thread t = new Thread(new ThreadStart(print));
            t.Start();
        }

        private void print()
        {
            try
            {
                GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                ext.Open(Application.StartupPath + @"\00B02.xlt");
                ext.FormCaption = "Colorbay";

                int copyrow = 6;
                int b = 8;

                ext.SetCellText(10, "J", xconfig.USER);
                foreach (DataRow i in theDT.Rows)
                {
                    if (i["SETTIME"].ToString() == string.Empty)
                    {
                        continue;
                    }

                    ext.InsertRow(copyrow, copyrow);
                }

                foreach (DataRow i in theDT.Rows)
                {
                    if (i["SETTIME"].ToString() == string.Empty)
                    {
                        continue;
                    }

                    ext.SetCellText(b, "A", i["SETTIME"].ToString());
                    ext.SetCellText(b, "B", i["SBM"].ToString());
                    ext.SetCellText(b, "C", i["SLIANG"].ToString());
                    ext.SetCellText(b, "D", i["SALES"].ToString());
                    ext.SetCellText(b, "E", i["YHUI"].ToString());
                    ext.SetCellText(b, "F", i["ZK"].ToString());
                    ext.SetCellText(b, "G", i["USER"].ToString());
                    ext.SetCellText(b, "H", i["KEHUID"].ToString());
                    ext.SetCellText(b, "I", i["KHU"].ToString());
                    ext.SetCellText(b, "J", i["SSALES"].ToString());
                    b++;
                }

                ext.DeleteRow(7);
                ext.DeleteRow(6);
                ext.DeleteRow(5);

                MSG.login.CloseIMG();
                Application.DoEvents();
                if (this.InvokeRequired)
                {
                    d d = delegate { toolStripButton3.Enabled = true; };
                    this.Invoke(d);
                }
                else
                {
                    toolStripButton3.Enabled = true;
                }

                ext.PrintPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}