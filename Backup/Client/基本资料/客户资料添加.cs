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
    public partial class 客户资料添加 : Form
    {
        DataTable panDT;
        DataGridView panDGV;

        public 客户资料添加(DataTable dt,DataGridView dgv)
        {
            panDT = dt;
            panDGV = dgv;
            InitializeComponent();
        }

        DataTable dstDQU = xconfig.DST.Tables["地区"];
        private void 客户资料添加_Load(object sender, EventArgs e)
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string xin, xmin, sji, xbie, nlin, email, dqusen, dqusi, dzhi, aho, zye, khuhao;
            double zk;
            DateTime sri;
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
            try { zk = double.Parse(hzkbox.Text); }
            catch { zk = 1; }
            
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

            DataRow[] itemdr2 = panDT.Select("khuhao='" + khuhao + "'", "");
            if (itemdr2.Length > 0)
            {
                MessageBox.Show("请注意！\n这个会员卡号已经分配！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                khuhaobox.Text = string.Empty;
                return;
            }

            DataRow[] itemdr1 = panDT.Select("[xin]='" + xin + "' and [min]='" + xmin + "'", "");
            ///同名处理函数

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into [qKHU]("
                    + "[xin],[khuhao],[min],[sji],[xbie],[nlin],[sri],[email],[dqusen],[dqusi],[dzhi],[ahao],[zye],[zk]"
                    + ")values("
                    + "'" + xin + "',"
                    + "'" + khuhao + "',"
                    + "'" + xmin + "',"
                    + "'" + sji + "',"
                    + "'" + xbie + "',"
                    + "'" + nlin + "',"
                    + "'" + sri + "',"
                    + "'" + email + "',"
                    + "'" + dqusen + "',"
                    + "'" + dqusi + "',"
                    + "'" + dzhi + "',"
                    + "'" + aho + "',"
                    + "'" + zye + "','"
                    + zk + "')", conn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter dars = new SqlDataAdapter("select [id],rtrim(khuhao) as khuhao,rtrim(xin) as xin,rtrim(min) as min,rtrim(sji) as sji"
                + ",rtrim(xbie) as xbie,[nlin],convert(char(10),[sri],120) as sri"
                + ",rtrim(email) as email,rtrim(dqusen) as dqusen,rtrim(dqusi) as dqusi,zk"
                + ",rtrim(dzhi) as dzhi,[ahao],rtrim(zye) as zye from [qKHU]", conn);
                panDT.Clear();
                dars.Fill(panDT);
                conn.Close();
                panDGV.DataSource = panDT;
                //config.conDGV.DGVAutoID(panDGV, "phao");

                xinbox.Text = "";
                minbox.Text = "";
                sjibox.Text = "";
                nlinbox.Text = "";
                sribox.Text = "1980-01-01";
                emailbox.Text = "";
                dqusenbox.Text = "";
                dqusibox.Text = "";
                dzhibox.Text = "";
                ahaobox.Text = "";
                zyebox.Text = "";
                khuhaobox.Text = "";
                hzkbox.Text = "1";

                if (!checkBox1.Checked)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void sjibox_Leave_1(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9_]+\@{1}[a-zA-Z0-9]+\.[a-zA-Z0-9]{1,3}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = string.Empty;
            }
            sjibox_Leave(sender, new EventArgs());
        }

        private void hzkbox_Enter(object sender, EventArgs e)
        {
            xinbox_Enter(sender, new EventArgs());
            if (((TextBox)sender).Text == "1")
            {
                ((TextBox)sender).Text = string.Empty;
            }
        }

        private void hzkbox_Leave(object sender, EventArgs e)
        {
            xinbox_Leave(sender, new EventArgs());
            if (((TextBox)sender).Text == string.Empty)
            {
                ((TextBox)sender).Text = "1";
            }
            Regex regex = new Regex(@"^\d\.\d{1,2}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = "1";
            }
        }

        private void hzkbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = e.KeyChar.ToString();
            Regex regex = new Regex(@"\d|\.");
            if (!regex.Match(str).Success)
            {
                e.Handled = true;
            }
        }
    }
}