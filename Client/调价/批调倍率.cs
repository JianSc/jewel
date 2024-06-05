using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Client.调价
{
    public partial class 批调倍率 : Form
    {
        DataGridView panDGV;

        public 批调倍率(DataGridView dgv)
        {
            panDGV = dgv;
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"\d|\.");
            if (!regex.Match(e.KeyChar.ToString()).Success)
            {
                e.Handled = true;
            }
        }

        private void 批调倍率_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;


        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^\d+$|^\d+\.\d{1,2}$");
            if (!regex.Match(((TextBox)sender).Text).Success)
            {
                textBox1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1_Leave(textBox1, new EventArgs());
            if (textBox1.Text == string.Empty)
            {
                textBox1.BackColor = Color.MistyRose;
                textBox1.Focus();
                return;
            }

            string blu = textBox1.Text.Trim();

            int j = 0;
            foreach (DataGridViewRow i in panDGV.Rows)
            {
                i.Cells["bLUDataGridViewTextBoxColumn"].Value = blu;

                调销售价.SaleNUM(panDGV, j, false);

                j++;
            }

            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }
    }
}