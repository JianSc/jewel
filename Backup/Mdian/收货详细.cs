using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mdian
{
    public partial class 收货详细 : Form
    {
        string panDH;
        DataTable cliDST = new theDST().Tables["goods"];
        Form panFORM;

        public 收货详细(string dh,Form f)
        {
            panFORM = f;
            panDH = dh;
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT ID,RTRIM(QGOODS.TM) AS TM,RTRIM(JLIAO) AS JLIAO,RTRIM(SLIAO) AS SLIAO,RTRIM(SSI) AS SSI,"
            + "RTRIM(QKOU) AS QKOU,RTRIM(ZSHU) AS ZSHU,RTRIM(KUS) AS KUS,RTRIM(HHAO) AS HHAO,RTRIM(DWEI) AS DWEI,"
            + "SLIANG,JIANZ,JINZ,PJIANZ,ZSZ,ZSS,FSZ,FSS,CBEI,XSOU,BZHU,SLBOL,IMGBOL,RTRIM(YSE) AS YSE,RTRIM(XZUANG) AS XZUANG,"
            + "RTRIM(QGONG) AS QGONG,RTRIM(JDU) AS JDU FROM QGOODS INNER JOIN QGOODS_CKD ON QGOODS.TM=QGOODS_CKD.TM "
            + "WHERE (QGOODS_CKD.XSTAT=1) AND (QGOODS_CKD.DH='" + panDH + "')";
            config.conData.fill("sql", constr, cmdstr, cliDST);
            InitializeComponent();
        }

        private void 收货详细_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            toolStripTextBox1.Text = panDH;
            dataGridView1.DataSource = cliDST;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if(cliDST.Rows.Count < 1)
            {
                return;
            }

            string TM ;
            TM = dataGridView1["tmDataGridViewTextBoxColumn",dataGridView1.CurrentCell.RowIndex].Value .ToString();

            bool imgbol;
            imgbol = bool.Parse(((DataGridView)sender)["imgbolDataGridViewCheckBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString());

            toolStripButton3.Enabled = imgbol;

            if (panel1.Visible)
            {
                if (imgbol)
                {
                    Image itemimg;
                    Bitmap itembmp;
                    itemimg = xconfig.netImgGET(xconfig.USER + "|收货接单|GETBMP|" + TM + "<EOF>");
                    itembmp = new Bitmap(itemimg, pictureBox1.Size);
                    pictureBox1.Image = itembmp;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.ImgGet_Err;
                }
            }
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

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cliDST.Rows.Count < 1) { return; }

            ((DataGridViewRow)((DataGridView)sender).CurrentRow).DefaultCellStyle.BackColor = Color.Green;
        }

        private void 未勾对ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cliDST.Rows.Count < 1) { return; }
            DataGridViewRow itemrow;
            itemrow = dataGridView1.CurrentRow;
            itemrow.DefaultCellStyle.BackColor = Color.Empty;
        }

        private void 收货详细_FormClosed(object sender, FormClosedEventArgs e)
        {
            panFORM.TopMost = true;
        }
    }
}