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
    public partial class 查询 : Form
    {
        DataTable theDT = new clidata().Tables["入库单列表"];
        DataGridView theDGV;
        ToolStripMenuItem panMENU;
        Form panFORM;

        public 查询(ToolStripMenuItem menu,Form panform)
        {
            panFORM = panform;
            panMENU = menu;
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(DH) AS DH,SETDATE as XTIME,RTRIM([USER]) AS [USER],SUM(SL) AS SL FROM "
            + "(SELECT DH,CONVERT(CHAR(10),SETDATE,120) AS SETDATE,[USER],SL FROM QGOODS_RKD) DERIVEDTBL "
            + "GROUP BY DH,SETDATE,[USER],[SL] order by dh";
            config.conData.fill("sql", constr, cmdstr, theDT);

            InitializeComponent();
        }

        private void 查询_Load(object sender, EventArgs e)
        {
            theDGV = dataGridView1;
            DataView itemdv = new DataView();
            itemdv.Table = theDT;
            itemdv.Sort = "id desc";
            theDGV.DataSource = itemdv;

            //this.WindowState = FormWindowState.Normal;

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.TopMost = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (theDT.Rows.Count < 1)
            {
                return;
            }

            string msg = xconfig.USER + "|打印|CONNECT<EOF>";
            xconfig.netSend(msg);

            MSG.login.Show("正在载入，请稍候.....");
            Application.DoEvents();

            string theDH = dataGridView1["dHDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            string theTIME = dataGridView1["xTimeDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            string theUSER = dataGridView1["uSERDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            Form itemform = new 查询打印详细(theDH,theTIME,theUSER,this);
            itemform.MdiParent = panFORM;
            this.TopMost = false;
            itemform.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (theDT.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show("正在导入打印数据，请稍候.....");
            Application.DoEvents();

            dh = dataGridView1["dHDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            time = dataGridView1["xTimeDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            xuser = dataGridView1["uSERDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            toolStripButton3.Enabled = false;
            this.TopMost = false;

            Thread t = new Thread(new ThreadStart(print));
            t.Start();
        }

        string dh, time, xuser;
        private delegate void d();
        private void print()
        {
            try
            {
                string theTime = time;
                string theDH = dh;
                string theGYS;
                string theUSER = xuser;

                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT RTRIM(NAME) AS NAME FROM QGOODS_GYS_LIST "
                    + "WHERE (DH='" + theDH + "') GROUP BY DH,NAME", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    theGYS = rs["NAME"].ToString();
                }
                else
                {
                    theGYS = "";
                }
                rs.Close();

                DataTable newDT = new clidata().Tables["goods"];

                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(QGOODS.JLIAO) AS JLIAO,"
                    + "RTRIM(QGOODS.SLIAO) AS SLIAO,RTRIM(QGOODS.SSI) AS SSI,RTRIM(QGOODS.ZSHU) AS ZSHU,"
                    + "RTRIM(QGOODS.KUS) AS KUS,QGOODS.SLIANG,RTRIM(QGOODS.DWEI) AS DWEI,CBEI,XSOU,JINZ,ZSS,ZSZ,FSS,FSZ FROM "
                    + "QGOODS INNER JOIN QGOODS_RKD ON QGOODS.TM = QGOODS_RKD.TM "
                    + "WHERE (QGOODS_RKD.DH = '" + theDH + "')", conn);
                da.Fill(newDT);
                conn.Close();

                GoldPrinter.ExcelAccess exlprint = new GoldPrinter.ExcelAccess();
                exlprint.Open(Application.StartupPath + @"\00R01.xlt");
                exlprint.IsVisibledExcel = false;
                exlprint.FormCaption = "ColorBay";

                int rowcopy = 9;
                int rowstart = 11;

                exlprint.SetCellText(4, "C", theTime);
                exlprint.SetCellText(4, "K", theDH);
                exlprint.SetCellText(5, "C", DateTime.Now.ToShortDateString());
                exlprint.SetCellText(5, "J", theGYS);
                exlprint.SetCellText(13, "K", theUSER);

                DataTable dstGOODS = newDT;

                int datatemprow = rowcopy;

                for (int i = 0; i < dstGOODS.Rows.Count; i++)
                {
                    exlprint.InsertRow(datatemprow, datatemprow);
                    //exlprint.SetRowHeight(datatemprow, 18.75f);
                }

                int datastartrow = rowstart;

                for (int i = 0; i < dstGOODS.Rows.Count; i++)
                {
                    exlprint.SetCellText(datastartrow + i, "A", dstGOODS.Rows[i]["tm"].ToString());
                    exlprint.SetCellText(datastartrow + i, "B", dstGOODS.Rows[i]["name"].ToString());
                    exlprint.SetCellText(datastartrow + i, "D", dstGOODS.Rows[i]["zshu"].ToString());
                    exlprint.SetCellText(datastartrow + i, "E", dstGOODS.Rows[i]["kus"].ToString());
                    exlprint.SetCellText(datastartrow + i, "F", dstGOODS.Rows[i]["sliang"].ToString());
                    exlprint.SetCellText(datastartrow + i, "G", dstGOODS.Rows[i]["dwei"].ToString());
                    exlprint.SetCellText(datastartrow + i, "K", dstGOODS.Rows[i]["cbei"].ToString());
                    exlprint.SetCellText(datastartrow + i, "L", dstGOODS.Rows[i]["xsou"].ToString());
                    exlprint.SetCellText(datastartrow + i, "I", dstGOODS.Rows[i]["zshi"].ToString());
                    exlprint.SetCellText(datastartrow + i, "J", dstGOODS.Rows[i]["fshi"].ToString());
                    exlprint.SetCellText(datastartrow + i, "H", dstGOODS.Rows[i]["jinz"].ToString());
                    //exlprint.SetCellText(datastartrow + i, "K", dstGOODS.Rows[i]["bzhu"].ToString());
                }

                exlprint.DeleteRow(10);
                exlprint.DeleteRow(9);
                exlprint.DeleteRow(8);

                MSG.login.Close();
                exlprint.PrintPreview();
                exlprint.Close();
                if (this.InvokeRequired) { d d = delegate { this.TopMost = true; }; this.Invoke(d); }
                else { this.TopMost = true; }
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }

            try
            {
                d d = delegate
                {
                    toolStripButton3.Enabled = true;
                };
                this.Invoke(d);
            }
            catch { }

            MSG.login.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int rows = ((DataGridView)sender).Rows.Count;
            if (rows > 9)
            {
                pictureBox1.Visible = false;
            }
            else if (rows < 10)
            {
                pictureBox1.Visible = true;
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView1_RowsAdded(sender, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void 查询_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
            string msg = xconfig.USER + "|闲置|CONNECT<EOF>";
            xconfig.netSend(msg);
        }

        private void 查询_Activated(object sender, EventArgs e)
        {
            //this.TopMost = true;
        }
    }
}