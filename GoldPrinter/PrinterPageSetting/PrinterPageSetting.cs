using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{

	/// <summary>
	/// 封装页面设置、打印机设置、打印预览，适合于Window和Asp.Net，Web方式下请在配置文件中增加键PrintMode及值Web。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class PrinterPageSetting
	{
		//申明一个封装页面设置、打印机设置、打印预览的接口
		private IPrinterPageSetting _printerPageSetting;

		//打印方式
		private PrintModeFlag _printModeFlag;

		public PrintModeFlag PrintMode
		{
			get
			{
				return this._printModeFlag;
			}
			set
			{
				this._printModeFlag = value;	
				//运用了抽象模式，创建接口对象的新实例，使其指象真正的实现其接口的对象
				if (this._printModeFlag == PrintModeFlag.Win)
				{
					_printerPageSetting = new WinPrinterPageSetting();
				}
				else if(this._printModeFlag == PrintModeFlag.Web)
				{
					//注意，Web方式下的程序请密切关注WWW.AlinkSoft.COM
					_printerPageSetting = new WebPrinterPageSetting();
				}
			}
		}

		/// <summary>
		/// 获取或设置打印文档
		/// </summary>
		public PrintDocument PrintDocument
		{
			get
			{
				return _printerPageSetting.PrintDocument;
			}
			set
			{
				_printerPageSetting.PrintDocument = value;
			}
		}


		/// <summary>
		/// 关联一个方法，目的是让具体的打印由实例化者来操作。这里当然属性使用，其实可以用PrintPage。
		/// 如PrinterPageSetting1.PrintPage += new PrintPageDelegate(this.PrintPageEventHandler);
		/// PrinterPageSetting1.PrintPageValue = new PrintPageDelegate(this.PrintPageEventHandler);
		/// </summary>
		public PrintPageDelegate PrintPageValue
		{
			set
			{	
				_printerPageSetting.PrintPageValue = value;
			}
			get
			{
				return _printerPageSetting.PrintPageValue;
			}
		}


		/// <summary>
		/// 当需要为当前页打印的输出时发生，如果对此不了解，就用PrintPageValue赋值也可以
		/// </summary>
		public event PrintPageDelegate PrintPage
		{
			add
			{
				_printerPageSetting.PrintPage += new PrintPageDelegate(value);
			}
			remove
			{
				_printerPageSetting.PrintPage -= new PrintPageDelegate(value);
			}		
		}

		/// <summary>
		/// 导出Excel的实现
		/// </summary>
		public ImportExcelDelegate ImportExcelValue
		{
			set
			{	
				_printerPageSetting.ImportExcelValue = value;				
			}
			get
			{
				return _printerPageSetting.ImportExcelValue;
			}		
		}

		#region	构造函数
		/// <summary>
		///  封装页面设置、打印机设置、打印预览，适合于Window和Asp.Net，Web方式下请在配置文件中增加键PrintMode及值Web，也可以实例化后设置PrintMode属性为PrintModeFlag.Web。
		/// </summary>
		public PrinterPageSetting():this(null)
		{
			
		}

		/// <summary>
		///  封装页面设置、打印机设置、打印预览，适合于Window和Asp.Net，Web方式下请在配置文件中增加键PrintMode及值Web，也可以实例化后设置PrintMode属性为PrintModeFlag.Web。
		/// </summary>
		/// <param name="printDocument">如果为null，则提供一个默认的PrintDocument</param>
		public PrinterPageSetting(PrintDocument printDocument)
		{
			string strPrintMode = "";

			//配置文件中键PrintMode
			strPrintMode = System.Configuration.ConfigurationSettings.AppSettings["PrintMode"];

			if (strPrintMode == null)
			{
				//默认为Win方式			
				strPrintMode = "Win";
			}

			//配置文件中键PrintMode的值Win/Web
			if (strPrintMode.ToUpper() == "WIN")
			{
				this.PrintMode = PrintModeFlag.Win;
			}
			else
			{
				this.PrintMode = PrintModeFlag.Web;			
			}

			/*  //配置文件中添加键值的写法
				<?xml version="1.0" encoding="utf-8" ?>
				<configuration>
					<appSettings>
							<add key="PrintMode" value="Web"/>	
					</appSettings>
				</configuration>
			*/

			if (printDocument != null)
			{
				_printerPageSetting.PrintDocument = printDocument;
			}

		}
		#endregion


		/// <summary>
		/// 显示页面设置对话框，并返回PageSettings
		/// </summary>
		/// <returns></returns>
		public PageSettings ShowPageSetupDialog()
		{
			return _printerPageSetting.ShowPageSetupDialog();
		}

		/// <summary>
		/// 显示打印机设置对话框，并返回PrinterSettings
		/// </summary>
		/// <returns></returns>
		public PrinterSettings ShowPrintSetupDialog()
		{
			return _printerPageSetting.ShowPrintSetupDialog();		
		}

		/// <summary>
		/// 显示打印预览对话框
		/// </summary>
		public void ShowPrintPreviewDialog()
		{
			_printerPageSetting.ShowPrintPreviewDialog();
		}

	}//End Class
}//End NameSpace
