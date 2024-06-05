using System;
using System.Drawing;
using System.Drawing.Printing;
namespace GoldPrinter
{
	/// <summary>
	/// 继承于Margins，用于描述打印页的边距及有效打印宽及高。
	/// 注意Margins使用的默认单位是PrinterUnit.Display即百分之一英寸即0.01。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class PrinterMargins:Margins
	{

		#region *****图例*****
		//对象的图形表示，外边界是打印纸，内面矩形为打印区
		//打印区的四个点的记法：从左到右竖线为X1、X2,从上到下横线为Y1、Y2

		//__________________________________________
		//											|
		//											|
		//					    A					|
		//					    |					|
		//					   Top					|
		//					    |                   |
		//					    V					|
		//											|
		//			(X1,Y1)				(X2,Y1)		|
		//			 ___________y1________			|
		//			|					  |			|
		//			|		WIDTH		  |			|
		//			|					H |			|
		//			|					E |			|
		//<--Left-->|					I |<-Right->|
		//			x1					G x2		|
		//			|					H | 		|
		//			|					T |			|
		//			|___________y2________|			|
		//											|
		//			(X1,Y2)				(X2,Y2)		|
		//											|
		//						A					|
		//						|					|
		//					 Bottom					|
		//					    |                   |
		//						V					|
		//__________________________________________|


		//				对象的图形表示
		#endregion

		int _width,_height;	//打印页有效打印宽及高，在图中为内面矩形
        int _X1,_X2;		//从左到右竖线为X1、X2			只能读取									
		int _Y1,_Y2;		//从上到下横线为Y1、Y2			只能读取									

		#region 字段属性
		/// <summary>
		/// 获取或设置有效打印宽
		/// </summary>
		public int Width
		{
			get{return _width;}
			set{_width = value;}		
		}

		/// <summary>
		/// 获取或设置有效打印高
		/// </summary>
		public int Height
		{
			get{return _height;}
			set{_height = value;}		
		}

		/// <summary>
		/// 获取有效打印区左边边界的X横坐标
		/// </summary>
		public int X1
		{
			get{return _X1;}
		}

		/// <summary>
		/// 获取有效打印区右边边界的X横坐标
		/// </summary>
		public int X2
		{
			get{return _X2;}
		}

		/// <summary>
		/// 获取有效打印区上边边界的Y纵坐标
		/// </summary>
		public int Y1
		{
			get{return _Y1;}
		}

		/// <summary>
		/// 获取有效打印区下边边界的Y纵坐标
		/// </summary>
		public int Y2
		{
			get{return _Y2;}
		}
		#endregion	

		/// <summary>
		/// 使用 1 个单位（没有具体指明是英寸还是什么）边距初始化 System.Drawing.Printing.Margins 类的新实例。
		/// </summary>
		public PrinterMargins():this(1,1,1,1,0,0)
		{
			
		}

		/// <summary>
		/// 用指定的边距及有效打印宽、高初始类的新实例
		/// </summary>
		/// <param name="left">左边距</param>
		/// <param name="right">右边距</param>
		/// <param name="top">上边距</param>
		/// <param name="bottom">下边距</param>
		/// <param name="width">有效打印区的宽</param>
		/// <param name="height">有效打印区的高</param>
		public PrinterMargins(int left,int right,int top,int bottom,int width,int height):base(left,right,top,bottom)
		{	
			_width = width;
			_height = height;

			Calculate();
		}

		/// <summary>
		/// 根据指定的打印文档对象初始实例化对象的 left,right,top,bottom,width,height边距及有效打印区的宽、高
		/// </summary>
		/// <param name="printDocument"></param>
        public PrinterMargins(PrintDocument printDocument)
        {
            PrinterMargins printerMargins = new PrinterMargins();
            printerMargins = GetPrinterMargins(printDocument);

            this.Left   = printerMargins.Left;
            this.Right  = printerMargins.Right;
            this.Top    = printerMargins.Top;
            this.Bottom = printerMargins.Bottom;
            this.Width  = printerMargins.Width;
            this.Height = printerMargins.Height;

            printerMargins = null;

            Calculate();
        }

        /// <summary>
        /// 通过PrintDocument获取一个PrinterMargins对象
        /// </summary>
        /// <param name="printDocument"></param>
        /// <returns></returns>
        private PrinterMargins GetPrinterMargins(PrintDocument printDocument)
        {
            //用于返回的变量
            PrinterMargins printerMargins;
				
            //绘图起始座标及字符串的宽与高
            int left,right,top,bottom,width,height;

			left = printDocument.DefaultPageSettings.Margins.Left;
			right = printDocument.DefaultPageSettings.Margins.Right;

			top = printDocument.DefaultPageSettings.Margins.Top;
			bottom = printDocument.DefaultPageSettings.Margins.Bottom;

            width = printDocument.DefaultPageSettings.PaperSize.Width;
            height = printDocument.DefaultPageSettings.PaperSize.Height;

            //横向使得宽与高交换
            if (printDocument.DefaultPageSettings.Landscape)
            {
                Swap(ref width,ref height);
            }

            //宽与高为打印区的宽，所以是页面宽与高减去相应的边距
            width = width - left - right;
            height = height - top - bottom;

            //实例化并返回
            printerMargins = new PrinterMargins(left ,right,top , bottom ,width ,height);

            return printerMargins;        
        }

        //交换两数
        private void Swap(ref int ValA,ref int ValB)
        {
            int tmp = ValA;
            ValA = ValB;
            ValB = tmp;        
        }

		//计算
		private void Calculate()
		{
			_X1 = this.Left;
			_X2 = this.Left + _width;

			_Y1 = this.Top;
			_Y2 = this.Top + _height;			
		}

	}//End Class
}//End NameSpace
