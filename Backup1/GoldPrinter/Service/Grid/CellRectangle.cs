using System;

namespace GoldPrinter
{
	/// <summary>
	/// ����Ԫ�����,�����󶥿��
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public struct CellRectangle
	{	
		private int _cellLeft,_cellTop,_cellWidth,_cellHeight;

		public CellRectangle(int cellLeft,int cellTop,int cellWidth,int cellHeight)
		{
			_cellLeft = cellLeft;
			_cellTop = cellTop;
			_cellWidth = cellWidth;
			_cellHeight = cellHeight;
		}

		/// <summary>
		/// ��Ԫ�����X����
		/// </summary>
		public int Left
		{
			get
			{
				return _cellLeft;
			}
			set
			{	
				this._cellLeft = value;
			}
		}

		/// <summary>
		/// ��Ԫ�����Y����
		/// </summary>
		public int Top
		{
			get
			{
				return _cellTop;
			}
			set
			{	
				this._cellTop = value;
			}
		}

		/// <summary>
		/// ��Ԫ���
		/// </summary>
		public int Width
		{
			get
			{
				return _cellWidth;
			}
			set
			{	
				this._cellWidth = value;
			}
		}

		/// <summary>
		/// ��Ԫ���
		/// </summary>
		public int Height
		{
			get
			{
				return _cellHeight;
			}
			set
			{	
				this._cellHeight = value;
			}
		}

	}//End Class
}//End NameSpace
