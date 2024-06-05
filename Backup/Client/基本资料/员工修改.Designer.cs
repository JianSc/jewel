namespace Client.基本资料
{
    partial class 员工修改
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mdianbox = new System.Windows.Forms.ComboBox();
            this.xbiebox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.namebox = new System.Windows.Forms.TextBox();
            this.ghbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.sfzhenbox = new System.Windows.Forms.TextBox();
            this.jguanbox = new System.Windows.Forms.TextBox();
            this.telbox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(340, 226);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox3);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.mdianbox);
            this.tabPage1.Controls.Add(this.xbiebox);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.namebox);
            this.tabPage1.Controls.Add(this.ghbox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(332, 201);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本资料";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox3.Location = new System.Drawing.Point(170, 74);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new System.Drawing.Size(134, 23);
            this.textBox3.TabIndex = 9;
            this.textBox3.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(168, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "密码:";
            // 
            // mdianbox
            // 
            this.mdianbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mdianbox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mdianbox.FormattingEnabled = true;
            this.mdianbox.Location = new System.Drawing.Point(22, 159);
            this.mdianbox.Name = "mdianbox";
            this.mdianbox.Size = new System.Drawing.Size(282, 22);
            this.mdianbox.TabIndex = 3;
            this.mdianbox.Leave += new System.EventHandler(this.ghbox_Leave);
            this.mdianbox.Enter += new System.EventHandler(this.ghbox_Enter);
            // 
            // xbiebox
            // 
            this.xbiebox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.xbiebox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xbiebox.FormattingEnabled = true;
            this.xbiebox.Items.AddRange(new object[] {
            "男",
            "女"});
            this.xbiebox.Location = new System.Drawing.Point(22, 117);
            this.xbiebox.Name = "xbiebox";
            this.xbiebox.Size = new System.Drawing.Size(57, 22);
            this.xbiebox.TabIndex = 2;
            this.xbiebox.Leave += new System.EventHandler(this.ghbox_Leave);
            this.xbiebox.Enter += new System.EventHandler(this.ghbox_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "所属门店:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "性别:";
            // 
            // namebox
            // 
            this.namebox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.namebox.Location = new System.Drawing.Point(22, 74);
            this.namebox.Name = "namebox";
            this.namebox.Size = new System.Drawing.Size(121, 23);
            this.namebox.TabIndex = 1;
            this.namebox.Enter += new System.EventHandler(this.ghbox_Enter);
            this.namebox.Leave += new System.EventHandler(this.ghbox_Leave);
            // 
            // ghbox
            // 
            this.ghbox.BackColor = System.Drawing.Color.White;
            this.ghbox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ghbox.Location = new System.Drawing.Point(22, 31);
            this.ghbox.Name = "ghbox";
            this.ghbox.ReadOnly = true;
            this.ghbox.Size = new System.Drawing.Size(121, 23);
            this.ghbox.TabIndex = 0;
            this.ghbox.Enter += new System.EventHandler(this.ghbox_Enter);
            this.ghbox.Leave += new System.EventHandler(this.ghbox_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "姓名:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "工号:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.sfzhenbox);
            this.tabPage2.Controls.Add(this.jguanbox);
            this.tabPage2.Controls.Add(this.telbox);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(332, 201);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "详细资料";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // sfzhenbox
            // 
            this.sfzhenbox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sfzhenbox.Location = new System.Drawing.Point(22, 123);
            this.sfzhenbox.Name = "sfzhenbox";
            this.sfzhenbox.Size = new System.Drawing.Size(282, 23);
            this.sfzhenbox.TabIndex = 2;
            this.sfzhenbox.Enter += new System.EventHandler(this.telbox_Enter);
            this.sfzhenbox.Leave += new System.EventHandler(this.telbox_Leave);
            // 
            // jguanbox
            // 
            this.jguanbox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.jguanbox.Location = new System.Drawing.Point(22, 80);
            this.jguanbox.Name = "jguanbox";
            this.jguanbox.Size = new System.Drawing.Size(282, 23);
            this.jguanbox.TabIndex = 1;
            this.jguanbox.Enter += new System.EventHandler(this.telbox_Enter);
            this.jguanbox.Leave += new System.EventHandler(this.telbox_Leave);
            // 
            // telbox
            // 
            this.telbox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.telbox.Location = new System.Drawing.Point(22, 37);
            this.telbox.Name = "telbox";
            this.telbox.Size = new System.Drawing.Size(183, 23);
            this.telbox.TabIndex = 0;
            this.telbox.Enter += new System.EventHandler(this.telbox_Enter);
            this.telbox.Leave += new System.EventHandler(this.telbox_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "身份证号码:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "籍贯:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "联系电话:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 281);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(364, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // button1
            // 
            this.button1.Image = global::Client.Properties.Resources._0x00003;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(12, 240);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 28);
            this.button1.TabIndex = 2;
            this.button1.TabStop = false;
            this.button1.Text = "  修改";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::Client.Properties.Resources._0x00002;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(96, 240);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 28);
            this.button2.TabIndex = 3;
            this.button2.TabStop = false;
            this.button2.Text = "  退出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // 员工修改
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 303);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "员工修改";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.员工修改_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox namebox;
        private System.Windows.Forms.TextBox ghbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ComboBox mdianbox;
        private System.Windows.Forms.ComboBox xbiebox;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox sfzhenbox;
        private System.Windows.Forms.TextBox jguanbox;
        private System.Windows.Forms.TextBox telbox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}