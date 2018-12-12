using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableLibrary.RozkladModels
{
	public class RozkladTeacher:IEquatable<RozkladTeacher>
	{
		public string Name { get; set; }

		public bool Equals(RozkladTeacher other)
		{
			if (Name == other.Name)
				return true;
			return false;
		}

		public static bool operator ==(RozkladTeacher left, RozkladTeacher right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(RozkladTeacher left, RozkladTeacher right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return $"Name: {Name}";
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as RozkladTeacher);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
