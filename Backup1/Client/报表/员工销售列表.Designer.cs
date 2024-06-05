namespace Client.报表
{
    partial class 员工销售列表
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.phao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uSERDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SETTIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sLIANGDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sSALEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.员工销售统计BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clidata = new Client.clidata();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.员工销售统计BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clidata)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(749, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::Client.Properties.Resources.quit;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(65, 36);
            this.toolStripButton1.Text = "退出";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::Client.Properties.Resources.list;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(65, 36);
            this.toolStripButton2.Text = "详细";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::Client.Properties.Resources.Excel;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(65, 36);
            this.toolStripButton3.Text = "导出";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
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
            this.uSERDataGridViewTextBoxColumn,
            this.SETTIME,
            this.sLIANGDataGridViewTextBoxColumn,
            this.sSALEDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.员工销售统计BindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(0, 38);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(749, 545);
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
            // uSERDataGridViewTextBoxColumn
            // 
            this.uSERDataGridViewTextBoxColumn.DataPropertyName = "USER";
            this.uSERDataGridViewTextBoxColumn.HeaderText = "姓名";
            this.uSERDataGridViewTextBoxColumn.Name = "uSERDataGridViewTextBoxColumn";
            this.uSERDataGridViewTextBoxColumn.ReadOnly = true;
            this.uSERDataGridViewTextBoxColumn.Width = 120;
            // 
            // SETTIME
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SETTIME.DefaultCellStyle = dataGridViewCellStyle2;
            this.SETTIME.HeaderText = "日期";
            this.SETTIME.Name = "SETTIME";
            this.SETTIME.ReadOnly = true;
            this.SETTIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SETTIME.Width = 180;
            // 
            // sLIANGDataGridViewTextBoxColumn
            // 
            this.sLIANGDataGridViewTextBoxColumn.DataPropertyName = "SLIANG";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.sLIANGDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.sLIANGDataGridViewTextBoxColumn.HeaderText = "数量";
            this.sLIANGDataGridViewTextBoxColumn.Name = "sLIANGDataGridViewTextBoxColumn";
            this.sLIANGDataGridViewTextBoxColumn.ReadOnly = true;
            this.sLIANGDataGridViewTextBoxColumn.Width = 80;
            // 
            // sSALEDataGridViewTextBoxColumn
            // 
            this.sSALEDataGridViewTextBoxColumn.DataPropertyName = "SSALE";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.sSALEDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.sSALEDataGridViewTextBoxColumn.HeaderText = "销售额";
            this.sSALEDataGridViewTextBoxColumn.Name = "sSALEDataGridViewTextBoxColumn";
            this.sSALEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sSALEDataGridViewTextBoxColumn.Width = 160;
            // 
            // 员工销售统计BindingSource
            // 
            this.员工销售统计BindingSource.DataMember = "员工销售统计";
            this.员工销售统计BindingSource.DataSource = this.clidata;
            // 
            // clidata
            // 
            this.clidata.DataSetName = "clidata";
            this.clidata.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // 员工销售列表
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 582);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.MinimizeBox = false;
            this.Name = "员工销售列表";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.员工销售列表_FormClosed);
            this.Load += new System.EventHandler(this.员工销售列表_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.员工销售统计BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clidata)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource 员工销售统计BindingSource;
        private clidata clidata;
        private System.Windows.Forms.DataGridViewTextBoxColumn phao;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SETTIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLIANGDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sSALEDataGridViewTextBoxColumn;
    }
}