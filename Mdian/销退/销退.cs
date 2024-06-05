using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;

namespace Mdian.销退
{
    public partial class 销退 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable thisDT = new theDST().Tables["销退表"];

        public 销退(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        private void 销退_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = thisDT;
        }

        private void 销退_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;
                foreach (DataRow i in thisDT.Rows)
                {
                    cmd = new SqlCommand("QZ_SALE_BACK", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", i["TM"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@SLIANG", i["SLIANG"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@DWEI", i["DWEI"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@SALE", i["SALE"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@TJIA", i["TJIA"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@KHU", i["KHU"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                    cmd.Parameters.Add(new SqlParameter("@ZKOU", i["ZKOU"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@SSALE", i["SSALE"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@MDIAN", xconfig.MDIAN));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

                button1.Enabled = false;
                toolStripButton1.Enabled = false;
                toolStripButton3.Enabled = true;
                toolStripButton5.Enabled = false;
                dataGridView1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                textBox1.Focus();
                return;
            }

            String TM = textBox1.Text.Trim();

            DataRow[] itemdr = thisDT.Select("TM='" + TM + "'", "");
            if (itemdr.Length > 0)
            {
                label3.Text = "这件商品已经存在于退货列表!!";
                panel2.Visible = true;
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(QGOODS.JLIAO) AS JLIAO,RTRIM(QGOODS.SLIAO) AS SLIAO,RTRIM(QGOODS.SSI) AS SSI,RTRIM(QGOODS_SALES.TM) AS TM,RTRIM(QGOODS_SALES.MDIAN) AS MDIAN,RTRIM(QGOODS_SALES.[USER]) AS [USER],RTRIM(QGOODS_SALES.KHU) AS KHU,"
                    + "RTRIM(QGOODS_SALES.SBM) AS SBM,QGOODS_SALES.SETTIME,QGOODS.IMGBOL,RTRIM(QGOODS_SALES.KEHUID) AS KEHUID,QGOODS_SALES.CBEI,QGOODS_SALES.SALE,QGOODS_SALES.SSALE,QGOODS_SALES.ZKOU,QGOODS_SALES.SLIANG,RTRIM(QGOODS_SALES.DWEI) AS DWEI,QGOODS.ZSZ,QGOODS.FSZ,QGOODS.ZSS,QGOODS.FSS,QGOODS.JINZ,RTRIM(QGOODS.KUS) AS KUS FROM QGOODS INNER JOIN "
                    + "QGOODS_SALES ON QGOODS.TM=QGOODS_SALES.TM WHERE (QGOODS.XSTAT=0) AND (QGOODS_SALES.TM='" + TM + "') ORDER BY QGOODS_SALES.SETTIME DESC", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                //while (rs.Read())
                if (rs.Read())
                {
                    DataRow i = thisDT.NewRow();
                    i["tm"] = rs["TM"].ToString();
                    i["settime"] = rs["SETTIME"].ToString();
                    i["sbm"] = rs["sbm"].ToString();
                    i["sliang"] = rs["sliang"].ToString();
                    i["dwei"] = rs["DWEI"].ToString();
                    i["zkou"] = rs["zkou"].ToString();
                    i["khu"] = rs["khu"].ToString();
                    i["user"] = rs["user"].ToString();
                    i["sale"] = rs["sale"].ToString();
                    i["ssale"] = rs["ssale"].ToString();
                    i["imgbol"] = rs["imgbol"].ToString();
                    i["jliao"] = rs["jliao"].ToString();
                    i["sliao"] = rs["sliao"].ToString();
                    i["ssi"] = rs["ssi"].ToString();
                    i["kehuid"] = rs["kehuid"].ToString();
                    i["tjia"] = rs["ssale"].ToString();
                    i["kus"] = rs["kus"].ToString();
                    i["zss"] = rs["zss"].ToString();
                    i["zsz"] = rs["zsz"].ToString();
                    i["fss"] = rs["fss"].ToString();
                    i["fsz"] = rs["fsz"].ToString();
                    i["jinz"] = rs["jinz"].ToString();
                    thisDT.Rows.Add(i);

                    textBox2.Text = thisDT.Compute("sum(tjia)", "").ToString();
                    textBox1.Text = "";
                    textBox1.Focus();
                }
                else
                {
                    label3.Text = "没有找到这件商品!!";
                    panel2.Visible = true;
                }
                rs.Close();
                conn.Close();
                foreach (DataRow i in thisDT.Rows)
                {
                    i["settime"] = Convert.ToDateTime(i["settime"].ToString()).ToShortDateString();
                }
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

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            string TM = dataGridView1["tmDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            DataRow[] dr = thisDT.Select("TM='" + TM + "'", "");
            foreach (DataRow i in dr)
            {
                thisDT.Rows.Remove(i);
            }

            textBox2.Text = thisDT.Compute("sum(tjia)", "").ToString();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                bool imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());
                toolStripButton4.Enabled = imgbol;

                string TM = string.Empty;
                TM = ((DataGridView)sender)["tmDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

                if (panel3.Visible)
                {
                    xconfig.GetIMG(xconfig.USER + "|销退|GETBMP|" + TM + "<EOF>", pictureBox2, imgbol, TM, tmLabel, jdLabel);
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!panel3.Visible)
            {
                panel3.Visible = true;
            }
            if (thisDT.Rows.Count < 1)
            {
                return;
            }

            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keystr = xconfig.ASC(e.KeyChar.ToString());
            if (keystr == 13)
            {
                button1_Click(button1, new EventArgs());
            }
            else if (keystr == 8)
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = thisDT.Compute("sum(tjia)", "").ToString();
            toolStripButton1.Enabled = true;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton1.Enabled = false;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count<1)
            {
                return;
            }

            toolStripButton3.Enabled = false;

            Thread t = new Thread(new ThreadStart(print));
            t.Start();
        }

        private delegate void d();

        private void print()
        {
            try
            {
                GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
                ext.Open(Application.StartupPath + @"\00T02.xlt");
                ext.FormCaption = "ColorBay";

                ext.SetCellText(4, "B", DateTime.Now.ToShortDateString());
                ext.SetCellText(12, "K", xconfig.USER);

                int copyrow = 8;
                int r = 10;

                foreach (DataRow i in thisDT.Rows)
                {
                    ext.InsertRow(copyrow, copyrow);
                }

                foreach (DataRow i in thisDT.Rows)
                {
                    ext.SetCellText(r, "A", i["TM"].ToString());
                    ext.SetCellText(r, "B", i["NAME"].ToString());
                    ext.SetCellText(r, "C", i["SLIANG"].ToString());
                    ext.SetCellText(r, "D", i["DWEI"].ToString());
                    ext.SetCellText(r, "E", i["KUS"].ToString());
                    ext.SetCellText(r, "F", i["JINZ"].ToString());
                    ext.SetCellText(r, "G", i["ZSHI"].ToString());
                    ext.SetCellText(r, "H", i["FSHI"].ToString());
                    ext.SetCellText(r, "I", i["ZKOU"].ToString());
                    ext.SetCellText(r, "J", i["SSALE"].ToString());
                    ext.SetCellText(r, "L", i["TJIA"].ToString());
                    r++;
                }

                ext.DeleteRow(9);
                ext.DeleteRow(8);
                ext.DeleteRow(7);

                ext.PrintPreview();
                ext.Close();

                if (this.InvokeRequired) { d d = delegate { toolStripButton3.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton3.Enabled = true; }
            }
            catch (Exception ex)
            {
                if (this.InvokeRequired) { d d = delegate { toolStripButton3.Enabled = true; }; this.Invoke(d); }
                else { toolStripButton3.Enabled = true; }
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

    }
}
