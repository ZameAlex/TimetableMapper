using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.RozkladModels;

namespace TimeTableLibrary.Mappers
{
	public class LessonMapper : IMapper<RozkladModels.RozkladLesson, FpmModels.FpmLesson>
	{
		public FpmLesson Map(RozkladLesson model)
		{
			var result = new FpmLesson();
			result.Room = model.LessonTypeAndRoom;
			var subjectMapper = new SubjectsMapper();
			var teacherMapper = new TeachersMapper();
			result.Subject = subjectMapper.Map(model.Subject);
			result.Teacher = teacherMapper.Map(model.Teacher);
			return result;
		}

		public List<FpmLesson> Map(List<RozkladLesson> models)
		{
			var result = new List<FpmLesson>();
			foreach (var lesson in models)
				result.Add(Map(lesson));
			return result;
		}
	}
}
