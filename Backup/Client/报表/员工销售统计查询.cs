using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client.报表
{
    public partial class 员工销售统计查询 : Form
    {
        ToolStripMenuItem panMENU;
        Form panFR;
        DataTable thisDT = new clidata().Tables["员工销售统计"];

        public 员工销售统计查询(ToolStripMenuItem m,Form f)
        {
            panMENU = m;
            panFR = f;

            InitializeComponent();
        }

        private void 员工销售统计查询_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            DataTable itemdt = new DataTable();
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM([NAME]) AS [NAME] FROM QMDIAN";
            config.conData.fill("sql", constr, cmdstr, itemdt);

            comboBox3.Items.Add("");
            comboBox4.Items.Add("");

            foreach (DataRow i in itemdt.Rows)
            {
                comboBox3.Items.Add(i["NAME"].ToString());
                comboBox4.Items.Add(i["NAME"].ToString());
            }

            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Now;

            int yearint = DateTime.Now.Year;
            for (int i = 0; i < 11; i++)
            {
                int j = yearint - i;
                comboBox1.Items.Add(j.ToString());
            }
            for (int i = 1; i < 13; i++)
            {
                comboBox2.Items.Add(i.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 员工销售统计查询_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime a, b;
            a = DateTime.Now;
            b = DateTime.Now;
            string comstr2 = string.Empty;
            string comstr = string.Empty;

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
                if (comboBox3.Text != string.Empty)
                {
                    comstr2 = " AND (MDIAN='" + comboBox3.Text + "')";
                }
                a = xconfig.DateTime_Max_Min(comboBox1.Text, comboBox2.Text, false);
                b = xconfig.DateTime_Max_Min(comboBox1.Text, comboBox2.Text, true);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (comboBox4.Text != string.Empty)
                {
                    comstr2 = " AND (MDIAN='" + comboBox4.Text + "')";
                }
                a = DateTime.Parse(dateTimePicker1.Text + " 00:00:00");
                b = DateTime.Parse(dateTimePicker2.Text + " 23:59:59");
            }

            comstr = "SELECT RTRIM([USER]) AS [USER],SUM(SLIANG) AS SLIANG,SUM(SSALE) AS SSALE FROM QGOODS_SALES WHERE (SETTIME BETWEEN '" + a + "' AND '" + b + "')" + comstr2 + " GROUP BY [USER]";
            string constr = xconfig.CONNSTR;
            config.conData.fill("sql", constr, comstr, thisDT);

            this.TopMost = false;
            Form itemform = new 报表.员工销售列表(thisDT, this, a, b);
            itemform.MdiParent = panFR;
            itemform.Show();


        }
    }
}