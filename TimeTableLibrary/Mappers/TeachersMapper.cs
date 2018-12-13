using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.RozkladModels;

namespace TimeTableLibrary.Mappers
{
	public class TeachersMapper:IElementsMapper<RozkladTeacher, FpmTeacher>
	{
		private class TeacherComparer : IEqualityComparer<RozkladModels.RozkladTeacher>
		{
			public bool Equals(RozkladModels.RozkladTeacher x, RozkladModels.RozkladTeacher y)
			{
				if (x.Name == y.Name)
					return true;
				return false;
			}

			public int GetHashCode(RozkladModels.RozkladTeacher obj)
			{
				return obj.Name.GetHashCode();
			}
		}

		private List<FpmTeacher> MapFpmTeachers(string[] rzkTch, List<FpmTeacher> fpmTch)
		{
			var result = new List<FpmModels.FpmTeacher>();
			var count = fpmTch.Count;
			var wordNumber = 0;
			var temp = fpmTch;
			while (count > 0 && wordNumber < rzkTch.Length)
			{
				var temporarySubjects = temp.Where(sbj => sbj.Name.Contains(rzkTch[wordNumber]));
				if (temporarySubjects.Count() > 0)
				{
					temp = temporarySubjects.ToList();
					wordNumber++;
					count = temp.Count;
				}
				else
				{
					break;
				}
				result = temp;
			}
			return result;
		}

		public Dictionary<RozkladTeacher, List<FpmTeacher>> Map(List<FpmModels.FpmTeacher> fpmTch, List<RozkladModels.RozkladTeacher> rzkTch)
		{
			var result = new Dictionary<RozkladTeacher, List<FpmModels.FpmTeacher>>();
			var distinctTeachers = rzkTch.Distinct(new TeacherComparer());
			foreach (var teacher in distinctTeachers)
			{
				result.Add(teacher, MapFpmTeachers(teacher.Name.Split(' '), fpmTch));
			}
			return result;
		}

		public FpmTeacher Map(RozkladTeacher model)
		{
			throw new NotImplementedException();
		}

		public List<FpmTeacher> Map(List<RozkladTeacher> models)
		{
			throw new NotImplementedException();
		}
	}
}
