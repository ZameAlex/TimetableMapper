using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.Extensions;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.Helpers;

namespace CoreUI.Controllers
{
    public class SubjectsController : Controller
    {
		FpmClient fpmClient;
		RozkladClient rozkladClient;
		ShareMappingService service;

		public SubjectsController(FpmClient fpmClient, RozkladClient rozkladClient, ShareMappingService service)
		{
			this.rozkladClient = rozkladClient;
			this.fpmClient = fpmClient;
			this.service = service;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult SubjectsMapping()
		{
			ViewBag.Subjects = fpmClient.Subjects;
			return View(rozkladClient.Subjects.Select(s=>s.Title).ToList());
		}
		[HttpPost]
		public IActionResult SubjectsMapping(string[] MappedSubjects)
		{
			
			return View();
		}
	}
}