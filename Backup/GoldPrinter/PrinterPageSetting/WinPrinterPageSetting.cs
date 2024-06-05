using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace GoldPrinter
{
	/// <summary>
	/// WinForm下的打印纸张设置、打印机设置、打印预览对话框。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class WinPrinterPageSetting:IPrinterPageSetting
	{
		//把PrintPage委托声明为类的一个成员变量
		private PrintPageDelegate _printPageValue;	
		//把导出到Excel委托声明为类的一个成员变量
		private ImportExcelDelegate _importExcelValue;	
	
		// 打印文档
		private PrintDocument _printDocument;

		#region	构造函数
		public WinPrinterPageSetting():this(null)
		{
			
		}

		/// <summary>
		/// 使用printDocument来初始化类的新实例，当printDocument为null时自动创建一个printDocument的实例
		/// </summary>
		/// <param name="printDocument">如果为null，则提供一个默认的PrintDocument</param>
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


		#region IPrinterPageSetting 成员

		/// <summary>
		/// 获取或设置打印文档
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
		/// 一定要实例化此类后在调用打印/预览之前设置此属性，使之关联一个方法，目的是让具体的打印由实例化者来操作。这里当属性使用，其实可以用PrintPage。
		/// 意思就是告诉printerPageSetting打印的具体实现过程是PrintPageEventHandler
		/// C#调用：
		///		PrinterPageSetting1.PrintPageValue = new PrintPageDelegate(this.PrintPageEventHandler);
		/// VB调用：    
		///    Me.printerPageSetting.PrintPageValue = New GoldPrinter.PrintPageDelegate(AddressOf printDocument_PrintPage)
		/// </summary>
		public PrintPageDelegate PrintPageValue
		{
			set
			{	
				//初始委托变量，关联方法
				_printPageValue = value;
				
				_printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this._printPageValue);
			}
			get
			{
				return _printPageValue;
			}		
		}

		/// <summary>
		/// 当需要为当前页打印的输出时发生，如果对此不了解，就用PrintPageValue赋值也可以
        /// C#调用：
        ///		PrinterPageSetting1.PrintPage += new PrintPageDelegate(this.PrintPageEventHandler);
        /// VB调用：    
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
		/// 导出Excel的实现
		/// </summary>
		public ImportExcelDelegate ImportExcelValue
		{
			set
			{	
				//初始委托变量，关联方法
				_importExcelValue = value;				
			}
			get
			{
				return _importExcelValue;
			}		
		}

		/// <summary>
		/// 显示页面设置对话框，并返回PageSettings
		/// </summary>
		/// <returns></returns>
		public PageSettings ShowPageSetupDialog()
		{
			return ShowPageSetupDialog(this._printDocument);
		}

		/// <summary>
		/// 显示打印机设置对话框，并返回PrinterSettings
		/// </summary>
		/// <returns></returns>
		public PrinterSettings ShowPrintSetupDialog()
		{
			return ShowPrintSetupDialog(this._printDocument);
		}

		/// <summary>
		/// 显示打印预览对话框
		/// </summary>
		public void ShowPrintPreviewDialog()
		{
			ShowPrintPreviewDialog(this._printDocument);
		}

		#endregion

	
        #region ***************页面设置\打印设置\打印预览对话框，可以独立使用***************
		#region 页面设置对话框 protected virtual PageSettings ShowPageSetupDialog(PrintDocument printDocument)
		/// <summary>
		/// 页面设置对话框，可以独立使用
		/// </summary>
		/// <param name="printDocument"></param>
		/// <returns></returns>
		/// <remarks>
		/// 作    者：周方勇
		/// 修改日期：2004-08-07
		/// </remarks>
		protected virtual PageSettings ShowPageSetupDialog(PrintDocument printDocument)
		{
            //检查printDocument是否为空，空的话抛出异常
            ThrowPrintDocumentNullException(printDocument);

			//声明返回值的PageSettings
			PageSettings ps = new PageSettings();

			//申明并实例化PageSetupDialog
			PageSetupDialog psDlg = new PageSetupDialog();

			ps = printDocument.DefaultPageSettings;

			try
			{			
				//相关文档及文档页面默认设置
				psDlg.Document = printDocument;

				Margins mg = printDocument.DefaultPageSettings.Margins;
				if (System.Globalization.RegionInfo.CurrentRegion.IsMetric)
				{
					mg = PrinterUnitConvert.Convert(mg, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
				}

				//备份打印文档的DefaultPageSettings，因为转换后会改变，而设置对话框单击取消按钮后不还原就不能正确显示原来的值
				PageSettings psPrintDocumentBack = (PageSettings)(printDocument.DefaultPageSettings.Clone());

				psDlg.PageSettings = psPrintDocumentBack;//printDocument.DefaultPageSettings; //用printDocument的时取消了对话框就要还原
				psDlg.PageSettings.Margins = mg;


				//显示对话框
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


		#region 打印设置对话框 protected virtual PrinterSettings ShowPrintSetupDialog(PrintDocument printDocument)
		/// <summary>
		/// 打印设置对话框，可以独立使用
		/// </summary>
		/// <param name="printDocument"></param>
		/// <returns></returns>
		/// <remarks>
		/// 作    者：周方勇
		/// 修改日期：2004-08-07
		/// </remarks>
		protected virtual PrinterSettings ShowPrintSetupDialog(PrintDocument printDocument)
		{
            //检查printDocument是否为空，空的话抛出异常
            ThrowPrintDocumentNullException(printDocument);

			//声明返回值的PrinterSettings
			PrinterSettings ps = new PrinterSettings();
			//申明并实例化PrintDialog
			PrintDialog pDlg = new PrintDialog();

			try
			{				
				//可以选定页
				pDlg.AllowSomePages = true;

				//指定打印文档
				pDlg.Document = printDocument;

				//显示对话框
				DialogResult result = pDlg.ShowDialog();
				if (result == DialogResult.OK)
				{
					//保存打印设置
					ps = pDlg.PrinterSettings;
					//打印
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
				//调用
				this._importExcelValue.BeginInvoke(sender,null,null,null);		
			}
		}

		#region 打印预览对话框 protected virtual void ShowPrintPreviewDialog(PrintDocument printDocument)
		/// <summary>
		/// 打印预览对话框，可以独立使用
		/// </summary>
		/// <param name="printDocument"></param>
		/// <returns></returns>
		/// <remarks>
		/// 作    者：周方勇
		/// 修改日期：2004-08-07
		/// </remarks>
		protected virtual void ShowPrintPreviewDialog(PrintDocument printDocument)
		{
            //检查printDocument是否为空，空的话抛出异常
			ThrowPrintDocumentNullException(printDocument);

			//申明并实例化PrintPreviewDialog
			PrintPreviewDialog ppDlg = new PrintPreviewDialog();
			
			//ppDlg.Icon = new Icon("..\\.....myIcon.ico");
			//ppDlg.Text = "打印预览--欢迎使用";					//PrintPreviewDialog()继承Forms，这里居然看不到这几个属性
			ppDlg.Text = printDocument.DocumentName;
			ppDlg.WindowState = FormWindowState.Maximized;

			//ppDlg.Controls[0].BackColor = Color.Red;		//打印预览控件白色纸张区域

			
			if (this._importExcelValue != null)
			{
				ToolBar tb = null;
				
				if (ppDlg.Controls[1] is ToolBar)
				{
					//Vs2003
					tb = (ToolBar)ppDlg.Controls[1];		//打印预览控件ToolBar控制			
					ToolBarButton toolbtn = new ToolBarButton();
					toolbtn.ToolTipText = "Import Excel";
			
					toolbtn.ImageIndex = 2;
					tb.ButtonClick +=new ToolBarButtonClickEventHandler(Excel_ButtonClick);
					tb.Buttons.Add(toolbtn);
				}
				else
				{
					//Vs2005
					//VS2005中ToolBar已被ToolStrip取代，只是为了向下兼容还保留ToolBar类。
					//tb = ((ToolStrip)ppDlg.Controls[1]).Items.Add(new ToolStripButton("Excel"));				
				}
			}
			
			try
			{				
				//指定打印文档
				ppDlg.Document = printDocument;	
						
				//显示对话框
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


        #region 对话框支持函数据  重构日期：2004-09-03
        /// <summary>
        /// 检查printDocument是否为空，空的话抛出异常
        /// </summary>
        /// <param name="printDocument"></param>
        protected virtual void ThrowPrintDocumentNullException(PrintDocument printDocument)
        {
            if (printDocument==null)
            {
                throw new Exception("关联的打印文档不能为空！");
            }        
        }

        /// <summary>
        /// 显示没安装打印机时的提示信息
        /// </summary>
        /// <param name="e"></param>
        protected virtual void ShowInvalidPrinterException(System.Drawing.Printing.InvalidPrinterException e)
        {
            MessageBox.Show("未安装打印机，请进入系统控制面版添加打印机！","打印",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示打印机其它错误提示信息
        /// </summary>
        /// <param name="ex"></param>
        protected virtual void ShowPrinterException(Exception ex)
        {
            MessageBox.Show(ex.Message,"打印",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
        #endregion
        #endregion

	}//End Class
}//End NameSpace