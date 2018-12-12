using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTableLibrary.RozkladModels;

namespace TimeTableLibrary.Client
{
	public interface IRozkladClient
	{
		Task<List<RozkladLesson>[]> GetTimetable();
		Task InitRequest();
	}
}
