using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using sys;
using System.IO;

namespace Server
{
    public partial class xMain : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        public xMain()
        {
            InitializeComponent();
        }

        Thread newTR;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            notifyIcon1.Visible = true;

            //if (!Directory.Exists(Application.StartupPath + @"\Img"))
            //{
            //    Directory.CreateDirectory(Application.StartupPath + @"\Img");
            //}

            PIC_A.Parent = this.pictureBox1;
            PIC_X.Parent = this.pictureBox1;
            PICEXIT.Parent = this.pictureBox1;
            Led1.Parent = this.pictureBox1;
            Led2.Parent = this.pictureBox1;
            pictureBox2.Parent = this.pictureBox1;



            IPAddress IP = IPAddress.Any;
            int PROT = 2010;
            DataTable data = serData.Tables["xUser"];

            xSocket mesk = new xSocket(IP, PROT, serData.Tables["xUser"]);
            mesk.ListenOff = true;
            newTR = new Thread(new ThreadStart(mesk.Start));
            newTR.Start();

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            //notifyIcon1.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PIC_X_MouseEnter(object sender, EventArgs e)
        {
            PIC_X.Image = Properties.Resources.X2;
        }

        private void PIC_X_MouseLeave(object sender, EventArgs e)
        {
            PIC_X.Image = Properties.Resources.X;
        }

        private void PIC_A_MouseEnter(object sender, EventArgs e)
        {
            PIC_A.Image = Properties.Resources._2;
        }

        private void PIC_A_MouseLeave(object sender, EventArgs e)
        {
            PIC_A.Image = Properties.Resources._;
        }

        private void PIC_A_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void PIC_X_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            //notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipTitle = "提示";
            notifyIcon1.BalloonTipText = "启智网络版服务端正在监控\n双击打开管理器";
            notifyIcon1.ShowBalloonTip(3000);
        }

        private void PICEXIT_MouseEnter(object sender, EventArgs e)
        {
            this.PICEXIT.Image = Properties.Resources.exit2;
        }

        private void PICEXIT_MouseLeave(object sender, EventArgs e)
        {
            this.PICEXIT.Image = Properties.Resources.exit;
        }

        private void PICEXIT_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("退出启智网络版服务端后客户端将无法正常运行！\n\n\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                newTR.Abort();
                this.Visible = false;
                try
                {
                    Socket mesk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint IPEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2010);
                    mesk.Connect(IPEP);
                    mesk.Close();
                }
                catch { }
                Application.Exit();
            }


        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView1.RowCount > 7)
            {
                myscrollbas.Visible = false;
            }
            else
            {
                myscrollbas.Visible = true;
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dataGridView1_RowsAdded(dataGridView1, new DataGridViewRowsAddedEventArgs(0, 0));
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);

        }

        private void 打开管理器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1_MouseDoubleClick(notifyIcon1, new MouseEventArgs(MouseButtons.Left,1,0,0,0));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = serData.Tables["xUser"];

            //for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            //{
            //    int ii = i + 1;
            //    this.dataGridView1["phao", i].Value = ii.ToString();
            //}

            DataTable dt = serData.Tables["xUser"];
            DateTime tem = DateTime.Parse(DateTime.Now.AddMinutes(-5).ToString());
            DataRow[] dr = dt.Select("newTime < '#" + tem + "#'");
            for (int i = 0; i < dr.Length; i++)
            {
                dt.Rows.Remove(dr[i]);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string errstr;
            errstr = e.Exception.Message;
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Tools().Show();
        }

        private void 新建用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new 用户管理().Show();
        }

    }
}