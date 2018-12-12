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

namespace TimeTableLibrary.FpmRequests
{
	public class FpmClient : AbstractClient, IFpmClient
	{
		private string sessionId;
		private FpmUser currentUser;
		public List<FpmGroup> Groups { get; set; }
		public List<FpmSubject> Subjects { get; set; }
		public List<FpmTeacher> Teachers { get; set; }
		private const string SUBJECTS_TEACHERS_FORM_NAME = "scheduler_groupToSubjectsForm";
		public FpmClient() : base()
		{
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

		public async Task Login(FpmUser user)
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
				{"login", user.Login },
				{"password",user.Password }
			});
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

		#region HelperMethods
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

		public Task SetTeacherToSubject(FpmTeacher teacher, FpmSubject subject)
		{
			throw new NotImplementedException();
		}
		#endregion HelperMethods
	}
}
