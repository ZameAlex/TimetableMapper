using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableLibrary.Enums;

namespace TimeTableLibrary.RozkladModels
{
	public class RozkladLesson : IEquatable<RozkladLesson>
	{
		public RozkladSubject Subject { get; set; }
		public RozkladTeacher Teacher { get; set; }
		public Day Day { get; set; }
		public LessonNumber LessonNumber { get; set; }
		public string LessonTypeAndRoom { get; set; }
		public bool FirstWeek { get; set; }

		public bool Equals(RozkladLesson other)
		{
			if (other.Subject == Subject && other.Teacher == Teacher)
				return true;
			return false;
		}

		public static bool operator ==(RozkladLesson left, RozkladLesson right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(RozkladLesson left, RozkladLesson right)
		{
			return !Equals(left, right);
		}
	

		public override string ToString()
		{
			return $"LessonNumber: {LessonNumber.ToString()} Day: {Day.ToString()}, Teacher: {Teacher}, Subject: {Subject}, LessonType: {LessonTypeAndRoom}";
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as RozkladLesson);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		
	}
}
