using System;
using System.Data;
using System.Drawing;
using GoldPrinter.ExcelConstants;

namespace GoldPrinter
{
	/// <summary>
	/// ������Ҫ����Excel�ĳ����������Excel����ӡ�����档����������Interop.VBIDE��Interop.Microsoft.Office.Core�������Ҫ������ϡ�
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class ExcelBase
	{
		private Excel.Application _xlApp;							//ExcelӦ�ó���
		private Excel.Workbook _xlWorkbook;							//Excel��������Ĭ��ֻ��һ������Open([Template])����

		private bool _IsVisibledExcel;								//��ӡ��Ԥ��ʱ�Ƿ�Ҫ��ʾExcel����
		private string _FormCaption;								//��ӡԤ��Excel����ı�����

		private Object oMissing = System.Reflection.Missing.Value;  //ʵ������������

		#region _xlApp��_xlWorkbook��IsVisibledExcel��FormCaption����
		/// <summary>
		/// ExcelӦ�ó���
		/// </summary>
		public Excel.Application Application
		{
			get
			{
				return _xlApp;
			}
		}

		/// <summary>
		/// Excel��������Ĭ��ֻ��һ������Open([Template])����
		/// </summary>
		public Excel.Workbook Workbooks
		{
			get
			{
				return _xlWorkbook;
			}
		}

		/// <summary>
		/// ��ӡ��Ԥ��ʱ�Ƿ�Ҫ��ʾExcel����
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
		/// ��ӡԤ��Excel����ı�����
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
		/// ������Excel�µ�ʵ��
		/// </summary>
		public ExcelBase()
		{
			_IsVisibledExcel = false;				//��ӡ��Ԥ��ʱExcel��ʾ
			_FormCaption = "��ӡԤ��";

			//Ӧ���Excell�����Ƿ��������У�����ÿ��ʵ����һ������Excell���̶�һ����������Close()�����ǿ���������գ����Բ�����ˡ�
			try
			{
				_xlApp = new Excel.ApplicationClass();
			}
			catch(System.Exception ex)
			{
				throw new ExceptionExcelCreateInstance("����Excel��ʵ��ʱ������ϸ��Ϣ��" + ex.Message);
			}

			_xlApp.DisplayAlerts = false;			 //�رճ�������Excel�ļ�ʱ��������ʾ�Ƿ�Ҫ�����޸�
		}

		#region �򿪹ر�
		/// <summary>
		/// ��Excel��������Ĭ�ϵ�Workbooks��
		/// </summary>
		/// <returns></returns>
		public void Open()
		{	
			//�򿪲��½���Ĭ�ϵ�Excel
			//Workbooks.Add([template]) As Workbooks

			try
			{
				_xlWorkbook = _xlApp.Workbooks.Add(oMissing);				
			}
			catch(System.Exception ex)
			{
				throw new ExceptionExcelOpen("��Excelʱ������ϸ��Ϣ��" + ex.Message);
			}

		}

		/// <summary>
		/// �������й�����ģ��򿪣����ָ����ģ�岻���ڣ�����Ĭ�ϵĿ�ģ��
		/// </summary>
		/// <param name="p_templateFileName">����ģ��Ĺ������ļ���</param>
		public void Open(string p_templateFileName)
		{	
			if (System.IO.File.Exists(p_templateFileName))
			{
				//��ģ���
				//Workbooks.Add Template:="C:\tpt.xlt"

				try
				{
					_xlWorkbook = _xlApp.Workbooks.Add(p_templateFileName);	
				}
				catch(System.Exception ex)
				{
					throw new ExceptionExcelOpen("��Excelʱ������ϸ��Ϣ��" + ex.Message);
				}
			}
			else
			{
				Open();
			}
		}

		/// <summary>
		/// �ر�
		/// </summary>
		public void Close()
		{
			
			_xlApp.Workbooks.Close();
			_xlWorkbook = null;

			_xlApp.Quit();
			_xlApp = null;

			oMissing = null;

			//ǿ���������գ�����ÿ��ʵ����Excel����Excell���̶�һ����
			System.GC.Collect();
		}
		#endregion

		#region PrintPreview()��Print()��Excel��ӡ��Ԥ�������Ҫ��ʾExcel���ڣ�������IsVisibledExcel 
		/// <summary>
		/// ��ʾExcel
		/// </summary>
		public void ShowExcel()
		{			
			_xlApp.Visible = true;
		}
			
		/// <summary>
		/// ��Excel��ӡԤ�������Ҫ��ʾExcel���ڣ�������IsVisibledExcel 
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
		/// ��Excel��ӡ�����Ҫ��ʾExcel���ڣ�������IsVisibledExcel 
		/// </summary>
		public void Print()
		{
			_xlApp.Visible = this.IsVisibledExcel;	

			Object oMissing = System.Reflection.Missing.Value;  //ʵ������������
			try
			{
				_xlApp.ActiveWorkbook.PrintOut(oMissing,oMissing,oMissing,oMissing,oMissing,oMissing,oMissing,oMissing);	
			}
			catch{}
		}
		#endregion

		#region ���
		/// <summary>
		/// ��档�������ɹ����򷵻�true������������治�ɹ���������Ѵ����ļ�����ѡ���˲��滻Ҳ����false
		/// </summary>
		/// <param name="p_fileName">��Ҫ������ļ���</param>
		/// <param name="p_ReplaceExistsFileName">����ļ����ڣ����滻</param>
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


		//���ĺ�����GetRange()����ȡָ����Χ�ڵĵ�Ԫ��
		/*
		public Excel.Range GetRange(int p_rowIndex,int p_colIndex)
		public Excel.Range GetRange(int p_rowIndex,string p_colChars)
		public Excel.Range GetRange(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex)
		public Excel.Range GetRange(int p_startRowIndex,string p_startColChars,int p_endRowIndex,string p_endColChars)
		*/
		
		#region GetRange����Range("B10")��Range("C8:F11")��Range(2,10)��Range(2,"A")��Range(3,8,6,11)��Range(3,"A",6,"F")
		/// <summary>
		/// ��ȡָ����Ԫ���ָ����Χ�ڵĵ�Ԫ��������Ϊ��1��ʼ�����֣����65536��������ΪA~Z��AA~AZ��BA~BZ...HA~HZ��IA~IV����ĸ����ϣ�Ҳ������1-65536���֡�
		/// </summary>
		/// <param name="p_rowIndex">��Ԫ������������1��ʼ</param>
		/// <param name="p_colIndex">��Ԫ������������1��ʼ��������Ҳ��������ĸA��Z����ĸ���AA~AZ�����IV��Excel��ĸ����</param>
		/// <returns></returns>
		public Excel.Range GetRange(int p_rowIndex,int p_colIndex)
		{
			//����	Range(10,3).Select		//��10��3��
			return GetRange(p_rowIndex,p_colIndex,p_rowIndex,p_colIndex);
		}

		/// <param name="p_colChars">��Ԫ������ĸ�������������A��ʼ</param>
		public Excel.Range GetRange(int p_rowIndex,string p_colChars)
		{
			//����	Range("C10").Select		//��10��3��			
			return GetRange(p_rowIndex,p_colChars,p_rowIndex,p_colChars);
		}

		/// <param name="p_startRowIndex">ָ����Ԫ��Χ��ʼ����������1��ʼ</param>
		/// <param name="p_startColIndex">ָ����Ԫ��Χ��ʼ��������������1��ʼ</param>
		/// <param name="p_endRowIndex">ָ����Ԫ��Χ����������</param>
		/// <param name="p_endColIndex">ָ����Ԫ��Χ��������������</param>
		public Excel.Range GetRange(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex)
		{
			Excel.Range range;
			range = _xlApp.get_Range(_xlApp.Cells[p_startRowIndex,p_startColIndex],_xlApp.Cells[p_endRowIndex,p_endColIndex]);
						
			return range;
		}

		/// <param name="p_startChars">ָ����Ԫ��Χ��ʼ����ĸ���������</param>
		/// <param name="p_endChars">ָ����Ԫ��Χ��������ĸ���������</param>
		public Excel.Range GetRange(int p_startRowIndex,string p_startColChars,int p_endRowIndex,string p_endColChars)
		{
			//����	Range("D8:F11").Select
			Excel.Range range;
			
			range = _xlApp.get_Range(p_startColChars + p_startRowIndex.ToString(),p_endColChars + p_endRowIndex.ToString());
						
			return range;
		}
		#endregion

		#region MergeCells(Excel.Range p_Range)�ϲ���Ԫ�񣬺ϲ���Ĭ�Ͼ���
		/// <summary>
		/// �ϲ�ָ����Χ�ڵ�Ԫ�񣬺ϲ���Ĭ�Ͼ���
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


		#region �����ҳ��������ʵ��
		/// <summary>
		/// ��ָ�������ϲ����ҳ��
		/// </summary>
		/// <param name="p_rowIndex">������</param>
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

		#region �������С�����InsertRow(int p_rowIndex)��InsertColumn(int p_colIndex)��InsertColumn(string p_colChars)
		/// <summary>
		/// ��ָ�������ϲ���һ����
		/// </summary>
		/// <param name="p_rowIndex">������</param>
		public void InsertRow(int p_rowIndex)
		{
			//    Rows("2:2").Select
			//    Selection.Insert Shift:=xlDown

			Excel.Range range;

			range = GetRange(p_rowIndex,"A");
			range.Select();

			//Excel2003֧��������
			//range.EntireRow.Insert(oMissing,oMissing);			

			//Excel2000֧��һ���������������ԣ���Interop.ExcelV1.3(Excel2000)����������������Excel2003��
			range.EntireRow.Insert(oMissing);
		}

		/// <summary>
		/// ��ģ������ָ�������ϲ��룬��Excel�Ĳ��븴�Ƶ�Ԫ��
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
		/// ��ָ�������ϲ���һ����
		/// </summary>
		/// <param name="p_colIndex">������</param>
		public void InsertColumn(int p_colIndex)
		{
			Excel.Range range;

			range = GetRange(1,p_colIndex);
			range.Select();
			
			//Excel2003֧��������
			//range.EntireColumn.Insert(oMissing,oMissing);				
			//Excel2000֧��һ������
			range.EntireColumn.Insert(oMissing);		
		}

		/// <summary>
		/// ��ָ�������ϲ���һ����
		/// </summary>
		/// <param name="p_colChars">����ĸ�����</param>
		public void InsertColumn(string p_colChars)
		{
			Excel.Range range;

			range = GetRange(1,p_colChars);
			range.Select();
			//Excel2003֧��������
			//range.EntireColumn.Insert(oMissing,oMissing);		
			//Excel2000֧��һ������
			range.EntireColumn.Insert(oMissing);		
		}
		#endregion

		#region ɾ�����С�����DeleteRow(int p_rowIndex)��DeleteColumn(int p_colIndex)��DeleteColumn(string p_colChars)
		/// <summary>
		/// ɾ��ָ��������
		/// </summary>
		/// <param name="p_rowIndex">������</param>
		public void DeleteRow(int p_rowIndex)
		{
			Excel.Range range;

			range = GetRange(p_rowIndex,"A");
			range.Select();
			range.EntireRow.Delete(oMissing);			
		}

		/// <summary>
		/// ɾ��ָ��������
		/// </summary>
		/// <param name="p_colIndex">������</param>
		public void DeleteColumn(int p_colIndex)
		{
			Excel.Range range;

			range = GetRange(1,p_colIndex);
			range.Select();
			range.EntireColumn.Delete(oMissing);		
		}

		/// <summary>
		/// ɾ��ָ��������
		/// </summary>
		/// <param name="p_colChars">����ĸ�����</param>
		public void DeleteColumn(string p_colChars)
		{
			Excel.Range range;

			range = GetRange(1,p_colChars);
			range.Select();
			range.EntireColumn.Delete(oMissing);		
		}
		#endregion
		
		#region �����и��п�SetRowHeight(int p_rowIndex,float p_rowHeight)��SetColumnWidth(int p_colIndex,float p_colWidth)��SetColumnWidth(string p_colChars,float p_colWidth)
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
		
		#region SetBordersEdge ����ָ����Χ�߿��󡢶����ҡ��ס������¶Խ��ߡ������϶Խ��ߡ��ڲ�ˮƽ�ߡ��ڲ���ֱ�ߡ����ߣ��ߣ�����ָ����������ʽ���ޡ����ߡ����ߵȣ����ߴ�ϸ
		/// <summary>
		/// ����������ͨ��ϸ��������ָ����Χ�ڵı߽�
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
					//���Ҷ��׵���
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
					//�Խ���
				case BordersEdge.xlDiagonalDown:			
					border =p_Range.Borders[Excel.XlBordersIndex.xlDiagonalDown];
					break;
				case BordersEdge.xlDiagonalUp:
					border =p_Range.Borders[Excel.XlBordersIndex.xlDiagonalUp];
					break;
					//�߿��ڲ��Ǻ�����(�������߿�)
				case BordersEdge.xlInsideHorizontal:
					border =p_Range.Borders[Excel.XlBordersIndex.xlInsideHorizontal];
					break;
				case BordersEdge.xlInsideVertical:
					border =p_Range.Borders[Excel.XlBordersIndex.xlInsideVertical];
					break;
				case BordersEdge.xlLineStyleNone:
					//���ȷ�Χ�������߶�û��
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

		#region ClearBordersEdge�����ָ����Χ�ڵ������ߣ���SetBordersEdge���ñ߿�Ϊ����
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