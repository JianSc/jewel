using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing;

namespace sys
{
    public class xSocket
    {
        private DataTable skDataTable;
        private Socket client;
        private IPAddress IP;
        private int PROT;
        private bool _ListenOff = false;
        private string accConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\data.dll;Persist Security Info=True;Jet OLEDB:Database Password=cnsdjian";

        /// <summary>
        /// 服务端监听开关
        /// </summary>
        public bool ListenOff { get { return _ListenOff; } set { _ListenOff = value; } }

        #region xSocket 实例初始化
        /// <summary>
        /// 初始化 xSocket 实例。(重载)
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="prot">端口号</param>
        /// <param name="data">DataTable 数据库</param>
        public xSocket(IPAddress ip, int prot, DataTable data)
        {
            try
            {
                IP = ip;
                PROT = prot;
                skDataTable = data;
            }
            catch{ }
        }

        /// <summary>
        /// 初始化 xSocket 实例
        /// </summary>
        /// <param name="ip">IP 地址</param>
        /// <param name="prot">端口号</param>
        public xSocket(IPAddress ip, int prot)
        {
            try
            {
                IP = ip;
                PROT = prot;
            }
            catch { }
        }
    #endregion

        public void Start()
        {
            IPEndPoint IPEP = new IPEndPoint(IP, PROT);
            Socket mesk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mesk.Bind(IPEP);
            mesk.Listen(10);

            while (_ListenOff)
            {
                client = mesk.Accept();
                Thread newTRD = new Thread(new ThreadStart(this.Datasell));
                newTRD.Start();
            }
        }

