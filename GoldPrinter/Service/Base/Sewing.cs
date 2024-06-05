using System;
using System.Drawing;
using System.Drawing.Printing;

/// 
/// 作 者：长江支流(周方勇)
/// Email：flygoldfish@163.com  QQ：150439795
/// 网 址：www.webmis.com.cn
/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
/// 

namespace GoldPrinter
{
	#region *****图例*****

	//				    左装订图例
	//   _______________________________________
	//	|           							|
	//	|           |	 						|
	//	|           |							|
	//	|           装							|
	//	|           |							|
	//	|           |							|
	//	|           订							|
	//	|           |							|
	//	|           |							|
	//	|           线							|
	//	|           |							|
	//	|           |							|
	//	|_______________________________________|
	// 
	//  |<- Margin ->|
	//  装订线文字所在的线长为LineLen


	//				    顶装订图例
	//   _______________________________________  __ __
	//	|           							|   |
	//	|           							|   |
	//	|           	 						| Margin
	//	|           							|   |
	//	|     -----装-------订-------线-----    | __|__
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|_______________________________________|
	// 
	//  装订线文字所在的线长为LineLen
	#endregion

	/// <summary>
	/// 装订线方位
	/// </summary>
	public enum SewingDirectionFlag
	{
		/// <summary>
		/// 在左边装订
		/// </summary>
		Left
			,
		/// <summary>
		/// 在顶端装订
		/// </summary>
		Top	
	}

	/// <summary>
	/// 装订类，在打印页面时可能在页面的左边或顶端为装订专门另预留一部分。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class Sewing:IDisposable
	{
		//装订方向、边界、装订线长
		private SewingDirectionFlag _sewingDirection;
		private int _margin;
		private int _lineLength;

		#region 字段属性
		/// <summary>
		/// 获取或设置装订方向
		/// </summary>
		public SewingDirectionFlag SewingDirection
		{
			set
			{
				this._sewingDirection = value;
			}
			get
			{
				return this._sewingDirection;
			}		
		}

		/// <summary>
		/// 获取或设置装订预留空白
		/// </summary>
		public int Margin
		{
			set
			{
				this._margin = value;
			}
			get
			{
				return this._margin;
			}
		}

		/// <summary>
		/// 获取或设置装订线长
		/// </summary>
		public int LineLen
		{
			set
			{
				this._lineLength = value;
			}
			get
			{
				return this._lineLength;
			}
		}
		#endregion

		#region 构造函数
		public Sewing()
		{	
			this._margin = 0;
			this._sewingDirection = SewingDirectionFlag.Left;
			_lineLength = 0;
		}

		public Sewing(int margin):this(margin,SewingDirectionFlag.Left,0)
		{
			
		}

		public Sewing(int margin,int lineLength):this(margin,SewingDirectionFlag.Left,lineLength)
		{
			
		}

		public Sewing(int margin,SewingDirectionFlag sewingDirection):this(margin,sewingDirection,0)
		{
			
		}

		//完整的构造函数，参数包括全部的字段
		public Sewing(int margin,SewingDirectionFlag sewingDirection,int lineLength)
		{
			this._margin = margin;
			this._sewingDirection = sewingDirection;
			this._lineLength = lineLength;
		}
		#endregion

		#region IDisposable 成员

		public virtual void Dispose()
		{

		}

		#endregion

		/// <summary>
		/// 在指定的绘图表面画装订线
		/// </summary>
		/// <param name="g">绘图表面</param>
		/// <remarks>
		/// 作    者：周方勇
		/// 修改日期：2004-08-07
		/// </remarks>
		public void Draw(Graphics g)
		{
			//字体
			Font font = new Font("宋体",8);
			//装订线文本
			string strText = "装                    订                    线";
			//写文字格式
			StringFormat sf = new StringFormat();
			//居中放
			sf.Alignment = StringAlignment.Center;

			int LeftMargin , TopMargin;
			int PageHeight , PageWidth;
			LeftMargin = TopMargin = this._margin;
			PageHeight = PageWidth = this._lineLength;

			Pen pen = new Pen(Color.Red);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			
			//左装订线边界
			if (this._sewingDirection == SewingDirectionFlag.Left)
			{
				//画竖线
				g.DrawLine(pen,LeftMargin,0,LeftMargin,PageHeight);
							

				//写文字
				sf.FormatFlags = StringFormatFlags.DirectionVertical;	//文字竖放

				int textWidth = (int)(g.MeasureString("装",font).Width);
				textWidth /= 2;
		
				Rectangle rec = new Rectangle(LeftMargin - textWidth,0,LeftMargin - textWidth,PageHeight);

				g.DrawString(strText,font,Brushes.DodgerBlue,rec,sf);
			}
			//上装订线边界
			else if (this._sewingDirection == SewingDirectionFlag.Top)
			{
				//画横线
				g.DrawLine(pen,0,TopMargin,PageWidth,TopMargin);				
				
				//写文字
				int textHeight = (int)(g.MeasureString("装",font).Height);
				textHeight /= 2;

				Rectangle rec = new Rectangle(0,TopMargin - textHeight,PageWidth,TopMargin - textHeight);

				g.DrawString(strText,font,Brushes.DodgerBlue,rec,sf);
			}	
			pen.Dispose();
			font.Dispose();
            sf.Dispose();
        }

    }//End Class
}//End NameSpace
