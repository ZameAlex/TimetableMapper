using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;

namespace TimeTableLibrary.GitHelpers
{
	public class GitChecker
	{
		public Dictionary<RozkladModels.RozkladSubject,FpmModels.FpmSubject> Subjects { get; set; }
		public Dictionary<RozkladModels.RozkladTeacher,FpmModels.FpmTeacher> Teachers { get; set; }
		public Dictionary<string, string> SubjectsExistInMapping{ get; protected set; }
		public Dictionary<string, string> TeachersExistInMapping { get; protected set; }

		FpmClient fpmClient;
		RozkladClient rozkladClient;

		public GitChecker(FpmClient fpmClient, RozkladClient rozkladClient)
		{
			this.fpmClient = fpmClient;
			this.rozkladClient = rozkladClient;
			Subjects = new Dictionary<RozkladModels.RozkladSubject, FpmModels.FpmSubject>();
			Teachers = new Dictionary<RozkladModels.RozkladTeacher, FpmModels.FpmTeacher>();
			SubjectsExistInMapping = new GitReader("teachers.csv").ParseSharedMapping();
			TeachersExistInMapping = new GitReader("subjects.csv").ParseSharedMapping();
		}

		public bool IsObjectsMapped(bool isTeacherOrSubject)
		{
			if (isTeacherOrSubject)
			{
				foreach (var item in rozkladClient.Teachers)
				{
					if (!TeachersExistInMapping.ContainsKey(item.Name))
						return false;
					else
					{
						Teachers.Add(rozkladClient.Teachers.SingleOrDefault(t => t.Name == item.Name),
							fpmClient.Teachers.SingleOrDefault(t => t.Id == TeachersExistInMapping[item.Name]));
					}
				}
				return true;
			}
			else
			{
				foreach (var item in rozkladClient.Subjects)
				{
					if (!SubjectsExistInMapping.ContainsKey(item.Name))
						return false;
					else
					{
						Subjects.Add(rozkladClient.Subjects.SingleOrDefault(t => t.Title == item.Title),
							fpmClient.Subjects.SingleOrDefault(t => t.Id == SubjectsExistInMapping[item.Title]));
					}
				}
				return true;
			}
		}
	}
}
