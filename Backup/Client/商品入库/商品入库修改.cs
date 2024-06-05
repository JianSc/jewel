using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Client.商品入库
{
    public partial class 商品入库修改 : Form
    {
        DataTable panDT;
        string panTM;
        DataGridView panDGV;
        DataTable dstJL = xconfig.DST.Tables["金料"];
        DataTable dstSL = xconfig.DST.Tables["石料"];
        DataTable dstSSI = xconfig.DST.Tables["首饰"];
        DataTable dstXZ = xconfig.DST.Tables["形状"];
        //DataTable dstSTONG = xconfig.DST.Tables["stong"];
        DataTable dstJD = xconfig.DST.Tables["净度"];
        DataTable dstQG = xconfig.DST.Tables["切工"];
        DataTable dstDW = xconfig.DST.Tables["单位"];
        DataTable dstYS = xconfig.DST.Tables["颜色"];
        public 商品入库修改(DataTable dt,DataGridView dgv,string tm)
        {
            panDT = dt;
            panTM = tm;
            panDGV = dgv;

            string constr, cmdstr;
            constr = xconfig.CONNSTR;
            cmdstr = "select rtrim(name) as name from qJLIAO";
            config.conData.fill("sql", constr, cmdstr, dstJL);
            cmdstr = "select rtrim(name) as name from qSLIAO";
            config.conData.fill("sql", constr, cmdstr, dstSL);
            cmdstr = "select rtrim(name) as name from qSSHI";
            config.conData.fill("sql", constr, cmdstr, dstSSI);
            cmdstr = "select rtrim(name) as name from qXZUANG";
            config.conData.fill("sql", constr, cmdstr, dstXZ);
            cmdstr = "select rtrim(name) as name from qJDU";
            config.conData.fill("sql", constr, cmdstr, dstJD);
            cmdstr = "select rtrim(name) as name from qQIG";
            config.conData.fill("sql", constr, cmdstr, dstQG);
            cmdstr = "select rtrim(name) as name from qDWEI";
            config.conData.fill("sql", constr, cmdstr, dstDW);
            cmdstr = "select rtrim(name) as name from qYSE";
            config.conData.fill("sql", constr, cmdstr, dstYS);
            //dstSTONG.Rows.Clear();
            InitializeComponent();
        }

        private void 商品入库修改_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            foreach (DataRow i in dstJL.Rows)
            {
                jlbox.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstSL.Rows)
            {
                slbox.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstSSI.Rows)
            {
                ssibox.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstXZ.Rows)
            {
                xzbox.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstJD.Rows)
            {
                jdbox.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstQG.Rows)
            {
                qgbox.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstDW.Rows)
            {
                dweibox.Items.Add(i["name"].ToString());
            }
            foreach (DataRow i in dstYS.Rows)
            {
                ysebox.Items.Add(i["name"].ToString());
            }

            if (panTM == "")
            {
                toolStripButton1.Enabled = false;
                MessageBox.Show("未知错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow[] dr = panDT.Select("tm='" + panTM + "'", "");
            if (dr.Length < 1)
            {
                toolStripButton1.Enabled = false;
                MessageBox.Show("未知错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tmbox.Text = panTM;
            jlbox.Text = dr[0]["jliao"].ToString();
            slbox.Text = dr[0]["sliao"].ToString();
            ssibox.Text = dr[0]["ssi"].ToString();
            qkbox.Text = dr[0]["qkou"].ToString();
            zsbox.Text = dr[0]["zshu"].ToString();
            ksbox.Text = dr[0]["kus"].ToString();
            hhbox.Text = dr[0]["hhao"].ToString();
            dweibox.Text = dr[0]["dwei"].ToString();
            sliangbox.Text = dr[0]["sliang"].ToString();
            ysebox.Text = dr[0]["yse"].ToString();
            xzbox.Text = dr[0]["xzuang"].ToString();
            qgbox.Text = dr[0]["qgong"].ToString();
            jdbox.Text = dr[0]["jdu"].ToString();
            jianzbox.Text = dr[0]["jianz"].ToString();
            jinzbox.Text = dr[0]["jinz"].ToString();
            pjzbox.Text = dr[0]["pjianz"].ToString();
            xsblbox.Text = dr[0]["blu"].ToString();
            zsibox.Text = dr[0]["zshi"].ToString();
            fsibox.Text = dr[0]["fshi"].ToString();
            cbeibox.Text = dr[0]["cbei"].ToString();
            xsbox.Text = dr[0]["xsou"].ToString();
            bzbox.Text = dr[0]["bzhu"].ToString();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            string box = sender.GetType().Name;
            if (box == "TextBox")
            {
                ((TextBox)sender).BackColor = Color.PaleGoldenrod;
            }
            else if (box == "ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.PaleGoldenrod;
            }
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            string box = sender.GetType().Name;
            if (box == "TextBox")
            {
                ((TextBox)sender).BackColor = Color.White;
            }
            else if (box == "ComboBox")
            {
                ((ComboBox)sender).BackColor = Color.White;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = xconfig.ASC(e.KeyChar.ToString());
            if (key == 13)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void textBox1_Enter_1(object sender, EventArgs e)
        {
            jlbox.Focus();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}