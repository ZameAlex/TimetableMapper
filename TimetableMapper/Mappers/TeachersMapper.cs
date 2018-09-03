using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableMapper.Mappers
{
    public class TeachersMapper
    {
        private class SubjectComparer : IEqualityComparer<RozkladModels.Teacher>
        {
            public bool Equals(RozkladModels.Teacher x, RozkladModels.Teacher y)
            {
                if (x.Name == y.Name)
                    return true;
                return false;
            }

            public int GetHashCode(RozkladModels.Teacher obj)
            {
                return obj.Name.GetHashCode();
            }
        }

        private List<FpmModels.Teacher> MapFpmSubjects(string[] rzkTch, List<FpmModels.Teacher> fpmTch)
        {
            var result = new List<FpmModels.Teacher>();
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

        public Dictionary<string, List<FpmModels.Teacher>> Map(List<FpmModels.Teacher> fpmTch, List<RozkladModels.Teacher> rzkTch)
        {
            var result = new Dictionary<string, List<FpmModels.Teacher>>();
            var distinctTeachers = rzkTch.Distinct(new SubjectComparer());
            foreach (var teacher in distinctTeachers)
            {
                result.Add(teacher.Name, MapFpmSubjects(teacher.Name.Split(' '), fpmTch));
            }
            return result;
        }
    }
}
