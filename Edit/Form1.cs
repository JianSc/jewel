using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Edit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string constr;
                bool isbol = checkBox1.Checked;
                if (isbol)
                {
                    constr = "SELECT A.TM FROM QGOODS_BACK A INNER JOIN QGOODS_CKU B ON A.TM=B.TM WHERE (A.SBM='" + textBox1.Text + "')";
                }
                else
                {
                    constr = "SELECT A.TM FROM QGOODS_BACK A INNER JOIN QGOODS_CKU B ON A.TM=B.TM WHERE (A.DH='" + textBox1.Text + "')";
                }
                DataTable adt = new DataTable();
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(constr, conn);
                da.Fill(adt);

                foreach (DataRow i in adt.Rows)
                {
                    if (i["TM"].ToString() != "")
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE QGOODS_CKU SET NAME='" + textBox2.Text + "' WHERE TM='" + i["TM"].ToString() + "'", conn);
                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();

                MessageBox.Show("修改完毕！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool isBOL = ((CheckBox)sender).Checked;
            if (isBOL)
            {
                label1.Text = "识别码:";
            }
            else
            {
                label1.Text = "退单号:";
            }
        }
    }
}