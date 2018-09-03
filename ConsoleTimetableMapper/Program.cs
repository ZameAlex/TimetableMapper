using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableMapper.RozkladModels;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using TimetableMapper.RozkladRequests;
using TimetableMapper.FpmRequests;
using TimetableMapper.Mappers;

namespace ConsoleTimetableMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            string groupName = "КВ-83мн";
            RozkladClient client = new RozkladClient(groupName);
            var result = client.GetTimetable().Result;
            foreach(var item in result[0])
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            foreach (var item in result[1])
            {
                Console.WriteLine(item);
            }
            var rzkSubjects = result[0].Select(r => r.Subject).Union(result[1].Select(r => r.Subject)).ToList();
            //var rzkTeachers = result[0].Select(r => r.Teacher).Union(result[1].Select(r => r.Teacher)).ToList();
            FpmClient fpmClient = new FpmClient();
            fpmClient.InitRequest().Wait();
            fpmClient.Login().Wait();
            fpmClient.SelectSubjectToGroup().Wait();
            SubjectsMapper subjectsMapper = new SubjectsMapper();
            TeachersMapper teachersMapper = new TeachersMapper();
            var mappedSubj = subjectsMapper.Map(fpmClient.Subjects, rzkSubjects);
            var resultSubjects = mappedSubj.Select(sbj => sbj.Value.FirstOrDefault()).ToList();
            var group = fpmClient.Groups.Where(g => g.Name == groupName).First();
            fpmClient.SetSubjectToGroup(group, resultSubjects).Wait();
            //var mappedTeach = teachersMapper.Map(fpmClient.Teachers, rzkTeachers);
        }

       
    }
}
