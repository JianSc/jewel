using System;

/// 
/// �� �ߣ�����֧��(�ܷ���)
/// Email��flygoldfish@163.com  QQ��150439795
/// �� ַ��www.webmis.com.cn
/// ���������������ʹ�ô˳��򣬵�����������������˵������ά��֪ʶ��Ȩ������
/// 

namespace GoldPrinter
{
	/// <summary>
	/// PrintDocument.PrintPage��ί�ж���
	/// </summary>
	public delegate void PrintPageDelegate(Object obj,System.Drawing.Printing.PrintPageEventArgs ev) ;

	/// <summary>
	/// ������Excelί�ж���
	/// </summary>
	public delegate void ImportExcelDelegate(Object obj,ImportExcelArgs ev);

}//End NameSpace