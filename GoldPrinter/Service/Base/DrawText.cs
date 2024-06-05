using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// 继承于DrawBase，在绘图表面Graphics绘制矩阵区域Rectangle用指定的画笔Brush、Pen绘制Draw() 格式为StringFormat、字体Font的文本Text。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class DrawText:DrawBase
	{
		private string _text;
		private Font _font;
		private StringFormat _stringFormat;

		private int _startChar;
		private int _linesFilled;
		private int _charsFitted;

		private bool _hasMorePages;		//是否还没有打完
		
		#region 字段属性		

		/// <summary>
		/// 获取或设置要绘制的文本
		/// </summary>
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}		
		}

		/// <summary>
		/// 获取或设置绘制的文本字体
		/// </summary>
		public Font Font
		{
			get
			{
				return _font;
			}
			set
			{
				if (value != null)
				{
					_font = value;
				}
			}		
		}

		/// <summary>
		/// 获取或设置要绘制的文本的格式，如行距
		/// </summary>
		public StringFormat StringFormat
		{
			get
			{
				return _stringFormat;
			}
			set
			{
				_stringFormat = value;
			}		
		}

		/// <summary>
		/// 获取或设置要绘制的文本时的起始字符，即从第几个开始。如果小于0则从最开始的符绘起，如大于或等于文本长度，则不绘制。
		/// </summary>
		public int StartChar
		{
			get
			{
				return _startChar;
			}
			set
			{
				_startChar = value;
				if (_startChar < 0)
				{
					_startChar = 0;
				}
				else if(_startChar >= this.Text.Length)
				{
					_startChar = this.Text.Length;
				}
			}		
		}

		/// <summary>
		/// 获取绘制区内绘制的文本数
		/// </summary>
		public int CharsFitted
		{
			get
			{
				return _charsFitted;
			}
		}

		/// <summary>
		/// 获取绘制区内绘制的文本行数
		/// </summary>
		public int LinesFilled
		{
			get
			{
				return _linesFilled;
			}
		}

		/*
		/// <summary>
		/// 获取是否还有没有打印完的文本
		/// </summary>
		public int HasMorePages
		{
			get
			{
				return _hasMorePages;
			}
		}
		*/
		#endregion

		#region 构造函数
		/// <summary>
		/// 获取DrawText新的实例，默认字体为宋体10号，受限制行LineLimit格式绘制
		/// </summary>
		public DrawText()
		{
			_text = "";
			_font = new Font("宋体",10);

			_startChar = 0;
			_linesFilled = 0;
			_charsFitted = 0;

			_stringFormat = new StringFormat(StringFormatFlags.LineLimit);
		}

		/// <param name="text">指定绘制的文本</param>
		public DrawText(string text):this()
		{
			_text = text;
		}
		#endregion

		/// <summary>
		/// 释放对象使用的所有资源
		/// </summary>
		public override void Dispose()
		{
			base.Dispose ();
			this._font.Dispose();
			this._stringFormat.Dispose();
		}

		//一次可以打印不完
		private void DrawOnePage()
		{
			if (this.Graphics != null)
			{
				int intLinesFilled, intCharsFitted;

				this.Graphics.MeasureString(_text.Substring(_startChar),_font,new SizeF(this.Rectangle.Width, this.Rectangle.Height),_stringFormat,out intCharsFitted,out intLinesFilled);
			
				this.Graphics.DrawString(_text.Substring(_startChar),_font,this.Brush,this.Rectangle,_stringFormat);

				this._linesFilled = intLinesFilled;
				this._charsFitted = intCharsFitted;
			}
		}


		/// <summary>
		/// 在绘图表面Graphics不为空的情况下，绘制矩阵区域Rectangle用指定的画笔Brush、Pen绘制Draw() 格式为StringFormat、字体Font的文本Text
		/// </summary>
		public override void Draw()
		{
			DrawOnePage();
		}


		//其它...
		private static int intCurrentCharIndex;			//静态量，记住打印的起始字符
		private bool Draw(DrawText p_drawText,string p_text)
		{
			DrawText mdrawText = new DrawText(p_text);
//			mdrawText.Draw();

			mdrawText = this;
			mdrawText.StartChar = intCurrentCharIndex;

			intCurrentCharIndex += mdrawText.CharsFitted;

			if (intCurrentCharIndex < p_text.Length)
			{
				return true;
			}
			else
			{				
				intCurrentCharIndex = 0;			//打印结束后一定要再重置
				return false;
			}
		}


	}//End Class
}//End Namespace