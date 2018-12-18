using System;
using System.Linq;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.Mappers;
using TimeTableLibrary.RozkladRequests;

namespace ConsoleTimetableMapper
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			string groupName = "КВ-83мн";
			RozkladClient client = new RozkladClient(groupName);
			var rozkladLessons = client.GetTimetable().Result;
			foreach (var item in rozkladLessons[0])
			{
				Console.WriteLine(item);
			}
			Console.WriteLine();
			foreach (var item in rozkladLessons[1])
			{
				Console.WriteLine(item);
			}
			var rzkSubjects = rozkladLessons[0].Select(r => r.Subject).Union(rozkladLessons[1].Select(r => r.Subject)).ToList();
			//var rzkTeachers = result[0].Select(r => r.Teacher).Union(result[1].Select(r => r.Teacher)).ToList();
			FpmClient fpmClient = new FpmClient();
			fpmClient.InitRequest().Wait();
			fpmClient.User = new TimeTableLibrary.FpmModels.FpmUser("leo", "leoleo");
			fpmClient.Login().Wait();
			fpmClient.GetSubjectsAndGroups().Wait();
			fpmClient.GetTeachers().Wait();
			SubjectsMapper subjectsMapper = new SubjectsMapper();
			TeachersMapper teachersMapper = new TeachersMapper();
			var mappedSubj = subjectsMapper.Map(fpmClient.Subjects, rzkSubjects);
			var SubjuctIds = mappedSubj.Select(sbj => sbj.Value.FirstOrDefault()).ToList();
			var group = fpmClient.Groups.Where(g => g.Name == groupName).First();
			var res = new ResultLessonsMapper().Map(rozkladLessons[0], rozkladLessons[1]);
			//Add checking the right comparing of the subjects
			fpmClient.SetSubjectsToGroup(group, SubjuctIds).Wait();
			//var mappedTeach = teachersMapper.Map(fpmClient.Teachers, rzkTeachers);
			//var dailyLessons = firstWeekLessons.Where(l => l.Day == day).Union(secondWeekLessons.Where(l => l.Day == day)).ToList();
		}


	}
}
