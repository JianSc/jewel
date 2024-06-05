using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MSG
{
    partial class MSGFORM1 : Form
    {
        public MSGFORM1()
        {
            InitializeComponent();
        }

        private static bool forclo = false;
        public static bool qz_forclo { get { return forclo; } }
        private static string msgstr;
        public static string qz_msgstr { set { msgstr = value; } }

        private void MSGFORM1_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            forclo = false;
            checkBox1.Checked = false;
            button1.Enabled = false;

            if (msgstr != null)
            {
                label2.Text = msgstr;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            forclo = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}