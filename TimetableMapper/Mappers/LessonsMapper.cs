using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableMapper.FpmModels;
using TimetableMapper.RozkladModels;

namespace TimetableMapper.Mappers
{
	public class LessonsMapper
	{
		public List<ResultLesson> Map(List<RozkladLesson> firstWeekLessons, List<RozkladLesson> secondWeekLessons)
		{
			var result = new List<ResultLesson>();

			return result;
		}
	}
}
