using System;

/// 
/// 作 者：长江支流(周方勇)
/// Email：flygoldfish@163.com  QQ：150439795
/// 网 址：www.webmis.com.cn
/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
/// 

namespace GoldPrinter
{
	/// <summary>
	/// 创建Excel类实例时错误
	/// </summary>
	public class ExceptionExcelCreateInstance:Exception
	{
		#region 实现...
		string _Message = "创建Excel类实例时错误！";

		public ExceptionExcelCreateInstance()
		{
			//
			// TODO: 在此处添加构造函数逻辑
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
	/// 打开Excel时错误
	/// </summary>
	public class ExceptionExcelOpen:Exception
	{
		#region 实现...
		string _Message = "打开Excel时错误！";

		public ExceptionExcelOpen()
		{
			//
			// TODO: 在此处添加构造函数逻辑
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
