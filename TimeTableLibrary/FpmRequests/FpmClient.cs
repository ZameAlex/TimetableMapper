using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTableLibrary.Client;
using TimeTableLibrary.FpmModels;
using HtmlAgilityPack;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using TimeTableLibrary.RozkladModels;

namespace TimeTableLibrary.FpmRequests
{
	public class FpmClient : AbstractClient//, IFpmClient
	{
		private string sessionId;
		private FpmUser currentUser;

		private const string SUBJECTS_TEACHERS_FORM_NAME = "scheduler_groupToSubjectsForm";

		public List<FpmGroup> Groups { get; set; }
		public List<FpmSubject> Subjects { get; set; }
		public List<FpmTeacher> Teachers { get; set; }
		public FpmGroup CurrentGroup { get; set; }
		public FpmUser User { get; set; }

		public FpmClient() : base()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			Groups = new List<FpmGroup>();
			Subjects = new List<FpmSubject>();
			Teachers = new List<FpmTeacher>();
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
			//disable safety certificate
			ServicePointManager.ServerCertificateValidationCallback =
				delegate (
					object s,
					X509Certificate certificate,
					X509Chain chain,
					SslPolicyErrors sslPolicyErrors
				) {
					return true;
				};

			message = new HttpRequestMessage();
			SetHeaders(message);
			message.RequestUri = new Uri("https://fpm.kpi.ua/login.do");
			message.Method = HttpMethod.Post;
			message.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
			{
				{"login", User.Login },
				{"password",User.Password }
			});
			Encoding.GetEncoding("windows-1251");
			var response = await client.SendAsync(message);
		}

		public async Task GetTeachers()
		{
			message = new HttpRequestMessage(HttpMethod.Get, "https://fpm.kpi.ua/scheduler/teachers/dependence/get_subjects.do");
			SetHeaders(message);
			var response = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
			var document = new HtmlDocument();
			document.LoadHtml(response);
			var options = document.DocumentNode.SelectNodes("//option").Skip(1);
			foreach (var option in options)
			{
				Teachers.Add(new FpmTeacher
				{
					Name = option.InnerText.ToString(),
					Id = option.Attributes["value"].Value.ToString()
				});
			}
		}

		public async Task GetSubjectsAndGroups()
		{
			message = new HttpRequestMessage(HttpMethod.Get, "https://fpm.kpi.ua/scheduler/groups/dependence/get_subjects.do");
			SetHeaders(message);
			var response = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
			var document = new HtmlDocument();
			document.LoadHtml(response);
			var options = document.DocumentNode.SelectNodes("//option").Skip(1);
			foreach (var option in options)
			{
				Groups.Add(new FpmGroup()
				{
					Name = option.InnerText.ToString(),
					Id = option.Attributes["value"].Value.ToString()
				});
			}
			Subjects = FormParsing(document.DocumentNode.SelectSingleNode($"//form[@name='{SUBJECTS_TEACHERS_FORM_NAME}']"));
		}

		#region PostRequest
		public async Task ClearRequest()
		{
			message = new HttpRequestMessage(HttpMethod.Post, new Uri("http://fpm.kpi.ua/scheduler/group.do"));
			SetHeaders(message);
			message.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
			{
				{"type", "group" },
				{"id",CurrentGroup.Id }
			});
			var response2 = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
			message = new HttpRequestMessage(HttpMethod.Get, new Uri("http://fpm.kpi.ua/scheduler/edit.do?id="+CurrentGroup.Id));
			SetHeaders(message);
			var response1 = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
			message = new HttpRequestMessage(HttpMethod.Post, "https://fpm.kpi.ua/savescheduler");
			List<KeyValuePair<string, string>> ids = new List<KeyValuePair<string, string>>();
			for (int j = 0; j < 6; j++)
			{
				for (int k = 0; k < 6; k++)
				{
					for (int i = 1; i < 3; i++)
					{
						ids.Add(new KeyValuePair<string, string>($"subject_{i}_{j}_{k}", ""));
						ids.Add(new KeyValuePair<string, string>($"room_{i}_{j}_{k}", ""));
					}
				}
			}
			message.Content = new FormUrlEncodedContent(ids);
			SetHeaders(message);
			message.Headers.Add("Referer", $"http://fpm.kpi.ua/scheduler/edit.do?id="+CurrentGroup.Id);
			Encoding.GetEncoding("windows-1251");
			var response = await client.SendAsync(message);
		}

		public async Task SetRequest(List<ResultLesson> lessons, CsvHelpers.CsvChecker checker)
		{
			message = new HttpRequestMessage(HttpMethod.Post, new Uri("http://fpm.kpi.ua/scheduler/group.do"));
			SetHeaders(message);
			message.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
			{
				{"type", "group" },
				{"id",CurrentGroup.Id }
			});
			var response2 = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
			message = new HttpRequestMessage(HttpMethod.Get, new Uri("http://fpm.kpi.ua/scheduler/edit.do?id=" + CurrentGroup.Id));
			SetHeaders(message);
			var response1 = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
			message = new HttpRequestMessage(HttpMethod.Post, "https://fpm.kpi.ua/savescheduler");
			SetHeaders(message);
			List<KeyValuePair<string, string>> ids = new List<KeyValuePair<string, string>>();
			//for (int j = 0; j < 6; j++)
			//{
			//	for (int k = 0; k < 6; k++)
			//	{
			//		for (int i = 1; i < 3; i++)
			//		{
			//			ids.Add(new KeyValuePair<string, string>($"subject_{i}_{j}_{k}", ""));
			//			ids.Add(new KeyValuePair<string, string>($"room_{i}_{j}_{k}", ""));
			//		}
			//	}
			//}
			foreach (var item in lessons)
			{
				//for (int j = 0; j < (int)item.LessonNumber-1; j++)
				//{
				//	for (int k = 0; k <= (int)item.DayOfWeek; k++)
				//	{
				//		for (int i = 1; i < 3; i++)
				//		{
				//		if()
				//			ids.Add(new KeyValuePair<string, string>($"subject_{i}_{j}_{k}", ""));
				//			ids.Add(new KeyValuePair<string, string>($"room_{i}_{j}_{k}", ""));
				//		}
				//	}
				//}
				if (item.Flasher)
				{
					if(item.FirstWeekLesson!=null)
					{
						ids.Add(GetLesson(true, checker, item));
						ids.Add(GetTeacher(true, checker, item));
						ids.Add(GetRoom(true, checker, item));
					}
					ids.Add(new KeyValuePair<string, string>($"week_{(int)item.LessonNumber - 1}_{(int)item.DayOfWeek}", "on"));
					if(item.SecondWeekLesson!=null)
					{
						ids.Add(GetLesson(false, checker, item));
						ids.Add(GetTeacher(false, checker, item));
						ids.Add(GetRoom(false, checker, item));
					}
				}
				else if(item.FirstWeekLesson==item.SecondWeekLesson && item.FirstWeekLesson != null)
				{
						ids.Add(GetLesson(true, checker, item));
						ids.Add(GetTeacher(true, checker, item));
						ids.Add(GetRoom(true, checker, item));
				}
			}
			Encoding.GetEncoding("windows-1251");
			message.Content = new FormUrlEncodedContent(ids);
			var response = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
		}
		#endregion PostRequest

		#region HelperMethods

		public KeyValuePair<string, string> GetLesson(bool first, CsvHelpers.CsvChecker checker, ResultLesson lesson)
		{
			if (first)
				return new KeyValuePair<string, string>($"subject_1_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", checker.Subjects[checker.Subjects.Keys.Single(t => t.Title == lesson.FirstWeekLesson.Subject.Title)].Id);
			else
				return new KeyValuePair<string, string>($"subject_2_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", checker.Subjects[checker.Subjects.Keys.Single(t => t.Title == lesson.SecondWeekLesson.Subject.Title)].Id);
		}

		public KeyValuePair<string, string> GetRoom(bool first, CsvHelpers.CsvChecker checker, ResultLesson lesson)
		{
			if (first)
				return new KeyValuePair<string, string>($"room_1_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", lesson.FirstWeekLesson.LessonTypeAndRoom);
			else
				return new KeyValuePair<string, string>($"room_2_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", lesson.SecondWeekLesson.LessonTypeAndRoom);
		}

		public KeyValuePair<string, string> GetTeacher(bool first, CsvHelpers.CsvChecker checker, ResultLesson lesson)
		{
			if (first)
				return new KeyValuePair<string, string>($"teacher_1_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", checker.Teachers[checker.Teachers.Keys.Single(t => t.Name == lesson.FirstWeekLesson.Teacher.Name)].Id);
			else
				return new KeyValuePair<string, string>($"teacher_2_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", checker.Teachers[checker.Teachers.Keys.Single(t => t.Name == lesson.SecondWeekLesson.Teacher.Name)].Id);
		}

		public async Task SetSubjectsToGroup(FpmGroup group, List<FpmSubject> subjects)
		{
			message = new HttpRequestMessage(HttpMethod.Post, "https://fpm.kpi.ua/scheduler/groups/dependence/save_subjects.do");
			SetHeaders(message);
			List<KeyValuePair<string, string>> ids = new List<KeyValuePair<string, string>>()
			{
				new KeyValuePair<string, string>("group_id",group.Id)
			};
			foreach (var item in subjects)
			{
				if (item != null)
					ids.Add(new KeyValuePair<string, string>("ids", item.Id));
			}
			message.Content = new FormUrlEncodedContent(ids);
			var response = await client.SendAsync(message);
		}

		private List<FpmSubject> FormParsing(HtmlNode form)
		{
			var result = new List<FpmSubject>();
			var table = form.Element("table");
			var trs = table.Elements("tr").Skip(2).ToList();
			trs.RemoveAt(trs.Count - 1);
			foreach (var tr in trs)
			{
				var childNodes = tr.Elements("td").ToList();
				var tempSubject = new FpmSubject()
				{
					Id = childNodes.First().ChildNodes[1].Attributes["value"].Value,
					Name = childNodes.Last().ChildNodes[1].InnerText.Replace("&#39;", $"{(char)8216}")
				};
				result.Add(tempSubject);
			}
			return result;
		}

		public async Task SetTeacherToSubjectAsync(FpmTeacher teacher, List<FpmSubject> subjects)
		{
			message = new HttpRequestMessage(HttpMethod.Post, "https://fpm.kpi.ua/scheduler/teachers/dependence/save_subjects.do");
			SetHeaders(message);
			List<KeyValuePair<string, string>> ids = new List<KeyValuePair<string, string>>()
			{
				new KeyValuePair<string, string>("teacher_id",teacher.Id)
			};
			foreach (var item in subjects)
			{
				if (item != null)
					ids.Add(new KeyValuePair<string, string>("ids", item.Id));
			}
			message.Content = new FormUrlEncodedContent(ids);
			var response = await client.SendAsync(message);
		}
		#endregion HelperMethods
	}
}
