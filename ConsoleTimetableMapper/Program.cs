using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableMapper.RozkladModels;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using TimetableMapper.RozkladRequests;

namespace ConsoleTimetableMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Lesson[] FirstWeek = new Lesson[7];
            Lesson[] SecondWeek = new Lesson[7];
            RozkladClient client = new RozkladClient();
            client.InitRequest().Wait();
        }

       
    }
}
