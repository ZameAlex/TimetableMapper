using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableLibrary.Enums;
using TimeTableLibrary.RozkladModels;

namespace TimeTableLibrary.FpmModels
{
	public class ResultLesson
	{
		public RozkladLesson FirstWeekLesson { get; set; }
		public RozkladLesson SecondWeekLesson { get; set; }
		public Day DayOfWeek { get; set; }
		public LessonNumber LessonNumber { get; set; }
		public bool Flasher { get; set; } = false;
	}
}
