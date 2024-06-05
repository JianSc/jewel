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
    public partial class 调金价 : Form
    {
        DataGridView panDGV;

        public 调金价(DataGridView dgv)
        {
            panDGV = dgv;
            InitializeComponent();
        }

        private void 调金价_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1_Leave(textBox1, new EventArgs());
            if (textBox1.Text == "")
            {
                return;
            }

            string jja = textBox1.Text;

            try
            {
                int j = 0;
                foreach (DataGridViewRow i in panDGV.Rows)
                {
                    i.Cells["jJADataGridViewTextBoxColumn"].Value = jja;
                    调成本价.CbeiNUM(panDGV, j);
                    j++;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                    
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex(@"\d|\.");
            if (!regex.Match(e.KeyChar.ToString()).Success)
            {
                e.Handled = true;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^\d+$|^\d+\.\d{1,2}$");
            if (!regex.Match(textBox1.Text).Success)
            {
                textBox1.Text = "";
                textBox1.Focus();
                return;
            }
        }
    }
}