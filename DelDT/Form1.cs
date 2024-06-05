using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DelDT
{
    public partial class Form1 : Form
    {
        string ConStr = string.Empty;

        DataTable ckuDT = new Dst().Tables["cku"];
        DataTable stongDT = new Dst().Tables["stong"];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            dataGridView1.DataSource = stongDT;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == string.Empty)
            {
                textBox2.Focus();
                return;
            }
            if (textBox3.Text == string.Empty)
            {
                textBox3.Focus();
                return;
            }

            ConStr = "SERVER=" + textBox3.Text + ";DATABASE=Qzhi;UID=" + textBox1.Text + ";PWD=" + textBox2.Text;

            try
            {
                SqlConnection conn = new SqlConnection(ConStr);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(NAME) AS NAME FROM QCKU", conn);
                ckuDT.Clear();
                da.Fill(ckuDT);
                da = new SqlDataAdapter("SELECT RTRIM(NAME) AS NAME FROM QMDIAN", conn);
                da.Fill(ckuDT);
                conn.Close();

                comboBox1.Items.Clear();

                foreach (DataRow i in ckuDT.Rows)
                {
                    comboBox1.Items.Add(i["NAME"].ToString());
                }

                MessageBox.Show("连接成功！\n现在你可以进行数据的操作了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                button1.Enabled = false;

            }
            catch
            {
                MessageBox.Show("数据库连接不正常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            if (MessageBox.Show("请注意\n\n此操作将删除以下列表商品的所有记录！\n包括入库、销售、交易往来等等。\n请确定是否删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(ConStr);
                conn.Open();
                SqlCommand cmd;
                foreach (DataRow i in stongDT.Rows)
                {
                    string TM = i["TM"].ToString();
                    if (TM == string.Empty)
                    {
                        continue;
                    }

                    cmd = new SqlCommand("DELETE FROM QGOODS WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_BACK WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_CBEI_LOG WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_CKD WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_CKU WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_CKU2 WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_GYS_LIST WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_KUT WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_LOG WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_RKD WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_SALES WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_SALES_BACK WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("DELETE FROM QGOODS_XSED_LOG WHERE (TM='" + TM + "')", conn);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

                MessageBox.Show("操作完毕！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                comboBox1.DroppedDown = true;
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(ConStr);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(A.TM) AS TM,CONVERT(CHAR(10),B.SETDATE,120) AS SETTIME,RTRIM(C.JLIAO) AS JLIAO,RTRIM(C.SLIAO) AS SLIAO,RTRIM(C.SSI) AS SSI,C.ZSZ,C.ZSS,C.FSZ,C.FSS,C.JIANZ,C.JINZ "
                    + "FROM QGOODS_CKU A INNER JOIN QGOODS_RKD B ON B.TM=A.TM INNER JOIN QGOODS C ON C.TM=A.TM WHERE (A.NAME='" + comboBox1.Text + "')", conn);

                stongDT.Clear();

                da.Fill(stongDT);
                conn.Close();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1["phao", i].Value = i + 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}