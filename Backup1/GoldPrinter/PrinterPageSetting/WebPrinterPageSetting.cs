using System;

namespace GoldPrinter
{
	/// <summary>
	/// WebForm�µĴ�ӡֽ�����á���ӡ�����á���ӡԤ���Ի��򡣣�***����ʵ�֣���鿴���߹���Web��ӡ�������������й�ע������վ��Blog������Ϣ***����
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class WebPrinterPageSetting:IPrinterPageSetting
	{
		public WebPrinterPageSetting()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region IPrinterPageSetting ��Ա

		public System.Drawing.Printing.PrintDocument PrintDocument
		{
			get
			{
				// TODO:  ��� WebPrinterPageSetting.PrintDocument getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� WebPrinterPageSetting.PrintDocument setter ʵ��
			}
		}

		public GoldPrinter.PrintPageDelegate PrintPageValue
		{
			get
			{
				// TODO:  ��� WebPrinterPageSetting.PrintPageValue getter ʵ��
				return null;
			}
			set
			{
				// TODO:  ��� WebPrinterPageSetting.PrintPageValue setter ʵ��
			}
		}

		public event GoldPrinter.PrintPageDelegate PrintPage;

		public ImportExcelDelegate ImportExcelValue
		{
			get
			{
				// TODO:  ���ʵ��
				return null;
			}
			set
			{
				// TODO:  ���ʵ��
			}
		}
		
		public System.Drawing.Printing.PageSettings ShowPageSetupDialog()
		{
			// TODO:  ��� WebPrinterPageSetting.ShowPageSetupDialog ʵ��
			return null;
		}

		public System.Drawing.Printing.PrinterSettings ShowPrintSetupDialog()
		{
			// TODO:  ��� WebPrinterPageSetting.ShowPrintSetupDialog ʵ��
			return null;
		}

		public void ShowPrintPreviewDialog()
		{
			// TODO:  ��� WebPrinterPageSetting.ShowPrintPreviewDialog ʵ��
		}

		#endregion

	}//End Class

}//End NameSpace