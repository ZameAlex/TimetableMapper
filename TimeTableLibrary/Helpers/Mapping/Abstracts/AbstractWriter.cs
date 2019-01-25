using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TimeTableLibrary.Helpers.Abstracts
{
	public abstract class AbstractWriter
	{
		protected virtual string Write(Dictionary<string, string> dictionary)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(writer);
			csvWriter.WriteRecords<KeyValuePair<string, string>>(dictionary);
			stream.Position = 0;
			StreamReader reader = new StreamReader(stream);
			return reader.ReadToEnd();
		}
	}
}
