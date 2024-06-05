using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace config
{
    public class conDGV
    {
        /// <summary>
        /// DataGridView 自动编号
        /// </summary>
        /// <param name="DGV">DataGridView</param>
        /// <param name="CountName">需要写入编号的列名</param>
        public static void DGVAutoID(DataGridView DGV, string CountName)
        {
            int Dr = DGV.Rows.Count;
            for (int i = 0; i < Dr; i++)
            {
                int j = i + 1;
                DGV[CountName, i].Value = j.ToString();
            }
        }
        /// <summary>
        /// DataGridView 自动编号
        /// </summary>
        /// <param name="DGV">DataGridView</param>
        /// <param name="CountName">需要写入编号的列名</param>
        /// <param name="row">需要减去的行数</param>
        public static void DGVAutoID(DataGridView DGV, string CountName, int row)
        {
            int dr = DGV.Rows.Count - row;
            for (int i = 0; i < dr; i++)
            {
                int j = i + 1;
                DGV[CountName, i].Value = j.ToString();
            }
        }

        public static string DateAutoID()
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
    }
}
