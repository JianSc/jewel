using System;
using System.Data;
using System.Drawing;
using GoldPrinter.ExcelConstants;

namespace GoldPrinter
{
	/// <summary>
	/// 该类主要定义Excel的程序对象，启动Excel并打印及保存。可能依赖于Interop.VBIDE及Interop.Microsoft.Office.Core，如果需要，请加上。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class ExcelAccess:ExcelBase
	{
		public ExcelAccess()
		{

		}

		//核心函数，GetRange()，获取指定范围内的单元格
		/*
		public Excel.Range GetRange(int p_rowIndex,int p_colIndex)
		public Excel.Range GetRange(int p_rowIndex,string p_colChars)
		public Excel.Range GetRange(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex)
		public Excel.Range GetRange(int p_startRowIndex,string p_startColChars,int p_endRowIndex,string p_endColChars)
		*/
		
		#region MergeCells()合并单元格，合并后，默认居中，用Range或它的指定范围作为参数
		/// <summary>
		/// 合并指定范围内单元格
		/// </summary>
		/// <param name="p_rowIndex">行索引，也可以指定起始行、终止行索引</param>
		/// <param name="p_colIndex">列索引，也可以指定起始列、终止列数字或字母及组合索引</param>
		/// 

		/// <summary>
		/// 合并指定范围内单元格
		/// </summary>
		/// <param name="p_startRowIndex">起始行索引</param>
		/// <param name="p_startColIndex">起始列索引，可以是数字或字母及组合索引</param>
		/// <param name="p_endRowIndex">结束行索引</param>
		/// <param name="p_endColIndex">结束列索引，可以是数字或字母及组合索引</param>
		public void MergeCells(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex)
		{
			MergeCells(GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex));	
		}

		/// <summary>
		/// 合并指定范围内单元格
		/// </summary>
		/// <param name="p_startRowIndex">起始行索引</param>
		/// <param name="p_startColChars">起始列索引，可以是数字或字母及组合索引</param>
		/// <param name="p_endRowIndex">结束行索引</param>
		/// <param name="p_endColChars">结束列索引，可以是数字或字母及组合索引</param>
		public void MergeCells(int p_startRowIndex,string p_startColChars,int p_endRowIndex,string p_endColChars)
		{
			MergeCells(GetRange(p_startRowIndex,p_startColChars,p_endRowIndex,p_endColChars));			
		}
		#endregion

		#region SetFont(Excel.Range p_Range,Font p_Font[,Color p_color])
		
