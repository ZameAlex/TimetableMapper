using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTableLibrary.FpmModels;

namespace TimeTableLibrary.Client
{
	public interface IFpmClient
	{
		List<FpmGroup> Groups { get; set; }
		List<FpmSubject> Subjects { get; set; }
		List<FpmTeacher> Teachers { get; set; }
		Task Login(FpmUser user);
		Task GetTeachers();
		Task GetSubjectsAndGroups();
		Task SetSubjectsToGroup(FpmGroup group, List<FpmSubject> subjects);
		Task SetTeacherToSubject(FpmTeacher teacher, FpmSubject subject);
		Task InitRequest();
		//Task SetScheduler()

	}
}
