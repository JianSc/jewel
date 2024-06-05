using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// �������񣬺����Ƕ�ά���飬������DataGrid��MsHFlexGrid��DataTable��HtmlTable�ȶ�ά������ʽ������Դ��
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class DrawGrid:GoldGrid,IDraw,IDisposable
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

		#endregion

		public DrawGrid()
		{
			_pen = new Pen(Color.Black);
			_brush = Brushes.Black;
		}

		public DrawGrid(int rows,int cols)
		{
            this.Initialize(rows,cols);			
		}

        public override void Dispose()
        {
            base.Dispose ();
            this._graphics.Dispose();
        }

		/// <summary>
		/// ���������ߣ��������߽���
		/// </summary>
		public void DrawGridLine()
		{
			DrawGridLine(this._graphics,this._rectangle,this.Pen,this.GridText,this.PreferredRowHeight,this.ColsWidth,this.Line,this.Border,new PointF(1.0F,1.0F),this.Merge);
		}

		/// <summary>
		/// ���������ı�
		/// </summary>
		public void DrawGridText()
		{
			DrawGridText(this._graphics,this._rectangle,this.Brush,this.GridText,this.PreferredRowHeight,this.ColsWidth,this.ColsAlignString,this.Font,new PointF(1.0F,1.0F),this.Merge);
		}

		/// <summary>
		/// ���Ʊ߿�
		/// </summary>
		public void DrawGridBorder()
		{
			DrawGridBorder(this._graphics,this._rectangle,this.Pen,this.Border);
		}

		/// <summary>
		/// ���������缰�ı��ͱ߿�
		/// </summary>
		public void Draw()
		{
			DrawGridLine();
			
			/*
			//���Ի����õ�ʱ��
			System.DateTime dt1;
			dt1 = System.DateTime.Now;
			Console.WriteLine(dt1.ToString() + dt1.Millisecond.ToString());
			*/
			//______________________		
			DrawGridText();
			//______________________

			/*
			System.DateTime dt2;
			dt2 = System.DateTime.Now;
			Console.WriteLine(dt2.ToString() + dt2.Millisecond.ToString());

			TimeSpan aa = dt2 - dt1;
			double secondDef = aa.TotalSeconds;
			Console.WriteLine(secondDef.ToString());
			*/
			DrawGridBorder();

		}

		#region	���ƺ���

		#region ����׼��������ߺ��� protected void DrawGridLine(Graphics g,Rectangle p_rec,int p_rows,int p_cols,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY)
        /// <summary>
        /// �������ߣ���׼���ĺ����߽������
        /// </summary>
        /// <param name="g">��ͼ����</param>
        /// <param name="p_rec">��ͼ��</param>
		/// <param name="p_pen">��ͼ�ߵıʣ����Զ�����ɫ���߿�</param>
		/// <param name="p_rows">����</param>
        /// <param name="p_cols">����</param>
        /// <param name="p_rowHeight">�и�</param>
        /// <param name="p_arrColsWidth">�п�</param>
        /// <param name="p_gridLineFlag">����������</param>
        /// <param name="p_gridBorderFlag">�߿�����</param>
        /// <param name="p_scaleXY">ˮƽ�봹ֱ����������</param>
		/// <remarks>
		/// ��    �ߣ��ܷ���
		/// �޸����ڣ�2004-08-07
		/// </remarks>
		protected void DrawGridLine(Graphics g,Rectangle p_rec,Pen p_pen,int p_rows,int p_cols,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY)
		{
			//���ž������ڻ�ͼ
			Rectangle rec = new Rectangle(p_rec.X,p_rec.Y,p_rec.Width,p_rec.Height);

			//���ų���
			this.TransGrid(g,rec,p_scaleXY);

			#region �������߲Ż�
			if (p_gridLineFlag != GridLineFlag.None)
			{
				int lngRows = p_rows;	//arrStrGrid.GetLength(0);			//������Ҳ���ɶ�ά�������
				int lngCols = p_cols;	//arrStrGrid.GetLength(1);			//����
	
				int lngRowIndex;		//��ǰ��
				int lngColIndex;		//��ǰ��
				
				//��ֹ����
				int X1, X2,Y1, Y2;

				int lngLineLen;			//�߳�
				int lngLineHei;			//�߸�


				//�������ꡢ�߳����߸�
				lngLineLen = rec.Width;
				lngLineHei = rec.Height;

				#region �������߾ͻ�
				if (p_gridLineFlag == GridLineFlag.Horizontal || p_gridLineFlag == GridLineFlag.Both)
				{
					//******�Ȼ�����******
					X1 = rec.X;
					Y1 = rec.Y;
					X2 = X1 + lngLineLen;	
	        			
					//���ϱ������±ߵ��߲���
					for(lngRowIndex = 1 ; lngRowIndex < lngRows ; lngRowIndex++)
					{
						Y1 += p_rowHeight;						//������Ի����и�����
						
						//Y1 += p_arrRowsWidth[lngRowIndex - 1];//������Ի����и�����

						Y2 = Y1;
						g.DrawLine(p_pen,X1,Y1,X2,Y2);			
					}
				}
				#endregion 

				#region �������߾ͻ�
				if (p_gridLineFlag == GridLineFlag.Vertical || p_gridLineFlag == GridLineFlag.Both)
				{
					//******�ٻ�����******
					//�п�
					int[] mArrColWidth = new int[lngCols];
					mArrColWidth = p_arrColsWidth;

					//Y����
					X1 = rec.X;
					Y1 = rec.Y;
					Y2 = Y1 + lngLineHei;

					//��������ұߵ��߲���
					for(lngColIndex = 0 ; lngColIndex < lngCols-1 ; lngColIndex++)
					{
						X1 += mArrColWidth[lngColIndex];
						X2 = X1;
						g.DrawLine(p_pen,X1,Y1,X2,Y2);			
					}	
				}
				#endregion 

			}//End If
			#endregion

			//******�߿�******
			if (p_gridBorderFlag != GridBorderFlag.None)
			{
				this.DrawGridBorder(g,rec,p_pen,p_gridBorderFlag);
			}
			
			//���ã����ٱ任
			this.ResetTransGrid();
		
		}
		#endregion
	
		#region protected void DrawGridLine(Graphics g,Rectangle p_rec,Pen p_pen,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY)
		/// <summary>
		/// �������ߣ���׼���ĺ����߽������
		/// </summary>
		/// <param name="g">��ͼ����</param>
		/// <param name="p_rec">��ͼ��</param>
		/// <param name="p_pen">��ͼ�ߵıʣ����Զ�����ɫ���߿�</param>
		/// <param name="arrStrGrid">��ά���飬��Ӧ�������С�����</param>
		/// <param name="p_rowHeight">�и�</param>
		/// <param name="p_arrColsWidth">�п�</param>
		/// <param name="p_gridLineFlag">����������</param>
		/// <param name="p_gridBorderFlag">�߿�����</param>
		/// <param name="p_scaleXY">ˮƽ�봹ֱ����������</param>
		/// <remarks>
		/// ��    �ߣ��ܷ���
		/// �޸����ڣ�2004-08-07
		/// </remarks>
		protected void DrawGridLine(Graphics g,Rectangle p_rec,Pen p_pen,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY)
		{
			int lngRows = arrStrGrid.GetLength(0);			//����
			int lngCols = arrStrGrid.GetLength(1);			//����
			DrawGridLine(g,p_rec,p_pen,lngRows,lngCols,p_rowHeight,p_arrColsWidth,p_gridLineFlag,p_gridBorderFlag,p_scaleXY);
		}
		#endregion

		#region ���ϲ��ߵĺ��� protected void DrawGridMergeLine(Graphics g,Rectangle p_rec,Pen p_pen,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY,GridMergeFlag gridMergeFlag)
		/// <summary>
		/// �������ߣ����ݺϲ���ʽ�ж����ڵ�Ԫ������һ��һ��Ļ�
		/// </summary>
		/// <param name="g">��ͼ����</param>
		/// <param name="p_rec">��ͼ��</param>
		/// <param name="p_pen">��ͼ�ߵıʣ����Զ�����ɫ���߿�</param>
		/// <param name="arrStrGrid">��ά����</param>
		/// <param name="p_rowHeight">�и�</param>
		/// <param name="p_arrColsWidth">�п�</param>
		/// <param name="p_gridLineFlag">����������</param>
		/// <param name="p_gridBorderFlag">�߿�����</param>
		/// <param name="p_scaleXY">ˮƽ�봹ֱ����������</param>
		/// <param name="gridMergeFlag">����Ԫ��ϲ���ʽ</param>
		/// <remarks>
		/// ��    �ߣ��ܷ���
		/// �޸����ڣ�2004-08-07
		/// </remarks>
		protected void DrawGridMergeLine(Graphics g,Rectangle p_rec,Pen p_pen,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY,GridMergeFlag gridMergeFlag)
		{
			//���ž������ڻ�ͼ
			Rectangle rec = new Rectangle(p_rec.X,p_rec.Y,p_rec.Width,p_rec.Height);
			
			int lngRows = arrStrGrid.GetLength(0);			//����
			int lngCols = arrStrGrid.GetLength(1);			//����

			//���񲻺ϲ�ֱ�ӻ���׼�����ߣ�����һ����Ԫ��һ����Ԫ��Ļ�
			if (gridMergeFlag == GridMergeFlag.None)
			{
				this.DrawGridLine(g,rec,p_pen,lngRows,lngCols,p_rowHeight,p_arrColsWidth,p_gridLineFlag,p_gridBorderFlag,p_scaleXY);
				return;
			}
			else
			{	
				#region �������߲Ż�
				if (p_gridLineFlag != GridLineFlag.None)
				{
					//�任
					this.TransGrid(g,rec,p_scaleXY);
				
					//��ֹ����
					int X1, X2,Y1, Y2;

					//�п�
					int[] mArrColWidth = new int[lngCols];
					mArrColWidth = p_arrColsWidth;	

					#region	����Ԫ����

					//�߽粻��
					for(int i = 0 ; i < lngRows ; i++)
					{
						X1 = rec.X;
						Y1 = rec.Y;

						for(int j = 0 ; j < lngCols ; j++)
						{
							//����������ˮƽ�ߣ���������
							X2 = X1 + mArrColWidth[j];

							Y1 = rec.Y + p_rowHeight * i;		//****�����и�����
							Y2 = Y1;
							//���ڶ��п�ʼ�����µĺ��ߣ���ǰ������һ���ı���ͬ
							if (i > 0)
							{
								//����ϲ���ֻҪ���ڵ�Ԫ�����ݲ�ͬ�ͻ��ߣ���ֻҪ���ڵ�Ԫ��������ͬ�ͺϲ�
								if (gridMergeFlag == GridMergeFlag.Any)
								{
									//����(����:���в��ϲ� || �ı��� || ��ǰ������һ���ı���ͬ)
									if(arrStrGrid[i,j] == "" || arrStrGrid[i,j] != arrStrGrid[i-1,j])
									{
										g.DrawLine(p_pen,X1,Y1,X2,Y2);
									}
								}
							}

							//����������'���ߣ���������
							//���ڶ����Ժ�����ߣ���ǰ������һ�бȽ�
							if (j > 0)
							{
								Y2 = Y2 + p_rowHeight;			//****�����и�����
								X2 = X1;
								//����ϲ���ֻҪ���ڵ�Ԫ�����ݲ�ͬ�ͻ��ߣ���ֻҪ���ڵ�Ԫ��������ͬ�ͺϲ�
								if (gridMergeFlag == GridMergeFlag.Any)
								{
									//����(����:���в��ϲ� || �ı��� || ��ǰ������һ���ı���ͬ)
									if(arrStrGrid[i,j] == "" || arrStrGrid[i,j] != arrStrGrid[i,j-1])
									{
										g.DrawLine(p_pen,X1,Y1,X2,Y2);
									}
								}
							}
						
							//��һ��,�����						
							X1 += mArrColWidth[j];

						}//End For ��	
					}//End For ��					
					#endregion

					//******�߿�******
					if (p_gridBorderFlag != GridBorderFlag.None)
					{
						this.DrawGridBorder(g,rec,p_pen,p_gridBorderFlag);
					}
			
					//���ã����ٱ任
					this.ResetTransGrid();
				}//End If
				#endregion

			}//End If		
		}//End Function
		#endregion

		#region ��׼���ϲ�������ı� protected void DrawGridText(Graphics g,Rectangle p_rec,Brush p_brush,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,string alignment,Font p_font,PointF p_scaleXY)
		/// <summary>
		/// ���������ı�����׼�������е�Ԫ���޺ϲ�
		/// </summary>
		/// <param name="g">��ͼ����</param>
		/// <param name="p_rec">��ͼ��</param>
		/// <param name="p_brush">��ͼ�ı��Ļ�ˢ�����Զ�����ɫ</param>
		/// <param name="arrStrGrid">��ά�ַ����飨����</param>
		/// <param name="p_rowHeight">�̶��и�</param>
		/// <param name="p_arrColsWidth">�п����飬Ϊnullʱ��ƽ���п�</param>
		/// <param name="alignment">��Left��Center��Right���뷽ʽ��һ����ĸ��ɵĴ�</param>
		/// <param name="p_scaleXY">ָ��X��Y�����ű���ֵ</param>
		/// <remarks>
		/// ��    �ߣ��ܷ���
		/// �޸����ڣ�2004-08-07
		/// </remarks>
		protected void DrawGridText(Graphics g,Rectangle p_rec,Brush p_brush,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,string alignment,Font p_font,PointF p_scaleXY)
		{
			try
			{
				//���ž������ڻ�ͼ
				Rectangle rec = new Rectangle(p_rec.X,p_rec.Y,p_rec.Width,p_rec.Height);

				Font font = p_font;
				if (font == null)
				{
					font = new Font("����",12.0F);
				}

				int lngRows = arrStrGrid.GetLength(0);			//����
				int lngCols = arrStrGrid.GetLength(1);			//����

				//�п�
				int[] mArrColWidth = new int[lngCols];
				mArrColWidth = p_arrColsWidth;

				//�ж��뷽ʽ
				AlignFlag[] arrAlign;
				arrAlign = this.GetColsAlign(alignment);

				//�任
				this.TransGrid(g,rec,p_scaleXY);
			
				//��ֹ����
				int X1,Y1,width;

				#region	����Ԫ���ı�
			
				StringFormat sf = new StringFormat();			//�ַ���ʽ
				sf.LineAlignment = StringAlignment.Center;		//��ֱ����
				sf.FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap;
			 
				for(int i = 0 ; i < lngRows ; i++)
				{
					X1 = rec.X;
					Y1 = rec.Y + p_rowHeight*i;					//****����������

					for(int j = 0 ; j < lngCols ; j++)
					{
						width = mArrColWidth[j];
					
						Rectangle recCell = new Rectangle(X1,Y1,width,p_rowHeight + 4);  //ʵ���Ͼ��л���΢ƫ�ϣ���Ϊ������Ԥ���߾�

						sf.Alignment = StringAlignment.Near;				//Ĭ�������						

						if(arrAlign.Length > j)
						{
							if (arrAlign[j] == AlignFlag.Center)
							{
								sf.Alignment = StringAlignment.Center;		//����
							}
							else if (arrAlign[j] == AlignFlag.Right)
							{
								sf.Alignment = StringAlignment.Far ;		//����
							}
						}

						g.DrawString(arrStrGrid[i,j],font,p_brush,recCell,sf);

						X1 += width;

					}//End For ��	
				
				}//End For ��					
				#endregion

				//���ã����ٱ任
				this.ResetTransGrid();

//				font.Dispose();
			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
			}
			finally
			{
				
			}

		}//End Function
		#endregion

		#region �ϲ���ʽ�µ������ı� protected void DrawGridText(Graphics g,Rectangle p_rec,Brush p_brush,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,string alignment,Font p_font,PointF p_scaleXY,GridMergeFlag gridMergeFlag)
		/// <summary>
		/// ���������ı�����׼�������е�Ԫ���޺ϲ�
		/// </summary>
		/// <param name="g">��ͼ����</param>
		/// <param name="p_rec">��ͼ��</param>
		/// <param name="p_brush">��ͼ�ı��Ļ�ˢ�����Զ�����ɫ</param>
		/// <param name="arrStrGrid">��ά�ַ����飨����</param>
		/// <param name="p_rowHeight">�̶��и�</param>
		/// <param name="p_arrColsWidth">�п����飬Ϊnullʱ��ƽ���п�</param>
		/// <param name="alignment">��Left��Center��Right���뷽ʽ��һ����ĸ��ɵĴ�</param>
		/// <param name="p_scaleXY">ָ��X��Y�����ű���ֵ</param>
		/// <remarks>
		/// ��    �ߣ��ܷ���
		/// �޸����ڣ�2004-08-07
		/// </remarks>
		protected void DrawGridMergeText(Graphics g,Rectangle p_rec,Brush p_brush,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,string alignment,Font p_font,PointF p_scaleXY,GridMergeFlag gridMergeFlag)
		{
			if (gridMergeFlag == GridMergeFlag.None)
			{
				DrawGridText(g,p_rec,p_brush,arrStrGrid,p_rowHeight,p_arrColsWidth,alignment,p_font,p_scaleXY);
				return;
			}

			try
			{
				//���ž������ڻ�ͼ
				Rectangle rec = new Rectangle(p_rec.X,p_rec.Y,p_rec.Width,p_rec.Height);

				Font font = p_font;
				if (font == null)
				{
					font = new Font("����",12.0F);
				}

				int lngRows = arrStrGrid.GetLength(0);			//����
				int lngCols = arrStrGrid.GetLength(1);			//����

				//�п�
				int[] mArrColWidth = new int[lngCols];
				mArrColWidth = p_arrColsWidth;

				//�ж��뷽ʽ
				AlignFlag[] arrAlign;
				arrAlign = this.GetColsAlign(alignment);

				//�任
				this.TransGrid(g,rec,p_scaleXY);
			
				#region	����Ԫ���ı�
			
				StringFormat sf = new StringFormat();			//�ַ���ʽ
				sf.LineAlignment = StringAlignment.Center;		//��ֱ����
				sf.FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoWrap;

				CellRectangle cell = new CellRectangle(rec.X,rec.Y,0,p_rowHeight);	//��Ԫ��

				for(int i = 0 ; i < lngRows ; i++)
				{
					for(int j = 0 ; j < lngCols ; j++)
					{	
						//.....
						cell = this.GetMergeCell(new Point(rec.X,rec.Y),arrStrGrid,p_rowHeight,mArrColWidth,i,j);

						Rectangle recCell = new Rectangle(cell.Left,cell.Top,cell.Width,cell.Height + 4);  //ʵ���Ͼ��л���΢ƫ�ϣ���Ϊ������Ԥ���߾�

						sf.Alignment = StringAlignment.Near;				//Ĭ�������						

						if(arrAlign.Length > j)
						{
							if (arrAlign[j] == AlignFlag.Center)
							{
								sf.Alignment = StringAlignment.Center;		//����
							}
							else if (arrAlign[j] == AlignFlag.Right)
							{
								sf.Alignment = StringAlignment.Far ;		//����
							}
						}

						g.DrawString(arrStrGrid[i,j],font,p_brush,recCell,sf);
					}//End For ��	
				
				}//End For ��					
				#endregion

				//���ã����ٱ任
				this.ResetTransGrid();

				//font.Dispose();
			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
			}
			finally
			{
				
			}
		}//End Function
		#endregion

		#region protected void DrawGridBorder(Graphics g,Rectangle rec,Pen p_pen,GridBorderFlag p_gridBorderFlag)
		/// <summary>
		/// ��������߿�
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rec"></param>
		/// <param name="p_gridBorderFlag"></param>
		protected void DrawGridBorder(Graphics g,Rectangle rec,Pen p_pen,GridBorderFlag p_gridBorderFlag)
		{
			//�ޱ߿���˳�
			if (p_gridBorderFlag == GridBorderFlag.None)
			{
				return;
			}

			float penwidth = 1F;		//�ʿ�
			int movXY = 0;				//���ݱʵĴ�ϸҪ��Ӧ�ĵ�������

			switch (p_gridBorderFlag)
			{
				case GridBorderFlag.Single:
					break;
				case GridBorderFlag.SingleBold:
					penwidth = 2F;
					break;
				case GridBorderFlag.Double:
					//˫���ڱ߿�
					g.DrawRectangle(p_pen,rec);
					movXY = 2;
					break;				
				case GridBorderFlag.DoubleBold:
					//˫���ڱ߿�
					g.DrawRectangle(p_pen,rec);
					penwidth = 2F;
					movXY = 3;
					break;				
			}

			
			Pen pen = (Pen)(p_pen.Clone());
			pen.Width = penwidth;

			//g.DrawRectangle(pen,rec);			

			Rectangle recBorder = rec;
			recBorder.X = rec.X - movXY;
			recBorder.Y = rec.Y - movXY;
			recBorder.Width = rec.Width + movXY * 2;
			recBorder.Height = rec.Height + movXY * 2;
			//��߿�
			g.DrawRectangle(pen,recBorder);		

			pen.Dispose();
		}

		#endregion

		/// <summary>
		/// �任����
		/// </summary>
		/// <param name="g"></param>
		/// <param name="p_rec"></param>
		/// <param name="p_scaleXY"></param>
		private void TransGrid(Graphics g,Rectangle p_rec,PointF p_scaleXY)
		{
			//����ƽ�ƣ�ʹ���Ƶ�����ͼ�����ԭ����
			float translateX = 0.0f;
			float translateY = 0.0f;

			//���ű任
			if (!p_scaleXY.IsEmpty)
			{
				g.ScaleTransform(p_scaleXY.X,p_scaleXY.Y);
			}		
		}

		private void ResetTransGrid()
		{
			//this.Graphics.ResetTransform();
		}
		#endregion


		#region ���ϲ��ߵĺ��� protected void DrawGridLine(Graphics g,Rectangle p_rec,Pen p_pen,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY,GridMergeFlag gridMergeFlag)
		/// <summary>
		/// �������ߣ����ݺϲ���ʽ�ж����ڵ�Ԫ������һ��һ��Ļ�
		/// </summary>
		/// <param name="g">��ͼ����</param>
		/// <param name="p_rec">��ͼ��</param>
		/// <param name="p_pen">��ͼ�ߵıʣ����Զ�����ɫ���߿�</param>
		/// <param name="arrStrGrid">��ά����</param>
		/// <param name="p_rowHeight">�и�</param>
		/// <param name="p_arrColsWidth">�п�</param>
		/// <param name="p_gridLineFlag">����������</param>
		/// <param name="p_gridBorderFlag">�߿�����</param>
		/// <param name="p_scaleXY">ˮƽ�봹ֱ����������</param>
		/// <param name="gridMergeFlag">����Ԫ��ϲ���ʽ</param>
		/// <remarks>
		/// ��    �ߣ��ܷ���
		/// �޸����ڣ�2004-08-07
		/// </remarks>
		protected void DrawGridLine(Graphics g,Rectangle p_rec,Pen p_pen,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,GridLineFlag p_gridLineFlag,GridBorderFlag p_gridBorderFlag,PointF p_scaleXY,GridMergeFlag gridMergeFlag)
		{
			//���ž������ڻ�ͼ
			Rectangle rec = new Rectangle(p_rec.X,p_rec.Y,p_rec.Width,p_rec.Height);
			
			int lngRows = arrStrGrid.GetLength(0);			//����
			int lngCols = arrStrGrid.GetLength(1);			//����

			//���񲻺ϲ�ֱ�ӻ���׼�����ߣ�����һ����Ԫ��һ����Ԫ��Ļ�
			if (gridMergeFlag == GridMergeFlag.None)
			{
				this.DrawGridLine(g,rec,p_pen,lngRows,lngCols,p_rowHeight,p_arrColsWidth,p_gridLineFlag,p_gridBorderFlag,p_scaleXY);
				return;
			}
			else
			{	
				#region �������߲Ż�
				if (p_gridLineFlag != GridLineFlag.None)
				{
					//�任
					this.TransGrid(g,rec,p_scaleXY);
				
					//��ֹ����
					int X1, X2,Y1, Y2;

					//�п�
					int[] mArrColWidth = new int[lngCols];
					mArrColWidth = p_arrColsWidth;	

					#region	����Ԫ����

					//�߽粻��
					for(int i = 0 ; i < lngRows ; i++)
					{
						X1 = rec.X;
						Y1 = rec.Y;

						for(int j = 0 ; j < lngCols ; j++)
						{
							//����������ˮƽ�ߣ���������
							X2 = X1 + mArrColWidth[j];

							Y1 = rec.Y + p_rowHeight * i;		//****�����и�����
							Y2 = Y1;
							//���ڶ��п�ʼ�����µĺ��ߣ���ǰ������һ���ı���ͬ
							if (i > 0)
							{
								switch(gridMergeFlag)
								{								
									case GridMergeFlag.None:
										//�޺ϲ���ֱ�ӻ���
										g.DrawLine(p_pen,X1,Y1,X2,Y2);
										break;
									case GridMergeFlag.Row:
										//��������������ͬ�ͺϲ������ϲ���
										//����(����:��������ֻҪ�����Ͻ����кϲ���ˮƽ�߿϶���)
										g.DrawLine(p_pen,X1,Y1,X2,Y2);
										break;
									case GridMergeFlag.Col:
										//��������������ͬ�ͺϲ������ϲ���
										//����(����:���в��ϲ� || �ı��� || ��ǰ������һ���ı���ͬ)
										if(arrStrGrid[i,j] == "" || arrStrGrid[i,j] != arrStrGrid[i-1,j])
										{
											g.DrawLine(p_pen,X1,Y1,X2,Y2);
										}
										break;
									case GridMergeFlag.Any:
										//����ϲ���ֻҪ���ڵ�Ԫ�����ݲ�ͬ�ͻ��ߣ���ֻҪ���ڵ�Ԫ��������ͬ�ͺϲ�
										//����(����: �ı��� || ��ǰ������һ���ı���ͬ)
										if(arrStrGrid[i,j] == "" || arrStrGrid[i,j] != arrStrGrid[i-1,j])
										{
											g.DrawLine(p_pen,X1,Y1,X2,Y2);
										}
										break;
								}

							}

							//����������'���ߣ���������
							//���ڶ����Ժ�����ߣ���ǰ������һ�бȽ�
							if (j > 0)
							{
								Y2 = Y2 + p_rowHeight;			//****�����и�����
								X2 = X1;

								switch(gridMergeFlag)
								{								
									case GridMergeFlag.None:
										//�޺ϲ���ֱ�ӻ���
										g.DrawLine(p_pen,X1,Y1,X2,Y2);
										break;
									case GridMergeFlag.Row:
										//��������������ͬ�ͺϲ������ϲ���
										//����(����:���в��ϲ� || �ı��� || ��ǰ������һ���ı���ͬ)
										if(arrStrGrid[i,j] == "" || arrStrGrid[i,j] != arrStrGrid[i,j-1])
										{
											g.DrawLine(p_pen,X1,Y1,X2,Y2);
										}
										break;
									case GridMergeFlag.Col:
										//��������������ͬ�ͺϲ������ϲ���
										//����(����:�ޣ�������Ҫ��)
										g.DrawLine(p_pen,X1,Y1,X2,Y2);
										break;
									case GridMergeFlag.Any:
										//����ϲ���ֻҪ���ڵ�Ԫ�����ݲ�ͬ�ͻ��ߣ���ֻҪ���ڵ�Ԫ��������ͬ�ͺϲ�
										//����(����: �ı��� || ��ǰ������һ���ı���ͬ)
										if(arrStrGrid[i,j] == "" || arrStrGrid[i,j] != arrStrGrid[i,j-1])
										{
											g.DrawLine(p_pen,X1,Y1,X2,Y2);
										}
										break;
								}								
							}
						
							//��һ��,�����						
							X1 += mArrColWidth[j];

						}//End For ��	
					}//End For ��					
					#endregion

					//******�߿�******
					if (p_gridBorderFlag != GridBorderFlag.None)
					{
						this.DrawGridBorder(g,rec,p_pen,p_gridBorderFlag);
					}
			
					//���ã����ٱ任
					this.ResetTransGrid();
				}//End If
				#endregion

			}//End If		
		}//End Function
		#endregion


		protected void DrawGridText(Graphics g,Rectangle p_rec,Brush p_brush,string[,] arrStrGrid,int p_rowHeight,int[] p_arrColsWidth,string alignment,Font p_font,PointF p_scaleXY,GridMergeFlag gridMergeFlag)
		{
			if (gridMergeFlag == GridMergeFlag.None)
			{
				DrawGridText(g,p_rec,p_brush,arrStrGrid,p_rowHeight,p_arrColsWidth,alignment,p_font,p_scaleXY);
				return;
			}

			try
			{
				//���ž������ڻ�ͼ
				Rectangle rec = new Rectangle(p_rec.X,p_rec.Y,p_rec.Width,p_rec.Height);

				Font font = p_font;
				if (font == null)
				{
					font = new Font("����",12.0F);
				}

				int lngRows = arrStrGrid.GetLength(0);			//����
				int lngCols = arrStrGrid.GetLength(1);			//����

				//�п�
				int[] mArrColWidth = new int[lngCols];
				mArrColWidth = p_arrColsWidth;

				//�ж��뷽ʽ
				AlignFlag[] arrAlign;
				arrAlign = this.GetColsAlign(alignment);

				//�任
				this.TransGrid(g,rec,p_scaleXY);
			
				#region	����Ԫ���ı�
			
				StringFormat sf = new StringFormat();			//�ַ���ʽ
				sf.LineAlignment = StringAlignment.Center;		//��ֱ����
				sf.FormatFlags = StringFormatFlags.LineLimit;	//| StringFormatFlags.NoWrap; //�ɻ��з�

				CellRectangle cell = new CellRectangle(rec.X,rec.Y,0,p_rowHeight);	//��Ԫ��

				for(int i = 0 ; i < lngRows ; i++)
				{
					for(int j = 0 ; j < lngCols ; j++)
					{	
						//.....
						cell = this.GetMergeCell(new Point(rec.X,rec.Y),arrStrGrid,p_rowHeight,mArrColWidth,i,j,gridMergeFlag);

						Rectangle recCell = new Rectangle(cell.Left,cell.Top,cell.Width,cell.Height + 4);  //ʵ���Ͼ��л���΢ƫ�ϣ���Ϊ������Ԥ���߾�

						sf.Alignment = StringAlignment.Near;				//Ĭ�������						

						if(arrAlign.Length > j)
						{
							if (arrAlign[j] == AlignFlag.Center)
							{
								sf.Alignment = StringAlignment.Center;		//����
							}
							else if (arrAlign[j] == AlignFlag.Right)
							{
								sf.Alignment = StringAlignment.Far ;		//����
							}
						}

						g.DrawString(arrStrGrid[i,j],font,p_brush,recCell,sf);
					}//End For ��	
				
				}//End For ��					
				#endregion

				//���ã����ٱ任
				this.ResetTransGrid();

				//font.Dispose();
			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message);
			}
			finally
			{
				
			}
		}//End Function


		#region protected virtual Cell GetMergeCell(Point GridLocation,string[,] arrStrGrid,int rowHeight,int[] ArrColWidth,int rowSel,int colSel,GridMergeFlag gridMergeFlag)
		
		/// <summary>
		/// ����ϲ���ʽ�·���ָ����Ԫ���󶥿��
		/// </summary>
		/// <param name="GridLocation">�����������</param>
		/// <param name="arrStrGrid">��ά����</param>
		/// <param name="rowHeight">�и�</param>
		/// <param name="ArrColWidth">�п�����</param>
		/// <param name="rowSel">ָ����Ԫ����</param>
		/// <param name="colSel">ָ����Ԫ����</param>
		/// <returns></returns>
		protected virtual CellRectangle GetMergeCell(Point GridLocation,string[,] arrStrGrid,int rowHeight,int[] ArrColWidth,int rowSel,int colSel,GridMergeFlag gridMergeFlag)
		{
			CellRectangle cell = new CellRectangle(0,0,0,0);

			int lngRows = arrStrGrid.GetLength(0);	//����
			int lngCols = arrStrGrid.GetLength(1);	//����

			int lngMergeRows = 1;					//�ϲ�������������Ϊ1��
			int lngMergeCols = 1;					//�ϲ�������

			int lngStartRow = rowSel;				//��¼��˵�Ԫ��ϲ�����ʼ��
			int lngEndRow = rowSel;					//�Ա����߼����Y����

			int lngStartCol = colSel;				//��¼��˵�Ԫ��ϲ�����ʼ��
			int lngEndCol = colSel;					//�Ա��������X����

			if (gridMergeFlag==GridMergeFlag.Any || gridMergeFlag==GridMergeFlag.Col || gridMergeFlag==GridMergeFlag.ColDependOnBeforeGroup)
			{
				//������"��"�Ͻ����кϲ�ʱ��ʼ����ϲ��Ķ���
				//���ϲ�ϲ�(�в���)
				for(int rowIndex = rowSel-1 ; rowIndex >= 0 ; rowIndex--)
				{					
					if(arrStrGrid[rowSel,colSel] == arrStrGrid[rowIndex,colSel])
					{
						lngMergeRows++;
						lngStartRow--;
					}
					else
					{
						break;
					}					
				}
				//���²�ϲ�(�в���)
				for(int rowIndex = rowSel+1 ; rowIndex < lngRows ; rowIndex++)
				{
					if(arrStrGrid[rowSel,colSel] == arrStrGrid[rowIndex,colSel])
					{
						lngMergeRows++;
						lngEndRow++;
					}
					else
					{
						break;
					}
				}
			}

			if (gridMergeFlag==GridMergeFlag.Any || gridMergeFlag==GridMergeFlag.Row)
			{
				
				//������"��"�Ͻ����кϲ�ʱ��ʼ����ϲ��Ķ���
				//�����ϲ�(�в���)
				for(int colIndex = colSel-1 ; colIndex >= 0 ; colIndex--)
				{
					if(arrStrGrid[rowSel,colSel] == arrStrGrid[rowSel,colIndex])
					{
						lngMergeCols++;
						lngStartCol--;
					}
					else
					{
						break;
					}
				}
				//���Ҳ�ϲ�(�в���)
				for(int colIndex = colSel+1 ; colIndex < lngCols ; colIndex++)
				{
					if(arrStrGrid[rowSel,colSel] == arrStrGrid[rowSel,colIndex])
					{
						lngMergeCols++;
						lngEndCol++;
					}
					else
					{
						break;
					}
				}

			}

			//******************�����󶥿��******************
			int cellLeft = GridLocation.X;
			int cellTop = GridLocation.Y + lngStartRow * rowHeight;	//���и߲��ǹ̶��иߣ����Լ���֮ǰ�е��и��ܺ�

			int cellWidth = 0;
			int cellHeight = 0;

			//��Ԫ��ϲ��е�ǰ�ߵĵ�Ԫ���п��
			for(int i = lngStartCol-1 ; i >= 0 ; i--)
			{
				cellLeft += ArrColWidth[i];
			}

			//��Ԫ��ϲ����п��
			for(int i = lngStartCol ; i <= lngEndCol ; i++)
			{
				cellWidth += ArrColWidth[i];
			}

			cellHeight = lngMergeRows * rowHeight;					//���и߲��ǹ̶��иߣ����Լ��������е��и��ܺ�

			cell = new CellRectangle(cellLeft,cellTop,cellWidth,cellHeight);

			return cell;		
		}

		#endregion

    }//End Class
}//End NameSpace


