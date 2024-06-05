using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mdian.报表
{
    public partial class 店员销售统计 : Form
    {
        DateTime a, b;
        ToolStripMenuItem panMenu;
        Form panFORM;

        public 店员销售统计(ToolStripMenuItem menu,Form panform)
        {
            panMenu = menu;
            panFORM = panform;

            InitializeComponent();
        }

        private void 店员销售统计_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            int coboxyear = DateTime.Now.Year;
            for (int i = 0; i < 8; i++)
            {
                int j = coboxyear - i;
                comboBox1.Items.Add(j.ToString() + "年");
            }
            for (int i = 1; i < 13; i++)
            {
                comboBox2.Items.Add(i.ToString() + "月");
            }
        }

        private DateTime Time_maxormin(string year,string month, bool max)
        {
            string aa = string.Empty;
            DateTime ab;
            if (max)
            {
                aa = year + "-" + month + "-31 23:59:59";
                if (!DateTime.TryParse(aa, out ab))
                {
                    aa = year + "-" + month + "-30 23:59:59";
                    if (!DateTime.TryParse(aa, out ab))
                    {
                        aa = year + "-" + month + "-29 23:59:59";
                        if (!DateTime.TryParse(aa, out ab))
                        {
                            aa = year + "-" + month + "-28 23:59:59";
                            if (!DateTime.TryParse(aa, out ab))
                            {
                                aa = year + "-" + month + "-27 23:59:59";
                            }
                        }
                    }
                }
            }
            else
            {
                aa = year + "-" + month + "-01 00:00:00";
            }

            return DateTime.Parse(aa);
        }
                

        private void button1_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 0)
            {
                if (comboBox1.Text == string.Empty)
                {
                    comboBox1.DroppedDown = true;
                    return;
                }
                if (comboBox2.Text == string.Empty)
                {
                    comboBox2.DroppedDown = true;
                    return;
                }
                MSG.login.Show();
                Application.DoEvents();

                a = Time_maxormin(comboBox1.Text.Replace("年", ""), comboBox2.Text.Replace("月", ""), false);
                b = Time_maxormin(comboBox1.Text.Replace("年", ""), comboBox2.Text.Replace("月", ""), true);

                DataTable dt = new theDST().Tables["销售统计表"];
                try
                {
                    SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM([USER]) AS [USER],SUM(SLIANG) AS SLIANG,SUM(SSALE) AS SSALE FROM QGOODS_SALES WHERE (MDIAN='" + xconfig.MDIAN + "') AND (SETTIME BETWEEN '" + a + "' AND '" + b + "') GROUP BY [USER]", conn);
                    da.Fill(dt);
                    conn.Close();
                    button1.Enabled = false;
                    this.TopMost = false;
                    Form itemform = new 店员销售统计详细(button1, this, a, b, dt);
                    foreach (DataRow i in dt.Rows)
                    {
                        if (i["SETTIME"].ToString() == string.Empty)
                        {
                            i["SETTIME"] = "[" + comboBox1.Text + comboBox2.Text + "]";
                        }
                    }
                    itemform.MdiParent = panFORM;
                    itemform.Show();

                    MSG.login.Close();
                }
                catch (Exception ex)
                {
                    MSG.login.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                MSG.login.Show();
                Application.DoEvents();

                a = DateTime.Parse(dateTimePicker1.Text + " 00:00:00");
                b = DateTime.Parse(dateTimePicker2.Text + " 23:59:59");

                DataTable dt = new theDST().Tables["销售统计表"];
                try
                {
                    SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT RTRIM([USER]) AS [USER],SUM(SLIANG) AS SLIANG,SUM(SSALE) AS SSALE FROM QGOODS_SALES WHERE (MDIAN='" + xconfig.MDIAN + "') AND (SETTIME BETWEEN '" + a + "' AND '" + b + "') GROUP BY [USER]", conn);
                    da.Fill(dt);
                    conn.Close();
                    button1.Enabled = false;
                    this.TopMost = false;
                    Form itemform = new 店员销售统计详细(button1, this, a, b, dt);
                    foreach (DataRow i in dt.Rows)
                    {
                        if (i["SETTIME"].ToString() == string.Empty)
                        {
                            i["SETTIME"] = "[" + dateTimePicker1.Text + " 至 " + dateTimePicker2.Text + "]";
                        }
                    }
                    itemform.MdiParent = panFORM;
                    itemform.Show();

                    MSG.login.Close();
                }
                catch (Exception ex)
                {
                    MSG.login.Close();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 店员销售统计_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMenu.Enabled = true;
        }
    }
}