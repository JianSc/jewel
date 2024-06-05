using System;

/// 
/// 作 者：长江支流(周方勇)
/// Email：flygoldfish@163.com  QQ：150439795
/// 网 址：www.webmis.com.cn
/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
/// 

namespace GoldPrinter.ExcelConstants
{
	/// <summary>
	/// Excel单元格范围内的边框及内部网格线
	/// </summary>
	public enum BordersEdge{xlLineStyleNone,xlLeft,xlRight,xlTop,xlBottom,xlDiagonalDown,xlDiagonalUp,xlInsideHorizontal,xlInsideVertical}

	/// <summary>
	/// Excel线样
	/// </summary>
	public enum BordersLineStyle{xlContinuous,xlDash,xlDashDot,xlDashDotDot,xlDot,xlDouble,xlLineStyleNone,xlSlantDashDot}

	/// <summary>
	/// Excel单元格范围内的边框及内部网格线粗细
	/// </summary>
	public enum BordersWeight{xlHairline,xlMedium,xlThick,xlThin}

}//End Namespace
