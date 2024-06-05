using System;
using System.Drawing;

namespace GoldPrinter
{
	/// <summary>
	/// 主标题，默认最多只打印两行文字，可用MaxRows调整，可以用\n（VB为VbCrlf）硬换行。在最后一行文字下画强调下划线。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class Title:Caption
	{
		public Title()
		{
			this.IsDrawAllPage = true;
			this.Font = new Font("黑体",21,FontStyle.Bold);		//字体
			this.MaxRows = 2;
		}

		/// <summary>
		/// 主标题构造函数
		/// </summary>
		/// <param name="text">默认最多只打印两行文字，可以用\n硬换行。在最后一行文字下画强调下划线</param>
		public Title(string text):this()
		{		
			this.Text = text;
		}

		public override void Draw()
		{
			base.Draw();
			//在最后一行文本下画两根下划线

			float x1,x2,y,lineWidth;

			lineWidth = this.Rectangle.Width;

			//仅找最后一个
			int index = this.Text.LastIndexOf("\n");
			
			//有硬换行
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

//			//高再相应的加下划线的高
//			this.Rectangle.Height += 2;
		}

	}//End Class
}//End NameSpace

