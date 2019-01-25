using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableLibrary.Helpers.Abstracts
{
	public abstract class AbstractIO
	{
		protected string filename;
		public AbstractIO(string filename)
		{
			this.filename = filename;
		}
	}
}
