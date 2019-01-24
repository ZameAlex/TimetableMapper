using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Extensions
{
	public static class DictionaryExtension
	{

		public static KeyValuePair<string, string> IndexOf(this IDictionary dictionary, int count)
		{
			var enumerator = dictionary.GetEnumerator();
			enumerator.MoveNext();
			for (int i = 0; i < dictionary.Count; i++)
			{
				if (i == count)
				{
					var result = new KeyValuePair<string, string>
					(
					enumerator.Key.ToString(),
					enumerator.Value.ToString() 
					);
					enumerator.Reset();
					return result;
				}
				enumerator.MoveNext();
			}
			enumerator.Reset();
			return new KeyValuePair<string, string>();
		}
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
