using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// 绘制基本类，无法实例化。接口为IDraw（绘制接口，记法：在绘图表面Graphics绘制矩阵区域Rectangle用指定的画笔Brush、Pen绘制Draw()）
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public abstract class DrawBase:IDraw,IDisposable
	{
		//************字    段************		
		private Graphics _graphics;				//绘图表面
		private Rectangle _rectangle;			//绘制区

        //绘笔
        private Brush _brush;
        private Pen _pen;

        #region IDraw 成员
        /// <summary>
        /// 获取或设置绘图表面
        /// </summary>
        public Graphics Graphics
        {
            get
            {
                return this._graphics;
            }
            set
            {
                this._graphics = value;
            }
        }

        /// <summary>
        /// 获取或设置绘制区域
        /// </summary>
        public System.Drawing.Rectangle Rectangle
        {
            get
            {
                return _rectangle;
            }
            set
            {
                _rectangle = value;
            }
        }

		/// <summary>
		/// 画笔
		/// </summary>
        public Pen Pen
        {
            get
            {
                return _pen;
            }
            set
            {
                if (value != null)
                {
                    _pen = value;
                }
            }		
        }

		/// <summary>
		/// 画刷
		/// </summary>
		public Brush Brush
		{
			get
			{
				return _brush;
			}
			set
			{
				if (value != null)
				{
					_brush = value;
				}
			}		
		}

		/// <summary>
        /// 绘制，抽象方法，必须由子类具体实现
        /// </summary>
        public abstract void Draw();

        #endregion

		/// <summary>
		/// 绘制基本类，无法实例化
		/// </summary>
        public DrawBase()
		{
			_rectangle = new Rectangle(0,0,0,0);

			_brush = Brushes.Black;
				
			_pen = new Pen(_brush);			 
            _pen = Pens.Black;            
		}

        #region IDisposable 成员

		/// <summary>
		/// 释放对象使用的所有资源
		/// </summary>
        public virtual void Dispose()
        {
			//注意：只能释放自己创建的对象，而不要释放了属性对象（即由外部传过来的对象）
			/*
            _graphics.Dispose();
			*/
            _brush.Dispose();
            _pen.Dispose();
			
        }

        #endregion

	}//End Class
}//End NameSpace
