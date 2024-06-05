using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Reflection;
using System.IO;


namespace Client
{
    public partial class xmain : Form
    {
        public xmain()
        {
            InitializeComponent();
        }

        private static Form fchuang;
        public static Form fr_fchuang { get { return null; } set { fchuang = value; } }

        private static bool exit = false;
        private void xmain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                string msgtxt = "TEST|0|USERCLOSE|" + configuser + "<EOF>";
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                mesk.xSocket mesk = new mesk.xSocket();
                string msgbol = mesk.Send(msgtxt, ip, port);
            }
            catch { }
            if (!exit)
            {
                Application.Exit();
            }
        }

        private void 退出ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //xconfig.USER = null;
            xconfig.PWD = null;
            fchuang.Visible = true;
            exit = true;
            this.Close();
        }

        string configuser;

        private void xmain_Load(object sender, EventArgs e)
        {
            exit = false;

            //this.MinimumSize = this.Size;
            //this.MaximumSize = this.Size;

            this.ztaitxt.Text = "连接成功";
            configuser = xconfig.USER;
            MSG.login.Close();
            //tolversion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string messtext;
            messtext = xconfig.USER + "|闲置|CONNECT<EOF>";
            mesk.xSocket mesk = new mesk.xSocket();

            IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
            int port = xconfig.SERVERPORT;

            string connbool = "false";
            connbool = mesk.Send(messtext, ip, port);

            if (connbool == "TRUE")
            {
                this.ztaitxt.Text = "连接成功";
            }
            else
            {
                this.ztaitxt.Text = "连接失败";
            }
        }

        private void 商品入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|入库|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msg);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MSG.login.Show();
            Application.DoEvents();

            商品入库ToolStripMenuItem.Enabled = false;
            Form cliform = new 商品入库.商品入库(商品入库ToolStripMenuItem);
            cliform.MdiParent = this;
            cliform.Show();
            this.Focus();
        }

        private void 入库撤单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|撤单|CONNECT<EOF>";
            xconfig.netSend(msg);

            入库撤单ToolStripMenuItem.Enabled = false;
            Form itemform = new 商品入库.入库单撤单(入库撤单ToolStripMenuItem,this);
            //itemform.MdiParent = this;
            itemform.Show();
        }

        private void 补打入库单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|打印|CONNECT<EOF>";
            xconfig.netSend(msg);

            MSG.login.Show();
            Application.DoEvents();

            补打入库单ToolStripMenuItem.Enabled = false;
            Form itemform = new 商品入库.查询(补打入库单ToolStripMenuItem,this);
            //itemform.MdiParent = this;
            itemform.Show();
            
            MSG.login.Close();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|修改密码|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            修改密码ToolStripMenuItem.Enabled = false;
            Form pwdform = new 修改密码(修改密码ToolStripMenuItem);
            //pwdform.MdiParent = this;
            pwdform.Show();
        }

        private void 门店发货ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|门店发货|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            门店发货ToolStripMenuItem.Enabled = false;
            Form itemform = new 门店发货.门店发货(门店发货ToolStripMenuItem);
            itemform.MdiParent = this;
            itemform.Show();
        }

        private void 发货单撤ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|发货撤单|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            发货单撤ToolStripMenuItem.Enabled = false;
            Form itemform = new 门店发货.发货撤单(发货单撤ToolStripMenuItem,this);
            //itemform.MdiParent = this;
            itemform.Show();
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|发货打印|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            打印ToolStripMenuItem.Enabled = false;
            Form itemform = new 门店发货.发货打印(打印ToolStripMenuItem,this);
            //itemform.MdiParent = this;
            itemform.Show();

            MSG.login.Close();
        }

        private void 发货状态查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|发货查询|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            发货状态查询ToolStripMenuItem.Enabled = false;
            Form itemform = new 分析.发货状态查询(发货状态查询ToolStripMenuItem,this);
            //itemform.MdiParent = this;
            itemform.Show();

            MSG.login.Close();

        }

        private void 综合查询打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|综合查询|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            综合查询打印ToolStripMenuItem.Enabled = false;
            Form itemform = new 综合查询打印(综合查询打印ToolStripMenuItem,this);
            //itemform.MdiParent = this;
            itemform.Show();

            MSG.login.Close();
        }

        private void 盘点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|库存盘点|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MSG.login.Show();
            Application.DoEvents();

            盘点ToolStripMenuItem.Enabled = false;
            Form itemform = new 盘点.盘点(盘点ToolStripMenuItem);
            itemform.MdiParent = this;
            itemform.Show();
            MSG.login.Close();
        }

        private void 基本资料管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mesk.xSocket mesk = new mesk.xSocket();
            IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
            int port = xconfig.SERVERPORT;
            string msgstr = xconfig.USER + "|资料维护|CONNECT<EOF>";
            string getnet = mesk.Send(msgstr, ip, port);
            if (getnet == "ERROR")
            {
                MessageBox.Show("服务端连接失败！\n请检查线路或与管理员联系。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MSG.login.Show();
            Application.DoEvents();
            基本资料管理ToolStripMenuItem.Enabled = false;
            Form cliform = new 基本资料.基本资料(基本资料管理ToolStripMenuItem);
            cliform.MdiParent = this;
            cliform.Show();
            MSG.login.Close();
        }

        private void 员工权限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|权限设置|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            员工权限ToolStripMenuItem.Enabled = false;
            Form itemform = new 基本资料.员工权限(员工权限ToolStripMenuItem);
            itemform.Show();

            MSG.login.Close();

        }

        private void 综合查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|导出条码|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            综合查询ToolStripMenuItem.Enabled = false;
            Form itemform = new 综合查询打印(综合查询ToolStripMenuItem, this, true);
            itemform.Show();
            MSG.login.Close();
        }

        private void 手工录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|导出条码|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            手工录入ToolStripMenuItem.Enabled = false;
            Form itemform = new 打条码.导出条码(手工录入ToolStripMenuItem);
            itemform.Show();
        }

        private void 产品状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|分析|CONNECT<EOF>";
            xconfig.netSend(msg);

            产品状态ToolStripMenuItem.Enabled = false;
            Form itemform = new 分析.产品状态分析(产品状态ToolStripMenuItem);
            //itemform.MdiParent = this;
            //MessageBox.Show(itemform.Size.ToString());
            itemform.Show();
        }

        private void 调成本价ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|调成本价|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            调成本价ToolStripMenuItem.Enabled = false;
            Form itemform = new 调价.调成本价(调成本价ToolStripMenuItem);
            itemform.MdiParent = this;
            itemform.Show();
        }

        private void 成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|查询成本|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            成ToolStripMenuItem.Enabled = false;
            Form itemform = new 分析.成本调价记录(成ToolStripMenuItem);
            itemform.MdiParent = this;
            itemform.Show();
        }

        private void 调售价ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|调销售价|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            调售价ToolStripMenuItem.Enabled = false;
            Form itemform = new 调价.调销售价(调售价ToolStripMenuItem);
            itemform.MdiParent = this;
            itemform.Show();

        }

        private void 售价调价记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|查询售价|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            售价调价记录ToolStripMenuItem.Enabled = false;
            Form itemform = new 分析.售价调价记妹(售价调价记录ToolStripMenuItem);
            itemform.Show();
        }

        private void 退货ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|接退货单|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            退货ToolStripMenuItem.Enabled = false;
            Form itemform = new 退货.门店退货(退货ToolStripMenuItem,this);
            itemform.Show();
        }

        private void 库退ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|库退|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);

            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            库退ToolStripMenuItem.Enabled = false;
            Form itemform = new 库退.库退(库退ToolStripMenuItem);
            itemform.MdiParent = this;
            itemform.Show();
        }

        private void 年度销售统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|库退|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);

            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            年度销售统计ToolStripMenuItem.Enabled = false;
            Form Itemform = new 报表.销售年度统计(年度销售统计ToolStripMenuItem);
            Itemform.Show();
        }

        private void 客户积分ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|报表|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);

            if(!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            客户积分ToolStripMenuItem.Enabled = false;
            Form itemform = new 报表.客户积分(客户积分ToolStripMenuItem);
            itemform.Show();

        }

        private void 供应商进货报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|报表|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);

            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            供应商进货报表ToolStripMenuItem.Enabled = false;
            Form itemform = new 报表.供应商进货分析(供应商进货报表ToolStripMenuItem);
            itemform.MdiParent = this;
            itemform.Show();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\version.exe"))
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\version.exe");
            }
        }

        private void 销售统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|报表|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);

            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            销售统计ToolStripMenuItem.Enabled = false;
            Form itemform = new 报表.销售统计查询(销售统计ToolStripMenuItem,this);
            itemform.Show();
        }

        private void 员工销售统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|报表|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);

            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            员工销售统计ToolStripMenuItem.Enabled = false;
            Form itemfom = new 报表.员工销售统计查询(员工销售统计ToolStripMenuItem, this);
            itemfom.Show();
        }

        private void 款式销售分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|报表|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);

            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            款式销售分析ToolStripMenuItem.Enabled = false;
            Form itemform = new 报表.款式销售分析查询(款式销售分析ToolStripMenuItem, this);
            itemform.Show();
        }

        private void 货品拍照ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|拍照|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);

            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            货品拍照ToolStripMenuItem.Enabled = false;
            Form itemform = new imgUP.imgform(xconfig.CONNSTR, 货品拍照ToolStripMenuItem, IPAddress.Parse(xconfig.SERVERIP), xconfig.SERVERPORT, xconfig.USER);
            itemform.ShowDialog();
        }

        private void 门店发退销统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|报表|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);

            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            门店发退销统计ToolStripMenuItem.Enabled = false;
            Form itf = new 报表.门店销售状况统计(门店发退销统计ToolStripMenuItem, this);
            itf.MdiParent = this;
            itf.Show();
        }

        private void 销售ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|分析|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);

            if (!msgbol)
            {
                MessageBox.Show("服务器连接失败！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            销售ToolStripMenuItem.Enabled = false;
            MSG.login.Show();
            Application.DoEvents();
            Analysis.SYS.ConStr = xconfig.CONNSTR;
            Form itf = new Analysis.销售统计(销售ToolStripMenuItem);
            itf.MdiParent = this;
            itf.Show();
        }

        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text.Length == 0 || e.Item.Text == "最小化(&N)" || e.Item.Text == "还原(&R)")
            {
                e.Item.Visible = false;
            }
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}