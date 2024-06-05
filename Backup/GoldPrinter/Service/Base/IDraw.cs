using System;
using System.Drawing;
using System.Drawing.Printing;

namespace GoldPrinter
{
	/// <summary>
	/// 绘制接口，记法：在绘图表面Graphics绘制矩阵区域Rectangle（用指定的画笔Brush、Pen）绘制Draw()
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public interface IDraw
	{
		/// <summary>
		/// 获取或设置绘图表面
		/// </summary>
		Graphics Graphics
		{
			get;
			set;
		}

		/// <summary>
		/// 获取或设置绘制区域
		/// </summary>
		System.Drawing.Rectangle Rectangle
		{
			get;
			set;		
		}

		/*
        /// <summary>
        /// 画笔
        /// </summary>
        Pen Pen
        {
            get;
            set;
        }

        /// <summary>
        /// 画刷
        /// </summary>
        Brush Brush
        {
            get;
            set;
        }
		*/
        
        /// <summary>
		/// 绘制
		/// </summary>
		void Draw();

	}//End Interface
}//End NameSpace
