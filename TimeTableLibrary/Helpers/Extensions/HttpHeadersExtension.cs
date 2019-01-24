using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace TimeTableLibrary.Extensions
{
	public static class HttpHeadersExtension
	{
		public static void AddIfNotExists(this HttpHeaders headers, string key, string value)
		{
			if (headers.Contains(key))
				return;
			headers.Add(key, value);
		}

		public static void AddIfNotExists(this HttpHeaders headers, string key, IEnumerable<string> value)
		{
			if (headers.Contains(key))
				return;
			headers.Add(key, value);
		}

	}
}
