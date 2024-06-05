using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	public class ImportExcelArgs
	{
		public Icon ButtonIcon = null;        	
	}

	/// <summary>
	/// IPrinterPageSetting 的接口，显示打印纸张设置、打印机设置、打印预览对话框。
	/// 
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public interface IPrinterPageSetting
	{
		/// <summary>
		/// 获取或设置打印文档
		/// </summary>
		System.Drawing.Printing.PrintDocument PrintDocument
		{
			get;
			set;
		}

		/// <summary>
		/// 关联一个方法，目的是让具体的打印由实例化者来操作。这里当属性使用，其实可以用PrintPage。
		/// 如PrinterPageSetting1.PrintPage = new PrintPageDelegate(this.PrintPageEventHandler);
		/// </summary>
		PrintPageDelegate PrintPageValue
		{
			get;
			set;
		}

		/// <summary>
		/// 当需要为当前页打印的输出时发生，如果对此不了解，就用PrintPageValue赋值也可以
		/// </summary>
		event PrintPageDelegate PrintPage;


		/// <summary>
		/// 导出到Excel委托定义的实现
		/// </summary>
		ImportExcelDelegate ImportExcelValue
		{
			get;
			set;		
		}

		/// <summary>
		/// 显示页面设置对话框，并返回PageSettings
		/// </summary>
		/// <returns></returns>
		System.Drawing.Printing.PageSettings ShowPageSetupDialog();

		/// <summary>
		/// 显示打印机设置对话框，并返回PrinterSettings
		/// </summary>
		/// <returns></returns>
		System.Drawing.Printing.PrinterSettings ShowPrintSetupDialog();

		/// <summary>
		/// 显示打印预览对话框
		/// </summary>
		void ShowPrintPreviewDialog();

	}//End Interface
}//End NameSpace
