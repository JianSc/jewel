using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// 定义打印过程中单件,即在系统过程中只会实例化一次的对象，并且只有同一个对象。本类不能实例化，但提供静态方法
	/// 原因之一是大家公用，但最重要的一个原因是改变PrintDocument会引起PrinterMargins的变化，而每次计算需要花好多的时间，影响效率。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class PrinterSingleton
	{
        private PrinterSingleton(){}

        private static PrintDocument thePrintDocumentInstance = null;
        private static PrinterMargins thePrinterMarginsInstance = null;

		public static void Reset()
		{
			if (thePrintDocumentInstance != null)
			{
				thePrintDocumentInstance.Dispose();
			}

			thePrintDocumentInstance = null;
			thePrinterMarginsInstance = null;
		}

		public static PrintDocument PrintDocument
		{
			get
			{
				return GetPrintDocumentInstance();
			}
			set
			{
				thePrintDocumentInstance = value;
			}
		}

		public static PrinterMargins PrinterMargins
		{
			get
			{
				return GetPrinterMarginsInstance();
			}
			set
			{
				thePrinterMarginsInstance = value;
			}
		}

        private static PrintDocument GetPrintDocumentInstance()
        {
            if (thePrintDocumentInstance == null)
            {
                thePrintDocumentInstance = new PrintDocument();
            }

            return thePrintDocumentInstance;
        }

        private static PrinterMargins GetPrinterMarginsInstance()
        {
            if (thePrinterMarginsInstance == null)
            {
                thePrinterMarginsInstance = new PrinterMargins(PrinterSingleton.GetPrintDocumentInstance());
            }

            return thePrinterMarginsInstance;
        }

	}//End Class
}//End NameSpace