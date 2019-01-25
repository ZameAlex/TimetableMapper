using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Helpers.Interfaces
{
	public interface IWriter
	{
		void WriteShareMapping(Dictionary<string, string> dictionary);
	}
}
