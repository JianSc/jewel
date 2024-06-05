using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// �����⣬Ĭ�����ֻ��ӡ�������֣�����MaxRows������������\n��VBΪVbCrlf��Ӳ���С������һ�������»�ǿ���»��ߡ�
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class Title:Caption
	{
		public Title()
		{
			this.IsDrawAllPage = true;
			this.Font = new Font("����",21,FontStyle.Bold);		//����
			this.MaxRows = 2;
		}

		/// <summary>
		/// �����⹹�캯��
		/// </summary>
		/// <param name="text">Ĭ�����ֻ��ӡ�������֣�������\nӲ���С������һ�������»�ǿ���»���</param>
		public Title(string text):this()
		{		
			this.Text = text;
		}

		public override void Draw()
		{
			base.Draw();
			//�����һ���ı��»������»���

			float x1,x2,y,lineWidth;

			lineWidth = this.Rectangle.Width;

			//�������һ��
			int index = this.Text.LastIndexOf("\n");
			
			//��Ӳ����
			if (index > 0)
			{
				string txt = this.Text.Substring(index+1);
				lineWidth = this.TextWidth(txt);
			}
			
			x1 = (this.PrinterMargins.Width - lineWidth)/2 + this.PrinterMargins.Left + this.MoveX;
			y = this.Rectangle.Y + this.Rectangle.Height;
			x2 = x1 + lineWidth;	

			this.Graphics.DrawLine(this.Pen,x1,y-4,x2,y-4);
			this.Graphics.DrawLine(this.Pen,x1,y-2,x2,y-2);

//			//������Ӧ�ļ��»��ߵĸ�
//			this.Rectangle.Height += 2;
		}

	}//End Class
}//End NameSpace

