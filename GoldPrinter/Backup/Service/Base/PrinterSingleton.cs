using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// �����ӡ�����е���,����ϵͳ������ֻ��ʵ����һ�εĶ��󣬲���ֻ��ͬһ�����󡣱��಻��ʵ���������ṩ��̬����
	/// ԭ��֮һ�Ǵ�ҹ��ã�������Ҫ��һ��ԭ���Ǹı�PrintDocument������PrinterMargins�ı仯����ÿ�μ�����Ҫ���ö��ʱ�䣬Ӱ��Ч�ʡ�
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
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