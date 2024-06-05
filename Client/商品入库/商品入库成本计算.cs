using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Client.商品入库
{
    public partial class 商品入库成本计算 : Form
    {
        TextBox txb, xsb, blb;
        double zzszl, ffszl, jzlist;
        /// <summary>
        /// 计算窗口
        /// </summary>
        /// <param name="tx">成本 textbox</param>
        /// <param name="fszl">辅石重量</param>
        /// <param name="zszl">主石重量</param>
        /// <param name="jzlists">金重</param>
        /// <param name="xs">销售价 textbox</param>
        /// <param name="bl">倍率</param>
        public 商品入库成本计算(TextBox tx,string fszl,string zszl,string jzlists,TextBox xs,TextBox bls)
        {
            txb = tx;
            xsb = xs;
            blb = bls;
            zzszl = double.Parse(zszl);
            ffszl = double.Parse(fszl);
            jzlist = double.Parse(jzlists);
            InitializeComponent();
        }

        private void 商品入库成本计算_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            textBox8.Text = JJItem.JJIA;
            //textBox1.Text = JJItem.ZSJIA;
            //textBox7.Text = JJItem.ZSMONY;
            //textBox5.Text = JJItem.FSJIA;
            //textBox2.Text = JJItem.FSMONY;
            textBox3.Text = JJItem.JGMONY;
            textBox6.Text = JJItem.JGSHAO;
            textBox4.Text = JJItem.QTMONY;
            textBox9.Text = JJItem.BL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox8.Text = JJItem.JJIA;
            //textBox1.Text = JJItem.ZSJIA;
            //textBox7.Text = JJItem.ZSMONY;
            //textBox5.Text = JJItem.FSJIA;
            //textBox2.Text = JJItem.FSMONY;
            //textBox3.Text = JJItem.JGMONY;
            //textBox6.Text = JJItem.JGSHAO;
            //textBox4.Text = JJItem.QTMONY;
            //textBox9.Text = JJItem.BL;

            double jjia, zsjia, zsmony, fsjia, fsmony, jgmony, jgshao, qtmony, bl, pjj;
            try { jjia = double.Parse(textBox8.Text); }
            catch { textBox8.Text = "0"; textBox8.SelectAll(); textBox8.Focus(); return; }

            try { zsjia = double.Parse(textBox1.Text); }
            catch { textBox1.Text = "0"; textBox1.SelectAll(); textBox1.Focus(); return; }

            try { zsmony = double.Parse(textBox7.Text); }
            catch { textBox7.Text = "0"; textBox7.SelectAll(); textBox7.Focus(); return; }

            try { fsjia = double.Parse(textBox5.Text); }
            catch { textBox5.Text = "0"; textBox5.SelectAll(); textBox5.Focus(); return; }

            try { fsmony = double.Parse(textBox2.Text); }
            catch { textBox2.Text = "0"; textBox2.SelectAll(); textBox2.Focus(); return; }

            try { jgmony = double.Parse(textBox3.Text); }
            catch { textBox3.Text = "0"; textBox3.SelectAll(); textBox3.Focus(); return; }

            try { jgshao = double.Parse(textBox6.Text); }
            catch { textBox6.Text = "0"; textBox6.SelectAll(); textBox6.Focus(); return; }

            try { qtmony = double.Parse(textBox4.Text); }
            catch { textBox4.Text = "0"; textBox4.SelectAll(); textBox4.Focus(); return; }

            try { bl = double.Parse(textBox9.Text); }
            catch { textBox9.Text = "0"; textBox9.SelectAll(); textBox9.Focus(); return; }

            try { pjj = double.Parse(textBox10.Text); }
            catch { textBox10.Text = "0"; textBox10.SelectAll(); textBox10.Focus(); return; }

            //double jjia, zsjia, zsmony, fsjia, fsmony, jgmony, jgshao, qtmony, bl;

            double itemint;
            itemint = (jjia * jzlist) + zsmony + fsmony + jgmony + jgshao + qtmony + pjj;
            int a = (int)(itemint * 100);
            itemint = a / 100.00;
            txb.Text = itemint.ToString();
            double itemint2;
            itemint2 = ((jjia * jzlist) + zsmony + fsmony + jgmony + jgshao + qtmony + pjj) * bl;
            int b = (int)(itemint2 * 100);
            itemint2 = b / 100.00;
            xsb.Text = itemint2.ToString();
            //blb.Text = bl.ToString();

            JJItem.JJIA = jjia.ToString();
            JJItem.ZSJIA = zsjia.ToString();
            JJItem.ZSMONY = zsmony.ToString();
            JJItem.FSJIA = fsjia.ToString();
            JJItem.FSMONY = fsmony.ToString();
            JJItem.JGMONY = jgmony.ToString();
            JJItem.JGSHAO = jgshao.ToString();
            JJItem.QTMONY = qtmony.ToString();
            JJItem.BL = bl.ToString();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            double fsj;
            try { fsj = double.Parse(textBox5.Text); }
            catch { textBox5.Text = "0"; textBox5.SelectAll(); textBox5.Focus(); return; }
            double itemstr = ffszl * fsj;
            textBox2.Text = itemstr.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double fsj;
            try { fsj = double.Parse(textBox1.Text); }
            catch { textBox1.Text = "0"; textBox1.SelectAll(); textBox1.Focus(); return; }
            double itemstr = zzszl * fsj;
            textBox7.Text = itemstr.ToString();
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.MistyRose;
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
            //Regex regex = new Regex("^[0-9]\\.[0-9]{1,3}
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            Regex regex = new Regex(@"^\d$|\.$");
            if (key == 13)
            {
                SendKeys.Send("{Tab}");
            }
            else if (key == 8)
            {
                e.Handled = false;
            }
            else
            {
                if (!regex.Match(e.KeyChar.ToString()).Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            blb.Text = ((TextBox)sender).Text;
        }

    }
}