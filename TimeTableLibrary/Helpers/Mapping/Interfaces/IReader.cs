using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Helpers.Interfaces
{
	public interface IReader:IMappingIO
	{
		Dictionary<string, string> ParseMapping();
	}
}
