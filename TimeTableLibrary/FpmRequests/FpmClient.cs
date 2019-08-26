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
using TimeTableLibrary.Extensions;
using System.Net.Security;
using TimeTableLibrary.RozkladModels;

namespace TimeTableLibrary.FpmRequests
{
	public class FpmClient : AbstractClient
	{
		private string _sessionId;

		private const string GroupTeachersFormName = "scheduler_groupToSubjectsForm";
		private const string SubjectTeacherFormName = "scheduler_teacherToSubjectsForm";

		private static FpmClient _instance;

		public static FpmClient Instance
		{
			get { return _instance ?? (_instance = new FpmClient()); }
		}

		public List<FpmGroup> Groups { get; set; }
		public List<FpmSubject> Subjects { get; set; }
		public List<FpmTeacher> Teachers { get; set; }
		public FpmGroup CurrentGroup { get; set; }
		public FpmUser User { get; set; }

		private FpmClient() : base()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			Groups = new List<FpmGroup>();
			Subjects = new List<FpmSubject>();
			Teachers = new List<FpmTeacher>();
			headers.AddIfNotExists("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
			headers.AddIfNotExists("Accept-Encoding", "gzip, deflate");
			headers.AddIfNotExists("Accept-Language", "uk-UA,uk;q=0.9,ru;q=0.8,en-US;q=0.7,en;q=0.6");
			headers.AddIfNotExists("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
		}

		#region GetRequests
		public async Task InitRequest()
		{
			message.RequestUri = new Uri("http://fpm.kpi.ua/");
			message.Method = HttpMethod.Get;
			try
			{
				var response = await client.SendAsync(message);
			}
			catch (InvalidOperationException e)
			{
				return;
			}
			_sessionId = message.RequestUri.ToString().Split('=').Last();
			headers.AddIfNotExists("Cookie", $"JSESSIONID={_sessionId}");
		}


		public async Task Login()
		{
			DisableSafetySertificate();
			message = new HttpRequestMessage();
			SetHeaders();
			message.RequestUri = new Uri("https://fpm.kpi.ua/login.do");
			message.Method = HttpMethod.Post;
			message.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
			{
				{"login", User.Login },
				{"password",User.Password }
			});
			Encoding.GetEncoding("windows-1251");
			try
			{
				var response = await client.SendAsync(message);
			}
			catch (InvalidOperationException e)
			{
				return;
			}
		}


