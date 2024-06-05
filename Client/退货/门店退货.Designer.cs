namespace Client.退货
{
    partial class 门店退货
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.门店退货BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clidata = new Client.clidata();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.phao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SBM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sETTIMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sLIANGDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mDIANDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uSERDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USERS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nTXT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.门店退货BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clidata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 296);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(843, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 26;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.phao,
            this.SBM,
            this.dHDataGridViewTextBoxColumn,
            this.sETTIMEDataGridViewTextBoxColumn,
            this.sLIANGDataGridViewTextBoxColumn,
            this.mDIANDataGridViewTextBoxColumn,
            this.uSERDataGridViewTextBoxColumn,
            this.USERS,
            this.nTXT});
            this.dataGridView1.DataSource = this.门店退货BindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(819, 235);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            // 
            // 门店退货BindingSource
            // 
            this.门店退货BindingSource.DataMember = "门店退货";
            this.门店退货BindingSource.DataSource = this.clidata;
            // 
            // clidata
            // 
            this.clidata.DataSetName = "clidata";
            this.clidata.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 253);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 32);
            this.button1.TabIndex = 3;
            this.button1.Text = "详 细";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(86, 253);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 32);
            this.button2.TabIndex = 4;
            this.button2.Text = "接 单";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(160, 253);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(68, 32);
            this.button3.TabIndex = 5;
            this.button3.Text = "退 出";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(618, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "仓库:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(659, 258);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(172, 22);
            this.comboBox1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Client.Properties.Resources._0x00s05;
            this.pictureBox1.Location = new System.Drawing.Point(814, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(17, 233);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // button4
            // 
            this.button4.Image = global::Client.Properties.Resources._0x00b02;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(281, 253);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(68, 32);
            this.button4.TabIndex = 8;
            this.button4.Text = "  撤消";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(425, 253);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(93, 38);
            this.button5.TabIndex = 9;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // phao
            // 
            this.phao.HeaderText = "编号";
            this.phao.Name = "phao";
            this.phao.ReadOnly = true;
            this.phao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.phao.Width = 35;
            // 
            // SBM
            // 
            this.SBM.DataPropertyName = "SBM";
            this.SBM.HeaderText = "SBM";
            this.SBM.Name = "SBM";
            this.SBM.ReadOnly = true;
            this.SBM.Visible = false;
            // 
            // dHDataGridViewTextBoxColumn
            // 
            this.dHDataGridViewTextBoxColumn.DataPropertyName = "DH";
            this.dHDataGridViewTextBoxColumn.HeaderText = "单号";
            this.dHDataGridViewTextBoxColumn.Name = "dHDataGridViewTextBoxColumn";
            this.dHDataGridViewTextBoxColumn.ReadOnly = true;
            this.dHDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // sETTIMEDataGridViewTextBoxColumn
            // 
            this.sETTIMEDataGridViewTextBoxColumn.DataPropertyName = "SETTIME";
            this.sETTIMEDataGridViewTextBoxColumn.HeaderText = "日期";
            this.sETTIMEDataGridViewTextBoxColumn.Name = "sETTIMEDataGridViewTextBoxColumn";
            this.sETTIMEDataGridViewTextBoxColumn.ReadOnly = true;
            this.sETTIMEDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // sLIANGDataGridViewTextBoxColumn
            // 
            this.sLIANGDataGridViewTextBoxColumn.DataPropertyName = "SLIANG";
            this.sLIANGDataGridViewTextBoxColumn.HeaderText = "数量";
            this.sLIANGDataGridViewTextBoxColumn.Name = "sLIANGDataGridViewTextBoxColumn";
            this.sLIANGDataGridViewTextBoxColumn.ReadOnly = true;
            this.sLIANGDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sLIANGDataGridViewTextBoxColumn.Width = 40;
            // 
            // mDIANDataGridViewTextBoxColumn
            // 
            this.mDIANDataGridViewTextBoxColumn.DataPropertyName = "MDIAN";
            this.mDIANDataGridViewTextBoxColumn.HeaderText = "门店名称";
            this.mDIANDataGridViewTextBoxColumn.Name = "mDIANDataGridViewTextBoxColumn";
            this.mDIANDataGridViewTextBoxColumn.ReadOnly = true;
            this.mDIANDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.mDIANDataGridViewTextBoxColumn.Width = 160;
            // 
            // uSERDataGridViewTextBoxColumn
            // 
            this.uSERDataGridViewTextBoxColumn.DataPropertyName = "USER";
            this.uSERDataGridViewTextBoxColumn.HeaderText = "操作员";
            this.uSERDataGridViewTextBoxColumn.Name = "uSERDataGridViewTextBoxColumn";
            this.uSERDataGridViewTextBoxColumn.ReadOnly = true;
            this.uSERDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.uSERDataGridViewTextBoxColumn.Width = 60;
            // 
            // USERS
            // 
            this.USERS.DataPropertyName = "USERS";
            this.USERS.HeaderText = "接单员";
            this.USERS.Name = "USERS";
            this.USERS.ReadOnly = true;
            this.USERS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.USERS.Width = 60;
            // 
            // nTXT
            // 
            this.nTXT.DataPropertyName = "nTXT";
            this.nTXT.HeaderText = "备注";
            this.nTXT.Name = "nTXT";
            this.nTXT.ReadOnly = true;
            this.nTXT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nTXT.Width = 222;
            // 
            // 门店退货
            // 
            this.ClientSize = new System.Drawing.Size(843, 318);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "门店退货";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.门店退货_Load);
            this.Activated += new System.EventHandler(this.门店退货_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.门店退货_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.门店退货BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clidata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource 门店退货BindingSource;
        private clidata clidata;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridViewTextBoxColumn phao;
        private System.Windows.Forms.DataGridViewTextBoxColumn SBM;
        private System.Windows.Forms.DataGridViewTextBoxColumn dHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sETTIMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLIANGDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mDIANDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uSERDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn USERS;
        private System.Windows.Forms.DataGridViewTextBoxColumn nTXT;
    }
}