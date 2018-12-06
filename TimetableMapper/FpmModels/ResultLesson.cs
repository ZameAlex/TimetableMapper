using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableMapper.Enums;

namespace TimetableMapper.FpmModels
{
	public class ResultLesson
	{
		public FpmLesson FirstWeekLesson { get; set; }
		public FpmLesson SecondWeekLesson { get; set; }
		public Day DayOfWeek { get; set; }
		public LessonNumber LessonNumber { get; set; }
		public bool Flasher { get; set; }
	}
}
