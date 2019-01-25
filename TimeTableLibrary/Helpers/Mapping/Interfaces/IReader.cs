using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Helpers.Interfaces
{
	public interface IReader
	{
		Dictionary<string, string> ParseMapping();
	}
}
