using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimetableMapper.Client;
using TimetableMapper.FpmModels;
using HtmlAgilityPack;

namespace TimetableMapper.FpmRequests
{
    public class FpmClient:AbstractClient
    {
        private string sessionId;
        private List<Group> groups;
        private List<Subject> subjects;
        private List<Teacher> teachers;
        public FpmClient():base()
        {
            groups = new List<Group>();
            subjects = new List<Subject>();
            teachers = new List<Teacher>();
            headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            headers.Add("Accept-Encoding", "gzip, deflate");
            headers.Add("Accept-Language", "uk-UA,uk;q=0.9,ru;q=0.8,en-US;q=0.7,en;q=0.6");
            headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
        }

        public async Task InitRequest()
        {
            message.RequestUri = new Uri("http://fpm.kpi.ua/");
            message.Method = HttpMethod.Get;
            var response = await client.SendAsync(message);
            sessionId = message.RequestUri.ToString().Split('=').Last();
            headers.Add("Cookie", $"JSESSIONID={sessionId}");
        }

        public async Task Login()
        {
            message = new HttpRequestMessage();
            SetHeaders(message);
            message.RequestUri = new Uri("https://fpm.kpi.ua/login.do");
            message.Method = HttpMethod.Post;
            message.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"login", "leo" },
                {"password","leoleo" }
            });
            var response = await client.SendAsync(message);
        }

        public async Task SelectSubjectToGroup()
        {
            message = new HttpRequestMessage(HttpMethod.Get, "https://fpm.kpi.ua/scheduler/groups/dependence/get_subjects.do");
            SetHeaders(message);
            var response = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
            var document = new HtmlDocument();
            document.LoadHtml(response);
            var options = document.DocumentNode.SelectNodes("//option").Skip(1);
            foreach(var option in options)
            {
                groups.Add(new Group()
                {
                    Name = option.InnerText,
                    Id = option.Attributes["value"].Value
                });
            }
        }
    }
}
