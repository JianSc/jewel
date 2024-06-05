using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mdian.退货
{
    public partial class 退货状态 : Form
    {
        DataTable isDT = new theDST().Tables["门店退货"];
        ToolStripMenuItem panMENU;
        Form panFORM;

        public 退货状态(ToolStripMenuItem t,Form f)
        {
            InitializeComponent();
            panMENU = t;
            panFORM = f;
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT A.SETTIME,A.[USER],A.MDIAN,A.SLIANG,A.SBM,A.DH,A.USERS,B.NTXT FROM (SELECT SETTIME,[USER],MDIAN,SUM(SLIANG) AS SLIANG,SBM,DH,USERS FROM (SELECT RTRIM([USER]) AS [USER],CONVERT(CHAR(10),SETTIME,120) AS SETTIME,RTRIM(MDIAN) AS MDIAN,SLIANG,RTRIM(SBM) AS SBM,RTRIM(DH) AS DH,RTRIM(USERS) AS USERS FROM QGOODS_BACK WHERE (MDIAN='" + xconfig.MDIAN + "')) DERIVEDTBL "
            + "GROUP BY SBM,SETTIME,[USER],[USERS],DH,MDIAN) A INNER JOIN QGOODS_BACK_TXT B ON A.SBM=B.SBM";
            config.conData.fill("sql", constr, cmdstr, isDT);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 退货状态_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            DataView dv = new DataView();
            dv.Table = isDT;
            dv.Sort = "id desc";
            dataGridView1.DataSource = dv;

            foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                if (i.Cells["uSERSDataGridViewTextBoxColumn"].Value.ToString() != string.Empty)
                {
                    i.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                if (i.Cells["dHDataGridViewTextBoxColumn"].Value.ToString() == "{撤退}")
                {
                    i.DefaultCellStyle.BackColor = Color.LightSalmon;
                }
            }
        }

        private void 退货状态_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count < 1)
            {
                return;
            }

            string n_sbm = dataGridView1["sBMDataGridViewTextBoxColumn",dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if(n_sbm == string.Empty)
            {
                return;
            }

            this.TopMost = false;

            Form newform = new 退货详情(panFORM, n_sbm);
            newform.MdiParent = panFORM;
            newform.Show();

        }
    }
}