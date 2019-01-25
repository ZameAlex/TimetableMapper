using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Helpers.Interfaces
{
	public interface IWriter
	{
		string Filename{ get; set;}
		void WriteMapping(Dictionary<string, string> dictionary);
	}
}
