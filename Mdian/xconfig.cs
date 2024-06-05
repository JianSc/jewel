using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Net.Sockets;

namespace Mdian
{
    class xconfig
    {
        private static string sqlip = null;
        private static string sqluser = null;
        private static string sqlpassword = null;
        private static string jhsqlip = null;
        private static string jhsqluser = null;
        private static string jhsqlpwd = null;

        private static DataSet dst = new theDST();

        private static string user = null;
        private static string pwd = null;
        private static string mdian = null;
        private static string id = null;
        private static string sid = string.Empty;
        private static bool[] chkbol = new bool[7];
        private static double myzk = 1;
        private static string dqusen = string.Empty;
        private static string dqusi = string.Empty;
        private static string dzhi = string.Empty;
        private static string tel = string.Empty;

        public static string DZHI { get { return dzhi; } set { dzhi = value; } }
        public static string TEL { get { return tel; } set { tel = value; } }
        public static double MYZK { get { return myzk; } set { myzk = value; } }
        public static bool[] CHKBOL { get { return chkbol; } set { chkbol = value; } }
        public static string SID { get { return sid; } set { sid = value; } }
        public static string ID { get { return id; } set { id = value; } }
        public static string MDIAN { get { return mdian; } set { mdian = value; } }
        public static string USER { get { return user; } set { user = value; } }
        public static string PWD { get { return pwd; } set { pwd = value; } }
        public static string VERSION { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        public static bool DBBOL { get {return dbbol(); } }
        public static string DQUSEN { get { return dqusen; } set { dqusen = value; } }
        public static string DQUSI { get { return dqusi; } set { dqusi = value; } }
        public static bool DBonlineWT { get { return onlineWT(); } }

        /// <summary>
        /// 返回部门名称。
        /// </summary>
        public static string GSNAME { get { return gsname(); } }
        /// <summary>
        /// 返回 SQL 服务器登陆用户名。(金汇)
        /// </summary>
        public static string JHSQLUSER { get { return jhsqluser; } set { jhsqluser = value; } }
        /// <summary>
        /// 返回 SQL 服务器IP地址。(金汇)
        /// </summary>
        public static string JHSQLIP { get { return jhsqlip; } set { jhsqlip = value; } }
        /// <summary>
        /// 返回 SQL 服务器登陆密码。(金汇)
        /// </summary>
        public static string JHSQLPWD { get { return jhsqlpwd; } set { jhsqlpwd = value; } }
        /// <summary>
        /// 返回Dataset数据集 (clidata)
        /// </summary>
        public static DataSet DST { get { return dst; } }
        /// <summary>
        /// SQL服务器IP地址
        /// </summary>
        public static string SQLIP { get { return sqlip; } set { sqlip = value; } }
        /// <summary>
        /// SQL服务器登陆用户名
        /// </summary>
        public static string SQLUSER { get { return sqluser; } set { sqluser = value; } }
        /// <summary>
        /// SQL服务器登陆密码
        /// </summary>
        public static string SQLPASSWORD { get { return sqlpassword; } set { sqlpassword = value; } }
        /// <summary>
        /// 返回服务器IP
        /// </summary>
        public static string SERVERIP { get { return serverip(); } }
        /// <summary>
        /// 返回服务器端口
        /// </summary>
        public static int SERVERPORT { get { return serverport(); } }


        static string olestr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath +"\\data.dll;Persist Security Info=True;Jet OLEDB:Database Password=cnsdjian";
        private static string serverip()
        {
            OleDbConnection conn = new OleDbConnection(olestr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select serverip,serverport from [config]", conn);
            OleDbDataReader rs = cmd.ExecuteReader();
            string data = null;
            if (rs.Read())
            {
                data = rs["serverip"].ToString().Trim();
            }
            else
            {
                data = "0.0.0.0";
            }
            rs.Close();
            conn.Close();
            return data;
        }
        private static int serverport()
        {
            OleDbConnection conn = new OleDbConnection(olestr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select serverip,serverport from [config]", conn);
            OleDbDataReader rs = cmd.ExecuteReader();
            int data = 0;
            if (rs.Read())
            {
                data = int.Parse(rs["serverport"].ToString().Trim());
            }
            else
            {
                data = 0;
            }
            rs.Close();
            conn.Close();
            return data;
        }
        private static string gsname()
        {
            OleDbConnection conn = new OleDbConnection(olestr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select GSNAME from [config]", conn);
            OleDbDataReader rs = cmd.ExecuteReader();
            string data = null;
            if (rs.Read())
            {
                data = rs["GSNAME"].ToString().Trim();
            }
            else
            {
                data = "null";
            }
            rs.Close();
            conn.Close();
            return data;
        }

        public static string FunMD5(string inStr)
        {
            //return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Str , "MD5");
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] InBytes = Encoding.GetEncoding("GB2312").GetBytes(inStr);
            byte[] OutBytes = md5.ComputeHash(InBytes);
            string OutString = "";
            for (int i = 0; i < OutBytes.Length; i++)
            {
                OutString += OutBytes[i].ToString("x2");
            }

            return OutString.ToUpper();
        }

        public static int ASC(String Data)   //获取ASC码   
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(Data);
            int p = 0;

            if (b.Length == 1)   //如果为英文字符直接返回   
                return (int)b[0];
            for (int i = 0; i < b.Length; i += 2)
            {
                p = (int)b[i];
                p = p * 256 + b[i + 1] - 65536;
            }
            return p;
        }

        public static string GetdateTimeSSID()
        {
            string sid;
            sid = DateTime.Now.Year.ToString();
            sid += DateTime.Now.Month.ToString();
            sid += DateTime.Now.Day.ToString();
            sid += DateTime.Now.Hour.ToString();
            sid += DateTime.Now.Minute.ToString();
            sid += DateTime.Now.Second.ToString();
            return sid;
        }
        /// <summary>
        /// 返回SQL连接字符串
        /// </summary>
        public static string CONNSTR { get { return connstr(); } }

        private static string connstr()
        {
            string conn;
            if (xconfig.SQLIP == null)
            {
                MessageBox.Show("连接失效\n\n错误：0X000002", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            if (xconfig.SQLUSER == null)
            {
                MessageBox.Show("连接失效\n\n错误：0X000003", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            if (xconfig.SQLPASSWORD == null)
            {
                MessageBox.Show("连接失效\n\n错误：0X000004", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            conn = "SERVER=" + xconfig.SQLIP + ";DATABASE=Qzhi;UID=" + xconfig.SQLUSER + ";PWD=" + xconfig.SQLPASSWORD;
            return conn;
        }

        /// <summary>
        /// 返回SQL连接字符串(金汇)
        /// </summary>
        public static string JHCONNSTR { get { return jhconnstr(); } }

        private static string jhconnstr()
        {
            string conn;
            if (xconfig.JHSQLIP == null)
            {
                MessageBox.Show("连接失效\n\n错误：0X000002", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            if (xconfig.JHSQLUSER == null)
            {
                MessageBox.Show("连接失效\n\n错误：0X000003", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            if (xconfig.JHSQLPWD == null)
            {
                MessageBox.Show("连接失效\n\n错误：0X000004", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            conn = "SERVER=" + xconfig.JHSQLIP + ";DATABASE=jewel;UID=" + xconfig.JHSQLUSER + ";PWD=" + xconfig.JHSQLPWD;
            return conn;
        }

        /// <summary>
        /// 获取 ACCESS 连接字符串
        /// </summary>
        /// <param name="mdbname">mdb数据库文件名,勿输后缀</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static string oldstr(string mdbname, string pwd)
        {
            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\" + mdbname + ".dll;Persist Security Info=True;Jet OLEDB:Database Password=" + pwd;
            return connstr;
        }

        /// <summary>
        /// Socket 连接过程。
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool netSend(string msg)
        {
            bool stat = false;
            mesk.xSocket mesk = new mesk.xSocket();
            IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
            int port = xconfig.SERVERPORT;
            string msgstr;
            msgstr = msg;
            string msgget;
            msgget = mesk.Send(msgstr, ip, port);
            if (msgget != "ERROR")
            {
                stat = true;
            }
            return stat;
        }

        private static bool dbbol()
        {
            OleDbConnection conn = new OleDbConnection(olestr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select DB from [config]", conn);
            OleDbDataReader rs = cmd.ExecuteReader();
            bool data;
            if (rs.Read())
            {
                data = bool.Parse(rs["DB"].ToString().Trim());
            }
            else
            {
                data = false;
            }
            rs.Close();
            conn.Close();
            return data;
        }

        private static bool onlineWT()
        {
            OleDbConnection conn = new OleDbConnection(olestr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("select [OR] from [config]", conn);
            OleDbDataReader rs = cmd.ExecuteReader();
            bool data;
            if (rs.Read())
            {
                data = bool.Parse(rs["OR"].ToString().Trim());
            }
            else
            {
                data = false;
            }
            rs.Close();
            conn.Close();
            return data;
        }

        public static Image netImgGET(string msg)
        {
            try
            {
                mesk.xSocket mesk = new mesk.xSocket();
                IPAddress ip = IPAddress.Parse(xconfig.SERVERIP);
                int port = xconfig.SERVERPORT;
                string msgstr;
                msgstr = msg;
                byte[] b = mesk.ImgGet(msgstr, ip, port);
                if (b.Length < 1)
                {
                    return Properties.Resources.ImgGet_Err;
                }
                MemoryStream fs = new MemoryStream(b);
                //创建Image对象，并格式化MemoryStream流对象
                Image byimg = Image.FromStream(fs);
                return byimg;
            }
            catch { return Properties.Resources.ImgGet_Err; }
        }

        static string theIMG_msg = string.Empty;
        static PictureBox theIMG_pic = new PictureBox();
        static bool theIMG_bol = true;
        static Label theJD_label = new Label();

        public static void GetIMG(string msg, PictureBox p,bool imgbol,string tm,Label tml,Label jd)
        {
            theIMG_msg = msg;
            theIMG_pic = p;
            //theIMG_bol = imgbol;
            if (!theIMG_bol)
            {
                return;
            }

            tml.Text = tm;
            theJD_label = jd;
            jd.Text = "0kb/0kb 0%";
            Application.DoEvents();

            if (!imgbol)
            {
                p.Image = Properties.Resources.ImgGet_Err;
                return;
            }

            theIMG_bol = false;
            p.Image = Properties.Resources.LoadIMG;
            Application.DoEvents();

            Thread t = new Thread(new ThreadStart(showIMG));
            t.Start();

        }

        private delegate void d();

        private static void showIMG()
        {
            try
            {
                PictureBox p = new PictureBox();
                if (theIMG_pic.InvokeRequired) { d d = delegate { p = theIMG_pic; }; theIMG_pic.Invoke(d); }
                else { p = theIMG_pic; }

                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint IPEP = new IPEndPoint(IPAddress.Parse(SERVERIP), SERVERPORT);
                s.Connect(IPEP);

                //发送连接请求字符串
                byte[] itemb = new byte[1024];
                itemb = System.Text.Encoding.UTF8.GetBytes(theIMG_msg);
                s.Send(itemb);
                //接收图片大小
                byte[] itemb2 = new byte[1024];
                s.Receive(itemb2);
                string da = System.Text.Encoding.UTF8.GetString(itemb2);
                da = da.Replace("\0", "");
                da = da.Replace("<EOF>", "");

                //定义变量表示图片大小
                int dataleng = int.Parse(da);

                //如果图片大小小于1则退出过程
                if (dataleng < 1)
                {
                    if (theJD_label.InvokeRequired) { d d = delegate { theJD_label.Text = "0kb/0kb 100%"; }; theJD_label.Invoke(d); }
                    else { theJD_label.Text = "0kb/0kb 100%"; }
                    theIMG_pic.Image = Properties.Resources.ImgGet_Err;
                    theIMG_bol = true;
                    return;
                }

                //发送接收请求
                itemb = new byte[1024];
                itemb = System.Text.Encoding.UTF8.GetBytes("true<EOF>");
                s.Send(itemb);

                //定义每次接收数据的Byte大小
                int itemleng = 1024;
                //定义获取的包大小
                int got = 0;
                //定义内存流
                MemoryStream f = new MemoryStream();
                
                //循环如果内存流中的字节小于图片大小则循环
                while ((int)f.Length < dataleng)
                {
                    //定义Byte大小为Itemleng(1024),每次接包的大小
                    byte[] b = new byte[itemleng];
                    //变量got等于所接到数据包的大小
                    s.ReceiveTimeout = 5000;
                    got = s.Receive(b);
                    //将接到的数据包从0到got写入到内存流中
                    f.Write(b, 0, got);

                    //如果内存流中的字节加上1024大小图片大小
                    if ((f.Length + 1024) > dataleng )
                    {
                        //将接收数据的Byte大小更改为图片总字节减去内存流的的字节数
                        itemleng = dataleng - (int)f.Length;
                    }

                    //定义一个double变量，显示组件百分比
                    double bfs = ((double)f.Length / (double)dataleng) * 100;
                    //将百分比整型
                    int bf = (int)bfs;

                    int getbyte = ((int)f.Length) / 1024;
                    int maxbyte = (int)(double)dataleng / 1024;
                    if (theJD_label.InvokeRequired) { d d = delegate { theJD_label.Text = getbyte.ToString() + "k/" + maxbyte.ToString() + "k " + bf.ToString() + "%"; }; theJD_label.Invoke(d); }
                    else { theJD_label.Text = getbyte.ToString () + "kb/" + maxbyte.ToString() + "kb " + bf.ToString() + "%"; }
                    Application.DoEvents();

                }

                //定义图片变量并格式化内存流中的数据
                Image img = Image.FromStream(f);
                //关闭内存流
                f.Close();
                //格式化图片，将图片比例改为 PictureBox 的比例
                Bitmap bmp = new Bitmap(img, theIMG_pic.Size);
                //定义 PictureBox 所显示的图片为Bmp变量
                theIMG_pic.Image = bmp;

                //打开图片再次读取开关
                theIMG_bol = true;
                //结束Socket
                s.Shutdown(SocketShutdown.Both);
                //关闭Socket
                s.Close();
            }
            catch {
                theIMG_pic.Image = Properties.Resources.ImgGet_Err;
                theIMG_bol = true; 
            }
        }

    }
}
