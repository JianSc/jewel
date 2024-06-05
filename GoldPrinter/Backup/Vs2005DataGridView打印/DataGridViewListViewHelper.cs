using System;
using System.Windows.Forms;

namespace GoldPrinter
{
	//ע�⣬Ϊ֧��VS2005��DataGridView��������Ҫ�����������볣��Vs2005�������ǽ�����������\���ɣ����������볣��DEBUG;TRACE��ΪDEBUG;TRACE;Vs2005���ɡ�
	//���Ǵ�ҿ��԰Ѵ���ר�ŷŵ����Լ���VS2005�����У�#if Vs2005 #endifȥ������������ŵ����ʴ�ӡͨ�У����������ʴ�ӡͨ��ʹ��VS2003�±���Ҳ����Ϊͨ����VS2005��

	/// <summary>
	/// �ṩ��ListView��VS2005DataGridViewת���ɶ�ά����ķ�������ȡ�п������ʴ�ӡͨ��ӡ��
	/// </summary>
	public class DataGridViewListViewHelper
	{
		//��ӡVS2005DataGridView����ʾ��
//		private void btnPrintEasy_Click(object sender, System.EventArgs e)
//		{
//			GoldPrinter.MisGoldPrinter webmis = new GoldPrinter.MisGoldPrinter();   //��ӡ���
//			webmis.Title = "MIS���ʴ�ӡͨ\nWWW.WebMIS.COM.CN";                      //���⣬���������ӱ���
//			(webmis.Title as GoldPrinter.Title).Font = new System.Drawing.Font("����", 12, System.Drawing.FontStyle.Bold);
//
//			//������һ��Ϳ��Դ�ӡDataGridView
//			//(webmis.Body as GoldPrinter.Body).DataSource = ToStringArray(dataGridView1, true);
//            
//			//Ϊ�����Ի����Զ�����壬�����������塢�п��ж��뷽ʽ
//			GoldPrinter.Body gridBody = new GoldPrinter.Body();
//			//�����ά������ͨͨ��ӡ������������GridText����
//			gridBody.DataSource = ToStringArray(dataGridView1, true);
//			gridBody.Font = dataGridView1.Font;
//			gridBody.ColsWidth = GetColsWidth(dataGridView1);
//			webmis.Body = gridBody;
//
//			webmis.Preview();
//			webmis.Dispose();
//		}

#if Vs2005

		/// <summary>
		/// ��ȡVS.Net 2005 DataGridView�ؼ����п�
		/// </summary>
		/// <param name="dataGridView">VS.Net 2005 DataGridView�ؼ���</param>
		/// <returns>�п����顣</returns>
		public static int[] GetColsWidth(DataGridView dataGridView)
		{
			#region ʵ��...

			int[] arrReturn = null;

			int colsCount = dataGridView.ColumnCount;

			arrReturn = new int[colsCount];
			for (int i = 0; i < colsCount ; i++)
			{
				arrReturn[i] = dataGridView.Columns[i].Width;
			}

			return arrReturn;

			#endregion ʵ��
		}

		/// <summary>
		/// ��VS.Net 2005 DataGridView�ؼ������ݵ�������ά���顣
		/// </summary>
		/// <param name="dataGridView">VS.Net 2005 DataGridView�ؼ���</param>
		/// <param name="includeColumnText">�Ƿ�Ҫ���б����ı�Ҳ���������С�</param>
		/// <remarks>
		///  <����>����֧��</����>
		///  <����>2005-12-14</����>
		///  <�޸�></�޸�>
		/// </remarks>
		/// <returns>��ά���顣</returns>
		public static string[,] ToStringArray(DataGridView dataGridView, bool includeColumnText)
		{
			#region ʵ��...

			string[,] arrReturn = null;

			int rowsCount = dataGridView.Rows.Count;
			int colsCount = dataGridView.Columns.Count;

			if (rowsCount > 0)
			{
				//���һ���ǹ��������ʱ�����ö����ݡ�
				if (dataGridView.Rows[rowsCount - 1].IsNewRow)
				{
					rowsCount--;
				}
			}

			int i = 0;

			//�����б���
			if (includeColumnText)
			{
				rowsCount++;
				arrReturn = new string[rowsCount, colsCount];
				for (i = 0; i < colsCount; i++)
				{
					arrReturn[0, i] = dataGridView.Columns[i].HeaderText;
				}

				i = 1;
			}
			else
			{
				arrReturn = new string[rowsCount, colsCount];
			}

			//��ȡ��Ԫ������
			int rowIndex = 0;
			for (; i < rowsCount; i++, rowIndex++)
			{
				for (int j = 0; j < colsCount; j++)
				{
					arrReturn[i, j] = dataGridView.Rows[rowIndex].Cells[j].Value.ToString();
				}
			}

			return arrReturn;

			#endregion ʵ��
		}
#endif

		/// <summary>
		/// ��ȡListView�ؼ����п�
		/// </summary>
		/// <param name="listView">��ά������ͼ</param>
		/// <returns>�п����顣</returns>
		public static int[] GetColsWidth(ListView listView)
		{
			#region ʵ��...

			int[] arrReturn = null;

			int colsCount = listView.Columns.Count;

			arrReturn = new int[colsCount];
			for (int i = 0; i < colsCount; i++)
			{
				arrReturn[i] = listView.Columns[i].Width;
			}

			return arrReturn;

			#endregion ʵ��
		}

		/// <summary>
		/// ��ListView�����ݵ�������ά���顣
		/// </summary>
		/// <param name="listView">��ά������ͼ</param>
		/// <param name="includeColumnText">�Ƿ�Ҫ���б����ı�Ҳ���������С�</param>
		/// <remarks>
		///  <����>����֧��</����>
		///  <����>2005-08-21</����>
		///  <�޸�></�޸�>
		/// </remarks>
		/// <returns>��ά���顣</returns>
		public static string[,] ToStringArray(ListView listView, bool includeColumnText)
		{
			#region ʵ��...

			ListView lvw = listView;
			int rowsCount = lvw.Items.Count;
			int colsCount = lvw.Columns.Count;

			//�����б���
			if (includeColumnText)
			{
				rowsCount++;
			}

			string[,] arrReturn = null;

			arrReturn = new string[rowsCount, colsCount];

			int i = 0;

			if (includeColumnText)
			{
				//д����
				for (i = 0; i < colsCount; i++)
				{
					arrReturn[0, i] = lvw.Columns[i].Text;
				}

				i = 1;
			}

			//д������Items
			int rowIndex = 0;
			for (; i < rowsCount; i++, rowIndex++)
			{
				for (int j = 0; j < colsCount; j++)
				{
					arrReturn[i, j] = lvw.Items[rowIndex].SubItems[j].Text;
				}
			}

			return arrReturn;

			#endregion ʵ��
		}

	}//End Class
}//End Namespace
