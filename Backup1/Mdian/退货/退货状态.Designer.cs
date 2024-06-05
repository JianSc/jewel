namespace Mdian.退货
{
    partial class 退货状态
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.门店退货BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.theDST = new Mdian.theDST();
            this.sETTIMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sLIANGDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uSERDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uSERSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nTXTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mDIANDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sBMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.门店退货BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.theDST)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 351);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(655, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(655, 39);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::Mdian.Properties.Resources.list;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(89, 36);
            this.toolStripButton1.Text = "退单详细";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::Mdian.Properties.Resources.quit;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(65, 36);
            this.toolStripButton2.Text = "退出";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeight = 26;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sETTIMEDataGridViewTextBoxColumn,
            this.dHDataGridViewTextBoxColumn,
            this.tMDataGridViewTextBoxColumn,
            this.sLIANGDataGridViewTextBoxColumn,
            this.uSERDataGridViewTextBoxColumn,
            this.uSERSDataGridViewTextBoxColumn,
            this.nTXTDataGridViewTextBoxColumn,
            this.mDIANDataGridViewTextBoxColumn,
            this.sBMDataGridViewTextBoxColumn,
            this.iDDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.门店退货BindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(0, 38);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(655, 313);
            this.dataGridView1.TabIndex = 2;
            // 
            // 门店退货BindingSource
            // 
            this.门店退货BindingSource.DataMember = "门店退货";
            this.门店退货BindingSource.DataSource = this.theDST;
            // 
            // theDST
            // 
            this.theDST.DataSetName = "theDST";
            this.theDST.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sETTIMEDataGridViewTextBoxColumn
            // 
            this.sETTIMEDataGridViewTextBoxColumn.DataPropertyName = "SETTIME";
            this.sETTIMEDataGridViewTextBoxColumn.HeaderText = "日期";
            this.sETTIMEDataGridViewTextBoxColumn.Name = "sETTIMEDataGridViewTextBoxColumn";
            this.sETTIMEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sETTIMEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dHDataGridViewTextBoxColumn
            // 
            this.dHDataGridViewTextBoxColumn.DataPropertyName = "DH";
            this.dHDataGridViewTextBoxColumn.HeaderText = "单号";
            this.dHDataGridViewTextBoxColumn.Name = "dHDataGridViewTextBoxColumn";
            this.dHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tMDataGridViewTextBoxColumn
            // 
            this.tMDataGridViewTextBoxColumn.DataPropertyName = "TM";
            this.tMDataGridViewTextBoxColumn.HeaderText = "条码";
            this.tMDataGridViewTextBoxColumn.Name = "tMDataGridViewTextBoxColumn";
            this.tMDataGridViewTextBoxColumn.ReadOnly = true;
            this.tMDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tMDataGridViewTextBoxColumn.Visible = false;
            this.tMDataGridViewTextBoxColumn.Width = 60;
            // 
            // sLIANGDataGridViewTextBoxColumn
            // 
            this.sLIANGDataGridViewTextBoxColumn.DataPropertyName = "SLIANG";
            this.sLIANGDataGridViewTextBoxColumn.HeaderText = "数量";
            this.sLIANGDataGridViewTextBoxColumn.Name = "sLIANGDataGridViewTextBoxColumn";
            this.sLIANGDataGridViewTextBoxColumn.ReadOnly = true;
            this.sLIANGDataGridViewTextBoxColumn.Width = 45;
            // 
            // uSERDataGridViewTextBoxColumn
            // 
            this.uSERDataGridViewTextBoxColumn.DataPropertyName = "USER";
            this.uSERDataGridViewTextBoxColumn.HeaderText = "退货填单";
            this.uSERDataGridViewTextBoxColumn.Name = "uSERDataGridViewTextBoxColumn";
            this.uSERDataGridViewTextBoxColumn.ReadOnly = true;
            this.uSERDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.uSERDataGridViewTextBoxColumn.Width = 80;
            // 
            // uSERSDataGridViewTextBoxColumn
            // 
            this.uSERSDataGridViewTextBoxColumn.DataPropertyName = "USERS";
            this.uSERSDataGridViewTextBoxColumn.HeaderText = "接单";
            this.uSERSDataGridViewTextBoxColumn.Name = "uSERSDataGridViewTextBoxColumn";
            this.uSERSDataGridViewTextBoxColumn.ReadOnly = true;
            this.uSERSDataGridViewTextBoxColumn.Width = 80;
            // 
            // nTXTDataGridViewTextBoxColumn
            // 
            this.nTXTDataGridViewTextBoxColumn.DataPropertyName = "nTXT";
            this.nTXTDataGridViewTextBoxColumn.HeaderText = "备注";
            this.nTXTDataGridViewTextBoxColumn.Name = "nTXTDataGridViewTextBoxColumn";
            this.nTXTDataGridViewTextBoxColumn.ReadOnly = true;
            this.nTXTDataGridViewTextBoxColumn.Width = 220;
            // 
            // mDIANDataGridViewTextBoxColumn
            // 
            this.mDIANDataGridViewTextBoxColumn.DataPropertyName = "MDIAN";
            this.mDIANDataGridViewTextBoxColumn.HeaderText = "门店";
            this.mDIANDataGridViewTextBoxColumn.Name = "mDIANDataGridViewTextBoxColumn";
            this.mDIANDataGridViewTextBoxColumn.ReadOnly = true;
            this.mDIANDataGridViewTextBoxColumn.Visible = false;
            // 
            // sBMDataGridViewTextBoxColumn
            // 
            this.sBMDataGridViewTextBoxColumn.DataPropertyName = "SBM";
            this.sBMDataGridViewTextBoxColumn.HeaderText = "SBM";
            this.sBMDataGridViewTextBoxColumn.Name = "sBMDataGridViewTextBoxColumn";
            this.sBMDataGridViewTextBoxColumn.ReadOnly = true;
            this.sBMDataGridViewTextBoxColumn.Visible = false;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Visible = false;
            // 
            // 退货状态
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 373);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "退货状态";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.退货状态_FormClosed);
            this.Load += new System.EventHandler(this.退货状态_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.门店退货BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.theDST)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource 门店退货BindingSource;
        private theDST theDST;
        private System.Windows.Forms.DataGridViewTextBoxColumn sETTIMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLIANGDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nTXTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mDIANDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sBMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
    }
}