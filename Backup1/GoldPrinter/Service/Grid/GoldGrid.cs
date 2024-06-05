using System;
using System.Drawing;
using System.Collections;


namespace GoldPrinter
{

	/// <summary>
	/// GoldGrid ��ժҪ˵����
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class GoldGrid:GridBase
	{
		public GoldGrid()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//			
			
		}

		#region	_arrStrGrid��ص������뷽�� GridText��ȡ����_arrStrGrid��SetText(int row,int col,string text)��GetText(int row,int col)��ȡ��Ԫ���ı�

		/// <summary>
		/// ����ָ����Ԫ���ı�
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <param name="text"></param>
		public void SetText(int row,int col,string text)
		{
			_arrStrGrid[row,col] = text;
		}
		public void SetText(string text)
		{
			_arrStrGrid[RowSel,ColSel] = text;
		}

		/// <summary>
		/// ��ȡָ����Ԫ���ı�
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public string GetText(int row,int col)
		{
			return _arrStrGrid[row,col];
		}
		public string GetText()
		{
			return _arrStrGrid[RowSel,ColSel];
		}

		#endregion

		private string _colsAlignList = "";				//�ж����б�,��Left��Center��Right��һ����ĸ��ɵĴ��Ķ��뷽ʽ�Ĵ�

		/// <summary>
		/// ��ȡ�������ж����б������Left��Center��Right��һ����ĸ��ɵĴ��Ķ��뷽ʽ�Ĵ�
		/// </summary>
		public string ColsAlignString
		{
			get
			{
				return this._colsAlignList;
			}
			set
			{
				if (value != null)
				{
					this._colsAlignList = value;
				}
			}
		}


		//��Ҫ����									
		private object _DataSource = null;				//����ı����м���������
		#region DataSource
		/// <summary>
		/// ��ȡ����������Դ������������Ķ�ά�����͵ģ����ά���顢�����
		/// </summary>
		public object DataSource
		{
			get
			{
				return this._DataSource;
			}
			set
			{	
				if (value != null)
				{
					_DataSource = value;

					//������ת���ɶ�ά����
					switch(value.GetType().ToString())
					{						
						case "System.String":			//�ַ���
							//...
							break;
						case "System.String[]":			//һά����
							string[] arr1 = (System.String[])_DataSource;
							string[,] arr2 = new string[1,arr1.Length];
							for(int i = 0 ; i < arr1.Length ; i++)
							{
								arr2[0,i] = arr1[i];
							}
							this.DataSource = arr2;
							break;
						case "System.String[,]":		//��ά����
							this.GridText = (System.String[,])_DataSource;
							break;
						case "System.Data.DataTable":	//���ݱ��							
							this.GridText = ToArrFromDataTable((System.Data.DataTable)_DataSource);
							break;							
						case "System.Windows.Forms.DataGrid":
							this.GridText = ToArrFromDataGrid((System.Windows.Forms.DataGrid)_DataSource);
							break;
						case "System.Web.UI.WebControls.DataGrid":
							this.GridText = ToArrFromDataGrid((System.Web.UI.WebControls.DataGrid)_DataSource);
							break;
						case "System.Web.UI.HtmlControls.HtmlTable":
							this.GridText = ToArrFromHtmlTable((System.Web.UI.HtmlControls.HtmlTable)_DataSource);
							break;
							//...̫���ˣ��������Լ��ӣ�ֻҪ��ת���ɶ�ά����
							//						case "MSHFlexGrid�����ͣ��Լ�ת��";
							//							this.GridText = ToArrFromMSHFlexGrid((MSHFlexGrid�����ͣ��Լ�ת��)_DataSource);
							//							break;
					}

					
				}
			}
		}
		#endregion

				
		public void SetTextOnRowSel(int rowSel, int startCol,int endCol, string text)
		{			
			for(int i = startCol ; i <= endCol ; i++)
			{
				SetText(rowSel, i, text);
			}
		}

		public void SetTextOnColSel(int colSel, int startRow,int endRow, string text)
		{			
			for(int i = startRow ; i <= endRow ; i++)
			{
				SetText(i,colSel, text);
			}
		}

		/// <summary>
		/// ���ҵ�ǰ��Ԫ���󶥿��
		/// </summary>
		/// <returns></returns>
		public CellRectangle GetMergeCell()
		{
			return GetMergeCell(this.Location,_arrStrGrid,this.PreferredRowHeight,this.ColsWidth,RowSel,ColSel);
		}



