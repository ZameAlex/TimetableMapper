using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeTableLibrary.Extensions;
using Octokit;
using System.Linq;

namespace TimeTableLibrary.Helpers.Git
{
	public class GitWriter : Abstracts.AbstractWriter, Interfaces.IWriter
	{
		public GitWriter(string filename) : base(filename)
		{
		}

		public void WriteMapping(Dictionary<string, string> dictionary)
		{
			bool isWriteNessesary = false;
			GitReader reader = new GitReader(filename);
			var parsedResult = new Dictionary<string, string>();
			parsedResult = reader.ParseMapping();
			foreach (var item in dictionary)
			{
				if (!parsedResult.ContainsKey(item.Key))
				{
					parsedResult.Add(item);
					isWriteNessesary = true;
				}
			}
			if (isWriteNessesary)
			{
				var github = new GitHubClient(new ProductHeaderValue("TimeTableMapper"));
				github.Connection.Credentials = new Credentials("ZameAlex", "Alexzams4123");
				var userName = "ZameAlex";
				var repository = github.Repository.GetAllForUser(userName).Result.Single(r => r.Name == "TimetableMapper");
				RepositoryContentsClient contentsClient = new RepositoryContentsClient(new ApiConnection(github.Connection));
				UpdateFileRequest request = new UpdateFileRequest($"{filename} was changed", Write(parsedResult), contentsClient.GetAllContents(userName, repository.Name, filename).Result.First().Sha);
				github.Repository.Content.UpdateFile(repository.Id, filename, request).Wait();
			}

		}
	}
}
