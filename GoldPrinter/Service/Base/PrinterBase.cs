using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// PrinterBase打印基类，描述打印文档及边距信息，继承于DrawBase(IDraw)，不能实倒化。
	/// 
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public abstract class PrinterBase:DrawBase
	{
        //private int ADD_PRINTER_MARGINS = 3;	        //打印时，为上下左右边距再预留空间       

        private PrintDocument _printDocument;	        //打印文档，注意PrintDocument的改变将直接影响页边距等。
        //注：2004－09－20 经过仔细考虑，装订线Sewing不再影响页边距等，影响页边距仅仅是PrintDocument。
        // 但是由于由PrintDocument计算边距花费时间很长，影响效率，因此改变此属性后在这不直接重新计算页边距，请手动调用CalculatePageInfo()。
        // 如果调用的次数太多，最好将计算后将PrinterMargins保存后以后赋给PrinterMargins属性，不再调用CalculatePageInfo()，除非PrintDocument发生变化，以提高系统效率。
	
        
        //以下几个是受PrintDocument影响，因此PrintDocument字段值一改变，这几个字段的值要重新计算，为了效率，请手动调用CalculatePageInfo()。
        private PrinterMargins _printerMargins;		    //打印页的边距及有效打印宽及高，改变PrinterMargins属性将会调用 SetPageInfo();


        private int _pageWidth;						    //页面宽
        private int _pageHeight;					    //页面高
        private int _leftMargin;					    //页面左边距
        private int _rightMargin;					    //页面右边距
        private int _topMargin;						    //页面顶边距
        private int _bottomMargin;					    //页面底边距

        #region  ************字段属性************
        /// <summary>
        /// 获取或设置打印文档，PrintDocument的改变将直接影响页边距等，但为了效率，请手动调用CalculatePageInfo()才去计算。
        /// </summary>
        public PrintDocument PrintDocument
        {
            get
            {
                return this._printDocument;
            }
            set
            {
                if (value != null)
                {
                    this._printDocument = value;
                }
            }
        }

        
        /// <summary>
        /// 获取或设置打印页的边距及有效打印宽及高
        /// </summary>
        public PrinterMargins PrinterMargins
        {
            get
            {
                return this._printerMargins;
            }
            set
            {
                if (value != null)
                {
                    this._printerMargins = value;
                    SetPageInfo();
                }

            }
        }
		
		
		/// <summary>
        /// 获取页面总宽，包括页边距
        /// </summary>
        public int PageWidth
        {
            get{return this._pageWidth;}
        }

        /// <summary>
        /// 获取页面总高，包括页边距
        /// </summary>
        public int PageHeight
        {
            get{return this._pageHeight;}
        }

        /// <summary>
        /// 获取页左边距
        /// </summary>
        public int LeftMargin
        {
            get{return this._leftMargin;}
        }

        /// <summary>
        /// 获取页右边距
        /// </summary>
        public int RightMargin
        {
            get{return this._rightMargin;}
        }

        /// <summary>
        /// 获取页顶边距
        /// </summary>
        public int TopMargin
        {
            get{return this._topMargin;}
        }

        /// <summary>
        /// 获取页底边距
        /// </summary>
        public int BottomMargin
        {
            get{return this._bottomMargin;}
        }
        #endregion
        
        public PrinterBase()
		{
            //实例化(单件模式)
            _printDocument = PrinterSingleton.PrintDocument;
            _printerMargins = PrinterSingleton.PrinterMargins; //提高效率，因为本类是打印的对象都是超类，所以每次实例化时都要根据PrintDocument计算边距花费时间太长。

            SetPageInfo();
        }


//        public override void Dispose()
//        {
//            base.Dispose ();
//			
//            _printDocument.Dispose();
//        }


        public void CalculatePageInfo()
        {
            this.PrinterMargins = new PrinterMargins(this.PrintDocument);

            SetPageInfo();
        }

        /// <summary>
        /// 设置PrintDocument后可能影响到一些变量的变动，如PrintDocument改变了，就要重求PageWidth\Height等，装订变了，就会影响PrinterMargins等
        /// </summary>
        private void SetPageInfo()
        {
            //获取系统非打印区域边距
            this._leftMargin = this.PrinterMargins.Left;
            this._topMargin = this.PrinterMargins.Top;

            this._rightMargin = this.PrinterMargins.Right;
            this._bottomMargin = this.PrinterMargins.Bottom;

            //注意PrinterMargins.Width/Height仅仅是打印区的宽与高
            this._pageWidth = this.PrinterMargins.Width + this._leftMargin + this._rightMargin;
            this._pageHeight = this.PrinterMargins.Height + this._topMargin + this._bottomMargin;


        }


        //加上相应的非打印区域
        private void AddNonePrintArea()
        {
            /*
            this._printerMargins.Left   += ADD_PRINTER_MARGINS;
            this._printerMargins.Right  += ADD_PRINTER_MARGINS;
            this._printerMargins.Top    += ADD_PRINTER_MARGINS;
            this._printerMargins.Bottom += ADD_PRINTER_MARGINS;
            this._printerMargins.Width  -= ADD_PRINTER_MARGINS * 2;
            this._printerMargins.Height -= ADD_PRINTER_MARGINS * 2;            
            */
        }
   

    }//End Class
}//End NameSpace