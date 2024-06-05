using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// 子标题。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class Caption:Printer
	{
		private string _text;			//文本
		private int _maxRows;			//限制行数
		private bool _hasborder;		//画边框

		/// <summary>
		///  默认构造函数
		/// </summary>
		public Caption()
		{	
			//初始值
			_text = "";
			_maxRows = 1;
			_hasborder = false;
			this.Font = new Font("宋体",13,FontStyle.Italic);		//字体

			this.IsDrawAllPage = true;
		}

		/// <summary>
		/// 子标题构造函数
		/// </summary>
		/// <param name="text"></param>
		public Caption(string text):this()
		{
			this._text = text;
		}

		public string Text
		{
			get{return this._text;}
			set{this._text = value;}
		}	

		public bool HasBorder
		{
			get{return this._hasborder;}
			set{this._hasborder = value;}
		}	

		/// <summary>
		/// 限制行数，小于0为不限制
		/// </summary>
		public int MaxRows
		{
			get{return this._maxRows;}
			set{this._maxRows = value;}
		}	

		public override void Draw()
		{
			base.Draw();

			//绘图起始座标及字符串的宽与高
			int x,y;
			x = this.PrinterMargins.X1;
			y = this.PrinterMargins.Y1;			

			//相对移动
			x += this.MoveX;
			y += this.MoveY;

			
			//测量字符串尺寸是否过量
			int width = this.PrinterMargins.Width;		//用最宽使居中	//this.TextWidth(this._text);	//获取文本的宽，最宽不会超过有效打印页的宽
			int height = this.TextHeight(this._text);	//获取文本的高，测量基宽为有效打印页的宽

			//不能超过最高
			if (height > this.PrinterMargins.Height)
			{
				height = this.PrinterMargins.Height;
			}

			//限制行数
			if (this._maxRows > 0)
			{
				if (height > this.Font.Height * _maxRows)
				{
					height = this.Font.Height * _maxRows;
				}
			}
           
			//文本的高测量基宽为有效打印页的宽，因此会根据实际字符自动换行
			Rectangle rec = new Rectangle(x,y,width,height);
            

			StringFormat sf = new StringFormat();

			sf.Alignment = StringAlignment.Center;			//横向居中
			sf.LineAlignment = StringAlignment.Center;		//竖向居中

			#region	计算实际绘图区
			//写绘制时的坐标信息
			rec.X = (this.PrinterMargins.Width - this.TextWidth(this.Text))/2  + this.PrinterMargins.Left + this.MoveX;
			rec.Y = y;
			if (this.TextWidth(this.Text) < this.PrinterMargins.Width)
			{
				rec.Width = this.TextWidth(this.Text);		
			}
			else
			{
				rec.Width = this.PrinterMargins.Width;				
			}

			rec.Height = height;	

			this.Rectangle = rec;
			#endregion

			//画上打印有效区的线
			if (_hasborder)
			{
				this.Graphics.DrawRectangle(this.Pen,this.Rectangle.X,this.Rectangle.Y,this.Rectangle.Width,this.Rectangle.Height);
			}

			rec.X = rec.X - 1;
			rec.Y = rec.Y - 1;
			rec.Width = rec.Width + 2;
			rec.Height = rec.Height + 2;
			//输出文本
			this.Graphics.DrawString(_text,this.Font,this.Brush,rec,sf);

			this.Height = height;
			
		}


	}//End Class
}//End NameSpace
