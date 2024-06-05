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
	public class ExcelBase
	{
		private Excel.Application _xlApp;							//Excel应用程序
		private Excel.Workbook _xlWorkbook;							//Excel工作薄，默认只有一个，用Open([Template])创建

		private bool _IsVisibledExcel;								//打印或预览时是否还要显示Excel窗体
		private string _FormCaption;								//打印预览Excel窗体的标题栏

		private Object oMissing = System.Reflection.Missing.Value;  //实例化参数对象

		#region _xlApp、_xlWorkbook、IsVisibledExcel、FormCaption属性
		/// <summary>
		/// Excel应用程序
		/// </summary>
		public Excel.Application Application
		{
			get
			{
				return _xlApp;
			}
		}

		/// <summary>
		/// Excel工作薄，默认只有一个，用Open([Template])创建
		/// </summary>
		public Excel.Workbook Workbooks
		{
			get
			{
				return _xlWorkbook;
			}
		}

		/// <summary>
		/// 打印或预览时是否还要显示Excel窗体
		/// </summary>
		public bool IsVisibledExcel
		{
			get
			{
				return _IsVisibledExcel;
			}
			set
			{
				_IsVisibledExcel = value;
			}		
		}

		/// <summary>
		/// 打印预览Excel窗体的标题栏
		/// </summary>
		public string FormCaption
		{
			get
			{
				return _FormCaption;
			}
			set
			{
				_FormCaption = value;
			}
		}

		#endregion		

		/// <summary>
		/// 创建立Excel新的实例
		/// </summary>
		public ExcelBase()
		{
			_IsVisibledExcel = false;				//打印及预览时Excel显示
			_FormCaption = "打印预览";

			//应检查Excell进程是否已在运行，否则每次实例化一个，则Excell进程多一个。现在在Close()里进行强制垃圾回收，可以不检测了。
			try
			{
				_xlApp = new Excel.ApplicationClass();
			}
			catch(System.Exception ex)
			{
				throw new ExceptionExcelCreateInstance("创建Excel类实例时错误，详细信息：" + ex.Message);
			}

			_xlApp.DisplayAlerts = false;			 //关闭程序建立的Excel文件时，不会提示是否要保存修改
		}

		#region 打开关闭
		/// <summary>
		/// 打开Excel，并建立默认的Workbooks。
		/// </summary>
		/// <returns></returns>
		public void Open()
		{	
			//打开并新建立默认的Excel
			//Workbooks.Add([template]) As Workbooks

			try
			{
				_xlWorkbook = _xlApp.Workbooks.Add(oMissing);				
			}
			catch(System.Exception ex)
			{
				throw new ExceptionExcelOpen("打开Excel时错误，详细信息：" + ex.Message);
			}

		}

		/// <summary>
		/// 根据现有工作薄模板打开，如果指定的模板不存在，则用默认的空模板
		/// </summary>
		/// <param name="p_templateFileName">用作模板的工作薄文件名</param>
		public void Open(string p_templateFileName)
		{	
			if (System.IO.File.Exists(p_templateFileName))
			{
				//用模板打开
				//Workbooks.Add Template:="C:\tpt.xlt"

				try
				{
					_xlWorkbook = _xlApp.Workbooks.Add(p_templateFileName);	
				}
				catch(System.Exception ex)
				{
					throw new ExceptionExcelOpen("打开Excel时错误，详细信息：" + ex.Message);
				}
			}
			else
			{
				Open();
			}
		}

		/// <summary>
		/// 关闭
		/// </summary>
		public void Close()
		{
			
			_xlApp.Workbooks.Close();
			_xlWorkbook = null;

			_xlApp.Quit();
			_xlApp = null;

			oMissing = null;

			//强制垃圾回收，否则每次实例化Excel，则Excell进程多一个。
			System.GC.Collect();
		}
		#endregion

		#region PrintPreview()、Print()用Excel打印、预览，如果要显示Excel窗口，请设置IsVisibledExcel 
		/// <summary>
		/// 显示Excel
		/// </summary>
		public void ShowExcel()
		{			
			_xlApp.Visible = true;
		}
			
		/// <summary>
		/// 用Excel打印预览，如果要显示Excel窗口，请设置IsVisibledExcel 
		/// </summary>
		public void PrintPreview()
		{			
			_xlApp.Caption = _FormCaption;
			_xlApp.Visible = true;

			try
			{	
				_xlApp.ActiveWorkbook.PrintPreview(oMissing);
			}
			catch{}

			_xlApp.Visible = this.IsVisibledExcel;		

		}

		/// <summary>
		/// 用Excel打印，如果要显示Excel窗口，请设置IsVisibledExcel 
		/// </summary>
		public void Print()
		{
			_xlApp.Visible = this.IsVisibledExcel;	

			Object oMissing = System.Reflection.Missing.Value;  //实例化参数对象
			try
			{
				_xlApp.ActiveWorkbook.PrintOut(oMissing,oMissing,oMissing,oMissing,oMissing,oMissing,oMissing,oMissing);	
			}
			catch{}
		}
		#endregion

		#region 另存
		/// <summary>
		/// 另存。如果保存成功，则返回true，否则，如果保存不成功或者如果已存在文件但是选择了不替换也返回false
		/// </summary>
		/// <param name="p_fileName">将要保存的文件名</param>
		/// <param name="p_ReplaceExistsFileName">如果文件存在，则替换</param>
		public bool SaveAs(string p_fileName,bool p_ReplaceExistsFileName)
		{
			bool blnReturn = false;
			if (System.IO.File.Exists(p_fileName))
			{
				if (p_ReplaceExistsFileName)
				{
					try
					{
						System.IO.File.Delete(p_fileName);
						blnReturn = true;
					}
					catch(Exception ex)
					{	
						string strErr = ex.Message; 
					}
				}
			}

			try
			{
				
				_xlApp.ActiveWorkbook.SaveCopyAs(p_fileName);
				blnReturn = true;
			}
			catch
			{
				blnReturn = false;
			}

			return blnReturn;
		}
		#endregion


		//核心函数，GetRange()，获取指定范围内的单元格
		/*
		public Excel.Range GetRange(int p_rowIndex,int p_colIndex)
		public Excel.Range GetRange(int p_rowIndex,string p_colChars)
		public Excel.Range GetRange(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex)
		public Excel.Range GetRange(int p_startRowIndex,string p_startColChars,int p_endRowIndex,string p_endColChars)
		*/
		
		#region GetRange，如Range("B10")，Range("C8:F11")，Range(2,10)，Range(2,"A")，Range(3,8,6,11)，Range(3,"A",6,"F")
		/// <summary>
		/// 获取指定单元格或指定范围内的单元格，行索引为从1开始的数字，最大65536，列索引为A~Z、AA~AZ、BA~BZ...HA~HZ、IA~IV的字母及组合，也可以是1-65536数字。
		/// </summary>
		/// <param name="p_rowIndex">单元格行索引，从1开始</param>
		/// <param name="p_colIndex">单元格列索引，从1开始，列索引也可以用字母A到Z或字母组合AA~AZ，最大IV的Excel字母索引</param>
		/// <returns></returns>
		public Excel.Range GetRange(int p_rowIndex,int p_colIndex)
		{
			//单个	Range(10,3).Select		//第10行3列
			return GetRange(p_rowIndex,p_colIndex,p_rowIndex,p_colIndex);
		}

		/// <param name="p_colChars">单元格列字母及组合索引，从A开始</param>
		public Excel.Range GetRange(int p_rowIndex,string p_colChars)
		{
			//单个	Range("C10").Select		//第10行3列			
			return GetRange(p_rowIndex,p_colChars,p_rowIndex,p_colChars);
		}

		/// <param name="p_startRowIndex">指定单元范围起始行索引，从1开始</param>
		/// <param name="p_startColIndex">指定单元范围起始列数字索引，从1开始</param>
		/// <param name="p_endRowIndex">指定单元范围结束行索引</param>
		/// <param name="p_endColIndex">指定单元范围结束列数字索引</param>
		public Excel.Range GetRange(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex)
		{
			Excel.Range range;
			range = _xlApp.get_Range(_xlApp.Cells[p_startRowIndex,p_startColIndex],_xlApp.Cells[p_endRowIndex,p_endColIndex]);
						
			return range;
		}

		/// <param name="p_startChars">指定单元范围起始列字母及组合索引</param>
		/// <param name="p_endChars">指定单元范围结束列字母及组合索引</param>
		public Excel.Range GetRange(int p_startRowIndex,string p_startColChars,int p_endRowIndex,string p_endColChars)
		{
			//矩形	Range("D8:F11").Select
			Excel.Range range;
			
			range = _xlApp.get_Range(p_startColChars + p_startRowIndex.ToString(),p_endColChars + p_endRowIndex.ToString());
						
			return range;
		}
		#endregion

		#region MergeCells(Excel.Range p_Range)合并单元格，合并后，默认居中
		/// <summary>
		/// 合并指定范围内单元格，合并后，默认居中
		/// </summary>
		/// <param name="p_Range"></param>
		public void MergeCells(Excel.Range p_Range)
		{
			p_Range.HorizontalAlignment = Excel.Constants.xlCenter;
			p_Range.VerticalAlignment = Excel.Constants.xlCenter;
			p_Range.WrapText = false;
			p_Range.Orientation = 0;
			p_Range.AddIndent = false;
			p_Range.IndentLevel = 0;
			p_Range.ShrinkToFit = false;
			//p_Range.ReadingOrder = Excel.Constants.xlContext;
			p_Range.MergeCells = false;
			p_Range.Merge(oMissing);		

			//    With Selection
			//        .HorizontalAlignment = xlCenter
			//        .VerticalAlignment = xlCenter
			//        .WrapText = False
			//        .Orientation = 0
			//        .AddIndent = False
			//        .IndentLevel = 0
			//        .ShrinkToFit = False
			//        .ReadingOrder = xlContext
			//        .MergeCells = False
			//    End With
			//    Selection.Merge
		}
		#endregion


		#region 插入分页符，暂无实现
		/// <summary>
		/// 在指定的行上插入分页符
		/// </summary>
		/// <param name="p_rowIndex">行索引</param>
		public void InsertVPageBreaks(int p_rowIndex)
		{

		}

		public void InsertHPageBreaks(int p_colIndex)
		{
		
		}

		public void InsertHPageBreaks(string p_colChars)
		{		
		
		}
		#endregion

		#region 插入整行、整列InsertRow(int p_rowIndex)、InsertColumn(int p_colIndex)、InsertColumn(string p_colChars)
		/// <summary>
		/// 在指定的行上插入一整行
		/// </summary>
		/// <param name="p_rowIndex">行索引</param>
		public void InsertRow(int p_rowIndex)
		{
			//    Rows("2:2").Select
			//    Selection.Insert Shift:=xlDown

			Excel.Range range;

			range = GetRange(p_rowIndex,"A");
			range.Select();

			//Excel2003支持两参数
			//range.EntireRow.Insert(oMissing,oMissing);			

			//Excel2000支持一个参数，经过测试，用Interop.ExcelV1.3(Excel2000)，可以正常运行在Excel2003中
			range.EntireRow.Insert(oMissing);
		}

		/// <summary>
		/// 用模板行在指定的行上插入，即Excel的插入复制单元格
		/// </summary>
		/// <param name="p_rowIndex"></param>
		/// <param name="p_templateRowIndex"></param>
		public void InsertRow(int p_rowIndex,int p_templateRowIndex)
		{
			Excel.Range range;
			range = (Excel.Range)_xlApp.Rows[p_templateRowIndex.ToString() + ":" + p_templateRowIndex.ToString(),oMissing];
			range.Select();
			range.Copy(oMissing);

			InsertRow(p_rowIndex);
		}

		/// <summary>
		/// 在指定的列上插入一整列
		/// </summary>
		/// <param name="p_colIndex">列索引</param>
		public void InsertColumn(int p_colIndex)
		{
			Excel.Range range;

			range = GetRange(1,p_colIndex);
			range.Select();
			
			//Excel2003支持两参数
			//range.EntireColumn.Insert(oMissing,oMissing);				
			//Excel2000支持一个参数
			range.EntireColumn.Insert(oMissing);		
		}

		/// <summary>
		/// 在指定的列上插入一整列
		/// </summary>
		/// <param name="p_colChars">列字母或组合</param>
		public void InsertColumn(string p_colChars)
		{
			Excel.Range range;

			range = GetRange(1,p_colChars);
			range.Select();
			//Excel2003支持两参数
			//range.EntireColumn.Insert(oMissing,oMissing);		
			//Excel2000支持一个参数
			range.EntireColumn.Insert(oMissing);		
		}
		#endregion

		#region 删除整行、整列DeleteRow(int p_rowIndex)、DeleteColumn(int p_colIndex)、DeleteColumn(string p_colChars)
		/// <summary>
		/// 删除指定的整行
		/// </summary>
		/// <param name="p_rowIndex">行索引</param>
		public void DeleteRow(int p_rowIndex)
		{
			Excel.Range range;

			range = GetRange(p_rowIndex,"A");
			range.Select();
			range.EntireRow.Delete(oMissing);			
		}

		/// <summary>
		/// 删除指定的整列
		/// </summary>
		/// <param name="p_colIndex">列索引</param>
		public void DeleteColumn(int p_colIndex)
		{
			Excel.Range range;

			range = GetRange(1,p_colIndex);
			range.Select();
			range.EntireColumn.Delete(oMissing);		
		}

		/// <summary>
		/// 删除指定的整列
		/// </summary>
		/// <param name="p_colChars">列字母或组合</param>
		public void DeleteColumn(string p_colChars)
		{
			Excel.Range range;

			range = GetRange(1,p_colChars);
			range.Select();
			range.EntireColumn.Delete(oMissing);		
		}
		#endregion
		
		#region 设置行高列宽SetRowHeight(int p_rowIndex,float p_rowHeight)、SetColumnWidth(int p_colIndex,float p_colWidth)、SetColumnWidth(string p_colChars,float p_colWidth)
		public void SetRowHeight(int p_rowIndex,float p_rowHeight)
		{
			Excel.Range range;

			range = GetRange(p_rowIndex,"A");
			range.Select();
			range.RowHeight = p_rowHeight;
		}

		public void SetColumnWidth(int p_colIndex,float p_colWidth)
		{
			Excel.Range range;

			range = GetRange(1,p_colIndex);
			range.Select();
			range.ColumnWidth = p_colWidth;		
		}

		public void SetColumnWidth(string p_colChars,float p_colWidth)
		{
			Excel.Range range;

			range = GetRange(1,p_colChars);
			range.Select();
			range.ColumnWidth = p_colWidth;		
		}
		#endregion 


		#region SetFont(Excel.Range p_Range,Font p_Font[,Color p_color])
		public void SetFont(Excel.Range p_Range,Font p_Font)
		{
			SetFont(p_Range,p_Font,Color.Black);
		}

		public void SetFont(Excel.Range p_Range,Font p_Font,Color p_color)
		{
			p_Range.Select();
			p_Range.Font.Name = p_Font.Name;
			p_Range.Font.Size = p_Font.Size;

			//p_Range.Font.Color = p_color;

			p_Range.Font.Bold = p_Font.Bold;
			p_Range.Font.Italic = p_Font.Italic;

			p_Range.Font.Strikethrough = p_Font.Strikeout;
			p_Range.Font.Underline = p_Font.Underline;		
		}
		#endregion 
		
		#region SetBordersEdge 设置指定范围边框（左、顶、右、底、往右下对角线、往右上对角线、内部水平线、内部垂直线、无线）线，并可指定线条的样式（无、虚线、点线等）及线粗细
		/// <summary>
		/// 用连续的普通粗细的线设置指定范围内的边界
		/// </summary>
		/// <param name="p_Range"></param>
		/// <param name="p_BordersEdge"></param>
		public void SetBordersEdge(Excel.Range p_Range,BordersEdge p_BordersEdge)
		{
			SetBordersEdge(p_Range,p_BordersEdge,BordersLineStyle.xlContinuous,BordersWeight.xlThin);
		}

		public void SetBordersEdge(Excel.Range p_Range,BordersEdge p_BordersEdge,BordersLineStyle p_BordersLineStyle,BordersWeight p_BordersWeight)
		{
			p_Range.Select();

			Excel.Border border = null;
			
			switch(p_BordersEdge)
			{	
					//左右顶底的线
				case BordersEdge.xlLeft:										
					border = p_Range.Borders[Excel.XlBordersIndex.xlEdgeLeft];
					break;
				case BordersEdge.xlRight:
					border = p_Range.Borders[Excel.XlBordersIndex.xlEdgeRight];
					break;
				case BordersEdge.xlTop:
					border =p_Range.Borders[Excel.XlBordersIndex.xlEdgeTop];
					break;
				case BordersEdge.xlBottom:
					border =p_Range.Borders[Excel.XlBordersIndex.xlEdgeBottom];
					break;
					//对角线
				case BordersEdge.xlDiagonalDown:			
					border =p_Range.Borders[Excel.XlBordersIndex.xlDiagonalDown];
					break;
				case BordersEdge.xlDiagonalUp:
					border =p_Range.Borders[Excel.XlBordersIndex.xlDiagonalUp];
					break;
					//边框内部是横竖线(不包括边框)
				case BordersEdge.xlInsideHorizontal:
					border =p_Range.Borders[Excel.XlBordersIndex.xlInsideHorizontal];
					break;
				case BordersEdge.xlInsideVertical:
					border =p_Range.Borders[Excel.XlBordersIndex.xlInsideVertical];
					break;
				case BordersEdge.xlLineStyleNone:
					//所先范围内所有线都没有
					p_Range.Borders[Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Excel.XlLineStyle.xlLineStyleNone;		//xlNone
					p_Range.Borders[Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
					p_Range.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
					p_Range.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
					p_Range.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
					p_Range.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
					p_Range.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
					p_Range.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlLineStyleNone;				
					break;
			}

			if (border != null)
			{
				//XlLineStyle
				Excel.XlLineStyle mXlLineStyle = Excel.XlLineStyle.xlContinuous;
				switch(p_BordersLineStyle)
				{
					case BordersLineStyle.xlContinuous:
						mXlLineStyle = Excel.XlLineStyle.xlContinuous;
						break;
					case BordersLineStyle.xlDash:
						mXlLineStyle = Excel.XlLineStyle.xlDash;
						break;
					case BordersLineStyle.xlDashDot:
						mXlLineStyle = Excel.XlLineStyle.xlDashDot;
						break;
					case BordersLineStyle.xlDashDotDot:
						mXlLineStyle = Excel.XlLineStyle.xlDashDotDot;
						break;
					case BordersLineStyle.xlDot:
						mXlLineStyle = Excel.XlLineStyle.xlDot;
						break;
					case BordersLineStyle.xlDouble:
						mXlLineStyle = Excel.XlLineStyle.xlDouble;
						break;
					case BordersLineStyle.xlLineStyleNone:
						mXlLineStyle = Excel.XlLineStyle.xlLineStyleNone;
						break;
					case BordersLineStyle.xlSlantDashDot:
						mXlLineStyle = Excel.XlLineStyle.xlSlantDashDot;
						break;
				}
				border.LineStyle = mXlLineStyle;
				
				//XlBorderWeight
				Excel.XlBorderWeight mXlBorderWeight = Excel.XlBorderWeight.xlThin;
				
				switch(p_BordersWeight)
				{
					case BordersWeight.xlHairline:
						mXlBorderWeight = Excel.XlBorderWeight.xlHairline;
						break;
					case BordersWeight.xlMedium:
						mXlBorderWeight = Excel.XlBorderWeight.xlMedium;
						break;
					case BordersWeight.xlThick:
						mXlBorderWeight = Excel.XlBorderWeight.xlThick;
						break;
					case BordersWeight.xlThin:
						mXlBorderWeight = Excel.XlBorderWeight.xlThin;
						break;
				}
				border.Weight = mXlBorderWeight;
				
			}//End IF

		}
		#endregion 

		#region ClearBordersEdge，清除指定范围内的所有线，以SetBordersEdge设置边框为基础
		public void ClearBordersEdge(Excel.Range p_Range)
		{
			SetBordersEdge(p_Range,BordersEdge.xlLineStyleNone);			
		}
		#endregion

		#region GetCellText(p_Range])
		public string GetCellText(Excel.Range p_Range)
		{
			string strReturn = "";
			strReturn = p_Range.Text.ToString();	
			return strReturn;	
		}
		#endregion


		#region SetCellText(Range)
		public void SetCellText(Excel.Range p_Range,string p_text)
		{
			p_Range.Cells.FormulaR1C1 = p_text;
		}
		#endregion

	}//End class
}//End Namespace