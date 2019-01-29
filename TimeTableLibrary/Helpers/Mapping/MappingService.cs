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
		public Dictionary<RozkladModels.RozkladSubject,FpmModels.FpmSubject> Subjects { get; protected set; }
		public Dictionary<RozkladModels.RozkladTeacher,FpmModels.FpmTeacher> Teachers { get; protected set; }

		IMapper<string,RozkladModels.RozkladSubject> RSMapper;
		IMapper<string, RozkladModels.RozkladTeacher> RTMapper;
		IMapper<string, FpmModels.FpmSubject> FSMapper;
		IMapper<string, FpmModels.FpmTeacher> FTMapper;
		IReader reader;
		IWriter writer;

		public MappingService(
		IMapper<string, RozkladModels.RozkladSubject> RSMapper,
		IMapper<string, RozkladModels.RozkladTeacher> RTMapper,
		IMapper<string, FpmModels.FpmSubject> FSMapper,
		IMapper<string, FpmModels.FpmTeacher> FTMapper,
		IReader reader,
		IWriter writer
		)
		{
			this.RTMapper = RTMapper;
			this.FSMapper = FSMapper;
			this.RSMapper = RSMapper;
			this.FTMapper = FTMapper;
			Subjects = new Dictionary<RozkladModels.RozkladSubject, FpmModels.FpmSubject>();
			Teachers = new Dictionary<RozkladModels.RozkladTeacher, FpmModels.FpmTeacher>();
			
		}

		public void LoadData(DefaultFiles files)
		{
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
				Teachers.AddIfNotExists(RTMapper.Map(item.Key), FTMapper.Map(item.Value));
			}
		}

		public void AddMappedSubjects(Dictionary<string, string> dictionary)
		{
			foreach (var item in dictionary)
			{
				Subjects.AddIfNotExists(RSMapper.Map(item.Key), FSMapper.Map(item.Value));
			}
		}
	}
}
