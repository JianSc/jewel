using System;
using System.Drawing;
using System.Drawing.Printing;
using winForm = System.Windows.Forms;

//《MIS金质打印通》一下版，敬请关注...
//			Web打印、XML解析、二次开发，全面支持...　
//					　　丰富格式、自由设计、人人参预打印平台...

namespace GoldPrinter
{
	/// <summary>
	///	本程序为通用打印程序，单据、会计凭证、发票清单、报表、任意复杂表格、合并表格如工矿企业合同都可以由系统提供的几个默。
	///	认打印对象组合打印。
	///	DataGrid、DataTable、MSHFlexGrid等二维形式全部可以打印。
	///	部分对象如PrinterMargins、Sewing、GridLineFlag、GridMergeFlag等提供图例，以促进理解。
	///	后期版本将提供XML描述、SQL数据源的打印，并用管理器管理任意多个网格、文本对象、图象等，用户可以随意定义。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class MisGoldPrinter:IDisposable
	{
		//打印对象之间的距离
		private const int CON_SPACE_TITLE_CAPTION = 5;
		private const int CON_SPACE_CAPTION_TOP = 20;
		private const int CON_SPACE_HEADER_BODY = 5;
		private const int CON_SPACE_BODY_FOOTER = 5;

		//下一对象的起点坐标及宽
		private int X,Y,Width;

		//缩放比
		private float Scale = 1.0F;

		//翻页用
		private int mCurrentPageIndex;		//当前页
		private int mCurrentRowIndex;		//主数据网格的当前行


		//绘图表面
		private Graphics mGraphics;	
		private Printer mPrinter;
		
		//打印文档
		private PrintDocument mPrintDocument;
		//打印区
		private PrinterMargins mPrinterMargins;
		//装订，对象的线长小于0则自动设置
		private Sewing _sewing;		

		private bool _isOnlySingleColor = true;	//仅仅单色（黑色）打印
		public Color BackColor = Color.White;	//背景颜色

		#region 字段属性

		/// <summary>
		/// 装订对象，对象的线长小于0则自动设置，为null时不起作用。注意是全局的
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
					this._sewing.Margin = 0;		//宽度为0则不打印
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
		

		//字段
		private int _rowsPerPage = -1;	 			//每页行数，小于等于0自适应，默认
		private bool _isSubTotalPerPage = false;	//是否每页都要显示主数据网格当前页小计（默认否）
		private string _subTotalColsList = "";		//每页小计要指定的列

		private bool _isSewingLine = false;			//是否打印装订线(默认无)
		private bool _isPrinterMargins = false;		//是否打印有效区域矩阵(默认无)
		
		private GridBorderFlag _gridBorder = GridBorderFlag.Double;	//网格边框（默认双边框）

		#region 字段属性

		/// <summary>
		/// 每页行数，小于等于0自适应，默认
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
		/// 是否每页都要显示当前页小计（默认否）
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
		/// 用分号分隔的要每页小计列
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
		/// 是否打印装订线,对象的线长小于等于0则自动设置
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
		/// 是否打印有效区域矩阵
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
		/// 网格边框
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

		//********************打印对象********************	
		private Title _title;				//主标题
		private Caption _caption;			//副标题
		private Top _top;					//简单的一行三列打印样式,第一列居左,第三列居右,中间列居中
		private Header _header;				//正文网格主体之上的几行几列的标注说明
		
		private MultiHeader _multiHeader;	//正文网格主体标题头可能需要多层合并表头说明
		private Body _body;					//＊正文网格主体,必须，打印以此为基准
		protected Footer _footer;			//正文网格主体之下的几行几列的标注说明
		private Bottom _bottom;				//简单的一行三列打印样式,第一列居左,第三列居右,中间列居中

		#region 打印对象字段属性

		#region Title、Caption 标题、副标题
		/// <summary>
		/// 获取或设置打印主标题，可以是文本，也可以是定义更多特性的Title对象
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
		/// 获取或设置打印副标题，可以是文本，也可以是定义更多特性的Caption对象
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

		#region 获取或设置网格顶、底，可以是以'|'分隔的字符串或一维数组或具有更多特性的Top/Bottom对象
		/// <summary>
		/// 或取或设置网格头，可以是以'|'分隔的字符串或或一维数组或具有更多特性的Top对象
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
		/// 或取或设置网格底，可以是以'|'分隔的字符串或或一维数组或具有更多特性的Bottom对象
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

		#region 获取或设置网格头部、下部，可以是一维数组、二维数组、或DataTable或具有更多特性的Header/Footer对象
		/// <summary>
		/// 或取或设置网格头部说明部分，可以是一维数组、二维数组、或DataTable或Header等
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
		/// 或取或设置网格下部说明部分，可以是一维数组、二维数组、或DataTable或Footer等等
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
		
		#region 获取或设置网格体及对应的标题，可以是一维数组、二维数组、或DataTable或具有更多特性的MultiHeader/Body对象
		
		/// <summary>
		/// 获取或设置网格体对应的标题，可以是一维数组、二维数组、或DataTable或具有更多特性的MultiHeader
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
		/// 获取或设置网格体，可以是一维数组、二维数组、或DataTable或具有更多特性的Body
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


		//还可以将此程序稍微修改,用一个集体管理,动态加载打印对象,形成任意多个网格的组合体,打印任意复杂的网格

		public MisGoldPrinter():this(false)
		{
			
		}

		public MisGoldPrinter(bool p_IsLandscape)
		{
			PrinterSingleton.Reset();

			mCurrentPageIndex = 1;
			mCurrentRowIndex = 0;

			//单一模式，全部打印对象使用下面相同的对象，提高打印速度效率
			mPrintDocument = PrinterSingleton.PrintDocument;

			mPrintDocument.DefaultPageSettings.Landscape = p_IsLandscape;

			mPrinterMargins = PrinterSingleton.PrinterMargins;


			mPrintDocument.DocumentName = "MIS金质打印通，欢迎使用！";


			_sewing = new Sewing(30,SewingDirectionFlag.Left);				
			
			mPrinter = new Printer();
			_body = new Body();			//主要对象，所以实例化
		}

		#region IDisposable 成员

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
		/// 获取或设置数据主体网格的数据源，可以是一维数组、二维数组、或DataTable或DataGrid、MshFlexGrid、HtmlTable等二维网格，不支持的自己转换成二维数组
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
		/// 页面设置对话框。
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
				//改变页面设置后，单件重置
				PrinterSingleton.PrintDocument = mPrintDocument;
				mPrinterMargins = new PrinterMargins(mPrintDocument);
				PrinterSingleton.PrinterMargins = mPrinterMargins;
			}

