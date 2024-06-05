using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// 打印基类，继承于DrawBase,IPrinter。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class Printer:PrinterBase
	{	       
		private Sewing _sewing;					        //装订线预留
		
		private Font _font;                             //绘制对象的文本字体

        private bool _isDrawAllPage;				    //多页打印时，每页都绘制，默认否	
	
        private int _height;					        //对象高



		//绘制对象相对移动
		private int _MoveX;
		private int _MoveY;

	
		#region  ************字段属性************

		/// <summary>
		/// 获取或设置装订线预留，装订线Sewing不再影响页边距等，影响页边距仅仅是PrintDocument。
		/// </summary>
		public virtual Sewing Sewing
		{
			get
			{
				return this._sewing;
			}
			set
			{
				if (value != null)
				{
					this._sewing = value;
			
				}
			}
		}

        /// <summary>
        /// 绘制对象文本字体
        /// </summary>
        public virtual Font Font
        {
            get
            {
                return this._font;
            }
            set
            {
                if (value != null)
                {
                    this._font = value;
                }
            }
        }

		/// <summary>
		/// 多页打印时，每页是否都绘制，默认否
		/// </summary>
		public virtual bool IsDrawAllPage
		{
			get
			{
				return this._isDrawAllPage;
			}
			set
			{
				this._isDrawAllPage = value;
			}
		}

        /// <summary>
        /// 获取或设置对象的高
        /// </summary>
        public virtual int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                if (_height < 0)
                {
                    _height = 0;
                }
            }
        }

		/// <summary>
		/// 绘图起始点相对移动值
		/// </summary>
		public int MoveX
		{
			get{return _MoveX;}
			set{_MoveX = value;}
		}

		/// <summary>
		/// 绘图起始点相对移动值
		/// </summary>
		public int MoveY
		{
			get{return _MoveY;}
			set{_MoveY = value;}
		}
		#endregion


		#region	构造函数
		/// <summary>
		/// 打印类
		/// </summary>
		public Printer()
		{            
			_sewing = new Sewing(); 
			_font = new Font("宋体",10);

			_isDrawAllPage = false;

            _height = 0;
						
//			//其它初始
			_MoveX = 0;
			_MoveY = 0;
		}
		#endregion

		/// <summary>
		/// 获取文本的宽，最宽不会超过有效打印页的宽
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public int TextWidth(string text)
		{
			int width = (int)(Graphics.MeasureString(text,this.Font,this.PrinterMargins.Width).Width);
			return width;
		}

		/// <summary>
		/// 获取文本的高，测量基宽为有效打印页的宽
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public int TextHeight(string text)
		{
			int height = (int)(Graphics.MeasureString(text,this.Font,this.PrinterMargins.Width).Height);	
			return height;
		}

		/// <summary>
		/// 获取文本的行距
		/// </summary>
		/// <returns></returns>
		public int TextHeight()
		{			
			return this.Font.Height;
		}

		/// <summary>
		/// 绘制印区域
		/// </summary>
		public virtual void DrawPrinterMargins()
		{
			CheckGraphics();
			//Rectangle rec = new Rectangle(this.PrinterMargins.Left - ADD_PRINTER_MARGINS,this.PrinterMargins.Top - ADD_PRINTER_MARGINS,this.PrinterMargins.Width + ADD_PRINTER_MARGINS * 2,this.PrinterMargins.Height + ADD_PRINTER_MARGINS * 2);
            Rectangle rec = new Rectangle(this.PrinterMargins.Left,this.PrinterMargins.Top,this.PrinterMargins.Width,this.PrinterMargins.Height);
            Graphics.DrawRectangle(this.Pen,rec);
		}
		
		/// <summary>
		/// 绘制装订线
		/// </summary>
		public virtual void DrawSewing()
		{
			CheckGraphics();
			this.Sewing.LineLen = GetSewingLineLength();
			this.Sewing.Draw(this.Graphics);
		}

        private int GetSewingLineLength()
        {
            int mLineLen = 0;
            if (this.Sewing.SewingDirection == SewingDirectionFlag.Left)
            {
                mLineLen = this.PageHeight;
            }
            else if(this.Sewing.SewingDirection == SewingDirectionFlag.Top)
            {
                mLineLen = this.PageWidth;			
            }

            return mLineLen;
        
        }

		/// <summary>
		/// 在绘图表面画图，这里没有实际的绘图，仅用CheckGraphics()检测了Graphics是否为空，实际的绘图过程由继承者去画。可以重写CheckGraphics()。
		/// </summary>
		public override void Draw()
		{
			CheckGraphics();
			//...
			//下面由继承者去画具体的
		}

		protected virtual void CheckGraphics()
		{
			if (this.Graphics == null)
			{
				throw new Exception("绘图表面不能为空，请设置Graphics属性！");
			}		
		}

        public override void Dispose()
        {
            base.Dispose ();
			_sewing.Dispose();
            _font.Dispose();
        }

//        //加上装订的非打印区域
//        private void AddSewingNonePrintArea()
//        {
//            if (this.Sewing.SewingDirection == SewingDirectionFlag.Left)
//            {
//                this.PrinterMargins.Left += this.Sewing.Margin;
//                this.PrinterMargins.Width -= this.Sewing.Margin;
//            }
//            else if (this.Sewing.SewingDirection == SewingDirectionFlag.Top)
//            {
//                this.PrinterMargins.Top += this.Sewing.Margin;
//                this.PrinterMargins.Height -= this.Sewing.Margin;			
//            }
//        }        


	}//End Class
}//End NameSpace
