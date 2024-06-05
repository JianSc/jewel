using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Net;
using System.Threading;

namespace Client
{
    public partial class Lend : Form
    {
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Lend_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            //打开LOGIN窗体，并预读SQL数据库连接信息
            new cliMain().ShowDialog();
            try
            {
                this.Visible = true;
            }
            catch { }

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.label1.Parent = this.pictureBox1;
            this.label2.Parent = this.pictureBox1;
            this.pictureBox2.Parent = this.pictureBox1;

            this.xexit.Parent = this.pictureBox1;
            this._min.Parent = this.pictureBox1;
            this.button1img.Parent = this.pictureBox1;
            this.button2img.Parent = this.pictureBox1;

            //passwordbox.Text = config.FunMD5("1");

            try
            {
                string user;
                mesk.xSocket mesk = new mesk.xSocket();
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                string messtext = "TEST|0|GETUSER<EOF>";
                user = mesk.Send(messtext, ip, port);

                string[] userlist = user.Split('|');
                if (userlist[0] != "NULL")
                {
                    for (int i = 0; i < userlist.Length; i++)
                    {
                        this.userbox.Items.Add(userlist[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void xexit_MouseEnter(object sender, EventArgs e)
        {
            this.xexit.Image = Properties.Resources.X2;
        }

        private void xexit_MouseLeave(object sender, EventArgs e)
        {
            this.xexit.Image = Properties.Resources.X;
        }

        private void _min_MouseLeave(object sender, EventArgs e)
        {
            this._min.Image = Properties.Resources._1;
        }

        private void _min_MouseEnter(object sender, EventArgs e)
        {
            this._min.Image = Properties.Resources._2;
        }

        private void button1img_MouseEnter(object sender, EventArgs e)
        {
            this.button1img.Image = Properties.Resources.button11;
        }

        private void button1img_MouseLeave(object sender, EventArgs e)
        {
            this.button1img.Image = Properties.Resources.button1;
        }

        private void button2img_MouseEnter(object sender, EventArgs e)
        {
            this.button2img.Image = Properties.Resources.button21;
        }

        private void button2img_MouseLeave(object sender, EventArgs e)
        {
            this.button2img.Image = Properties.Resources.button2;
        }

        private void xexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void _min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2img_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void button1img_Click(object sender, EventArgs e)
        {
            if (userbox.Text == "")
            {
                //userbox.BackColor = Color.Tomato;
                userbox.Focus();
                passwordbox.Text = "";
                return;
            }
            if (passwordbox.Text == "")
            {
                passwordbox.Focus();
                return;
            }

            try
            {
                string messtext;
                string pwdmess;
                pwdmess = xconfig.FunMD5(passwordbox.Text.Trim());
                messtext = "TEST|0|GETPWD|" + userbox.Text.Trim() + "," + pwdmess + "<EOF>";

                mesk.xSocket mesk = new mesk.xSocket();
                string recdata = null;
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                recdata = mesk.Send(messtext, ip, port);

                bool pwdexp = false;
                pwdexp = bool.Parse(recdata);

                //Thread.Sleep(100);

                if (!pwdexp)
                {
                    MessageBox.Show("出错了！\n\n用户名或密码不正确。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                MSG.login.Show();
                Application.DoEvents();

                messtext = userbox.Text.Trim() + "|登陆|CONNECT<EOF>";
                recdata = mesk.Send(messtext, ip, port);

                xconfig.USER = userbox.Text.Trim();
                xconfig.PWD = passwordbox.Text.Trim();

                //Thread.Sleep(100);

                new xmain().Show();
                xmain.fr_fchuang = this;
                this.Visible = false;
            }
            catch
            {
                MessageBox.Show("与服务器连接不正常");
            }
            
        }

        private void Lend_VisibleChanged(object sender, EventArgs e)
        {
            this.userbox.Text = "";
            this.passwordbox.Text = "";
        }
    }
}