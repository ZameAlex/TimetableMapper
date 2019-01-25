﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Helpers.Interfaces
{
	public interface IWriter:IMappingIO
	{
		void WriteMapping(Dictionary<string, string> dictionary);
	}
}
