using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// Body，数据表格主题。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class Body:Outer
	{
		public Body()
		{
			this.IsDrawAllPage = false;
			mdrawGrid.AlignMent = AlignFlag.Left;
			mdrawGrid.Border = GridBorderFlag.Single;
			mdrawGrid.Line = GridLineFlag.Both;
			this.IsAverageColsWidth = false;
			
			//不合并
			mdrawGrid.Merge = GridMergeFlag.None;
			//this.Font = new Font("宋体",12);

			mdrawGrid.Font = new Font("宋体",12);			
			mdrawGrid.PreferredRowHeight = mdrawGrid.Font.Height + 2;			
		}

		public Body(int rows,int cols):this()
		{			
			base.Initialize(rows,cols);
		}

		public string[,] GridText
		{
			set
			{
				mblnHadInitialized = true;
				mdrawGrid.GridText = value;
			}
			get
			{
				return mdrawGrid.GridText;
			}
		}

		public string ColsAlignString
		{
			set
			{
				mdrawGrid.ColsAlignString = value;
			}
			get
			{
				return mdrawGrid.ColsAlignString;
			}
		}

		public override Font Font
		{
			get
			{
				return mdrawGrid.Font;
			}
			set
			{
				mdrawGrid.Font = value;
			}
		}


	}//End Class
}//End NameSpace
