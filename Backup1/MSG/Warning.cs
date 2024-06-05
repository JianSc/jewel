using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MSG
{
    public class Warning
    {
        static Form newform = new MSGFORM1();
        public static bool Show(string msgstr)
        {
            MSGFORM1.qz_msgstr = msgstr;
            newform.ShowDialog();
            return MSGFORM1.qz_forclo;
        }
        public static bool Show()
        {
            newform.ShowDialog();
            return MSGFORM1.qz_forclo;
        }
        
    }
}
