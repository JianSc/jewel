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
	/// ����Excel��ʵ��ʱ����
	/// </summary>
	public class ExceptionExcelCreateInstance:Exception
	{
		#region ʵ��...
		string _Message = "����Excel��ʵ��ʱ����";

		public ExceptionExcelCreateInstance()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//		
		}
		
		public ExceptionExcelCreateInstance(string message)
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

	}//End ExceptionExcelCreateInstance


	/// <summary>
	/// ��Excelʱ����
	/// </summary>
	public class ExceptionExcelOpen:Exception
	{
		#region ʵ��...
		string _Message = "��Excelʱ����";

		public ExceptionExcelOpen()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//		
		}
		
		public ExceptionExcelOpen(string message)
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

	}//End ExceptionExcelCreateInstance

}//End Namespace
