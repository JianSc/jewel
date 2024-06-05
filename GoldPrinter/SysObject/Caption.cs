using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// �ӱ��⡣
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class Caption:Printer
	{
		private string _text;			//�ı�
		private int _maxRows;			//��������
		private bool _hasborder;		//���߿�

		/// <summary>
		///  Ĭ�Ϲ��캯��
		/// </summary>
		public Caption()
		{	
			//��ʼֵ
			_text = "";
			_maxRows = 1;
			_hasborder = false;
			this.Font = new Font("����",13,FontStyle.Italic);		//����

			this.IsDrawAllPage = true;
		}

		/// <summary>
		/// �ӱ��⹹�캯��
		/// </summary>
		/// <param name="text"></param>
		public Caption(string text):this()
		{
			this._text = text;
		}

		public string Text
		{
			get{return this._text;}
			set{this._text = value;}
		}	

		public bool HasBorder
		{
			get{return this._hasborder;}
			set{this._hasborder = value;}
		}	

		/// <summary>
		/// ����������С��0Ϊ������
		/// </summary>
		public int MaxRows
		{
			get{return this._maxRows;}
			set{this._maxRows = value;}
		}	

		public override void Draw()
		{
			base.Draw();

			//��ͼ��ʼ���꼰�ַ����Ŀ����
			int x,y;
			x = this.PrinterMargins.X1;
			y = this.PrinterMargins.Y1;			

			//����ƶ�
			x += this.MoveX;
			y += this.MoveY;

			
			//�����ַ����ߴ��Ƿ����
			int width = this.PrinterMargins.Width;		//�����ʹ����	//this.TextWidth(this._text);	//��ȡ�ı��Ŀ�����ᳬ����Ч��ӡҳ�Ŀ�
			int height = this.TextHeight(this._text);	//��ȡ�ı��ĸߣ���������Ϊ��Ч��ӡҳ�Ŀ�

			//���ܳ������
			if (height > this.PrinterMargins.Height)
			{
				height = this.PrinterMargins.Height;
			}

			//��������
			if (this._maxRows > 0)
			{
				if (height > this.Font.Height * _maxRows)
				{
					height = this.Font.Height * _maxRows;
				}
			}
           
			//�ı��ĸ߲�������Ϊ��Ч��ӡҳ�Ŀ���˻����ʵ���ַ��Զ�����
			Rectangle rec = new Rectangle(x,y,width,height);
            

			StringFormat sf = new StringFormat();

			sf.Alignment = StringAlignment.Center;			//�������
			sf.LineAlignment = StringAlignment.Center;		//�������

			#region	����ʵ�ʻ�ͼ��
			//д����ʱ��������Ϣ
			rec.X = (this.PrinterMargins.Width - this.TextWidth(this.Text))/2  + this.PrinterMargins.Left + this.MoveX;
			rec.Y = y;
			if (this.TextWidth(this.Text) < this.PrinterMargins.Width)
			{
				rec.Width = this.TextWidth(this.Text);		
			}
			else
			{
				rec.Width = this.PrinterMargins.Width;				
			}

			rec.Height = height;	

			this.Rectangle = rec;
			#endregion

			//���ϴ�ӡ��Ч������
			if (_hasborder)
			{
				this.Graphics.DrawRectangle(this.Pen,this.Rectangle.X,this.Rectangle.Y,this.Rectangle.Width,this.Rectangle.Height);
			}

			rec.X = rec.X - 1;
			rec.Y = rec.Y - 1;
			rec.Width = rec.Width + 2;
			rec.Height = rec.Height + 2;
			//����ı�
			this.Graphics.DrawString(_text,this.Font,this.Brush,rec,sf);

			this.Height = height;
			
		}


	}//End Class
}//End NameSpace
