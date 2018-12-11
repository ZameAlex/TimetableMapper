using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableMapper;
using TimetableMapper.FpmRequests;
using TimetableMapper.RozkladRequests;

namespace ComparerUtility
{
	class Program
	{
		static void Main(string[] args)
		{
			FpmClient fpmClient = new FpmClient();
			fpmClient.InitRequest().Wait();
			fpmClient.Login().Wait();
			fpmClient.GetSubjectsAndGroups().Wait();
			fpmClient.GetTeachers().Wait();
			RozkladClient rozkladClient = new RozkladClient("");
			rozkladClient.GetTimetable().Wait();
		}
	}
}
