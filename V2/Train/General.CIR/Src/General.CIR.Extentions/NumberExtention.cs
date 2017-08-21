using System.Collections.Generic;

namespace General.CIR.Extentions
{
	public static class NumberExtention
	{
		public static string GetCurrentString(this IList<string> value, string str)
		{
			bool flag = value == null || value.Count == 0;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				int num = value.IndexOf(str);
				bool flag2 = num == -1;
				if (flag2)
				{
					result = value[0];
				}
				else
				{
					bool flag3 = num < value.Count - 1;
					if (flag3)
					{
						result = value[num + 1];
					}
					else
					{
						result = value[0];
					}
				}
			}
			return result;
		}

		public static string GetCurrentNumber(this string value, IList<string> values, string str)
		{
			bool flag = string.IsNullOrEmpty(value);
			string result;
			if (flag)
			{
				result = str;
			}
			else
			{
				bool flag2 = values.Contains(value[value.Length - 1].ToString());
				if (flag2)
				{
					result = value.Substring(0, value.Length - 1) + str;
				}
				else
				{
					result = value + str;
				}
			}
			return result;
		}

		public static string GetColumnNumber(this string value, string str, int length = 6)
		{
			bool flag = string.IsNullOrEmpty(value);
			string result;
			if (flag)
			{
				result = str;
			}
			else
			{
				bool flag2 = value.Length == length;
				if (flag2)
				{
					result = value;
				}
				else
				{
					value = (result = value + str);
				}
			}
			return result;
		}
	}
}
