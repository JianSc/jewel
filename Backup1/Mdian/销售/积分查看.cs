using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Text.RegularExpressions;

namespace Mdian.销售
{
    public partial class 积分查看 : Form
    {
        string theKHJF = string.Empty;
        DataTable theDT = new theDST().Tables["客户购买记录"];
        TextBox tBox;
        CheckBox theCK;
        TextBox theTB2;

        public 积分查看(string jf,TextBox tb,CheckBox ck,TextBox tb2)
        {
            theCK = ck;
            theTB2 = tb2;
            theKHJF = jf;
            tBox = tb;
            InitializeComponent();
        }

        private void 积分查看_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.dataGridView1.DataSource = theDT;

            if (theKHJF == string.Empty)
            {
                MessageBox.Show("内部错误！！\n请与管理员联系", "出错了", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            label3.Text = theKHJF;

            for (int i = 1; i < 31; i++)
            {
                int j = i * 100;
                comboBox1.Items.Add(j.ToString());
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT JF FROM QGOODS_CUSTJF WHERE KH = '" + theKHJF + "'", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    label4.Text = rs["JF"].ToString().Trim();
                }
                else
                {
                    label4.Text = "0";
                }
                rs.Close();

                SqlDataAdapter da = new SqlDataAdapter("SELECT CONVERT(CHAR(10),QGOODS_SALES.SETTIME,120) AS SETTIME,RTRIM(QGOODS_SALES.TM) AS TM,SSALE,ZKOU,RTRIM(MDIAN) AS MDIAN,RTRIM(QGOODS.JLIAO) AS JLIAO,"
                    + "RTRIM(QGOODS.SLIAO) AS SLIAO,RTRIM(QGOODS.SSI) AS SSI FROM QGOODS_SALES INNER JOIN "
                    + "QGOODS ON QGOODS.TM=QGOODS_SALES.TM "
                    + "WHERE QGOODS_SALES.KEHUID = '" + theKHJF + "'", conn);

                theDT.Clear();
                da.Fill(theDT);

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            try
            {
                i = int.Parse(((ComboBox)sender).Text);
            }
            catch { return; }

            i = i * 50;
            label6.Text = "(积分需求:" + i.ToString() + ")";

            ((ComboBox)sender).BackColor = Color.White;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 8)
            {
                e.Handled = false;
            }
            else if (key == 13)
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

            ((ComboBox)sender).BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k = 0;
            try
            {
                k = int.Parse(comboBox1.Text);
            }
            catch
            {
                comboBox1.BackColor = Color.MistyRose;
                return;
            }

            if (k < 100)
            {
                comboBox1.Text = "100";
                comboBox1.BackColor = Color.MistyRose;
                return;
            }

            int jf = int.Parse(comboBox1.Text) * 50;

            label6.Text = "(积分需求:" + jf.ToString() + ")";
            Application.DoEvents();

            if (int.Parse(label4.Text) < jf)
            {
                MessageBox.Show("客户积分不够！");
                return;
            }

            tBox.Text = comboBox1.Text;
            theCK.Enabled = false;
            theTB2.Enabled = false;
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}