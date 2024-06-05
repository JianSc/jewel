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
    public partial class 基本资料 : Form
    {
        DataTable dstDQU = xconfig.DST.Tables["地区"];
        DataTable dstKHU = xconfig.DST.Tables["客户"];
        DataTable dstGYS = xconfig.DST.Tables["供应商"];
        DataTable dstMD = xconfig.DST.Tables["门店"];
        DataTable dstYG = xconfig.DST.Tables["员工"];
        DataTable dstJL = xconfig.DST.Tables["金料"];
        DataTable dstSL = xconfig.DST.Tables["石料"];
        DataTable dstSSI = xconfig.DST.Tables["首饰"];
        DataTable dstJDU = xconfig.DST.Tables["净度"];
        DataTable dstDWEI = xconfig.DST.Tables["单位"];
        DataTable dstCKU = xconfig.DST.Tables["仓库"];
        DataTable dstXZ = xconfig.DST.Tables["形状"];
        DataTable dstQG = xconfig.DST.Tables["切工"];
        DataTable dstYS = xconfig.DST.Tables["颜色"];
        ToolStripMenuItem panMENU;

        public 基本资料(ToolStripMenuItem menu)
        {
            panMENU = menu;
            try
            {
                string connstr, cmdstr;
                connstr = xconfig.CONNSTR;
                cmdstr = "select rtrim(name) as name,[lv],[id],[sid] from [qDQU]";
                config.conData.fill("sql", connstr, cmdstr, dstDQU);
                cmdstr = "select [id],rtrim(khuhao) as khuhao,rtrim(xin) as xin,rtrim(min) as min,rtrim(sji) as sji"
                + ",rtrim(xbie) as xbie,[nlin],convert(char(10),[sri],120) as sri"
                + ",rtrim(email) as email,rtrim(dqusen) as dqusen,rtrim(dqusi) as dqusi,zk"
                + ",rtrim(dzhi) as dzhi,[ahao],rtrim(zye) as zye from [qKHU]";
                config.conData.fill("sql", connstr, cmdstr, dstKHU);
                cmdstr = "select [id],rtrim(name) as name,rtrim([user]) as [user],rtrim(tel) as tel,"
                + "rtrim(czhen) as czhen,rtrim(dzhi) as dzhi,rtrim(email) as email,rtrim(dqusen) as dqusen,"
                + "rtrim(dqusi) as dqusi from [qGYSHANG]";
                config.conData.fill("sql", connstr, cmdstr, dstGYS);
                cmdstr = "select [id],rtrim(name) as name,rtrim([user]) as [user],rtrim(tel) as tel,"
                + "rtrim(czhen) as czhen,rtrim(dzhi) as dzhi,rtrim(email) as email,rtrim(dqusen) as dqusen,"
                + "rtrim(dqusi) as dqusi from [qMDIAN]";
                config.conData.fill("sql", connstr, cmdstr, dstMD);
                cmdstr = "select id,rtrim(sid) as sid,rtrim(name) as name,rtrim(tel) as tel,"
                +"rtrim(jguan) as jguan,rtrim(xbie) as xbie,rtrim(mdian) as mdian,rtrim(sfzhen) as sfzhen,convert(char(10),[time],120) as [time] from [qYGONG]";
                config.conData.fill("sql", connstr, cmdstr, dstYG);
                cmdstr = "select rtrim(name) as name from qJLIAO";
                config.conData.fill("sql", connstr, cmdstr, dstJL);
                cmdstr = "select rtrim(name) as name from qSLIAO";
                config.conData.fill("sql", connstr, cmdstr, dstSL);
                cmdstr = "select rtrim(name) as name from qSSHI";
                config.conData.fill("sql", connstr, cmdstr, dstSSI);
                cmdstr = "select rtrim(name) as name from qJDU";
                config.conData.fill("sql", connstr, cmdstr, dstJDU);
                cmdstr = "select rtrim(name) as name from qDWEI";
                config.conData.fill("sql", connstr, cmdstr, dstDWEI);
                cmdstr = "select rtrim(name) as name from qCKU";
                config.conData.fill("sql", connstr, cmdstr, dstCKU);
                cmdstr = "select rtrim(name) as name from qXZUANG";
                config.conData.fill("sql", connstr, cmdstr, dstXZ);
                cmdstr = "select rtrim(name) as name from qQIG";
                config.conData.fill("sql", connstr, cmdstr, dstQG);
                cmdstr = "select rtrim(name) as name from qYSE";
                config.conData.fill("sql", connstr, cmdstr, dstYS);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            InitializeComponent();
        }

        private void 基本资料_Load(object sender, EventArgs e)
        {
            //this.MaximumSize = this.Size;
            //this.MinimumSize = this.Size;

            this.WindowState = FormWindowState.Maximized;

            newtreevoid();
            khudgvset();
            gysdgvset();
            mddgvset();
            ygdgvset();
            jldgvset();
            sldgvset();
            ssidgvset();
            jdudgvset();
            dweidgvset();
            ckudgvset();
            xzdgvset();
            qgdgvset();
            ysdgvset();

            MSG.login.Close();
        }

        private void ysdgvset()
        {
            dataGridView9.DataSource = dstYS;
        }

        private void qgdgvset()
        {
            dataGridView8.DataSource = dstQG;
        }


        private void xzdgvset()
        {
            dataGridView7.DataSource = dstXZ;
        }

        private void ckudgvset()
        {
            dataGridView6.DataSource = dstCKU;
        }

        private void dweidgvset()
        {
            dataGridView5.DataSource = dstDWEI;
        }

        private void jdudgvset()
        {
            dataGridView4.DataSource = dstJDU;
        }

        private void ssidgvset()
        {
            dataGridView3.DataSource = dstSSI;
        }

        private void sldgvset()
        {
            dataGridView2.DataSource = dstSL;
        }

        private void jldgvset()
        {
            dataGridView1.DataSource = dstJL;
        }

        private void ygdgvset()
        {
            ygDGV.DataSource = dstYG;
            toolStripComboBox1.Items.Clear();
            toolStripComboBox1.Items.Add("加载全部.....");
            foreach (DataRow i in dstMD.Rows)
            {
                toolStripComboBox1.Items.Add(i["name"].ToString());
            }
        }

        private void mddgvset()
        {
            mdianDGV.DataSource = dstMD;
        }

        private void gysdgvset()
        {
            gysDGV.DataSource = dstGYS;
        }

        private void khudgvset()
        {
            khuDGV.DataSource = dstKHU;
            khutoolStripComboBox1.Items.Clear();
            khutoolStripComboBox1.Items.Add("加载全部.....");
            DataRow[] itemdr1 = dstDQU.Select("[lv]=1", "[name]");
            for (int i = 0; i < itemdr1.Length; i++)
            {
                khutoolStripComboBox1.Items.Add(" " + itemdr1[i]["name"].ToString());
                DataRow[] itemdr2 = dstDQU.Select("[lv]=2 and [sid]='" + itemdr1[i]["id"].ToString() + "'", "[name]");
                for (int j = 0; j < itemdr2.Length; j++)
                {
                    khutoolStripComboBox1.Items.Add("    " + itemdr2[j]["name"].ToString());
                }
            }
        }

        private void newtreevoid()
        {
            treeView2.Nodes.Clear();
            try
            {
                DataRow[] dr1 = dstDQU.Select("[lv]=1", "[name]");
                for (int i = 0; i < dr1.Length; i++)
                {
                    TreeNode newTree = new TreeNode(dr1[i]["name"].ToString());
                    treeView2.Nodes.Add(newTree);
                    DataRow[] dr2 = dstDQU.Select("[lv]=2 and [sid]='" + dr1[i]["id"].ToString() + "'", "[name]");
                    for (int j = 0; j < dr2.Length; j++)
                    {
                        newTree.Nodes.Add(dr2[j]["name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 基本资料_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void 基本资料_Deactivate(object sender, EventArgs e)
        {
            this.TopMost = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string msgstr;
            msgstr = xconfig.USER + "|资料维护|CONNECT<EOF>";
            bool getnet = xconfig.netSend(msgstr);
            if (!getnet)
            {
                MessageBox.Show("服务器连接错误！");
                return;
            }

            new 地区资料添加(dstDQU, treeView2).ShowDialog();
            this.Focus();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string treestr = treeView1.SelectedNode.Text;
            //MessageBox.Show(treestr);
            switch (treestr)
            {
                case "地区资料":
                    tabControl1.SelectedIndex = 0;
                    break;
                case "客户资料":
                    tabControl1.SelectedIndex = 1;
                    break;
                case "供应商资料":
                    tabControl1.SelectedIndex = 2;
                    break;
                case "门店资料":
                    tabControl1.SelectedIndex = 3;
                    break;
                case "员工管理":
                    tabControl1.SelectedIndex = 4;
                    break;
                case "金料类别":
                    tabControl1.SelectedIndex = 5;
                    break;
                case "石料类别":
                    tabControl1.SelectedIndex = 6;
                    break;
                case "首饰类别":
                    tabControl1.SelectedIndex = 7;
                    break;
                case "净度":
                    tabControl1.SelectedIndex = 8;
                    break;
                case "切工":
                    tabControl1.SelectedIndex = 9;
                    break;
                case "单位":
                    tabControl1.SelectedIndex = 10;
                    break;
                case "仓库":
                    tabControl1.SelectedIndex = 11;
                    break;
                case "形状":
                    tabControl1.SelectedIndex = 12;
                    break;
                case "颜色":
                    tabControl1.SelectedIndex = 13;
                    break;
                                   
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string treetext = treeView2.SelectedNode.Text;

            new 地区资料修改(dstDQU, treeView2, treetext).ShowDialog();
            this.Focus();
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("正在执行的删除操作无法恢复！\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            string itemstr = treeView2.SelectedNode.Text;
            if (itemstr == "")
            {
                return;
            }

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from [qDQU] where [name]='" + itemstr + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                DataRow[] itemdr = dstDQU.Select("[name]='" + itemstr + "'", "");
                if (itemdr.Length > 0)
                {
                    dstDQU.Rows.Remove(itemdr[0]);
                }
                newtreevoid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            string msgstr = xconfig.USER + "|资料维护|CONNECT<EOF>";
            bool getmsg;
            getmsg = xconfig.netSend(msgstr);
            if (!getmsg)
            {
                MessageBox.Show("服务器连接错误！\n请稍候再试，或联系管理员", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            new 客户资料添加(dstKHU, khuDGV).ShowDialog();
        }

        private void khuDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(khuDGV, "phao");
        }

        private void khuDGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            khuDGV_RowsAdded(khuDGV, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (khutoolStripComboBox1.Text == "")
            {
                khutoolStripComboBox1.DroppedDown = true;
                return;
            }

            string comstr = khutoolStripComboBox1.Text.Trim();
            if (comstr == "加载全部.....")
            {
                khuDGV.DataSource = null;
                khuDGV.DataSource = dstKHU;
                return;
            }
            string cmdstr;
            DataRow[] itemdr1 = dstDQU.Select("[name]='" + comstr + "'", "");
            if (itemdr1.Length < 1)
            {
                return;
            }
            string lv = itemdr1[0]["lv"].ToString();
            if (lv == "1")
            {
                cmdstr = "[dqusen]='" + comstr + "'";
            }
            else
            {
                cmdstr = "[dqusi]='" + comstr + "'";
            }

            DataRow[] itemdr3 = dstKHU.Select(cmdstr, "");
            khuDGV.DataSource = itemdr3;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (khuDGV.Rows.Count < 1) { return; }
            string msgstr;
            msgstr = xconfig.USER + "|资料维护|CONNECT<EOF>";
            bool stat;
            stat = xconfig.netSend(msgstr);
            if (!stat)
            {
                MessageBox.Show("服务器连接错误！\n请稍候再试，或联系管理员", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string ssid;
            ssid = khuDGV["idDataGridViewTextBoxColumn", khuDGV.CurrentCell.RowIndex].Value.ToString();
            new 客户资料修改(dstKHU, khuDGV, ssid).ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (khuDGV.Rows.Count < 1) { return; }
            if (MessageBox.Show("正在执行的删除操作无法恢复！\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            string ssid = khuDGV["idDataGridViewTextBoxColumn", khuDGV.CurrentCell.RowIndex].Value.ToString();
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from [qKHU] where [id]='" + ssid + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                DataRow[] itemdr1 = dstKHU.Select("[id]='" + ssid + "'", "");
                if (itemdr1.Length > 0)
                {
                    dstKHU.Rows.Remove(itemdr1[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|资料维护|CONNECT<EOF>";
            bool msgbol;
            msgbol = xconfig.netSend(msg);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new 供应商添加(dstGYS, gysDGV).ShowDialog();
            this.Focus();
        }

        private void gysDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(gysDGV, "gysphao");
        }

        private void gysDGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            gysDGV_RowsAdded(gysDGV, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (gysDGV.Rows.Count < 1)
            {
                return;
            }
            string msg = xconfig.USER + "|资料维护|CONNECT<EOF>";
            bool msgbol;
            msgbol = xconfig.netSend(msg);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ssid;
            ssid = gysDGV["idDataGridViewTextBoxColumn1",gysDGV.CurrentCell.RowIndex].Value.ToString();
            new 供应商修改(dstGYS, gysDGV, ssid).ShowDialog();
            this.Focus();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (gysDGV.Rows.Count < 1)
            {
                return;
            }
            if (MessageBox.Show("正在执行的删除操作是无法恢复！\n是否继续", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }
            try
            {
                string ssid = gysDGV["idDataGridViewTextBoxColumn1", gysDGV.CurrentCell.RowIndex].Value.ToString();
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from qGYSHANG where id='" + ssid + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                DataRow[] itemdr = dstGYS.Select("id='" + ssid + "'", "");
                if (itemdr.Length > 0)
                {
                    dstGYS.Rows.Remove(itemdr[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mdianDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(mdianDGV, "mdphao");
        }

        private void mdianDGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            mdianDGV_RowsAdded(mdianDGV, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|资料维护|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msg);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new 门店添加(dstMD, mdianDGV).ShowDialog();
            this.Focus();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|资料维护|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msg);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            string ssid = mdianDGV["idDataGridViewTextBoxColumn2", mdianDGV.CurrentCell.RowIndex].Value.ToString();
            new 门店修改(dstMD, mdianDGV, ssid).ShowDialog();
            this.Focus();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            string msg = xconfig.USER + "|资料维护|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msg);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            new 员工添加(dstYG, ygDGV).ShowDialog();
            this.Focus();
        }

        private void ygDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(ygDGV, "ygphao");
        }

        private void ygDGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ygDGV_RowsAdded(ygDGV, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text == "加载全部.....")
            {
                ygDGV.DataSource = null;
                ygDGV.DataSource = dstYG;
            }
            else
            {
                string itemstr = toolStripComboBox1.Text;
                DataRow[] itemdr1 = dstYG.Select("mdian='" + itemstr + "'", "name");
                ygDGV.DataSource = itemdr1;
            }

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView1, "jliaophao", 1);
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView1_RowsAdded(dataGridView1, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton25.Enabled = false;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton25.Enabled = true;
        }

        private void 基本资料_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataGridView1.DataSource = null;
            panMENU.Enabled = true;

            string msgstr = xconfig.USER + "|闲置|CONNECT<EOF>";
            xconfig.netSend(msgstr);
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            if (dstJL.Rows.Count < 1)
            {
                return;
            }

            toolStripButton25.Enabled = false;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter dars = new SqlDataAdapter("select * from qJLIAO", conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(dars);
                dars.Update(dstJL);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            toolStripButton25.Enabled = true;
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView2, "sliaophao", 1);
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView2_RowsAdded(dataGridView2, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton29.Enabled = false;
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton29.Enabled = true;
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            if (dstSL.Rows.Count < 1)
            {
                return;
            }
            toolStripButton29.Enabled = false;
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter dars = new SqlDataAdapter("select * from qSLIAO", conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(dars);
                dars.Update(dstSL);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            toolStripButton29.Enabled = true;
        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView3_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView3, "ssiphao", 1);
        }

        private void dataGridView3_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView3_RowsAdded(dataGridView3, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void dataGridView3_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton31.Enabled = false;
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton31.Enabled = true;
        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            if (dstSSI.Rows.Count < 1)
            {
                return;
            }

            toolStripButton31.Enabled = false;
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter dars = new SqlDataAdapter("select * from qSSHI", conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(dars);
                dars.Update(dstSSI);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            toolStripButton31.Enabled = true;
        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView4_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            if (dstJDU.Rows.Count < 1)
            {
                return;
            }

            toolStripButton33.Enabled = false;
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter dars = new SqlDataAdapter("select * from qJDU", conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(dars);
                dars.Update(dstJDU);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            toolStripButton33.Enabled = true;

        }

        private void dataGridView4_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView4, "jduphao", 1);
        }

        private void dataGridView4_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView4_RowsAdded(dataGridView4, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void dataGridView4_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton33.Enabled = false;
        }

        private void dataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton33.Enabled = true;
        }

        private void toolStripButton36_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView5_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView5, "dweiphao", 1);
        }

        private void dataGridView5_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView5_RowsAdded(dataGridView5, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void dataGridView5_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private void dataGridView5_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton35.Enabled = false;
        }

        private void dataGridView5_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton35.Enabled = true;
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            if (dstDWEI.Rows.Count < 1)
            {
                return;
            }

            toolStripButton35.Enabled = false;
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter dars = new SqlDataAdapter("select * from qDWEI", conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(dars);
                dars.Update(dstDWEI);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            toolStripButton35.Enabled = true;
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            if (ygDGV.Rows.Count < 1)
            {
                return;
            }
            if (MessageBox.Show("正在执行的删除操作无法恢复！\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }
            try
            {
                string ssid = ygDGV["idDataGridViewTextBoxColumn3", ygDGV.CurrentCell.RowIndex].Value.ToString();
                string sid = ygDGV["sidDataGridViewTextBoxColumn",ygDGV.CurrentCell.RowIndex].Value.ToString();
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from qYGONG where id='" + ssid + "'", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from qYGONG_STAT where UserID='" + sid + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                DataRow[] itemdr1 = dstYG.Select("id='" + ssid + "'", "");
                if (itemdr1.Length > 0)
                {
                    dstYG.Rows.Remove(itemdr1[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            if (ygDGV.Rows.Count < 1)
            {
                return;
            }
            string sid;
            sid = ygDGV["idDataGridViewTextBoxColumn3", ygDGV.CurrentCell.RowIndex].Value.ToString();
            if (sid == "" || sid == "0")
            {
                return;
            }
            string msg = xconfig.USER + "|资料维护|CONNECT<EOF>";
            bool msgbol;
            msgbol = xconfig.netSend(msg);
            if (!msgbol)
            {
                MessageBox.Show("服务器连接不正常！\n请检查线路或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            new 员工修改(dstYG, ygDGV, sid).ShowDialog();
            this.Focus();
        }

        private void toolStripButton38_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView6_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView6, "ckuphao", 1);
        }

        private void dataGridView6_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView6_RowsAdded(dataGridView6, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void dataGridView6_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void dataGridView6_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton37.Enabled = false;
        }

        private void dataGridView6_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton37.Enabled = true;
        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            if (dstCKU.Rows.Count < 1)
            {
                return;
            }
            toolStripButton37.Enabled = false;
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter dars = new SqlDataAdapter("select * from qCKU", conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(dars);
                dars.Update(dstCKU);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            toolStripButton37.Enabled=true;

        }

        private void toolStripButton40_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView7_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView7, "xzphao", 1);
        }

        private void dataGridView7_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView7_RowsAdded(dataGridView7, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void dataGridView7_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void dataGridView7_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton39.Enabled = false;
        }

        private void dataGridView7_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton39.Enabled = true;
        }

        private void toolStripButton39_Click(object sender, EventArgs e)
        {
            if (dstXZ.Rows.Count < 1)
            {
                return;
            }

            toolStripButton39.Enabled = false;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter dars = new SqlDataAdapter("select * from qXZUANG", conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(dars);
                dars.Update(dstXZ);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            toolStripButton39.Enabled = true;
        }

        private void toolStripButton42_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView8_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void dataGridView8_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView8, "qgphao", 1);
        }

        private void dataGridView8_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView8_RowsAdded(dataGridView8, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void dataGridView8_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton41.Enabled = false;
        }

        private void dataGridView8_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton41.Enabled = true;
        }

        private void toolStripButton41_Click(object sender, EventArgs e)
        {
            int d8row = dstQG.Rows.Count;
            if (d8row < 1)
            {
                return;
            }
            toolStripButton41.Enabled = false;
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter dars = new SqlDataAdapter("select * from qQIG", conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(dars);
                dars.Update(dstQG);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            toolStripButton41.Enabled = true;
        }

        private void dataGridView9_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void dataGridView9_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            toolStripButton43.Enabled = false;
        }

        private void dataGridView9_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton43.Enabled = true;
        }

        private void dataGridView9_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            config.conDGV.DGVAutoID(((DataGridView)sender), "ysephao", 1);
        }

        private void dataGridView9_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView9_RowsAdded(sender, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void toolStripButton44_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton43_Click(object sender, EventArgs e)
        {
            if (dstYS.Rows.Count < 1)
            {
                return;
            }

            toolStripButton43.Enabled = false;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter dars = new SqlDataAdapter("select * from qYSE", conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(dars);
                dars.Update(dstYS);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            toolStripButton43.Enabled = true;
        }

        private void 设置权限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ygDGV.Rows.Count < 1) { return; }
            string userID;
            userID = ygDGV["sidDataGridViewTextBoxColumn", ygDGV.CurrentCell.RowIndex].Value.ToString();
            if (userID == "") { return; }

            Form itemform = new 员工权限(userID);
            itemform.Show();
        }
    }
}