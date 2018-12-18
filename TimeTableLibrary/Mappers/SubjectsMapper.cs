using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.RozkladModels;
using CsvHelper;
using System.IO;

namespace TimeTableLibrary.Mappers
{
	public class SubjectsMapper:IElementsMapper<RozkladSubject, FpmSubject>
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

		private List<FpmSubject> MapFpmSubjects(string[] rzkSubj, string[] rzkSubjTitle, List<FpmSubject> fpmSbj)
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

		public Dictionary<RozkladSubject, List<FpmSubject>> Map(List<FpmModels.FpmSubject> fpmElements, List<RozkladModels.RozkladSubject> rozkladElements)
		{
			var result = new Dictionary<RozkladSubject, List<FpmModels.FpmSubject>>();
			var distinctSubjects = rozkladElements.Distinct(new SubjectComparer());
			foreach (var subject in distinctSubjects)
			{
				result.Add(subject, MapFpmSubjects(subject.Name.Split(' '), subject.Title.Split(' '), fpmElements));
			}
			return result;
		}

		public void WriteNewMapping(Dictionary<RozkladSubject, List<FpmSubject>> values)
		{
			CsvWriter writer = new CsvWriter(new StreamWriter("mapingSbj.csv"));
			
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
