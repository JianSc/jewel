using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client.分析
{
    public partial class 发货状态查询 : Form
    {
        DataTable cliDST = new clidata().Tables["发货单列表"];
        DataGridView cliDGV;
        ToolStripMenuItem panMENU;
        Form panFORM;

        public 发货状态查询(ToolStripMenuItem menu,Form panform)
        {
            panFORM = panform;

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(DH) AS DH,RTRIM([USER]) AS [USER],SETTIME AS XTIME,SUM(SL) AS SL,RTRIM(MDIAN) AS MDIAN,RTRIM(MDIANUSER) AS MDIANUSER FROM "
            + "(SELECT DH,[USER],CONVERT(CHAR(10),SETTIME,120) AS SETTIME,SL,MDIAN,MDIANUSER FROM QGOODS_CKD) DERIVEDTBL "
            + "GROUP BY DH,[USER],SETTIME,MDIAN,MDIANUSER";
            config.conData.fill("sql", constr, cmdstr, cliDST);
            panMENU = menu;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void 发货状态查询_Load(object sender, EventArgs e)
        {
            cliDGV = dataGridView1;

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            DataView dv = new DataView();
            dv.Table = cliDST;
            dv.Sort = "id desc";
            cliDGV.DataSource = dv;

            foreach (DataGridViewRow i in cliDGV.Rows)
            {
                if (i.Cells["mDIANUSERDataGridViewTextBoxColumn"].Value.ToString().Trim() != "")
                {
                    i.DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        private void 发货状态查询_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
            string msgstr = xconfig.USER + "|闲置|USERCLOSE<EOF>";
            xconfig.netSend(msgstr);
        }

        private void 发货状态查询_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}