		public async Task SelectTeachers()
		{
			message = new HttpRequestMessage(HttpMethod.Get, "https://fpm.kpi.ua/scheduler/teachers/dependence/get_subjects.do");
			SetHeaders();
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

		public async Task SelectSubjectsAndGroups()
		{
			message = new HttpRequestMessage(HttpMethod.Get, "https://fpm.kpi.ua/scheduler/groups/dependence/get_subjects.do");
			SetHeaders();
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
			Subjects = FormParsing(document.DocumentNode.SelectSingleNode($"//form[@name='{GroupTeachersFormName}']"));
		}

		#endregion GetRequests

		#region PostRequest

		public async Task SetDependencies<T>(T dependency, List<FpmSubject> objects)
		{
			List<KeyValuePair<string, string>> ids = new List<KeyValuePair<string, string>>();
			var subjects = await PreSetDependenciesRequests<T>(dependency);
			if (dependency is FpmGroup)
			{
				message = new HttpRequestMessage(HttpMethod.Post, "https://fpm.kpi.ua/scheduler/groups/dependence/save_subjects.do");
				ids.AddIfNotExists(GetObjectForSetRequest<FpmGroup>(dependency as FpmGroup));
			}
			else
			{
				message = new HttpRequestMessage(HttpMethod.Post, "https://fpm.kpi.ua/scheduler/teachers/dependence/save_subjects.do");
				ids.AddIfNotExists(GetObjectForSetRequest<FpmTeacher>(dependency as FpmTeacher));
			}
			SetHeaders();
			foreach (var item in objects)
			{
				ids.AddIfNotExists(new KeyValuePair<string, string>("ids", item.Id));
			}
			foreach (var item in subjects)
			{
				ids.AddIfNotExists(new KeyValuePair<string, string>("ids", item));
			}
			message.Content = new FormUrlEncodedContent(ids);
			message.Headers.AddIfNotExists("Referer", "https://fpm.kpi.ua/scheduler/groups/dependence/get_subjects.do");
			var response = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
			message = new HttpRequestMessage(HttpMethod.Get, "https://fpm.kpi.ua/_includes/blank_page.jsp");
			await client.SendAsync(message);
		}

		public async Task TimetableClearRequest()
		{
			PreChangingTimetableRequests();
			message = new HttpRequestMessage(HttpMethod.Post, "https://fpm.kpi.ua/savescheduler");
			Dictionary<string, string> ids = new Dictionary<string, string>();
			for (int j = 0; j < 6; j++)
			{
				for (int k = 0; k < 6; k++)
				{
					for (int i = 1; i < 3; i++)
					{
						ids.AddIfNotExists($"subject_{i}_{j}_{k}", "");
						ids.AddIfNotExists($"room_{i}_{j}_{k}", "");
					}
				}
			}
			message.Content = new FormUrlEncodedContent(ids);
			SetHeaders();
			message.Headers.AddIfNotExists("Referer", $"https://fpm.kpi.ua/scheduler/edit.do?id=" + CurrentGroup.Id);
			Encoding.GetEncoding("windows-1251");
			var response = await client.SendAsync(message);
		}

		public async Task SetTimetableRequest(List<ResultLesson> lessons, Helpers.MappingService checker)
		{
			PreChangingTimetableRequests();
			message = new HttpRequestMessage(HttpMethod.Post, "https://fpm.kpi.ua/savescheduler");
			SetHeaders();
			Dictionary<string, string> ids = new Dictionary<string, string>();
			foreach (var item in lessons)
			{
				if (item.Flasher)
				{
					if (item.FirstWeekLesson != null)
					{
						ids.Add(GetLesson(true, checker, item));
						ids.Add(GetTeacher(true, checker, item));
						ids.Add(GetRoom(true, checker, item));
					}
					ids.AddIfNotExists($"week_{(int)item.LessonNumber - 1}_{(int)item.DayOfWeek}", "on");
					if (item.SecondWeekLesson != null)
					{
						ids.Add(GetLesson(false, checker, item));
						ids.Add(GetTeacher(false, checker, item));
						ids.Add(GetRoom(false, checker, item));
					}
				}
				else if (item.FirstWeekLesson == item.SecondWeekLesson && item.FirstWeekLesson != null)
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

		#region CreatePostObjects
		private Tuple<string, string> GetLesson(bool first, Helpers.MappingService checker, ResultLesson lesson)
		{
			if (first)
				return new Tuple<string, string>($"subject_1_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", checker.Subjects[checker.Subjects.Keys.Single(t => t == lesson.FirstWeekLesson.Subject.Title)].Id);
			else
				return new Tuple<string, string>($"subject_2_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", checker.Subjects[checker.Subjects.Keys.Single(t => t == lesson.SecondWeekLesson.Subject.Title)].Id);
		}

		private Tuple<string, string> GetRoom(bool first, Helpers.MappingService checker, ResultLesson lesson)
		{
			if (first)
				return new Tuple<string, string>($"room_1_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", lesson.FirstWeekLesson.LessonTypeAndRoom);
			else
				return new Tuple<string, string>($"room_2_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", lesson.SecondWeekLesson.LessonTypeAndRoom);
		}

		private Tuple<string, string> GetTeacher(bool first, Helpers.MappingService checker, ResultLesson lesson)
		{
			if (first)
				return new Tuple<string, string>($"teacher_1_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", checker.Teachers[checker.Teachers.Keys.Single(t => t == lesson.FirstWeekLesson.Teacher.Name)].Id);
			else
				return new Tuple<string, string>($"teacher_2_{(int)lesson.LessonNumber - 1}_{(int)lesson.DayOfWeek}", checker.Teachers[checker.Teachers.Keys.Single(t => t == lesson.SecondWeekLesson.Teacher.Name)].Id);
		}

		private KeyValuePair<string, string> GetObjectForSetRequest<T>(T dependency)
		{
			if (dependency is FpmGroup)
				return new KeyValuePair<string, string>("group_id", (dependency as FpmGroup).Id);
			else
				return new KeyValuePair<string, string>("teacher_id", (dependency as FpmTeacher).Id);
		}
		#endregion CreatePostObjects


		#region RequestMethods

		public void PreChangingTimetableRequests()
		{
			message = new HttpRequestMessage(HttpMethod.Post, new Uri("http://fpm.kpi.ua/scheduler/group.do"));
			SetHeaders();
			message.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
			{
				{"type", "group" },
				{"id",CurrentGroup.Id }
			});
			client.SendAsync(message).Result.Content.ReadAsStringAsync();
			message = new HttpRequestMessage(HttpMethod.Get, new Uri("http://fpm.kpi.ua/scheduler/edit.do?id=" + CurrentGroup.Id));
			SetHeaders();
			client.SendAsync(message).Result.Content.ReadAsStringAsync();
		}

		private async Task<List<string>> PreSetDependenciesRequests<T>(T dependency)
		{

			SetHeaders();
			Dictionary<string, string> ids = new Dictionary<string, string>();
			if (dependency is FpmGroup)
			{
				message = new HttpRequestMessage(HttpMethod.Post, "https://fpm.kpi.ua/scheduler/groups/dependence/get_subjects.do");
				SetHeaders();
				message.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
				{
					GetObjectForSetRequest<FpmGroup>(dependency as FpmGroup)
				});
				var response = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
				message = new HttpRequestMessage(HttpMethod.Get, "https://fpm.kpi.ua/_includes/blank_page.jsp");
				await client.SendAsync(message);
				return new List<string>();
			}
			else
			{
				message = new HttpRequestMessage(HttpMethod.Post, "https://fpm.kpi.ua/scheduler/teachers/dependence/get_subjects.do");
				SetHeaders();
				message.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
				{
					GetObjectForSetRequest<FpmTeacher>(dependency as FpmTeacher)
				});
				var response = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
				message = new HttpRequestMessage(HttpMethod.Get, "https://fpm.kpi.ua/_includes/blank_page.jsp");
				await client.SendAsync(message);
				return ParseCheckedCheckboxes(response);
			}


		}

		#endregion RequestMethods

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

		private List<string> ParseCheckedCheckboxes(string html)
		{
			var result = new List<string>();
			var index = html.IndexOf("t_subj_ids");
			var i = index;
			for (; html[i] != '\n'; i++)
			{
			}
			var ids = html.Substring(index, i - index).Split('\'');
			foreach (var item in ids)
			{
				if (item.IndexOf("sbj_") != -1)
					result.Add(item);
			}
			return result;
		}

		private void DisableSafetySertificate()
		{
			ServicePointManager.ServerCertificateValidationCallback =
				delegate (
					object s,
					X509Certificate certificate,
					X509Chain chain,
					SslPolicyErrors sslPolicyErrors
				)
				{
					return true;
				};

		}

		#endregion HelperMethods
	}
}
