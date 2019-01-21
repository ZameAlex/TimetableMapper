using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Extensions
{
	public static class DictionaryExtension
	{
		public static void Add(this IDictionary dictionary, Tuple<string,string> tuple)
		{
			dictionary.Add(tuple.Item1, tuple.Item2);
		}

		public static void Add(this IDictionary dictionary, KeyValuePair<string, string> pair)
		{
			dictionary.Add(pair.Key, pair.Value);
		}

		public static string ConvertToString(this Dictionary<string,string> dictionary)
		{
			StringBuilder builder = new StringBuilder();
			foreach (var item in dictionary)
			{
				builder.Append(item.Key);
				builder.Append(',');
				builder.Append(item.Value);
				builder.Append('\n');
			}
			return builder.ToString();
		}
	}
}
