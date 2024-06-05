using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace mesk
{
    public class xSocket
    {
        public string Send(string message,IPAddress IP,int PORT)
        {
            try
            {
                Socket mesk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint IPEP = new IPEndPoint(IP, PORT);
                mesk.Connect(IPEP);
                byte[] b = new byte[1024];
                b = System.Text.Encoding.UTF8.GetBytes(message);
                mesk.Send(b);

                string str = string.Empty;
                while (true)
                {
                    byte[] c = new byte[1024];
                    int d = mesk.Receive(c);
                    str += System.Text.Encoding.UTF8.GetString(c);
                    if (str.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                mesk.Shutdown(SocketShutdown.Both);
                mesk.Close();

                string data = str;
                data = data.Replace("<EOF>", "");
                data = data.Replace("\0", "");

                return data;
            }
            catch { return "ERROR"; }
        }

        /// <summary>
        /// 本地图像上传。
        /// </summary>
        /// <param name="message">发送文本数据</param>
        /// <param name="IP">IP地址。</param>
        /// <param name="PORT">端口。</param>
        /// <param name="TM">条码。</param>
        /// <returns></returns>
        public bool ImgSend(string message, IPAddress IP, int PORT, string TM)
        {
            try
            {
                Socket mesk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint IPEP = new IPEndPoint(IP, PORT);
                mesk.Connect(IPEP);
                byte[] b1 = new byte[1024];
                b1 = System.Text.Encoding.UTF8.GetBytes(message);
                mesk.Send(b1);

                string str1 = string.Empty;
                byte[] b2 = new byte[1024];
                int d1 = mesk.Receive(b2);
                str1 += System.Text.Encoding.UTF8.GetString(b2);
                string data = str1;

                data = data.Replace("<EOF>", "");
                data = data.Replace("\0", "");

                bool databol = bool.Parse(data);
                bool filebol = File.Exists(Application.StartupPath + @"\\Item\\" + TM + ".bmp");
                if (databol)
                {
                    if (filebol)
                    {
                        //创建对象对本地文件进行读取
                        FileStream fs = File.OpenRead(Application.StartupPath + @"\\Item\\" + TM + ".bmp");
                        int fsleng = int.Parse(fs.Length.ToString());
                        if (fsleng < 1)
                        {
                            return false;
                        }
                        byte[] imgbyte = new byte[fs.Length];
                        fs.Read(imgbyte, 0, imgbyte.Length);
                        mesk.Send(imgbyte);
                        fs.Close();
                        string str2 = string.Empty;
                        byte[] b3 = new byte[1024];
                        int d2 = mesk.Receive(b3);
                        str2 += System.Text.Encoding.UTF8.GetString(b3);
                        string data1 = str2;
                        data1 = data.Replace("<EOF>", "");
                        data1 = data.Replace("\0", "");
                        bool databol1 = bool.Parse(data1);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                mesk.Shutdown(SocketShutdown.Both);
                mesk.Close();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 本地图像上传。
        /// </summary>
        /// <param name="message">发送文本数据</param>
        /// <param name="IP">IP地址。</param>
        /// <param name="PORT">端口。</param>
        /// <param name="TM">条码。</param>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public bool ImgSend(string message, IPAddress IP, int PORT, string TM,string path)
        {
            try
            {
                Socket mesk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint IPEP = new IPEndPoint(IP, PORT);
                mesk.Connect(IPEP);
                byte[] b1 = new byte[1024];
                b1 = System.Text.Encoding.UTF8.GetBytes(message);
                mesk.Send(b1);

                string str1 = string.Empty;
                byte[] b2 = new byte[1024];
                int d1 = mesk.Receive(b2);
                str1 += System.Text.Encoding.UTF8.GetString(b2);
                string data = str1;

                data = data.Replace("<EOF>", "");
                data = data.Replace("\0", "");

                bool databol = bool.Parse(data);
                bool filebol = File.Exists(Application.StartupPath + @"\\" + path + "\\" + TM + ".bmp");
                if (databol)
                {
                    if (filebol)
                    {
                        //创建对象对本地文件进行读取
                        FileStream fs = File.OpenRead(Application.StartupPath + @"\\" + path + "\\" + TM + ".bmp");
                        int fsleng = int.Parse(fs.Length.ToString());
                        if (fsleng < 1)
                        {
                            return false;
                        }
                        byte[] imgbyte = new byte[fs.Length];
                        fs.Read(imgbyte, 0, imgbyte.Length);
                        mesk.Send(imgbyte);
                        fs.Close();
                        string str2 = string.Empty;
                        byte[] b3 = new byte[1024];
                        int d2 = mesk.Receive(b3);
                        str2 += System.Text.Encoding.UTF8.GetString(b3);
                        string data1 = str2;
                        data1 = data.Replace("<EOF>", "");
                        data1 = data.Replace("\0", "");
                        bool databol1 = bool.Parse(data1);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                mesk.Shutdown(SocketShutdown.Both);
                mesk.Close();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 获取图像。
        /// </summary>
        /// <param name="msg">发送文本数据、条码</param>
        /// <param name="IP">IP地址。</param>
        /// <param name="PORT">端口。</param>
        /// <returns></returns>
        public byte[] ImgGet(string msg, IPAddress IP, int PORT)
        {
            try
            {
                Socket mesk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint IPEP = new IPEndPoint(IP, PORT);
                mesk.Connect(IPEP);
                byte[] b1 = new byte[1024];
                b1 = System.Text.Encoding.UTF8.GetBytes(msg);
                mesk.Send(b1);

                string str1 = string.Empty;
                byte[] b2 = new byte[1024];
                int d1 = mesk.Receive(b2);
                str1 += System.Text.Encoding.UTF8.GetString(b2);
                string data = str1;

                data = data.Replace("<EOF>", "");
                data = data.Replace("\0", "");

                int dataint = int.Parse(data);

                if (dataint < 1)
                {
                    return new byte[0];
                }

                byte[] bs1 = new byte[1024];
                string recestr = "true<EOF>";
                bs1 = System.Text.Encoding.UTF8.GetBytes(recestr);
                mesk.Send(bs1);

                byte[] b = new byte[dataint];
                int d2 = mesk.Receive(b);
                return b;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new byte[0];
            }
        }

    }
}
