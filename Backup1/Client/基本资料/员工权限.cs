using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Client.基本资料
{
    public partial class 员工权限 : Form
    {
        DataTable thisDST = new DataTable();
        CheckBox[] thisCHK = new CheckBox[7];
        ToolStripMenuItem panMENU;
        bool menubol;

        public 员工权限(ToolStripMenuItem menu)
        {
            panMENU = menu;
            menubol = true;
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(USERID) AS USERID,RTRIM(USERSTAT) AS USERSTAT,RTRIM(QYGONG.NAME) AS USERNAME,USERZK FROM QYGONG_STAT "
            + "INNER JOIN QYGONG ON QYGONG_STAT.USERID=QYGONG.SID";
            config.conData.fill("sql", constr, cmdstr, thisDST);

            InitializeComponent();
        }

        public 员工权限(string SID)
        {
            //panMENU = menu;
            menubol = false;
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "SELECT RTRIM(USERID) AS USERID,RTRIM(USERSTAT) AS USERSTAT,RTRIM(QYGONG.NAME) AS USERNAME,USERZK FROM QYGONG_STAT "
            + "INNER JOIN QYGONG ON QYGONG_STAT.USERID=QYGONG.SID WHERE (QYGONG_STAT.USERID='" + SID + "')";
            config.conData.fill("sql", constr, cmdstr, thisDST);

            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1) { return; }
            //if (textBox1.Text == "") { textBox1.Focus(); return; }

            string boxstr = textBox1.Text.Trim();

            Regex regex = new Regex(@"^[0]\.\d{1,2}$|^[1]$|^[1]\.[00]$");
            if (!regex.Match(boxstr).Success)
            {
                textBox1.Text = "1.0";
                textBox1.Focus();
                textBox1.BackColor = Color.MistyRose;
                return;
            }

            string userSTAT=string.Empty;
            int j=0;
            foreach (CheckBox i in thisCHK)
            {
                userSTAT += i.Checked.ToString();
                if (j < 6)
                {
                    userSTAT += ",";
                }
                j++;
            }
            string userID;
            userID = dataGridView1["userIDDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
            if (userID == "") { return; }
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE QYGONG_STAT SET USERSTAT='" + userSTAT + "',USERZK='" + textBox1.Text.Trim() + "' WHERE (USERID='" + userID + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                DataRow[] dr = thisDST.Select("USERID='" + userID + "'", "");
                if (dr.Length > 0)
                {
                    dr[0]["USERSTAT"] = userSTAT;
                    dr[0]["USERZK"] = textBox1.Text.Trim();
                }

                //toolStripButton2.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 8)
            {
                e.Handled = false;
            }
            else
            {
                Regex regex = new Regex(@"\d|\.");
                string keystr = e.KeyChar.ToString();
                if (!regex.Match(keystr).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string boxstr = ((TextBox)sender).Text.Trim();

            Regex regex = new Regex(@"^[0]\.\d{1,2}$|^[1]$|^[1]\.[00]$");
            if (!regex.Match(boxstr).Success)
            {
                ((TextBox)sender).Text = "1.0";
            }

            ((TextBox)sender).BackColor = Color.White;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.LimeGreen;
        }

        private void 员工权限_Load(object sender, EventArgs e)
        {
            thisCHK[0] = checkBox1;
            thisCHK[1] = checkBox2;
            thisCHK[2] = checkBox3;
            thisCHK[3] = checkBox4;
            thisCHK[4] = checkBox5;
            thisCHK[5] = checkBox6;
            thisCHK[6] = checkBox7;

            dataGridView1.DataSource = thisDST;
            config.conDGV.DGVAutoID(dataGridView1, "phao");


            if (dataGridView1.Rows.Count > 17)
            {
                pictureBox1.Visible = false;
            }
            else if (dataGridView1.Rows.Count < 18)
            {
                pictureBox1.Visible = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 员工权限_FormClosed(object sender, FormClosedEventArgs e)
        {
            string msgstr = xconfig.USER + "|闲置|CONNECT<EOF>";
            xconfig.netSend(msgstr);

            if (menubol)
            {
                panMENU.Enabled = true;
            }
        }

        private void 员工权限_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1) { return; }
            try
            {
                toolStripButton2.Enabled = true;

                string arrstat = dataGridView1["userStatDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
                string[] arrchk = arrstat.Split(new char[] { ',' });
                for (int i = 0; i < 7; i++)
                {
                    thisCHK[i].Checked = bool.Parse(arrchk[i]);
                }
                string userzk = dataGridView1["userZKDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString();
                textBox1.Text = userzk;
            }
            catch { }
        }
    }
}