namespace Mdian.报表
{
    partial class 店员销售统计详细
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.phao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sETTIMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uSERDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sLIANGDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sSALEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看详细销售记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.销售统计表BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.theDST = new Mdian.theDST();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.销售统计表BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.theDST)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.toolStripButton3,
            this.toolStripSeparator1,
            this.toolStripButton5,
            this.toolStripSeparator2,
            this.toolStripButton4,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(898, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::Mdian.Properties.Resources.quit;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(65, 36);
            this.toolStripButton1.Text = "退出";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::Mdian.Properties.Resources.Excel;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(65, 36);
            this.toolStripButton3.Text = "导出";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = global::Mdian.Properties.Resources.print;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(65, 36);
            this.toolStripButton4.Text = "打印";
            this.toolStripButton4.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            this.toolStripSeparator2.Visible = false;
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = global::Mdian.Properties.Resources.list;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(89, 36);
            this.toolStripButton5.Text = "查看详细";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = global::Mdian.Properties.Resources.ImgLook;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(89, 36);
            this.toolStripButton2.Text = "查看图片";
            this.toolStripButton2.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeight = 26;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.phao,
            this.sETTIMEDataGridViewTextBoxColumn,
            this.uSERDataGridViewTextBoxColumn,
            this.sLIANGDataGridViewTextBoxColumn,
            this.sSALEDataGridViewTextBoxColumn});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.DataSource = this.销售统计表BindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(0, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(898, 537);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // phao
            // 
            this.phao.HeaderText = "编号";
            this.phao.Name = "phao";
            this.phao.ReadOnly = true;
            this.phao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.phao.Width = 35;
            // 
            // sETTIMEDataGridViewTextBoxColumn
            // 
            this.sETTIMEDataGridViewTextBoxColumn.DataPropertyName = "SETTIME";
            this.sETTIMEDataGridViewTextBoxColumn.HeaderText = "日期";
            this.sETTIMEDataGridViewTextBoxColumn.Name = "sETTIMEDataGridViewTextBoxColumn";
            this.sETTIMEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sETTIMEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sETTIMEDataGridViewTextBoxColumn.Width = 200;
            // 
            // uSERDataGridViewTextBoxColumn
            // 
            this.uSERDataGridViewTextBoxColumn.DataPropertyName = "USER";
            this.uSERDataGridViewTextBoxColumn.HeaderText = "员工姓名";
            this.uSERDataGridViewTextBoxColumn.Name = "uSERDataGridViewTextBoxColumn";
            this.uSERDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sLIANGDataGridViewTextBoxColumn
            // 
            this.sLIANGDataGridViewTextBoxColumn.DataPropertyName = "SLIANG";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.sLIANGDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.sLIANGDataGridViewTextBoxColumn.HeaderText = "销售数量";
            this.sLIANGDataGridViewTextBoxColumn.Name = "sLIANGDataGridViewTextBoxColumn";
            this.sLIANGDataGridViewTextBoxColumn.ReadOnly = true;
            this.sLIANGDataGridViewTextBoxColumn.Width = 200;
            // 
            // sSALEDataGridViewTextBoxColumn
            // 
            this.sSALEDataGridViewTextBoxColumn.DataPropertyName = "SSALE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.sSALEDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.sSALEDataGridViewTextBoxColumn.HeaderText = "销售金额";
            this.sSALEDataGridViewTextBoxColumn.Name = "sSALEDataGridViewTextBoxColumn";
            this.sSALEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sSALEDataGridViewTextBoxColumn.Width = 200;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看详细销售记录ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(167, 54);
            // 
            // 查看详细销售记录ToolStripMenuItem
            // 
            this.查看详细销售记录ToolStripMenuItem.Name = "查看详细销售记录ToolStripMenuItem";
            this.查看详细销售记录ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.查看详细销售记录ToolStripMenuItem.Text = "查看详细销售记录";
            this.查看详细销售记录ToolStripMenuItem.Click += new System.EventHandler(this.查看详细销售记录ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 销售统计表BindingSource
            // 
            this.销售统计表BindingSource.DataMember = "销售统计表";
            this.销售统计表BindingSource.DataSource = this.theDST;
            // 
            // theDST
            // 
            this.theDST.DataSetName = "theDST";
            this.theDST.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // 店员销售统计详细
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 573);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.MinimizeBox = false;
            this.Name = "店员销售统计详细";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.店员销售统计详细_FormClosed);
            this.Load += new System.EventHandler(this.店员销售统计详细_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.销售统计表BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.theDST)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.BindingSource 销售统计表BindingSource;
        private theDST theDST;
        private System.Windows.Forms.DataGridViewTextBoxColumn phao;
        private System.Windows.Forms.DataGridViewTextBoxColumn sETTIMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLIANGDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sSALEDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查看详细销售记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;

    }
}