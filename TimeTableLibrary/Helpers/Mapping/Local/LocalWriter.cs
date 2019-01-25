using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TimeTableLibrary.Helpers.Local
{
	public class LocalWriter : Abstracts.AbstractWriter, Interfaces.IWriter
	{
		public LocalWriter(string filename) : base(filename)
		{
		}

		public void WriteMapping(Dictionary<string, string> dictionary)
		{
			Write(dictionary);
		}

		protected override string Write(Dictionary<string, string> dictionary)
		{
			CsvHelper.CsvWriter writer = new CsvHelper.CsvWriter(new StreamWriter(filename));
			writer.WriteRecords(dictionary);
			writer.Dispose();
			return filename;
		}
	}
}
