using System;

namespace GoldPrinter
{
	/// <summary>
	/// TimeDef ��ժҪ˵����
	/// </summary>
	public class TimeDef
	{
		static System.DateTime dt1,dt2;

		public TimeDef()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public static void Start()
		{
			dt1 = System.DateTime.Now;
			Console.WriteLine("��ʼ��" + dt1.ToString() + dt1.Millisecond.ToString());		
		}
		
		public static void End()
		{
			dt2 = System.DateTime.Now;
			Console.WriteLine("������" + dt2.ToString() + dt2.Millisecond.ToString());

			TimeSpan aa = dt2 - dt1;
			double secondDef = aa.TotalSeconds;
			Console.WriteLine("��ʱ��"  + secondDef.ToString() + "��");
			Console.WriteLine("��ʱ��"  + aa.TotalMilliseconds.ToString() + "����");		
		}

	}
}
