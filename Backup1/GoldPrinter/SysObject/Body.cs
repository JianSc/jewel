using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// Body�����ݱ�����⡣
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class Body:Outer
	{
		public Body()
		{
			this.IsDrawAllPage = false;
			mdrawGrid.AlignMent = AlignFlag.Left;
			mdrawGrid.Border = GridBorderFlag.Single;
			mdrawGrid.Line = GridLineFlag.Both;
			this.IsAverageColsWidth = false;
			
			//���ϲ�
			mdrawGrid.Merge = GridMergeFlag.None;
			//this.Font = new Font("����",12);

			mdrawGrid.Font = new Font("����",12);			
			mdrawGrid.PreferredRowHeight = mdrawGrid.Font.Height + 2;			
		}

		public Body(int rows,int cols):this()
		{			
			base.Initialize(rows,cols);
		}

		public string[,] GridText
		{
			set
			{
				mblnHadInitialized = true;
				mdrawGrid.GridText = value;
			}
			get
			{
				return mdrawGrid.GridText;
			}
		}

		public string ColsAlignString
		{
			set
			{
				mdrawGrid.ColsAlignString = value;
			}
			get
			{
				return mdrawGrid.ColsAlignString;
			}
		}

		public override Font Font
		{
			get
			{
				return mdrawGrid.Font;
			}
			set
			{
				mdrawGrid.Font = value;
			}
		}


	}//End Class
}//End NameSpace
