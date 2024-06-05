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
    public partial class 门店添加 : Form
    {
        DataTable panDT;
        DataGridView panDGV;
        DataTable dstDQU = xconfig.DST.Tables["地区"];

        public 门店添加(DataTable dt,DataGridView dgv)
        {
            panDT = dt;
            panDGV = dgv;
            InitializeComponent();
        }

        private void 门店添加_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "select id,rtrim(name) as name,lv,sid from qDQU";
            config.conData.fill("sql", constr, cmdstr, dstDQU);

            try
            {
                dqusenbox.Items.Clear();
                DataRow[] itemdr1 = dstDQU.Select("lv=1", "name");
                for (int i = 0; i < itemdr1.Length; i++)
                {
                    dqusenbox.Items.Add(itemdr1[i]["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            DataRow[] itemdr1 = panDT.Select("name='" + namebox.Text.Trim() + "'", "");
            if (itemdr1.Length > 0)
            {
                MessageBox.Show("门店名称已经存在！");
                namebox.Text = "";
                namebox.Focus();
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
                SqlCommand cmd = new SqlCommand("insert into [qMDIAN]("
                    + "name,[user],tel,czhen,dzhi,email,dqusen,dqusi"
                    + ")values('"
                    + name + "','"
                    + userx + "','"
                    + tel + "','"
                    + czhen + "','"
                    + dzhi + "','"
                    + email + "','"
                    + dqusen + "','"
                    + dqusi + "')", conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter dars =new SqlDataAdapter("select [id],rtrim(name) as name,rtrim([user]) as [user],rtrim(tel) as tel,"
                + "rtrim(czhen) as czhen,rtrim(dzhi) as dzhi,rtrim(email) as email,rtrim(dqusen) as dqusen,"
                + "rtrim(dqusi) as dqusi from [qMDIAN]",conn);
                panDT.Clear();
                dars.Fill(panDT);
                conn.Close();

                namebox.Text = "";
                userbox.Text = "";
                telbox.Text = "";
                czhenbox.Text = "";
                dzhibox.Text = "";
                emailbox.Text = "";
                dqusenbox.Text = "";
                dqusibox.Text = "";

                if (!checkBox1.Checked)
                {
                    this.Close();
                }
            }catch(Exception ex){
                MessageBox.Show(ex.Message);
            }

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