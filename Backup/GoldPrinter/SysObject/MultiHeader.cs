using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// 中国特色的多层表头。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class MultiHeader:Header
	{
		private const int CONST_MAX_ROWS = 3;
		private bool _isDrawDiagonalLine;		//是否打印第一列的对角线起点为0行0列，指定行数为终点
		private float _DiagonalLineRows;		//行数，可以是小数如1.5
		
		#region 字段属性		
		/// <summary>
		/// 是否打印第一列的对角线，要指定行数
		/// </summary>
		public bool IsDrawDiagonalLine
		{
			get
			{
				return _isDrawDiagonalLine;
			}
			set
			{
				_isDrawDiagonalLine = value;
			}
		}

		/// <summary>
		/// 对角线起点为0行0列，指定行数为终点。行数可以是小数如1.5
		/// </summary>
		public float DiagonalLineRows
		{
			get
			{
				return _DiagonalLineRows;
			}
			set
			{
				_DiagonalLineRows = value;
			}
		}
		#endregion

		public MultiHeader()
		{
			this.IsDrawAllPage = true;
			mdrawGrid.AlignMent = AlignFlag.Center;
			mdrawGrid.Border = GridBorderFlag.Single;
			mdrawGrid.Line = GridLineFlag.Both;
			this.IsAverageColsWidth = false;

			_isDrawDiagonalLine= false;
			_DiagonalLineRows = 2;
			
			//粗体显示并合并
			mdrawGrid.Merge = GridMergeFlag.Any;
			this.Font = new Font("宋体",12,FontStyle.Bold);
			
			mdrawGrid.PreferredRowHeight = this.Font.Height + 10;			
		}

		protected override int SetMaxRows()
		{
			return CONST_MAX_ROWS;
		}

		public void SetMergeTextOnRowSel(int row, int startCol,int endCol, string text)
		{			
			mdrawGrid.SetTextOnRowSel(row,startCol,endCol, text);
		}

		public void SetMergeTextOnColSel(int col, int startRow,int endRow, string text)
		{			
			mdrawGrid.SetTextOnColSel(col,startRow,endRow, text);
		}


		public MultiHeader(int rows,int cols):this()
		{
			base.Initialize(rows,cols);

			string mstrAlignment = "";

			//所有列居中对齐
			for(int i = 0 ; i < cols ; i++)
			{
				mstrAlignment += "C";
			}
			this.mdrawGrid.ColsAlignString = mstrAlignment;
		}

		/// <summary>
		/// 画对角线，仅限于第一列
		/// </summary>
		protected void DrawDiagonalLine(float rows)
		{	
			try
			{
				int x1,y1,x2,y2;

				x1 = mdrawGrid.Rectangle.X;
				y1 = mdrawGrid.Rectangle.Y;

				x2 = x1 + mdrawGrid.ColsWidth[0];
				y2 = y1 + (int)(mdrawGrid.PreferredRowHeight * this._DiagonalLineRows);			
				
				this.Graphics.SetClip(new Rectangle(x1,y1,mdrawGrid.ColsWidth[0],mdrawGrid.PreferredRowHeight * mdrawGrid.Rows));

				this.Graphics.DrawLine(Pens.Black,x1,y1,x2,y2);
			}
			catch(Exception e)
			{}
			finally
			{
				this.Graphics.ResetClip();
			}

		}

		public override void Draw()
		{
			base.Draw ();
			if (_isDrawDiagonalLine)
			{
				DrawDiagonalLine(this._DiagonalLineRows);
			}
		}


		public string ColsAlign
		{
			get
			{
				return this.mdrawGrid.ColsAlignString;
			}
			set
			{
				this.mdrawGrid.ColsAlignString = value;
			}
		}


	}//End Class
}//End NameSpace
