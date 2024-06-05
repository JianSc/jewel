using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace Mdian.报表
{
    public partial class 销售年度统计 : Form
    {
        const int panH = 394;
        const int panY = 446;
        ToolStripMenuItem panMENU;

        public 销售年度统计(ToolStripMenuItem menu)
        {
            panMENU = menu;
            InitializeComponent();
        }

        Panel[] mypan = new Panel[12];

        private void 销售年度统计_Load(object sender, EventArgs e)
        {
            int year = int.Parse(DateTime.Now.Year.ToString());
            for (int i = 0; i < 10; i++)
            {
                int j = year - i;
                toolStripComboBox1.Items.Add(j.ToString()+"年");
            }

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            mypan[0] = panel1;
            mypan[1] = panel2;
            mypan[2] = panel3;
            mypan[3] = panel4;
            mypan[4] = panel5;
            mypan[5] = panel6;
            mypan[6] = panel7;
            mypan[7] = panel8;
            mypan[8] = panel9;
            mypan[9] = panel10;
            mypan[10] = panel11;
            mypan[11] = panel12;

            dataGridView1.RowCount = 1;
        }

        private void 销售年度统计_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text == string.Empty)
            {
                toolStripComboBox1.DroppedDown = true;
                return;
            }

            yearint = toolStripComboBox1.Text.Replace("年",string.Empty);

            Thread mythread = new Thread(new ThreadStart(getdata));
            mythread.Start();
        }

        private string yearint = string.Empty;

        private DateTime GetDateDayMax(string year, string month, bool maxORmin)
        {
            string rendate = string.Empty;
            DateTime a;
            if (maxORmin)
            {
                rendate = year + "-" + month + "-31 23:59:59";
                if (!DateTime.TryParse(rendate, out a))
                {
                    rendate = year + "-" + month + "-30 23:59:59";
                    if (!DateTime.TryParse(rendate, out a))
                    {
                        rendate = year + "-" + month + "-29 23:59:59";
                        if (!DateTime.TryParse(rendate, out a))
                        {
                            rendate = year + "-" + month + "-28 23:59:59";
                            if (!DateTime.TryParse(rendate, out a))
                            {
                                rendate = year + "-" + month + "-27 23:59:59";
                            }
                        }
                    }
                }
            }
            else
            {
                rendate = year + "-" + month + "-01 00:00:00";
            }
            return DateTime.Parse(rendate);
        }

        private delegate void mydele();

        private void getdata()
        {
            DateTime date1 = DateTime.Parse(yearint + "-01-01 00:00:00");
            DateTime date2 = DateTime.Parse(yearint + "-12-31 23:59:59");

            DataTable yeardt = new DataTable();
            double sumdata = 0.00;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(SSALE) AS SSALE FROM QGOODS_SALES WHERE (MDIAN='" + xconfig.MDIAN + "') AND (SETTIME BETWEEN '" + date1 + "' AND '" + date2 + "')", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    try { sumdata = int.Parse(rs["SSALE"].ToString()); }
                    catch { sumdata = 0; }
                }
                rs.Close();
                for (int i = 1; i < 13; i++)
                {
                    cmd = new SqlCommand("SELECT SUM(SSALE) AS SSALE FROM QGOODS_SALES WHERE (MDIAN='" + xconfig.MDIAN + "') AND (SETTIME BETWEEN '" + GetDateDayMax(yearint, i.ToString(), false) + "' AND '" + GetDateDayMax(yearint, i.ToString(), true) + "')", conn);
                    rs = cmd.ExecuteReader();
                    int v = i - 1;
                    if (rs.Read())
                    {
                        if (dataGridView1.InvokeRequired)
                        {
                            mydele d = delegate
                            {
                                DataGridViewRow j = dataGridView1.Rows[0];
                                j.Cells[v].Value = rs["SSALE"].ToString();
                                if (j.Cells[v].Value.ToString() == string.Empty)
                                {
                                    j.Cells[v].Value = "0";
                                }
                            };
                            dataGridView1.Invoke(d);
                        }
                        else
                        {
                            DataGridViewRow j = dataGridView1.Rows[0];
                            j.Cells[v].Value = rs["SSALE"].ToString();
                            if (j.Cells[v].Value.ToString() == string.Empty)
                            {
                                j.Cells[v].Value = "0";
                            }
                        }

                        double abc = 0.00;
                        try { abc = int.Parse(rs["SSALE"].ToString()); }
                        catch { abc = 0.00; }

                        double abcd;
                        try { abcd = abc / sumdata; }
                        catch { abcd = 0; }
                        int aac = (int)(panH * abcd);
                        if (aac < 4) { aac = 4; }

                        if (mypan[v].InvokeRequired)
                        {
                            mydele d = delegate
                            {
                                for (int ii = 1; ii < aac; ii++)
                                {
                                    //mypan[v].Visible = false;
                                    int w = 68;
                                    int h = ii;
                                    mypan[v].Size = new Size(w, h);
                                    int x = mypan[v].Location.X;
                                    int y = 0;
                                    if (ii > 4) { y = panY - ii; }
                                    else { y = panY - 0; }
                                    Point xy = new Point(x, y);
                                    mypan[v].Location = xy;
                                    //mypan[v].Visible = true;
                                    Application.DoEvents();
                                }
                            };
                            mypan[v].Invoke(d);
                        }
                        else
                        {
                            int x = mypan[v].Location.X;
                            int y = 0;
                            if (aac > 4) { y = panY - aac; }
                            else { y = panY - 0; }
                            Point xy = new Point(x, y);
                            mypan[v].Location = xy;
                            int w = 68;
                            int h = aac;
                            mypan[v].Size = new Size(w, h);
                        }
                    }
                    rs.Close();
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}