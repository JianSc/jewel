using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace Client.盘点
{
    public partial class 盘亏表 : Form
    {
        DataTable panDT;
        string panCKU;

        public 盘亏表(DataTable dt,string cku)
        {
            panDT = dt;
            panCKU = cku;
            InitializeComponent();
        }

        private void 盘亏表_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = panDT;
            config.conDGV.DGVAutoID(dataGridView1, "phao");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            try
            {
                bool imgbol;
                imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());

                string TM;
                TM = ((DataGridView)sender)["tmDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

                toolStripButton3.Enabled = imgbol;

                if (panel1.Visible)
                {
                    if (imgbol)
                    {
                        Image itemimg;
                        Bitmap itembmp;
                        itemimg = xconfig.netImgGET(xconfig.USER + "|库存盘点|GETBMP|" + TM + "<EOF>");
                        itembmp = new Bitmap(itemimg, pictureBox1.Size);
                        pictureBox1.Image = itembmp;
                    }
                    else
                    {
                        pictureBox1.Image = Properties.Resources.ImgGet_Err;
                    }
                }
            }
            catch { }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (panDT.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show("正在写入数据，请稍候.....");
            Application.DoEvents();

            try
            {
                GoldPrinter.ExcelAccess extshow = new GoldPrinter.ExcelAccess();
                extshow.Open(Application.StartupPath + @"\00I01.xlt");
                extshow.FormCaption = "ColorBay";

                extshow.SetCellText(1, "A", "TM");
                extshow.SetCellText(1, "B", "数量");
                extshow.SetCellText(1, "C", "门店/仓库");
                extshow.SetCellText(1, "D", "单号");
                extshow.SetCellText(1, "E", "证书号");
                extshow.SetCellText(1, "F", "金料");
                extshow.SetCellText(1, "G", "石料");
                extshow.SetCellText(1, "H", "首饰类别");
                extshow.SetCellText(1, "I", "品名");
                extshow.SetCellText(1, "J", "款号");
                extshow.SetCellText(1, "K", "成本");
                extshow.SetCellText(1, "L", "售价");
                extshow.SetCellText(1, "M", "供应商");
                extshow.SetCellText(1, "N", "日期");
                extshow.SetCellText(1, "O", "主石");
                extshow.SetCellText(1, "P", "辅石");
                extshow.SetCellText(1, "Q", "件重");
                extshow.SetCellText(1, "R", "金重");
                extshow.SetCellText(1, "S", "圈口");
                extshow.SetCellText(1, "T", "净度");
                extshow.SetCellText(1, "U", "颜色");


                int j = 1;
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                SqlDataReader rs;
                foreach (DataRow i in panDT.Rows)
                {
                    extshow.SetCellText(j + 1, "A", i["TM"].ToString());
                    //extshow.SetRowHeight(j + 1, 18.75f);
                    string tm = i["TM"].ToString();
                    extshow.SetCellText(j + 1, "B", i["SLIANG"].ToString());
                    cmd = new SqlCommand("SELECT RTRIM(NAME) AS NAME FROM QGOODS_CKU WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        extshow.SetCellText(j + 1, "C", rs["NAME"].ToString());
                    }
                    rs.Close();
                    cmd = new SqlCommand("SELECT RTRIM(DH) AS DH FROM QGOODS_RKD WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        extshow.SetCellText(j + 1, "D", rs["DH"].ToString());
                    }
                    rs.Close();
                    extshow.SetCellText(j + 1, "E", i["ZSHU"].ToString());
                    extshow.SetCellText(j + 1, "F", i["JLIAO"].ToString());
                    extshow.SetCellText(j + 1, "G", i["SLIAO"].ToString());
                    extshow.SetCellText(j + 1, "H", i["SSI"].ToString());
                    extshow.SetCellText(j + 1, "I", i["NAME"].ToString());
                    extshow.SetCellText(j + 1, "J", i["KUS"].ToString());
                    extshow.SetCellText(j + 1, "K", i["CBEI"].ToString());
                    extshow.SetCellText(j + 1, "L", i["XSOU"].ToString());
                    cmd = new SqlCommand("SELECT RTRIM(NAME) AS NAME FROM QGOODS_GYS_LIST WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        extshow.SetCellText(j + 1, "M", rs["NAME"].ToString());
                    }
                    rs.Close();
                    cmd = new SqlCommand("SELECT CONVERT(CHAR(10),SETTIME,120) AS SETTIME FROM QGOODS WHERE (TM='" + tm + "')", conn);
                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        extshow.SetCellText(j + 1, "N", rs["SETTIME"].ToString());
                    }
                    rs.Close();
                    extshow.SetCellText(j + 1, "O", i["ZSHI"].ToString());
                    extshow.SetCellText(j + 1, "P", i["FSHI"].ToString());
                    extshow.SetCellText(j + 1, "Q", i["JIANZ"].ToString());
                    extshow.SetCellText(j + 1, "R", i["JINZ"].ToString());
                    if (i["QKOU"].ToString() != "0")
                    {
                        extshow.SetCellText(j + 1, "S", i["QKOU"].ToString());
                    }
                    extshow.SetCellText(j + 1, "T", i["JDU"].ToString());
                    extshow.SetCellText(j + 1, "U", i["YSE"].ToString());
                    j++;
                }
                conn.Close();
                extshow.ShowExcel();
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (panDT.Rows.Count < 1)
            {
                return;
            }

            MSG.login.ShowIMG();
            Application.DoEvents();

            toolStripButton1.Enabled = false;
            Thread t = new Thread(new ThreadStart(print));
            t.Start();
        }

        private delegate void d();

        private void print()
        {
            try
            {
                Thread.Sleep(500);

                GoldPrinter.ExcelAccess exl = new GoldPrinter.ExcelAccess();
                exl.Open(Application.StartupPath + @"\00P02.xlt");
                exl.IsVisibledExcel = false;
                exl.FormCaption = "ColorBay";

                int datatemprow = 9;
                int datastartrow = 11;

                string time = DateTime.Now.ToShortDateString();
                exl.SetCellText(4, "C", time);
                //exl.SetCellText(4, "D", rkdhbox.Text);
                exl.SetCellText(4, "J", panCKU);
                exl.SetCellText(13, "L", xconfig.USER);

                DataTable dstGOODS = panDT;

                for (int i = 0; i < dstGOODS.Rows.Count; i++)
                {
                    exl.InsertRow(datatemprow, datatemprow);
                    //exl.SetRowHeight(datatemprow, 18.75f);
                }

                for (int i = 0; i < dstGOODS.Rows.Count; i++)
                {
                    exl.SetCellText(datastartrow + i, "A", dstGOODS.Rows[i]["tm"].ToString());
                    exl.SetCellText(datastartrow + i, "B", dstGOODS.Rows[i]["name"].ToString());
                    exl.SetCellText(datastartrow + i, "D", dstGOODS.Rows[i]["zshu"].ToString());
                    exl.SetCellText(datastartrow + i, "E", dstGOODS.Rows[i]["kus"].ToString());
                    exl.SetCellText(datastartrow + i, "F", dstGOODS.Rows[i]["sliang"].ToString());
                    exl.SetCellText(datastartrow + i, "G", dstGOODS.Rows[i]["dwei"].ToString());
                    exl.SetCellText(datastartrow + i, "K", dstGOODS.Rows[i]["cbei"].ToString());
                    exl.SetCellText(datastartrow + i, "L", dstGOODS.Rows[i]["xsou"].ToString());
                    exl.SetCellText(datastartrow + i, "I", dstGOODS.Rows[i]["zsz"].ToString() + "/" + dstGOODS.Rows[i]["zss"].ToString());
                    exl.SetCellText(datastartrow + i, "J", dstGOODS.Rows[i]["fsz"].ToString() + "/" + dstGOODS.Rows[i]["fss"].ToString());
                    exl.SetCellText(datastartrow + i, "H", dstGOODS.Rows[i]["jinz"].ToString());
                    //exl.SetCellText(datastartrow + i, "K", dstGOODS.Rows[i]["bzhu"].ToString());
                }

                exl.DeleteRow(10);
                exl.DeleteRow(9);
                exl.DeleteRow(8);

                MSG.login.CloseIMG();

                exl.PrintPreview();
                exl.Close();

                if (this.InvokeRequired) { d d = delegate { toolStripButton1.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton1.Enabled = true; }
            }
            catch (Exception ex)
            {
                MSG.login.CloseIMG();
                if (this.InvokeRequired) { d d = delegate { toolStripButton1.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton1.Enabled = true; }
                MessageBox.Show(ex.Message);
            }
        }
    }
}