			return pstNew;
		}

		/// <summary>
		/// 打印设置对话框。
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
		/// 打印预览对话框。
		/// </summary>
		public void Preview()
		{
			this.mCurrentPageIndex = 1;
			this.mCurrentRowIndex = 0;

			PrinterPageSetting	printerPageSetting;		
			printerPageSetting = new PrinterPageSetting(mPrintDocument);
			printerPageSetting.PrintPage += new PrintPageDelegate(this.PrintPageEventHandler);

			//导出到Excel方法实现
			printerPageSetting.ImportExcelValue = new ImportExcelDelegate(this.ImportExcelMethodHandler);			
			printerPageSetting.ShowPrintPreviewDialog();		
		}

		public void ImportExcelMethodHandler(Object obj,ImportExcelArgs ev)
		{
			#region 实现...

			ExcelAccess excel = new ExcelAccess();	
			excel.Open();

			excel.MergeCells(1,1,1,this._body.Cols);				//合并单元格写标题，并设置字体
			excel.SetFont(1,1,1,this._body.Cols,this._title.Font);
			excel.SetCellText(1,1,1,this._body.Cols,this._title.Text);

			//打印网格及网格线
			excel.SetCellText((System.Data.DataTable)this.DataSource,3,1,true);
			
			System.Windows.Forms.FileDialog fileDlg = new System.Windows.Forms.SaveFileDialog();
			fileDlg.AddExtension = true;
			fileDlg.DefaultExt = ".xls";
		
			//fileDlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
			fileDlg.Title = "保存到Excel文件";
			fileDlg.Filter = "Microsoft Office Excel 工作簿(*.xls)|*.xls|模板(*.xlt)|*.xlt";
			
			if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				if (excel.SaveAs(fileDlg.FileName,true))
				{
					System.Windows.Forms.MessageBox.Show("数据成功保存到Excel文件！","GoldPrinter",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
				}
			}

			fileDlg.Dispose();

			excel.Close();

			#endregion
		}


		//绘制		
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

					//打印或预览完毕后重置，以免在预览窗口中打印时打不出网格体Body
					this.mCurrentPageIndex = 1;
					this.mCurrentRowIndex = 0;
				}
			}
			catch(Exception e)
			{
				//System.Windows.Forms.MessageBox.Show(e.Message);
			}
		}


		/// 对象打印接口
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

		#region 绘制过程

		/// <summary>
		/// 绘制过程
		/// </summary>
		/// <param name="g"></param>
		/// <returns>一页没绘制完返回false</returns>
		private bool Draw(Graphics g)
		{
			bool blnHasMorePage = false;		//是否还有下一页标记

			if (this._body.Rows < 0)
			{
				throw new Exception("打印主要网格不能为空，请用Body设置！");
			}

			mPrinter.Graphics = g;
			mPrinter.PrintDocument = this.mPrintDocument;
			mPrinter.Sewing = this.Sewing;
			mPrinter.PrinterMargins = this.mPrinterMargins; 

			
			//初起打印起点坐标及打印区域的宽
			Y = mPrinter.PrinterMargins.Top;
			X = mPrinter.PrinterMargins.Left;
			Width = mPrinter.PrinterMargins.Width;
                       
			//画打印区域及装订线
			this.DrawPrinterMargins(mPrinter);
			this.DrawSewing(mPrinter);
			
			//正标题每页必重复打印，无需判断（改了，更多选择吧）
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
				Y += CON_SPACE_CAPTION_TOP;			//标题与下面有一定距离
			}

			//启用实际宽度
			int lngInfactWidth = 0;

			if (!this._body.IsAverageColsWidth)
			{
				for(int i=0;i<_body.ColsWidth.Length;i++)
				{
					lngInfactWidth += _body.ColsWidth[i];			
				}

				if (lngInfactWidth > this.mPrinterMargins.Width)
				{
					//缩放
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
				Y += CON_SPACE_HEADER_BODY;	//网格与页头距离
			}

			if (_multiHeader != null && (mCurrentPageIndex == 1 || _multiHeader.IsDrawAllPage))
			{
				OutObject(_multiHeader);				
			}

            
			#region 主体数据网格

			//TimeDef.Start();

			//计算有效高度，便于分页
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
				throw new Exception("预留给打印主要网格的空间太小，请适当调整！");			
			}

			//有效高度中当前页行数
			int mRowsInCurPage = 0;
			mRowsInCurPage = (int)(validHeight/(float)(this._body.RowHeight));

			//如果指定每页行数，则以其为主
			if (this.RowsPerPage > 0 && this.RowsPerPage < mRowsInCurPage)
			{
				mRowsInCurPage = this.RowsPerPage;
			}

			if (this.IsSubTotalPerPage)
			{
				mRowsInCurPage--;
			}

			//TimeDef.End();

			//************以Body为主************

			//TimeDef.Start();

			string[,] mArrGridText;			//保留当前页文本，用于页小计
			GoldPrinter.Body mbody;
			
			//如果指定每页行数，则以其为主
			if (this.RowsPerPage > 0 && this.RowsPerPage < mRowsInCurPage)
			{
				mbody = new Body(mRowsInCurPage,this._body.Cols);
			}
			else
			{
				//否则自适应
				if (mRowsInCurPage > (this._body.Rows - this.mCurrentRowIndex))
				{
					mRowsInCurPage = this._body.Rows - this.mCurrentRowIndex;
				}
				mbody = new Body(mRowsInCurPage,this._body.Cols);			
			}

			//存当前页的二维文本
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


			//判断是否要分页，只要数据网格行数据大于数据网格行指针，则还有下一页
			if (mCurrentRowIndex < this._body.Rows)
			{
				blnHasMorePage = true;
			}


            
			#region 打印每页小计，只需要将当前数组用循环累计就OK了，这段程序应专门重构为一个函数，读者可以自己试一试
           
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
					//循环
					//....
					mhSubTotal.SetText(0,0,"本页小计");
					mhSubTotal.SetText(0,1,"本页小计");
				
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

						mdblSubTotal = 0;		//网友“有你一生”发现此bug，要求清零
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
				Y += CON_SPACE_BODY_FOOTER;			//网格与页底距离
			}

			//打印页脚与最底
			if (_footer != null)
			{
				//最后一页必打
				if (blnHasMorePage == false || _footer.IsDrawAllPage)
				{	
					//如果每页都打印，对_footer分页失去了意义
					if (_footer.IsDrawAllPage)
					{
						OutObject(_footer);
					}					
					else
					{
						//不是每都打，但是最后一页必打_footer，这时要做分页处理
						//与Body同样的处理
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
						//计算有效高度
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
            

			//画边框
			DrawBorder(g,this._multiHeader,mbody);

			mbody.Dispose();
			mbody = null;
			
			return blnHasMorePage;
		}

		#endregion


		/// <summary>
		/// 画打印区域
		/// </summary>
		private void DrawPrinterMargins(Printer printer)
		{
			if (this.IsPrinterMargins)
			{
				printer.DrawPrinterMargins();
			}
		}

		/// <summary>
		/// 画装订线
		/// </summary>
		private void DrawSewing(Printer printer)
		{
			#region 实现...

			if (this.IsSewingLine && this.Sewing.Margin > 0)
			{
				//对象的线长小于0则自动设置
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
		/// 画边框
		/// </summary>
		private void DrawBorder(Graphics g,MultiHeader multiHeader,Body body)
		{
			#region 实现...

			//网格边框矩阵
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
		/// 加上装订的非打印区域，打印对象整体移动
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
			#region 打印相关信息

			Graphics g = ev.Graphics;
			Console.WriteLine ("*****Information about the printer*****");
			Console.WriteLine("纸张的大小  ev.PageSettings.PaperSize:" + ev.PageSettings.PaperSize);
			Console.WriteLine("打印分辨率  ev.PageSettings.PrinterResolution:" + ev.PageSettings.PrinterResolution);
			Console.WriteLine("旋转的角度  ev.PageSettings.PrinterSettings.LandscapeAngle" + ev.PageSettings.PrinterSettings.LandscapeAngle);
			Console.WriteLine("");
			Console.WriteLine ("*****Information about the page*****");
			Console.WriteLine("页面的大小  ev.PageSettings.Bounds:" + ev.PageSettings.Bounds); 
			Console.WriteLine("页面(同上)  ev.PageBounds:" + ev.PageBounds); 
			Console.WriteLine("页面的边距    ev.PageSettings.Margins.:" + ev.PageSettings.Margins); 
			Console.WriteLine("页面的边距    ev.MarginBounds:" + ev.MarginBounds); 

			Console.WriteLine("水平分辨率    ev.Graphics.DpiX:" + ev.Graphics.DpiX ); 
			Console.WriteLine("垂直分辨率    ev.Graphics.DpiY:" + ev.Graphics.DpiY ); 

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
