using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Net;

namespace imgUP
{
    public partial class imgform : Form
    {
        string thisConstr = string.Empty;
        ToolStripMenuItem panMENU;
        DataTable theDT = new theDST().Tables["imgDST"];
        camACP.Cam theCam;
        IPAddress ip;
        int port;
        string user;

        public imgform(string constr,ToolStripMenuItem m,IPAddress serverip,int serverport,string username)
        {
            InitializeComponent();
            thisConstr = constr;
            panMENU = m;
            ip = serverip;
            port = serverport;
            user = username;
        }

        private void imgform_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            dataGridView1.DataSource = theDT;

            theCam = new camACP.Cam(pictureBox1.Handle, 0, 0, pictureBox1.Width, pictureBox1.Height);
            
            if(!Directory.Exists(Application.StartupPath + @"\camimg"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\camimg");
            }

            string[] fw = Directory.GetFiles(Application.StartupPath + @"\camimg");
            foreach (string l in fw)
            {
                File.Delete(l);
            }

            MSG.login.Show("正在开启本机摄像头，请稍候....");
            theCam.Start();
            MSG.login.Close();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void imgform_FormClosed(object sender, FormClosedEventArgs e)
        {
            panMENU.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                textBox1.Focus();
                return;
            }

            string tm = textBox1.Text;
            DataRow[] dr = theDT.Select("name='" + tm + "'","");
            if (dr.Length > 0)
            {
                MessageBox.Show("这个条码已经存在！");
                textBox1.SelectAll();
                return;
            }

            bool rsbol;

                SqlConnection conn = new SqlConnection(thisConstr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TM FROM QGOODS WHERE TM='" + tm + "'", conn);
                SqlDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    rsbol = true;
                }
                else
                {
                    rsbol = false;
                }
                rs.Close();
                conn.Close();

            if (!rsbol)
            {
                MessageBox.Show("这个条码的货物不存在！");
                textBox1.SelectAll();
                return;
            }

            DataRow newrow = theDT.NewRow();
            newrow["name"] = tm;
            newrow["bol"] = true;
            theDT.Rows.Add(newrow);

            DataView dv = new DataView();
            dv.Table = theDT;
            dv.Sort = "id desc";

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dv;

            theCam.GrabImage(Application.StartupPath + @"\camimg\" + textBox1.Text + ".bmp");

            textBox1.Text = string.Empty;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            toolStripButton1.Enabled = false;

            MSG.login.Show("正在上传图片至服务器，请稍候...");

            DataRow[] oldr = theDT.Select("bol=true", "");

            foreach (DataRow i in oldr)
            {
                string tm = i["name"].ToString();
                if (!File.Exists(Application.StartupPath + @"\camimg\" + tm + ".bmp"))
                {
                    continue;
                }
                FileStream fs = File.OpenRead(Application.StartupPath + @"\camimg\" + tm + ".bmp");
                if (fs.Length < 1)
                {
                    continue;
                }

                mesk.xSocket mesk = new mesk.xSocket();

                string msg = user + "|入库|SETBMP|" + tm + "," + fs.Length.ToString() + "<EOF>";
                fs.Close();
                bool setbol = mesk.ImgSend(msg, ip, port, tm,"camimg");
                if (setbol)
                {
                    File.Delete(Application.StartupPath + @"\\Item\\" + tm + ".bmp");

                    SqlConnection conn = new SqlConnection(thisConstr);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE QGOODS SET IMGBOL=1 WHERE TM='" + tm + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MSG.login.Close();
            }
        }
    }
}