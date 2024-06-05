using System;

namespace GoldPrinter
{
	/// <summary>
	/// TimeDef 的摘要说明。
	/// </summary>
	public class TimeDef
	{
		static System.DateTime dt1,dt2;

		public TimeDef()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static void Start()
		{
			dt1 = System.DateTime.Now;
			Console.WriteLine("开始：" + dt1.ToString() + dt1.Millisecond.ToString());		
		}
		
		public static void End()
		{
			dt2 = System.DateTime.Now;
			Console.WriteLine("结束：" + dt2.ToString() + dt2.Millisecond.ToString());

			TimeSpan aa = dt2 - dt1;
			double secondDef = aa.TotalSeconds;
			Console.WriteLine("用时："  + secondDef.ToString() + "秒");
			Console.WriteLine("用时："  + aa.TotalMilliseconds.ToString() + "毫秒");		
		}

	}
}
