using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// Top���ṩһ��һ�����еĶ��󣬵�һ�о��󣬵����о��ң��м�һѮ���С�Ĭ��ÿҳ�ظ���ӡ��
	/// 
	/// �� �ߣ�����֧��(�ܷ���)
	/// Email��flygoldfish@163.com  QQ��150439795
	/// �� ַ��www.webmis.com.cn
	/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
	/// 
	/// </summary>
	public class Top:Printer
	{
		//����Ϊ������󣬲��������û�����
		protected DrawGrid mdrawGrid;
		protected string _text;
		protected object _dataSource;

		public Top()
		{
			this.IsDrawAllPage = true;

			_text = "";
			mdrawGrid = new DrawGrid();
            
			//mdrawGrid.AlignMent = AlignFlag.Left;
			mdrawGrid.ColsAlignString = "LCR";
			mdrawGrid.Border = GridBorderFlag.None;
			mdrawGrid.Line = GridLineFlag.None;
			mdrawGrid.Merge = GridMergeFlag.None;
			
			this.Font = new Font("����",11);			

			mdrawGrid.PreferredRowHeight = this.Font.Height;

			mdrawGrid.Initialize(1,3);

		}		

		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
				SetText(this._text);
			}
		}


		/// <summary>
		/// ��ȡ����������Դ����������'|'�ָ����ַ�����һά����
		/// </summary>
		public object DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{	
				if (value != null)
				{
					if (value.GetType().ToString() == "System.String")
					{
						this.Text = (string)value;
					}
					else if(value.GetType().ToString() == "System.String[]")
					{
						string mstr = "";
						string[] marr = (string[])value;
						if (marr.Length > 0)
						{
							for(int i = 0 ; i < marr.Length ; i++)
							{
								mstr += "|" + marr[i];
							}
							mstr = mstr.Substring(1);
							this.Text = mstr;
						}
						else
						{
							this.Text = "";
						}
					}
				}
				else
				{
					this._dataSource = null;
				}
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
		/// �÷ָ���(Ĭ��'|')�ָ��Ĵ������ı�ֵ
		/// </summary>
		/// <param name="text"></param>
		public virtual void SetText(string text)
		{
			this._text = text;
			SetText(text,'|');
		}

		/// <summary>
		/// ��ָ���ָ����ָ��Ĵ������ı�ֵ
		/// </summary>
		/// <param name="text"></param>
		/// <param name="split"></param>
		public virtual void SetText(string text,char split)
		{
			this._text = text;

			string mstr = text;
			char msplit = split;

			string[] arrStr = mstr.Split(msplit);
			if (arrStr.Length > 0)
			{
				mdrawGrid.SetText(0,0,arrStr[0]);
			}
			if (arrStr.Length > 1)
			{
				mdrawGrid.SetText(0,1,arrStr[1]);
			}
			if (arrStr.Length > 2)
			{
				mdrawGrid.SetText(0,2,arrStr[2]);
			}

		}


		/// <summary>
		/// �ڻ�ͼ������ƶ�������ı�
		/// </summary>
		public override void Draw()
		{
			base.Draw();

			//��ָ���������ڻ����ı�			
			mdrawGrid.Rectangle = new Rectangle((int)this.Rectangle.X + (int)this.MoveX,(int)this.Rectangle.Y  + (int)this.MoveY,(int)this.Rectangle.Width,(int)this.Rectangle.Height);			
			mdrawGrid.Graphics = this.Graphics;
			
			mdrawGrid.Width = mdrawGrid.Rectangle.Width;
			mdrawGrid.ColsWidth = mdrawGrid.GetAverageColsWidth();

			mdrawGrid.Draw();
		}

	}//End Class
}//End NameSpace
