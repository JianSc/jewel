using System;
using System.Windows.Forms;

namespace GoldPrinter
{
	//注意，为支持VS2005的DataGridView方法，需要设置条件编译常数Vs2005。方法是进入配置属性\生成，将条件编译常数DEBUG;TRACE改为DEBUG;TRACE;Vs2005即可。
	//但是大家可以把此类专门放到你自己的VS2005工程中（#if Vs2005 #endif去掉），而无需放到金质打印通中，这样，金质打印通即使在VS2003下编译也可以为通用于VS2005。

	/// <summary>
	/// 提供将ListView、VS2005DataGridView转换成二维数组的方法并提取列宽，供金质打印通打印。
	/// </summary>
	public class DataGridViewListViewHelper
	{
		//打印VS2005DataGridView方法示例
//		private void btnPrintEasy_Click(object sender, System.EventArgs e)
//		{
//			GoldPrinter.MisGoldPrinter webmis = new GoldPrinter.MisGoldPrinter();   //打印组件
//			webmis.Title = "MIS金质打印通\nWWW.WebMIS.COM.CN";                      //标题，还可设置子标题
//			(webmis.Title as GoldPrinter.Title).Font = new System.Drawing.Font("宋体", 12, System.Drawing.FontStyle.Bold);
//
//			//下面这一句就可以打印DataGridView
//			//(webmis.Body as GoldPrinter.Body).DataSource = ToStringArray(dataGridView1, true);
//            
//			//为人特性化，自定义表体，可以设置字体、列宽、列对齐方式
//			GoldPrinter.Body gridBody = new GoldPrinter.Body();
//			//任意二维的数据通通打印，或者是设置GridText属性
//			gridBody.DataSource = ToStringArray(dataGridView1, true);
//			gridBody.Font = dataGridView1.Font;
//			gridBody.ColsWidth = GetColsWidth(dataGridView1);
//			webmis.Body = gridBody;
//
//			webmis.Preview();
//			webmis.Dispose();
//		}

#if Vs2005

		/// <summary>
		/// 获取VS.Net 2005 DataGridView控件的列宽。
		/// </summary>
		/// <param name="dataGridView">VS.Net 2005 DataGridView控件。</param>
		/// <returns>列宽数组。</returns>
		public static int[] GetColsWidth(DataGridView dataGridView)
		{
			#region 实现...

			int[] arrReturn = null;

			int colsCount = dataGridView.ColumnCount;

			arrReturn = new int[colsCount];
			for (int i = 0; i < colsCount ; i++)
			{
				arrReturn[i] = dataGridView.Columns[i].Width;
			}

			return arrReturn;

			#endregion 实现
		}

		/// <summary>
		/// 将VS.Net 2005 DataGridView控件的数据导出到二维数组。
		/// </summary>
		/// <param name="dataGridView">VS.Net 2005 DataGridView控件。</param>
		/// <param name="includeColumnText">是否要把列标题文本也导到数组中。</param>
		/// <remarks>
		///  <作者>长江支流</作者>
		///  <日期>2005-12-14</日期>
		///  <修改></修改>
		/// </remarks>
		/// <returns>二维数组。</returns>
		public static string[,] ToStringArray(DataGridView dataGridView, bool includeColumnText)
		{
			#region 实现...

			string[,] arrReturn = null;

			int rowsCount = dataGridView.Rows.Count;
			int colsCount = dataGridView.Columns.Count;

			if (rowsCount > 0)
			{
				//最后一行是供输入的行时，不用读数据。
				if (dataGridView.Rows[rowsCount - 1].IsNewRow)
				{
					rowsCount--;
				}
			}

			int i = 0;

			//包括列标题
			if (includeColumnText)
			{
				rowsCount++;
				arrReturn = new string[rowsCount, colsCount];
				for (i = 0; i < colsCount; i++)
				{
					arrReturn[0, i] = dataGridView.Columns[i].HeaderText;
				}

				i = 1;
			}
			else
			{
				arrReturn = new string[rowsCount, colsCount];
			}

			//读取单元格数据
			int rowIndex = 0;
			for (; i < rowsCount; i++, rowIndex++)
			{
				for (int j = 0; j < colsCount; j++)
				{
					arrReturn[i, j] = dataGridView.Rows[rowIndex].Cells[j].Value.ToString();
				}
			}

			return arrReturn;

			#endregion 实现
		}
#endif

		/// <summary>
		/// 获取ListView控件的列宽。
		/// </summary>
		/// <param name="listView">二维数据视图</param>
		/// <returns>列宽数组。</returns>
		public static int[] GetColsWidth(ListView listView)
		{
			#region 实现...

			int[] arrReturn = null;

			int colsCount = listView.Columns.Count;

			arrReturn = new int[colsCount];
			for (int i = 0; i < colsCount; i++)
			{
				arrReturn[i] = listView.Columns[i].Width;
			}

			return arrReturn;

			#endregion 实现
		}

		/// <summary>
		/// 将ListView的数据导出到二维数组。
		/// </summary>
		/// <param name="listView">二维数据视图</param>
		/// <param name="includeColumnText">是否要把列标题文本也导到数组中。</param>
		/// <remarks>
		///  <作者>长江支流</作者>
		///  <日期>2005-08-21</日期>
		///  <修改></修改>
		/// </remarks>
		/// <returns>二维数组。</returns>
		public static string[,] ToStringArray(ListView listView, bool includeColumnText)
		{
			#region 实现...

			ListView lvw = listView;
			int rowsCount = lvw.Items.Count;
			int colsCount = lvw.Columns.Count;

			//包括列标题
			if (includeColumnText)
			{
				rowsCount++;
			}

			string[,] arrReturn = null;

			arrReturn = new string[rowsCount, colsCount];

			int i = 0;

			if (includeColumnText)
			{
				//写标题
				for (i = 0; i < colsCount; i++)
				{
					arrReturn[0, i] = lvw.Columns[i].Text;
				}

				i = 1;
			}

			//写数据行Items
			int rowIndex = 0;
			for (; i < rowsCount; i++, rowIndex++)
			{
				for (int j = 0; j < colsCount; j++)
				{
					arrReturn[i, j] = lvw.Items[rowIndex].SubItems[j].Text;
				}
			}

			return arrReturn;

			#endregion 实现
		}

	}//End Class
}//End Namespace
