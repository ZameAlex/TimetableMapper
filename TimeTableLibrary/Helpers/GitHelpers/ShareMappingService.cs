using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.Mappers.FpmMappers;
using TimeTableLibrary.Mappers.RozkladMappers;
using TimeTableLibrary.Extensions;

namespace TimeTableLibrary.GitHelpers
{
	public class ShareMappingService
	{
		public Dictionary<RozkladModels.RozkladSubject,FpmModels.FpmSubject> Subjects { get; protected set; }
		public Dictionary<RozkladModels.RozkladTeacher,FpmModels.FpmTeacher> Teachers { get; protected set; }

		FpmClient fpmClient;
		RozkladClient rozkladClient;
		RozkladSubjectMapper RSMapper;
		RozkladTeacherMapper RTMapper;
		FpmSubjectMapper FSMapper;
		FpmTeacherMapper FTMapper;

		public ShareMappingService(
		FpmClient fpmClient, 
		RozkladClient rozkladClient, 
		RozkladSubjectMapper RSMapper, 
		RozkladTeacherMapper RTMapper, 
		FpmSubjectMapper FSMapper, 
		FpmTeacherMapper FTMapper)
		{
			this.fpmClient = fpmClient;
			this.rozkladClient = rozkladClient;
			this.RTMapper = RTMapper;
			this.FSMapper = FSMapper;
			this.RSMapper = RSMapper;
			this.FTMapper = FTMapper;
			Subjects = new Dictionary<RozkladModels.RozkladSubject, FpmModels.FpmSubject>();
			Teachers = new Dictionary<RozkladModels.RozkladTeacher, FpmModels.FpmTeacher>();
			var SubjectsExistInMapping = new GitReader("teachers.csv").ParseSharedMapping();
			var TeachersExistInMapping = new GitReader("subjects.csv").ParseSharedMapping();
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
