using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Helpers.Interfaces
{
	public interface IReader
	{
		string Filename { get; set;}
		Dictionary<string, string> ParseMapping();
	}
}
