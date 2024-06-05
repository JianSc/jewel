using System;
using System.Drawing;
using System.Collections;

namespace GoldPrinter
{
	/// <summary>
	/// ��ά������ࡣ
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class GridBase:IGrid,IDisposable
	{
		//ע�⣬�����ģ�һ��ı��������������ݵĸı䣬�����д����Ӧ�Ĺ��� ��ChangeField������������
		//���������������иߡ��п�ĸı��������Ӧ�ĸı�

		//��ArrayList���ڲ������У���ΪArrayList��Insert()������ֱ����RowHeight/PreferredColWidth��ʼ������и߻��п�
		protected ArrayList _arrRowsHeight;					//�и����飬�ڲ��ã�����Set/GetRowHeight()���û�ȡָ���е��и�
		protected ArrayList _arrColsWidth;					//�п����飬�ڲ��ã�����Set/GetColWidth()���û�ȡָ���е��п�
		protected ArrayList _arrColsAlign;					//�ж������飬�ڲ��ã�����Set/GetColAlign()���û�ȡָ���е��п�
		protected string[,] _arrStrGrid = new string[0,0];	//����������(��ά����)�����Ըı������������������

		//���뵽һ���ܺõķ�������������еĲ��롢���ӣ��������ɵ�Ԫ���ı����иߡ��п�ȵȣ������������¶�������ʲô�ģ�Ч�ʴ�����
		#region �뷨
		//��DataTable������Ҫ�����Ķ����Զ�ά�����ʽ���棬ÿһ���ֶ���һ��Ҫ���صĶ���

		//protected System.Data.DataTable mdtGridAttributes = new System.Data.DataTable("GridAttributes");	//��¼��������

		/*
		 * mdataTable.Rows.Count		Ϊ���������
		 * mdataTable.Columns.Count		Ϊ���������
		 * mdataTable.Rows.Add()		����������һ��
		 * mdataTable.Rows.InsertAt()	���Բ���һ��
		_____________________________________________________________________________________________
			|		| �и�	| 		| 	...
		____|_______|_______|_______|________________________________________________________________
			|		|	18	|		|
--����ָ��__|_______|_______|_______|________________________________________________________________
		 	|		| ...	|		| 	...
		____|_______|_______|_______|________________________________________________________________
			|		|	18	|		|
		____|_______|_______|_______|________________________________________________________________
		
		��		��		�ı�	ͼ��	ǰ��ɫ	����ɫ	������
		0		0		0*0		��		blank	white
		0		1		0*1		��		blank	white
		...				...
		100		100		100*100	��		blank	white
		*/
		#endregion
		

		#region IGridBase ��Ա*********����ͨ��*********

		private Point _location = new Point(0,0);			//�����������
		private int _Width = 300;							//�����
		private int _Height = 200;							//�����
		private Font _font	= new Font("����",10);			//�ı�����

		#region ������㼰����

		/// <summary>
		/// �����������
		/// </summary>
		public Point Location
		{
			get
			{ 
				return this._location;
			}
			set
			{	
				this._location = value;
			}
		}

		/// <summary>
		/// ��ȡ����Ŀ�
		/// </summary>
		public int Width
		{
			get
			{				
				return this._Width;
			}
			set
			{
				this._Width = this.GetValidIntValue(value);
			}
		}

		/// <summary>
		/// ��ȡ����ĸ�
		/// </summary>
		public int Height
		{
			get
			{				
				return this._Height;
			}
			set
			{
				this._Height = this.GetValidIntValue(value);
			}
		}

		/// <summary>
		/// �����ı����壬Ĭ��������Ϊ�����СΪ10���ı����壬ֻ�����ػ�ʱ��������
		/// </summary>
		public Font Font
		{
			get
			{				
				return this._font;
			}
			set
			{
				this._font = value;
			}		
		}			

		#endregion

		private AlignFlag _alignFlag			= AlignFlag.Left;			//������������뷽ʽ�����Ըı��ı��п��ַ����б�
		private GridLineFlag _gridLineFlag		= GridLineFlag.Both;		//����������
		private GridMergeFlag _gridMergeFlag	= GridMergeFlag.None ;		//��Ԫ��ϲ���ʽ
		private GridBorderFlag _gridBorderFlag	= GridBorderFlag.Single;	//����߿�����

		#region �ֶ�����AlignMent\Line\Merge\Border

		/// <summary>
		/// ����������뷽ʽ
		/// </summary>
		public AlignFlag AlignMent
		{
			get
			{
				return this._alignFlag;
			}
			set
			{	
				this._alignFlag = value;
				ChangeFieldAlignMent();
			}
		}		
		
		/// <summary>
		/// ����������
		/// </summary>
		public GridLineFlag Line
		{
			get
			{
				return this._gridLineFlag;
			}
			set
			{	
				this._gridLineFlag = value;
			}
		}		

		/// <summary>
		/// ��Ԫ��ϲ���ʽ
		/// </summary>
		public GridMergeFlag Merge
		{
			get
			{
				return this._gridMergeFlag;
			}
			set
			{	
				this._gridMergeFlag = value;
			}
		}		

		/// <summary>
		/// ����߿�����
		/// </summary>
		public GridBorderFlag Border
		{
			get
			{
				return this._gridBorderFlag;
			}
			set
			{	
				this._gridBorderFlag = value;
			}
		}
		
		#endregion

		private int _rows	= 0;						//�����������Ըı���ض������飬���һ��������и�
		private int _cols	= 0;						//�����������Ըı���ض������飬�������������ж����б�

		private int _fixedRows	= 0;					//�̶���������ӡʱҲ�ظ���ӡ������������ʱ�̶�
		private int _fixedCols	= 0;					//�̶���������ӡʱҲ�ظ���ӡ������������ʱ�̶�

		private int _row	= 0;						//��ǰ�У���ǰѡ����Χ��ʼ��
		private int _col	= 0;						//��ǰ�У���ǰѡ����Χ��ʼ��

		private int _rowSel	= 0;						//��ǰѡ����Χ������
		private int _colSel	= 0;						//��ǰѡ����Χ������

		//��ȡ��Ч�������������������Ƕ����Ը�ֵʱҪ���õ�
		private int GetValidIntValue(int val)
		{
			int mval = val;
			if (mval < 0)
			{
				mval = 0;
			}
			if (mval > int.MaxValue)
			{
				mval = int.MaxValue;
			}
			return mval;					
		}

		#region �ֶ�����Rows\Cols\FixedRows\FixedCols\Row\Col\RowSel\ColSel�����������̶�����������ǰ���С�ѡ����������
		/// <summary>
		/// ������ע��ı������ᣬ��Ӱ�������ı������һ��������и�
		/// </summary>
		public int Rows
		{
			get
			{
				return this._rows;
			}
			set
			{	
				this._rows = GetValidIntValue(value);
				//�ı������ֶ�
				this.ChangeFieldRows();
			}
		}

		/// <summary>
		/// ������ע��ı���������Ӱ�������ı�
		/// </summary>
		public int Cols
		{
			get
			{
				return this._cols;
			}
			set
			{	
				this._cols = GetValidIntValue(value);
				//�ı������ֶ�
				this.ChangeFieldCols();
			}
		}

		/// <summary>
		/// �̶���������ӡʱҲ�ظ���ӡ������������ʱ�̶�����СΪ0�����Ϊ������
		/// </summary>
		public int FixedRows
		{
			get
			{
				return this._fixedRows;
			}
			set
			{
				this._fixedRows = GetValidIntValue(value);
				if (this._fixedRows > this.Rows)
				{
					this._fixedRows = this.Rows;
				}
			}
		}	

		/// <summary>
		/// �̶���������ӡʱҲ�ظ���ӡ������������ʱ�̶�����СΪ0�����Ϊ������
		/// </summary>
		public int FixedCols
		{
			get
			{
				return this._fixedCols;
			}
			set
			{
				this._fixedCols = GetValidIntValue(value);
				if (this._fixedCols > this.Cols)
				{
					this._fixedCols = this.Cols;
				}
			}
		}	

		/// <summary>
		/// ��ǰ�У���ǰѡ����Χ��ʼ��
		/// </summary>
		public int Row
		{
			get
			{
				return this._row;
			}
			set
			{	
				this._row = GetValidIntValue(value);

				if (this._row >= this.Rows)
				{
					this._row = this.Rows - 1;
				}
			}
		}

		/// <summary>
		/// ��ǰ�У���ǰѡ����Χ��ʼ��
		/// </summary>
		public int Col
		{
			get
			{
				return this._col;
			}
			set
			{	
				this._col = GetValidIntValue(value);

				if (this._col >= this.Cols)
				{
					this._col = this.Cols - 1;
				}
			}
		}

		
		/// <summary>
		/// ѡ���У�����ǰѡ����Χ������
		/// </summary>
		public int RowSel
		{
			get
			{
				return this._rowSel;
			}
			set
			{	
				int mrow = GetValidIntValue(value);
				//ѡ����Ϊ0�����һ��
				if(mrow >= this._rows)
				{
					mrow = this._rows - 1;
				}
				this._rowSel = mrow;
			}
		}

		/// <summary>
		/// ѡ���У�����ǰѡ����Χ������
		/// </summary>
		public int ColSel
		{
			get
			{
				return this._colSel;
			}
			set
			{	
				int mcol = GetValidIntValue(value);
				//ѡ����Ϊ0�����һ��
				if(mcol >= this._cols)
				{
					mcol = this._cols - 1;
				}
				this._rowSel = mcol;

			}
		}

		#endregion

		private int _rowheight	= 20;					//���иߣ����ڳ�ʼÿ�е��иߡ����Ըı��ʹ_arrRowsHeight��������
		private int _colWidth	= 75;					//���п����ڳ�ʼÿ�е��п����Ըı��ʹ_arrColsWidth�������ã�������5�����ֵĿ�

		#region ��ÿ���иߡ��п� ��RowHeight��PreferredColWidth�����ض�Ӧ������

		/// <summary>
		/// ��ȡ��������ѡ�иߣ�������Ϊ��λ
		/// </summary>
		public int PreferredRowHeight
		{
			get
			{
				return this._rowheight;
			}
			set
			{	
				this._rowheight = GetValidIntValue(value);
				this.ChangeFieldPreferredRowHeight();
			}
		}

		/// <summary>
		/// ��ȡ������Ĭ�ϵ��п�������Ϊ��λ
		/// </summary>
		public int PreferredColWidth
		{
			get
			{

				return this._rowheight;
			}
			set
			{	
				this._rowheight = GetValidIntValue(value);
				this.ChangeFieldPreferredColWidth();
			}
		}

		/// <summary>
		/// ��ȡָ���е��и�
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int get_RowHeight(int index)
		{
			return (int)(_arrRowsHeight[index]);
		}

		/// <summary>
		/// ����ָ���е��и�
		/// </summary>
		/// <param name="index"></param>
		/// <param name="rowHeight"></param>
		public void set_RowHeight(int index,int rowHeight)
		{
			int mRowHeight = GetValidIntValue(rowHeight);
			_arrRowsHeight[index] = mRowHeight;		
		}

		/// <summary>
		/// ��ȡָ���е��п�
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int get_ColWidth(int index)
		{
			return (int)(_arrColsWidth[index]);
		}

		/// <summary>
		/// ����ָ���е��п�
		/// </summary>
		/// <param name="index"></param>
		/// <param name="colWidth"></param>
		/// <returns></returns>
		public void set_ColWidth(int index,int colWidth)
		{
			int mcolWidth = GetValidIntValue(colWidth);
			_arrColsWidth[index] = mcolWidth;				
		}

		/// <summary>
		/// ����ˮƽ�ж��뷽ʽ
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public AlignFlag  get_ColAlignment(int index)
		{
			return (AlignFlag)(_arrColsAlign[index]);
		}

		/// <summary>
		/// ��ȡˮƽ�ж��뷽ʽ
		/// </summary>
		/// <param name="index"></param>
		/// <param name="colAlignment"></param>
		public void set_ColAlignment(int index, AlignFlag colAlignment)
		{
			_arrColsAlign[index] = colAlignment;
		}


		/// <summary>
		/// ��ȡ�������и����飬����ֵ���鳤����������ʱֻ������ָ�����֣�������и߲��䡣
		/// </summary>
		/// <returns></returns>
		public int[] RowsHeight
		{
			get
			{
				//���巵�ص��п�����
				int[] arr = new int[this._rows];

				int c = 0;

				for(int i = 0 ; i < this._rows ; i++)
				{
					arr[i] = (int)(this._arrRowsHeight[i]);	
				}			

				return arr;	
			}
			set
			{
				int c = 0;
				int[] arr = value;

				for(int i = 0 ; i < this._rows && i < arr.Length; i++)
				{
					this._arrRowsHeight[i] = arr[i];	
				}			
			}
		}

		/// <summary>
		/// ��ȡ�������п����飬����ֵ���鳤����������ʱֻ������ָ�����֣�������п��䡣
		/// </summary>
		/// <returns></returns>
		public int[] ColsWidth
		{
			get
			{
				//���巵�ص��п�����
				int[] arr = new int[this._cols];

				int c = 0;

				for(int i = 0 ; i < this._cols ; i++)
				{
					arr[i] = (int)(this._arrColsWidth[i]);	
				}			

				return arr;	
			}
			set
			{
				int c = 0;
				int[] arr = value;

				for(int i = 0 ; i < this._cols && i < arr.Length; i++)
				{
					this._arrColsWidth[i] = arr[i];	
				}			
			}		
		}

		/// <summary>
		/// �����ж�������
		/// </summary>
		/// <returns></returns>
		public AlignFlag[] ColsAlignment
		{
			get
			{

				//���巵�ص��п�����
				AlignFlag[] arr = new AlignFlag[this._cols];

				int c = 0;

				for(int i = 0 ; i < this._cols ; i++)
				{
					arr[i] = (AlignFlag)(this._arrColsAlign[i]);	
				}			

				return arr;		
			}
			set
			{

				//���巵�ص��п�����
				AlignFlag[] arr = new AlignFlag[this._cols];

				int c = 0;

				for(int i = 0 ; i < this._cols && i < arr.Length; i++)
				{
					this._arrColsAlign[i] = arr[i];	
				}			
			}
		}

		#endregion

		#region ��Ԫ���ı�

		/// <summary>
		/// ��ȡ�����õ�ǰ��Ԫ���ı�
		/// </summary>		
		public string Text
		{
			get
			{
				return _arrStrGrid[this._row,this._col];
			}
			set
			{
				_arrStrGrid[this._row,this._col] = value;			
			}
		}	

		/// <summary>
		/// ��ȡָ�����е�Ԫ���ı�
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public string get_TextMatrix(int row,int col)
		{
			return _arrStrGrid[row,col];
		}

		/// <summary>
		/// ����ָ����Ԫ���ı�
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <param name="textMatrix"></param>
		/// <returns></returns>
		public void set_TextMatrix(int row,int col,string textMatrix)
		{
			_arrStrGrid[row,col] = textMatrix;		
		}

		#endregion

		#endregion

		//Begin*****IGridBase֧�ֺ���*****
		#region ��Ӧ�ֶ����Ըı�ʱ�������Ĺ��̵���

		//�ı����и��ֶΣ���RowHeight����setʱ����
		protected virtual void ChangeFieldPreferredRowHeight()
		{
			InitRowHeight();
		}

		//�ı����и��ֶΣ���PreferredColWidth����setʱ����
		protected virtual void ChangeFieldPreferredColWidth()
		{
			InitColWidth();
		}

		//�ı��˶��뷽ʽ�ֶΣ���AlignMent����setʱ����
		protected virtual void ChangeFieldAlignMent()
		{
			InitColAlignMent();
		}

		//�ı������ֶΣ���Rows����setʱ����
		protected virtual void ChangeFieldRows()
		{
			ReDimArrString(ref _arrStrGrid,this._rows,this._cols);
			ResetRowHeight();		
		}

		//�ı������ֶΣ���Cols����setʱ����
		protected virtual void ChangeFieldCols()
		{
			ReDimArrString(ref _arrStrGrid,this._rows,this._cols);
			ResetColWidth();
		}

		#endregion

		#region �ı�Ĭ�ϵ��иߡ��п��ж���ʱ���� protected void InitRowHeight/InitColWidth()/InitColAlignMent()

		/// <summary>
		/// ��ʼ�����и�
		/// </summary>
		protected void InitRowHeight()
		{
			this._arrRowsHeight = new ArrayList();

			for(int i = 0 ; i < this._rows ; i++)
			{
				this._arrRowsHeight.Add(this._rowheight);
			}
		}

		/// <summary>
		/// ��ʼ�����п�
		/// </summary>
		protected void InitColWidth()
		{
			this._arrColsWidth = new ArrayList();

			for(int i = 0 ; i < this._cols ; i++)
			{
				this._arrColsWidth.Add(this._colWidth);
			}
		}

		/// <summary>
		/// ��ʼ�����ж���
		/// </summary>
		protected void InitColAlignMent()
		{
			this._arrColsAlign = new ArrayList();

			for(int i = 0 ; i < this._cols ; i++)
			{
				this._arrColsAlign.Add(this._alignFlag);
			}
		}

		#endregion

		#region �ı�������ʱ������ protected void ResetRowHeight/ResetColWidth()

		/// <summary>
		/// �ı�������ʱ�����������и�
		/// </summary>
		protected void ResetRowHeight()
		{
			int c = 0;
			c = this._arrRowsHeight.Count - this._cols;

			//�б��٣���������
			if (c > 0)
			{
				c = System.Math.Abs(c);
				for(int i=0;i<c;i++)
				{
					this._arrRowsHeight.RemoveAt(this._arrRowsHeight.Count-1);
				}
			}
			else if (c < 0)
			{
				//�����ӣ��ó�ʼ�и�
				c = System.Math.Abs(c);
				for(int i=0;i<c;i++)
				{
					this._arrRowsHeight.Add(this._rowheight);
				}
			}
		}

		/// <summary>
		/// �ı�������ʱ�����������п�
		/// </summary>
		protected void ResetColWidth()
		{
			int c = 0;
			c = this._arrColsWidth.Count - this._cols;

			//�б��٣���������
			if (c > 0)
			{
				c = System.Math.Abs(c);
				for(int i=0;i<c;i++)
				{
					this._arrColsWidth.RemoveAt(this._arrColsWidth.Count-1);
				}
			}
			else if (c < 0)
			{
				//�����ӣ��ó�ʼ�п�
				c = System.Math.Abs(c);
				for(int i=0;i<c;i++)
				{
					this._arrColsWidth.Add(this._colWidth);
				}
			}
		}

		#endregion
		//End*****IGridBase֧�ֺ���*****


		#region IDisposable ��Ա

		/// <summary>
		/// �ͷ��ɴ˶������õ�������Դ
		/// </summary>
		public virtual void Dispose()
		{
			this.Font.Dispose();
		}

		#endregion

		#region ���캯��

		public GridBase():this(3,4)
		{

		}

		public GridBase(int rows,int cols)
		{  
			//���иı�Ҫ���������ĸı�
			this._rows = rows;
			this._cols = cols;
			
			Initialize(rows,cols);
			
		}

		#endregion

		/// <summary>
		/// ������ǰ�����ǳ����
		/// </summary>
		/// <returns></returns>
		public IGrid Clone()
		{
			return (IGrid)(base.MemberwiseClone());
		}

		/// <summary>
		/// ��ʼ�������������ע����ι��캯���Զ����ô˷���
		/// </summary>
		/// <param name="rows"></param>
		/// <param name="cols"></param>
		public void Initialize(int rows,int cols)
		{
			this._rows = rows;
			this._cols = cols;

			ReDimArrString(ref _arrStrGrid,rows,cols);

			InitRowHeight();
			InitColWidth();
			InitColAlignMent();
		}

		/// <summary>
		/// ���ض��������п��
		/// </summary>
		public int GetAllColsWidthSum()
		{
			#region ʵ��...
			int mwidth = 0;
			for(int i = 0 ; i < this.Cols ; i++)
			{
				mwidth += (int)_arrColsWidth[i];
			}
			return mwidth;
			#endregion
		}
		
		/// <summary>
		/// ���ض��������иߺ�
		/// </summary>
		public int GetAllRowsHeightSum()
		{
			#region ʵ��...
			int mheight = 0;
			for(int i = 0 ; i < this.Rows ; i++)
			{
				mheight += (int)_arrRowsHeight[i];
			}
			return mheight;
			#endregion
		}


		public string[,] GridText
		{
			get
			{
				return _arrStrGrid;
			}
			set
			{
				_arrStrGrid = value;

				//��������������
				this._rows = _arrStrGrid.GetLength(0);
				this._cols = _arrStrGrid.GetLength(1);

				InitRowHeight();
				InitColWidth();
				InitColAlignMent();
				
			}
		}

		/// <summary>
		/// ���������ƽ���п�����
		/// </summary>
		/// <param name="IsAverageWidth">�Ƿ�ƽ������Ŀ�Ϊ�п�</param>
		/// <returns></returns>
		public int[] GetAverageColsWidth()
		{
			#region ʵ��...
			
			//���巵�ص��п�����
			int[] arrReturn = new int[this._cols];

			int c = 0;

			//ƽ���п�
			int avgWidth = this._Width / this._cols;
			for(int i = 0 ; i < this._cols - 1 ; i++)
			{
				arrReturn[i] = avgWidth;	
				c++;
			}
			//���һ��Ϊʣ�µ�ֵ����������Ϊ��ƽ��ʱ������С�������ܿ����
			arrReturn[arrReturn.Length - 1] = this._Width - avgWidth * c;

			return arrReturn;	
	
			#endregion
		}


		//֧�ֺ���
		#region	�ض����ά�ַ����鲢����ԭ������ protected void ReDimArrString(string[,] arrStr,int rows,int cols)
		/// <summary>
		/// ��ָ���������ض����ά�ַ����飬���齫����������Χ�ڵ�ԭ�����ݣ�������ÿմ�""���
		/// </summary>
		/// <param name="arrStr">ԭ��ά�ַ�����</param>
		/// <param name="rows">������</param>
		/// <param name="cols">������</param>
		protected void ReDimArrString(ref string[,] arrStr,int rows,int cols)
		{
			if(arrStr == null || arrStr.Length == 0)
			{
				arrStr = new string[rows,cols];
				//��""���
				for(int i = 0 ; i < rows ; i++)
				{
					for(int j = 0 ; j < cols ; j++)
					{							
						arrStr[i,j] = "";				 
					}				 
				}

			}
			else
			{
				string[,] arr = new string[rows,cols];
				int mOriginalRows = arrStr.GetLength(0);		//ԭ�е�����
				int mOriginalCols = arrStr.GetLength(1);		//ԭ�е�����

				int mBackRows = 0;								//��Ҫ����ԭ�����ݵ����������Ա���ԭ�����ݳ�ʼ
				int mBackcols = 0;									

				//�������...

				#region �������������п��ܱ��Ҳ���ܱ�С
				if(rows >= mOriginalRows)
				{
					//�б��
					if(cols >= mOriginalCols)
					{	//��ȫ��ԭ������Χ�ڻ���ȫ����ԭ��������ԭ�����ݳ�ʼ
						mBackRows = mOriginalRows;
						mBackcols = mOriginalCols;

						//���������""��ʼ
						for(int i = 0 ; i < mOriginalRows ; i++)
						{
							for(int j = 0 ; j < cols ; j++)
							{							
								arr[i,j] = "";				 
							}				 
						}

						//���������""��ʼ
						for(int i = mOriginalRows ; i < rows ; i++)
						{
							for(int j = 0 ; j < cols ; j++)
							{							
								arr[i,j] = "";				 
							}				 
						}
					
					}

					//�б�С
					if(cols <= mOriginalCols)
					{	
						mBackRows = mOriginalRows;
						mBackcols = cols;					
					
						//��������""��ʼ
						for(int i = mOriginalRows ; i < rows ; i++)
						{
							for(int j = 0 ; j < mOriginalCols ; j++)
							{							
								arr[i,j] = "";				 
							}				 
						}
					}
				}
				#endregion


				#region �������������п��ܱ��Ҳ���ܱ�С
				if(rows <= mOriginalRows)
				{
					//�б��
					if(cols >= mOriginalCols)
					{
						mBackRows = rows;
						mBackcols = mOriginalCols;					
					
						//����ԭ�з�Χ�ڣ����������ˣ����������""��ʼ
						for(int i = 0 ; i < mOriginalRows ; i++)
						{
							for(int j = mOriginalCols ; j < cols ; j++)
							{							
								arr[i,j] = "";				 
							}				 
						}
					}

					//�б�С
					if(cols <= mOriginalCols)
					{	//��ȫ��ԭ������Χ�ڻ���ȫ����ԭ��������ԭ�����ݳ�ʼ
						mBackRows = rows;
						mBackcols = cols;
					}
				}
				#endregion


				//��ԭ���������
				for(int i = 0 ; i < mBackRows ; i++)
				{
					for(int j = 0 ; j < mBackcols ; j++)
					{							
						arr[i,j] = arrStr[i,j];				 
					}				 
				}

				arrStr = arr;
			}
		}
		#endregion
			  
	}//End Class

}//End Namespace
