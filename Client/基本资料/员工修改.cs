using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Data.SqlClient;

namespace Client.基本资料
{
    public partial class 员工修改 : Form
    {
        DataTable panDT;
        DataGridView panDGV;
        string panSID;
        public 员工修改(DataTable dt,DataGridView dgv,string sid)
        {
            panDT = dt;
            panDGV = dgv;
            panSID = sid;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ghbox_Enter(object sender, EventArgs e)
        {
            string str = sender.GetType().ToString();
            if (str == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.MistyRose;
            }
            else if (str == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.MistyRose;
            }
        }

        private void ghbox_Leave(object sender, EventArgs e)
        {
            string str = sender.GetType().ToString();
            if (str == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.White;
            }
            else if (str == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.White;
            }
        }

        private void telbox_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.MistyRose;
        }

        private void telbox_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.MistyRose;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ghbox.Text == "")
            {
                ghbox.Focus();
                return;
            }
            if (namebox.Text == "")
            {
                namebox.Focus();
                return;
            }
            if (xbiebox.Text == "")
            {
                xbiebox.Focus();
                return;
            }
            if (mdianbox.Text == "")
            {
                mdianbox.Focus();
                return;
            }

            string name,cmdstr,tel,jguan,xbie,mdian,sfzhen;
            //if (checkBox2.Checked)
            //{
            //    mesk.xSocket mesk = new mesk.xSocket();
            //    IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
            //    int port = xconfig.SERVERPORT;
            //    string msg = xconfig.USER + "|资料维护|GETCODEGH|GET<EOF>";
            //    string str = mesk.Send(msg, ip, port);
            //    if (str == "ERROR")
            //    {
            //        MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    else
            //    {
            //        sid = str;
            //    }
            //}
            //else
            //{
            //    sid = ghbox.Text;
            //}
            name = namebox.Text;
            tel=telbox.Text;
            jguan=jguanbox.Text;
            xbie=xbiebox.Text;
            mdian=mdianbox.Text;
            sfzhen=sfzhenbox.Text;
            if (textBox3.Text != "")
            {
                string pwd = xconfig.FunMD5(textBox3.Text);
                cmdstr = "update qYGONG set name='" + name + "',tel='" + tel + "',jguan='" + jguan
                + "',xbie='" + xbie + "',mdian='" + mdian + "',sfzhen='" + sfzhen + "',pwd='" + pwd + "' where id='" + panSID + "'";
            }
            else
            {
                cmdstr = "update qYGONG set name='" + name + "',tel='" + tel + "',jguan='" + jguan
                + "',xbie='" + xbie + "',mdian='" + mdian + "',sfzhen='" + sfzhen + "' where id='" + panSID + "'";
            }
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand(cmdstr, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                DataRow[] itemdr1 = panDT.Select("id='" + panSID + "'", "");
                if (itemdr1.Length > 0)
                {
                    itemdr1[0]["name"] = name;
                    itemdr1[0]["tel"] = tel;
                    itemdr1[0]["jguan"] = jguan;
                    itemdr1[0]["xbie"] = xbie;
                    itemdr1[0]["mdian"] = mdian;
                    itemdr1[0]["sfzhen"] = sfzhen;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            foreach (DataGridViewRow i in panDGV.Rows)
            {
                if (i.Cells["idDataGridViewTextBoxColumn3"].Value.ToString() == panSID)
                {
                    i.DefaultCellStyle.BackColor = Color.Lime;
                }
            }


            this.Close();
        }

        private void 员工修改_Load(object sender, EventArgs e)
        {
            DataTable dstMD = xconfig.DST.Tables["门店"];
            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "select [id],rtrim(name) as name,rtrim([user]) as [user],rtrim(tel) as tel,"
                + "rtrim(czhen) as czhen,rtrim(dzhi) as dzhi,rtrim(email) as email,rtrim(dqusen) as dqusen,"
                + "rtrim(dqusi) as dqusi from [qMDIAN]";
            config.conData.fill("sql", constr, cmdstr, dstMD);

            mdianbox.Items.Clear();
            foreach (DataRow i in dstMD.Rows)
            {
                mdianbox.Items.Add(i["name"].ToString());
            }

            DataRow[] itemdr1 = panDT.Select("id='" + panSID + "'", "");
            if (itemdr1.Length < 1)
            {
                this.Close();
            }

            namebox.Text = itemdr1[0]["name"].ToString();
            ghbox.Text = itemdr1[0]["sid"].ToString();
            telbox.Text = itemdr1[0]["tel"].ToString();
            jguanbox.Text = itemdr1[0]["jguan"].ToString();
            xbiebox.Text = itemdr1[0]["xbie"].ToString();
            sfzhenbox.Text = itemdr1[0]["sfzhen"].ToString();
            mdianbox.Text = itemdr1[0]["mdian"].ToString();
        }
    }
}