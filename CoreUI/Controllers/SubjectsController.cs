using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.Extensions;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;

namespace CoreUI.Controllers
{
    public class SubjectsController : Controller
    {
		FpmClient fpmClient;
		RozkladClient rozkladClient;

		public SubjectsController(FpmClient fpmClient, RozkladClient rozkladClient)
		{
			this.rozkladClient = rozkladClient;
			this.fpmClient = fpmClient;
		}

		public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult SubjectsMapping()
		{
			var dictionary = new Dictionary<string, string>();
 			foreach (var item in rozkladClient.Subjects)
			{
				dictionary.Add(item.Title, "");
			}
			ViewBag.Subjects = fpmClient.Subjects;
			return View(dictionary);
		}
		[HttpPost]
		public IActionResult SubjectsMapping(string[] MappedSubjects)
		{
			return View();
		}
	}
}