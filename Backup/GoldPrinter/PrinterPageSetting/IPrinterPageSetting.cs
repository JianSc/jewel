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
	/// IPrinterPageSetting �Ľӿڣ���ʾ��ӡֽ�����á���ӡ�����á���ӡԤ���Ի���
	/// 
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public interface IPrinterPageSetting
	{
		/// <summary>
		/// ��ȡ�����ô�ӡ�ĵ�
		/// </summary>
		System.Drawing.Printing.PrintDocument PrintDocument
		{
			get;
			set;
		}

		/// <summary>
		/// ����һ��������Ŀ�����þ���Ĵ�ӡ��ʵ�����������������ﵱ����ʹ�ã���ʵ������PrintPage��
		/// ��PrinterPageSetting1.PrintPage = new PrintPageDelegate(this.PrintPageEventHandler);
		/// </summary>
		PrintPageDelegate PrintPageValue
		{
			get;
			set;
		}

		/// <summary>
		/// ����ҪΪ��ǰҳ��ӡ�����ʱ����������Դ˲��˽⣬����PrintPageValue��ֵҲ����
		/// </summary>
		event PrintPageDelegate PrintPage;


		/// <summary>
		/// ������Excelί�ж����ʵ��
		/// </summary>
		ImportExcelDelegate ImportExcelValue
		{
			get;
			set;		
		}

		/// <summary>
		/// ��ʾҳ�����öԻ��򣬲�����PageSettings
		/// </summary>
		/// <returns></returns>
		System.Drawing.Printing.PageSettings ShowPageSetupDialog();

		/// <summary>
		/// ��ʾ��ӡ�����öԻ��򣬲�����PrinterSettings
		/// </summary>
		/// <returns></returns>
		System.Drawing.Printing.PrinterSettings ShowPrintSetupDialog();

		/// <summary>
		/// ��ʾ��ӡԤ���Ի���
		/// </summary>
		void ShowPrintPreviewDialog();

	}//End Interface
}//End NameSpace
