using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.Mappers.FpmMappers;
using TimeTableLibrary.Mappers.RozkladMappers;
using TimeTableLibrary.Extensions;
using TimeTableLibrary.Helpers.Git;
using TimeTableLibrary.Helpers.Interfaces;
using TimeTableLibrary.Mappers.Interfaces;
using TimeTableLibrary.Helpers.Models;

namespace TimeTableLibrary.Helpers
{
	public class MappingService
	{
		public Dictionary<string, FpmModels.FpmSubject> Subjects { get; protected set; }
		public Dictionary<string ,FpmModels.FpmTeacher> Teachers { get; protected set; }

		RozkladClient rozkladClient;
		FpmClient fpmClient;
		IReader reader;
		IWriter writer;

		private DefaultFiles files;

		public MappingService(IReader reader, IWriter writer, RozkladClient rozkladClient, FpmClient fpmClient)
		{
			this.fpmClient = fpmClient;
			this.rozkladClient = rozkladClient;
			Subjects = new Dictionary<string, FpmModels.FpmSubject>();
			Teachers = new Dictionary<string, FpmModels.FpmTeacher>();
			this.writer = writer;
			this.reader = reader;
		}

		public void LoadData(DefaultFiles files)
		{
			this.files = files;
			reader.Filename = files.Subject;
			var SubjectsExistInMapping = reader.ParseMapping();
			reader.Filename = files.Teacher;
			var TeachersExistInMapping = reader.ParseMapping();
			AddMappedSubjects(SubjectsExistInMapping);
			AddMappedTeachers(TeachersExistInMapping);
		}

		public void AddMappedTeachers(Dictionary<string,string> dictionary)
		{
			foreach (var item in dictionary)
			{
				Teachers.AddIfNotExists(rozkladClient.Teachers.SingleOrDefault(t=>t.Name==item.Key).Name, 
				fpmClient.Teachers.SingleOrDefault(t=>t.Id==item.Value));
			}
		}

		public void AddMappedSubjects(Dictionary<string, string> dictionary)
		{
			foreach (var item in dictionary)
			{
				Subjects.AddIfNotExists(rozkladClient.Subjects.SingleOrDefault(s => s.Title == item.Key).Title,
				fpmClient.Subjects.SingleOrDefault(s => s.Id == item.Value));
			}
		}

		~MappingService()
		{
			writer.Filename = files.Subject;
			writer.WriteMapping(Subjects.ConvertToStringDictionary());
			writer.Filename = files.Teacher;
			writer.WriteMapping(Teachers.ConvertToStringDictionary());
		}
	}
}
