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

namespace Client
{
    class xconfig
    {
        private static string sqlip = null;
        private static string sqluser = null;
        private static string sqlpassword = null;
        private static string jhsqlip = null;
        private static string jhsqluser = null;
        private static string jhsqlpwd = null;

        private static DataSet dst = new clidata();

        private static string user = null;
        private static string pwd = null;

        public static string VERSION { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        public static string USER { get { return user; } set { user = value; } }
        public static string PWD { get { return pwd; } set { pwd = value; } }

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
        public static string oldstr(string mdbname)
        {
            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\" + mdbname + ".MDB;Persist Security Info=True";
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

        /// <summary>
        /// 获取月份最大日期和最小日期。返回(Datetime);
        /// </summary>
        /// <param name="year">年份。(String)</param>
        /// <param name="month">月份。(String)</param>
        /// <param name="b">最大日期还是最小(true:最大|false:最小) 例:2011-02-28 23:59:59 | 2011-01-01 00:00:00</param>
        /// <returns>Datetime</returns>
        public static DateTime DateTime_Max_Min(string year, string month, bool b)
        {
            DateTime abb;
            //DateTime outabb;
            string date = string.Empty;
            if (b)
            {
                date = year + "-" + month + "-31 23:59:59";
                if (!DateTime.TryParse(date,out abb))
                {
                    date = year + "-" + month + "-30 23:59:59";
                    if (!DateTime.TryParse(date,out abb))
                    {
                        date = year + "-" + month + "-29 23:59:59";
                        if (!DateTime.TryParse(date,out abb))
                        {
                            date = year + "-" + month + "-28 23:59:59";
                            if (!DateTime.TryParse(date,out abb))
                            {
                                date = year + "-" + month + "-27 23:59:59";
                            }
                        }
                    }
                }
            }
            else
            {
                date = year + "-" + month + "-1 00:00:00";
            }

            return DateTime.Parse(date);
        }

    }
}
