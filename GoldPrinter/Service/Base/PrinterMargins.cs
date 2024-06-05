using System;
using System.Drawing;
using System.Drawing.Printing;
namespace GoldPrinter
{
	/// <summary>
	/// �̳���Margins������������ӡҳ�ı߾༰��Ч��ӡ���ߡ�
	/// ע��Marginsʹ�õ�Ĭ�ϵ�λ��PrinterUnit.Display���ٷ�֮һӢ�缴0.01��
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class PrinterMargins:Margins
	{

		#region *****ͼ��*****
		//�����ͼ�α�ʾ����߽��Ǵ�ӡֽ���������Ϊ��ӡ��
		//��ӡ�����ĸ���ļǷ�������������ΪX1��X2,���ϵ��º���ΪY1��Y2

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


		//				�����ͼ�α�ʾ
		#endregion

		int _width,_height;	//��ӡҳ��Ч��ӡ���ߣ���ͼ��Ϊ�������
        int _X1,_X2;		//����������ΪX1��X2			ֻ�ܶ�ȡ									
		int _Y1,_Y2;		//���ϵ��º���ΪY1��Y2			ֻ�ܶ�ȡ									

		#region �ֶ�����
		/// <summary>
		/// ��ȡ��������Ч��ӡ��
		/// </summary>
		public int Width
		{
			get{return _width;}
			set{_width = value;}		
		}

		/// <summary>
		/// ��ȡ��������Ч��ӡ��
		/// </summary>
		public int Height
		{
			get{return _height;}
			set{_height = value;}		
		}

		/// <summary>
		/// ��ȡ��Ч��ӡ����߽߱��X������
		/// </summary>
		public int X1
		{
			get{return _X1;}
		}

		/// <summary>
		/// ��ȡ��Ч��ӡ���ұ߽߱��X������
		/// </summary>
		public int X2
		{
			get{return _X2;}
		}

		/// <summary>
		/// ��ȡ��Ч��ӡ���ϱ߽߱��Y������
		/// </summary>
		public int Y1
		{
			get{return _Y1;}
		}

		/// <summary>
		/// ��ȡ��Ч��ӡ���±߽߱��Y������
		/// </summary>
		public int Y2
		{
			get{return _Y2;}
		}
		#endregion	

		/// <summary>
		/// ʹ�� 1 ����λ��û�о���ָ����Ӣ�绹��ʲô���߾��ʼ�� System.Drawing.Printing.Margins �����ʵ����
		/// </summary>
		public PrinterMargins():this(1,1,1,1,0,0)
		{
			
		}

		/// <summary>
		/// ��ָ���ı߾༰��Ч��ӡ���߳�ʼ�����ʵ��
		/// </summary>
		/// <param name="left">��߾�</param>
		/// <param name="right">�ұ߾�</param>
		/// <param name="top">�ϱ߾�</param>
		/// <param name="bottom">�±߾�</param>
		/// <param name="width">��Ч��ӡ���Ŀ�</param>
		/// <param name="height">��Ч��ӡ���ĸ�</param>
		public PrinterMargins(int left,int right,int top,int bottom,int width,int height):base(left,right,top,bottom)
		{	
			_width = width;
			_height = height;

			Calculate();
		}

		/// <summary>
		/// ����ָ���Ĵ�ӡ�ĵ������ʼʵ��������� left,right,top,bottom,width,height�߾༰��Ч��ӡ���Ŀ���
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
        /// ͨ��PrintDocument��ȡһ��PrinterMargins����
        /// </summary>
        /// <param name="printDocument"></param>
        /// <returns></returns>
        private PrinterMargins GetPrinterMargins(PrintDocument printDocument)
        {
            //���ڷ��صı���
            PrinterMargins printerMargins;
				
            //��ͼ��ʼ���꼰�ַ����Ŀ����
            int left,right,top,bottom,width,height;

			left = printDocument.DefaultPageSettings.Margins.Left;
			right = printDocument.DefaultPageSettings.Margins.Right;

			top = printDocument.DefaultPageSettings.Margins.Top;
			bottom = printDocument.DefaultPageSettings.Margins.Bottom;

            width = printDocument.DefaultPageSettings.PaperSize.Width;
            height = printDocument.DefaultPageSettings.PaperSize.Height;

            //����ʹ�ÿ���߽���
            if (printDocument.DefaultPageSettings.Landscape)
            {
                Swap(ref width,ref height);
            }

            //�����Ϊ��ӡ���Ŀ�������ҳ�����߼�ȥ��Ӧ�ı߾�
            width = width - left - right;
            height = height - top - bottom;

            //ʵ����������
            printerMargins = new PrinterMargins(left ,right,top , bottom ,width ,height);

            return printerMargins;        
        }

        //��������
        private void Swap(ref int ValA,ref int ValB)
        {
            int tmp = ValA;
            ValA = ValB;
            ValB = tmp;        
        }

		//����
		private void Calculate()
		{
			_X1 = this.Left;
			_X2 = this.Left + _width;

			_Y1 = this.Top;
			_Y2 = this.Top + _height;			
		}

	}//End Class
}//End NameSpace
