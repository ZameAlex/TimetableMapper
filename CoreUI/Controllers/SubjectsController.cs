using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.Extensions;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.GitHelpers;

namespace CoreUI.Controllers
{
    public class SubjectsController : Controller
    {
		FpmClient fpmClient;
		RozkladClient rozkladClient;
		ShareMappingService checker;

		public SubjectsController(FpmClient fpmClient, RozkladClient rozkladClient, ShareMappingService checker)
		{
			this.rozkladClient = rozkladClient;
			this.fpmClient = fpmClient;
			this.checker = checker;
		}

		public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult SubjectsMapping()
		{
			ViewBag.Subjects = fpmClient.Subjects;
			return View(rozkladClient.Subjects);
		}
		[HttpPost]
		public IActionResult SubjectsMapping(string[] MappedSubjects)
		{
			//checker.Subjects = new Dictionary<TimeTableLibrary.RozkladModels.RozkladSubject, TimeTableLibrary.FpmModels.FpmSubject>
			return View();
		}
	}
}