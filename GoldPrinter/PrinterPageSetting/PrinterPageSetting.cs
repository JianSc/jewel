using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{

	/// <summary>
	/// ��װҳ�����á���ӡ�����á���ӡԤ�����ʺ���Window��Asp.Net��Web��ʽ�����������ļ������Ӽ�PrintMode��ֵWeb��
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class PrinterPageSetting
	{
		//����һ����װҳ�����á���ӡ�����á���ӡԤ���Ľӿ�
		private IPrinterPageSetting _printerPageSetting;

		//��ӡ��ʽ
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
				//�����˳���ģʽ�������ӿڶ������ʵ����ʹ��ָ��������ʵ����ӿڵĶ���
				if (this._printModeFlag == PrintModeFlag.Win)
				{
					_printerPageSetting = new WinPrinterPageSetting();
				}
				else if(this._printModeFlag == PrintModeFlag.Web)
				{
					//ע�⣬Web��ʽ�µĳ��������й�עWWW.AlinkSoft.COM
					_printerPageSetting = new WebPrinterPageSetting();
				}
			}
		}

		/// <summary>
		/// ��ȡ�����ô�ӡ�ĵ�
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
		/// ����һ��������Ŀ�����þ���Ĵ�ӡ��ʵ�����������������ﵱȻ����ʹ�ã���ʵ������PrintPage��
		/// ��PrinterPageSetting1.PrintPage += new PrintPageDelegate(this.PrintPageEventHandler);
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
		/// ����ҪΪ��ǰҳ��ӡ�����ʱ����������Դ˲��˽⣬����PrintPageValue��ֵҲ����
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
		/// ����Excel��ʵ��
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

		#region	���캯��
		/// <summary>
		///  ��װҳ�����á���ӡ�����á���ӡԤ�����ʺ���Window��Asp.Net��Web��ʽ�����������ļ������Ӽ�PrintMode��ֵWeb��Ҳ����ʵ����������PrintMode����ΪPrintModeFlag.Web��
		/// </summary>
		public PrinterPageSetting():this(null)
		{
			
		}

		/// <summary>
		///  ��װҳ�����á���ӡ�����á���ӡԤ�����ʺ���Window��Asp.Net��Web��ʽ�����������ļ������Ӽ�PrintMode��ֵWeb��Ҳ����ʵ����������PrintMode����ΪPrintModeFlag.Web��
		/// </summary>
		/// <param name="printDocument">���Ϊnull�����ṩһ��Ĭ�ϵ�PrintDocument</param>
		public PrinterPageSetting(PrintDocument printDocument)
		{
			string strPrintMode = "";

			//�����ļ��м�PrintMode
			strPrintMode = System.Configuration.ConfigurationSettings.AppSettings["PrintMode"];

			if (strPrintMode == null)
			{
				//Ĭ��ΪWin��ʽ			
				strPrintMode = "Win";
			}

			//�����ļ��м�PrintMode��ֵWin/Web
			if (strPrintMode.ToUpper() == "WIN")
			{
				this.PrintMode = PrintModeFlag.Win;
			}
			else
			{
				this.PrintMode = PrintModeFlag.Web;			
			}

			/*  //�����ļ�����Ӽ�ֵ��д��
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
		/// ��ʾҳ�����öԻ��򣬲�����PageSettings
		/// </summary>
		/// <returns></returns>
		public PageSettings ShowPageSetupDialog()
		{
			return _printerPageSetting.ShowPageSetupDialog();
		}

		/// <summary>
		/// ��ʾ��ӡ�����öԻ��򣬲�����PrinterSettings
		/// </summary>
		/// <returns></returns>
		public PrinterSettings ShowPrintSetupDialog()
		{
			return _printerPageSetting.ShowPrintSetupDialog();		
		}

		/// <summary>
		/// ��ʾ��ӡԤ���Ի���
		/// </summary>
		public void ShowPrintPreviewDialog()
		{
			_printerPageSetting.ShowPrintPreviewDialog();
		}

	}//End Class
}//End NameSpace
