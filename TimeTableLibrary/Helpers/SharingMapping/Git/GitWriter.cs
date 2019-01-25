using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeTableLibrary.Extensions;
using Octokit;
using System.Linq;

namespace TimeTableLibrary.Helpers.Git
{
	public class GitWriter:Interfaces.IWriter
	{
		private readonly string fileName;

		public GitWriter(string fileName)
		{
			this.fileName = fileName;
		}

		public void WriteShareMapping(Dictionary<string,string> dictionary)
		{
			bool isWriteNessesary = false;
			GitReader reader = new GitReader(fileName);
			var parsedResult = new Dictionary<string, string>();
			parsedResult = reader.ParseSharedMapping();
			foreach (var item in dictionary)
			{
				if (!parsedResult.ContainsKey(item.Key))
				{
					parsedResult.Add(item);
					isWriteNessesary = true;
				}
			}
			if(isWriteNessesary)
			{
				var github = new GitHubClient(new ProductHeaderValue("TimeTableMapper"));
				github.Connection.Credentials = new Credentials("ZameAlex", "Alexzams4123");
				var userName = "ZameAlex";
				var repository = github.Repository.GetAllForUser(userName).Result.Single(r => r.Name == "TimetableMapper");
				RepositoryContentsClient contentsClient = new RepositoryContentsClient(new ApiConnection(github.Connection));
				UpdateFileRequest request = new UpdateFileRequest($"{fileName} was changed", Write(parsedResult), contentsClient.GetAllContents(userName, repository.Name, fileName).Result.First().Sha);
				github.Repository.Content.UpdateFile(repository.Id, fileName, request).Wait();
			}

		}

		private string Write(Dictionary<string, string> dictionary)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(writer);
			csvWriter.WriteRecords<KeyValuePair<string, string>>(dictionary);
			//writer.Close();
			stream.Position = 0;
			StreamReader reader = new StreamReader(stream);
			return reader.ReadToEnd();
		}
	}
}
