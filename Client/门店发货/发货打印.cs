using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Client.门店发货
{
    public partial class 发货打印 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable theDT = new clidata().Tables["发货单列表"];
        string extFilename = string.Empty;
        DataGridView theDGV;
        Form panFORM;

        public 发货打印(ToolStripMenuItem menu,Form panform)
        {
            panMENU = menu;
            panFORM = panform;

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(DH) AS DH,RTRIM([USER]) AS [USER],SETTIME AS XTIME,SUM(SL) AS SL,RTRIM(MDIAN) AS MDIAN FROM "
            + "(SELECT DH,[USER],CONVERT(CHAR(10),SETTIME,120) AS SETTIME,SL,MDIAN FROM QGOODS_CKD) DERIVEDTBL "
            + "GROUP BY DH,[USER],SETTIME,MDIAN";
            config.conData.fill("sql", constr, cmdstr, theDT);

            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 发货打印_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            theDGV = dataGridView1;
            DataView dv = new DataView();
            dv.Table = theDT;
            dv.Sort = "id desc";
            theDGV.DataSource = dv;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            radioButton3_CheckedChanged(radioButton1, new EventArgs());

            if (theDT.Rows.Count < 1) { return; }

            MSG.login.Show("请稍候，正在读取数据.....");
            Application.DoEvents();

            try
            {
                string theDH, XTIME, USER, MDIAN;
                theDH = dataGridView1["dHDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
                XTIME = DateTime.Now.ToShortDateString();
                USER = dataGridView1["uSERDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
                MDIAN = dataGridView1["MDIAN", dataGridView1.CurrentCell.RowIndex].Value.ToString();

                DataTable newDT = new clidata().Tables["goods"];

                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(QGOODS.JLIAO) AS JLIAO,"
                    + "RTRIM(QGOODS.SLIAO) AS SLIAO,RTRIM(QGOODS.SSI) AS SSI,RTRIM(QGOODS.ZSHU) AS ZSHU,"
                    + "RTRIM(QGOODS.KUS) AS KUS,QGOODS.SLIANG,RTRIM(QGOODS.DWEI) AS DWEI,CBEI,JINZ,XSOU,ZSS,ZSZ,FSS,FSZ,JIES FROM "
                    + "QGOODS INNER JOIN QGOODS_CKD ON QGOODS.TM = QGOODS_CKD.TM "
                    + "WHERE (QGOODS_CKD.DH = '" + theDH + "')", conn);
                da.Fill(newDT);
                conn.Close();

                GoldPrinter.ExcelAccess exlprint = new GoldPrinter.ExcelAccess();
                exlprint.Open(Application.StartupPath + @"\" + extFilename);
                exlprint.IsVisibledExcel = false;
                exlprint.FormCaption = "启智软体";

                int rowcopy = 9;
                int rowstart = 11;

                exlprint.SetCellText(4, "C", XTIME);
                exlprint.SetCellText(5, "C", XTIME);
                exlprint.SetCellText(4, "K", theDH);
                exlprint.SetCellText(5, "J", MDIAN);
                switch (extFilename)
                {
                    case "00C01.xlt":
                        exlprint.SetCellText(13, "L", USER);
                        break;
                    case "00C02.xlt":
                        exlprint.SetCellText(13, "L", USER);
                        break;
                    case "00C03.xlt":
                        exlprint.SetCellText(13, "L", USER);
                        break;
                    case "00C04.xlt":
                        exlprint.SetCellText(13, "L", USER);
                        break;
                }

                foreach (DataRow i in newDT.Rows)
                {
                    exlprint.InsertRow(rowcopy, rowcopy);
                    //exlprint.SetRowHeight(rowcopy, 18.75f);
                }

                int j = 0;
                foreach (DataRow i in newDT.Rows)
                {
                    exlprint.SetCellText(rowstart + j, "A", i["TM"].ToString());
                    exlprint.SetCellText(rowstart + j, "B", i["NAME"].ToString());
                    exlprint.SetCellText(rowstart + j, "D", i["ZSHU"].ToString());
                    exlprint.SetCellText(rowstart + j, "E", i["KUS"].ToString());
                    exlprint.SetCellText(rowstart + j, "F", i["SLIANG"].ToString());
                    exlprint.SetCellText(rowstart + j, "G", i["DWEI"].ToString());
                    exlprint.SetCellText(rowstart + j, "H", i["JINZ"].ToString());
                    switch (extFilename)
                    {
                        case "00C01.xlt":
                            exlprint.SetCellText(rowstart + j, "K", i["CBEI"].ToString());
                            exlprint.SetCellText(rowstart + j, "L", i["XSOU"].ToString());
                            exlprint.SetCellText(rowstart + j, "I", i["ZSHI"].ToString());
                            exlprint.SetCellText(rowstart + j, "J", i["FSHI"].ToString());
                            break;
                        case "00C02.xlt":
                            exlprint.SetCellText(rowstart + j, "K", i["CBEI"].ToString());
                            //exlprint.SetCellText(rowstart + j, "J", i["XSOU"].ToString());
                            exlprint.SetCellText(rowstart + j, "I", i["ZSHI"].ToString());
                            exlprint.SetCellText(rowstart + j, "J", i["FSHI"].ToString());
                            break;
                        case "00C03.xlt":
                            //exlprint.SetCellText(rowstart + j, "I", i["CBEI"].ToString());
                            exlprint.SetCellText(rowstart + j, "K", i["XSOU"].ToString());
                            exlprint.SetCellText(rowstart + j, "I", i["ZSHI"].ToString());
                            exlprint.SetCellText(rowstart + j, "J", i["FSHI"].ToString());
                            exlprint.SetCellText(rowstart + j, "L", i["JIES"].ToString());
                            break;
                        case "00C04.xlt":
                            exlprint.SetCellText(rowstart + j, "K", i["XSOU"].ToString());
                            //exlprint.SetCellText(rowstart + j, "J", i["XSOU"].ToString());
                            exlprint.SetCellText(rowstart + j, "I", i["ZSHI"].ToString());
                            exlprint.SetCellText(rowstart + j, "J", i["FSHI"].ToString());
                            break;
                    }
                    j++;
                }

                exlprint.DeleteRow(10);
                exlprint.DeleteRow(9);
                exlprint.DeleteRow(8);

                MSG.login.Close();
                Application.DoEvents();
                //this.Close();
                this.TopMost = false;
                exlprint.PrintPreview();
                this.TopMost = true;
                //exlprint.Close();

            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void 发货打印_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                extFilename = "00C01.xlt";
            }
            else if (radioButton2.Checked)
            {
                extFilename = "00C02.xlt";
            }
            else if (radioButton3.Checked)
            {
                extFilename = "00C03.xlt";
            }
            else if (radioButton4.Checked)
            {
                extFilename = "00C04.xlt";
            }
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

        private void 发货打印_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}