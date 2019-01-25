using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeTableLibrary.Helpers.Abstracts;
using TimeTableLibrary.Helpers.Interfaces;

namespace TimeTableLibrary.Helpers.Local
{
	public class LocalReader : AbstractReader, IReader
	{
		public LocalReader(string filename) : base(filename)
		{
		}

		public Dictionary<string, string> ParseMapping()
		{
			return Read(new StreamReader(filename));
		}
	}
}
