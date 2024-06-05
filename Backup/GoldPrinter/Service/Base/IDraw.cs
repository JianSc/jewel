using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// ���ƽӿڣ��Ƿ����ڻ�ͼ����Graphics���ƾ�������Rectangle����ָ���Ļ���Brush��Pen������Draw()
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public interface IDraw
	{
		/// <summary>
		/// ��ȡ�����û�ͼ����
		/// </summary>
		Graphics Graphics
		{
			get;
			set;
		}

		/// <summary>
		/// ��ȡ�����û�������
		/// </summary>
		System.Drawing.Rectangle Rectangle
		{
			get;
			set;		
		}

		/*
        /// <summary>
        /// ����
        /// </summary>
        Pen Pen
        {
            get;
            set;
        }

        /// <summary>
        /// ��ˢ
        /// </summary>
        Brush Brush
        {
            get;
            set;
        }
		*/
        
        /// <summary>
		/// ����
		/// </summary>
		void Draw();

	}//End Interface
}//End NameSpace
