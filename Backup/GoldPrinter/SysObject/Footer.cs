using System;

namespace GoldPrinter
{
	/// <summary>
	/// Footer，紧挨在网格体下的对象，行列数不受限制。
	/// 
	/// 作 者：长江支流(周方勇)
	/// Email：flygoldfish@163.com  QQ：150439795
	/// 网 址：www.webmis.com.cn
	/// ★★★★★您可以免费使用此程序，但是请您完整保留此说明，以维护知识产权★★★★★
	/// 
	/// </summary>
	public class Footer:Outer
	{
		public Footer()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public Footer(int rows,int cols):this()
		{			
			base.Initialize(rows,cols);
		}

	}//End Class
}//End NameSpace
