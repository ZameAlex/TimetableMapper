using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Octokit;
using TimeTableLibrary.Helpers.Interfaces;

namespace TimeTableLibrary.Helpers.Git
{
	public class GitReader: Interfaces.IReader
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
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(fileContent);
			writer.Flush();
			stream.Position = 0;
			var result = Read(stream);

			return result;
		}

		private Dictionary<string, string> Read(MemoryStream stream)
		{
			StreamReader reader = new StreamReader(stream);
			CsvHelper.CsvReader csvReader = new CsvHelper.CsvReader(reader);
			var result = new Dictionary<string, string>();
			var anonymousTypeDefinition = new
			{
				Key = string.Empty,
				Value = string.Empty
			};
			var r = csvReader.GetRecords(anonymousTypeDefinition);
			foreach (var item in r)
			{
				if (item.Key.ToLower() != "key" && item.Value.ToLower() != "value")
					result.Add(item.Key, item.Value);
			}
			reader.Close();
			return result;
		}

	}
}
