using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTableLibrary.Extensions;

namespace TimeTableLibrary.Client
{
	public abstract class AbstractClient
	{
		protected HttpClient client;
		protected HttpRequestMessage message;
		protected Dictionary<string, string> Timetable;
		protected Dictionary<string, string> headers;

		public AbstractClient()
		{
			client = new HttpClient(new HttpClientHandler()
			{
				UseCookies = true,
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; },
			});
			message = new HttpRequestMessage();
			Timetable = new Dictionary<string, string>();
			headers = new Dictionary<string, string>();
		} 

		protected void SetHeaders()
		{
			foreach (var item in headers)
				message.Headers.AddIfNotExists(item.Key, item.Value);
		}
	}
}
