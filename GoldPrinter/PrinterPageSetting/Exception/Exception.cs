using System;

/// 
/// �� �ߣ�����֧��(�ܷ���)
/// Email��flygoldfish@163.com  QQ��150439795
/// �� ַ��www.webmis.com.cn
/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
/// 

namespace GoldPrinter
{
	/// <summary>
	/// ��Ч�Ĵ�ӡ�������ڿ����������Ӵ�ӡ��
	/// </summary>
	public class ExceptionInvalidPrinter:Exception
	{
		#region ʵ��...
		string _Message = "��Ч�Ĵ�ӡ�������ڿ����������Ӵ�ӡ����";

		public ExceptionInvalidPrinter()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//		
		}
		
		public ExceptionInvalidPrinter(string message)
		{
			this._Message = message;
		}

		public override string Message
		{
			get
			{
				return this._Message;
			}
		}
		#endregion

	}//End ExceptionInvalidPrinter


}//End Namespace
