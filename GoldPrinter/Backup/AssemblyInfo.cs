using System.Reflection;
using System.Runtime.CompilerServices;

//
// �йس��򼯵ĳ�����Ϣ��ͨ������
// ���Լ����Ƶġ�������Щ����ֵ���޸������
// ��������Ϣ��
//
//
// 
// �����ṩ���ܷ���(����֧��)  Email��flygoldfish@sina.com  QQ��150439795  ��ַ��www.webmis.com.cn
// ���������������ʹ�ô˳��򣬵�������������˵������ά��֪ʶ��Ȩ������

[assembly: AssemblyTitle("MIS���ʴ�ӡͨ�֣�����")]
[assembly: AssemblyDescription("��ͨ�ô�ӡ���򣬵��ݡ����ƾ֤����Ʊ�嵥���������⸴�ӱ�񡢺ϲ�����繤����ҵ��ͬ��������ϵͳ�ṩ�ļ���Ĭ�ϴ�ӡ������ϴ�ӡ��\r\n��DataGrid��DataTable��MSHFlexGrid��ListView�ȶ�ά��ʽȫ�����Դ�ӡ��Excel�����ӡ���Զ���ģ�塢��ʽ��ͳ�Ʒ�����ӡһ�и㶨��\r\n�������򿪷���Ϊ�ܷ��£���Ȩ���С�")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("���ߣ��ܷ���\r\nEmail��flygoldfish@sina.com\r\nQQ��150439795\r\n��ַ��www.webmis.com.cn")]
[assembly: AssemblyProduct("MIS���ʴ�ӡͨ--Դ�빲���")]
[assembly: AssemblyCopyright("Copyright���ܷ���")]
[assembly: AssemblyTrademark("Ʒ�ƣ����ʴ�ӡͨ")]
[assembly: AssemblyCulture("")]		


//
// ���򼯵İ汾��Ϣ������ 4 ��ֵ���:
//
//      ���汾
//      �ΰ汾 
//      �ڲ��汾��
//      �޶���
//
// ������ָ��������Щֵ��Ҳ����ʹ�á��޶��š��͡��ڲ��汾�š���Ĭ��ֵ�������ǰ�
// ������ʾʹ�� '*':

[assembly: AssemblyVersion("2.5.*")]

//
// Ҫ�Գ��򼯽���ǩ��������ָ��Ҫʹ�õ���Կ���йس���ǩ���ĸ�����Ϣ����ο� 
// Microsoft .NET Framework �ĵ���
//
// ʹ����������Կ�������ǩ������Կ��
//
// ע��:
//   (*) ���δָ����Կ������򼯲��ᱻǩ����
//   (*) KeyName ��ָ�Ѿ���װ�ڼ�����ϵ�
//      ���ܷ����ṩ����(CSP)�е���Կ��KeyFile ��ָ����
//       ��Կ���ļ���
//   (*) ��� KeyFile �� KeyName ֵ����ָ������ 
//       �������д���:
//       (1) ����� CSP �п����ҵ� KeyName����ʹ�ø���Կ��
//       (2) ��� KeyName �����ڶ� KeyFile ���ڣ��� 
//           KeyFile �е���Կ��װ�� CSP �в���ʹ�ø���Կ��
//   (*) Ҫ���� KeyFile������ʹ�� sn.exe(ǿ����)ʵ�ù��ߡ�
//       ��ָ�� KeyFile ʱ��KeyFile ��λ��Ӧ�������
//       ��Ŀ���Ŀ¼����
//       %Project Directory%\obj\<configuration>�����磬��� KeyFile λ��
//       ����ĿĿ¼��Ӧ�� AssemblyKeyFile 
//       ����ָ��Ϊ [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//   (*) ���ӳ�ǩ������һ���߼�ѡ�� - �й����ĸ�����Ϣ������� Microsoft .NET Framework
//       �ĵ���
//
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]
