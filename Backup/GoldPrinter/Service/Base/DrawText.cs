using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// �̳���DrawBase���ڻ�ͼ����Graphics���ƾ�������Rectangle��ָ���Ļ���Brush��Pen����Draw() ��ʽΪStringFormat������Font���ı�Text��
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class DrawText:DrawBase
	{
		private string _text;
		private Font _font;
		private StringFormat _stringFormat;

		private int _startChar;
		private int _linesFilled;
		private int _charsFitted;

		private bool _hasMorePages;		//�Ƿ�û�д���
		
		#region �ֶ�����		

		/// <summary>
		/// ��ȡ������Ҫ���Ƶ��ı�
		/// </summary>
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}		
		}

		/// <summary>
		/// ��ȡ�����û��Ƶ��ı�����
		/// </summary>
		public Font Font
		{
			get
			{
				return _font;
			}
			set
			{
				if (value != null)
				{
					_font = value;
				}
			}		
		}

		/// <summary>
		/// ��ȡ������Ҫ���Ƶ��ı��ĸ�ʽ�����о�
		/// </summary>
		public StringFormat StringFormat
		{
			get
			{
				return _stringFormat;
			}
			set
			{
				_stringFormat = value;
			}		
		}

		/// <summary>
		/// ��ȡ������Ҫ���Ƶ��ı�ʱ����ʼ�ַ������ӵڼ�����ʼ�����С��0����ʼ�ķ���������ڻ�����ı����ȣ��򲻻��ơ�
		/// </summary>
		public int StartChar
		{
			get
			{
				return _startChar;
			}
			set
			{
				_startChar = value;
				if (_startChar < 0)
				{
					_startChar = 0;
				}
				else if(_startChar >= this.Text.Length)
				{
					_startChar = this.Text.Length;
				}
			}		
		}

		/// <summary>
		/// ��ȡ�������ڻ��Ƶ��ı���
		/// </summary>
		public int CharsFitted
		{
			get
			{
				return _charsFitted;
			}
		}

		/// <summary>
		/// ��ȡ�������ڻ��Ƶ��ı�����
		/// </summary>
		public int LinesFilled
		{
			get
			{
				return _linesFilled;
			}
		}

		/*
		/// <summary>
		/// ��ȡ�Ƿ���û�д�ӡ����ı�
		/// </summary>
		public int HasMorePages
		{
			get
			{
				return _hasMorePages;
			}
		}
		*/
		#endregion

		#region ���캯��
		/// <summary>
		/// ��ȡDrawText�µ�ʵ����Ĭ������Ϊ����10�ţ���������LineLimit��ʽ����
		/// </summary>
		public DrawText()
		{
			_text = "";
			_font = new Font("����",10);

			_startChar = 0;
			_linesFilled = 0;
			_charsFitted = 0;

			_stringFormat = new StringFormat(StringFormatFlags.LineLimit);
		}

		/// <param name="text">ָ�����Ƶ��ı�</param>
		public DrawText(string text):this()
		{
			_text = text;
		}
		#endregion

		/// <summary>
		/// �ͷŶ���ʹ�õ�������Դ
		/// </summary>
		public override void Dispose()
		{
			base.Dispose ();
			this._font.Dispose();
			this._stringFormat.Dispose();
		}

		//һ�ο��Դ�ӡ����
		private void DrawOnePage()
		{
			if (this.Graphics != null)
			{
				int intLinesFilled, intCharsFitted;

				this.Graphics.MeasureString(_text.Substring(_startChar),_font,new SizeF(this.Rectangle.Width, this.Rectangle.Height),_stringFormat,out intCharsFitted,out intLinesFilled);
			
				this.Graphics.DrawString(_text.Substring(_startChar),_font,this.Brush,this.Rectangle,_stringFormat);

				this._linesFilled = intLinesFilled;
				this._charsFitted = intCharsFitted;
			}
		}


		/// <summary>
		/// �ڻ�ͼ����Graphics��Ϊ�յ�����£����ƾ�������Rectangle��ָ���Ļ���Brush��Pen����Draw() ��ʽΪStringFormat������Font���ı�Text
		/// </summary>
		public override void Draw()
		{
			DrawOnePage();
		}


		//����...
		private static int intCurrentCharIndex;			//��̬������ס��ӡ����ʼ�ַ�
		private bool Draw(DrawText p_drawText,string p_text)
		{
			DrawText mdrawText = new DrawText(p_text);
//			mdrawText.Draw();

			mdrawText = this;
			mdrawText.StartChar = intCurrentCharIndex;

			intCurrentCharIndex += mdrawText.CharsFitted;

			if (intCurrentCharIndex < p_text.Length)
			{
				return true;
			}
			else
			{				
				intCurrentCharIndex = 0;			//��ӡ������һ��Ҫ������
				return false;
			}
		}


	}//End Class
}//End Namespace