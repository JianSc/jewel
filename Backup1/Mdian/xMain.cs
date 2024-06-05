using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Mdian
{
    public partial class xMain : Form
    {
        Form panform;
        DataTable panDT;
        bool formclose;
        public xMain(Form xform,DataTable ygongDT)
        {
            InitializeComponent();
            panform = xform;
            panDT = ygongDT;
        }

        string configuser;

        private void xMain_Load(object sender, EventArgs e)
        {
            //this.MaximumSize = this.Size;
            //this.MinimumSize = this.Size;

            configuser = xconfig.USER;

            toolStripStatusLabel1.Text = "";

            timer1_Tick(timer1, new EventArgs());
            timer1.Enabled = true;

            string[] itembol = new string[7];
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(USERSTAT) AS USERSTAT,USERZK FROM QYGONG_STAT WHERE (USERID='" + xconfig.SID + "')", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    itembol = rs["USERSTAT"].ToString().Split(new char[] { ',' });
                    xconfig.MYZK = double.Parse(rs["USERZK"].ToString());
                }
                rs.Close();
                conn.Close();

                if (itembol.Length < 7)
                {
                    MessageBox.Show("权限数据读取不完全！\n请联系系统管理员。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
                return;
            }

            for (int i = 0; i < 7; i++)
            {
                xconfig.CHKBOL[i] = bool.Parse(itembol[i].ToString());
            }

            if (!xconfig.CHKBOL[0])
            {
                收货EToolStripMenuItem.Enabled = false;
                销售ToolStripMenuItem.Enabled = false;
                销退ToolStripMenuItem.Enabled = false;
                库存盘点ToolStripMenuItem.Enabled = false;
                报表ToolStripMenuItem.Enabled = false;
                return;
            }

            收货EToolStripMenuItem.Enabled = xconfig.CHKBOL[1];
            销售ToolStripMenuItem.Enabled = xconfig.CHKBOL[2];
            销退ToolStripMenuItem.Enabled = xconfig.CHKBOL[3];
            库存盘点ToolStripMenuItem.Enabled = xconfig.CHKBOL[4];
            报表ToolStripMenuItem.Enabled = xconfig.CHKBOL[5];

        }

        private void xMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            string msgstr = "TEST|0|USERCLOSE|" + configuser + "<EOF>";
            xconfig.netSend(msgstr);
            if (!formclose)
            {
                Application.Exit();
            }
        }

        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xconfig.USER = null;
            xconfig.PWD = null;
            xconfig.MDIAN = null;
            panform.Visible = true;
            formclose = true;
            this.Close();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form cliform = new 修改密码(panDT);
            cliform.MdiParent = this;
            cliform.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 收货EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|收货接单|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form itemform = new 收货(收货EToolStripMenuItem,this);
            收货EToolStripMenuItem.Enabled = false;
            //itemform.MdiParent = this;
            itemform.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|闲置|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                toolStripStatusLabel1.Text = "连接失败！";
            }
            else
            {
                toolStripStatusLabel1.Text = "连接成功！";
            }
        }

        private void 销退ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|销退开单|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接出错！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            销退ToolStripMenuItem.Enabled = false;

            Form itemform = new 销退.销退(销退ToolStripMenuItem);
            itemform.MdiParent = this;
            itemform.Show();
        }

        private void 库存盘点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|门店盘点|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接出错！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            库存盘点ToolStripMenuItem.Enabled = false;

            Form itemform = new 盘点.盘点(库存盘点ToolStripMenuItem);
            itemform.MdiParent = this;
            itemform.Show();
        }

        private void 销售统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|门店报表|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接出错！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            销售统计ToolStripMenuItem.Enabled = false;
            Form itemform = new 报表.销售统计选择(销售统计ToolStripMenuItem,this);
            itemform.Show();
        }

        private void 销售年度统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|门店报表|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接出错！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            销售年度统计ToolStripMenuItem.Enabled = false;
            Form itemform = new 报表.销售年度统计(销售年度统计ToolStripMenuItem);
            itemform.Show();
        }

        private void 店员销售统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|门店报表|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接出错！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            店员销售统计ToolStripMenuItem.Enabled = false;
            Form itemform = new 报表.店员销售统计(店员销售统计ToolStripMenuItem, this);
            itemform.Show();
        }

        private void 销售开单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|销售开单|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            销售开单ToolStripMenuItem.Enabled = false;
            Form xsform = new 销售.销售(销售开单ToolStripMenuItem);
            xsform.MdiParent = this;
            xsform.Show();
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|销售打印|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            打印ToolStripMenuItem.Enabled = false;
            Form itemform = new 销售.销售打印总览(打印ToolStripMenuItem,this);
            itemform.Show();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\version.exe"))
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\version.exe");
            }
        }

        private void 帮助ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + @"\help.chm");
        }

        private void 仓库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|查看库存|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            仓库ToolStripMenuItem.Enabled = false;
            Form newform = new 仓库.仓库(仓库ToolStripMenuItem);
            newform.MdiParent = this;
            newform.Show();
        }

        private void 退货开单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|门店退货|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接出错！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            退货开单ToolStripMenuItem.Enabled = false;

            Form itemform = new 退货.退货(退货开单ToolStripMenuItem);
            itemform.MdiParent = this;
            itemform.Show();
        }

        private void 退货状态查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|门店退货|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接出错！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            退货状态查询ToolStripMenuItem.Enabled = false;

            Form itemform = new 退货.退货状态(退货状态查询ToolStripMenuItem,this);
            itemform.MdiParent = this;
            itemform.Show();
        }
    }
}