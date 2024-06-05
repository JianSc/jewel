using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace Client.报表
{
    public partial class 客户积分 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable thisDT = new clidata().Tables["客户积分"];

        public 客户积分(ToolStripMenuItem m)
        {
            panMENU = m;
            InitializeComponent();
        }

        private void 客户积分_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            //this.TopMost = true;

            MSG.login.ShowIMG();
            Application.DoEvents();

            Thread t = new Thread(new ThreadStart(show));
            t.Start();
        }

        private delegate void d();

        private void show()
        {
            Thread.Sleep(500);
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(KH) AS KHH,JF,RTRIM(QKHU.XIN) AS XIN,RTRIM(QKHU.MIN) AS MIN,RTRIM(QKHU.SJI) AS SJI,RTRIM(QKHU.XBIE) AS XBIE,RTRIM(QKHU.EMAIL) AS EMAIL,RTRIM(QKHU.DQUSEN) AS DQUSEN,RTRIM(QKHU.DQUSI) AS DQUSI,CONVERT(CHAR(10),QKHU.SRI,120) AS SRI FROM qGOODS_CUSTJF INNER JOIN QKHU ON QKHU.KHUHAO=QGOODS_CUSTJF.KH", conn);
                da.Fill(thisDT);
                conn.Close();

                if (dataGridView1.InvokeRequired)
                {
                    d d = delegate { dataGridView1.DataSource = thisDT; };
                    dataGridView1.Invoke(d);
                }
                else
                {
                    dataGridView1.DataSource = thisDT;
                }
                if (thisDT.Rows.Count > 18)
                {
                    if (pictureBox1.InvokeRequired) { d d = delegate { pictureBox1.Visible = false; }; pictureBox1.Invoke(d); }
                    else { pictureBox1.Visible = false; }
                }
                config.conDGV.DGVAutoID(dataGridView1, "phao");
                MSG.login.CloseIMG();
                if (this.InvokeRequired) { d d = delegate { this.TopMost = true; }; this.Invoke(d); }
                else { this.TopMost = true; }
            }
            catch (Exception ex)
            {
                MSG.login.CloseIMG();
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 客户积分_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }
            string dh = dataGridView1["kHHDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (dh == string.Empty)
            {
                return;
            }

            this.TopMost = false;
            Form itemform = new 报表.客户积分详细(this, dh);
            itemform.Show();
        }

        ToolStripButton theBT = new ToolStripButton();

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            theBT = toolStripButton3;
            MSG.login.ShowIMG();
            Application.DoEvents();

            theBT.Enabled = false;

            Thread t = new Thread(new ThreadStart(theed));
            t.Start();
        }

        private void theed()
        {
            GoldPrinter.ExcelAccess ext = new GoldPrinter.ExcelAccess();
            ext.Open(Application.StartupPath + @"\00I01.xlt");
            ext.FormCaption = "Colorbay";

            int r = 2;

            ext.SetCellText(1, "A", "客户号");
            ext.SetCellText(1, "B", "客户姓名");
            ext.SetCellText(1, "C", "性别");
            ext.SetCellText(1, "D", "生日");
            ext.SetCellText(1, "E", "Email");
            ext.SetCellText(1, "F", "省");
            ext.SetCellText(1, "G", "市");
            ext.SetCellText(1, "H", "电话");
            ext.SetCellText(1, "I", "积分");

            foreach (DataRow i in thisDT.Rows)
            {
                ext.SetCellText(r, "A", i["KHH"].ToString());
                ext.SetCellText(r, "B", i["NAME"].ToString());
                ext.SetCellText(r, "C", i["XBIE"].ToString());
                ext.SetCellText(r, "D", i["SRI"].ToString());
                ext.SetCellText(r, "E", i["EMAIL"].ToString());
                ext.SetCellText(r, "F", i["DQUSEN"].ToString());
                ext.SetCellText(r, "G", i["DQUSI"].ToString());
                ext.SetCellText(r, "H", i["SJI"].ToString());
                ext.SetCellText(r, "I", i["JF"].ToString());
                r++;
            }

            MSG.login.CloseIMG();

            if (this.InvokeRequired) { d d = delegate { theBT.Enabled = true; }; this.Invoke(d); }
            else { theBT.Enabled = true; }

            ext.ShowExcel();

            if (this.InvokeRequired) { d d = delegate { this.Close(); }; this.Invoke(d); }
            else { this.Close(); }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView1, "phao");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            string mailto = "mailto:";

            foreach (DataRow i in thisDT.Rows)
            {
                if (i["EMAIL"].ToString() != string.Empty)
                {
                    if (mailto != "mailto:")
                    {
                        mailto += ";";
                        mailto += i["EMAIL"].ToString();
                    }
                    else
                    {
                        mailto += i["EMAIL"].ToString();
                    }
                }
            }

            if (mailto != "mailto:")
            {
                System.Diagnostics.Process.Start(mailto);
            }
        }
    }
}