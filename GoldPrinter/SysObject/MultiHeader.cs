using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// �й���ɫ�Ķ���ͷ��
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class MultiHeader:Header
	{
		private const int CONST_MAX_ROWS = 3;
		private bool _isDrawDiagonalLine;		//�Ƿ��ӡ��һ�еĶԽ������Ϊ0��0�У�ָ������Ϊ�յ�
		private float _DiagonalLineRows;		//������������С����1.5
		
		#region �ֶ�����		
		/// <summary>
		/// �Ƿ��ӡ��һ�еĶԽ��ߣ�Ҫָ������
		/// </summary>
		public bool IsDrawDiagonalLine
		{
			get
			{
				return _isDrawDiagonalLine;
			}
			set
			{
				_isDrawDiagonalLine = value;
			}
		}

		/// <summary>
		/// �Խ������Ϊ0��0�У�ָ������Ϊ�յ㡣����������С����1.5
		/// </summary>
		public float DiagonalLineRows
		{
			get
			{
				return _DiagonalLineRows;
			}
			set
			{
				_DiagonalLineRows = value;
			}
		}
		#endregion

		public MultiHeader()
		{
			this.IsDrawAllPage = true;
			mdrawGrid.AlignMent = AlignFlag.Center;
			mdrawGrid.Border = GridBorderFlag.Single;
			mdrawGrid.Line = GridLineFlag.Both;
			this.IsAverageColsWidth = false;

			_isDrawDiagonalLine= false;
			_DiagonalLineRows = 2;
			
			//������ʾ���ϲ�
			mdrawGrid.Merge = GridMergeFlag.Any;
			this.Font = new Font("����",12,FontStyle.Bold);
			
			mdrawGrid.PreferredRowHeight = this.Font.Height + 10;			
		}

		protected override int SetMaxRows()
		{
			return CONST_MAX_ROWS;
		}

		public void SetMergeTextOnRowSel(int row, int startCol,int endCol, string text)
		{			
			mdrawGrid.SetTextOnRowSel(row,startCol,endCol, text);
		}

		public void SetMergeTextOnColSel(int col, int startRow,int endRow, string text)
		{			
			mdrawGrid.SetTextOnColSel(col,startRow,endRow, text);
		}


		public MultiHeader(int rows,int cols):this()
		{
			base.Initialize(rows,cols);

			string mstrAlignment = "";

			//�����о��ж���
			for(int i = 0 ; i < cols ; i++)
			{
				mstrAlignment += "C";
			}
			this.mdrawGrid.ColsAlignString = mstrAlignment;
		}

		/// <summary>
		/// ���Խ��ߣ������ڵ�һ��
		/// </summary>
		protected void DrawDiagonalLine(float rows)
		{	
			try
			{
				int x1,y1,x2,y2;

				x1 = mdrawGrid.Rectangle.X;
				y1 = mdrawGrid.Rectangle.Y;

				x2 = x1 + mdrawGrid.ColsWidth[0];
				y2 = y1 + (int)(mdrawGrid.PreferredRowHeight * this._DiagonalLineRows);			
				
				this.Graphics.SetClip(new Rectangle(x1,y1,mdrawGrid.ColsWidth[0],mdrawGrid.PreferredRowHeight * mdrawGrid.Rows));

				this.Graphics.DrawLine(Pens.Black,x1,y1,x2,y2);
			}
			catch(Exception e)
			{}
			finally
			{
				this.Graphics.ResetClip();
			}

		}

		public override void Draw()
		{
			base.Draw ();
			if (_isDrawDiagonalLine)
			{
				DrawDiagonalLine(this._DiagonalLineRows);
			}
		}


		public string ColsAlign
		{
			get
			{
				return this.mdrawGrid.ColsAlignString;
			}
			set
			{
				this.mdrawGrid.ColsAlignString = value;
			}
		}


	}//End Class
}//End NameSpace
