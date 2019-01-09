using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;

namespace TimeTableLibrary.CsvHelpers
{
	public class CsvReader
	{
		private readonly string fileName;

		public CsvReader(string fileName)
		{
			this.fileName = fileName;
		}

		public Dictionary<string, string> Read()
		{
			StreamReader reader = new StreamReader(fileName);
			CsvHelper.CsvReader csvReader = new CsvHelper.CsvReader(reader);
			var result = new Dictionary<string, string>();
			var anonymousTypeDefinition = new
			{
				Key = string.Empty,
				Value = string.Empty
			};
			var r = csvReader.GetRecords(anonymousTypeDefinition);
			foreach(var item in r)
			{
				result.Add(item.Key, item.Value);
			}
			reader.Close();
			return result;
		}

	}
}
