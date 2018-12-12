﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TimeTableLibrary.Client;
using TimeTableLibrary.RozkladModels;
using TimeTableLibrary.Enums;

namespace TimeTableLibrary.RozkladRequests
{
	public class RozkladClient : AbstractClient, IRozkladClient
	{
		private const string FIRST_WEEK = "ctl00_MainContent_FirstScheduleTable";
		private const string SECOND_WEEK = "ctl00_MainContent_SecondScheduleTable";
		private readonly string group;
		public RozkladClient(string group)
		{
			this.group = group;
			client = new HttpClient(new HttpClientHandler() { UseCookies = false });
			message = new HttpRequestMessage();
			Timetable = new Dictionary<string, string>();
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



		public async Task InitRequest()
		{
			SetHeaders(message);
			message.Headers.Add("Referer", "http://rozklad.kpi.ua");
			message.Method = HttpMethod.Get;
			message.RequestUri = new Uri("http://rozklad.kpi.ua/Schedules/ScheduleGroupSelection.aspx");
			var document = new HtmlDocument();
			var html = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
			document.LoadHtml(html);
			var inputs = document.DocumentNode.SelectNodes(@"//input[@type='hidden']");
			foreach (var item in inputs)
			{
				Timetable.Add(item.Attributes["name"].Value, item.Attributes["value"].Value);
			}
		}



		public async Task<List<RozkladLesson>[]> GetTimetable()
		{
			await InitRequest();
			//Group selection
			Timetable.Add("ctl00$MainContent$ctl00$txtboxGroup", group);
			message = new HttpRequestMessage();
			SetHeaders(message);
			message.Headers.Add("Referer", "http://rozklad.kpi.ua/Schedules/ScheduleGroupSelection.aspx");
			message.Method = HttpMethod.Post;
			message.RequestUri = new Uri("http://rozklad.kpi.ua/Schedules/ScheduleGroupSelection.aspx");
			Timetable.Add("ctl00$MainContent$ctl00$btnShowSchedule", "Розклад занять");
			message.Content = new FormUrlEncodedContent(Timetable);
			var response = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
			var document = new HtmlDocument();
			document.LoadHtml(response);
			var result = new List<RozkladLesson>[2];
			result[0] = ParseTable(document.DocumentNode.SelectSingleNode($@"//table[@id='{FIRST_WEEK}']"), true);
			result[1] = ParseTable(document.DocumentNode.SelectSingleNode($@"//table[@id='{SECOND_WEEK}']"), false);
			return result;
		}




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

		#endregion
	}
}