using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Data.SqlClient;
using System.Threading;
using System.Data.OleDb;

namespace Mdian
{
    public partial class Login : Form
    {
        DataTable teDST;
        public Login(DataTable pandst)
        {
            teDST = pandst;
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            string MdianName = string.Empty;

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.Show();
            Application.DoEvents();

            label1.Text = "请稍候，正在读取系统资料.....";
            Application.DoEvents();
            Thread.Sleep(500);
            try
            {
                OleDbConnection oleconn = new OleDbConnection(xconfig.oldstr("data", "cnsdjian"));
                oleconn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT RTRIM(GSNAME) AS GSNAME FROM CONFIG", oleconn);
                OleDbDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    MdianName = rs["GSNAME"].ToString();
                }
                rs.Close();
                oleconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
                return;
            }

            label1.Text = "正在测试服务器连接，请稍候...";
            Application.DoEvents();

            string msgstr;
            msgstr = "TEST|0|CONNECT<EOF>";
            bool msgbol;
            msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("连接服务器超时！\n请与检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            string version = string.Empty;
            this.label1.Text = "正在检测程序版本...";
            Application.DoEvents();
            Thread.Sleep(500);

            try
            {
                mesk.xSocket mesk = new mesk.xSocket();
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                version = mesk.Send("TEST|0|GETVERSION|M<EOF>", ip, port);
            }
            catch { version = "ERROR"; }

            if (version != xconfig.VERSION)
            {
                MessageBox.Show("当前程序版本:" + xconfig.VERSION + "\n当前最新版本:" + version + "\n\n你的程序已过期！请联系管理员获取最新版。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            label1.Text = "正在与服务器同步时间，请稍候...";
            Application.DoEvents();

            DateTime newtime;
            mesk.xSocket meskss = new mesk.xSocket();
            IPAddress ips = IPAddress.Parse(xconfig.SERVERIP);
            int ports = xconfig.SERVERPORT;
            string msgstrs = "TEST|0|GETTIME<EOF>";
            string strgettime = "flase";
            strgettime = meskss.Send(msgstrs, ips, ports);
            if (strgettime != "ERROR")
            {
                newtime = DateTime.Parse(strgettime);
                bool setTimeOut;
                setTimeOut = config.conSetTime.SetSysTime(newtime);
            }

            label1.Text = "正在测试数据库连接，请稍候...";
            Application.DoEvents();
            try
            {
                string data = null;
                mesk.xSocket mesks = new mesk.xSocket();
                string mess = string.Empty;
                if (xconfig.DBBOL)
                {
                    if (!xconfig.DBonlineWT)
                    {
                        mess = "TEST|0|GETYDB2<EOF>";
                    }
                    else
                    {
                        mess = "TEST|0|GETYDB<EOF>";
                    }
                }
                else
                {
                    mess = "TEST|0|GETDB<EOF>";
                }
                data = mesks.Send(mess, IPAddress.Parse(xconfig.SERVERIP), xconfig.SERVERPORT);
                //MessageBox.Show(data);
                string[] tem = data.Split('|');
                if (tem[0] == "NULL" || tem[0] == "ERROR")
                {
                    MessageBox.Show("连接服务器出错", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
                xconfig.SQLIP = tem[1];
                xconfig.SQLUSER = tem[2];
                xconfig.SQLPASSWORD = tem[3];



                try
                {
                    SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                    conn.Open();
                    conn.Close();
                }
                catch
                {
                    MessageBox.Show("连接服务器出错", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("未知错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            //label2.Text = xconfig.GSNAME;

            this.Close();
        }
    }
}