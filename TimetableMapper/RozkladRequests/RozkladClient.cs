using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TimetableMapper.RozkladModels;
using TimetableMapper.RozkladModels.Enums;

namespace TimetableMapper.RozkladRequests
{
    public class RozkladClient
    {
        private HttpClient client;
        private HttpRequestMessage message;
        private Dictionary<string, string> getTimetableBodyDictionary;
        private Dictionary<string, string> headers;
        private const string FIRST_WEEK = "ctl00_MainContent_FirstScheduleTable";
        private const string SECOND_WEEK = "ctl00_MainContent_SecondScheduleTable";
        public RozkladClient()
        {
            client = new HttpClient(new HttpClientHandler() { UseCookies = false });
            message = new HttpRequestMessage();
            getTimetableBodyDictionary = new Dictionary<string, string>();
            headers = new Dictionary<string, string>();
            //headers initialization
            headers.Add("Host", "rozklad.kpi.ua");
            headers.Add("Connection", "keep-alive");
            headers.Add("Origin", "http://rozklad.kpi.ua");
            headers.Add("X-Requested-With", "XMLHttpRequest");
            headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
            headers.Add("Accept", @"*/*");
            headers.Add("Accept-Encoding", "gzip, deflate");
            headers.Add("Accept-Language", "gen-US,en;q=0.9");
            headers.Add("Cookie", "_ga=GA1.2.826275426.1517305737");
            
        }

        private void SetHeaders(HttpRequestMessage message)
        {
            foreach (var item in headers)
                message.Headers.Add(item.Key, item.Value);
        }

        private List<Lesson> ParseTable(HtmlNode table,bool firstWeek)
        {
            var result = new List<Lesson>();
            var trs = table.ChildNodes.Skip(2).Take(5).ToList();
            int lessonNumber = 1;
            foreach(var tr in trs)
            {
                var tds = tr.ChildNodes.Skip(2).Take(6).ToList();
                for(int i=0;i<tds.Count;i++)
                {
                    if (String.IsNullOrWhiteSpace(tds[i].InnerText))
                        continue;
                    Lesson tempLesson = new Lesson()
                    {
                        Day = (Day)i,
                        FirstWeek = firstWeek,
                        LessonNumber = (LessonNumber)lessonNumber,
                        LessonType = tds[i].LastChild.InnerText,
                        Subject = new Subject() { Name = tds[i].ChildNodes[0].ChildNodes[0].InnerText },
                        Teacher = new Teacher() { Name = tds[i].ChildNodes[2].InnerText }
                    };
                    result.Add(tempLesson);
                }
                lessonNumber++;
            }
            return result;

        }

        private async Task InitRequest()
        {
            SetHeaders(message);
            message.Headers.Add("Referer", "http://rozklad.kpi.ua");
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("http://rozklad.kpi.ua/Schedules/ScheduleGroupSelection.aspx");
            var document = new HtmlDocument();
            var html = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
            document.LoadHtml(html);
            var inputs = document.DocumentNode.SelectNodes(@"//input[@type='hidden']");
            foreach(var item in inputs)
            {
                getTimetableBodyDictionary.Add(item.Attributes["name"].Value, item.Attributes["value"].Value);
            }
        }



        public async Task<List<Lesson>[]> GetTimetable()
        {
            await InitRequest();
            //Group selection
            getTimetableBodyDictionary.Add("ctl00$MainContent$ctl00$txtboxGroup", "КП-51");
            message = new HttpRequestMessage();
            SetHeaders(message);
            message.Headers.Add("Referer", "http://rozklad.kpi.ua/Schedules/ScheduleGroupSelection.aspx");
            message.Method = HttpMethod.Post;
            message.RequestUri = new Uri("http://rozklad.kpi.ua/Schedules/ScheduleGroupSelection.aspx");
            getTimetableBodyDictionary.Add("ctl00$MainContent$ctl00$btnShowSchedule", "Розклад занять");
            message.Content = new FormUrlEncodedContent(getTimetableBodyDictionary);
            var response = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
            var document = new HtmlDocument();
            document.LoadHtml(response);
            var result = new List<Lesson>[2];
            result[0] = ParseTable(document.DocumentNode.SelectSingleNode($@"//table[@id='{FIRST_WEEK}']"),true);
            result[1] = ParseTable(document.DocumentNode.SelectSingleNode($@"//table[@id='{SECOND_WEEK}']"),false);
            return result;
        }
    }
}
