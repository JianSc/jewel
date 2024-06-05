using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Mdian
{
    public partial class Lend : Form
    {
        DataTable teDST = new theDST().Tables["员工"];

        public Lend()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void Lend_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            try
            {
                new Login(teDST).ShowDialog();
            }
            catch { }
            try
            {
                this.Visible = true;
            }
            catch { }
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            Mdianbox.Text = xconfig.GSNAME;

            this.pictureBox2.Parent = pictureBox1;
            this._min.Parent = pictureBox1;
            this.xexit.Parent = pictureBox1;

            button1img.Parent = pictureBox1;
            button2img.Parent = pictureBox1;

            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            label3.Parent = pictureBox1;

            Mdianbox.Parent = pictureBox1;
            userbox.Parent = pictureBox1;
            passwordbox.Parent = pictureBox1;
           
        }

        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void _min_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Properties.Resources._2;
        }

        private void _min_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Properties.Resources._;
        }

        private void xexit_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Properties.Resources.X2;
        }

        private void xexit_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Properties.Resources.X;
        }

        private void button1img_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Properties.Resources.button21;
        }

        private void button1img_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Properties.Resources.button2;
        }

        private void button2img_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Properties.Resources.button11;
        }

        private void button2img_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).Image = Properties.Resources.button1;
        }

        private void button1img_Click(object sender, EventArgs e)
        {
            if (Mdianbox.Text == "")
            {
                Mdianbox.Focus();
                return;
            }
            if (userbox.Text == "")
            {
                userbox.Focus();
                return;
            }
            if (passwordbox.Text == "")
            {
                passwordbox.Focus();
                return;
            }

            string user, pwd, id, SID;
            user  =string.Empty;
            pwd=string.Empty;
            id=string.Empty;
            pwd = xconfig.FunMD5(passwordbox.Text);
            SID = string.Empty;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT RTRIM(SID) AS SID,ID,RTRIM(NAME) AS NAME,RTRIM(PWD) AS PWD FROM QYGONG WHERE (SID='" + userbox.Text + "') AND (PWD='" + xconfig.FunMD5(passwordbox.Text) + "') AND (MDIAN='"+Mdianbox.Text +"')", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    user = rs["NAME"].ToString();
                    pwd = rs["PWD"].ToString();
                    id = rs["ID"].ToString();
                    SID = rs["SID"].ToString();
                }
                else
                {
                    user = "null";
                }
                rs.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }

            if (user == "null")
            {
                MessageBox.Show("工号或密码不正确！没有找到相关资料！\n请仔细核对你的资料后再试。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string dqusen = string.Empty;
            string dqusi = string.Empty;
            string dzhi = string.Empty;
            string tel = string.Empty;

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT DQUSEN,DQUSI,DZHI,TEL FROM QMDIAN WHERE [NAME]='" + Mdianbox.Text + "'", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    dqusen = rs["dqusen"].ToString().Trim();
                    dqusi = rs["dqusi"].ToString().Trim();
                    dzhi = rs["dzhi"].ToString().Trim();
                    tel = rs["tel"].ToString().Trim();
                }
                rs.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            xconfig.DZHI = dzhi;
            xconfig.TEL = tel;
            xconfig.USER = user;
            xconfig.PWD = pwd;
            xconfig.MDIAN = Mdianbox.Text;
            xconfig.ID = id;
            xconfig.SID = SID;
            xconfig.DQUSEN = dqusen;
            xconfig.DQUSI = dqusi;

            string msgstr = xconfig.USER + "|登陆|CONNECT<EOF>";
            xconfig.netSend(msgstr);

            this.Visible = false;
            userbox.Text = "";
            passwordbox.Text = "";
            new xMain(this, teDST).Show();
        }

        private void button2img_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void xexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void _min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void userbox_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.LightGreen;
        }

        private void userbox_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }

        private void userbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{Tab}");
            }
        }
    }
}