using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// Outer，网格体之外的对象，通常用于表头表底做表体的附加信息。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class Outer:Printer,IDisposable
	{
		//平均列宽
		private bool _IsAverageColsWidth;

		#region 字段属性
		/// <summary>
		/// 是否平均分配列宽
		/// </summary>
		public bool IsAverageColsWidth
		{
			get
			{
				return _IsAverageColsWidth;
			}
			set
			{
				_IsAverageColsWidth = value;
			}
		}
		#endregion

		//核心为网格对象，不对最终用户公开
		protected DrawGrid mdrawGrid;

		//标识是否初始了行列数，Initialize(int rows, int cols)
		//只有初始了，才能执行Draw()操作。
		protected bool mblnHadInitialized;

		public Outer()
		{
			_IsAverageColsWidth = true;

			mblnHadInitialized = false;
			this.IsDrawAllPage = false;

			mdrawGrid = new DrawGrid();
			
			mdrawGrid.AlignMent = AlignFlag.Left;
			mdrawGrid.ColsAlignString = "";
			mdrawGrid.Border = GridBorderFlag.None;
			mdrawGrid.Line = GridLineFlag.None;
			mdrawGrid.Merge = GridMergeFlag.None;

			this.Font = new Font("宋体",11);
			
			mdrawGrid.PreferredRowHeight = this.Font.Height;
			mdrawGrid.Initialize(0,0);

		}

		public object DataSource
		{
			get
			{
				return this.mdrawGrid.DataSource;
			}
			set
			{
				this.mdrawGrid.DataSource = value;

				if (DataSource.GetType().ToString() == "System.String[]" || DataSource.GetType().ToString() == "System.String[,]" || DataSource.GetType().ToString() == "System.Data.DataTable")
				{
					mblnHadInitialized = true;
				}
			}		
		}

		public string[,] Text
		{
			get
			{
				return this.mdrawGrid.GridText;
			}
			set
			{
				this.mdrawGrid.GridText = value;
				mblnHadInitialized = true;
			}		
		}

		/// <summary>
		/// 获取是否能执行绘制操作，只有初始了对象的行列数才可以执行Draw()操作
		/// </summary>
		/// <returns></returns>
		public bool CanDraw
		{
			get
			{
				return this.mblnHadInitialized;
			}
		}

		public int RowHeight
		{
			get
			{
				return mdrawGrid.PreferredRowHeight;
			}
			set
			{
				mdrawGrid.PreferredRowHeight = value;			
			}
		
		}

		/// <summary>
		/// 初始行列数
		/// </summary>
		/// <param name="rows">初始对象的行数</param>
		/// <param name="cols">初始对象的列数</param>
		public virtual void Initialize(int rows, int cols)
		{
			mblnHadInitialized = true;
			mdrawGrid.Initialize(rows,cols);
		}

		/// <summary>
		/// 获取对象的行数
		/// </summary>
		public int Rows
		{
			get
			{
				return mdrawGrid.Rows;
			}
		}

		/// <summary>
		/// 获取对象的列数
		/// </summary>
		public int Cols
		{
			get
			{
				return mdrawGrid.Cols;
			}
		}

		/// <summary>
		/// 获取或设置对象的列宽
		/// </summary>
		public int[] ColsWidth
		{
			get
			{
				return mdrawGrid.ColsWidth;
			}
			set
			{
				mdrawGrid.ColsWidth = value;
			}
	
	}

		/// <summary>
		/// 获取对象的高
		/// </summary>
		public override int Height
		{
			get
			{
				return mdrawGrid.Rows * mdrawGrid.PreferredRowHeight;
			}
		}

		/// <summary>
		/// 为对象指定单元设置文本值
		/// </summary>
		/// <param name="row">单元行</param>
		/// <param name="col">单元列</param>
		/// <param name="text">文本值</param>
		public virtual void SetText(int row, int col, string text)
		{
			mdrawGrid.SetText(row,col,text);
		}

        
		/// <summary>
		/// 用指定的行列分隔分隔的一串字符串，些操作默认已执行初始行列数
		/// </summary>
		/// <param name="text"></param>
		/// <param name="colSplit"></param>
		public virtual void SetText(char rowSplit,char colSplit,string text)
		{
			mdrawGrid.SetText(rowSplit,colSplit,text);

			//mblnHadInitialized = true;
		}

		/// <summary>
		/// 获取对象指定单元文本值
		/// </summary>
		/// <param name="row">单元行</param>
		/// <param name="col">单元列</param>
		/// <returns></returns>
		public virtual string GetText(int row, int col)
		{
			return mdrawGrid.GetText(row,col);		
		}

		/// <summary>
		/// 在绘图表面绘制对象绘制文本
		/// </summary>
		public override void Draw()
		{
			if (mblnHadInitialized)
			{
				base.Draw();

				//在指定的区域内绘制文本				
				mdrawGrid.Rectangle = new Rectangle((int)this.Rectangle.X + (int)this.MoveX,(int)this.Rectangle.Y  + (int)this.MoveY,(int)this.Rectangle.Width,(int)this.Rectangle.Height);
				mdrawGrid.Graphics = this.Graphics;

				if (this._IsAverageColsWidth)
				{
					mdrawGrid.Width = mdrawGrid.Rectangle.Width;
					mdrawGrid.ColsWidth = mdrawGrid.GetAverageColsWidth();
				}

				mdrawGrid.Draw();
			}
			else
			{
				throw new Exception("对象的行列数还未初始，请用Initialize（）进行操作！");
			}
		}

        #region IDisposable 成员

        public override void Dispose()
        {
            this.mdrawGrid.Dispose();
        }

        #endregion

    }//End Class
}//End NameSpace