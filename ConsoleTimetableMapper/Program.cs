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

namespace ConsoleTimetableMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            /*RozkladClient client = new RozkladClient();
            var result = client.GetTimetable().Result;
            foreach(var item in result[0])
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            foreach (var item in result[1])
            {
                Console.WriteLine(item);
            }*/
            FpmClient client = new FpmClient();
            client.InitRequest().Wait();
            client.Login().Wait();
            client.SelectSubjectToGroup().Wait();
        }

       
    }
}
