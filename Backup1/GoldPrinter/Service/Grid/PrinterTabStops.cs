using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// ���Ʊ���ָ����ַ�������|�ָ��У���\n���У��ж����Ϊ���󡣺���������ÿ���ַ��ٵļ��м��еĴ�ӡ��
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class PrinterTabStops:IDisposable
	{
		private int _cols;				//����

		public Font Font;				//����
		public Rectangle Rectangle;	//�ڴ�����

		private string _text;			//ԭʼ��ӡ�ı�
		private string _textConverted;	//ת�����ӡ�ı�		 

		public PrinterTabStops()
		{
			Font = new Font("����",10);
			Rectangle = new Rectangle(0,0,this.Font.Height,100);
		}

		public PrinterTabStops(string text):this()
		{
			this.Text = text;
		}


		/// <summary>
		/// ��ȡ�������������
		/// </summary>
		public int Cols
		{
			get{return this._cols;}
			set{this._cols = value;}
		}

		/// <summary>
		/// ��ȡ�����ô�ӡ�ı�
		/// </summary>
		public string Text
		{
			get{return _text;}
			set
			{
				_text = value;

				string txt = _text;
				_textConverted = ConvertText(txt);
			}
		}
		public void Draw(Graphics g)
		{
			//�ı���ʽ
			StringFormat sf = new StringFormat();
			sf.FormatFlags = StringFormatFlags.NoWrap;
			float colWidth;
			
			colWidth = Rectangle.Width / _cols;

			float[] arrcolWidth = new float[_cols];

			for(int i = 0 ; i < _cols ; i++)
			{
				arrcolWidth[i] = colWidth;
			}


			//�����Ʊ�λ
			sf.SetTabStops(0.0f,arrcolWidth);

			g.DrawString(this._textConverted,this.Font,Brushes.Black,this.Rectangle,sf);		
		
		}

		/// <summary>
		/// ���ı���|�ָ����ı�ת��Ϊ�Ʊ��ı�
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private string ConvertText(string text)
		{
			string txt = text;
			txt = txt.Replace("|","\t");
			
			return txt;

        }

        #region IDisposable ��Ա

        public void Dispose()
        {
            this.Font.Dispose();
        }

        #endregion

    }//End Class
}//End NameSpace
