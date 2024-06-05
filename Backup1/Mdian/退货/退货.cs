using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;

namespace Mdian.退货
{
    public partial class 退货 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable thisDT = new theDST().Tables["goods"];
        public 退货(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        private void 退货_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = thisDT;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            string TM = dataGridView1["tmDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            DataRow[] i = thisDT.Select("TM='" + TM + "'", "");
            foreach (DataRow j in i)
            {
                thisDT.Rows.Remove(j);
            }

            config.conDGV.DGVAutoID(dataGridView1, "phao");
        }

        private void 退货_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                textBox1.Focus();
                label2.Text = "请输入条码！";
                panel2.Visible = true;
                return;
            }

            string TM = textBox1.Text;

            DataRow[] itemdr = thisDT.Select("TM='" + TM + "'", "");
            if (itemdr.Length > 0)
            {
                label2.Text = "这件商品已存在！";
                panel2.Visible = true;
                textBox1.Focus();
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(DWEI) AS DWEI,SLIANG,JIANZ,JINZ,PJIANZ,ZSZ,ZSS,FSZ,FSS,XSOU,IMGBOL,RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM QGOODS "
                    + "INNER JOIN QGOODS_CKU ON QGOODS.TM=QGOODS_CKU.TM WHERE (QGOODS_CKU.NAME='" + xconfig.MDIAN + "') AND (QGOODS.XSTAT=1) AND (QGOODS.TM='" + TM + "')", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    DataRow i = thisDT.NewRow();
                    i["TM"] = rs["TM"].ToString();
                    i["JLIAO"] = rs["JLIAO"].ToString();
                    i["SLIAO"] = rs["SLIAO"].ToString();
                    i["SSI"] = rs["SSI"].ToString();
                    i["QKOU"] = rs["QKOU"].ToString();
                    i["ZSHU"] = rs["ZSHU"].ToString();
                    i["KUS"] = rs["KUS"].ToString();
                    i["DWEI"] = rs["DWEI"].ToString();
                    i["SLIANG"] = rs["SLIANG"].ToString();
                    i["JIANZ"] = rs["JIANZ"].ToString();
                    i["JINZ"] = rs["JINZ"].ToString();
                    i["PJIANZ"] = rs["PJIANZ"].ToString();
                    i["ZSZ"] = rs["ZSZ"].ToString();
                    i["ZSS"] = rs["ZSS"].ToString();
                    i["FSZ"] = rs["FSZ"].ToString();
                    i["FSS"] = rs["FSS"].ToString();
                    i["XSOU"] = rs["XSOU"].ToString();
                    i["IMGBOL"] = rs["IMGBOL"].ToString();
                    i["YSE"] = rs["YSE"].ToString();
                    i["XZUANG"] = rs["XZUANG"].ToString();
                    i["QGONG"] = rs["QGONG"].ToString();
                    i["JDU"] = rs["JDU"].ToString();
                    thisDT.Rows.Add(i);
                }
                else
                {
                    label2.Text = "没有找到这件商品！";
                    panel2.Visible = true;
                }
                rs.Close();
                conn.Close();
                config.conDGV.DGVAutoID(dataGridView1, "phao");
                textBox1.Text = string.Empty;
                textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void panel2_VisibleChanged(object sender, EventArgs e)
        {
            bool visbol = ((Panel)sender).Visible;
            if (visbol)
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                button1_Click(button1, new EventArgs());
            }
            else if (key == 8)
            {
                e.Handled = false;
            }
            else
            {
                Regex regex = new Regex(@"\d");
                if (!regex.Match(e.KeyChar.ToString()).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                bool imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                toolStripButton3.Enabled = imgbol;
                string TM = ((DataGridView)sender)["tmDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

                if (panel3.Visible)
                {
                    xconfig.GetIMG(xconfig.USER + "|退货|GETBMP|" + TM + "<EOF>", pictureBox2, imgbol, TM, tmLabel, jdLabel);
                }
            }
            catch { }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            if (!panel3.Visible)
            {
                panel3.Visible = true;
            }

            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            string DH = string.Empty;
            IPAddress IP;
            int PORT;
            IP = IPAddress.Parse(xconfig.SERVERIP);
            PORT = xconfig.SERVERPORT;
            mesk.xSocket mesk = new mesk.xSocket();
            string msgstr = xconfig.USER + "|门店退货|GETTIMEDH<EOF>";
            DH = mesk.Send(msgstr, IP, PORT);
            if (DH == "ERROR")
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                foreach (DataRow i in thisDT.Rows)
                {
                    cmd = new SqlCommand("QZ_GOODS_BACK", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", i["TM"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                    cmd.Parameters.Add(new SqlParameter("@MDIAN", xconfig.MDIAN));
                    cmd.Parameters.Add(new SqlParameter("@SLIANG", i["SLIANG"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@SBM", DH));
                    cmd.Parameters.Add(new SqlParameter("@NTXT", textBox2.Text));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton5.Enabled = true;
                button1.Enabled = false;
                dataGridView1.Enabled = false;
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

            toolStripButton5.Enabled = false;
            MSG.login.Show();

            Thread t = new Thread(new ThreadStart(print));
            t.Start();
        }

        private delegate void d();

        private void print()
        {
            try
            {
                GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                ext.Open(Application.StartupPath + @"\00T01.xlt");
                ext.FormCaption = "ColorBay";
                ext.IsVisibledExcel = false;

                DataGridView thisDGV = new DataGridView();
                if (dataGridView1.InvokeRequired) { d d = delegate { thisDGV = dataGridView1; }; dataGridView1.Invoke(d); }
                else { thisDGV = dataGridView1; }

                ext.SetCellText(4, "B", DateTime.Now.ToShortDateString());
                ext.SetCellText(13, "J", xconfig.USER);

                int copyrow = 9;
                int r = 11;

                foreach (DataGridViewRow i in thisDGV.Rows)
                {
                    ext.InsertRow(copyrow, copyrow);
                }

                foreach (DataGridViewRow i in thisDGV.Rows)
                {
                    ext.SetCellText(r, "A", i.Cells["tmDataGridViewTextBoxColumn"].Value.ToString());
                    ext.SetCellText(r, "B", i.Cells["nameDataGridViewTextBoxColumn"].Value.ToString());
                    ext.SetCellText(r, "C", i.Cells["kusDataGridViewTextBoxColumn"].Value.ToString());
                    ext.SetCellText(r, "D", i.Cells["sliangDataGridViewTextBoxColumn"].Value.ToString());
                    ext.SetCellText(r, "E", i.Cells["dweiDataGridViewTextBoxColumn"].Value.ToString());
                    ext.SetCellText(r, "F", i.Cells["jianzDataGridViewTextBoxColumn"].Value.ToString());
                    ext.SetCellText(r, "G", i.Cells["jinzDataGridViewTextBoxColumn"].Value.ToString());
                    ext.SetCellText(r, "H", i.Cells["zshiDataGridViewTextBoxColumn"].Value.ToString());
                    ext.SetCellText(r, "I", i.Cells["fshiDataGridViewTextBoxColumn"].Value.ToString());
                    ext.SetCellText(r, "J", i.Cells["xsouDataGridViewTextBoxColumn"].Value.ToString());
                    r++;
                }

                ext.DeleteRow(10);
                ext.DeleteRow(9);
                ext.DeleteRow(8);

                MSG.login.Close();

                ext.PrintPreview();
                ext.Close();

                if (this.InvokeRequired) { d d = delegate { toolStripButton5.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton5.Enabled = true; }
            }
            catch (Exception ex)
            {
                if (this.InvokeRequired) { d d = delegate { toolStripButton5.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton5.Enabled = true; }
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }
    }
}