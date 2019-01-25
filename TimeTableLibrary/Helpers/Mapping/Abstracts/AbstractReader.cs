using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TimeTableLibrary.Helpers.Abstracts
{
	public abstract class AbstractReader:AbstractIO
	{
		public AbstractReader(string filename) : base(filename)
		{
		}

		protected virtual Dictionary<string, string> Read(StreamReader reader)
		{
			CsvHelper.CsvReader csvReader = new CsvHelper.CsvReader(reader);
			var result = new Dictionary<string, string>();
			var anonymousTypeDefinition = new
			{
				Key = string.Empty,
				Value = string.Empty
			};
			var r = csvReader.GetRecords(anonymousTypeDefinition);
			foreach (var item in r)
			{
				if (item.Key.ToLower() != "key" && item.Value.ToLower() != "value")
					result.Add(item.Key, item.Value);
			}
			reader.Close();
			return result;
		}
	}
}
