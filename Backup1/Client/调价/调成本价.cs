using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Client.调价
{
    public partial class 调成本价 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable thisDST = new clidata().Tables["调成本价"];
        public 调成本价(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        private void 调成本价_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            dataGridView1.DataSource = thisDST;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 调成本价_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton1.Enabled = false;
        }

        public static void CbeiNUM(DataGridView dgv,int rowint)
        {
            DataGridViewRow dgr = dgv.Rows[rowint];
            double jja, pjja, zsja, zsje, fsja, fsje, jgdj, other, jgsh, zsz, fsz, jinz,fsjeone,zsjeone;
            decimal jgsh3, pjja2, zsje2, fsje2, jgdj2, other2, cbei2;
            try { jja = double.Parse(dgr.Cells["jJADataGridViewTextBoxColumn"].Value.ToString()); }
            catch { jja = 0; }
            try { pjja = double.Parse(dgr.Cells["pJJADataGridViewTextBoxColumn"].Value.ToString()); }
            catch { pjja = 0; }
            try { zsz = double.Parse(dgr.Cells["ZSZ"].Value.ToString()); }
            catch { zsz = 0; }
            try { zsja = double.Parse(dgr.Cells["zSJADataGridViewTextBoxColumn"].Value.ToString()); }
            catch { zsja = 0; }
            try { fsz = double.Parse(dgr.Cells["FSZ"].Value.ToString()); }
            catch { fsz = 0; }
            try { fsja = double.Parse(dgr.Cells["fSJADataGridViewTextBoxColumn"].Value.ToString()); }
            catch { fsja = 0; }
            try { jgdj = double.Parse(dgr.Cells["jGDJDataGridViewTextBoxColumn"].Value.ToString()); }
            catch { jgdj = 0; }
            try { other = double.Parse(dgr.Cells["oTHERDataGridViewTextBoxColumn"].Value.ToString()); }
            catch { other = 0; }
            try { jgsh = double.Parse(dgr.Cells["jGSHDataGridViewTextBoxColumn"].Value.ToString()); }
            catch { jgsh = 0; }
            try { jinz = double.Parse(dgr.Cells["JINZ"].Value.ToString()); }
            catch { jinz = 0; }
            try { zsjeone = double.Parse(dgr.Cells["zSJEDataGridViewTextBoxColumn"].Value.ToString()); }
            catch { zsjeone = 0; }
            try { fsjeone = double.Parse(dgr.Cells["fSJEDataGridViewTextBoxColumn"].Value.ToString()); }
            catch { fsjeone = 0; }
            
            zsje = zsz * zsja;
            fsje = fsz * fsja;
            double jgsh2 = (jinz * jgsh) * jja;
            //int itemjgsh = (int)jgsh2 * 100;
            //jgsh2 = itemjgsh / 100;

            jgsh3 = decimal.Round((decimal)jgsh2, 2);
            pjja2 = decimal.Round((decimal)pjja, 2);
            if (zsz == 0 || zsja == 0)
            {
                zsje2 = decimal.Round((decimal)zsjeone, 2);
            }
            else
            {
                zsje2 = decimal.Round((decimal)zsje, 2);
            }
            if (fsz == 0 || fsja == 0)
            {
                fsje2 = decimal.Round((decimal)fsjeone, 2);
            }
            else
            {
                fsje2 = decimal.Round((decimal)fsje, 2);
            }
            jgdj2 = decimal.Round((decimal)jgdj, 2);
            other2 = decimal.Round((decimal)other, 2);

            cbei2 = jgsh3 + pjja2 + zsje2 + fsje2 + jgdj2 + other2;
            //cbei2 = decimal.Round((decimal)cbei, 2);

            dgr.Cells["fSJEDataGridViewTextBoxColumn"].Value = fsje2.ToString();
            dgr.Cells["zSJEDataGridViewTextBoxColumn"].Value = zsje2.ToString();
            dgr.Cells["cBEIDataGridViewTextBoxColumn"].Value = cbei2.ToString();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CbeiNUM(dataGridView1, dataGridView1.CurrentCell.RowIndex);

            toolStripButton1.Enabled = true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            new 成本单件添加(thisDST).ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            new 综合查询打印(thisDST, "cbei").ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }
            new 调金价(dataGridView1).ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (thisDST.Rows.Count < 1)
            {
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                foreach (DataRow i in thisDST.Rows)
                {
                    SqlCommand cmd = new SqlCommand("QZ_GOODSTJ", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", i["TM"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@CBEI", i["CBEI"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                    cmd.Parameters.Add(new SqlParameter("@JJA", i["JJA"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@PJJA", i["PJJA"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@ZSJA", i["ZSJA"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@ZSJE", i["ZSJE"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@FSJA", i["FSJA"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@FSJE", i["FSJE"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@JGDJ", i["JGDJ"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@OTHER", i["OTHER"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@JGSH", i["JGSH"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@ZSHI", i["ZSHI"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@FSHI", i["FSHI"].ToString()));
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            toolStripButton1.Enabled = false;
            dataGridView1.Enabled = false;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            string TM = dataGridView1["tMDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();

            DataRow[] dr = thisDST.Select("TM='" + TM + "'", "");
            if (dr.Length > 0)
            {
                foreach (DataRow i in dr)
                {
                    thisDST.Rows.Remove(i);
                }
            }
        }
    }
}