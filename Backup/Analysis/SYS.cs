using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Analysis
{
    public class SYS
    {
        static string _constr = string.Empty;
        public static string ConStr { get { return _constr; } set { _constr = value; } }

        static DataTable _gysdt = new DataTable();
        static DataTable _mdiandt = new DataTable();
        static DataTable _ygdt = new DataTable();
        static DataTable _diqudt = new DataTable();
        static DataTable _jliaodt = new DataTable();
        static DataTable _sliaodt = new DataTable();
        static DataTable _sstypedt = new DataTable();

        public static DataTable GYSDT { get { return _gysdt; } set { _gysdt = value; } }
        public static DataTable MdianDT { get { return _mdiandt; } set { _mdiandt = value; } }
        public static DataTable YGDT { get { return _ygdt; } set { _ygdt = value; } }
        public static DataTable DiQuDT { get { return _diqudt; } set { _diqudt = value; } }
        public static DataTable JLiaoDT { get { return _jliaodt; } set { _jliaodt = value; } }
        public static DataTable SLiaoDT { get { return _sliaodt; } set { _sliaodt = value; } }
        public static DataTable SSTypeDT { get { return _sstypedt; } set { _sstypedt = value; } }

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

    }
}
