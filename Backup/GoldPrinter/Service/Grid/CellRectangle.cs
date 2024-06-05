using System;

namespace GoldPrinter
{
	/// <summary>
	/// 网格单元格矩阵,描述左顶宽高
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public struct CellRectangle
	{	
		private int _cellLeft,_cellTop,_cellWidth,_cellHeight;

		public CellRectangle(int cellLeft,int cellTop,int cellWidth,int cellHeight)
		{
			_cellLeft = cellLeft;
			_cellTop = cellTop;
			_cellWidth = cellWidth;
			_cellHeight = cellHeight;
		}

		/// <summary>
		/// 单元格起点X坐标
		/// </summary>
		public int Left
		{
			get
			{
				return _cellLeft;
			}
			set
			{	
				this._cellLeft = value;
			}
		}

		/// <summary>
		/// 单元格起点Y坐标
		/// </summary>
		public int Top
		{
			get
			{
				return _cellTop;
			}
			set
			{	
				this._cellTop = value;
			}
		}

		/// <summary>
		/// 单元格宽
		/// </summary>
		public int Width
		{
			get
			{
				return _cellWidth;
			}
			set
			{	
				this._cellWidth = value;
			}
		}

		/// <summary>
		/// 单元格高
		/// </summary>
		public int Height
		{
			get
			{
				return _cellHeight;
			}
			set
			{	
				this._cellHeight = value;
			}
		}

	}//End Class
}//End NameSpace
