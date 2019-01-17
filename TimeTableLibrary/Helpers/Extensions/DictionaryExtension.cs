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
	}
}
