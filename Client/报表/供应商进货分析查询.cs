using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client.报表
{
    public partial class 供应商进货分析查询 : Form
    {
        DataTable panDT;
        DataTable gysDT = new DataTable();

        public 供应商进货分析查询(DataTable d)
        {
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(NAME) AS NAME FROM QGYSHANG";
            config.conData.fill("sql", constr, cmdstr,gysDT);
            panDT = d;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 供应商进货分析查询_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            foreach (DataRow i in gysDT.Rows)
            {
                comboBox1.Items.Add(i["NAME"].ToString());
            }

            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool bol = ((CheckBox)sender).Checked;
            dateTimePicker1.Enabled = bol;
            dateTimePicker2.Enabled = bol;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool bol = ((CheckBox)sender).Checked;
            comboBox1.Enabled = bol;
        }

        private delegate void d();

        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked)
            {
                return;
            }

            if (checkBox2.Checked)
            {
                if (comboBox1.Text == string.Empty)
                {
                    comboBox1.DroppedDown = true;
                    return;
                }
            }
            string a = string.Empty;
            string b = string.Empty;

            if (dateTimePicker1.InvokeRequired) { d d = delegate { a = dateTimePicker1.Text; }; dateTimePicker1.Invoke(d); }
            else { a = dateTimePicker1.Text; }
            if (dateTimePicker2.InvokeRequired) { d d = delegate { b = dateTimePicker2.Text; }; dateTimePicker2.Invoke(d); }
            else { b = dateTimePicker2.Text; }
            a += " 00:00:00";
            b += " 23:59:59";

            string cmdstr=string.Empty;
            string cmdstr2= string.Empty;
            string constr = xconfig.CONNSTR;
            cmdstr2 = "SELECT DH,QGOODS_GYS_LIST.SLIANG,NAME,CONVERT(CHAR(10),QGOODS_GYS_LIST.SETTIME,120) AS SETTIME,QGOODS.JIANZ,QGOODS.JINZ,QGOODS.CBEI FROM QGOODS_GYS_LIST INNER JOIN QGOODS ON QGOODS.TM=QGOODS_GYS_LIST.TM WHERE (1=1)";
            if (checkBox1.Checked)
            {
                cmdstr2 += " AND (QGOODS_GYS_LIST.SETTIME BETWEEN '" + a + "' AND '" + b + "')";
            }
            if (checkBox2.Checked)
            {
                string c = comboBox1.Text;
                if (c == string.Empty) { return; }
                cmdstr2 += " AND (NAME='" + c + "')";
            }

            cmdstr = "SELECT RTRIM(DH) AS DH,SUM(SLIANG) AS SLIANG,RTRIM(NAME) AS NAME,CAST(SUM(JIANZ) AS DECIMAL(19,3)) AS JIANZ,SETTIME,CAST(SUM(JINZ) AS DECIMAL(19,3)) AS JINZ,SUM(CBEI) AS CBEI FROM (" + cmdstr2 + ") DERIVEDTBL";
            cmdstr += " GROUP BY NAME,DH,SETTIME";
            config.conData.fill("sql", constr, cmdstr, panDT);

            this.Close();
        }
    }
}