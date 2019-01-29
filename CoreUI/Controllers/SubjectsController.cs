using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.Extensions;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.Helpers;
using TimeTableLibrary.Helpers.Models;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace CoreUI.Controllers
{
    public class SubjectsController : Controller
    {
		FpmClient fpmClient;
		RozkladClient rozkladClient;
		MappingService service;

		public SubjectsController(FpmClient fpmClient, RozkladClient rozkladClient, MappingService service, IOptions<DefaultFiles> defaultFiles)
		{
			this.rozkladClient = rozkladClient;
			this.fpmClient = fpmClient;
			this.service = service;
			service.LoadData(defaultFiles.Value);
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
		public IActionResult SubjectsMapping(Dictionary<string,string> MappedSubjects)
		{
			service.AddMappedSubjects(MappedSubjects);
			return View();
		}
	}
}