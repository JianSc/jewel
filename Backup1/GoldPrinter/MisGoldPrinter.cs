using System;
using System.Drawing;
using System.Drawing.Printing;
using winForm = System.Windows.Forms;

//��MIS���ʴ�ӡͨ��һ�°棬�����ע...
//			Web��ӡ��XML���������ο�����ȫ��֧��...��
//					�����ḻ��ʽ��������ơ����˲�Ԥ��ӡƽ̨...

namespace GoldPrinter
{
	/// <summary>
	///	������Ϊͨ�ô�ӡ���򣬵��ݡ����ƾ֤����Ʊ�嵥���������⸴�ӱ�񡢺ϲ�����繤����ҵ��ͬ��������ϵͳ�ṩ�ļ���Ĭ��
	///	�ϴ�ӡ������ϴ�ӡ��
	///	DataGrid��DataTable��MSHFlexGrid�ȶ�ά��ʽȫ�����Դ�ӡ��
	///	���ֶ�����PrinterMargins��Sewing��GridLineFlag��GridMergeFlag���ṩͼ�����Դٽ���⡣
	///	���ڰ汾���ṩXML������SQL����Դ�Ĵ�ӡ�����ù��������������������ı�����ͼ��ȣ��û��������ⶨ�塣
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class MisGoldPrinter:IDisposable
	{
		//��ӡ����֮��ľ���
		private const int CON_SPACE_TITLE_CAPTION = 5;
		private const int CON_SPACE_CAPTION_TOP = 20;
		private const int CON_SPACE_HEADER_BODY = 5;
		private const int CON_SPACE_BODY_FOOTER = 5;

		//��һ�����������꼰��
		private int X,Y,Width;

		//���ű�
		private float Scale = 1.0F;

		//��ҳ��
		private int mCurrentPageIndex;		//��ǰҳ
		private int mCurrentRowIndex;		//����������ĵ�ǰ��


		//��ͼ����
		private Graphics mGraphics;	
		private Printer mPrinter;
		
		//��ӡ�ĵ�
		private PrintDocument mPrintDocument;
		//��ӡ��
		private PrinterMargins mPrinterMargins;
		//װ����������߳�С��0���Զ�����
		private Sewing _sewing;		

		private bool _isOnlySingleColor = true;	//������ɫ����ɫ����ӡ
		public Color BackColor = Color.White;	//������ɫ

		#region �ֶ�����

		/// <summary>
		/// װ�����󣬶�����߳�С��0���Զ����ã�Ϊnullʱ�������á�ע����ȫ�ֵ�
		/// </summary>
		public Sewing Sewing
		{
			get
			{
				return this._sewing;
			}
			set
			{
				if (value != null)
				{
					this._sewing = value;
				}
				else
				{
					this._sewing.Margin = 0;		//���Ϊ0�򲻴�ӡ
				}
			}
		}

		public string DocumentName
		{
			get
			{
				return this.mPrintDocument.DocumentName;
			}
			set
			{
				this.mPrintDocument.DocumentName = value;			
			}
		}

		#endregion
		

		//�ֶ�
		private int _rowsPerPage = -1;	 			//ÿҳ������С�ڵ���0����Ӧ��Ĭ��
		private bool _isSubTotalPerPage = false;	//�Ƿ�ÿҳ��Ҫ��ʾ����������ǰҳС�ƣ�Ĭ�Ϸ�
		private string _subTotalColsList = "";		//ÿҳС��Ҫָ������

		private bool _isSewingLine = false;			//�Ƿ��ӡװ����(Ĭ����)
		private bool _isPrinterMargins = false;		//�Ƿ��ӡ��Ч�������(Ĭ����)
		
		private GridBorderFlag _gridBorder = GridBorderFlag.Double;	//����߿�Ĭ��˫�߿�

		#region �ֶ�����

		/// <summary>
		/// ÿҳ������С�ڵ���0����Ӧ��Ĭ��
		/// </summary>
		public int RowsPerPage
		{
			get
			{
				return _rowsPerPage;
			}
			set
			{
				_rowsPerPage = value;
			}
		}

		/// <summary>
		/// �Ƿ�ÿҳ��Ҫ��ʾ��ǰҳС�ƣ�Ĭ�Ϸ�
		/// </summary>
		public bool IsSubTotalPerPage
		{
			get
			{
				return _isSubTotalPerPage;
			}
			set
			{
				_isSubTotalPerPage = value;
			}
		}

		/// <summary>
		/// �÷ֺŷָ���ҪÿҳС����
		/// </summary>
		public string SubTotalColsList
		{
			get
			{
				return _subTotalColsList;
			}
			set
			{
				_subTotalColsList = value;
			}
		}

		/// <summary>
		/// �Ƿ��ӡװ����,������߳�С�ڵ���0���Զ�����
		/// </summary>
		public bool IsSewingLine
		{
			get
			{
				return _isSewingLine;
			}
			set
			{
				_isSewingLine = value;
			}
		}

		/// <summary>
		/// �Ƿ��ӡ��Ч�������
		/// </summary>
		public bool IsPrinterMargins
		{
			get
			{
				return _isPrinterMargins;
			}
			set
			{
				_isPrinterMargins = value;
			}
		}

		/// <summary>
		/// ����߿�
		/// </summary>
		public GridBorderFlag GridBorder
		{
			get
			{
				return this._gridBorder;
			}
			set
			{
				this._gridBorder = value;
			}
		}

		#endregion

		//********************��ӡ����********************	
		private Title _title;				//������
		private Caption _caption;			//������
		private Top _top;					//�򵥵�һ�����д�ӡ��ʽ,��һ�о���,�����о���,�м��о���
		private Header _header;				//������������֮�ϵļ��м��еı�ע˵��
		
		private MultiHeader _multiHeader;	//���������������ͷ������Ҫ���ϲ���ͷ˵��
		private Body _body;					//��������������,���룬��ӡ�Դ�Ϊ��׼
		protected Footer _footer;			//������������֮�µļ��м��еı�ע˵��
		private Bottom _bottom;				//�򵥵�һ�����д�ӡ��ʽ,��һ�о���,�����о���,�м��о���

		#region ��ӡ�����ֶ�����

		#region Title��Caption ���⡢������
		/// <summary>
		/// ��ȡ�����ô�ӡ�����⣬�������ı���Ҳ�����Ƕ���������Ե�Title����
		/// </summary>
		public object Title
		{
			get
			{
				return this._title;
			}
			set
			{	
				if (value != null)
				{
					if (value.GetType().ToString() == "System.String")
					{
						if (this._title == null)
						{
							this._title = new Title();
						}
						this._title.Text = (string)value;
					}
					else if(value.GetType().ToString() == "GoldPrinter.Title")
					{
						this._title = (GoldPrinter.Title)value;
					}
				}
			}
		}

		/// <summary>
		/// ��ȡ�����ô�ӡ�����⣬�������ı���Ҳ�����Ƕ���������Ե�Caption����
		/// </summary>
		public object Caption
		{
			get
			{
				return this._caption;
			}
			set
			{	
				if (value != null)
				{
					if (value.GetType().ToString() == "System.String")
					{
						if (this._caption == null)
						{
							this._caption = new Caption();
						}
						this._caption.Text = (string)value;
					}
					else if(value.GetType().ToString() == "GoldPrinter.Caption")
					{
						this._caption = (GoldPrinter.Caption)value;
					}
				}
			}
		}
		#endregion

		#region ��ȡ���������񶥡��ף���������'|'�ָ����ַ�����һά�������и������Ե�Top/Bottom����
		/// <summary>
		/// ��ȡ����������ͷ����������'|'�ָ����ַ������һά�������и������Ե�Top����
		/// </summary>
		public object Top
		{
			get
			{
				return this._top;
			}
			set
			{	
				if (value != null)
				{
					if (value.GetType().ToString() == "System.String" || value.GetType().ToString() == "System.String[]")
					{
						if (this._top == null)
						{
							this._top = new Top();
						}
						this._top.DataSource = value;
					}
					else if(value.GetType().ToString() == "GoldPrinter.Top")
					{
						this._top = (GoldPrinter.Top)value;
					}
				}
			}
		}

		/// <summary>
		/// ��ȡ����������ף���������'|'�ָ����ַ������һά�������и������Ե�Bottom����
		/// </summary>
		public object Bottom
		{
			get
			{
				return this._bottom;
			}
			set
			{	
				if (value != null)
				{
					if (value.GetType().ToString() == "System.String" || value.GetType().ToString() == "System.String[]")
					{
						if (this._bottom == null)
						{
							this._bottom = new Bottom();
						}
						this._bottom.DataSource = (string)value;
					}
					else if(value.GetType().ToString() == "GoldPrinter.Bottom")
					{
						this._bottom = (GoldPrinter.Bottom)value;
					}
				}
			}
		}
		#endregion

		#region ��ȡ����������ͷ�����²���������һά���顢��ά���顢��DataTable����и������Ե�Header/Footer����
		/// <summary>
		/// ��ȡ����������ͷ��˵�����֣�������һά���顢��ά���顢��DataTable��Header��
		/// </summary>
		public object Header
		{
			get
			{
				return _header;
			}
			set
			{
				if (value != null)
				{
					if (value.GetType().ToString() == "System.String[]" || value.GetType().ToString() == "System.String[,]" || value.GetType().ToString() == "System.Data.DataTable")
					{
						if (this._header == null)
						{
							this._header = new Header();
						}
						this._header.DataSource = value;
					}
					else if(value.GetType().ToString() == "GoldPrinter.Header")
					{
						this._header = (GoldPrinter.Header)value;
					}
				}				
			}
		}

		/// <summary>
		/// ��ȡ�����������²�˵�����֣�������һά���顢��ά���顢��DataTable��Footer�ȵ�
		/// </summary>
		public object Footer
		{
			get
			{
				return this._footer;
			}
			set
			{
				if (value != null)
				{
					if (value.GetType().ToString() == "System.String[]" || value.GetType().ToString() == "System.String[,]" || value.GetType().ToString() == "System.Data.DataTable")
					{
						if (this._footer == null)
						{
							this._footer = new Footer();
						}
						this._footer.DataSource = value;
					}
					else if(value.GetType().ToString() == "GoldPrinter.Footer")
					{
						this._footer = (GoldPrinter.Footer)value;
					}
				}				
			}
		}

		#endregion
		
		#region ��ȡ�����������弰��Ӧ�ı��⣬������һά���顢��ά���顢��DataTable����и������Ե�MultiHeader/Body����
		
		/// <summary>
		/// ��ȡ�������������Ӧ�ı��⣬������һά���顢��ά���顢��DataTable����и������Ե�MultiHeader
		/// </summary>
		public object MultiHeader
		{
			get
			{
				return _multiHeader;
			}
			set
			{
				if (value != null)
				{
					if (value.GetType().ToString() == "System.String[]" || value.GetType().ToString() == "System.String[,]" || value.GetType().ToString() == "System.Data.DataTable")
					{
						if (this._multiHeader == null)
						{
							this._multiHeader = new MultiHeader();
						}
						this._multiHeader.DataSource = value;
					}
					else if(value.GetType().ToString() == "GoldPrinter.MultiHeader")
					{
						this._multiHeader = (GoldPrinter.MultiHeader)value;
					}
				}				
			}
		}

		/// <summary>
		/// ��ȡ�����������壬������һά���顢��ά���顢��DataTable����и������Ե�Body
		/// </summary>
		public object Body
		{
			get
			{
				return _body;
			}
			set
			{
				if (value != null)
				{
					if (value.GetType().ToString() == "System.String[]" || value.GetType().ToString() == "System.String[,]" || value.GetType().ToString() == "System.Data.DataTable")
					{
						if (this._body == null)
						{
							this._body = new Body();
						}
						this._body.DataSource = value;
					}
					else if(value.GetType().ToString() == "GoldPrinter.Body")
					{
						this._body = (GoldPrinter.Body)value;
					}
				}				
			}
		}

		#endregion

		#endregion


		//�����Խ��˳�����΢�޸�,��һ���������,��̬���ش�ӡ����,�γ�����������������,��ӡ���⸴�ӵ�����

		public MisGoldPrinter():this(false)
		{
			
		}

		public MisGoldPrinter(bool p_IsLandscape)
		{
			PrinterSingleton.Reset();

			mCurrentPageIndex = 1;
			mCurrentRowIndex = 0;

			//��һģʽ��ȫ����ӡ����ʹ��������ͬ�Ķ�����ߴ�ӡ�ٶ�Ч��
			mPrintDocument = PrinterSingleton.PrintDocument;

			mPrintDocument.DefaultPageSettings.Landscape = p_IsLandscape;

			mPrinterMargins = PrinterSingleton.PrinterMargins;


			mPrintDocument.DocumentName = "MIS���ʴ�ӡͨ����ӭʹ�ã�";


			_sewing = new Sewing(30,SewingDirectionFlag.Left);				
			
			mPrinter = new Printer();
			_body = new Body();			//��Ҫ��������ʵ����
		}

		#region IDisposable ��Ա

		public virtual void Dispose()
		{
			try
			{
				mGraphics.Dispose();
				mPrintDocument.Dispose();
			}
			catch{}
		}

		#endregion

		/// <summary>
		/// ��ȡ�����������������������Դ��������һά���顢��ά���顢��DataTable��DataGrid��MshFlexGrid��HtmlTable�ȶ�ά���񣬲�֧�ֵ��Լ�ת���ɶ�ά����
		/// </summary>
		public object DataSource
		{
			get
			{
				return this._body.DataSource;
			}
			set
			{
				this._body.DataSource = value;
			}
		}

		/// <summary>
		/// ҳ�����öԻ���
		/// </summary>
		public System.Drawing.Printing.PageSettings PageSetup()
		{
			PrinterPageSetting	printerPageSetting;	
			printerPageSetting = new PrinterPageSetting(mPrintDocument);
			printerPageSetting.PrintPage += new PrintPageDelegate(this.PrintPageEventHandler);
			
			PageSettings pstBack = mPrintDocument.DefaultPageSettings;
			PageSettings pstNew = printerPageSetting.ShowPageSetupDialog();

			if (pstBack != pstNew)
			{
				//�ı�ҳ�����ú󣬵�������
				PrinterSingleton.PrintDocument = mPrintDocument;
				mPrinterMargins = new PrinterMargins(mPrintDocument);
				PrinterSingleton.PrinterMargins = mPrinterMargins;
			}

			return pstNew;
		}

		/// <summary>
		/// ��ӡ���öԻ���
		/// </summary>
		public System.Drawing.Printing.PrinterSettings Print()
		{
			this.mCurrentPageIndex = 1;
			this.mCurrentRowIndex = 0;

			PrinterPageSetting	printerPageSetting;
			printerPageSetting = new PrinterPageSetting(mPrintDocument);
			printerPageSetting.PrintPage += new PrintPageDelegate(this.PrintPageEventHandler);

			return printerPageSetting.ShowPrintSetupDialog();	
		}


		/// <summary>
		/// ��ӡԤ���Ի���
		/// </summary>
		public void Preview()
		{
			this.mCurrentPageIndex = 1;
			this.mCurrentRowIndex = 0;

			PrinterPageSetting	printerPageSetting;		
			printerPageSetting = new PrinterPageSetting(mPrintDocument);
			printerPageSetting.PrintPage += new PrintPageDelegate(this.PrintPageEventHandler);

			//������Excel����ʵ��
			printerPageSetting.ImportExcelValue = new ImportExcelDelegate(this.ImportExcelMethodHandler);			
			printerPageSetting.ShowPrintPreviewDialog();		
		}

		public void ImportExcelMethodHandler(Object obj,ImportExcelArgs ev)
		{
			#region ʵ��...

			ExcelAccess excel = new ExcelAccess();	
			excel.Open();

			excel.MergeCells(1,1,1,this._body.Cols);				//�ϲ���Ԫ��д���⣬����������
			excel.SetFont(1,1,1,this._body.Cols,this._title.Font);
			excel.SetCellText(1,1,1,this._body.Cols,this._title.Text);

			//��ӡ����������
			excel.SetCellText((System.Data.DataTable)this.DataSource,3,1,true);
			
			System.Windows.Forms.FileDialog fileDlg = new System.Windows.Forms.SaveFileDialog();
			fileDlg.AddExtension = true;
			fileDlg.DefaultExt = ".xls";
		
			//fileDlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
			fileDlg.Title = "���浽Excel�ļ�";
			fileDlg.Filter = "Microsoft Office Excel ������(*.xls)|*.xls|ģ��(*.xlt)|*.xlt";
			
			if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				if (excel.SaveAs(fileDlg.FileName,true))
				{
					System.Windows.Forms.MessageBox.Show("���ݳɹ����浽Excel�ļ���","GoldPrinter",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
				}
			}

			fileDlg.Dispose();

			excel.Close();

			#endregion
		}


		//����		
		private void PrintPageEventHandler(object obj,System.Drawing.Printing.PrintPageEventArgs ev)
		{
			Graphics g = ev.Graphics ;
			
			this.mGraphics = g;

			//g.Clear(this.BackColor);

			//WriteMetricsToConsole(ev);

			try
			{	
				bool blnMore = this.Draw(g);
				
				if (blnMore)
				{
					ev.HasMorePages = true;
					mCurrentPageIndex++;
				}
				else
				{
					ev.HasMorePages = false;	

					//��ӡ��Ԥ����Ϻ����ã�������Ԥ�������д�ӡʱ�򲻳�������Body
					this.mCurrentPageIndex = 1;
					this.mCurrentRowIndex = 0;
				}
			}
			catch(Exception e)
			{
				//System.Windows.Forms.MessageBox.Show(e.Message);
			}
		}


		/// �����ӡ�ӿ�
		private void OutObject(Printer outer)
		{
			if (outer != null)
			{
				outer.Graphics = this.mGraphics;
				outer.Rectangle = new Rectangle(X,Y,Width,outer.Height);
	
				if (this._isOnlySingleColor)
				{
					outer.Pen = Pens.Black;
					outer.Brush = Brushes.Black;
				}

				outer.Draw();
				this.Y  += outer.Rectangle.Height;
			}
		}

		#region ���ƹ���

		/// <summary>
		/// ���ƹ���
		/// </summary>
		/// <param name="g"></param>
		/// <returns>һҳû�����귵��false</returns>
		private bool Draw(Graphics g)
		{
			bool blnHasMorePage = false;		//�Ƿ�����һҳ���

			if (this._body.Rows < 0)
			{
				throw new Exception("��ӡ��Ҫ������Ϊ�գ�����Body���ã�");
			}

			mPrinter.Graphics = g;
			mPrinter.PrintDocument = this.mPrintDocument;
			mPrinter.Sewing = this.Sewing;
			mPrinter.PrinterMargins = this.mPrinterMargins; 

			
			//�����ӡ������꼰��ӡ����Ŀ�
			Y = mPrinter.PrinterMargins.Top;
			X = mPrinter.PrinterMargins.Left;
			Width = mPrinter.PrinterMargins.Width;
                       
			//����ӡ����װ����
			this.DrawPrinterMargins(mPrinter);
			this.DrawSewing(mPrinter);
			
			//������ÿҳ���ظ���ӡ�������жϣ����ˣ�����ѡ��ɣ�
			if (_title != null && (mCurrentPageIndex == 1 || _title.IsDrawAllPage))
			{
				_title.PrinterMargins = mPrinterMargins;
				OutObject(_title);
			}

			if (_caption != null && (mCurrentPageIndex == 1 || _caption.IsDrawAllPage))
			{
				_caption.MoveY = 0;
				if (_title != null && (mCurrentPageIndex == 1 || _title.IsDrawAllPage))
				{
					_caption.MoveY = (int)this._title.Height + CON_SPACE_TITLE_CAPTION;					
				}
				_caption.PrinterMargins = mPrinterMargins;
				OutObject(_caption);
			}

			if (_title != null || _caption != null)
			{
				Y += CON_SPACE_CAPTION_TOP;			//������������һ������
			}

			//����ʵ�ʿ��
			int lngInfactWidth = 0;

			if (!this._body.IsAverageColsWidth)
			{
				for(int i=0;i<_body.ColsWidth.Length;i++)
				{
					lngInfactWidth += _body.ColsWidth[i];			
				}

				if (lngInfactWidth > this.mPrinterMargins.Width)
				{
					//����
					Scale = this.mPrinterMargins.Width/lngInfactWidth;
				}
				else
				{
					Width = lngInfactWidth;
					X += (this.mPrinterMargins.Width - Width)/2;
				}
			}


			if (_top != null && (mCurrentPageIndex == 1 || _top.IsDrawAllPage))
			{
				OutObject(_top);
			}

			if (_header != null && (mCurrentPageIndex == 1 || _header.IsDrawAllPage))
			{
				OutObject(_header);
			}

			if ((_top != null || _header != null) && (mCurrentPageIndex == 1 || (_top != null && _top.IsDrawAllPage) || (_header != null && _header.IsDrawAllPage)))
			{
				Y += CON_SPACE_HEADER_BODY;	//������ҳͷ����
			}

			if (_multiHeader != null && (mCurrentPageIndex == 1 || _multiHeader.IsDrawAllPage))
			{
				OutObject(_multiHeader);				
			}

            
			#region ������������

			//TimeDef.Start();

			//������Ч�߶ȣ����ڷ�ҳ
			float validHeight = mPrinter.PrinterMargins.Height - (Y - mPrinter.PrinterMargins.Top);
			if(_footer != null && _footer.IsDrawAllPage)
			{
				validHeight -= this._footer.Height;
			}
			if(_bottom != null && _bottom.IsDrawAllPage)
			{
				validHeight -= this._bottom.Height;
			}
			if (validHeight < 0)
			{
				throw new Exception("Ԥ������ӡ��Ҫ����Ŀռ�̫С�����ʵ�������");			
			}

			//��Ч�߶��е�ǰҳ����
			int mRowsInCurPage = 0;
			mRowsInCurPage = (int)(validHeight/(float)(this._body.RowHeight));

			//���ָ��ÿҳ������������Ϊ��
			if (this.RowsPerPage > 0 && this.RowsPerPage < mRowsInCurPage)
			{
				mRowsInCurPage = this.RowsPerPage;
			}

			if (this.IsSubTotalPerPage)
			{
				mRowsInCurPage--;
			}

			//TimeDef.End();

			//************��BodyΪ��************

			//TimeDef.Start();

			string[,] mArrGridText;			//������ǰҳ�ı�������ҳС��
			GoldPrinter.Body mbody;
			
			//���ָ��ÿҳ������������Ϊ��
			if (this.RowsPerPage > 0 && this.RowsPerPage < mRowsInCurPage)
			{
				mbody = new Body(mRowsInCurPage,this._body.Cols);
			}
			else
			{
				//��������Ӧ
				if (mRowsInCurPage > (this._body.Rows - this.mCurrentRowIndex))
				{
					mRowsInCurPage = this._body.Rows - this.mCurrentRowIndex;
				}
				mbody = new Body(mRowsInCurPage,this._body.Cols);			
			}

			//�浱ǰҳ�Ķ�ά�ı�
			mArrGridText = new string[mRowsInCurPage,this._body.Cols];
			for(int i = 0 ; i < mRowsInCurPage && mCurrentRowIndex < this._body.Rows ; i++)
			{
				for(int j = 0 ; j < this._body.Cols ; j++)
				{
					mArrGridText[i,j] = this._body.GetText(mCurrentRowIndex,j);
				}					
				mCurrentRowIndex++;
			}

			mbody.GridText = mArrGridText;

			mbody.ColsAlignString = this._body.ColsAlignString;
			mbody.ColsWidth = this._body.ColsWidth;
			mbody.IsAverageColsWidth = this._body.IsAverageColsWidth;
			mbody.Font = (Font)(this._body.Font.Clone());

			//TimeDef.End();

			//TimeDef.Start();
			OutObject(mbody);
					
			//TimeDef.End();
			//mArrGridText = null;


			//�ж��Ƿ�Ҫ��ҳ��ֻҪ�������������ݴ�������������ָ�룬������һҳ
			if (mCurrentRowIndex < this._body.Rows)
			{
				blnHasMorePage = true;
			}


            
			#region ��ӡÿҳС�ƣ�ֻ��Ҫ����ǰ������ѭ���ۼƾ�OK�ˣ���γ���Ӧר���ع�Ϊһ�����������߿����Լ���һ��
           
			if (_isSubTotalPerPage && _subTotalColsList != "")
			{
				try
				{
					GoldPrinter.MultiHeader mhSubTotal = new MultiHeader(1,this._body.Cols);
					mhSubTotal.ColsWidth = this._body.ColsWidth;
					mhSubTotal.Graphics = g;
					mhSubTotal.PrintDocument = this.mPrintDocument;
					mhSubTotal.Sewing = this._sewing;		

					mhSubTotal.Rectangle = new Rectangle(X,Y,Width,mhSubTotal.Height);		
					//ѭ��
					//....
					mhSubTotal.SetText(0,0,"��ҳС��");
					mhSubTotal.SetText(0,1,"��ҳС��");
				
					string[] marrSubTotalCol = this._subTotalColsList.Split(';');
					Double mdblSubTotal = 0f;
					int mintCol = 0;

					for(int i = 0 ; i < marrSubTotalCol.Length ; i ++)
					{
						mintCol = int.Parse(marrSubTotalCol[i]);

						for(int j = 0 ; j < mArrGridText.GetLength(0) ; j++)
						{
							mdblSubTotal += Double.Parse(mArrGridText[j,mintCol]);					
						}
						mhSubTotal.SetText(0,mintCol,mdblSubTotal.ToString());

						mdblSubTotal = 0;		//���ѡ�����һ�������ִ�bug��Ҫ������
					}


					mhSubTotal.Draw();			

					Y += mhSubTotal.Height;
				}
				catch
				{
				}
			}
            
			#endregion
            

			#endregion 

			if ((_footer != null || _bottom != null) && (mCurrentPageIndex == 1 ||  (_top != null && _top.IsDrawAllPage) || (_header != null && _header.IsDrawAllPage)))
			{
				Y += CON_SPACE_BODY_FOOTER;			//������ҳ�׾���
			}

			//��ӡҳ�������
			if (_footer != null)
			{
				//���һҳ�ش�
				if (blnHasMorePage == false || _footer.IsDrawAllPage)
				{	
					//���ÿҳ����ӡ����_footer��ҳʧȥ������
					if (_footer.IsDrawAllPage)
					{
						OutObject(_footer);
					}					
					else
					{
						//����ÿ���򣬵������һҳ�ش�_footer����ʱҪ����ҳ����
						//��Bodyͬ���Ĵ���
						//...
					}
                    
				}
			}

			if (_bottom != null)
			{
				if (blnHasMorePage == false || _bottom.IsDrawAllPage)
				{
					if (_bottom.IsDrawAllPage)
					{
						OutObject(_bottom);
					}
					else
					{
						//������Ч�߶�
						validHeight = mPrinter.PrinterMargins.Height - (Y - mPrinter.PrinterMargins.Top);
						if (validHeight < _bottom.Height)
						{
							blnHasMorePage = true;
						}
						else
						{
							OutObject(_bottom);
						}
						
					}
				}
			}
            

			//���߿�
			DrawBorder(g,this._multiHeader,mbody);

			mbody.Dispose();
			mbody = null;
			
			return blnHasMorePage;
		}

		#endregion


		/// <summary>
		/// ����ӡ����
		/// </summary>
		private void DrawPrinterMargins(Printer printer)
		{
			if (this.IsPrinterMargins)
			{
				printer.DrawPrinterMargins();
			}
		}

		/// <summary>
		/// ��װ����
		/// </summary>
		private void DrawSewing(Printer printer)
		{
			#region ʵ��...

			if (this.IsSewingLine && this.Sewing.Margin > 0)
			{
				//������߳�С��0���Զ�����
				if (this.Sewing.LineLen <= 0)
				{
					if (this.Sewing.SewingDirection == SewingDirectionFlag.Left)
					{
						this.Sewing.LineLen = printer.PageHeight;
					}
					else if (this.Sewing.SewingDirection == SewingDirectionFlag.Top)
					{
						this.Sewing.LineLen = printer.PageWidth;
					}

				}
				printer.Sewing = this.Sewing;
				printer.DrawSewing();
			}

			#endregion
		}

		/// <summary>
		/// ���߿�
		/// </summary>
		private void DrawBorder(Graphics g,MultiHeader multiHeader,Body body)
		{
			#region ʵ��...

			//����߿����
			Rectangle mrecGridBorder;
			int x,y,width,height;
			
			width = body.Rectangle.Width;
			height = body.Rectangle.Height;
			if (multiHeader != null)
			{
				x = multiHeader.Rectangle.X;
				y = multiHeader.Rectangle.Y;
				height += multiHeader.Rectangle.Height;
			}
			else
			{
				x = body.Rectangle.X;
				y = body.Rectangle.Y;
			}
			if (this.IsSubTotalPerPage)
			{
				GoldPrinter.MultiHeader m = new MultiHeader(1,1);
				height += m.RowHeight;
				m = null;
			}
			
			mrecGridBorder = new Rectangle(x,y,width,height);
			Pen pen = new Pen(Color.Black,1);

			GoldPrinter.DrawRectangle dr = new DrawRectangle();
			dr.Graphics = g;
			dr.Rectangle = mrecGridBorder;
			dr.Pen = pen;

			switch (GridBorder)
			{
				case GridBorderFlag.Single:
					dr.Draw();
					break;
				case GridBorderFlag.SingleBold:
					dr.Pen.Width = 2;
					dr.Draw();
					if (multiHeader != null)
					{
						dr.Rectangle = body.Rectangle;
						dr.DrawTopLine();
					}
					break;
				case GridBorderFlag.Double:
					dr.Draw();
					mrecGridBorder = new Rectangle(x-2,y-2,width+4,height+4);
					dr.Rectangle = mrecGridBorder;
					dr.Draw();
					break;				
				case GridBorderFlag.DoubleBold:
					dr.Draw();
					mrecGridBorder = new Rectangle(x-2,y-2,width+4,height+4);
					dr.Rectangle = mrecGridBorder;
					dr.Pen.Width = 2;
					dr.Draw();
					break;				
			}
			
			#endregion
		}

		/// <summary>
		/// ����װ���ķǴ�ӡ���򣬴�ӡ���������ƶ�
		/// </summary>
		private void AddSewingNonePrintArea()
		{
			if (this.Sewing.SewingDirection == SewingDirectionFlag.Left)
			{
				this.mPrinterMargins.Left += this.Sewing.Margin;
				this.mPrinterMargins.Width -= this.Sewing.Margin;
			}
			else if (this.Sewing.SewingDirection == SewingDirectionFlag.Top)
			{
				this.mPrinterMargins.Top += this.Sewing.Margin;
				this.mPrinterMargins.Height -= this.Sewing.Margin;			
			}
		} 

		private void WriteMetricsToConsole(PrintPageEventArgs ev)
		{
			#region ��ӡ�����Ϣ

			Graphics g = ev.Graphics;
			Console.WriteLine ("*****Information about the printer*****");
			Console.WriteLine("ֽ�ŵĴ�С  ev.PageSettings.PaperSize:" + ev.PageSettings.PaperSize);
			Console.WriteLine("��ӡ�ֱ���  ev.PageSettings.PrinterResolution:" + ev.PageSettings.PrinterResolution);
			Console.WriteLine("��ת�ĽǶ�  ev.PageSettings.PrinterSettings.LandscapeAngle" + ev.PageSettings.PrinterSettings.LandscapeAngle);
			Console.WriteLine("");
			Console.WriteLine ("*****Information about the page*****");
			Console.WriteLine("ҳ��Ĵ�С  ev.PageSettings.Bounds:" + ev.PageSettings.Bounds); 
			Console.WriteLine("ҳ��(ͬ��)  ev.PageBounds:" + ev.PageBounds); 
			Console.WriteLine("ҳ��ı߾�    ev.PageSettings.Margins.:" + ev.PageSettings.Margins); 
			Console.WriteLine("ҳ��ı߾�    ev.MarginBounds:" + ev.MarginBounds); 

			Console.WriteLine("ˮƽ�ֱ���    ev.Graphics.DpiX:" + ev.Graphics.DpiX ); 
			Console.WriteLine("��ֱ�ֱ���    ev.Graphics.DpiY:" + ev.Graphics.DpiY ); 

			ev.Graphics.SetClip(ev.PageBounds);
			Console.WriteLine("ev.Graphics.VisibleClipBounds:" + ev.Graphics.VisibleClipBounds);

			SizeF drawingSurfaceSize = new SizeF(
				ev.Graphics.VisibleClipBounds.Width * ev.Graphics.DpiX/100,
				ev.Graphics.VisibleClipBounds.Height * ev.Graphics.DpiY/100);
			Console.WriteLine("drawing Surface Size in Pixels" + drawingSurfaceSize);

			#endregion
		}

	}//End class
}//End Namespace
