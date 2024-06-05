using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace Mdian.仓库
{
    public partial class 仓库 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable theDT = new theDST().Tables["goods"];

        public 仓库(ToolStripMenuItem m)
        {
            panMENU = m;

            InitializeComponent();
        }

        private void 仓库_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            dataGridView1.DataSource = theDT;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,SLIANG,CAST(JIANZ AS DECIMAL(19,3)) AS JIANZ,CAST(JINZ AS DECIMAL(19,3)) AS JINZ,CAST(PJIANZ AS DECIMAL(19,3)) AS PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,"
                + "RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM QGOODS INNER JOIN QGOODS_CKU ON QGOODS.TM=QGOODS_CKU.TM WHERE (QGOODS_CKU.NAME='" + xconfig.MDIAN + "') AND (QGOODS.XSTAT=1)", conn);
                theDT.Clear();
                da.Fill(theDT);
                conn.Close();

                if (theDT.Rows.Count > 1)
                {
                    config.conDGV.DGVAutoID(dataGridView1, "phao");

                    DataRow newrows = theDT.NewRow();
                    newrows["tm"] = "合计:";
                    newrows["zss"] = theDT.Compute("sum(zss)", "");
                    newrows["fss"] = theDT.Compute("sum(fss)", "");
                    newrows["zsz"] = theDT.Compute("sum(zsz)", "");
                    newrows["fsz"] = theDT.Compute("sum(fsz)", "");
                    newrows["jinz"] = decimal.Parse(theDT.Compute("sum(jinz)", "").ToString());
                    newrows["jianz"] = theDT.Compute("sum(jianz)", "");
                    newrows["sliang"] = theDT.Compute("sum(sliang)", "");
                    newrows["pjianz"] = theDT.Compute("sum(pjianz)", "");
                    theDT.Rows.Add(newrows);

                    foreach (DataGridViewRow i in dataGridView1.Rows)
                    {
                        if (i.Cells["tmDataGridViewTextBoxColumn"].Value.ToString() == "合计:")
                        {
                            i.DefaultCellStyle.BackColor = Color.LightBlue;
                            i.DefaultCellStyle.ForeColor = Color.Red;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void 仓库_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            try
            {
                bool imgbol = false;
                imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                string tm = string.Empty;
                tm = ((DataGridView)sender)["tmDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

                toolStripButton3.Enabled = imgbol;

                if (panel1.Visible)
                {
                    string msg = string.Empty;
                    msg = xconfig.USER + "|查看库存|GETBMP|" + tm + "<EOF>";
                    xconfig.GetIMG(msg, pictureBox1, imgbol, tm, tmLabel, jdLabel);
                }
            }
            catch { }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!panel1.Visible)
            {
                panel1.Visible = true;
            }
            if (theDT.Rows.Count < 1)
            {
                return;
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
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show("请稍候，正在导出数据......");
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
                ext.FormCaption = "ColorBay";

                ext.SetCellText(1, "A", "条码");
                ext.SetCellText(1, "B", "品名");
                ext.SetCellText(1, "C", "金料");
                ext.SetCellText(1, "D", "石料");
                ext.SetCellText(1, "E", "类别");
                ext.SetCellText(1, "F", "款号");
                ext.SetCellText(1, "G", "证书号");
                ext.SetCellText(1, "H", "数量");
                ext.SetCellText(1, "I", "单位");
                ext.SetCellText(1, "J", "主石");
                ext.SetCellText(1, "K", "辅石");
                ext.SetCellText(1, "L", "圈口");
                ext.SetCellText(1, "M", "件重");
                ext.SetCellText(1, "N", "金重");
                ext.SetCellText(1, "O", "配件重");
                ext.SetCellText(1, "P", "售价");

                int j = 1;
                foreach (DataRow i in theDT.Rows)
                {
                    if (i["TM"].ToString() == "合计:")
                    {
                        continue;
                    }
                    ext.SetCellText(j + 1, "A", i["TM"].ToString());
                    ext.SetCellText(j + 1, "B", i["NAME"].ToString());
                    ext.SetCellText(j + 1, "C", i["JLIAO"].ToString());
                    ext.SetCellText(j + 1, "D", i["SLIAO"].ToString());
                    ext.SetCellText(j + 1, "E", i["SSI"].ToString());
                    ext.SetCellText(j + 1, "F", i["KUS"].ToString());
                    ext.SetCellText(j + 1, "G", i["ZSHU"].ToString());
                    ext.SetCellText(j + 1, "H", i["SLIANG"].ToString());
                    ext.SetCellText(j + 1, "I", i["DWEI"].ToString());
                    ext.SetCellText(j + 1, "J", i["ZSHI"].ToString());
                    ext.SetCellText(j + 1, "K", i["FSHI"].ToString());
                    ext.SetCellText(j + 1, "L", i["QKOU"].ToString());
                    ext.SetCellText(j + 1, "M", i["JIANZ"].ToString());
                    ext.SetCellText(j + 1, "N", i["JINZ"].ToString());
                    ext.SetCellText(j + 1, "O", i["PJIANZ"].ToString());
                    ext.SetCellText(j + 1, "P", i["XSOU"].ToString());
                    j++;
                }

                MSG.login.Close();
                ext.ShowExcel();
                //ext.Close();

                if (this.InvokeRequired)
                {
                    d d = delegate
                    {
                        toolStripButton2.Enabled = true;
                    };
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

            if (this.InvokeRequired) { d d = delegate { toolStripButton2.Enabled = true; }; this.Invoke(d); }
            else { toolStripButton2.Enabled = true; }

        }
    }
}