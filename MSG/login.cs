using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MSG
{
    public class login
    {
        private static Form newForm = new MSGFORM();
        private static Form newForm2 = new MSGFORM2();

        public static void Show(string msg)
        {
            MSGFORM.msgtxt = msg;
            if (newForm.InvokeRequired)
            {
                dg d = delegate
                {
                    newForm.Visible = true;
                };
                newForm.Invoke(d);
            }
            else
            {
                newForm.Visible = true;
            }
            //Application.DoEvents();
        }

        public static void Show()
        {
            string msgstr = "请稍候，正在加载.....";
            MSGFORM.msgtxt = msgstr;
            if (newForm.InvokeRequired)
            {
                dg d = delegate
                {
                    newForm.Visible = true;
                };
                newForm.Invoke(d);
            }
            else
            {
                newForm.Visible = true;
            }
            //Application.DoEvents();
        }

        private delegate void dg();

        public static void Close()
        {
            if (newForm.InvokeRequired)
            {
                dg d = delegate
                {
                    newForm.Visible = false;
                };
                newForm.Invoke(d);
            }
            else
            {
                newForm.Visible = false;
            }
            //Application.DoEvents();
        }

        public static void ShowIMG()
        {
            if (newForm2.InvokeRequired)
            {
                dg d = delegate
                {
                    newForm2.Visible = true;
                };
                newForm2.Invoke(d);
            }
            else
            {
                newForm2.Visible = true;
            }
        }

        public static void CloseIMG()
        {
            if (newForm2.InvokeRequired)
            {
                dg d = delegate
                {
                    newForm2.Visible = false;
                };
                newForm2.Invoke(d);
            }
            else
            {
                newForm2.Visible = false;
            }
        }
    }
}
