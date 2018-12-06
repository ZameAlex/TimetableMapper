using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableMapper.FpmModels
{
	public class FpmLesson
	{
		public FpmSubject Subject { get; set; }
		public FpmTeacher Teacher { get; set; }
		public string Room { get; set; }
	}
}
