using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Extensions
{
	public static class ListExtension
	{
		public static void AddIfNotExists(this List<KeyValuePair<string, string>> list, KeyValuePair<string, string> keyValue)
		{
			foreach (var item in list)
			{
				if (item.Key == keyValue.Key && item.Value == keyValue.Value)
					return;
			}
			list.Add(keyValue);
		}
	}
}
