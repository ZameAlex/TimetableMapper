using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableMapper.Enums;

namespace TimetableMapper.RozkladModels
{
	public class RozkladLesson
	{
		public RozkladSubject Subject { get; set; }
		public RozkladTeacher Teacher { get; set; }
		public Day Day { get; set; }
		public LessonNumber LessonNumber { get; set; }
		public string LessonType { get; set; }
		public bool FirstWeek { get; set; }

		public override string ToString()
		{
			return $"LessonNumber: {LessonNumber.ToString()} Day: {Day.ToString()}, Teacher: {Teacher}, Subject: {Subject}, LessonType: {LessonType}";
		}
	}
}
