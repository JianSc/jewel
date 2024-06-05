using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// Outer��������֮��Ķ���ͨ�����ڱ�ͷ���������ĸ�����Ϣ��
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class Outer:Printer,IDisposable
	{
		//ƽ���п�
		private bool _IsAverageColsWidth;

		#region �ֶ�����
		/// <summary>
		/// �Ƿ�ƽ�������п�
		/// </summary>
		public bool IsAverageColsWidth
		{
			get
			{
				return _IsAverageColsWidth;
			}
			set
			{
				_IsAverageColsWidth = value;
			}
		}
		#endregion

		//����Ϊ������󣬲��������û�����
		protected DrawGrid mdrawGrid;

		//��ʶ�Ƿ��ʼ����������Initialize(int rows, int cols)
		//ֻ�г�ʼ�ˣ�����ִ��Draw()������
		protected bool mblnHadInitialized;

		public Outer()
		{
			_IsAverageColsWidth = true;

			mblnHadInitialized = false;
			this.IsDrawAllPage = false;

			mdrawGrid = new DrawGrid();
			
			mdrawGrid.AlignMent = AlignFlag.Left;
			mdrawGrid.ColsAlignString = "";
			mdrawGrid.Border = GridBorderFlag.None;
			mdrawGrid.Line = GridLineFlag.None;
			mdrawGrid.Merge = GridMergeFlag.None;

			this.Font = new Font("����",11);
			
			mdrawGrid.PreferredRowHeight = this.Font.Height;
			mdrawGrid.Initialize(0,0);

		}

		public object DataSource
		{
			get
			{
				return this.mdrawGrid.DataSource;
			}
			set
			{
				this.mdrawGrid.DataSource = value;

				if (DataSource.GetType().ToString() == "System.String[]" || DataSource.GetType().ToString() == "System.String[,]" || DataSource.GetType().ToString() == "System.Data.DataTable")
				{
					mblnHadInitialized = true;
				}
			}		
		}

		public string[,] Text
		{
			get
			{
				return this.mdrawGrid.GridText;
			}
			set
			{
				this.mdrawGrid.GridText = value;
				mblnHadInitialized = true;
			}		
		}

		/// <summary>
		/// ��ȡ�Ƿ���ִ�л��Ʋ�����ֻ�г�ʼ�˶�����������ſ���ִ��Draw()����
		/// </summary>
		/// <returns></returns>
		public bool CanDraw
		{
			get
			{
				return this.mblnHadInitialized;
			}
		}

		public int RowHeight
		{
			get
			{
				return mdrawGrid.PreferredRowHeight;
			}
			set
			{
				mdrawGrid.PreferredRowHeight = value;			
			}
		
		}

		/// <summary>
		/// ��ʼ������
		/// </summary>
		/// <param name="rows">��ʼ���������</param>
		/// <param name="cols">��ʼ���������</param>
		public virtual void Initialize(int rows, int cols)
		{
			mblnHadInitialized = true;
			mdrawGrid.Initialize(rows,cols);
		}

		/// <summary>
		/// ��ȡ���������
		/// </summary>
		public int Rows
		{
			get
			{
				return mdrawGrid.Rows;
			}
		}

		/// <summary>
		/// ��ȡ���������
		/// </summary>
		public int Cols
		{
			get
			{
				return mdrawGrid.Cols;
			}
		}

		/// <summary>
		/// ��ȡ�����ö�����п�
		/// </summary>
		public int[] ColsWidth
		{
			get
			{
				return mdrawGrid.ColsWidth;
			}
			set
			{
				mdrawGrid.ColsWidth = value;
			}
	
	}

		/// <summary>
		/// ��ȡ����ĸ�
		/// </summary>
		public override int Height
		{
			get
			{
				return mdrawGrid.Rows * mdrawGrid.PreferredRowHeight;
			}
		}

		/// <summary>
		/// Ϊ����ָ����Ԫ�����ı�ֵ
		/// </summary>
		/// <param name="row">��Ԫ��</param>
		/// <param name="col">��Ԫ��</param>
		/// <param name="text">�ı�ֵ</param>
		public virtual void SetText(int row, int col, string text)
		{
			mdrawGrid.SetText(row,col,text);
		}

        
		/// <summary>
		/// ��ָ�������зָ��ָ���һ���ַ�����Щ����Ĭ����ִ�г�ʼ������
		/// </summary>
		/// <param name="text"></param>
		/// <param name="colSplit"></param>
		public virtual void SetText(char rowSplit,char colSplit,string text)
		{
			mdrawGrid.SetText(rowSplit,colSplit,text);

			//mblnHadInitialized = true;
		}

		/// <summary>
		/// ��ȡ����ָ����Ԫ�ı�ֵ
		/// </summary>
		/// <param name="row">��Ԫ��</param>
		/// <param name="col">��Ԫ��</param>
		/// <returns></returns>
		public virtual string GetText(int row, int col)
		{
			return mdrawGrid.GetText(row,col);		
		}

		/// <summary>
		/// �ڻ�ͼ������ƶ�������ı�
		/// </summary>
		public override void Draw()
		{
			if (mblnHadInitialized)
			{
				base.Draw();

				//��ָ���������ڻ����ı�				
				mdrawGrid.Rectangle = new Rectangle((int)this.Rectangle.X + (int)this.MoveX,(int)this.Rectangle.Y  + (int)this.MoveY,(int)this.Rectangle.Width,(int)this.Rectangle.Height);
				mdrawGrid.Graphics = this.Graphics;

				if (this._IsAverageColsWidth)
				{
					mdrawGrid.Width = mdrawGrid.Rectangle.Width;
					mdrawGrid.ColsWidth = mdrawGrid.GetAverageColsWidth();
				}

				mdrawGrid.Draw();
			}
			else
			{
				throw new Exception("�������������δ��ʼ������Initialize�������в�����");
			}
		}

        #region IDisposable ��Ա

        public override void Dispose()
        {
            this.mdrawGrid.Dispose();
        }

        #endregion

    }//End Class
}//End NameSpace