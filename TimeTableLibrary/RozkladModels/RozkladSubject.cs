using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableMapper.RozkladModels
{
	public class RozkladSubject:IEquatable<RozkladSubject>
	{
		public string Name { get; set; }
		public string Title { get; set; }

		public bool Equals(RozkladSubject other)
		{
			if (Title == other.Title || Name == other.Name)
				return true;
			return false;
		}

		public static bool operator ==(RozkladSubject left, RozkladSubject right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(RozkladSubject left, RozkladSubject right)
		{
			return !Equals(left, right);
		}

		public override string ToString()
		{
			return $"Name: {Name}";
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return Equals(obj as RozkladSubject);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
