﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeTableLibrary.Extensions;
using Octokit;
using System.Linq;
using TimeTableLibrary.Helpers.Interfaces;

namespace TimeTableLibrary.Helpers.Git
{
	public class GitWriter : Abstracts.AbstractWriter, IWriter
	{
		public string Filename { get; set; }

		public void WriteMapping(Dictionary<string, string> dictionary)
		{
			bool isWriteNessesary = false;
			GitReader reader = new GitReader();
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
				UpdateFileRequest request = new UpdateFileRequest($"{Filename} was changed", Write(parsedResult), contentsClient.GetAllContents(userName, repository.Name, Filename).Result.First().Sha);
				github.Repository.Content.UpdateFile(repository.Id, Filename, request).Wait();
			}

		}

		void IWriter.WriteMapping(Dictionary<string, string> dictionary)
		{
			throw new NotImplementedException();
		}
	}
}