        /// <summary>
        /// 数据处理模块
        /// </summary>
        private void Datasell()
        {
            try
            {
                IPEndPoint clientEP = (IPEndPoint)client.RemoteEndPoint;
                string clientIP = clientEP.Address.ToString();

                string data = null;

                while (true)
                {
                    byte[] b = new byte[1024];
                    int n = client.Receive(b);
                    data += System.Text.Encoding.UTF8.GetString(b);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                data = data.Replace("\0", "");
                data = data.Replace("<EOF>", "");
                string[] da = data.Split('|');

                string xdata;
                if (da.Length > 3)
                {
                    xdata = da[3];
                }
                else
                {
                    xdata = "null";
                }
                string xname = da[0];
                string xip = clientIP;
                string xstat = da[1];
                string sell = da[2];

                this.hand(xip, xname, xstat, client, sell, xdata);

                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch { }
        }

        private void hand(string xip, string xname, string xstat, Socket mesk,string hand,string data)
        {
            try
            {
                if (xname != "TEST")
                {
                    DataTable dt = skDataTable;
                    DataRow[] thedr = dt.Select("[xName] = '" + xname + "'");
                    if (thedr.Length > 0)
                    {
                        DataRow dr = thedr[0];
                        string newTime = DateTime.Now.ToString();
                        dr["newTime"] = newTime;
                        dr["xstat"] = xstat;
                    }
                    else
                    {
                        string newTime = DateTime.Now.ToString();
                        DataRow dr = dt.NewRow();
                        dr["xname"] = xname;
                        dr["xip"] = xip;
                        dr["xstat"] = xstat;
                        string xtime = DateTime.Now.Date.ToString();
                        xtime = xtime.Replace("0:00:00", "");
                        dr["xTime"] = xtime;
                        dr["newTime"] = newTime;
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch { }

            this.shell(mesk, hand,data);
            
        }

        private void shell(Socket mesk, string hand,string data)
        {
            switch (hand)
            {
                #region CONNECT
                case "CONNECT":
                    byte[] bt = new byte[1024];
                    bt = System.Text.Encoding.UTF8.GetBytes("TRUE<EOF>");
                    mesk.Send(bt);
                    break;
                #endregion
                #region GETDB
                case "GETDB":
                    string dbstr = accConStr;
                    string dbip,dbuser,dbpassword;
                    string mess = "NULL|0<EOF>";
                    try
                    {
                        OleDbConnection conn = new OleDbConnection(dbstr);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select dbuser,dbip,dbpassword from dbconfig", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            dbip = rs["dbip"].ToString().Trim();
                            dbuser = rs["dbuser"].ToString().Trim();
                            dbpassword = rs["dbpassword"].ToString().Trim();
                            mess = "TRUE|" + dbip + "|" + dbuser + "|" + dbpassword + "<EOF>";
                        }
                        else
                        {
                            mess = "ERROR|0<EOF>";
                        }
                        rs.Close();
                        conn.Close();

                        byte[] b = new byte[1024];
                        b = System.Text.Encoding.UTF8.GetBytes(mess);
                        mesk.Send(b);
                    }
                    catch { }
                    break;
                #endregion
                #region GETYDB
                case "GETYDB":
                    string ydbstr = accConStr;
                    string ydbip, ydbuser, ydbpassword;
                    string ymess = "NULL|0<EOF>";
                    try
                    {
                        OleDbConnection conn = new OleDbConnection(ydbstr);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select ydbuser,ydbip,ydbpassword from dbconfig", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            ydbip = rs["ydbip"].ToString().Trim();
                            ydbuser = rs["ydbuser"].ToString().Trim();
                            ydbpassword = rs["ydbpassword"].ToString().Trim();
                            ymess = "TRUE|" + ydbip + "|" + ydbuser + "|" + ydbpassword + "<EOF>";
                        }
                        else
                        {
                            ymess = "ERROR|0<EOF>";
                        }
                        rs.Close();
                        conn.Close();

                        byte[] b = new byte[1024];
                        b = System.Text.Encoding.UTF8.GetBytes(ymess);
                        mesk.Send(b);
                    }
                    catch { }
                    break;
                    #endregion
                #region GETYDB2
                case "GETYDB2":
                    string ydbstr2 = accConStr;
                    string ydbip2, ydbuser2, ydbpassword2;
                    string ymess2 = "NULL|0<EOF>";
                    try
                    {
                        OleDbConnection conn = new OleDbConnection(ydbstr2);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select ydbuser,ydbip2,ydbpassword from dbconfig", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            ydbip2 = rs["ydbip2"].ToString().Trim();
                            ydbuser2 = rs["ydbuser"].ToString().Trim();
                            ydbpassword2 = rs["ydbpassword"].ToString().Trim();
                            ymess2 = "TRUE|" + ydbip2 + "|" + ydbuser2 + "|" + ydbpassword2 + "<EOF>";
                        }
                        else
                        {
                            ymess2 = "ERROR|0<EOF>";
                        }
                        rs.Close();
                        conn.Close();

                        byte[] b = new byte[1024];
                        b = System.Text.Encoding.UTF8.GetBytes(ymess2);
                        mesk.Send(b);
                    }
                    catch { }
                    break;
                #endregion
                #region GETJHDB
                case "GETJHDB":
                    string dbstr1 = accConStr;
                    string dbip1, dbuser1, dbpassword1;
                    string mess1 = "NULL|0<EOF>";
                    try
                    {
                        OleDbConnection conn = new OleDbConnection(dbstr1);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select JHSERVERUSER,JHSERVERIP,JHSERVERPWD from dbconfig", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            dbip1 = rs["JHSERVERIP"].ToString().Trim();
                            dbuser1 = rs["JHSERVERUSER"].ToString().Trim();
                            dbpassword1 = rs["JHSERVERPWD"].ToString().Trim();
                            mess1 = "TRUE|" + dbip1 + "|" + dbuser1 + "|" + dbpassword1 + "<EOF>";
                        }
                        else
                        {
                            mess1 = "ERROR|0<EOF>";
                        }
                        rs.Close();
                        conn.Close();

                        byte[] b = new byte[1024];
                        b = System.Text.Encoding.UTF8.GetBytes(mess1);
                        mesk.Send(b);
                    }
                    catch { }
                    break;
                #endregion
                #region GETUSRE
                case "GETUSER":
                    try
                    {
                        string connstr = accConStr;
                        string userlist = null;
                        OleDbConnection conn = new OleDbConnection(accConStr);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select [name] from [user] order by [name] desc", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        while (rs.Read())
                        {
                            if (rs["name"].ToString().Trim() != "admin")
                            {
                                if (userlist == null)
                                {
                                    userlist += rs["name"].ToString().Trim();
                                }
                                else
                                {
                                    userlist += "|" + rs["name"].ToString().Trim();
                                }
                            }
                        }
                        if (userlist == null) { userlist = "NULL"; }
                        userlist += "<EOF>";
                        rs.Close();
                        conn.Close();
                        byte[] b = new byte[1024];
                        b = System.Text.Encoding.UTF8.GetBytes(userlist);
                        mesk.Send(b);
                    }
                    catch { }
                    break;
                #endregion
                #region GETPWD
                case "GETPWD":
                    string[] pwddata;
                    string pwdmesstext = "false<EOF>";
                    try
                    {
                        pwddata = data.Split(',');
                        OleDbConnection pwdconn = new OleDbConnection(accConStr);
                        pwdconn.Open();
                        OleDbCommand pwdcmd = new OleDbCommand("select [name],[password] from [user] where [name]='" + pwddata[0] + "' and [password] = '" + pwddata[1] + "'", pwdconn);
                        OleDbDataReader pwdrs = pwdcmd.ExecuteReader();
                        if (pwdrs.Read())
                        {
                            pwdmesstext = "true<EOF>";
                        }
                        pwdrs.Close();
                        pwdconn.Close();
                        byte[] b = new byte[1024];
                        b = System.Text.Encoding.UTF8.GetBytes(pwdmesstext);
                        mesk.Send(b);
                    }
                    catch { }
                    break;                     
                #endregion
                #region SETPWD
                case "SETPWD":
                    string meskstr = "false<EOF>";
                    byte[] setb = new byte[1024];
                    setb = System.Text.Encoding.UTF8.GetBytes(meskstr);
                    try
                    {
                        string[] xdata;
                        string xuser, xpwd;
                        xdata = data.Split(new char[]{','});
                        xuser = xdata[0];
                        xpwd = xdata[1];
                        OleDbConnection conn = new OleDbConnection(accConStr);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("UPDATE [USER] SET [PASSWORD]='" + xpwd + "' WHERE NAME = '" + xuser + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        meskstr = "true<EOF>";
                        setb = new byte[1024];
                        setb = System.Text.Encoding.UTF8.GetBytes(meskstr);
                    }
                    catch { }
                    mesk.Send(setb);
                    break;
                #endregion
                #region USERCLOSE
                case "USERCLOSE":
                    try
                    {
                        string user = data;
                        DataTable dt = skDataTable;
                        DataRow[] dr = dt.Select("[xName] = '" + user + "'", "");
                        if (dr.Length > 0)
                        {
                            dt.Rows.Remove(dr[0]);
                        }
                        string msgtxt;
                        msgtxt = "true<EOF>";
                        byte[] b = new byte[1024];
                        b = System.Text.Encoding.UTF8.GetBytes(msgtxt);
                        mesk.Send(b);
                    }
                    catch { }
                    break;
                #endregion
                #region GETTIME
                case "GETTIME":
                    try
                    {
                        string temTime;
                        temTime = DateTime.Now.ToString();
                        temTime += "<EOF>";
                        byte[] b = new byte[1024];
                        b = System.Text.Encoding.UTF8.GetBytes(temTime);
                        mesk.Send(b);
                    }
                    catch { }
                    break;
                #endregion
                #region GETCODETM
                case "GETCODETM":
                    try
                    {
                        string tmyear = null;
                        int tmautoid = 0;
                        OleDbConnection conn = new OleDbConnection(accConStr);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select [TMYEAR],[TMAUTOID] from [dbconfig]", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            tmyear = rs["TMYEAR"].ToString();
                            tmautoid = int.Parse(rs["TMAUTOID"].ToString());
                        }
                        rs.Close();
                        if (tmyear != null && tmautoid != 0)
                        {
                            int t = int.Parse(tmyear);
                            if (t < DateTime.Now.Year)
                            {
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[TMYEAR]='" + DateTime.Now.Year.ToString() + "',"
                                    + "[TMAUTOID]=" + 1, conn);
                                cmds.ExecuteNonQuery();
                                tmyear = DateTime.Now.Year.ToString();
                                tmautoid = 1;
                            }
                            else
                            {
                                tmautoid++;
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[TMAUTOID]=" + tmautoid, conn);
                                cmds.ExecuteNonQuery();
                            }
                            //截取年份字符段作为首3位
                            tmyear = tmyear.Remove(1, 1);
                            if (tmautoid.ToString().Length > 4) { tmautoid = 9999; }
                            //生成后面字符 5 表示5位数加上头3位为8位
                            int tmautoidsum = 4 - (tmautoid.ToString().Length);
                            string tmautoidstr = tmautoid.ToString();
                            for (int i = 1; i < tmautoidsum + 1; i++)
                            {
                                tmautoidstr = "0" + tmautoidstr;
                            }

                            Random rd = new Random();
                            int rdm = rd.Next(1, 9);

                            string msgstr = tmyear + tmautoidstr + rdm.ToString();
                            msgstr += "<EOF>";
                            byte[] b = new byte[1024];
                            b = System.Text.Encoding.UTF8.GetBytes(msgstr);
                            mesk.Send(b);
                        }
                        conn.Close();
                    }
                    catch { }
                    break;
                #endregion
                #region GETCODEDDH
                case "GETCODEDDH":
                    try
                    {
                        string tmyear = null;
                        int tmautoid = 0;
                        OleDbConnection conn = new OleDbConnection(accConStr);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select [DDHYEAR],[DDHAUTOID] from [dbconfig]", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            tmyear = rs["DDHYEAR"].ToString();
                            tmautoid = int.Parse(rs["DDHAUTOID"].ToString());
                        }
                        rs.Close();
                        if (tmyear != null && tmautoid != 0)
                        {
                            int t = int.Parse(tmyear);
                            if (t < DateTime.Now.Year)
                            {
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[DDHYEAR]='" + DateTime.Now.Year.ToString() + "',"
                                    + "[DDHAUTOID]=" + 1, conn);
                                cmds.ExecuteNonQuery();
                                tmyear = DateTime.Now.Year.ToString();
                                tmautoid = 1;
                            }
                            if (data == "GET")
                            {
                                int g = tmautoid + 1;
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[DDHAUTOID]=" + g, conn);
                                cmds.ExecuteNonQuery();
                            }
                            //截取年份字符段作为首3位
                            tmyear = tmyear.Remove(0, 1);
                            if (tmautoid.ToString().Length > 5) { tmautoid = 99999; }
                            //生成后面字符 5 表示5位数加上头3位为8位
                            int tmautoidsum = 5 - (tmautoid.ToString().Length);
                            string tmautoidstr = tmautoid.ToString();
                            for (int i = 1; i < tmautoidsum + 1; i++)
                            {
                                tmautoidstr = "0" + tmautoidstr;
                            }

                            string msgstr = "D" + tmyear + tmautoidstr;
                            msgstr += "<EOF>";
                            byte[] b = new byte[1024];
                            b = System.Text.Encoding.UTF8.GetBytes(msgstr);
                            mesk.Send(b);
                        }
                        conn.Close();
                    }
                    catch { }
                    break;
                    #endregion
                #region GETCODERKD
                case "GETCODERKD":
                    try
                    {
                        string tmyear = null;
                        int tmautoid = 0;
                        OleDbConnection conn = new OleDbConnection(accConStr);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select [RKDYEAR],[RKDAUTOID] from [dbconfig]", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            tmyear = rs["RKDYEAR"].ToString();
                            tmautoid = int.Parse(rs["RKDAUTOID"].ToString());
                        }
                        rs.Close();
                        if (tmyear != null && tmautoid != 0)
                        {
                            int t = int.Parse(tmyear);
                            if (t < DateTime.Now.Year)
                            {
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[RKDYEAR]='" + DateTime.Now.Year.ToString() + "',"
                                    + "[RKDAUTOID]=" + 1, conn);
                                cmds.ExecuteNonQuery();
                                tmyear = DateTime.Now.Year.ToString();
                                tmautoid = 1;
                            }
                            if (data == "GET")
                            {
                                int g = tmautoid + 1;
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[RKDAUTOID]=" + g, conn);
                                cmds.ExecuteNonQuery();
                            }
                            //截取年份字符段作为首3位
                            tmyear = tmyear.Remove(0, 1);
                            if (tmautoid.ToString().Length > 5) { tmautoid = 99999; }
                            //生成后面字符 5 表示5位数加上头3位为8位
                            int tmautoidsum = 5 - (tmautoid.ToString().Length);
                            string tmautoidstr = tmautoid.ToString();
                            for (int i = 1; i < tmautoidsum + 1; i++)
                            {
                                tmautoidstr = "0" + tmautoidstr;
                            }

