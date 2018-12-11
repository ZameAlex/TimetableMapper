using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableMapper.RozkladModels
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
	}
}
