using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// Top，提供一个一行三列的对象，第一列居左，第三列居右，中间一旬居中。默认每页重复打印。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class Top:Printer
	{
		//核心为网格对象，不对最终用户公开
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
			
			this.Font = new Font("宋体",11);			

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
		/// 获取或设置数据源，可以是以'|'分隔的字符串或一维数组
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
		/// 获取对象的高
		/// </summary>
		public override int Height
		{
			get
			{
				return mdrawGrid.Rows * mdrawGrid.PreferredRowHeight;
			}
		}


		/// <summary>
		/// 用分隔符(默认'|')分隔的串设置文本值
		/// </summary>
		/// <param name="text"></param>
		public virtual void SetText(string text)
		{
			this._text = text;
			SetText(text,'|');
		}

		/// <summary>
		/// 用指定分隔符分隔的串设置文本值
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
		/// 在绘图表面绘制对象绘制文本
		/// </summary>
		public override void Draw()
		{
			base.Draw();

			//在指定的区域内绘制文本			
			mdrawGrid.Rectangle = new Rectangle((int)this.Rectangle.X + (int)this.MoveX,(int)this.Rectangle.Y  + (int)this.MoveY,(int)this.Rectangle.Width,(int)this.Rectangle.Height);			
			mdrawGrid.Graphics = this.Graphics;
			
			mdrawGrid.Width = mdrawGrid.Rectangle.Width;
			mdrawGrid.ColsWidth = mdrawGrid.GetAverageColsWidth();

			mdrawGrid.Draw();
		}

	}//End Class
}//End NameSpace
