﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableLibrary.FpmModels
{
	public class FpmSubject
	{
		public string Name { get; set; }
		public string Id { get; set; }
		public override string ToString()
		{
			return $"Name:{Name}";
		}
	}
}
