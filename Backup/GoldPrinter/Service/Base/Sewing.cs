using System;
using System.Drawing;
using System.Drawing.Printing;

/// 
/// �� �ߣ�����֧��(�ܷ���)
/// Email��flygoldfish@163.com  QQ��150439795
/// �� ַ��www.webmis.com.cn
/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
/// 

namespace GoldPrinter
{
	#region *****ͼ��*****

	//				    ��װ��ͼ��
	//   _______________________________________
	//	|           							|
	//	|           |	 						|
	//	|           |							|
	//	|           װ							|
	//	|           |							|
	//	|           |							|
	//	|           ��							|
	//	|           |							|
	//	|           |							|
	//	|           ��							|
	//	|           |							|
	//	|           |							|
	//	|_______________________________________|
	// 
	//  |<- Margin ->|
	//  װ�����������ڵ��߳�ΪLineLen


	//				    ��װ��ͼ��
	//   _______________________________________  __ __
	//	|           							|   |
	//	|           							|   |
	//	|           	 						| Margin
	//	|           							|   |
	//	|     -----װ-------��-------��-----    | __|__
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|           							|
	//	|_______________________________________|
	// 
	//  װ�����������ڵ��߳�ΪLineLen
	#endregion

	/// <summary>
	/// װ���߷�λ
	/// </summary>
	public enum SewingDirectionFlag
	{
		/// <summary>
		/// �����װ��
		/// </summary>
		Left
			,
		/// <summary>
		/// �ڶ���װ��
		/// </summary>
		Top	
	}

	/// <summary>
	/// װ���࣬�ڴ�ӡҳ��ʱ������ҳ�����߻򶥶�Ϊװ��ר����Ԥ��һ���֡�
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class Sewing:IDisposable
	{
		//װ�����򡢱߽硢װ���߳�
		private SewingDirectionFlag _sewingDirection;
		private int _margin;
		private int _lineLength;

		#region �ֶ�����
		/// <summary>
		/// ��ȡ������װ������
		/// </summary>
		public SewingDirectionFlag SewingDirection
		{
			set
			{
				this._sewingDirection = value;
			}
			get
			{
				return this._sewingDirection;
			}		
		}

		/// <summary>
		/// ��ȡ������װ��Ԥ���հ�
		/// </summary>
		public int Margin
		{
			set
			{
				this._margin = value;
			}
			get
			{
				return this._margin;
			}
		}

		/// <summary>
		/// ��ȡ������װ���߳�
		/// </summary>
		public int LineLen
		{
			set
			{
				this._lineLength = value;
			}
			get
			{
				return this._lineLength;
			}
		}
		#endregion

		#region ���캯��
		public Sewing()
		{	
			this._margin = 0;
			this._sewingDirection = SewingDirectionFlag.Left;
			_lineLength = 0;
		}

		public Sewing(int margin):this(margin,SewingDirectionFlag.Left,0)
		{
			
		}

		public Sewing(int margin,int lineLength):this(margin,SewingDirectionFlag.Left,lineLength)
		{
			
		}

		public Sewing(int margin,SewingDirectionFlag sewingDirection):this(margin,sewingDirection,0)
		{
			
		}

		//�����Ĺ��캯������������ȫ�����ֶ�
		public Sewing(int margin,SewingDirectionFlag sewingDirection,int lineLength)
		{
			this._margin = margin;
			this._sewingDirection = sewingDirection;
			this._lineLength = lineLength;
		}
		#endregion

		#region IDisposable ��Ա

		public virtual void Dispose()
		{

		}

		#endregion

		/// <summary>
		/// ��ָ���Ļ�ͼ���滭װ����
		/// </summary>
		/// <param name="g">��ͼ����</param>
		/// <remarks>
		/// ��    �ߣ��ܷ���
		/// �޸����ڣ�2004-08-07
		/// </remarks>
		public void Draw(Graphics g)
		{
			//����
			Font font = new Font("����",8);
			//װ�����ı�
			string strText = "װ                    ��                    ��";
			//д���ָ�ʽ
			StringFormat sf = new StringFormat();
			//���з�
			sf.Alignment = StringAlignment.Center;

			int LeftMargin , TopMargin;
			int PageHeight , PageWidth;
			LeftMargin = TopMargin = this._margin;
			PageHeight = PageWidth = this._lineLength;

			Pen pen = new Pen(Color.Red);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			
			//��װ���߽߱�
			if (this._sewingDirection == SewingDirectionFlag.Left)
			{
				//������
				g.DrawLine(pen,LeftMargin,0,LeftMargin,PageHeight);
							

				//д����
				sf.FormatFlags = StringFormatFlags.DirectionVertical;	//��������

				int textWidth = (int)(g.MeasureString("װ",font).Width);
				textWidth /= 2;
		
				Rectangle rec = new Rectangle(LeftMargin - textWidth,0,LeftMargin - textWidth,PageHeight);

				g.DrawString(strText,font,Brushes.DodgerBlue,rec,sf);
			}
			//��װ���߽߱�
			else if (this._sewingDirection == SewingDirectionFlag.Top)
			{
				//������
				g.DrawLine(pen,0,TopMargin,PageWidth,TopMargin);				
				
				//д����
				int textHeight = (int)(g.MeasureString("װ",font).Height);
				textHeight /= 2;

				Rectangle rec = new Rectangle(0,TopMargin - textHeight,PageWidth,TopMargin - textHeight);

				g.DrawString(strText,font,Brushes.DodgerBlue,rec,sf);
			}	
			pen.Dispose();
			font.Dispose();
            sf.Dispose();
        }

    }//End Class
}//End NameSpace
