using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Threading;

namespace Client.商品入库
{
    public partial class 数据导入 : Form
    {
        DataTable panDST;
        DataGridView panDGV;
        DataTable itemDST;
        DataTable fxDST;
        public 数据导入(DataTable dst,DataGridView dgv)
        {
            panDST = dst;
            panDGV = dgv;
            itemDST = new clidata().Tables["金汇分销单"];
            fxDST = new clidata().Tables["分销暂存"];
            InitializeComponent();
        }

        private void 数据导入_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            MSG.login.Show();
            Application.DoEvents();

            Thread t = new Thread(new ThreadStart(showdatafill));
            t.Start();
        }

        private delegate void t_del();

        private void showdatafill()
        {
            DateTime time1 = DateTime.Parse(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 23:59:59");
            DateTime time2 = DateTime.Parse(DateTime.Now.AddMonths(-5).ToShortDateString() + " 00:00:00");

            try
            {
                SqlConnection conn = new SqlConnection(xconfig.JHCONNSTR);
                SqlDataAdapter dars = new SqlDataAdapter("SELECT SUM(quan) AS quan,[no],date,site,operator "
                    + "FROM (SELECT QUAN,[NO],CONVERT(CHAR(10),DATE,120) AS DATE,SITE,OPERATOR FROM GOODS_O WHERE ([date] BETWEEN '" + time2 + "' AND '" + time1 + "')) DERIVERDTBL "
                    + "GROUP BY [no],[date],site,operator ORDER BY [DATE] DESC", conn);
                itemDST.Clear();
                dars.Fill(itemDST);
                conn.Close();

                t_del dd = delegate
                {
                    dataGridView1.DataSource = itemDST;
                };
                dataGridView1.Invoke(dd);

                t_del dll = delegate
                {
                    config.conDGV.DGVAutoID(dataGridView1, "phao");
                };
                dataGridView1.Invoke(dll);

                SqlConnection conn1 = new SqlConnection(xconfig.CONNSTR);
                conn1.Open();
                SqlDataAdapter da1 = new SqlDataAdapter("SELECT RTRIM([NAME]) AS [NAME],RTRIM([USER]) AS [USER] FROM QRKDH_LOG", conn1);
                fxDST.Clear();
                da1.Fill(fxDST);
                conn1.Close();

                foreach (DataGridViewRow i in dataGridView1.Rows)
                {
                    string rkdh;
                    rkdh = i.Cells["noDataGridViewTextBoxColumn"].Value.ToString();
                    DataRow[] dr = fxDST.Select("[NAME]='" + rkdh + "'", "");
                    if (dr.Length > 0)
                    {
                        ((DataGridViewCheckBoxCell)i.Cells["check"]).Value = true;
                        i.DefaultCellStyle.BackColor = Color.Red;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MSG.login.Close();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            config.conDGV.DGVAutoID(dataGridView1, "phao");
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            int i = dataGridView1.Rows.Count;
            if (i < 14)
            {
                pictureBox1.Visible = true;
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int i = dataGridView1.Rows.Count;
            if (i > 13)
            {
                pictureBox1.Visible = false;
            }
        }

        private void 数据导入_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                return;
            }

            MSG.login.Show();
            Application.DoEvents();

            toolStripButton1.Enabled = false;
            int datasum = 0;
            string FDH;
            FDH = dataGridView1["noDataGridViewTextBoxColumn", dataGridView1.CurrentCell.RowIndex].Value.ToString().Trim();
            
            try
            {
                OleDbConnection connitem = new OleDbConnection(xconfig.oldstr("goods", "cnsdjian"));
                connitem.Open();
                OleDbCommand cmditem = new OleDbCommand("select count(id) as id from goods", connitem);
                OleDbDataReader rsitem = cmditem.ExecuteReader();
                rsitem.Read();
                datasum = int.Parse(rsitem["id"].ToString().Trim());
                connitem.Close();
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
                toolStripButton1.Enabled = true;
            }

            if (datasum > 0)
            {
                MSG.login.Close();
                if (MessageBox.Show("数据缓存中已存在数据！\n是否清空？\n\n选择[是]将清空原有数据，选择[否]将在原有数据后导入。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    try
                    {
                        MSG.login.Show();
                        Application.DoEvents();
                        OleDbConnection connitem = new OleDbConnection(xconfig.oldstr("goods", "cnsdjian"));
                        connitem.Open();
                        OleDbCommand cmditem = new OleDbCommand("delete from goods", connitem);
                        cmditem.ExecuteNonQuery();
                        connitem.Close();
                    }
                    catch (Exception ex)
                    {
                        MSG.login.Close();
                        MessageBox.Show(ex.Message);
                        toolStripButton1.Enabled = true;
                    }
                }
            }

            try
            {
                SqlConnection sqlconn = new SqlConnection(xconfig.JHCONNSTR);
                sqlconn.Open();
                SqlCommand sqlcmd = new SqlCommand("select "
                    + "goods.id as tm,"
                    + "goods.name_o as jliao,"
                    + "goods.name_g as sliao,"
                    + "goods.name_c as ssi,"
                    + "goods.type_id as kus,"
                    + "goods.unit as dwei,"
                    + "goods.quan as sliang,"
                    + "goods.cost as cbei,"
                    + "goods.amount as xsou,"
                    + "goods.book_id as zshu,"
                    +"goods.goods_no as hhao,goods.weight as jianz,goods.weight_g as jinz,goods.weight_l as pjianz,"
                    +"goods.shape as xzuang,goods.color as yse,goods.clean as jdu,goods.process as qgong,goods.rate as blu,goods.remarks as bzhu,"
                    + "goods.stone_m,goods.stone_a,"
                    + "goods.[size] as qkou"
                    + " FROM goods INNER JOIN"
                    + " goods_o ON goods.id=goods_o.id"
                    + " WHERE (goods_o.[no] = '" + FDH + "')", sqlconn);
                SqlDataReader sqlrs = sqlcmd.ExecuteReader();

                string tm, jliao, sliao, ssi, kus, dwei, sliang, qkou, stongmz, stongms, stongaz, stongas;
                string cbei, xsou;
                string[] item1,item2;
                string zshu, hhao, jianz, jinz, pjianz, xzuang, yse, jdu, qgong, blu, bzhu;
                string jja, pjja, zsja, zsje, fsja, fsje, jgdj, other, jgsh;
                jja = "0";
                pjja = "0";
                zsja="0";
                zsje="0";
                fsja="0";
                fsje="0";
                jgdj="0";
                other="0";
                jgsh="0";

                OleDbConnection conn = new OleDbConnection(xconfig.oldstr("goods", "cnsdjian"));
                conn.Open();
                OleDbCommand cmd;
                SqlConnection sqlconnitem = new SqlConnection(xconfig.JHCONNSTR);
                sqlconnitem.Open();
                SqlCommand sqlcmditem;
                while (sqlrs.Read())
                { 
                    tm = sqlrs["tm"].ToString().Trim();
                    sqlcmditem = new SqlCommand("SELECT PRICE_G,PRICE_L,PRICE_M,AMT_M,PRICE_A,AMT_A,FEE,OTHER,DISRATE FROM GOODS_I WHERE (ID='" + tm + "')", sqlconnitem);
                    SqlDataReader sqlrsitem = sqlcmditem.ExecuteReader();
                    if (sqlrsitem.Read())
                    {
                        jja = sqlrsitem["PRICE_G"].ToString();
                        pjja = sqlrsitem["PRICE_L"].ToString();
                        zsja = sqlrsitem["PRICE_M"].ToString();
                        zsje = sqlrsitem["AMT_M"].ToString();
                        fsja = sqlrsitem["PRICE_A"].ToString();
                        fsje = sqlrsitem["AMT_A"].ToString();
                        jgdj = sqlrsitem["FEE"].ToString();
                        other = sqlrsitem["OTHER"].ToString();
                        jgsh = sqlrsitem["DISRATE"].ToString();
                    }
                    sqlrsitem.Close();
                    
                    jliao = sqlrs["jliao"].ToString().Trim();
                    sliao = sqlrs["sliao"].ToString().Trim();
                    zshu = sqlrs["zshu"].ToString().Trim();
                    ssi = sqlrs["ssi"].ToString().Trim();
                    kus = sqlrs["kus"].ToString().Trim();
                    dwei = sqlrs["dwei"].ToString().Trim();
                    sliang = sqlrs["sliang"].ToString().Trim();
                    hhao = sqlrs["hhao"].ToString().Trim();
                    jianz = sqlrs["jianz"].ToString().Trim();
                    jinz = sqlrs["jinz"].ToString().Trim();
                    pjianz = sqlrs["pjianz"].ToString().Trim();
                    xzuang = sqlrs["xzuang"].ToString().Trim();
                    yse = sqlrs["yse"].ToString().Trim();
                    jdu = sqlrs["jdu"].ToString().Trim();
                    qgong = sqlrs["qgong"].ToString().Trim();
                    blu = sqlrs["blu"].ToString().Trim();
                    bzhu = sqlrs["bzhu"].ToString().Trim();
                    if (sqlrs["qkou"].ToString().Trim() == "") { qkou = "0"; }
                    else { qkou = sqlrs["qkou"].ToString().Trim(); }
                    qkou = qkou.Replace("#", "");
                    cbei = sqlrs["cbei"].ToString().Trim();
                    xsou = sqlrs["xsou"].ToString().Trim();
                    string item11 = "0/0";
                    string item22 = "0/0";
                    if (sqlrs["stone_m"].ToString().Trim() == "") { item1 = item11.Split(new char[] { '/' }); }
                    else { item1 = sqlrs["stone_m"].ToString().Split(new char[] { '/' }); }
                    if (sqlrs["stone_a"].ToString().Trim() == "") { item2 = item22.Split(new char[] { '/' }); }
                    else { item2 = sqlrs["stone_a"].ToString().Split(new char[] { '/' }); }
                    stongmz = item1[0];
                    if (stongmz.Trim() == "") { stongmz = "0"; }
                    stongms = item1[1];
                    if (stongms.Trim() == "") { stongms = "1"; }
                    stongaz = item2[0];
                    if (stongaz.Trim() == "") { stongaz = "0"; }
                    stongas = item2[1];
                    if (stongas.Trim() == "") { stongas = "1"; }
                    if (qkou == "") { qkou = "0"; }
                    cmd = new OleDbCommand("insert into goods(tm,jliao,sliao,ssi,kus,dwei,sliang,qkou,zsz,zss,fsz,fss,cbei,xsou,zshu,hhao,jianz,jinz,pjianz,xzuang,yse,jdu,qgong,blu,bzhu,jja,pjja,zsja,zsje,fsja,fsje,jgdj,other,jgsh)values('"
                        + tm + "','"
                        + jliao + "','"
                        + sliao + "','"
                        + ssi + "','"
                        + kus + "','"
                        + dwei + "','"
                        + sliang + "','"
                        + qkou + "','"
                        + stongmz + "','"
                        + stongms + "','"
                        + stongaz + "','"
                        + stongas + "',"
                        + cbei + ","
                        + xsou + ",'"
                        + zshu + "','"
                        + hhao + "',"
                        + jianz + ","
                        + jinz + ","
                        + pjianz + ",'"
                        + xzuang + "','"
                        + yse + "','"
                        + jdu + "','"
                        + qgong + "',"
                        + blu + ",'"
                        + bzhu + "',"
                        + jja + ","
                        + pjja + ","
                        + zsja + ","
                        + zsje + ","
                        + fsja + ","
                        + fsje + ","
                        + jgdj + ","
                        + other + ","
                        + jgsh
                        + ")", conn);
                    cmd.ExecuteNonQuery();
                }
                sqlrs.Close();
                conn.Close();
                sqlconnitem.Close();

                OleDbDataAdapter dars = new OleDbDataAdapter("select id,rtrim(tm) as tm,"
                    + "rtrim(jliao) as jliao,rtrim(sliao) as sliao,rtrim(ssi) as ssi,"
                    + "qkou,rtrim(zshu) as zshu,rtrim(kus) as kus,rtrim(hhao) as hhao,"
                    + "rtrim(dwei) as dwei,sliang,jianz,jinz,pjianz,blu,zsz,zss,fsz,fss,"
                    + "cbei,xsou,rtrim(bzhu) as bzhu,slbol,imgbol,jja,pjja,zsja,zsje,fsja,fsje,jgdj,other,jgsh from goods order by id desc", conn);
                panDST.Clear();
                dars.Fill(panDST);
                foreach (DataRow i in panDST.Rows)
                {
                    if (i["qkou"].ToString() == "0")
                    {
                        i["qkou"] = "";
                    }
                }
                config.conDGV.DGVAutoID(panDGV, "phao");
                sqlconn.Close();

                SqlConnection rkdhconn = new SqlConnection(xconfig.CONNSTR);
                rkdhconn.Open();
                SqlCommand rkdhcmd = new SqlCommand("QZ_RKDH_LOG", rkdhconn);
                rkdhcmd.CommandType = CommandType.StoredProcedure;
                rkdhcmd.Parameters.Add(new SqlParameter("@NAME", FDH));
                rkdhcmd.Parameters.Add(new SqlParameter("@USER", xconfig.USER));
                rkdhcmd.ExecuteNonQuery();
                rkdhconn.Close();

                MSG.login.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MSG.login.Close();
                MessageBox.Show(ex.Message);
                toolStripButton1.Enabled = true;
            }

            MSG.login.Close();
        }
    }
}