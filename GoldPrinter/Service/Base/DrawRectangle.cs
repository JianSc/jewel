using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// ���α߿��ߣ����ҡ������׼��ıߡ����¶Խ��ߡ����϶Խ��ߡ�
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public enum BordersEdgeFlag{FourEdge,Left,Right,Top,Bottom,DiagonalDown,DiagonalUp}
				
	/// <summary>
	/// �ṩ�ڻ�ͼ������ָ���ı���ָ���ľ��������ߣ������ҡ������׼��ıߡ����¶Խ��ߡ����϶Խ��ߣ����Ըı�Pen����������ɫ����ȡ���ʽ��
	/// 
	/// �����ṩ���ܷ���(����֧��)
	/// ����������Email��flygoldfish@sina.com
	///���������������ʹ�ô˳��򣬵�������������˵������ά��֪ʶ��Ȩ������
	/// </summary>
	public class DrawRectangle:DrawBase
	{
		//���α߿���
		private BordersEdgeFlag _borderEdge;

		//������ɫ
		private Color _backColor;

		#region �ֶ�����

		/// <summary>
		/// ��ȡ�����þ��α߿��ߣ������ҡ������׼��ıߡ����¶Խ��ߡ����϶Խ��ߵ�
		/// </summary>
		public BordersEdgeFlag BordersEdge
		{
			get
			{
				return _borderEdge;
			}
			set
			{
				_borderEdge = value;
			}
		}

		/// <summary>
		/// ��ȡ�����������εı�����ɫ�� Ĭ��ΪColor.White��ֻ�иı�Ϊ��������ɫ����ʾ����FillRectangle()�����Ż�ȥ�汳��
		/// </summary>
		public Color BackColor
		{
			get
			{
				return _backColor;
			}
			set
			{
				_backColor = value;
			}		
		}

		#endregion
		
		/// <summary>
		/// �ڻ�ͼ������ָ���Ļ�����ָ���ľ��������ƾ��λ����ܱ߿��߻�Խ���
		/// </summary>
		public DrawRectangle()
		{
            _borderEdge = BordersEdgeFlag.FourEdge;

			_backColor = Color.White;
		}

		/// <summary>
		/// ���ξ��λ�߿���ûָ��BorderEdge��Ĭ�ϻ����������Ρ�
		/// </summary>
		public override void Draw()
		{
			switch(this.BordersEdge)
			{
				case BordersEdgeFlag.Left:	
					DrawLeftLine();
					break;
				case BordersEdgeFlag.Right:	
					DrawRightLine();
					break;
				case BordersEdgeFlag.Top:	
					DrawTopLine();
					break;
				case BordersEdgeFlag.Bottom:	
					DrawBottomLine();
					break;
				case BordersEdgeFlag.DiagonalDown:	
					DrawDiagonalDownLine();
					break;
				case BordersEdgeFlag.DiagonalUp:	
					DrawDiagonalUpLine();
					break;
				default:	//case BordersEdgeFlag.FourEdge:	
					Draw(this.Graphics,this.Pen,this.Rectangle);
					break;
			}

			//ֻ�иı���ɫ�Ż�����
			if (this.BackColor != Color.White)
			{
				FillRectangle();
			}
		}


		/// <summary>
		/// �ñ�����ɫ������
		/// </summary>
		public void FillRectangle()
		{
			Pen pen = new Pen(this.BackColor);
			this.Brush = pen.Brush;

			DrawBackColor(this.Graphics,this.Brush,Rectangle);
		}

		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		public void DrawLeftLine()
		{
			DrawLeftLine(this.Graphics,this.Pen,this.Rectangle);
		}

		/// <summary>
		/// �������Ҷ���
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		public void DrawRightLine()
		{
			DrawRightLine(this.Graphics,this.Pen,this.Rectangle);
		}

		/// <summary>
		/// �����ζ�����
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		public void DrawTopLine()
		{
			DrawTopLine(this.Graphics,this.Pen,this.Rectangle);
		}

		/// <summary>
		/// �����ε׶���
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		public void DrawBottomLine()
		{
			DrawBottomLine(this.Graphics,this.Pen,this.Rectangle);
		}

		/// <summary>
		/// �����Ͻǵ����½ǵĶԽ���
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		public void DrawDiagonalDownLine()
		{
			DrawDiagonalDownLine(this.Graphics,this.Pen,this.Rectangle);
		}

		/// <summary>
		/// �����½ǵ����ϽǵĶԽ���
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		public void DrawDiagonalUpLine()
		{
			DrawDiagonalUpLine(this.Graphics,this.Pen,this.Rectangle);
		}

		#region protected ȫ��������
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		protected void Draw(Graphics g,Pen pen,Rectangle rec)
		{			
			//�����񶥶˺���
			g.DrawRectangle(pen,rec.X,rec.Y,rec.Width,rec.Height);
		}

		/// <summary>
		/// �����������
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		protected void DrawLeftLine(Graphics g,Pen pen,Rectangle rec)
		{
			int X = rec.Left;
			int Y1 = rec.Top;
			int Y2 = rec.Bottom;

			//��������˺���
			g.DrawLine(pen,X,Y1,X,Y2);
		}

		/// <summary>
		/// �������Ҷ���
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		protected void DrawRightLine(Graphics g,Pen pen,Rectangle rec)
		{
			int X = rec.Right;
			int Y1 = rec.Top;
			int Y2 = rec.Bottom;

			//�������Ҷ˺���
			g.DrawLine(pen,X,Y1,X,Y2);
		}

		/// <summary>
		/// �����ζ�����
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		protected void DrawTopLine(Graphics g,Pen pen,Rectangle rec)
		{
			int X1 = rec.Left;
			int X2 = rec.Right;
			int Y = rec.Top;

			//�����񶥶˺���
			g.DrawLine(pen,X1,Y,X2,Y);
		}

		/// <summary>
		/// �����ε׶���
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		protected void DrawBottomLine(Graphics g,Pen pen,Rectangle rec)
		{
			int X1 = rec.Left;
			int X2 = rec.Right;
			int Y = rec.Bottom;

			//������׶˺���
			g.DrawLine(pen,X1,Y,X2,Y);
		}

		/// <summary>
		/// �����Ͻǵ����½ǵĶԽ���
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		protected void DrawDiagonalDownLine(Graphics g,Pen pen,Rectangle rec)
		{
			int X1 = rec.X;			//���Ͻ�����
			int Y1 = rec.Y;
			int X2 = rec.Right;		//���½�
			int Y2 = rec.Bottom;

			//������׶˺���
			g.DrawLine(pen,X1,Y1,X2,Y2);
		}

		/// <summary>
		/// �����½ǵ����ϽǵĶԽ���
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		protected void DrawDiagonalUpLine(Graphics g,Pen pen,Rectangle rec)
		{
			int X1 = rec.X;			//���½�����
			int Y1 = rec.Bottom;
			int X2 = rec.Right;		//���½�
			int Y2 = rec.Top;

			//������׶˺���
			g.DrawLine(pen,X1,Y1,X2,Y2);
		}

		protected void DrawBackColor(Graphics g,Brush brush,Rectangle rec)
		{
			//������
			g.FillRectangle(brush,rec);
		}

		#endregion

	}//End Class
}//End Namespace
