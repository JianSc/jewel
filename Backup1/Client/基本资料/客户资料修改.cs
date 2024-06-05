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
    public partial class 客户资料修改 : Form
    {
        DataTable panDT;
        DataGridView panDGV;
        string panSSID;

        public 客户资料修改(DataTable dt,DataGridView dgv,string ssid)
        {
            panDT = dt;
            panDGV = dgv;
            panSSID = ssid;
            InitializeComponent();
        }

        DataTable dstDQU = xconfig.DST.Tables["地区"];
        private void 客户资料修改_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "select [id],rtrim(name) as name,[sid],[lv] from [qDQU]";
            config.conData.fill("sql", constr, cmdstr, dstDQU);

            dqusenbox.Items.Clear();
            dqusibox.Items.Clear();
            DataRow[] itemdr1 = dstDQU.Select("[lv]=1", "[name]");
            for (int i = 0; i < itemdr1.Length; i++)
            {
                dqusenbox.Items.Add(itemdr1[i]["name"].ToString());
            }

            DataRow[] itemdr32 = panDT.Select("[id]='" + panSSID + "'", "");
            if (itemdr32.Length < 1)
            {
                MessageBox.Show("未知错误！\n\n请重新打开或与管理员联系。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            khuhaobox.Text = itemdr32[0]["khuhao"].ToString();
            xinbox.Text = itemdr32[0]["xin"].ToString();
            minbox.Text = itemdr32[0]["min"].ToString();
            sjibox.Text = itemdr32[0]["sji"].ToString();
            xbiebox.Text = itemdr32[0]["xbie"].ToString();
            nlinbox.Text = itemdr32[0]["nlin"].ToString();
            emailbox.Text = itemdr32[0]["email"].ToString();
            dqusenbox.Text = itemdr32[0]["dqusen"].ToString();
            dqusibox.Text = itemdr32[0]["dqusi"].ToString();
            dzhibox.Text = itemdr32[0]["dzhi"].ToString();
            ahaobox.Text = itemdr32[0]["ahao"].ToString();
            zyebox.Text = itemdr32[0]["zye"].ToString();
            sribox.Text = itemdr32[0]["sri"].ToString();
            textBox1.Text = itemdr32[0]["zk"].ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string xin, xmin, sji, xbie, nlin, email, dqusen, dqusi, dzhi, aho, zye, khuhao;
            DateTime sri;
            double zk;
            xin = xinbox.Text;
            xmin = minbox.Text;
            sji = sjibox.Text;
            xbie = xbiebox.Text;
            nlin = nlinbox.Text;
            email = emailbox.Text;
            dqusen = dqusenbox.Text;
            dqusi = dqusibox.Text;
            dzhi = dzhibox.Text;
            aho = ahaobox.Text;
            zye = zyebox.Text;
            khuhao = khuhaobox.Text;
            sri = DateTime.Parse(sribox.Text);
            sri = DateTime.Parse(sri.ToShortDateString());
            if (textBox1.Text == string.Empty) { textBox1.Text = "1"; }
            try { zk = double.Parse(textBox1.Text); }
            catch { zk = 1; textBox1.Text = "1"; }
            
            if (khuhao == "")
            {
                khuhaobox.Focus();
                return;
            }
            if (xin == "")
            {
                tabControl1.SelectedIndex = 0;
                xinbox.Focus();
                return;
            }
            if (xmin == "")
            {
                tabControl1.SelectedIndex = 0;
                minbox.Focus();
                return;
            }
            if (xbie == "")
            {
                tabControl1.SelectedIndex = 0;
                xbiebox.DroppedDown = true;
                return;
            }
            if (dqusenbox.Text == "")
            {
                tabControl1.SelectedIndex = 0;
                dqusenbox.DroppedDown = true;
                return;
            }

            DataRow[] itemdr1 = panDT.Select("[xin]='" + xin + "' and [min]='" + xmin + "'", "");
            ///同名处理函数
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("update [qKHU] set"
                    + "[xin]='" + xin + "',"
                    + "[min]='" + xmin + "',"
                    + "[sji]='" + sji + "',"
                    + "[xbie]='" + xbie + "',"
                    + "[nlin]='" + nlin + "',"
                    + "[sri]='" + sri + "',"
                    + "[email]='" + email + "',"
                    + "[dqusen]='" + dqusen + "',"
                    + "[dqusi]='" + dqusi + "',"
                    + "[dzhi]='" + dzhi + "',"
                    + "[ahao]='" + aho + "',"
                    + "[zye]='" + zye + "',"
                    + "[zk]='" + zk + "' where [id]='" + panSSID + "'", conn);
                cmd.ExecuteNonQuery();
                DataRow[] itemdr2 = panDT.Select("[id]='" + panSSID + "'", "");
                if (itemdr2.Length > 0)
                {
                    itemdr2[0]["xin"] = xin;
                    itemdr2[0]["min"] = xmin;
                    itemdr2[0]["sji"] = sji;
                    itemdr2[0]["xbie"] = xbie;
                    itemdr2[0]["nlin"] = nlin;
                    itemdr2[0]["sri"] = sri;
                    itemdr2[0]["email"] = email;
                    itemdr2[0]["dqusen"] = dqusen;
                    itemdr2[0]["dqusi"] = dqusi;
                    itemdr2[0]["dzhi"] = dzhi;
                    itemdr2[0]["ahao"] = aho;
                    itemdr2[0]["zye"] = zye;
                    itemdr2[0]["zk"] = zk;
                }

                panDGV.DataSource = null;
                panDGV.DataSource = panDT;

                foreach (DataGridViewRow i in panDGV.Rows)
                {
                    if (i.Cells["idDataGridViewTextBoxColumn"].Value.ToString() == panSSID)
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

        private void xinbox_Enter(object sender, EventArgs e)
        {
            string stat;
            stat = sender.GetType().ToString();
            if (stat == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.MistyRose;
            }
            else if (stat == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.MistyRose;
            }
        }

        private void xinbox_Leave(object sender, EventArgs e)
        {
            string stat;
            stat = sender.GetType().ToString();
            if (stat == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.White;
            }
            else if (stat == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.White;
            }
        }

        private void sjibox_Enter(object sender, EventArgs e)
        {
            string stat;
            stat = sender.GetType().ToString();
            if (stat == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.MistyRose;
            }
            else if (stat == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.MistyRose;
            }
        }

        private void sjibox_Leave(object sender, EventArgs e)
        {
            string stat;
            stat = sender.GetType().ToString();
            if (stat == "System.Windows.Forms.TextBox")
            {
                ((TextBox)sender).BackColor = Color.White;
            }
            else if (stat == "System.Windows.Forms.ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.White;
            }
        }

        private void sjibox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key;
            key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void xinbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key;
            key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void dqusenbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow[] itemdr1 = dstDQU.Select("[name]='" + dqusenbox.Text + "'", "");
            if (itemdr1.Length < 1)
            {
                return;
            }
            string ssid = itemdr1[0]["id"].ToString();

            DataRow[] itemdr2 = dstDQU.Select("[lv]=2 and [sid]='" + ssid + "'", "[name]");
            dqusibox.Items.Clear();
            for (int i = 0; i < itemdr2.Length; i++)
            {
                dqusibox.Items.Add(itemdr2[i]["name"].ToString());
            }
        }

        private void khuhaobox_Enter(object sender, EventArgs e)
        {
            xinbox.Focus();
        }

        private void sjibox_Leave_1(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9_]+\@{1}[a-zA-Z0-9]+\.[a-zA-Z0-9]{1,3}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = string.Empty;
            }

            sjibox_Leave(sender, new EventArgs());
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string key = e.KeyChar.ToString();
            Regex regex = new Regex(@"\d|\.");
            if (!regex.Match(key).Success)
            {
                e.Handled = true;
            }
        }
    }
}