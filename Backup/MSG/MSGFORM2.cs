using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MSG
{
    partial class MSGFORM2 : Form
    {
        public MSGFORM2()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MSGFORM2_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.pictureBox2.Parent = this.pictureBox1;
            this.TopMost = true;
        }

        private void MSGFORM2_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}