namespace Client.报表
{
    partial class 款式销售分析详细
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sETTIMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kUSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBEIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sSALEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zKOUDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sLIANGDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kHUDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mDIANDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uSERDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.款式销售分析详细BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clidata = new Client.clidata();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.款式销售分析详细BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clidata)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(796, 39);
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
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeight = 28;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sETTIMEDataGridViewTextBoxColumn,
            this.tMDataGridViewTextBoxColumn,
            this.kUSDataGridViewTextBoxColumn,
            this.cBEIDataGridViewTextBoxColumn,
            this.sSALEDataGridViewTextBoxColumn,
            this.zKOUDataGridViewTextBoxColumn,
            this.sLIANGDataGridViewTextBoxColumn,
            this.kHUDataGridViewTextBoxColumn,
            this.mDIANDataGridViewTextBoxColumn,
            this.uSERDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.款式销售分析详细BindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(0, 38);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(796, 444);
            this.dataGridView1.TabIndex = 1;
            // 
            // sETTIMEDataGridViewTextBoxColumn
            // 
            this.sETTIMEDataGridViewTextBoxColumn.DataPropertyName = "SETTIME";
            this.sETTIMEDataGridViewTextBoxColumn.HeaderText = "销售日期";
            this.sETTIMEDataGridViewTextBoxColumn.Name = "sETTIMEDataGridViewTextBoxColumn";
            this.sETTIMEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sETTIMEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sETTIMEDataGridViewTextBoxColumn.Width = 120;
            // 
            // tMDataGridViewTextBoxColumn
            // 
            this.tMDataGridViewTextBoxColumn.DataPropertyName = "TM";
            this.tMDataGridViewTextBoxColumn.HeaderText = "条码";
            this.tMDataGridViewTextBoxColumn.Name = "tMDataGridViewTextBoxColumn";
            this.tMDataGridViewTextBoxColumn.ReadOnly = true;
            this.tMDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tMDataGridViewTextBoxColumn.Width = 80;
            // 
            // kUSDataGridViewTextBoxColumn
            // 
            this.kUSDataGridViewTextBoxColumn.DataPropertyName = "KUS";
            this.kUSDataGridViewTextBoxColumn.HeaderText = "款式";
            this.kUSDataGridViewTextBoxColumn.Name = "kUSDataGridViewTextBoxColumn";
            this.kUSDataGridViewTextBoxColumn.ReadOnly = true;
            this.kUSDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kUSDataGridViewTextBoxColumn.Width = 80;
            // 
            // cBEIDataGridViewTextBoxColumn
            // 
            this.cBEIDataGridViewTextBoxColumn.DataPropertyName = "CBEI";
            this.cBEIDataGridViewTextBoxColumn.HeaderText = "成本";
            this.cBEIDataGridViewTextBoxColumn.Name = "cBEIDataGridViewTextBoxColumn";
            this.cBEIDataGridViewTextBoxColumn.ReadOnly = true;
            this.cBEIDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cBEIDataGridViewTextBoxColumn.Width = 80;
            // 
            // sSALEDataGridViewTextBoxColumn
            // 
            this.sSALEDataGridViewTextBoxColumn.DataPropertyName = "SSALE";
            this.sSALEDataGridViewTextBoxColumn.HeaderText = "销售额";
            this.sSALEDataGridViewTextBoxColumn.Name = "sSALEDataGridViewTextBoxColumn";
            this.sSALEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sSALEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sSALEDataGridViewTextBoxColumn.Width = 120;
            // 
            // zKOUDataGridViewTextBoxColumn
            // 
            this.zKOUDataGridViewTextBoxColumn.DataPropertyName = "ZKOU";
            this.zKOUDataGridViewTextBoxColumn.HeaderText = "折扣";
            this.zKOUDataGridViewTextBoxColumn.Name = "zKOUDataGridViewTextBoxColumn";
            this.zKOUDataGridViewTextBoxColumn.ReadOnly = true;
            this.zKOUDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.zKOUDataGridViewTextBoxColumn.Width = 45;
            // 
            // sLIANGDataGridViewTextBoxColumn
            // 
            this.sLIANGDataGridViewTextBoxColumn.DataPropertyName = "SLIANG";
            this.sLIANGDataGridViewTextBoxColumn.HeaderText = "数量";
            this.sLIANGDataGridViewTextBoxColumn.Name = "sLIANGDataGridViewTextBoxColumn";
            this.sLIANGDataGridViewTextBoxColumn.ReadOnly = true;
            this.sLIANGDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sLIANGDataGridViewTextBoxColumn.Width = 60;
            // 
            // kHUDataGridViewTextBoxColumn
            // 
            this.kHUDataGridViewTextBoxColumn.DataPropertyName = "KHU";
            this.kHUDataGridViewTextBoxColumn.HeaderText = "客户";
            this.kHUDataGridViewTextBoxColumn.Name = "kHUDataGridViewTextBoxColumn";
            this.kHUDataGridViewTextBoxColumn.ReadOnly = true;
            this.kHUDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kHUDataGridViewTextBoxColumn.Width = 120;
            // 
            // mDIANDataGridViewTextBoxColumn
            // 
            this.mDIANDataGridViewTextBoxColumn.DataPropertyName = "MDIAN";
            this.mDIANDataGridViewTextBoxColumn.HeaderText = "门店";
            this.mDIANDataGridViewTextBoxColumn.Name = "mDIANDataGridViewTextBoxColumn";
            this.mDIANDataGridViewTextBoxColumn.ReadOnly = true;
            this.mDIANDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.mDIANDataGridViewTextBoxColumn.Width = 160;
            // 
            // uSERDataGridViewTextBoxColumn
            // 
            this.uSERDataGridViewTextBoxColumn.DataPropertyName = "USER";
            this.uSERDataGridViewTextBoxColumn.HeaderText = "员工";
            this.uSERDataGridViewTextBoxColumn.Name = "uSERDataGridViewTextBoxColumn";
            this.uSERDataGridViewTextBoxColumn.ReadOnly = true;
            this.uSERDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.uSERDataGridViewTextBoxColumn.Width = 120;
            // 
            // 款式销售分析详细BindingSource
            // 
            this.款式销售分析详细BindingSource.DataMember = "款式销售分析详细";
            this.款式销售分析详细BindingSource.DataSource = this.clidata;
            // 
            // clidata
            // 
            this.clidata.DataSetName = "clidata";
            this.clidata.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // 款式销售分析详细
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 481);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.MinimizeBox = false;
            this.Name = "款式销售分析详细";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.款式销售分析详细_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.款式销售分析详细BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clidata)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource 款式销售分析详细BindingSource;
        private clidata clidata;
        private System.Windows.Forms.DataGridViewTextBoxColumn sETTIMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kUSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBEIDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sSALEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zKOUDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLIANGDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kHUDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mDIANDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERDataGridViewTextBoxColumn;
    }
}