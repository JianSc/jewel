using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// ��ӡ���࣬�̳���DrawBase,IPrinter��
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class Printer:PrinterBase
	{	       
		private Sewing _sewing;					        //װ����Ԥ��
		
		private Font _font;                             //���ƶ�����ı�����

        private bool _isDrawAllPage;				    //��ҳ��ӡʱ��ÿҳ�����ƣ�Ĭ�Ϸ�	
	
        private int _height;					        //�����



		//���ƶ�������ƶ�
		private int _MoveX;
		private int _MoveY;

	
		#region  ************�ֶ�����************

		/// <summary>
		/// ��ȡ������װ����Ԥ����װ����Sewing����Ӱ��ҳ�߾�ȣ�Ӱ��ҳ�߾������PrintDocument��
		/// </summary>
		public virtual Sewing Sewing
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
			}
		}

        /// <summary>
        /// ���ƶ����ı�����
        /// </summary>
        public virtual Font Font
        {
            get
            {
                return this._font;
            }
            set
            {
                if (value != null)
                {
                    this._font = value;
                }
            }
        }

		/// <summary>
		/// ��ҳ��ӡʱ��ÿҳ�Ƿ񶼻��ƣ�Ĭ�Ϸ�
		/// </summary>
		public virtual bool IsDrawAllPage
		{
			get
			{
				return this._isDrawAllPage;
			}
			set
			{
				this._isDrawAllPage = value;
			}
		}

        /// <summary>
        /// ��ȡ�����ö���ĸ�
        /// </summary>
        public virtual int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                if (_height < 0)
                {
                    _height = 0;
                }
            }
        }

		/// <summary>
		/// ��ͼ��ʼ������ƶ�ֵ
		/// </summary>
		public int MoveX
		{
			get{return _MoveX;}
			set{_MoveX = value;}
		}

		/// <summary>
		/// ��ͼ��ʼ������ƶ�ֵ
		/// </summary>
		public int MoveY
		{
			get{return _MoveY;}
			set{_MoveY = value;}
		}
		#endregion


		#region	���캯��
		/// <summary>
		/// ��ӡ��
		/// </summary>
		public Printer()
		{            
			_sewing = new Sewing(); 
			_font = new Font("����",10);

			_isDrawAllPage = false;

            _height = 0;
						
//			//������ʼ
			_MoveX = 0;
			_MoveY = 0;
		}
		#endregion

		/// <summary>
		/// ��ȡ�ı��Ŀ�����ᳬ����Ч��ӡҳ�Ŀ�
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public int TextWidth(string text)
		{
			int width = (int)(Graphics.MeasureString(text,this.Font,this.PrinterMargins.Width).Width);
			return width;
		}

		/// <summary>
		/// ��ȡ�ı��ĸߣ���������Ϊ��Ч��ӡҳ�Ŀ�
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public int TextHeight(string text)
		{
			int height = (int)(Graphics.MeasureString(text,this.Font,this.PrinterMargins.Width).Height);	
			return height;
		}

		/// <summary>
		/// ��ȡ�ı����о�
		/// </summary>
		/// <returns></returns>
		public int TextHeight()
		{			
			return this.Font.Height;
		}

		/// <summary>
		/// ����ӡ����
		/// </summary>
		public virtual void DrawPrinterMargins()
		{
			CheckGraphics();
			//Rectangle rec = new Rectangle(this.PrinterMargins.Left - ADD_PRINTER_MARGINS,this.PrinterMargins.Top - ADD_PRINTER_MARGINS,this.PrinterMargins.Width + ADD_PRINTER_MARGINS * 2,this.PrinterMargins.Height + ADD_PRINTER_MARGINS * 2);
            Rectangle rec = new Rectangle(this.PrinterMargins.Left,this.PrinterMargins.Top,this.PrinterMargins.Width,this.PrinterMargins.Height);
            Graphics.DrawRectangle(this.Pen,rec);
		}
		
		/// <summary>
		/// ����װ����
		/// </summary>
		public virtual void DrawSewing()
		{
			CheckGraphics();
			this.Sewing.LineLen = GetSewingLineLength();
			this.Sewing.Draw(this.Graphics);
		}

        private int GetSewingLineLength()
        {
            int mLineLen = 0;
            if (this.Sewing.SewingDirection == SewingDirectionFlag.Left)
            {
                mLineLen = this.PageHeight;
            }
            else if(this.Sewing.SewingDirection == SewingDirectionFlag.Top)
            {
                mLineLen = this.PageWidth;			
            }

            return mLineLen;
        
        }

		/// <summary>
		/// �ڻ�ͼ���滭ͼ������û��ʵ�ʵĻ�ͼ������CheckGraphics()�����Graphics�Ƿ�Ϊ�գ�ʵ�ʵĻ�ͼ�����ɼ̳���ȥ����������дCheckGraphics()��
		/// </summary>
		public override void Draw()
		{
			CheckGraphics();
			//...
			//�����ɼ̳���ȥ�������
		}

		protected virtual void CheckGraphics()
		{
			if (this.Graphics == null)
			{
				throw new Exception("��ͼ���治��Ϊ�գ�������Graphics���ԣ�");
			}		
		}

        public override void Dispose()
        {
            base.Dispose ();
			_sewing.Dispose();
            _font.Dispose();
        }

//        //����װ���ķǴ�ӡ����
//        private void AddSewingNonePrintArea()
//        {
//            if (this.Sewing.SewingDirection == SewingDirectionFlag.Left)
//            {
//                this.PrinterMargins.Left += this.Sewing.Margin;
//                this.PrinterMargins.Width -= this.Sewing.Margin;
//            }
//            else if (this.Sewing.SewingDirection == SewingDirectionFlag.Top)
//            {
//                this.PrinterMargins.Top += this.Sewing.Margin;
//                this.PrinterMargins.Height -= this.Sewing.Margin;			
//            }
//        }        


	}//End Class
}//End NameSpace
