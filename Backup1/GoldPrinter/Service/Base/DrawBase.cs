using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// ���ƻ����࣬�޷�ʵ�������ӿ�ΪIDraw�����ƽӿڣ��Ƿ����ڻ�ͼ����Graphics���ƾ�������Rectangle��ָ���Ļ���Brush��Pen����Draw()��
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public abstract class DrawBase:IDraw,IDisposable
	{
		//************��    ��************		
		private Graphics _graphics;				//��ͼ����
		private Rectangle _rectangle;			//������

        //���
        private Brush _brush;
        private Pen _pen;

        #region IDraw ��Ա
        /// <summary>
        /// ��ȡ�����û�ͼ����
        /// </summary>
        public Graphics Graphics
        {
            get
            {
                return this._graphics;
            }
            set
            {
                this._graphics = value;
            }
        }

        /// <summary>
        /// ��ȡ�����û�������
        /// </summary>
        public System.Drawing.Rectangle Rectangle
        {
            get
            {
                return _rectangle;
            }
            set
            {
                _rectangle = value;
            }
        }

		/// <summary>
		/// ����
		/// </summary>
        public Pen Pen
        {
            get
            {
                return _pen;
            }
            set
            {
                if (value != null)
                {
                    _pen = value;
                }
            }		
        }

		/// <summary>
		/// ��ˢ
		/// </summary>
		public Brush Brush
		{
			get
			{
				return _brush;
			}
			set
			{
				if (value != null)
				{
					_brush = value;
				}
			}		
		}

		/// <summary>
        /// ���ƣ����󷽷����������������ʵ��
        /// </summary>
        public abstract void Draw();

        #endregion

		/// <summary>
		/// ���ƻ����࣬�޷�ʵ����
		/// </summary>
        public DrawBase()
		{
			_rectangle = new Rectangle(0,0,0,0);

			_brush = Brushes.Black;
				
			_pen = new Pen(_brush);			 
            _pen = Pens.Black;            
		}

        #region IDisposable ��Ա

		/// <summary>
		/// �ͷŶ���ʹ�õ�������Դ
		/// </summary>
        public virtual void Dispose()
        {
			//ע�⣺ֻ���ͷ��Լ������Ķ��󣬶���Ҫ�ͷ������Զ��󣨼����ⲿ�������Ķ���
			/*
            _graphics.Dispose();
			*/
            _brush.Dispose();
            _pen.Dispose();
			
        }

        #endregion

	}//End Class
}//End NameSpace
