using System;

/// 
/// 作 者：长江支流(周方勇)
/// Email：flygoldfish@163.com  QQ：150439795
/// 网 址：www.webmis.com.cn
/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
/// 

namespace GoldPrinter
{

	#region 网格整体对齐方式 enum AlignFlag{Left,Center,Right}
	/// <summary>
	/// 对齐方式
	/// </summary>
	public enum AlignFlag
	{
		//	 ____________________________
		//  |							 |
		//	|Left						 |
		//	|____________________________|
		//  |							 |
		//	|			Center			 |
		//	|____________________________|
		//	|							 |
		//	|						Right|
		//	|____________________________|

		/// <summary>
		/// 左对齐
		/// </summary>
		Left	
			,
		/// <summary>
		/// 居中对齐
		/// </summary>
		Center	
			,
		/// <summary>
		/// 右对齐
		/// </summary>
		Right	
	}
	#endregion

	#region 网 格 线 enum GridLineFlag{None,Horizontal,Vertical,Both}
	/// <summary>
	/// 网格线，不包括边界
	/// </summary>
	public enum GridLineFlag
	{
		//			竖			线
		//  		|			|		 
		//			|			|		 
		//	 _______|___________|________	横
		//  		|			|		 
		//			|			|		 
		//	 _______|___________|________	线
		//			|			|		 
		//			|			|		 
		//			|			|

		/// <summary>
		/// 无
		/// </summary>
		None
			,
		/// <summary>
		/// 仅横线
		/// </summary>
		Horizontal
			,
		/// <summary>
		/// 仅竖线
		/// </summary>
		Vertical		
			,
		/// <summary>
		/// 横竖线两者都有
		/// </summary>
		Both		
	}
	#endregion

	#region 合并方式 enum GridMergeFlag{None,Row,Col,Both,Any}
	/// <summary>
	/// 合并方式
	/// </summary>
	public enum GridMergeFlag
	{
		//	None，这是标准备的行列
		//	 ____________________________
		//  |		|			|		 |
		//	|_______|___________|________|
		//  |		|			|		 |
		//	|_______|___________|________|
		//	|		|			|		 |
		//	|_______|___________|________|

		//	Row，在行上如果相邻列单元格内容相同，进行列合并
		//	 ____________________________
		//  |		|			|		 |
		//	|_______|___________|________|
		//	|					|		 |
		//  |此行相邻列相同,合并|		 |
		//	|___________________|________|
		//	|		|		列合并		 |
		//	|_______|____________________|

		//	Col，在列上如果相邻行单元格内容相等，进行行合并
		//	 ____________________________
		//  |		|这列上相邻	|		 |
		//	|_______|几行内容同	| 也合   |
		//  |		|因此行合并	|		 |
		//	|_______|___________| 并了   |
		//	|		|			|		 |
		//	|_______|___________|________|

		//	Both，在指定的行列上进行合并，相邻行单元格内容同合并，相邻列单元格内容同合并
		//	 ____________________________
		//  |		|这列上相邻	|		 |
		//	|_______|几行内容同	|________|
		//  |		|因此要合并	|		 |
		//	|_______|___________|________|
		//	|					|		 |
		//	|此行这几列相同,合并|		 |
		//	|___________________|________|

		//	Any，任意，只要相同就合并
		//	 ____________________________
		//  |					|		 |
		//	|		 ___________|________|
		//  |		|			|		 |
		//	|_______|___________|________|
		//	|		|			|		 |
		//	|_______|___________|________|

        //  ColDependOnBeforeGroup，在列上如果相邻行单元格内容相等，进行行合并，
        //  但是条件是前一列的组是同一组
        //   _______________________________________
        //  |        |_____1_____|___2____|___3_____|
        //  |        |           |        |A111属A11|
        //  |        |A1 这几个单|A11 同属|_________|
        //  |    A   |元格同属于A|A1小组  |___4_____|
        //  |        |组且内容同 |________|___5_____|
        //  |        |   合并    |A12 同属|___5_____|    
        //  |        |           |A1小组  |___6_____|
        //  |        |           |________|___7_____|
        //  |________|___________|____8___|___9_____|
        //  |        |           |____8___|___10____|    
        //  |        |           |    11  |   12    |    
        //  |   B    | E 同属于D |________|_________|
        //  |        |    合并   |    13  |   14    |
        //  |________|___________|________|_________|

        //这里虽然5、5相邻，但非同一组，即前面的依赖不一样，所有不合并
        //同理8、8也不合并

    	/// <summary>
		/// 无
		/// </summary>
        None
			,
		/// <summary>
		/// 在行上如果相邻列单元格内容相同，进行列合并
		/// </summary>
		Row
			,
		/// <summary>
		/// 在列上如果相邻行单元格内容相等，进行行合并
		/// </summary>
		Col	
			,
		/// <summary>
		/// 在列上如果相邻行单元格内容相等，进行行合并，但是条件是前一列的组是同一组
		/// </summary>
		ColDependOnBeforeGroup	
			,
		/// <summary>
		/// 在指定的行列上进行合并
		/// </summary>
		Both	
		,
		/// <summary>
		/// 任意，只要相同就合并
		/// </summary>
		Any	
	}
	#endregion

	#region 网格边框 enum GridBorderFlag{None,Single,SingleBold,Double,DoubleBold}
	/// <summary>
	/// 网格边框
	/// </summary>
	public enum GridBorderFlag
	{
		/// <summary>
		/// 无边框
		/// </summary>
		None
			,
		/// <summary>
		/// 单线边框
		/// </summary>
		Single
			,
		/// <summary>
		/// 单线边框加粗
		/// </summary>
		SingleBold	
			,
		/// <summary>
		/// 双边框
		/// </summary>
		Double	
			,
		/// <summary>
		/// 双边框加粗
		/// </summary>
		DoubleBold
	}
	#endregion

	#region 水平对齐方式 enum HAlignFlag{Left,Center,Right}
	/// <summary>
	/// 水平对齐方式
	/// </summary>
	public enum HAlignFlag
	{
		//	 ____________________________
		//  |							 |
		//	|Left						 |
		//	|____________________________|
		//  |							 |
		//	|			Center			 |
		//	|____________________________|
		//	|							 |
		//	|						Right|
		//	|____________________________|

		/// <summary>
		/// 左对齐
		/// </summary>
		Left	
			,
		/// <summary>
		/// 居中对齐
		/// </summary>
		Center	
			,
		/// <summary>
		/// 右对齐
		/// </summary>
		Right	

		//其它的根据GDI+的参数设置吧
	}
	#endregion

	#region 垂直方向对齐方式 enum VAlignFlag{Top,Middle,Bottom}
	/// <summary>
	/// 垂直对齐方式
	/// </summary>
	public enum VAlignFlag
	{
		//	 ____________________________
		//	|			Top				|
		//	|___________________________|
		//  |							|
		//	|			Center			|
		//	|___________________________|
		//	|							|
		//	|			Bottom			|
		//	 ---------------------------
		/// <summary>
		/// 顶对齐
		/// </summary>
		Top	
			,
		/// <summary>
		/// 垂直居中对齐
		/// </summary>
		Middle	
			,
		/// <summary>
		/// 底端
		/// </summary>
		Bottom	

		//其它的根据GDI+的参数设置吧
	}
	#endregion


}//End NameSpace