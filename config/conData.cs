using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace config
{
    public class conData
    {
        /// <summary>
        /// DataTable Fille 函数
        /// </summary>
        /// <param name="accorsql">ACC或SQL数据库。请输"acc"或者"sql" 区分大小写</param>
        /// <param name="connstr">conn连接字符串</param>
        /// <param name="commstr">comm连接字符串</param>
        /// <param name="dt">DataTable</param>
        public static void fill(string accorsql, string connstr, string commstr, DataTable dt)
        {
            try
            {
                if (accorsql == "acc")
                {
                    OleDbConnection conn = new OleDbConnection(connstr);
                    conn.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter(commstr, conn);
                    dt.Clear();
                    da.Fill(dt);
                    conn.Close();
                }
                else if (accorsql == "sql")
                {
                    SqlConnection conn = new SqlConnection(connstr);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(commstr, conn);
                    dt.Clear();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
