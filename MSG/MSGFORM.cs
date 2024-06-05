using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MSG
{
    partial class MSGFORM : Form
    {
        public MSGFORM()
        {
            InitializeComponent();
        }

        private static string _msgtxt;
        public static string msgtxt { set { _msgtxt = value; } }

        private void MSGFORM_Load(object sender, EventArgs e)
        {
            this.label1.Text = _msgtxt;
        }

        private void MSGFORM_VisibleChanged(object sender, EventArgs e)
        {
            this.label1.Text = _msgtxt;
        }
    }
}