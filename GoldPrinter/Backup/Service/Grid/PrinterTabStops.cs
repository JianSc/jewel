using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// 绘制表符分隔的字符串，以|分隔列，以\n换行，列对齐均为居左。合适于列少每列字符少的几行几列的打印。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class PrinterTabStops:IDisposable
	{
		private int _cols;				//列数

		public Font Font;				//字体
		public Rectangle Rectangle;	//在此区域画

		private string _text;			//原始打印文本
		private string _textConverted;	//转换后打印文本		 

		public PrinterTabStops()
		{
			Font = new Font("宋体",10);
			Rectangle = new Rectangle(0,0,this.Font.Height,100);
		}

		public PrinterTabStops(string text):this()
		{
			this.Text = text;
		}


		/// <summary>
		/// 或取或设置最大列数
		/// </summary>
		public int Cols
		{
			get{return this._cols;}
			set{this._cols = value;}
		}

		/// <summary>
		/// 或取或设置打印文本
		/// </summary>
		public string Text
		{
			get{return _text;}
			set
			{
				_text = value;

				string txt = _text;
				_textConverted = ConvertText(txt);
			}
		}
		public void Draw(Graphics g)
		{
			//文本格式
			StringFormat sf = new StringFormat();
			sf.FormatFlags = StringFormatFlags.NoWrap;
			float colWidth;
			
			colWidth = Rectangle.Width / _cols;

			float[] arrcolWidth = new float[_cols];

			for(int i = 0 ; i < _cols ; i++)
			{
				arrcolWidth[i] = colWidth;
			}


			//起用制表位
			sf.SetTabStops(0.0f,arrcolWidth);

			g.DrawString(this._textConverted,this.Font,Brushes.Black,this.Rectangle,sf);		
		
		}

		/// <summary>
		/// 将文本以|分隔的文本转换为制表文本
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private string ConvertText(string text)
		{
			string txt = text;
			txt = txt.Replace("|","\t");
			
			return txt;

        }

        #region IDisposable 成员

        public void Dispose()
        {
            this.Font.Dispose();
        }

        #endregion

    }//End Class
}//End NameSpace
