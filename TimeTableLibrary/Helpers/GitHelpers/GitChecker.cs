﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;

namespace TimeTableLibrary.GitHelpers
{
	public class GitChecker
	{
		public Dictionary<RozkladModels.RozkladSubject,FpmModels.FpmSubject> Subjects { get; set; }
		public Dictionary<RozkladModels.RozkladTeacher,FpmModels.FpmTeacher> Teachers { get; set; }

		FpmClient fpmClient;
		RozkladClient rozkladClient;

		public GitChecker(FpmClient fpmClient, RozkladClient rozkladClient)
		{
			this.fpmClient = fpmClient;
			this.rozkladClient = rozkladClient;
			Subjects = new Dictionary<RozkladModels.RozkladSubject, FpmModels.FpmSubject>();
			Teachers = new Dictionary<RozkladModels.RozkladTeacher, FpmModels.FpmTeacher>();
		}

		public bool IsTeacherMapped()
		{
			var dictionary = new GitReader("teachers.csv").ParseSharedMapping();
			foreach(var item in rozkladClient.Teachers)
			{
				if (!dictionary.ContainsKey(item.Name))
					return false;
				else
				{
					Teachers.Add(rozkladClient.Teachers.SingleOrDefault(t => t.Name == item.Name),
						fpmClient.Teachers.SingleOrDefault(t => t.Id == dictionary[item.Name]));
				}
			}
			return true;
		}

		public bool IsSubjectMapped()
		{
			var dictionary = new GitReader("subjects.csv").ParseSharedMapping();
			foreach (var item in rozkladClient.Subjects)
			{
				if (!dictionary.ContainsKey(item.Title))
					return false;
				else
				{
					Subjects.Add(rozkladClient.Subjects.SingleOrDefault(t => t.Title == item.Title),
						fpmClient.Subjects.SingleOrDefault(t => t.Id == dictionary[item.Title]));
				}
			}
			return true;
		}
	}
}