		/// <summary>
		/// ��ʼ�ж����ַ�����������AlignMent��Colsʱ����
		/// </summary>
		private void InitColsAlignString()
		{
			string malignString = "";
			string malignChar = "";

			switch(this.AlignMent)
			{
				case AlignFlag.Left:
					malignChar = "L";
					break;
				case AlignFlag.Center:
					malignChar = "C";
					break;
				case AlignFlag.Right:
					malignChar = "R";
					break;
			}

			for(int i = _colsAlignList.Length ; i < this.Cols ; i++)
			{
				malignString += malignChar;
			}		
			this._colsAlignList = malignString;

			if (_colsAlignList.Length > Cols)
			{
				_colsAlignList.Substring(0,Cols);
			}
		}


		#region ����Ҫ�õ��Ķ�ά���Ķ������ŵ�����ɣ���������ö�����һ������
		public string[,] ToArrFromDataTable(System.Data.DataTable source)
		{
			if (source == null)
			{
				return new string[0,0];
			}

			int mRows,mCols;
			string[,] arrGridText;

			mRows = source.Rows.Count;
			mCols = source.Columns.Count;		

			arrGridText = new string[mRows,mCols];

			for(int i = 0 ; i < mRows ; i++)
			{
				for(int j = 0 ; j < mCols ; j++)
				{					
					arrGridText[i,j] = source.Rows[i][j].ToString();
				}
			}

			return arrGridText;
		}

		public string[,] ToArrFromDataGrid(System.Windows.Forms.DataGrid source)
		{

			if (source == null)
			{
				return new string[0,0];
			}

			int mRows = 0;
			int mCols = 0;
			string[,] arrGridText;

			//TimeDef.Start();	//�ò������ķ�����һ�κ�ʱ̫��50��6�еĻ���������4��࣬���ڶ���ֻ��20.0288����
			
			//��ʱ��4.656696��
			//��ʱ��4656.696����
			//����ԭ���ǵ�һ��Ҫ����mscorlib.resources.dll

			try			
			{
				string s = "";
				for(int i = 0 ; i < int.MaxValue - 1 ; i ++)
				{
					s = source[0,i].ToString();
					mCols++;
				}
			}
			catch(Exception e){}
				
			try			
			{
				string s = "";
				for(int i = 0 ; i < int.MaxValue - 1 ; i ++)
				{
					s = source[i,0].ToString();
					mRows++;
				}
			}
			catch(Exception e){}
			//TimeDef.End();


			arrGridText = new string[mRows,mCols];

			try
			{

				for(int i = 0 ; i < mRows ; i++)
				{
					for(int j = 0 ; j < mCols ; j++)
					{					
						arrGridText[i,j] = source[i,j].ToString();
					}
				}
			}
			catch(Exception e)
			{}

			return arrGridText;
		}

		public string[,] ToArrFromDataGrid(System.Web.UI.WebControls.DataGrid source)
		{
			if (source == null)
			{
				return new string[0,0];
			}

			int mRows = 0;
			int mCols = 0;
			string[,] arrGridText;

			mRows = source.Items.Count;
			mCols = source.Columns.Count;

			arrGridText = new string[mRows,mCols];

			for(int i = 0 ; i < mRows ; i++)
			{
				for(int j = 0 ; j < mCols ; j++)
				{					
					arrGridText[i,j] = source.Items[i].Cells[j].Text;
				}
			}

			return arrGridText;
		}

		public string[,] ToArrFromHtmlTable(System.Web.UI.HtmlControls.HtmlTable source)
		{
			if (source == null)
			{
				return new string[0,0];
			}

			int mRows = source.Rows.Count;
			int mCols = source.Rows[0].Cells.Count;
			string[,] arrGridText = new string[mRows,mCols];

			for(int i = 0 ; i < mRows ; i++)
			{
				for(int j = 0 ; j < mCols ; j++)
				{					
					arrGridText[i,j] = source.Rows[i].Cells[j].InnerText;
				}
			}

			return arrGridText;
		}
		#endregion


		#region protected virtual GetAlignFlag[] GetColsAlign(string alignment)	
		/// <summary>
		/// ������Left��Center��Right��һ����ĸ��ɵĴ��Ķ��뷽ʽ������
		/// </summary>
		/// <param name="Alignment">��Left��Center��Right��һ����ĸ��ɵĴ�</param>
		/// <returns></returns>
		protected virtual AlignFlag[] GetColsAlign(string alignment)
		{	
			if (alignment == null || alignment.Length == 0)
			{
				return (new AlignFlag[0]); 	
			}
			int len = alignment.Length;
			AlignFlag[] arrAlign = new AlignFlag[len];
			string strAlign = "";

			for(int i = 0 ; i < len ; i++)
			{				
				strAlign = alignment.Substring(i,1).ToUpper();
				switch(strAlign)
				{
						//					case "L":
						//						break;
					case "C":
						arrAlign[i] = AlignFlag.Center;
						break;
					case "R":
						arrAlign[i] = AlignFlag.Right;
						break;
					default:
						arrAlign[i] = AlignFlag.Left;
						break;
				}
			}
			return arrAlign;
		}
		#endregion

