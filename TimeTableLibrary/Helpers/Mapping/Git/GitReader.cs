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
	public class GitReader: Abstracts.AbstractReader, Interfaces.IReader
	{

		private string DownloadCsv()
		{
			var github = new GitHubClient(new ProductHeaderValue("TimeTableMapper"));
			var userName = "ZameAlex";
			var repository = github.Repository.GetAllForUser(userName).Result.Single(r => r.Name == "TimetableMapper");
			RepositoryContentsClient contentsClient = new RepositoryContentsClient(new ApiConnection(github.Connection));
			var content = contentsClient.GetAllContents(userName, repository.Name, Filename).Result.First();
			return content.Content;
		}

		public Dictionary<string,string> ParseMapping()
		{
			var fileContent = DownloadCsv();
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(fileContent);
			writer.Flush();
			stream.Position = 0;
			var result = Read(new StreamReader(stream));

			return result;
		}

	}
}
