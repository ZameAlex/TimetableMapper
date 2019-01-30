using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TimeTableLibrary.Client;
using TimeTableLibrary.RozkladModels;
using TimeTableLibrary.Enums;
using ConsoleTimetableMapper;
using System.Diagnostics;
using TimeTableLibrary.Extensions;

namespace TimeTableLibrary.RozkladRequests
{
	public class RozkladClient : AbstractClient
	{
		private const string FIRST_WEEK = "ctl00_MainContent_FirstScheduleTable";
		private const string SECOND_WEEK = "ctl00_MainContent_SecondScheduleTable";
		public string Group { get; set; }
		public List<RozkladLesson>[] rozkladTimeTable {get;set;}
		public List<RozkladSubject> Subjects { get; set; }
		public List<RozkladTeacher> Teachers { get; set; }

		#region GetRequests
		public RozkladClient()
		{
			//var observer = new ExampleDiagnosticObserver();
			//IDisposable subscription = DiagnosticListener.AllListeners.Subscribe(observer);
			client = new HttpClient(new HttpClientHandler() { UseCookies = false });
			message = new HttpRequestMessage();
			Timetable = new Dictionary<string, string>();
			headers = new Dictionary<string, string>();
			Teachers = new List<RozkladTeacher>();
			Subjects = new List<RozkladSubject>();
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

		private async Task InitRequest()
		{
			SetHeaders();
			message.Headers.AddIfNotExists("Referer", "http://rozklad.kpi.ua");
			message.Method = HttpMethod.Get;
			message.RequestUri = new Uri("http://rozklad.kpi.ua/Schedules/ScheduleGroupSelection.aspx");
			var document = new HtmlDocument();
			var html = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
			document.LoadHtml(html);
			var inputs = document.DocumentNode.SelectNodes(@"//input[@type='hidden']");
			foreach (var item in inputs)
			{
				Timetable.AddIfNotExists(item.Attributes["name"].Value, item.Attributes["value"].Value);
			}
		}

		public async Task<List<RozkladLesson>[]> GetTimetable()
		{
			try
			{
				await InitRequest();
			}
			catch(Exception e)
			{

			}
			//Group selection
			Timetable.AddNew("ctl00$MainContent$ctl00$txtboxGroup", Group);
			message = new HttpRequestMessage();
			SetHeaders();
			message.Headers.AddIfNotExists("Referer", "http://rozklad.kpi.ua/Schedules/ScheduleGroupSelection.aspx");
			message.Method = HttpMethod.Post;
			message.RequestUri = new Uri("http://rozklad.kpi.ua/Schedules/ScheduleGroupSelection.aspx");
			Timetable.AddIfNotExists("ctl00$MainContent$ctl00$btnShowSchedule", "Розклад занять");
			message.Content = new FormUrlEncodedContent(Timetable);
			var response = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
			var document = new HtmlDocument();
			document.LoadHtml(response);
			var result = new List<RozkladLesson>[2];
			result[0] = ParseTable(document.DocumentNode.SelectSingleNode($@"//table[@id='{FIRST_WEEK}']"), true);
			result[1] = ParseTable(document.DocumentNode.SelectSingleNode($@"//table[@id='{SECOND_WEEK}']"), false);
			foreach(var item in result[0])
			{
				if (!Teachers.Exists(t => t.Name == item.Teacher.Name))
					Teachers.Add(item.Teacher);
				if (!Subjects.Exists(t => t.Name == item.Subject.Title))
					Subjects.Add(item.Subject);
			}
			foreach (var item in result[1])
			{
				if (!Teachers.Exists(t => t.Name == item.Teacher.Name))
					Teachers.Add(item.Teacher);
				if (!Subjects.Exists(t => t.Name == item.Subject.Title))
					Subjects.Add(item.Subject);
			}
			rozkladTimeTable = result;
			return result;
		}
		#endregion GetRequests

		#region HelperMethods
		private List<RozkladLesson> ParseTable(HtmlNode table, bool firstWeek)
		{
			var result = new List<RozkladLesson>();
			var trs = table.ChildNodes.Skip(2).Take(5).ToList();
			int lessonNumber = 1;
			foreach (var tr in trs)
			{
				var tds = tr.ChildNodes.Skip(2).Take(6).ToList();
				for (int i = 0; i < tds.Count; i++)
				{
					if (String.IsNullOrWhiteSpace(tds[i].InnerText))
						continue;
					RozkladLesson tempLesson = new RozkladLesson()
					{
						Day = (Day)i,
						FirstWeek = firstWeek,
						LessonNumber = (LessonNumber)lessonNumber,
						LessonTypeAndRoom = tds[i].LastChild.InnerText,
						Subject = new RozkladSubject()
						{
							Name = tds[i].ChildNodes[0].ChildNodes[0].InnerText.Replace((char)39, (char)8216),
							Title = tds[i].ChildNodes[0].ChildNodes[0].Attributes["title"].Value.Replace((char)39, (char)8216)
						},
						Teacher = new RozkladTeacher() { Name = tds[i].ChildNodes[2].InnerText.Replace((char)39, (char)8216) }
					};
					result.Add(tempLesson);
				}
				lessonNumber++;
			}
			return result;
		}

		public Dictionary<string,List<RozkladSubject>> GetTeachersBySubject()
		{
			var result = new Dictionary<string, List<RozkladSubject>>();
			foreach (var item in Teachers)
			{
				result.Add(item.Name, new List<RozkladSubject>());
			}
			foreach (var item in rozkladTimeTable[0])
			{
				if (!result[item.Teacher.Name].Contains(item.Subject))
					result[item.Teacher.Name].Add(item.Subject);
			}
			foreach (var item in rozkladTimeTable[1])
			{
				if (!result[item.Teacher.Name].Contains(item.Subject))
					result[item.Teacher.Name].Add(item.Subject);
			}
			return result;
		}
		#endregion
	}
}
