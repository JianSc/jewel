using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// IGrid����������Ľӿڡ�
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public interface IGrid
	{

		//*****************����*****************	
		#region ������㼰�ߡ���ʹ�õ�����

		/// <summary>
		/// �����������
		/// </summary>
		Point Location
		{
			get;
			set;
		}

		/// <summary>
		/// �����
		/// </summary>
		int Width
		{
			get;
			set;
		}

		/// <summary>
		/// �����
		/// </summary>
		int Height
		{
			get;
			set;
		}

		/// <summary>
		/// �����ı�����
		/// </summary>
		Font Font
		{
			get;
			set;
		}			

		#endregion

		#region ������롢�����ߡ��ϲ����߿�ʽ

		/// <summary>
		/// ����������뷽ʽ
		/// </summary>
		AlignFlag AlignMent
		{
			get;
			set;
		}		
		
		/// <summary>
		/// ����������
		/// </summary>
		GridLineFlag Line
		{
			get;
			set;
		}		

		/// <summary>
		/// ��Ԫ��ϲ���ʽ
		/// </summary>
		GridMergeFlag Merge
		{
			get;
			set;
		}		

		/// <summary>
		/// ����߿�����
		/// </summary>
		GridBorderFlag Border
		{
			get;
			set;
		}	

		#endregion
	
		#region ���������̶�������

		/// <summary>
		/// ����
		/// </summary>
		int Rows
		{
			get;
			set;
		}	

		/// <summary>
		/// ����
		/// </summary>
		int Cols
		{
			get;
			set;
		}	

		/// <summary>
		/// �̶�����
		/// </summary>
		int FixedRows
		{
			get;
			set;
		}	

		/// <summary>
		/// �̶�����
		/// </summary>
		int FixedCols
		{
			get;
			set;
		}

		#endregion

		#region �����иߡ��п�����Ӧ���иߡ��п��ж�������

		/// <summary>
		/// ��ȡ��������ѡ�и�
		/// </summary>
		int PreferredRowHeight
		{
			get;
			set;
		}

		/// <summary>
		/// ��ȡ������Ĭ�ϵ��п�
		/// </summary>
		int PreferredColWidth
		{
			get;
			set;
		}

		//*********�����Ƕ�Ӧ������*********
		/// <summary>
		/// ��ȡ�������и�����
		/// </summary>
		/// <returns></returns>
		int[] RowsHeight
		{
			get;
			set;
		}

		/// <summary>
		/// �����п�����
		/// </summary>
		/// <returns></returns>
		int[] ColsWidth
		{
			get;
			set;
		}

		/// <summary>
		/// �����ж�������
		/// </summary>
		/// <returns></returns>
		AlignFlag[] ColsAlignment
		{
			get;
			set;
		}

		#endregion

		#region ��Ԫ���ı�
		
		/// <summary>
		/// ��ȡ�����õ�ǰ��Ԫ���ı�
		/// </summary>		
		string Text
		{
			get;
			set;
		}	

		/// <summary>
		/// ��ȡָ�����е�Ԫ���ı�
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		string get_TextMatrix(int row,int col);

		/// <summary>
		/// ����ָ����Ԫ���ı�
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <param name="textMatrix"></param>
		/// <returns></returns>
		void set_TextMatrix(int row,int col,string textMatrix);

		/// <summary>
		/// ��ȡ�����ö�ά������ı�
		/// </summary>
		string[,] GridText
		{
			get;
			set;
		}	
		
		#endregion


		#region ��ǰ���С�ѡ��������

		/// <summary>
		/// ��ǰ��
		/// </summary>
		int Row
		{
			get;
			set;
		}	

		/// <summary>
		/// ��ǰ��
		/// </summary>
		int Col
		{
			get;
			set;
		}	

		/// <summary>
		/// ѡ����
		/// </summary>
		int RowSel
		{
			get;
			set;
		}	

		/// <summary>
		/// ѡ����
		/// </summary>
		int ColSel
		{
			get;
			set;
		}	

		#endregion

		#region ��ȡ������ĳ��/�е��и�/�п�

		/// <summary>
		/// ��ȡָ���е��и�
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		int get_RowHeight(int index);

		/// <summary>
		/// ����ָ���е��и�
		/// </summary>
		/// <param name="index"></param>
		/// <param name="rowHeight"></param>
		void set_RowHeight(int index,int rowHeight);

		/// <summary>
		/// ��ȡָ���е��п�
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		int get_ColWidth(int index);

		/// <summary>
		/// ����ָ���е��п�
		/// </summary>
		/// <param name="index"></param>
		/// <param name="colWidth"></param>
		/// <returns></returns>
		void set_ColWidth(int index,int colWidth);

		/// <summary>
		/// ����ˮƽ�ж��뷽ʽ
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		AlignFlag  get_ColAlignment(int index);

		/// <summary>
		/// ��ȡˮƽ�ж��뷽ʽ
		/// </summary>
		/// <param name="index"></param>
		/// <param name="colAlignment"></param>
		void set_ColAlignment(int index, AlignFlag colAlignment);

		#endregion


		



		/*
		bool get_ColIsVisible(int index);
		bool set_ColIsVisible(int index);



		bool get_RowIsVisible(int index);

		bool get_MergeCol(int index);
		void set_MergeCol(int index,bool mergeCol);

		bool get_MergeRow(int index);
		bool set_MergeRow(int index,bool mergeRow);

		*/

	}//End Class
}//End NameSpace