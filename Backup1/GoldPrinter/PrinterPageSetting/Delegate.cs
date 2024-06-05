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
	/// PrintDocument.PrintPage的委托定义
	/// </summary>
	public delegate void PrintPageDelegate(Object obj,System.Drawing.Printing.PrintPageEventArgs ev) ;

	/// <summary>
	/// 导出到Excel委托定义
	/// </summary>
	public delegate void ImportExcelDelegate(Object obj,ImportExcelArgs ev);

}//End NameSpace