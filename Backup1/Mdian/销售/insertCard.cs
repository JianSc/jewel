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
    public partial class insertCard : Form
    {
        TextBox pan_tb;
        TextBox pan_zktb;
        CheckBox pan_cb;

        public insertCard(TextBox tb,TextBox zktb,CheckBox cb)
        {
            pan_cb = cb;
            pan_tb = tb;
            pan_zktb = zktb;
            InitializeComponent();
        }

        private void insertCard_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyasc = xconfig.ASC(e.KeyChar.ToString());
            if (keyasc != 13 && keyasc != 8)
            {
                Regex regex = new Regex(@"^[0-9a-zA-Z]&");
                string key = e.KeyChar.ToString();
                if (regex.Match(key).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                textBox1.Focus();
                return;
            }

            string msgstr = xconfig.USER + "|销售开单|CONNECT<EOF>";
            bool msgbol = xconfig.netSend(msgstr);
            if (!msgbol)
            {
                MessageBox.Show("服务端连接不正常！\n请检查线路，或与管理员联系。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool usebol = false;
            double usezk =1;
            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(KHUHAO) AS KHUHAO,ZK FROM QKHU WHERE KHUHAO='" + textBox1.Text.Trim() + "'", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    usebol = true;
                    usezk = double.Parse(rs["zk"].ToString());
                }
                else
                {
                    usebol = false;
                }
                rs.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (!usebol)
            {
                new 客户资料添加(textBox1.Text.Trim()).ShowDialog();
            }
            else
            {
                pan_tb.Text = textBox1.Text.Trim();
                pan_zktb.Text = usezk.ToString();
                pan_cb.Checked = usebol;
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}