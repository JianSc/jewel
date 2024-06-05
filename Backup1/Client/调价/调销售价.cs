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
    public partial class 调销售价 : Form
    {
        ToolStripMenuItem panMENU;
        DataTable thisDT = new clidata().Tables["调销售价"];

        public 调销售价(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 调销售价_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            dataGridView1.DataSource = thisDT;
        }

        private void 调销售价_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            new 售价单件添加(thisDT).ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            new 综合查询打印(thisDT, "xsou").ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            try
            {
                bool imgbol = bool.Parse(dataGridView1["IMGBOL", dataGridView1.CurrentCell.RowIndex].Value.ToString());

                toolStripButton6.Enabled = imgbol;
                string TM = ((DataGridView)sender)["tMDataGridViewTextBoxColumn", ((DataGridView)sender).CurrentCell.RowIndex].Value.ToString();

                if (panel1.Visible)
                {
                    if (imgbol)
                    {
                        Image itemimg;
                        Bitmap itembmp;
                        itemimg = xconfig.netImgGET(xconfig.USER + "|调销售价|GETBMP|" + TM + "<EOF>");
                        itembmp = new Bitmap(itemimg, pictureBox1.Size);
                        pictureBox1.Image = itembmp;
                    }
                    else
                    {
                        pictureBox1.Image = Properties.Resources.ImgGet_Err;
                    }
                }
            }
            catch { }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (!panel1.Visible)
            {
                panel1.Visible = true;

            }

            dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton1.Enabled = false;
        }

        public static void SaleNUM (DataGridView grd,int rows,bool sales)
        {
            //sales = false;
            DataGridViewRow iRow = grd.Rows[rows];
            double sale, blu, cbei;
            try { sale = double.Parse(iRow.Cells["mONYDataGridViewTextBoxColumn"].Value.ToString()); }
            catch { sale = 0; }
            try { blu = double.Parse(iRow.Cells["bLUDataGridViewTextBoxColumn"].Value.ToString()); }
            catch { blu = 1; }
            try { cbei = double.Parse(iRow.Cells["cBEIDataGridViewTextBoxColumn"].Value.ToString()); }
            catch { cbei = 0; }

            if (sales)
            {
                int aa = (int)((sale / cbei) * 100);
                blu = aa / 100.00;
                iRow.Cells["bLUDataGridViewTextBoxColumn"].Value = blu.ToString();
            }
            else
            {
                int salea = (int)(cbei * blu);
                iRow.Cells["mONYDataGridViewTextBoxColumn"].Value = salea.ToString();
            } 
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool salebol = false;
            if (e.ColumnIndex.ToString() == "4") { salebol = false; } else if (e.ColumnIndex.ToString() == "5") { salebol = true; }
            SaleNUM((DataGridView)sender, ((DataGridView)sender).CurrentCell.RowIndex, salebol);

            toolStripButton1.Enabled = true;
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }
            new 批调倍率(dataGridView1).ShowDialog();
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
                    cmd = new SqlCommand("QZ_GDOOSTJ_XSED", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TM", i["TM"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@XSOU", i["MONY"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                    cmd.Parameters.Add(new SqlParameter("@BLU", i["BLU"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@CBEI", i["CBEI"].ToString()));
                    cmd.ExecuteNonQuery();
                }

                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            toolStripButton1.Enabled = false;
            dataGridView1.Enabled = false;
        }
    }
}