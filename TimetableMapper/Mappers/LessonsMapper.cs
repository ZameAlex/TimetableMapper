using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableMapper.Enums;
using TimetableMapper.FpmModels;
using TimetableMapper.RozkladModels;

namespace TimetableMapper.Mappers
{
	public class LessonsMapper:IMapper<RozkladLesson,FpmLesson>
	{
		public List<ResultLesson> Map(List<RozkladLesson> firstWeekLessons, List<RozkladLesson> secondWeekLessons)
		{
			var result = new List<ResultLesson>();
			for (Day  day = 0; day < Day.Sunday; day++)
			{
				var dailyLessons = firstWeekLessons.Where(l => l.Day == day).Union(secondWeekLessons.Where(l => l.Day == day)).ToList();
				for (LessonNumber lesson = LessonNumber.First; lesson < LessonNumber.Fifth; lesson++)
				{
					Lazy<ResultLesson> resultLesson = new Lazy<ResultLesson>();
					var lessonsByNumber = dailyLessons.Where(l => l.LessonNumber == lesson).ToList();
					if (lessonsByNumber == null || lessonsByNumber.Count == 0)
						continue;
					switch (lessonsByNumber.Count)
					{
						case 0:
							break;
						case 1:
							//resultLesson.Value.FirstWeekLesson = lessonsByNumber.First();
							resultLesson.Value.Flasher = false;
							
							break;
						case 2:
							//resultLesson.Value.FirstWeekLesson = lessonsByNumber.First();
							//resultLesson.Value.SecondWeekLesson = lessonsByNumber.Last();
							resultLesson.Value.Flasher = true;
							break;
					}
					resultLesson.Value.DayOfWeek = day;
					resultLesson.Value.LessonNumber = lesson;
					result.Add(resultLesson.Value);
				}
			}
			return result;
		}

		public FpmLesson Map(RozkladLesson model)
		{
			throw new NotImplementedException();
		}

		public List<FpmLesson> Map(List<RozkladLesson> models)
		{
			throw new NotImplementedException();
		}
	}
}
