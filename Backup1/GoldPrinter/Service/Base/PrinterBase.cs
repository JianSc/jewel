using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// PrinterBase��ӡ���࣬������ӡ�ĵ����߾���Ϣ���̳���DrawBase(IDraw)������ʵ������
	/// 
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public abstract class PrinterBase:DrawBase
	{
        //private int ADD_PRINTER_MARGINS = 3;	        //��ӡʱ��Ϊ�������ұ߾���Ԥ���ռ�       

        private PrintDocument _printDocument;	        //��ӡ�ĵ���ע��PrintDocument�ĸı佫ֱ��Ӱ��ҳ�߾�ȡ�
        //ע��2004��09��20 ������ϸ���ǣ�װ����Sewing����Ӱ��ҳ�߾�ȣ�Ӱ��ҳ�߾������PrintDocument��
        // ����������PrintDocument����߾໨��ʱ��ܳ���Ӱ��Ч�ʣ���˸ı�����Ժ����ⲻֱ�����¼���ҳ�߾࣬���ֶ�����CalculatePageInfo()��
        // ������õĴ���̫�࣬��ý������PrinterMargins������Ժ󸳸�PrinterMargins���ԣ����ٵ���CalculatePageInfo()������PrintDocument�����仯�������ϵͳЧ�ʡ�
	
        
        //���¼�������PrintDocumentӰ�죬���PrintDocument�ֶ�ֵһ�ı䣬�⼸���ֶε�ֵҪ���¼��㣬Ϊ��Ч�ʣ����ֶ�����CalculatePageInfo()��
        private PrinterMargins _printerMargins;		    //��ӡҳ�ı߾༰��Ч��ӡ���ߣ��ı�PrinterMargins���Խ������ SetPageInfo();


        private int _pageWidth;						    //ҳ���
        private int _pageHeight;					    //ҳ���
        private int _leftMargin;					    //ҳ����߾�
        private int _rightMargin;					    //ҳ���ұ߾�
        private int _topMargin;						    //ҳ�涥�߾�
        private int _bottomMargin;					    //ҳ��ױ߾�

        #region  ************�ֶ�����************
        /// <summary>
        /// ��ȡ�����ô�ӡ�ĵ���PrintDocument�ĸı佫ֱ��Ӱ��ҳ�߾�ȣ���Ϊ��Ч�ʣ����ֶ�����CalculatePageInfo()��ȥ���㡣
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
        /// ��ȡ�����ô�ӡҳ�ı߾༰��Ч��ӡ����
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
        /// ��ȡҳ���ܿ�����ҳ�߾�
        /// </summary>
        public int PageWidth
        {
            get{return this._pageWidth;}
        }

        /// <summary>
        /// ��ȡҳ���ܸߣ�����ҳ�߾�
        /// </summary>
        public int PageHeight
        {
            get{return this._pageHeight;}
        }

        /// <summary>
        /// ��ȡҳ��߾�
        /// </summary>
        public int LeftMargin
        {
            get{return this._leftMargin;}
        }

        /// <summary>
        /// ��ȡҳ�ұ߾�
        /// </summary>
        public int RightMargin
        {
            get{return this._rightMargin;}
        }

        /// <summary>
        /// ��ȡҳ���߾�
        /// </summary>
        public int TopMargin
        {
            get{return this._topMargin;}
        }

        /// <summary>
        /// ��ȡҳ�ױ߾�
        /// </summary>
        public int BottomMargin
        {
            get{return this._bottomMargin;}
        }
        #endregion
        
        public PrinterBase()
		{
            //ʵ����(����ģʽ)
            _printDocument = PrinterSingleton.PrintDocument;
            _printerMargins = PrinterSingleton.PrinterMargins; //���Ч�ʣ���Ϊ�����Ǵ�ӡ�Ķ����ǳ��࣬����ÿ��ʵ����ʱ��Ҫ����PrintDocument����߾໨��ʱ��̫����

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
        /// ����PrintDocument�����Ӱ�쵽һЩ�����ı䶯����PrintDocument�ı��ˣ���Ҫ����PageWidth\Height�ȣ�װ�����ˣ��ͻ�Ӱ��PrinterMargins��
        /// </summary>
        private void SetPageInfo()
        {
            //��ȡϵͳ�Ǵ�ӡ����߾�
            this._leftMargin = this.PrinterMargins.Left;
            this._topMargin = this.PrinterMargins.Top;

            this._rightMargin = this.PrinterMargins.Right;
            this._bottomMargin = this.PrinterMargins.Bottom;

            //ע��PrinterMargins.Width/Height�����Ǵ�ӡ���Ŀ����
            this._pageWidth = this.PrinterMargins.Width + this._leftMargin + this._rightMargin;
            this._pageHeight = this.PrinterMargins.Height + this._topMargin + this._bottomMargin;


        }


        //������Ӧ�ķǴ�ӡ����
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