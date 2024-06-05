using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// Header，紧挨在网格体上的对象，限制在10行内，列数不限。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class Header:Outer
	{
		private const int CONST_MAX_ROWS = 10;
		private readonly int MAX_ROWS;

		public Header()
		{
			MAX_ROWS = SetMaxRows();
		}

		protected virtual int SetMaxRows()
		{
			return CONST_MAX_ROWS;
		}

		public Header(int rows,int cols):this()
		{
			this.Initialize(rows,cols);
		}

		public override void Initialize(int rows, int cols)
		{
			int mrows = rows;

			if (mrows < 0)
			{
				mrows = 0;
			}

			if (mrows > MAX_ROWS)
			{
				throw new Exception("行数限制在“" + MAX_ROWS.ToString() + "”行以内！");
			}
			else
			{
				base.Initialize(mrows,cols);
			}		
		}


	}//End Class
}//End NameSpace
