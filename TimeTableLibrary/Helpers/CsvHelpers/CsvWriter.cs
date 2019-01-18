using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TimeTableLibrary.CsvHelpers
{
	public class CsvWriter
	{
		private readonly string fileName;

		public CsvWriter(string fileName)
		{
			this.fileName = fileName;
		}

		public void Write(Dictionary<string,string> dictionary)
		{
			StreamWriter writer = new StreamWriter(Path.Combine(Path.GetTempPath(),fileName));
			CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(writer);
			csvWriter.WriteRecords<KeyValuePair<string, string>>(dictionary);
			writer.Close();
		}
	}
}
