using System;

namespace GoldPrinter
{
	/// <summary>
	/// WebForm下的打印纸张设置、打印机设置、打印预览对话框。（***暂无实现，请查看作者关于Web打印方案，敬请密切关注作者网站或Blog发布消息***）。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class WebPrinterPageSetting:IPrinterPageSetting
	{
		public WebPrinterPageSetting()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region IPrinterPageSetting 成员

		public System.Drawing.Printing.PrintDocument PrintDocument
		{
			get
			{
				// TODO:  添加 WebPrinterPageSetting.PrintDocument getter 实现
				return null;
			}
			set
			{
				// TODO:  添加 WebPrinterPageSetting.PrintDocument setter 实现
			}
		}

		public GoldPrinter.PrintPageDelegate PrintPageValue
		{
			get
			{
				// TODO:  添加 WebPrinterPageSetting.PrintPageValue getter 实现
				return null;
			}
			set
			{
				// TODO:  添加 WebPrinterPageSetting.PrintPageValue setter 实现
			}
		}

		public event GoldPrinter.PrintPageDelegate PrintPage;

		public ImportExcelDelegate ImportExcelValue
		{
			get
			{
				// TODO:  添加实现
				return null;
			}
			set
			{
				// TODO:  添加实现
			}
		}
		
		public System.Drawing.Printing.PageSettings ShowPageSetupDialog()
		{
			// TODO:  添加 WebPrinterPageSetting.ShowPageSetupDialog 实现
			return null;
		}

		public System.Drawing.Printing.PrinterSettings ShowPrintSetupDialog()
		{
			// TODO:  添加 WebPrinterPageSetting.ShowPrintSetupDialog 实现
			return null;
		}

		public void ShowPrintPreviewDialog()
		{
			// TODO:  添加 WebPrinterPageSetting.ShowPrintPreviewDialog 实现
		}

		#endregion

	}//End Class

}//End NameSpace