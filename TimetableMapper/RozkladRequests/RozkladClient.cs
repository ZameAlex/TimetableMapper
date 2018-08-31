using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TimetableMapper.RozkladRequests
{
    public class RozkladClient
    {
        private HttpClient client;
        private HttpRequestMessage message;
        private Dictionary<string, string> getTimetableBodyDictionary;
        public RozkladClient()
        {
            client = new HttpClient(new HttpClientHandler() { UseCookies = false });
            message = new HttpRequestMessage();
            message.Headers.Add("Host", "rozklad.kpi.ua");
            message.Headers.Add("Connection", "keep-alive");
            message.Headers.Add("Origin", "http://rozklad.kpi.ua");
            message.Headers.Add("X-Requested-With", "XMLHttpRequest");
            message.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
            message.Headers.Add("Accept", @"*/*");
            message.Headers.Add("Referer", "http://rozklad.kpi.ua");
            message.Headers.Add("Accept-Encoding", "gzip, deflate");
            message.Headers.Add("Accept-Language", "gen-US,en;q=0.9");
            message.Headers.Add("Cookie", "_ga=GA1.2.826275426.1517305737");
            getTimetableBodyDictionary = new Dictionary<string, string>();
        }

        public async Task InitRequest()
        {
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri("http://rozklad.kpi.ua/");
            var document = new HtmlDocument();
            var html = await client.SendAsync(message).Result.Content.ReadAsStringAsync();
            document.LoadHtml(html);
            var inputs = document.DocumentNode.SelectNodes(@"//input[@type='hidden']");

        }
    }
}
