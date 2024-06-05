using System;

namespace GoldPrinter
{
	/// <summary>
	/// Bottom，提供一个一行三列的对象，第一列居左，第三列居右，中间一旬居中。默认每页重复打印。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class Bottom:Top
	{
		public Bottom()
		{
			this.IsDrawAllPage = true;
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
