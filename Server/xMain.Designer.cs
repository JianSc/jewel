namespace Server
{
    partial class xMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开管理器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.新建用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户权限设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.备份数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.恢复数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.phao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xIPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xStatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xUserBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.serData = new Server.serData();
            this.Led2 = new System.Windows.Forms.PictureBox();
            this.Led1 = new System.Windows.Forms.PictureBox();
            this.PICEXIT = new System.Windows.Forms.PictureBox();
            this.PIC_A = new System.Windows.Forms.PictureBox();
            this.PIC_X = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.myscrollbas = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xUserBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Led2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Led1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICEXIT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myscrollbas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "启智网络服务端";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开管理器ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.新建用户ToolStripMenuItem,
            this.用户权限设置ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.备份数据库ToolStripMenuItem,
            this.恢复数据库ToolStripMenuItem,
            this.设置ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 170);
            // 
            // 打开管理器ToolStripMenuItem
            // 
            this.打开管理器ToolStripMenuItem.Name = "打开管理器ToolStripMenuItem";
            this.打开管理器ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开管理器ToolStripMenuItem.Text = "打开管理器";
            this.打开管理器ToolStripMenuItem.Click += new System.EventHandler(this.打开管理器ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // 新建用户ToolStripMenuItem
            // 
            this.新建用户ToolStripMenuItem.Name = "新建用户ToolStripMenuItem";
            this.新建用户ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新建用户ToolStripMenuItem.Text = "用户管理";
            this.新建用户ToolStripMenuItem.Click += new System.EventHandler(this.新建用户ToolStripMenuItem_Click);
            // 
            // 用户权限设置ToolStripMenuItem
            // 
            this.用户权限设置ToolStripMenuItem.Name = "用户权限设置ToolStripMenuItem";
            this.用户权限设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.用户权限设置ToolStripMenuItem.Text = "用户权限设置";
            this.用户权限设置ToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // 备份数据库ToolStripMenuItem
            // 
            this.备份数据库ToolStripMenuItem.Name = "备份数据库ToolStripMenuItem";
            this.备份数据库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.备份数据库ToolStripMenuItem.Text = "备份数据库";
            this.备份数据库ToolStripMenuItem.Visible = false;
            // 
            // 恢复数据库ToolStripMenuItem
            // 
            this.恢复数据库ToolStripMenuItem.Name = "恢复数据库ToolStripMenuItem";
            this.恢复数据库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.恢复数据库ToolStripMenuItem.Text = "恢复数据库";
            this.恢复数据库ToolStripMenuItem.Visible = false;
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.设置ToolStripMenuItem.Text = "设置";
            this.设置ToolStripMenuItem.Click += new System.EventHandler(this.设置ToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 24;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.phao,
            this.xNameDataGridViewTextBoxColumn,
            this.xIPDataGridViewTextBoxColumn,
            this.xStatDataGridViewTextBoxColumn,
            this.xTimeDataGridViewTextBoxColumn,
            this.newTimeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.xUserBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Location = new System.Drawing.Point(67, 109);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(389, 198);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            // 
            // phao
            // 
            this.phao.HeaderText = "编号";
            this.phao.Name = "phao";
            this.phao.ReadOnly = true;
            this.phao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.phao.Visible = false;
            this.phao.Width = 40;
            // 
            // xNameDataGridViewTextBoxColumn
            // 
            this.xNameDataGridViewTextBoxColumn.DataPropertyName = "xName";
            this.xNameDataGridViewTextBoxColumn.HeaderText = "名称";
            this.xNameDataGridViewTextBoxColumn.Name = "xNameDataGridViewTextBoxColumn";
            this.xNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.xNameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.xNameDataGridViewTextBoxColumn.Width = 70;
            // 
            // xIPDataGridViewTextBoxColumn
            // 
            this.xIPDataGridViewTextBoxColumn.DataPropertyName = "xIP";
            this.xIPDataGridViewTextBoxColumn.HeaderText = "IP地址";
            this.xIPDataGridViewTextBoxColumn.Name = "xIPDataGridViewTextBoxColumn";
            this.xIPDataGridViewTextBoxColumn.ReadOnly = true;
            this.xIPDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // xStatDataGridViewTextBoxColumn
            // 
            this.xStatDataGridViewTextBoxColumn.DataPropertyName = "xStat";
            this.xStatDataGridViewTextBoxColumn.HeaderText = "状态";
            this.xStatDataGridViewTextBoxColumn.Name = "xStatDataGridViewTextBoxColumn";
            this.xStatDataGridViewTextBoxColumn.ReadOnly = true;
            this.xStatDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // xTimeDataGridViewTextBoxColumn
            // 
            this.xTimeDataGridViewTextBoxColumn.DataPropertyName = "xTime";
            this.xTimeDataGridViewTextBoxColumn.HeaderText = "登陆时间";
            this.xTimeDataGridViewTextBoxColumn.Name = "xTimeDataGridViewTextBoxColumn";
            this.xTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.xTimeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.xTimeDataGridViewTextBoxColumn.Width = 104;
            // 
            // newTimeDataGridViewTextBoxColumn
            // 
            this.newTimeDataGridViewTextBoxColumn.DataPropertyName = "newTime";
            this.newTimeDataGridViewTextBoxColumn.HeaderText = "newTime";
            this.newTimeDataGridViewTextBoxColumn.Name = "newTimeDataGridViewTextBoxColumn";
            this.newTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.newTimeDataGridViewTextBoxColumn.Visible = false;
            // 
            // xUserBindingSource
            // 
            this.xUserBindingSource.DataMember = "xUser";
            this.xUserBindingSource.DataSource = this.serData;
            // 
            // serData
            // 
            this.serData.DataSetName = "serData";
            this.serData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Led2
            // 
            this.Led2.BackColor = System.Drawing.Color.Transparent;
            this.Led2.Image = global::Server.Properties.Resources.Led1;
            this.Led2.Location = new System.Drawing.Point(16, 171);
            this.Led2.Name = "Led2";
            this.Led2.Size = new System.Drawing.Size(35, 15);
            this.Led2.TabIndex = 5;
            this.Led2.TabStop = false;
            this.Led2.Visible = false;
            // 
            // Led1
            // 
            this.Led1.BackColor = System.Drawing.Color.Transparent;
            this.Led1.Image = global::Server.Properties.Resources.Led1;
            this.Led1.Location = new System.Drawing.Point(16, 150);
            this.Led1.Name = "Led1";
            this.Led1.Size = new System.Drawing.Size(35, 15);
            this.Led1.TabIndex = 4;
            this.Led1.TabStop = false;
            this.Led1.Visible = false;
            // 
            // PICEXIT
            // 
            this.PICEXIT.BackColor = System.Drawing.Color.Transparent;
            this.PICEXIT.Image = global::Server.Properties.Resources.exit;
            this.PICEXIT.Location = new System.Drawing.Point(6, 256);
            this.PICEXIT.Name = "PICEXIT";
            this.PICEXIT.Size = new System.Drawing.Size(54, 54);
            this.PICEXIT.TabIndex = 3;
            this.PICEXIT.TabStop = false;
            this.PICEXIT.MouseLeave += new System.EventHandler(this.PICEXIT_MouseLeave);
            this.PICEXIT.Click += new System.EventHandler(this.PICEXIT_Click);
            this.PICEXIT.MouseEnter += new System.EventHandler(this.PICEXIT_MouseEnter);
            // 
            // PIC_A
            // 
            this.PIC_A.BackColor = System.Drawing.Color.Transparent;
            this.PIC_A.Image = global::Server.Properties.Resources._;
            this.PIC_A.Location = new System.Drawing.Point(407, 4);
            this.PIC_A.Name = "PIC_A";
            this.PIC_A.Size = new System.Drawing.Size(25, 25);
            this.PIC_A.TabIndex = 2;
            this.PIC_A.TabStop = false;
            this.PIC_A.MouseLeave += new System.EventHandler(this.PIC_A_MouseLeave);
            this.PIC_A.Click += new System.EventHandler(this.PIC_A_Click);
            this.PIC_A.MouseEnter += new System.EventHandler(this.PIC_A_MouseEnter);
            // 
            // PIC_X
            // 
            this.PIC_X.BackColor = System.Drawing.Color.Transparent;
            this.PIC_X.Image = global::Server.Properties.Resources.X;
            this.PIC_X.Location = new System.Drawing.Point(438, 4);
            this.PIC_X.Name = "PIC_X";
            this.PIC_X.Size = new System.Drawing.Size(25, 25);
            this.PIC_X.TabIndex = 1;
            this.PIC_X.TabStop = false;
            this.PIC_X.MouseLeave += new System.EventHandler(this.PIC_X_MouseLeave);
            this.PIC_X.Click += new System.EventHandler(this.PIC_X_Click);
            this.PIC_X.MouseEnter += new System.EventHandler(this.PIC_X_MouseEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Server.Properties.Resources.SerMain;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(470, 322);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // myscrollbas
            // 
            this.myscrollbas.Image = global::Server.Properties.Resources.Scrollbas;
            this.myscrollbas.Location = new System.Drawing.Point(440, 109);
            this.myscrollbas.Name = "myscrollbas";
            this.myscrollbas.Size = new System.Drawing.Size(16, 198);
            this.myscrollbas.TabIndex = 7;
            this.myscrollbas.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(401, 29);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // xMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 322);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.myscrollbas);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Led2);
            this.Controls.Add(this.Led1);
            this.Controls.Add(this.PICEXIT);
            this.Controls.Add(this.PIC_A);
            this.Controls.Add(this.PIC_X);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "xMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "启智网络版服务端";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xUserBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Led2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Led1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICEXIT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PIC_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myscrollbas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox PIC_X;
        private System.Windows.Forms.PictureBox PIC_A;
        private System.Windows.Forms.PictureBox PICEXIT;
        private System.Windows.Forms.PictureBox Led1;
        private System.Windows.Forms.PictureBox Led2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox myscrollbas;
        private System.Windows.Forms.BindingSource xUserBindingSource;
        private serData serData;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 新建用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户权限设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 备份数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 恢复数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开管理器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn phao;
        private System.Windows.Forms.DataGridViewTextBoxColumn xNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xIPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xStatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn newTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
    }
}

