using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;

namespace Tools
{
    public partial class Form1 : Form
    {
        ComboBox thiscombox;
        public Form1()
        {
            //try
            //{
            //    OleDbConnection conn = new OleDbConnection(xconfig.oldstr("data","cnsdjian"));
            //    conn.Open();
            //    OleDbCommand cmd = new OleDbCommand("SELECT SERVERIP,SERVERPORT,GSNAME",conn);
            //    OleDbDataReader rs = cmd.ExecuteReader();
            //    if(rs.Read())
            //    {
            //        xconfig.SERVERIP = rs["SERVERIP"].ToString();
            //        xconfig.SERVERPORT =int.Parse(rs["SERVERPORT"].ToString());
            //        xconfig.GSNAME = rs["GSNAME"].ToString();
            //        xconfig.DBBOL = bool.Parse(rs["DB"].ToString());
            //    }
            //    rs.Close();
            //    conn.Close();
            //}catch{}
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            thiscombox = comboBox1;
            textBox1.Text = xconfig.SERVERIP;
            textBox2.Text=xconfig.SERVERPORT.ToString();
            checkBox1.Checked = xconfig.DBBOL;
            Application.DoEvents();
            Thread thisTR = new Thread(new ThreadStart(getmdian));
            thisTR.Start();
            if (xconfig.ORBOL)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
            }
        }

        private delegate void threadstr();
        private void getmdian()
        {
            try
            {
                string data = null;
                mesk.xSocket mesks = new mesk.xSocket();
                string mess = string.Empty;
                if (xconfig.DBBOL)
                {
                    if (!xconfig.ORBOL)
                    {
                        mess = "TEST|0|GETYDB2<EOF>";
                    }
                    else
                    {
                        mess = "TEST|0|GETYDB<EOF>";
                    }
                }
                else
                {
                    mess = "TEST|0|GETDB<EOF>";
                }
                data = mesks.Send(mess, IPAddress.Parse(xconfig.SERVERIP), xconfig.SERVERPORT);
                //MessageBox.Show(data);
                string[] tem = data.Split('|');
                if (tem[0] == "NULL" || tem[0] == "ERROR")
                {
                    return;
                }
                xconfig.SQLIP = tem[1];
                xconfig.SQLUSER = tem[2];
                xconfig.SQLPASSWORD = tem[3];

                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(xconfig.CONNSTR);
                conn.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("SELECT RTRIM(NAME) AS NAME FROM QMDIAN", conn);
                cmd.Fill(dt);
                conn.Close();

                foreach (DataRow i in dt.Rows)
                {
                    if (comboBox1.InvokeRequired)
                    {
                        threadstr d = delegate
                        {
                            comboBox1.Items.Add(i["NAME"].ToString());
                        };
                        comboBox1.Invoke(d);
                    }
                    else
                    {
                        comboBox1.Items.Add(i["NAME"].ToString());
                    }
                }

            }
            catch
            {
                if (comboBox1.InvokeRequired)
                {
                    threadstr d = delegate
                    {
                        comboBox1.Enabled = false;
                    };
                    comboBox1.Invoke(d);
                }
                else
                {
                    comboBox1.Enabled = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                tabControl1.SelectedIndex = 1;
                return;
            }

            try
            {
                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("data","cnsdjian"));
                conn.Open();
                string cmdstr = string.Empty;
                bool wlan = false;
                if (radioButton1.Checked)
                {
                    wlan = true;
                }
                else
                {
                    wlan = false;
                }

                if (comboBox1.Text == string.Empty)
                {
                    cmdstr = "UPDATE CONFIG SET SERVERIP='" + textBox1.Text + "',SERVERPORT='" + textBox2.Text + "',DB=" + checkBox1.Checked + ",[OR]=" + wlan;
                }
                else
                {
                    cmdstr = "UPDATE CONFIG SET SERVERIP='" + textBox1.Text + "',SERVERPORT='" + textBox2.Text + "',DB=" + checkBox1.Checked + ",[OR]=" + wlan + ",GSNAME='" + comboBox1.Text + "'";
                }
                OleDbCommand cmd = new OleDbCommand(cmdstr, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else if (key == 8)
            {
                e.Handled = false;
            }
            else
            {
                Regex regex = new Regex(@"\d|\.");
                if (!regex.Match(e.KeyChar.ToString()).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.MistyRose;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }

        private void textBox1_Leave_1(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                ((TextBox)sender).Text = string.Empty;
            }
            textBox1_Leave(sender, new EventArgs());
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else if (key == 8)
            {
                e.Handled = false;
            }
            else
            {
                Regex regex = new Regex(@"\d");
                if (!regex.Match(e.KeyChar.ToString()).Success)
                {
                    e.Handled = true;
                }
            }
        }
    }
}