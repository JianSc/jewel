using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Analysis
{
    public partial class 销售统计 : Form
    {
        ToolStripMenuItem panM;

        public 销售统计(ToolStripMenuItem m)
        {
            panM = m;
            InitializeComponent();
        }

        private void 销售统计_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            dateTimePicker1.Text = DateTime.Now.AddMonths(-1).ToShortDateString();

            try
            {
                SqlConnection conn = new SqlConnection(SYS.ConStr);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM(NAME) AS NAME FROM QGYSHANG", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox1.Items.Add(string.Empty);
                foreach (DataRow i in dt.Rows)
                {
                    comboBox1.Items.Add(i["name"].ToString());
                }

                da = new SqlDataAdapter("SELECT RTRIM(NAME) AS NAME FROM QMDIAN", conn);
                dt.Clear();
                da.Fill(dt);
                comboBox2.Items.Add(string.Empty);
                foreach (DataRow i in dt.Rows)
                {
                    comboBox2.Items.Add(i["name"].ToString());
                }
                da = new SqlDataAdapter("SELECT RTRIM(NAME) AS NAME FROM QYGONG", conn);
                dt.Clear();
                da.Fill(dt);
                comboBox3.Items.Add(string.Empty);
                foreach (DataRow i in dt.Rows)
                {
                    comboBox3.Items.Add(i["name"].ToString());
                }
                da = new SqlDataAdapter("SELECT RTRIM(NAME) AS NAME FROM QDQU WHERE (LV=1)", conn);
                dt.Clear();
                da.Fill(dt);
                comboBox4.Items.Add(string.Empty);
                foreach (DataRow i in dt.Rows)
                {
                    comboBox4.Items.Add(i["name"].ToString());
                }
                da = new SqlDataAdapter("SELECT RTRIM(NAME) AS NAME FROM QJLIAO", conn);
                dt.Clear();
                da.Fill(dt);
                comboBox5.Items.Add(string.Empty);
                foreach (DataRow i in dt.Rows)
                {
                    comboBox5.Items.Add(i["name"].ToString());
                }
                da = new SqlDataAdapter("SELECT RTRIM(NAME) AS NAME FROM QSLIAO", conn);
                dt.Clear();
                da.Fill(dt);
                comboBox6.Items.Add(string.Empty);
                foreach (DataRow i in dt.Rows)
                {
                    comboBox6.Items.Add(i["name"].ToString());
                }
                da = new SqlDataAdapter("SELECT RTRIM(NAME) AS NAME FROM QSSHI", conn);
                dt.Clear();
                da.Fill(dt);
                comboBox7.Items.Add(string.Empty);
                foreach (DataRow i in dt.Rows)
                {
                    comboBox7.Items.Add(i["name"].ToString());
                }

                da.Dispose();
                conn.Close();

            }
            catch { }

            string[] months = { "名称", "数量","成本","数量","成本", "数量", "成本", "销售额", "数量", "成本", "销退额","数量","成本" };
            for (int i = 0; i < months.Length; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), months[i]);
            }

            foreach(DataGridViewColumn i in dataGridView1.Columns)
            {
                i.SortMode = DataGridViewColumnSortMode.NotSortable;
                if(i.HeaderText=="数量" ){i.Width = 45;}
                if(i.HeaderText =="名称"){i.Width =200;}
            }

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            MSG.login.Close();

        }

        private void 销售统计_FormClosed(object sender, FormClosedEventArgs e)
        {
            panM.Enabled = true;
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                StringFormat strmat = new StringFormat();
                strmat.Alignment = StringAlignment.Center;
                strmat.LineAlignment = StringAlignment.Center;

                string[] months = { "期初", "发货","销售", "销退" ,"库存"};

                //期初:
                Rectangle r1 = dataGridView1.GetColumnDisplayRectangle(1, true);
                r1.X += 1;
                r1.Y += 1;
                r1.Width = dataGridView1.Columns[1].Width + dataGridView1.Columns[2].Width - 2;
                r1.Height = dataGridView1.ColumnHeadersHeight / 2 - 2;
                e.Graphics.FillRectangle(new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1);
                e.Graphics.DrawString(months[0], dataGridView1.ColumnHeadersDefaultCellStyle.Font,
                    new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor), r1, strmat);

                //发货:
                r1 = dataGridView1.GetColumnDisplayRectangle(3, true);
                r1.X += 1;
                r1.Y += 1;
                r1.Width = dataGridView1.Columns[3].Width + dataGridView1.Columns[4].Width - 2;
                r1.Height = dataGridView1.ColumnHeadersHeight / 2 - 2;
                e.Graphics.FillRectangle(new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1);
                e.Graphics.DrawString(months[1], dataGridView1.ColumnHeadersDefaultCellStyle.Font,
                    new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor), r1, strmat);

                //销售:
                r1 = dataGridView1.GetColumnDisplayRectangle(5, true);
                r1.X += 1;
                r1.Y += 1;
                r1.Width = dataGridView1.Columns[5].Width + dataGridView1.Columns[6].Width + dataGridView1.Columns[7].Width - 2;
                r1.Height = dataGridView1.ColumnHeadersHeight / 2 - 2;
                e.Graphics.FillRectangle(new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1);
                e.Graphics.DrawString(months[2], dataGridView1.ColumnHeadersDefaultCellStyle.Font,
                    new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor), r1, strmat);

                //销退:
                r1 = dataGridView1.GetColumnDisplayRectangle(8, true);
                r1.X += 1;
                r1.Y += 1;
                r1.Width = dataGridView1.Columns[8].Width + dataGridView1.Columns[9].Width + dataGridView1.Columns[10].Width - 2;
                r1.Height = dataGridView1.ColumnHeadersHeight / 2 - 2;
                e.Graphics.FillRectangle(new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1);
                e.Graphics.DrawString(months[3], dataGridView1.ColumnHeadersDefaultCellStyle.Font,
                    new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor), r1, strmat);

                //库存
                r1 = dataGridView1.GetColumnDisplayRectangle(11, true);
                r1.X += 1;
                r1.Y += 1;
                r1.Width = dataGridView1.Columns[11].Width + dataGridView1.Columns[12].Width - 2;
                r1.Height = dataGridView1.ColumnHeadersHeight / 2 - 2;
                e.Graphics.FillRectangle(new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1);
                e.Graphics.DrawString(months[4], dataGridView1.ColumnHeadersDefaultCellStyle.Font,
                    new SolidBrush(dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor), r1, strmat);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime a1 = DateTime.Parse(dateTimePicker1.Text + " 00:00:00");
            DateTime a2 = DateTime.Parse(dateTimePicker2.Text + " 23:59:59");

            

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SYS.ASC(e.KeyChar.ToString()) == 13)
            {
                SendKeys.Send("{Tab}");
                return;
            }
            if (SYS.ASC(e.KeyChar.ToString()) != 8)
            {
                Regex regex = new Regex(@"^\d$|^\.$");
                if (!regex.Match(e.KeyChar.ToString()).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            TextBox ttx = (TextBox)sender;

            Regex regex = new Regex(@"^\d+\.\d{1,3}$");
            if (!regex.Match(ttx.Text).Success)
            {
                ttx.Text = string.Empty;
            }
        }

    }
}