                            string msgstr = "R" + tmyear + tmautoidstr;
                            msgstr += "<EOF>";
                            byte[] b = new byte[1024];
                            b = System.Text.Encoding.UTF8.GetBytes(msgstr);
                            mesk.Send(b);
                        }
                        conn.Close();
                    }
                    catch { }
                    break;
                    #endregion
                #region GETCODECKD
                case "GETCODECKD":
                    try
                    {
                        string tmyear = null;
                        int tmautoid = 0;
                        OleDbConnection conn = new OleDbConnection(accConStr);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select [CKDYEAR],[CKDAUTOID] from [dbconfig]", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            tmyear = rs["CKDYEAR"].ToString();
                            tmautoid = int.Parse(rs["CKDAUTOID"].ToString());
                        }
                        rs.Close();
                        if (tmyear != null && tmautoid != 0)
                        {
                            int t = int.Parse(tmyear);
                            if (t < DateTime.Now.Year)
                            {
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[CKDYEAR]='" + DateTime.Now.Year.ToString() + "',"
                                    + "[CKDAUTOID]=" + 1, conn);
                                cmds.ExecuteNonQuery();
                                tmyear = DateTime.Now.Year.ToString();
                                tmautoid = 1;
                            }
                            if (data == "GET")
                            {
                                int g = tmautoid + 1;
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[CKDAUTOID]=" + g, conn);
                                cmds.ExecuteNonQuery();
                            }
                            //截取年份字符段作为首3位
                            tmyear = tmyear.Remove(0, 1);
                            if (tmautoid.ToString().Length > 5) { tmautoid = 99999; }
                            //生成后面字符 5 表示5位数加上头3位为8位
                            int tmautoidsum = 5 - (tmautoid.ToString().Length);
                            string tmautoidstr = tmautoid.ToString();
                            for (int i = 1; i < tmautoidsum + 1; i++)
                            {
                                tmautoidstr = "0" + tmautoidstr;
                            }

                            string msgstr = "C" + tmyear + tmautoidstr;
                            msgstr += "<EOF>";
                            byte[] b = new byte[1024];
                            b = System.Text.Encoding.UTF8.GetBytes(msgstr);
                            mesk.Send(b);
                        }
                        conn.Close();
                    }
                    catch { }
                    break;
                    #endregion
                #region GETCODETHD
                case "GETCODETHD":
                    try
                    {
                        string tmyear = null;
                        int tmautoid = 0;
                        OleDbConnection conn = new OleDbConnection(accConStr);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select [THDYEAR],[THDAUTOID] from [dbconfig]", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            tmyear = rs["THDYEAR"].ToString();
                            tmautoid = int.Parse(rs["THDAUTOID"].ToString());
                        }
                        rs.Close();
                        if (tmyear != null && tmautoid != 0)
                        {
                            int t = int.Parse(tmyear);
                            if (t < DateTime.Now.Year)
                            {
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[THDYEAR]='" + DateTime.Now.Year.ToString() + "',"
                                    + "[THDAUTOID]=" + 1, conn);
                                cmds.ExecuteNonQuery();
                                tmyear = DateTime.Now.Year.ToString();
                                tmautoid = 1;
                            }
                            if (data == "GET")
                            {
                                int g = tmautoid + 1;
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[THDAUTOID]=" + g, conn);
                                cmds.ExecuteNonQuery();
                            }
                            //截取年份字符段作为首3位
                            tmyear = tmyear.Remove(0, 1);
                            if (tmautoid.ToString().Length > 5) { tmautoid = 99999; }
                            //生成后面字符 5 表示5位数加上头3位为8位
                            int tmautoidsum = 5 - (tmautoid.ToString().Length);
                            string tmautoidstr = tmautoid.ToString();
                            for (int i = 1; i < tmautoidsum + 1; i++)
                            {
                                tmautoidstr = "0" + tmautoidstr;
                            }

                            string msgstr = "T" + tmyear + tmautoidstr;
                            msgstr += "<EOF>";
                            byte[] b = new byte[1024];
                            b = System.Text.Encoding.UTF8.GetBytes(msgstr);
                            mesk.Send(b);
                        }
                        conn.Close();
                    }
                    catch { }
                    break;
                     #endregion
                #region GETCODEKTD
                case "GETCODEKTD":
                    try
                    {
                        string tmyear = null;
                        int tmautoid = 0;
                        OleDbConnection conn = new OleDbConnection(accConStr);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select [KTDYEAR],[KTDAUTOID] from [dbconfig]", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            tmyear = rs["KTDYEAR"].ToString();
                            tmautoid = int.Parse(rs["KTDAUTOID"].ToString());
                        }
                        rs.Close();
                        if (tmyear != null && tmautoid != 0)
                        {
                            int t = int.Parse(tmyear);
                            if (t < DateTime.Now.Year)
                            {
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[KTDYEAR]='" + DateTime.Now.Year.ToString() + "',"
                                    + "[KTDAUTOID]=" + 1, conn);
                                cmds.ExecuteNonQuery();
                                tmyear = DateTime.Now.Year.ToString();
                                tmautoid = 1;
                            }
                            if (data == "GET")
                            {
                                int g = tmautoid + 1;
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[KTDAUTOID]=" + g, conn);
                                cmds.ExecuteNonQuery();
                            }
                            //截取年份字符段作为首3位
                            tmyear = tmyear.Remove(0, 1);
                            if (tmautoid.ToString().Length > 5) { tmautoid = 99999; }
                            //生成后面字符 5 表示5位数加上头3位为8位
                            int tmautoidsum = 5 - (tmautoid.ToString().Length);
                            string tmautoidstr = tmautoid.ToString();
                            for (int i = 1; i < tmautoidsum + 1; i++)
                            {
                                tmautoidstr = "0" + tmautoidstr;
                            }

                            string msgstr = "K" + tmyear + tmautoidstr;
                            msgstr += "<EOF>";
                            byte[] b = new byte[1024];
                            b = System.Text.Encoding.UTF8.GetBytes(msgstr);
                            mesk.Send(b);
                        }
                        conn.Close();
                    }
                    catch { }
                    break;
                     #endregion
                #region GETCODEGH
                case "GETCODEGH":
                    try
                    {
                        //string tmyear = null;
                        int tmautoid = 0;
                        OleDbConnection conn = new OleDbConnection(accConStr);
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("select [GHAO] from [dbconfig]", conn);
                        OleDbDataReader rs = cmd.ExecuteReader();
                        if (rs.Read())
                        {
                            tmautoid = int.Parse(rs["GHAO"].ToString());
                        }
                        rs.Close();
                        if (tmautoid != 0)
                        {
                           if (data == "GET")
                            {
                                int g = tmautoid + 1;
                                OleDbCommand cmds = new OleDbCommand("update [dbconfig] set "
                                    + "[GHAO]=" + g, conn);
                                cmds.ExecuteNonQuery();
                            }
                            if (tmautoid.ToString().Length > 3) { tmautoid = 999; }
                            //生成后面字符 5 表示5位数加上头3位为8位
                            int tmautoidsum = 3 - (tmautoid.ToString().Length);
                            string tmautoidstr = tmautoid.ToString();
                            for (int i = 1; i < tmautoidsum + 1; i++)
                            {
                                tmautoidstr = "0" + tmautoidstr;
                            }

                            string msgstr = tmautoidstr;
                            msgstr += "<EOF>";
                            byte[] b = new byte[1024];
                            b = System.Text.Encoding.UTF8.GetBytes(msgstr);
                            mesk.Send(b);
                        }
                        conn.Close();
                    }
                    catch { }
                    break;
                    #endregion
                #region SETBMP
                case "SETBMP":
                    try
                    {
                        //接收数据并把data分解成数组，TEST|TEST|SETBMP|000222,63579<EOF> 000222为条码63579为图片大小
                        string[] da = data.Split(new char[] { ',' });
                        string statbol = "false<EOF>";
                        string TM = da[0];
                        int tmsize = int.Parse(da[1]);
                        byte[] b = new byte[1024];
                        statbol = "true<EOF>";
                        b = System.Text.Encoding.UTF8.GetBytes(statbol);
                        //发送数据表示服务端已作好接收准备
                        mesk.Send(b);

                        //定义一个数组，大小为data的图片大小
                        byte[] b1 = new byte[tmsize];
                        //开始接收数据
                        int m = mesk.Receive(b1);
                        //将接收到的数据储存在内存流中
                        MemoryStream fs = new MemoryStream(b1);
                        //创建Image对象，并格式化MemoryStream流对象
                        Image byimg = Image.FromStream(fs);
                        //将Image对象储存为本地文件
                        byimg.Save(Application.StartupPath + @"\\Img\\" + TM + ".bmp");

                        fs.Close();
                        statbol = "true<EOF>";
                        byte[] b2 = new byte[1024];
                        b2 = System.Text.Encoding.UTF8.GetBytes(statbol);
                        //发送数据表示接收完成
                        mesk.Send(b2);

                    }
                    catch { }
                    break;
                #endregion
                #region GETBMP
                case "GETBMP":
                    try
                    {
                        string TM = data;
                        bool filebol = File.Exists(Application.StartupPath + @"\\img\\" + TM + ".bmp");
                        if (!filebol)
                        {
                            string msg = "0<EOF>";
                            byte[] msgbyte = System.Text.Encoding.UTF8.GetBytes(msg);
                            mesk.Send(msgbyte);
                        }
                        else
                        {
                            FileStream fs = File.OpenRead(Application.StartupPath + @"\\img\\" + TM + ".bmp");
                            int fsleng = int.Parse(fs.Length.ToString());
                            if (fsleng < 1)
                            {
                                string msg = "0<EOF>";
                                byte[] msgbyte = System.Text.Encoding.UTF8.GetBytes(msg);
                                mesk.Send(msgbyte);
                            }
                            else
                            {
                                string imgsize = fs.Length.ToString() + "<EOF>";
                                byte[] msgbytes = System.Text.Encoding.UTF8.GetBytes(imgsize);
                                mesk.Send(msgbytes);
                                byte[] llb1 = new byte[1024];
                                int intllb1 = mesk.Receive(llb1);
                                string item2 = System.Text.Encoding.UTF8.GetString(llb1);

                                item2 = item2.Replace("\0", "");
                                item2 = item2.Replace("<EOF>", "");

                                bool getbol = bool.Parse(item2);

                                if (getbol)
                                {
                                    byte[] imgbyte = new byte[fs.Length];
                                    fs.Read(imgbyte, 0, imgbyte.Length);
                                    mesk.SendTimeout = 15000;
                                    mesk.Send(imgbyte);
                                }
                                else
                                {
                                    string msg = "0<EOF>";
                                    byte[] msgbyte = System.Text.Encoding.UTF8.GetBytes(msg);
                                    mesk.Send(msgbyte);
                                }
                            }
                            fs.Close();
                        }
                    }
                    catch { }
                    break;
                #endregion
                #region GETTIMEDH
                case "GETTIMEDH":
                    string sid;
                    sid = DateTime.Now.Year.ToString();
                    string itemsid;
                    itemsid = DateTime.Now.Month.ToString();
                    if (itemsid.Length < 2) { sid += "0"; sid += itemsid; }
                    else { sid += itemsid; }
                    itemsid = DateTime.Now.Day.ToString();
                    if (itemsid.Length < 2) { sid += "0"; sid += itemsid; }
                    else { sid += itemsid; }
                    itemsid = DateTime.Now.Hour.ToString();
                    if (itemsid.Length < 2) { sid += "0"; sid += itemsid; }
                    else { sid += itemsid; }
                    itemsid = DateTime.Now.Minute.ToString();
                    if (itemsid.Length < 2) { sid += "0"; sid += itemsid; }
                    else { sid += itemsid; }
                    itemsid = DateTime.Now.Second.ToString();
                    if (itemsid.Length < 2) { sid += "0"; sid += itemsid; }
                    else { sid += itemsid; }
                    sid += "<EOF>";
                    byte[] timeid = new byte[1024];
                    timeid = System.Text.Encoding.UTF8.GetBytes(sid);
                    mesk.Send(timeid);
                    break;
                #endregion
                #region GETVERSION
                case "GETVERSION":
                    string CBB = string.Empty;
                    string MBB=string.Empty;
                    OleDbConnection bbcon = new OleDbConnection(accConStr);
                    bbcon.Open();
                    OleDbCommand bbcmd = new OleDbCommand("SELECT RTRIM(CBB) AS CBB,RTRIM(MBB) AS MBB FROM DBCONFIG", bbcon);
                    OleDbDataReader bbrs = bbcmd.ExecuteReader();
                    if (bbrs.Read())
                    {
                        CBB = bbrs["CBB"].ToString();
                        MBB = bbrs["MBB"].ToString();
                    }
                    bbrs.Close();
                    bbcon.Close();

                    string cmbb = string.Empty;

                    if (data == "C")
                    {
                        cmbb = CBB + "<EOF>";
                    }
                    else if (data == "M")
                    {
                        cmbb = MBB + "<EOF>";
                    }

                    byte[] bbb = new byte[1024];
                    bbb = System.Text.Encoding.UTF8.GetBytes(cmbb);
                    mesk.Send(bbb);
                    break;
                #endregion
            }
        }
    }
}
