using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableMapper.FpmModels;
using TimetableMapper.RozkladModels;

namespace TimetableMapper.Mappers
{
    public class SubjectsMapper
    {
        public SubjectsMapper()
        {

        }

        private class SubjectComparer : IEqualityComparer<RozkladModels.Subject>
        {
            public bool Equals(RozkladModels.Subject x, RozkladModels.Subject y)
            {
                if (x.Name == y.Name)
                    return true;
                return false;
            }

            public int GetHashCode(RozkladModels.Subject obj)
            {
                return obj.Name.GetHashCode();
            }
        }

        private List<FpmModels.Subject> MapFpmSubjects(string[] rzkSubj, string[]rzkSubjTitle, List<FpmModels.Subject> fpmSbj)
        {
            var result = new List<FpmModels.Subject>();
            var count = fpmSbj.Count;
            var wordNumber = 0;
            var temp = fpmSbj;
            while (count > 0 && wordNumber<rzkSubj.Length)
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
            if(result.Count==0)
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

        public Dictionary<string, List<FpmModels.Subject>> Map(List<FpmModels.Subject> fpmSbj, List<RozkladModels.Subject> rzkSbj)
        {
            var result = new Dictionary<string, List<FpmModels.Subject>>();
            var distinctSubjects = rzkSbj.Distinct(new SubjectComparer());
            foreach(var subject in distinctSubjects)
            {
                result.Add(subject.Name, MapFpmSubjects(subject.Name.Split(' '),subject.Title.Split(' '), fpmSbj));
            }
            return result;
        }
    }
}
