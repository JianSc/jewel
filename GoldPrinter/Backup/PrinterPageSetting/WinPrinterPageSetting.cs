using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace GoldPrinter
{
	/// <summary>
	/// WinForm�µĴ�ӡֽ�����á���ӡ�����á���ӡԤ���Ի���
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class WinPrinterPageSetting:IPrinterPageSetting
	{
		//��PrintPageί������Ϊ���һ����Ա����
		private PrintPageDelegate _printPageValue;	
		//�ѵ�����Excelί������Ϊ���һ����Ա����
		private ImportExcelDelegate _importExcelValue;	
	
		// ��ӡ�ĵ�
		private PrintDocument _printDocument;

		#region	���캯��
		public WinPrinterPageSetting():this(null)
		{
			
		}

		/// <summary>
		/// ʹ��printDocument����ʼ�������ʵ������printDocumentΪnullʱ�Զ�����һ��printDocument��ʵ��
		/// </summary>
		/// <param name="printDocument">���Ϊnull�����ṩһ��Ĭ�ϵ�PrintDocument</param>
		public WinPrinterPageSetting(PrintDocument printDocument)
		{
			if (printDocument != null)
			{
				_printDocument = printDocument;
			}
			else
			{
				_printDocument = new PrintDocument();
			}
		}
		#endregion


		#region IPrinterPageSetting ��Ա

		/// <summary>
		/// ��ȡ�����ô�ӡ�ĵ�
		/// </summary>
		public PrintDocument PrintDocument
		{
			get
			{
				return this._printDocument;
			}
			set
			{
				this._printDocument = value;
			}
		}

		/// <summary>
		/// һ��Ҫʵ����������ڵ��ô�ӡ/Ԥ��֮ǰ���ô����ԣ�ʹ֮����һ��������Ŀ�����þ���Ĵ�ӡ��ʵ�����������������ﵱ����ʹ�ã���ʵ������PrintPage��
		/// ��˼���Ǹ���printerPageSetting��ӡ�ľ���ʵ�ֹ�����PrintPageEventHandler
		/// C#���ã�
		///		PrinterPageSetting1.PrintPageValue = new PrintPageDelegate(this.PrintPageEventHandler);
		/// VB���ã�    
		///    Me.printerPageSetting.PrintPageValue = New GoldPrinter.PrintPageDelegate(AddressOf printDocument_PrintPage)
		/// </summary>
		public PrintPageDelegate PrintPageValue
		{
			set
			{	
				//��ʼί�б�������������
				_printPageValue = value;
				
				_printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this._printPageValue);
			}
			get
			{
				return _printPageValue;
			}		
		}

		/// <summary>
		/// ����ҪΪ��ǰҳ��ӡ�����ʱ����������Դ˲��˽⣬����PrintPageValue��ֵҲ����
        /// C#���ã�
        ///		PrinterPageSetting1.PrintPage += new PrintPageDelegate(this.PrintPageEventHandler);
        /// VB���ã�    
        ///    Me.printerPageSetting.PrintPageValue = New GoldPrinter.PrintPageDelegate(AddressOf printDocument_PrintPage)
        /// </summary>
		public event PrintPageDelegate PrintPage
		{
			add
			{
				_printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(value);
				_printPageValue = value;
			}
			remove
			{
				_printDocument.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(value);
				_printPageValue = null;
			}		
		}

		/// <summary>
		/// ����Excel��ʵ��
		/// </summary>
		public ImportExcelDelegate ImportExcelValue
		{
			set
			{	
				//��ʼί�б�������������
				_importExcelValue = value;				
			}
			get
			{
				return _importExcelValue;
			}		
		}

		/// <summary>
		/// ��ʾҳ�����öԻ��򣬲�����PageSettings
		/// </summary>
		/// <returns></returns>
		public PageSettings ShowPageSetupDialog()
		{
			return ShowPageSetupDialog(this._printDocument);
		}

		/// <summary>
		/// ��ʾ��ӡ�����öԻ��򣬲�����PrinterSettings
		/// </summary>
		/// <returns></returns>
		public PrinterSettings ShowPrintSetupDialog()
		{
			return ShowPrintSetupDialog(this._printDocument);
		}

		/// <summary>
		/// ��ʾ��ӡԤ���Ի���
		/// </summary>
		public void ShowPrintPreviewDialog()
		{
			ShowPrintPreviewDialog(this._printDocument);
		}

		#endregion

	
        #region ***************ҳ������\��ӡ����\��ӡԤ���Ի��򣬿��Զ���ʹ��***************
		#region ҳ�����öԻ��� protected virtual PageSettings ShowPageSetupDialog(PrintDocument printDocument)
		/// <summary>
		/// ҳ�����öԻ��򣬿��Զ���ʹ��
		/// </summary>
		/// <param name="printDocument"></param>
		/// <returns></returns>
		/// <remarks>
		/// ��    �ߣ��ܷ���
		/// �޸����ڣ�2004-08-07
		/// </remarks>
		protected virtual PageSettings ShowPageSetupDialog(PrintDocument printDocument)
		{
            //���printDocument�Ƿ�Ϊ�գ��յĻ��׳��쳣
            ThrowPrintDocumentNullException(printDocument);

			//��������ֵ��PageSettings
			PageSettings ps = new PageSettings();

			//������ʵ����PageSetupDialog
			PageSetupDialog psDlg = new PageSetupDialog();

			ps = printDocument.DefaultPageSettings;

			try
			{			
				//����ĵ����ĵ�ҳ��Ĭ������
				psDlg.Document = printDocument;

				Margins mg = printDocument.DefaultPageSettings.Margins;
				if (System.Globalization.RegionInfo.CurrentRegion.IsMetric)
				{
					mg = PrinterUnitConvert.Convert(mg, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
				}

				//���ݴ�ӡ�ĵ���DefaultPageSettings����Ϊת�����ı䣬�����öԻ��򵥻�ȡ����ť�󲻻�ԭ�Ͳ�����ȷ��ʾԭ����ֵ
				PageSettings psPrintDocumentBack = (PageSettings)(printDocument.DefaultPageSettings.Clone());

				psDlg.PageSettings = psPrintDocumentBack;//printDocument.DefaultPageSettings; //��printDocument��ʱȡ���˶Ի����Ҫ��ԭ
				psDlg.PageSettings.Margins = mg;


				//��ʾ�Ի���
				DialogResult result = psDlg.ShowDialog();
				if (result == DialogResult.OK)
				{
					ps = psDlg.PageSettings;
					printDocument.DefaultPageSettings = psDlg.PageSettings;
				}
				else
				{
					
				}


			}
            catch(System.Drawing.Printing.InvalidPrinterException e)
            {
                ShowInvalidPrinterException(e);
            }
            catch(Exception ex)
            {
                ShowPrinterException(ex);
            }
            finally
			{
				psDlg.Dispose();
				psDlg = null;
			}

			return ps;
		}
		#endregion


		#region ��ӡ���öԻ��� protected virtual PrinterSettings ShowPrintSetupDialog(PrintDocument printDocument)
		/// <summary>
		/// ��ӡ���öԻ��򣬿��Զ���ʹ��
		/// </summary>
		/// <param name="printDocument"></param>
		/// <returns></returns>
		/// <remarks>
		/// ��    �ߣ��ܷ���
		/// �޸����ڣ�2004-08-07
		/// </remarks>
		protected virtual PrinterSettings ShowPrintSetupDialog(PrintDocument printDocument)
		{
            //���printDocument�Ƿ�Ϊ�գ��յĻ��׳��쳣
            ThrowPrintDocumentNullException(printDocument);

			//��������ֵ��PrinterSettings
			PrinterSettings ps = new PrinterSettings();
			//������ʵ����PrintDialog
			PrintDialog pDlg = new PrintDialog();

			try
			{				
				//����ѡ��ҳ
				pDlg.AllowSomePages = true;

				//ָ����ӡ�ĵ�
				pDlg.Document = printDocument;

				//��ʾ�Ի���
				DialogResult result = pDlg.ShowDialog();
				if (result == DialogResult.OK)
				{
					//�����ӡ����
					ps = pDlg.PrinterSettings;
					//��ӡ
					printDocument.Print();
				}

			}
            catch(System.Drawing.Printing.InvalidPrinterException e)
            {
                ShowInvalidPrinterException(e);
            }
            catch(Exception ex)
            {
                ShowPrinterException(ex);
            }
            finally
			{
				pDlg.Dispose();
				pDlg = null;
			}

			return ps;
		}
		#endregion


		private void Excel_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			ToolBarButton tb = e.Button;

			string strToolTipText = tb.ToolTipText;

			if (strToolTipText == "Import Excel" && (this._importExcelValue != null))
			{
				//����
				this._importExcelValue.BeginInvoke(sender,null,null,null);		
			}
		}

		#region ��ӡԤ���Ի��� protected virtual void ShowPrintPreviewDialog(PrintDocument printDocument)
		/// <summary>
		/// ��ӡԤ���Ի��򣬿��Զ���ʹ��
		/// </summary>
		/// <param name="printDocument"></param>
		/// <returns></returns>
		/// <remarks>
		/// ��    �ߣ��ܷ���
		/// �޸����ڣ�2004-08-07
		/// </remarks>
		protected virtual void ShowPrintPreviewDialog(PrintDocument printDocument)
		{
            //���printDocument�Ƿ�Ϊ�գ��յĻ��׳��쳣
			ThrowPrintDocumentNullException(printDocument);

			//������ʵ����PrintPreviewDialog
			PrintPreviewDialog ppDlg = new PrintPreviewDialog();
			
			//ppDlg.Icon = new Icon("..\\.....myIcon.ico");
			//ppDlg.Text = "��ӡԤ��--��ӭʹ��";					//PrintPreviewDialog()�̳�Forms�������Ȼ�������⼸������
			ppDlg.Text = printDocument.DocumentName;
			ppDlg.WindowState = FormWindowState.Maximized;

			//ppDlg.Controls[0].BackColor = Color.Red;		//��ӡԤ���ؼ���ɫֽ������

			
			if (this._importExcelValue != null)
			{
				ToolBar tb = null;
				
				if (ppDlg.Controls[1] is ToolBar)
				{
					//Vs2003
					tb = (ToolBar)ppDlg.Controls[1];		//��ӡԤ���ؼ�ToolBar����			
					ToolBarButton toolbtn = new ToolBarButton();
					toolbtn.ToolTipText = "Import Excel";
			
					toolbtn.ImageIndex = 2;
					tb.ButtonClick +=new ToolBarButtonClickEventHandler(Excel_ButtonClick);
					tb.Buttons.Add(toolbtn);
				}
				else
				{
					//Vs2005
					//VS2005��ToolBar�ѱ�ToolStripȡ����ֻ��Ϊ�����¼��ݻ�����ToolBar�ࡣ
					//tb = ((ToolStrip)ppDlg.Controls[1]).Items.Add(new ToolStripButton("Excel"));				
				}
			}
			
			try
			{				
				//ָ����ӡ�ĵ�
				ppDlg.Document = printDocument;	
						
				//��ʾ�Ի���
//				ppDlg.FindForm().Visible = false;

				
				DialogResult result = ppDlg.ShowDialog();

//				ppDlg.FindForm().Visible = true;
				if (result == DialogResult.OK)
				{
					//...
				}

			}
			catch(System.Drawing.Printing.InvalidPrinterException e)
			{
				ShowInvalidPrinterException(e);
			}
			catch(Exception ex)
			{
				ShowPrinterException(ex);
			}
			finally
			{
				ppDlg.Dispose();
				ppDlg = null;
			}
		}
		#endregion


        #region �Ի���֧�ֺ�����  �ع����ڣ�2004-09-03
        /// <summary>
        /// ���printDocument�Ƿ�Ϊ�գ��յĻ��׳��쳣
        /// </summary>
        /// <param name="printDocument"></param>
        protected virtual void ThrowPrintDocumentNullException(PrintDocument printDocument)
        {
            if (printDocument==null)
            {
                throw new Exception("�����Ĵ�ӡ�ĵ�����Ϊ�գ�");
            }        
        }

        /// <summary>
        /// ��ʾû��װ��ӡ��ʱ����ʾ��Ϣ
        /// </summary>
        /// <param name="e"></param>
        protected virtual void ShowInvalidPrinterException(System.Drawing.Printing.InvalidPrinterException e)
        {
            MessageBox.Show("δ��װ��ӡ���������ϵͳ���������Ӵ�ӡ����","��ӡ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ��ʾ��ӡ������������ʾ��Ϣ
        /// </summary>
        /// <param name="ex"></param>
        protected virtual void ShowPrinterException(Exception ex)
        {
            MessageBox.Show(ex.Message,"��ӡ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
        #endregion
        #endregion

	}//End Class
}//End NameSpace