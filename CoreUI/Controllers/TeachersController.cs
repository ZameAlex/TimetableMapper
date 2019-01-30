using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.Helpers;
using TimeTableLibrary.RozkladRequests;

namespace CoreUI.Controllers
{
    public class TeachersController : Controller
    {
		FpmClient fpmClient;
		RozkladClient rozkladClient;
		MappingService service;

		public TeachersController(FpmClient fpmClient, RozkladClient rozkladClient, MappingService service)
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
		public IActionResult TeachersMapping()
		{
			ViewBag.Teachers = fpmClient.Teachers;
			return View(rozkladClient.Teachers.Select(s => s.Name).ToList());
		}
		[HttpPost]
		public IActionResult TeachersMapping(Dictionary<string, string> MappedTeachers)
		{
			service.AddMappedTeachers(MappedTeachers);
			return View();
		}
	}
}