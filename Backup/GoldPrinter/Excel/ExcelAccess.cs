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
	public class ExcelAccess:ExcelBase
	{
		public ExcelAccess()
		{

		}

		//���ĺ�����GetRange()����ȡָ����Χ�ڵĵ�Ԫ��
		/*
		public Excel.Range GetRange(int p_rowIndex,int p_colIndex)
		public Excel.Range GetRange(int p_rowIndex,string p_colChars)
		public Excel.Range GetRange(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex)
		public Excel.Range GetRange(int p_startRowIndex,string p_startColChars,int p_endRowIndex,string p_endColChars)
		*/
		
		#region MergeCells()�ϲ���Ԫ�񣬺ϲ���Ĭ�Ͼ��У���Range������ָ����Χ��Ϊ����
		/// <summary>
		/// �ϲ�ָ����Χ�ڵ�Ԫ��
		/// </summary>
		/// <param name="p_rowIndex">��������Ҳ����ָ����ʼ�С���ֹ������</param>
		/// <param name="p_colIndex">��������Ҳ����ָ����ʼ�С���ֹ�����ֻ���ĸ���������</param>
		/// 

		/// <summary>
		/// �ϲ�ָ����Χ�ڵ�Ԫ��
		/// </summary>
		/// <param name="p_startRowIndex">��ʼ������</param>
		/// <param name="p_startColIndex">��ʼ�����������������ֻ���ĸ���������</param>
		/// <param name="p_endRowIndex">����������</param>
		/// <param name="p_endColIndex">���������������������ֻ���ĸ���������</param>
		public void MergeCells(int p_startRowIndex,int p_startColIndex,int p_endRowIndex,int p_endColIndex)
		{
			MergeCells(GetRange(p_startRowIndex,p_startColIndex,p_endRowIndex,p_endColIndex));	
		}

		/// <summary>
		/// �ϲ�ָ����Χ�ڵ�Ԫ��
		/// </summary>
		/// <param name="p_startRowIndex">��ʼ������</param>
		/// <param name="p_startColChars">��ʼ�����������������ֻ���ĸ���������</param>
		/// <param name="p_endRowIndex">����������</param>
		/// <param name="p_endColChars">���������������������ֻ���ĸ���������</param>
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
		
		#region SetBordersEdge ����ָ����Χ�߿��󡢶����ҡ��ס������¶Խ��ߡ������϶Խ��ߡ��ڲ�ˮƽ�ߡ��ڲ���ֱ�ߡ����ߣ��ߣ�����ָ����������ʽ���ޡ����ߡ����ߵȣ����ߴ�ϸ
		/// <summary>
		/// ����ָ����Χ�߿��󡢶����ҡ��ס������¶Խ��ߡ������϶Խ��ߡ��ڲ�ˮƽ�ߡ��ڲ���ֱ�ߡ����ߣ��ߣ�����ָ���ߵ���ʽ���ߴ�ϸ
		/// </summary>
		/// <param name="p_rowIndex">��������Ҳ����ָ����ʼ�С���ֹ������</param>
		/// <param name="p_colIndex">��������Ҳ����ָ����ʼ�С���ֹ�����ֻ���ĸ���������</param>
		/// <param name="p_BordersEdge">�߿��󡢶����ҡ��ס������¶Խ��ߡ������϶Խ��ߡ��ڲ�ˮƽ�ߡ��ڲ���ֱ�ߡ�����</param>
		/// <param name="p_BordersLineStyle">������ʽ���ޡ����ߡ����ߵȣ���Excel��֪</param>
		/// <param name="p_BordersWeight">��ϸ</param>
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
		/// ����ָ����Χ�ڱ߽缰�ڲ�������
		/// </summary>
		/// <param name="p_startRowIndex"></param>
		/// <param name="p_startColIndex"></param>
		/// <param name="p_endRowIndex"></param>
		/// <param name="p_endColIndex"></param>
		/// <param name="p_endColIndex">IsBordersOrBordersGrid��trueֻ������ܵı߿򣬷�������߿���������</param>
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

		#region ClearBordersEdge�����ָ����Χ�ڵ������ߣ���SetBordersEdge���ñ߿�Ϊ����
		/// <summary>
		/// �����SetBordersEdge���õı߿��ڵ�������
		/// </summary>
		/// <param name="p_rowIndex">��������Ҳ����ָ����ʼ�С���ֹ������</param>
		/// <param name="p_colIndex">��������Ҳ����ָ����ʼ�С���ֹ�����ֻ���ĸ���������</param>
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

		#region GetCellText(int p_rowIndex,int p_colIndex[/string p_colChars])����Range������ָ����Χ��Ϊ����
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


		#region SetCellText(...)��������Ӧ��Range(...)������һ����Ԫ��Ҳ���������ڵĵ�Ԫ��һ������ͬ�����ı�����Range������ָ����Χ��Ϊ����
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