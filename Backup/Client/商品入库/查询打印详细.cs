using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace Client.商品入库
{
    public partial class 查询打印详细 : Form
    {
        string panDH = string.Empty;
        string panTIME = string.Empty;
        string panUSER = string.Empty;
        DataTable theDT = new clidata().Tables["goods"];
        DataGridView theDGV;
        Form panFORM;

        public 查询打印详细(string dh,string t,string u,Form f)
        {
            panFORM = f;
            panDH = dh;
            panTIME = t;
            panUSER = u;

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,"
            + "RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,"
            + "RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,BLU,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,"
            + "SLBOL,IMGBOL,RTRIM(JDU) AS JDU,RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,"
            + "XSTAT,RTRIM(CADI) AS CADI,RTRIM(DDH) AS DDH,CONVERT(CHAR(10),SETTIME,120) AS SETTIME,ID FROM "
            + "QGOODS INNER JOIN QGOODS_RKD ON QGOODS.TM = QGOODS_RKD.TM "
            + "WHERE (QGOODS_RKD.DH = '" + panDH + "')";
            config.conData.fill("sql", constr, cmdstr, theDT);
            foreach (DataRow i in theDT.Rows)
            {
                if (i["qkou"].ToString() == "0")
                {
                    i["qkou"] = "";
                }
            }
            
            InitializeComponent();
        }

        private delegate void dat();

        private void print()
        {
            try
            {
                GoldPrinter.ExcelAccess exl = new GoldPrinter.ExcelAccess();
                exl.Open(Application.StartupPath + @"\00R01.xlt");
                exl.IsVisibledExcel = false;
                exl.FormCaption = "ColorBay";

                int datatemprow = 9;
                int datastartrow = 11;

                string time = DateTime.Now.ToShortDateString();
                exl.SetCellText(4, "C", panTIME);
                exl.SetCellText(5, "C", DateTime.Now.ToShortDateString());
                exl.SetCellText(4, "K", panDH);
                string theGYS = string.Empty;
                try
                {
                    SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT RTRIM(NAME) AS NAME FROM QGOODS_GYS_LIST "
                        + "WHERE (DH='" + panDH + "') GROUP BY DH,NAME", conn);
                    SqlDataReader rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        theGYS = rs["NAME"].ToString();
                    }
                    else
                    {
                        theGYS = string.Empty;
                    }
                    rs.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                exl.SetCellText(5, "J",theGYS );
                exl.SetCellText(13, "K", panUSER);

                DataTable dstGOODS = theDT;

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
                    exl.SetCellText(datastartrow + i, "I", dstGOODS.Rows[i]["zshi"].ToString());
                    exl.SetCellText(datastartrow + i, "J", dstGOODS.Rows[i]["fshi"].ToString());
                    exl.SetCellText(datastartrow + i, "H", dstGOODS.Rows[i]["jinz"].ToString());
                    //exl.SetCellText(datastartrow + i, "K", dstGOODS.Rows[i]["bzhu"].ToString());
                }

                exl.DeleteRow(10);
                exl.DeleteRow(9);
                exl.DeleteRow(8);

                MSG.login.Close();
                exl.PrintPreview();
                exl.Close();
                dat d = delegate { toolStripButton1.Enabled = true; };
                this.Invoke(d);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 查询打印详细_Load(object sender, EventArgs e)
        {
            this.Text = "单号：" + panDH;
            //toolStripStatusLabel1.Text = "单号：" + panDH;
            theDGV = dataGridView1;
            theDGV.DataSource = theDT;
            this.WindowState = FormWindowState.Maximized;

            MSG.login.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (theDT.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show("正在导入打印数据，请稍候.....");
            Application.DoEvents();

            toolStripButton1.Enabled = false;
            Thread t = new Thread(new ThreadStart(print));
            t.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).Rows.Count < 1) { return; }
            try
            {
                string tm;
                bool imgbol;
                tm = ((DataGridView)sender)["tmDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();
                imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());

                toolStripButton3.Enabled = imgbol;

                if (panel1.Visible)
                {
                    if (imgbol)
                    {
                        Image itemimg;
                        Bitmap itembmp;
                        itemimg = xconfig.netImgGET(xconfig.USER + "|打印详细|GETBMP|" + tm + "<EOF>");
                        itembmp = new Bitmap(itemimg, pictureBox1.Size);
                        pictureBox1.Image = itembmp;
                    }
                    else
                    {
                        pictureBox1.Image = Properties.Resources.ImgGet_Err;
                    }
                }
            }
            catch { pictureBox1.Image = Properties.Resources.ImgGet_Err; }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!panel1.Visible)
            {
                panel1.Visible = true;
            }

            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void 查询打印详细_FormClosed(object sender, FormClosedEventArgs e)
        {
            panFORM.TopMost = true;
        }
    }
}