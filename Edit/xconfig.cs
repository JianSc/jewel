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

namespace Edit
{
    class xconfig
    {
        /// <summary>
        /// 返回SQL连接字符串
        /// </summary>
        public static string CONNSTR { get { return connstr(); } }
        private static string sqlip = null;
        private static string sqluser = null;
        private static string sqlpassword = null;
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
    }
}