		public void SetFont(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex,Font p_Font)
		{
			SetFont(GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex),p_Font,Color.Black);
		}
		#endregion 
		
		#region SetBordersEdge 设置指定范围边框（左、顶、右、底、往右下对角线、往右上对角线、内部水平线、内部垂直线、无线）线，并可指定线条的样式（无、虚线、点线等）及线粗细
		/// <summary>
		/// 设置指定范围边框（左、顶、右、底、往右下对角线、往右上对角线、内部水平线、内部垂直线、无线）线，并可指定线的样式及线粗细
		/// </summary>
		/// <param name="p_rowIndex">行索引，也可以指定起始行、终止行索引</param>
		/// <param name="p_colIndex">列索引，也可以指定起始列、终止列数字或字母及组合索引</param>
		/// <param name="p_BordersEdge">边框：左、顶、右、底、往右下对角线、往右上对角线、内部水平线、内部垂直线、无线</param>
		/// <param name="p_BordersLineStyle">线条样式：无、虚线、点线等，看Excel便知</param>
		/// <param name="p_BordersWeight">粗细</param>
		public void SetBordersEdge(int p_rowIndex,int p_colIndex,BordersEdge p_BordersEdge,BordersLineStyle p_BordersLineStyle,BordersWeight p_BordersWeight)
		{
			SetBordersEdge(GetRange(p_rowIndex,p_colIndex),p_BordersEdge,p_BordersLineStyle,p_BordersWeight);
		}

		public void SetBordersEdge(int p_rowIndex,string p_colChars,BordersEdge p_BordersEdge,BordersLineStyle p_BordersLineStyle,BordersWeight p_BordersWeight)
		{
			SetBordersEdge(GetRange(p_rowIndex,p_colChars),p_BordersEdge,p_BordersLineStyle,p_BordersWeight);		
		}

		public void SetBordersEdge(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex,BordersEdge p_BordersEdge,BordersLineStyle p_BordersLineStyle,BordersWeight p_BordersWeight)
		{
			SetBordersEdge(GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex),p_BordersEdge,p_BordersLineStyle,p_BordersWeight);	
		}

		public void SetBordersEdge(int p_startRowIndex,string p_startColChars,int p_endRowIndex,string p_endColChars,BordersEdge p_BordersEdge,BordersLineStyle p_BordersLineStyle,BordersWeight p_BordersWeight)
		{
			SetBordersEdge(GetRange(p_startRowIndex,p_startColChars,p_endRowIndex,p_endColChars),p_BordersEdge,p_BordersLineStyle,p_BordersWeight);			
		}

		/// <summary>
		/// 设置指定范围内边界及内部网格线
		/// </summary>
		/// <param name="p_startRowIndex"></param>
		/// <param name="p_startColIndex"></param>
		/// <param name="p_endRowIndex"></param>
		/// <param name="p_endColIndex"></param>
		/// <param name="p_endColIndex">IsBordersOrBordersGrid，true只输出四周的边框，否则输出边框与网格线</param>
		public void SetBordersEdge(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex,bool IsBordersOrBordersGrid)
		{
			SetBordersEdge(GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex),BordersEdge.xlLeft);
			SetBordersEdge(GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex),BordersEdge.xlTop);
			SetBordersEdge(GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex),BordersEdge.xlRight);
			SetBordersEdge(GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex),BordersEdge.xlBottom);

			if (!IsBordersOrBordersGrid)
			{
				SetBordersEdge(GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex),BordersEdge.xlInsideHorizontal);
				SetBordersEdge(GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex),BordersEdge.xlInsideVertical);		
			}
		}
		#endregion 

		#region ClearBordersEdge，清除指定范围内的所有线，以SetBordersEdge设置边框为基础
		/// <summary>
		/// 清除用SetBordersEdge设置的边框内的所有线
		/// </summary>
		/// <param name="p_rowIndex">行索引，也可以指定起始行、终止行索引</param>
		/// <param name="p_colIndex">列索引，也可以指定起始列、终止列数字或字母及组合索引</param>
		public void ClearBordersEdge(int p_rowIndex,int p_colIndex)
		{
			SetBordersEdge(GetRange(p_rowIndex,p_colIndex),BordersEdge.xlLineStyleNone);
		}

		public void ClearBordersEdge(int p_rowIndex,string p_colChars)
		{
			SetBordersEdge(GetRange(p_rowIndex,p_colChars),BordersEdge.xlLineStyleNone);		
		}

		public void ClearBordersEdge(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex)
		{
			SetBordersEdge(GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex),BordersEdge.xlLineStyleNone);	
		}

		public void ClearBordersEdge(int p_startRowIndex,string p_startColChars,int p_endRowIndex,string p_endColChars)
		{
			SetBordersEdge(GetRange(p_startRowIndex,p_startColChars,p_endRowIndex,p_endColChars),BordersEdge.xlLineStyleNone);			
		}
		#endregion

		#region GetCellText(int p_rowIndex,int p_colIndex[/string p_colChars])，用Range或它的指定范围作为参数
		public string GetCellText(int p_rowIndex,int p_colIndex)
		{
			string strReturn = "";
			Excel.Range range;

			range = GetRange(p_rowIndex,p_colIndex);

			strReturn = range.Text.ToString();	

			range = null;

			return strReturn;	
		}

		public string GetCellText(int p_rowIndex,string p_colChars)
		{
			string strReturn = "";
			Excel.Range range;

			range = GetRange(p_rowIndex,p_colChars);

			strReturn = range.Text.ToString();	

			range = null;

			return strReturn;	
		}
		#endregion


		#region SetCellText(...)，参数对应于Range(...)，可以一个单元格也可以区域内的单元格一起设置同样的文本。用Range或它的指定范围作为参数
		public void SetCellText(int p_rowIndex,int p_colIndex,string p_text)
		{
			//			xlApp.Cells[p_rowIndex,p_colIndex] = p_text;			
			Excel.Range range;	
			range = GetRange(p_rowIndex,p_colIndex);
			range.Cells.FormulaR1C1 = p_text;
			range = null;
		}

		public void SetCellText(int p_rowIndex,string p_colChars,string p_text)
		{
			Excel.Range range;	
			range = GetRange(p_rowIndex,p_colChars);
			range.Cells.FormulaR1C1 = p_text;
			range = null;
		}		
		
		public void SetCellText(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex,string p_text)
		{
			Excel.Range range;	
			range = GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex);
			range.Cells.FormulaR1C1 = p_text;
			range = null;
		}

		public void SetCellText(int p_startRowIndex,string p_startColChars,int p_endRowIndex,string p_endColChars,string p_text)
		{
			Excel.Range range;	
			range = GetRange(p_startRowIndex,p_startColChars,p_endRowIndex,p_endColChars);
			range.Cells.FormulaR1C1 = p_text;
			range = null;
		}
		#endregion


		public void SetCellText(DataTable p_DataTable,int p_startExcelRowIndex,int p_startExcelColIndex,bool IsDrawGridLine)
		{
			
			for(int i=0;i< p_DataTable.Rows.Count;i++)
			{
				for(int j=0;j<p_DataTable.Columns.Count;j++)
				{
					SetCellText(p_startExcelRowIndex + i , p_startExcelColIndex + j ,p_DataTable.Rows[i][j].ToString());
				}			
			}	
			if (IsDrawGridLine)
			{
				SetBordersEdge(p_startExcelRowIndex,p_startExcelColIndex,p_startExcelRowIndex + p_DataTable.Rows.Count - 1,p_startExcelColIndex + p_DataTable.Columns.Count - 1,false);
			}
		}

	}//End class
}//End Namespace