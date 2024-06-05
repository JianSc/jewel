namespace Mdian
{
    partial class Lend
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordbox = new System.Windows.Forms.TextBox();
            this.button2img = new System.Windows.Forms.PictureBox();
            this.button1img = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.xexit = new System.Windows.Forms.PictureBox();
            this._min = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.userbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Mdianbox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.button2img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.button1img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xexit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(42, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "密  码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(42, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "工  号：";
            // 
            // passwordbox
            // 
            this.passwordbox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordbox.Location = new System.Drawing.Point(98, 180);
            this.passwordbox.Name = "passwordbox";
            this.passwordbox.PasswordChar = '*';
            this.passwordbox.Size = new System.Drawing.Size(167, 26);
            this.passwordbox.TabIndex = 1;
            this.passwordbox.Enter += new System.EventHandler(this.userbox_Enter);
            this.passwordbox.Leave += new System.EventHandler(this.userbox_Leave);
            this.passwordbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userbox_KeyPress);
            // 
            // button2img
            // 
            this.button2img.BackColor = System.Drawing.Color.Transparent;
            this.button2img.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2img.Image = global::Mdian.Properties.Resources.button1;
            this.button2img.Location = new System.Drawing.Point(93, 215);
            this.button2img.Name = "button2img";
            this.button2img.Size = new System.Drawing.Size(82, 39);
            this.button2img.TabIndex = 12;
            this.button2img.TabStop = false;
            this.button2img.MouseLeave += new System.EventHandler(this.button2img_MouseLeave);
            this.button2img.Click += new System.EventHandler(this.button2img_Click);
            this.button2img.MouseEnter += new System.EventHandler(this.button2img_MouseEnter);
            // 
            // button1img
            // 
            this.button1img.BackColor = System.Drawing.Color.Transparent;
            this.button1img.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1img.Image = global::Mdian.Properties.Resources.button2;
            this.button1img.Location = new System.Drawing.Point(5, 215);
            this.button1img.Name = "button1img";
            this.button1img.Size = new System.Drawing.Size(82, 39);
            this.button1img.TabIndex = 11;
            this.button1img.TabStop = false;
            this.button1img.MouseLeave += new System.EventHandler(this.button1img_MouseLeave);
            this.button1img.Click += new System.EventHandler(this.button1img_Click);
            this.button1img.MouseEnter += new System.EventHandler(this.button1img_MouseEnter);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pictureBox2.Location = new System.Drawing.Point(2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(289, 27);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            // 
            // xexit
            // 
            this.xexit.BackColor = System.Drawing.Color.Transparent;
            this.xexit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.xexit.Image = global::Mdian.Properties.Resources.X;
            this.xexit.Location = new System.Drawing.Point(327, 3);
            this.xexit.Name = "xexit";
            this.xexit.Size = new System.Drawing.Size(25, 25);
            this.xexit.TabIndex = 4;
            this.xexit.TabStop = false;
            this.xexit.MouseLeave += new System.EventHandler(this.xexit_MouseLeave);
            this.xexit.Click += new System.EventHandler(this.xexit_Click);
            this.xexit.MouseEnter += new System.EventHandler(this.xexit_MouseEnter);
            // 
            // _min
            // 
            this._min.BackColor = System.Drawing.Color.Transparent;
            this._min.Cursor = System.Windows.Forms.Cursors.Hand;
            this._min.Image = global::Mdian.Properties.Resources._;
            this._min.Location = new System.Drawing.Point(296, 3);
            this._min.Name = "_min";
            this._min.Size = new System.Drawing.Size(25, 25);
            this._min.TabIndex = 3;
            this._min.TabStop = false;
            this._min.MouseLeave += new System.EventHandler(this._min_MouseLeave);
            this._min.Click += new System.EventHandler(this._min_Click);
            this._min.MouseEnter += new System.EventHandler(this._min_MouseEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Mdian.Properties.Resources.LEND;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(358, 260);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // userbox
            // 
            this.userbox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userbox.Location = new System.Drawing.Point(98, 149);
            this.userbox.Name = "userbox";
            this.userbox.Size = new System.Drawing.Size(167, 26);
            this.userbox.TabIndex = 0;
            this.userbox.Enter += new System.EventHandler(this.userbox_Enter);
            this.userbox.Leave += new System.EventHandler(this.userbox_Leave);
            this.userbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userbox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(42, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "门  店：";
            // 
            // Mdianbox
            // 
            this.Mdianbox.BackColor = System.Drawing.Color.White;
            this.Mdianbox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Mdianbox.Location = new System.Drawing.Point(98, 118);
            this.Mdianbox.Name = "Mdianbox";
            this.Mdianbox.ReadOnly = true;
            this.Mdianbox.Size = new System.Drawing.Size(167, 26);
            this.Mdianbox.TabIndex = 19;
            this.Mdianbox.TabStop = false;
            // 
            // Lend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 260);
            this.Controls.Add(this.Mdianbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.userbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordbox);
            this.Controls.Add(this.button2img);
            this.Controls.Add(this.button1img);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.xexit);
            this.Controls.Add(this._min);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Lend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登陆";
            this.Load += new System.EventHandler(this.Lend_Load);
            ((System.ComponentModel.ISupportInitialize)(this.button2img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.button1img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xexit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox xexit;
        private System.Windows.Forms.PictureBox _min;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordbox;
        private System.Windows.Forms.PictureBox button2img;
        private System.Windows.Forms.PictureBox button1img;
        private System.Windows.Forms.TextBox userbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Mdianbox;


    }
}