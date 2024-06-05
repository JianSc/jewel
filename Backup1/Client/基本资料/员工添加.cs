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
    public partial class 员工添加 : Form
    {
        DataTable panDT;
        DataGridView panDGV;
        public 员工添加(DataTable dt,DataGridView dgv)
        {
            panDT = dt;
            panDGV = dgv;
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

            DataRow[] dr = panDT.Select("name='" + namebox.Text.Trim() + "'", "");
            if (dr.Length > 0)
            {
                MessageBox.Show("这个姓名已存在！\n推荐使用:" + namebox.Text.Trim() + "2");
                namebox.Focus();
                return;
            }

            string sid,name,pwd,tel,jguan,xbie,mdian,sfzhen;
            if (checkBox2.Checked)
            {
                mesk.xSocket mesk = new mesk.xSocket();
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                string msg = xconfig.USER + "|资料维护|GETCODEGH|GET<EOF>";
                string str = mesk.Send(msg, ip, port);
                if (str == "ERROR")
                {
                    MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    sid = str;
                }
            }
            else
            {
                sid = ghbox.Text;
            }
            name = namebox.Text;
            tel=telbox.Text;
            jguan=jguanbox.Text;
            xbie=xbiebox.Text;
            mdian=mdianbox.Text;
            sfzhen=sfzhenbox.Text;
            pwd=xconfig.FunMD5("1");

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();

                SqlCommand cmd = new SqlCommand("QZ_YGONG_ADD", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SID", sid));
                cmd.Parameters.Add(new SqlParameter("@NAME", name));
                cmd.Parameters.Add(new SqlParameter("@PWD", pwd));
                cmd.Parameters.Add(new SqlParameter("@TEL", tel));
                cmd.Parameters.Add(new SqlParameter("@JGUAN", jguan));
                cmd.Parameters.Add(new SqlParameter("@XBIE", xbie));
                cmd.Parameters.Add(new SqlParameter("@MDIAN", mdian));
                cmd.Parameters.Add(new SqlParameter("@SFZHEN", sfzhen));
                cmd.ExecuteNonQuery();

                SqlDataAdapter dars = new SqlDataAdapter("select id,rtrim(sid) as sid,rtrim(name) as name,rtrim(tel) as tel,"
                + "rtrim(jguan) as jguan,rtrim(xbie) as xbie,rtrim(mdian) as mdian,rtrim(sfzhen) as sfzhen,convert(char(10),[time],120) as [time] from [qYGONG]", conn);
                panDT.Clear();
                dars.Fill(panDT);
                conn.Close();
                panDGV.DataSource = panDT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (!checkBox1.Checked)
            {
                this.Close();
            }

            ghbox.Text = "";
            namebox.Text = "";
            telbox.Text = "";
            jguanbox.Text = "";
            xbiebox.Text = "";
            mdianbox.Text = "";
            sfzhenbox.Text = "";
            checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                mesk.xSocket mesk = new mesk.xSocket();
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                string msg = xconfig.USER + "|资料维护|GETCODEGH<EOF>";
                string itemID = mesk.Send(msg, ip, port);
                if (itemID != "ERROR")
                {
                    ghbox.Text = itemID;
                }
            }
        }

        private void 员工添加_Load(object sender, EventArgs e)
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
        }
    }
}