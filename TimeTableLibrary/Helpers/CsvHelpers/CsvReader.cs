using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Octokit;

namespace TimeTableLibrary.CsvHelpers
{
	public class CsvReader
	{
		private readonly string fileName;

		public CsvReader(string fileName)
		{
			this.fileName = fileName;
			var github = new GitHubClient(new ProductHeaderValue("TimeTableMapper"));
			var userName = "ZameAlex";
			var repository = github.Repository.GetAllForUser(userName).Result.Single(r=>r.Name=="TimetableMapper");
			RepositoryContentsClient contentsClient = new RepositoryContentsClient(new ApiConnection(github.Connection));
			var content = contentsClient.GetAllContents(userName, repository.Name,fileName).Result.First();
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