		#region protected virtual Cell GetMergeCell(Point GridLocation,string[,] arrStrGrid,int rowHeight,int[] ArrColWidth,int rowSel,int colSel)
		/// <summary>
		/// ����ϲ���ʽ�·���ָ����Ԫ���󶥿��
		/// </summary>
		/// <param name="GridLocation">�����������</param>
		/// <param name="arrStrGrid">��ά����</param>
		/// <param name="rowHeight">�и�</param>
		/// <param name="ArrColWidth">�п�����</param>
		/// <param name="rowSel">ָ����Ԫ����</param>
		/// <param name="colSel">ָ����Ԫ����</param>
		/// <returns></returns>
		protected virtual CellRectangle GetMergeCell(Point GridLocation,string[,] arrStrGrid,int rowHeight,int[] ArrColWidth,int rowSel,int colSel)
		{
			CellRectangle cell = new CellRectangle(0,0,0,0);

			int lngRows = arrStrGrid.GetLength(0);	//����
			int lngCols = arrStrGrid.GetLength(1);	//����

			int lngMergeRows = 1;					//�ϲ�������
			int lngMergeCols = 1;					//�ϲ�������

			int lngStartRow = rowSel;				//��¼��˵�Ԫ��ϲ�����ʼ��
			int lngEndRow = rowSel;					//�Ա����߼����Y����

			int lngStartCol = colSel;				//��¼��˵�Ԫ��ϲ�����ʼ��
			int lngEndCol = colSel;					//�Ա��������X����

			//������"��"�Ͻ����кϲ�ʱ��ʼ����ϲ��Ķ���
			//���ϲ�ϲ�(�в���)
			for(int rowIndex = rowSel-1 ; rowIndex >= 0 ; rowIndex--)
			{
				if(arrStrGrid[rowSel,colSel] == arrStrGrid[rowIndex,colSel])
				{
					lngMergeRows++;
					lngStartRow--;
				}
				else
				{
					break;
				}
			}
			//���²�ϲ�(�в���)
			for(int rowIndex = rowSel+1 ; rowIndex < lngRows ; rowIndex++)
			{
				if(arrStrGrid[rowSel,colSel] == arrStrGrid[rowIndex,colSel])
				{
					lngMergeRows++;
					lngEndRow++;
				}
				else
				{
					break;
				}
			}

			//������"��"�Ͻ����кϲ�ʱ��ʼ����ϲ��Ķ���
			//�����ϲ�(�в���)
			for(int colIndex = colSel-1 ; colIndex >= 0 ; colIndex--)
			{
				if(arrStrGrid[rowSel,colSel] == arrStrGrid[rowSel,colIndex])
				{
					lngMergeCols++;
					lngStartCol--;
				}
				else
				{
					break;
				}
			}
			//���Ҳ�ϲ�(�в���)
			for(int colIndex = colSel+1 ; colIndex < lngCols ; colIndex++)
			{
				if(arrStrGrid[rowSel,colSel] == arrStrGrid[rowSel,colIndex])
				{
					lngMergeCols++;
					lngEndCol++;
				}
				else
				{
					break;
				}
			}


			//******************�����󶥿��******************
			int cellLeft = GridLocation.X;
			int cellTop = GridLocation.Y + lngStartRow * rowHeight;	//���и߲��ǹ̶��иߣ����Լ���֮ǰ�е��и��ܺ�

			int cellWidth = 0;
			int cellHeight = 0;

			//��Ԫ��ϲ��е�ǰ�ߵĵ�Ԫ���п��
			for(int i = lngStartCol-1 ; i >= 0 ; i--)
			{
				cellLeft += ArrColWidth[i];
			}

			//��Ԫ��ϲ����п��
			for(int i = lngStartCol ; i <= lngEndCol ; i++)
			{
				cellWidth += ArrColWidth[i];
			}

			cellHeight = lngMergeRows * rowHeight;					//���и߲��ǹ̶��иߣ����Լ��������е��и��ܺ�

			cell = new CellRectangle(cellLeft,cellTop,cellWidth,cellHeight);

			return cell;		
		}
		#endregion



	}//End Class

}//End Namespace
