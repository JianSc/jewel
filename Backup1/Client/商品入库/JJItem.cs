using System;
using System.Collections.Generic;
using System.Text;

namespace Client.商品入库
{
    class JJItem
    {
        static string jjia = "0";
        static string zsjia = "0";
        static string zsmony = "0";
        static string fsjia = "0";
        static string fsmony = "0";
        static string jgmony = "0";
        static string jgshao = "0";
        static string qtmony = "0";
        static string bl = "1";

        /// <summary>
        /// 金价
        /// </summary>
        public static string JJIA { get { return jjia; } set { jjia = value; } }
        /// <summary>
        /// 主石价
        /// </summary>
        public static string ZSJIA { get { return zsjia; } set { zsjia = value; } }
        /// <summary>
        /// 主石金额
        /// </summary>
        public static string ZSMONY { get { return zsmony; } set { zsmony = value; } }
        /// <summary>
        /// 辅石价
        /// </summary>
        public static string FSJIA { get { return fsjia; } set { fsjia = value; } }
        /// <summary>
        /// 辅石金额
        /// </summary>
        public static string FSMONY { get { return fsmony; } set { fsmony = value; } }
        /// <summary>
        /// 加工费
        /// </summary>
        public static string JGMONY { get { return jgmony; } set { jgmony = value; } }
        /// <summary>
        /// 加工损耗
        /// </summary>
        public static string JGSHAO { get { return jgshao; } set { jgshao = value; } }
        /// <summary>
        /// 其它损耗
        /// </summary>
        public static string QTMONY { get { return qtmony; } set { qtmony = value; } }
        /// <summary>
        /// 倍率
        /// </summary>
        public static string BL { get { return bl; } set { bl = value; } }
    }
}
