using System;
using System.Collections.Generic;

namespace General.CIR.Extentions
{
	public static class EnmerableExtention
	{
		public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
		{
			foreach (T current in values)
			{
				if (action != null)
				{
					action(current);
				}
			}
		}
	}
}
