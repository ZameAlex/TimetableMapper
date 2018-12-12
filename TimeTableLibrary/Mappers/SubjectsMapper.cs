using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.RozkladModels;

namespace TimeTableLibrary.Mappers
{
	public class SubjectsMapper:IMapper<RozkladSubject,FpmSubject>
	{
		public SubjectsMapper()
		{

		}

		private class SubjectComparer : IEqualityComparer<RozkladModels.RozkladSubject>
		{
			public bool Equals(RozkladModels.RozkladSubject x, RozkladModels.RozkladSubject y)
			{
				if (x.Name == y.Name)
					return true;
				return false;
			}

			public int GetHashCode(RozkladModels.RozkladSubject obj)
			{
				return obj.Name.GetHashCode();
			}
		}

		public List<FpmModels.FpmSubject> MapFpmSubjects(string[] rzkSubj, string[] rzkSubjTitle, List<FpmModels.FpmSubject> fpmSbj)
		{
			var result = new List<FpmModels.FpmSubject>();
			var count = fpmSbj.Count;
			var wordNumber = 0;
			var temp = fpmSbj;
			while (count > 0 && wordNumber < rzkSubj.Length)
			{
				var temporarySubjects = temp.Where(sbj => sbj.Name.Contains(rzkSubj[wordNumber]));
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
			if (result.Count == 0)
			{
				wordNumber = 0;
				temp = fpmSbj;
				while (count > 0 && wordNumber < rzkSubjTitle.Length)
				{
					var temporarySubjects = temp.Where(sbj => sbj.Name.Contains(rzkSubjTitle[wordNumber]));
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
			}
			return result;

		}

		public Dictionary<RozkladSubject, List<FpmModels.FpmSubject>> Map(List<FpmModels.FpmSubject> fpmSbj, List<RozkladModels.RozkladSubject> rzkSbj)
		{
			var result = new Dictionary<RozkladSubject, List<FpmModels.FpmSubject>>();
			var distinctSubjects = rzkSbj.Distinct(new SubjectComparer());
			foreach (var subject in distinctSubjects)
			{
				result.Add(subject, MapFpmSubjects(subject.Name.Split(' '), subject.Title.Split(' '), fpmSbj));
			}
			return result;
		}

		public FpmSubject Map(RozkladSubject model)
		{
			throw new NotImplementedException();
		}

		public List<FpmSubject> Map(List<RozkladSubject> models)
		{
			throw new NotImplementedException();
		}
	}
}
