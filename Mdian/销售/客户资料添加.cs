using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Mdian.销售
{
    public partial class 客户资料添加 : Form
    {
        string KHAO = string.Empty;

        public 客户资料添加(string khao)
        {
            KHAO = khao;
            InitializeComponent();
        }

        DataTable dstDQU = new DataTable();

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

            khuhaobox.Text = KHAO;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string xin, xmin, sji, xbie, nlin, email, dqusen, dqusi, dzhi, aho, zye, khuhao;
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

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into [qKHU]("
                    + "[xin],[khuhao],[min],[sji],[xbie],[nlin],[sri],[email],[dqusen],[dqusi],[dzhi],[ahao],[zye]"
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
                    + "'" + zye + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                //panDGV.DataSource = panDT;

                this.Close();
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
    }
}