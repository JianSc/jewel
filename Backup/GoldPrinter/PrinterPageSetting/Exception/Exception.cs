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
	/// 无效的打印机，请在控制面版中添加打印机
	/// </summary>
	public class ExceptionInvalidPrinter:Exception
	{
		#region 实现...
		string _Message = "无效的打印机，请在控制面版中添加打印机！";

		public ExceptionInvalidPrinter()
		{
			//
			// TODO: 在此处添加构造函数逻辑
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
