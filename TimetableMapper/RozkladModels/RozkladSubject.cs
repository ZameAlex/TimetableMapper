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
			return left.Equals(right);
		}

		public static bool operator !=(RozkladSubject left, RozkladSubject right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return $"Name: {Name}";
		}

	}
}
