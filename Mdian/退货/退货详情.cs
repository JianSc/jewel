using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace Mdian.退货
{
    public partial class 退货详情 : Form
    {
        Form panF;
        string panSBM;
        DataTable n_dt = new theDST().Tables["goods"];

        public 退货详情(Form f,string SBM)
        {
            panF = f;
            panSBM = SBM;
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 退货详情_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(A.TM) AS TM,RTRIM(A.JLIAO) AS JLIAO,RTRIM(A.SLIAO) AS SLIAO,RTRIM(A.SSI) AS SSI,RTRIM(A.ZSHU) AS ZSHU,"
                    + "RTRIM(A.KUS) AS KUS,A.SLIANG,RTRIM(A.DWEI) AS DWEI,A.QKOU,A.JIANZ,A.JINZ,A.PJIANZ,A.ZSS,A.FSS,A.ZSZ,A.FSZ,A.XSOU,A.IMGBOL,"
                    + "RTRIM(A.YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU,BZHU FROM QGOODS A INNER JOIN QGOODS_BACK B ON A.TM=B.TM WHERE (B.SBM='" + panSBM + "')", conn);
                da.Fill(n_dt);
                conn.Close();

                string sale = n_dt.Compute("SUM(XSOU)", "").ToString();
                string sl = n_dt.Compute("SUM(SLIANG)", "").ToString();
                string jianz = n_dt.Compute("SUM(JIANZ)", "").ToString();
                string jinz = n_dt.Compute("SUM(JINZ)", "").ToString();
                string pjianz = n_dt.Compute("SUM(PJIANZ)", "").ToString();

                DataRow dr = n_dt.NewRow();
                dr["KUS"] = "合计:";
                dr["XSOU"] = sale;
                dr["SLIANG"] = sl;
                dr["JIANZ"] = jianz;
                dr["JINZ"] = jinz;
                dr["PJIANZ"] = pjianz;
                dr["TM"] = " ";
                n_dt.Rows.Add(dr);

                dataGridView1.DataSource = n_dt;

                foreach (DataGridViewRow i in dataGridView1.Rows)
                {
                    if (i.Cells["kusDataGridViewTextBoxColumn"].Value.ToString() == "合计:")
                    {
                        i.DefaultCellStyle.BackColor = Color.LightSteelBlue;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void 退货详情_FormClosed(object sender, FormClosedEventArgs e)
        {
            panF.TopMost = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!panel3.Visible)
            {
                panel3.Visible = true;
            }

            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                bool imgbol = false;
                imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                toolStripButton3.Enabled = imgbol;

                string tm = ((DataGridView)sender)["tmDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();
                if (tm == string.Empty)
                {
                    return;
                }

                if (panel3.Visible)
                {
                    xconfig.GetIMG(xconfig.USER + "|销售|GETBMP|" + tm + "<EOF>", pictureBox1, imgbol, tm, tmLabel, jdLabel);
                }
            }
            catch { }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            MSG.login.Show("正在执行操作，请稍候......");
            Application.DoEvents();

            toolStripButton2.Enabled = false;

            Thread t = new Thread(new ThreadStart(showE));
            t.Start();
        }

        private delegate void d();

        private void showE()
        {
            try
            {
                GoldPrinter.ExcelAccess extshow = new GoldPrinter.ExcelAccess();
                extshow.Open(Application.StartupPath + @"\00I01.xlt");
                extshow.FormCaption = "ColorBay";

                extshow.SetCellText(1, "A", "条码");
                extshow.SetCellText(1, "B", "数量");
                extshow.SetCellText(1, "C", "证书号");
                extshow.SetCellText(1, "D", "金料");
                extshow.SetCellText(1, "E", "石料");
                extshow.SetCellText(1, "F", "首饰类别");
                extshow.SetCellText(1, "G", "品名");
                extshow.SetCellText(1, "H", "款号");
                extshow.SetCellText(1, "I", "售价");
                extshow.SetCellText(1, "J", "主石");
                extshow.SetCellText(1, "K", "辅石");
                extshow.SetCellText(1, "L", "件重");
                extshow.SetCellText(1, "M", "金重");
                extshow.SetCellText(1, "N", "圈口");
                extshow.SetCellText(1, "O", "净度");
                extshow.SetCellText(1, "P", "颜色");

                int j = 1;
                foreach (DataRow i in n_dt.Rows)
                {
                    if (i["KUS"].ToString() == "合计:")
                    {
                        continue;
                    }
                    extshow.SetCellText(j + 1, "A", i["TM"].ToString());
                    //string tm = i["TM"].ToString();
                    extshow.SetCellText(j + 1, "B", i["SLIANG"].ToString());
                    extshow.SetCellText(j + 1, "C", i["ZSHU"].ToString());
                    extshow.SetCellText(j + 1, "D", i["JLIAO"].ToString());
                    extshow.SetCellText(j + 1, "E", i["SLIAO"].ToString());
                    extshow.SetCellText(j + 1, "F", i["SSI"].ToString());
                    extshow.SetCellText(j + 1, "G", i["NAME"].ToString());
                    extshow.SetCellText(j + 1, "H", i["KUS"].ToString());
                    extshow.SetCellText(j + 1, "I", i["XSOU"].ToString());
                    extshow.SetCellText(j + 1, "J", i["ZSHI"].ToString());
                    extshow.SetCellText(j + 1, "K", i["FSHI"].ToString());
                    extshow.SetCellText(j + 1, "L", i["JIANZ"].ToString());
                    extshow.SetCellText(j + 1, "M", i["JINZ"].ToString());
                    if (i["QKOU"].ToString() != "0")
                    {
                        extshow.SetCellText(j + 1, "N", i["QKOU"].ToString());
                    }
                    extshow.SetCellText(j + 1, "O", i["JDU"].ToString());
                    extshow.SetCellText(j + 1, "P", i["YSE"].ToString());
                    j++;
                }
                MSG.login.Close();
                d d = delegate
                {
                    toolStripButton2.Enabled = true;
                };
                this.Invoke(d);

                extshow.ShowExcel();
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }

        }
    }
}