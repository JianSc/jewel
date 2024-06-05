using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Client.基本资料
{
    public partial class 门店修改 : Form
    {
        DataTable panDT;
        DataGridView panDGV;
        string panSID;
        DataTable dstDQU = xconfig.DST.Tables["地区"];

        public 门店修改(DataTable dt,DataGridView dgv,string sid)
        {
            panDT = dt;
            panDGV = dgv;
            panSID = sid;
            InitializeComponent();
        }

        private void 门店修改_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "select id,rtrim(name) as name,lv,sid from qDQU";
            config.conData.fill("sql", constr, cmdstr, dstDQU);

            DataRow[] itemdr1 = panDT.Select("id='" + panSID + "'", "");
            if (itemdr1.Length < 1)
            {
                MessageBox.Show("未知错误！\n请联系管理员。");
                this.Close();
            }

            DataRow[] itemdr2 = dstDQU.Select("lv=1", "name");
            dqusenbox.Items.Clear();
            for (int i = 0; i < itemdr2.Length; i++)
            {
                dqusenbox.Items.Add(itemdr2[i]["name"].ToString());
            }

            namebox.Text = itemdr1[0]["name"].ToString();
            userbox.Text = itemdr1[0]["user"].ToString();
            telbox.Text = itemdr1[0]["tel"].ToString();
            czhenbox.Text = itemdr1[0]["czhen"].ToString();
            dzhibox.Text = itemdr1[0]["dzhi"].ToString();
            emailbox.Text = itemdr1[0]["email"].ToString();
            dqusenbox.Text = itemdr1[0]["dqusen"].ToString();
            dqusibox.Text = itemdr1[0]["dqusi"].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.MistyRose;
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            string str = sender.GetType().ToString();
            if (str == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.MistyRose;
            }
            else if (str == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.MistyRose;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            string str = sender.GetType().ToString();
            if (str == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.White;
            }
            else if (str == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.White;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key;
            key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key;
            key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (namebox.Text == "")
            {
                namebox.Focus();
                return;
            }
            if (telbox.Text == "")
            {
                telbox.Focus();
                return;
            }
            if (czhenbox.Text == "")
            {
                czhenbox.Focus();
                return;
            }

            string name,userx,tel,czhen,dzhi,email,dqusen,dqusi;
            name = namebox.Text;
            userx = userbox.Text;
            tel = telbox.Text;
            czhen = czhenbox.Text;
            dzhi=dzhibox.Text;
            email=emailbox.Text;
            dqusen=dqusenbox.Text;
            dqusi=dqusibox.Text;
            string msgstr = xconfig.USER + "|资料维护|CONNECT<EOF>";
            bool msgbol;
            msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("update qMDIAN set "
                    + "name='" + name + "',"
                    + "[user]='" + userx + "',"
                    + "tel='" + tel + "',"
                    + "czhen='" + czhen + "',"
                    + "dzhi='" + dzhi + "',"
                    + "email='" + email + "',"
                    + "dqusen='" + dqusen + "',"
                    + "dqusi='" + dqusi + "' where id='" + panSID + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                DataRow[] dr = panDT.Select("id='" + panSID + "'", "");
                if (dr.Length > 0)
                {
                    dr[0]["name"] = name;
                    dr[0]["user"] = userx;
                    dr[0]["tel"] = tel;
                    dr[0]["czhen"] = czhen;
                    dr[0]["dzhi"] = dzhi;
                    dr[0]["email"] = email;
                    dr[0]["dqusen"] = dqusen;
                    dr[0]["dqusi"] = dqusi;
                }

                panDGV.DataSource = panDT;
                foreach (DataGridViewRow i in panDGV.Rows)
                {
                    if (i.Cells["idDataGridViewTextBoxColumn2"].Value.ToString() == panSID)
                    {
                        i.DefaultCellStyle.BackColor = Color.Lime;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Close();

        }

        private void dqusenbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow[] itemdr1 = dstDQU.Select("name='" + dqusenbox.Text + "'", "");
            if (itemdr1.Length < 1)
            {
                return;
            }
            string sid = itemdr1[0]["id"].ToString();
            DataRow[] itemdr2 = dstDQU.Select("lv=2 and sid='" + sid + "'", "name");
            dqusibox.Items.Clear();
            for (int i = 0; i < itemdr2.Length; i++)
            {
                dqusibox.Items.Add(itemdr2[i]["name"].ToString());
            }
        }

    }
}