using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Extensions
{
	public static class DictionaryExtension
	{
		public static void Add<TKey,TValue>(this IDictionary dictionary, Tuple<TKey, TValue> tuple)
		{
			dictionary.Add(tuple.Item1, tuple.Item2);
		}

		public static void Add<TKey, TValue>(this IDictionary dictionary, KeyValuePair<TKey, TValue> pair)
		{
			dictionary.Add(pair.Key, pair.Value);
		}

		public static void AddIfNotExists<TKey, TValue>(this IDictionary dictionary, KeyValuePair<TKey, TValue> pair)
		{
			foreach(var item in dictionary.Keys)
			{
				if (item.Equals(pair.Key))
					return;
			}
			dictionary.Add(pair);
		}

		public static void AddIfNotExists<TKey, TValue>(this IDictionary dictionary, TKey key, TValue value)
		{
			foreach (var item in dictionary.Keys)
			{
				if (item.Equals(key))
					return;
			}
			dictionary.Add(key, value);
		}

		public static void AddNew<TKey, TValue>(this IDictionary dictionary, KeyValuePair<TKey, TValue> pair)
		{
			dictionary.Remove(pair.Key);
			dictionary.Add(pair);
		}

		public static void AddNew<TKey, TValue>(this IDictionary dictionary, TKey key, TValue value)
		{
			dictionary.Remove(key);
			dictionary.Add(key,value);
		}

		public static string ConvertToString(this Dictionary<object, object> dictionary)
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
