using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Reflection;

namespace Client
{
    public partial class cliMain : Form
    {
        public cliMain()
        {
            InitializeComponent();
        }

        private void cliMain_Load(object sender, EventArgs e)
        {
            this.label1.Parent = this.pictureBox1;
            this.label2.Parent = this.pictureBox1;
            this.label3.Parent = this.pictureBox1;
            this.labversion.Parent = this.pictureBox1;
            labversion.Text = xconfig.VERSION;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            this.label1.Text = null;
            this.Show();
            Application.DoEvents();
            this.label1.Text = "请稍候，正在加载程序...";
            Application.DoEvents();
            Thread.Sleep(500);
            if (!Directory.Exists(Application.StartupPath + @"\Item"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Item");
            }
            this.label1.Text = "与服务器建立连接...";
            Application.DoEvents();
            //Thread.Sleep(500);

            try
            {
                string data = null;
                mesk.xSocket mesk = new mesk.xSocket();
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                string mess = "TEST|0|CONNECT<EOF>";

                data = mesk.Send(mess, ip, port);
                
                if (data != null && data != "ERROR")
                {
                    this.label1.Text = "成功建立与服务器连接，正在读取初始化数据...";
                    Application.DoEvents();
                }
                else
                {
                    MessageBox.Show("程序初始化失败\n\n未能与服务器成功建立连接，请检查线路！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("内部错误：C100001\n\n请联系管理员。","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
                version = mesk.Send("TEST|0|GETVERSION|C<EOF>", ip, port);
            }
            catch { version = "ERROR"; }

            if (version != xconfig.VERSION)
            {
                MessageBox.Show("当前程序版本:" + xconfig.VERSION + "\n当前最新版本:" + version + "\n\n你的程序已过期！请联系管理员获取最新版。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            //Thread.Sleep(500);

            label1.Text = "检查本地时间...";
            Application.DoEvents();
            //Thread.Sleep(500);
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
            //if(!setTimeOut || strgettime == "flase")
            //{
            //    MessageBox.Show("检查本地时间出错！\n\n请联系你的管理员。","出错了",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //    this.Close();
            //    return;
            //}


            label1.Text = "缓存数据到本地缓冲区...";
            Application.DoEvents();
            //Thread.Sleep(500);

            try
            {
                string data = null;
                mesk.xSocket mesks = new mesk.xSocket();
                string mess = "TEST|0|GETDB<EOF>";
                data = mesks.Send(mess, IPAddress.Parse(xconfig.SERVERIP),xconfig.SERVERPORT );
                //MessageBox.Show(data);
                string[] tem = data.Split('|');
                if(tem[0] == "NULL" ||tem[0] == "ERROR")
                {
                    MessageBox.Show("连接服务器出错","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
                xconfig.SQLIP = tem[1];
                xconfig.SQLUSER = tem[2];
                xconfig.SQLPASSWORD = tem[3];

                label1.Text = "测试数据库服务器连接...";
                Application.DoEvents();
                Thread.Sleep(100);
                try
                {
                    SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                    conn.Open();
                    conn.Close();
                }
                catch
                {
                    MessageBox.Show("数据库服务器不能正常连接！\n请稍候再试，或与管理员联系。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            Thread.Sleep(500);
            this.label1.Text = "Open 程序...";
            Application.DoEvents();
            Thread.Sleep(500);
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}