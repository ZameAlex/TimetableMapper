using System;
using System.Diagnostics;
using System.Linq;
using TimeTableLibrary.Helpers;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.Mappers;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.Helpers.Git;

namespace ConsoleTimetableMapper
{
	class Program
	{
		static void Main(string[] args)
		{
			var content = new GitReader("teachers.csv").ParseMapping();
			new GitWriter("teachers.csv").WriteMapping(content);
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			string groupName = "КВ-83мн";
			RozkladClient client = new RozkladClient();
			client.Group = groupName;
			var observer = new ExampleDiagnosticObserver();
			IDisposable subscription = DiagnosticListener.AllListeners.Subscribe(observer);
			var rozkladLessons = client.GetTimetable().Result;
			var rzkSubjects = rozkladLessons[0].Select(r => r.Subject).Union(rozkladLessons[1].Select(r => r.Subject)).ToList();
			FpmClient fpmClient = new FpmClient();
			fpmClient.InitRequest().Wait(); 
			fpmClient.User = new TimeTableLibrary.FpmModels.FpmUser("leo", "leo");
			fpmClient.Login().Wait();
			fpmClient.SelectSubjectsAndGroups().Wait();
			fpmClient.SelectTeachers().Wait();
			var group = fpmClient.Groups.Where(g => g.Name == groupName).First();
			var res = new ResultLessonsMapper().Map(rozkladLessons[0], rozkladLessons[1]);
		}


	}
}
