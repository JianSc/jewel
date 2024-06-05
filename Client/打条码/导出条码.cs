using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;

namespace Client.打条码
{
    public partial class 导出条码 : Form
    {
        ToolStripMenuItem panMENU;
        public 导出条码(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        private void 导出条码_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

        }

        private void 导出条码_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView1.Rows.Count > 16)
            {
                pictureBox1.Visible = false;
            }
            else if (dataGridView1.Rows.Count < 17)
            {
                pictureBox1.Visible = true;
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView1_RowsAdded(sender, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 2)
            {
                return;
            }

            if(!File.Exists(Application.StartupPath +@"\TM.mdb"))
            {
                MessageBox.Show("存储条码文件不存在！\n请联系管理员。","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            int j = 0;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd;

                OleDbConnection oleconn = new OleDbConnection(xconfig.oldstr("TM"));
                oleconn.Open();
                OleDbCommand olecmd = new OleDbCommand("DELETE FROM TM", oleconn);
                olecmd.ExecuteNonQuery();

                for(int l=1;l<dataGridView1.Rows.Count;l++)
                {
                    DataGridViewRow i = dataGridView1.Rows[l - 1];
                    cmd = new SqlCommand("SELECT RTRIM(JLIAO) AS NAME_O,RTRIM(SLIAO) AS NAME_G,RTRIM(SSI) AS NAME_C,"
                        + "XSOU AS AMOUNT,TM AS ID,JINZ AS WEIGHT_G,QKOU AS [SIZE],ZSS,ZSZ,FSS,FSZ,RTRIM(KUS) AS TYPE_ID,RTRIM(ZSHU) AS BOOK_ID,RTRIM(JDU) AS CLEAN,RTRIM(YSE) AS COLOR FROM QGOODS WHERE (TM='" + i.Cells["TM"].Value.ToString().Trim() + "')", conn);
                    SqlDataReader rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        string size_o;
                        if (rs["SIZE"].ToString() == "0")
                        {
                            size_o = string.Empty;
                        }
                        else
                        {
                            size_o = rs["SIZE"].ToString();
                        }
                        olecmd = new OleDbCommand("INSERT INTO TM(NAME_O,NAME_G,NAME_C,AMOUNT,ID,WEIGHT_G,[SIZE],STONE_M,STONE_A,TYPE_ID,BOOK_ID,CLEAN,COLOR)VALUES("
                            + "'" + rs["NAME_O"].ToString()
                        + "','" + rs["NAME_G"].ToString()
                        + "','" + rs["NAME_C"].ToString()
                        + "','" + rs["AMOUNT"].ToString()
                        + "','" + rs["ID"].ToString()
                        + "','" + rs["WEIGHT_G"].ToString()
                        + "','" + size_o
                        + "','" + rs["ZSZ"].ToString() + "/" + rs["ZSS"].ToString()
                        + "','" + rs["FSZ"].ToString() + "/" + rs["FSS"].ToString()
                        + "','" + rs["TYPE_ID"].ToString()
                        + "','" + rs["BOOK_ID"].ToString()
                        + "','" + rs["CLEAN"].ToString()
                        + "','" + rs["COLOR"].ToString()
                        + "')", oleconn);
                        olecmd.ExecuteNonQuery();
                        j++;
                    }
                    else
                    {
                        i.Cells["TM"].Style.BackColor = Color.Red;
                    }
                    rs.Close();
                }
                oleconn.Close();
                conn.Close();

                MSG.login.Close();
                MessageBox.Show("已导出 " + j.ToString() + " 个条码。");
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
            }
            
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton1.Enabled = false;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton1.Enabled = true;
        }
    }
}