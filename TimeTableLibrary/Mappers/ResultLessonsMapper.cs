using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableLibrary.Enums;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.RozkladModels;

namespace TimeTableLibrary.Mappers
{
	public class ResultLessonsMapper
	{
		public List<ResultLesson> Map(List<RozkladLesson> firstWeekLessons, List<RozkladLesson> secondWeekLessons)
		{
			var result = new List<ResultLesson>();
			for (Day day = 0; day < Day.Sunday; day++)
			{
				for (LessonNumber number = LessonNumber.First; number < LessonNumber.Fifth; number++)
				{
					ResultLesson resultLesson = null;
					var firstWeekLesson = GetLessonByDayAndNumber(firstWeekLessons, day, number);
					var secondWeekLesson = GetLessonByDayAndNumber(secondWeekLessons, day, number);
					if (firstWeekLesson == null && secondWeekLesson == null)
						continue;
					else
					{
						resultLesson = new ResultLesson
						{
							DayOfWeek = day,
							LessonNumber = number,
							FirstWeekLesson = firstWeekLesson,
							SecondWeekLesson = secondWeekLesson
						};
						if (firstWeekLesson != secondWeekLesson)
							resultLesson.Flasher = true;
					}
					if (resultLesson != null)
						result.Add(resultLesson);
				}
			}
			return result;
		}

		private RozkladLesson GetLessonByDayAndNumber(List<RozkladLesson> lessons, Day day,LessonNumber number)
		{
			var lesson = lessons.Where(l => l.Day == day && l.LessonNumber == number).ToList();
			if (lesson == null || lesson.Count == 0)
				return null;
			return lesson.First();
		}

	}
}
