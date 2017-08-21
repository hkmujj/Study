using System.Collections.Generic;
using System.Linq;

namespace General.CIR.Extentions
{
	public static class EnumerableExtention
	{
		public static T GetNextItem<T>(this IEnumerable<T> values, T current, bool flag = true)
		{
			List<T> list = values.ToList<T>();
			int num = list.IndexOf(current);
			bool flag2 = num == -1;
			T result;
			if (flag2)
			{
				result = list.FirstOrDefault<T>();
			}
			else
			{
				bool flag3 = num == list.Count - 1;
				if (flag3)
				{
					if (flag)
					{
						result = list.FirstOrDefault<T>();
					}
					else
					{
						result = current;
					}
				}
				else
				{
					result = list[num + 1];
				}
			}
			return result;
		}

		public static T GetLastItem<T>(this IEnumerable<T> values, T current, bool flsg = true)
		{
			List<T> list = values.ToList<T>();
			int num = list.IndexOf(current);
			bool flag = num == -1;
			T result;
			if (flag)
			{
				result = list.FirstOrDefault<T>();
			}
			else
			{
				bool flag2 = num == 0;
				if (flag2)
				{
					if (flsg)
					{
						result = list[list.Count - 1];
					}
					else
					{
						result = current;
					}
				}
				else
				{
					result = list[num - 1];
				}
			}
			return result;
		}
	}
}
