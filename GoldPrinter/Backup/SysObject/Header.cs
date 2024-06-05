using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// Header���������������ϵĶ���������10���ڣ��������ޡ�
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
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
				throw new Exception("���������ڡ�" + MAX_ROWS.ToString() + "�������ڣ�");
			}
			else
			{
				base.Initialize(mrows,cols);
			}		
		}


	}//End Class
}//End NameSpace
