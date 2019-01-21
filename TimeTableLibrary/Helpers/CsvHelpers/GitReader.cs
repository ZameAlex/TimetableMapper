using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Octokit;

namespace TimeTableLibrary.CsvHelpers
{
	public class GitReader
	{
		private readonly string fileName;

		public GitReader(string fileName)
		{
			this.fileName = fileName;

		}

		private string DownloadCsv()
		{
			var github = new GitHubClient(new ProductHeaderValue("TimeTableMapper"));
			var userName = "ZameAlex";
			var repository = github.Repository.GetAllForUser(userName).Result.Single(r => r.Name == "TimetableMapper");
			RepositoryContentsClient contentsClient = new RepositoryContentsClient(new ApiConnection(github.Connection));
			var content = contentsClient.GetAllContents(userName, repository.Name, fileName).Result.First();
			return content.Content;
		}

		public Dictionary<string,string> ParseSharedMapping()
		{
			var fileContent = DownloadCsv();
			var keyValue = fileContent.Split(',','\n');
			var result = new Dictionary<string, string>();
			for (int str = 0; str < keyValue.Length-1; str += 2)
			{
				result.Add(keyValue[str], keyValue[str + 1]);
			}
			return result;
		}

		public Dictionary<string, string> Read()
		{
			StreamReader reader = new StreamReader(fileName);
			CsvHelper.CsvReader csvReader = new CsvHelper.CsvReader(reader);
			var result = new Dictionary<string, string>();
			var anonymousTypeDefinition = new
			{
				Key = string.Empty,
				Value = string.Empty
			};
			var r = csvReader.GetRecords(anonymousTypeDefinition);
			foreach(var item in r)
			{
				result.Add(item.Key, item.Value);
			}
			reader.Close();
			return result;
		}

	}
}
